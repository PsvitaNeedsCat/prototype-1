//BASE SHADER TUTORIAL FROM MANUELA MALASAÑA
Shader "Unlit/Scrolling Texture"
{
    Properties
    {
		[Header(DIFFUSE)]
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {}
		[Space]
		[Header(SCROLLING)]
		_ScrollTex("Scrolling Texture", 2D) = "white" {}
		_SpeedX("Horizontal Speed", Range(-1, 1)) = 0
		_SpeedY("VerticalSpeed", Range(-1, 1)) = 0
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
				half2 patternUV : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _ScrollTex;
			float4 _ScrollTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.patternUV = TRANSFORM_TEX(v.uv, _ScrollTex);
				o.color = v.color;
                return o;
            }

			half _SpeedX;
			half _SpeedY;

            half4 frag (v2f i) : SV_Target
            {
				half4 col = tex2D(_MainTex, i.uv);			//Sample texture
				float2 offset = frac(_Time.y * float2(_SpeedX, _SpeedY));
				half4 pattern = tex2D(_ScrollTex, i.patternUV + offset);		//Unpack scroll texture
                return col * i.color * pattern;
            }
            ENDCG
        }
    }
}
