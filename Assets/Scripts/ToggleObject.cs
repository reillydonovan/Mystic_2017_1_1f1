using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour {

    public GameObject toggleGameObject;
    bool toggle;

    void Start()
    {

        toggle = false;
    }
	// Use this for initialization
	public void ObjectToToggle () {
        toggle = !toggle;
        toggleGameObject.SetActive(toggle);
     		
	}
}
