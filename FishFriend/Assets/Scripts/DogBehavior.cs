using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogBehavior : PickupBehavior {

    public GameObject Player;
    public GameObject AnimDogo;
    public float StoppingRadius;

    NavMeshAgent agent;

    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = StoppingRadius;

        agent.SetDestination(Player.transform.position);
    }

    private void Update()
    {
        agent.SetDestination(Player.transform.position);
    }

    public void Warp(Vector3 newPos)
    {
        agent.Warp(newPos);
    }

}
