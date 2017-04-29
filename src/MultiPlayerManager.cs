using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class MultiPlayerManager : BaseSubSystemManager, ITeamRuleManager
{
	public int remainChallengeTimes;

	private static MultiPlayerManager instance;

	public static MultiPlayerManager Instance
	{
		get
		{
			if (MultiPlayerManager.instance == null)
			{
				MultiPlayerManager.instance = new MultiPlayerManager();
			}
			return MultiPlayerManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<PveTodayChallengeCountNty>(new NetCallBackMethod<PveTodayChallengeCountNty>(this.OnChallengeCountNty));
		NetworkManager.AddListenEvent<DarkTrainAutoMatchRes>(new NetCallBackMethod<DarkTrainAutoMatchRes>(this.OnDarkTrainAutoMatchRes));
		NetworkManager.AddListenEvent<DarkTrainChallengeRes>(new NetCallBackMethod<DarkTrainChallengeRes>(this.OnDarkTrainChallengeRes));
		NetworkManager.AddListenEvent<DarkTrainQuickEnterRes>(new NetCallBackMethod<DarkTrainQuickEnterRes>(this.OnDarkTrainQuickEnterRes));
		NetworkManager.AddListenEvent<DarkTrainCancelMatchRes>(new NetCallBackMethod<DarkTrainCancelMatchRes>(this.OnDarkTrainCancelMatchReq));
		NetworkManager.AddListenEvent<PveBattleResultNty>(new NetCallBackMethod<PveBattleResultNty>(this.OnPveBattleResultNty));
		NetworkManager.AddListenEvent<PveEnterBattleNty>(new NetCallBackMethod<PveEnterBattleNty>(this.OnPveEnterBattleNty));
		NetworkManager.AddListenEvent<PveExitBattleRes>(new NetCallBackMethod<PveExitBattleRes>(this.OnPveExitBattleRes));
		NetworkManager.AddListenEvent<PveReadyTimeNty>(new NetCallBackMethod<PveReadyTimeNty>(this.OnPveReadyTimeNty));
		NetworkManager.AddListenEvent<PvePlayerExitNty>(new NetCallBackMethod<PvePlayerExitNty>(this.OnPvePlayerExitNty));
		NetworkManager.AddListenEvent<ExitBattleRes>(new NetCallBackMethod<ExitBattleRes>(this.OnExitBattleRes));
	}

	public override void Release()
	{
	}

	private void OnChallengeCountNty(short state, PveTodayChallengeCountNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.remainChallengeTimes = down.count;
		EventDispatcher.Broadcast<int>("RefreshChallengeCount", this.remainChallengeTimes);
	}

	private void OnDarkTrainAutoMatchRes(short state, DarkTrainAutoMatchRes down)
	{
		if (state != 0)
		{
			if (down != null && down.reasons != null && down.reasons.get_Count() > 0)
			{
				TeamDetailReason.RSType reasonType = down.reasons.get_Item(0).reasonType;
				int num = 0;
				switch (reasonType)
				{
				case TeamDetailReason.RSType.LvLimit:
					num = 516123;
					break;
				case TeamDetailReason.RSType.TimesLimit:
					num = 516120;
					break;
				case TeamDetailReason.RSType.BagFull:
					num = 516122;
					break;
				case TeamDetailReason.RSType.InFighting:
					num = 516115;
					break;
				case TeamDetailReason.RSType.NotAgree:
					num = 516119;
					break;
				case TeamDetailReason.RSType.NotAnswer:
					num = 516119;
					break;
				}
				if (num != 0)
				{
					string chineseContent = GameDataUtils.GetChineseContent(num, false);
					UIManagerControl.Instance.ShowToastText(chineseContent);
					return;
				}
			}
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		int countDown = (int)float.Parse(DataReader<MultiCopy>.Get("match_auto_time").value);
		this.OnMatchRes(countDown, false, null);
	}

	private void OnDarkTrainChallengeRes(short state, DarkTrainChallengeRes down)
	{
		if (state != 0)
		{
			if (down != null && down.reasons != null && down.reasons.get_Count() > 0)
			{
				TeamDetailReason.RSType reasonType = down.reasons.get_Item(0).reasonType;
				int num = 0;
				TeamDetailReason.RSType rSType = reasonType;
				switch (rSType)
				{
				case TeamDetailReason.RSType.LvLimit:
					num = 516123;
					goto IL_CD;
				case TeamDetailReason.RSType.TimesLimit:
					num = 516120;
					goto IL_CD;
				case TeamDetailReason.RSType.BagFull:
					num = 516122;
					goto IL_CD;
				case TeamDetailReason.RSType.NotFound:
				case (TeamDetailReason.RSType)6:
				case (TeamDetailReason.RSType)7:
				case (TeamDetailReason.RSType)8:
				case (TeamDetailReason.RSType)9:
				case (TeamDetailReason.RSType)10:
					IL_76:
					if (rSType != TeamDetailReason.RSType.Others)
					{
						goto IL_CD;
					}
					goto IL_CD;
				case TeamDetailReason.RSType.InFighting:
					num = 516115;
					goto IL_CD;
				case TeamDetailReason.RSType.NotAgree:
					num = 516119;
					goto IL_CD;
				case TeamDetailReason.RSType.NotAnswer:
					num = 516119;
					goto IL_CD;
				}
				goto IL_76;
				IL_CD:
				if (num != 0)
				{
					string chineseContent = GameDataUtils.GetChineseContent(num, false);
					UIManagerControl.Instance.ShowToastText(chineseContent);
					return;
				}
			}
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OnChallengeRes();
	}

	private void OnDarkTrainQuickEnterRes(short state, DarkTrainQuickEnterRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		int countDown = (int)float.Parse(DataReader<MultiCopy>.Get("match_2_time").value);
		this.OnMatchRes(countDown, false, null);
	}

	private void OnDarkTrainCancelMatchReq(short state, DarkTrainCancelMatchRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText("取消匹配成功");
	}

	private void OnPveEnterBattleNty(short state, PveEnterBattleNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			MultiPlayerInstance.Instance.isMulti = true;
			Debug.Log("服务端通知客户端进入多人副本战斗成功");
			TeamManager.Instance.CloseMatchUI();
			Debug.Log("down.DungeonID-------" + down.dungeonId);
			InstanceManager.ChangeInstanceManager(down.dungeonId, false);
		}
	}

	private void OnPveBattleResultNty(short state, PveBattleResultNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			InstanceManager.StopAllClientAI(true);
			MultiPlayerInstance.Instance.GetInstanceResult(down);
		}
	}

	private void OnPveReadyTimeNty(short state, PveReadyTimeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Debuger.Info("down.time" + down.time, new object[0]);
			if (down.time == 0)
			{
				FigureTime.CanleTimer(false);
			}
		}
	}

	private void OnPvePlayerExitNty(short state, PvePlayerExitNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			string text = string.Empty;
			if (down.roles != null)
			{
				for (int i = 0; i < down.roles.get_Count(); i++)
				{
					if (down.roles.get_Item(i).roleId != EntityWorld.Instance.EntSelf.ID)
					{
						text += down.roles.get_Item(i).roleName;
					}
				}
			}
			text += " 退出游戏";
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), text, delegate
			{
				MultiPlayerInstance.Instance.isMulti = false;
				this.SendPveExitBattleReq();
				this.SendExitBattleReq();
			}, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
		}
	}

	private void OnPveExitBattleRes(short state, PveExitBattleRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
			if (down.forceExit)
			{
				EventDispatcher.Broadcast("SHOW_TOWN_UI");
			}
		}
	}

	private void OnExitBattleRes(short state, ExitBattleRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public void SendDarkTrainAutoMatchReq()
	{
		InstanceManager.SecurityCheck(delegate
		{
			NetworkManager.Send(new DarkTrainAutoMatchReq(), ServerType.Data);
		}, null);
	}

	public void SendDarkTrainChallengeReq()
	{
		NetworkManager.Send(new DarkTrainChallengeReq(), ServerType.Data);
	}

	public void SendDarkTrainQuickEnterReq()
	{
		NetworkManager.Send(new DarkTrainQuickEnterReq(), ServerType.Data);
	}

	public void SendDarkDarkTrainCancelMatchReq()
	{
		NetworkManager.Send(new DarkTrainCancelMatchReq(), ServerType.Data);
	}

	public void SendPveExitBattleReq()
	{
		MultiPlayerInstance.Instance.isMulti = false;
		NetworkManager.Send(new PveExitBattleReq(), ServerType.Data);
	}

	public void SendExitBattleReq()
	{
		NetworkManager.Send(new ExitBattleReq
		{
			realRoleId = EntityWorld.Instance.EntSelf.ID
		}, ServerType.Data);
	}

	public void OpenMultiPlayerUI(int activityID, string activityName = "")
	{
		MultiPlayerUI multiPlayerUI = UIManagerControl.Instance.OpenUI("MultiPlayerUI", null, true, UIType.FullScreen) as MultiPlayerUI;
		multiPlayerUI.SettingUI(activityID, activityName);
	}

	public void CheckCanStartFight()
	{
		TeamBasicManager.Instance.OnClickStart(DungeonType.ENUM.Team, delegate
		{
			MultiPlayerManager.Instance.SendDarkTrainAutoMatchReq();
		}, delegate
		{
			MultiPlayerManager.Instance.SendDarkTrainChallengeReq();
		});
	}

	public void OnMatchRes(int countDown, bool isOrder = false, Action callBack = null)
	{
		TeamBasicManager.Instance.OnMatchRes(countDown, false, delegate
		{
			this.SendDarkDarkTrainCancelMatchReq();
		});
	}

	public void OnChallengeRes()
	{
		TeamBasicManager.Instance.OnChallengeSuccessCallBack(DungeonType.ENUM.Team, 1, TeamBasicManager.Instance.CdTime, null);
	}

	public void OnMakeTeam(DungeonType.ENUM dungeonType, List<int> dungeonParams = null, int systemID = 0)
	{
		TeamBasicManager.Instance.OnMakeTeamByDungeonType(dungeonType, dungeonParams, systemID);
	}

	public void OnMatchFailedCallBack()
	{
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(516118, false), null, delegate
			{
				EliteDungeonManager.Instance.SendEliteChallengeReq(EliteDungeonManager.Instance.eliteCfgID);
			}, "前往招募", "立即挑战", "button_orange_1", "button_yellow_1", null, true, true);
			return;
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516118, false));
	}
}
