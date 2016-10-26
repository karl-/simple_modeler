Shader "Custom/FaceAndEdgeHighlight"
{
	Properties
	{
		_Color ("Color Tint", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off
		// ZTest LEqual
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull back

		Pass
		{
			AlphaTest Greater .25

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float4 _Color;

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;

				/// https://www.opengl.org/discussion_boards/showthread.php/166719-Clean-Wireframe-Over-Solid-Mesh
				o.pos = mul(UNITY_MATRIX_MV, v.vertex);
				o.pos.xyz *= .97;
				o.pos = mul(UNITY_MATRIX_P, o.pos);

				return o;
			}

			half4 frag (v2f i) : COLOR
			{
				return _Color;
			}

			ENDCG
		}
	}
}
