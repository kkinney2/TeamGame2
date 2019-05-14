using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : IState
{
    public float gravity = 10.0f;

    PlayerController owner;
    Animator animator;
    CharacterController charController;
    Camera mainCamera;

    public Aiming(PlayerController newOwner, Animator newAnimator)
    {
        this.owner = newOwner;
        animator = newAnimator;
    }

    public void Enter()
    {
        Debug.Log("Entering Aiming State");

        // <<-----------------------------------------------------------------------------**
        // Assigns Camera
        // **----------------------------------------**

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        else
        {
            Debug.Log("No Main Camera Found");
        }

        // **----------------------------------------------------------------------------->>

        charController = owner.GetComponent<CharacterController>();

        owner.ToggleReticle();
    }

    public void Execute()
    {
        animator.SetBool("isThrowing", false);
        // <<-----------------------------------------------------------------------------**
        // Applies Gravity to Player
        // **----------------------------------------**

        Vector3 newGrav = new Vector3(0, 0f - (gravity * Time.deltaTime), 0);
        charController.Move(newGrav);

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Throw Stuff
        // **----------------------------------------**

        if (Input.GetMouseButtonDown(0) && owner.IsHoldingObj())
        {
            animator.SetBool("isThrowing", true);
            Throw();
        }

        // **----------------------------------------------------------------------------->>
    }

    public void Exit()
    {
        Debug.Log("Exiting Aiming State");

        owner.ToggleReticle();
    }

    void Throw()
    {
        //Gets Held Obj
        GameObject heldObj = owner.GetHeldObj();

        // <<-----------------------------------------------------------------------------**
        // Prepares Held Obj for Throwing and Throws Obj
        // **----------------------------------------**
        PickupBehavior objPickupBehaviour = heldObj.GetComponent<PickupBehavior>();

        owner.IsHoldingObj(false);
        owner.probeController.SetObj(null);
        // De-child's the held obj
        heldObj.transform.SetParent(null);
        // Unlocks the rotation and position
        objPickupBehaviour.ToggleBeingHeld();

        // <<-----------------------------------------------------------------------------**
        // Determines Vector to Target, and Normalize that Vector
        // **----------------------------------------**

       

        Vector3 targetDir = (owner.Reticle.transform.position + owner.AdditionalThrowHeight) - heldObj.transform.position;
        //targetDir = targetDir.normalized;
        Vector3.Normalize(targetDir);

        // **----------------------------------------------------------------------------->>

        objPickupBehaviour.Throw(targetDir, owner.throwPower);

        // **----------------------------------------------------------------------------->>

    }
}
