//SHADER TUTORIAL FROM MANUELA MALASAÑA
Shader "Unlit/ToonShading"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_AlphaMap("Alpha Map", Color) = (1, 1, 1, 1)
		_BumpMap("Normal", 2D) = "bump" {}
		_NormalStrength("Normal Strength", Range(0, 2)) = 1
		[HDR]_EmissionColor("Emmission Color", Color) = (0, 0, 0, 1)
		_EmissionMap("Emission Map", 2D) = "white" {}
		[HDR]_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimPower("Rim Fill", Range(0, 2)) = 0.1
		_RimSmoothing("Rim Smoothing", Range(0.5, 1)) = 1
		_ShadowThresh("Shadow Threshold", Range(0, 2)) = 1
		_ShadowSmooth("Shadow Smoothness", Range(0.5, 1)) = 0.6
		[HDR]_ShadowColor("Shadow Color", Color) = (0, 0, 0, 1)
		_SpecMap("Specular Map", 2D) = "white" {}
		_Gloss("Glossiness", Range(0, 20)) = 10
		_GlossSmoothness("Gloss Smoothness", Range(0, 2)) = 1
		[HDR]_GlossColor("Gloss Color", Color) = (1, 1, 1, 1)
	}
		SubShader
		{
			Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }
			LOD 200								//Level of Detail = 200

			CGPROGRAM
			#pragma surface surf Toon fullforwardshadows alpha
			#pragma target 3.0

			half _ShadowThresh;
			half _ShadowSmooth;
			half3 _ShadowColor;
			half _Gloss;
			half3 _GlossColor;
			half _GlossSmoothness;

			half4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half d = pow(dot(s.Normal, lightDir) * 0.5 + 0.5, _ShadowThresh);
				half shadow = smoothstep(0.5, _ShadowSmooth, d);
				half3 shadowColor = lerp(_ShadowColor, half3(1, 1, 1), shadow);
				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * atten * shadowColor;
				c.a = s.Alpha;

				half3 halfDir = normalize(lightDir + viewDir);
				half halfDot = pow(dot(s.Normal, halfDir), _Gloss);
				half gloss = smoothstep(0.5, max(0.5, _GlossSmoothness), halfDot) * s.Specular;
				c.rgb = lerp(c.rgb, _GlossColor * _LightColor0.rgb, gloss);
				return c;
			}

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

			void surf(Input IN, inout SurfaceOutput o)
			{
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;		//Albedo = (diffuse, diffuse UVs) colour * colour chosen in inspector
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));	//normal = unpack normal channels(normal map, diffuse UVs)
				o.Normal.z /= _NormalStrength;								//normal zAxis = normal zAxis / normal strength chosen in inspector, divide to invert
				o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
				o.Emission = _EmissionColor * tex2D(_EmissionMap, IN.uv_MainTex) * o.Albedo;
				half d = 1 - pow(dot(o.Normal, IN.viewDir), _RimPower);		//Create dot texture * rim power
				o.Emission += _RimColor * smoothstep(0.5, max(0.5, _RimSmoothing), d);			//Apply emission to dot texture
				o.Specular = tex2D(_SpecMap, IN.uv_MainTex).r;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
