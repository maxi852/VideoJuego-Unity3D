Shader "Custom/DarknessShader"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1) // Color de oscuridad
        _Intensity ("Intensity", Range(0,1)) = 1 // Intensidad de la oscuridad
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

struct appdata_t
{
    float4 vertex : POSITION;
    float4 color : COLOR;
};

struct v2f
{
    float4 pos : POSITION;
    float4 color : COLOR;
};

uniform float4 _Color;
uniform float _Intensity;

v2f vert(appdata_t v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.color = v.color;
    return o;
}

half4 frag(v2f i) : SV_Target
{
                // Aplicar oscuridad basada en el color y la intensidad
    half4 darkness = _Color * _Intensity;
    return darkness;
}
            ENDCG
        }
    }
FallBack"Diffuse"
}
