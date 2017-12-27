using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSketch : MonoBehaviour {

    public GameObject myPrefab;
    public int totalCubes = 12;
    float totalDistance = 2.5f;


    // Use this for initialization
    void Start () {


        //LINEAR DISTRIBUTION
        /*
        for (int i = 0; i < totalCubes; i++)
        {
            float perc = i / (float)totalCubes;
            float x = perc * totalDistance;
            float y = 5.0f;
            float z = 0.0f;


            var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            newCube.GetComponent<CubeScript>().SetSize(1.0f - perc);
            newCube.GetComponent<CubeScript>().rotateSpeed = perc;
        }
        */
        //SIN DISTRIBUTION
        for (int i = 0; i < totalCubes; i++)
        {
            float perc = i / (float)totalCubes;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);
            float x = 2.0f + sin * totalDistance;
            float y = 5.0f;
            float z = 0.0f;


            var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            newCube.GetComponent<CubeScript>().SetSize(0.5f * (1.0f - perc));
            newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * perc;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
