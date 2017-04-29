using System;
using UnityEngine;

public class SettingManager
{
	private const int DEFAULT_PEOPLE_NUM = 45;

	public const string IsGuideOnName = "IsGuideOnName";

	private const string IsPostProcessOnName = "IsPostProcessOnName";

	private const string PP_MotionBlurOnName = "PP_MotionBlurOnName";

	private const string PP_BloomOnName = "PP_BloomOnName";

	private const string PeopleNumName = "PeopleNumName";

	public const string QualityOfLODName = "QualityOfLODName";

	private const string IsResolutionOnName = "IsResolutionOnName";

	private const string ResolutionWidthName = "ResolutionWidthName";

	private const string AAName = "AAName";

	private const string IsMusicOnName = "IsMusicOnName";

	private const string IsSoundOnName = "IsSoundOnName";

	private const string IsHeadInfoOnName = "IsHeadInfoOnName";

	private const string IsManNumOnName = "IsManNumOnName";

	public static readonly SettingManager Instance = new SettingManager();

	public void ReadSetting()
	{
		SystemConfig.IsGuideSystemOn = PlayerPrefsExt.GetBool("IsGuideOnName", SysConfigID.GetBool(11));
		SystemConfig.IsPostProcessOn = PlayerPrefsExt.GetBool("IsPostProcessOnName", SystemConfig.IsPostProcessOn);
		SystemConfig.PP_MotionBlurOn = PlayerPrefsExt.GetBool("PP_MotionBlurOnName", SystemConfig.PP_MotionBlurOn);
		SystemConfig.PP_BloomOn = PlayerPrefsExt.GetBool("PP_BloomOnName", SystemConfig.PP_BloomOn);
		SettingManager.ReadResolution();
		GameLevelManager.GameLevelVariable.PeopleNum = PlayerPrefs.GetInt("PeopleNumName", 45);
		SettingManager.ReadLOD();
		SystemConfig.IsMusicOn = PlayerPrefsExt.GetBool("IsMusicOnName", SystemConfig.IsMusicOn);
		SystemConfig.IsSoundOn = PlayerPrefsExt.GetBool("IsMusicOnName", SystemConfig.IsMusicOn);
		SystemConfig.IsHeadInfoOn = PlayerPrefsExt.GetBool("IsHeadInfoOnName", SystemConfig.IsHeadInfoOn);
		SystemConfig.IsManNumOn = PlayerPrefsExt.GetBool("IsManNumOnName", SystemConfig.IsManNumOn);
	}

	public void SaveSetting()
	{
		PlayerPrefsExt.SetBool("IsGuideOnName", SystemConfig.IsGuideSystemOn);
		PlayerPrefsExt.SetBool("IsPostProcessOnName", SystemConfig.IsPostProcessOn);
		PlayerPrefsExt.SetBool("PP_MotionBlurOnName", SystemConfig.PP_MotionBlurOn);
		PlayerPrefsExt.SetBool("PP_BloomOnName", SystemConfig.PP_BloomOn);
		PlayerPrefsExt.SetBool("IsResolutionOnName", SystemConfig.IsSetHardwareResolutionOn);
		PlayerPrefs.SetInt("ResolutionWidthName", SystemConfig.RESOLUTION_WIDTH);
		PlayerPrefs.SetInt("PeopleNumName", GameLevelManager.GameLevelVariable.PeopleNum);
		PlayerPrefs.SetInt("QualityOfLODName", GameLevelManager.GameLevelVariable.LODLEVEL);
		PlayerPrefs.SetInt("AAName", QualitySettings.get_antiAliasing());
		PlayerPrefsExt.SetBool("IsMusicOnName", SystemConfig.IsMusicOn);
		PlayerPrefsExt.SetBool("IsHeadInfoOnName", SystemConfig.IsHeadInfoOn);
		PlayerPrefsExt.SetBool("IsManNumOnName", SystemConfig.IsManNumOn);
	}

	private static void ReadResolution()
	{
		SystemConfig.IsSetHardwareResolutionOn = PlayerPrefsExt.GetBool("IsResolutionOnName", SystemConfig.IsSetHardwareResolutionOn);
		int @int = PlayerPrefs.GetInt("ResolutionWidthName", SystemConfig.RESOLUTION_WIDTH);
		int num = @int;
		if (num != 960 && num != 1280 && num != 1920)
		{
			UIUtils.SetHardwareResolution();
		}
		else
		{
			SystemConfig.RESOLUTION_WIDTH = @int;
			UIUtils.SetHardwareResolution();
		}
	}

	private static void ReadLOD()
	{
		int @int = PlayerPrefs.GetInt("QualityOfLODName", 0);
		int num = @int;
		if (num != 200 && num != 250 && num != 300)
		{
			GameLevelManager.SetGameQuality(GameLevelManager.GameLevelVariable.LODLEVEL, true);
		}
		else
		{
			GameLevelManager.SetGameQuality(@int, true);
		}
	}
}
