Shader "Hidden/BMOVision"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		//_bwBlend("Black & White blend", Range(0, 1)) = 0
		_DeltaX("Delta X", Float) = 0.01
		_DeltaY("Delta Y", Float) = 0.01
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
			uniform float _DeltaX;
			uniform float _DeltaY;

			float sobel(sampler2D tex, float2 uv) 
			{
				float2 delta = float2(_DeltaX, _DeltaY);

				float4 hr = float4(0, 0, 0, 0);
				float4 vt = float4(0, 0, 0, 0);

				hr += tex2D(tex, (uv + float2(-1.0, -1.0) * delta)) *  1.0;
				hr += tex2D(tex, (uv + float2(0.0, -1.0) * delta)) *  0.0;
				hr += tex2D(tex, (uv + float2(1.0, -1.0) * delta)) * -1.0;
				hr += tex2D(tex, (uv + float2(-1.0, 0.0) * delta)) *  2.0;
				hr += tex2D(tex, (uv + float2(0.0, 0.0) * delta)) *  0.0;
				hr += tex2D(tex, (uv + float2(1.0, 0.0) * delta)) * -2.0;
				hr += tex2D(tex, (uv + float2(-1.0, 1.0) * delta)) *  1.0;
				hr += tex2D(tex, (uv + float2(0.0, 1.0) * delta)) *  0.0;
				hr += tex2D(tex, (uv + float2(1.0, 1.0) * delta)) * -1.0;

				vt += tex2D(tex, (uv + float2(-1.0, -1.0) * delta)) *  1.0;
				vt += tex2D(tex, (uv + float2(0.0, -1.0) * delta)) *  2.0;
				vt += tex2D(tex, (uv + float2(1.0, -1.0) * delta)) *  1.0;
				vt += tex2D(tex, (uv + float2(-1.0, 0.0) * delta)) *  0.0;
				vt += tex2D(tex, (uv + float2(0.0, 0.0) * delta)) *  0.0;
				vt += tex2D(tex, (uv + float2(1.0, 0.0) * delta)) *  0.0;
				vt += tex2D(tex, (uv + float2(-1.0, 1.0) * delta)) * -1.0;
				vt += tex2D(tex, (uv + float2(0.0, 1.0) * delta)) * -2.0;
				vt += tex2D(tex, (uv + float2(1.0, 1.0) * delta)) * -1.0;

				return sqrt(hr * hr + vt * vt);
			}

			float4 frag(v2f_img i) : COLOR
			{
				float3 colLow = float3(0.07, 0.52, 0);
				float3 colHigh = float3(0.12, 1, 0);

				float4 result = float4(1, 1, 1, 1);

				float s = sobel(_MainTex, i.uv);
				result.rgb = lerp(colLow, colHigh, s);
				return result;
			}
			ENDCG
		}
	}
}
