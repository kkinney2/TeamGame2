using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : IState
{
    public float gravity = 10.0f;

    PlayerController owner;
    private CharacterController charController;

    public Aiming(PlayerController newOwner)
    {
        this.owner = newOwner;
    }

    public void Enter()
    {
        Debug.Log("Entering Aiming State");
        charController = owner.GetComponent<CharacterController>();
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
        // Raycast for aiming
        // **----------------------------------------**

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log("Ray hit: " + hitInfo.collider.gameObject.name);

            // TODO: Obj following camera for spawning
            // Spawn the Obj
            if (!objSpawned)
            {
                spawnedObj = objToSpawn;
                SpawnObj(spawnedObj, hitInfo.point);
                objSpawned = true;
            }
            else
            {
                spawnedObj.transform.position = hitInfo.point;
            }
        }
        // **----------------------------------------------------------------------------->>
    }

    public void Exit()
    {
        Debug.Log("Exiting Aiming State");
    }
}
