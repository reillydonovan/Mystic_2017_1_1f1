using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int resolution = 50;
    float umin = 0.0f;
    float umax = Mathf.PI * 2;
    float vmin = 0;
    float vmax = Mathf.PI * 2;


    public float a = 0.0f;
    public float b = 0.0f;
    public float c = 0.0f;
    public float n = 0.0f;
    public float u = 0.0f;
    public float v = 0.0f;
    public float r = 0.0f;
    public float modulation = 0.1f;
    public int frequency = 15;


    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;

    //set of vars exposed to create waves parallel to the 'latitude'
    // of the sphere
    public int xMod1Period = 4; //how many ups and downs there are
    public float xMod1PhaseOffset = 1.0f; //how far the wave is shifted
    public float xMod1Scale = 2.0f; //how big the wave is
    public float xMod1YOffset = 1.1f; //how big the base of the wave is
    public float xMod1TimeResponse = 1.0f; //the amount the wave moves with time

    //set of vars exposed to create waves parallel to the 'longitude'
    // of the sphere
    public int yMod1Period = 4; //how many ups and downs there are
    public float yMod1PhaseOffset = 1.0f; //how far the wave is shifted
    public float yMod1Scale = 2.0f; //how big the wave is
    public float yMod1YOffset = 1.1f; //how big the base of the wave is
    public float yMod1TimeResponse = 1.0f; //the amount the wave moves with time

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
            u = Remap(i, 0, resolution, umin, umax);
            for (int j = 0; j < resolution; j++)
            {
                v = Remap(j, 0, resolution, vmin, vmax);
                //   u = umin + i * (umax - umin) / resolution;
                //   v = vmin + j * (vmax - vmin) / resolution;

                //the get radius function is where 'hamonics' are added
                r = GetRadius(v, u, seconds);

                //add uvs so that we can texture the mesh if we want
                uvs[vIndex] = new Vector2(j * 1.0f / resolution, i * 1.0f / resolution);

                //create a vertex 
                //optimization alert: since the only thing that changes here is the radius
                //(when the number of divisions stays the same) we could cache these numbers
                // and use a shader to create and apply the variations in radius and compute 
                // the normals.


                x = a * (1 - u / Mathf.PI * 2) * Mathf.Cos(n * u) * (1 + Mathf.Cos(v)) + c * Mathf.Cos(n * u);
                y = a * (1 - u / Mathf.PI * 2) * Mathf.Sin(n * u) * (1 + Mathf.Cos(v)) + c * Mathf.Sin(n * u);
                z = b * u / Mathf.PI * 2 + a * (1 - u / Mathf.PI * 2) * Mathf.Sin(v);
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

    float GetRadius(float phi, float theta, float time = 0)
    {
        return xMod1YOffset + xMod1Scale * Mathf.Sin(xMod1TimeResponse * time + theta * xMod1Period * time * .1f + xMod1PhaseOffset * time) +
            yMod1YOffset + yMod1Scale * Mathf.Sin(yMod1TimeResponse * time + phi * yMod1Period * time * .1f + yMod1PhaseOffset * time);
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



