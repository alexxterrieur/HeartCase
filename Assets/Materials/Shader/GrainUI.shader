Shader "UI/GrainEffect"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _GrainIntensity ("Grain Intensity", Range(0, 0.1)) = 0.05
        _SeedX ("Grain Seed X", Float) = 12.9898
        _SeedY ("Grain Seed Y", Float) = 78.233
        _Amplification ("Grain Amplification", Float) = 43758.5453
    }
    SubShader
    {
        Tags
        {
            "Queue"="Overlay"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GrainIntensity;
            float _SeedX;
            float _SeedY;
            float _Amplification;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float grain = frac(sin(dot(i.uv, float2(_SeedX, _SeedY))) * _Amplification);

                col.rgb += grain * _GrainIntensity;

                return col;
            }
            ENDCG
        }
    }
}