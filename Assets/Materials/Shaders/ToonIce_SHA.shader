//BASE SHADER TUTORIAL FROM MANUELA MALASAÑA
Shader "Unlit/Toon Ice"
{
	Properties
	{
		[Header(DIFFUSE)]
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Alpha("Albedo Alpha", Range(0, 1)) = 1
		[Space]
		[Header(NORMAL)]
		_BumpMap("Normal", 2D) = "bump" {}
		_NormalStrength("Normal Strength", Range(0, 2)) = 1
		[Space]
		[Header(RIM LIGHTING)]
		[HDR]_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimPower("Rim Fill", Range(0, 2)) = 0.1
		_RimSmoothing("Rim Smoothing", Range(0.5, 1)) = 1
		[Space]
		[Header(SHADOWS)]
		_ShadowThresh("Shadow Threshold", Range(0, 2)) = 1
		_ShadowSmooth("Shadow Smoothness", Range(0.5, 1)) = 0.6
		[HDR]_ShadowColor("Shadow Color", Color) = (0, 0, 0, 1)
		[Space]
		[Header(SPECULAR)]
		_SpecMap("Specular Map", 2D) = "white" {}
		_Gloss("Glossiness", Range(0, 20)) = 10
		_GlossSmoothness("Gloss Smoothness", Range(0, 2)) = 1
		[HDR]_GlossColor("Gloss Color", Color) = (1, 1, 1, 1)
		[Space]
		[Header(EMISSION)]
		[HDR]_EmissionColor("Emmission Color", Color) = (0, 0, 0, 1)
		_EmissionMap("Emission Map", 2D) = "white" {}
		[Space]
		[Header(ICE VALUES)]
		_IceColor("Ice Edge Color", Color) = (1, 1, 1, 1)
		_IceWidth("Ice Width", Range(0, 2)) = 0.1
		_IceSmoothing("Ice Smoothing", Range(0.5, 1)) = 1
	}
		SubShader
		{
			Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }
			LOD 200	
			//Level of Detail = 200

			ZWrite Off

			CGPROGRAM
			#include "ToonCommon.cginc"
			#pragma surface surf Toon fullforwardshadows alpha
			#pragma target 3.0

			half _ShadowThresh;
			half _ShadowSmooth;
			half3 _ShadowColor;
			half _Gloss;
			half3 _GlossColor;
			half _GlossSmoothness;

			sampler2D _MainTex;			//sample diffuse
			sampler2D _BumpMap;			//sample normal map
			sampler2D _EmissionMap;		//sample emission map
			sampler2D _SpecMap;			//sample specular map

			struct Input
			{
				float2 uv_MainTex;		//Input object UVs
				float3 viewDir;			//Grab view/camera direction
			};

			fixed4 _Color;				//Declare colour
			float _NormalStrength;		//Declare normal strength
			half4 _EmissionColor;		//Declare emission colour
			half4 _RimColor;			//Declare rim colour
			half _RimPower;				//Declare rim power
			half _RimSmoothing;			//Declare rim cutoff
			half _Alpha;
			half4 _IceColor;
			half _IceWidth;
			half _IceSmoothing;

			void surf(Input IN, inout SurfaceOutput o)
			{
				InitLightingToon(_ShadowThresh, _ShadowSmooth, _ShadowColor, _Gloss, _GlossSmoothness, _GlossColor);
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;		//Albedo = (diffuse, diffuse UVs) colour * colour chosen in inspector
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));	//normal = unpack normal channels(normal map, diffuse UVs)
				o.Normal.z /= _NormalStrength;								//normal zAxis = normal zAxis / normal strength chosen in inspector, divide to invert
				//o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a * _Alpha;
				o.Emission = _EmissionColor * tex2D(_EmissionMap, IN.uv_MainTex) * o.Albedo;
				half d = 1 - pow(dot(o.Normal, IN.viewDir), _RimPower);		//Create dot texture * rim power
				half ice = 1 - pow(dot(o.Normal, IN.viewDir), _IceWidth);		//Create dot texture * rim power
				o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a * _Alpha + (smoothstep(0, max(0, _IceSmoothing), ice));
				o.Emission += _RimColor * smoothstep(0.5, max(0.5, _RimSmoothing), d);			//Apply emission to dot texture
				o.Specular = tex2D(_SpecMap, IN.uv_MainTex).r;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
