Shader "Hidden/MarcelineVision" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_bwBlend("Black & White blend", Range(0, 1)) = 0
		_contrast("Contrast", Range(0, 1)) = 0

	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _bwBlend;
			uniform float _contrast;

			uniform float weights[5];

			float4 frag(v2f_img i) : COLOR
			{
				//weights[0] = 0.227027;
				//weights[1] = 0.1945946;
				//weights[2] = 0.1216216;
				//weights[3] = 0.054054;
				//weights[4] = 0.016216;

				float4 c = tex2D(_MainTex, i.uv);

				c.rgb = ((c.rgb - 0.5f) * max(_contrast, 0));

				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3(lum, lum, lum);

				float4 result = c;
				result.rgb = lerp(c.rgb, bw, _bwBlend);
				float red = max(c.r - c.b - c.g, 0);
				result.r += red;

				//for (int j = 1; j < 5; ++j)
				//{
				//	result.rgb += tex2D(_MainTex, i.uv + float2(j, 0.0)).rgb * weights[j];
				//	result.rgb += tex2D(_MainTex, i.uv - float2(j, 0.0)).rgb * weights[j];
				//	result.rgb += tex2D(_MainTex, i.uv + float2(0.0, j)).rgb * weights[j];
				//	result.rgb += tex2D(_MainTex, i.uv - float2(0.0, j)).rgb * weights[j];
				//}

				return result;
			}
			ENDCG
		}
	}
}

