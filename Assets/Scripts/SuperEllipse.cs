using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEllipse : MonoBehaviour {
    /*
    public float n1 = 0.3f;
    public float n2 = 0.3f;
    public float n3 = 0.3f;
    public float m = 0.0f;
    public float a = 1.0f;
    public float b = 1.0f;
    public float osc = 0.0f;
    public float radius = 100.0f;
    public float resolution = 100.0f;
    public float increment = Mathf.PI * 2 / resolution;
    // Use this for initialization
    void Start() {
        //we need a mesh filter
        GetComponent<MeshFilter>().mesh = new Mesh();
    }

    // Update is called once per frame
    void Update() {

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
        

        float seconds = Time.timeSinceLevelLoad;

        // build an array of vectors holding the vertex data
        int vIndex = 0;

        for (float angle = 0; angle < Mathf.PI * 2; angle += increment)
        {

            float r = Sshape(angle);
            float x = radius * r * Mathf.Cos(angle);
            float y = radius * r * Mathf.Sin(angle);



        }
    

        m.vertices = vectors;
        m.uv = uvs;

       
        m.RecalculateNormals();
        return m;
    }


    float Sshape(float theta)
    {
        float part1 = (1 / a) * Mathf.Cos(theta * m / 4);
        part1 = Mathf.Abs(part1);
        part1 = Mathf.Pow(part1, n2);

        float part2 = (1 / b) * Mathf.Sin(theta * m / 4);
        part2 = Mathf.Abs(part2);
        part2 = Mathf.Pow(part2, n3);

        float part3 = Mathf.Pow(part1 + part2, 1 / n1);

        if(part3 == 0)
        {
            return 0;
        }
        return (1 / part3);
    }

    float Remap(float val, float srcMin, float srcMax, float dstMin, float dstMax)
    {
        if (val >= srcMax) return dstMax;
        if (val <= srcMin) return dstMin;
        return dstMin + (val - srcMin) / (srcMax - srcMin) * (dstMax - dstMin);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireMesh(UpdateMesh(null), transform.position, transform.rotation, transform.localScale);

    }
    */
}
