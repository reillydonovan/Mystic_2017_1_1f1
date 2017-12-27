using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(Vector3.right * Time.deltaTime);
        this.gameObject.transform.position = new Vector3(Mathf.Sin(Time.time * .1f), 0f, 2f);
	}
}
