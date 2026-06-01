Shader "Hidden/RectDrawer"
{
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            ZWrite Off
            ZTest Always
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float4 _PosSize;

            bool isInsideRectangle(float2 pixelPos)
            {
                return pixelPos.x >= _PosSize.x && pixelPos.x < (_PosSize.x + _PosSize.z) &&
                       pixelPos.y >= _PosSize.y && pixelPos.y < (_PosSize.y + _PosSize.w);
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 pixelPos = floor(input.positionCS.xy);

                return isInsideRectangle(pixelPos) ? half4(1, 1, 1, 1) : half4(0, 0, 0, 1);
            }

            ENDHLSL
        }
    }
}