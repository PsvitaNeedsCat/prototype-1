// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Wind Shader"
{
	Properties
	{
		//_Color("Color", Color) = (0, 0, 0, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		//_DiffuseTex("Diff", 2D) = "white" {}
		_Strength("Strength", Range(0, 2)) = 1
		_Speed("Speed", Range(-200, 200)) = 100
	}

		SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Opaque"
			"CanUseSpriteAtlas" = "True"
		}
		LOD 200

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float _Strength;
			float _Speed;

			struct vertexInput
			{
				float4 vertex : POSITION;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
			};

			struct appdata
			{
				float4 vertex : POSITION; // vertex position
				float2 uv : TEXCOORD0; // texture coordinate
			};

			struct v2f
			{
				float2 uv : TEXCOORD0; // texture coordinate
				float4 vertex : SV_POSITION; // clip space position
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;


			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			vertexOutput vert(vertexInput IN)
			{
				vertexOutput o;
				float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);
				float4 displacement = (cos(worldPos.y) + cos(worldPos.x + (_Speed * _Time)));
				worldPos.y = worldPos.y + (displacement * _Strength);
				o.pos = mul(UNITY_MATRIX_VP, worldPos);
				return o;

			}

			half4 frag(v2f i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}