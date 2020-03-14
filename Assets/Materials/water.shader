Shader "Custom/water" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalTex("normal map texture", 2D) = "" {}
		_EmissionTex("emission texture", 2D) = "" {}
		_GrayTex("grayscale texture", 2D) = "" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _GrayTex;
		sampler2D _EmissionTex;
		
		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert( inout appdata_full v) {
			v.vertex.y += sin((v.texcoord.x + _Time.y*0.1)*12.)*0.2+ cos((v.texcoord.y + _Time.y*0.1)*12.)*0.2;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			
			fixed4 gray = tex2D(_MainTex, float2(IN.uv_MainTex.x + sin(_Time.x*0.1), IN.uv_MainTex.y + cos(_Time.x*0.1)));
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c/**dott*/;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Normal = tex2D(_NormalTex, IN.uv_MainTex);
			o.Emission = lerp(tex2D(_EmissionTex, IN.uv_MainTex),float4(0.,0.,0.,0.), (1.-gray));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
