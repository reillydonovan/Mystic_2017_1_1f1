using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleShape : MonoBehaviour
{
    public int resolution = 50;

    float umin = -1 * Mathf.PI;
    float umax = Mathf.PI;
    float vmin = -1 * Mathf.PI;
    float vmax = Mathf.PI;

    public float a = 0.0f;
    public float u = 2.0f;
    public float v = 0.0f;
    public float r = 50.0f;

    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;

    void Start()
    {
        //we need a mesh filter
        GetComponent<MeshFilter>().mesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMesh(GetComponent<MeshFilter>().mesh);
    }

    Mesh UpdateMesh(Mesh m)
    {
        if (m == null)
        {
            m = new Mesh();
        }
        m.Clear();

        Vector3[] vectors = new Vector3[(resolution + 1) * (resolution + 1)];
        Vector2[] uvs = new Vector2[(resolution + 1) * (resolution + 1)];

        float seconds = Time.timeSinceLevelLoad;
        
        // build an array of vectors holding the vertex data
        int vIndex = 0;
        for (int i = 0; i < resolution + 1; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                u = umin + i * (umax - umin) / resolution;
                v = vmin + j * (vmax - vmin) / resolution;

                //the get radius function is where 'hamonics' are added
                //  m1 = GetRadius(lonDivs, latDivs, seconds);

                //add uvs so that we can texture the mesh if we want
               uvs[vIndex] = new Vector2(j * 1.0f / resolution, i * 1.0f / resolution);

                //create a vertex 
                //optimization alert: since the only thing that changes here is the radius
                //(when the number of divisions stays the same) we could cache these numbers
                // and use a shader to create and apply the variations in radius and compute 
                // the normals.
             
                    x = r *  ((-1 * 2/15) * Mathf.Cos(u)) * (3 * Mathf.Cos(v) - 30 * Mathf.Sin(u) + 
                    90 * Mathf.Cos(u) * Mathf.Sin(u));
                    y = r * Mathf.Sin(u) * (a + Mathf.Sin(v) * Mathf.Cos(u / 2) - Mathf.Sin(2 * v) * Mathf.Sin(u / 2) / 2);
                    z = r * Mathf.Sin(u / 2) * Mathf.Sin(v) + Mathf.Cos(u / 2) * Mathf.Sin(2 * v) / 2;
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

        
        int triCount = 2 * (resolution + 1) * (resolution + 1);
        int[] triIndecies = new int[triCount * 3];
        int curTriIndex = 0;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                int ul = i * resolution + j;
                int ur = i * resolution + ((j + 1) % resolution);
                int ll = (i + 1) * resolution + j;
                int lr = (i + 1) * resolution + ((j + 1) % resolution);
                                                                  
                //triangle one
                triIndecies[curTriIndex++] = ll;
                triIndecies[curTriIndex++] = ul; 
                triIndecies[curTriIndex++] = ur; 

                //triangle two
                triIndecies[curTriIndex++] = ur; 
                triIndecies[curTriIndex++] = lr; 
                triIndecies[curTriIndex++] = ll; 
            }
        }

        m.triangles = triIndecies;
        //use the triangle info to calculate vertex normals so we dont have to B)
        Vector3[] normals = m.normals; 
        m.RecalculateNormals();    
        return m;

    }



    //show a representation in the editor window
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



