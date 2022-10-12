Shader "Custom/SceneryDraw"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Noise("Noise", 2D) = "white" {}
        _NoiseTiling("Noise Tiling", Float) = 1.0
        _EdgeColor("Edge Color", Color) = (0,1,1,1)
        _EdgeSize("Edge Size", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
            #pragma vertex vert
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        sampler2D _Noise;
        float _NoiseTiling;
        float _FadeRange;
        fixed4 _EdgeColor;
        fixed _EdgeSize;

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = v.vertex.xyz;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed noiseValue = tex2D(_Noise, fixed2(IN.worldPos.x, IN.worldPos.z) * _NoiseTiling).r;
            fixed distance = sqrt(pow(IN.worldPos.x, 2) + pow(IN.worldPos.z, 2)) - _FadeRange;
            fixed heightDistance = abs(IN.worldPos.y) - pow(distance, 2) / 5 + noiseValue;
            clip(-distance);
            clip(-heightDistance);
            if (distance > -_EdgeSize || heightDistance > -_EdgeSize)
            {
                o.Albedo = _EdgeColor;
            }
            else
            {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex);
            }
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
