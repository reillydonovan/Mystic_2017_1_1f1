using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sketch : MonoBehaviour {

	public GameObject plane;
	public bool isJitter = true;

	Mesh mesh;

	List<Vector3> verts = new List<Vector3>();

	public float jitterRange = 0.1f;
	// Use this for initialization
	void Start () {
	
		mesh = plane.GetComponent<MeshFilter>().mesh;
		Debug.Log ("The plane has " + mesh.vertexCount + "vertices");
		UpdateVertices ();
	}

	void UpdateVertices() {
		verts.Clear ();
		for (int i = 0; i < mesh.vertexCount; i++) {
			Vector3 newPos = mesh.vertices [i];

			int col = i % 10;
			//newPos.y += Random.Range (-jitterRange, jitterRange);
			//newPos.y += (0 - newPos.y) * .12f;

			newPos.y = 0.2f * Mathf.Sin (col * Time.frameCount * .01f);

			if (isJitter) {
				newPos.y += Random.Range (-0.02f, 0.02f);
			}
			verts.Add (newPos);
		}
		mesh.SetVertices (verts);
		mesh.RecalculateNormals();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateVertices ();
		
	}
}
