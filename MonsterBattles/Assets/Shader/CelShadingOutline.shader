Shader "Custom/CelShadingOutline" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}		
		_Treshold ("Cel treshold", Range(1., 20.)) = 5.
        _Ambient ("Ambient intensity", Range(0., 0.5)) = 0.1
		
        _Outline ("_Outline", Range(0,0.1)) = 0.025
        _OutlineColor ("OutlineColor", Color) = (0, 0, 0, 1)
	}
		SubShader{
		Tags{
		"RenderType" = "Opaque"
	}
	LOD 200
	
	Pass {
            Tags { "RenderType"="Opaque" }
            Cull Front
 
            CGPROGRAM
 
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct v2f {
                float4 pos : SV_POSITION;
            };
 
            float _Outline;
            float4 _OutlineColor;
 
            float4 vert(appdata_base v) : SV_POSITION {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 normal = mul((float3x3) UNITY_MATRIX_MV, v.normal);
                normal.x *= UNITY_MATRIX_P[0][0];
                normal.y *= UNITY_MATRIX_P[1][1];
                o.pos.xy += normal.xy * _Outline;
                return o.pos;
            }
 
            half4 frag(v2f i) : COLOR {
                return _OutlineColor;
            }
 
            ENDCG
        }
		
	CGPROGRAM
	#pragma surface surf CelShadingForward
	#pragma target 3.0

	
	sampler2D _MainTex;
	fixed4 _Color;	
	float _Treshold;
    half _Ambient;

	float LightToonShading(float3 normal, float3 lightDir)
    {
        float NdotL = max(0.0, dot(normalize(normal), normalize(lightDir)));
        return floor(NdotL * _Treshold) / (_Treshold - 0.5);
    }

	half4 LightingCelShadingForward(SurfaceOutput s, half3 lightDir, half atten) {
		half saturation = saturate(LightToonShading(s.Normal, lightDir) + _Ambient);
		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * (saturation * atten * 2);//lerp(s.Albedo, _LightColor0.rgb, saturation);
		/*half NdotL = dot(s.Normal, lightDir);
		if (NdotL <= 0.0) NdotL = 0;
		else NdotL = 1;
		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);
		*/
		c.a = s.Alpha;
		return c;
	}

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}