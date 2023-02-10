/* Valon ブレンド式トゥーン描画、縦グラデーション付き　（不透明）
   ベーステクスチャのα値によって使用するToonラインが変わります。
   縦グラデーションは、アンビエントカラーを参照します。
   
   2012.4.19 Higuchi (base optimized by Takaki)
*/

Shader "Potato/Chara/BlendToon-Mask-HeightGradation-Lightmap-Outline"
{
	Properties
	{
		_Color("Add Color (RGB), Add Strength (A)", Color) = (1,1,1,0)
//		_HeightColor("Height Gradation (RGB)", Color) = (0,0,0,1)
//		_HeightYScale("Gradation Scale", Float) = 3.0
//		_HeightYOffset("Gradation Offset", Float) = -2.0
//		_HiddenColor ("Hidden Color (RGB)", Color) = (0.48, 0.37, 0.49, 1.0)	//65,60,65,255
//		_TeamColor ("Team Color (RGB)", Color) = (0.29, 0.58, 1.0, 1.0)	//青:75,150,255 赤:191,1,0
//		_BlinkTeamScaling ("Blink Scaling for Team Color (default=100)", Float) = 100
//		_AlphaTeamScaling ("Alpha Scaling for Team Color (default=20)", Float) = 20
		_AlphaOffset ("Alpha Offset for Shading (0~1: default=0.375)", Float) = 0.375
		_MainTex("Diffuse (RGB), Shading Mask (A)", 2D) = "white" {}
		_ToonTex("Toon (RGBA)", 2D) = "white" {}
		_OutlineColor("Outline Color (RGB)", Color) = (1,1,1,1)
		_OutlineWidth("Outline Width", Float) = 0.0015
	}

	SubShader
	{	
		Tags
		{
			"Queue"		 = "Geometry+500"
			"RenderType" = "Opaque"
			"LightMode"  = "Vertex"
		}
		
		// Surface Shader Pass
		Pass
		{
			Lighting On
			Cull Back
			ZWrite On
			Blend Off
			//Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vert 
			#pragma fragment frag

			//#define	__IGNORE_LIGHT_VECTOR__
			#define	__USE_HEIGHT_GRAD__
			#define	__USE_TOON_MASK__
			//#define	__REPLACE_GREEN_WITH_ALPHA__
			//#define	__USE_TEAM_COLOR__
			#define	__USE_OVERWRITE_COLOR__
			//#define	__USE_ALPHA_BLEND__
			#define __USE_LIGHTMAP_TOON__
			
			#include "HValonMaterialBlendToon5.cginc"

			ENDCG
		}
		
		// Outline Pass
		Pass
		{
			Tags
			{
				"Queue"		 = "Geometry+500"
				"RenderType" = "Opaque"
			}

			Cull Front
			//ZWrite Off
			Lighting Off
			//Blend SrcAlpha OneMinusSrcAlpha
			Blend Off
			ZTest Less
			
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			//#define	__USE_ALPHA_BLEND__
			//#define	__USE_SCALING__
			//#define	__USE_BLINK__
			#include "HValonMaterialVertFrag3.cginc"
			//#undef	__USE_ALPHA_BLEND__

			ENDCG
		}
	}
	
	// Other Environment
	//Fallback "Diffuse"
}
