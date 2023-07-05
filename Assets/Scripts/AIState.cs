using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SceneManagement;
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

    protected NavMeshAgent agent;
    protected AIState nextState;
    protected Transform[] points;

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
