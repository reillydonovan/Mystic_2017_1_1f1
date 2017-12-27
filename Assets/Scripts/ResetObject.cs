using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour {

    public GameObject resetTargetObject;
    public GameObject resetObject;
   // private Transform resetTargetTransform;
    bool toggle;

    void Start()
    {

        toggle = false;
    }
    // Use this for initialization
    public void ResetTransform()
    {
        toggle = !toggle;
        resetObject.transform.position = new Vector3(resetTargetObject.transform.position.x, resetObject.transform.position.y, resetTargetObject.transform.position.z);

    }
}
