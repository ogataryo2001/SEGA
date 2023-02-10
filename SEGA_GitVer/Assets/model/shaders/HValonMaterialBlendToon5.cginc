// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: excluded shader from Xbox360; has structs without semantics (struct v2f members grad)
#pragma exclude_renderers xbox360
#include "UnityCG.cginc"
#include "Lighting.cginc"

fixed4 _Color;
#ifdef	__USE_RIMLINE__
fixed _RimWidth;
#endif
#ifdef	__USE_HEIGHT_GRAD_WITH_MATERIAL__
float4 _HeightColor;
//float _HeightYScale ;
//float _HeightYOffset ;
#endif
//float _ToonMaterial;
sampler2D _MainTex;
#ifdef	__SEPARATE_ALPHA__
sampler2D _AlphaTex;
#endif
sampler2D _ToonTex;
fixed _AlphaOffset;
half _AlphaTeamScaling;
#ifdef	__USE_TEAM_COLOR__
fixed4 _TeamColor;
	#ifdef	__USE_TEAM_COLOR_BLINK__
	fixed _BlinkTeamScaling;
	#endif
#endif
#ifdef	__USE_UV_SCROLL__
float4 _ScrollingSpeed;
#endif
#ifdef	__USE_ALPHA_TEXTURE__
half _AlphaScaling;
#endif
#ifdef	__USE_SPECULAR__
half _SpecularPower;
half _SpecularScale;
#endif
#ifdef	__USE_HIDDEN_HEIGHT__
half _HiddenHeight;
#endif

struct v2f
{
	float4	pos		: SV_POSITION ;
	fixed2	main_uv	: TEXCOORD0 ;
	fixed2	toon_uv	: TEXCOORD2 ;
//	fixed3	col		: COLOR ;
#ifdef	__USE_HEIGHT_GRAD__
	fixed3	grad : TEXCOORD3 ;
#endif
//#ifdef	__USE_TEAM_COLOR__
	fixed	alpha_scaling : TEXCOORD4 ;
	#ifdef	__USE_TEAM_COLOR_BLINK__
	fixed3	team_color : COLOR ;
	#endif
//#endif
#ifdef	__USE_RIMLINE__
	half3	view_direction : TEXCOORD6 ;
	half3	view_normal : TEXCOORD7 ;
#endif
#ifdef	__USE_SPECULAR__
	fixed3	spec : TEXCOORD5 ;
#endif
#ifdef	__USE_HIDDEN_HEIGHT__
	half height : TEXCOORD6;
#endif
} ;

v2f vert( appdata_base v )
{
	v2f o;

	o.pos = UnityObjectToClipPos( v.vertex );

#ifdef	__USE_UV_SCROLL__
	o.main_uv = v.texcoord + frac(_ScrollingSpeed * _Time.y);
#else
	o.main_uv.xy = v.texcoord.xy ;
#endif

//#ifdef	__USE_TEAM_COLOR__
	o.alpha_scaling = 1.0f / (1.0f - _AlphaOffset);
#ifdef	__USE_TEAM_COLOR_BLINK__
	half scl = ((sin(_Time.y * _BlinkTeamScaling) + 1.0f) * 0.5f + 0.25f);
	o.team_color = _TeamColor.rgb * scl;
#endif
//#endif

	// Toon UV
#define	TOON_VAL0	(0.0f)
#define	TOON_VAL1	(1.0f)
#define	TOON_LIGHT_X	(-0.7071067812f)
#define	TOON_LIGHT_Y	(0.7071067812f)
#define	HEIGHT_Y_SCALE	(3.0f)
#define	HEIGHT_Y_OFFSET	(-2.0f)
	fixed3	view_normal = normalize(mul((float3x3)UNITY_MATRIX_MV, v.normal).xyz) ;
//	fixed3	view_normal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal).xyz) ;
#ifdef	__IGNORE_LIGHT_VECTOR__
	half	toon_strength = dot( view_normal.xy, fixed2(TOON_LIGHT_X, TOON_LIGHT_Y) ) ;
#else
	#ifdef __DEBUG_LIGHT_VECTOR_ZERO__
		half	toon_strength = dot( view_normal.xyz, half3(0,0,0) ) ;
	#else
		half	toon_strength = dot( view_normal.xyz, unity_LightPosition[0].xyz ) ;
	#endif
#endif
#ifdef	__USE_SPECULAR__
#define VIEW_DIR	half3(0.0f, 0.0f, 1.0f)
	half3 half_vector = normalize(unity_LightPosition[0].xyz + VIEW_DIR);
	half nh = max(0, dot(view_normal, half_vector));
//	half nh = dot(view_normal, half_vector);	//NG for flickering
	#ifdef __DEBUG_LIGHT_COLOR_ZERO__
		o.spec = pow(nh, _SpecularPower) * _SpecularScale * 0;
	#else
		o.spec = pow(nh, _SpecularPower) * _SpecularScale * unity_LightColor[0].rgb;
	#endif
	o.spec *= o.spec;
#endif
	o.toon_uv.x = 0.0f ;
//	o.toon_uv.y = lerp( ((TOON_VAL0 + TOON_VAL1)/2.0f), TOON_VAL1, toon_strength ) ;
	o.toon_uv.y = toon_strength * 0.5f + 0.5f ;

#ifdef	__USE_RIMLINE__
	// Rim Line
	o.view_normal = view_normal;
	o.view_direction = normalize(half3(0, 0, 40) - o.pos.xyz) ;
#endif

	// Heigh Color
#ifdef	__USE_HEIGHT_GRAD__
	fixed4	p = mul(unity_ObjectToWorld,v.vertex);
	fixed	height = p.y;
	#ifdef __USE_HIDDEN_HEIGHT__
		o.height = p.y;
	#endif
	#ifdef	__USE_HEIGHT_GRAD_WITH_MATERIAL__
		o.grad = (1.0f - saturate((height*HEIGHT_Y_SCALE)+HEIGHT_Y_OFFSET)) * _HeightColor.rgb;
	#else
		#ifdef __DEBUG_LIGHT_COLOR_ZERO__
			o.grad = (1.0f - saturate((height*HEIGHT_Y_SCALE)+HEIGHT_Y_OFFSET)) * 0;
		#else
			o.grad = (1.0f - saturate((height*HEIGHT_Y_SCALE)+HEIGHT_Y_OFFSET)) * glstate_lightmodel_ambient.rgb;
		#endif
	#endif
#else
	#ifdef __USE_HIDDEN_HEIGHT__
		half4 p = mul(unity_ObjectToWorld, v.vertex);
		o.height = p.y;
	#endif
#endif

	// Lighting
//	o.col.rgb = unity_LightColor[0].rgb ;

	return o;
}

fixed4 frag( v2f i ) : COLOR
{
#ifdef __USE_HIDDEN_HEIGHT__
	//if (i.height < _HiddenHeight) {
	//	discard;
	//}
	clip(i.height - _HiddenHeight);
#endif
	half	alpha_color;
	half4	main_color = tex2D(_MainTex, i.main_uv.xy) ;
#ifdef	__REPLACE_GREEN_WITH_ALPHA__
	fixed3	c = fixed3(main_color.r, main_color.a, main_color.b);
	alpha_color = main_color.g;
#else
	fixed3	c = main_color.rgb;
	#ifdef	__SEPARATE_ALPHA__
		alpha_color = tex2D(_AlphaTex, i.main_uv.xy).a ;
	#else
		alpha_color = main_color.a;
	#endif
#endif

	// Specular Toon Adding
#ifdef	__USE_SPECULAR__
//	alpha_color = saturate(alpha_color + i.spec);
//	alpha_color = saturate(alpha_color * i.spec);
#endif

#ifdef	__USE_TOON_MASK__
//	#ifdef	__USE_TEAM_COLOR__
		fixed4	toon_color = tex2D(_ToonTex, half2(1.0f-((alpha_color-_AlphaOffset)*i.alpha_scaling), i.toon_uv.y)) ;
//	#else
//		fixed4	toon_color = tex2D(_ToonTex, half2(1.0f - alpha_color, i.toon_uv.y)) ;
//	#endif
#else
	fixed4	toon_color = tex2D(_ToonTex, i.toon_uv.xy) ;
#endif
	// Luminance Check
//	toon_color.a *= dot(c.rgb, fixed3(0.298912*0.6, 0.586611*0.6, 0.114478*0.6)) + 0.4 ;

	// Output Color
	fixed4	out_color ;

	// Height Gradation Blending
#ifdef	__USE_HEIGHT_GRAD__
	toon_color.rgb += i.grad;
#endif

	// Toon Blending
#ifdef __DEBUG_LIGHT_COLOR_ZERO__
	#ifdef __USE_LIGHTMAP_TOON__
//		out_color.rgb = c.rgb * 0 + (toon_color.rgb - 0.5);
//		out_color.rgb = c.rgb * 0 * (toon_color.rgb * 2.0);
		out_color.rgb = c.rgb * 0 * (toon_color.rgb);
	#else
		out_color.rgb = lerp(c.rgb * 0, toon_color.rgb, toon_color.a) ;
	#endif
#else
	#ifdef __USE_LIGHTMAP_TOON__
//		out_color.rgb = c.rgb * unity_LightColor[0].rgb + (toon_color.rgb - 0.5);
//		out_color.rgb = c.rgb * unity_LightColor[0].rgb * (toon_color.rgb * 2.0);
		out_color.rgb = c.rgb * unity_LightColor[0].rgb * (toon_color.rgb);
	#else
		out_color.rgb = lerp(c.rgb * unity_LightColor[0].rgb, toon_color.rgb, toon_color.a) ;
	#endif
#endif
#ifdef	__USE_SELECTED_DARK__
	out_color.rgb *= 0.75f;
#endif
	// Team Color
#ifdef	__USE_TEAM_COLOR__
	fixed alpha = max(1.0 - alpha_color * _AlphaTeamScaling, 0);
	#ifdef	__USE_TEAM_COLOR_BLINK__
//	out_color.rgb = lerp(i.team_color, out_color.rgb, alpha);
	out_color.rgb += i.team_color.rgb * alpha;
	#else
//	out_color.rgb = lerp(_TeamColor, out_color.rgb, alpha);
	out_color.rgb += _TeamColor.rgb * alpha;
	#endif
#endif

	// Overwrite Color
#ifdef	__USE_OVERWRITE_COLOR__
	out_color.rgb += _Color.rgb * _Color.a;
#endif

	// Rim Line
#ifdef	__USE_RIMLINE__
	fixed v = dot(i.view_normal.xyz, i.view_direction.xyz) * 1000.0f - _RimWidth;
	fixed strength = min(v, 1.0);
	out_color.rgb *= strength;
#endif

	// Specular Adding
#ifdef	__USE_SPECULAR__
	out_color.rgb += i.spec * alpha_color;
#endif

	// Alpha Blending
#ifdef	__USE_ALPHA_BLEND__
	out_color.a = _Color.a ;
#else
	#ifdef	__USE_ALPHA_TEXTURE__
//	out_color.a = max(alpha_color + 0.1, 1.0) * _Color.a ;

//Reverse Alpha
//	if (alpha_color >= 0.9) {
//		out_color.a = (1-alpha_color) * _AlphaScaling ;
//	}
//	else {
//		out_color.a = 1.0f;
//	}

	out_color.a = alpha_color * _AlphaScaling ;
	#else
	out_color.a = 1.0f ;
	#endif
#endif
	return out_color ;
}
