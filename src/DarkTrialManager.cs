using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class DarkTrialManager : BaseSubSystemManager, ITeamRuleManager
{
	protected static DarkTrialManager instance;

	protected XDict<int, int> instanceRank = new XDict<int, int>();

	protected int remainTimes;

	protected int maxTimes;

	protected int curInstanceID;

	public static DarkTrialManager Instance
	{
		get
		{
			if (DarkTrialManager.instance == null)
			{
				DarkTrialManager.instance = new DarkTrialManager();
			}
			return DarkTrialManager.instance;
		}
	}

	public int RemainTimes
	{
		get
		{
			return this.remainTimes;
		}
	}

	public int MaxTimes
	{
		get
		{
			return this.maxTimes;
		}
	}

	protected DarkTrialManager()
	{
		this.maxTimes = (int)float.Parse(DataReader<DarkTrial>.Get("AwardNum").value);
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.instanceRank.Clear();
		this.remainTimes = 0;
		this.curInstanceID = 0;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<MultiPveInfoNty>(new NetCallBackMethod<MultiPveInfoNty>(this.OnDarkTrialInfoNty));
		NetworkManager.AddListenEvent<MultiPveBestRecordInfoNty>(new NetCallBackMethod<MultiPveBestRecordInfoNty>(this.OnDarkTrialInfoNty));
		NetworkManager.AddListenEvent<EnterMultiPveRes>(new NetCallBackMethod<EnterMultiPveRes>(this.OnEnterDarkTrialRes));
		NetworkManager.AddListenEvent<MultiPveSettleNty>(new NetCallBackMethod<MultiPveSettleNty>(this.OnDarkTrialSettleNty));
		NetworkManager.AddListenEvent<LeaveMultiPveRes>(new NetCallBackMethod<LeaveMultiPveRes>(this.OnExitDarkTrialRes));
	}

	protected void OnDarkTrialInfoNty(short state, MultiPveInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.remainTimes = down.todayPlayerTimes;
		this.TryUpdateDarkTrialUI();
	}

	protected void OnDarkTrialInfoNty(short state, MultiPveBestRecordInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		for (int i = 0; i < down.records.get_Count(); i++)
		{
			if (this.instanceRank.ContainsKey(down.records.get_Item(i).dungeonId))
			{
				this.instanceRank[down.records.get_Item(i).dungeonId] = this.GetRankByPassTime(down.records.get_Item(i).dungeonId, down.records.get_Item(i).passTime);
			}
			else
			{
				this.instanceRank.Add(down.records.get_Item(i).dungeonId, this.GetRankByPassTime(down.records.get_Item(i).dungeonId, down.records.get_Item(i).passTime));
			}
		}
		this.TryUpdateDarkTrialUI();
	}

	protected int GetRankByPassTime(int instanceID, int passTime)
	{
		List<DarkLevel> dataList = DataReader<DarkLevel>.DataList;
		List<DarkLevel> list = new List<DarkLevel>();
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).Id == instanceID)
			{
				list.Add(dataList.get_Item(i));
			}
		}
		list.Sort(ComparisonUtility.DarkLevelComparison);
		for (int j = 0; j < list.get_Count(); j++)
		{
			if (passTime < list.get_Item(j).Time)
			{
				return list.get_Item(j).Lv;
			}
		}
		return list.get_Item(list.get_Count() - 1).Lv;
	}

	protected void TryUpdateDarkTrialUI()
	{
		DarkTrialUI darkTrialUI = UIManagerControl.Instance.GetUIIfExist("DarkTrialUI") as DarkTrialUI;
		if (darkTrialUI)
		{
			darkTrialUI.UpdateData();
		}
	}

	public int GetInstanceRank(int instanceID)
	{
		if (!this.instanceRank.ContainsKey(instanceID))
		{
			return -1;
		}
		return this.instanceRank[instanceID];
	}

	public void SeekTeam(int instanceID)
	{
		this.curInstanceID = instanceID;
		TeamBasicManager.Instance.OpenSeekTeamUI(DungeonType.ENUM.MultiPve, this.curInstanceID, null);
	}

	public void ShowMyTeam(int instanceID)
	{
		this.curInstanceID = instanceID;
		UIManagerControl.Instance.OpenUI("TeamBasicUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public void StartDarkTrial(int instanceID)
	{
		this.curInstanceID = instanceID;
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(1005022, false), GameDataUtils.GetChineseContent(50724, false), delegate
			{
			}, delegate
			{
				DarkTrialManager arg_1D_0 = this;
				DungeonType.ENUM arg_1D_1 = DungeonType.ENUM.MultiPve;
				List<int> list = new List<int>();
				list.Add(instanceID);
				arg_1D_0.OnMakeTeam(arg_1D_1, list, 111);
			}, GameDataUtils.GetChineseContent(50725, false), GameDataUtils.GetChineseContent(50726, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
		else if (TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			this.StartDarkTrial();
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516124, false));
		}
	}

	public void StartDarkTrial()
	{
		this.EnterDarkTrial(this.curInstanceID);
	}

	public void EnterDarkTrial(int instanceID)
	{
		if (this.RemainTimes >= this.MaxTimes)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(1005022, false), string.Format(GameDataUtils.GetChineseContent(50740, false), this.MaxTimes), delegate
			{
			}, delegate
			{
				InstanceManager.SecurityCheck(delegate
				{
					NetworkManager.Send(new EnterMultiPveReq
					{
						dungeonId = instanceID
					}, ServerType.Data);
				}, null);
			}, GameDataUtils.GetChineseContent(50742, false), GameDataUtils.GetChineseContent(50741, false), "button_orange_1", "button_yellow_1", null, true, true);
			DialogBoxUIView.Instance.isClick = false;
		}
		else
		{
			InstanceManager.SecurityCheck(delegate
			{
				NetworkManager.Send(new EnterMultiPveReq
				{
					dungeonId = instanceID
				}, ServerType.Data);
			}, null);
		}
	}

	protected void OnEnterDarkTrialRes(short state, EnterMultiPveRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	protected void OnDarkTrialSettleNty(short state, MultiPveSettleNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		DarkTrialInstance.Instance.GetInstanceResult(down);
	}

	public void ExitDarkTrial()
	{
		NetworkManager.Send(new LeaveMultiPveReq(), ServerType.Data);
	}

	protected void OnExitDarkTrialRes(short state, LeaveMultiPveRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public void OnMatchRes(int countDown, bool isOrder = false, Action callBack = null)
	{
	}

	public void OnChallengeRes()
	{
		TeamBasicManager.Instance.OnChallengeSuccessCallBack(DungeonType.ENUM.MultiPve, 1, TeamBasicManager.Instance.CdTime, null);
	}

	public void OnMakeTeam(DungeonType.ENUM dungeonType, List<int> dungeonParams = null, int systemID = 0)
	{
		TeamBasicManager.Instance.OnMakeTeamByDungeonType(dungeonType, dungeonParams, systemID);
	}

	public void OnMatchFailedCallBack()
	{
	}
}
