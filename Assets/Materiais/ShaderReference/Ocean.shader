Shader "Unlit/Ocean"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_MyColor("Colour", Color) = (1, 0, 0, 1)    // (R, G, B, A)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float intensity : PSIZE;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _MyColor;
            v2f vert (appdata v)
            {
                v2f o;
				float wave = sin(_Time.y + v.vertex.x + v.vertex.z);
				float4 myvertex = float4(v.vertex.x, v.vertex.y+ wave,
					v.vertex.z, v.vertex.w);
                o.vertex = UnityObjectToClipPos(myvertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.intensity = wave*0.1;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				float2 myuv = float2(-i.uv.x,i.uv.y + _Time.x);
				float2 myuv2 = float2(i.uv.x - _Time.x, i.uv.y );
				fixed4 col =  tex2D(_MainTex, myuv);
				fixed4 col2 = tex2D(_MainTex, myuv2);
				col= (col + col2) / 2 * _MyColor + i.intensity;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
