using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blob : MonoBehaviour {

    public int resolution = 50;
    public float r = 2;
	// Use this for initialization
	void Start () {
        //we need a mesh filter
        GetComponent<MeshFilter>().mesh = new Mesh();
    }
	
	// Update is called once per frame
	void Update () {
        this.UpdateMesh(GetComponent<MeshFilter>().mesh);
	}

    Mesh UpdateMesh(Mesh m)
    {
        if (m == null)
        {
            m = new Mesh();
        }

        //clear out the old mesh
        m.Clear();

        Vector3[] vectors = new Vector3[(resolution + 1) * (resolution)];
        Vector2[] uvs = new Vector2[(resolution + 1) * (resolution)];

        float seconds = Time.timeSinceLevelLoad;

        // build an array of vectors holding the vertex data
        int vIndex = 0;
         for (int i = 0; i < resolution + 1; i++) {
            float lat = Remap(i, 0, resolution, 0, Mathf.PI);
            for (int j = 0; j < resolution; j++)
            {
              //  x* x +y * y + z * z + sin(4 * x) + sin(4 * y) + sin(4 * z) - 1
                float lon = Remap(j, 0, resolution, 0, Mathf.PI * 2);
                float x = r * Mathf.Sin(lat) * Mathf.Cos(lon);
                float y = r * Mathf.Sin(lat) * Mathf.Sin(lon);
                float z = r * Mathf.Cos(lat);

                uvs[vIndex] = new Vector2(((j * 1.0f) / (resolution + 1)), ((i * 1.0f) / (resolution + 1)));
                vectors[vIndex++] = new Vector3(x, y, z);
            }
        }
        m.vertices = vectors;
        m.uv = uvs;

        //assign triangles - these take the form of 'triangle strips' wrapping the
        // circumference of the sphere
        //there is room to optimise this by not recalculating/reassigning if the 
        //count of the vertecies hasn't changed because the topology will still
        // be the same.
        int triCount = 2 * (resolution + 1) * (resolution);
        int[] triIndecies = new int[triCount * 3];
        int curTriIndex = 0;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                int ul = i * resolution + j;//"upper left" vert
                int ur = i * resolution + ((j + 1) % resolution);//"upper right" vert
                int ll = (i + 1) * resolution + j;//"lower left" vert
                int lr = (i + 1) * resolution + ((j + 1) % resolution); //"lower right" vert
                                                                      //triangle one
                triIndecies[curTriIndex++] = ul;
                triIndecies[curTriIndex++] = ll;
                triIndecies[curTriIndex++] = ur;

                //triangle two
                triIndecies[curTriIndex++] = ll;
                triIndecies[curTriIndex++] = lr;
                triIndecies[curTriIndex++] = ur;
            }
        }
        m.triangles = triIndecies;
        //use the triangle info to calculate vertex normals so we dont have to B)
        m.RecalculateNormals();
        return m;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireMesh(UpdateMesh(null), transform.position, transform.rotation, transform.localScale);

    }

    float Remap(float val, float srcMin, float srcMax, float dstMin, float dstMax)
    {
        if (val >= srcMax) return dstMax;
        if (val <= srcMin) return dstMin;
        return dstMin + (val - srcMin) / (srcMax - srcMin) * (dstMax - dstMin);
    }
}
