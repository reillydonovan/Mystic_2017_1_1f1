using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MobiusStrip : MonoBehaviour
{


    //    private Mesh mesh;
    public int divisions = 200;
    public float stripWidth = 1.0f;
    public float radius = 3.5f;
    public float modulation = 0.1f;
    public int frequency = 15;

    public float a = 0.0f;
    public float u = 0.0f;
    public float v = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshFilter>().mesh = Generate();
    }

    private Mesh Generate()
    {
        Mesh newMesh = new Mesh();

        float tm = Time.realtimeSinceStartup;


        Vector3[] vertices = new Vector3[divisions * 2];
        Vector3[] normals = new Vector3[divisions * 2];
        //        print("vertices.Length: " + vertices.Length);
        float radianDivWidth = Mathf.PI * 4.0f / (divisions - 0);
        float stripRadianDivWidth = Mathf.PI * 2.0f / (divisions - 0);

        for (int i = 0; i < divisions; i++)
        {
            float curRingRad = radianDivWidth * i;
            Vector3 ringVec = new Vector3(radius * Mathf.Cos(curRingRad),
                                         0,
                                      radius * Mathf.Sin(curRingRad));
            float totalRadius = stripWidth * (1 + modulation *
            Mathf.Sin(tm + curRingRad * frequency));
            Vector3 outVec = ringVec.normalized * totalRadius;
            Vector3 scaledUpV = Vector3.up * totalRadius;

            float curStripRotRad = stripRadianDivWidth * i;

            Vector3 toroidalOffset = outVec * Mathf.Cos(curStripRotRad) +
                                     scaledUpV * Mathf.Sin(curStripRotRad);

            vertices[i * 2] = ringVec + toroidalOffset;
            vertices[i * 2 + 1] = ringVec - toroidalOffset;
            Vector3 normal = outVec * Mathf.Cos(curStripRotRad + Mathf.PI / 2.0f) +
                             scaledUpV * Mathf.Sin(curStripRotRad + Mathf.PI / 2.0f);
            normal.Normalize();
            normals[i * 2] = normals[i * 2 + 1] = normal;
        }


        int[] triangles = new int[vertices.Length * 3];
        //        print("triangles.Length: " + triangles.Length);
        for (int i = 0; i < triangles.Length; i += 6)
        {
            int vIndex = i / 6;
            triangles[i] = vIndex;
            triangles[i + 1] = vIndex + 2;
            triangles[i + 2] = vIndex + 1;

            triangles[i + 3] = vIndex + 1;
            triangles[i + 4] = vIndex + 2;
            triangles[i + 5] = vIndex + 3;
        }

        newMesh.name = "Procedural Klein Bottle";
        newMesh.vertices = vertices;
        newMesh.triangles = triangles;
        newMesh.normals = normals;
        //        newMesh.RecalculateNormals();
        return newMesh;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireMesh(Generate());

    }
}