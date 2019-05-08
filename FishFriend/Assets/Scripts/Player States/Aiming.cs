using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : IState
{
    public float gravity = 10.0f;

    PlayerController owner;
    CharacterController charController;
    Camera mainCamera;

    public Aiming(PlayerController newOwner)
    {
        this.owner = newOwner;
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
        // <<-----------------------------------------------------------------------------**
        // Gets Held Obj, Determines Vector to Target, and Normalize that Vector
        // **----------------------------------------**

        GameObject heldObj = owner.GetHeldObj();

        Vector3 targetDir = (owner.Reticle.transform.position + owner.AdditionalThrowHeight) - heldObj.transform.position;
        //targetDir = targetDir.normalized;
        Vector3.Normalize(targetDir);

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Prepares Held Obj for Throwing and Throws Obj
        // **----------------------------------------**
        PickupBehavior objPickup = heldObj.GetComponent<PickupBehavior>();

        owner.IsHoldingObj(false);
        // De-child's the held obj
        heldObj.transform.SetParent(null);
        // Unlocks the rotation and position
        objPickup.ToggleBeingHeld();

        objPickup.Throw(targetDir, owner.throwPower);

        // **----------------------------------------------------------------------------->>

    }
}
