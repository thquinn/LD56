Shader "thquinn/OverlapShader"
{
    Properties {
        _Alpha ("Alpha", Range(0, 1)) = .5
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
    }

        Category {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
        Blend SrcAlpha OneMinusSrcAlpha

        Stencil {
                Ref 10
                ReadMask 10
            Comp NotEqual
        Pass Replace
    }

        SubShader {
                Pass {

                CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0

#include "UnityCG.cginc"

    struct appdata_t {
        float4 vertex : POSITION;
        float2 texcoord  : TEXCOORD0;
    };

    struct v2f {
        float4 vertex : SV_POSITION;
        float2 texcoord : TEXCOORD0;
    };

    v2f vert (appdata_t v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.texcoord = v.texcoord;
        return o;
    }

    float _Alpha;
    sampler2D _MainTex;

    fixed4 frag (v2f i) : SV_Target
    {
        fixed4 tex = tex2D (_MainTex, i.texcoord);
        fixed4 col = fixed4(0, 0, 0, _Alpha * tex.a);
        return col;
    }
        ENDCG
    }
    }
    }
}