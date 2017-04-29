using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNetwork;

public class DungeonManager : BaseSubSystemManager
{
	public enum InsType
	{
		NONE,
		MAIN,
		WEAPON,
		FIELD
	}

	public const int instancesPerChapter = 9;

	public Dictionary<int, DungeonInfo> dicDungeonInfo = new Dictionary<int, DungeonInfo>();

	public int chapterStart = 1;

	public bool ShouldNotSendChallenge;

	public int usedFreeMopUpTimes;

	public int BattleSeconds;

	public int TaskEffectInstanceID;

	public int TaskEffectFxID;

	protected Action m_buyChallengeTimesSuccessCallBack;

	protected Action m_buySuccessBuyMopUpTimeCallBack;

	public List<ChallengeResult> listMopUpReslut = new List<ChallengeResult>();

	public List<ChapterResume> listChapterResume = new List<ChapterResume>();

	private bool isRequestingChapters;

	private Action m_actionGetDungeonDataCallback;

	public bool shouldNotShowLoseUI;

	protected ChallengeResult challengeData;

	protected int instanceDataID;

	public DungeonManager.InsType DungeonInstanceType;

	public List<int> DungeonTarget = new List<int>();

	private List<ChapterInfo> normalData = new List<ChapterInfo>();

	private List<ChapterInfo> eliteData = new List<ChapterInfo>();

	private List<ChapterInfo> teamData = new List<ChapterInfo>();

	private int normalOpen;

	private int eliteOpen;

	private int teamOpen;

	private static DungeonManager instance;

	public int WeaponModelId
	{
		get;
		set;
	}

	public ChallengeResult ChallengeData
	{
		get
		{
			return this.challengeData;
		}
		set
		{
			this.challengeData = value;
		}
	}

	public int InstanceDataID
	{
		get
		{
			return this.instanceDataID;
		}
		set
		{
			this.instanceDataID = value;
			InstanceManager.ChangeInstanceManager(value, false);
		}
	}

	public List<ChapterInfo> NormalData
	{
		get
		{
			return this.normalData;
		}
	}

	public List<ChapterInfo> EliteData
	{
		get
		{
			return this.eliteData;
		}
	}

	public List<ChapterInfo> TeamData
	{
		get
		{
			return this.teamData;
		}
	}

	public int NormalOpen
	{
		get
		{
			return this.normalOpen;
		}
	}

	public int EliteOpen
	{
		get
		{
			return this.eliteOpen;
		}
	}

	public int TeamOpen
	{
		get
		{
			return this.teamOpen;
		}
	}

	public static DungeonManager Instance
	{
		get
		{
			if (DungeonManager.instance == null)
			{
				DungeonManager.instance = new DungeonManager();
			}
			return DungeonManager.instance;
		}
	}

	private DungeonManager()
	{
	}

	public List<ChapterInfo> GetDataByInstanceType(int chapterType)
	{
		List<ChapterInfo> result = null;
		if (chapterType == 101)
		{
			result = this.NormalData;
		}
		else if (chapterType == 102)
		{
			result = this.EliteData;
		}
		else if (chapterType == 103)
		{
			result = this.TeamData;
		}
		return result;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.NormalData.Clear();
		this.EliteData.Clear();
		this.TeamData.Clear();
		this.dicDungeonInfo.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<DungeonDataPush>(new NetCallBackMethod<DungeonDataPush>(this.OnGetDungeonDataPush));
		NetworkManager.AddListenEvent<DungeonChangedNty>(new NetCallBackMethod<DungeonChangedNty>(this.OnGetDungeonChangeData));
		NetworkManager.AddListenEvent<GetDungeonDataRes>(new NetCallBackMethod<GetDungeonDataRes>(this.OnGetGetDungeonDataRes));
		NetworkManager.AddListenEvent<ChallengeDungeonRes>(new NetCallBackMethod<ChallengeDungeonRes>(this.OnChallengeDungeonRes));
		NetworkManager.AddListenEvent<ExitChallengeRes>(new NetCallBackMethod<ExitChallengeRes>(this.OnExitChallengeRes));
		NetworkManager.AddListenEvent<RequestClientExitChallengeNty>(new NetCallBackMethod<RequestClientExitChallengeNty>(this.OnRequestClientExitChallengeNty));
		NetworkManager.AddListenEvent<DungeonMiscChangedNty>(new NetCallBackMethod<DungeonMiscChangedNty>(this.OnGetDungeonMiscChangedNty));
		NetworkManager.AddListenEvent<MopUpDungeonRes>(new NetCallBackMethod<MopUpDungeonRes>(this.OnMopUpDungeonRes));
		NetworkManager.AddListenEvent<BuyMopUpTimesRes>(new NetCallBackMethod<BuyMopUpTimesRes>(this.OnGetBuyMopUpTimeRes));
		NetworkManager.AddListenEvent<ResetChallengeTimesRes>(new NetCallBackMethod<ResetChallengeTimesRes>(this.OnGetResetChallengeTimesRes));
		NetworkManager.AddListenEvent<SettleDungeonRes>(new NetCallBackMethod<SettleDungeonRes>(this.OnGetSettleDungeonRes));
		NetworkManager.AddListenEvent<ChallengeResult>(new NetCallBackMethod<ChallengeResult>(this.OnGetChallengeResult));
		NetworkManager.AddListenEvent<ReChallengeRes>(new NetCallBackMethod<ReChallengeRes>(this.OnReChallengeRes));
		NetworkManager.AddListenEvent<AllDungeonChallengeTimesResetNty>(new NetCallBackMethod<AllDungeonChallengeTimesResetNty>(this.OnGetAllDungeonChallengeTimesResetNty));
		EventDispatcher.AddListener(ExperienceInstanceManagerEvent.ExperienceInstanceEnd, new Callback(this.ExperienceInstanceEnd));
		EventDispatcher.AddListener<bool>(LocalInstanceEvent.LocalInstanceFinish, new Callback<bool>(this.NotifyLocalDungeonInstanceFinish));
	}

	private void NotifyLocalDungeonInstanceFinish(bool isWin)
	{
		if (InstanceManager.CurrentInstanceType != DungeonNormalInstance.Instance.Type)
		{
			return;
		}
		DungeonManager.Instance.SendSettleDungeonReq(BattleBlackboard.Instance.SelfDead, !BattleBlackboard.Instance.IsAllMateAlive, BattleBlackboard.Instance.BossDead, BattleBlackboard.Instance.IsAllPetAlive, (int)BattleBlackboard.Instance.SelfLowestHPPercentage, (int)BattleBlackboard.Instance.PetLowestHPPercentage, (int)BattleBlackboard.Instance.SelfCurrentHPPercentage, (int)BattleBlackboard.Instance.BossLifeTime, BattleBlackboard.Instance.AllAttendPet, BattleBlackboard.Instance.AllFusePet, BattleBlackboard.Instance.IsAllNPCDead, BattleBlackboard.Instance.IsAnyNPCDead, BattleBlackboard.Instance.IsAllNPCArrived, BattleBlackboard.Instance.IsWinEnd, BattleBlackboard.Instance.FinishTime, BattleBlackboard.Instance.DeadMonserCount);
	}

	private void OnGetDungeonDataPush(short state, DungeonDataPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.usedFreeMopUpTimes != 0)
			{
				this.usedFreeMopUpTimes = down.usedFreeMopUpTimes;
			}
			this.listChapterResume.Clear();
			this.listChapterResume.AddRange(down.chapterInfos);
			this.RequestDungeonDataLogin();
		}
	}

	private void OnGetDungeonChangeData(short state, DungeonChangedNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			DungeonType.ENUM dungeonType = down.dungeonType;
			switch (dungeonType)
			{
			case DungeonType.ENUM.Normal:
				this.UpdateData(down, this.normalData);
				this.SetPross(DungeonType.ENUM.Normal);
				EventDispatcher.Broadcast(DungeonManagerEvent.InstanceDataHaveChange);
				break;
			case DungeonType.ENUM.Elite:
				this.UpdateData(down, this.eliteData);
				this.SetPross(DungeonType.ENUM.Elite);
				EventDispatcher.Broadcast(DungeonManagerEvent.InstanceDataHaveChange);
				break;
			case DungeonType.ENUM.Team:
				break;
			case DungeonType.ENUM.Arena:
				break;
			default:
				if (dungeonType != DungeonType.ENUM.Society)
				{
				}
				break;
			}
		}
	}

	private void RequestDungeonDataLogin()
	{
		if (this.NormalData.get_Count() > 0)
		{
			return;
		}
		if (this.EliteData.get_Count() > 0)
		{
			return;
		}
		if (this.TeamData.get_Count() > 0)
		{
			return;
		}
		for (int i = 0; i < this.listChapterResume.get_Count(); i++)
		{
			ChapterResume chapterResume = this.listChapterResume.get_Item(i);
			this.SendGetDungeonDataReq(chapterResume.chapterId, chapterResume.dungeonType, null);
		}
	}

	private void SetupDicDungeonInfo(List<ChapterInfo> chapter)
	{
		for (int i = 0; i < chapter.get_Count(); i++)
		{
			ChapterInfo chapterInfo = chapter.get_Item(i);
			for (int j = 0; j < chapterInfo.dungeons.get_Count(); j++)
			{
				DungeonInfo dungeonInfo = chapterInfo.dungeons.get_Item(j);
				if (!this.dicDungeonInfo.ContainsKey(dungeonInfo.dungeonId))
				{
					this.dicDungeonInfo.Add(dungeonInfo.dungeonId, dungeonInfo);
				}
			}
		}
	}

	public void SetPross(DungeonType.ENUM dungeonType)
	{
		if (dungeonType == DungeonType.ENUM.Normal)
		{
			for (int i = 0; i < this.normalData.get_Count(); i++)
			{
				DungeonInfo dungeonInfo = Enumerable.FirstOrDefault<DungeonInfo>(this.normalData.get_Item(i).dungeons, (DungeonInfo k) => !k.clearance);
				if (dungeonInfo != null)
				{
					this.normalOpen = dungeonInfo.dungeonId;
					return;
				}
			}
		}
		else if (dungeonType == DungeonType.ENUM.Elite)
		{
			for (int j = 0; j < this.eliteData.get_Count(); j++)
			{
				DungeonInfo dungeonInfo2 = Enumerable.FirstOrDefault<DungeonInfo>(this.eliteData.get_Item(j).dungeons, (DungeonInfo k) => !k.clearance);
				if (dungeonInfo2 != null)
				{
					this.eliteOpen = dungeonInfo2.dungeonId;
					return;
				}
			}
		}
		else if (dungeonType == DungeonType.ENUM.Team)
		{
			for (int l = 0; l < this.teamData.get_Count(); l++)
			{
				DungeonInfo dungeonInfo3 = Enumerable.FirstOrDefault<DungeonInfo>(this.teamData.get_Item(l).dungeons, (DungeonInfo k) => !k.clearance);
				if (dungeonInfo3 != null)
				{
					this.teamOpen = dungeonInfo3.dungeonId;
					return;
				}
			}
		}
	}

	private void UpdateData(DungeonChangedNty downf, List<ChapterInfo> data)
	{
		ChapterInfo chapterInfo = data.Find((ChapterInfo e) => e.chapterId == downf.chapterId);
		if (chapterInfo != null)
		{
			chapterInfo.totalStar = downf.chapterTotalStar;
			DungeonInfo dungeonInfo = chapterInfo.dungeons.Find((DungeonInfo e) => e.dungeonId == downf.dungeon.dungeonId);
			if (dungeonInfo != null)
			{
				dungeonInfo.clearance = downf.dungeon.clearance;
				dungeonInfo.remainingChallengeTimes = downf.dungeon.remainingChallengeTimes;
				dungeonInfo.star = downf.dungeon.star;
				dungeonInfo.resetChallengeTimes = downf.dungeon.resetChallengeTimes;
				Debug.Log(string.Concat(new object[]
				{
					"dungeon.clearance  ",
					dungeonInfo.clearance,
					"  dungeon.remainingChallengeTimes  ",
					dungeonInfo.remainingChallengeTimes,
					"  dungeon.star  ",
					dungeonInfo.star,
					"  dungeon.resetChallengeTimes  ",
					dungeonInfo.resetChallengeTimes
				}));
				this.dicDungeonInfo.set_Item(downf.dungeon.dungeonId, dungeonInfo);
			}
			else
			{
				DungeonInfo dungeonInfo2 = new DungeonInfo();
				dungeonInfo2.clearance = downf.dungeon.clearance;
				dungeonInfo2.remainingChallengeTimes = downf.dungeon.remainingChallengeTimes;
				dungeonInfo2.star = downf.dungeon.star;
				dungeonInfo2.resetChallengeTimes = downf.dungeon.resetChallengeTimes;
				chapterInfo.dungeons.Add(dungeonInfo2);
				this.dicDungeonInfo.Add(downf.dungeon.dungeonId, dungeonInfo2);
			}
			return;
		}
	}

	public DungeonInfo GetDungeonInfo(int dungeonID)
	{
		if (this.dicDungeonInfo.ContainsKey(dungeonID))
		{
			return this.dicDungeonInfo.get_Item(dungeonID);
		}
		return null;
	}

	public void SendGetDungeonDataReq(int chapterId, DungeonType.ENUM type, Action actionGetDungeonDataCallback)
	{
		if (this.isRequestingChapters)
		{
			return;
		}
		this.m_actionGetDungeonDataCallback = actionGetDungeonDataCallback;
		this.isRequestingChapters = true;
		ChapterResume chapterResume = new ChapterResume();
		chapterResume.chapterId = chapterId;
		chapterResume.dungeonType = type;
		NetworkManager.Send(new GetDungeonDataReq
		{
			chaterInfo = chapterResume
		}, ServerType.Data);
	}

	public void SendReChallengeReq()
	{
		NetworkManager.Send(new ReChallengeReq(), ServerType.Data);
	}

	public void SendChallengeDungeonReq(int dungeonId)
	{
		NetworkManager.Send(new ChallengeDungeonReq
		{
			dungeonId = dungeonId
		}, ServerType.Data);
	}

	public void SendSettleDungeonReq(bool selfDeadParm, bool friendDeadParm, bool targetDeadParm, bool allPetNotDeadParm, int minimumHpPcntParm, int minimumHpPcntPetParm, int remainingHpPcntParm, int killTargetUsedTimeParm, List<int> summonedPetTypeIdsParm, List<int> fittedPetTypeIdsParm, bool allNpcDead, bool anyNpcDead, bool npcArrivedDst, bool isWin, int time, Dictionary<int, int> deadMonsterCount)
	{
		if (InstanceManager.CurrentInstance.InstanceData.serverBorn != 1)
		{
			BattleDmgCollectManager.Instance.SetDmgCalByChallengeResult(BattleDmgCollectManager.Instance.localSoldierSettleInfos);
		}
		Debug.Log("发送挑战结果");
		SettleDungeonReq settleDungeonReq = new SettleDungeonReq();
		settleDungeonReq.selfDead = selfDeadParm;
		settleDungeonReq.friendDead = friendDeadParm;
		settleDungeonReq.targetDead = targetDeadParm;
		settleDungeonReq.allPetNotDead = allPetNotDeadParm;
		settleDungeonReq.minimumHpPcnt = minimumHpPcntParm;
		settleDungeonReq.minimumHpPcntPet = minimumHpPcntPetParm;
		settleDungeonReq.remainingHpPcnt = remainingHpPcntParm;
		settleDungeonReq.killTargetUsedTime = killTargetUsedTimeParm;
		settleDungeonReq.summonedPetTypeIds.AddRange(summonedPetTypeIdsParm);
		settleDungeonReq.fittedPetTypeIds.AddRange(fittedPetTypeIdsParm);
		settleDungeonReq.npcDead = allNpcDead;
		settleDungeonReq.anyNpcDead = anyNpcDead;
		settleDungeonReq.npcArrivedDst = npcArrivedDst;
		settleDungeonReq.isWin = isWin;
		settleDungeonReq.elapseSec = time;
		settleDungeonReq.tick = TimeManager.Instance.ScaleServerSecond;
		using (Dictionary<int, int>.Enumerator enumerator = deadMonsterCount.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				settleDungeonReq.deadMonster.Add(new SettleDungeonReq.DeadMonster
				{
					monsterId = current.get_Key(),
					monsterCount = current.get_Value()
				});
			}
		}
		ClientGMManager.Instance.SendSettleReq.Add(new KeyValuePair<int, DateTime>(InstanceManager.CurrentInstanceDataID, DateTime.get_Now()));
		NetworkManager.VitalSend(settleDungeonReq, ServerType.Data);
	}

	public void SendExitDungeonReq()
	{
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		if (MultiPlayerInstance.Instance.isMulti)
		{
			MultiPlayerManager.Instance.SendPveExitBattleReq();
		}
		else if (this.DungeonInstanceType == DungeonManager.InsType.FIELD)
		{
			NetworkManager.Send(new ExitChallengeReq
			{
				pos = Pos.FromScenePos(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position())
			}, ServerType.Data);
		}
		else
		{
			NetworkManager.Send(new ExitChallengeReq(), ServerType.Data);
		}
		Debug.Log("退出挑战请求" + MultiPlayerInstance.Instance.isMulti);
	}

	private void OnReChallengeRes(short state, ReChallengeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		LocalBattleHandler.Instance.ResetData();
		LocalInstanceHandler.Instance.ResetData();
		BattleBlackboard.Instance.ResetData();
		GlobalBattleNetwork.Instance.ResetData();
		BattleDmgCollectManager.Instance.ClearData();
		EntityWorld.Instance.ClearAllMapObjects();
		InstanceManager.SimulateEnterField(InstanceManager.CurrentInstanceData.type, null);
		InstanceManager.SimulateSwicthMap(InstanceManager.CurrentInstanceData.scene, LocalInstanceHandler.Instance.CreateSelfInfo(InstanceManager.CurrentInstanceData.type, InstanceManager.CurrentInstanceDataID, InstanceManager.CurrentInstanceData.scene, 0, 0, null, null, null), null, 0);
		WaitUI.CloseUI(0u);
	}

	private void OnGetSettleDungeonRes(short state, SettleDungeonRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			this.SendExitDungeonReq();
			return;
		}
		if (InstanceManager.CurrentInstance.InstanceData.serverBorn == 1)
		{
			BattleDmgCollectManager.Instance.SetDmgCalByChallengeResult(down.result.settleInfos);
		}
		this.ChallengeData = down.result;
		if (InstanceManager.CurrentInstanceType == DungeonNormalInstance.Instance.Type)
		{
			DungeonNormalInstance.Instance.GetInstanceResult(this.ChallengeData);
		}
		EventDispatcher.Broadcast<bool>(EventNames.DungeonResultNty, this.ChallengeData.winnerId == EntityWorld.Instance.EntSelf.ID);
		Debug.Log("任务副本结算推送");
		if (this.ChallengeData.winnerId == EntityWorld.Instance.EntSelf.ID && MainTaskManager.Instance.IsGoingTask(InstanceManager.CurrentInstanceDataID))
		{
			FXManager.Instance.DeleteFX(DungeonManager.Instance.TaskEffectFxID);
			DungeonManager.Instance.TaskEffectInstanceID = 0;
		}
	}

	private void OnGetChallengeResult(short state, ChallengeResult down = null)
	{
		if (state == 0)
		{
			Debug.Log("OnGetChallengeResult-------------  down.settleInfos.Count  " + down.settleInfos.get_Count());
			if (InstanceManager.CurrentInstance.InstanceData.serverBorn == 1)
			{
				BattleDmgCollectManager.Instance.SetDmgCalByChallengeResult(down.settleInfos);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetGetDungeonDataRes(short state, GetDungeonDataRes down = null)
	{
		Debug.Log("-----------------OnGetGetDungeonDataRes");
		this.isRequestingChapters = false;
		if (state == 0)
		{
			switch (down.chapterInfo.dungeonType)
			{
			case DungeonType.ENUM.Normal:
				this.normalData.Clear();
				this.normalData.AddRange(down.chapterInfo.chapters);
				this.SetupDicDungeonInfo(this.normalData);
				this.SetPross(DungeonType.ENUM.Normal);
				break;
			case DungeonType.ENUM.Elite:
				this.eliteData.Clear();
				this.eliteData.AddRange(down.chapterInfo.chapters);
				this.SetupDicDungeonInfo(this.eliteData);
				this.SetPross(DungeonType.ENUM.Elite);
				break;
			case DungeonType.ENUM.Team:
				this.teamData.Clear();
				this.teamData.AddRange(down.chapterInfo.chapters);
				this.SetupDicDungeonInfo(this.teamData);
				this.SetPross(DungeonType.ENUM.Team);
				break;
			}
			if (this.m_actionGetDungeonDataCallback != null)
			{
				this.m_actionGetDungeonDataCallback.Invoke();
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnChallengeDungeonRes(short state, ChallengeDungeonRes down = null)
	{
		if (state == 0)
		{
			UIStackManager.Instance.PopUIIfTarget("InstanceDetailUI");
			InstanceManager.ChangeInstanceManager(down.dungeonId, false);
			InstanceManager.SimulateEnterField(InstanceManager.CurrentInstanceData.type, null);
			InstanceManager.SimulateSwicthMap(InstanceManager.CurrentInstanceData.scene, LocalInstanceHandler.Instance.CreateSelfInfo(InstanceManager.CurrentInstanceData.type, InstanceManager.CurrentInstanceDataID, InstanceManager.CurrentInstanceData.scene, 0, 0, null, null, null), null, 0);
			BattleDmgCollectManager.Instance.ClearData();
			EventDispatcher.Broadcast<bool>(EventNames.ChallengeDungeonResult, true);
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast<bool>(EventNames.ChallengeDungeonResult, false);
		}
		WaitUI.CloseUI(0u);
	}

	private void OnExitChallengeRes(short state, ExitChallengeRes down = null)
	{
		if (state == 0)
		{
			LocalInstanceHandler.Instance.Finish(false);
			InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnRequestClientExitChallengeNty(short state, RequestClientExitChallengeNty down = null)
	{
		if (state == 0)
		{
			LocalInstanceHandler.Instance.Finish(false);
			this.NotifyLocalDungeonInstanceFinish(false);
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetAllDungeonChallengeTimesResetNty(short state, AllDungeonChallengeTimesResetNty down = null)
	{
		if (state == 0)
		{
			Debug.Log("--------------OnGetAllDungeonChallengeTimesResetNty");
			using (Dictionary<int, DungeonInfo>.Enumerator enumerator = this.dicDungeonInfo.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, DungeonInfo> current = enumerator.get_Current();
					DungeonInfo value = current.get_Value();
					ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(value.dungeonId);
					current.get_Value().remainingChallengeTimes = zhuXianPeiZhi.num;
					current.get_Value().resetChallengeTimes = 0;
				}
			}
			EventDispatcher.Broadcast(EventNames.OnGetAllDungeonChallengeTimesResetNty);
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetDungeonMiscChangedNty(short state, DungeonMiscChangedNty down = null)
	{
		if (state == 0)
		{
			Debuger.Error("----------------------down.usedFreeMopUpTimes  " + down.usedFreeMopUpTimes, new object[0]);
			this.usedFreeMopUpTimes = down.usedFreeMopUpTimes;
			EventDispatcher.Broadcast(EventNames.UsedFreeMopUpTimesChange);
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void ExperienceInstanceEnd()
	{
		this.DungeonInstanceType = DungeonManager.InsType.MAIN;
		DungeonManager.Instance.SendChallengeDungeonReq(10101);
	}

	private void OnBackToTown()
	{
		if (!(SceneLoadedUISetting.CurrentType != "SHOW_TOWN_UI"))
		{
			if (MainTaskManager.Instance.IsGoingTask(InstanceManager.CurrentInstanceDataID))
			{
				UIStackManager.Instance.PopTownUI();
			}
		}
	}

	public void SendBuyMopUpTimeReq(int dungeonIdParm, int timesParm, Action buySuccessBuyMopUpTimeCallBack)
	{
		this.m_buySuccessBuyMopUpTimeCallBack = buySuccessBuyMopUpTimeCallBack;
		NetworkManager.Send(new BuyMopUpTimesReq
		{
			dungeonId = dungeonIdParm,
			times = timesParm
		}, ServerType.Data);
	}

	public void SendMopUpDungeonReq(int dungeonIdPram, int timesParm)
	{
		Debug.Log(string.Concat(new object[]
		{
			"dungeonIdPram  ",
			dungeonIdPram,
			"  timesParm  ",
			timesParm
		}));
		NetworkManager.Send(new MopUpDungeonReq
		{
			dungeonId = dungeonIdPram,
			times = timesParm
		}, ServerType.Data);
	}

	private void OnMopUpDungeonRes(short state, MopUpDungeonRes down = null)
	{
		EventDispatcher.Broadcast(EventNames.OpenMopupBtn);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.listMopUpReslut.Clear();
			this.listMopUpReslut.AddRange(down.results);
			EventDispatcher.Broadcast(DungeonManagerEvent.OnMopUpDungeonRes);
		}
	}

	private void OnGetBuyMopUpTimeRes(short state, BuyMopUpTimesRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.m_buySuccessBuyMopUpTimeCallBack != null)
		{
			this.m_buySuccessBuyMopUpTimeCallBack.Invoke();
			this.m_buySuccessBuyMopUpTimeCallBack = null;
		}
	}

	public void SendResetChallengeTimesReq(int dungeonIdPram)
	{
		NetworkManager.Send(new ResetChallengeTimesReq
		{
			dungeonId = dungeonIdPram
		}, ServerType.Data);
	}

	private void OnGetResetChallengeTimesRes(short state, ResetChallengeTimesRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.m_buyChallengeTimesSuccessCallBack != null)
		{
			this.m_buyChallengeTimesSuccessCallBack.Invoke();
			this.m_buyChallengeTimesSuccessCallBack = null;
		}
	}

	public bool IsFinish(int instanceID)
	{
		if (instanceID <= 0)
		{
			return false;
		}
		FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(instanceID);
		if (fuBenJiChuPeiZhi == null)
		{
			return false;
		}
		List<ChapterInfo> dataByInstanceType = this.GetDataByInstanceType(fuBenJiChuPeiZhi.type);
		if (dataByInstanceType == null)
		{
			return false;
		}
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(instanceID);
		if (zhuXianPeiZhi == null)
		{
			return false;
		}
		ZhuXianZhangJiePeiZhi zhuXianZhangJiePeiZhi = DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi.chapterId);
		if (zhuXianZhangJiePeiZhi == null)
		{
			return false;
		}
		if (zhuXianZhangJiePeiZhi.chapterOrder - 1 < 0 || zhuXianZhangJiePeiZhi.chapterOrder - 1 >= dataByInstanceType.get_Count())
		{
			return false;
		}
		ChapterInfo chapterInfo = dataByInstanceType.get_Item(zhuXianZhangJiePeiZhi.chapterOrder - 1);
		for (int i = 0; i < chapterInfo.dungeons.get_Count(); i++)
		{
			if (instanceID == chapterInfo.dungeons.get_Item(i).dungeonId)
			{
				return chapterInfo.dungeons.get_Item(i).clearance;
			}
		}
		return false;
	}

	public void BuyChallengeTimes(int instanceID, Action buyChallengeTimesSuccessCallBack)
	{
		this.m_buyChallengeTimesSuccessCallBack = buyChallengeTimesSuccessCallBack;
		DungeonInfo di = DungeonManager.Instance.GetDungeonInfo(instanceID);
		if (di == null)
		{
			return;
		}
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(instanceID);
		if (di.resetChallengeTimes >= VIPPrivilegeManager.Instance.GetVipTimesByType(4))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505118, false), 1f, 1f);
			return;
		}
		LeiXingXiaoHaoBiao leiXingXiaoHaoBiao = DataReader<LeiXingXiaoHaoBiao>.DataList.Find((LeiXingXiaoHaoBiao a) => a.ID == 1 && a.resetTime == di.resetChallengeTimes + 1);
		int needGoodNum = leiXingXiaoHaoBiao.needGoodNum;
		string text = GameDataUtils.GetChineseContent(505071, false);
		text = text.Replace("x{0}", needGoodNum.ToString());
		text = text.Replace("x{1}", zhuXianPeiZhi.num.ToString());
		text = text.Replace("x{2}", (VIPPrivilegeManager.Instance.GetVipTimesByType(4) - di.resetChallengeTimes).ToString());
		text = text.Replace("x{3}", VIPPrivilegeManager.Instance.GetVipTimesByType(4).ToString());
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(500032, false), text, delegate
		{
		}, delegate
		{
			DungeonManager.Instance.SendResetChallengeTimesReq(instanceID);
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	public int GetTheLastInstaceID(InstanceType instanceType)
	{
		List<ChapterInfo> dataByInstanceType = this.GetDataByInstanceType((int)instanceType);
		bool flag = false;
		int result = 0;
		for (int i = 0; i < dataByInstanceType.get_Count(); i++)
		{
			ChapterInfo chapterInfo = dataByInstanceType.get_Item(i);
			for (int j = 0; j < chapterInfo.dungeons.get_Count(); j++)
			{
				DungeonInfo dungeonInfo = chapterInfo.dungeons.get_Item(j);
				if (!dungeonInfo.clearance)
				{
					result = dungeonInfo.dungeonId;
					flag = true;
					break;
				}
			}
			if (flag)
			{
				break;
			}
		}
		if (!flag)
		{
			ChapterInfo chapterInfo2 = dataByInstanceType.get_Item(dataByInstanceType.get_Count() - 1);
			DungeonInfo dungeonInfo2 = chapterInfo2.dungeons.get_Item(chapterInfo2.dungeons.get_Count() - 1);
			result = dungeonInfo2.dungeonId;
		}
		return result;
	}

	public Hashtable CheckLock(int instanceID)
	{
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(instanceID);
		bool flag = true;
		if (zhuXianPeiZhi.linkTask != 0 && MainTaskManager.Instance.CurTaskId != zhuXianPeiZhi.linkTask)
		{
			flag = false;
		}
		bool flag2 = false;
		string text = GameDataUtils.GetChineseContent(505094, false);
		for (int i = 0; i < zhuXianPeiZhi.frontInstance.get_Count(); i++)
		{
			int num = zhuXianPeiZhi.frontInstance.get_Item(i);
			if (num != 0 && num != instanceID)
			{
				DungeonInfo dungeonInfo = DungeonManager.Instance.GetDungeonInfo(num);
				if ((dungeonInfo != null && (!dungeonInfo.clearance || !flag)) || dungeonInfo == null)
				{
					flag2 = true;
					FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(num);
					ZhuXianPeiZhi zhuXianPeiZhi2 = DataReader<ZhuXianPeiZhi>.Get(num);
					if (fuBenJiChuPeiZhi.type == 101)
					{
						text = text + string.Format(GameDataUtils.GetChineseContent(505095, false), DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi2.chapterId).chapterOrder, zhuXianPeiZhi2.instance) + "\n";
					}
					else if (fuBenJiChuPeiZhi.type == 102)
					{
						text = text + string.Format(GameDataUtils.GetChineseContent(505096, false), DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi2.chapterId).chapterOrder, zhuXianPeiZhi2.instance) + "\n";
					}
					else if (fuBenJiChuPeiZhi.type == 103)
					{
						text = text + string.Format(GameDataUtils.GetChineseContent(505097, false), DataReader<ZhuXianZhangJiePeiZhi>.Get(zhuXianPeiZhi2.chapterId).chapterOrder, zhuXianPeiZhi2.instance) + "\n";
					}
				}
			}
		}
		text = text.Substring(0, text.get_Length() - 1);
		if (zhuXianPeiZhi.minLv > EntityWorld.Instance.EntSelf.Lv)
		{
			flag2 = true;
		}
		if (zhuXianPeiZhi.minLv > EntityWorld.Instance.EntSelf.Lv)
		{
			text = GameDataUtils.GetChineseContent(510114, false);
			text = text.Replace("{s1}", zhuXianPeiZhi.minLv.ToString());
		}
		else if (DataReader<FuBenJiChuPeiZhi>.Get(instanceID).type != 2 || zhuXianPeiZhi.instance != 1)
		{
			if (DataReader<FuBenJiChuPeiZhi>.Get(instanceID).type == 3)
			{
			}
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("ISLock", flag2);
		hashtable.Add("LockReason", text);
		return hashtable;
	}

	public void CheckCanShowTip()
	{
	}

	public void OnGetDungeonDataReq()
	{
		if (this.normalData.get_Count() == 0)
		{
			ChapterResume chapterResume = this.listChapterResume.Find((ChapterResume a) => a.dungeonType == DungeonType.ENUM.Normal);
		}
	}
}
