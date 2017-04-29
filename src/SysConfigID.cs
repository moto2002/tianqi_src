using GameData;
using System;
using UnityEngine;

public class SysConfigID
{
	public const int LogOn = 1;

	public const int IsTargetFrameRateOn = 2;

	public const int IsDebugInfoOn = 3;

	public const int IsDebugPing = 4;

	public const int antiAliasing = 5;

	public const int IsPostProcessOn = 6;

	public const int IsSelfSyncPointFlagOn = 7;

	public const int IsPetAndMonsterSyncPointFlagOn = 8;

	public const int IsShowMonsterDir = 9;

	public const int IsReleaseResourceOn = 10;

	public const int IsGuideSystemOn = 11;

	public const int IsBattleGMOn = 12;

	public const int IsResolutionOn = 13;

	public const int PP_MotionBlurOn = 14;

	public const int PP_BloomOn = 15;

	public const int LODLevel = 16;

	public const int IsVoiceTalkOn = 17;

	public static bool GetBool(int id)
	{
		return SysConfigID.GetInt(id) != 0;
	}

	public static int GetInt(int id)
	{
		SysConfig sysConfig = DataReader<SysConfig>.Get(id);
		if (sysConfig == null)
		{
			return 0;
		}
		if (Application.get_platform() == 11)
		{
			return sysConfig.android_release;
		}
		if (Application.get_platform() == 8)
		{
			return sysConfig.iphone_release;
		}
		return sysConfig.editor_release;
	}
}
