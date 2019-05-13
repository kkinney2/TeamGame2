using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public float mouseSensitivity = 10f;
    public float distanceFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-30, 85);
    public float acceleration = .12f;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    Coroutine currentCamCoroutine_Pos;
    Coroutine currentCamCoroutine_Target;
    Transform target;
    Vector3 targetPos;
    Vector3 offset;
    float rounder = 10000f;

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
        SetPos(3, new Vector3(0, 1.5f, 0));
    }

    public void SetPos(float distToTarget, Vector3 newOffset)
    {
        distanceFromTarget = distToTarget;
        offset = newOffset;
    }

    public void SetTargetCameraPos(float distToTarget, Vector3 newOffset)
    {
        if (currentCamCoroutine_Pos != null)
        {
            StopCoroutine(currentCamCoroutine_Pos);
        }
        currentCamCoroutine_Pos = StartCoroutine(LerpCamera(distToTarget, newOffset));
    }

    public void SetCamTarget(Transform tempTrans)
    {
        if (currentCamCoroutine_Target != null)
        {
            StopCoroutine(currentCamCoroutine_Target);
        }
        currentCamCoroutine_Target = StartCoroutine(LerpCamTarget(tempTrans));
    }

    public void SetTarget(Vector3 tempPos)
    {
        targetPos = tempPos;
    }

    public void SetTarget(Transform tempTrans)
    {
        target = tempTrans;
    }

    IEnumerator LerpCamTarget(Transform newPos)
    {
        float m = 0;
        Vector3 targetPos = newPos.position;
        Vector3 currentPos = transform.position;
        Vector3 tempPos;

        while (true)
        {
            tempPos = new Vector3
                (
                    Mathf.Round(Mathf.Lerp(currentPos.x * rounder, targetPos.x * rounder, m)) / rounder,
                    Mathf.Round(Mathf.Lerp(currentPos.y * rounder, targetPos.y * rounder, m)) / rounder,
                    Mathf.Round(Mathf.Lerp(currentPos.z * rounder, targetPos.z * rounder, m)) / rounder
                );

            m += 0.01f;
            if (m >= 1f)
            {
                m = 0;
                break;
            }

            SetTarget(tempPos);

            yield return new WaitForEndOfFrame();

        }
        yield break;
    }

    IEnumerator LerpCamera(float newDistToTarget, Vector3 newOffset)
    {
        //Debug.Log("LerpCamera Coroutine Started");

        float t = 0;
        float tempDist;
        Vector3 tempOffset;
        while (true)
        {
            tempDist = Mathf.Round(Mathf.Lerp(distanceFromTarget * rounder, newDistToTarget * rounder, t)) / rounder;
            tempOffset = new Vector3
                (
                    Mathf.Round(Mathf.Lerp(offset.x * rounder, newOffset.x * rounder, t)) / rounder,
                    Mathf.Round(Mathf.Lerp(offset.y * rounder, newOffset.y * rounder, t)) / rounder,
                    Mathf.Round(Mathf.Lerp(offset.z * rounder, newOffset.z * rounder, t)) / rounder
                );

            t += 0.01f;
            if (t >= 1f)
            {
                t = 0;
                break;
            }

            SetPos(tempDist, tempOffset );

            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("LerpCamera Coroutine Ended");
        yield break;
    }
}
   