using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : IState
{
    PlayerController owner;

    public Aiming(PlayerController newOwner)
    {
        this.owner = newOwner;
    }

    public void Enter()
    {
        Debug.Log("Entering Aiming State");
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        Debug.Log("Exiting Aiming State");
    }
}
