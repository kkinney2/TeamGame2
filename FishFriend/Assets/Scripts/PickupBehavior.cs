using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour {

    bool isHeld = false;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        // <<-----------------------------------------------------------------------------**
        // Switches Rigidbody Constraints based on bool "isHeld"
        // **----------------------------------------**

        if (isHeld)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;

        }

        // **----------------------------------------------------------------------------->>
    }

    public void ToggleBeingHeld()
    {
        isHeld = !isHeld;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Probe"))
        {
            other.GetComponent<ProbeBehavior>().SetObj(gameObject);
            Debug.Log(gameObject.name + " told Probe is Pickup");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Probe"))
        {
            other.GetComponent<ProbeBehavior>().SetObj(null);
        }
    }

    public void Throw(Vector3 direction, float power)
    {
        rb.AddForce(direction * power, ForceMode.Impulse/*Or ForceMode.ChangeVelocity*/);
    }
}
