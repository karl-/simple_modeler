/**
 * Wireframe shader adapted from:
 * http://codeflow.org/entries/2012/aug/02/easy-wireframe-display-with-barycentric-coordinates/
 */

Shader "Custom/Wireframe"
{
	Properties
	{
		_Color ("Wire Color", Color) = (0,0,0,1)
		_Thickness ("Wire Thickness", Range(0, 1)) = .5
		_Opacity ("Wire Opacity", Range (0, 1)) = .8
	}

	SubShader
	{
		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

		Pass
		{
			AlphaTest Greater .25

			CGPROGRAM
		
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _Color;
			float _Thickness;
			float _Opacity;

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float4 pos : POSITION;
				float4 color : COLOR;
			};

			float edgeFactor(fixed3 pos)
			{
				fixed3 d = fwidth(pos);
				fixed3 a3 = smoothstep( fixed3(0.0,0.0,0.0), d * _Thickness, pos);
				return min(min(a3.x, a3.y), a3.z);
			}

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MV, v.vertex);
				o.pos.xyz *= .98;
				o.pos = mul(UNITY_MATRIX_P, o.pos);
				o.color = v.color;
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				fixed4 n = fixed4(0,0,0,0);
				return lerp( _Color, n, edgeFactor(i.color) );
			}
			ENDCG
		}
	}

	Fallback "Diffuse"
}
