using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {

   // public GameObject rotateTarget;

    public float rotateSpeed = 1.0f;
    public Vector3 spinSpeed = Vector3.zero;
    public Vector3 spinAxis = new Vector3(0, 1, 0);
  //  private Vector3 rotateTargetPosition;

	void Start () {

        //SetSize(2.0f); Called from external script
       // this.transform.position = new Vector3(0, 5, 3); The same deal as above
        spinSpeed = new Vector3(Random.value, Random.value, Random.value);
        spinAxis = Vector3.up;
        spinAxis.x = (Random.value - Random.value) * .1f;

	}

    public void SetSize(float size)
    {
        this.transform.localScale = new Vector3(size, size, size);
    }

    // Update is called once per frame
    void Update () {
      //  rotateTargetPosition = rotateTarget.transform.position;
        this.transform.Rotate(spinSpeed);
        this.transform.RotateAround(Vector3.zero, spinAxis, rotateSpeed);

	}


}
