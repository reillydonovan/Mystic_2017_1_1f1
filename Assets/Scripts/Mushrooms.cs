using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : MonoBehaviour {
    public GameObject mushroomPrefab;
    public float tankSize = 5;
   // public float tankScale = 1;
    public int numShrooms = 20;
    public GameObject[] allShrooms;
    public float vertexAnimOffset;
    public float minScale;
    public float maxScale;

    // Use this for initialization
    void Start()
    {
        //tankSize = tankSize * tankScale;
        
        allShrooms = new GameObject[numShrooms];
        for (int i = 0; i < numShrooms; i++)
        {
            Vector3 pos = new Vector3(Random.Range(transform.position.x - tankSize, transform.position.x + tankSize),
                -1.5f,
                Random.Range(transform.position.z - tankSize, transform.position.z + tankSize));
            allShrooms[i] = (GameObject)Instantiate(mushroomPrefab, pos, Quaternion.Euler(new Vector3(-90, 0, 0)));// Quaternion.identity);


            vertexAnimOffset = Random.Range(0, 10);
            allShrooms[i].GetComponent<Renderer>().material.SetFloat("_VertexAnimOffset", vertexAnimOffset);
            allShrooms[i].transform.localScale *= Random.Range(minScale, maxScale);

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
