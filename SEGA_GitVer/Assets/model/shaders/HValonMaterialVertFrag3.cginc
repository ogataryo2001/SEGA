// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

float4 _Color;
float4 _OutlineColor;
float _OutlineWidth;
#ifdef	__USE_ALPHA_BLEND__
sampler2D _MainTex;
#endif
#ifdef	__USE_HIDDEN_HEIGHT__
half _HiddenHeight;
#endif

struct v2f
{
	float4 pos : SV_POSITION;
#ifdef	__USE_BLINK__
	half col : TEXCOORD0;
#endif
#ifdef	__USE_ALPHA_BLEND__
	half2 uv  : TEXCOORD1;
#endif
#ifdef	__USE_HIDDEN_HEIGHT__
	half height : TEXCOORD2;
#endif
};

v2f vert( appdata_base v )
{
	v2f o;

//	Camera Axis Width
//	float pos_w = dot( UNITY_MATRIX_MVP._m30_m31_m32_m33, v.vertex.xyzw );
//	float4 edge_pos = v.vertex + ((pos_w * _OutlineWidth) * float4( v.normal, 0.0 ));

	//World Axis Width
#ifdef	__USE_SCALING__
	half wid = (2.0 + sin(_Time.y * 10.0)) * _OutlineWidth * 10.0;
#else
	half wid = _OutlineWidth * 20.0;
#endif
	float4 edge_pos = v.vertex + (wid * float4( normalize(v.normal), 0.0 ));

#ifdef	__USE_BLINK__
	o.col = ( 1.0 + sin( _Time.y * 10.0 ) ) * 0.5 + 0.25;
#endif
	o.pos = UnityObjectToClipPos( edge_pos );
#ifdef __USE_HIDDEN_HEIGHT__
	half4 p = mul(unity_ObjectToWorld, v.vertex);
	o.height = p.y;
#endif
#ifdef	__USE_ALPHA_BLEND__
	o.uv = TRANSFORM_UV(0);
#endif

	return o;
}
half4 frag( v2f i ) : COLOR
{
#ifdef __USE_HIDDEN_HEIGHT__
	if (i.height < _HiddenHeight) {
		discard;
	}
#endif
#ifdef	__USE_ALPHA_BLEND__
	return half4( _OutlineColor.rgb, min(tex2D(_MainTex, i.uv.xy).a * 100.0f, 1.0) );
#else
	#ifdef __HIDDEN_OUTLINE__
		return half4(0.0,0.9,0.0,1.0);
		//return half4(0.0,0.63,0.63,1.0);
	#else
		#ifdef	__USE_BLINK__
			half4 col = _OutlineColor;
			//col.gb *= i.col;
			col.rgb *= i.col;
			return col;
		#else
			return _OutlineColor;
		#endif
	#endif
#endif
}
