Shader "Hidden/VHSPostProcessEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
					
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _VHSTex;
			//float4 _Vector;
			float _yScanline;
			float _xScanline;

			fixed4 frag (v2f_img i) : COLOR{	
				fixed4 vhs = tex2D (_VHSTex, i.uv);
				
				float dy = 1-abs(distance(i.uv.y, _yScanline));
				float dx = 1-abs(distance(i.uv.y, _xScanline));
				i.uv.x += dy * 0.05;
				
				float white = (vhs.r+vhs.g+vhs.b)/3;
				i.uv.y += step(0.99, dx) * white * dx;
				
				fixed4 c = tex2D (_MainTex, i.uv);
				
				return c + vhs;
			}
			ENDCG
		}
	}
Fallback off
}