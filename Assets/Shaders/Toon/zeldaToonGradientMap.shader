Shader "Unlit/zeldaToonGradientMap"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _ColorGradientMap ("Color Gradient Map", 2D) = "white" {}
        _YScale ("Y Scale", Float) = 1
        [Toggle] _UsesSpecularLight ("Uses Specular Light", Float) = 1
        [Toggle] _UsesFresnelLight ("Uses Fresnel Light", Float) = 1
        [HDR] _AmbientLightColor("Ambient Light Color", Color) = (0.4, 0.4, 0.4, 1)
        _DiffuseLightGradientStrength ("Diffuse Light Gradient Strength", Range(0.01, 0.1)) = 0.01
        _SpecularLightGradientStrength ("Specular Light Gradient Strength", Range(0.01, 0.1)) = 0.01
        _FresnelLightGradientStrength ("Fresnel Light Gradient Strength", Range(0.01, 0.5)) = 0.01
        _GlossRange ("Gloss Range", Range(0, 1)) = 1
        _FresnelThicknessRange ("Fresnel Thickness Range", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Tags { "LightMode" = "ForwardBase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "ZeldaLikeToonGradientMap.cginc"
            ENDCG
        }
        
        Pass {
            Tags { "LightMode" = "ForwardAdd" }
            Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd

            #include "RegularLightingTemplate.cginc"
            ENDCG
        }
        
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
