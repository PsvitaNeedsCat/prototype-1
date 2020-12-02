Shader "Unlit/Water"
{
	Properties
	{
		_Color("Base Colour", Color) = (1, 1, 1, 1)
		_MainTex("Texture 1", 2D) = "white" {}
		_Dist("Distortion", Float) = 0.1
		_BGDist("Background Distortion", Float) = 0.1
		_BGScale("Background Scale", Float) = 1
	}
		SubShader
		{
			Tags { "Queue"="Transparent" "RenderType" = "Transparent" }
			LOD 100

			Lighting Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			/*GrabPass
			{
				"_BackgroundTex"
			}*/

				CGPROGRAM
				#pragma surface surf Standard vertex:vert alpha

				struct Input
				{
					float2 st_MainTex;
					float2 st_BackgroundTex;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				sampler2D _MainTex2;
				float4 _MainTex2_ST;
				float _Dist;
				float _BGDist;
				float _BGScale;
				sampler2D _BackgroundTex;
				float4 _BackgroundTexture_ST;

				void vert(inout appdata_full v, out Input o)
				{
					UNITY_INITIALIZE_OUTPUT(Input, o);

					o.st_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);

					o.st_MainTex.x += sin((o.st_MainTex.x + o.st_MainTex.y) + _Time.g) * _Dist;
					o.st_MainTex.y += cos((o.st_MainTex.x - o.st_MainTex.y) + _Time.g) * _Dist;
				}

				fixed4 _EmissionColor;
				float4 _Color;

				float2 UVDistort(float2 uv, float time)
				{
					return uv + time;
				}

				float2 moveUV(float2 uv, float time)
				{
					return float2(uv.x, uv.y + time);
				}

				void surf(Input IN, inout SurfaceOutputStandard o)
				{
					float2 uv = moveUV(IN.st_MainTex, _Time.y);

					float4 c = tex2D(_MainTex, IN.st_MainTex);
					float4 bg = tex2D(_BackgroundTex, IN.st_MainTex);

					o.Albedo = c.rgb * _Color.rgb;
					o.Alpha = c.a * _Color.a;
				}
				ENDCG
		}
			Fallback "Diffuse"
}
