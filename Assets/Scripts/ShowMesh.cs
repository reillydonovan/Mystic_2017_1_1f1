using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ShowMesh : MonoBehaviour {

	public GameObject[] meshObject;
	bool toggle;

	void Start()
	{

		toggle = false;
	}
	// Use this for initialization
	public void MeshToShow () {
		for (int i = 0; i < meshObject.Length; i++)
		{
			toggle = !toggle;
			meshObject[i].GetComponent<MeshRenderer>().enabled = toggle;
		}

	}
}