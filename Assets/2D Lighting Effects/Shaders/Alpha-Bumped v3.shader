// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Alpha-Bumped v3" {

	Properties{
		_MainTex("Sprite Texture", 2D) = ""
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 1
	}

		Subshader{
		Tags{ "Queue" = "Transparent" }
		ZWrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		struct v2f {
		float4 position : SV_POSITION;
		float2 uv_mainTex : TEXCOORD;


	};

	uniform float4 _MainTex_ST;
	v2f vert(float4 position : POSITION, float2 uv : TEXCOORD0) {
		v2f o;
		o.position = UnityObjectToClipPos(position);
		o.uv_mainTex = uv * _MainTex_ST.xy + _MainTex_ST.zw;
		return o;
	}

	uniform sampler2D _MainTex;
	fixed4 frag(float2 uv_mainTex : TEXCOORD) : COLOR{
		fixed4 mainTex = tex2D(_MainTex, uv_mainTex);
	fixed4 fragColor;
	fragColor.rgb = dot(mainTex.rgb, fixed3(.222, .707, .071));
	fragColor.a = mainTex.a;
	return fragColor;
	}
		ENDCG
	}
	}

}