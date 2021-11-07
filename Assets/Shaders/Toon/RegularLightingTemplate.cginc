#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#include "ZeldaLikeToonLib.cginc"

struct MeshData
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
};

struct Interpolators
{
    float2 uv : TEXCOORD0;
    float4 pos : SV_POSITION;
    float3 normal : TEXCOORD1;
    float3 worldPosition : TEXCOORD2;
    SHADOW_COORDS(3)
    LIGHTING_COORDS(4,5)
};

float4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
float _GlossRange;

Interpolators vert (MeshData v)
{
    Interpolators o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.normal = UnityObjectToWorldNormal(v.normal);
    o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
    TRANSFER_SHADOW(o)
    TRANSFER_VERTEX_TO_FRAGMENT(o)
    
    return o;
}

float4 frag (Interpolators i) : SV_Target
{
    float shadow = SHADOW_ATTENUATION(i);
    float attenuation = LIGHT_ATTENUATION(i);
    
    // vectors
    float3 normalDirection = normalize(i.normal);
    float3 lightDirection = normalize(UnityWorldSpaceLightDir(i.worldPosition));
    float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPosition);
    float3 halfDirection = normalize(lightDirection + viewDirection);

    // dot products
    float dotNL = getDotNL(normalDirection, lightDirection);
    float dotHN = getDotHN(halfDirection, normalDirection);

    // lights
    float4 diffuseLight = dotNL * shadow * attenuation * _LightColor0;
    float4 specularLight = pow(dotHN, mapToGloss(_GlossRange)) * shadow * attenuation * _LightColor0;

    float4 light = diffuseLight + specularLight;

    // textures
    float4 textureColor = tex2D(_MainTex, i.uv);
    
    return light * _Color * textureColor;
}