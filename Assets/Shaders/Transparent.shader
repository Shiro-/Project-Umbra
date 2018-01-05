Shader "Custom/Transparent"
{
	Properties
	{
		_Color("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Main Texture", 2D) = "white" {}
		_Alpha("Alpha Value", Range(0.0, 1.0)) = 1.0
	}

		SubShader
		{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}

			//Render passes
			Pass
			{
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				uniform sampler2D _MainTex;
				
				uniform half4 _Color;
				uniform float _Alpha;

				struct vertexInput
				{
					float4 vertex : POSITION;
					float4 texcoord : TEXCOORD0;
				};

				struct vertexOutput
				{
					float4 pos : SV_POSITION;
					float4 texcoord : TEXCOORD0;
				};

				vertexOutput vert(vertexInput v)
				{
					vertexOutput o;
					o.pos = UnityObjectToClipPos(v.vertex);
					return o;
				}

				half4 frag(vertexOutput i) : COLOR
				{
					float4 col = _Color;
					col.a = _Alpha;
					return col;
				}
			ENDCG
		}
	}
	Fallback "Diffuse"
}