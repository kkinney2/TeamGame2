using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogBehavior : PickupBehavior {

    public GameObject Player;
    public float StoppingRadius;

    NavMeshAgent agent;
    Animator animator;
    Rigidbody rb;

    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        agent.stoppingDistance = StoppingRadius;

        agent.SetDestination(Player.transform.position);
        
    }

    private void Update()
    {
        agent.SetDestination(Player.transform.position);
        if (IsGrounded() && rb.velocity.magnitude > 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else animator.SetBool("isRunning", false);
        //Debug.Log("Destination: " + agent.destination);
    }

    public void Warp(Vector3 newPos)
    {
        agent.Warp(newPos);
    }

    public void ToggleEnabledNav()
    {
        agent.enabled = !agent.enabled;
    }

}
