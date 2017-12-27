using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ToggleTapObject : MonoBehaviour {

    public GameObject[] toggleTapGameObject;
    bool toggle;

    void Start()
    {

        toggle = false;
    }
	// Use this for initialization
	public void ObjectToToggle () {
        for (int i = 0; i < toggleTapGameObject.Length; i++)
        {
            toggle = !toggle;
            toggleTapGameObject[i].GetComponent<TapToPlace>().enabled = toggle;
        }
     		
	}
}
