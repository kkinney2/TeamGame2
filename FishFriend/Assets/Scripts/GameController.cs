using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // <<-----------------------------------------------------------------------------**
        // Quits application on Unity defined "Cancel" key(s)
        // **-------------------------------------------------**

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        // **----------------------------------------------------------------------------->>
    }
}
