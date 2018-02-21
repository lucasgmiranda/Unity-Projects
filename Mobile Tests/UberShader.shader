Shader "hidden/preview"
{
    Properties
    {
        Color_B3FB9611("Color", Color) = (0,0,0,0)
    }
    HLSLINCLUDE
    #define USE_LEGACY_UNITY_MATRIX_VARIABLES
    #include "CoreRP/ShaderLibrary/Common.hlsl"
    #include "CoreRP/ShaderLibrary/Packing.hlsl"
    #include "CoreRP/ShaderLibrary/Color.hlsl"
    #include "CoreRP/ShaderLibrary/UnityInstancing.hlsl"
    #include "CoreRP/ShaderLibrary/EntityLighting.hlsl"
    #include "ShaderGraphLibrary/ShaderVariables.hlsl"
    #include "ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
    #include "ShaderGraphLibrary/Functions.hlsl"
    float Vector1_889D3DCA;
    float4 Color_B3FB9611;
    float4 _Voronoi_FB130FE7_UV;
    float _Voronoi_FB130FE7_AngleOffset;
    float _Voronoi_FB130FE7_CellDensity;
    struct SurfaceInputs{
    	half4 uv0;
    };


    inline float2 unity_voronoi_noise_randomVector (float2 UV, float offset)
    {
        float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
        UV = frac(sin(mul(UV, m)) * 46839.32);
        return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
    }

    void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
    {
        float2 g = floor(UV * CellDensity);
        float2 f = frac(UV * CellDensity);
        float t = 8.0;
        float3 res = float3(8.0, 0.0, 0.0);

        for(int y=-1; y<=1; y++)
        {
            for(int x=-1; x<=1; x++)
            {
                float2 lattice = float2(x,y);
                float2 offset = unity_voronoi_noise_randomVector(lattice + g, AngleOffset);
                float d = distance(lattice + offset, f);

                if(d < res.x)
                {

                    res = float3(d, offset.x, offset.y);
                    Out = res.x;
                    Cells = res.y;

                }
            }

        }

    }
    struct GraphVertexInput
    {
    	float4 vertex : POSITION;
    	float3 normal : NORMAL;
    	float4 tangent : TANGENT;
    	float4 texcoord0 : TEXCOORD0;
    	UNITY_VERTEX_INPUT_INSTANCE_ID
    };
    struct SurfaceDescription{
    	float4 PreviewOutput;
    };
    GraphVertexInput PopulateVertexData(GraphVertexInput v){
    	return v;
    }
    SurfaceDescription PopulateSurfaceData(SurfaceInputs IN) {
    	SurfaceDescription surface = (SurfaceDescription)0;
    	float _Voronoi_FB130FE7_Out;
    	float _Voronoi_FB130FE7_Cells;
    	Unity_Voronoi_float(IN.uv0.xy, _Voronoi_FB130FE7_AngleOffset, _Voronoi_FB130FE7_CellDensity, _Voronoi_FB130FE7_Out, _Voronoi_FB130FE7_Cells);
    	if (Vector1_889D3DCA == 1) { surface.PreviewOutput = half4(_Voronoi_FB130FE7_Out, _Voronoi_FB130FE7_Out, _Voronoi_FB130FE7_Out, 1.0); return surface; }
    	return surface;
    }
    ENDHLSL

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct GraphVertexOutput
            {
                float4 position : POSITION;
                half4 uv0 : TEXCOORD;

            };

            GraphVertexOutput vert (GraphVertexInput v)
            {
                v = PopulateVertexData(v);

                GraphVertexOutput o;
                float3 positionWS = TransformObjectToWorld(v.vertex);
                o.position = TransformWorldToHClip(positionWS);
                o.uv0 = v.texcoord0;

                return o;
            }

            float4 frag (GraphVertexOutput IN) : SV_Target
            {
                float4 uv0 = IN.uv0;


                SurfaceInputs surfaceInput = (SurfaceInputs)0;;
                surfaceInput.uv0 = uv0;


                SurfaceDescription surf = PopulateSurfaceData(surfaceInput);
                return surf.PreviewOutput;

            }
            ENDHLSL
        }
    }
}
