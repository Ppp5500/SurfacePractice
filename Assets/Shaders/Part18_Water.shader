Shader "Custom/Part18_Water"
{
    Properties
    {
        _Bumpmap("NormalMap", 2D) = "bump" {}
        _Cube("Cube", Cube) = "" {}
        _SPColor("Specular Color", color) = (1,1,1,1)
        _SPPower("Specular Power", Range(50, 300)) = 150
        _SPMulti("Specular Multiply", Range(1,10)) = 3
        _WaveH("Wave Height", Range(0,1)) = 0.5
        _WaveL("Wave Length", Range(5,20)) = 10
        _WaveT("Wave Timeing", Range(1,5)) = 1
        _Refract("Refract Strength", Range(0, 0.2)) = 0.1
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}

        GrabPass{}

        CGPROGRAM
        #pragma surface surf WaterSpecular alpha:fade vertex:vert

        samplerCUBE _Cube;
        sampler2D _Bumpmap;
        sampler2D _GrabTexture;
        float4 _SPColor;
        float _SPPower;
        float _SPMulti;
        float _WaveH;
        float _WaveL;
        float _WaveT;
        float _Refract;

        struct Input
        {
            float2 uv_Bumpmap;
            float3 worldRefl;
            float3 viewDir;
            float4 screenPos;
            INTERNAL_DATA
        };

        void vert(inout appdata_full v)
        {
            float movement;
            movement = sin(abs((v.texcoord.x * 2 - 1) * _WaveL) + _Time.y * _WaveT) * _WaveH;
            movement += sin(abs((v.texcoord.y * 2 - 1) * _WaveL) + _Time.y * _WaveT) * _WaveH;
            v.vertex.y += sin(abs(v.texcoord.x * 2 - 1) * 12)*0.2;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // 물의 흔들림
            float3 normal1 = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap + _Time.x * 0.1));
            float3 normal2 = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap - _Time.x * 0.1));
            o.Normal = (normal1+normal2)/2;

            // 하늘빛 반사 효과
            float3 refcolor = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal));

            // 굴절 효과
            float3 screenUV = IN.screenPos.rgb / IN.screenPos.a;
            float3 refraction = tex2D(_GrabTexture, (screenUV.xy + o.Normal.xy * _Refract));


            // rim term
            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, 2);

            o.Emission = (refcolor * rim + refraction)*0.5;
            o.Alpha = 1;
        }

        float4 LightingWaterSpecular(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            // 스페큘러
            float3 H = normalize(lightDir + viewDir);
            float spec = saturate(dot(H, s.Normal));
            spec = pow(spec, _SPPower);

            // 최종 결과
            float4 finalColor;
            finalColor.rgb = spec * _SPColor.rgb * _SPMulti;
            finalColor.a = s.Alpha + spec;

            return finalColor;
        }
        ENDCG
    }
    FallBack "Legacy Shaders/Transparent/Vertexlit"
}
