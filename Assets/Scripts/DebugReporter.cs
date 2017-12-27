using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugReporter : MonoBehaviour {

    public TextMesh debugTextMesh;
    public string debugInfoString;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        debugInfoString = "Debug Log";
        debugTextMesh.text = debugInfoString;
	}
}
