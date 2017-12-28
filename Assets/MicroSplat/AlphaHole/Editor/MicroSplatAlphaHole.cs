//////////////////////////////////////////////////////
// MicroSplat
// Copyright (c) Jason Booth, slipster216@gmail.com
//////////////////////////////////////////////////////


using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections.Generic;

namespace JBooth.MicroSplat
{
   #if __MICROSPLAT__
   [InitializeOnLoad]
   public class MicroSplatAlphaHole : FeatureDescriptor
   {
      const string sDefine = "__MICROSPLAT_ALPHAHOLE__";
      static MicroSplatAlphaHole()
      {
         MicroSplatDefines.InitDefine(sDefine);
      }
      [PostProcessSceneAttribute (0)]
      public static void OnPostprocessScene()
      { 
         MicroSplatDefines.InitDefine(sDefine);
      }

      public override string ModuleName()
      {
         return "Alpha Hole";
      }

      public enum DefineFeature
      {
         _ALPHAHOLE,
         _ALPHABELOWHEIGHT,
         kNumFeatures,
      }

      static TextAsset properties;
      static TextAsset funcs;


      public bool alphaHole;
      public bool alphaBelowHeight;
      public int textureIndex;


      GUIContent CAlphaHole = new GUIContent("Alpha Hole", "Paint areas which are transparent");
      GUIContent CAlphaBelowHeight = new GUIContent("Alpha Water Level", "Clip any area below this level");
      // Can we template these somehow?
      static Dictionary<DefineFeature, string> sFeatureNames = new Dictionary<DefineFeature, string>();
      public static string GetFeatureName(DefineFeature feature)
      {
         string ret;
         if (sFeatureNames.TryGetValue(feature, out ret))
         {
            return ret;
         }
         string fn = System.Enum.GetName(typeof(DefineFeature), feature);
         sFeatureNames[feature] = fn;
         return fn;
      }

      public static bool HasFeature(string[] keywords, DefineFeature feature)
      {
         string f = GetFeatureName(feature);
         for (int i = 0; i < keywords.Length; ++i)
         {
            if (keywords[i] == f)
               return true;
         }
         return false;
      }

      static GUIContent CTextureIndex = new GUIContent("Texture Index", "Texture Index which is considered 'transparent'");
      static GUIContent CWaterLevel = new GUIContent("Water Level", "Height at which to clip terrain below");

      public override string GetVersion()
      {
         return "1.7";
      }

      public override void DrawFeatureGUI(Material mat)
      {
         alphaHole = EditorGUILayout.Toggle(CAlphaHole, alphaHole);
         alphaBelowHeight = EditorGUILayout.Toggle(CAlphaBelowHeight, alphaBelowHeight);
      }

      public override void DrawShaderGUI(MicroSplatShaderGUI shaderGUI, Material mat, MaterialEditor materialEditor, MaterialProperty[] props)
      {
         if (alphaHole || alphaBelowHeight)
         {
            if (MicroSplatUtilities.DrawRollup("Alpha"))
            {
               if (mat.HasProperty("_AlphaData"))
               {
                  Vector4 vals = shaderGUI.FindProp("_AlphaData", props).vectorValue;
                  Vector4 newVals = vals;
                  if (alphaHole)
                  {
                     newVals.x = (int)EditorGUILayout.IntSlider(CTextureIndex, (int)vals.x, 0, 16);
                  }
                  if (alphaBelowHeight)
                  {
                     newVals.y = EditorGUILayout.FloatField(CWaterLevel, vals.y);
                  }
                  if (newVals != vals)
                  {
                     shaderGUI.FindProp("_AlphaData", props).vectorValue = newVals;
                  }
               }
            }
         }

      }

      public override string[] Pack()
      {
         List<string> features = new List<string>();
         if (alphaHole)
         {
            features.Add(GetFeatureName(DefineFeature._ALPHAHOLE));
         }
         if (alphaBelowHeight)
         {
            features.Add(GetFeatureName(DefineFeature._ALPHABELOWHEIGHT));
         }
         return features.ToArray();
      }

      public override void Unpack(string[] keywords)
      {
         alphaHole = HasFeature(keywords, DefineFeature._ALPHAHOLE);
         alphaBelowHeight = HasFeature(keywords, DefineFeature._ALPHABELOWHEIGHT);
      }

      public override void InitCompiler(string[] paths)
      {
         for (int i = 0; i < paths.Length; ++i)
         {
            string p = paths[i];
            if (p.EndsWith("microsplat_properties_alphahole.txt"))
            {
               properties = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_func_alphahole.txt"))
            {
               funcs = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
         }
      }

      public override void WriteProperties(string[] features, System.Text.StringBuilder sb)
      {
         if (HasFeature(features, DefineFeature._ALPHAHOLE) || (HasFeature(features, DefineFeature._ALPHABELOWHEIGHT)))
         {
            sb.Append(properties.text);
         }
      }

      public override void WriteFunctions(System.Text.StringBuilder sb)
      {
         if (alphaHole || alphaBelowHeight)
         {
            sb.Append(funcs.text);
         }
      }

      public override void ComputeSampleCounts(string[] features, ref int arraySampleCount, ref int textureSampleCount, ref int maxSamples, ref int tessellationSamples, ref int depTexReadLevel)
      {

      }

   }   

   #endif
}