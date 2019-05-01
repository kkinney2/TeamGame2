using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public float mouseSensitivity = 10;
    public Transform target;
    public Vector3 offset;
    public float distanceFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-30, 85);
    public float acceleration = .12f;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    public bool InvertPitch;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        currentRotation = transform.eulerAngles;
    }

    void LateUpdate()
    {
        // <<-----------------------------------------------------------------------------**
        // Get input data and clamp 
        // **---------------------------------------------**

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;

        if (InvertPitch)
        {
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        else
        {
            pitch += Input.GetAxis("Mouse Y") * -mouseSensitivity;
        }

        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Calculate rotation and apply to camera
        // **---------------------------------------------**

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, acceleration);

        transform.eulerAngles = currentRotation;

        transform.position = target.position + offset - (transform.forward * distanceFromTarget);
        /* TODO: Lerp position so that offset changes arent abrupt
        Vector3 tempNewPos = target.position + offset - (transform.forward * distanceFromTarget);
        Vector3.Lerp(transform.position, tempNewPos, 0.5f);
        */

        // **----------------------------------------------------------------------------->>
    }

    public void ResetCameraPos()
    {
        SetCameraPos(5, new Vector3(0, 1, 0));
    }

    public void SetCameraPos(float distToTarget, Vector3 newOffset)
    {
        distanceFromTarget = distToTarget;
        offset = newOffset;
    }
}
   