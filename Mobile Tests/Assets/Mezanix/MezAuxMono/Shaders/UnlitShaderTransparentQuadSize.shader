Shader "Mezanix/UnlitShaderTransparentQuadSize"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		//_TrPos ("Transform Position", vector) = (0, 0, 0, 1)
		//_InClipSize ("InClipSize", float) = 10.2
		//_topRightVertex ("TopRightVertex", vector) = (0, 5, 0, 1)
	}
	SubShader
	{
	//"Queue"="Transparent"
		Tags { "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
		Cull Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				//UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			//float4 _TrPos;
			//float _InClipSize;
			//float4 _topRightVertex;

			//float4 ClipVertHalfFromCenter (float4 clipVert)
			//{
			//	float4 r;
			//	float4 TrPosClip = UnityObjectToClipPos(_TrPos);
			//
			//	float _1 = 0.5;
			//	float3 dir = normalize (clipVert.xyz - TrPosClip.xyz);
			//	r.xyz = TrPosClip.xyz + _1*dir*0.5*sqrt(2);
			//
			//	r.w = clipVert.w;
			//	return r;
			//}

			//float ComputeInClipSize (float4 clipVert)
			//{
			//	float r;
			//	float4 TrPosClip = ComputeScreenPos ( UnityObjectToClipPos(_TrPos) );
			//
			//	r = abs (ComputeScreenPos (clipVert).x - TrPosClip.x);
			//
			//	return r;
			//}

			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = ClipVertHalfFromCenter (UnityObjectToClipPos(v.vertex));
				o.vertex = UnityObjectToClipPos(v.vertex);
				//_InClipSize = ComputeInClipSize (o.vertex);
				//o.vertex.xyz = UnityObjectToViewPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//if (o.uv.x == 1 && o.uv.y == 1)
				//{
				//	_topRightVertex = o.vertex;
				//}
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
