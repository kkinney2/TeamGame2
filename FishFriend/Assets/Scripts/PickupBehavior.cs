using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour {

    public float distToGround;

    bool isHeld = false;
    bool isGrounded = false;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ToggleBeingHeld()
    {
        isHeld = !isHeld;
    }

    public bool IsHeld()
    {
        return isHeld;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Throw(Vector3 direction, float power)
    {
        rb.AddForce(direction * power, ForceMode.Impulse/*Or ForceMode.ChangeVelocity*/);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
