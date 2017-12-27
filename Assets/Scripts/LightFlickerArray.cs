using UnityEngine;
using System.Collections;

public class LightFlickerArray : MonoBehaviour
{
    [SerializeField]
    Light[] _lights = new Light[6];
    int index;

    void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Light");
        for (int i = 0; i < gos.Length; i++) _lights[i] = gos[i].GetComponent<Light>();
        foreach (Light lgt in _lights) lgt.enabled = false;
        index = 0;
        InvokeRepeating("Blink", 0.01f, 0.1f);
    }

    void Blink()
    {
        foreach (Light lgt in _lights) lgt.enabled = false;
        _lights[index].enabled = true;
        if (++index == _lights.Length) index = 0;
    }
}