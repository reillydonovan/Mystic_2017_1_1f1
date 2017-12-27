using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexRandomOffset : MonoBehaviour
{
    float vertexAnimOffset;
    // public float minScale;
    // public float maxScale;

    // Use this for initialization
    void Start()
    {
        vertexAnimOffset = Random.Range(0, 10);
        this.gameObject.GetComponent<Renderer>().material.SetFloat("_VertexAnimOffset", vertexAnimOffset);

        /*
          GameObject[] allShrooms = GameObject.FindGameObjectsWithTag("Mushroom");
   float vertexAnimOffset;
        //tankSize = tankSize * tankScale;
        foreach (GameObject shroom in allShrooms)
        {
            vertexAnimOffset = Random.Range(0, 10);
            shroom.GetComponent<Renderer>().material.SetFloat("_VertexAnimOffset", vertexAnimOffset);
        }
        */

        /*
        allShrooms = new GameObject[numShrooms];
        for (int i = 0; i < numShrooms; i++)
        {
            Vector3 pos = new Vector3(Random.Range(transform.position.x - tankSize, transform.position.x + tankSize),
                -1.68f,
                Random.Range(transform.position.z - tankSize, transform.position.z + tankSize));
            allShrooms[i] = (GameObject)Instantiate(mushroomPrefab, pos, Quaternion.identity);

            vertexAnimOffset = Random.Range(0, 10);
            allShrooms[i].GetComponent<Renderer>().material.SetFloat("_VertexAnimOffset", vertexAnimOffset);
            allShrooms[i].transform.localScale *= Random.Range(minScale, maxScale);

        }
        */
    }
    // Update is called once per frame
    void Update()
    {

    }
}

