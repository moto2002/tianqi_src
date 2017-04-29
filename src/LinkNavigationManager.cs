using GameData;
using Package;
using System;
using UnityEngine;

public class LinkNavigationManager
{
	public static bool BroadcastLink(int link)
	{
		switch (link)
		{
		case 103:
			LinkNavigationManager.OpenTeamUI();
			return true;
		case 104:
			LinkNavigationManager.OpenPVPUI();
			return true;
		case 105:
			LinkNavigationManager.OpenGangFightUI();
			return true;
		case 106:
			LinkNavigationManager.OpenSurvivalChallengeUI();
			return true;
		case 114:
			LinkNavigationManager.OpenVIPUI2VipLimit();
			return true;
		case 115:
			LinkNavigationManager.OpenVIPUI2Privilege();
			return true;
		case 116:
			LinkNavigationManager.OpenMushroomHitUI();
			return true;
		case 117:
			LinkNavigationManager.OpenVIPUI2VipLimit();
			return true;
		}
		UIManagerControl.Instance.ShowToastText("无法跳转到目标界面, link = " + link);
		return false;
	}

	public static void SystemLink(int systemId, bool isTip = true, Action finish_callback = null)
	{
		if (DataReader<SystemOpen>.Get(systemId) == null)
		{
			Debug.Log("<color=red>Error:</color>找不到对应的系统, systemId = " + systemId);
			return;
		}
		if (!SystemOpenManager.IsSystemClickOpen(systemId, 0, isTip))
		{
			return;
		}
		switch (systemId)
		{
		case 3:
			LinkNavigationManager.OpenSkillUI(finish_callback);
			return;
		case 4:
			LinkNavigationManager.OpenPetUI(finish_callback);
			return;
		case 5:
			LinkNavigationManager.OpenActorUI(finish_callback);
			return;
		case 6:
		case 8:
		case 10:
		case 12:
		case 20:
		case 23:
		case 26:
		case 30:
		case 34:
		case 36:
		case 40:
		case 41:
		case 42:
		case 43:
		case 44:
		case 47:
		case 49:
		case 50:
		case 51:
		case 52:
		case 53:
		case 57:
		case 58:
		case 59:
		case 60:
		case 64:
		case 69:
		case 70:
		case 72:
		case 73:
		case 75:
		case 76:
		case 77:
			IL_17C:
			switch (systemId)
			{
			case 102:
				LinkNavigationManager.OpenXMarketUI2();
				return;
			case 104:
				LinkNavigationManager.OpenZeroTaskUI();
				return;
			case 105:
				LinkNavigationManager.OpenPVPShopUI();
				return;
			case 106:
				LinkNavigationManager.OpenVIPUI2VipInvest();
				return;
			case 107:
				LinkNavigationManager.OpenRankUpUI();
				return;
			case 110:
				LinkNavigationManager.OpenMultiPVPUI();
				return;
			case 111:
				LinkNavigationManager.OpenMultiPVEUI();
				return;
			case 112:
				LinkNavigationManager.OpenPetTaskUI();
				return;
			}
			Debug.Log("<color=red>Error:</color>找不到超链接: systemId = " + systemId);
			goto IL_41F;
		case 7:
			UIManagerControl.Instance.OpenUI("LuckDrawUI", null, false, UIType.FullScreen);
			goto IL_41F;
		case 9:
			LinkNavigationManager.OpenFirstPayUI(finish_callback);
			return;
		case 11:
			LinkNavigationManager.OpenDailyTaskUI();
			goto IL_41F;
		case 13:
			LinkNavigationManager.OpenPVPUI();
			goto IL_41F;
		case 14:
			InstanceManagerUI.OpenGangFightUI();
			goto IL_41F;
		case 15:
			LinkNavigationManager.OpenSurvivalChallengeUI();
			goto IL_41F;
		case 16:
			UIManagerControl.Instance.OpenUI("ElementInstanceUI", null, false, UIType.FullScreen);
			goto IL_41F;
		case 17:
			InstanceManagerUI.OpenSpecialInstanceUI();
			goto IL_41F;
		case 18:
			InstanceManagerUI.OpenEliteDungeonUI();
			goto IL_41F;
		case 19:
			MultiPlayerManager.Instance.OpenMultiPlayerUI(10002, "多人副本");
			goto IL_41F;
		case 21:
			LinkNavigationManager.OpenEquipStrengthenUI(EquipLibType.ELT.Weapon, finish_callback);
			return;
		case 22:
			LinkNavigationManager.OpenEquipGemUI(EquipLibType.ELT.Weapon, null);
			goto IL_41F;
		case 24:
			LinkNavigationManager.OpenPetLevelUI();
			goto IL_41F;
		case 25:
			LinkNavigationManager.OpenPetStarUI();
			goto IL_41F;
		case 27:
			InstanceManagerUI.OpenBountyUI();
			goto IL_41F;
		case 28:
			goto IL_41F;
		case 29:
			CurrenciesUIViewModel.Instance.OnClickGold();
			goto IL_41F;
		case 31:
			EnergyManager.Instance.BuyEnergy(null);
			goto IL_41F;
		case 32:
			LinkNavigationManager.OpenVIPUI2VipLimit();
			return;
		case 33:
			LinkNavigationManager.OpenVIPUI2Recharge();
			return;
		case 35:
			LinkNavigationManager.OpenActorUI(delegate
			{
				ActorUI actorUI = UIManagerControl.Instance.GetUIIfExist("ActorUI") as ActorUI;
				if (!actorUI)
				{
					return;
				}
				actorUI.LogicClickTabToWing();
				if (finish_callback != null)
				{
					finish_callback.Invoke();
				}
			});
			return;
		case 37:
			LinkNavigationManager.OpenEquipStarUpUI(EquipLibType.ELT.Weapon, finish_callback);
			return;
		case 38:
			LinkNavigationManager.OpenEquipWashUI(EquipLibType.ELT.Weapon, finish_callback);
			return;
		case 39:
			LinkNavigationManager.OpenEquipEnchantmentUI(EquipLibType.ELT.Weapon, finish_callback);
			return;
		case 45:
			LinkNavigationManager.OpenGuildUI(null);
			return;
		case 46:
			UIStackManager.Instance.PopTownUI();
			MainTaskManager.Instance.ExecuteTask(MainTaskManager.Instance.RingTaskId, false);
			goto IL_41F;
		case 48:
			UIStackManager.Instance.PopTownUI();
			goto IL_41F;
		case 54:
			InstanceManagerUI.OpenSpecialInstanceGuardUI();
			goto IL_41F;
		case 55:
			LinkNavigationManager.OpenTramcarUI();
			goto IL_41F;
		case 56:
			InstanceManagerUI.OpenSpecialInstanceAttackUI();
			goto IL_41F;
		case 61:
			LinkNavigationManager.OpenMemCollabUI();
			goto IL_41F;
		case 62:
			LinkNavigationManager.OpenXMarketUI();
			return;
		case 63:
			InstanceManagerUI.OpenSpecialInstanceExperienceUI();
			goto IL_41F;
		case 65:
			LinkNavigationManager.OpenGodSoldierUI();
			goto IL_41F;
		case 66:
			LinkNavigationManager.OpenMushroomHitUI();
			goto IL_41F;
		case 67:
			LinkNavigationManager.OpenGodWeaponUI();
			return;
		case 68:
			LinkNavigationManager.OpenBossBookUI();
			return;
		case 71:
			UIManagerControl.Instance.OpenUI("RadarMapUI", UINodesManager.NormalUIRoot, false, UIType.FullScreen);
			goto IL_41F;
		case 74:
			LinkNavigationManager.OpenHuntUI(0);
			return;
		case 78:
			LinkNavigationManager.OpenEquipSuitForgeUI(EquipLibType.ELT.Weapon, finish_callback);
			return;
		case 79:
			LinkNavigationManager.OpenFriendUI();
			return;
		}
		goto IL_17C;
		IL_41F:
		if (finish_callback != null)
		{
			finish_callback.Invoke();
		}
	}

	public static BattleUI OpenBattleUI()
	{
		return UIManagerControl.Instance.OpenUI("BattleUI", null, true, UIType.FullScreen) as BattleUI;
	}

	public static BattleLoseUI OpenBattleLoseUI()
	{
		LinkNavigationManager.BattleFinishSetting();
		UIBase uIBase = UIManagerControl.Instance.OpenUI("BattleLoseUI", null, true, UIType.NonPush);
		return uIBase as BattleLoseUI;
	}

	public static CommonBattlePassUI OpenCommonBattlePassUI()
	{
		LinkNavigationManager.BattleFinishSetting();
		UIBase uIBase = UIManagerControl.Instance.OpenUI("CommonBattlePassUI", null, false, UIType.NonPush);
		return uIBase as CommonBattlePassUI;
	}

	private static void BattleFinishSetting()
	{
		UIManagerControl.Instance.HideUI("RoleShowUI");
		UIManagerControl.Instance.HideUI("ChatUI");
		UIManagerControl.Instance.HideUI("PopButtonsAdjustUI");
	}

	public static MultiPVPPassUI OpenMultiPVPPassUI()
	{
		UIBase uIBase = UIManagerControl.Instance.OpenUI("MultiPVPPassUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		return uIBase as MultiPVPPassUI;
	}

	public static void OpenActivityCenterUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(41, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("ActivityCenterUI", null, true, UIType.FullScreen);
	}

	public static void OpenPVPUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(13, 0, true))
		{
			return;
		}
		InstanceManagerUI.OpenPVPUI(null);
	}

	public static void OpenGangFightUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(14, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("GangFightUI", null, false, UIType.FullScreen);
	}

	public static void OpenSurvivalChallengeUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(15, 0, true))
		{
			return;
		}
		InstanceManagerUI.OpenSurvivalChallengeUI(null);
	}

	public static void OpenSpecialInstanceUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(17, 0, true))
		{
			return;
		}
		InstanceManagerUI.OpenSpecialInstanceUI();
	}

	public static void OpenBountyUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(27, 0, true))
		{
			return;
		}
		InstanceManagerUI.OpenBountyUI();
	}

	public static void OpenTeamUI()
	{
	}

	public static void OpenSeekTeamUI(DungeonType.ENUM type, int param = 0)
	{
		TeamBasicManager.Instance.OpenSeekTeamUI(type, param, null);
	}

	public static void OpenMultiPVPUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(110, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("MultiPVPUI", null, true, UIType.FullScreen);
	}

	public static void OpenMultiPVEUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(110, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("DarkTrialUI", null, true, UIType.FullScreen);
	}

	public static void OpenVIPUI2Privilege()
	{
		if (!SystemOpenManager.IsSystemClickOpen(32, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("PrivilegeUI", null, null);
	}

	public static void OpenVIPUI2Recharge()
	{
		if (!SystemOpenManager.IsSystemClickOpen(32, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("PrivilegeUI", delegate(UIBase uibase)
		{
			PrivilegeUIViewModel.Instance.OnBtnRechargeUp();
			uibase.get_transform().SetAsLastSibling();
		}, null);
	}

	public static void OpenVIPUI2VipLimit()
	{
		if (!SystemOpenManager.IsSystemClickOpen(32, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("PrivilegeUI", delegate(UIBase uibase)
		{
			PrivilegeUIViewModel.Instance.OnBtnVipLimit();
		}, null);
	}

	public static void OpenVIPUI2VipInvest()
	{
		if (!SystemOpenManager.IsSystemClickOpen(32, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("PrivilegeUI", delegate(UIBase uibase)
		{
			PrivilegeUIViewModel.Instance.OnBtnInvest();
		}, null);
	}

	public static void OpenFleaMarketUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(6, 0, true))
		{
			return;
		}
		MarketManager.Instance.OpenShop(3);
	}

	public static void OpenXMarketUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(62, 0, true))
		{
			return;
		}
		XMarketManager.Instance.OpenShop(0);
	}

	public static void OpenXMarketUI2()
	{
		if (!SystemOpenManager.IsSystemClickOpen(62, 0, true))
		{
			return;
		}
		XMarketManager.Instance.OpenShop(1);
	}

	public static void OpenPVPShopUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(105, 0, true))
		{
			return;
		}
		MarketManager.Instance.OpenShop(2);
	}

	public static void OpenGuildMarketUI()
	{
		GuildMarketManager.Instance.OpenShop();
	}

	public static void OpenTownUI()
	{
		UIManagerControl.Instance.OpenUI("TownUI", null, false, UIType.FullScreen);
	}

	public static void OpenSetttingUI()
	{
		UIManagerControl.Instance.OpenUI("SettingUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public static void OpenFriendUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(79, 0, true))
		{
			return;
		}
		FriendUIView friendUIView = UIManagerControl.Instance.OpenUI("FriendUI", UINodesManager.NormalUIRoot, true, UIType.Pop) as FriendUIView;
		friendUIView.get_transform().SetAsLastSibling();
		FriendUIViewModel.Instance.SetChannel(0);
	}

	public static void OpenMailUI()
	{
		UIManagerControl.Instance.OpenUI("MailUI", UINodesManager.NormalUIRoot, true, UIType.FullScreen);
	}

	public static void OpenRankingUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(8, 0, true))
		{
			return;
		}
		RankingManager.Instance.OpenRankingUI();
	}

	public static void OpenTaskDescUI(int taskId, bool isFromClick = false, bool isQuickCommit = false)
	{
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			Debug.Log("任务详细[" + taskId + "]被主城未开拦截!!!");
			MainTaskManager.Instance.IsOpenAgainOnEnable = !isQuickCommit;
			return;
		}
		if (!isFromClick && GuideManager.Instance.talk_desc_ui_lock)
		{
			Debug.Log("任务详细[" + taskId + "]被新手开关拦截!!!");
			GuideManager.Instance.DebugIfNeed();
			return;
		}
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("任务详细[" + taskId + "]被神器开关拦截!!!");
			return;
		}
		bool flag = UIManagerControl.Instance.IsOpen("TaskDescUI");
		UIManagerControl.Instance.HideUI("ChatUI");
		MainTaskManager.Instance.CurTaskId = taskId;
		TaskDescUI.OpenByTaskId = taskId;
		UIManagerControl.Instance.OpenUI("TaskDescUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		if (!flag && GuideManager.Instance.talk_desc_ui_lock && !isFromClick)
		{
			UIManagerControl.Instance.HideUI("TaskDescUI");
			return;
		}
		if (UIManagerControl.Instance.IsOpen("TownUI"))
		{
			TownUI.Instance.SwitchRightBottom(false, false);
		}
	}

	public static void OpenChangeCareerUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(34, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("ChangeCareerUI", UINodesManager.NormalUIRoot, true, UIType.FullScreen);
	}

	public static void OpenGodWeaponUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(67, 0, true))
		{
			return;
		}
		WaitUI.OpenUI(8000u);
		UIManagerControl.Instance.OpenUI("GodWeaponUI", null, true, UIType.FullScreen);
	}

	public static void OpenBossBookUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(68, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("BossBookUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public static void OpenGuildWarInfoUI()
	{
		GuildWarInfoUI guildWarInfoUI = UIManagerControl.Instance.OpenUI("GuildWarInfoUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWarInfoUI;
		guildWarInfoUI.get_transform().SetAsLastSibling();
	}

	public static void OpenZeroTaskUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(104, 0, true))
		{
			return;
		}
		WaitUI.OpenUI(10000u);
		UIManagerControl.Instance.OpenUI("ZeroTaskUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public static void OpenAcOpenServerUI()
	{
		AcOpenServerUI acOpenServerUI = UIManagerControl.Instance.OpenUI("AcOpenServerUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as AcOpenServerUI;
		acOpenServerUI.get_transform().SetAsLastSibling();
	}

	public static void OpenSignInUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(10, 0, true))
		{
			return;
		}
		SignInUI.currentState = SignInUI.SignInUIState.MonthSign;
		UIManagerControl.Instance.OpenUI("SignInUI", UINodesManager.NormalUIRoot, true, UIType.Pop);
	}

	public static void OpenFirstPayUI(Action finish_callback)
	{
		if (!SystemOpenManager.IsSystemClickOpen(9, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("FirstPayGiftUI", UINodesManager.MiddleUIRoot, true, UIType.NonPush);
	}

	public static void OpenLuckDrawUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(7, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("LuckDrawUI", null, true, UIType.FullScreen);
	}

	public static void OpenStrongerUI()
	{
		UIManagerControl.Instance.OpenUI("StrongerUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public static void OpenDailyTaskUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(11, 0, true))
		{
			return;
		}
		WaitUI.OpenUI(5000u);
		UIManagerControl.Instance.OpenUI("DailyTaskUI", null, true, UIType.FullScreen);
	}

	public static void OpenGrowUpPlanUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(57, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("GrowUpPlanUI", null, true, UIType.FullScreen);
	}

	public static void OpenMemCollabUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(61, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("MemCollabUI", UINodesManager.NormalUIRoot, false, UIType.FullScreen);
	}

	public static void OpenOffHookUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(64, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("OffHookUI", UINodesManager.TopUIRoot, false, UIType.Pop);
	}

	public static void OpenMushroomHitUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(66, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("MushroomHitStartUI", null, true, UIType.FullScreen);
	}

	public static void OpenGiftExchangeUI()
	{
		UIManagerControl.Instance.OpenUI("GiftExchangeUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public static void OpenDayGfitUI()
	{
		UIManagerControl.Instance.OpenUI("DayGiftUI", UINodesManager.TopUIRoot, false, UIType.Pop);
	}

	public static void OpenVipTasteCardUI()
	{
		UIManagerControl.Instance.OpenUI("VipTasteCardUI", UINodesManager.MiddleUIRoot, false, UIType.Pop);
		if (VipTasteCardUI.Instance != null)
		{
			VipTasteCardUI.Instance.SwitchMode(1);
		}
	}

	public static void OpenVipExprieCardUI()
	{
		UIManagerControl.Instance.OpenUI("VipTasteCardUI", UINodesManager.MiddleUIRoot, false, UIType.Pop);
		if (VipTasteCardUI.Instance != null)
		{
			VipTasteCardUI.Instance.SwitchMode(2);
		}
	}

	public static void OpenVipLimitardExprieUI()
	{
		UIManagerControl.Instance.OpenUI("VipTasteCardUI", UINodesManager.MiddleUIRoot, false, UIType.Pop);
		if (VipTasteCardUI.Instance != null)
		{
			VipTasteCardUI.Instance.SwitchMode(3);
		}
	}

	public static void OpenHuntUI(int mapId = 0)
	{
		if (!SystemOpenManager.IsSystemClickOpen(74, 0, true))
		{
			return;
		}
		HuntUI huntUI = UIManagerControl.Instance.OpenUI("HuntUI", UINodesManager.NormalUIRoot, false, UIType.FullScreen) as HuntUI;
		if (mapId > 0)
		{
			huntUI.OpenHuntCityById(mapId);
		}
	}

	public static void OpenSevenDayUI()
	{
		UIManagerControl.Instance.OpenUI("SevenDayUI", UINodesManager.MiddleUIRoot, false, UIType.Pop);
	}

	public static void OpenActivityTossDiscountUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(109, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("ActivityTossDiscountUI", UINodesManager.MiddleUIRoot, false, UIType.Pop);
	}

	public static void OpenNewPeoperGiftUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(114, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("NewPeoperGiftPackage", UINodesManager.MiddleUIRoot, false, UIType.Pop);
	}

	public static void OpenCheckPlayerInfoUI()
	{
		UIManagerControl.Instance.OpenUI("CheckPlayerInfoUI", UINodesManager.MiddleUIRoot, false, UIType.Pop);
	}

	public static void OpenRankUpChangeUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(113, 0, true))
		{
			return;
		}
		RankUpChangeManager.Instance.OpenRankUpChangeUI();
	}

	public static void OpenTramcarUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(55, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("TramcarUI", null, false, UIType.FullScreen);
	}

	public static void OpenActorUI(Action finish_callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(5, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("ActorUI", delegate(UIBase uibase)
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenSkillUI(Action finish_callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(3, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("SkillUI", delegate(UIBase uibase)
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenRoleTalentUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(36, 0, true))
		{
			return;
		}
		if (!ChangeCareerManager.Instance.IsCareerChanged())
		{
			UIManagerControl.Instance.ShowToastText("转职后开启");
			return;
		}
		UIManagerControl.Instance.OpenUI("RoleTalentUI", null, false, UIType.FullScreen);
	}

	public static void OpenSkillRuneUI()
	{
		if (!SystemOpenManager.IsSystemOn(3))
		{
			return;
		}
		if (!ChangeCareerManager.Instance.IsCareerChanged())
		{
			UIManagerControl.Instance.ShowToastText("转职后开启");
			return;
		}
		UIManagerControl.Instance.OpenUI("SkillRuneUI", null, false, UIType.FullScreen);
	}

	public static void OpenGodSoldierUI()
	{
		if (!SystemOpenManager.IsSystemClickOpen(65, 0, true))
		{
			return;
		}
		WaitUI.OpenUI(5000u);
		UIManagerControl.Instance.OpenUI("GodSoldierUI", null, true, UIType.FullScreen);
	}

	public static void OpenBackpackUI(Action finish_callback)
	{
		UIManagerControl.Instance.OpenUI_Async("BackpackUI", delegate(UIBase uibase)
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static bool OpenRankUpUI()
	{
		return false;
	}

	public static void OpenEquipStrengthenUI(EquipLibType.ELT elt = EquipLibType.ELT.Weapon, Action finish_callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(21, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("EquipDetailedUI", delegate(UIBase uibase)
		{
			EquipDetailedUI equipDetailedUI = uibase as EquipDetailedUI;
			equipDetailedUI.RefreshUI(EquipDetailedUIState.EquipStrengthen, elt);
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenEquipGemUI(EquipLibType.ELT elt = EquipLibType.ELT.Weapon, Action finish_callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(22, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("EquipDetailedUI", delegate(UIBase uibase)
		{
			EquipDetailedUI equipDetailedUI = uibase as EquipDetailedUI;
			equipDetailedUI.RefreshUI(EquipDetailedUIState.EquipGem, elt);
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenEquipWashUI(EquipLibType.ELT elt = EquipLibType.ELT.Weapon, Action finish_callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(38, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("EquipDetailedUI", delegate(UIBase uibase)
		{
			EquipDetailedUI equipDetailedUI = uibase as EquipDetailedUI;
			equipDetailedUI.RefreshUI(EquipDetailedUIState.EquipWash, elt);
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenEquipStarUpUI(EquipLibType.ELT elt = EquipLibType.ELT.Weapon, Action finish_callback = null)
	{
		UIManagerControl.Instance.OpenUI_Async("EquipDetailedUI", delegate(UIBase uibase)
		{
			EquipDetailedUI equipDetailedUI = uibase as EquipDetailedUI;
			equipDetailedUI.RefreshUI(EquipDetailedUIState.EquipStarUp, elt);
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenEquipEnchantmentUI(EquipLibType.ELT elt = EquipLibType.ELT.Weapon, Action finish_callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(39, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("EquipDetailedUI", delegate(UIBase uibase)
		{
			EquipDetailedUI equipDetailedUI = uibase as EquipDetailedUI;
			equipDetailedUI.RefreshUI(EquipDetailedUIState.EquipEnchantment, elt);
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenEquipSuitForgeUI(EquipLibType.ELT pos = EquipLibType.ELT.Weapon, Action finish_callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(78, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("EquipDetailedUI", delegate(UIBase uibase)
		{
			EquipDetailedUI equipDetailedUI = uibase as EquipDetailedUI;
			equipDetailedUI.RefreshUI(EquipDetailedUIState.EquipSuitForge, pos);
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenPetUI(Action finish_callback)
	{
		if (!SystemOpenManager.IsSystemClickOpen(4, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("PetBasicUI", delegate(UIBase uibase)
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke();
			}
		}, null);
	}

	public static void OpenPetLevelUI()
	{
		PetManager.Instance.Jump2PetLevelUI();
	}

	public static void OpenPetStarUI()
	{
		PetManager.Instance.Jump2PetUpgradeUI();
	}

	public static void OpenPetSkillUI()
	{
		PetManager.Instance.Jump2PetSkillUI();
	}

	public static void OpenPetTaskUI()
	{
		UIManagerControl.Instance.OpenUI("PetTaskUI", UINodesManager.NormalUIRoot, false, UIType.FullScreen);
	}

	public static void OpenGuildUI(Action finish_Callback = null)
	{
		if (!SystemOpenManager.IsSystemClickOpen(45, 0, true))
		{
			return;
		}
		UIManagerControl.Instance.OpenUI_Async("GuildUI", delegate(UIBase uibase)
		{
			if (finish_Callback != null)
			{
				finish_Callback.Invoke();
			}
		}, null);
	}

	public static void OpenGuildWarRewardUI()
	{
	}

	public static void OpenGuildBossUI()
	{
	}

	public static void OpenGuildBossCallUI()
	{
	}

	public static void OpenNpcShopUIUI()
	{
		WaitUI.OpenUI(5000u);
		UIManagerControl.Instance.OpenUI("NpcShopUI", UINodesManager.NormalUIRoot, true, UIType.NonPush);
	}

	public static void OpenRedBagUI()
	{
		UIManagerControl.Instance.OpenUI("RedBagUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
		RedBagManager.Instance.SendGetRedPacketReq();
	}

	public static void ItemNotEnoughToLink(int itemID, bool showItemName = true, Action callbck = null, bool Rclose = true)
	{
		int linkID = 102;
		Items items = DataReader<Items>.Get(itemID);
		if (items == null)
		{
			return;
		}
		Transform root = UINodesManager.TopUIRoot;
		int id = 505103;
		if (items.tab == 4)
		{
			linkID = 7;
			id = 505104;
		}
		else if (items.id == 2)
		{
			linkID = 29;
		}
		else if (items.id == 11)
		{
			linkID = 18;
			id = 505104;
		}
		else if (items.id == 37001)
		{
			id = 505110;
			root = UINodesManager.MiddleUIRoot;
		}
		else if (items.id >= 36200 && items.id <= 36205)
		{
			linkID = 62;
		}
		else if (items.id >= 36206 && items.id <= 36208)
		{
			linkID = 33;
		}
		else if (items.id == 39003)
		{
		}
		if (!SystemConfig.IsOpenPay && (linkID == 62 || linkID == 102))
		{
			string itemName = GameDataUtils.GetItemName(itemID, true, 0L);
			UIManagerControl.Instance.ShowToastText("您所拥有的" + itemName + "不足!!!");
			return;
		}
		string text = string.Empty;
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(linkID);
		if (systemOpen != null)
		{
			string text2 = string.Empty;
			string itemName2 = GameDataUtils.GetItemName(itemID, false, 0L);
			text = GameDataUtils.GetChineseContent(systemOpen.name, false);
			text2 = ((!showItemName) ? "您所拥有的物品不足" : string.Format(GameDataUtils.GetChineseContent(508022, false), itemName2));
			text2 += string.Format("," + GameDataUtils.GetChineseContent(id, false), text);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), text2, null, delegate
			{
				if (callbck != null)
				{
					callbck.Invoke();
					DialogBoxUIViewModel.Instance.BtnRclose = Rclose;
				}
				else
				{
					LinkNavigationManager.SystemLink(linkID, true, null);
				}
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", root, true, true);
			DialogBoxUIView.Instance.SetMask(0.7f, true, false);
		}
	}

	public static RewardUI OpenRewardUI(Transform root)
	{
		return UIManagerControl.Instance.OpenUI("RewardUI", root, false, UIType.NonPush) as RewardUI;
	}
}
