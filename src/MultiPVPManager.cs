using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class MultiPVPManager : BaseSubSystemManager, ITeamRuleManager
{
	public int VictoryKillNum;

	public int MyTotalKillNum;

	public int MyTotalDeathNum;

	public int MyTeamTotalKillNum;

	public int EnemyTeamTotalKillNum;

	public List<MultiPvpDailyRewardInfoNty.MultiPvpRewardInfo> MultiPvpRewardInfoList;

	public static bool isOpenQuickBtn;

	private static MultiPVPManager instance;

	protected BattleUI battleUI;

	private bool isKillToastStart;

	private List<string> killToastTipList;

	private bool isKillToastTipShow;

	private uint killToastTimerID;

	private bool isKillComboStart;

	private List<string> killComboTipList;

	private bool isKillComboTipShow;

	private uint killComboTimerID;

	public static MultiPVPManager Instance
	{
		get
		{
			if (MultiPVPManager.instance == null)
			{
				MultiPVPManager.instance = new MultiPVPManager();
			}
			return MultiPVPManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
		if (this.GetMultiPvpCfgValueByKey("victory") != null && this.GetMultiPvpCfgValueByKey("victory").get_Count() > 0)
		{
			this.VictoryKillNum = this.GetMultiPvpCfgValueByKey("victory").get_Item(0);
		}
	}

	public override void Release()
	{
		this.MyTotalKillNum = 0;
		this.MyTotalDeathNum = 0;
		this.VictoryKillNum = 0;
		this.RemoveKillToastTip();
		this.RemoveKillComboTip();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<MultiPvpGetDailyRewardRes>(new NetCallBackMethod<MultiPvpGetDailyRewardRes>(this.OnMultiPvpGetDailyRewardRes));
		NetworkManager.AddListenEvent<MultiPvpDailyRewardInfoNty>(new NetCallBackMethod<MultiPvpDailyRewardInfoNty>(this.OnMultiPvpDailyRewardInfoNty));
		NetworkManager.AddListenEvent<MultiPvpBeginMatchRes>(new NetCallBackMethod<MultiPvpBeginMatchRes>(this.OnMultiPvpBeginMatchRes));
		NetworkManager.AddListenEvent<LeaveMultiPvpRes>(new NetCallBackMethod<LeaveMultiPvpRes>(this.OnLeaveMultiPvpRes));
		NetworkManager.AddListenEvent<MultiPvpBattleInfoNty>(new NetCallBackMethod<MultiPvpBattleInfoNty>(this.OnMultiPvpBattleInfoNty));
		NetworkManager.AddListenEvent<MultiPvpRoleInfoUpdateNty>(new NetCallBackMethod<MultiPvpRoleInfoUpdateNty>(this.OnMultiPvpRoleInfoUpdateNty));
		NetworkManager.AddListenEvent<MultiPvpSettleNty>(new NetCallBackMethod<MultiPvpSettleNty>(this.OnMultiPvpSettleNty));
		NetworkManager.AddListenEvent<MultiPvpBattleKillNty>(new NetCallBackMethod<MultiPvpBattleKillNty>(this.OnMultiPvpBattleKillNty));
		NetworkManager.AddListenEvent<MultiPvpBattleComboNty>(new NetCallBackMethod<MultiPvpBattleComboNty>(this.OnMultiPvpBattleComboNty));
	}

	public void SendMultiPvpBeginMatchReq()
	{
		NetworkManager.Send(new MultiPvpBeginMatchReq(), ServerType.Data);
	}

	public void SendMultiPvpGetDailyRewardReq(int type = 1)
	{
		NetworkManager.Send(new MultiPvpGetDailyRewardReq
		{
			rewardType = type
		}, ServerType.Data);
	}

	public void SendLeaveMultiPvpReq()
	{
		NetworkManager.Send(new LeaveMultiPvpReq(), ServerType.Data);
	}

	private void OnMultiPvpGetDailyRewardRes(short state, MultiPvpGetDailyRewardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			List<int> list = new List<int>();
			List<long> list2 = new List<long>();
			List<long> list3 = new List<long>();
			for (int i = 0; i < down.rewardList.get_Count(); i++)
			{
				list.Add(down.rewardList.get_Item(i).cfgId);
				list2.Add(down.rewardList.get_Item(i).count);
				list3.Add(down.rewardList.get_Item(i).uId);
			}
			if (list.get_Count() > 0)
			{
				RewardUI rewardUI = UIManagerControl.Instance.OpenUI("RewardUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as RewardUI;
				rewardUI.SetRewardItem(GameDataUtils.GetChineseContent(513622, false), list, list2, true, false, null, list3);
			}
		}
	}

	private void OnMultiPvpDailyRewardInfoNty(short state, MultiPvpDailyRewardInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.MultiPvpRewardInfoList = down.dailyReward;
			EventDispatcher.Broadcast(EventNames.OnMultiPvpDailyRewardInfoNty);
		}
	}

	private void OnMultiPvpBeginMatchRes(short state, MultiPvpBeginMatchRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.OnMatchRes(30, true, delegate
		{
		});
	}

	private void OnLeaveMultiPvpRes(short state, LeaveMultiPvpRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RemoveKillToastTip();
			this.RemoveKillComboTip();
		}
	}

	private void OnMultiPvpBattleInfoNty(short state, MultiPvpBattleInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.infoList.get_Count(); i++)
			{
				if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.Camp == down.infoList.get_Item(i).camp)
				{
					this.MyTeamTotalKillNum = down.infoList.get_Item(i).killCount;
				}
				else
				{
					this.EnemyTeamTotalKillNum = down.infoList.get_Item(i).killCount;
				}
			}
			this.UpdateMultiPvpVSInfo();
		}
	}

	private void OnMultiPvpRoleInfoUpdateNty(short state, MultiPvpRoleInfoUpdateNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.MyTotalKillNum = down.killCount;
			this.MyTotalDeathNum = down.deathCount;
			this.UpdateMyRoleBattleInfo();
		}
	}

	private void OnMultiPvpSettleNty(short state, MultiPvpSettleNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RemoveKillToastTip();
			this.RemoveKillComboTip();
			MultiPVPInstance.Instance.GetInstanceResult(down);
		}
	}

	private void OnMultiPvpBattleKillNty(short state, MultiPvpBattleKillNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle)
		{
			string text = string.Format(GameDataUtils.GetChineseContent(503113, false), down.killerName, down.deadName);
			if (this.killToastTipList == null)
			{
				this.killToastTipList = new List<string>();
			}
			this.killToastTipList.Add(text);
			if (!this.isKillToastTipShow)
			{
				this.isKillToastTipShow = true;
				this.isKillToastStart = true;
				this.ShowKillToastTip(this.killToastTipList);
			}
		}
	}

	private void OnMultiPvpBattleComboNty(short state, MultiPvpBattleComboNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle)
		{
			if (down.combo <= 1)
			{
				return;
			}
			string text = string.Empty;
			int id = 503114;
			if (down.combo == 3)
			{
				id = 503115;
			}
			else if (down.combo == 4)
			{
				id = 503116;
			}
			else if (down.combo == 5)
			{
				id = 503117;
			}
			else if (down.combo >= 6)
			{
				id = 503118;
			}
			text = string.Format(GameDataUtils.GetChineseContent(id, false), down.roleName);
			if (this.killComboTipList == null)
			{
				this.killComboTipList = new List<string>();
			}
			this.killComboTipList.Add(text);
			if (!this.isKillComboTipShow)
			{
				this.isKillComboTipShow = true;
				this.isKillComboStart = true;
				this.ShowKillComboTip(this.killComboTipList);
			}
		}
	}

	public void ShowReliveUI()
	{
		GuildWarReliveUI guildWarReliveUI = UIManagerControl.Instance.OpenUI("GuildWarReliveUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWarReliveUI;
		int second = 5;
		if (this.GetMultiPvpCfgValueByKey("safeRevivalTime") != null && this.GetMultiPvpCfgValueByKey("safeRevivalTime").get_Count() > 0)
		{
			second = this.GetMultiPvpCfgValueByKey("safeRevivalTime").get_Item(0) / 1000;
		}
		guildWarReliveUI.RefreshUI(second, delegate
		{
		}, false);
	}

	public void HideReliveUI()
	{
		UIManagerControl.Instance.HideUI("GuildWarReliveUI");
	}

	public void OpenMatchUI(int coolDown = 30)
	{
		MatchUI matchUI = UIManagerControl.Instance.OpenUI("MatchUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as MatchUI;
		matchUI.SetData(coolDown, true, delegate
		{
		});
	}

	public void UpdateMyRoleBattleInfo()
	{
		if (this.battleUI == null)
		{
			this.battleUI = (UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI);
		}
		if (this.battleUI)
		{
			this.battleUI.SetPeakBattleKillAndDeathNumText(this.MyTotalKillNum, this.MyTotalDeathNum);
		}
	}

	public void UpdateMultiPvpVSInfo()
	{
		if (this.battleUI == null)
		{
			this.battleUI = (UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI);
		}
		if (this.battleUI)
		{
			this.battleUI.SetPeakBattleVSText(this.MyTeamTotalKillNum, this.EnemyTeamTotalKillNum);
		}
	}

	public List<int> GetMultiPvpCfgValueByKey(string keyName = "")
	{
		List<int> result = new List<int>();
		if (DataReader<DianFengZhanChangPeiZhi>.Contains(keyName))
		{
			result = DataReader<DianFengZhanChangPeiZhi>.Get(keyName).value;
		}
		return result;
	}

	public List<MultiPvpRoleInfo> GetMultiPvpRoleInfoListByCamp(List<MultiPvpRoleInfo> multiPvpRoleInfoList, bool isMyTeam = true)
	{
		List<MultiPvpRoleInfo> list = new List<MultiPvpRoleInfo>();
		if (multiPvpRoleInfoList == null || multiPvpRoleInfoList.get_Count() < 0)
		{
			return list;
		}
		int camp = 0;
		if (EntityWorld.Instance.EntSelf != null)
		{
			camp = EntityWorld.Instance.EntSelf.Camp;
		}
		if (isMyTeam)
		{
			list = multiPvpRoleInfoList.FindAll((MultiPvpRoleInfo a) => a.camp == camp);
		}
		else
		{
			list = multiPvpRoleInfoList.FindAll((MultiPvpRoleInfo a) => a.camp != camp);
		}
		list.Sort(new Comparison<MultiPvpRoleInfo>(MultiPVPManager.MultiPvpRankingSortCompare));
		return list;
	}

	public int GetMulitPvpTotalKillNum(List<MultiPvpRoleInfo> multiPvpRoleInfoList, bool isMyTeam = true)
	{
		int num = 0;
		List<MultiPvpRoleInfo> multiPvpRoleInfoListByCamp = this.GetMultiPvpRoleInfoListByCamp(multiPvpRoleInfoList, isMyTeam);
		if (multiPvpRoleInfoListByCamp == null)
		{
			return num;
		}
		for (int i = 0; i < multiPvpRoleInfoListByCamp.get_Count(); i++)
		{
			num += multiPvpRoleInfoListByCamp.get_Item(i).killCount;
		}
		return num;
	}

	public List<KeyValuePair<int, long>> GetDropRewardLit(int dropID)
	{
		List<KeyValuePair<int, long>> list = new List<KeyValuePair<int, long>>();
		List<DiaoLuo> list2 = DataReader<DiaoLuo>.DataList.FindAll((DiaoLuo a) => a.ruleId == dropID);
		if (list2 != null)
		{
			for (int i = 0; i < list2.get_Count(); i++)
			{
				if (list2.get_Item(i).dropType == 1)
				{
					list.Add(new KeyValuePair<int, long>(list2.get_Item(i).goodsId, list2.get_Item(i).minNum));
				}
			}
		}
		return list;
	}

	public int GetActivityRemainTime()
	{
		if (ActivityCenterManager.Instance.CheckActivityIsOpen(10006))
		{
			HuoDongZhongXin activityCfgData = ActivityCenterManager.Instance.GetActivityCfgData(ActivityType.MultiPVP);
			if (activityCfgData != null)
			{
				DateTime preciseServerTime = TimeManager.Instance.PreciseServerTime;
				for (int i = 0; i < activityCfgData.starttime.get_Count(); i++)
				{
					string[] array = activityCfgData.starttime.get_Item(i).Split(new char[]
					{
						':'
					});
					int num = int.Parse((!array[0].StartsWith("0")) ? array[0] : array[0].Substring(1));
					int num2 = int.Parse((!array[1].StartsWith("0")) ? array[1] : array[1].Substring(1));
					string[] array2 = activityCfgData.endtime.get_Item(i).Split(new char[]
					{
						':'
					});
					int num3 = int.Parse((!array2[0].StartsWith("0")) ? array2[0] : array2[0].Substring(1));
					int num4 = int.Parse((!array2[1].StartsWith("0")) ? array2[1] : array2[1].Substring(1));
					DateTime dateTime = new DateTime(preciseServerTime.get_Year(), preciseServerTime.get_Month(), preciseServerTime.get_Day(), num, num2, 0);
					DateTime dateTime2 = new DateTime(preciseServerTime.get_Year(), preciseServerTime.get_Month(), preciseServerTime.get_Day(), num3, num4, 0);
					if (dateTime <= preciseServerTime && preciseServerTime <= dateTime2)
					{
						return (int)(dateTime2 - preciseServerTime).get_TotalSeconds();
					}
				}
			}
		}
		return 0;
	}

	public string GetMultiPvpOpenTimeText(int index = 0)
	{
		HuoDongZhongXin activityCfgData = ActivityCenterManager.Instance.GetActivityCfgData(ActivityType.MultiPVP);
		if (activityCfgData != null)
		{
			DateTime preciseServerTime = TimeManager.Instance.PreciseServerTime;
			int i = 0;
			while (i < activityCfgData.starttime.get_Count())
			{
				string[] array = activityCfgData.starttime.get_Item(i).Split(new char[]
				{
					':'
				});
				int num = int.Parse((!array[0].StartsWith("0")) ? array[0] : array[0].Substring(1));
				int num2 = int.Parse((!array[1].StartsWith("0")) ? array[1] : array[1].Substring(1));
				string[] array2 = activityCfgData.endtime.get_Item(i).Split(new char[]
				{
					':'
				});
				int num3 = int.Parse((!array2[0].StartsWith("0")) ? array2[0] : array2[0].Substring(1));
				int num4 = int.Parse((!array2[1].StartsWith("0")) ? array2[1] : array2[1].Substring(1));
				DateTime dateTime = new DateTime(preciseServerTime.get_Year(), preciseServerTime.get_Month(), preciseServerTime.get_Day(), num, num2, 0);
				DateTime dateTime2 = new DateTime(preciseServerTime.get_Year(), preciseServerTime.get_Month(), preciseServerTime.get_Day(), num3, num4, 0);
				if (index == i)
				{
					if (ActivityCenterManager.Instance.CheckActivityIsOpen(10006) && dateTime <= preciseServerTime && preciseServerTime <= dateTime2)
					{
						string text = activityCfgData.starttime.get_Item(i) + "-" + activityCfgData.endtime.get_Item(i) + "（进行中）";
						return TextColorMgr.GetColor(text, "fffae6", string.Empty);
					}
					string text2 = activityCfgData.starttime.get_Item(i) + "-" + activityCfgData.endtime.get_Item(i) + "（未开始）";
					return TextColorMgr.GetColor(text2, "4d2a1a", string.Empty);
				}
				else
				{
					i++;
				}
			}
		}
		return string.Empty;
	}

	private static int MultiPvpRankingSortCompare(MultiPvpRoleInfo AF1, MultiPvpRoleInfo AF2)
	{
		int result = 0;
		if (AF1.score > AF2.score)
		{
			return -1;
		}
		if (AF1.score == AF2.score && AF1.roleLv >= AF1.roleLv)
		{
			return -1;
		}
		if (AF1.score < AF2.score)
		{
			return 1;
		}
		return result;
	}

	private void ShowKillToastTip(List<string> contentList)
	{
		if (contentList != null && contentList.get_Count() > 0)
		{
			int start = (!this.isKillToastStart) ? 3000 : 0;
			this.killToastTimerID = TimerHeap.AddTimer((uint)start, 0, delegate
			{
				this.isKillToastStart = false;
				if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle)
				{
					BattleKillToastUI battleKillToastUI = UIManagerControl.Instance.OpenUI("BattleKillToastUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BattleKillToastUI;
					battleKillToastUI.ShowText(contentList.get_Item(0), 3f);
				}
				contentList.RemoveAt(0);
				this.ShowKillToastTip(contentList);
			});
		}
		else
		{
			this.RemoveKillToastTip();
		}
	}

	private void RemoveKillToastTip()
	{
		if (this.killToastTipList != null)
		{
			this.killToastTipList.Clear();
		}
		TimerHeap.DelTimer(this.killToastTimerID);
		this.isKillToastTipShow = false;
	}

	private void ShowKillComboTip(List<string> contentList)
	{
		if (contentList != null && contentList.get_Count() > 0)
		{
			int start = (!this.isKillComboStart) ? 3000 : 0;
			this.killComboTimerID = TimerHeap.AddTimer((uint)start, 0, delegate
			{
				this.isKillComboStart = false;
				if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle)
				{
					UIManagerControl.Instance.ShowBattleToastText(contentList.get_Item(0), 2f);
				}
				contentList.RemoveAt(0);
				this.ShowKillComboTip(contentList);
			});
		}
		else
		{
			this.RemoveKillComboTip();
		}
	}

	private void RemoveKillComboTip()
	{
		if (this.killComboTipList != null)
		{
			this.killComboTipList.Clear();
		}
		TimerHeap.DelTimer(this.killComboTimerID);
		this.isKillComboTipShow = false;
	}

	public void OnClickMatch()
	{
		TeamBasicManager.Instance.OnClickMatch(DungeonType.ENUM.MultiPvp, delegate
		{
			this.SendMultiPvpBeginMatchReq();
		});
	}

	public void OnMatchRes(int countDown, bool isOrder = false, Action callBack = null)
	{
		if (TeamBasicManager.Instance.MyTeamData == null || (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Count() <= 1))
		{
			TeamBasicManager.Instance.OnMatchRes(countDown, isOrder, callBack);
		}
	}

	public void OnChallengeRes()
	{
	}

	public void OnMakeTeam(DungeonType.ENUM dungeonType = DungeonType.ENUM.MultiPvp, List<int> dungeonParams = null, int systemID = 0)
	{
		TeamBasicManager.Instance.OnMakeTeamByDungeonType(dungeonType, dungeonParams, systemID);
	}

	public void OnMatchFailedCallBack()
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), "当前没有队伍可以加入，是否创建队伍？", null, delegate
			{
				this.OnMakeTeam(DungeonType.ENUM.MultiPvp, new List<int>(), 110);
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
	}
}
