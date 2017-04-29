using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class InstanceManager
{
	protected static InstanceParent currentInstance = null;

	protected static InstanceType lastInstanceType = InstanceType.None;

	protected static bool isCurrentInBattle = false;

	protected static BattleField currentBattleFieldType = BattleField.None;

	protected static CommunicationType currentCommunicationType = CommunicationType.None;

	protected static int serverBatch = 1;

	protected static bool isCurrentInstanceWithTask = false;

	protected static bool isLastInstanceWithTask = false;

	protected static bool isPauseTimeEscape = false;

	protected static bool isAIThinking = false;

	protected static bool isActorAnimatorOn = true;

	protected static int aiLimitLevel = 15;

	protected static int aiLimitTip = 0;

	protected static bool hasSceneLoaded = false;

	protected static bool hasSelfPositionGot = false;

	protected static bool hasSelfPositionUpdated = false;

	protected static bool isLoadingTimeOut = false;

	protected static bool isWaitingEnter = false;

	protected static uint mainSceneDelayTimer = 0u;

	protected static uint waitForPlayerTimer = 0u;

	protected static uint countDownTimer = 0u;

	protected static uint delayBatteUITimer = 0u;

	protected static uint openingHintTimer = 0u;

	protected static uint delayEndingHintTimer = 0u;

	protected static uint endingCameraTimer = 0u;

	protected static uint endingHintTimer = 0u;

	protected static bool isDebut = false;

	protected static XDict<long, BattleSituationInfo> bossHurtTable = new XDict<long, BattleSituationInfo>();

	protected static XDict<long, BattleSituationInfo> playerHurtTable = new XDict<long, BattleSituationInfo>();

	protected static XDict<long, BattleSituationInfo> playerHurtFromPlayerTable = new XDict<long, BattleSituationInfo>();

	protected static XDict<long, BattleSituationInfo> playerDamageTable = new XDict<long, BattleSituationInfo>();

	protected static XDict<BattleHurtInfoType, Action<XDict<long, BattleSituationInfo>>> queryBattleSituationCallBack = new XDict<BattleHurtInfoType, Action<XDict<long, BattleSituationInfo>>>();

	protected static XDict<int, long> realTimeDropCache = new XDict<int, long>();

	protected static bool isShowInstanceDrop = true;

	protected static int instanceDropMonsterTypeID;

	protected static int instanceDropMonsterModelID;

	protected static Vector3 instanceDropOriginPosition;

	protected static List<Vector3> instanceDropWaitPosition = new List<Vector3>();

	protected static bool isShowMonsterDrop = true;

	protected static List<Func<Action, Action, bool>> securityCheckAction = new List<Func<Action, Action, bool>>();

	protected static bool isInSecurityCheck = false;

	protected static Action securityCheckPassAction = null;

	protected static Action securityCheckBlockAndCancelAction = null;

	public static InstanceParent CurrentInstance
	{
		get
		{
			if (InstanceManager.currentInstance == null)
			{
				InstanceManager.currentInstance = new InstanceParent();
			}
			return InstanceManager.currentInstance;
		}
	}

	public static InstanceType LastInstanceType
	{
		get
		{
			return InstanceManager.lastInstanceType;
		}
		set
		{
			InstanceManager.lastInstanceType = value;
		}
	}

	public static InstanceType CurrentInstanceType
	{
		get
		{
			return InstanceManager.CurrentInstance.Type;
		}
	}

	public static int CurrentInstanceDataID
	{
		get
		{
			return InstanceManager.CurrentInstance.InstanceDataID;
		}
	}

	public static FuBenJiChuPeiZhi CurrentInstanceData
	{
		get
		{
			return InstanceManager.CurrentInstance.InstanceData;
		}
	}

	public static bool IsCurrentInBattle
	{
		get
		{
			return InstanceManager.isCurrentInBattle;
		}
		set
		{
			InstanceManager.isCurrentInBattle = value;
			if (value)
			{
				EntityWorld.Instance.EntSelf.ChangeToBattleMode();
			}
			else
			{
				EntityWorld.Instance.EntSelf.ChangeToCityMode();
			}
			EventDispatcher.Broadcast(HeartbeatManagerEvent.ForceSendHeartbeat);
		}
	}

	public static BattleField CurrentBattleFieldType
	{
		get
		{
			return InstanceManager.currentBattleFieldType;
		}
	}

	public static BattleFieldType CurrentBattleFieldTypeData
	{
		get
		{
			return (InstanceManager.CurrentBattleFieldType != BattleField.None) ? DataReader<BattleFieldType>.Get((int)InstanceManager.CurrentBattleFieldType) : null;
		}
	}

	public static CommunicationType CurrentCommunicationType
	{
		get
		{
			return InstanceManager.currentCommunicationType;
		}
	}

	public static bool IsServerBattle
	{
		get
		{
			return InstanceManager.CurrentCommunicationType == CommunicationType.Server;
		}
	}

	public static bool IsLocalBattle
	{
		get
		{
			return InstanceManager.CurrentCommunicationType == CommunicationType.Client || InstanceManager.CurrentCommunicationType == CommunicationType.Mixed;
		}
	}

	public static bool IsServerCreate
	{
		get
		{
			return InstanceManager.CurrentCommunicationType == CommunicationType.Mixed || InstanceManager.CurrentCommunicationType == CommunicationType.Server;
		}
	}

	public static bool IsClientCreate
	{
		get
		{
			return InstanceManager.CurrentCommunicationType == CommunicationType.Client;
		}
	}

	public static int CurrentInstanceBatch
	{
		get
		{
			return (!InstanceManager.IsServerCreate) ? LocalInstanceHandler.Instance.CurrentBatch : InstanceManager.ServerBatch;
		}
	}

	public static int ServerBatch
	{
		get
		{
			return InstanceManager.serverBatch;
		}
		set
		{
			InstanceManager.serverBatch = value;
			if (InstanceManager.IsServerCreate)
			{
				EventDispatcher.Broadcast<int>(InstanceManagerEvent.BatchChanged, value);
			}
			EventDispatcher.Broadcast<int, EntityMonster, string>("BattleDialogTrigger", 10, null, value.ToString());
		}
	}

	public static bool IsCurrentInstanceWithTask
	{
		get
		{
			return InstanceManager.isCurrentInstanceWithTask;
		}
		set
		{
			InstanceManager.isCurrentInstanceWithTask = value;
		}
	}

	public static bool IsLastInstanceWithTask
	{
		get
		{
			return InstanceManager.isLastInstanceWithTask;
		}
		set
		{
			InstanceManager.isLastInstanceWithTask = value;
		}
	}

	public static int InstanceTime
	{
		get
		{
			return BattleTimeManager.Instance.InstanceTime;
		}
	}

	public static int CurUsedTime
	{
		get
		{
			return BattleTimeManager.Instance.UsedTime;
		}
	}

	public static int TotalUsedTime
	{
		get
		{
			return BattleTimeManager.Instance.TotalUsedTime;
		}
	}

	public static int LeftTime
	{
		get
		{
			return BattleTimeManager.Instance.LeftTime;
		}
	}

	public static int LeftTimePercentage
	{
		get
		{
			return BattleTimeManager.Instance.LeftTimePercentage;
		}
	}

	public static bool IsPauseTimeEscape
	{
		get
		{
			return InstanceManager.isPauseTimeEscape;
		}
		set
		{
			InstanceManager.isPauseTimeEscape = value;
			InstanceManager.IsActorAnimatorOn = !value;
			if (value)
			{
				InstanceManager.PauseAllClientAI();
			}
			else
			{
				InstanceManager.ResumeAllClientAI();
			}
		}
	}

	public static bool IsAIThinking
	{
		get
		{
			return InstanceManager.isAIThinking;
		}
		set
		{
			Debug.Log("IsAIThinking: " + value);
			InstanceManager.isAIThinking = value;
			if (value)
			{
				EventDispatcher.Broadcast(AIManagerEvent.ResumeAI);
			}
			else
			{
				EventDispatcher.Broadcast(AIManagerEvent.PauseAI);
			}
		}
	}

	public static bool IsActorAnimatorOn
	{
		get
		{
			return InstanceManager.isActorAnimatorOn;
		}
		set
		{
			InstanceManager.isActorAnimatorOn = value;
			InstanceManager.SetAnimtorsOfBattle(value);
		}
	}

	public static bool IsAILevelLimit
	{
		get
		{
			return EntityWorld.Instance.EntSelf == null || EntityWorld.Instance.EntSelf.Lv < InstanceManager.aiLimitLevel;
		}
	}

	public static int AILimitTip
	{
		get
		{
			return InstanceManager.aiLimitTip;
		}
	}

	public static bool IsDebut
	{
		get
		{
			return InstanceManager.isDebut;
		}
	}

	public static XDict<int, long> RealTimeDropCache
	{
		get
		{
			return InstanceManager.realTimeDropCache;
		}
	}

	public static bool IsShowInstanceDrop
	{
		get
		{
			return InstanceManager.isShowInstanceDrop;
		}
		set
		{
			InstanceManager.isShowInstanceDrop = value;
		}
	}

	public static int InstanceDropMonsterTypeID
	{
		get
		{
			return InstanceManager.instanceDropMonsterTypeID;
		}
		set
		{
			InstanceManager.instanceDropMonsterTypeID = value;
		}
	}

	public static int InstanceDropMonsterModelID
	{
		get
		{
			return InstanceManager.instanceDropMonsterModelID;
		}
		set
		{
			InstanceManager.instanceDropMonsterModelID = value;
		}
	}

	public static Vector3 InstanceDropOriginPosition
	{
		get
		{
			return InstanceManager.instanceDropOriginPosition;
		}
		set
		{
			InstanceManager.instanceDropOriginPosition = value;
		}
	}

	public static bool IsShowMonsterDrop
	{
		get
		{
			return InstanceManager.isShowMonsterDrop;
		}
		set
		{
			InstanceManager.isShowMonsterDrop = value;
		}
	}

	public static float CurrentCollectDropTime
	{
		get
		{
			return InstanceManager.CurrentInstance.CurrentCollectDropTime;
		}
	}

	public static bool IsInSecurityCheck
	{
		get
		{
			return InstanceManager.isInSecurityCheck;
		}
		protected set
		{
			InstanceManager.isInSecurityCheck = value;
		}
	}

	public static Action SecurityCheckPassAction
	{
		get
		{
			return InstanceManager.securityCheckPassAction;
		}
		protected set
		{
			InstanceManager.securityCheckPassAction = value;
		}
	}

	public static Action SecurityCheckBlockAndCancelAction
	{
		get
		{
			return InstanceManager.securityCheckBlockAndCancelAction;
		}
		protected set
		{
			InstanceManager.securityCheckBlockAndCancelAction = value;
		}
	}

	protected InstanceManager()
	{
	}

	public static void Init()
	{
		if (DataReader<GlobalParams>.Contains("aiLimit"))
		{
			string value = DataReader<GlobalParams>.Get("aiLimit").value;
			string[] array = value.Split(new char[]
			{
				';'
			});
			if (array.Length > 1)
			{
				InstanceManager.aiLimitLevel = int.Parse(array[0]);
				InstanceManager.aiLimitTip = int.Parse(array[1]);
			}
		}
		InstanceManager.AddInstanceListeners();
	}

	public static void Release()
	{
		InstanceManager.RemoveInstanceListeners();
		InstanceManager.DeactiveCurrentInstance();
		InstanceManager.lastInstanceType = InstanceType.None;
		InstanceManager.isCurrentInBattle = false;
		InstanceManager.serverBatch = 1;
		InstanceManager.isCurrentInstanceWithTask = false;
		InstanceManager.isLastInstanceWithTask = false;
		InstanceManager.isPauseTimeEscape = false;
		InstanceManager.isAIThinking = false;
		InstanceManager.isActorAnimatorOn = true;
		InstanceManager.bossHurtTable.Clear();
		InstanceManager.playerHurtTable.Clear();
		InstanceManager.playerHurtFromPlayerTable.Clear();
		InstanceManager.playerDamageTable.Clear();
		InstanceManager.queryBattleSituationCallBack.Clear();
		InstanceManager.realTimeDropCache.Clear();
		InstanceManager.isShowInstanceDrop = true;
		InstanceManager.instanceDropMonsterTypeID = 0;
		InstanceManager.instanceDropOriginPosition = Vector3.get_zero();
		InstanceManager.instanceDropWaitPosition.Clear();
		InstanceManager.isShowMonsterDrop = true;
		InstanceManager.securityCheckAction.Clear();
		InstanceManager.isInSecurityCheck = false;
		InstanceManager.securityCheckPassAction = null;
		InstanceManager.securityCheckBlockAndCancelAction = null;
	}

	protected static void AddInstanceListeners()
	{
		NetworkManager.AddListenEvent<RoleEnteredFieldNty>(new NetCallBackMethod<RoleEnteredFieldNty>(InstanceManager.EnterBattleField));
		EventDispatcher.AddListener(SceneManagerEvent.ClearSceneDependentLogic, new Callback(InstanceManager.ClearSceneDependentLogic));
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(InstanceManager.UnloadScene));
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.LoadSceneStart, new Callback<int, int>(InstanceManager.LoadSceneStart));
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(InstanceManager.LoadSceneEnd));
		EventDispatcher.AddListener(InstanceManagerEvent.GetAllClientLoadDone, new Callback(InstanceManager.GetAllClientLoadDone));
		NetworkManager.AddListenEvent<BattleLoadInfo>(new NetCallBackMethod<BattleLoadInfo>(InstanceManager.SendClientLoadDoneResp));
		NetworkManager.AddListenEvent<PauseFieldRes>(new NetCallBackMethod<PauseFieldRes>(InstanceManager.ServerPauseFieldRes));
		NetworkManager.AddListenEvent<ResumeFieldRes>(new NetCallBackMethod<ResumeFieldRes>(InstanceManager.ServerResumeFieldRes));
		NetworkManager.AddListenEvent<QueryBattleInfoRes>(new NetCallBackMethod<QueryBattleInfoRes>(InstanceManager.SetBattleSituation));
		NetworkManager.AddListenEvent<BattleFieldResetNty>(new NetCallBackMethod<BattleFieldResetNty>(InstanceManager.ResetBattleFieldResp));
		NetworkManager.AddListenEvent<RoleWillLeaveFieldNty>(new NetCallBackMethod<RoleWillLeaveFieldNty>(InstanceManager.ExitBattleField));
		NetworkManager.AddListenEvent<BattleDropItemInfoNty>(new NetCallBackMethod<BattleDropItemInfoNty>(InstanceManager.OnBattleDropItemInfoNty));
		NetworkManager.AddListenEvent<BattleCollectItemAddNty>(new NetCallBackMethod<BattleCollectItemAddNty>(InstanceManager.OnBattleCollectItemInfoAddNty));
		NetworkManager.AddListenEvent<BattleCollectItemRemoveNty>(new NetCallBackMethod<BattleCollectItemRemoveNty>(InstanceManager.OnBattleCollectItemInfoRemoveNty));
		NetworkManager.AddListenEvent<BattlePickCollectionRes>(new NetCallBackMethod<BattlePickCollectionRes>(InstanceManager.OnCollectDropRes));
		NetworkManager.AddListenEvent<BattleReliveRes>(new NetCallBackMethod<BattleReliveRes>(InstanceManager.OnReliveRes));
	}

	protected static void RemoveInstanceListeners()
	{
		NetworkManager.RemoveListenEvent<RoleEnteredFieldNty>(new NetCallBackMethod<RoleEnteredFieldNty>(InstanceManager.EnterBattleField));
		EventDispatcher.RemoveListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(InstanceManager.UnloadScene));
		EventDispatcher.RemoveListener<int, int>(SceneManagerEvent.LoadSceneStart, new Callback<int, int>(InstanceManager.LoadSceneStart));
		EventDispatcher.RemoveListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(InstanceManager.LoadSceneEnd));
		EventDispatcher.RemoveListener(InstanceManagerEvent.GetAllClientLoadDone, new Callback(InstanceManager.GetAllClientLoadDone));
		NetworkManager.RemoveListenEvent<BattleLoadInfo>(new NetCallBackMethod<BattleLoadInfo>(InstanceManager.SendClientLoadDoneResp));
		NetworkManager.RemoveListenEvent<PauseFieldRes>(new NetCallBackMethod<PauseFieldRes>(InstanceManager.ServerPauseFieldRes));
		NetworkManager.RemoveListenEvent<ResumeFieldRes>(new NetCallBackMethod<ResumeFieldRes>(InstanceManager.ServerResumeFieldRes));
		NetworkManager.RemoveListenEvent<QueryBattleInfoRes>(new NetCallBackMethod<QueryBattleInfoRes>(InstanceManager.SetBattleSituation));
		NetworkManager.RemoveListenEvent<BattleFieldResetNty>(new NetCallBackMethod<BattleFieldResetNty>(InstanceManager.ResetBattleFieldResp));
		NetworkManager.RemoveListenEvent<RoleWillLeaveFieldNty>(new NetCallBackMethod<RoleWillLeaveFieldNty>(InstanceManager.ExitBattleField));
		NetworkManager.RemoveListenEvent<BattleDropItemInfoNty>(new NetCallBackMethod<BattleDropItemInfoNty>(InstanceManager.OnBattleDropItemInfoNty));
		NetworkManager.RemoveListenEvent<BattleCollectItemAddNty>(new NetCallBackMethod<BattleCollectItemAddNty>(InstanceManager.OnBattleCollectItemInfoAddNty));
		NetworkManager.RemoveListenEvent<BattleCollectItemRemoveNty>(new NetCallBackMethod<BattleCollectItemRemoveNty>(InstanceManager.OnBattleCollectItemInfoRemoveNty));
		NetworkManager.RemoveListenEvent<BattlePickCollectionRes>(new NetCallBackMethod<BattlePickCollectionRes>(InstanceManager.OnCollectDropRes));
		NetworkManager.RemoveListenEvent<BattleReliveRes>(new NetCallBackMethod<BattleReliveRes>(InstanceManager.OnReliveRes));
	}

	public static void ChangeInstanceManager(int instanceDataID, bool isForceChange = false)
	{
		FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(instanceDataID);
		if (fuBenJiChuPeiZhi == null)
		{
			return;
		}
		InstanceType type = (InstanceType)fuBenJiChuPeiZhi.type;
		InstanceParent instance;
		switch (type)
		{
		case InstanceType.DungeonNormal:
			instance = DungeonNormalInstance.Instance;
			break;
		case InstanceType.DungeonElite:
			instance = DungeonEliteInstance.Instance;
			break;
		case InstanceType.DungeonMutiPeople:
			instance = MultiPlayerInstance.Instance;
			break;
		case InstanceType.Arena:
			instance = PVPInstance.Instance;
			break;
		case InstanceType.GangFight:
			instance = GangFightInstance.Instance;
			break;
		case InstanceType.SurvivalChallenge:
			instance = SurvivalInstance.Instance;
			break;
		case InstanceType.Element:
			instance = ElementInstance.Instance;
			break;
		case InstanceType.Defence:
			instance = TowerInstance.Instance;
			break;
		case InstanceType.Escort:
			instance = TowerInstance.Instance;
			break;
		case InstanceType.Salvation:
			instance = TowerInstance.Instance;
			break;
		case InstanceType.Experience:
			instance = ExperienceInstance.Instance;
			break;
		case InstanceType.GuildWar:
			instance = GuildWarInstance.Instance;
			break;
		case InstanceType.Bounty:
			instance = BountyInstance.Instance;
			break;
		case InstanceType.ChangeCareer:
			instance = ExperienceInstance.Instance;
			break;
		case InstanceType.Field:
			instance = FieldInstance.Instance;
			break;
		case InstanceType.WildBossSingle:
			instance = WildBossSingleInstance.Instance;
			break;
		case InstanceType.ExperienceCopy:
			instance = ExperienceCopyInstance.Instance;
			break;
		case InstanceType.WildBossMulti:
			instance = WildBossMultiInstance.Instance;
			break;
		case InstanceType.Hook:
			instance = HookInstance.Instance;
			break;
		case InstanceType.GuildBoss:
			instance = GuildBossInstance.Instance;
			break;
		case InstanceType.MultiPVP:
			instance = MultiPVPInstance.Instance;
			break;
		case InstanceType.DarkTrial:
			instance = DarkTrialInstance.Instance;
			break;
		case InstanceType.Tramcar:
			instance = TramcarInstance.Instance;
			break;
		default:
			if (type != InstanceType.ClientTest)
			{
				if (type != InstanceType.ServerTest)
				{
					return;
				}
				instance = ServerTestInstance.Instance;
			}
			else
			{
				instance = ClientTestInstance.Instance;
			}
			break;
		}
		instance.InstanceDataID = instanceDataID;
		InstanceManager.ChangeInstanceManager(instance, isForceChange);
	}

	public static void ChangeInstanceManager(InstanceParent newManager, bool isForceChange = false)
	{
		if (newManager == InstanceManager.currentInstance && !isForceChange)
		{
			return;
		}
		InstanceManager.DeactiveCurrentInstance();
		InstanceManager.currentInstance = newManager;
		InstanceManager.ActiveCurrentInstance();
	}

	protected static void ActiveCurrentInstance()
	{
		InstanceManager.CurrentInstance.AddInstanceListeners();
		InstanceManager.isAIThinking = (InstanceManager.CurrentInstanceData != null && InstanceManager.CurrentInstanceData.defaultAiState != 0);
		InstanceManager.isPauseTimeEscape = false;
		InstanceManager.realTimeDropCache.Clear();
		InstanceManager.isShowInstanceDrop = false;
		InstanceManager.isShowMonsterDrop = false;
	}

	protected static void DeactiveCurrentInstance()
	{
		InstanceManager.ResetInstanceProcessArgument();
		if (InstanceManager.currentInstance != null)
		{
			InstanceManager.LastInstanceType = InstanceManager.currentInstance.Type;
			InstanceManager.IsLastInstanceWithTask = InstanceManager.IsCurrentInstanceWithTask;
		}
		InstanceManager.CurrentInstance.RemoveInstanceListeners();
		InstanceManager.CurrentInstance.ReleaseData();
	}

	protected static void EnterBattleField(short state, RoleEnteredFieldNty down = null)
	{
		Debug.Log("收到玩家进入战场通知");
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		CityFakeDrop.DeleteAllCityFakeDrop();
		InstanceManager.ResetInstanceProcessArgument();
		LocalBattleHandler.Instance.ResetData();
		LocalInstanceHandler.Instance.ResetData();
		BattleBlackboard.Instance.ResetData();
		GlobalBattleNetwork.Instance.ResetData();
		InstanceManager.IsCurrentInBattle = true;
		if (down.dungeonId != 0)
		{
			InstanceManager.ChangeInstanceManager(down.dungeonId, false);
			InstanceManager.CurrentInstance.SetPreloadTypeData(down.preloadRoleTypeId, down.preloadPetId, down.preloadMonsterId, down.preloadRoleSkillId, down.preloadPetSkillId);
		}
		BattleTimeManager.Instance.WaitForSetTime();
		InstanceManager.currentBattleFieldType = (BattleField)down.logicType;
		InstanceManager.SetBattleCommunicateType(down.type, down.cliDrvBattleIgnoreDmg);
		if (InstanceManager.IsLocalBattle)
		{
			BattleCalculator.SetData(down.cliDrvBattleRandSeed, down.cliDrvBattleRandLen);
			LocalBattleHandler.Instance.SetData(down.buffIds);
		}
		InstanceManager.CurrentInstance.EnterBattleField();
	}

	protected static void ResetInstanceProcessArgument()
	{
		InstanceManager.hasSceneLoaded = false;
		InstanceManager.hasSelfPositionGot = false;
		InstanceManager.hasSelfPositionUpdated = false;
		InstanceManager.isLoadingTimeOut = false;
		TimerHeap.DelTimer(InstanceManager.mainSceneDelayTimer);
		TimerHeap.DelTimer(InstanceManager.waitForPlayerTimer);
		TimerHeap.DelTimer(InstanceManager.countDownTimer);
		TimerHeap.DelTimer(InstanceManager.delayBatteUITimer);
		TimerHeap.DelTimer(InstanceManager.openingHintTimer);
		TimerHeap.DelTimer(InstanceManager.delayEndingHintTimer);
		TimerHeap.DelTimer(InstanceManager.endingCameraTimer);
		TimerHeap.DelTimer(InstanceManager.endingHintTimer);
		InstanceManager.isDebut = false;
	}

	protected static void SetBattleCommunicateType(BattleFieldT.BFT type, bool isIgnoreVerify)
	{
		switch (type)
		{
		case BattleFieldT.BFT.FrontEnd:
			InstanceManager.currentCommunicationType = CommunicationType.Client;
			break;
		case BattleFieldT.BFT.BackEnd:
			InstanceManager.currentCommunicationType = CommunicationType.Server;
			ReconnectManager.Instance.DataServerReconnectHandler = DataServerServerBattleReconnectHandler.Instance;
			break;
		case BattleFieldT.BFT.Mixed:
			if (isIgnoreVerify)
			{
				InstanceManager.currentCommunicationType = CommunicationType.MixedEx;
				ReconnectManager.Instance.DataServerReconnectHandler = DataServerMixBattleReconnectHandler.Instance;
			}
			else
			{
				InstanceManager.currentCommunicationType = CommunicationType.Mixed;
				ReconnectManager.Instance.DataServerReconnectHandler = DataServerMixBattleReconnectHandler.Instance;
			}
			break;
		}
		if (InstanceManager.IsServerBattle)
		{
			Debuger.EnableLog = false;
		}
	}

	protected static void ClearSceneDependentLogic()
	{
		CameraGlobal.cameraType = CameraType.None;
		CameraGlobal.isAreaPointBActive = false;
		Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 1);
	}

	protected static void UnloadScene(int lastSceneID, int nextSceneID)
	{
		InstanceManager.DeactiveSelfAI();
		WaveBloodManager.Instance.ClearWaveBlood();
	}

	protected static void LoadSceneStart(int lastSceneID, int nextSceneID)
	{
		InstanceManager.CurrentInstance.LoadSceneStart(lastSceneID, nextSceneID);
	}

	public static void PreLoadData(int sceneID)
	{
		InstanceManager.CurrentInstance.PreLoadData(sceneID);
	}

	protected static void LoadSceneEnd(int sceneID)
	{
		InstanceManager.CurrentInstance.LoadSceneEnd(sceneID);
		InstanceManager.hasSceneLoaded = true;
		InstanceManager.CheckClientLoadDone(sceneID);
	}

	public static void SelfPositionGot()
	{
		InstanceManager.hasSelfPositionGot = true;
		InstanceManager.CheckClientLoadDone(MySceneManager.Instance.CurSceneID);
	}

	public static void SelfPositionUpdated()
	{
		InstanceManager.hasSelfPositionUpdated = true;
		InstanceManager.CheckClientLoadDone(MySceneManager.Instance.CurSceneID);
	}

	public static void CheckClientLoadDone(int sceneID)
	{
		if (!InstanceManager.hasSceneLoaded || !InstanceManager.hasSelfPositionGot || !InstanceManager.hasSelfPositionUpdated)
		{
			return;
		}
		CameraGlobal.CreateCamera();
		InstanceManager.ResendCameraEvent();
		if (MySceneManager.IsMainScene(sceneID))
		{
			InstanceManager.mainSceneDelayTimer = TimerHeap.AddTimer(0u, 0, delegate
			{
				InstanceManager.currentCommunicationType = CommunicationType.None;
				InstanceManager.CurrentInstance.SetCommonLogic();
			});
		}
		else if (InstanceManager.isLoadingTimeOut)
		{
			InstanceManager.isWaitingEnter = true;
			InstanceManager.SetAllClientLoadDone();
		}
		else
		{
			InstanceManager.SendClientLoadDoneReq(sceneID);
		}
	}

	public static void SetIsWaitingEnter(bool isWaiting)
	{
		InstanceManager.isWaitingEnter = isWaiting;
	}

	public static void SendClientLoadDoneReq(int sceneID)
	{
		Debug.Log("加载完成上发");
		InstanceManager.isWaitingEnter = true;
		InstanceManager.CurrentInstance.SendClientLoadDoneReq(sceneID);
	}

	public static void SendClientLoadDoneResp(short state, BattleLoadInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.CheckWaitForOtherPlayer(down.loadTimeout);
	}

	protected static void CheckWaitForOtherPlayer(int millisecond)
	{
		if (InstanceManager.IsClientCreate && millisecond <= 0)
		{
			Debug.Log("战场直接开始!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			InstanceManager.SetAllClientLoadDone();
		}
	}

	protected static void GetAllClientLoadDone()
	{
		Debug.Log("所有玩家加载完毕或超时，战场开始!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		if (!InstanceManager.hasSceneLoaded || !InstanceManager.hasSelfPositionGot || !InstanceManager.hasSelfPositionUpdated)
		{
			InstanceManager.isLoadingTimeOut = true;
		}
		else
		{
			InstanceManager.SetAllClientLoadDone();
		}
	}

	protected static void SetAllClientLoadDone()
	{
		if (!InstanceManager.isWaitingEnter)
		{
			return;
		}
		InstanceManager.isWaitingEnter = false;
		LoadingUIView.Close();
		InstanceManager.CurrentInstance.SetBGM();
		InstanceManager.SetIsInstanceWithTask();
	}

	protected static void SetIsInstanceWithTask()
	{
		InstanceManager.IsCurrentInstanceWithTask = MainTaskManager.Instance.IsGoingTask(InstanceManager.CurrentInstanceDataID);
		InstanceManager.CheckOpeningCG();
	}

	protected static void CheckOpeningCG()
	{
		if (InstanceManager.CurrentInstanceData.timeLine > 0)
		{
			InstanceManager.CurrentInstance.PlayOpeningCG(InstanceManager.CurrentInstanceData.timeLine, delegate
			{
				CGCompleteAnnouncer.Announce(InstanceManager.CurrentInstanceData.timeLine);
				InstanceManager.CheckOpeningCountDown();
			});
		}
		else
		{
			InstanceManager.CheckOpeningCountDown();
		}
	}

	protected static void CheckOpeningCountDown()
	{
		int num = (!InstanceManager.IsClientCreate) ? ((int)(BattleTimeManager.Instance.StartTime - TimeManager.Instance.PreciseServerTime).get_TotalMilliseconds()) : InstanceManager.CurrentInstanceData.countdown;
		if (num > 0)
		{
			if (InstanceManager.CurrentInstanceData.countdown > 0)
			{
				InstanceManager.OpeningCountDown(num);
			}
			InstanceManager.countDownTimer = TimerHeap.AddTimer((uint)num, 0, new Action(InstanceManager.OpeningCountDownTimesUp));
		}
		else
		{
			InstanceManager.SetInstance();
		}
	}

	protected static void OpeningCountDown(int millisecond)
	{
		if (millisecond > 1000)
		{
			InstanceManager.CurrentInstance.OpeningCountDown(millisecond);
		}
	}

	protected static void OpeningCountDownTimesUp()
	{
		InstanceManager.CurrentInstance.OpeningCountDownTimesUp();
		InstanceManager.SetInstance();
	}

	protected static void SetInstance()
	{
		InstanceManager.CurrentInstance.SetAI(InstanceManager.CurrentInstanceData.startProcessAi == 0);
		InstanceManager.CurrentInstance.SetCommonLogic();
		InstanceManager.CurrentInstance.SetTime();
		InstanceManager.CheckBattleUIDelay();
	}

	protected static void CheckBattleUIDelay()
	{
		if (InstanceManager.CurrentInstanceData.uiDelay > 0)
		{
			InstanceManager.delayBatteUITimer = TimerHeap.AddTimer((uint)InstanceManager.CurrentInstanceData.uiDelay, 0, new Action(InstanceManager.ShowBattleUI));
		}
		else
		{
			InstanceManager.ShowBattleUI();
		}
	}

	protected static void ShowBattleUI()
	{
		Debug.Log("================ShowBattleUI===================");
		FXSpineManager.Instance.ShowBattleStart2(delegate
		{
			InstanceManager.Debut();
			InstanceManager.CurrentInstance.ShowBattleUI();
			InstanceManager.CheckOpeningHint();
		});
	}

	protected static void Debut()
	{
		InstanceManager.CurrentInstance.SetDebutCD();
		InstanceManager.isDebut = true;
	}

	protected static void CheckOpeningHint()
	{
		if (InstanceManager.CurrentInstanceData.startMessage > 0)
		{
			InstanceManager.ShowOpeningHint(InstanceManager.CurrentInstanceData.startMessage);
			InstanceManager.openingHintTimer = TimerHeap.AddTimer(3000u, 0, new Action(InstanceManager.ShowOpeningHintTimesUp));
		}
	}

	protected static void ShowOpeningHint(int textID)
	{
		InstanceManager.CurrentInstance.ShowOpeningHint(textID);
	}

	protected static void ShowOpeningHintTimesUp()
	{
		InstanceManager.CurrentInstance.ShowOpeningHintTimesUp();
	}

	public static void PlayerInitEnd(EntityPlayer player)
	{
		InstanceManager.CurrentInstance.PlayerInitEnd(player);
	}

	public static void BossInitEnd(EntityMonster boss)
	{
		InstanceManager.CurrentInstance.BossInitEnd(boss);
	}

	public static void SelfDie()
	{
		InstanceManager.CurrentInstance.SelfDie();
	}

	public static void SelfRelive()
	{
		InstanceManager.CurrentInstance.SelfRelive();
	}

	protected static void ResetBattleFieldResp(short state, BattleFieldResetNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		InstanceManager.StopAllClientAI(false);
		InstanceManager.serverBatch = 1;
		BattleTimeManager.Instance.WaitForSetTime();
		LocalBattleHandler.Instance.ResetData();
		LocalInstanceHandler.Instance.ResetData();
		BattleBlackboard.Instance.ResetData();
		GlobalBattleNetwork.Instance.ResetData();
		EntityWorld.Instance.ClearAllMapObjects();
		if (down == null)
		{
			return;
		}
		if (down.objs == null)
		{
			return;
		}
		XDict<long, EntityParent> allEntities = EntityWorld.Instance.AllEntities;
		using (List<MapObjInfo>.Enumerator enumerator = down.objs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MapObjInfo current = enumerator.get_Current();
				if (current.id == EntityWorld.Instance.EntSelf.ID)
				{
					EntityWorld.Instance.EntSelf.SetDataByMapObjInfo(current, false);
				}
				else if (allEntities.ContainsKey(current.id))
				{
					allEntities[current.id].SetDataByMapObjInfo(current, false);
				}
				else
				{
					AOIService.Instance.CreateEntity(current, false);
				}
			}
		}
		InstanceManager.CurrentInstance.ResetBattleFieldResp();
	}

	public static void BeforeDungeonCountDown(Action callback)
	{
		SoundManager.Instance.StopBGM(null);
		float num = float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraTime").value);
		float num2 = (float)InstanceManager.CurrentInstanceData.completeDelayTime;
		float num3 = (num <= num2) ? num2 : num;
		if (num3 > 0f)
		{
			if (num > 0f)
			{
				float ratio = float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraSpeed").value);
				GlobalRatioMgr.Instance.SetGlobalTimeRatio(ratio, (int)num);
			}
			InstanceManager.delayEndingHintTimer = TimerHeap.AddTimer((uint)num3, 0, callback);
		}
		else if (callback != null)
		{
			callback.Invoke();
		}
	}

	public static void DungeonInstanceWin()
	{
		InstanceManager.CheckEndingHint();
	}

	public static void InstanceWin()
	{
		SoundManager.Instance.StopBGM(null);
		float num = float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraTime").value);
		float num2 = (float)InstanceManager.CurrentInstanceData.completeDelayTime;
		float num3 = (num <= num2) ? num2 : num;
		if (num3 > 0f)
		{
			if (num > 0f)
			{
				float ratio = float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraSpeed").value);
				GlobalRatioMgr.Instance.SetGlobalTimeRatio(ratio, (int)num);
			}
			InstanceManager.delayEndingHintTimer = TimerHeap.AddTimer((uint)num3, 0, new Action(InstanceManager.CheckEndingCountdown));
		}
		else
		{
			InstanceManager.CheckEndingCountdown();
		}
	}

	protected static void CheckEndingCountdown()
	{
		InstanceManager.EndingCountdown(delegate
		{
			InstanceManager.EndingCountdownTimesUp();
		});
	}

	protected static void EndingCountdown(Action onCountdownEnd)
	{
		InstanceManager.CurrentInstance.EndingCountdown(onCountdownEnd);
	}

	protected static void EndingCountdownTimesUp()
	{
		InstanceManager.CurrentInstance.EndingCountdownTimesUp();
		InstanceManager.CheckEndingHint();
	}

	protected static void CheckEndingHint()
	{
		if (InstanceManager.CurrentInstanceData.completeMessege > 0)
		{
			InstanceManager.ShowEndingHint(InstanceManager.CurrentInstanceData.completeMessege);
			InstanceManager.endingHintTimer = TimerHeap.AddTimer(3000u, 0, new Action(InstanceManager.ShowEndingHintTimesUp));
		}
		else
		{
			InstanceManager.CheckPlayEndingCG();
		}
	}

	protected static void ShowEndingHint(int textID)
	{
		InstanceManager.CurrentInstance.ShowEndingHint(textID);
	}

	protected static void ShowEndingHintTimesUp()
	{
		InstanceManager.CurrentInstance.ShowEndingHintTimesUp();
		InstanceManager.CheckPlayEndingCG();
	}

	protected static void CheckPlayEndingCG()
	{
		if (InstanceManager.CurrentInstanceData.timeLine > 0)
		{
			InstanceManager.PlayEndingCG(InstanceManager.CurrentInstanceData.timeLine, delegate
			{
				CGCompleteAnnouncer.Announce(InstanceManager.CurrentInstanceData.timeLine);
				InstanceManager.ShowWinUI();
			});
		}
		else
		{
			InstanceManager.ShowWinUI();
		}
	}

	protected static void PlayEndingCG(int timeline, Action onPlayCGEnd)
	{
		InstanceManager.CurrentInstance.PlayEndingCG(timeline, onPlayCGEnd);
	}

	protected static void ShowWinUI()
	{
		InstanceManager.CurrentInstance.HideBattleUIs();
		InstanceManager.CurrentInstance.ShowWinUI();
	}

	public static void InstanceLose()
	{
		SoundManager.Instance.StopBGM(null);
		float num = float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraTime").value);
		if (num > 0f && InstanceManager.CurrentInstance.ShouldSetEndingCamera())
		{
			float ratio = float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraSpeed").value);
			GlobalRatioMgr.Instance.SetGlobalTimeRatio(ratio, (int)num);
			InstanceManager.endingCameraTimer = TimerHeap.AddTimer((uint)num, 0, new Action(InstanceManager.ShowLoseUI));
		}
		else
		{
			InstanceManager.ShowLoseUI();
		}
	}

	protected static void ShowLoseUI()
	{
		InstanceManager.CurrentInstance.HideBattleUIs();
		InstanceManager.CurrentInstance.ShowLoseUI();
	}

	protected static void ExitBattleField(short state, RoleWillLeaveFieldNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (InstanceManager.IsServerBattle)
		{
			Debug.Log("LeaveServerBattle EntitySelf Pos: " + PosDirUtility.ToDetailString(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position()));
		}
		InstanceManager.CurrentInstance.ExitBattleField();
		GuideManager.Instance.StopGuideIfInstance();
		Utils.PauseGame(false);
		FakeDrop.DeleteAllFakeDrop();
		LocalBattleHandler.Instance.ResetData();
		LocalInstanceHandler.Instance.ResetData();
		BattleBlackboard.Instance.ResetData();
		GlobalBattleNetwork.Instance.ResetData();
		EntityWorld.Instance.ClearAllMapObjects();
		InstanceManager.serverBatch = 1;
		BattleTimeManager.Instance.ResetTimeToZero();
		InstanceManager.IsCurrentInBattle = false;
		ReconnectManager.Instance.DataServerReconnectHandler = DataServerCityReconnectHandler.Instance;
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
	}

	public static void PreloadEntityResource()
	{
		if (InstanceManager.IsClientCreate)
		{
			InstanceManager.PreloadClientCreateResource();
		}
		else
		{
			InstanceManager.PreloadServerCreateResource();
		}
	}

	protected static void PreloadClientCreateResource()
	{
		InstanceManager.PreloadClientSelfData();
		InstanceManager.PreloadClientPetData(InstanceManager.CurrentInstance.GetPreloadClientCreatePetIDs());
		InstanceManager.PreloadClientMonsterData(InstanceManager.CurrentInstance.GetPreloadClientCreateMonsterIDs(), true);
	}

	protected static void PreloadServerCreateResource()
	{
		InstanceManager.PreloadServerPlayerData(InstanceManager.CurrentInstance.GetPreloadServerCreatePlayerTypeIDs(), InstanceManager.CurrentInstance.GetPreloadServerCreatePlayerSkillIDs());
		InstanceManager.PreloadServerPetData(InstanceManager.CurrentInstance.GetPreloadServerCreatePetTypeIDs(), InstanceManager.CurrentInstance.GetPreloadServerCreatePetSkillIDs());
		InstanceManager.PreloadServerMonsterData(InstanceManager.CurrentInstance.GetPreloadServerCreateMonsterTypeIDs(), true);
	}

	protected static void PreloadClientSelfData()
	{
		List<int> list = new List<int>();
		list.Add(EntityWorld.Instance.EntSelf.FixModelID);
		InstanceManager.PreloadEntityData(list, EntityWorld.Instance.EntSelf.GetSkillAllValue(), false, false);
	}

	protected static void PreloadClientPetData(List<int> typeIDs)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < typeIDs.get_Count(); i++)
		{
			if (DataReader<Pet>.Contains(typeIDs.get_Item(i)))
			{
				Pet dataPet = DataReader<Pet>.Get(typeIDs.get_Item(i));
				list.Add(PetManager.Instance.GetSelfPetModel(dataPet));
				list2.AddRange(PetManager.Instance.GetSelfPetSkills(dataPet));
			}
		}
		InstanceManager.PreloadEntityData(list, list2, false, false);
	}

	protected static void PreloadClientMonsterData(List<int> typeIDs, bool isCheckSummonMonster = true)
	{
		InstanceManager.PreloadMonsterData(typeIDs, isCheckSummonMonster, false);
	}

	protected static void PreloadServerPlayerData(List<int> typeIDs, List<int> skillIDs)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < typeIDs.get_Count(); i++)
		{
			if (DataReader<RoleCreate>.Contains(typeIDs.get_Item(i)))
			{
				list.Add(DataReader<RoleCreate>.Get(typeIDs.get_Item(i)).modle);
			}
		}
		InstanceManager.PreloadEntityData(list, skillIDs, false, false || SystemConfig.IsForcePreloadCompleteFx);
	}

	protected static void PreloadServerPetData(List<int> typeIDs, List<int> skillIDs)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < typeIDs.get_Count(); i++)
		{
			if (DataReader<Pet>.Contains(typeIDs.get_Item(i)))
			{
				list.AddRange(DataReader<Pet>.Get(typeIDs.get_Item(i)).model);
			}
		}
		InstanceManager.PreloadEntityData(list, skillIDs, false, false || SystemConfig.IsForcePreloadCompleteFx);
	}

	protected static void PreloadServerMonsterData(List<int> typeIDs, bool isCheckSummonMonster = true)
	{
		InstanceManager.PreloadMonsterData(typeIDs, isCheckSummonMonster, false || SystemConfig.IsForcePreloadCompleteFx);
	}

	protected static void PreloadMonsterData(List<int> typeIDs, bool isCheckSummonMonster, bool isLoadCompleteFx)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < typeIDs.get_Count(); i++)
		{
			if (DataReader<Monster>.Contains(typeIDs.get_Item(i)))
			{
				Monster monster = DataReader<Monster>.Get(typeIDs.get_Item(i));
				list.Add(monster.model);
				list2.AddRange(monster.skill);
			}
		}
		InstanceManager.PreloadEntityData(list, list2, isCheckSummonMonster, isLoadCompleteFx);
	}

	protected static void PreloadEntityData(List<int> modelIDs, List<int> skillIDs, bool isCheckSummonMonster, bool isLoadCompleteFx)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < modelIDs.get_Count(); i++)
		{
			LoadingRes.AddPreloadModelID(modelIDs.get_Item(i));
			if (DataReader<AvatarModel>.Contains(modelIDs.get_Item(i)))
			{
				AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelIDs.get_Item(i));
				if (isLoadCompleteFx)
				{
					for (int j = 0; j < avatarModel.diefx.get_Count(); j++)
					{
						LoadingRes.AddPreloadFxID(avatarModel.diefx.get_Item(j));
					}
				}
			}
			if (isLoadCompleteFx)
			{
				HashSet<int> modelOfFXs = ResourceManager.GetModelOfFXs(modelIDs.get_Item(i));
				if (modelOfFXs != null)
				{
					using (HashSet<int>.Enumerator enumerator = modelOfFXs.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int current = enumerator.get_Current();
							LoadingRes.AddPreloadFxID(current);
						}
					}
				}
			}
			HashSet<int> modelOfEffects = ResourceManager.GetModelOfEffects(modelIDs.get_Item(i));
			if (modelOfEffects != null)
			{
				list.AddRange(modelOfEffects);
			}
		}
		InstanceManager.PreloadEntityBattleData(skillIDs, list, false, isLoadCompleteFx);
	}

	protected static void PreloadEntityBattleData(List<int> skillIDs, List<int> effectIDs, bool isCheckSummonMonster, bool isLoadCompleteFx)
	{
		for (int i = 0; i < skillIDs.get_Count(); i++)
		{
			if (DataReader<Skill>.Contains(skillIDs.get_Item(i)))
			{
				effectIDs.AddRange(DataReader<Skill>.Get(skillIDs.get_Item(i)).effect);
			}
		}
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int j = 0; j < effectIDs.get_Count(); j++)
		{
			if (DataReader<Effect>.Contains(effectIDs.get_Item(j)))
			{
				Effect effect = DataReader<Effect>.Get(effectIDs.get_Item(j));
				if (effect.type1 == 3 && effect.monsterId != 0 && !list.Contains(effect.monsterId))
				{
					list.Add(effect.monsterId);
				}
				if (isLoadCompleteFx)
				{
					LoadingRes.AddPreloadFxID(effect.fx);
					LoadingRes.AddPreloadFxID(effect.fxRepeated);
					LoadingRes.AddPreloadFxID(effect.hitFx);
				}
				LoadingRes.AddPreloadFxID(effect.bullet);
				if (effect.addBuff.get_Count() > 0)
				{
					list2.AddRange(effect.addBuff);
				}
				if (effect.addLoopBuff.get_Count() > 0)
				{
					list2.AddRange(effect.addLoopBuff);
				}
				if (effect.gradeAddBuffId.get_Count() > 0)
				{
					for (int k = 0; k < effect.gradeAddBuffId.get_Count(); k++)
					{
						list2.Add(effect.gradeAddBuffId.get_Item(k).value);
					}
				}
				if (effect.gradeAddLoopBuffId.get_Count() > 0)
				{
					for (int l = 0; l < effect.gradeAddLoopBuffId.get_Count(); l++)
					{
						list2.Add(effect.gradeAddLoopBuffId.get_Item(l).value);
					}
				}
			}
		}
		if (isLoadCompleteFx)
		{
			for (int m = 0; m < list2.get_Count(); m++)
			{
				if (DataReader<Buff>.Contains(list2.get_Item(m)))
				{
					Buff buff = DataReader<Buff>.Get(list2.get_Item(m));
					for (int n = 0; n < buff.fx.get_Count(); n++)
					{
						LoadingRes.AddPreloadFxID(buff.fx.get_Item(n));
					}
				}
			}
		}
		if (isCheckSummonMonster)
		{
			InstanceManager.PreloadMonsterData(list, false, isLoadCompleteFx);
		}
	}

	public static void PreloadGroceries()
	{
	}

	protected static void PreloadCommonFx()
	{
		List<int> preloadCommonFxIDs = InstanceManager.CurrentInstance.GetPreloadCommonFxIDs();
		for (int i = 0; i < preloadCommonFxIDs.get_Count(); i++)
		{
			LoadingRes.AddPreloadFxID(preloadCommonFxIDs.get_Item(i));
		}
	}

	protected static void PreloadCGData()
	{
		List<int> preloadCGModelIDs = InstanceManager.CurrentInstance.GetPreloadCGModelIDs();
		if (preloadCGModelIDs != null && preloadCGModelIDs.get_Count() > 0)
		{
			for (int i = 0; i < preloadCGModelIDs.get_Count(); i++)
			{
				LoadingRes.AddPreloadModelID(preloadCGModelIDs.get_Item(i));
				HashSet<int> modelOfFXs = ResourceManager.GetModelOfFXs(preloadCGModelIDs.get_Item(i));
				if (modelOfFXs != null)
				{
					using (HashSet<int>.Enumerator enumerator = modelOfFXs.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int current = enumerator.get_Current();
							LoadingRes.AddPreloadFxID(current);
						}
					}
				}
			}
		}
		List<int> preloadCGComicIDs = InstanceManager.CurrentInstance.GetPreloadCGComicIDs();
		if (preloadCGComicIDs != null && preloadCGComicIDs.get_Count() > 0)
		{
			for (int j = 0; j < preloadCGComicIDs.get_Count(); j++)
			{
				LoadingRes.AddPreloadFxID(preloadCGComicIDs.get_Item(j));
			}
		}
	}

	public static void SimulateEnterField(int logicType, List<int> globalBuffIDs)
	{
		RoleEnteredFieldNty roleEnteredFieldNty = new RoleEnteredFieldNty();
		roleEnteredFieldNty.type = BattleFieldT.BFT.FrontEnd;
		roleEnteredFieldNty.logicType = (BattleFieldLogicType.BFLT)logicType;
		roleEnteredFieldNty.cliDrvBattleRandSeed = (long)Random.Range(0, 10000);
		roleEnteredFieldNty.cliDrvBattleRandLen = 0;
		if (globalBuffIDs != null)
		{
			roleEnteredFieldNty.buffIds.AddRange(globalBuffIDs);
		}
		InstanceManager.EnterBattleField(0, roleEnteredFieldNty);
	}

	public static void SimulateSwicthMap(int sceneID, MapObjInfo selfInfo = null, List<MapObjInfo> otherInfo = null, int selfClientModelID = 0)
	{
		SwitchMapRes switchMapRes = new SwitchMapRes();
		switchMapRes.newMapId = MySceneManager.Instance.CurSceneID;
		switchMapRes.newMapId = sceneID;
		switchMapRes.mapLayer = 0;
		if (selfInfo != null)
		{
			switchMapRes.selfObj = selfInfo;
		}
		if (otherInfo != null)
		{
			switchMapRes.otherObjs.AddRange(otherInfo);
		}
		switchMapRes.transformId = selfClientModelID;
		MySceneManager.Instance.SwitchMapResp(0, switchMapRes);
	}

	public static void SimulateExitField()
	{
		InstanceManager.ExitBattleField(0, new RoleWillLeaveFieldNty());
	}

	public static bool GetCurrentBattlePathPoint(out Vector2 position)
	{
		if (InstanceManager.CurrentInstanceData == null)
		{
			position = Vector2.get_zero();
			return false;
		}
		if (InstanceManager.CurrentInstanceBatch <= 0)
		{
			position = Vector2.get_zero();
			return false;
		}
		if (InstanceManager.CurrentInstanceData.refreshId.get_Count() < InstanceManager.CurrentInstanceBatch)
		{
			position = Vector2.get_zero();
			return false;
		}
		if (!MySceneManager.Instance.IsSceneExist)
		{
			position = Vector2.get_zero();
			return false;
		}
		BoCiBiao boCiBiao = DataReader<BoCiBiao>.Get(InstanceManager.CurrentInstanceData.refreshId.get_Item(InstanceManager.CurrentInstanceBatch - 1));
		if (boCiBiao == null)
		{
			position = Vector2.get_zero();
			return false;
		}
		return MapDataManager.Instance.GetPathPointByBornPoint(MySceneManager.Instance.CurSceneID, boCiBiao.pathPoint, out position);
	}

	public static bool GetCurrentMarkPoint(out Vector2 position)
	{
		if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.Actor && EntityWorld.Instance.LockOnTarget != null && EntityWorld.Instance.LockOnTarget.Actor)
		{
			position = new Vector2(EntityWorld.Instance.LockOnTarget.Actor.FixTransform.get_position().x, EntityWorld.Instance.LockOnTarget.Actor.FixTransform.get_position().z);
			return true;
		}
		return InstanceManager.GetCurrentBattlePathPoint(out position);
	}

	public static void QueryBattleSituation(BattleHurtInfoType type, Action<XDict<long, BattleSituationInfo>> callBack = null)
	{
		if (callBack != null)
		{
			if (InstanceManager.queryBattleSituationCallBack.ContainsKey(type))
			{
				InstanceManager.queryBattleSituationCallBack[type] = callBack;
			}
			else
			{
				InstanceManager.queryBattleSituationCallBack.Add(type, callBack);
			}
		}
		NetworkManager.Send(new QueryBattleInfoReq
		{
			hurtInfoType = type,
			needRoleName = 1
		}, ServerType.Data);
	}

	protected static void SetBattleSituation(short state, QueryBattleInfoRes down = null)
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
		XDict<long, BattleSituationInfo> xDict = null;
		switch (down.hurtInfoType)
		{
		case BattleHurtInfoType.RoleMakeBossHurt:
			xDict = InstanceManager.bossHurtTable;
			break;
		case BattleHurtInfoType.RoleBeBossHurt:
			xDict = InstanceManager.playerHurtTable;
			break;
		case BattleHurtInfoType.RoleBePkHurt:
			xDict = InstanceManager.playerHurtFromPlayerTable;
			break;
		case BattleHurtInfoType.RoleMakeTotalHurt:
			xDict = InstanceManager.playerDamageTable;
			break;
		}
		if (xDict == null)
		{
			return;
		}
		InstanceManager.UpdateBattleSituation(xDict, down.info);
		if (InstanceManager.queryBattleSituationCallBack.ContainsKey(down.hurtInfoType))
		{
			InstanceManager.queryBattleSituationCallBack[down.hurtInfoType].Invoke(xDict);
			InstanceManager.queryBattleSituationCallBack.Remove(down.hurtInfoType);
		}
	}

	protected static void UpdateBattleSituation(XDict<long, BattleSituationInfo> updateTable, List<QueryBattleInfoRes.HurtInfo> newInfo)
	{
		updateTable.Clear();
		for (int i = 0; i < newInfo.get_Count(); i++)
		{
			updateTable.Add(newInfo.get_Item(i).roleId, new BattleSituationInfo
			{
				id = newInfo.get_Item(i).roleId,
				name = newInfo.get_Item(i).roleName,
				num = newInfo.get_Item(i).totalHurt
			});
		}
	}

	public static float GetPlayerBossDamagePercentage(long playerID)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < InstanceManager.bossHurtTable.Count; i++)
		{
			if (InstanceManager.bossHurtTable.ElementKeyAt(i) == playerID)
			{
				num += (float)InstanceManager.bossHurtTable.ElementValueAt(i).num;
			}
			else
			{
				num2 += (float)InstanceManager.bossHurtTable.ElementValueAt(i).num;
			}
		}
		if (num == 0f && num2 == 0f)
		{
			return 0f;
		}
		if (num == 0f)
		{
			return -1000000f;
		}
		return (num - num2) / num;
	}

	public static float GetPlayerFramedPercentage(long playerID)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < InstanceManager.playerHurtTable.Count; i++)
		{
			if (InstanceManager.playerHurtTable.ElementKeyAt(i) == playerID)
			{
				num = (float)InstanceManager.playerHurtTable.ElementValueAt(i).num;
				break;
			}
		}
		if (num == 0f)
		{
			return 0f;
		}
		for (int j = 0; j < InstanceManager.playerHurtFromPlayerTable.Count; j++)
		{
			if (InstanceManager.playerHurtFromPlayerTable.ElementKeyAt(j) == playerID)
			{
				num2 = (float)InstanceManager.playerHurtFromPlayerTable.ElementValueAt(j).num;
				break;
			}
		}
		return num2 / num;
	}

	public static Vector2 GetMonsterFixBornDirection(int fixType, Vector3 position, long ownerID, int scenePoint)
	{
		Vector2 vector = new Vector2();
		switch (fixType)
		{
		case 1:
			if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.Actor)
			{
				vector.x = EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position().x - position.x;
				vector.y = EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position().z - position.z;
			}
			else
			{
				vector.x = 0f;
				vector.y = 1f;
			}
			break;
		case 2:
		{
			Vector2 point = MapDataManager.Instance.GetPoint(MySceneManager.Instance.CurSceneID, InstanceManager.CurrentInstanceData.pointId);
			vector.x = point.x * 0.01f - position.x;
			vector.y = point.y * 0.01f - position.z;
			break;
		}
		case 3:
		{
			float value = Random.get_value();
			vector.x = value * (float)((Random.Range(0, 2) != 0) ? 1 : -1);
			vector.y = Mathf.Sqrt(1f - value * value) * (float)((Random.Range(0, 2) != 0) ? 1 : -1);
			break;
		}
		case 4:
		{
			Vector2 point2 = MapDataManager.Instance.GetPoint(MySceneManager.Instance.CurSceneID, scenePoint);
			vector.x = point2.x * 0.01f - position.x;
			vector.y = point2.y * 0.01f - position.z;
			break;
		}
		case 5:
		{
			EntityParent entityByID = EntityWorld.Instance.GetEntityByID(ownerID);
			if (entityByID != null && entityByID.Actor)
			{
				vector.x = entityByID.Actor.FixTransform.get_position().x - position.x;
				vector.y = entityByID.Actor.FixTransform.get_position().z - position.z;
			}
			else
			{
				EntityWorld.Instance.ForceOut("根本没有属主", string.Format("根本找不到属主: {0}", ownerID), null);
			}
			break;
		}
		default:
			vector.x = 0f;
			vector.y = 1f;
			break;
		}
		return vector;
	}

	public static bool IsShowMonsterBorn(int monsterID)
	{
		return InstanceManager.CurrentInstance.IsShowMonsterBorn(monsterID);
	}

	public static bool IsShowPlayerAimMark(bool isSameCamp)
	{
		return InstanceManager.CurrentInstance.IsShowPlayerAimMark(isSameCamp);
	}

	public static bool IsShowPetAimMark(bool isSameCamp)
	{
		return InstanceManager.CurrentInstance.IsShowPetAimMark(isSameCamp);
	}

	public static void PauseAllClientAI()
	{
		InstanceManager.IsAIThinking = false;
	}

	public static void ResumeAllClientAI()
	{
		InstanceManager.IsAIThinking = true;
	}

	public static void StopAllClientAI(bool isPauseSelfAICheck = true)
	{
		switch (InstanceManager.CurrentCommunicationType)
		{
		case CommunicationType.Client:
		case CommunicationType.Mixed:
			if (isPauseSelfAICheck)
			{
				InstanceManager.PauseSelfAICheckAndDeactiveSelfAI();
			}
			else
			{
				InstanceManager.DeactiveSelfAI();
			}
			InstanceManager.DeactiveAllPlayerAI();
			InstanceManager.DeactiveAllMonsterAI();
			InstanceManager.DeactiveAllPetAI();
			break;
		case CommunicationType.Server:
			if (isPauseSelfAICheck)
			{
				InstanceManager.PauseSelfAICheckAndDeactiveSelfAI();
			}
			else
			{
				InstanceManager.DeactiveSelfAI();
			}
			break;
		}
	}

	protected static void DeactiveSelfAI()
	{
		EventDispatcher.Broadcast(AIManagerEvent.SelfAIDeactive);
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("BattleUI");
		if (uIIfExist == null)
		{
			return;
		}
		(uIIfExist as BattleUI).IsInAuto = false;
	}

	protected static void PauseSelfAICheckAndDeactiveSelfAI()
	{
		EventDispatcher.Broadcast(AIManagerEvent.SelfAIDeactive);
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("BattleUI");
		if (uIIfExist == null)
		{
			return;
		}
		BattleUI battleUI = uIIfExist as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.IsInAuto = false;
		battleUI.IsPauseCheck = true;
	}

	protected static void ActiveAllPlayerAI()
	{
		InstanceManager.ActiveEntityListAI<EntityPlayer>();
	}

	protected static void ActiveAllMonsterAI()
	{
		InstanceManager.ActiveEntityListAI<EntityMonster>();
	}

	protected void ActiveAllPetAI()
	{
		InstanceManager.ActiveEntityListAI<EntityPet>();
	}

	protected static void DeactiveAllPlayerAI()
	{
		InstanceManager.DeactiveEntityListAI<EntityPlayer>();
	}

	protected static void DeactiveAllMonsterAI()
	{
		InstanceManager.DeactiveEntityListAI<EntityMonster>();
	}

	protected static void DeactiveAllPetAI()
	{
		InstanceManager.DeactiveEntityListAI<EntityPet>();
	}

	protected static void ActiveEntityListAI<T>() where T : EntityParent
	{
		List<EntityParent> list = new List<EntityParent>();
		List<EntityParent> values = EntityWorld.Instance.GetEntities<T>().Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (values.get_Item(i) != null)
			{
				list.Add(values.get_Item(i));
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			if (list.get_Item(j).IsClientDominate)
			{
				list.get_Item(j).GetAIManager().Active();
			}
		}
	}

	protected static void DeactiveEntityListAI<T>() where T : EntityParent
	{
		List<EntityParent> list = new List<EntityParent>();
		List<EntityParent> values = EntityWorld.Instance.GetEntities<T>().Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (values.get_Item(i) != null)
			{
				list.Add(values.get_Item(i));
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			if (list.get_Item(j).IsClientDominate)
			{
				list.get_Item(j).GetAIManager().Deactive();
				if (list.get_Item(j).Actor)
				{
					list.get_Item(j).Actor.StopAIMove();
				}
			}
		}
	}

	protected static void SetAnimtorsOfBattle(bool isOn)
	{
		InstanceManager.SetAnimtorsOfBattle<EntitySelf>(isOn);
		InstanceManager.SetAnimtorsOfBattle<EntityPlayer>(isOn);
		InstanceManager.SetAnimtorsOfBattle<EntityPet>(isOn);
		InstanceManager.SetAnimtorsOfBattle<EntityMonster>(isOn);
	}

	protected static void SetAnimtorsOfBattle<T>(bool isOn) where T : EntityParent
	{
		List<EntityParent> values = EntityWorld.Instance.GetEntities<T>().Values;
		Debug.Log(string.Concat(new object[]
		{
			"SetAnimtorsOfBattle: ",
			typeof(T).get_Name(),
			" ",
			values.get_Count(),
			" ",
			isOn
		}));
		int i;
		for (i = 0; i < values.get_Count(); i++)
		{
			if (values.get_Item(i).Actor && values.get_Item(i).Actor.FixAnimator)
			{
				values.get_Item(i).Actor.FixAnimator.set_enabled(isOn);
			}
		}
		Debug.Log(string.Concat(new object[]
		{
			"Finally SetAnimtorsOfBattle: ",
			typeof(T).get_Name(),
			" ",
			i,
			" ",
			isOn
		}));
	}

	protected static void OnBattleDropItemInfoNty(short state, BattleDropItemInfoNty down = null)
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
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (down.roleId != EntityWorld.Instance.EntSelf.ID)
		{
			return;
		}
		InstanceManager.SetRealTimeDrop(down.dropItemInfo);
	}

	protected static void SetRealTimeDrop(List<BattleDropItemInfoNty.DropItemInfo> updateList)
	{
		XDict<int, long> xDict = new XDict<int, long>();
		for (int i = 0; i < updateList.get_Count(); i++)
		{
			int operatorType = updateList.get_Item(i).operatorType;
			if (operatorType != 1)
			{
				if (operatorType == 2)
				{
					if (InstanceManager.realTimeDropCache.ContainsKey(updateList.get_Item(i).dropItems.typeId))
					{
						InstanceManager.realTimeDropCache[updateList.get_Item(i).dropItems.typeId] = ((InstanceManager.realTimeDropCache[updateList.get_Item(i).dropItems.typeId] - updateList.get_Item(i).dropItems.count >= 0L) ? (InstanceManager.realTimeDropCache[updateList.get_Item(i).dropItems.typeId] - updateList.get_Item(i).dropItems.count) : 0L);
					}
				}
			}
			else
			{
				if (xDict.ContainsKey(updateList.get_Item(i).dropItems.typeId))
				{
					XDict<int, long> xDict2;
					XDict<int, long> expr_4A = xDict2 = xDict;
					int typeId;
					int expr_5D = typeId = updateList.get_Item(i).dropItems.typeId;
					long num = xDict2[typeId];
					expr_4A[expr_5D] = num + updateList.get_Item(i).dropItems.count;
				}
				else
				{
					xDict.Add(updateList.get_Item(i).dropItems.typeId, updateList.get_Item(i).dropItems.count);
				}
				if (InstanceManager.realTimeDropCache.ContainsKey(updateList.get_Item(i).dropItems.typeId))
				{
					XDict<int, long> xDict3;
					XDict<int, long> expr_D5 = xDict3 = InstanceManager.realTimeDropCache;
					int typeId;
					int expr_E9 = typeId = updateList.get_Item(i).dropItems.typeId;
					long num = xDict3[typeId];
					expr_D5[expr_E9] = num + updateList.get_Item(i).dropItems.count;
				}
				else
				{
					InstanceManager.realTimeDropCache.Add(updateList.get_Item(i).dropItems.typeId, updateList.get_Item(i).dropItems.count);
				}
			}
		}
		InstanceManager.CurrentInstance.GetNewRealTimeDrop(xDict);
		InstanceManager.CurrentInstance.UpdateRealTimeDrop(InstanceManager.realTimeDropCache);
	}

	public static void SetInstanceDropPreData(int typeID, int modelID, Transform transform, float minRange, float maxRange, float angle)
	{
		InstanceManager.InstanceDropMonsterTypeID = typeID;
		InstanceManager.InstanceDropMonsterModelID = modelID;
		InstanceManager.InstanceDropOriginPosition = transform.get_position();
		InstanceManager.instanceDropWaitPosition.Clear();
		InstanceManager.instanceDropWaitPosition.AddRange(InstanceManager.GetDropWaitPosition(transform, minRange, maxRange, angle));
	}

	public static void SetInstanceDrop(List<KeyValuePair<int, long>> dropID)
	{
		if (!InstanceManager.IsShowInstanceDrop)
		{
			return;
		}
		if (InstanceManager.instanceDropWaitPosition.get_Count() == 0)
		{
			return;
		}
		InstanceManager.CreateDrop(InstanceManager.InstanceDropMonsterTypeID, InstanceManager.InstanceDropOriginPosition, InstanceManager.instanceDropWaitPosition, InstanceManager.GetRandomIndex(0, InstanceManager.instanceDropWaitPosition.get_Count()), dropID);
	}

	public static void SetMonsterDrop(int monsterTypeID, Transform transform, float minRange, float maxRange, float angle)
	{
		if (!InstanceManager.IsShowMonsterDrop)
		{
			return;
		}
		List<Vector3> dropWaitPosition = InstanceManager.GetDropWaitPosition(transform, minRange, maxRange, angle);
		if (dropWaitPosition.get_Count() == 0)
		{
			return;
		}
		InstanceManager.CreateGoldDrop(monsterTypeID, transform.get_position(), dropWaitPosition, InstanceManager.GetRandomIndex(0, dropWaitPosition.get_Count()), 0);
	}

	protected static List<Vector3> GetDropWaitPosition(Transform transform, float minRange, float maxRange, float angle)
	{
		List<Vector3> list = new List<Vector3>();
		if (angle == 0f)
		{
			return list;
		}
		for (float num = 0f; num < 360f; num += angle)
		{
			float num2 = (minRange <= maxRange) ? (minRange + Random.get_value() * (maxRange - minRange)) : maxRange;
			Vector3 vector;
			if (num == 0f)
			{
				vector = transform.get_position() + transform.get_forward() * num2;
			}
			else
			{
				vector = transform.get_position() + Quaternion.Euler(transform.get_eulerAngles().x, transform.get_eulerAngles().y + num, transform.get_eulerAngles().z) * Vector3.get_forward() * num2;
			}
			list.Add(vector);
		}
		return list;
	}

	protected static void CreateDrop(int monsterTypeID, Vector3 originPosition, List<Vector3> waitPosition, List<int> indexes, List<KeyValuePair<int, long>> itemID)
	{
		if (itemID == null)
		{
			return;
		}
		if (itemID.get_Count() == 0)
		{
			return;
		}
		bool flag = false;
		int num = 0;
		for (int i = 0; i < itemID.get_Count(); i++)
		{
			if (itemID.get_Item(i).get_Value() != 0L)
			{
				if (itemID.get_Item(i).get_Key() == 2)
				{
					flag = true;
				}
				else
				{
					Items items = DataReader<Items>.Get(itemID.get_Item(i).get_Key());
					if (items != null)
					{
						if (items.modelId != 0)
						{
							int num2 = 0;
							while ((long)num2 < itemID.get_Item(i).get_Value())
							{
								if (InstanceManager.CreateItemDrop(itemID.get_Item(i).get_Key(), originPosition, waitPosition.get_Item(indexes.get_Item(num % indexes.get_Count()))))
								{
									num++;
								}
								num2++;
							}
						}
					}
				}
			}
		}
		if (flag)
		{
			InstanceManager.CreateGoldDrop(monsterTypeID, originPosition, waitPosition, indexes, num);
		}
	}

	protected static int CreateGoldDrop(int monsterTypeID, Vector3 originPosition, List<Vector3> waitPosition, List<int> indexes, int beginIndex)
	{
		List<int> golds = DataReader<Monster>.Get(monsterTypeID).golds;
		if (golds.get_Count() == 0)
		{
			return beginIndex;
		}
		JinBiDiaoLuoBiao jinBiDiaoLuoBiao = DataReader<JinBiDiaoLuoBiao>.Get(golds.get_Item(Random.Range(0, golds.get_Count())));
		if (jinBiDiaoLuoBiao == null)
		{
			return beginIndex;
		}
		int goldDropNum = InstanceManager.GetGoldDropNum(jinBiDiaoLuoBiao.num1);
		int goldDropNum2 = InstanceManager.GetGoldDropNum(jinBiDiaoLuoBiao.num2);
		int num = 0;
		if (goldDropNum > 0)
		{
			for (int i = 0; i < goldDropNum; i++)
			{
				InstanceManager.CreateGoldDrop(jinBiDiaoLuoBiao.modelId1, originPosition, waitPosition.get_Item(indexes.get_Item((beginIndex + num) % indexes.get_Count())));
				num++;
			}
		}
		if (goldDropNum2 > 0)
		{
			for (int j = 0; j < goldDropNum2; j++)
			{
				InstanceManager.CreateGoldDrop(jinBiDiaoLuoBiao.modelId2, originPosition, waitPosition.get_Item(indexes.get_Item((beginIndex + num) % indexes.get_Count())));
				num++;
			}
		}
		return num;
	}

	protected static int GetGoldDropNum(List<int> number)
	{
		if (number.get_Count() == 1)
		{
			return number.get_Item(0);
		}
		if (number.get_Count() >= 2)
		{
			return Random.Range(number.get_Item(0), number.get_Item(1) + 1);
		}
		return 0;
	}

	protected static void CreateGoldDrop(int modelID, Vector3 originPosition, Vector3 waitPosition)
	{
		FakeDrop.CreateFakeDrop(modelID, originPosition, waitPosition, 3109, 3107);
	}

	protected static bool CreateItemDrop(int itemID, Vector3 originPosition, Vector3 waitPosition)
	{
		if (!DataReader<Items>.Contains(itemID))
		{
			return false;
		}
		Items items = DataReader<Items>.Get(itemID);
		if (items.modelId == 0)
		{
			return false;
		}
		FakeDrop.CreateFakeDrop(items.modelId, originPosition, waitPosition, InstanceManager.GetDropWaitFxID(items.color), InstanceManager.GetDropFlyToEntityFxID(items.color));
		return true;
	}

	protected static bool CreateItemDrop(int index, int itemID, Vector3 originPosition, Vector3 waitPosition)
	{
		if (!DataReader<Items>.Contains(itemID))
		{
			return false;
		}
		Items items = DataReader<Items>.Get(itemID);
		if (items.modelId == 0)
		{
			return false;
		}
		RealDrop.CreateRealDrop(index, items.modelId, GameDataUtils.GetItemName(itemID, true, 0L), originPosition, waitPosition, InstanceManager.GetDropWaitFxID(items.color), 0);
		return true;
	}

	protected static int GetDropWaitFxID(int color)
	{
		switch (color)
		{
		case 2:
			return 3102;
		case 3:
			return 3103;
		case 4:
			return 3104;
		case 5:
			return 3105;
		case 6:
			return 3106;
		default:
			return 3101;
		}
	}

	protected static int GetDropFlyToEntityFxID(int color)
	{
		return 3107;
	}

	protected static List<int> GetRandomIndex(int num)
	{
		return InstanceManager.GetRandomIndex(0, num);
	}

	protected static List<int> GetRandomIndex(int min, int max)
	{
		List<int> list = new List<int>();
		if (min >= max)
		{
			return list;
		}
		for (int i = min; i < max; i++)
		{
			list.Add(i);
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			int num = Random.Range(0, max);
			int num2 = list.get_Item(j);
			list.set_Item(j, list.get_Item(num));
			list.set_Item(num, num2);
		}
		return list;
	}

	public static void SetCollectCountDown(int time)
	{
		Debug.LogError("SetCollectCountDown: " + time);
	}

	protected static void OnBattleCollectItemInfoAddNty(short state, BattleCollectItemAddNty down = null)
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
		for (int i = 0; i < down.infoList.get_Count(); i++)
		{
			InstanceManager.CreateItemDrop(down.infoList.get_Item(i).idx, down.infoList.get_Item(i).dropItems.typeId, InstanceManager.InstanceDropOriginPosition, PosDirUtility.ToTerrainPoint(down.infoList.get_Item(i).pos, 0f));
		}
	}

	protected static void OnBattleCollectItemInfoRemoveNty(short state, BattleCollectItemRemoveNty down = null)
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
		RealDrop.DeleteRealDrop(down.removeIdxList);
	}

	public static void CollectDrop(int index)
	{
		NetworkManager.Send(new BattlePickCollectionReq
		{
			pickIdx = index
		}, ServerType.Data);
	}

	protected static void OnCollectDropRes(short state, BattlePickCollectionRes down = null)
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
	}

	public static void TryPause()
	{
	}

	public static void TryPauseSuccess()
	{
		Utils.PauseGame(true);
	}

	public static void TryPauseFailed()
	{
		switch (InstanceManager.CurrentCommunicationType)
		{
		case CommunicationType.Client:
			InstanceManager.SetLocalLogicResume();
			break;
		case CommunicationType.Server:
			break;
		case CommunicationType.Mixed:
			InstanceManager.SetLocalLogicResume();
			break;
		default:
			Utils.PauseGame(false);
			break;
		}
	}

	public static void TryResume()
	{
	}

	public static void TryResumeSuccess()
	{
		switch (InstanceManager.CurrentCommunicationType)
		{
		case CommunicationType.Client:
			InstanceManager.SetLocalLogicResume();
			break;
		case CommunicationType.Mixed:
			InstanceManager.SetLocalLogicResume();
			break;
		}
	}

	public static void TryResumeFailed()
	{
		Utils.PauseGame(true);
	}

	protected static void SetLocalLogicPause()
	{
		LocalBattleHandler.Instance.SetLogicPause();
	}

	protected static void SetLocalLogicResume()
	{
		LocalBattleHandler.Instance.SetLogicResume();
	}

	protected static void ServerPauseFieldRes(short state, PauseFieldRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state == 0)
		{
			InstanceManager.TryPauseSuccess();
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
			InstanceManager.TryPauseFailed();
		}
	}

	protected static void ServerResumeFieldRes(short state, ResumeFieldRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state == 0)
		{
			LocalBattleHandler.Instance.SetLogicResume();
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
			InstanceManager.TryResumeFailed();
		}
	}

	protected static void ResendCameraEvent()
	{
		for (int i = 0; i < EntityWorld.Instance.AllEntities.Values.get_Count(); i++)
		{
			if (!EntityWorld.Instance.AllEntities.Values.get_Item(i).IsPlayerMate)
			{
				if (EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor)
				{
					if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsEntityPlayerType)
					{
						EventDispatcher.Broadcast<int, Transform>(CameraEvent.PlayerBorn, EntityWorld.Instance.AllEntities.Values.get_Item(i).TypeID, EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.FixTransform);
					}
					else if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsEntityMonsterType)
					{
						EventDispatcher.Broadcast<int, Transform>(CameraEvent.MonsterBorn, EntityWorld.Instance.AllEntities.Values.get_Item(i).TypeID, EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.FixTransform);
					}
				}
			}
		}
	}

	public static void ResetCameraEvent()
	{
		for (int i = 0; i < EntityWorld.Instance.AllEntities.Values.get_Count(); i++)
		{
			if (EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor)
			{
				if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsPlayerMate)
				{
					if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsEntityPlayerType)
					{
						EventDispatcher.Broadcast<Transform>(CameraEvent.PlayerDie, EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.FixTransform);
					}
					else if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsEntityMonsterType)
					{
						EventDispatcher.Broadcast<Transform>(CameraEvent.MonsterDie, EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.FixTransform);
					}
				}
				else if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsEntityPlayerType)
				{
					EventDispatcher.Broadcast<int, Transform>(CameraEvent.PlayerBorn, EntityWorld.Instance.AllEntities.Values.get_Item(i).TypeID, EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.FixTransform);
				}
				else if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsEntityMonsterType)
				{
					EventDispatcher.Broadcast<int, Transform>(CameraEvent.MonsterBorn, EntityWorld.Instance.AllEntities.Values.get_Item(i).TypeID, EntityWorld.Instance.AllEntities.Values.get_Item(i).Actor.FixTransform);
				}
			}
		}
	}

	public static void GlobalRelive()
	{
		NetworkManager.Send(new BattleReliveReq(), ServerType.Data);
	}

	protected static void OnReliveRes(short state, BattleReliveRes down = null)
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
	}

	public static void GlobalGiveUpRelive()
	{
		InstanceManager.CurrentInstance.GiveUpRelive();
	}

	public static void RegisterSecurityCheck(Func<Action, Action, bool> passAction)
	{
		InstanceManager.securityCheckAction.Add(passAction);
	}

	public static void SecurityCheck(Action passAction, Action blockAndCancelAction = null)
	{
		InstanceManager.IsInSecurityCheck = true;
		InstanceManager.SecurityCheckPassAction = passAction;
		InstanceManager.SecurityCheckBlockAndCancelAction = blockAndCancelAction;
		for (int i = 0; i < InstanceManager.securityCheckAction.get_Count(); i++)
		{
			if (!InstanceManager.securityCheckAction.get_Item(i).Invoke(passAction, delegate
			{
				InstanceManager.IsInSecurityCheck = false;
				if (blockAndCancelAction != null)
				{
					blockAndCancelAction.Invoke();
				}
			}))
			{
				return;
			}
		}
		InstanceManager.IsInSecurityCheck = false;
		if (passAction != null)
		{
			passAction.Invoke();
		}
	}

	public static void ContinueSecurityCheck()
	{
		InstanceManager.SecurityCheck(InstanceManager.SecurityCheckPassAction, InstanceManager.SecurityCheckBlockAndCancelAction);
	}
}
