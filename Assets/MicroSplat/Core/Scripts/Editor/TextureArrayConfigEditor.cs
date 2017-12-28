//////////////////////////////////////////////////////
// MicroSplat - 256 texture splat mapping
// Copyright (c) Jason Booth, slipster216@gmail.com
//////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

namespace JBooth.MicroSplat
{
   [CustomEditor(typeof(TextureArrayConfig))]
   public class TextureArrayConfigEditor : Editor
   {

      void DrawHeader(TextureArrayConfig cfg)
      {
         if (cfg.textureMode != TextureArrayConfig.TextureMode.Basic)
         {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("", GUILayout.Width(30));
            EditorGUILayout.LabelField("Channel", GUILayout.Width(64));
            EditorGUILayout.EndVertical();
            EditorGUILayout.LabelField(new GUIContent(""), GUILayout.Width(20));
            EditorGUILayout.LabelField(new GUIContent(""), GUILayout.Width(64));
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new GUIContent("Height"), GUILayout.Width(64));
            cfg.allTextureChannelHeight = (TextureArrayConfig.AllTextureChannel)EditorGUILayout.EnumPopup(cfg.allTextureChannelHeight, GUILayout.Width(64));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new GUIContent("Smoothness"), GUILayout.Width(64));
            cfg.allTextureChannelSmoothness = (TextureArrayConfig.AllTextureChannel)EditorGUILayout.EnumPopup(cfg.allTextureChannelSmoothness, GUILayout.Width(64));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            if (cfg.IsAdvancedDetail())
            {
               EditorGUILayout.LabelField(new GUIContent("Alpha"), GUILayout.Width(64));
               cfg.allTextureChannelAlpha = (TextureArrayConfig.AllTextureChannel)EditorGUILayout.EnumPopup(cfg.allTextureChannelAlpha, GUILayout.Width(64));
            }
            else
            {         
               EditorGUILayout.LabelField(new GUIContent("AO"), GUILayout.Width(64));
               cfg.allTextureChannelAO = (TextureArrayConfig.AllTextureChannel)EditorGUILayout.EnumPopup(cfg.allTextureChannelAO, GUILayout.Width(64));
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            GUILayout.Box(Texture2D.blackTexture, GUILayout.Height(3), GUILayout.ExpandWidth(true));
         }
      }

      void DrawAntiTileEntry(TextureArrayConfig cfg, TextureArrayConfig.TextureEntry e, int i)
      {
         EditorGUILayout.BeginHorizontal();
         EditorGUILayout.Space();EditorGUILayout.Space();
         EditorGUILayout.BeginVertical();

         EditorGUILayout.LabelField(new GUIContent("Noise Normal"), GUILayout.Width(92));
         e.noiseNormal = (Texture2D)EditorGUILayout.ObjectField(e.noiseNormal, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
         EditorGUILayout.EndVertical();

         EditorGUILayout.BeginVertical();
         EditorGUILayout.LabelField(new GUIContent("Detail"), GUILayout.Width(92));
         e.detailNoise = (Texture2D)EditorGUILayout.ObjectField(e.detailNoise, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
         e.detailChannel = (TextureArrayConfig.TextureChannel)EditorGUILayout.EnumPopup(e.detailChannel, GUILayout.Width(64));
         EditorGUILayout.EndVertical();

         EditorGUILayout.BeginVertical();
         EditorGUILayout.LabelField(new GUIContent("Distance"), GUILayout.Width(92));
         e.distanceNoise = (Texture2D)EditorGUILayout.ObjectField(e.distanceNoise, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
         e.distanceChannel = (TextureArrayConfig.TextureChannel)EditorGUILayout.EnumPopup(e.distanceChannel, GUILayout.Width(64));
         EditorGUILayout.EndVertical();


         EditorGUILayout.EndHorizontal();

         if (e.noiseNormal == null)
         {
            int index = (int)Mathf.Repeat(i, 3);
            e.noiseNormal = MicroSplatUtilities.GetAutoTexture("microsplat_def_detail_normal_0" + (index+1).ToString());
         }

      }

      bool DrawTextureEntry(TextureArrayConfig cfg, TextureArrayConfig.TextureEntry e, int i, bool controls = true)
      {
         bool ret = false;
         if (controls)
         {
            EditorGUILayout.BeginHorizontal();

            if (e.HasTextures())
            {
               EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(30));
               EditorGUILayout.LabelField(e.diffuse != null ? e.diffuse.name : "empty");
               ret = GUILayout.Button("Clear Entry");
            }
            else
            {
               EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(30));
               EditorGUILayout.HelpBox("Removing an entry completely can cause texture choices to change on existing terrains. You can leave it blank to preserve the texture order and MicroSplat will put a dummy texture into the array.", MessageType.Warning);
               ret = (GUILayout.Button("Delete Entry"));
            }
            EditorGUILayout.EndHorizontal();
         }

         EditorGUILayout.BeginHorizontal();

         if (cfg.textureMode == TextureArrayConfig.TextureMode.PBR)
         {
            EditorGUILayout.BeginVertical();
            if (controls)
            {
               EditorGUILayout.LabelField(new GUIContent("Substance"), GUILayout.Width(64));
            }
            e.substance = (ProceduralMaterial)EditorGUILayout.ObjectField(e.substance, typeof(ProceduralMaterial), false, GUILayout.Width(64), GUILayout.Height(64));
            EditorGUILayout.EndVertical();
         }

         EditorGUILayout.BeginVertical();
         if (controls)
         {
            EditorGUILayout.LabelField(new GUIContent("Diffuse"), GUILayout.Width(64));
         }
         e.diffuse = (Texture2D)EditorGUILayout.ObjectField(e.diffuse, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
         EditorGUILayout.EndVertical();

         EditorGUILayout.BeginVertical();
         if (controls)
         {
            EditorGUILayout.LabelField(new GUIContent("Normal"), GUILayout.Width(64));
         }
         e.normal = (Texture2D)EditorGUILayout.ObjectField(e.normal, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
         EditorGUILayout.EndVertical();

         if (cfg.textureMode == TextureArrayConfig.TextureMode.PBR || cfg.IsAdvancedDetail())
         {
            EditorGUILayout.BeginVertical();
            if (controls)
            {
               EditorGUILayout.LabelField(new GUIContent("Height"), GUILayout.Width(64));
            }
            e.height = (Texture2D)EditorGUILayout.ObjectField(e.height, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
            if (cfg.allTextureChannelHeight == TextureArrayConfig.AllTextureChannel.Custom)
            {
               e.heightChannel = (TextureArrayConfig.TextureChannel)EditorGUILayout.EnumPopup(e.heightChannel, GUILayout.Width(64));
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            if (controls)
            {
               EditorGUILayout.LabelField(new GUIContent("Smoothness"), GUILayout.Width(64));
            }
            e.smoothness = (Texture2D)EditorGUILayout.ObjectField(e.smoothness, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
            if (cfg.allTextureChannelSmoothness == TextureArrayConfig.AllTextureChannel.Custom)
            {
               e.smoothnessChannel = (TextureArrayConfig.TextureChannel)EditorGUILayout.EnumPopup(e.smoothnessChannel, GUILayout.Width(64));
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Invert", GUILayout.Width(44));
            e.isRoughness = EditorGUILayout.Toggle(e.isRoughness, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            if (cfg.IsAdvancedDetail())
            {
               if (controls)
               {
                  EditorGUILayout.LabelField(new GUIContent("Alpha"), GUILayout.Width(64));
               }
               e.alpha = (Texture2D)EditorGUILayout.ObjectField(e.alpha, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
               if (cfg.allTextureChannelAlpha == TextureArrayConfig.AllTextureChannel.Custom)
               {
                  e.alphaChannel = (TextureArrayConfig.TextureChannel)EditorGUILayout.EnumPopup(e.alphaChannel, GUILayout.Width(64));
               }
            }
            else
            {
               if (controls)
               {
                  EditorGUILayout.LabelField(new GUIContent("AO"), GUILayout.Width(64));
               }
               e.ao = (Texture2D)EditorGUILayout.ObjectField(e.ao, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
               if (cfg.allTextureChannelAO == TextureArrayConfig.AllTextureChannel.Custom)
               {
                  e.aoChannel = (TextureArrayConfig.TextureChannel)EditorGUILayout.EnumPopup(e.aoChannel, GUILayout.Width(64));
               }
            }
            EditorGUILayout.EndVertical();
         }
         EditorGUILayout.EndHorizontal();

         return ret;
      }

      public static bool GetFromTerrain(TextureArrayConfig cfg, Terrain t)
      {
         if (t != null && cfg.sourceTextures.Count == 0 && t.terrainData != null)
         {
            int count = t.terrainData.splatPrototypes.Length;
            for (int i = 0; i < count; ++i)
            {
               var proto = t.terrainData.splatPrototypes[i];
               var e = new TextureArrayConfig.TextureEntry();
               e.diffuse = proto.texture;
               e.normal = proto.normalMap;
               cfg.sourceTextures.Add(e);
            }
            return true;
         }
         return false;
      }

      static void GetFromTerrain(TextureArrayConfig cfg)
      {
         Terrain[] terrains = GameObject.FindObjectsOfType<Terrain>();
         for (int x = 0; x < terrains.Length; ++x)
         {
            var t = terrains[x];
            if (GetFromTerrain(cfg, t))
               return;
         }
      }

      public static TextureArrayConfig CreateConfig(string path)
      {
         string configPath = AssetDatabase.GenerateUniqueAssetPath(path + "/MicroSplatConfig.asset");
         TextureArrayConfig cfg = TextureArrayConfig.CreateInstance<TextureArrayConfig>();

         AssetDatabase.CreateAsset(cfg, configPath);
         AssetDatabase.SaveAssets();
         AssetDatabase.Refresh();
         cfg = AssetDatabase.LoadAssetAtPath<TextureArrayConfig>(configPath);
         CompileConfig(cfg);
         return cfg;
      }

      public static TextureArrayConfig CreateConfig(Terrain t)
      {
         string path = MicroSplatUtilities.RelativePathFromAsset(t.terrainData);
         string configPath = AssetDatabase.GenerateUniqueAssetPath(path + "/MicroSplatConfig.asset");
         TextureArrayConfig cfg = TextureArrayConfig.CreateInstance<TextureArrayConfig>();
         GetFromTerrain(cfg, t);

         AssetDatabase.CreateAsset(cfg, configPath);
         AssetDatabase.SaveAssets();
         AssetDatabase.Refresh();
         cfg = AssetDatabase.LoadAssetAtPath<TextureArrayConfig>(configPath);
         CompileConfig(cfg);
         return cfg;

      }

      static GUIContent CTextureMode = new GUIContent("Texturing Mode", "Do you have just diffuse and normal, or a fully PBR pipeline with height, smoothness, and ao textures?");
      static GUIContent CTextureSize = new GUIContent("Texture Size", "Size for all textures");
      #if __MICROSPLAT_TEXTURECLUSTERS__
      static GUIContent CClusterMode = new GUIContent("Cluster Mode", "Add extra slots for packing parallel arrays for texture clustering");
      #endif
      #if __MICROSPLAT_DETAILRESAMPLE__
      static GUIContent CAntiTileArray = new GUIContent("AntiTile Array", "Create an array for each texture to have it's own Noise Normal, Detail, and Distance noise texture");
      #endif

      void Remove(TextureArrayConfig cfg, int i)
      {
         cfg.sourceTextures.RemoveAt(i);
         cfg.sourceTextures2.RemoveAt(i);
         cfg.sourceTextures3.RemoveAt(i);
      }

      void Reset(TextureArrayConfig cfg, int i)
      {
         cfg.sourceTextures[i].Reset();
         cfg.sourceTextures2[i].Reset();
         cfg.sourceTextures3[i].Reset();
      }

      static void MatchArrayLength(TextureArrayConfig cfg)
      {
         int srcCount = cfg.sourceTextures.Count;
         bool change = false;
         while (cfg.sourceTextures2.Count < srcCount)
         {
            var entry = new TextureArrayConfig.TextureEntry();
            entry.aoChannel = cfg.sourceTextures[0].aoChannel;
            entry.heightChannel = cfg.sourceTextures[0].heightChannel;
            entry.smoothnessChannel = cfg.sourceTextures[0].smoothnessChannel;
            cfg.sourceTextures2.Add(entry);
            change = true;
         }

         while (cfg.sourceTextures3.Count < srcCount)
         {
            var entry = new TextureArrayConfig.TextureEntry();
            entry.aoChannel = cfg.sourceTextures[0].aoChannel;
            entry.heightChannel = cfg.sourceTextures[0].heightChannel;
            entry.smoothnessChannel = cfg.sourceTextures[0].smoothnessChannel;
            cfg.sourceTextures3.Add(entry);
            change = true;
         }

         while (cfg.sourceTextures2.Count > srcCount)
         {
            cfg.sourceTextures2.RemoveAt(cfg.sourceTextures2.Count - 1);
            change = true;
         }
         while (cfg.sourceTextures3.Count > srcCount)
         {
            cfg.sourceTextures3.RemoveAt(cfg.sourceTextures3.Count - 1);
            change = true;
         }
         if (change)
         {
            EditorUtility.SetDirty(cfg);
         }
      }

      public override void OnInspectorGUI()
      {
         var cfg = target as TextureArrayConfig;
         MatchArrayLength(cfg);
         EditorGUI.BeginChangeCheck();
         cfg.textureMode = (TextureArrayConfig.TextureMode)EditorGUILayout.EnumPopup(CTextureMode, cfg.textureMode);
         #if __MICROSPLAT_DETAILRESAMPLE__
         cfg.antiTileArray = EditorGUILayout.Toggle(CAntiTileArray, cfg.antiTileArray);
         #endif
         if (cfg.IsAdvancedDetail())
         {
            cfg.clusterMode = TextureArrayConfig.ClusterMode.None;
         }
         #if __MICROSPLAT_TEXTURECLUSTERS__
         if (!cfg.IsAdvancedDetail())
         {
            cfg.clusterMode = (TextureArrayConfig.ClusterMode)EditorGUILayout.EnumPopup(CClusterMode, cfg.clusterMode);
         }
         #endif


         if (cfg.textureMode != TextureArrayConfig.TextureMode.Basic)
         {
            cfg.diffuseTextureSize = (TextureArrayConfig.TextureSize)EditorGUILayout.EnumPopup("Diffuse Texture Size", cfg.diffuseTextureSize);
            cfg.diffuseCompression = (TextureArrayConfig.Compression)EditorGUILayout.EnumPopup("Diffuse Compression", cfg.diffuseCompression);
            EditorGUILayout.BeginHorizontal();
            cfg.diffuseFilterMode = (FilterMode)EditorGUILayout.EnumPopup("Diffuse Filter Mode", cfg.diffuseFilterMode);
            EditorGUILayout.LabelField("Aniso", GUILayout.Width(64));
            cfg.diffuseAnisoLevel = EditorGUILayout.IntSlider(cfg.diffuseAnisoLevel, 1, 16);
            EditorGUILayout.EndHorizontal();

            cfg.normalSAOTextureSize = (TextureArrayConfig.TextureSize)EditorGUILayout.EnumPopup("Normal Texture Size", cfg.normalSAOTextureSize);
            cfg.normalCompression = (TextureArrayConfig.Compression)EditorGUILayout.EnumPopup("Normal Compression", cfg.normalCompression);
            EditorGUILayout.BeginHorizontal();
            cfg.normalFilterMode = (FilterMode)EditorGUILayout.EnumPopup("Normal Filter Mode", cfg.normalFilterMode);
            EditorGUILayout.LabelField("Aniso", GUILayout.Width(64));
            cfg.normalAnisoLevel = EditorGUILayout.IntSlider(cfg.normalAnisoLevel, 1, 16);
            EditorGUILayout.EndHorizontal();

            if (cfg.antiTileArray)
            {
               cfg.antiTileTextureSize = (TextureArrayConfig.TextureSize)EditorGUILayout.EnumPopup("Anti-Tile Texture Size", cfg.antiTileTextureSize);
               cfg.antiTileCompression = (TextureArrayConfig.Compression)EditorGUILayout.EnumPopup("Anti-Tile Compression", cfg.antiTileCompression);
               EditorGUILayout.BeginHorizontal();
               cfg.antiTileFilterMode = (FilterMode)EditorGUILayout.EnumPopup("Anti-Tile Filter Mode", cfg.antiTileFilterMode);
               EditorGUILayout.LabelField("Aniso", GUILayout.Width(64));
               cfg.antiTileAnisoLevel = EditorGUILayout.IntSlider(cfg.antiTileAnisoLevel, 1, 16);
               EditorGUILayout.EndHorizontal();
            }
         }
         else
         {
            EditorGUILayout.HelpBox("Select PBR mode to use substances or provide custom height, smoothness, and ao textures to greatly increase quality!", MessageType.Info);
            cfg.diffuseTextureSize = (TextureArrayConfig.TextureSize)EditorGUILayout.EnumPopup(CTextureSize, cfg.diffuseTextureSize);
            cfg.normalSAOTextureSize = cfg.diffuseTextureSize;
         }

         if (MicroSplatUtilities.DrawRollup("Textures", true))
         {
            EditorGUILayout.HelpBox("Don't have a normal map? Any missing textures will be generated automatically from the best available source texture", MessageType.Info);
            bool disableClusters = cfg.IsAdvancedDetail();
            DrawHeader(cfg);
            for (int i = 0; i < cfg.sourceTextures.Count; ++i)
            {
               using (new GUILayout.VerticalScope(GUI.skin.box))
               {
                  bool remove = (DrawTextureEntry(cfg, cfg.sourceTextures[i], i));

 
                  if (cfg.clusterMode != TextureArrayConfig.ClusterMode.None && !disableClusters)
                  {
                     DrawTextureEntry(cfg, cfg.sourceTextures2[i], i, false);
                  }
                  if (cfg.clusterMode == TextureArrayConfig.ClusterMode.ThreeVariations && !disableClusters)
                  {
                     DrawTextureEntry(cfg, cfg.sourceTextures3[i], i, false);
                  }
                  if (remove)
                  {
                     var e = cfg.sourceTextures[i];
                     if (!e.HasTextures())
                     {
                        Remove(cfg, i);
                        i--;
                     }
                     else
                     {
                        Reset(cfg, i);
                     }
                  }

                  if (cfg.antiTileArray)
                  {
                     DrawAntiTileEntry(cfg, cfg.sourceTextures[i], i);
                  }

                  GUILayout.Box(Texture2D.blackTexture, GUILayout.Height(3), GUILayout.ExpandWidth(true));
               }
            }
            if (GUILayout.Button("Add Textures"))
            {
               if (cfg.sourceTextures.Count > 0)
               {
                  var entry = new TextureArrayConfig.TextureEntry();
                  entry.aoChannel = cfg.sourceTextures[0].aoChannel;
                  entry.heightChannel = cfg.sourceTextures[0].heightChannel;
                  entry.smoothnessChannel = cfg.sourceTextures[0].smoothnessChannel;
                  entry.alphaChannel = cfg.sourceTextures[0].alphaChannel;
                  cfg.sourceTextures.Add(entry);
               }
               else
               {
                  var entry = new TextureArrayConfig.TextureEntry();
                  entry.aoChannel = TextureArrayConfig.TextureChannel.G;
                  entry.heightChannel = TextureArrayConfig.TextureChannel.G;
                  entry.smoothnessChannel = TextureArrayConfig.TextureChannel.G;
                  entry.alphaChannel = TextureArrayConfig.TextureChannel.G;
                  cfg.sourceTextures.Add(entry);
               }
            }
         }
         if (GUILayout.Button("Grab From Scene Terrain"))
         {
            cfg.sourceTextures.Clear();
            GetFromTerrain(cfg);
         }
         if (GUILayout.Button("Update"))
         {
            staticConfig = cfg;
            EditorApplication.delayCall += DelayedCompileConfig;
         }
         if (EditorGUI.EndChangeCheck())
         {
            EditorUtility.SetDirty(cfg);
         }
      }

      static bool IsLinear(TextureImporter ti)
      {
         return ti.sRGBTexture == false;
      }

      static Texture2D ResizeTexture(Texture2D source, int width, int height, bool linear)
      {
         RenderTexture rt = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, linear ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.sRGB);
         rt.DiscardContents();
         GL.sRGBWrite = (QualitySettings.activeColorSpace == ColorSpace.Linear) && !linear;
         Graphics.Blit(source, rt);
         GL.sRGBWrite = false;
         RenderTexture.active = rt;
         Texture2D ret = new Texture2D(width, height, TextureFormat.ARGB32, true, linear);
         ret.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
         ret.Apply(true);
         RenderTexture.active = null;
         rt.Release();
         DestroyImmediate(rt);
         return ret;
      }

      static TextureFormat GetTextureFormat()
      {
         var platform = EditorUserBuildSettings.activeBuildTarget;
         if (platform == BuildTarget.Android)
         {
            return TextureFormat.ETC2_RGBA8;
         }
         else if (platform == BuildTarget.iOS)
         {
            return TextureFormat.PVRTC_RGBA4;
         }
         else
         {
            return TextureFormat.DXT5;
         }
      }

      static Texture2D RenderMissingTexture(Texture2D src, string shaderPath, int width, int height, int channel = -1)
      {
         Texture2D res = new Texture2D(width, height, TextureFormat.ARGB32, true, true);
         RenderTexture resRT = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
         resRT.DiscardContents();
         Shader s = Shader.Find(shaderPath);
         if (s == null)
         {
            Debug.LogError("Could not find shader " + shaderPath);
            res.Apply();
            return res;
         }
         Material genMat = new Material(Shader.Find(shaderPath));
         if (channel >= 0)
         {
            genMat.SetInt("_Channel", channel);
         }

         GL.sRGBWrite = (QualitySettings.activeColorSpace == ColorSpace.Linear);
         Graphics.Blit(src, resRT, genMat);
         GL.sRGBWrite = false;

         RenderTexture.active = resRT;
         res.ReadPixels(new Rect(0, 0, width, height), 0, 0);
         res.Apply();
         RenderTexture.active = null;
         resRT.Release();
         DestroyImmediate(resRT);
         DestroyImmediate(genMat);
         return res;
      }

      static void MergeInChannel(Texture2D target, int targetChannel, 
         Texture2D merge, int mergeChannel, bool linear, bool invert = false)
      {
         Texture2D src = ResizeTexture(merge, target.width, target.height, linear);
         Color[] sc = src.GetPixels();
         Color[] tc = target.GetPixels();

         for (int i = 0; i < tc.Length; ++i)
         {
            Color s = sc[i];
            Color t = tc[i];
            t[targetChannel] = s[mergeChannel];
            tc[i] = t;
         }
         if (invert)
         {
            for (int i = 0; i < tc.Length; ++i)
            {
               Color t = tc[i];
               t[targetChannel] = 1.0f - t[targetChannel];
               tc[i] = t;
            }
         }

         target.SetPixels(tc);
         target.Apply();
         DestroyImmediate(src);
      }

      static Texture2D BakeSubstance(string path, ProceduralTexture pt, bool linear = true, bool isNormal = false, bool invert = false)
      {
         string texPath = path + pt.name + ".tga";
         TextureImporter ti = TextureImporter.GetAtPath(texPath) as TextureImporter;
         if (ti != null)
         {
            bool changed = false;
            if (ti.sRGBTexture == true && linear)
            {
               ti.sRGBTexture = false;
               changed = true;
            }
            else if (ti.sRGBTexture == false && !linear)
            {
               ti.sRGBTexture = true;
               changed = true;
            }
            if (isNormal && ti.textureType != TextureImporterType.NormalMap)
            {
               ti.textureType = TextureImporterType.NormalMap;
               changed = true;
            }
            if (changed)
            {
               ti.SaveAndReimport();
            }
         }
         var srcTex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);
         return srcTex;
      }

      static void PreprocessTextureEntries(List<TextureArrayConfig.TextureEntry> src, TextureArrayConfig cfg, bool diffuseIsLinear)
      {
         for (int i = 0; i < src.Count; ++i)
         {
            var e = src[i];
            // fill out substance data if it exists
            if (e.substance != null)
            {
               e.substance.isReadable = true;
               e.substance.RebuildTexturesImmediately();
               string srcPath = AssetDatabase.GetAssetPath(e.substance);

               e.substance.SetProceduralVector("$outputsize", new Vector4(11, 11, 0, 0)); // in mip map space, so 2048

               SubstanceImporter si = AssetImporter.GetAtPath(srcPath) as SubstanceImporter;

               si.SetMaterialScale(e.substance, new Vector2(2048, 2048));
               string path = AssetDatabase.GetAssetPath(cfg);
               path = path.Replace("\\", "/");
               path = path.Substring(0, path.LastIndexOf("/"));
               path += "/SubstanceExports/";
               System.IO.Directory.CreateDirectory(path);
               si.ExportBitmaps(e.substance, path, true);
               AssetDatabase.Refresh();

               Texture[] textures = e.substance.GetGeneratedTextures();
               for (int tidx = 0; tidx < textures.Length; tidx++)
               {
                  ProceduralTexture pt = e.substance.GetGeneratedTexture(textures[tidx].name);
                  
                  if (pt.GetProceduralOutputType() == ProceduralOutputType.Diffuse)
                  {
                     e.diffuse = BakeSubstance(path, pt, diffuseIsLinear);
                  }
                  else if (pt.GetProceduralOutputType() == ProceduralOutputType.Height)
                  {
                     e.height = BakeSubstance(path, pt);
                  }
                  else if (pt.GetProceduralOutputType() == ProceduralOutputType.AmbientOcclusion)
                  {
                     e.ao = BakeSubstance(path, pt);
                  }
                  else if (pt.GetProceduralOutputType() == ProceduralOutputType.Normal)
                  {
                     e.normal = BakeSubstance(path, pt, true, true);
                  }
                  else if (pt.GetProceduralOutputType() == ProceduralOutputType.Smoothness)
                  {
                     e.smoothness = BakeSubstance(path, pt);
                     e.isRoughness = false;
                  }
                  else if (pt.GetProceduralOutputType() == ProceduralOutputType.Roughness)
                  {
                     e.smoothness = BakeSubstance(path, pt, true, false);
                     e.isRoughness = true;
                  }
               }
            }

         }
      }

      static void PreprocessTextureEntries(TextureArrayConfig cfg)
      {
         bool diffuseIsLinear = QualitySettings.activeColorSpace == ColorSpace.Linear;

         PreprocessTextureEntries(cfg.sourceTextures, cfg, diffuseIsLinear);
         if (cfg.clusterMode != TextureArrayConfig.ClusterMode.None && !cfg.IsAdvancedDetail())
         {
            PreprocessTextureEntries(cfg.sourceTextures2, cfg, diffuseIsLinear);
         }
         if (cfg.clusterMode == TextureArrayConfig.ClusterMode.ThreeVariations && !cfg.IsAdvancedDetail())
         {
            PreprocessTextureEntries(cfg.sourceTextures3, cfg, diffuseIsLinear);
         }

      }


      static TextureArrayConfig staticConfig;
      void DelayedCompileConfig()
      {
         CompileConfig(staticConfig);
      }

      static string GetDiffPath(TextureArrayConfig cfg, string ext)
      {
         string path = AssetDatabase.GetAssetPath(cfg);
         // create array path
         path = path.Replace("\\", "/");
         return path.Replace(".asset", "_diff" + ext + "_tarray.asset");
      }

      static string GetNormPath(TextureArrayConfig cfg, string ext)
      {
         string path = AssetDatabase.GetAssetPath(cfg);
         // create array path
         path = path.Replace("\\", "/");
         return path.Replace(".asset", "_normSAO" + ext + "_tarray.asset");
      }

      static string GetAntiTilePath(TextureArrayConfig cfg, string ext)
      {
         string path = AssetDatabase.GetAssetPath(cfg);
         // create array path
         path = path.Replace("\\", "/");
         return path.Replace(".asset", "_antiTile" + ext + "_tarray.asset");
      }

      static int SizeToMipCount(int size)
      {
         int mips = 11;
         if (size == 4096)
            mips = 13;
         else if (size == 2048)
            mips = 12;
         else if (size == 1024)
            mips = 11;
         else if (size == 512)
            mips = 10;
         else if (size == 256)
            mips = 9;
         else if (size == 128)
            mips = 8;
         else if (size == 64)
            mips = 7;
         else if (size == 32)
            mips = 6;
         return mips;
      }

      static void CompileConfig(TextureArrayConfig cfg, 
                                List<TextureArrayConfig.TextureEntry> src,
                                string ext, 
                                bool isCluster = false)
      {
         bool diffuseIsLinear = QualitySettings.activeColorSpace == ColorSpace.Linear;

         int diffuseWidth = (int)cfg.diffuseTextureSize;
         int diffuseHeight = (int)cfg.diffuseTextureSize;
         int normalWidth = (int)cfg.normalSAOTextureSize;
         int normalHeight = (int)cfg.normalSAOTextureSize;
         int antiTileWidth = (int)cfg.antiTileTextureSize;
         int antiTileHeight = (int)cfg.antiTileTextureSize;

         int diffuseAnisoLevel = cfg.diffuseAnisoLevel;
         int normalAnisoLevel = cfg.normalAnisoLevel;
         int antiTileAnisoLevel = cfg.antiTileAnisoLevel;

         FilterMode diffuseFilter = cfg.diffuseFilterMode;
         FilterMode normalFilter = cfg.normalFilterMode;
         FilterMode antiTileFilter = cfg.antiTileFilterMode;

         int diffuseMipCount = SizeToMipCount(diffuseWidth);
         int normalMipCount = SizeToMipCount(normalWidth);
         int antiTileMipCount = SizeToMipCount(antiTileWidth);

         int texCount = src.Count;
         if (texCount < 1)
            texCount = 1;
         Texture2DArray diffuseArray = new Texture2DArray(diffuseWidth, diffuseHeight, texCount,
            cfg.diffuseCompression == TextureArrayConfig.Compression.AutomaticCompressed ? GetTextureFormat() : TextureFormat.ARGB32,
            true, diffuseIsLinear);

         diffuseArray.wrapMode = TextureWrapMode.Repeat;
         diffuseArray.filterMode = diffuseFilter;
         diffuseArray.anisoLevel = diffuseAnisoLevel;



         Texture2DArray normalSAOArray = new Texture2DArray(normalWidth, normalHeight, texCount,
            cfg.normalCompression == TextureArrayConfig.Compression.AutomaticCompressed ? GetTextureFormat() : TextureFormat.ARGB32,
            true, true);

         normalSAOArray.wrapMode = TextureWrapMode.Repeat;
         normalSAOArray.filterMode = normalFilter;
         normalSAOArray.anisoLevel = normalAnisoLevel;

         Texture2DArray antiTileArray = null;
         if (!isCluster && cfg.antiTileArray)
         {
            antiTileArray = new Texture2DArray(antiTileWidth, antiTileHeight, texCount,
               cfg.antiTileCompression == TextureArrayConfig.Compression.AutomaticCompressed ? GetTextureFormat() : TextureFormat.ARGB32,
               true, true);

            antiTileArray.wrapMode = TextureWrapMode.Repeat;
            antiTileArray.filterMode = antiTileFilter;
            antiTileArray.anisoLevel = antiTileAnisoLevel;
         }

         for (int i = 0; i < src.Count; ++i)
         {
            try
            {
               EditorUtility.DisplayProgressBar("Packing textures...", "", (float)i / (float)src.Count);

               // first, generate any missing data. We generate a full NSAO map from diffuse or height map
               // if no height map is provided, we then generate it from the resulting or supplied normal. 
               var e = src[i];
               Texture2D diffuse = e.diffuse;
               if (diffuse == null)
               {
                  diffuse = Texture2D.whiteTexture;
               }

               // resulting maps
               Texture2D diffuseHeightTex = ResizeTexture(diffuse, diffuseWidth, diffuseHeight, diffuseIsLinear);
               Texture2D normalSAOTex = null;
               Texture2D antiTileTex = null;

               int heightChannel = (int)e.heightChannel;
               int aoChannel = (int)e.aoChannel;
               int smoothChannel = (int)e.smoothnessChannel;
               int alphaChannel = (int)e.alphaChannel;
               int detailChannel = (int)e.detailChannel;
               int distanceChannel = (int)e.distanceChannel;

               if (cfg.allTextureChannelHeight != TextureArrayConfig.AllTextureChannel.Custom)
               {
                  heightChannel = (int)cfg.allTextureChannelHeight;
               }
               if (cfg.allTextureChannelAO != TextureArrayConfig.AllTextureChannel.Custom)
               {
                  aoChannel = (int)cfg.allTextureChannelAO;
               }
               if (cfg.allTextureChannelSmoothness != TextureArrayConfig.AllTextureChannel.Custom)
               {
                  smoothChannel = (int)cfg.allTextureChannelSmoothness;
               }
               if (cfg.allTextureChannelAlpha != TextureArrayConfig.AllTextureChannel.Custom)
               {
                  alphaChannel = (int)cfg.allTextureChannelAlpha;
               }

               if (e.normal == null)
               {
                  if (e.height == null)
                  {
                     normalSAOTex = RenderMissingTexture(diffuse, "Hidden/MicroSplat/NormalSAOFromDiffuse", normalWidth, normalHeight);
                  }
                  else
                  {
                     normalSAOTex = RenderMissingTexture(e.height, "Hidden/MicroSplat/NormalSAOFromHeight", normalWidth, normalHeight, heightChannel);
                  }
               }
               else
               {
                  // copy, but go ahead and generate other channels in case they aren't provided later.
                  normalSAOTex = RenderMissingTexture(e.normal, "Hidden/MicroSplat/NormalSAOFromNormal", normalWidth, normalHeight);
               }


               if (!isCluster && cfg.antiTileArray)
               {
                  antiTileTex = RenderMissingTexture(e.noiseNormal, "Hidden/MicroSplat/NormalSAOFromNormal", antiTileWidth, antiTileHeight);
               }

               bool destroyHeight = false;
               Texture2D height = e.height;
               if (height == null)
               {
                  destroyHeight = true;
                  height = RenderMissingTexture(normalSAOTex, "Hidden/MicroSplat/HeightFromNormal", diffuseHeight, diffuseWidth);
               }

               MergeInChannel(diffuseHeightTex, (int)TextureArrayConfig.TextureChannel.A, height, heightChannel, diffuseIsLinear);


               if (cfg.IsAdvancedDetail())
               {
                  if (e.alpha != null)
                  {
                     MergeInChannel(normalSAOTex, (int)TextureArrayConfig.TextureChannel.B, e.alpha, alphaChannel, true);
                  }
                  else
                  {
                     MergeInChannel(normalSAOTex, (int)TextureArrayConfig.TextureChannel.B, Texture2D.whiteTexture, 0, true);
                  }
               }
               else if (e.ao != null)
               {
                  MergeInChannel(normalSAOTex, (int)TextureArrayConfig.TextureChannel.B, e.ao, aoChannel, true);
               }

               if (e.smoothness != null)
               {
                  MergeInChannel(normalSAOTex, (int)TextureArrayConfig.TextureChannel.R, e.smoothness, smoothChannel, true, e.isRoughness);
               }

               if (!isCluster && cfg.antiTileArray && antiTileTex != null)
               {
                  Texture2D detail = e.detailNoise;
                  Texture2D distance = e.distanceNoise;
                  bool destroyDetail = false;
                  bool destroyDistance = false;
                  if (detail == null)
                  {
                     detail = new Texture2D(1, 1);
                     detail.SetPixel(0, 0, Color.grey);
                     detail.Apply();
                     destroyDetail = true;
                     detailChannel = (int)TextureArrayConfig.TextureChannel.G;
                  }
                  if (distance == null)
                  {
                     distance = new Texture2D(1, 1);
                     distance.SetPixel(0, 0, Color.grey);
                     distance.Apply();
                     destroyDistance = true;
                     distanceChannel = (int)TextureArrayConfig.TextureChannel.G;
                  }
                  MergeInChannel(antiTileTex, (int)TextureArrayConfig.TextureChannel.R, detail, detailChannel, true, false);
                  MergeInChannel(antiTileTex, (int)TextureArrayConfig.TextureChannel.B, distance, distanceChannel, true, false);

                  if (destroyDetail)
                  {
                     GameObject.DestroyImmediate(detail);
                  }
                  if (destroyDistance)
                  {
                     GameObject.DestroyImmediate(distance);
                  }
               }


               if (cfg.normalCompression != TextureArrayConfig.Compression.Uncompressed)
               {
                  EditorUtility.CompressTexture(normalSAOTex, GetTextureFormat(), TextureCompressionQuality.Normal);
               }

               if (cfg.diffuseCompression != TextureArrayConfig.Compression.Uncompressed)
               {
                  EditorUtility.CompressTexture(diffuseHeightTex, GetTextureFormat(), TextureCompressionQuality.Normal);
               }

               if (antiTileTex != null && cfg.antiTileCompression != TextureArrayConfig.Compression.Uncompressed)
               {
                  EditorUtility.CompressTexture(antiTileTex, GetTextureFormat(), TextureCompressionQuality.Normal);
               }

               normalSAOTex.Apply();
               diffuseHeightTex.Apply();
               if (antiTileTex != null)
               {
                  antiTileTex.Apply();
               }

               for (int mip = 0; mip < diffuseMipCount; ++mip)
               {
                  Graphics.CopyTexture(diffuseHeightTex, 0, mip, diffuseArray, i, mip);
               }
               for (int mip = 0; mip < normalMipCount; ++mip)
               {
                  Graphics.CopyTexture(normalSAOTex, 0, mip, normalSAOArray, i, mip);
               }
               if (antiTileTex != null)
               {
                  for (int mip = 0; mip < antiTileMipCount; ++mip)
                  {
                     Graphics.CopyTexture(antiTileTex, 0, mip, antiTileArray, i, mip);
                  }
               }
               DestroyImmediate(diffuseHeightTex);
               DestroyImmediate(normalSAOTex);

               if (antiTileTex != null)
               {
                  DestroyImmediate(antiTileTex);
               }

               if (destroyHeight)
               {
                  DestroyImmediate(height);
               }


            }
            finally
            {
               EditorUtility.ClearProgressBar();
            }

         }
         EditorUtility.ClearProgressBar();

         diffuseArray.Apply(false, true);
         normalSAOArray.Apply(false, true);
         if (antiTileArray != null)
         {
            antiTileArray.Apply(false, true);
         }

         string diffPath = GetDiffPath(cfg, ext);
         string normSAOPath = GetNormPath(cfg, ext);
         string antiTilePath = GetAntiTilePath(cfg, ext);

         {
            var existing = AssetDatabase.LoadAssetAtPath<Texture2DArray>(diffPath);
            if (existing != null)
            {
               EditorUtility.CopySerialized(diffuseArray, existing);
            }
            else
            {
               AssetDatabase.CreateAsset(diffuseArray, diffPath);
            }
         }

         {
            var existing = AssetDatabase.LoadAssetAtPath<Texture2DArray>(normSAOPath);
            if (existing != null)
            {
               EditorUtility.CopySerialized(normalSAOArray, existing);
            }
            else
            {
               AssetDatabase.CreateAsset(normalSAOArray, normSAOPath);
            }
         }

         if (cfg.antiTileArray && antiTileArray != null)
         {
            var existing = AssetDatabase.LoadAssetAtPath<Texture2DArray>(antiTilePath);
            if (existing != null)
            {
               EditorUtility.CopySerialized(antiTileArray, existing);
            }
            else
            {
               AssetDatabase.CreateAsset(antiTileArray, antiTilePath);
            }
         }

         EditorUtility.SetDirty(cfg);
         AssetDatabase.Refresh();
         AssetDatabase.SaveAssets();

         MicroSplatUtilities.ClearPreviewCache();
         MicroSplatTerrain.SyncAll();
      }

      public static void CompileConfig(TextureArrayConfig cfg)
      {
         MatchArrayLength(cfg);

         PreprocessTextureEntries(cfg);

         CompileConfig(cfg, cfg.sourceTextures, "", false);
         if (cfg.clusterMode != TextureArrayConfig.ClusterMode.None)
         {
            CompileConfig(cfg, cfg.sourceTextures2, "_C2", true);
         }
         if (cfg.clusterMode == TextureArrayConfig.ClusterMode.ThreeVariations)
         {
            CompileConfig(cfg, cfg.sourceTextures3, "_C3", true);
         }


         cfg.diffuseArray = AssetDatabase.LoadAssetAtPath<Texture2DArray>(GetDiffPath(cfg, ""));
         cfg.normalSAOArray = AssetDatabase.LoadAssetAtPath<Texture2DArray>(GetNormPath(cfg, ""));

         EditorUtility.SetDirty(cfg);
         AssetDatabase.Refresh();
         AssetDatabase.SaveAssets();

         MicroSplatUtilities.ClearPreviewCache();
         MicroSplatTerrain.SyncAll();
      }

   }
}
