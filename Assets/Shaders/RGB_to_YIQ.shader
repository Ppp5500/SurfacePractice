Shader "Custom/RGB_to_YIQ"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed Grayscale;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            Grayscale = (0.2989 * c.r + 0.5870 * c.g + 0.1140 * c.b);
            o.Albedo = Grayscale;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
