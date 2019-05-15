using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SphereCollider))]
public class TargetEventTrigger : MonoBehaviour {

    [Tooltip("Object with an animator")]
    public Animator objectToAnimate;
    [Tooltip("The tag on an object to detect")]
    [SerializeField]
    private string tagNameToDetect = "Pickup";
    [Header("Optional")]
    [Tooltip("Use this to enable an object when the event is triggered")]
    public GameObject objectToEnable;

    void Start()
    {
        objectToAnimate.enabled = false;
        SphereCollider sphereColl = GetComponent<SphereCollider>();
        sphereColl.radius = 0.5f;
        sphereColl.isTrigger = true;

        if (objectToEnable != null)
            objectToEnable.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagNameToDetect)
        {
            objectToAnimate.enabled = true;

            if (objectToEnable != null)
                objectToEnable.SetActive(true);

            print("TARGET HIT: WOOOO!");
        }
    }
}
