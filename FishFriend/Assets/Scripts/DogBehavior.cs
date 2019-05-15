using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogBehavior : PickupBehavior {

    public GameObject Player;
    public float StoppingRadius;

    NavMeshAgent agent; // Has velocity var
    Animator animator;
    


    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.stoppingDistance = StoppingRadius;

        agent.SetDestination(Player.transform.position);
        
    }

    private void Update()
    {
        animator.SetBool("isRunning", false);
        //Debug.Log("Dog Velocity: " + agent.velocity);

        agent.SetDestination(Player.transform.position);

        /*
        if (IsGrounded() && agent.velocity.magnitude > 0f)
        {
            animator.SetBool("isRunning", true);
        }


        if (IsGrounded())
        {
            Debug.Log("Running while grounded");
            animator.SetBool("isRunning", true);
        }*/

        if (agent.velocity.magnitude > 0f)
        {
            Debug.Log("Running while isMoving");
            animator.SetBool("isRunning", true);
        }
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
