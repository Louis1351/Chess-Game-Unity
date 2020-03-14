Shader "GrabPassInvert"
{
	Properties{
		_Deformation("deformation texture", 2D) = "white" {}
	}
	SubShader
	{
		// Draw ourselves after all opaque geometry
		Tags { "Queue" = "Transparent" }

		// Grab the screen behind the object into _BackgroundTexture
		GrabPass
		{
			"_BackgroundTexture"
		}

		// Render the object with the texture generated above, and invert the colors
		Pass
		{
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _Deformation;
			struct v2f
			{
				float2 uv : TEXCOORD1;
				float4 grabPos : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			v2f vert(appdata_base v) {
				v2f o;
				o.uv = float2(v.texcoord.x, v.texcoord.y);
				// use UnityObjectToClipPos from UnityCG.cginc to calculate 
				// the clip-space of the vertex
				o.pos = UnityObjectToClipPos(v.vertex);
				// use ComputeGrabScreenPos function from UnityCG.cginc
				// to get the correct texture coordinate
				o.grabPos = ComputeGrabScreenPos(o.pos);
				return o;
			}

			sampler2D _BackgroundTexture;

			half4 frag(v2f i) : SV_Target
			{
				float4 c = tex2D(_Deformation,i.uv)*0.05;
				half4 bgcolor = tex2Dproj(_BackgroundTexture, float4(i.grabPos.x+c.x, i.grabPos.y + c.y, i.grabPos.zw));
				return bgcolor;
			}
			ENDCG
		}

	}
}