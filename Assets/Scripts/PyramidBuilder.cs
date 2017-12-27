using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidBuilder : MonoBehaviour {

	GameObject Pyramid;

	List<Vector3> verts = new List<Vector3> ();
	List<int> tris = new List<int> ();
	List<Vector2> uvs = new List<Vector2>();




	public Material material;
	// Use this for initialization
	void Start () {
		Pyramid = new GameObject ();
		Pyramid.AddComponent<MeshFilter> ();
		Pyramid.AddComponent<MeshRenderer> ();
		Pyramid.GetComponent<MeshRenderer> ().material = material;
		BuildPyramid ();
	}

	void BuildPyramid(){

		//FACE 1
		verts.Add (new Vector3 (0, 0, 0));
		verts.Add (new Vector3 (.5f, 1.0f, 0.5f));
		verts.Add (new Vector3 (1, 0, 0));

		tris.Add (0);
		tris.Add (1);
		tris.Add (2);

		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (0.5f, 0));
		uvs.Add (new Vector2 (1, 1));

		//FACE 2
		verts.Add (new Vector3 (1, 0, 0));
		verts.Add (new Vector3 (.5f, 1.0f, 0.5f));
		verts.Add (new Vector3 (1, 0, 1));

		tris.Add (3);
		tris.Add (4);
		tris.Add (5);

		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (0.5f, 0));
		uvs.Add (new Vector2 (1, 1));

		//FACE 3
		verts.Add (new Vector3 (1, 0, 1));
		verts.Add (new Vector3 (.5f, 1.0f, 0.5f));
		verts.Add (new Vector3 (0, 0, 1));

		tris.Add (6);
		tris.Add (7);
		tris.Add (8);

		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (0.5f, 0));
		uvs.Add (new Vector2 (1, 1));

		//FACE 4
		verts.Add (new Vector3 (0, 0, 1));
		verts.Add (new Vector3 (.5f, 1.0f, 0.5f));
		verts.Add (new Vector3 (0, 0, 0));

		tris.Add (9);
		tris.Add (10);
		tris.Add (11);

		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (0.5f, 0));
		uvs.Add (new Vector2 (1, 1));

		for (int i = 0; i < verts.Count; i++) {
			Vector3 shift = verts [i];
			shift.x -= 0.5f;
			shift.z -= 0.5f;
			verts [i] = shift;
		}

		Mesh mesh = new Mesh();

		mesh.SetVertices (verts);
		mesh.triangles = tris.ToArray ();
		mesh.uv = uvs.ToArray ();

		Pyramid.GetComponent<MeshFilter>().mesh = mesh;
		Pyramid.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		Pyramid.transform.Translate (0, 1, 0);
	}
	// Update is called once per frame
	void Update () {
		Pyramid.transform.Rotate (0, 1f, 0);
	}
}
