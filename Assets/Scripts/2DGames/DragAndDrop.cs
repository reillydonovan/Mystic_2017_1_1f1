using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    Vector3 originalPosition;
	// Use this for initialization
	void Start () {

        originalPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Drag()
    {
        //GameObject.Find("Image").transform.position = Input.mousePosition;
        gameObject.transform.position = Input.mousePosition;
        print("Dragging" + gameObject.name);
    }

    public void Drop()
    {
        CheckMatch();
        
    }

    public void CheckMatch()
    {
        //GameObject ph1 = GameObject.Find("PH1");
        //GameObject img = GameObject.Find("Image");

        GameObject img = gameObject;
        string tag = gameObject.tag;

        GameObject ph1 = GameObject.Find("PH" + tag);

        float distance = Vector3.Distance(ph1.transform.position, img.transform.position);

        if (distance <= 50) Snap(img, ph1);
        else MoveBack();
    }

    public void MoveBack()
    {
        transform.position = originalPosition;
    }

    public void Snap(GameObject img, GameObject ph)
    {
        img.transform.position = ph.transform.position;
    }
}
