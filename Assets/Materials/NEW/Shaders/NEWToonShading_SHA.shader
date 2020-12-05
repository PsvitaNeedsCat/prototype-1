//BASE SHADER TUTORIAL FROM MANUELA MALASAÑA
Shader "Unlit/NEWToonShading"
{
	Properties
	{
		[Header(DIFFUSE)]
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_ShadowThresh("Shadow Threshold", Range(0, 1)) = 0.3
		_ShadowSmooth("Shadow Smooth", Range(0.5, 1)) = 0.5
		_ShadowColor("Shadow Color", Color) = (0, 0, 0, 1)
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
	}
		SubShader
	{
		Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
		LOD 200								//Level of Detail = 200
		Cull Off

		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma surface surf SoftLight alphatest:_Cutoff
		#pragma target 3.0

		half _ShadowThresh;
		half3 _ShadowColor;
		half _ShadowSmooth;

			half4 LightingSoftLight(SurfaceOutput s, half3 lightDir, half atten)
			{
				half d = pow(dot(s.Normal, lightDir) * 0.5, _ShadowThresh);
				half shadow = smoothstep(0.5, _ShadowSmooth, d);
				half3 shadowCol = lerp(_ShadowColor, half3(1, 1, 1), shadow);
				half4 c;

				c.rgb = s.Albedo * atten * shadowCol;
				c.a = s.Alpha;

				return c;
			}

		sampler2D _MainTex;			//sample diffuseap

		struct Input
		{
			float2 uv_MainTex;		//Input object UVs
		};

		float4 _Color;

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex);

			o.Albedo = col.rgb * _Color;		//Albedo = (diffuse, diffuse UVs) colour * colour chosen in inspector
			o.Alpha = col.a;
		}
		ENDCG
	}
		FallBack "Diffuse"
}
