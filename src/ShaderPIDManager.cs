using System;
using UnityEngine;

public class ShaderPIDManager
{
	public static readonly string Name_Color = "_Color";

	public static readonly int _Color = Shader.PropertyToID(ShaderPIDManager.Name_Color);

	public static readonly int _ColorAnimate = Shader.PropertyToID("_ColorAnimate");

	public static readonly int _Amount = Shader.PropertyToID("_Amount");

	public static readonly int _StartAmount = Shader.PropertyToID("_StartAmount");

	public static readonly int _Illuminate = Shader.PropertyToID("_Illuminate");

	public static readonly int _Tile = Shader.PropertyToID("_Tile");

	public static readonly int _OffsetYHeightMap = Shader.PropertyToID("_OffsetYHeightMap");

	public static readonly int _MainTex = Shader.PropertyToID("_MainTex");

	public static readonly int _NoiseTex = Shader.PropertyToID("_NoiseTex");

	public static readonly int _DissolveColor = Shader.PropertyToID("_DissolveColor");

	public static readonly int _TileFactor = Shader.PropertyToID("_TileFactor");

	public static readonly int _OutlineColor = Shader.PropertyToID("_OutlineColor");

	public static readonly int _Outline = Shader.PropertyToID("_Outline");

	public static readonly int _ColorMix = Shader.PropertyToID("_ColorMix");

	public static readonly int _Parameter = Shader.PropertyToID("_Parameter");

	public static readonly int _TintColor = Shader.PropertyToID("_TintColor");

	public static readonly int _RimColor = Shader.PropertyToID("_RimColor");

	public static readonly int _RimPower = Shader.PropertyToID("_RimPower");

	public static readonly int _RimBrightnessRatio = Shader.PropertyToID("_RimBrightnessRatio");

	public static readonly int _DissColor = Shader.PropertyToID("_DissColor");

	public static readonly int _Transparency = Shader.PropertyToID("_Transparency");

	public static readonly int _Chanel = Shader.PropertyToID("_Chanel");

	public static readonly int _SizeY = Shader.PropertyToID("_SizeY");

	public static readonly int _SizeX = Shader.PropertyToID("_SizeX");

	public static readonly int _IndexY = Shader.PropertyToID("_IndexY");

	public static readonly int _IndexX = Shader.PropertyToID("_IndexX");

	public static readonly int _HueShift = Shader.PropertyToID("_HueShift");

	public static readonly int _Sat = Shader.PropertyToID("_Sat");

	public static readonly int _Val = Shader.PropertyToID("_Val");

	public static readonly int _BeginTime = Shader.PropertyToID("_BeginTime");

	public static readonly int _Interval = Shader.PropertyToID("_Interval");

	public static readonly int _TimeOnce = Shader.PropertyToID("_TimeOnce");

	public static readonly int _DisintegrateAmount = Shader.PropertyToID("_DisintegrateAmount");

	public static readonly int _HideY = Shader.PropertyToID("_HideY");

	public static readonly int _DiffuseAmount = Shader.PropertyToID("_DiffuseAmount");

	public static readonly int _ClipPower = Shader.PropertyToID("_ClipPower");

	public static readonly int _LitDirX = Shader.PropertyToID("_LitDirX");

	public static readonly int _LitDirY = Shader.PropertyToID("_LitDirY");

	public static readonly int _LitDirZ = Shader.PropertyToID("_LitDirZ");

	public static readonly int _ShadowY = Shader.PropertyToID("_ShadowY");

	public static readonly int _Speed = Shader.PropertyToID("_Speed");

	public static readonly int _Range = Shader.PropertyToID("_Range");

	public static readonly int _OffsetPixel = Shader.PropertyToID("_OffsetPixel");

	public static readonly int _ClipTex = Shader.PropertyToID("_ClipTex");

	public static readonly int _OffsetTex = Shader.PropertyToID("_OffsetTex");

	public static readonly int _ScreenResolution = Shader.PropertyToID("_ScreenResolution");

	public static readonly int _CullingTex = Shader.PropertyToID("_CullingTex");

	public static readonly int _TimeX = Shader.PropertyToID("_TimeX");

	public static readonly int _Level = Shader.PropertyToID("_Level");

	public static readonly int _Distance = Shader.PropertyToID("_Distance");

	public static readonly int _CenterX = Shader.PropertyToID("_CenterX");

	public static readonly int _CenterY = Shader.PropertyToID("_CenterY");

	public static readonly int _Strength = Shader.PropertyToID("_Strength");

	public static readonly int _MtxColor = Shader.PropertyToID("_MtxColor");

	public static readonly int _LumThreshold = Shader.PropertyToID("_LumThreshold");

	public static readonly int _Radius = Shader.PropertyToID("_Radius");

	public static readonly int _Darkness = Shader.PropertyToID("_Darkness");

	public static readonly int _SeeThroughness = Shader.PropertyToID("_SeeThroughness");

	public static readonly int _EdgeSharpness = Shader.PropertyToID("_EdgeSharpness");

	public static readonly int _BlendAmount = Shader.PropertyToID("_BlendAmount");

	public static readonly int _Distortion = Shader.PropertyToID("_Distortion");

	public static readonly int _RotationMatrix = Shader.PropertyToID("_RotationMatrix");

	public static readonly int _CenterRadius = Shader.PropertyToID("_CenterRadius");

	public static readonly int _Angle = Shader.PropertyToID("_Angle");

	public static readonly int _BrightnessRatio = Shader.PropertyToID("_BrightnessRatio");

	public static readonly int _FrustumCornersWS = Shader.PropertyToID("_FrustumCornersWS");

	public static readonly int _CameraWS = Shader.PropertyToID("_CameraWS");

	public static readonly int _ObjectFocusParameter = Shader.PropertyToID("_ObjectFocusParameter");

	public static readonly int _ForegroundBlurExtrude = Shader.PropertyToID("_ForegroundBlurExtrude");

	public static readonly int _InvRenderTargetSize = Shader.PropertyToID("_InvRenderTargetSize");

	public static readonly int _Offsets = Shader.PropertyToID("_Offsets");

	public static readonly int _FgOverlap = Shader.PropertyToID("_FgOverlap");

	public static readonly int offsets = Shader.PropertyToID("offsets");

	public static readonly int _StretchTex = Shader.PropertyToID("_StretchTex");

	public static readonly int _ScreenSize = Shader.PropertyToID("_ScreenSize");

	public static readonly int _OutLineColor = Shader.PropertyToID("_OutLineColor");

	public static readonly int _OcclusionTex = Shader.PropertyToID("_OcclusionTex");

	public static readonly int _LowRez = Shader.PropertyToID("_LowRez");

	public static readonly int _CurveParams = Shader.PropertyToID("_CurveParams");
}
