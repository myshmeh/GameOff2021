Shader "Unlit/ElectricalShock"
{
    Properties
    {
        _LineTex ("Line Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NoiseStrength ("Noise Strength", Range(0, 1)) = .1
        _Color("Color", Color) = (1, 1, 1, 1)
        _Brightness ("Brightness", Range(0, 1)) = .5
        _NoiseFrequency ("Noise Frequency", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend One One
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD1;
                float2 noiseUv : TEXCOORD0;
                float3 normal: TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _LineTex;
            float4 _LineTex_ST;
            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            float _NoiseStrength;
            float4 _Color;
            float _Brightness;
            float _NoiseFrequency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _LineTex);
                o.noiseUv = TRANSFORM_TEX(v.uv, _NoiseTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float mapBrightness()
            {
                float mapped = _Brightness + 1.5;
                return mapped;
            }

            float mapNoiseFrequency()
            {
                float mapped = (_NoiseFrequency * .5 + .5) * 10;
                return mapped;
            }

            float4 frag (v2f i) : SV_Target
            {
                float offsettable = _Time.a * mapNoiseFrequency() % 1 > .5;
                float noiseSamplingOffset = offsettable ? _SinTime.x + .5 : 0;
                float2 noiseSamplingPos = float2(i.noiseUv.x + noiseSamplingOffset, i.noiseUv.y);
                float4 noise = tex2D(_NoiseTex, noiseSamplingPos);
                float2 lineSamplingPos = i.uv + noise * _NoiseStrength;
                float4 lineColor = tex2D(_LineTex, lineSamplingPos);

                return lineColor * _Color * mapBrightness();
            }
            ENDCG
        }
    }
}
