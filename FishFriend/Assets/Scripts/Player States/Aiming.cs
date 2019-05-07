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

        // References camera in scene tagged 'MainCamera'
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        else
        {
            Debug.Log("No Main Camera Found");
        }
        
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
        GameObject heldObj = owner.GetHeldObj();

        Vector3 targetDir = owner.Reticle.gameObject.transform.position - heldObj.transform.position;
        targetDir = targetDir.normalized;

        Rigidbody objRb = heldObj.GetComponent<Rigidbody>();

        objRb.AddRelativeForce(targetDir);

    }
}
