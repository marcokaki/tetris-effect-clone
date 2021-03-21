﻿Shader "Custom/TileMapShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend One OneMinusSrcAlpha
        ZWrite Off

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;            
            float4 _MainTex_ST;
            int _TileMapArray[200];


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col;

                fixed4 uvCol = fixed4(i.uv.x, i.uv.y, 0, 1);
                col = tex2D(_MainTex, i.uv);               
                col *= _TileMapArray[floor(i.uv.x) + 10 * floor(i.uv.y)];

                return col;
            }
            ENDCG
        }
    }
}
