using Package;
using System;

public class PopButtonTabsManager
{
	public const string BUTTON_COLOR = "button_yellow_1";

	public static ButtonInfoData GetButtonData2Black(long roleId)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(502019, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				if (FriendManager.Instance.IsRelationOfBlack(roleId))
				{
					UIManagerControl.Instance.ShowToastText("该玩家已在黑名单");
					return;
				}
				DialogBoxUIViewModel.Instance.ShowAsOKCancel("系统提示", GameDataUtils.GetChineseContent(502057, false), delegate
				{
				}, delegate
				{
					FriendManager.Instance.SendMoveToBlackList(roleId);
				}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(502019, false), "button_orange_1", "button_yellow_1", null, true, true);
			}
		};
	}

	public static ButtonInfoData GetButtonData2Delete(long roleId)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(502029, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				DialogBoxUIViewModel.Instance.ShowAsOKCancel("系统提示", GameDataUtils.GetChineseContent(502056, false), delegate
				{
				}, delegate
				{
					FriendManager.Instance.SendDelBuddy(roleId);
				}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(502029, false), "button_orange_1", "button_yellow_1", null, true, true);
			}
		};
	}

	public static ButtonInfoData GetButtonData2Show(long roleId, Action callBack = null)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(502016, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				FriendManager.Instance.SendFindBuddy(roleId);
				if (callBack != null)
				{
					callBack.Invoke();
				}
			}
		};
	}

	public static ButtonInfoData GetButtonData2AddFriend(long roleId)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(502017, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				FriendManager.Instance.SendAddBuddy(roleId, false);
			}
		};
	}

	public static ButtonInfoData GetButtonData2PrivateTalk(long roleId, string roleName)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(502018, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				if (FriendManager.Instance.IsRelationOfBlack(roleId))
				{
					UIManagerControl.Instance.ShowToastText("该玩家已在黑名单");
					return;
				}
				ChatManager.Instance.OpenChatUI2ChannelPrivate(roleId, roleName);
			}
		};
	}

	public static ButtonInfoData GetButtonData2GuildInvitation(long roleId)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(502020, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				GuildManager.Instance.SendInvitePlayerToGuild(roleId);
			}
		};
	}

	public static ButtonInfoData GetButtonData2GuildApplication(long guildId)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(502021, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				GuildManager.Instance.SendMakeAnApplicationForAGuild(guildId);
			}
		};
	}

	public static ButtonInfoData GetButtonData2GuildKick(long roleId, string roleName)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(506062, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				string chineseContent = GameDataUtils.GetChineseContent(506062, false);
				string content = string.Format(GameDataUtils.GetChineseContent(506053, false), roleName);
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, content, delegate
				{
				}, delegate
				{
					GuildManager.Instance.SendKickOffGuildMember(roleId);
				}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(506062, false), "button_orange_1", "button_yellow_1", null, true, true);
			}
		};
	}

	public static ButtonInfoData GetButtonData2GuildChangePos(long roleId, MemberTitleType.MTT title)
	{
		ButtonInfoData buttonInfoData = new ButtonInfoData();
		switch (title)
		{
		case MemberTitleType.MTT.ViceChairman:
			buttonInfoData.buttonName = GameDataUtils.GetChineseContent(506060, false);
			break;
		}
		buttonInfoData.color = "button_yellow_1";
		buttonInfoData.onCall = delegate
		{
		};
		return buttonInfoData;
	}

	public static ButtonInfoData GetButtonData2GuildAppointment(long roleId)
	{
		return new ButtonInfoData
		{
			buttonName = "职位任免",
			color = "button_yellow_1",
			onCall = delegate
			{
				GuildAppointmentTipUI guildAppointmentTipUI = UIManagerControl.Instance.OpenUI("GuildAppointmentTipUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildAppointmentTipUI;
				guildAppointmentTipUI.Refresh(roleId);
			}
		};
	}

	public static ButtonInfoData GetButtonData2TeamInvite(long roleID)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(516111, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				string content = "确定邀请该玩家进行组队吗？";
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, null, delegate
				{
					TeamBasicManager.Instance.SendInvitePartnerReq(roleID);
				}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			}
		};
	}

	public static ButtonInfoData GetButtonData2LeaveTeam(long roleID, string roleName)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(516112, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				string content = string.Format(GameDataUtils.GetChineseContent(516104, false), roleName);
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, null, delegate
				{
					TeamBasicManager.Instance.SendKickoffMemberReq(roleID);
				}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			}
		};
	}

	public static ButtonInfoData GetButtonData2ChangeLeader(long roleId, string roleName)
	{
		return new ButtonInfoData
		{
			buttonName = "转移队长",
			color = "button_yellow_1",
			onCall = delegate
			{
				string content = string.Format(GameDataUtils.GetChineseContent(516105, false), roleName);
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, null, delegate
				{
					TeamBasicManager.Instance.SendTeamChangeLeaderReq(roleId);
				}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			}
		};
	}

	public static ButtonInfoData GetButtonDataTreasure(long roleId, Action action)
	{
		return new ButtonInfoData
		{
			buttonName = "打宝",
			color = "button_yellow_1",
			onCall = delegate
			{
				if (action != null)
				{
					action.Invoke();
				}
				LinkNavigationManager.OpenBossBookUI();
			}
		};
	}

	public static ButtonInfoData GetButtonDataFightBoss(long roleId, Action action)
	{
		return new ButtonInfoData
		{
			buttonName = "boss讨伐",
			color = "button_yellow_1",
			onCall = delegate
			{
				if (action != null)
				{
					action.Invoke();
				}
				InstanceManagerUI.OpenEliteDungeonUI();
			}
		};
	}

	public static ButtonInfoData GetButtonDataRewardTask(long roleId, Action action)
	{
		return new ButtonInfoData
		{
			buttonName = "赏金任务",
			color = "button_yellow_1",
			onCall = delegate
			{
				if (action != null)
				{
					action.Invoke();
				}
				LinkNavigationManager.OpenDailyTaskUI();
			}
		};
	}

	public static ButtonInfoData GetButtonDataDarkTrial(long roleId, Action action)
	{
		return new ButtonInfoData
		{
			buttonName = "黑暗试炼",
			color = "button_yellow_1",
			onCall = delegate
			{
				if (action != null)
				{
					action.Invoke();
				}
				LinkNavigationManager.OpenMultiPVEUI();
			}
		};
	}

	public static ButtonInfoData GetButtonDataZeroCityTask(long roleId, Action action)
	{
		return new ButtonInfoData
		{
			buttonName = "零城任务",
			color = "button_yellow_1",
			onCall = delegate
			{
				if (action != null)
				{
					action.Invoke();
				}
				LinkNavigationManager.OpenZeroTaskUI();
			}
		};
	}
}
