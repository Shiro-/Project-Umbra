Shader "Custom/Transparent"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Alpha("Alpha Value", Range(0.0, 1.0)) = 1.0
	}
		SubShader
		{
			Pass
			{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
			LOD 200

			CGPROGRAM

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0
			#pragma fragment frag

			sampler2D _MainTex;
			uniform float _Alpha;
			uniform half4 _Color;

			half4 frag() : COLOR
			{
				float4 col = _Color;
				col.a = _Alpha;

				return col;
			}
			ENDCG
			}
		}
			FallBack "Diffuse"
}
