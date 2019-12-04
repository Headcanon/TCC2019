Shader "Unlit/TreeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Ajust ("ajust",Float)= 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 color:COLOR;
				float3 normal:NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 color:COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Ajust;
            v2f vert (appdata v)
            {
                v2f o;

				if (v.color.y > _Ajust) {
					float wave = sin(_Time.y +v.vertex.z*1000)*0.1;
					float4 myvertex = float4(v.vertex.x, v.vertex.y + wave,
						v.vertex.z, v.vertex.w);
					o.vertex = UnityObjectToClipPos(myvertex);
				}
				else {
					o.vertex = UnityObjectToClipPos(v.vertex);
				}
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
				float bright = dot(v.normal, normalize(_WorldSpaceLightPos0.xyz));
				o.color = v.color*bright;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv)*float4(i.color,1);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
