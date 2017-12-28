//////////////////////////////////////////////////////
// MicroSplat
// Copyright (c) Jason Booth, slipster216@gmail.com
//////////////////////////////////////////////////////


using System.Collections.Generic;
using UnityEngine;

namespace JBooth.MicroSplat
{
   [CreateAssetMenu(menuName = "MicroSplat/Texture Array Config", order = 1)]
   [ExecuteInEditMode]
   public class TextureArrayConfig : ScriptableObject 
   {
      public enum AllTextureChannel
      {
         R = 0,
         G,
         B,
         A,
         Custom
      }


      public enum TextureChannel
      {
         R = 0,
         G,
         B,
         A
      }

      public enum Compression
      {
         AutomaticCompressed,
         Uncompressed
      }

      public enum TextureSize
      {
         k4096 = 4096,
         k2048 = 2048,
         k1024 = 1024,
         k512 = 512,
         k256 = 256,
         k128 = 128,
         k64 = 64,
         k32 = 32,
      }

      // for the interface
      public enum TextureMode
      {
         Basic,
         PBR,
         #if __MICROSPLAT_ADVANCED_DETAIL__
         AdvancedDetails,
         #endif
      }

      public enum ClusterMode
      {
         None,
         TwoVariations,
         ThreeVariations
      }

      public bool IsAdvancedDetail()
      {
      #if __MICROSPLAT_ADVANCED_DETAIL__
         return textureMode == TextureMode.AdvancedDetails;
      #else
         return false;
      #endif
      }
         
      [HideInInspector]
      public bool antiTileArray = false;

      [HideInInspector]
      public TextureMode textureMode = TextureMode.Basic;   

      [HideInInspector]
      public ClusterMode clusterMode = ClusterMode.None;

      [HideInInspector]
      public int hash;

      public int GetNewHash()
      {
         unchecked
         {
            int h = 17;
            h = h * Application.platform.GetHashCode() * 31;
            h = h * Application.unityVersion.GetHashCode() * 37;
            #if UNITY_EDITOR
            h = h * UnityEditor.EditorUserBuildSettings.activeBuildTarget.GetHashCode() * 13;
            #endif
            return h;
         }
      }

      static List<TextureArrayConfig> sAllConfigs = new List<TextureArrayConfig>();
      void Awake()
      {
         sAllConfigs.Add(this);
      }

      void OnDestroy()
      {
         sAllConfigs.Remove(this);
      }

      #if UNITY_EDITOR
      public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
      {
         List<T> assets = new List<T>();
         string[] guids = UnityEditor.AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T).ToString().Replace("UnityEngine.", "")));
         for( int i = 0; i < guids.Length; i++ )
         {
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath( guids[i] );
            T asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>( assetPath );
            if( asset != null )
            {
               assets.Add(asset);
            }
         }
         return assets;
      }
      #endif

      public static TextureArrayConfig FindConfig(Texture2DArray diffuse)
      {
         #if UNITY_EDITOR
         if (sAllConfigs.Count == 0)
         {
            sAllConfigs = FindAssetsByType<TextureArrayConfig>();
         }
         #endif

         for (int i = 0; i < sAllConfigs.Count; ++i)
         {
            if (sAllConfigs[i].diffuseArray == diffuse)
            {
               return sAllConfigs[i];
            }
         }
         return null;
      }

      [HideInInspector]
      public Texture2DArray diffuseArray;
      [HideInInspector]
      public Texture2DArray normalSAOArray;

      [HideInInspector]
      public Texture2DArray diffuseArray2;
      [HideInInspector]
      public Texture2DArray normalSAOArray2;

      [HideInInspector]
      public Texture2DArray diffuseArray3;
      [HideInInspector]
      public Texture2DArray normalSAOArray3;
      
      public TextureSize diffuseTextureSize = TextureSize.k1024;
      public Compression diffuseCompression = Compression.AutomaticCompressed;
      public FilterMode diffuseFilterMode = FilterMode.Bilinear;
      public int diffuseAnisoLevel = 1;

      public TextureSize normalSAOTextureSize = TextureSize.k1024;
      public Compression normalCompression = Compression.AutomaticCompressed;
      public FilterMode normalFilterMode = FilterMode.Trilinear;
      public int normalAnisoLevel = 1;

      public TextureSize antiTileTextureSize = TextureSize.k1024;
      public Compression antiTileCompression = Compression.AutomaticCompressed;
      public FilterMode antiTileFilterMode = FilterMode.Trilinear;
      public int antiTileAnisoLevel = 1;


      [HideInInspector]
      public AllTextureChannel allTextureChannelHeight = AllTextureChannel.G;
      [HideInInspector]
      public AllTextureChannel allTextureChannelSmoothness = AllTextureChannel.G;
      [HideInInspector]
      public AllTextureChannel allTextureChannelAO = AllTextureChannel.G;
      [HideInInspector]     
      public AllTextureChannel allTextureChannelAlpha = AllTextureChannel.A;


      [System.Serializable]
      public class TextureEntry
      {
         public ProceduralMaterial substance;
         public Texture2D diffuse;
         public Texture2D height;
         public TextureChannel heightChannel = TextureChannel.G;
         public Texture2D normal;
         public Texture2D smoothness;
         public TextureChannel smoothnessChannel = TextureChannel.G;
         public bool isRoughness;
         public Texture2D ao;
         public TextureChannel aoChannel = TextureChannel.G;       
         public Texture2D alpha;
         public TextureChannel alphaChannel = TextureChannel.G;         

         // anti tile
         public Texture2D noiseNormal;
         public Texture2D detailNoise;
         public TextureChannel detailChannel = TextureChannel.G;      
         public Texture2D distanceNoise;
         public TextureChannel distanceChannel = TextureChannel.G;      

         public void Reset()
         {
            substance = null;
            diffuse = null;
            height = null;
            normal = null;
            smoothness = null;
            ao = null;
            isRoughness = false;
            alpha = null;
            detailNoise = null;
            distanceNoise = null;
            heightChannel = TextureChannel.G;
            smoothnessChannel = TextureChannel.G;
            aoChannel = TextureChannel.G;
            alphaChannel = TextureChannel.G;
            distanceChannel = TextureChannel.G;
            detailChannel = TextureChannel.G;
         }

         public bool HasTextures()
         {
            return (substance != null || diffuse != null || height != null || normal != null || smoothness != null || ao != null);
         }
      }

      [HideInInspector]
      public List<TextureEntry> sourceTextures = new List<TextureEntry>();
      [HideInInspector]
      public List<TextureEntry> sourceTextures2 = new List<TextureEntry>();
      [HideInInspector]
      public List<TextureEntry> sourceTextures3 = new List<TextureEntry>();
   }
}
