using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class LocalInstanceHandler : ILocalDimension
{
	protected LocalDimensionSpirit selfSpirit = new LocalDimensionSpirit();

	protected XDict<long, LocalDimensionSpirit> playerSpirit = new XDict<long, LocalDimensionSpirit>();

	protected XDict<long, XDict<int, LocalDimensionPetSpirit>> petSpirit = new XDict<long, XDict<int, LocalDimensionPetSpirit>>();

	protected XDict<long, LocalDimensionSpirit> monsterSpirit = new XDict<long, LocalDimensionSpirit>();

	protected static LocalInstanceHandler instance;

	protected bool hasInit;

	protected bool isEnable;

	protected int poolID = 1;

	protected int instanceID;

	protected int sceneID;

	protected bool isPauseTimeEscape;

	protected float totalDeltaTime;

	protected SortedDictionary<int, List<int>> spawnPointCount = new SortedDictionary<int, List<int>>();

	protected int currentBatch = 1;

	protected int maxBatches = 3;

	protected int currentBatchMinMass;

	protected XDict<int, List<LocalMonsterGenerator>> monsterGenerator = new XDict<int, List<LocalMonsterGenerator>>();

	protected List<long> sleepingFriendlyNPC = new List<long>();

	protected List<long> sleepingBoss = new List<long>();

	protected List<uint> startGenerateTimerList = new List<uint>();

	protected List<uint> finishGenerateTimerList = new List<uint>();

	protected List<uint> waitingGenerateTimerList = new List<uint>();

	protected int waitingGenerateCount;

	protected XDict<LocalDimensionRangeChecker, LocalMonsterGenerator> generatorRangeChecker = new XDict<LocalDimensionRangeChecker, LocalMonsterGenerator>();

	protected List<KeyValuePair<LocalMonsterGenerateMaterial, MonsterRefresh>> monsterGeneratorBlockList = new List<KeyValuePair<LocalMonsterGenerateMaterial, MonsterRefresh>>();

	protected XDict<long, MonsterRefresh> aliveFriendlyMonsterInBatch = new XDict<long, MonsterRefresh>();

	protected XDict<long, MonsterRefresh> aliveFriendlyMonsterOutOfBatch = new XDict<long, MonsterRefresh>();

	protected XDict<long, MonsterRefresh> aliveEnemyMonsterInBatch = new XDict<long, MonsterRefresh>();

	protected XDict<long, MonsterRefresh> aliveEnemyMonsterOutOfBatch = new XDict<long, MonsterRefresh>();

	public List<long> aliveFriendlyNPC = new List<long>();

	public List<long> aliveBoss = new List<long>();

	protected List<long> summonMonsterList = new List<long>();

	protected int currentMonsterMass;

	protected int maxMonsterMass;

	protected bool isBossDieEnd;

	protected List<int> ghostMonsterTypeID = new List<int>();

	protected List<int> ghostMonsterLevel = new List<int>();

	protected List<int> ghostMonsterSpawnPoint = new List<int>();

	protected List<int> winCondition = new List<int>();

	protected List<int> loseCondition = new List<int>();

	protected int localTime;

	protected int timeOutLimit;

	protected int finishTimeLimit;

	protected LocalDimensionRangeChecker finishRangeChecker;

	protected KillCountDownType killCountDownType;

	protected int finishTotalKillCountDown;

	protected List<int> finishKillID = new List<int>();

	protected List<int> finishKillNum = new List<int>();

	protected bool isStart;

	protected bool isFinished;

	public static LocalInstanceHandler Instance
	{
		get
		{
			if (LocalInstanceHandler.instance == null)
			{
				LocalInstanceHandler.instance = new LocalInstanceHandler();
			}
			return LocalInstanceHandler.instance;
		}
	}

	protected bool HasInit
	{
		get
		{
			return this.hasInit;
		}
		set
		{
			this.hasInit = value;
		}
	}

	protected bool IsEnable
	{
		get
		{
			return this.isEnable;
		}
		set
		{
			this.isEnable = value;
		}
	}

	public int PoolID
	{
		get
		{
			return this.poolID++;
		}
	}

	public int InstanceID
	{
		get
		{
			return this.instanceID;
		}
		set
		{
			this.instanceID = value;
		}
	}

	public int SceneID
	{
		get
		{
			return this.sceneID;
		}
		set
		{
			this.sceneID = value;
		}
	}

	public bool IsPauseTimeEscape
	{
		get
		{
			return this.isPauseTimeEscape;
		}
		set
		{
			this.isPauseTimeEscape = value;
		}
	}

	public SortedDictionary<int, List<int>> SpawnPointCount
	{
		get
		{
			return this.spawnPointCount;
		}
		set
		{
			this.spawnPointCount = value;
		}
	}

	public int CurrentBatch
	{
		get
		{
			return this.currentBatch;
		}
		set
		{
			if (this.currentBatch != value)
			{
				EventDispatcher.Broadcast<int, EntityMonster, string>("BattleDialogTrigger", 10, null, value.ToString());
			}
			this.currentBatch = value;
			Debug.Log("CurrentBatch: " + this.currentBatch);
			if (this.monsterGenerator.ContainsKey(value))
			{
				for (int i = 0; i < this.monsterGenerator[value].get_Count(); i++)
				{
					if (this.monsterGenerator[value].get_Item(i).generator.eventTwo.get_Count() > 0)
					{
						for (int j = 0; j < this.monsterGenerator[value].get_Item(i).generator.eventTwo.get_Count(); j++)
						{
							GearParent.TriggerGearEvent(this.monsterGenerator[value].get_Item(i).generator.eventTwo.get_Item(j));
						}
					}
				}
			}
			if (this.CurrentBatch <= this.MaxBatches && this.CurrentBatch > 0)
			{
				this.SetCurrentBatchMinMass();
				this.CheckMonsterGenerator();
			}
			if (!InstanceManager.IsServerCreate)
			{
				EventDispatcher.Broadcast<int>(InstanceManagerEvent.BatchChanged, value);
			}
			BubbleDialogueManager.Instance.BubbleDialogueTrigger(2, this.CurrentBatch - 1);
		}
	}

	public int MaxBatches
	{
		get
		{
			return this.maxBatches;
		}
		set
		{
			this.maxBatches = value;
		}
	}

	public int CurrentBatchMinMass
	{
		get
		{
			return this.currentBatchMinMass;
		}
		set
		{
			this.currentBatchMinMass = value;
		}
	}

	protected int WaitingGenerateCount
	{
		get
		{
			return this.waitingGenerateCount;
		}
		set
		{
			this.waitingGenerateCount = value;
			if (this.waitingGenerateCount == 0 && this.generatorRangeChecker.Count == 0 && this.monsterGeneratorBlockList.get_Count() == 0)
			{
				this.CheckToNextBatch(false, 0L);
			}
		}
	}

	protected int CurrentMonsterMass
	{
		get
		{
			return this.currentMonsterMass;
		}
		set
		{
			this.currentMonsterMass = value;
		}
	}

	protected int MaxMonsterMass
	{
		get
		{
			return this.maxMonsterMass;
		}
		set
		{
			this.maxMonsterMass = value;
		}
	}

	protected bool IsBossDieEnd
	{
		get
		{
			return this.isBossDieEnd;
		}
		set
		{
			this.isBossDieEnd = value;
		}
	}

	protected int LocalTime
	{
		get
		{
			return this.localTime;
		}
		set
		{
			this.localTime = value;
		}
	}

	public int TimeOutLimit
	{
		get
		{
			return this.timeOutLimit;
		}
		set
		{
			this.timeOutLimit = value;
		}
	}

	public int FinishTimeLimit
	{
		get
		{
			return this.finishTimeLimit;
		}
		set
		{
			this.finishTimeLimit = value;
		}
	}

	protected KillCountDownType KillCountDownType
	{
		get
		{
			return this.killCountDownType;
		}
		set
		{
			this.killCountDownType = value;
		}
	}

	protected int FinishTotalKillCountDown
	{
		get
		{
			return this.finishTotalKillCountDown;
		}
		set
		{
			this.finishTotalKillCountDown = value;
		}
	}

	protected bool IsStart
	{
		get
		{
			return this.isStart;
		}
		set
		{
			this.isStart = value;
		}
	}

	protected bool IsFinished
	{
		get
		{
			return this.isFinished;
		}
		set
		{
			this.isFinished = value;
		}
	}

	protected LocalDimensionSpirit CreateSimpleSpirit(MapObjInfo info)
	{
		return new LocalDimensionSpirit
		{
			ID = info.id,
			OwnerID = info.ownerId,
			CurHp = info.battleInfo.battleBaseAttr.Hp,
			IsDead = info.battleInfo.battleBaseAttr.Hp == 0L
		};
	}

	protected LocalDimensionPetSpirit CreatePetSpirit(long id, int typeID, long ownerID, MapObjInfo info, int index, float existTime, BattleSkillInfo summonSkillInfo, bool isSummonMonopolize, BattleSkillInfo fuseRitualSkillInfo, int fuseRitualSkillBound, List<int> talentIDs, List<int> summonOwnerAttrChange, XDict<int, int> checkOwnerAttrChange, XDict<int, int> checkSelfAttrChange)
	{
		LocalDimensionPetSpirit localDimensionPetSpirit = new LocalDimensionPetSpirit();
		localDimensionPetSpirit.ID = id;
		localDimensionPetSpirit.TypeID = typeID;
		localDimensionPetSpirit.OwnerID = ownerID;
		localDimensionPetSpirit.CurHp = info.battleInfo.battleBaseAttr.Hp;
		localDimensionPetSpirit.IsDead = (info.battleInfo.battleBaseAttr.Hp == 0L);
		localDimensionPetSpirit.IsSummoned = false;
		localDimensionPetSpirit.initialData = info;
		localDimensionPetSpirit.existTime = existTime;
		localDimensionPetSpirit.summonSkillInfo = summonSkillInfo;
		localDimensionPetSpirit.IsSummonMonopolize = isSummonMonopolize;
		localDimensionPetSpirit.fuseRitualSkillInfo = fuseRitualSkillInfo;
		localDimensionPetSpirit.fuseRitualSkillBound = fuseRitualSkillBound;
		localDimensionPetSpirit.talentIDs = talentIDs;
		localDimensionPetSpirit.summonOwnerAttrChangeData.AddRange(summonOwnerAttrChange);
		for (int i = 0; i < checkOwnerAttrChange.Count; i++)
		{
			localDimensionPetSpirit.checkOwnerAttrChangeData.Add(checkOwnerAttrChange.ElementKeyAt(i), checkOwnerAttrChange.ElementValueAt(i));
		}
		for (int j = 0; j < checkSelfAttrChange.Count; j++)
		{
			localDimensionPetSpirit.checkSelfAttrChangeData.Add(checkSelfAttrChange.ElementKeyAt(j), checkSelfAttrChange.ElementValueAt(j));
		}
		return localDimensionPetSpirit;
	}

	public long GetSpiritCurHp(EntityParent entity)
	{
		switch (LocalDimensionSpirit.GetSpiritType(entity.IsEntitySelfType, entity.IsEntityPlayerType, entity.IsEntityPetType, entity.IsEntityMonsterType))
		{
		case LocalDimensionSpirit.SpiritType.Self:
			return this.GetSelfSpiritCurHp();
		case LocalDimensionSpirit.SpiritType.Player:
			return this.GetPlayerSpiritCurHp(entity.ID);
		case LocalDimensionSpirit.SpiritType.Pet:
			return this.GetPetSpiritCurHp(entity.OwnerID, entity.OwnerListIdx);
		case LocalDimensionSpirit.SpiritType.Monster:
			return this.GetMonsterSpiritCurHp(entity.ID);
		default:
			return -1L;
		}
	}

	public void SetSpiritCurHp(EntityParent entity, long curHp)
	{
		switch (LocalDimensionSpirit.GetSpiritType(entity.IsEntitySelfType, entity.IsEntityPlayerType, entity.IsEntityPetType, entity.IsEntityMonsterType))
		{
		case LocalDimensionSpirit.SpiritType.Self:
			this.SetSelfSpiritCurHp(curHp);
			break;
		case LocalDimensionSpirit.SpiritType.Player:
			this.SetPlayerSpiritCurHp(curHp, entity.ID);
			break;
		case LocalDimensionSpirit.SpiritType.Pet:
			this.SetPetSpiritCurHp(curHp, entity.OwnerID, entity.OwnerListIdx);
			break;
		case LocalDimensionSpirit.SpiritType.Monster:
			this.SetMonsterSpiritCurHp(curHp, entity.ID, entity.TypeID);
			break;
		}
	}

	public bool GetSpiritIsDead(EntityParent entity)
	{
		if (!entity.IsClientCreate)
		{
			return entity.IsDead;
		}
		switch (LocalDimensionSpirit.GetSpiritType(entity.IsEntitySelfType, entity.IsEntityPlayerType, entity.IsEntityPetType, entity.IsEntityMonsterType))
		{
		case LocalDimensionSpirit.SpiritType.Self:
			return this.GetSelfSpiritIsDead();
		case LocalDimensionSpirit.SpiritType.Player:
			return this.GetPlayerSpiritIsDead(entity.ID);
		case LocalDimensionSpirit.SpiritType.Pet:
			return this.GetPetSpiritIsDead(entity.OwnerID, entity.OwnerListIdx);
		case LocalDimensionSpirit.SpiritType.Monster:
			return this.GetMonsterSpiritIsDead(entity.ID);
		default:
			return true;
		}
	}

	protected void ResetSelfSpirit()
	{
		this.selfSpirit = null;
	}

	protected void SelfEnterField(LocalDimensionSpirit info)
	{
		this.selfSpirit = info;
	}

	protected void SelfExitField()
	{
		this.selfSpirit = null;
	}

	protected long GetSelfSpiritCurHp()
	{
		if (this.selfSpirit == null)
		{
			return -1L;
		}
		return this.selfSpirit.CurHp;
	}

	protected void SetSelfSpiritCurHp(long curHp)
	{
		if (this.selfSpirit.CurHp != 0L && curHp == 0L)
		{
			this.MarkSelfSpiritDie();
		}
		else if (this.selfSpirit.IsDead && curHp > 0L)
		{
			this.MarkSelfSpiritRelive();
		}
		this.selfSpirit.CurHp = curHp;
	}

	protected bool GetSelfSpiritIsDead()
	{
		return this.selfSpirit == null || this.selfSpirit.IsDead;
	}

	protected void MarkSelfSpiritDie()
	{
		this.selfSpirit.IsDead = true;
		this.OnSelfDie();
	}

	protected void MarkSelfSpiritRelive()
	{
		this.selfSpirit.IsDead = false;
	}

	protected void ResetPlayerSpirit()
	{
		this.playerSpirit.Clear();
	}

	protected void PlayerEnterField(LocalDimensionSpirit info)
	{
		if (this.playerSpirit.ContainsKey(info.ID))
		{
			this.playerSpirit[info.ID] = info;
		}
		else
		{
			this.playerSpirit.Add(info.ID, info);
		}
	}

	protected void PlayerExitField(long id)
	{
		this.playerSpirit.Remove(id);
	}

	protected long GetPlayerSpiritCurHp(long id)
	{
		if (!this.playerSpirit.ContainsKey(id))
		{
			return -1L;
		}
		return this.playerSpirit[id].CurHp;
	}

	protected void SetPlayerSpiritCurHp(long curHp, long id)
	{
		if (!this.playerSpirit.ContainsKey(id))
		{
			return;
		}
		if (this.playerSpirit[id].CurHp != 0L && curHp == 0L)
		{
			this.MarkPlayerSpiritDie(this.playerSpirit[id]);
		}
		else if (this.playerSpirit[id].IsDead && curHp > 0L)
		{
			this.MarkPlayerSpiritRelive(this.playerSpirit[id]);
		}
		this.playerSpirit[id].CurHp = curHp;
	}

	protected bool GetPlayerSpiritIsDead(long id)
	{
		return !this.playerSpirit.ContainsKey(id) || this.playerSpirit[id].IsDead;
	}

	protected void MarkPlayerSpiritDie(LocalDimensionSpirit info)
	{
		info.IsDead = true;
	}

	protected void MarkPlayerSpiritRelive(LocalDimensionSpirit info)
	{
		info.IsDead = false;
	}

	protected void ResetPetSpirit()
	{
		this.petSpirit.Clear();
	}

	protected void PetEnterField(LocalDimensionPetSpirit info, int index)
	{
		if (!this.petSpirit.ContainsKey(info.OwnerID))
		{
			this.petSpirit.Add(info.OwnerID, new XDict<int, LocalDimensionPetSpirit>());
		}
		if (this.petSpirit[info.OwnerID].ContainsKey(index))
		{
			this.petSpirit[info.OwnerID][index] = info;
		}
		else
		{
			this.petSpirit[info.OwnerID].Add(index, info);
		}
	}

	protected void PetLeaveField(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		this.petSpirit[ownerID].Remove(index);
	}

	protected void SendPetEnterBattleField(long petID, Pos pos, Vector2 dir, float existTime)
	{
		LocalBattleProtocolSimulator.SendPetEnterBattleField(petID, pos, dir, existTime);
	}

	protected void SendPetLeaveBattleField(long petID)
	{
		LocalBattleHandler.Instance.AppClearBuff(petID);
		LocalBattleProtocolSimulator.SendPetLeaveBattleField(petID);
	}

	protected long GetPetSpiritCurHp(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return -1L;
		}
		XDict<int, LocalDimensionPetSpirit> xDict = this.petSpirit[ownerID];
		for (int i = 0; i < xDict.Count; i++)
		{
			if (xDict.ElementKeyAt(i) == index)
			{
				return xDict.ElementValueAt(i).CurHp;
			}
		}
		return -1L;
	}

	protected void SetPetSpiritCurHp(long curHp, long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		XDict<int, LocalDimensionPetSpirit> xDict = this.petSpirit[ownerID];
		for (int i = 0; i < xDict.Count; i++)
		{
			if (xDict.ElementKeyAt(i) == index)
			{
				if (xDict.ElementValueAt(i).CurHp != 0L && curHp == 0L)
				{
					this.MarkPetSpiritDie(xDict.ElementValueAt(i), ownerID, index);
				}
				else if (xDict.ElementValueAt(i).IsDead && curHp > 0L)
				{
					this.MarkPetSpiritRelive(xDict.ElementValueAt(i));
				}
				xDict.ElementValueAt(i).CurHp = curHp;
				break;
			}
		}
	}

	protected bool GetPetSpiritIsDead(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return true;
		}
		XDict<int, LocalDimensionPetSpirit> xDict = this.petSpirit[ownerID];
		for (int i = 0; i < xDict.Count; i++)
		{
			if (xDict.ElementKeyAt(i) == index)
			{
				return xDict.ElementValueAt(i).IsDead;
			}
		}
		return true;
	}

	protected void MarkPetSpiritDie(LocalDimensionPetSpirit info, long ownerID, int index)
	{
		if (info.IsDead)
		{
			return;
		}
		info.IsSummoned = true;
		info.IsDead = true;
		this.OnPetDie(info, ownerID);
	}

	protected void MarkPetSpiritRelive(LocalDimensionPetSpirit info)
	{
		info.IsDead = false;
	}

	public XDict<int, LocalDimensionPetSpirit> GetPetSpiritByOwnerID(long ownerID)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return new XDict<int, LocalDimensionPetSpirit>();
		}
		return this.petSpirit[ownerID];
	}

	protected void ResetMonsterSpirit()
	{
		this.monsterSpirit.Clear();
	}

	protected void MonsterEnterField(LocalDimensionSpirit info)
	{
		if (this.monsterSpirit.ContainsKey(info.ID))
		{
			this.monsterSpirit[info.ID] = info;
		}
		else
		{
			this.monsterSpirit.Add(info.ID, info);
		}
	}

	protected void MonsterExitField(long id)
	{
		this.monsterSpirit.Remove(id);
	}

	protected void SetMonsterSpiritDirectDie(long id, int typeID)
	{
		if (!this.monsterSpirit.ContainsKey(id))
		{
			return;
		}
		if (!this.monsterSpirit[id].IsDead)
		{
			this.MarkMonsterSpiritDie(id, typeID);
		}
	}

	protected long GetMonsterSpiritCurHp(long id)
	{
		if (!this.monsterSpirit.ContainsKey(id))
		{
			return -1L;
		}
		return this.monsterSpirit[id].CurHp;
	}

	protected void SetMonsterSpiritCurHp(long curHp, long id, int typeID)
	{
		if (!this.monsterSpirit.ContainsKey(id))
		{
			return;
		}
		if (this.monsterSpirit[id].CurHp != 0L && curHp == 0L)
		{
			this.MarkMonsterSpiritDie(id, typeID);
		}
		else if (this.monsterSpirit[id].IsDead && curHp > 0L)
		{
			this.MarkMonsterSpiritRelive(this.monsterSpirit[id]);
		}
		this.monsterSpirit[id].CurHp = curHp;
	}

	protected bool GetMonsterSpiritIsDead(long id)
	{
		return !this.monsterSpirit.ContainsKey(id) || this.monsterSpirit[id].IsDead;
	}

	protected void MarkMonsterSpiritDie(long id, int typeID)
	{
		this.monsterSpirit[id].IsDead = true;
		this.OnMonsterDie(id, typeID);
	}

	protected void MarkMonsterSpiritRelive(LocalDimensionSpirit info)
	{
		info.IsDead = false;
	}

	public void Init()
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		this.ResetData();
		this.AddListener();
	}

	public void Release()
	{
		this.RemoveListener();
		this.ResetData();
		this.HasInit = false;
	}

	protected void AddListener()
	{
		NetworkManager.AddListenEvent<BattleOperationRes>(new NetCallBackMethod<BattleOperationRes>(this.OnBattleOperationRes));
		EventDispatcher.AddListener<int>(LocalInstanceEvent.TriggerGenarator, new Callback<int>(this.TriggerRangeGenerator));
		EventDispatcher.AddListener<EntityPet>(LocalInstanceEvent.MarkSelfPet, new Callback<EntityPet>(this.MarkSelfPet));
		EventDispatcher.AddListener<EntityPet>(LocalInstanceEvent.UnmarkSelfPet, new Callback<EntityPet>(this.UnmarkSelfPet));
		EventDispatcher.AddListener<long, int>(LocalInstanceEvent.SetMonsterDie, new Callback<long, int>(this.SetMonsterSpiritDirectDie));
		EventDispatcher.AddListener(InstanceManagerEvent.BossDieEnd, new Callback(this.BossAnimaEnd));
	}

	protected void RemoveListener()
	{
		NetworkManager.RemoveListenEvent<BattleOperationRes>(new NetCallBackMethod<BattleOperationRes>(this.OnBattleOperationRes));
		EventDispatcher.RemoveListener<int>(LocalInstanceEvent.TriggerGenarator, new Callback<int>(this.TriggerRangeGenerator));
		EventDispatcher.RemoveListener<EntityPet>(LocalInstanceEvent.MarkSelfPet, new Callback<EntityPet>(this.MarkSelfPet));
		EventDispatcher.RemoveListener<EntityPet>(LocalInstanceEvent.UnmarkSelfPet, new Callback<EntityPet>(this.UnmarkSelfPet));
		EventDispatcher.RemoveListener<long, int>(LocalInstanceEvent.SetMonsterDie, new Callback<long, int>(this.SetMonsterSpiritDirectDie));
		EventDispatcher.RemoveListener(InstanceManagerEvent.BossDieEnd, new Callback(this.BossAnimaEnd));
	}

	public void ResetData()
	{
		this.IsStart = false;
		this.IsFinished = false;
		this.LocalTime = 0;
		this.IsPauseTimeEscape = false;
		this.ResetPoint();
		this.ResetFinishCondition();
		this.ResetSelf();
		this.ResetPlayer();
		this.ResetPet();
		this.ResetGhostMosnter();
		this.ResetMosnter();
		ClientEffectHandler.Instance.ResetEffect();
		this.InstanceID = 0;
		this.SceneID = 0;
		this.IsEnable = false;
	}

	public void SetData(FuBenJiChuPeiZhi instanceData, int monsterRefreshAppointedLevel = 0)
	{
		int arg_64_1 = instanceData.type;
		int arg_64_2 = instanceData.id;
		int arg_64_3 = instanceData.scene;
		int arg_64_4 = instanceData.time;
		List<int> arg_64_5 = instanceData.refreshId;
		int arg_64_6 = instanceData.limit;
		List<int> arg_64_8 = instanceData.ghostMonsterTypeId;
		List<int> arg_64_9 = instanceData.ghostMonsterLevel;
		List<int> arg_64_10 = instanceData.ghostMonsterSpawnPoint;
		List<int> list = new List<int>();
		list.Add(instanceData.completeTarget);
		List<int> arg_64_11 = list;
		list = new List<int>();
		list.Add(instanceData.failJudge);
		this.SetData(arg_64_1, arg_64_2, arg_64_3, arg_64_4, arg_64_5, arg_64_6, monsterRefreshAppointedLevel, arg_64_8, arg_64_9, arg_64_10, arg_64_11, list, instanceData.targetValue);
	}

	public void SetData(FuBenJiChuPeiZhi instanceData, List<int> theWinCondition, List<int> finishArgs, int monsterRefreshAppointedLevel = 0)
	{
		int arg_4E_1 = instanceData.type;
		int arg_4E_2 = instanceData.id;
		int arg_4E_3 = instanceData.scene;
		int arg_4E_4 = instanceData.time;
		List<int> arg_4E_5 = instanceData.refreshId;
		int arg_4E_6 = instanceData.limit;
		List<int> arg_4E_8 = instanceData.ghostMonsterTypeId;
		List<int> arg_4E_9 = instanceData.ghostMonsterLevel;
		List<int> arg_4E_10 = instanceData.ghostMonsterSpawnPoint;
		List<int> list = new List<int>();
		list.Add(instanceData.failJudge);
		this.SetData(arg_4E_1, arg_4E_2, arg_4E_3, arg_4E_4, arg_4E_5, arg_4E_6, monsterRefreshAppointedLevel, arg_4E_8, arg_4E_9, arg_4E_10, theWinCondition, list, finishArgs);
	}

	public void SetData(int instanceType, int instanceDataID, int sceneID, int instanceTime, List<int> batches, int maxMass, int monsterRefreshAppointedLevel, List<int> theGhostMonsterTypeID, List<int> theGhostMonsterLevel, List<int> theGhostMonsterSpawnPoint, List<int> theWinCondition, List<int> theLoseCondition, List<int> finishArgs)
	{
		this.IsEnable = true;
		this.InstanceID = instanceDataID;
		this.SceneID = sceneID;
		this.TimeOutLimit = instanceTime;
		this.SetPoint(sceneID);
		this.SetFinishCondition(theWinCondition, theLoseCondition, finishArgs);
		this.SetPet(instanceType, EntityWorld.Instance.EntSelf.ID);
		this.SetGhostMonster(theGhostMonsterTypeID, theGhostMonsterLevel, theGhostMonsterSpawnPoint);
		this.SetMonster(batches, maxMass, monsterRefreshAppointedLevel);
	}

	public void Start()
	{
		ReconnectManager.Instance.DataServerReconnectHandler = DataServerClientBattleReconnectHandler.Instance;
		this.IsStart = true;
		this.IsFinished = false;
		this.totalDeltaTime = 0f;
		this.LocalTime = 0;
		this.IsPauseTimeEscape = false;
		this.CreateGhostMonster();
		this.CurrentBatch = 1;
		this.TryOperation(LocalBattleOperation.Start, InstanceManager.CurrentInstanceType);
	}

	public void Finish(bool isWinFinish)
	{
		this.SetInstanceFinish(isWinFinish);
	}

	public void Update(float deltaTime)
	{
		if (!this.IsEnable)
		{
			return;
		}
		if (!this.IsStart)
		{
			return;
		}
		if (this.IsPauseTimeEscape)
		{
			return;
		}
		this.totalDeltaTime += deltaTime;
		if (this.totalDeltaTime > 1f)
		{
			this.totalDeltaTime -= 1f;
			this.LocalTime++;
			this.CheckTimeOutFinish();
			this.CheckInstanceTimeout();
		}
	}

	protected void ResetPoint()
	{
		this.SpawnPointCount.Clear();
	}

	protected void SetPoint(int sceneID)
	{
		ArrayList pointDataBySceneID = MapDataManager.Instance.GetPointDataBySceneID(sceneID);
		IEnumerator enumerator = pointDataBySceneID.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Hashtable hashtable = (Hashtable)enumerator.get_Current();
				int num = (int)((double)hashtable.get_Item("point"));
				List<int> list = new List<int>();
				IEnumerator enumerator2 = ((ArrayList)hashtable.get_Item("child_point")).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						Hashtable hashtable2 = (Hashtable)enumerator2.get_Current();
						list.Add(0);
					}
				}
				finally
				{
					IDisposable disposable = enumerator2 as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				if (!this.SpawnPointCount.ContainsKey(num))
				{
					this.SpawnPointCount.Add(num, list);
				}
				else
				{
					this.SpawnPointCount.set_Item(num, list);
				}
			}
		}
		finally
		{
			IDisposable disposable2 = enumerator as IDisposable;
			if (disposable2 != null)
			{
				disposable2.Dispose();
			}
		}
	}

	public Vector3 GetSpawnPosition(int groupID)
	{
		Vector3 zero = Vector3.get_zero();
		if (!this.SpawnPointCount.ContainsKey(groupID))
		{
			return zero;
		}
		if (this.SpawnPointCount.get_Item(groupID).get_Count() == 0)
		{
			return zero;
		}
		ArrayList pointDataByGroupKey = MapDataManager.Instance.GetPointDataByGroupKey(this.SceneID, groupID);
		if (pointDataByGroupKey == null)
		{
			return zero;
		}
		int num = this.SpawnPointCount.get_Item(groupID).get_Item(0);
		for (int i = 0; i < this.SpawnPointCount.get_Item(groupID).get_Count(); i++)
		{
			if (num > this.SpawnPointCount.get_Item(groupID).get_Item(i))
			{
				num = this.SpawnPointCount.get_Item(groupID).get_Item(i);
			}
		}
		List<int> list = new List<int>();
		for (int j = 0; j < this.SpawnPointCount.get_Item(groupID).get_Count(); j++)
		{
			if (this.SpawnPointCount.get_Item(groupID).get_Item(j) == num)
			{
				list.Add(j);
			}
		}
		int num2 = list.get_Item(Random.Range(0, list.get_Count()));
		List<int> list2;
		List<int> expr_11C = list2 = this.SpawnPointCount.get_Item(groupID);
		int num3;
		int expr_121 = num3 = num2;
		num3 = list2.get_Item(num3);
		expr_11C.set_Item(expr_121, num3 + 1);
		for (int k = 0; k < pointDataByGroupKey.get_Count(); k++)
		{
			Hashtable hashtable = (Hashtable)pointDataByGroupKey.get_Item(k);
			double num4 = (double)hashtable.get_Item("point");
			if ((int)num4 == num2 + 1)
			{
				double num5 = (double)hashtable.get_Item("x") * 0.01;
				double num6 = (double)hashtable.get_Item("y") * 0.01;
				zero = new Vector3((float)num5, 0f, (float)num6);
			}
		}
		return zero;
	}

	public Vector3 GetSpawnPosition(List<int> groupIDs)
	{
		Vector3 zero = Vector3.get_zero();
		List<int> list = new List<int>();
		int num = 2147483647;
		for (int i = 0; i < groupIDs.get_Count(); i++)
		{
			if (this.SpawnPointCount.ContainsKey(groupIDs.get_Item(i)))
			{
				if (this.SpawnPointCount.get_Item(groupIDs.get_Item(i)).get_Count() != 0)
				{
					if (MapDataManager.Instance.GetPointDataByGroupKey(this.SceneID, groupIDs.get_Item(i)) != null)
					{
						list.Add(groupIDs.get_Item(i));
						for (int j = 0; j < this.SpawnPointCount.get_Item(groupIDs.get_Item(i)).get_Count(); j++)
						{
							if (num > this.SpawnPointCount.get_Item(groupIDs.get_Item(i)).get_Item(j))
							{
								num = this.SpawnPointCount.get_Item(groupIDs.get_Item(i)).get_Item(j);
							}
						}
					}
				}
			}
		}
		if (list.get_Count() == 0)
		{
			return zero;
		}
		List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
		for (int k = 0; k < list.get_Count(); k++)
		{
			for (int l = 0; l < this.SpawnPointCount.get_Item(list.get_Item(k)).get_Count(); l++)
			{
				if (this.SpawnPointCount.get_Item(list.get_Item(k)).get_Item(l) == num)
				{
					list2.Add(new KeyValuePair<int, int>(list.get_Item(k), l));
				}
			}
		}
		KeyValuePair<int, int> keyValuePair = list2.get_Item(Random.Range(0, list2.get_Count()));
		List<int> list3;
		List<int> expr_1B7 = list3 = this.SpawnPointCount.get_Item(keyValuePair.get_Key());
		int num2;
		int expr_1C1 = num2 = keyValuePair.get_Value();
		num2 = list3.get_Item(num2);
		expr_1B7.set_Item(expr_1C1, num2 + 1);
		ArrayList pointDataByGroupKey = MapDataManager.Instance.GetPointDataByGroupKey(this.SceneID, keyValuePair.get_Key());
		for (int m = 0; m < pointDataByGroupKey.get_Count(); m++)
		{
			Hashtable hashtable = (Hashtable)pointDataByGroupKey.get_Item(m);
			double num3 = (double)hashtable.get_Item("point");
			if ((int)num3 == keyValuePair.get_Value() + 1)
			{
				double num4 = (double)hashtable.get_Item("x") * 0.01;
				double num5 = (double)hashtable.get_Item("y") * 0.01;
				zero = new Vector3((float)num4, 0f, (float)num5);
			}
		}
		return zero;
	}

	protected void ResetSelf()
	{
		this.ResetSelfSpirit();
		if (this.IsEnable && EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.ResetBattleState();
		}
	}

	public MapObjInfo CreateSelfInfo(int instanceType, int instanceDataID, int sceneID, int trailType = 0, int trailModel = 0, MapObjDecorations trialDecorations = null, List<PetInfo> trialPetInfos = null, List<BattleSkillInfo> trialSkillInfos = null)
	{
		MapObjInfo mapObjInfo = LocalDimensionSelfInfoCreator.CreateSelfMapObjInfo(instanceType, instanceDataID, sceneID, trailType, trailModel, trialDecorations, trialPetInfos, trialSkillInfos);
		this.SelfEnterField(this.CreateSimpleSpirit(mapObjInfo));
		return mapObjInfo;
	}

	protected void OnSelfDie()
	{
		this.CheckDeadFinish();
	}

	protected void ResetPlayer()
	{
		this.ResetPlayerSpirit();
	}

	public void CopySelf(int pointGroupID, bool isSameCamp, bool isCopyNow)
	{
		List<PetInfo> petBattleInfo = PetManager.Instance.GetPetBattleInfo((int)InstanceManager.CurrentInstanceType);
		for (int i = 0; i < petBattleInfo.get_Count(); i++)
		{
			petBattleInfo.get_Item(i).id = (long)this.PoolID;
		}
		MapObjInfo mapObjInfo = LocalDimensionPlayerInfoCreator.CreateSelfCopyMapObjInfo(this.PoolID, this.SceneID, pointGroupID, (!isCopyNow) ? LocalDimensionPlayerInfoCreator.CreateSelfCopyBattleInfo(isSameCamp, petBattleInfo, (float)InstanceManager.CurrentInstanceData.actionPoint) : LocalDimensionPlayerInfoCreator.CreateSelfCopyNowBattleInfo(isSameCamp, petBattleInfo));
		this.PlayerEnterField(this.CreateSimpleSpirit(mapObjInfo));
		EntityWorld.Instance.CreateOtherPlayer(mapObjInfo);
		this.CreateSelfCopyPetInfo(petBattleInfo, mapObjInfo.id, (float)mapObjInfo.battleInfo.battleBaseAttr.ActPoint);
	}

	protected void CreateSelfCopyPetInfo(List<PetInfo> petOriginInfos, long ownerID, float ownerActionPoint)
	{
		int num = 0;
		for (int i = 0; i < petOriginInfos.get_Count(); i++)
		{
			if (DataReader<Pet>.Get(petOriginInfos.get_Item(i).petId) != null)
			{
				this.PetEnterField(this.CreatePetSpirit(petOriginInfos.get_Item(i).id, petOriginInfos.get_Item(i).petId, ownerID, LocalDimensionPetInfoCreator.CreatePetMapObjInfo(ownerID, num, petOriginInfos.get_Item(i), this.SceneID), num, (float)PetManager.Instance.GetExistTime(petOriginInfos.get_Item(i)), PetManager.Instance.GetSummonSkillInfo(petOriginInfos.get_Item(i), num), PetManager.Instance.GetSummonMonopolize(petOriginInfos.get_Item(i)), PetManager.Instance.GetFuseRitualSkillInfo(petOriginInfos.get_Item(i), num), PetManager.Instance.GetFuseRitualSkillBound(petOriginInfos.get_Item(i)), PetManager.Instance.GetTalentIDs(petOriginInfos.get_Item(i).id), PetManager.Instance.GetSummonOwnerAttrChange(petOriginInfos.get_Item(i).id), PetManager.Instance.GetCheckOwnerAttrChange(petOriginInfos.get_Item(i).id), PetManager.Instance.GetCheckPetSelfAttrChange(petOriginInfos.get_Item(i).id)), num);
				num++;
			}
		}
		this.CheckPet(ownerID, ownerActionPoint);
	}

	protected void ResetPet()
	{
		for (int i = 0; i < this.petSpirit.Count; i++)
		{
			this.ClearPetManualSkill(this.petSpirit.ElementKeyAt(i));
			this.ClearPetFuseRitualSkill(this.petSpirit.ElementKeyAt(i));
			this.ClearPetSleepTimer(this.petSpirit.ElementKeyAt(i));
		}
		this.ResetPetSpirit();
	}

	protected void SetPet(int mapType, long ownerID)
	{
		EntityParent entityByID = EntityWorld.Instance.GetEntityByID(ownerID);
		if (entityByID == null)
		{
			return;
		}
		List<PetInfo> petBattleInfo = PetManager.Instance.GetPetBattleInfo(mapType);
		int num = 0;
		for (int i = 0; i < petBattleInfo.get_Count(); i++)
		{
			if (DataReader<Pet>.Get(petBattleInfo.get_Item(i).petId) != null)
			{
				this.PetEnterField(this.CreatePetSpirit(petBattleInfo.get_Item(i).id, petBattleInfo.get_Item(i).petId, ownerID, LocalDimensionPetInfoCreator.CreatePetMapObjInfo(ownerID, num, petBattleInfo.get_Item(i), this.SceneID), num, (float)PetManager.Instance.GetExistTime(petBattleInfo.get_Item(i)), PetManager.Instance.GetSummonSkillInfo(petBattleInfo.get_Item(i), num), PetManager.Instance.GetSummonMonopolize(petBattleInfo.get_Item(i)), PetManager.Instance.GetFuseRitualSkillInfo(petBattleInfo.get_Item(i), num), PetManager.Instance.GetFuseRitualSkillBound(petBattleInfo.get_Item(i)), PetManager.Instance.GetTalentIDs(petBattleInfo.get_Item(i).id), PetManager.Instance.GetSummonOwnerAttrChange(petBattleInfo.get_Item(i).id), PetManager.Instance.GetCheckOwnerAttrChange(petBattleInfo.get_Item(i).id), PetManager.Instance.GetCheckPetSelfAttrChange(petBattleInfo.get_Item(i).id)), num);
				num++;
			}
		}
		this.CheckPet(ownerID, (float)entityByID.ActPoint);
	}

	public void CheckPet(long ownerID, float actionPoint)
	{
	}

	public void CheckPetSummon(long ownerID, float actionPoint, LocalDimensionPetSpirit info)
	{
		EntityParent entityByID = EntityWorld.Instance.GetEntityByID(ownerID);
		if (entityByID == null)
		{
			return;
		}
		if (!entityByID.Actor)
		{
			return;
		}
		if (info.IsSummoned && !info.IsDead)
		{
			return;
		}
		if (info.IsDead)
		{
			if (actionPoint < info.resummonPoint)
			{
				return;
			}
			info.IsSummoned = true;
			info.IsDead = false;
			Vector3 position = entityByID.Actor.FixTransform.get_position();
			Quaternion rotation = entityByID.Actor.FixTransform.get_rotation();
			this.PetSummonRitual(ownerID, info, this.GetPetRandomPos(info.TypeID, position, rotation), this.GetPetDir(rotation));
		}
		else if (!info.IsSummoned)
		{
			if (actionPoint < info.summonPoint)
			{
				return;
			}
			info.IsSummoned = true;
			info.IsDead = false;
			Vector3 position2 = entityByID.Actor.FixTransform.get_position();
			Quaternion rotation2 = entityByID.Actor.FixTransform.get_rotation();
			this.PetSummonRitual(ownerID, info, this.GetPetRandomPos(info.TypeID, position2, rotation2), this.GetPetDir(rotation2));
		}
	}

	public void SummonPet(long ownerID, LocalDimensionPetSpirit info)
	{
		EntityParent entityByID = EntityWorld.Instance.GetEntityByID(ownerID);
		if (entityByID == null)
		{
			return;
		}
		if (!entityByID.Actor)
		{
			return;
		}
		TimerHeap.DelTimer(info.sleepTimer);
		Vector3 petRandomPos = this.GetPetRandomPos(info.TypeID, entityByID.Actor.FixTransform.get_position(), entityByID.Actor.FixTransform.get_rotation());
		Vector3 petDir = this.GetPetDir(entityByID.Actor.FixTransform.get_rotation());
		if (!info.IsSummoned)
		{
			this.PetSummonRitual(ownerID, info, petRandomPos, petDir);
		}
		else if (!info.IsDead)
		{
			this.SendPetEnterBattleField(info.ID, new Pos
			{
				x = petRandomPos.x * 100f,
				y = petRandomPos.z * 100f
			}, new Vector2
			{
				x = petDir.x,
				y = petDir.z
			}, info.existTime);
			this.AddPetFuseRitualSkill(ownerID, info);
			info.sleepTimer = TimerHeap.AddTimer((uint)(info.existTime * 1000f), 0, delegate
			{
				this.RemovePetFuseRitualSkill(ownerID, info);
				this.ReleasePet(info, false);
			});
		}
	}

	public void ReleasePet(LocalDimensionPetSpirit info, bool isDead = false)
	{
		TimerHeap.DelTimer(info.sleepTimer);
		if (!isDead)
		{
			this.SendPetLeaveBattleField(info.ID);
		}
	}

	public Vector3 GetPetRandomPos(int typeID, Vector3 position, Quaternion rotation)
	{
		Pet pet = DataReader<Pet>.Get(typeID);
		if (pet == null)
		{
			return Vector3.get_zero();
		}
		Vector3 vector = Vector3.get_zero();
		Vector3 vector2 = Vector3.get_zero();
		if (pet.summonOffset.get_Count() > 0)
		{
			vector = rotation * Vector3.get_left() * (float)pet.summonOffset.get_Item(0) * 0.01f;
		}
		if (pet.summonOffset.get_Count() > 1)
		{
			vector2 = rotation * Vector3.get_forward() * (float)pet.summonOffset.get_Item(1) * 0.01f;
		}
		return position + vector + vector2;
	}

	public Vector3 GetPetDir(Quaternion rotation)
	{
		return rotation * Vector3.get_forward();
	}

	protected void PetSummonRitual(long ownerID, LocalDimensionPetSpirit info, Vector3 pos, Vector3 dir)
	{
		if (info.IsSummoned)
		{
			return;
		}
		info.IsSummoned = true;
		info.IsDead = false;
		EntityPet entityPet = EntityWorld.Instance.CreatePet(info.initialData, true);
		if (entityPet == null)
		{
			return;
		}
		entityPet.Element = DataReader<Pet>.Get(entityPet.TypeID).element;
		info.currentSummonOwnerAttr.Clear();
		if (!entityPet.IsFighting)
		{
			this.SendPetEnterBattleField(info.ID, new Pos
			{
				x = pos.x * 100f,
				y = pos.z * 100f
			}, new Vector2
			{
				x = dir.x,
				y = dir.z
			}, info.existTime);
			this.AddPetFuseRitualSkill(ownerID, info);
		}
		info.sleepTimer = TimerHeap.AddTimer((uint)(info.existTime * 1000f), 0, delegate
		{
			this.RemovePetFuseRitualSkill(ownerID, info);
			this.ReleasePet(info, false);
		});
		EntityParent entityByID = EntityWorld.Instance.GetEntityByID(ownerID);
		if (entityByID != null)
		{
			for (int i = 0; i < info.summonOwnerAttrChangeData.get_Count(); i++)
			{
				info.currentSummonOwnerAttr.Add(info.summonOwnerAttrChangeData.get_Item(i));
				entityByID.BattleBaseAttrs.AddValuesByTemplateID(info.summonOwnerAttrChangeData.get_Item(i));
			}
		}
		this.CheckCurrentPetAttrChange();
	}

	protected void CheckCurrentPetAttrChange()
	{
		for (int i = 0; i < this.petSpirit.Count; i++)
		{
			EntityParent entityByID = EntityWorld.Instance.GetEntityByID(this.petSpirit.ElementKeyAt(i));
			if (entityByID != null)
			{
				XDict<int, LocalDimensionPetSpirit> xDict = this.petSpirit.ElementValueAt(i);
				for (int j = 0; j < xDict.Count; j++)
				{
					LocalDimensionPetSpirit localDimensionPetSpirit = xDict.ElementValueAt(j);
					if (localDimensionPetSpirit.IsSummoned)
					{
						if (!localDimensionPetSpirit.IsDead)
						{
							EntityParent entityByID2 = EntityWorld.Instance.GetEntityByID(localDimensionPetSpirit.ID);
							if (entityByID2 != null)
							{
								List<int> list = new List<int>();
								List<int> list2 = new List<int>();
								for (int k = 0; k < this.petSpirit.Count; k++)
								{
									EntityParent entityByID3 = EntityWorld.Instance.GetEntityByID(this.petSpirit.ElementKeyAt(k));
									if (entityByID3 != null)
									{
										if (entityByID3.Camp == entityByID.Camp)
										{
											XDict<int, LocalDimensionPetSpirit> xDict2 = this.petSpirit.ElementValueAt(k);
											for (int l = 0; l < xDict2.Count; l++)
											{
												LocalDimensionPetSpirit localDimensionPetSpirit2 = xDict2.ElementValueAt(l);
												if (localDimensionPetSpirit2.IsSummoned)
												{
													if (!localDimensionPetSpirit2.IsDead)
													{
														for (int m = 0; m < localDimensionPetSpirit2.talentIDs.get_Count(); m++)
														{
															if (localDimensionPetSpirit.checkOwnerAttrChangeData.ContainsKey(localDimensionPetSpirit2.talentIDs.get_Item(m)))
															{
																list.Add(localDimensionPetSpirit.checkOwnerAttrChangeData[localDimensionPetSpirit2.talentIDs.get_Item(m)]);
															}
															if (localDimensionPetSpirit.checkSelfAttrChangeData.ContainsKey(localDimensionPetSpirit2.talentIDs.get_Item(m)))
															{
																list2.Add(localDimensionPetSpirit.checkSelfAttrChangeData[localDimensionPetSpirit2.talentIDs.get_Item(m)]);
															}
														}
													}
												}
											}
										}
									}
								}
								List<int> list3 = new List<int>();
								for (int n = 0; n < localDimensionPetSpirit.currentCheckOwnerAttr.get_Count(); n++)
								{
									if (!list.Contains(localDimensionPetSpirit.currentCheckOwnerAttr.get_Item(n)))
									{
										list3.Add(localDimensionPetSpirit.currentCheckOwnerAttr.get_Item(n));
									}
								}
								for (int num = 0; num < list3.get_Count(); num++)
								{
									localDimensionPetSpirit.currentCheckOwnerAttr.Remove(list3.get_Item(num));
									entityByID.BattleBaseAttrs.RemoveValuesByTemplateID(list3.get_Item(num));
								}
								for (int num2 = 0; num2 < list.get_Count(); num2++)
								{
									if (!localDimensionPetSpirit.currentCheckOwnerAttr.Contains(list.get_Item(num2)))
									{
										localDimensionPetSpirit.currentCheckOwnerAttr.Add(list.get_Item(num2));
										entityByID.BattleBaseAttrs.RemoveValuesByTemplateID(list.get_Item(num2));
									}
								}
								list3.Clear();
								for (int num3 = 0; num3 < localDimensionPetSpirit.currentCheckSelfAttr.get_Count(); num3++)
								{
									if (!list2.Contains(localDimensionPetSpirit.currentCheckSelfAttr.get_Item(num3)))
									{
										list3.Add(localDimensionPetSpirit.currentCheckSelfAttr.get_Item(num3));
									}
								}
								for (int num4 = 0; num4 < list3.get_Count(); num4++)
								{
									localDimensionPetSpirit.currentCheckSelfAttr.Remove(list3.get_Item(num4));
									entityByID2.BattleBaseAttrs.RemoveValuesByTemplateID(list3.get_Item(num4));
								}
								for (int num5 = 0; num5 < list2.get_Count(); num5++)
								{
									if (!localDimensionPetSpirit.currentCheckSelfAttr.Contains(list2.get_Item(num5)))
									{
										localDimensionPetSpirit.currentCheckSelfAttr.Add(list2.get_Item(num5));
										entityByID2.BattleBaseAttrs.AddValuesByTemplateID(list2.get_Item(num5));
									}
								}
							}
						}
					}
				}
			}
		}
	}

	protected void ClearPetSleepTimer(long ownerID)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		for (int i = 0; i < this.petSpirit[ownerID].Count; i++)
		{
			TimerHeap.DelTimer(this.petSpirit[ownerID].ElementValueAt(i).sleepTimer);
		}
	}

	protected void CheckPetSkill(long ownerID, float actionPoint, LocalDimensionPetSpirit info)
	{
		if (actionPoint >= (float)info.fuseRitualSkillBound)
		{
			this.RemovePetManualSkill(ownerID, info);
			this.AddPetFuseRitualSkill(ownerID, info);
		}
		else if (actionPoint >= (float)info.manualSkillBound)
		{
			this.RemovePetFuseRitualSkill(ownerID, info);
			this.AddPetManualSkill(ownerID, info);
		}
		else
		{
			this.RemovePetManualSkill(ownerID, info);
			this.RemovePetFuseRitualSkill(ownerID, info);
		}
	}

	protected void AddPetSummonRitualSkill(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		if (!this.petSpirit[ownerID].ContainsKey(index))
		{
			return;
		}
		this.AddPetSummonRitualSkill(ownerID, this.petSpirit[ownerID][index]);
	}

	protected void AddPetSummonRitualSkill(long ownerID, LocalDimensionPetSpirit info)
	{
		if (info.IsSummoned && !info.IsDead && info.summonSkillInfo != null)
		{
			LocalBattleHandler.Instance.AddSkill(ownerID, info.summonSkillInfo.skillIdx, info.summonSkillInfo.skillId, info.summonSkillInfo.skillLv);
		}
	}

	protected void RemovePetSummonRitualSkill(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		if (!this.petSpirit[ownerID].ContainsKey(index))
		{
			return;
		}
		this.RemovePetSummonRitualSkill(ownerID, this.petSpirit[ownerID][index]);
	}

	public void RemovePetSummonRitualSkill(long ownerID, LocalDimensionPetSpirit info)
	{
		if (info.summonSkillInfo == null)
		{
			return;
		}
		LocalBattleHandler.Instance.RemoveSkill(ownerID, info.summonSkillInfo.skillId);
	}

	public void ClearPetSummonRitualSkill(long ownerID)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		for (int i = 0; i < this.petSpirit[ownerID].Count; i++)
		{
			if (this.petSpirit[ownerID].ElementValueAt(i).summonSkillInfo != null)
			{
				LocalBattleHandler.Instance.RemoveSkill(ownerID, this.petSpirit[ownerID].ElementValueAt(i).summonSkillInfo.skillId);
			}
		}
	}

	public void AddPetManualSkill(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		if (!this.petSpirit[ownerID].ContainsKey(index))
		{
			return;
		}
		this.AddPetManualSkill(ownerID, this.petSpirit[ownerID][index]);
	}

	protected void AddPetManualSkill(long ownerID, LocalDimensionPetSpirit info)
	{
		if (info.IsSummoned && !info.IsDead && info.manualSkillInfo != null)
		{
			LocalBattleHandler.Instance.AddSkill(ownerID, info.manualSkillInfo.skillIdx, info.manualSkillInfo.skillId, info.manualSkillInfo.skillLv);
		}
	}

	protected void RemovePetManualSkill(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		if (!this.petSpirit[ownerID].ContainsKey(index))
		{
			return;
		}
		this.RemovePetManualSkill(ownerID, this.petSpirit[ownerID][index]);
	}

	protected void RemovePetManualSkill(long ownerID, LocalDimensionPetSpirit info)
	{
		if (info.manualSkillInfo == null)
		{
			return;
		}
		LocalBattleHandler.Instance.RemoveSkill(ownerID, info.manualSkillInfo.skillId);
	}

	public void ClearPetManualSkill(long ownerID)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		for (int i = 0; i < this.petSpirit[ownerID].Count; i++)
		{
			if (this.petSpirit[ownerID].ElementValueAt(i).manualSkillInfo != null)
			{
				LocalBattleHandler.Instance.RemoveSkill(ownerID, this.petSpirit[ownerID].ElementValueAt(i).manualSkillInfo.skillId);
			}
		}
	}

	public void AddPetFuseRitualSkill(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		if (!this.petSpirit[ownerID].ContainsKey(index))
		{
			return;
		}
		this.AddPetFuseRitualSkill(ownerID, this.petSpirit[ownerID][index]);
	}

	protected void AddPetFuseRitualSkill(long ownerID, LocalDimensionPetSpirit info)
	{
		if (info.IsSummoned && !info.IsDead && info.fuseRitualSkillInfo != null && info.fuseRitualSkillInfo.skillId != 0)
		{
			LocalBattleHandler.Instance.AddSkill(ownerID, info.fuseRitualSkillInfo.skillIdx, info.fuseRitualSkillInfo.skillId, info.fuseRitualSkillInfo.skillLv);
		}
	}

	protected void RemovePetFuseRitualSkill(long ownerID, int index)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		if (!this.petSpirit[ownerID].ContainsKey(index))
		{
			return;
		}
		this.RemovePetFuseRitualSkill(ownerID, this.petSpirit[ownerID][index]);
	}

	protected void RemovePetFuseRitualSkill(long ownerID, LocalDimensionPetSpirit info)
	{
		if (info.fuseRitualSkillInfo == null)
		{
			return;
		}
		LocalBattleHandler.Instance.RemoveSkill(ownerID, info.fuseRitualSkillInfo.skillId);
	}

	public void ClearPetFuseRitualSkill(long ownerID)
	{
		if (!this.petSpirit.ContainsKey(ownerID))
		{
			return;
		}
		for (int i = 0; i < this.petSpirit[ownerID].Count; i++)
		{
			if (this.petSpirit[ownerID].ElementValueAt(i).fuseRitualSkillInfo != null)
			{
				LocalBattleHandler.Instance.RemoveSkill(ownerID, this.petSpirit[ownerID].ElementValueAt(i).fuseRitualSkillInfo.skillId);
			}
		}
	}

	protected void OnPetDie(LocalDimensionPetSpirit info, long ownerID)
	{
		this.ReleasePet(info, true);
		EntityParent entityByID = EntityWorld.Instance.GetEntityByID(ownerID);
		if (entityByID == null)
		{
			return;
		}
		if (entityByID.IsEntitySelfType)
		{
			this.RemovePetSummonRitualSkill(ownerID, info);
			this.RemovePetManualSkill(ownerID, info);
			this.RemovePetFuseRitualSkill(ownerID, info);
		}
		for (int i = 0; i < info.currentSummonOwnerAttr.get_Count(); i++)
		{
			entityByID.BattleBaseAttrs.RemoveValuesByTemplateID(info.currentSummonOwnerAttr.get_Item(i));
		}
		this.CheckCurrentPetAttrChange();
	}

	public void MarkSelfPet(EntityPet pet)
	{
		if (EntityWorld.Instance.EntCurPet.ContainsKey(pet.ID))
		{
			EntityWorld.Instance.EntCurPet.set_Item(pet.ID, pet);
		}
		else
		{
			EntityWorld.Instance.EntCurPet.Add(pet.ID, pet);
		}
		BattleBlackboard.Instance.AttendPet = pet.ID;
	}

	public void UnmarkSelfPet(EntityPet pet)
	{
		if (EntityWorld.Instance.EntCurPet.ContainsKey(pet.ID))
		{
			EntityWorld.Instance.EntCurPet.Remove(pet.ID);
		}
	}

	public List<int> GetPreloadPetIDs()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.petSpirit.Values.get_Count(); i++)
		{
			for (int j = 0; j < this.petSpirit.Values.get_Item(i).Count; j++)
			{
				list.Add(this.petSpirit.Values.get_Item(i)[j].TypeID);
			}
		}
		return list;
	}

	protected void ResetMosnter()
	{
		this.currentBatch = 1;
		this.maxBatches = 0;
		this.currentBatchMinMass = 0;
		this.monsterGenerator.Clear();
		this.sleepingFriendlyNPC.Clear();
		this.sleepingBoss.Clear();
		for (int i = 0; i < this.startGenerateTimerList.get_Count(); i++)
		{
			TimerHeap.DelTimer(this.startGenerateTimerList.get_Item(i));
		}
		this.startGenerateTimerList.Clear();
		for (int j = 0; j < this.finishGenerateTimerList.get_Count(); j++)
		{
			TimerHeap.DelTimer(this.finishGenerateTimerList.get_Item(j));
		}
		this.finishGenerateTimerList.Clear();
		for (int k = 0; k < this.waitingGenerateTimerList.get_Count(); k++)
		{
			TimerHeap.DelTimer(this.waitingGenerateTimerList.get_Item(k));
		}
		this.waitingGenerateTimerList.Clear();
		this.waitingGenerateCount = 0;
		for (int l = 0; l < this.generatorRangeChecker.Count; l++)
		{
			this.generatorRangeChecker.ElementKeyAt(l).StopCheck();
		}
		this.generatorRangeChecker.Clear();
		this.monsterGeneratorBlockList.Clear();
		this.aliveEnemyMonsterInBatch.Clear();
		this.aliveEnemyMonsterOutOfBatch.Clear();
		this.aliveFriendlyMonsterInBatch.Clear();
		this.aliveFriendlyMonsterOutOfBatch.Clear();
		this.aliveBoss.Clear();
		this.aliveFriendlyNPC.Clear();
		this.summonMonsterList.Clear();
		this.currentMonsterMass = 0;
		this.maxMonsterMass = 0;
		this.IsBossDieEnd = false;
		this.ResetMonsterSpirit();
	}

	public void SetMonster(List<int> batches, int maxMass, int monsterRefreshAppointedLevel)
	{
		if (batches == null)
		{
			return;
		}
		this.MaxBatches = batches.get_Count();
		this.MaxMonsterMass = maxMass;
		this.SetMonsterGenerator(batches, monsterRefreshAppointedLevel);
	}

	protected void SetCurrentBatchMinMass()
	{
		int num = 0;
		for (int i = 0; i < this.monsterGenerator.Count; i++)
		{
			if (this.monsterGenerator.ElementKeyAt(i) == this.CurrentBatch)
			{
				List<LocalMonsterGenerator> list = this.monsterGenerator.ElementValueAt(i);
				for (int j = 0; j < list.get_Count(); j++)
				{
					if (list.get_Item(j).generator.minMass > num)
					{
						num = list.get_Item(j).generator.minMass;
					}
				}
				break;
			}
		}
		this.CurrentBatchMinMass = num;
	}

	protected void SetMonsterGenerator(List<int> batches, int monsterRefreshAppointedLevel)
	{
		for (int i = 0; i < batches.get_Count(); i++)
		{
			BoCiBiao boCiBiao = DataReader<BoCiBiao>.Get(batches.get_Item(i));
			if (boCiBiao != null)
			{
				if (boCiBiao.monsterRefreshId.get_Count() != 0)
				{
					this.monsterGenerator.Add(i + 1, new List<LocalMonsterGenerator>());
					for (int j = 0; j < boCiBiao.monsterRefreshId.get_Count(); j++)
					{
						MonsterRefresh monsterRefresh = DataReader<MonsterRefresh>.Get(boCiBiao.monsterRefreshId.get_Item(j));
						if (monsterRefresh != null)
						{
							int monsterDataLevel = this.GetMonsterDataLevel(monsterRefresh, monsterRefreshAppointedLevel);
							int monsterCamp = (monsterRefresh.selfType != 1) ? 3 : EntityWorld.Instance.EntSelf.Camp;
							bool isBoss = monsterRefresh.target == 2;
							LocalMonsterGenerator localMonsterGenerator = new LocalMonsterGenerator();
							localMonsterGenerator.generator = monsterRefresh;
							localMonsterGenerator.monsterInfo.AddRange(this.GetMonsterGenerateMaterial(monsterRefresh, monsterDataLevel, monsterCamp, isBoss));
							for (int k = 0; k < localMonsterGenerator.monsterInfo.get_Count(); k++)
							{
								if (monsterRefresh.selfType == 1 && DataReader<Monster>.Get(localMonsterGenerator.monsterInfo.get_Item(k).monsterInfo.typeId).npcMark > 0)
								{
									this.sleepingFriendlyNPC.Add(localMonsterGenerator.monsterInfo.get_Item(k).monsterInfo.id);
								}
								if (monsterRefresh.target == 2)
								{
									this.sleepingBoss.Add(localMonsterGenerator.monsterInfo.get_Item(k).monsterInfo.id);
								}
							}
							this.monsterGenerator[i + 1].Add(localMonsterGenerator);
						}
					}
				}
			}
		}
		for (int l = 0; l < this.monsterGenerator.Count; l++)
		{
			Debug.Log(string.Concat(new object[]
			{
				"monsterGenerator: ",
				l,
				" ",
				this.monsterGenerator.Keys.get_Item(l),
				" ",
				this.monsterGenerator.Values.get_Item(l).get_Count()
			}));
		}
	}

	protected int GetMonsterDataLevel(MonsterRefresh monsterRefreshData, int monsterRefreshAppointedLevel)
	{
		int refreshLv = monsterRefreshData.refreshLv;
		if (refreshLv == 1)
		{
			return EntityWorld.Instance.EntSelf.Lv;
		}
		if (refreshLv != 2)
		{
			return monsterRefreshData.monsterLv;
		}
		return monsterRefreshAppointedLevel;
	}

	protected List<LocalMonsterGenerateMaterial> GetMonsterGenerateMaterial(MonsterRefresh item, int monsterLevel, int monsterCamp, bool isBoss)
	{
		MonsterRefreshMonsterType refreshType = (MonsterRefreshMonsterType)item.refreshType;
		if (refreshType == MonsterRefreshMonsterType.ID)
		{
			return this.GetConstMonsterGenerateMaterialList(item, monsterLevel, monsterCamp, isBoss);
		}
		if (refreshType != MonsterRefreshMonsterType.InventoryID)
		{
			return new List<LocalMonsterGenerateMaterial>();
		}
		return this.GetRandomMonsterGenerateMaterialList(item, monsterLevel, monsterCamp, isBoss);
	}

	protected List<LocalMonsterGenerateMaterial> GetConstMonsterGenerateMaterialList(MonsterRefresh item, int monsterLevel, int monsterCamp, bool isBoss)
	{
		List<LocalMonsterGenerateMaterial> list = new List<LocalMonsterGenerateMaterial>();
		for (int i = 0; i < item.num; i++)
		{
			MapObjInfo monsterInfo = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, item.monster, monsterLevel, 0L, monsterCamp, isBoss, this.GetSpawnPosition(item.bornPoint), true);
			float flushTime = Random.get_value() * (float)item.time + (float)item.bornDelay;
			list.Add(new LocalMonsterGenerateMaterial
			{
				monsterInfo = monsterInfo,
				flushTime = flushTime
			});
		}
		return list;
	}

	protected List<LocalMonsterGenerateMaterial> GetRandomMonsterGenerateMaterialList(MonsterRefresh item, int monsterLevel, int monsterCamp, bool isBoss)
	{
		List<LocalMonsterGenerateMaterial> list = new List<LocalMonsterGenerateMaterial>();
		if (!DataReader<MonsterRandom>.Contains(item.monster))
		{
			return list;
		}
		MonsterRandom monsterRandom = DataReader<MonsterRandom>.Get(item.monster);
		if (monsterRandom.librariesId.get_Count() > 0 && monsterRandom.num.get_Count() > 0)
		{
			XDict<int, int> randomMonsterIDAndNum = this.GetRandomMonsterIDAndNum(monsterRandom);
			if (randomMonsterIDAndNum.Count >= 0)
			{
				list.AddRange(this.GetRandomMonsterGenerateMaterialInventoryList(randomMonsterIDAndNum, monsterRandom, monsterLevel, monsterCamp, isBoss, (float)item.bornDelay));
			}
		}
		if (monsterRandom.appointedId > 0 && monsterRandom.appointedNum > 0)
		{
			list.AddRange(this.GetRandomMonsterGenerateMaterialAppointedList(monsterRandom, monsterLevel, monsterCamp, isBoss, (float)item.bornDelay));
		}
		return list;
	}

	protected XDict<int, int> GetRandomMonsterIDAndNum(MonsterRandom monsterRandomData)
	{
		XDict<int, int> xDict = new XDict<int, int>();
		int num = 0;
		for (int i = 0; i < monsterRandomData.librariesId.get_Count(); i++)
		{
			if (num < monsterRandomData.num.get_Count())
			{
				List<int> allMonsterIDByMonsterLibraryID = GameDataUtils.GetAllMonsterIDByMonsterLibraryID(monsterRandomData.librariesId.get_Item(i));
				if (i < monsterRandomData.randomTypeNum.get_Count())
				{
					int num2 = (monsterRandomData.randomTypeNum.get_Item(i) >= allMonsterIDByMonsterLibraryID.get_Count()) ? allMonsterIDByMonsterLibraryID.get_Count() : monsterRandomData.randomTypeNum.get_Item(i);
					if (num2 != 0)
					{
						for (int j = 0; j < num2; j++)
						{
							if (num >= monsterRandomData.num.get_Count())
							{
								break;
							}
							int num3 = Random.Range(0, allMonsterIDByMonsterLibraryID.get_Count());
							if (xDict.ContainsKey(allMonsterIDByMonsterLibraryID.get_Item(num3)))
							{
								XDict<int, int> xDict2;
								XDict<int, int> expr_C9 = xDict2 = xDict;
								int num4;
								int expr_D4 = num4 = allMonsterIDByMonsterLibraryID.get_Item(num3);
								num4 = xDict2[num4];
								expr_C9[expr_D4] = num4 + monsterRandomData.num.get_Item(num);
							}
							else
							{
								xDict.Add(allMonsterIDByMonsterLibraryID.get_Item(num3), monsterRandomData.num.get_Item(num));
							}
							num++;
							allMonsterIDByMonsterLibraryID.RemoveAt(num3);
						}
					}
				}
			}
		}
		return xDict;
	}

	protected List<LocalMonsterGenerateMaterial> GetRandomMonsterGenerateMaterialInventoryList(XDict<int, int> monsterIDAndNum, MonsterRandom monsterRandomData, int monsterLevel, int monsterCamp, bool isBoss, float bornDelay)
	{
		List<LocalMonsterGenerateMaterial> list = new List<LocalMonsterGenerateMaterial>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < monsterIDAndNum.Values.get_Count(); i++)
		{
			if (monsterIDAndNum.Values.get_Item(i) > num2)
			{
				num = monsterIDAndNum.Keys.get_Item(i);
				num2 = monsterIDAndNum.Values.get_Item(i);
			}
		}
		int num3 = 0;
		for (int j = 0; j < monsterIDAndNum.Keys.get_Count(); j++)
		{
			if (monsterIDAndNum.Keys.get_Item(j) != num)
			{
				num3 += monsterIDAndNum.Values.get_Item(j);
			}
		}
		int num4 = num2 % monsterRandomData.serialNum;
		int num5 = (num4 != 0) ? (num2 / monsterRandomData.serialNum + 1) : (num2 / monsterRandomData.serialNum);
		List<int> list2 = (num5 - 1 <= num3) ? this.GetRandomMonsterGenerateMaterialInventorySolvableList(monsterIDAndNum, num, monsterRandomData.serialNum, num5, num4) : this.GetRandomMonsterGenerateMaterialInventoryUnsolvableList(monsterIDAndNum);
		for (int k = 0; k < list2.get_Count(); k++)
		{
			MapObjInfo monsterInfo = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, list2.get_Item(k), monsterLevel, 0L, monsterCamp, isBoss, this.GetSpawnPosition(monsterRandomData.bornPoint), true);
			float flushTime = (float)(k * monsterRandomData.inteval) + bornDelay;
			list.Add(new LocalMonsterGenerateMaterial
			{
				monsterInfo = monsterInfo,
				flushTime = flushTime
			});
		}
		return list;
	}

	protected List<int> GetRandomMonsterGenerateMaterialInventoryUnsolvableList(XDict<int, int> monsterIDAndNum)
	{
		List<int> list = new List<int>();
		while (monsterIDAndNum.Count > 0)
		{
			int num = Random.Range(0, monsterIDAndNum.Keys.get_Count());
			int num2 = monsterIDAndNum.Keys.get_Item(num);
			list.Add(num2);
			int num3;
			int expr_39 = num3 = num2;
			num3 = monsterIDAndNum[num3];
			monsterIDAndNum[expr_39] = num3 - 1;
			if (monsterIDAndNum[num2] <= 0)
			{
				monsterIDAndNum.Remove(num2);
			}
		}
		return list;
	}

	protected List<int> GetRandomMonsterGenerateMaterialInventorySolvableList(XDict<int, int> monsterIDAndNum, int maxMonsterID, int serialNum, int groupCount, int restMaxMonsterNum)
	{
		List<int> list = new List<int>();
		List<List<int>> list2 = new List<List<int>>();
		for (int i = 0; i < groupCount; i++)
		{
			list2.Add(new List<int>());
		}
		for (int j = 0; j < groupCount; j++)
		{
			int num = (j != groupCount - 1) ? serialNum : restMaxMonsterNum;
			for (int k = 0; k < num; k++)
			{
				list2.get_Item(j).Add(maxMonsterID);
			}
		}
		int num2 = 0;
		for (int l = 0; l < monsterIDAndNum.Count; l++)
		{
			if (monsterIDAndNum.Keys.get_Item(l) != maxMonsterID)
			{
				for (int m = 0; m < monsterIDAndNum.Values.get_Item(l); m++)
				{
					list2.get_Item(num2).Add(monsterIDAndNum.Keys.get_Item(l));
					num2 = ((num2 + 1 != list2.get_Count()) ? (num2 + 1) : 0);
				}
			}
		}
		for (int n = 0; n < list2.get_Count(); n++)
		{
			int num3 = 0;
			int num4 = list2.get_Item(n).get_Count() - 1;
			for (int num5 = 0; num5 < list2.get_Item(n).get_Count(); num5++)
			{
				if (list2.get_Item(n).get_Item(num5) != maxMonsterID)
				{
					num3 = n;
					break;
				}
			}
			if (num4 > num3)
			{
				for (int num6 = num3; num6 < num4; num6++)
				{
					int num7 = Random.Range(num3, num4);
					int num8 = list2.get_Item(n).get_Item(num6);
					list2.get_Item(n).set_Item(num6, list2.get_Item(n).get_Item(num7));
					list2.get_Item(n).set_Item(num7, num8);
				}
			}
			for (int num9 = 0; num9 < list2.get_Item(n).get_Count(); num9++)
			{
				list.Add(list2.get_Item(n).get_Item(num9));
			}
		}
		return list;
	}

	protected List<LocalMonsterGenerateMaterial> GetRandomMonsterGenerateMaterialAppointedList(MonsterRandom monsterRandomData, int monsterLevel, int monsterCamp, bool isBoss, float bornDelay)
	{
		List<LocalMonsterGenerateMaterial> list = new List<LocalMonsterGenerateMaterial>();
		int i = monsterRandomData.appointedNum;
		int num = 0;
		while (i > 0)
		{
			int num2 = 1;
			if (monsterRandomData.appointedNumRange.get_Count() > 0)
			{
				num2 = ((monsterRandomData.appointedNumRange.get_Count() != 1) ? Random.Range(monsterRandomData.appointedNumRange.get_Item(0), monsterRandomData.appointedNumRange.get_Item(1)) : monsterRandomData.appointedNumRange.get_Item(0));
			}
			if (num2 > i)
			{
				num2 = i;
			}
			i -= num2;
			for (int j = 0; j < num2; j++)
			{
				MapObjInfo monsterInfo = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, monsterRandomData.appointedId, monsterLevel, 0L, monsterCamp, isBoss, this.GetSpawnPosition(monsterRandomData.bornPoint), true);
				float flushTime = (float)(monsterRandomData.appointedDelay + num * monsterRandomData.appointedInterval) + bornDelay;
				list.Add(new LocalMonsterGenerateMaterial
				{
					monsterInfo = monsterInfo,
					flushTime = flushTime
				});
			}
			num++;
		}
		return list;
	}

	protected void ResetGhostMosnter()
	{
		this.ghostMonsterTypeID.Clear();
		this.ghostMonsterLevel.Clear();
		this.ghostMonsterSpawnPoint.Clear();
	}

	protected void SetGhostMonster(List<int> theGhostMonsterTypeID, List<int> theGhostMonsterLevel, List<int> theGhostMonsterSpawnPoint)
	{
		this.ghostMonsterTypeID.AddRange(theGhostMonsterTypeID);
		this.ghostMonsterLevel.AddRange(theGhostMonsterLevel);
		this.ghostMonsterSpawnPoint.AddRange(theGhostMonsterSpawnPoint);
	}

	protected void CheckMonsterGenerator()
	{
		if (this.IsFinished)
		{
			return;
		}
		if (!this.monsterGenerator.ContainsKey(this.CurrentBatch))
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < this.monsterGenerator[this.CurrentBatch].get_Count(); i++)
		{
			num += this.monsterGenerator[this.CurrentBatch].get_Item(i).monsterInfo.get_Count();
		}
		this.WaitingGenerateCount = num;
		List<LocalMonsterGenerator> list = this.monsterGenerator[this.CurrentBatch];
		for (int j = 0; j < list.get_Count(); j++)
		{
			MonsterRefreshGenerateWay way = (MonsterRefreshGenerateWay)list.get_Item(j).generator.way;
			if (way != MonsterRefreshGenerateWay.Directly)
			{
				if (way == MonsterRefreshGenerateWay.ByRange)
				{
					this.BuildMonsterRefreshRangeChecker(list.get_Item(j));
				}
			}
			else
			{
				this.SetGenerateMonster(list.get_Item(j).monsterInfo, list.get_Item(j).generator);
			}
		}
	}

	protected void BuildMonsterRefreshRangeChecker(LocalMonsterGenerator item)
	{
		if (this.IsFinished)
		{
			return;
		}
		Vector2 point = MapDataManager.Instance.GetPoint(this.SceneID, item.generator.bornPoint);
		Vector2 theCenter = new Vector2(point.x * 0.01f, point.y * 0.01f);
		float theRange = (float)item.generator.triggerArea * 0.01f;
		LocalDimensionRangeChecker localDimensionRangeChecker = LocalDimensionRangeChecker.GetLocalDimensionRangeChecker(theCenter, theRange, 500, new Action<int, List<long>>(this.CheckRangeGenerate));
		this.generatorRangeChecker.Add(localDimensionRangeChecker, item);
	}

	protected void CheckRangeGenerate(int rangeCheckID, List<long> insider)
	{
		if (this.IsFinished)
		{
			return;
		}
		for (int i = 0; i < insider.get_Count(); i++)
		{
			if (insider.get_Item(i) == EntityWorld.Instance.EntSelf.ID || this.aliveFriendlyMonsterInBatch.ContainsKey(insider.get_Item(i)) || this.aliveFriendlyMonsterOutOfBatch.ContainsKey(insider.get_Item(i)))
			{
				EventDispatcher.Broadcast<int>(LocalInstanceEvent.TriggerGenarator, rangeCheckID);
				return;
			}
		}
	}

	protected void TriggerRangeGenerator(int id)
	{
		if (this.IsFinished)
		{
			return;
		}
		LocalDimensionRangeChecker localDimensionRangeChecker = null;
		for (int i = 0; i < this.generatorRangeChecker.Count; i++)
		{
			if (this.generatorRangeChecker.ElementKeyAt(i).ID == id)
			{
				LocalMonsterGenerator localMonsterGenerator = this.generatorRangeChecker.ElementValueAt(i);
				localDimensionRangeChecker = this.generatorRangeChecker.ElementKeyAt(i);
				this.SetGenerateMonster(localMonsterGenerator.monsterInfo, localMonsterGenerator.generator);
				if (localMonsterGenerator.generator.eventOne.get_Count() > 0)
				{
					for (int j = 0; j < localMonsterGenerator.generator.eventOne.get_Count(); j++)
					{
						GearParent.TriggerGearEvent(localMonsterGenerator.generator.eventOne.get_Item(j));
					}
				}
				break;
			}
		}
		if (localDimensionRangeChecker == null)
		{
			return;
		}
		localDimensionRangeChecker.StopCheck();
		this.generatorRangeChecker.Remove(localDimensionRangeChecker);
	}

	protected void SetGenerateMonster(List<LocalMonsterGenerateMaterial> infos, MonsterRefresh generator)
	{
		if (this.IsFinished)
		{
			return;
		}
		float num = 3.40282347E+38f;
		float num2 = 0f;
		for (int i = 0; i < infos.get_Count(); i++)
		{
			if (infos.get_Item(i).flushTime > num2)
			{
				num2 = infos.get_Item(i).flushTime;
			}
			if (infos.get_Item(i).flushTime < num)
			{
				num = infos.get_Item(i).flushTime;
			}
			this.waitingGenerateTimerList.Add(TimerHeap.AddTimer<LocalMonsterGenerateMaterial, MonsterRefresh>((uint)infos.get_Item(i).flushTime, 0, new Action<LocalMonsterGenerateMaterial, MonsterRefresh>(this.TryGenerateMonster), infos.get_Item(i), generator));
		}
		this.startGenerateTimerList.Add(TimerHeap.AddTimer((uint)num, 0, delegate
		{
			EventDispatcher.Broadcast<string>(LocalInstanceEvent.DirectlyRefreshBegin, generator.@event);
		}));
	}

	protected void TryGenerateMonster(LocalMonsterGenerateMaterial material, MonsterRefresh generator)
	{
		if (this.IsFinished)
		{
			return;
		}
		if (this.CurrentMonsterMass >= this.MaxMonsterMass)
		{
			this.monsterGeneratorBlockList.Add(new KeyValuePair<LocalMonsterGenerateMaterial, MonsterRefresh>(material, generator));
		}
		else
		{
			this.GenerateMonster(material, generator);
		}
	}

	protected void GenerateMonster(LocalMonsterGenerateMaterial material, MonsterRefresh generator)
	{
		if (this.IsFinished)
		{
			return;
		}
		if (this.CurrentMonsterMass >= this.MaxMonsterMass)
		{
			return;
		}
		Monster monster = DataReader<Monster>.Get(material.monsterInfo.typeId);
		this.CurrentMonsterMass += monster.mass;
		if (this.sleepingFriendlyNPC.Contains(material.monsterInfo.id))
		{
			this.sleepingFriendlyNPC.Remove(material.monsterInfo.id);
			this.aliveFriendlyNPC.Add(material.monsterInfo.id);
		}
		if (this.sleepingBoss.Contains(material.monsterInfo.id))
		{
			this.sleepingBoss.Remove(material.monsterInfo.id);
			this.aliveBoss.Add(material.monsterInfo.id);
		}
		if (generator.alwaysExist == 0 && generator.selfType == 0)
		{
			this.aliveEnemyMonsterInBatch.Add(material.monsterInfo.id, generator);
		}
		else if (generator.alwaysExist == 1 && generator.selfType == 0)
		{
			this.aliveEnemyMonsterOutOfBatch.Add(material.monsterInfo.id, generator);
		}
		else if (generator.alwaysExist == 0 && generator.selfType == 1)
		{
			this.aliveFriendlyMonsterInBatch.Add(material.monsterInfo.id, generator);
		}
		else
		{
			this.aliveFriendlyMonsterOutOfBatch.Add(material.monsterInfo.id, generator);
		}
		this.WaitingGenerateCount--;
		this.MonsterEnterField(this.CreateSimpleSpirit(material.monsterInfo));
		EntityMonster entityMonster = EntityWorld.Instance.CreateMonster(material.monsterInfo, true, 0L);
		if (generator.dialogue.get_Count() > 0)
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(monster.model);
			if (avatarModel != null && BubbleDialogueManager.Instance.AddBubbleDialogueLimit(entityMonster.Actor.FixTransform, (float)avatarModel.height_HP, entityMonster.ID, generator.dialogueMonster))
			{
				BubbleDialogueManager.Instance.SetContentsBySequence(entityMonster.ID, generator.dialogue, generator.dialogueTime, 0);
			}
		}
		if (generator.npcBorn == 1)
		{
			BubbleDialogueManager.Instance.BubbleDialogueTriggerNPCBorn(generator, entityMonster.ID);
		}
	}

	protected void CheckBlockList()
	{
		if (this.IsFinished)
		{
			return;
		}
		List<KeyValuePair<LocalMonsterGenerateMaterial, MonsterRefresh>> list = new List<KeyValuePair<LocalMonsterGenerateMaterial, MonsterRefresh>>(this.monsterGeneratorBlockList);
		this.monsterGeneratorBlockList.Clear();
		for (int i = 0; i < list.get_Count(); i++)
		{
			this.TryGenerateMonster(list.get_Item(i).get_Key(), list.get_Item(i).get_Value());
		}
	}

	protected void CheckToNextBatch(bool isDeadCheck, long monsterID = 0L)
	{
		if (this.IsFinished)
		{
			return;
		}
		if (this.WaitingGenerateCount == 0 && this.generatorRangeChecker.Count == 0 && this.monsterGeneratorBlockList.get_Count() == 0 && ((this.aliveEnemyMonsterInBatch.Count == 0 && this.aliveFriendlyMonsterInBatch.Count == 0) || (this.CurrentBatchMinMass > 0 && this.CurrentMonsterMass <= this.CurrentBatchMinMass)))
		{
			List<long> list = new List<long>();
			for (int i = 0; i < this.aliveFriendlyMonsterOutOfBatch.Count; i++)
			{
				if (this.aliveFriendlyMonsterOutOfBatch.ElementValueAt(i).dieBatch != 0 && this.aliveFriendlyMonsterOutOfBatch.ElementValueAt(i).dieBatch == this.CurrentBatch)
				{
					list.Add(this.aliveFriendlyMonsterOutOfBatch.ElementKeyAt(i));
				}
			}
			for (int j = 0; j < list.get_Count(); j++)
			{
				this.aliveFriendlyMonsterOutOfBatch.Remove(list.get_Item(j));
				EntityParent entityByID = EntityWorld.Instance.GetEntityByID(list.get_Item(j));
				entityByID.Hp = 0L;
			}
			List<long> list2 = new List<long>();
			for (int k = 0; k < this.aliveEnemyMonsterOutOfBatch.Count; k++)
			{
				if (this.aliveEnemyMonsterOutOfBatch.ElementValueAt(k).dieBatch != 0 && this.aliveEnemyMonsterOutOfBatch.ElementValueAt(k).dieBatch == this.CurrentBatch)
				{
					list2.Add(this.aliveEnemyMonsterOutOfBatch.ElementKeyAt(k));
				}
			}
			for (int l = 0; l < list2.get_Count(); l++)
			{
				this.aliveEnemyMonsterOutOfBatch.Remove(list2.get_Item(l));
				EntityParent entityByID2 = EntityWorld.Instance.GetEntityByID(list2.get_Item(l));
				entityByID2.Hp = 0L;
			}
			if (!isDeadCheck)
			{
				this.CurrentBatch++;
			}
			else if (this.monsterGenerator.ContainsKey(this.CurrentBatch))
			{
				List<LocalMonsterGenerator> list3 = this.monsterGenerator[this.CurrentBatch];
				for (int m = 0; m < list3.get_Count(); m++)
				{
					for (int n = 0; n < list3.get_Item(m).monsterInfo.get_Count(); n++)
					{
						if (list3.get_Item(m).monsterInfo.get_Item(n).monsterInfo.id == monsterID)
						{
							this.CurrentBatch++;
						}
					}
				}
			}
		}
	}

	protected void CreateGhostMonster()
	{
		int num = (this.ghostMonsterTypeID.get_Count() >= this.ghostMonsterLevel.get_Count()) ? this.ghostMonsterLevel.get_Count() : this.ghostMonsterTypeID.get_Count();
		if (num > this.ghostMonsterSpawnPoint.get_Count())
		{
			num = this.ghostMonsterSpawnPoint.get_Count();
		}
		for (int i = 0; i < num; i++)
		{
			this.CreateGhostMonster(this.ghostMonsterTypeID.get_Item(i), this.ghostMonsterLevel.get_Item(i), this.ghostMonsterSpawnPoint.get_Item(i));
		}
	}

	protected void CreateGhostMonster(int monsterTypeID, int monsterLevel, int pointGroupID)
	{
		Monster monster = DataReader<Monster>.Get(monsterTypeID);
		if (this.CurrentMonsterMass >= this.MaxMonsterMass && monster.mass > 0)
		{
			return;
		}
		MapObjInfo info = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, monsterTypeID, monsterLevel, 0L, 3, false, this.GetSpawnPosition(pointGroupID), true);
		this.MonsterEnterField(this.CreateSimpleSpirit(info));
		EntityMonster entityMonster = EntityWorld.Instance.CreateMonster(info, true, 0L);
		if (entityMonster != null)
		{
			this.CurrentMonsterMass += monster.mass;
		}
	}

	public void SummonMonster(int monsterTypeID, int monsterLevel, long ownerID, int camp, int pointGroupID, Quaternion casterRotation, List<int> offset)
	{
		Monster monster = DataReader<Monster>.Get(monsterTypeID);
		if (this.CurrentMonsterMass >= this.MaxMonsterMass && monster.mass > 0)
		{
			return;
		}
		Vector3 spawnPosition = this.GetSpawnPosition(pointGroupID);
		float num = 0f;
		float num2 = 0f;
		if (offset.get_Count() > 0)
		{
			num = (float)offset.get_Item(0) / 100f;
		}
		if (offset.get_Count() > 1)
		{
			num2 = (float)offset.get_Item(1) / 100f;
		}
		Vector3 position = spawnPosition + casterRotation * Vector3.get_forward() * num2 + casterRotation * Vector3.get_left() * num;
		MapObjInfo info = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, monsterTypeID, monsterLevel, ownerID, camp, false, position, true);
		this.MonsterEnterField(this.CreateSimpleSpirit(info));
		EntityMonster entityMonster = EntityWorld.Instance.CreateMonster(info, true, 0L);
		if (entityMonster != null)
		{
			this.CurrentMonsterMass += monster.mass;
			this.summonMonsterList.Add(entityMonster.ID);
		}
	}

	public void SummonMonster(int monsterTypeID, int monsterLevel, long ownerID, int camp, Vector3 pos)
	{
		Monster monster = DataReader<Monster>.Get(monsterTypeID);
		if (this.CurrentMonsterMass >= this.MaxMonsterMass && monster.mass > 0)
		{
			return;
		}
		MapObjInfo info = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, monsterTypeID, monsterLevel, ownerID, camp, false, pos, true);
		this.MonsterEnterField(this.CreateSimpleSpirit(info));
		EntityMonster entityMonster = EntityWorld.Instance.CreateMonster(info, true, 0L);
		if (entityMonster != null)
		{
			this.CurrentMonsterMass += monster.mass;
			this.summonMonsterList.Add(entityMonster.ID);
		}
	}

	public long CreateComponentMonster(int monsterTypeID, int monsterLevel, long ownerID, int camp, long noumenonID)
	{
		MapObjInfo mapObjInfo = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, monsterTypeID, monsterLevel, ownerID, camp, false, Vector3.get_zero(), false);
		this.MonsterEnterField(this.CreateSimpleSpirit(mapObjInfo));
		EntityMonster entityMonster = EntityWorld.Instance.CreateMonster(mapObjInfo, true, noumenonID);
		if (entityMonster != null)
		{
			Monster monster = DataReader<Monster>.Get(entityMonster.TypeID);
			this.CurrentMonsterMass += monster.mass;
			this.summonMonsterList.Add(entityMonster.ID);
		}
		return mapObjInfo.id;
	}

	public void CreateGMMonster(int monsterTypeID, int monsterLevel, int camp, int pointGroupID, bool isBoss)
	{
		Monster monster = DataReader<Monster>.Get(monsterTypeID);
		if (this.CurrentMonsterMass >= this.MaxMonsterMass && monster.mass > 0)
		{
			return;
		}
		MapObjInfo info = LocalDimensionMonsterInfoCreator.CreateMonsterMapObjInfo(this.PoolID, monsterTypeID, monsterLevel, 0L, camp, isBoss, this.GetSpawnPosition(pointGroupID), true);
		this.MonsterEnterField(this.CreateSimpleSpirit(info));
		EntityWorld.Instance.CreateMonster(info, true, 0L);
	}

	protected void OnMonsterDie(long monsterID, int typeID)
	{
		EventDispatcher.Broadcast<int>(LocalInstanceEvent.LocalInstanceMonsterDie, typeID);
		BattleBlackboard.Instance.MonsterDead = typeID;
		Monster monster = DataReader<Monster>.Get(typeID);
		this.CurrentMonsterMass -= monster.mass;
		this.CheckBlockList();
		bool flag = false;
		if (this.CheckFinishCountDown(typeID))
		{
			flag = true;
		}
		if (this.aliveFriendlyNPC.Contains(monsterID))
		{
			flag = true;
			this.aliveFriendlyNPC.Remove(monsterID);
			BattleBlackboard.Instance.IsAnyNPCDead = true;
			if (this.sleepingFriendlyNPC.get_Count() == 0 && this.aliveFriendlyNPC.get_Count() == 0)
			{
				BattleBlackboard.Instance.IsAllNPCDead = true;
			}
		}
		if (this.aliveBoss.Contains(monsterID))
		{
			this.aliveBoss.Remove(monsterID);
			if (this.sleepingBoss.get_Count() == 0 && this.aliveBoss.get_Count() == 0)
			{
				BattleBlackboard.Instance.BossDead = true;
				flag = true;
			}
		}
		if (this.summonMonsterList.Contains(monsterID))
		{
			this.summonMonsterList.Remove(monsterID);
		}
		if (this.aliveFriendlyMonsterInBatch.ContainsKey(monsterID))
		{
			this.aliveFriendlyMonsterInBatch.Remove(monsterID);
		}
		else if (this.aliveEnemyMonsterInBatch.ContainsKey(monsterID))
		{
			this.aliveEnemyMonsterInBatch.Remove(monsterID);
		}
		else if (this.aliveFriendlyMonsterOutOfBatch.ContainsKey(monsterID))
		{
			this.aliveFriendlyMonsterOutOfBatch.Remove(monsterID);
		}
		else if (this.aliveEnemyMonsterOutOfBatch.ContainsKey(monsterID))
		{
			this.aliveEnemyMonsterOutOfBatch.Remove(monsterID);
		}
		if (flag && this.CheckDeadFinish())
		{
			return;
		}
		this.CheckToNextBatch(true, monsterID);
	}

	protected bool CheckFinishCountDown(int typeID)
	{
		if (this.IsFinished)
		{
			return false;
		}
		switch (this.KillCountDownType)
		{
		case KillCountDownType.TotalNumByID:
			if (this.finishKillID.Contains(typeID))
			{
				this.FinishTotalKillCountDown--;
			}
			if (this.FinishTotalKillCountDown == 0)
			{
				BattleBlackboard.Instance.IsKillCountDownEnd = true;
				return true;
			}
			return false;
		case KillCountDownType.Or:
		{
			int i = 0;
			while (i < this.finishKillID.get_Count())
			{
				if (this.finishKillID.get_Item(i) != typeID)
				{
					i++;
				}
				else
				{
					List<int> list;
					List<int> expr_90 = list = this.finishKillNum;
					int num;
					int expr_94 = num = i;
					num = list.get_Item(num);
					expr_90.set_Item(expr_94, num - 1);
					if (this.finishKillNum.get_Item(i) <= 0)
					{
						BattleBlackboard.Instance.IsKillCountDownEnd = true;
						return true;
					}
					break;
				}
			}
			return false;
		}
		case KillCountDownType.And:
		{
			for (int j = 0; j < this.finishKillID.get_Count(); j++)
			{
				if (this.finishKillID.get_Item(j) == typeID)
				{
					List<int> list2;
					List<int> expr_10A = list2 = this.finishKillNum;
					int num;
					int expr_10E = num = j;
					num = list2.get_Item(num);
					expr_10A.set_Item(expr_10E, num - 1);
					break;
				}
			}
			bool flag = false;
			for (int k = 0; k < this.finishKillNum.get_Count(); k++)
			{
				if (this.finishKillNum.get_Item(k) > 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				BattleBlackboard.Instance.IsKillCountDownEnd = true;
				return true;
			}
			return false;
		}
		case KillCountDownType.TotalNum:
			this.FinishTotalKillCountDown--;
			if (this.FinishTotalKillCountDown == 0)
			{
				BattleBlackboard.Instance.IsKillCountDownEnd = true;
				return true;
			}
			return false;
		default:
			return false;
		}
	}

	protected void BossAnimaEnd()
	{
		this.IsBossDieEnd = true;
		this.CheckDeadFinish();
	}

	public List<int> GetPreloadMonsterIDs()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.monsterGenerator.Values.get_Count(); i++)
		{
			for (int j = 0; j < this.monsterGenerator.Values.get_Item(i).get_Count(); j++)
			{
				for (int k = 0; k < this.monsterGenerator.Values.get_Item(i).get_Item(j).monsterInfo.get_Count(); k++)
				{
					if (!list.Contains(this.monsterGenerator.Values.get_Item(i).get_Item(j).monsterInfo.get_Item(k).monsterInfo.typeId))
					{
						list.Add(this.monsterGenerator.Values.get_Item(i).get_Item(j).monsterInfo.get_Item(k).monsterInfo.typeId);
					}
				}
			}
		}
		return list;
	}

	public Dictionary<long, MonsterRefresh> GetCurrentNPCList()
	{
		Dictionary<long, MonsterRefresh> dictionary = new Dictionary<long, MonsterRefresh>();
		for (int i = 0; i < this.aliveFriendlyMonsterInBatch.Count; i++)
		{
			if (this.aliveFriendlyMonsterInBatch.ElementValueAt(i).npcBorn == 1)
			{
				dictionary.Add(this.aliveFriendlyMonsterInBatch.ElementKeyAt(i), this.aliveFriendlyMonsterInBatch.ElementValueAt(i));
			}
		}
		for (int j = 0; j < this.aliveFriendlyMonsterOutOfBatch.Count; j++)
		{
			if (this.aliveFriendlyMonsterOutOfBatch.ElementValueAt(j).npcBorn == 1)
			{
				dictionary.Add(this.aliveFriendlyMonsterOutOfBatch.ElementKeyAt(j), this.aliveFriendlyMonsterOutOfBatch.ElementValueAt(j));
			}
		}
		for (int k = 0; k < this.aliveEnemyMonsterInBatch.Count; k++)
		{
			if (this.aliveEnemyMonsterInBatch.ElementValueAt(k).npcBorn == 1)
			{
				dictionary.Add(this.aliveEnemyMonsterInBatch.ElementKeyAt(k), this.aliveEnemyMonsterInBatch.ElementValueAt(k));
			}
		}
		for (int l = 0; l < this.aliveEnemyMonsterOutOfBatch.Count; l++)
		{
			if (this.aliveEnemyMonsterOutOfBatch.ElementValueAt(l).npcBorn == 1)
			{
				dictionary.Add(this.aliveEnemyMonsterOutOfBatch.ElementKeyAt(l), this.aliveEnemyMonsterOutOfBatch.ElementValueAt(l));
			}
		}
		return dictionary;
	}

	protected void ResetFinishCondition()
	{
		this.FinishTimeLimit = 0;
		if (this.finishRangeChecker != null)
		{
			this.finishRangeChecker.StopCheck();
		}
		this.finishRangeChecker = null;
		this.KillCountDownType = KillCountDownType.None;
		this.FinishTotalKillCountDown = 0;
		this.finishKillID.Clear();
		this.finishKillNum.Clear();
		for (int i = 0; i < this.winCondition.get_Count(); i++)
		{
			int num = this.winCondition.get_Item(i);
			if (num == 4)
			{
				EventDispatcher.RemoveListener<bool>(LocalInstanceEvent.AllInFinishRange, new Callback<bool>(this.SetInstanceFinish));
			}
		}
		this.winCondition.Clear();
		this.loseCondition.Clear();
	}

	protected void SetFinishCondition(List<int> theWinCondition, List<int> theLoseCondition, List<int> finishArgs)
	{
		this.winCondition.AddRange(theWinCondition);
		this.loseCondition.AddRange(theLoseCondition);
		for (int i = 0; i < this.winCondition.get_Count(); i++)
		{
			switch (this.winCondition.get_Item(i))
			{
			case 2:
			case 3:
				if (finishArgs != null)
				{
					if (finishArgs.get_Count() != 0)
					{
						this.FinishTimeLimit = finishArgs.get_Item(0);
					}
				}
				break;
			case 4:
				if (finishArgs != null)
				{
					if (finishArgs.get_Count() >= 3)
					{
						EventDispatcher.AddListener<bool>(LocalInstanceEvent.AllInFinishRange, new Callback<bool>(this.SetInstanceFinish));
						this.FinishTimeLimit = finishArgs.get_Item(0);
						Vector2 point = MapDataManager.Instance.GetPoint(this.SceneID, finishArgs.get_Item(1));
						Vector2 theCenter = new Vector2(point.x * 0.01f, point.y * 0.01f);
						float theRange = (float)finishArgs.get_Item(2) * 0.01f;
						this.finishRangeChecker = LocalDimensionRangeChecker.GetLocalDimensionRangeChecker(theCenter, theRange, 500, new Action<int, List<long>>(this.CheckRangeFinish));
					}
				}
				break;
			case 5:
				if (finishArgs != null)
				{
					if (finishArgs.get_Count() >= 3)
					{
						this.FinishTimeLimit = finishArgs.get_Item(0);
						this.KillCountDownType = (KillCountDownType)finishArgs.get_Item(1);
						switch (this.KillCountDownType)
						{
						case KillCountDownType.TotalNumByID:
							this.FinishTotalKillCountDown = finishArgs.get_Item(2);
							for (int j = 3; j < finishArgs.get_Count(); j++)
							{
								this.finishKillID.Add(finishArgs.get_Item(j));
							}
							break;
						case KillCountDownType.Or:
							for (int k = 2; k < finishArgs.get_Count(); k += 2)
							{
								this.finishKillID.Add(finishArgs.get_Item(k));
							}
							for (int l = 3; l < finishArgs.get_Count(); l += 2)
							{
								this.finishKillNum.Add(finishArgs.get_Item(l));
							}
							break;
						case KillCountDownType.And:
							for (int m = 2; m < finishArgs.get_Count(); m += 2)
							{
								this.finishKillID.Add(finishArgs.get_Item(m));
							}
							for (int n = 3; n < finishArgs.get_Count(); n += 2)
							{
								this.finishKillNum.Add(finishArgs.get_Item(n));
							}
							break;
						case KillCountDownType.TotalNum:
							this.FinishTotalKillCountDown = finishArgs.get_Item(2);
							break;
						}
					}
				}
				break;
			}
		}
		for (int num = 0; num < this.loseCondition.get_Count(); num++)
		{
			int num2 = this.loseCondition.get_Item(num);
			if (num2 == 2)
			{
				if (finishArgs != null)
				{
					if (finishArgs.get_Count() != 0)
					{
						this.FinishTimeLimit = finishArgs.get_Item(0);
					}
				}
			}
		}
	}

	protected bool CheckDeadFinish()
	{
		if (this.IsFinished)
		{
			return true;
		}
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < this.winCondition.get_Count(); i++)
		{
			switch (this.winCondition.get_Item(i))
			{
			case 1:
				if (this.sleepingBoss.get_Count() == 0 && this.aliveBoss.get_Count() == 0)
				{
					InstanceManager.StopAllClientAI(true);
					BattleBlackboard.Instance.IsWinEnd = true;
					flag = true;
				}
				break;
			case 2:
				if (this.LocalTime >= this.FinishTimeLimit && !this.selfSpirit.IsDead)
				{
					BattleBlackboard.Instance.IsWinEnd = true;
					flag = true;
				}
				break;
			case 3:
				if (this.LocalTime >= this.FinishTimeLimit && !BattleBlackboard.Instance.IsAllNPCDead)
				{
					BattleBlackboard.Instance.IsWinEnd = true;
					flag = true;
				}
				break;
			case 5:
				if (this.LocalTime < this.FinishTimeLimit && BattleBlackboard.Instance.IsKillCountDownEnd)
				{
					BattleBlackboard.Instance.IsWinEnd = true;
					flag = true;
				}
				break;
			}
		}
		for (int j = 0; j < this.loseCondition.get_Count(); j++)
		{
			switch (this.loseCondition.get_Item(j))
			{
			case 1:
				if (this.selfSpirit.IsDead || BattleBlackboard.Instance.IsAllNPCDead)
				{
					flag2 = true;
				}
				break;
			case 2:
				if (this.LocalTime >= this.FinishTimeLimit || this.selfSpirit.IsDead || BattleBlackboard.Instance.IsAllNPCDead)
				{
					flag2 = true;
				}
				break;
			case 3:
				if (this.LocalTime >= this.FinishTimeLimit || this.selfSpirit.IsDead || BattleBlackboard.Instance.IsAnyNPCDead)
				{
					flag2 = true;
				}
				break;
			}
		}
		if (flag)
		{
			this.SetInstanceFinish(true);
		}
		else if (flag2)
		{
			this.SetInstanceFinish(false);
		}
		return flag;
	}

	protected void CheckRangeFinish(int rangeCheckID, List<long> insider)
	{
		if (this.IsFinished)
		{
			return;
		}
		if (this.sleepingFriendlyNPC.get_Count() > 0)
		{
			return;
		}
		if (this.aliveFriendlyNPC.get_Count() == 0)
		{
			return;
		}
		for (int i = 0; i < this.aliveFriendlyNPC.get_Count(); i++)
		{
			if (!insider.Contains(this.aliveFriendlyNPC.get_Item(i)))
			{
				return;
			}
		}
		BattleBlackboard.Instance.IsAllNPCArrived = true;
		BattleBlackboard.Instance.IsWinEnd = true;
		EventDispatcher.Broadcast<bool>(LocalInstanceEvent.AllInFinishRange, true);
	}

	protected void CheckTimeOutFinish()
	{
		if (this.IsFinished)
		{
			return;
		}
		if (this.FinishTimeLimit == 0)
		{
			return;
		}
		if (this.LocalTime > this.FinishTimeLimit && !this.CheckDeadFinish())
		{
			this.SetInstanceFinish(false);
		}
	}

	protected void CheckInstanceTimeout()
	{
		if (this.IsFinished)
		{
			return;
		}
		if (InstanceManager.CurrentInstanceData == null)
		{
			return;
		}
		if (this.LocalTime > this.TimeOutLimit)
		{
			this.SetInstanceFinish(false);
		}
	}

	protected void SetInstanceFinish(bool isWinFinish)
	{
		if (this.IsFinished)
		{
			return;
		}
		this.IsFinished = true;
		this.IsPauseTimeEscape = true;
		BattleBlackboard.Instance.FinishTime = this.LocalTime;
		ReconnectManager.Instance.DataServerReconnectHandler = DataServerDefaultReconnectHandler.Instance;
		EventDispatcher.Broadcast<bool>(LocalInstanceEvent.LocalInstanceFinish, isWinFinish);
		this.monsterGeneratorBlockList.Clear();
		for (int i = 0; i < this.generatorRangeChecker.Count; i++)
		{
			this.generatorRangeChecker.ElementKeyAt(i).StopCheck();
		}
		this.generatorRangeChecker.Clear();
		if (this.finishRangeChecker != null)
		{
			this.finishRangeChecker.StopCheck();
		}
		this.finishRangeChecker = null;
		this.KillCountDownType = KillCountDownType.None;
		this.FinishTotalKillCountDown = 0;
		this.finishKillID.Clear();
		this.finishKillNum.Clear();
		InstanceManager.StopAllClientAI(true);
		if (isWinFinish)
		{
			EntityWorld.Instance.KillAllMonsters();
		}
	}

	public void TryOperation(LocalBattleOperation operation, InstanceType type)
	{
		if (InstanceManager.CurrentInstanceType == InstanceType.ChangeCareer)
		{
			return;
		}
		NetworkManager.Send(new BattleOperationReq
		{
			operationId = (int)operation,
			dungeonType = (int)type
		}, ServerType.Data);
	}

	protected void OnBattleOperationRes(short state, BattleOperationRes down = null)
	{
		if (down == null)
		{
			return;
		}
		if (state == 0)
		{
			switch (down.operationId)
			{
			case 0:
				this.IsPauseTimeEscape = false;
				break;
			case 1:
				this.IsPauseTimeEscape = true;
				InstanceManager.TryPauseSuccess();
				break;
			case 2:
				this.IsPauseTimeEscape = false;
				InstanceManager.TryResumeSuccess();
				break;
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
			switch (down.operationId)
			{
			case 0:
				this.IsPauseTimeEscape = true;
				break;
			case 1:
				this.IsPauseTimeEscape = false;
				InstanceManager.TryPauseFailed();
				break;
			case 2:
				this.IsPauseTimeEscape = true;
				InstanceManager.TryResumeFailed();
				break;
			}
		}
	}

	public void Fuck()
	{
		Debug.Log(string.Concat(new object[]
		{
			"CurrentBatch: ",
			this.CurrentBatch,
			" MaxBatches: ",
			this.MaxBatches,
			" CurrentBatchMinMass: ",
			this.CurrentBatchMinMass,
			" monsterGenerator.Count: ",
			this.monsterGenerator.Count,
			" sleepingFriendlyNPC: ",
			this.sleepingFriendlyNPC.get_Count(),
			" sleepingBoss: ",
			this.sleepingBoss.get_Count(),
			" startGenerateTimerList: ",
			this.startGenerateTimerList.get_Count(),
			" finishGenerateTimerList: ",
			this.finishGenerateTimerList.get_Count(),
			" waitingGenerateTimerList: ",
			this.waitingGenerateTimerList.get_Count(),
			" WaitingGenerateCount: ",
			this.WaitingGenerateCount,
			" generatorRangeChecker: ",
			this.generatorRangeChecker.Count,
			" monsterGeneratorBlockList: ",
			this.monsterGeneratorBlockList.get_Count(),
			" aliveFriendlyMonsterInBatch: ",
			this.aliveFriendlyMonsterInBatch.Count,
			" aliveFriendlyMonsterOutOfBatch: ",
			this.aliveFriendlyMonsterOutOfBatch.Count,
			" aliveEnemyMonsterInBatch: ",
			this.aliveEnemyMonsterInBatch.Count,
			" aliveEnemyMonsterOutOfBatch: ",
			this.aliveEnemyMonsterOutOfBatch.Count,
			" aliveFriendlyNPC: ",
			this.aliveFriendlyNPC.get_Count(),
			" aliveBoss: ",
			this.aliveBoss.get_Count(),
			" summonMonsterList: ",
			this.summonMonsterList.get_Count(),
			" CurrentMonsterMass: ",
			this.CurrentMonsterMass,
			" MaxMonsterMass: ",
			this.MaxMonsterMass
		}));
	}
}
