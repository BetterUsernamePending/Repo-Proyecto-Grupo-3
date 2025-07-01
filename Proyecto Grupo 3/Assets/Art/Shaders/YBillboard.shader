Shader "Custom/YBillboard"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata i)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(i.uv, _MainTex);
                
                //break out the axis
                float3 right = normalize(UNITY_MATRIX_V._m00_m01_m02);
                float3 up = float3(0, 1, 0);
                float3 forward = normalize(UNITY_MATRIX_V._m20_m21_m22);
                //get the rotation parts of the matrix
                float4x4 rotationMatrix = float4x4( right,      0,
                                                    up,         0,
                                                    forward,    0,
                                                    0, 0, 0,    1);
                
                //the inverse of a rotation matrix happens to always be the transpose
                float4x4 rotationMatrixInverse = transpose(rotationMatrix);
                
                //apply the rotationMatrixInverse, model, view and projection matrix
                float4 pos = i.vertex;
                pos = mul(rotationMatrixInverse, pos);
                pos = mul(UNITY_MATRIX_M, pos);
                pos = mul(UNITY_MATRIX_V, pos);
                pos = mul(UNITY_MATRIX_P, pos);
                o.vertex = pos;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
