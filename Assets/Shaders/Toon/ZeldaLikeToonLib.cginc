float mapToGloss(float glossRange)
{
    float gloss = glossRange * 12;
    return exp2(gloss) + 2;
}

float mapToFresnelThickness(float fresnelThicknessRange)
{
    return fresnelThicknessRange * 1 + 1;
}

float getDotNL(float3 normal, float3 lightDirection)
{
    return max(0, dot(normal, lightDirection));
}

float getDotHN(float3 halfDirection, float3 normal)
{
    return max(0, dot(halfDirection, normal));
}

float getDotNV(float3 normal, float3 viewDirection)
{
    return dot(normal, viewDirection);
}

float4 getToonDiffuseLight(float dotNL, float shadow, float gradientStrength, float4 lightColor)
{
    float diffuseLight = dotNL * shadow;
    return smoothstep(0, 0 + gradientStrength, diffuseLight) * lightColor;
}

float4 getToonSpecularLight(float dotHN, float dotNL, float shadow, float gradientStrength, float gloss, float4 lightColor)
{
    float specularLight = pow(dotHN, gloss) * (dotNL > 0) * shadow;
    return smoothstep(0, 0 + gradientStrength, specularLight) * lightColor;
}

float4 getToonFresnelLight(float dotNV, float dotNL, float shadow, float thickness, float gradientStrength, float4 lightColor)
{
    float fresnel = (1 - dotNV) * thickness * shadow;
    float cutoutFresnel = fresnel * dotNL;
    return smoothstep(0.6, 0.6 + gradientStrength, cutoutFresnel) * lightColor;
}