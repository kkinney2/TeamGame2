﻿using System.Collections;
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

        // **----------------------------------------------------------------------------->>
    }

    public void ResetCameraPos()
    {
        SetCameraPos(3, new Vector3(0, 1.5f, 0));
    }

    public void SetCameraPos(float distToTarget, Vector3 newOffset)
    {
        distanceFromTarget = distToTarget;
        offset = newOffset;
    }

    public void SetTargetCameraPos(float distToTarget, Vector3 newOffset)
    {
        StartCoroutine(LerpCamera(distToTarget, newOffset));
    }

    IEnumerator LerpCamera(float newDistToTarget, Vector3 newOffset)
    {
        StopCoroutine("LerpCamera");
        float t = 0;
        float tempDist;
        Vector3 tempOffset;
        while (true)
        {
            tempDist = Mathf.Round(Mathf.Lerp(distanceFromTarget * 1000f, newDistToTarget * 1000f, t)) / 1000f;
            tempOffset = new Vector3
                (
                    Mathf.Round(Mathf.Lerp(offset.x * 1000f, newOffset.x * 1000f, t)) / 1000f,
                    Mathf.Round(Mathf.Lerp(offset.y * 1000f, newOffset.y * 1000f, t)) / 1000f,
                    Mathf.Round(Mathf.Lerp(offset.z * 1000f, newOffset.z * 1000f, t)) / 1000f
                );

            t += 0.01f;
            if (t >= 1f)
            {
                t = 0;
                break;
            }

            /* TODO: Debug Camera Lerping 
            Debug.Log("TempDist: " + tempDist);
            Debug.Log("TempOffset: " + tempOffset);
            */

            SetCameraPos(tempDist, tempOffset );

            yield return new WaitForSeconds(0.05f);
        }
        yield break;
    }
}
   