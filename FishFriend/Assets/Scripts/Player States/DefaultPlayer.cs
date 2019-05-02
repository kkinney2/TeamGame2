﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayer : IState
{
    PlayerController owner;
    bool lookingAtPickUp = false;
    GameObject targetPickUp;

    public float moveSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float sprintSpeed = 10f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private float speed;
    float horizontalMove;
    float verticalMove;

    public DefaultPlayer(PlayerController newOwner)
    {
        this.owner = newOwner;
    }

    public void Enter()
    {
        Debug.Log("Entering DefaultPlayer State");
        charController = owner.GetComponent<CharacterController>();
    }

    public void Execute()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        // Determine if/what pickup is being looked at
        CheckForPickUps();

        if (lookingAtPickUp)
        {
            // Toggle UI to signify PickUp

            if (Input.GetKeyUp(KeyCode.E))
            {
                PickUpObject(targetPickUp);
            }
        }

        Movement();
    }

    public void Exit()
    {
        Debug.Log("Exiting DefaultPlayer State");
    }

    void Movement()
    {
        // <<-----------------------------------------------------------------------------**
        // Determines Player Speed
        // **-------------------------------------** 

        if (Input.GetButton("Sprint") && charController.isGrounded)
        {
            speed = sprintSpeed;
        }
        else if (speed == sprintSpeed && !charController.isGrounded)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = moveSpeed;
        }

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Determines if Player "isGrounded"
        // **-------------------------------------**

        if (charController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Generate moveDirection from user input
        // Calculate user input to local direction
        // **-------------------------------------**

        float moveY = moveDirection.y;
        moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        moveDirection = owner.transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
        moveDirection.y = moveY;

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Apply gravity and Move Controller
        // **-------------------------------------**

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);

        // **----------------------------------------------------------------------------->>
    }

    void CheckForPickUps()
    {

    }

    void PickUpObject(GameObject targetObj)
    {

    }

    void ThrowPickUp()
    {

    }
}
