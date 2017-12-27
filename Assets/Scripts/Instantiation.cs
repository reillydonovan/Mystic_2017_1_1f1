using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                //  GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}