using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool isHoldingObj = false;
    bool lookingAtPickUp = false;
    GameObject targetPickUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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

        if (holdingObj)
        {
            if (Input.GetMouseButton(1))
            {
                // Toggle "Aiming" UI
            }
        }

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

    public bool IsHoldingObj()
    {
        return isHoldingObj == true ? true : false;
    }
    
}

/*
public class SampleState : IState
{
    OwnerClass owner;

    public void SampleState(OwnerClass newOwner)
    {
        this.owner = newOwner;
    }

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
*/

public class DefaultPlayer : IState
{
    PlayerController owner;

    public void Aiming(PlayerController newOwner)
    {
        this.owner = newOwner;
    }

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}

public class Aiming : IState
{
    PlayerController owner;

    public void Aiming(PlayerController newOwner)
    {
        this.owner = newOwner;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        if (owner.IsHoldingObj())
        {
            if (Input.GetMouseButton(1))
            {
                // Toggle "Aiming" UI
            }
        }
    }

    public void Exit()
    {

    }
}
