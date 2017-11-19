Shader "Custom/Water" {
  Properties {
    _Tint("Tint", Color) = (1, 1, 1, 1)
    _Displacement("Displacement", 2D) = "defaulttexture"
  }
	SubShader {
    Pass {
		  CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

      struct appdata {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

		  struct v2f {
			  float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
        float2 screenuv : TEXCOORD1;
		  };

      v2f vert(appdata v) {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;

        o.screenuv = ComputeScreenPos(o.vertex);
        return o;
      }

      uniform sampler2D _SSWaterReflectionsTex;
      uniform float _TopEdgePosition;
      
      fixed4 _Tint;
      sampler2D _Displacement;
      float4 _Displacement_ST;
      float PespectiveCorrection;
      
		  float4 frag(v2f i) : SV_TARGET {
        float2 perspectiveCorrection = float2(2.0 * (0.5 - i.uv.x) * i.uv.y, 0.0);
        float4 displacement = normalize(tex2D(_Displacement, TRANSFORM_TEX(i.uv + perspectiveCorrection, _Displacement)));
        float2 adjusted = i.screenuv.xy + ((displacement.rg - 0.5) * 0.015);

        float distanceToEdge = abs(_TopEdgePosition - adjusted.y);
        float sampleY = _TopEdgePosition + distanceToEdge;

        float4 output = tex2D(_SSWaterReflectionsTex, float2(adjusted.x, sampleY)) * _Tint;
        return output;
		  }
		  ENDCG
	  }
  }
}
