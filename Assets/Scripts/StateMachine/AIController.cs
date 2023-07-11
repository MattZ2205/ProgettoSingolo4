using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform[] storagePoints;
    [SerializeField] Transform cookingPoint;
    [SerializeField] Transform sleepPoint;

    NavMeshAgent agent;
    Inventory inv;
    Routines rout;
    AIState currentState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        inv = GetComponent<Inventory>();
        rout = GetComponent<Routines>();
        currentState = new Waiting(gameObject, agent, inv, cookingPoint, sleepPoint, storagePoints, rout);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }
}
