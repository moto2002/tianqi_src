using System;
using System.IO;
using UnityEngine;

public class SystemConfig
{
	public const int FrameCount2AVCUpdate = 10;

	public const int MillSceond2BubbleDialogueUpdateZ = 10000;

	public const int MillSceond2HeadInfoUpdateZ = 10000;

	public const string Bugly_IOS_AppId = "900057157";

	public const string Bugly_Android_AppId = "900057844";

	public static bool IsTargetFrameRateOn;

	public static bool IsDebugInfoOn;

	public static bool IsDebugPing;

	public static bool IsPostProcessOn = true;

	public static bool PP_MotionBlurOn;

	public static bool PP_BloomOn;

	public static bool IsSelfSyncPointFlagOn;

	public static bool IsPetAndMonsterSyncPointFlagOn;

	public static bool IsShowMonsterDir;

	public static bool IsReleaseResourceOn;

	public static bool IsGuideSystemOn = true;

	public static bool IsBattleGMOn;

	public static bool IsVoiceTalkOn = true;

	public static bool IsRefenenceControlOn = true;

	public static bool IsFXOn = true;

	public static bool IsUIFXOn = true;

	public static bool IsShaderEffectOn = true;

	public static bool IsOpenEffectDrawLine = true;

	public static bool IsShowBattleState;

	public static bool IsClearCD;

	public static bool IsUseResourceLoad = true;

	public static bool IsEffectOn = true;

	public static bool IsFindOcclusionByCameraOn;

	public static bool IsOutlineStatusOn;

	public static bool IsLogToFile;

	public static bool IsAudioOn = true;

	public static bool IsMusicOn = true;

	public static bool IsSoundOn = true;

	public static bool IsBillboardOn = true;

	public static bool IsHeadInfoOn = true;

	public static bool IsReadUIImageOn = true;

	public static bool IsPreloadAllUiAtlas;

	public static bool IsForcePreloadCompleteFx;

	public static bool IsDisableServerProBattle;

	public static bool IsManNumOn = true;

	private static bool _IsSetHardwareResolutionOn;

	private static int _RESOLUTION_WIDTH = 1280;

	public static bool IsBuglySDKLogOn;

	public static bool IsOpenPay = true;

	public static bool IsAudit;

	public static bool IsSetHardwareResolutionOn
	{
		get
		{
			return SystemConfig._IsSetHardwareResolutionOn;
		}
		set
		{
			SystemConfig._IsSetHardwareResolutionOn = value;
		}
	}

	public static int RESOLUTION_WIDTH
	{
		get
		{
			return Mathf.Max(960, SystemConfig._RESOLUTION_WIDTH);
		}
		set
		{
			SystemConfig._RESOLUTION_WIDTH = value;
		}
	}

	public static void Init()
	{
		SystemConfig.LogSetting(SysConfigID.GetBool(1));
		SystemConfig.IsTargetFrameRateOn = SysConfigID.GetBool(2);
		SystemConfig.IsDebugInfoOn = SysConfigID.GetBool(3);
		SystemConfig.IsDebugPing = SysConfigID.GetBool(4);
		QualitySettings.set_antiAliasing(SysConfigID.GetInt(5));
		SystemConfig.IsPostProcessOn = SysConfigID.GetBool(6);
		SystemConfig.IsSelfSyncPointFlagOn = SysConfigID.GetBool(7);
		SystemConfig.IsPetAndMonsterSyncPointFlagOn = SysConfigID.GetBool(8);
		SystemConfig.IsShowMonsterDir = SysConfigID.GetBool(9);
		SystemConfig.IsReleaseResourceOn = SystemConfig.CheckIsReleaseResourceOn(SysConfigID.GetInt(10));
		SystemConfig.IsBattleGMOn = SysConfigID.GetBool(12);
		SystemConfig.IsSetHardwareResolutionOn = SysConfigID.GetBool(13);
		SystemConfig.PP_MotionBlurOn = SysConfigID.GetBool(14);
		SystemConfig.PP_BloomOn = SysConfigID.GetBool(15);
		SystemConfig.IsVoiceTalkOn = SysConfigID.GetBool(17);
		SettingManager.Instance.ReadSetting();
	}

	private static bool CheckIsReleaseResourceOn(int config_value)
	{
		return true;
	}

	public static void LogSetting(bool isOn)
	{
		if (!Application.get_isEditor() && File.Exists(AppConst.LogOnFilePath))
		{
			isOn = true;
		}
		Debuger.EnableLog = isOn;
		Debug.get_logger().set_logEnabled(isOn);
	}
}
