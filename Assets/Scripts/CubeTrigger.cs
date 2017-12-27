using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour {

	public Material[] material;
	public GameObject ChangeObject;
	Renderer rend;

	void Start(){
		rend = ChangeObject.GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material [0];
	}

	void OnTriggerEnter(Collider col){
		switch (col.tag){
		case "MainCamera":
			Debug.Log ("Object entered the trigger");
			rend.sharedMaterial = material [1];	
			break;
		}
	}

	void OnTriggerStay(Collider col){
		switch (col.tag){
		case "MainCamera":
			Debug.Log ("Object is within the trigger");
			break;
		}
	}

	void OnTriggerExit (Collider col){
		switch (col.tag){
		case "MainCamera":
			Debug.Log ("Object exited the trigger");
			rend.sharedMaterial = material [0];
			break;
		}
	}
}
