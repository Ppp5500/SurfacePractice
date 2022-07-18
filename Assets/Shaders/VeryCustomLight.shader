Shader "Custom/VeryCustomLight"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Test noambient

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        // 함수 이름은 Lighting + 라이트 이름
        float4 LightingTest(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(0.5, 0.5, 1, 1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}