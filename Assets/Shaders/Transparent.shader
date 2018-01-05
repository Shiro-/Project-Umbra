//Shader "Custom/Transparent"
//{
//	Properties
//	{
//		_Color("Color", Color) = (1,1,1,1)
//		_MainTex("Albedo (RGB)", 2D) = "white" {}
//		_Alpha("Alpha Value", Range(0.0, 1.0)) = 1.0
//	}
//		SubShader
//		{
//			Pass
//			{
//			Blend SrcAlpha OneMinusSrcAlpha
//
//			Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
//			LOD 200
//
//			CGPROGRAM
//
//			// Use shader model 3.0 target, to get nicer looking lighting
//			#pragma target 3.0
//			#pragma fragment frag
//
//			sampler2D _MainTex;
//			uniform float _Alpha;
//			uniform half4 _Color;
//
//			/*half4 frag() : COLOR
//			{
//				_Color.a = _Alpha;
//				float4 col = _Color;
//
//				return col;
//			}*/
//
//			/*struct Input
//			{
//				float2 uv_MainTex;
//			};
//
//			void sur(Input IN, inout SurfaceOutput o)
//			{
//				_Color.a = _Alpha;
//				o.Albedo = _Color.rgb;
//				o.Alpha = _Color.a;
//			}*/
//			ENDCG
//			}
//		}
//			FallBack "Diffuse"
//}

Shader "Custom/Transparent"
{
	Properties
	{

	}

	SubShader
	{
		Pass
		{
		CGPROGRAM
		ENDCG
		}
	}
}