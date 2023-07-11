using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class Filling : AIState
{
    List<Item> itemsToTake = new List<Item>();
    Plate plate;

    public Filling(GameObject _shef, NavMeshAgent _agent, Inventory _inventory, Transform _cookingPoint, Transform _sleepingPoint, Transform[] _storagePoints, Routines _routines, List<Item> _itemsToTake, Plate _plate)
        : base(_shef, _agent, _inventory, _cookingPoint, _sleepingPoint, _storagePoints, _routines)
    {
        state = Event.Enter;
        action = State.Walking;

        shef = _shef;
        agent = _agent;
        itemsToTake = _itemsToTake;
        cookingPoint = _cookingPoint;
        sleepingPoint = _sleepingPoint;
        storagePoints = _storagePoints;
        inventory = _inventory;
        plate = _plate;
        routines = _routines;
    }

    void Move()
    {
        if (itemsToTake.Count > 0)
        {
            if (Vector3.Distance(shef.transform.position, storagePoints[inventory.FindItem(itemsToTake[0].itemName)].position) > 0.1f)
            {
                agent.SetDestination(storagePoints[inventory.FindItem(itemsToTake[0].itemName)].position);
            }
        }
    }

    void Fill()
    {


        if (itemsToTake.Count > 0)
        {
            if (Vector3.Distance(shef.transform.position, storagePoints[inventory.FindItem(itemsToTake[0].itemName)].position) > 0.1f) return;
            inventory.FillInventory(itemsToTake[0]);
            routines.Fill();
            if (routines.isF)
            {
                routines.filling = false;
                routines.isF = false;
                itemsToTake.RemoveAt(0);
                Enter();
            }
        }
        else
        {
            nextState = new Cooking(shef, agent, inventory, cookingPoint, sleepingPoint, storagePoints, routines, plate);
            state = Event.Exit;
        }
    }

    public override void Enter()
    {
        Move();

        base.Enter();
    }

    public override void Updating()
    {
        Fill();
    }

    //IEnumerator Move()
    //{
    //    for (int i = 0; i < itemsToTake.Count; i++)
    //    {
    //        Vector3 destination = storagePoints[inventory.FindItem(itemsToTake[i].itemName)].position;
    //        if (Vector3.Distance(shef.transform.position, destination) > 0.1f)
    //        {
    //            agent.SetDestination(destination);
    //            while (Vector3.Distance(destination, transform.position) > 0.1f)
    //            {
    //                yield return null;
    //            }
    //        }
    //        StartCoroutine(inventory.actionFill(itemsToTake[i]));
    //        while (!inventory.IsFilled(itemsToTake[i]))
    //        {
    //            yield return null;
    //        }
    //    }
    //}
}
