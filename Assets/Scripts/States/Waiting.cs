using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiting : AIState
{
    public Waiting(GameObject _shef, NavMeshAgent _agent, Inventory _inventory, Transform _cookingPoint, Transform _sleepingPoint, Transform[] _storagePoints, Routines _routines)
        : base(_shef, _agent, _inventory, _cookingPoint, _sleepingPoint, _storagePoints, _routines)
    {
        state = Event.Enter;

        shef = _shef;
        agent = _agent;
        inventory = _inventory;
        cookingPoint = _cookingPoint;
        sleepingPoint = _sleepingPoint;
        storagePoints = _storagePoints;
        routines = _routines;

        CookManager.onOrdination += PlateOrdered;
    }

    private void OnDisable()
    {
        CookManager.onOrdination -= PlateOrdered;
    }

    void PlateOrdered(Plate plate)
    {
        nextState = new Cooking(shef, agent, inventory, cookingPoint, sleepingPoint, storagePoints, routines, plate);
        state = Event.Exit;
        CookManager.onOrdination -= PlateOrdered;
    }
}
