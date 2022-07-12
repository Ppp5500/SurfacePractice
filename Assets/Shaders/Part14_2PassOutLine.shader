Shader "Custom/Part14_2PassOutLine"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        cull front
        CGPROGRAM
        #pragma surface surf Nolight vertex:vert noshadow noambient

        #pragma target 3.0

        sampler2D _MainTex;

        void vert(inout appdata_full v) {

            v.vertex.xyz = v.vertex.xyz + v.normal.xyz * 0.005;

        }

        struct Input
        {
            float2 color:COLOR;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {

        }

        float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(0, 0, 0, 1);
        }
        ENDCG


        cull back
        CGPROGRAM
        #pragma surface surf Toon noambient

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        float4 LightingToon(SurfaceOutput s, float3 lightDir, float atten) {
            
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

            if      (ndotl > 0.5)
                ndotl = 1;
            else if (ndotl > 0.4)
                ndotl = 0.3;
            else
                ndotl = 0;

            // ceil함수는 소숫점이 있는 숫자를
            // 정수로 올려줌
            /*ndotl = ndotl * 5;
            ndotl = ceil(ndotl) / 5;*/

            float4 final;

            final.rgb = s.Albedo * ndotl * _LightColor0.rgb;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
