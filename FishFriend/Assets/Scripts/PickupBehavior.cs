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
        if (isHeld)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            /*// TODO: Frozen rotation is hardcoded
            transform.rotation = Quaternion.identity;
            transform.Rotate(-90, 0, 0);
            rb.freezeRotation = true;*/
        }
        else
        {
            /*rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = false;*/
        }
	}

    public void ToggleBeingHeld()
    {
        isHeld = !isHeld;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Probe"))
        {
            other.GetComponent<ProbeBehavior>().SetObj(this.gameObject);
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
}
