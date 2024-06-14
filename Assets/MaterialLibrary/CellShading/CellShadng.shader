Shader "CellShading" {
        Properties {
            _MainTex ("Texture", 2D) = "white" {}
             _Ramp ("Ramp", 2D) = "white" {}
             _ShadowCol ("Shadow Color", Color) = (0,0,0,1)
             _HighlightCol ("Highlight Color", Color) = (0,0,0,1)
             _Levels ("Level", Int) = 2
        }

        SubShader {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        #pragma surface surf SimpleLambert
  
        sampler2D _Ramp;
        float3 _HighlightCol;
        float3 _ShadowCol;
        int _Levels;

        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = dot (s.Normal, lightDir);
            half4 c;
            half diff = NdotL * 0.5 + 0.5;
            half diff2 = round((NdotL * 0.5 + 0.5)*_Levels)/max(1,_Levels);
            half3 ramp = lerp(_ShadowCol, _HighlightCol,diff2);
            // half3 ramp = tex2D (_Ramp, float2(diff, diff)).rgb;
            c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
            c.a = s.Alpha;
            return c;
        }
  
        struct Input {
            float2 uv_MainTex;
        };
        
        sampler2D _MainTex;
        
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
        }
        Fallback "Diffuse"
    }