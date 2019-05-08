using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeBehavior : MonoBehaviour {

    GameObject closestObj;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            closestObj = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        closestObj = null;
    }

    public GameObject GetObj()
    {
        return closestObj;
    }

}
