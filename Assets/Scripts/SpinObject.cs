using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour {

	public float rotateSpeed = 0.5f;

	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround (Vector3.zero, Vector3.up, rotateSpeed);
		this.transform.LookAt (Vector3.zero);
	}
}
