using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMatAlpha : MonoBehaviour {

    public float cutOffValue;
    public bool isVisible = true;

    Renderer renderer;
    Coroutine currentCoroutine;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isVisible)
        {
            SetAlpha(cutOffValue);
        }
        else SetAlpha(1f);
    }

    void SetAlpha(float newAlpha)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(LerpAlpha(newAlpha));
    }

    IEnumerator LerpAlpha(float targetAlpha)
    {
        //Debug.Log("LerpAlpha Coroutine Started");

        float t = 0f;
        float tempAlpha;
        float rounder = 10000f;
        while (true)
        {
            tempAlpha = Mathf.Round(Mathf.Lerp(renderer.material.GetFloat("_Cutoff") * rounder, targetAlpha * rounder, t)) / rounder;

            
            t += 0.1f;
            if (t >= 1f)
            {
                t = 0;
                break;
            }

            renderer.material.SetFloat("_Cutoff", tempAlpha);

            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("LerpAlpha Coroutine Ended");
        yield break;
    }

    public void ToggleVisible()
    {
        isVisible = !isVisible;
    }
}
