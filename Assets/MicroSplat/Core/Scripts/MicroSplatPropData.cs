﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// layout
// 0 perTex UV Scale and Offset
// 1 PerTex Tint and interpolation contrast (a)
// 2 Normal Strength, Smoothness, AO, Metallic
// 3 Brightness, Contrast, porosity, foam
// 4 DetailNoiseStrength, distance Noise Strength, Distance Resample
// 5 geoTex, tint strength, normal strength
// 6 displace, bias, offset
// 7 Noise 0, Noise 1, Noise 2, Wind Particulate Strength
// 8 Snow (R), Glitter
// 9 Triplanar, trplanar contrast
// 10 Texture Cluster Contrast, boost

// because unity's HDR import pipeline is broke (assumes gamma, so breaks data in textures)
public class MicroSplatPropData : ScriptableObject 
{
   [HideInInspector]
   public Color[] values = new Color[16*16];

   Texture2D tex;

   [HideInInspector]
   public AnimationCurve geoCurve = AnimationCurve.Linear(0, 0.0f, 0, 0.0f);
   Texture2D geoTex;

   public Color GetValue(int x, int y)
   {
      return values[y * 16 + x];
   }

   public void SetValue(int x, int y, Color c)
   {
      #if UNITY_EDITOR
      UnityEditor.Undo.RecordObject(this, "Changed Value");
      #endif

      values[y * 16 + x] = c;

      #if UNITY_EDITOR
      UnityEditor.EditorUtility.SetDirty(this);
      #endif
   }

   public void SetValue(int x, int y, int channel, float value)
   {
      #if UNITY_EDITOR
      UnityEditor.Undo.RecordObject(this, "Changed Value");
      #endif
      int index = y * 16 + x;
      Color c = values[index];
      c[channel] = value;
      values[index] = c;

      #if UNITY_EDITOR
      UnityEditor.EditorUtility.SetDirty(this);
      #endif
   }

   public Texture2D GetTexture()
   {
      if (tex == null)
      {
         if (Application.platform == RuntimePlatform.Switch)
         {
            tex = new Texture2D(16, 16, TextureFormat.RGBAHalf, false, true);
         }
         else
         {
            tex = new Texture2D(16, 16, TextureFormat.RGBAFloat, false, true);
         }
         tex.hideFlags = HideFlags.HideAndDontSave;
         tex.wrapMode = TextureWrapMode.Clamp;
         tex.filterMode = FilterMode.Point;

      }
      tex.SetPixels(values);
      tex.Apply();
      return tex;
   }

   public Texture2D GetGeoCurve()
   {
      if (geoTex == null)
      {
         geoTex = new Texture2D(256, 1, TextureFormat.RHalf, false, true);
         geoTex .hideFlags = HideFlags.HideAndDontSave;
      }
      for (int i = 0; i < 256; ++i)
      {
         float v = geoCurve.Evaluate((float)i / 255.0f);
         geoTex.SetPixel(i, 0, new Color(v, v, v, v));
      }
      geoTex.Apply();
      return geoTex;
   }
}

