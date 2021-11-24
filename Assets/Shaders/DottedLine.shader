Shader "Custom/DottedLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _DotFrequency ("Dot Frequency", Range(0, 1)) = .5
        _AnimationSpeed ("Animation Speed", Range(-1, 1)) = 0
    }
    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "Queue"="Transparent" 
        }
        LOD 100

        Pass
        {
            ZWrite Off
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

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed whiteness = saturate(sin((i.uv.y + -_Time.y * _AnimationSpeed) * mappedDotFrequency())) > 0;
                return whiteness * tex * _Color;
            }
            ENDCG
        }
    }
}
