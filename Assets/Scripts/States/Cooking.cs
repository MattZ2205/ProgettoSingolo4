using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Cooking : AIState
{
    Plate plateToCook;
    bool alreadyCooking = false;

    public delegate void PlateComplete();
    public static PlateComplete onPlateCompleted;

    public Cooking(GameObject _shef, NavMeshAgent _agent, Inventory _inventory, Transform _cookingPoint, Transform _sleepingPoint, Transform[] _storagePoints, Routines _routines, Plate _plateToCook)
        : base(_shef, _agent, _inventory, _cookingPoint, _sleepingPoint, _storagePoints, _routines)
    {
        state = Event.Enter;
        action = State.Walking;

        shef = _shef;
        agent = _agent;
        inventory = _inventory;
        cookingPoint = _cookingPoint;
        sleepingPoint = _sleepingPoint;
        storagePoints = _storagePoints;
        plateToCook = _plateToCook;
        routines = _routines;
    }

    void Move()
    {
        if (Vector3.Distance(shef.transform.position, cookingPoint.position) > 0.1f)
        {
            agent.SetDestination(cookingPoint.position);
        }
    }

    void Cook()
    {
        if (Vector3.Distance(shef.transform.position, cookingPoint.position) > 0.1f) return;
        if (!alreadyCooking)
        {
            alreadyCooking = true;
            action = State.Cooking;
            List<Item> takingItem = new List<Item>();
            for (int i = 0; i < plateToCook.ingredients.Count; i++)
            {
                if (!inventory.CanTakeIngredient(plateToCook.ingredients[i].ing, plateToCook.ingredients[i].quantity))
                {
                    takingItem.Add(plateToCook.ingredients[i].ing);
                }
            }

            if (takingItem.Count > 0)
            {
                nextState = new Filling(shef, agent, inventory, cookingPoint, sleepingPoint, storagePoints, routines, takingItem, plateToCook);
                state = Event.Exit;
            }
            else
            {
                for (int i = 0; i < plateToCook.ingredients.Count; i++)
                {
                    inventory.TakeIngredient(plateToCook.ingredients[i].ing, plateToCook.ingredients[i].quantity);
                }
                routines.Cook(plateToCook.cookingTime);
            }
        }

        if (routines.isC)
        {
            routines.isC = false;
            alreadyCooking = false;
            nextState = new Waiting(shef, agent, inventory, cookingPoint, sleepingPoint, storagePoints, routines);
            state = Event.Exit;
            onPlateCompleted?.Invoke();
        }
    }

    public override void Enter()
    {
        Move();

        base.Enter();
    }

    public override void Updating()
    {
        Cook();
    }
}
