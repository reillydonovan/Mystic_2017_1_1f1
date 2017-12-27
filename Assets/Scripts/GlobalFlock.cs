using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {

	public GameObject flockPrefab;
	public GameObject goalPrefab;
	public static int tankSize = 2;

	public static int numFlock = 20;
	public static GameObject[] allFlock = new GameObject [numFlock];

	public static Vector3 goalPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < numFlock; i++) {
			Vector3 pos = new Vector3 (Random.Range (-tankSize, tankSize), Random.Range (0, tankSize), Random.Range (-tankSize, tankSize));
			allFlock [i] = (GameObject)Instantiate (flockPrefab, pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Random.Range (0, 1000) < 5) {

			goalPos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-0,tankSize), Random.Range(-tankSize,tankSize));
			goalPrefab.transform.position = goalPos;
		}

		
	}
}
