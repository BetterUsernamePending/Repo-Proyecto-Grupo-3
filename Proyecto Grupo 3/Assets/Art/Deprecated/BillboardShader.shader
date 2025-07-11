Shader "Custom/Billboard"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
//
        _OriginalColor("Original Color", Color) = (1,0,1,1)
        _TargetColor("Target Color", Color) = (0,0,1,1)
        _Tolerance("Tolerance", Range(0,0.01)) = 0.001
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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            const float3 vect3Zero = float3(0.0, 0.0, 0.0);

            sampler2D _MainTex;
            //
            float4 _MainTex_ST;
            float4 _OriginalColor;
            float4 _TargetColor;
            float _Tolerance;

            v2f vert(appdata v)
            {
                v2f o;

                float4 camPos = float4(UnityObjectToViewPos(vect3Zero).xyz, 1.0);    // UnityObjectToViewPos(pos) is equivalent to mul(UNITY_MATRIX_MV, float4(pos, 1.0)).xyz,
                                                                                    // This gives us the camera's origin in 3D space (the position (0,0,0) in Camera Space)

                float4 viewDir = float4(v.pos.x, v.pos.y, 0.0, 0.0);            // Since w is 0.0, in homogeneous coordinates this represents a vector direction instead of a position
                float4 outPos = mul(UNITY_MATRIX_P, camPos + viewDir);            // Add the camera position and direction, then multiply by UNITY_MATRIX_P to get the new projected vert position

                o.pos = outPos;
                o.uv = v.uv;

                return o;
            }
/*
            fixed4 frag(v2f i) : SV_Target
            {
                // Don't need to do anything special, just render the texture
                return tex2D(_MainTex, i.uv);
            }*/
            //
            half4 frag (v2f i) : SV_Target
            {
                // sample the texture
                half4 col = tex2D(_MainTex, i.uv);
                
                if (col.a == 0)
                {
                    return half4(0,0,0,0);
                }

                if (length(col - _OriginalColor) < _Tolerance)
                {
                    return half4(_TargetColor.rgb, col.a);
                }

                return col;
            }
            ENDCG
        }
    }
}