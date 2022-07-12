﻿Shader "Custom/Get_Propeties_From_Outside"
{
	Properties
	{
		_Red("Red", Range(0,1)) = 0
		_Green("Green", Range(0, 1)) = 0
		_Blue("Blue", Range(0, 1)) = 0
        _BrightDark("Brightness&Darkness", Range(-1,1))=0
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0


        struct Input
        {
            float4 color : COlOR;
        };

        float _Red;
        float _Green;
        float _Blue;
        float _BrightDark;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = float3(_Red, _Green, _Blue) + _BrightDark;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
