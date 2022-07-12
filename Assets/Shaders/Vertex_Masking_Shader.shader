Shader "Custom/Vertex_Masking_Shader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2("Albedo (RGB)", 2D) = "white" {}
        _MainTex3("Albedo (RGB)", 2D) = "white" {}
        _MainTex4("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalMap", 2D) = "bump" {}
        _Metallic("Metallic", Range(0,1)) = 0
        _Smoothness("Smoothness", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard

        sampler2D _MainTex;
        sampler2D _MainTex2;
        sampler2D _MainTex3;
        sampler2D _MainTex4;
        sampler2D _BumpMap;
        float _Metallic;
        float _Smoothness;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
            float2 uv_MainTex3;
            float2 uv_MainTex4;
            float4 color:COLOR;
            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 d = tex2D (_MainTex2, IN.uv_MainTex);
            fixed4 e = tex2D (_MainTex3, IN.uv_MainTex);
            fixed4 f = tex2D (_MainTex4, IN.uv_MainTex);
            fixed4 m = IN.color;
            fixed3 b = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            //o.Albedo = c.rgb * (d.rgb + IN.color.r) * (e.rgb + IN.color.g);
            //o.Albedo = IN.color.r;
            // 
            //o.Albedo = lerp(c.rgb, d.rgb, IN.color.r);
            //o.Albedo = lerp(o.Albedo, e.rgb, IN.color.g);
            //o.Albedo = lerp(o.Albedo, f.rgb, IN.color.b);

            //o.Albedo = d.rgb * IN.color.r + e.rgb * IN.color.g + f.rgb * IN.color.b
            //    + c.rgb * (1 - (IN.color.r + IN.color.g + IN.color.b));

            o.Albedo = c.rgb * (1 - (m.r + m.g + m.b)) + d.rgb * m.r + e.rgb * m.g + f.rgb * m.b;
            o.Normal = b;
            o.Metallic = _Metallic;
            //o.Smoothness = _Smoothness;
            o.Smoothness = IN.color.g + _Smoothness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
