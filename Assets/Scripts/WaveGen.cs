using UnityEngine;
using System.Collections;

public class WaveGen : MonoBehaviour
{
    public float waveScale = 0.1f;
    public float waveGenSpeed = 1.0f;
    public float noiseStrength = 1f;
    public float noiseWalk = 1f;

    private Vector3[] baseHeight;
    private Vector3[] vertices;
    private Mesh mesh;
    /*
    public float waveHeight = 0.31f;
    public float waveSpeed = 1.18f;
    public float waveLength = 2f;
    public float randomHeight = 0.01f;
    public float randomSpeed = 9f;
    public float noiseOffset = 20.0f;
    */
   // private List<float> perVertexRandoms = new List<float>();

    private void Awake()
    {
        {
            mesh = GetComponent<MeshFilter>().mesh;

            if (baseHeight == null)
                baseHeight = mesh.vertices;

            
        }
    }
    void Update()
    {
        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * waveGenSpeed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * waveScale;
            vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = vertex;
            /*
            Vector3 vertex = baseHeight[i];
           // Random.InitState((int)((vertex.x + noiseOffset) * (vertex.x + noiseOffset) + (vertex.z + noiseOffset) * (vertex.z + noiseOffset)));
            vertex.y += Mathf.Sin(Time.time * waveSpeed + baseHeight[i].x * waveLength + baseHeight[i].y * waveLength) * waveHeight;
            vertex.y += Mathf.Sin(Mathf.Cos(Random.value * 1.0f) * randomHeight * Mathf.Cos(Time.time * randomSpeed * Mathf.Sin(Random.value * 1.0f)));
            //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + Mathf.Cos(Time.time * 0.1f) + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = vertex;
            */
            
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}