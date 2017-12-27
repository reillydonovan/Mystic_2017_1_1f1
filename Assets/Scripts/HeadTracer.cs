using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTracer : MonoBehaviour {
    public GameObject tracer;
    public bool tracerBool = true;
    float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (tracerBool)
        {
            time += Time.deltaTime;

            if (time >= .05)
            {


                time = 0;
                GameObject t = (GameObject)Instantiate(tracer, transform.position, Quaternion.identity);
                Destroy(t, 5);
            }
        }
       
	}

    public void tracerTogger()
    {
        tracerBool = !tracerBool;
     
    }
}
