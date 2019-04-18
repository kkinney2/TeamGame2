using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool holdingObj = false;
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
    
}
