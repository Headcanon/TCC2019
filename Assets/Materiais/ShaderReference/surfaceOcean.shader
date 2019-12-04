Shader "Custom/surfaceOcean"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _Normal;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v) {
			//v.vertex.xyz += v.normal;
			float wave = sin(_Time.y + v.vertex.x + v.vertex.z);
			float4 myvertex = float4(v.vertex.x, v.vertex.y + wave,
				v.vertex.z, v.vertex.w);
			v.vertex = myvertex;
			v.normal = float3(v.normal.x + wave*2, v.normal.y , v.normal.z + wave*2);
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float2 myuv = float2(-IN.uv_MainTex.x, IN.uv_MainTex.y + _Time.x);
			float2 myuv2 = float2(IN.uv_MainTex.x - _Time.x, IN.uv_MainTex.y);
			fixed4 c = tex2D(_MainTex, myuv);
			fixed4 c2 = tex2D(_MainTex, myuv2);
			c = (c + c2) / 2 * _Color;

			fixed3 n = UnpackNormal(tex2D(_Normal, myuv));
			fixed3 n2 = UnpackNormal(tex2D(_Normal, myuv2));
			//o.Normal = (n+n2)/2;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
