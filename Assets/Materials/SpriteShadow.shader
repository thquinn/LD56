// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Default"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float4 objectVertex   : POSITION1;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				// scale the sprite by 2
				float4 wpos = IN.vertex;
				wpos.x *= 2;
				wpos.y *= 2;
				OUT.vertex = UnityObjectToClipPos(wpos);
				OUT.objectVertex = IN.vertex;
				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				float2 uv = float2(IN.objectVertex.x, IN.objectVertex.y * 50);
				fixed4 c;
				if (uv.x >= 0 && uv.x <= .1 && uv.y >= 0 && uv.y <= 1) {
					c = tex2D (_MainTex, uv) * IN.color;
				} else {
					//c = tex2D (_MainTex, unscaledUV) * IN.color;
					c = fixed4(0, 0, 0, 0);
				}
				return c;
			}
		ENDCG
		}
	}
}
