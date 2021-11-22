Shader "Custom/DottedSquare"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _DotFrequency ("Dot Frequency", Range(0, 1)) = .5
        _LineThickness ("Line Thickness", Range(0, 1)) = .5
        _AnimationSpeed ("Animation Speed", Range(-1, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _DotFrequency;
            float _LineThickness;
            float _AnimationSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float mappedDotFrequency()
            {
                float offset = _DotFrequency * .9f + .1f;
                float pi10x = 31.1415f;
                return pi10x * 5.0f * offset;
            }

            float getDistantThreshold()
            {
                return (1 - _LineThickness) * .5f;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                
                float dotFreq = mappedDotFrequency();
                fixed whitenessByX = saturate(sin(i.uv.x * dotFreq)) > 0;
                fixed whitenessByY = saturate(sin((i.uv.y + -_Time.y * _AnimationSpeed) * dotFreq)) > 0;
                fixed distantThreshold = getDistantThreshold();
                fixed whitenessByDistance = (abs(0.5f - i.uv.x) > distantThreshold) + (abs(0.5f - i.uv.y) > distantThreshold);
                fixed whiteness = whitenessByX * whitenessByY * whitenessByDistance;

                return whiteness * tex * _Color;
            }
            ENDCG
        }
    }
}
