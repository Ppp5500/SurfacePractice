Shader "Custom/Metallic_And_Smoothness"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normalmap", 2D) = "bump" {}
        _Occlusion("Occlusion", 2D) = "bump" {}
        _Metallic("Metallic", Range(0,1)) = 0
        _Smoothness("Smoothness", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _Occlusion;
        float _Metallic;
        float _Smoothness;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            // UnpackNormal은 float3 데이터로 받아야함
            fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            // 오클루젼은 메인 텍스쳐와 같은 uv를 받아야함
            fixed4 oc = tex2D(_Occlusion, IN.uv_MainTex);

            o.Occlusion = oc;
            o.Albedo = c.rgb;
            o.Normal = n;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
