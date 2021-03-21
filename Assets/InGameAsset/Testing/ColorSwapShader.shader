Shader "Custom/ColorSwapShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
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
            int _PieceArray[16];
            half4x4 _ColorMatrix;


            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col;
                col = tex2D(_MainTex, i.uv);

                fixed x = col.r;

                col = _ColorMatrix[x * 3];
                //col *= _PieceArray[floor(i.uv.x) + 4 * floor(i.uv.y)];

                return col;
            }
            ENDCG
        }
    }
}
