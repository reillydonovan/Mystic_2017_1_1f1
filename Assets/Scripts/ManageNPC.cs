using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageNPC : MonoBehaviour {
    public int health = 100;
    public GameObject smoke;

	// Use this for initialization
	void Start () {
		
	}

 

    public void gotHit()
    {
        health -= 50;
    }

    public void Destroy()
    {
        GameObject lastSmoke = (GameObject)(Instantiate(smoke, transform.position, Quaternion.identity));
        Destroy(lastSmoke, 3);
        Destroy(gameObject);
   
    }
    void Update()
    {
        if (health <= 0) Destroy();
    }
}
