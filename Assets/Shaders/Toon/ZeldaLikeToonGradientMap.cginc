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
    float3 objectPosition : TEXCOORD3;
    SHADOW_COORDS(4)
};

float4 _Color;
sampler2D _ColorGradientMap;
float4 _ColorGradientMap_ST;
float _YScale;
sampler2D _MainTex;
float4 _MainTex_ST;
float4 _AmbientLightColor;
float _DiffuseLightGradientStrength;
float _SpecularLightGradientStrength;
float _GlossRange;
float _FresnelThicknessRange;
float _FresnelLightGradientStrength;
float _UsesSpecularLight;
float _UsesFresnelLight;

Interpolators vert (MeshData v)
{
    Interpolators o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.normal = UnityObjectToWorldNormal(v.normal);
    o.objectPosition = v.vertex;
    o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
    TRANSFER_SHADOW(o)
    
    return o;
}

float4 frag (Interpolators i) : SV_Target
{
    // shadow
    float shadow = SHADOW_ATTENUATION(i);

    // vectors
    float3 normalDirection = normalize(i.normal);
    float3 lightDirection = normalize(UnityWorldSpaceLightDir(i.worldPosition));
    float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPosition);
    float3 halfDirection = normalize(lightDirection + viewDirection);

    // dot products
    float dotNL = getDotNL(normalDirection, lightDirection);
    float dotHN = getDotHN(halfDirection, normalDirection);
    float dotNV = getDotNV(normalDirection, viewDirection);

    // lights
    float4 diffuseLight = getToonDiffuseLight(dotNL, shadow, _DiffuseLightGradientStrength, _LightColor0);
    float4 specularLight = getToonSpecularLight(dotHN, dotNL, shadow, _SpecularLightGradientStrength, mapToGloss(_GlossRange), _LightColor0);
    float4 fresnelLight = getToonFresnelLight(dotNV, dotNL, shadow, mapToFresnelThickness(_FresnelThicknessRange), _FresnelLightGradientStrength, _LightColor0);
    float4 gradientMappedAmbientLight = tex2D(_ColorGradientMap, _AmbientLightColor.rr);
    
    float4 light = diffuseLight + specularLight * _UsesSpecularLight + fresnelLight * _UsesFresnelLight + gradientMappedAmbientLight;

    // textures
    float4 textureColor = tex2D(_MainTex, i.uv);
    float2 sampleCoord = float2(textureColor.g, .5f);
    float4 sampleColor = tex2D(_ColorGradientMap, sampleCoord);

    float2 depthColorSampleCoord = float2(i.objectPosition.y / (1.0f/_YScale) * .5f + .5f, .5f);
    float4 depthColor = tex2D(_ColorGradientMap, depthColorSampleCoord);

    float replaceWith_Color = textureColor.r == 1 && textureColor.g == 0 && textureColor.b == 0;

    float4 definitiveColors = light * depthColor;
    
    return definitiveColors * (replaceWith_Color ? _Color : sampleColor);
}