using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ToggleColliders : MonoBehaviour
{

    public GameObject[] toggleCollider;
    bool toggle;

    void Start()
    {
        toggle = false;
    }   
    // Use this for initialization
    public void ColliderToToggle()
    {
        for (int i = 0; i < toggleCollider.Length; i++)
        {
            toggle = !toggle;
            toggleCollider[i].GetComponent<BoxCollider>().enabled = toggle;
        }

    }
}
