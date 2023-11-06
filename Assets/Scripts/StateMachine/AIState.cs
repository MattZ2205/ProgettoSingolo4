using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AIState : MonoBehaviour
{
    public enum State
    {
        Cooking,
        Walking,
        Filling,
        Sleeping
    }

    public enum Event
    {
        Enter,
        Updating,
        Exit
    }

    public State action;
    protected Event state;

    protected GameObject shef;
    protected Routines routines;
    protected Inventory inventory;
    protected NavMeshAgent agent;
    protected Transform cookingPoint;
    protected Transform sleepingPoint;
    protected Transform[] storagePoints;
    protected AIState nextState;

    public AIState(GameObject _shef, NavMeshAgent _agent, Inventory _inventory, Transform _cookinPoint, Transform _sleepingPoints, Transform[] _storagePoints, Routines _routines)
    {
        state = Event.Enter;

        shef = _shef;
        agent = _agent;
        inventory = _inventory;
        cookingPoint = _cookinPoint;
        sleepingPoint = _sleepingPoints;
        storagePoints = _storagePoints;
        routines = _routines;
    }

    public virtual void Enter() => state = Event.Updating;

    public virtual void Updating() => state = Event.Updating;

    public virtual void Exit() => state = Event.Exit;

    public AIState Process()
    {
        if (state == Event.Enter) Enter();
        else if (state == Event.Updating) Updating();
        else if (state == Event.Exit)
        {
            Exit();
            return nextState;
        }

        return this;
    }
}
