using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

	public static float Value (Vector3 point, float frequency)
    {
        point *= frequency;
        int i = Mathf.FloorToInt(point.x);
        return i & 1;
    }
}
