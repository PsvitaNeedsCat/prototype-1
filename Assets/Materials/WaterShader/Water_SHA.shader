//BASE SHADER TUTORIAL FROM MANUELA MALASAÑA
Shader "Unlit/Water Texture"
{
	Properties
	{
		[Header(DIFFUSE)]
		[PerRendererData] _MainTex("Texture", 2D) = "white" {}
		[Space]
		[Header(SCROLLING)]
		_ScrollTexOne("Scrolling Texture One", 2D) = "white" {}
		_SpeedXOne("Horizontal Speed", Range(-1, 1)) = 0
		_SpeedYOne("VerticalSpeed", Range(-1, 1)) = 0
		_ScrollTexTwo("Scrolling Texture Two", 2D) = "white" {}
		_SpeedXTwo("Horizontal Speed", Range(-1, 1)) = 0
		_SpeedYTwo("VerticalSpeed", Range(-1, 1)) = 0
	}
		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
				"CanUseSpriteAtlas" = "True"
			}
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"


				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float4 color : COLOR;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					half4 color : COLOR;
					half2 patternUV1 : TEXCOORD1;
					half2 patternUV2 : TEXCOORD2;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				sampler2D _ScrollTexOne;
				float4 _ScrollTexOne_ST;
				sampler2D _ScrollTexTwo;
				float4 _ScrollTexTwo_ST;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.patternUV1 = TRANSFORM_TEX(v.uv, _ScrollTexOne);
					o.patternUV2 = TRANSFORM_TEX(v.uv, _ScrollTexTwo);
					o.color = v.color;
					return o;
				}

				half _SpeedXOne;
				half _SpeedYOne;
				half _SpeedXTwo;
				half _SpeedYTwo;

				half4 frag(v2f i) : SV_Target
				{
					half4 col = tex2D(_MainTex, i.uv);			//Sample texture
					float2 offsetOne = frac(_Time.y * float2(_SpeedXOne, _SpeedYOne));
					float2 offsetTwo = frac(_Time.y * float2(_SpeedXTwo, _SpeedYTwo));
					half4 patternOne = tex2D(_ScrollTexOne, i.patternUV1 + offsetOne);		//Unpack scroll texture
					half4 patternTwo = tex2D(_ScrollTexTwo, i.patternUV2 + offsetTwo);
					return col * i.color * patternOne * patternTwo;
				}
				ENDCG
			}
		}
}
