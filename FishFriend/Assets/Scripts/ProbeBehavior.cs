using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeBehavior : MonoBehaviour {

    GameObject obj;

    public void SetObj(GameObject temp)
    {
        obj = temp;
    }

    public GameObject GetObj()
    {
        return obj;
    }

}
