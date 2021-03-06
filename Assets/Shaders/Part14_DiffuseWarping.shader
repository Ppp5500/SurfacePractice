Shader "Custom/Part14_DiffuseWarping"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normal", 2D) = "bump"{}
        _RampTex("RampTex", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf warp

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _RampTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        float4 Lightingwarp(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {
            //float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

            //// ndotl(음영) 값에 따라
            //// 텍스쳐의 색상을 입힘
            //float4 ramp = tex2D(_RampTex, float2(ndotl, 0.5));


            //float4 final;
            //final.rgb = s.Albedo.rgb * ramp.rgb;
            //final.a = s.Alpha;
            //return final;

            float3 H = normalize(lightDir + viewDir);
            float spec = saturate(dot(s.Normal, H));
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            float4 ramp = tex2D(_RampTex, float2(ndotl, spec));

            float4 final;
            final.rgb = (s.Albedo.rgb * ramp.rgb) + (ramp.rgb * 0.1);
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
