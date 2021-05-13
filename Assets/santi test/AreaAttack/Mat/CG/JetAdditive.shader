Shader "Jettelly/JetAdditive"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "blue" {}
        _DisTex ("Distortion Texture", 2D) = "blue"{}
        [Space(10)]
        _DisValue ("Distortion Value", Range(2, 10)) = 3
        _DisSpeed ("Distortion Speed", Range(-0.4, 0.4)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Blend SrcAlpha One
        ZWrite Off
        Cull Off

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
                float4 color : COLOR;   // color input de los vertices de nuestro mesh
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;   // color output de los vertices de nuestro mesh
            };

            sampler2D _MainTex;
            sampler2D _DisTex;
            float4 _MainTex_ST;
            float _DisSpeed;
            float _DisValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {        
                half distortion = tex2D(_DisTex, i.uv + (_Time * _DisSpeed)).r;
                i.uv.x += distortion / _DisValue;

                fixed4 col = tex2D(_MainTex, i.uv);
                return float4(i.color.rgb, col.a * i.color.a);
            }
            ENDCG
        }
    }
}
