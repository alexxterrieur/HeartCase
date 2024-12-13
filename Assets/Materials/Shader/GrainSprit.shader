Shader "Custom/GrainEffect"
{
    Properties
    {
        MainTex ("Sprite Texture", 2D) = "white" {}
        GrainIntensity ("Grain Intensity", Range(0, 0.1)) = 0.05
        SeedX ("Grain Seed X", Float) = 12.9898
        SeedY ("Grain Seed Y", Float) = 78.233
        Amplification ("Grain Amplification", Float) = 43758.5453
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D MainTex;
            float4 MainTex_ST;
            float GrainIntensity;
            float SeedX; 
            float SeedY; 
            float Amplification; 

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(MainTex, i.uv);

                float grain = frac(sin(dot(i.uv, float2(SeedX, SeedY))) * Amplification);

                col.rgb += grain * GrainIntensity;

                return col;
            }
            ENDCG
        }
    }
}