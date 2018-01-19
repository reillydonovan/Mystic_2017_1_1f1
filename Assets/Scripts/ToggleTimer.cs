using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTimer : MonoBehaviour
{

    public GameObject toggleGameObject;
    bool toggle;

    void Start()
    {

        toggle = false;
    }
    // Use this for initialization
    public void ObjectToToggle()
    {
        toggle = !toggle;
        toggleGameObject.SetActive(toggle);

    }

    public void ToggleOn()
    {
        toggleGameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        toggleGameObject.SetActive(false);
    }
}