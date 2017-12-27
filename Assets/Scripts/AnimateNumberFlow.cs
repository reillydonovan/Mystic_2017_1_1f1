using UnityEngine;
using CatlikeCoding.NumberFlow;

public class AnimateNumberFlow : MonoBehaviour
{

    public Material material;

    public Diagram diagram;

    public int width, height;

    // Value timeValue;
    Value timeValue;

    Color[] buffer;
    Texture2D texture;

    void Awake()
    {
        timeValue = diagram.GetInputValue("Time");
        buffer = new Color[width * height];
        texture = new Texture2D(width, height);
        material.mainTexture = texture;
    }

    void Update()
    {
        timeValue.Float = Time.time;
        diagram.Fill(buffer, width, height);
        texture.SetPixels(buffer);
        texture.Apply();
    }
}