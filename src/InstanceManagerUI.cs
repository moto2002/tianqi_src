using GameData;
using Package;
using System;
using System.Collections.Generic;

public class InstanceManagerUI
{
	private static int lastOpenInstanceID;

	private static int instanceID;

	public static int LastOpenInstanceID
	{
		get
		{
			return InstanceManagerUI.lastOpenInstanceID;
		}
		set
		{
			InstanceManagerUI.lastOpenInstanceID = value;
		}
	}

	public static int InstanceID
	{
		get
		{
			return InstanceManagerUI.instanceID;
		}
		set
		{
			InstanceManagerUI.instanceID = value;
		}
	}

	public static bool IsPetLimit()
	{
		if (ChangePetChooseUI.limit_pet)
		{
			InstanceManagerUI.ShowLimitMessage();
			return true;
		}
		return false;
	}

	public static void ShowLimitMessage()
	{
		string chineseContent = GameDataUtils.GetChineseContent(510119, false);
		string petTypeName = PetManagerBase.GetPetTypeName(InstanceManagerUI.GetInstanceLimitPetType());
		DialogBoxUIViewModel.Instance.ShowAsConfirm("提示", string.Format(chineseContent, petTypeName), null, "确定", "button_orange_1", null);
	}

	public static int GetInstanceLimitPetType()
	{
		if (InstanceManagerUI.InstanceID <= 0)
		{
			return 0;
		}
		FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(InstanceManagerUI.InstanceID);
		if (fuBenJiChuPeiZhi == null)
		{
			return 0;
		}
		return fuBenJiChuPeiZhi.petType;
	}

	public static void OpenEliteDungeonUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(18, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("EliteDungeonUI", null, false, UIType.FullScreen);
	}

	public static void OpenInstanceUI(int instanceID, bool hideTheVisible = false, UIType type = UIType.FullScreen)
	{
		InstanceManagerUI.InstanceID = instanceID;
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(instanceID);
		int chapterID = zhuXianPeiZhi.chapterId;
		ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.Get(chapterID);
		List<ChapterInfo> dataByInstanceType = DungeonManager.Instance.GetDataByInstanceType(zhuXianZhangJiePeiZhi.chapterType);
		if (dataByInstanceType.get_Count() == 0 || dataByInstanceType.Find((ChapterInfo a) => a.chapterId == chapterID) == null)
		{
			DungeonManager.Instance.SendGetDungeonDataReq(chapterID, (DungeonType.ENUM)zhuXianZhangJiePeiZhi.chapterType, delegate
			{
				InstanceManagerUI.OpenInstanceUI(instanceID, hideTheVisible, type);
			});
			return;
		}
		FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(instanceID);
		if (fuBenJiChuPeiZhi.type == 103)
		{
			InstanceSelectUI instanceSelectUI = UIManagerControl.Instance.OpenUI("InstanceSelectUI", null, true, UIType.FullScreen) as InstanceSelectUI;
			instanceSelectUI.RefreshUIByInstanceID(instanceID);
		}
		else
		{
			InstanceSelectUI instanceSelectUI2 = UIManagerControl.Instance.OpenUI("InstanceSelectUI", null, true, UIType.FullScreen) as InstanceSelectUI;
			instanceSelectUI2.RefreshUIByInstanceID(instanceID);
			InstanceDetailUI instanceDetailUI = UIManagerControl.Instance.OpenUI("InstanceDetailUI", null, false, UIType.Pop) as InstanceDetailUI;
			instanceDetailUI.RefreshUI(instanceID);
		}
	}

	public static void OpenTheLastInstanceSelectUI(InstanceType instanceType)
	{
		ChapterResume chapterResume = DungeonManager.Instance.listChapterResume.Find((ChapterResume a) => a.dungeonType == (DungeonType.ENUM)instanceType);
		List<ChapterInfo> dataByInstanceType = DungeonManager.Instance.GetDataByInstanceType((int)instanceType);
		if (dataByInstanceType.get_Count() == 0)
		{
			return;
		}
		InstanceManagerUI.LastOpenInstanceID = DungeonManager.Instance.GetTheLastInstaceID(instanceType);
		InstanceSelectUI instanceSelectUI = UIManagerControl.Instance.OpenUI("InstanceSelectUI", null, true, UIType.FullScreen) as InstanceSelectUI;
		instanceSelectUI.RefreshUIByInstanceID(InstanceManagerUI.LastOpenInstanceID);
	}

	public static void OpenInstanceSelectUI(int instanceID, bool hideTheVisible = true, UIType type = UIType.FullScreen)
	{
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(instanceID);
		int chapterId = zhuXianPeiZhi.chapterId;
		ZhuXianZhangJiePeiZhi datazj = DataReader<ZhuXianZhangJiePeiZhi>.Get(chapterId);
		ChapterResume chapterResume = DungeonManager.Instance.listChapterResume.Find((ChapterResume a) => a.dungeonType == (DungeonType.ENUM)datazj.chapterType);
		List<ChapterInfo> dataByInstanceType = DungeonManager.Instance.GetDataByInstanceType(datazj.chapterType);
		if (dataByInstanceType.get_Count() == 0)
		{
			DungeonManager.Instance.SendGetDungeonDataReq(chapterResume.chapterId, chapterResume.dungeonType, delegate
			{
				InstanceManagerUI.OpenInstanceSelectUI(instanceID, hideTheVisible, type);
			});
			return;
		}
		InstanceManagerUI.LastOpenInstanceID = instanceID;
		InstanceSelectUI instanceSelectUI = UIManagerControl.Instance.OpenUI("InstanceSelectUI", null, true, UIType.FullScreen) as InstanceSelectUI;
		instanceSelectUI.RefreshUIByInstanceID(InstanceManagerUI.LastOpenInstanceID);
	}

	public static void OpenPVPUI(Action<UIBase> finish_callback)
	{
		InstanceManagerUI.InstanceID = PVPInstance.PVPInstanceDataID;
		UIManagerControl.Instance.OpenUI_Async("PVPUI", finish_callback, null);
	}

	public static void OpenGangFightUI()
	{
		InstanceManagerUI.InstanceID = GangFightInstance.GangFightInstanceDataID;
		UIManagerControl.Instance.OpenUI("GangFightUI", null, false, UIType.FullScreen);
	}

	public static void OpenBountyUI()
	{
		InstanceManagerUI.InstanceID = 0;
		UIManagerControl.Instance.OpenUI("BountyUI", null, false, UIType.FullScreen);
	}

	public static void OpenMultiPlayerUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(19, 0, true))
		{
			return;
		}
		if (!ActivityCenterManager.Instance.CheckActivityIsOpen(10002))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513526, false));
			return;
		}
		InstanceManagerUI.InstanceID = 0;
		MultiPlayerUI multiPlayerUI = UIManagerControl.Instance.OpenUI("MultiPlayerUI", null, true, UIType.FullScreen) as MultiPlayerUI;
		multiPlayerUI.SettingUI(10002, string.Empty);
	}

	public static void OpenSpecialInstanceUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(17, 0, true))
		{
			return;
		}
		InstanceManagerUI.InstanceID = 0;
		UIManagerControl.Instance.OpenUI("SpecialInstanceUI", null, false, UIType.FullScreen);
	}

	public static void OpenSpecialInstanceGuardUI()
	{
		InstanceManagerUI.OpenSpecialInstanceAppointedUI(SpecialFightMode.Hold);
	}

	public static void OpenSpecialInstanceEscortUI()
	{
		InstanceManagerUI.OpenSpecialInstanceAppointedUI(SpecialFightMode.Protect);
	}

	public static void OpenSpecialInstanceAttackUI()
	{
		InstanceManagerUI.OpenSpecialInstanceAppointedUI(SpecialFightMode.Save);
	}

	public static void OpenSpecialInstanceExperienceUI()
	{
		InstanceManagerUI.OpenSpecialInstanceAppointedUI(SpecialFightMode.Expericence);
	}

	protected static void OpenSpecialInstanceAppointedUI(SpecialFightMode mode)
	{
		if (!SystemOpenManager.IsSystemClickOpen(17, 0, true))
		{
			return;
		}
		if (!SystemOpenManager.IsSystemClickOpen(SpecialFightManager.GetSystemIDByMode(mode), 0, true))
		{
			return;
		}
		InstanceManagerUI.InstanceID = 0;
		SpecialInstanceUI specialInstanceUI = UIManagerControl.Instance.OpenUI("SpecialInstanceUI", null, false, UIType.FullScreen) as SpecialInstanceUI;
		specialInstanceUI.FakeClick(mode);
	}

	public static void OpenSurvivalChallengeUI(Action<UIBase> finish_callback)
	{
		InstanceManagerUI.InstanceID = 0;
		UIManagerControl.Instance.OpenUI_Async("SurvivalChallengeUI", finish_callback, null);
	}

	public static ElementInstanceMonsterMeet OpenElementInstanceMonsterMeet()
	{
		InstanceManagerUI.InstanceID = 0;
		UIBase uIBase = UIManagerControl.Instance.OpenUI("ElementInstanceMonsterMeet", null, false, UIType.NonPush);
		return uIBase as ElementInstanceMonsterMeet;
	}
}
