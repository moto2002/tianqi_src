using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class WildBossManager : BaseSubSystemManager
{
	protected static readonly int CityFakeDropModel = 10003;

	protected static WildBossManager instance;

	protected bool isCurrentSceneUseTimes = true;

	protected int bossRemainRewardTimes;

	protected int multiBossRemainRewardTimes;

	protected XDict<int, WildBossData> bossData = new XDict<int, WildBossData>();

	protected int challengeLevelFloor;

	protected int challengeLevelRange;

	protected int bossEnterID;

	protected int bossEnterMonsterDataID;

	protected bool isSingleBossWaiting;

	protected int singleBossWaitingBossID;

	protected int singleBossWaitingBossTypeID;

	protected int singleBossWaitingBossLevel;

	protected int singleBossWaitingNum = -1;

	protected bool isMultiBossWaiting;

	protected int multiBossWaitingBossID;

	protected int multiBossWaitingBossTypeID;

	protected int multiBossWaitingBossLevel;

	protected int multiBossWaitingNum = -1;

	protected bool isWaitingShowQueueDialog;

	protected bool isShowingQueueDialog;

	protected long bossCurHp;

	protected long bossHpLmt;

	protected List<WildBossUICurrentData> wildBossUICurrentDataList = new List<WildBossUICurrentData>();

	protected List<WildBossUIWaitingData> wildBossUIWaitingDataList = new List<WildBossUIWaitingData>();

	protected WildBossUIWaitingData selfWaitingData;

	public static WildBossManager Instance
	{
		get
		{
			if (WildBossManager.instance == null)
			{
				WildBossManager.instance = new WildBossManager();
			}
			return WildBossManager.instance;
		}
	}

	public int BossRemainRewardTimes
	{
		get
		{
			return this.bossRemainRewardTimes;
		}
	}

	public int MultiBossRemainRewardTimes
	{
		get
		{
			return this.multiBossRemainRewardTimes;
		}
	}

	public XDict<int, WildBossData> BossData
	{
		get
		{
			return this.bossData;
		}
	}

	protected int BossEnterID
	{
		get
		{
			return this.bossEnterID;
		}
		set
		{
			this.bossEnterID = value;
		}
	}

	protected int BossEnterMonsterDataID
	{
		get
		{
			return this.bossEnterMonsterDataID;
		}
		set
		{
			this.bossEnterMonsterDataID = value;
		}
	}

	public bool IsWaitForUI
	{
		get
		{
			return this.BossEnterID > 0 && this.BossEnterMonsterDataID > 0;
		}
	}

	public bool IsSingleBossWaiting
	{
		get
		{
			return this.isSingleBossWaiting;
		}
		protected set
		{
			this.isSingleBossWaiting = value;
		}
	}

	protected int SingleBossWaitingBossID
	{
		get
		{
			return this.singleBossWaitingBossID;
		}
		set
		{
			this.singleBossWaitingBossID = value;
		}
	}

	protected int SingleBossWaitingBossTypeID
	{
		get
		{
			return this.singleBossWaitingBossTypeID;
		}
		set
		{
			this.singleBossWaitingBossTypeID = value;
		}
	}

	public string SingleBossWaitingBossName
	{
		get
		{
			if (!DataReader<Monster>.Contains(this.SingleBossWaitingBossTypeID))
			{
				return string.Empty;
			}
			return GameDataUtils.GetChineseContent(DataReader<Monster>.Get(this.SingleBossWaitingBossTypeID).name, false);
		}
	}

	protected int SingleBossWaitingBossLevel
	{
		get
		{
			return this.singleBossWaitingBossLevel;
		}
		set
		{
			this.singleBossWaitingBossLevel = value;
		}
	}

	public int SingleBossWaitingBossRank
	{
		get
		{
			if (!DataReader<YeWaiBOSSJieJi>.Contains(this.SingleBossWaitingBossLevel))
			{
				return 0;
			}
			return DataReader<YeWaiBOSSJieJi>.Get(this.SingleBossWaitingBossLevel).ManyRank;
		}
	}

	public int SingleBossWaitingNum
	{
		get
		{
			return this.singleBossWaitingNum;
		}
		protected set
		{
			this.singleBossWaitingNum = value;
		}
	}

	public bool IsMultiBossWaiting
	{
		get
		{
			return this.isMultiBossWaiting;
		}
		protected set
		{
			this.isMultiBossWaiting = value;
		}
	}

	protected int MultiBossWaitingBossID
	{
		get
		{
			return this.multiBossWaitingBossID;
		}
		set
		{
			this.multiBossWaitingBossID = value;
		}
	}

	protected int MultiBossWaitingBossTypeID
	{
		get
		{
			return this.multiBossWaitingBossTypeID;
		}
		set
		{
			this.multiBossWaitingBossTypeID = value;
		}
	}

	public string MultiBossWaitingBossName
	{
		get
		{
			if (!DataReader<Monster>.Contains(this.MultiBossWaitingBossTypeID))
			{
				return string.Empty;
			}
			return GameDataUtils.GetChineseContent(DataReader<Monster>.Get(this.MultiBossWaitingBossTypeID).name, false);
		}
	}

	protected int MultiBossWaitingBossLevel
	{
		get
		{
			return this.multiBossWaitingBossLevel;
		}
		set
		{
			this.multiBossWaitingBossLevel = value;
		}
	}

	public int MultiBossWaitingBossRank
	{
		get
		{
			if (!DataReader<YeWaiBOSSJieJi>.Contains(this.MultiBossWaitingBossLevel))
			{
				return 0;
			}
			return DataReader<YeWaiBOSSJieJi>.Get(this.MultiBossWaitingBossLevel).ManyRank;
		}
	}

	public int MultiBossWaitingNum
	{
		get
		{
			return this.multiBossWaitingNum;
		}
		protected set
		{
			this.multiBossWaitingNum = value;
		}
	}

	protected bool IsWaitingShowQueueDialog
	{
		get
		{
			return this.isWaitingShowQueueDialog;
		}
		set
		{
			this.isWaitingShowQueueDialog = value;
		}
	}

	protected bool IsShowingQueueDialog
	{
		get
		{
			return this.isShowingQueueDialog;
		}
		set
		{
			this.isShowingQueueDialog = value;
		}
	}

	protected long BossCurHp
	{
		get
		{
			return this.bossCurHp;
		}
		set
		{
			this.bossCurHp = value;
		}
	}

	protected long BossHpLmt
	{
		get
		{
			return this.bossHpLmt;
		}
		set
		{
			this.bossHpLmt = value;
		}
	}

	protected WildBossManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.bossData.Clear();
		this.challengeLevelFloor = (int)float.Parse(DataReader<YeWaiBOSS>.Get("player_lv").value);
		this.challengeLevelRange = (int)float.Parse(DataReader<YeWaiBOSS>.Get("match_lv").value);
		InstanceManager.RegisterSecurityCheck(new Func<Action, Action, bool>(this.SingleSecurityCheck));
		InstanceManager.RegisterSecurityCheck(new Func<Action, Action, bool>(this.MultiSecurityCheck));
	}

	public override void Release()
	{
		this.isCurrentSceneUseTimes = true;
		this.bossRemainRewardTimes = 0;
		this.multiBossRemainRewardTimes = 0;
		this.bossData.Clear();
		this.challengeLevelFloor = 0;
		this.challengeLevelRange = 0;
		this.bossEnterID = 0;
		this.bossEnterMonsterDataID = 0;
		this.isSingleBossWaiting = false;
		this.singleBossWaitingBossID = 0;
		this.singleBossWaitingBossTypeID = 0;
		this.singleBossWaitingBossLevel = 0;
		this.isMultiBossWaiting = false;
		this.multiBossWaitingBossID = 0;
		this.multiBossWaitingBossTypeID = 0;
		this.multiBossWaitingBossLevel = 0;
		this.multiBossWaitingNum = -1;
		this.isWaitingShowQueueDialog = false;
		this.isShowingQueueDialog = false;
		this.bossCurHp = 0L;
		this.bossHpLmt = 0L;
		this.wildBossUICurrentDataList.Clear();
		this.wildBossUIWaitingDataList.Clear();
		this.selfWaitingData = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<WildBossRewardInfoNty>(new NetCallBackMethod<WildBossRewardInfoNty>(this.UpdateRemainRewardTimes));
		NetworkManager.AddListenEvent<WildBossInfoNty>(new NetCallBackMethod<WildBossInfoNty>(this.CreateBossNPC));
		NetworkManager.AddListenEvent<WildBossUpdateNty>(new NetCallBackMethod<WildBossUpdateNty>(this.UpdateBossNPC));
		EventDispatcher.AddListener<int, int>(BossNPCBehavior.OnEnterNPC, new Callback<int, int>(this.OnEnterBossNPC));
		EventDispatcher.AddListener<int>(BossNPCBehavior.OnExitNPC, new Callback<int>(this.OnExitBossNPC));
		EventDispatcher.AddListener<int, int>(BossNPCBehavior.OnSeleteNPC, new Callback<int, int>(this.OnSeleteNPC));
		EventDispatcher.AddListener(SceneManagerEvent.ClearSceneDependentLogic, new Callback(this.ClearBossNPC));
		NetworkManager.AddListenEvent<ChallengeWildBossRes>(new NetCallBackMethod<ChallengeWildBossRes>(this.OnChallengeWildBossRes));
		NetworkManager.AddListenEvent<WildBossQueueUpNty>(new NetCallBackMethod<WildBossQueueUpNty>(this.OnWildBossQueueUpNty));
		EventDispatcher.AddListener(WildBossManagerEvent.ShowQueue, new Callback(this.ShowQueueDialog));
		EventDispatcher.AddListener(WildBossManagerEvent.UpdateQueue, new Callback(this.UpdateQueue));
		EventDispatcher.AddListener(WildBossManagerEvent.QuitQueue, new Callback(this.QuitQueue));
		NetworkManager.AddListenEvent<WildBossQueueUpDetailRes>(new NetCallBackMethod<WildBossQueueUpDetailRes>(this.OnWildBossQueueUpDetailRes));
		NetworkManager.AddListenEvent<WildBossCancelQueueUpRes>(new NetCallBackMethod<WildBossCancelQueueUpRes>(this.OnWildBossCancelQueueUpRes));
		NetworkManager.AddListenEvent<ResultWildBossNty>(new NetCallBackMethod<ResultWildBossNty>(this.OnGetChallengeWildBossResult));
		NetworkManager.AddListenEvent<ExitWildBossRes>(new NetCallBackMethod<ExitWildBossRes>(this.OnExitWildBossRes));
		NetworkManager.AddListenEvent<WildBossCityDropNty>(new NetCallBackMethod<WildBossCityDropNty>(this.CreateCityFakeDrop));
		EventDispatcher.AddListener(WildBossManagerEvent.GetCityFakeDrop, new Callback(this.GetCityFakeDrop));
		NetworkManager.AddListenEvent<PickUpWildBossItemRes>(new NetCallBackMethod<PickUpWildBossItemRes>(this.OnGetCityFakeDrop));
	}

	protected void UpdateRemainRewardTimes(short state, WildBossRewardInfoNty down = null)
	{
		Debug.Log(string.Concat(new object[]
		{
			"UpdateRemainRewardTimes: ",
			down.singleRewardTms,
			" ",
			down.teamRewardTms
		}));
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.bossRemainRewardTimes = (int)float.Parse(DataReader<YeWaiBOSS>.Get("limitA").value) - down.singleRewardTms;
		this.multiBossRemainRewardTimes = (int)float.Parse(DataReader<YeWaiBOSS>.Get("limitB").value) - down.teamRewardTms;
	}

	protected void CreateBossNPC(short state, WildBossInfoNty down = null)
	{
		Debug.Log("CreateBossNPC");
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		if (down.bossInfo == null)
		{
			return;
		}
		this.isCurrentSceneUseTimes = down.rewardLmt;
		for (int i = 0; i < down.bossInfo.get_Count(); i++)
		{
			if (!this.bossData.ContainsKey(down.bossInfo.get_Item(i).idx))
			{
				if (down.bossInfo.get_Item(i).status != 3)
				{
					if (down.bossInfo.get_Item(i).bossCode != 0)
					{
						if (DataReader<Monster>.Contains(down.bossInfo.get_Item(i).bossCode))
						{
							Monster monster = DataReader<Monster>.Get(down.bossInfo.get_Item(i).bossCode);
							if (DataReader<AvatarModel>.Contains(monster.model))
							{
								AvatarModel avatarModel = DataReader<AvatarModel>.Get(monster.model);
								if (DataReader<YeWaiBOSSXinXi>.Contains(down.bossInfo.get_Item(i).bossCfgId))
								{
									YeWaiBOSSXinXi yeWaiBOSSXinXi = DataReader<YeWaiBOSSXinXi>.Get(down.bossInfo.get_Item(i).bossCfgId);
									this.bossData.Add(down.bossInfo.get_Item(i).idx, new WildBossData
									{
										isBoss = down.bossInfo.get_Item(i).isGroupBoss,
										lv = down.bossInfo.get_Item(i).bossLv,
										nameID = monster.name,
										iconID = avatarModel.icon,
										positionX = down.bossInfo.get_Item(i).pos.x * 0.01f,
										positionZ = down.bossInfo.get_Item(i).pos.y * 0.01f
									});
									WildBossNPCManager.Instance.CreateNPC(down.bossInfo.get_Item(i).idx, down.bossInfo.get_Item(i).bossCode, down.bossInfo.get_Item(i).isGroupBoss, down.bossInfo.get_Item(i).bossLv, down.bossInfo.get_Item(i).pos, yeWaiBOSSXinXi.face, down.bossInfo.get_Item(i).status);
								}
							}
						}
					}
				}
			}
		}
		EventDispatcher.Broadcast(WildBossManagerEvent.CreateBoss);
	}

	protected void UpdateBossNPC(short state, WildBossUpdateNty down = null)
	{
		Debug.Log("UpdateBossNPC");
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		WildBossNPCManager.Instance.UpdateNPC(down.idx, down.updateValue);
	}

	public void RemoveBossNPC(int id)
	{
		if (!this.bossData.ContainsKey(id))
		{
			return;
		}
		this.CheckHideChallengeUI(id);
		this.bossData.Remove(id);
		WildBossNPCManager.Instance.RemoveNPC(id);
		EventDispatcher.Broadcast(WildBossManagerEvent.RemoveBoss);
	}

	protected void ClearBossNPC()
	{
		this.HideChallengeUI();
		this.bossData.Clear();
		this.BossEnterID = 0;
		this.BossEnterMonsterDataID = 0;
		EventDispatcher.Broadcast(WildBossManagerEvent.ClearBoss);
	}

	protected void OnEnterBossNPC(int id, int monsterDataID)
	{
		if (!this.bossData.ContainsKey(id))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
			return;
		}
		this.ShowChallengeBubble(id, monsterDataID);
	}

	protected void OnExitBossNPC(int id)
	{
		this.CheckHideChallengeUI(id);
	}

	protected void OnSeleteNPC(int id, int monsterDataID)
	{
		this.ShowChallengeUI(id, monsterDataID);
	}

	protected void ShowChallengeBubble(int id, int monsterDataID)
	{
		this.BossEnterID = id;
		this.BossEnterMonsterDataID = monsterDataID;
		TownUI townUI = UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI;
		if (townUI != null)
		{
			townUI.ShowWildBossBubble(true, new Action(this.OnClickChallengeUI));
		}
	}

	public void OnClickChallengeUI()
	{
		this.ShowChallengeUI(this.BossEnterID, this.BossEnterMonsterDataID);
	}

	protected void ShowChallengeUI(int bossID, int bossDataID)
	{
		if (this.bossData.ContainsKey(bossID))
		{
			if (this.bossData[bossID].isBoss)
			{
				if (this.IsMultiBossWaiting && bossID == this.MultiBossWaitingBossID)
				{
					this.ShowQueueDialog();
				}
				else if (this.isCurrentSceneUseTimes && this.multiBossRemainRewardTimes <= 0)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505178, false), 1f, 1f);
				}
				else
				{
					this.TryShowMultiChallengeUI(bossID, bossDataID, this.bossData[bossID].lv);
				}
			}
			else if (this.IsSingleBossWaiting && bossID == this.SingleBossWaitingBossID)
			{
				this.ShowQueueDialog();
			}
			else if (this.isCurrentSceneUseTimes && this.bossRemainRewardTimes <= 0)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505178, false), 1f, 1f);
			}
			else
			{
				this.TryShowSingleChallengeUI(bossID, bossDataID, this.bossData[bossID].lv);
			}
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
		}
	}

	protected void CheckTeamLeaderChallenge(int id, int monsterDataID, Action<int, int, Action<int>> showUIAction)
	{
		int teamLowestLv = TeamBasicManager.Instance.GetTeamLowestLv();
		int teamHighestLv = TeamBasicManager.Instance.GetTeamHighestLv();
		if (teamLowestLv < this.challengeLevelFloor)
		{
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(505169, false), this.challengeLevelFloor), 1f, 1f);
		}
		else if (this.bossData[id].lv - teamLowestLv > this.challengeLevelRange || teamHighestLv - this.bossData[id].lv > this.challengeLevelRange)
		{
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(505166, false), this.challengeLevelRange), 1f, 1f);
		}
		else if (showUIAction != null)
		{
			showUIAction.Invoke(id, monsterDataID, new Action<int>(this.CheckMultiChallengeBossLegality));
		}
	}

	protected void CheckTeammateChallenge()
	{
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516124, false), 1f, 1f);
	}

	protected void CheckSolitaryChallenge(int id, int monsterDataID, int level, Action<int, int, int, Action<int>> showUIAction, Action<int> checkLegalAction)
	{
		if (EntityWorld.Instance.EntSelf.Lv < this.challengeLevelFloor)
		{
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(505167, false), this.challengeLevelFloor), 1f, 1f);
		}
		else if (Mathf.Abs(level - EntityWorld.Instance.EntSelf.Lv) > this.challengeLevelRange)
		{
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(505168, false), this.challengeLevelRange), 1f, 1f);
		}
		else if (showUIAction != null)
		{
			showUIAction.Invoke(id, monsterDataID, level, checkLegalAction);
		}
	}

	protected void TryShowMultiChallengeUI(int id, int monsterDataID, int level)
	{
		if (this.bossData.ContainsKey(id))
		{
			this.CheckSolitaryChallenge(id, monsterDataID, level, new Action<int, int, int, Action<int>>(this.ShowMultiChallengeUI), new Action<int>(this.CheckMultiChallengeBossLegality));
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
		}
	}

	protected void ShowMultiChallengeUI(int id, int monsterDataID, int level, Action<int> callback)
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(505158, false), string.Format(GameDataUtils.GetChineseContent(505170, false), DataReader<YeWaiBOSSJieJi>.Get(level).ManyRank, GameDataUtils.GetChineseContent(DataReader<Monster>.Get(monsterDataID).name, false)), delegate
		{
		}, delegate
		{
			if (callback != null)
			{
				callback.Invoke(id);
			}
		}, GameDataUtils.GetChineseContent(621272, false), GameDataUtils.GetChineseContent(621271, false), "button_orange_1", "button_yellow_1", null, true, true);
		DialogBoxUIView.Instance.isClick = false;
	}

	protected void CheckMultiChallengeBossLegality(int id)
	{
		if (this.bossData.ContainsKey(id))
		{
			switch (WildBossNPCManager.Instance.GetNPCState(id))
			{
			case 1:
				this.ChallengeMultiWildBoss(id);
				break;
			case 2:
				this.ChallengeMultiWildBoss(id);
				break;
			case 3:
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
				break;
			default:
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
				break;
			}
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
		}
	}

	protected void ChallengeMultiWildBoss(int id)
	{
		InstanceManager.SecurityCheck(delegate
		{
			this.IsWaitingShowQueueDialog = true;
			WaitUI.OpenUI(3000u);
			this.ChallengeWildBoss(id);
		}, null);
	}

	protected bool MultiSecurityCheck(Action passAction, Action blockAndCancelAction)
	{
		if (!this.IsMultiBossWaiting)
		{
			return true;
		}
		if (this.MultiBossWaitingNum < 0)
		{
			return true;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(505158, false), GameDataUtils.GetChineseContent(505174, false), delegate
		{
			if (blockAndCancelAction != null)
			{
				blockAndCancelAction.Invoke();
			}
		}, delegate
		{
			this.CancelQueueUpReq();
		}, GameDataUtils.GetChineseContent(1413, false), GameDataUtils.GetChineseContent(505177, false), "button_orange_1", "button_yellow_1", null, true, true);
		DialogBoxUIView.Instance.isClick = false;
		return false;
	}

	protected void TryShowSingleChallengeUI(int id, int monsterDataID, int level)
	{
		if (this.bossData.ContainsKey(id))
		{
			this.CheckSolitaryChallenge(id, monsterDataID, level, new Action<int, int, int, Action<int>>(this.ShowSingleChallengeUI), new Action<int>(this.CheckSingleChallengeBossLegality));
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
		}
	}

	protected void ShowSingleChallengeUI(int id, int monsterDataID, int level, Action<int> callback)
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(505190, false), string.Format(GameDataUtils.GetChineseContent(505159, false), GameDataUtils.GetChineseContent(DataReader<Monster>.Get(monsterDataID).name, false)), delegate
		{
		}, delegate
		{
			if (callback != null)
			{
				callback.Invoke(id);
			}
		}, GameDataUtils.GetChineseContent(621272, false), GameDataUtils.GetChineseContent(621271, false), "button_orange_1", "button_yellow_1", null, true, true);
		DialogBoxUIView.Instance.isClick = false;
	}

	protected void CheckSingleChallengeBossLegality(int id)
	{
		if (this.bossData.ContainsKey(id))
		{
			switch (WildBossNPCManager.Instance.GetNPCState(id))
			{
			case 1:
				this.ChallengeSingleWildBoss(id);
				break;
			case 2:
				this.ChallengeSingleWildBoss(id);
				break;
			case 3:
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
				break;
			default:
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
				break;
			}
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505161, false), 1f, 1f);
		}
	}

	protected void ChallengeSingleWildBoss(int id)
	{
		InstanceManager.SecurityCheck(delegate
		{
			this.IsWaitingShowQueueDialog = true;
			WaitUI.OpenUI(3000u);
			this.ChallengeWildBoss(id);
		}, null);
	}

	protected bool SingleSecurityCheck(Action passAction, Action blockAndCancelAction)
	{
		if (!this.IsSingleBossWaiting)
		{
			return true;
		}
		if (this.SingleBossWaitingNum < 0)
		{
			return true;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(505190, false), GameDataUtils.GetChineseContent(505174, false), delegate
		{
			if (blockAndCancelAction != null)
			{
				blockAndCancelAction.Invoke();
			}
		}, delegate
		{
			this.CancelQueueUpReq();
		}, GameDataUtils.GetChineseContent(1413, false), GameDataUtils.GetChineseContent(505177, false), "button_orange_1", "button_yellow_1", null, true, true);
		DialogBoxUIView.Instance.isClick = false;
		return false;
	}

	protected void OnWildBossQueueUpNty(short state, WildBossQueueUpNty down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		Debug.LogError("down.queueCount: " + down.queueCount);
		if (down.teamBoss)
		{
			this.IsMultiBossWaiting = true;
			this.MultiBossWaitingNum = down.queueCount;
			this.MultiBossWaitingBossID = down.idx;
			this.MultiBossWaitingBossTypeID = down.bossCode;
			this.MultiBossWaitingBossLevel = down.bossLv;
			this.IsSingleBossWaiting = false;
			this.SingleBossWaitingNum = -1;
		}
		else
		{
			this.IsSingleBossWaiting = true;
			this.SingleBossWaitingNum = down.queueCount;
			this.SingleBossWaitingBossID = down.idx;
			this.SingleBossWaitingBossTypeID = down.bossCode;
			this.SingleBossWaitingBossLevel = down.bossLv;
			this.IsMultiBossWaiting = false;
			this.MultiBossWaitingNum = -1;
		}
		if (down.detail != null)
		{
			this.BossCurHp = down.detail.bossHp;
			this.BossHpLmt = down.detail.bossHpLmt;
			this.wildBossUICurrentDataList.Clear();
			for (int i = 0; i < down.detail.challengingRoles.get_Count(); i++)
			{
				WildBossUICurrentData wildBossUICurrentData = new WildBossUICurrentData();
				wildBossUICurrentData.name = down.detail.challengingRoles.get_Item(i).name;
				wildBossUICurrentData.career = down.detail.challengingRoles.get_Item(i).career;
				wildBossUICurrentData.curHp = down.detail.challengingRoles.get_Item(i).hp;
				wildBossUICurrentData.hpLmt = down.detail.challengingRoles.get_Item(i).hpLmt;
				this.wildBossUICurrentDataList.Add(wildBossUICurrentData);
			}
			this.wildBossUIWaitingDataList.Clear();
			this.selfWaitingData = null;
			for (int j = 0; j < down.detail.queueRoles.get_Count(); j++)
			{
				WildBossUIWaitingData wildBossUIWaitingData = new WildBossUIWaitingData();
				wildBossUIWaitingData.rank = j + 1;
				wildBossUIWaitingData.name = down.detail.queueRoles.get_Item(j).name;
				wildBossUIWaitingData.career = down.detail.queueRoles.get_Item(j).career;
				wildBossUIWaitingData.fighting = down.detail.queueRoles.get_Item(j).fighting;
				this.wildBossUIWaitingDataList.Add(wildBossUIWaitingData);
				if (EntityWorld.Instance.EntSelf != null && down.detail.queueRoles.get_Item(j).roleId == EntityWorld.Instance.EntSelf.ID)
				{
					this.selfWaitingData = wildBossUIWaitingData;
				}
			}
		}
		this.TryUpdateQueueUI();
	}

	protected void TryUpdateQueueUI()
	{
		if (this.IsWaitingShowQueueDialog)
		{
			this.IsWaitingShowQueueDialog = false;
			this.ShowQueueDialog();
		}
		if (this.IsSingleBossWaiting)
		{
			if (this.IsShowingQueueDialog)
			{
				if (this.SingleBossWaitingNum < 0)
				{
					this.CloseQueueDialog();
				}
				else
				{
					this.UpdateQueueDialog();
				}
			}
			int num = this.SingleBossWaitingNum;
			if (num == -2)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505176, false), 1f, 1f);
			}
			EventDispatcher.Broadcast<int, string, int>(WildBossManagerEvent.OnSingleWaitingNumChanged, this.SingleBossWaitingBossRank, this.SingleBossWaitingBossName, this.SingleBossWaitingNum);
		}
		else if (this.IsMultiBossWaiting)
		{
			if (this.IsShowingQueueDialog)
			{
				if (this.MultiBossWaitingNum < 0)
				{
					this.CloseQueueDialog();
				}
				else
				{
					this.UpdateQueueDialog();
				}
			}
			int num = this.MultiBossWaitingNum;
			if (num == -2)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505176, false), 1f, 1f);
			}
			EventDispatcher.Broadcast<int, string, int>(WildBossManagerEvent.OnMultiWaitingNumChanged, this.MultiBossWaitingBossRank, this.MultiBossWaitingBossName, this.MultiBossWaitingNum);
		}
	}

	protected void ShowQueueDialog()
	{
		this.IsShowingQueueDialog = true;
		this.UpdateQueueDialog();
	}

	protected void UpdateQueue()
	{
		this.QueueUpDetailReq();
	}

	protected void QuitQueue()
	{
		this.CloseQueueDialog();
		this.CancelQueueUpReq();
	}

	protected void UpdateQueueDialog()
	{
		if (this.IsSingleBossWaiting)
		{
			if (DataReader<Monster>.Contains(this.SingleBossWaitingBossTypeID))
			{
				Monster monster = DataReader<Monster>.Get(this.SingleBossWaitingBossTypeID);
				WildBossWaitingUI wildBossWaitingUI = UIManagerControl.Instance.OpenUI("WildBossWaitingUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as WildBossWaitingUI;
				wildBossWaitingUI.SetData(monster.icon2, GameDataUtils.GetChineseContent(monster.name, false), this.SingleBossWaitingBossRank, (this.BossHpLmt != 0L) ? ((float)((double)this.BossCurHp / (double)this.BossHpLmt)) : 1f, this.wildBossUICurrentDataList, this.wildBossUIWaitingDataList, this.selfWaitingData);
			}
		}
		else if (this.IsMultiBossWaiting && DataReader<Monster>.Contains(this.MultiBossWaitingBossTypeID))
		{
			Monster monster2 = DataReader<Monster>.Get(this.MultiBossWaitingBossTypeID);
			WildBossWaitingUI wildBossWaitingUI2 = UIManagerControl.Instance.OpenUI("WildBossWaitingUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as WildBossWaitingUI;
			wildBossWaitingUI2.SetData(monster2.icon2, GameDataUtils.GetChineseContent(monster2.name, false), this.MultiBossWaitingBossRank, (this.BossHpLmt != 0L) ? ((float)((double)this.BossCurHp / (double)this.BossHpLmt)) : 1f, this.wildBossUICurrentDataList, this.wildBossUIWaitingDataList, this.selfWaitingData);
		}
	}

	protected void CloseQueueDialog()
	{
		this.IsShowingQueueDialog = false;
		UIManagerControl.Instance.HideUI("WildBossWaitingUI");
	}

	protected void QueueUpDetailReq()
	{
		WaitUI.OpenUI(3000u);
		if (this.IsSingleBossWaiting)
		{
			NetworkManager.Send(new WildBossQueueUpDetailReq(), ServerType.Data);
		}
		else if (this.IsMultiBossWaiting)
		{
			NetworkManager.Send(new WildBossQueueUpDetailReq(), ServerType.Data);
		}
	}

	protected void OnWildBossQueueUpDetailRes(short state, WildBossQueueUpDetailRes down = null)
	{
		if (state != 0)
		{
			WaitUI.CloseUI(0u);
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	protected void CancelQueueUpReq()
	{
		WaitUI.OpenUI(3000u);
		if (this.IsSingleBossWaiting)
		{
			NetworkManager.Send(new WildBossCancelQueueUpReq(), ServerType.Data);
		}
		else if (this.IsMultiBossWaiting)
		{
			NetworkManager.Send(new WildBossCancelQueueUpReq(), ServerType.Data);
		}
	}

	protected void OnWildBossCancelQueueUpRes(short state, WildBossCancelQueueUpRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		Debug.LogError("OnWildBossCancelQueueUpRes");
		if (down.teamBoss)
		{
			this.IsMultiBossWaiting = false;
			this.MultiBossWaitingNum = -1;
		}
		else
		{
			this.IsSingleBossWaiting = false;
			this.SingleBossWaitingNum = -1;
		}
		if (InstanceManager.IsInSecurityCheck)
		{
			InstanceManager.ContinueSecurityCheck();
		}
	}

	protected void CheckHideChallengeUI(int id)
	{
		if (id != this.BossEnterID)
		{
			return;
		}
		this.HideChallengeUI();
	}

	protected void HideChallengeUI()
	{
		this.BossEnterID = 0;
		this.BossEnterMonsterDataID = 0;
		TownUI townUI = UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI;
		if (townUI != null)
		{
			townUI.ShowWildBossBubble(false, null);
		}
	}

	protected void CreateCityFakeDrop(short state, WildBossCityDropNty down = null)
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
		Vector3 vector = PosDirUtility.ToTerrainPoint(down.pos, 0f);
		List<Vector3> list = this.SetCityFakeDropWaitingPosition(down.bossCode, vector);
		for (int i = 0; i < down.dropCount; i++)
		{
			if (list.get_Count() == 0)
			{
				CityFakeDrop.CreateCityFakeDrop(WildBossManager.CityFakeDropModel, vector, vector, 0);
			}
			else
			{
				CityFakeDrop.CreateCityFakeDrop(WildBossManager.CityFakeDropModel, vector, list.get_Item(i % list.get_Count()), 0);
			}
		}
	}

	protected List<Vector3> SetCityFakeDropWaitingPosition(int typeID, Vector3 originPos)
	{
		List<Vector3> list = new List<Vector3>();
		Monster monster = DataReader<Monster>.Get(typeID);
		if (monster == null)
		{
			return list;
		}
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(monster.model);
		if (monster == null)
		{
			return list;
		}
		if (avatarModel.flopAngle == 0)
		{
			return list;
		}
		int num = Random.Range(0, 360);
		for (float num2 = 0f; num2 < 360f; num2 += (float)avatarModel.flopAngle)
		{
			float num3 = (float)avatarModel.flopRange * 0.01f * 0.5f + Random.get_value() * (float)avatarModel.flopRange * 0.01f * 0.5f;
			Vector3 vector = originPos + Quaternion.Euler(0f, (float)num + num2, 0f) * Vector3.get_forward() * num3;
			list.Add(vector);
		}
		return list;
	}

	public void GetCityFakeDrop()
	{
		NetworkManager.Send(new PickUpWildBossItemReq(), ServerType.Data);
	}

	protected void OnGetCityFakeDrop(short state, PickUpWildBossItemRes down = null)
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
		for (int i = 0; i < down.item.get_Count(); i++)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetItemName(down.item.get_Item(i).typeId, true, down.item.get_Item(i).count), 1f, 1f);
		}
	}

	protected void ChallengeWildBoss(int monsterID)
	{
		NetworkManager.Send(new ChallengeWildBossReq
		{
			idx = monsterID
		}, ServerType.Data);
	}

	protected void OnChallengeWildBossRes(short state, ChallengeWildBossRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		InstanceManager.ChangeInstanceManager(down.dungeonId, false);
	}

	public void InitiativeQuit()
	{
		this.ExitWildBoss();
	}

	protected void OnGetChallengeWildBossResult(short state, ResultWildBossNty down = null)
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
		if (InstanceManager.CurrentInstanceType == InstanceType.WildBossSingle)
		{
			WildBossSingleInstance.Instance.GetInstanceResult(down);
		}
		else if (InstanceManager.CurrentInstanceType == InstanceType.WildBossMulti)
		{
			WildBossMultiInstance.Instance.GetInstanceResult(down);
		}
	}

	public void ExitWildBoss()
	{
		NetworkManager.Send(new ExitWildBossReq(), ServerType.Data);
	}

	protected void OnExitWildBossRes(short state, ExitWildBossRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
	}
}
