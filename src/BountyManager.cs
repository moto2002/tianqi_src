using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class BountyManager : BaseSubSystemManager
{
	public BountyLoginPush Info;

	public int freeCountDown;

	public int rewardBoxId = 1;

	public DateTime BoxStarCountdown;

	public Dictionary<ulong, DateTime> ProductionTimeMap = new Dictionary<ulong, DateTime>();

	public DateTime Countdown;

	public bool HasGotRewardDaily;

	public bool[] HasGotRewardUrgent = new bool[3];

	public DateTime BoxStarCountdownUrgent;

	public bool isSelectDaily;

	public bool GettingReward;

	private static BountyManager instance;

	public List<bool> LastStarCondition = new List<bool>();

	public static BountyManager Instance
	{
		get
		{
			if (BountyManager.instance == null)
			{
				BountyManager.instance = new BountyManager();
			}
			return BountyManager.instance;
		}
	}

	private BountyManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<BountyLoginPush>(new NetCallBackMethod<BountyLoginPush>(this.OnBountyLoginPush));
		NetworkManager.AddListenEvent<BountyTaskNty>(new NetCallBackMethod<BountyTaskNty>(this.OnBountyTaskNty));
		NetworkManager.AddListenEvent<BountyTaskRefreshRes>(new NetCallBackMethod<BountyTaskRefreshRes>(this.OnBountyTaskRefreshRes));
		NetworkManager.AddListenEvent<ProductionInfoNty>(new NetCallBackMethod<ProductionInfoNty>(this.OnProductionInfoNty));
		NetworkManager.AddListenEvent<BountyTaskOpenBoxRes>(new NetCallBackMethod<BountyTaskOpenBoxRes>(this.OnBountyTaskOpenBoxRes));
		NetworkManager.AddListenEvent<BountyAccelerateBoxOpenRes>(new NetCallBackMethod<BountyAccelerateBoxOpenRes>(this.OnBountyAccelerateBoxOpenRes));
		NetworkManager.AddListenEvent<BountyGetStarBoxRes>(new NetCallBackMethod<BountyGetStarBoxRes>(this.OnBountyGetStarBoxRes));
		NetworkManager.AddListenEvent<BountyStarBoxNty>(new NetCallBackMethod<BountyStarBoxNty>(this.OnBountyStarBoxNty));
		NetworkManager.AddListenEvent<BountyRankListRes>(new NetCallBackMethod<BountyRankListRes>(this.OnBountyRankListRes));
		NetworkManager.AddListenEvent<BountyTaskResultNty>(new NetCallBackMethod<BountyTaskResultNty>(this.OnBountyTaskResultNty));
		NetworkManager.AddListenEvent<BountyAcceptTaskRes>(new NetCallBackMethod<BountyAcceptTaskRes>(this.OnBountyAcceptTaskRes));
		NetworkManager.AddListenEvent<BountyMatchStatusNty>(new NetCallBackMethod<BountyMatchStatusNty>(this.OnBountyMatchStatusNty));
		NetworkManager.AddListenEvent<BountyRoleDeadNty>(new NetCallBackMethod<BountyRoleDeadNty>(this.OnBountyRoleDeadNty));
		NetworkManager.AddListenEvent<BountyTaskExitBtlRes>(new NetCallBackMethod<BountyTaskExitBtlRes>(this.OnBountyTaskExitBtlRes));
	}

	private void OnBountyLoginPush(short state, BountyLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.Info = down;
			this.SetTaskCountdown();
			this.SetProductionCountdown();
			this.BroadcastRefreshEvent();
		}
		if (this.Info == null)
		{
			Debug.LogError("OnBountyLoginPush Info == null");
		}
	}

	private void OnBountyMatchStatusNty(short state, BountyMatchStatusNty down = null)
	{
		if (state != 0)
		{
			TeamManager.Instance.CloseMatchUI();
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			InstanceManager.ChangeInstanceManager(down.dungeonId, true);
			TowerInstance.Instance.InstanceDataID = down.dungeonId;
		}
	}

	private void OnBountyAcceptTaskRes(short state, BountyAcceptTaskRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		TeamManager.Instance.SendPveAutoMatchReq(AutoMatchType.ENUM.Rule2);
	}

	private void OnBountyTaskResultNty(short state, BountyTaskResultNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.LastStarCondition = down.gotStarCondition;
			this.Info.productions.Clear();
			this.Info.productions.AddRange(down.productions);
			this.Info.score = down.totalScore;
			this.SetProductionCountdown();
			this.BroadcastRefreshEvent();
			BountyInstance.Instance.GetInstanceResult(down);
			using (List<ProductionInfo>.Enumerator enumerator = this.Info.productions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProductionInfo current = enumerator.get_Current();
					if (current.countDown <= 0)
					{
						EventDispatcher.Broadcast("GuideManager.BountyExistProduction");
					}
				}
			}
		}
	}

	private void OnBountyTaskNty(short state, BountyTaskNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.taskType == BountyTaskType.ENUM.Normal)
			{
				this.Info.taskId = down.taskId;
				int num;
				if (down.value <= 0)
				{
					num = 0;
				}
				else
				{
					DateTime dateTime = TimeZone.get_CurrentTimeZone().ToLocalTime(new DateTime(1970, 1, 1));
					num = down.value - (int)(TimeManager.Instance.PreciseServerTime - dateTime).get_TotalSeconds();
				}
				this.Info.freeCountDown = num;
				this.freeCountDown = num;
				this.SetTaskCountdown();
			}
			else if (down.taskType == BountyTaskType.ENUM.Urgent && this.Info != null)
			{
				this.Info.urgentTaskId = down.taskId;
			}
			this.BroadcastRefreshEvent();
		}
	}

	private void OnBountyTaskRefreshRes(short state, BountyTaskRefreshRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.taskId > 0)
		{
			this.Info.taskId = down.taskId;
			this.Info.freeCountDown = down.freeCountDown;
			this.freeCountDown = down.freeCountDown;
			this.SetTaskCountdown();
			this.BroadcastRefreshEvent();
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513629, false), 1f, 2f);
		}
	}

	private void OnProductionInfoNty(short state, ProductionInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.Info.productions.Clear();
			this.Info.productions.AddRange(down.productions);
			this.SetProductionCountdown();
			this.BroadcastRefreshEvent();
			using (List<ProductionInfo>.Enumerator enumerator = this.Info.productions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProductionInfo current = enumerator.get_Current();
					if (current.countDown <= 0)
					{
						EventDispatcher.Broadcast("GuideManager.BountyExistProduction");
					}
				}
			}
		}
	}

	private void OnBountyTaskOpenBoxRes(short state, BountyTaskOpenBoxRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			using (List<BountyTaskOpenBoxRes.DropItemInfo>.Enumerator enumerator = down.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BountyTaskOpenBoxRes.DropItemInfo current = enumerator.get_Current();
					dictionary.Add(current.cfgId, current.count);
				}
			}
			this.ShowRewardDailog(dictionary, null);
			this.BroadcastRefreshEvent();
		}
	}

	private void OnBountyAccelerateBoxOpenRes(short state, BountyAccelerateBoxOpenRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			using (List<BountyAccelerateBoxOpenRes.DropItemInfo>.Enumerator enumerator = down.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BountyAccelerateBoxOpenRes.DropItemInfo current = enumerator.get_Current();
					dictionary.Add(current.cfgId, current.count);
				}
			}
			this.ShowRewardDailog(dictionary, null);
			this.BroadcastRefreshEvent();
		}
	}

	private void OnBountyGetStarBoxRes(short state, BountyGetStarBoxRes down = null)
	{
		if (state != 0)
		{
			BountyManager.Instance.GettingReward = false;
			StateManager.Instance.StateShow(state, 0);
		}
		if (down != null)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			using (List<BountyGetStarBoxRes.DropItemInfo>.Enumerator enumerator = down.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BountyGetStarBoxRes.DropItemInfo current = enumerator.get_Current();
					dictionary.Add(current.cfgId, current.count);
				}
			}
			this.BroadcastRefreshEvent();
			this.ShowRewardDailog(dictionary, null);
			BountyManager.Instance.GettingReward = false;
		}
		else
		{
			BountyManager.Instance.GettingReward = false;
		}
	}

	private void OnBountyRankListRes(short state, BountyRankListRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			BountyRankUI bountyRankUI = UIManagerControl.Instance.OpenUI("BountyRankUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as BountyRankUI;
			bountyRankUI.UpdateList(down.myRankListInfo, down.rankListInfo);
		}
	}

	private void OnBountyStarBoxNty(short state, BountyStarBoxNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.Info.hasStar = down.hasStarDaily;
			this.HasGotRewardDaily = down.hasGotDaily;
			this.rewardBoxId = down.typeIdDaily;
			this.Info.hasStarUrgent = down.hasStar;
			for (int i = 0; i < 3; i++)
			{
				this.HasGotRewardUrgent[i] = down.hasGotBoxCfgIds.Contains(i + 1);
			}
			this.BoxStarCountdownUrgent = TimeManager.Instance.CalculateLocalServerTimeBySecond((int)down.nextOpenUtc);
			this.BroadcastRefreshEvent();
		}
	}

	private void OnBountyRoleDeadNty(short state, BountyRoleDeadNty down = null)
	{
		if (down != null && down.roleId != EntityWorld.Instance.EntSelf.ID)
		{
			UIManagerControl.Instance.ShowBattleToastText(GameDataUtils.GetChineseContent(513648, false), 2f);
		}
	}

	private void OnBountyTaskExitBtlRes(short state, BountyTaskExitBtlRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.Info.score = down.totalScore;
			EventDispatcher.Broadcast(EventNames.BountyRefreshUI);
		}
	}

	private void SetTaskCountdown()
	{
		if (this.Info.freeCountDown >= 0)
		{
			this.Countdown = TimeManager.Instance.PreciseServerTime.AddSeconds((double)this.Info.freeCountDown);
		}
		else
		{
			this.Countdown = TimeManager.Instance.CalculateLocalServerTimeBySecond(0);
		}
	}

	private void SetProductionCountdown()
	{
		this.ProductionTimeMap.Clear();
		using (List<ProductionInfo>.Enumerator enumerator = this.Info.productions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ProductionInfo current = enumerator.get_Current();
				this.ProductionTimeMap.Add(current.uId, TimeManager.Instance.PreciseServerTime.AddSeconds((double)current.countDown));
			}
		}
	}

	private void ShowRewardDailog(Dictionary<int, int> itemIdNum, Action CallBack = null)
	{
		BountyDialogBoxRewardUI bountyDialogBoxRewardUI = UIManagerControl.Instance.OpenUI("BountyDialogBoxRewardUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BountyDialogBoxRewardUI;
		bountyDialogBoxRewardUI.InitUI(513622, itemIdNum);
	}

	public void BroadcastRefreshEvent()
	{
		bool flag = false;
		if (BountyManager.Instance.freeCountDown >= 0)
		{
			if (BountyManager.Instance.Countdown <= TimeManager.Instance.PreciseServerTime)
			{
				flag = true;
			}
		}
		if (this.rewardBoxId > 0 && !BountyManager.Instance.HasGotRewardDaily)
		{
			ShengLiBaoXiang shengLiBaoXiang = DataReader<ShengLiBaoXiang>.Get(this.rewardBoxId);
			if (shengLiBaoXiang != null && this.Info.hasStar >= shengLiBaoXiang.star)
			{
				flag = true;
			}
		}
		flag = (this.HasGotRewardUrgent[0] || this.HasGotRewardUrgent[1] || this.HasGotRewardUrgent[2]);
		using (Dictionary<ulong, DateTime>.ValueCollection.Enumerator enumerator = BountyManager.Instance.ProductionTimeMap.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DateTime current = enumerator.get_Current();
				if (current <= TimeManager.Instance.PreciseServerTime)
				{
					flag = true;
				}
			}
		}
		bool flag2 = this.HasOpenedUrgentTask();
		if (!flag && flag2)
		{
			flag = flag2;
		}
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsTownUiBountyUI, flag);
		EventDispatcher.Broadcast(EventNames.BountyRefreshUI);
	}

	public bool HasOpenedUrgentTask()
	{
		for (int i = 1; i < DataReader<JinJiKaiFangShiJian>.DataList.get_Count() + 1; i++)
		{
			JinJiKaiFangShiJian jinJiKaiFangShiJian = DataReader<JinJiKaiFangShiJian>.Get(i);
			DateTime dateTime = DateTime.Parse(TimeManager.Instance.PreciseServerTime.ToString("yyyy-MM-dd ") + jinJiKaiFangShiJian.openTime);
			DateTime dateTime2 = dateTime.AddSeconds((double)jinJiKaiFangShiJian.time);
			if (dateTime < TimeManager.Instance.PreciseServerTime && TimeManager.Instance.PreciseServerTime < dateTime2)
			{
				return true;
			}
		}
		return false;
	}

	public int GetMarkIndex()
	{
		int num = 0;
		for (int i = 1; i < 5; i++)
		{
			XuanShangJiFenDuan xuanShangJiFenDuan = DataReader<XuanShangJiFenDuan>.Get("dropId" + i);
			if (xuanShangJiFenDuan.low < this.Info.score && this.Info.score <= xuanShangJiFenDuan.high)
			{
				break;
			}
			num++;
		}
		return num;
	}

	public bool HasProductionPos()
	{
		for (int i = 0; i < this.Info.productions.get_Count(); i++)
		{
			ProductionInfo productionInfo = this.Info.productions.get_Item(i);
			if (productionInfo.countDown <= 0)
			{
				return true;
			}
		}
		return false;
	}
}
