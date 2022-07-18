Shader "Custom/Hologram"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _HoloColor("HoloColor", Color) = (1,1,1)
        _HoloStrength("HoloStrength", Range(1,10)) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf nolight noambient alpha:fade

        sampler2D _MainTex;
        float3 _HoloColor;
        float _HoloStrength;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 viewDir;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            
            o.Emission = _HoloColor;
            float frenel = saturate(dot(o.Normal, IN.viewDir));
            frenel = (pow(1 - frenel, 10 - _HoloStrength) + pow(frac(IN.worldPos.g  - _Time.x*2), 30)) * (abs(sin(_Time.y)) + 0.3);
            
            // 이 값을 알파에 넣으면 투명 효과
            //frenel = pow(1 - frenel, 10 - _HoloStrength);

            // 깜빡이는 효과
            //frenel = frenel * abs(sin(_Time.y));

            // 줄무늬 효과
            // frac() 함수는 입력값의 소숫점 부분만 리턴함;
            //frenel = frenel + pow(frac(IN.worldPos.g * 3 - _Time.y), 30);
            o.Alpha = frenel;
        }

        float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(0, 0, 0, s.Alpha);
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}
