// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UnlitGlassVertexTest"
{
	Properties
	{
		_Cube("Cubemap", CUBE) = "" {}
		_Material_Color("Material Color", Color) = (0, 0.5, 1, 1)
		_Light_Dir("Light Direction, Specular Power", Vector) = (0.5, -1, 0, 10)
		_Brightness("Brightness", Range(0.0, 5.0)) = 1.0
		_Side_Brightness("Side Brightness", Range(0.0, 1.0)) = 1.0
		_ReflectionAmount("Reflection Amount", Range(0.0, 5.0)) = 0.5
		_ReflectionBlur("Reflection Blur", Range(0.0, 10.0)) = 0.0
		_Reflection_Tint("Reflection Tint, Alpha", Color) = (1, 1, 1, 1)
		_Iridecence("Iridecence Amount", Range(0.0, 1.0)) = 0.02
		_Alpha("Alpha", Range(0.0, 1.0)) = 1.0
         _Value1 ("Value1", Range(-100,100)) = 1.0
         _Value2 ("Value2", Range(-100,100)) = 1.0
         _Value3 ("Value3", Range(-100,100)) = 1.0

			[Header(Blend State)]
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("SrcBlend", Float) = 1 //"One"
			[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("DestBlend", Float) = 0 //"Zero"
			[Space(20)]

		[Header(Other)]
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2 //"Back"
			[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 4 //"LessEqual"
			[Enum(Off,0,On,1)] _ZWrite("ZWrite", Float) = 1.0 //"On"
			[Enum(UnityEngine.Rendering.ColorWriteMask)] _ColorWriteMask("ColorWriteMask", Float) = 15 //"All"
	}
	SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "PerformanceChecks" = "False" }
		pass
		{
			Fog{ Mode Off }
			Lighting Off
			ZWrite On
			Cull Off
				LOD 100
				Blend[_SrcBlend][_DstBlend]
				ZTest[_ZTest]
				ZWrite[_ZWrite]
				Cull[_Cull]
				ColorMask[_ColorWriteMask]

			CGPROGRAM
			#pragma vertex vert fullforwardshadows
			#pragma fragment frag
		//	#pragma surface surf Standard vertex:vert fullforwardshadows
			#include "UnityCG.cginc"
			//#include "GazeHighlight.cginc"
			//#include "NearFade.cginc"
			#pragma target 4.0

			fixed4 _Light_Dir;
			fixed4 _Material_Color;
			samplerCUBE _Cube;
			fixed _ReflectionAmount;
			fixed _ReflectionBlur;
			fixed4 _Reflection_Tint;
			fixed _Iridecence;
			fixed _Brightness;
			fixed _Side_Brightness;
			fixed _Alpha;
			float _Amount;
         float _Value1;
         float _Value2;
         float _Value3;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				fixed4 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed4 uv : TEXCOORD0;
				fixed3 localNormal : TEXCOORD1;
				fixed3 viewDir : TEXCOORD2;
				fixed3 halfVec : TEXCOORD3;
				fixed4 wPos : TEXCOORD4;
			};

			v2f vert(appdata v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				v.vertex.y += sin((v.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1;
				//o.pos.y = mul(UNITY_MATRIX_MVP, v.vertex += sin((v.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1);
				o.uv.xy = v.texcoord;
				o.localNormal = normalize(v.normal);
				o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
				o.uv.z = lerp(v.color.g, 1, _Side_Brightness);

				//need to normalize after (not before) multiply
				fixed3 nLinght = normalize(mul((float3x3)unity_WorldToObject, _Light_Dir.xyz));
				o.halfVec = o.viewDir - nLinght;
				o.wPos = mul(unity_ObjectToWorld, v.vertex);
				//v.vertex.y += sin((v.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1;
				return o;
			};

			fixed4 frag(v2f i) : COLOR
			{
				min16float fresnel = dot(i.viewDir.xyz, i.localNormal);
				min16float HdotN = dot(normalize(i.halfVec), i.localNormal);
				min16float4 texcol = _Material_Color + fixed4(sin(HdotN * i.wPos.xyz * 35), 0) * _Iridecence;

				min16float3 reflectVector = mul(unity_ObjectToWorld, i.localNormal * saturate(fresnel) * 2.0 - i.viewDir);
				texcol = lerp(texcol, _ReflectionAmount * _Reflection_Tint * texCUBElod(_Cube, fixed4(reflectVector.rgb, _ReflectionBlur)), pow(1 - fresnel, 0.5));
				texcol *= _Brightness * (smoothstep(0.2, 1, 1 - length(i.uv.xy - fixed2(0.5, 0.5))) + 0.75) * i.uv.z;

				return texcol;
				//return gazeHighlight(texcol, i.wPos) * nearFade(i.wPos);
			}

		//	 void vert (inout appdata_full v) {
           //  v.vertex.xz += v.normal.xz * _Amount * abs(sin(_Time * 10)) * v.color.x;
           //  v.vertex.y += _Amount * abs(sin(_Time * 200)) * v.color.y;

           //Fat Mesh
         //  v.vertex.xyz += v.normal * _Value1;

           //Wave Mesh
        //   v.vertex.x += sin((v.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1;
      //   v.vertex.y += sin((v.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1;

       //Bubbling Mesh
    //   v.vertex.xyz += v.normal * ( sin((v.vertex.x + _Time * _Value3) * _Value2) + cos((v.vertex.z + _Time* _Value3) * _Value2)) * _Value1;

     //    }

			ENDCG
		}
	}
}
