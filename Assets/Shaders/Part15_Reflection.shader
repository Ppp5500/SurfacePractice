﻿Shader "Custom/Part15_Reflection"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalMap", 2D) = "bump"{}
        _MaskMap("MaskMap", 2D) = "white"{}
        _Cube("cubemap", Cube) = ""{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert noambient

        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _MaskMap;
        samplerCUBE _Cube;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_MaskMap;
            float3 worldRefl;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            float4 re = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal));
            float4 m = tex2D(_MaskMap, IN.uv_MaskMap);

            o.Albedo = c.rgb;
            o.Emission = re.rgb * m.r;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
