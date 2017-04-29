using EntitySubSystem;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class EntityMonster : EntityParent
{
	protected BattleBaseAttrs battleBaseAttrs = new BattleBaseAttrs();

	protected bool isEternalInvisible;

	protected bool isGolem;

	protected bool isNoumenonActive;

	protected bool isComponontActive;

	protected long noumenonID;

	protected EntityParent noumenon;

	protected List<long> componontIDs = new List<long>();

	protected List<long> aliveComponontIDs = new List<long>();

	protected bool isNoumenonDead;

	protected bool isComponontAllDeadCastDie = true;

	protected uint instanceEndCameraTimer;

	public override BattleBaseAttrs BattleBaseAttrs
	{
		get
		{
			return this.battleBaseAttrs;
		}
	}

	public override int MoveSpeed
	{
		get
		{
			return this.BattleBaseAttrs.MoveSpeed;
		}
		set
		{
			this.BattleBaseAttrs.MoveSpeed = value;
		}
	}

	public override int ActSpeed
	{
		get
		{
			return this.BattleBaseAttrs.ActSpeed;
		}
		set
		{
			this.BattleBaseAttrs.ActSpeed = value;
		}
	}

	public override int Lv
	{
		get
		{
			return this.BattleBaseAttrs.Lv;
		}
		set
		{
			this.BattleBaseAttrs.Lv = value;
		}
	}

	public override long Fighting
	{
		get
		{
			return this.BattleBaseAttrs.Fighting;
		}
		set
		{
			this.BattleBaseAttrs.Fighting = value;
		}
	}

	public override int VipLv
	{
		get
		{
			return this.BattleBaseAttrs.VipLv;
		}
		set
		{
			this.BattleBaseAttrs.VipLv = value;
		}
	}

	public override int Atk
	{
		get
		{
			return this.BattleBaseAttrs.Atk;
		}
		set
		{
			this.BattleBaseAttrs.Atk = value;
		}
	}

	public override int Defence
	{
		get
		{
			return this.BattleBaseAttrs.Defence;
		}
		set
		{
			this.BattleBaseAttrs.Defence = value;
		}
	}

	public override long HpLmt
	{
		get
		{
			return this.BattleBaseAttrs.HpLmt;
		}
		set
		{
			this.BattleBaseAttrs.HpLmt = value;
		}
	}

	public override int PveAtk
	{
		get
		{
			return this.BattleBaseAttrs.PveAtk;
		}
		set
		{
			this.BattleBaseAttrs.PveAtk = value;
		}
	}

	public override int PvpAtk
	{
		get
		{
			return this.BattleBaseAttrs.PvpAtk;
		}
		set
		{
			this.BattleBaseAttrs.PvpAtk = value;
		}
	}

	public override int HitRatio
	{
		get
		{
			return this.BattleBaseAttrs.HitRatio;
		}
		set
		{
			this.BattleBaseAttrs.HitRatio = value;
		}
	}

	public override int DodgeRatio
	{
		get
		{
			return this.BattleBaseAttrs.DodgeRatio;
		}
		set
		{
			this.BattleBaseAttrs.DodgeRatio = value;
		}
	}

	public override int CritRatio
	{
		get
		{
			return this.BattleBaseAttrs.CritRatio;
		}
		set
		{
			this.BattleBaseAttrs.CritRatio = value;
		}
	}

	public override int DecritRatio
	{
		get
		{
			return this.BattleBaseAttrs.DecritRatio;
		}
		set
		{
			this.BattleBaseAttrs.DecritRatio = value;
		}
	}

	public override int CritHurtAddRatio
	{
		get
		{
			return this.BattleBaseAttrs.CritHurtAddRatio;
		}
		set
		{
			this.BattleBaseAttrs.CritHurtAddRatio = value;
		}
	}

	public override int ParryRatio
	{
		get
		{
			return this.BattleBaseAttrs.ParryRatio;
		}
		set
		{
			this.BattleBaseAttrs.ParryRatio = value;
		}
	}

	public override int DeparryRatio
	{
		get
		{
			return this.BattleBaseAttrs.DeparryRatio;
		}
		set
		{
			this.BattleBaseAttrs.DeparryRatio = value;
		}
	}

	public override int ParryHurtDeRatio
	{
		get
		{
			return this.BattleBaseAttrs.ParryHurtDeRatio;
		}
		set
		{
			this.BattleBaseAttrs.ParryHurtDeRatio = value;
		}
	}

	public override int SuckBloodScale
	{
		get
		{
			return this.BattleBaseAttrs.SuckBloodScale;
		}
		set
		{
			this.BattleBaseAttrs.SuckBloodScale = value;
		}
	}

	public override int HurtAddRatio
	{
		get
		{
			return this.BattleBaseAttrs.HurtAddRatio;
		}
		set
		{
			this.BattleBaseAttrs.HurtAddRatio = value;
		}
	}

	public override int HurtDeRatio
	{
		get
		{
			return this.BattleBaseAttrs.HurtDeRatio;
		}
		set
		{
			this.BattleBaseAttrs.HurtDeRatio = value;
		}
	}

	public override int PveHurtAddRatio
	{
		get
		{
			return this.BattleBaseAttrs.PveHurtAddRatio;
		}
		set
		{
			this.BattleBaseAttrs.PveHurtAddRatio = value;
		}
	}

	public override int PveHurtDeRatio
	{
		get
		{
			return this.BattleBaseAttrs.PveHurtDeRatio;
		}
		set
		{
			this.BattleBaseAttrs.PveHurtDeRatio = value;
		}
	}

	public override int PvpHurtAddRatio
	{
		get
		{
			return this.BattleBaseAttrs.PvpHurtAddRatio;
		}
		set
		{
			this.BattleBaseAttrs.PvpHurtAddRatio = value;
		}
	}

	public override int PvpHurtDeRatio
	{
		get
		{
			return this.BattleBaseAttrs.PvpHurtDeRatio;
		}
		set
		{
			this.BattleBaseAttrs.PvpHurtDeRatio = value;
		}
	}

	public override int AtkMulAmend
	{
		get
		{
			return this.BattleBaseAttrs.AtkMulAmend;
		}
		set
		{
			this.BattleBaseAttrs.AtkMulAmend = value;
		}
	}

	public override int DefMulAmend
	{
		get
		{
			return this.BattleBaseAttrs.DefMulAmend;
		}
		set
		{
			this.BattleBaseAttrs.DefMulAmend = value;
		}
	}

	public override int HpLmtMulAmend
	{
		get
		{
			return this.BattleBaseAttrs.HpLmtMulAmend;
		}
		set
		{
			this.BattleBaseAttrs.HpLmtMulAmend = value;
		}
	}

	public override int PveAtkMulAmend
	{
		get
		{
			return this.BattleBaseAttrs.PveAtkMulAmend;
		}
		set
		{
			this.BattleBaseAttrs.PveAtkMulAmend = value;
		}
	}

	public override int PvpAtkMulAmend
	{
		get
		{
			return this.BattleBaseAttrs.PvpAtkMulAmend;
		}
		set
		{
			this.BattleBaseAttrs.PvpAtkMulAmend = value;
		}
	}

	public override int ActPointRecoverSpeedAmend
	{
		get
		{
			return this.BattleBaseAttrs.ActPointRecoverSpeedAmend;
		}
		set
		{
			this.BattleBaseAttrs.ActPointRecoverSpeedAmend = value;
		}
	}

	public override int VpLmt
	{
		get
		{
			return this.BattleBaseAttrs.VpLmt;
		}
		set
		{
			this.BattleBaseAttrs.VpLmt = value;
		}
	}

	public override int VpLmtMulAmend
	{
		get
		{
			return this.BattleBaseAttrs.VpLmtMulAmend;
		}
		set
		{
			this.BattleBaseAttrs.VpLmtMulAmend = value;
		}
	}

	public override int VpAtk
	{
		get
		{
			return this.BattleBaseAttrs.VpAtk;
		}
		set
		{
			this.BattleBaseAttrs.VpAtk = value;
		}
	}

	public override int VpAtkMulAmend
	{
		get
		{
			return this.BattleBaseAttrs.VpAtkMulAmend;
		}
		set
		{
			this.BattleBaseAttrs.VpAtkMulAmend = value;
		}
	}

	public override int VpResume
	{
		get
		{
			return this.BattleBaseAttrs.VpResume;
		}
		set
		{
			this.BattleBaseAttrs.VpResume = value;
		}
	}

	public override int IdleVpResume
	{
		get
		{
			return this.BattleBaseAttrs.IdleVpResume;
		}
		set
		{
			this.BattleBaseAttrs.IdleVpResume = value;
		}
	}

	public override int WaterBuffAddProbAddAmend
	{
		get
		{
			return this.BattleBaseAttrs.WaterBuffAddProbAddAmend;
		}
		set
		{
			this.BattleBaseAttrs.WaterBuffAddProbAddAmend = value;
		}
	}

	public override int WaterBuffDurTimeAddAmend
	{
		get
		{
			return this.BattleBaseAttrs.WaterBuffDurTimeAddAmend;
		}
		set
		{
			this.BattleBaseAttrs.WaterBuffDurTimeAddAmend = value;
		}
	}

	public override int ThunderBuffAddProbAddAmend
	{
		get
		{
			return this.BattleBaseAttrs.ThunderBuffAddProbAddAmend;
		}
		set
		{
			this.BattleBaseAttrs.ThunderBuffAddProbAddAmend = value;
		}
	}

	public override int ThunderBuffDurTimeAddAmend
	{
		get
		{
			return this.BattleBaseAttrs.ThunderBuffDurTimeAddAmend;
		}
		set
		{
			this.BattleBaseAttrs.ThunderBuffDurTimeAddAmend = value;
		}
	}

	public override int HealIncreasePercent
	{
		get
		{
			return this.BattleBaseAttrs.HealIncreasePercent;
		}
		set
		{
			this.BattleBaseAttrs.HealIncreasePercent = value;
		}
	}

	public override int CritAddValue
	{
		get
		{
			return this.BattleBaseAttrs.CritAddValue;
		}
		set
		{
			this.BattleBaseAttrs.CritAddValue = value;
		}
	}

	public override int HpRestore
	{
		get
		{
			return this.BattleBaseAttrs.HpRestore;
		}
		set
		{
			this.BattleBaseAttrs.HpRestore = value;
		}
	}

	public override int ActPointLmt
	{
		get
		{
			return this.BattleBaseAttrs.ActPointLmt;
		}
		set
		{
			this.BattleBaseAttrs.ActPointLmt = value;
		}
	}

	public override int BuffMoveSpeedMulPosAmend
	{
		get
		{
			return this.BattleBaseAttrs.BuffMoveSpeedMulPosAmend;
		}
		set
		{
			this.BattleBaseAttrs.BuffMoveSpeedMulPosAmend = value;
		}
	}

	public override int BuffActSpeedMulPosAmend
	{
		get
		{
			return this.BattleBaseAttrs.BuffActSpeedMulPosAmend;
		}
		set
		{
			this.BattleBaseAttrs.BuffActSpeedMulPosAmend = value;
		}
	}

	public override int SkillTreatScaleBOAtk
	{
		get
		{
			return this.BattleBaseAttrs.SkillTreatScaleBOAtk;
		}
		set
		{
			this.BattleBaseAttrs.SkillTreatScaleBOAtk = value;
		}
	}

	public override int SkillTreatScaleBOHpLmt
	{
		get
		{
			return this.BattleBaseAttrs.SkillTreatScaleBOHpLmt;
		}
		set
		{
			this.BattleBaseAttrs.SkillTreatScaleBOHpLmt = value;
		}
	}

	public override int SkillIgnoreDefenceHurt
	{
		get
		{
			return this.BattleBaseAttrs.SkillIgnoreDefenceHurt;
		}
		set
		{
			this.BattleBaseAttrs.SkillIgnoreDefenceHurt = value;
		}
	}

	public override int SkillNmlDmgScale
	{
		get
		{
			return this.BattleBaseAttrs.SkillNmlDmgScale;
		}
		set
		{
			this.BattleBaseAttrs.SkillNmlDmgScale = value;
		}
	}

	public override int SkillNmlDmgAddAmend
	{
		get
		{
			return this.BattleBaseAttrs.SkillNmlDmgAddAmend;
		}
		set
		{
			this.BattleBaseAttrs.SkillNmlDmgAddAmend = value;
		}
	}

	public override int SkillHolyDmgScaleBOMaxHp
	{
		get
		{
			return this.BattleBaseAttrs.SkillHolyDmgScaleBOMaxHp;
		}
		set
		{
			this.BattleBaseAttrs.SkillHolyDmgScaleBOMaxHp = value;
		}
	}

	public override int SkillHolyDmgScaleBOCurHp
	{
		get
		{
			return this.BattleBaseAttrs.SkillHolyDmgScaleBOCurHp;
		}
		set
		{
			this.BattleBaseAttrs.SkillHolyDmgScaleBOCurHp = value;
		}
	}

	public override int Affinity
	{
		get
		{
			return this.BattleBaseAttrs.Affinity;
		}
		set
		{
			this.BattleBaseAttrs.Affinity = value;
		}
	}

	public override int OnlineTime
	{
		get
		{
			return this.BattleBaseAttrs.OnlineTime;
		}
		set
		{
			this.BattleBaseAttrs.OnlineTime = value;
		}
	}

	public override int ActPoint
	{
		get
		{
			return this.BattleBaseAttrs.ActPoint;
		}
		set
		{
			this.BattleBaseAttrs.ActPoint = value;
		}
	}

	public override long Hp
	{
		get
		{
			return this.BattleBaseAttrs.Hp;
		}
		set
		{
			this.BattleBaseAttrs.Hp = value;
		}
	}

	public override int Vp
	{
		get
		{
			return this.BattleBaseAttrs.Vp;
		}
		set
		{
			this.BattleBaseAttrs.Vp = value;
		}
	}

	public override long RealHpLmt
	{
		get
		{
			return this.BattleBaseAttrs.RealHpLmt;
		}
		set
		{
			this.BattleBaseAttrs.RealHpLmt = value;
		}
	}

	public override int RealVpLmt
	{
		get
		{
			return this.BattleBaseAttrs.RealVpLmt;
		}
		set
		{
			this.BattleBaseAttrs.RealVpLmt = value;
		}
	}

	public override int RealMoveSpeed
	{
		get
		{
			return this.BattleBaseAttrs.RealMoveSpeed;
		}
		set
		{
			this.BattleBaseAttrs.RealMoveSpeed = value;
		}
	}

	public override int RealActionSpeed
	{
		get
		{
			return this.BattleBaseAttrs.RealActionSpeed;
		}
		set
		{
			this.BattleBaseAttrs.RealActionSpeed = value;
		}
	}

	public override string Name
	{
		get
		{
			Monster monster = DataReader<Monster>.Get(this.TypeID);
			if (monster != null)
			{
				return GameDataUtils.GetChineseContent(monster.name, false);
			}
			return string.Empty;
		}
		set
		{
			base.Name = value;
		}
	}

	public override bool IsDead
	{
		get
		{
			return base.IsDead;
		}
		set
		{
			if (base.IsLogicBoss)
			{
				InstanceManager.CurrentInstance.BossDie(this);
			}
			base.IsDead = value;
			if (value)
			{
				this.DeactiveMonster();
				if (DataReader<Monster>.Get(this.TypeID).prompt == 1)
				{
					this.instanceEndCameraTimer = TimerHeap.AddTimer((uint)float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraTime").value), 0, delegate
					{
						FXSpineManager.Instance.BossSuccess();
					});
				}
			}
		}
	}

	public override bool IsCloseRenderer
	{
		get
		{
			return base.IsCloseRenderer;
		}
		set
		{
			if (this.IsEternalInvisible)
			{
				return;
			}
			if (base.IsCloseRenderer && !value && DataReader<Monster>.Get(this.TypeID).nomoreVisible > 0)
			{
				return;
			}
			base.IsCloseRenderer = value;
		}
	}

	public override bool IsWeak
	{
		get
		{
			return base.IsWeak;
		}
		set
		{
			if (base.IsDisplayBoss)
			{
				BattleBlackboard.Instance.BossWeak = value;
			}
			if (value)
			{
				EventDispatcher.BroadcastAsync("GuideManager.BossEnterTired");
			}
			base.IsWeak = value;
		}
	}

	public override bool IsEntityMonsterType
	{
		get
		{
			return true;
		}
	}

	public override int LayerEntityNumber
	{
		get
		{
			return 3;
		}
	}

	public bool IsEternalInvisible
	{
		get
		{
			return this.isEternalInvisible;
		}
		set
		{
			if (value)
			{
				this.IsCloseRenderer = true;
			}
			this.isEternalInvisible = value;
		}
	}

	public bool IsGolem
	{
		get
		{
			return this.isGolem;
		}
		set
		{
			this.isGolem = value;
		}
	}

	public bool IsNoumenon
	{
		get
		{
			return this.componontIDs.get_Count() > 0;
		}
	}

	public bool IsNoumenonActive
	{
		get
		{
			return this.isNoumenonActive;
		}
		set
		{
			this.isNoumenonActive = value;
			if (value)
			{
				this.ConnectMonster();
			}
			else
			{
				this.DisconnectMonster();
			}
		}
	}

	public bool IsComponont
	{
		get
		{
			return this.noumenonID != 0L;
		}
	}

	public bool IsComponontActive
	{
		get
		{
			return this.isComponontActive;
		}
		set
		{
			this.isComponontActive = value;
			if (value)
			{
				this.ConnectMonster();
			}
			else
			{
				this.DisconnectMonster();
			}
		}
	}

	public long NoumenonID
	{
		get
		{
			return this.noumenonID;
		}
		set
		{
			this.noumenonID = value;
			this.SetNoumenon(this.NoumenonID);
		}
	}

	public EntityParent Noumenon
	{
		get
		{
			return this.SetNoumenon(this.NoumenonID);
		}
	}

	public List<long> ComponontIDs
	{
		get
		{
			return new List<long>(this.componontIDs);
		}
	}

	public Dictionary<long, EntityParent> Compononts
	{
		get
		{
			Dictionary<long, EntityParent> dictionary = new Dictionary<long, EntityParent>();
			for (int i = 0; i < this.componontIDs.get_Count(); i++)
			{
				dictionary.Add(this.componontIDs.get_Item(i), EntityWorld.Instance.GetEntityByID(this.componontIDs.get_Item(i)));
			}
			return dictionary;
		}
	}

	public List<long> AliveComponontIDs
	{
		get
		{
			return new List<long>(this.aliveComponontIDs);
		}
	}

	public Dictionary<long, EntityParent> AliveComponents
	{
		get
		{
			Dictionary<long, EntityParent> dictionary = new Dictionary<long, EntityParent>();
			for (int i = 0; i < this.aliveComponontIDs.get_Count(); i++)
			{
				dictionary.Add(this.aliveComponontIDs.get_Item(i), EntityWorld.Instance.GetEntityByID(this.aliveComponontIDs.get_Item(i)));
			}
			return dictionary;
		}
	}

	public Dictionary<long, EntityParent> AllParts
	{
		get
		{
			Dictionary<long, EntityParent> dictionary = new Dictionary<long, EntityParent>();
			if (this.IsNoumenon)
			{
				dictionary.Add(base.ID, this);
				for (int i = 0; i < this.componontIDs.get_Count(); i++)
				{
					dictionary.Add(this.componontIDs.get_Item(i), EntityWorld.Instance.GetEntityByID(this.componontIDs.get_Item(i)));
				}
				return dictionary;
			}
			if (this.IsComponont && this.Noumenon != null)
			{
				return (this.Noumenon as EntityMonster).AllParts;
			}
			dictionary.Add(base.ID, this);
			return dictionary;
		}
	}

	public bool IsNoumenonDead
	{
		get
		{
			return this.isNoumenonDead;
		}
		set
		{
			this.isNoumenonDead = value;
		}
	}

	public bool IsComponontAllDeadCastDie
	{
		get
		{
			return this.isComponontAllDeadCastDie;
		}
		set
		{
			this.isComponontAllDeadCastDie = value;
		}
	}

	public EntityMonster()
	{
		this.battleBaseAttrs.AttrChangedDelegate = new Action<GameData.AttrType, long, long>(this.OnAttrChanged);
	}

	public void OnCreate(MapObjInfo info, bool isClient, long noumenonID)
	{
		base.OnCreate(info, isClient);
		this.NoumenonID = noumenonID;
	}

	public override void SetDataByMapObjInfo(MapObjInfo info, bool isClientCreate = false)
	{
		base.SetDataByMapObjInfo(info, isClientCreate);
		Monster monster = DataReader<Monster>.Get(this.TypeID);
		base.MonsterRank = (EntityParent.MonsterRankType)monster.monstertype;
		base.IsBuffEntity = (monster.buffflag == 1);
		if (monster.invisible > 0)
		{
			this.IsEternalInvisible = true;
		}
		if (monster.golem > 0)
		{
			this.IsGolem = true;
		}
		if (monster.npcMark > 0)
		{
			base.IsNPC = true;
		}
		if (this.IsMixEntity)
		{
			Vector2 monsterFixBornDirection = InstanceManager.GetMonsterFixBornDirection(monster.monsterBornDirection, base.Pos, base.OwnerID, monster.scenePoint);
			base.Dir = new Vector3(monsterFixBornDirection.x, 0f, monsterFixBornDirection.y);
		}
	}

	public override void CreateActor()
	{
		Monster monster = DataReader<Monster>.Get(this.TypeID);
		if (this.IsComponont)
		{
			if (this.Noumenon != null && this.Noumenon.Actor)
			{
				PartMonster partMonster = DataReader<PartMonster>.Get(monster.partId);
				ActorMonster actorMonster = EntityWorld.Instance.GetMonsterActor(this.Noumenon.Actor.FixGameObject, partMonster.hangPoint, partMonster.hangPointWay);
				this.SetComponontActor(actorMonster);
			}
		}
		else if (this.IsGolem)
		{
			ActorMonster actorMonster = EntityWorld.Instance.MonsterHaunt(monster.golem, this.FixModelID);
			if (actorMonster)
			{
				this.SetNoumenonActor(actorMonster);
			}
			else
			{
				Debug.LogError(string.Format("GolemID: {0} 创建GolemActor失败，通知策划修改配置；开始尝试直接创建，ModelID：{1}", monster.golem, this.FixModelID));
				if (this.IsClientDominate)
				{
					base.AsyncLoadID = EntityWorld.Instance.GetMonsterActorAsync(this.FixModelID, new Action<ActorMonster>(this.SetNoumenonActor));
				}
				else
				{
					actorMonster = EntityWorld.Instance.GetMonsterActor(this.FixModelID);
					this.SetNoumenonActor(actorMonster);
				}
			}
		}
		else if (this.IsClientDominate)
		{
			base.AsyncLoadID = EntityWorld.Instance.GetMonsterActorAsync(this.FixModelID, new Action<ActorMonster>(this.SetNoumenonActor));
		}
		else
		{
			ActorMonster actorMonster = EntityWorld.Instance.GetMonsterActor(this.FixModelID);
			this.SetNoumenonActor(actorMonster);
		}
	}

	protected void SetComponontActor(ActorMonster actorMonster)
	{
		Monster monster = DataReader<Monster>.Get(this.TypeID);
		PartMonster partMonster = DataReader<PartMonster>.Get(monster.partId);
		if (!actorMonster)
		{
			Debug.LogError(string.Format("PartId: {0} 创建ComponontActor失败", monster.partId));
			return;
		}
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.FixModelID);
		base.Actor = actorMonster;
		actorMonster.theEntity = this;
		base.Actor.FixGameObject.set_name(base.ID.ToString());
		actorMonster.RelocateAnimator(this.Noumenon.Actor.FixAnimator, partMonster.suffix);
		actorMonster.SetImitationCollider(partMonster.colider, partMonster.localPosition, partMonster.localRotation, partMonster.localScale);
		base.Actor.InitActionPriorityTable();
		base.Actor.DefaultModelHeight = avatarModel.defaultModelHeight;
		base.Actor.ModelHeight = avatarModel.defaultModelHeight;
		base.Actor.IsLockModelDir = (avatarModel.lockDirection == 1);
		base.Actor.RotateSpeed = avatarModel.RotateSpeed;
		base.Actor.RotateInterval = (float)avatarModel.rotateInterval;
		base.Actor.StartRotateAngle = (float)avatarModel.startAngle;
		base.Actor.FinishRotateAngle = (float)avatarModel.finishAngle;
		base.Actor.FloatRate = avatarModel.floatProba;
		ShadowController.ShowShadow(base.ID, base.Actor, false, this.FixModelID);
		ShaderEffectUtils.SetHSV(base.Actor.FixTransform.get_parent(), monster.colour);
		for (int i = 0; i < partMonster.texture.get_Count(); i++)
		{
			Renderer component = XUtility.RecursiveFindGameObject(actorMonster.FixTransform.get_parent().get_gameObject(), partMonster.texture.get_Item(i)).GetComponent<Renderer>();
			if (component != null)
			{
				if (ShadowSlicePlaneMgr.IsShadow(component.get_gameObject()))
				{
					this.shadowRenderer = component;
				}
				else
				{
					this.shaderRenderers.Add(component);
				}
			}
		}
		ShaderEffectUtils.InitHitEffects(this.shaderRenderers, this.hitControls);
		ShaderEffectUtils.InitTransparencys(this.shaderRenderers, this.alphaControls);
		ShaderEffectUtils.SetOutlineStatus(actorMonster.FixTransform.get_parent(), LuminousOutline.Status.Normal);
		if (avatarModel.curve == 1)
		{
			ShaderEffectUtils.SetMeshRenderToLayerFXDistortion(base.Actor.FixTransform);
		}
		this.InitBillboard((float)avatarModel.height_HP, avatarModel.bloodBar);
		WaveBloodManager.Instance.AddWaveBloodControl(base.Actor.FixTransform, (float)avatarModel.height_Damage, base.ID);
		if (base.IsLogicBoss)
		{
			BubbleDialogueManager.Instance.BubbleDialogueTrigger(3, 0);
		}
		this.CheckShowFlag();
		this.InitActorState();
	}

	protected void SetNoumenonActor(ActorMonster actorMonster)
	{
		if (!actorMonster)
		{
			Debug.LogError(string.Format("ModelID: {0} 创建Actor失败", this.FixModelID));
			return;
		}
		Monster monster = DataReader<Monster>.Get(this.TypeID);
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.FixModelID);
		base.Actor = actorMonster;
		actorMonster.theEntity = this;
		base.Actor.FixGameObject.set_name(base.ID.ToString());
		base.Actor.FixGameObject.SetActive(true);
		base.Actor.InitActionPriorityTable();
		base.Actor.CanAnimatorApplyMotion = false;
		base.Actor.DataLayerState = ((avatarModel.collideOff <= 0) ? 0 : 1);
		base.Actor.DefaultModelHeight = avatarModel.defaultModelHeight;
		base.Actor.ModelHeight = avatarModel.defaultModelHeight;
		base.Actor.IsLockModelDir = (avatarModel.lockDirection == 1);
		base.Actor.RotateSpeed = avatarModel.RotateSpeed;
		base.Actor.RotateInterval = (float)avatarModel.rotateInterval;
		base.Actor.StartRotateAngle = (float)avatarModel.startAngle;
		base.Actor.FinishRotateAngle = (float)avatarModel.finishAngle;
		base.Actor.FloatRate = avatarModel.floatProba;
		ShadowController.ShowShadow(base.ID, base.Actor, false, this.FixModelID);
		ShaderEffectUtils.SetHSV(base.Actor.FixTransform, monster.colour);
		ShaderEffectUtils.InitShaderRenderers(base.Actor.FixTransform, this.shaderRenderers, ref this.shadowRenderer, ref this.shadowSlicePlane);
		ShaderEffectUtils.InitHitEffects(this.shaderRenderers, this.hitControls);
		ShaderEffectUtils.InitTransparencys(this.shaderRenderers, this.alphaControls);
		ShaderEffectUtils.SetFadeRightNow(this.alphaControls, false);
		ShaderEffectUtils.SetOutlineStatus(base.Actor.FixTransform, LuminousOutline.Status.Normal);
		if (avatarModel.curve == 1)
		{
			ShaderEffectUtils.SetMeshRenderToLayerFXDistortion(base.Actor.FixTransform);
		}
		this.InitBillboard((float)avatarModel.height_HP, avatarModel.bloodBar);
		WaveBloodManager.Instance.AddWaveBloodControl(base.Actor.FixTransform, (float)avatarModel.height_Damage, base.ID);
		ShadowController.ShowShadow(base.ID, base.Actor, true, this.FixModelID);
		if (InstanceManager.IsShowMonsterBorn(this.TypeID))
		{
			base.Actor.SetBorn();
			if (monster.aiStop != 0)
			{
				InstanceManager.IsAIThinking = false;
			}
		}
		else
		{
			this.BornEnd();
		}
		if (base.IsLogicBoss)
		{
			BubbleDialogueManager.Instance.BubbleDialogueTrigger(3, 0);
		}
		this.CheckShowFlag();
		this.InitActorState();
	}

	protected void CheckShowFlag()
	{
		if (base.Actor)
		{
			PosDirUtility.AddDirectionFlag(PosDirUtility.DirectionFlagType.Monster, base.Actor.FixTransform);
		}
	}

	public override void OnLeaveField()
	{
		this.DeactiveMonster();
		TimerHeap.DelTimer(this.instanceEndCameraTimer);
		if (!base.Actor)
		{
			EntityWorld.Instance.CancelGetMonsterActorAsync(base.AsyncLoadID);
		}
		base.OnLeaveField();
	}

	protected override void InitManager()
	{
		this.m_subSystems.Add("MonsterAI", new MonsterAIManager());
		this.m_subSystems.Add("MonsterBattle", new MonsterBattleManager());
		this.m_subSystems.Add("MonsterBuff", new MonsterBuffManager());
		this.m_subSystems.Add("MonsterCondition", new MonsterConditionManager());
		this.m_subSystems.Add("MonsterSkill", new MonsterSkillManager());
		this.m_subSystems.Add("MonsterWarning", new MonsterWarningManager());
		base.InitManager();
	}

	protected override void InitEntityState()
	{
		base.InitEntityState();
		this.IsInBattle = true;
	}

	public override void InitActorState()
	{
		if (!this.IsComponont)
		{
			base.SetCheckDead(this.Hp);
		}
		Monster monster = DataReader<Monster>.Get(this.TypeID);
		if (monster.mainId > 0)
		{
			MainMonster mainMonster = DataReader<MainMonster>.Get(monster.mainId);
			List<long> list = new List<long>();
			for (int i = 0; i < mainMonster.partId.get_Count(); i++)
			{
				list.Add(LocalInstanceHandler.Instance.CreateComponentMonster(mainMonster.partId.get_Item(i), this.Lv, 0L, this.Camp, base.ID));
			}
			this.SetComponont(list);
			this.IsComponontAllDeadCastDie = (mainMonster.independent == 0);
		}
		if (!this.IsGolem && !this.IsComponont)
		{
			base.SetPos(base.Pos);
			base.SetDir(base.Dir);
			base.SetFloor(base.Floor);
		}
		this.SetIsFighting(this.IsFighting);
		base.SetMoveSpeed((long)this.RealMoveSpeed);
		base.SetDefaultActionSpeed((long)this.ActSpeed);
		base.SetRunActionSpeed((long)this.RealActionSpeed);
		base.SetPressSkill(base.IsSkillPressing, base.PressingSkillID, base.Dir, this.IsSkillInTrustee);
		this.SetModelSundryOption();
		if ((this.IsNoumenon || this.IsComponont) && !this.IsFighting && base.Actor)
		{
			base.Actor.SetAllCollider(false);
			List<Collider> imitationCollider = (base.Actor as ActorMonster).ImitationCollider;
			for (int j = 0; j < imitationCollider.get_Count(); j++)
			{
				imitationCollider.get_Item(j).set_enabled(false);
			}
			ShadowController.ShowShadow(base.ID, base.Actor, true, this.FixModelID);
			this.ResetModelSundryOption();
		}
		this.SetIsCloseRenderer(this.IsCloseRenderer);
		if (DataReader<Monster>.Get(this.TypeID).exclusiveBGM > 0)
		{
			SoundManager.Instance.PlayBGMByID(DataReader<Monster>.Get(this.TypeID).exclusiveBGM);
		}
		EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 7, this, string.Empty);
		EventDispatcher.Broadcast<int, Transform>(CameraEvent.MonsterBorn, this.TypeID, base.Actor.FixTransform);
		if (this.IsClientDominate)
		{
			base.GetConditionManager().RegistCounterSkillCondition(base.GetSkillAllValue());
			if (monster.mode == 1)
			{
				base.GetConditionManager().RegistTriggerSkillCondition(monster.triggerCondition, monster.triggerSkillId);
			}
			if (InstanceManager.IsDebut)
			{
				this.DebutBattle();
			}
		}
		else
		{
			this.DebutBattle();
		}
		if (base.IsDisplayBoss)
		{
			InstanceManager.BossInitEnd(this);
		}
	}

	public override void AddValue(GameData.AttrType type, int value, bool isFirstTry)
	{
		this.BattleBaseAttrs.AddValue(type, value, isFirstTry);
	}

	public override void AddValue(GameData.AttrType type, long value, bool isFirstTry)
	{
		this.BattleBaseAttrs.AddValue(type, value, isFirstTry);
	}

	public override void RemoveValue(GameData.AttrType type, int value, bool isFirstTry)
	{
		this.BattleBaseAttrs.RemoveValue(type, value, isFirstTry);
	}

	public override void RemoveValue(GameData.AttrType type, long value, bool isFirstTry)
	{
		this.BattleBaseAttrs.RemoveValue(type, value, isFirstTry);
	}

	public override void SetValue(GameData.AttrType type, int value, bool isFirstTry)
	{
		this.BattleBaseAttrs.SetValue(type, value, isFirstTry);
	}

	public override void SetValue(GameData.AttrType type, long value, bool isFirstTry)
	{
		this.BattleBaseAttrs.SetValue(type, value, isFirstTry);
	}

	public override long GetValue(GameData.AttrType type)
	{
		return this.BattleBaseAttrs.GetValue(type);
	}

	public override long TryAddValue(GameData.AttrType type, long tryAddValue)
	{
		return this.BattleBaseAttrs.TryAddValue(type, tryAddValue);
	}

	public override void SwapValue(GameData.AttrType type, long oldValue, long newValue)
	{
		this.BattleBaseAttrs.SwapValue(type, oldValue, newValue);
	}

	public override void AddValuesByTemplateID(int templateID)
	{
		this.BattleBaseAttrs.AddValuesByTemplateID(templateID);
	}

	public override void RemoveValuesByTemplateID(int templateID)
	{
		this.BattleBaseAttrs.RemoveValuesByTemplateID(templateID);
	}

	public override BuffCtrlAttrs GetBuffCtrlAttrs(int elementType)
	{
		return this.BattleBaseAttrs.GetBuffCtrlAttrs(elementType);
	}

	public override void SetBuffCtrlAttrs(BuffCtrlAttrs attrs)
	{
		this.BattleBaseAttrs.SetBuffCtrlAttrs(attrs);
	}

	protected override void OnChangeHp(long oldValue, long newValue)
	{
		base.OnChangeHp(oldValue, newValue);
		if (base.IsDisplayBoss)
		{
			InstanceManager.CurrentInstance.BossHpChange(this);
		}
		EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 8, this, (this.Hp / this.RealHpLmt).ToString());
	}

	protected override void OnChangeRealHpLmt(long oldValue, long newValue)
	{
		base.OnChangeRealHpLmt(oldValue, newValue);
		if (base.IsDisplayBoss)
		{
			InstanceManager.CurrentInstance.BossHpLmtChange(this);
		}
		EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 8, this, (this.Hp / this.RealHpLmt).ToString());
	}

	protected override void OnChangeVp(long oldValue, long newValue)
	{
		base.OnChangeVp(oldValue, newValue);
		if (base.IsDisplayBoss)
		{
			InstanceManager.CurrentInstance.BossVpChange(this);
		}
	}

	protected override void OnChangeRealVpLmt(long oldValue, long newValue)
	{
		base.OnChangeRealVpLmt(oldValue, newValue);
		if (base.IsDisplayBoss)
		{
			InstanceManager.CurrentInstance.BossVpLmtChange(this);
		}
	}

	protected override void SetIsFighting(bool state)
	{
		if (!base.Actor)
		{
			return;
		}
		if (state)
		{
			if (this.IsNoumenon)
			{
				this.ActiveNoumenon();
			}
			else if (this.IsComponont)
			{
				this.ActiveComponont();
			}
			else
			{
				this.ActiveMonster();
			}
			this.SetModelSundryOption();
		}
		else if (!this.IsDead)
		{
			if (this.IsNoumenon)
			{
				this.DeactiveNoumenon();
			}
			else if (this.IsComponont)
			{
				this.DeactiveComponont();
			}
			else
			{
				this.DeactiveMonster();
			}
			this.ResetModelSundryOption();
		}
	}

	protected override void SetIsCloseRenderer(bool state)
	{
		base.SetIsCloseRenderer(state);
		if (state)
		{
			this.CloseAllModelSundryOption();
		}
		else
		{
			this.SetModelSundryOption();
		}
	}

	protected void SetModelSundryOption()
	{
		Monster monster = DataReader<Monster>.Get(this.TypeID);
		if (monster == null)
		{
			return;
		}
		List<int> list = new List<int>();
		if (monster.floatBlood == 0)
		{
			list.Add(1);
		}
		if (monster.arrow == 0)
		{
			list.Add(2);
		}
		if (monster.talk == 0)
		{
			list.Add(4);
		}
		if (monster.shadow == 0)
		{
			list.Add(5);
		}
		if (monster.BloodBar == 0)
		{
			list.Add(6);
		}
		BillboardManager.Instance.SetBillboardInfoOptionsOff(base.ID, list);
	}

	protected void ResetModelSundryOption()
	{
		if (this.IsComponont)
		{
			List<int> list = new List<int>();
			Array values = Enum.GetValues(typeof(BillboardManager.BillboardInfoOffOption));
			for (int i = 0; i < values.get_Length(); i++)
			{
				list.Add((int)values.GetValue(i));
			}
			BillboardManager.Instance.SetBillboardInfoOptionsOff(base.ID, list);
		}
		else
		{
			BillboardManager.Instance.SetBillboardInfoOptionsOff(base.ID, new List<int>());
		}
	}

	protected void CloseAllModelSundryOption()
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(2);
		list.Add(4);
		list.Add(5);
		list.Add(6);
		BillboardManager.Instance.SetBillboardInfoOptionsOff(base.ID, list);
	}

	public override void InitBillboard(float height, List<int> bloodBarSize)
	{
		if (base.IsDisplayBoss)
		{
			this.SetSpecialMonsterBillboard((!base.IsPlayerMate) ? 51 : 53, height);
		}
		else if (base.MonsterRank == EntityParent.MonsterRankType.Elite)
		{
			this.SetStandardMonsterBillboard((!base.IsPlayerMate) ? 41 : 42, height, bloodBarSize);
		}
		else if (base.IsNPC)
		{
			this.SetStandardMonsterBillboard((!base.IsPlayerMate) ? 32 : 33, height, bloodBarSize);
		}
		else
		{
			this.SetStandardMonsterBillboard((!base.IsPlayerMate) ? 61 : 62, height, bloodBarSize);
		}
	}

	protected void SetStandardMonsterBillboard(int actorType, float height, List<int> bloodBarSize)
	{
		BillboardManager.Instance.AddBillboardsInfo(actorType, base.Actor, height, base.ID, base.IsLogicBoss, true, !this.IsDead);
		HeadInfoManager.Instance.SetName(actorType, base.ID, this.Lv, this.Name);
		HeadInfoManager.Instance.SetBloodBarSize(base.ID, bloodBarSize);
		this.UpdateBlood();
	}

	protected void SetSpecialMonsterBillboard(int actorType, float height)
	{
		BillboardManager.Instance.AddBillboardsInfo(actorType, base.Actor, height, base.ID, base.IsLogicBoss, true, !this.IsDead);
	}

	protected EntityParent SetNoumenon(long id)
	{
		if (this.noumenon == null)
		{
			this.noumenon = EntityWorld.Instance.GetEntityByID(id);
		}
		return this.noumenon;
	}

	public void ActiveNoumenon()
	{
		if (!this.IsNoumenon)
		{
			return;
		}
		if (this.IsNoumenonActive)
		{
			return;
		}
		this.IsNoumenonActive = true;
	}

	public void DeactiveNoumenon()
	{
		if (!this.IsNoumenon)
		{
			return;
		}
		if (!this.IsNoumenonActive)
		{
			return;
		}
		this.IsNoumenonActive = false;
	}

	public void SetComponont(List<long> theCompononts)
	{
		this.componontIDs.AddRange(theCompononts);
		this.aliveComponontIDs.AddRange(theCompononts);
	}

	public void ActiveComponont()
	{
		if (!this.IsComponont)
		{
			return;
		}
		if (this.IsComponontActive)
		{
			return;
		}
		this.IsComponontActive = true;
	}

	public void DeactiveComponont()
	{
		if (!this.IsComponont)
		{
			return;
		}
		if (!this.IsComponontActive)
		{
			return;
		}
		this.IsComponontActive = false;
	}

	public void NotifyNoumenonDie()
	{
		this.IsNoumenonDead = true;
		this.Hp = 0L;
	}

	public void NotifyComponontDie(long id)
	{
		if (this.aliveComponontIDs.Contains(id))
		{
			this.aliveComponontIDs.Remove(id);
		}
		if (this.IsComponontAllDeadCastDie && this.aliveComponontIDs.get_Count() == 0)
		{
			this.Hp = 0L;
		}
	}

	public void ConnectMonster()
	{
		this.ActiveMonster();
		this.IsFighting = true;
		if (base.Actor)
		{
			ActorMonster actorMonster = base.Actor as ActorMonster;
			if (actorMonster.MountGameObject)
			{
				base.Actor.FixTransform.set_position(actorMonster.MountGameObject.get_transform().get_position());
				base.Actor.FixTransform.set_rotation(actorMonster.MountGameObject.get_transform().get_rotation());
				base.Actor.FixTransform.set_forward(new Vector3(base.Actor.FixTransform.get_forward().x, 0f, base.Actor.FixTransform.get_forward().z));
			}
			base.Actor.SetAllCollider(true);
			for (int i = 0; i < actorMonster.ImitationCollider.get_Count(); i++)
			{
				actorMonster.ImitationCollider.get_Item(i).set_enabled(true);
			}
			ShadowController.ShowShadow(base.ID, base.Actor, false, this.FixModelID);
		}
	}

	public void DisconnectMonster()
	{
		this.DeactiveMonster();
		this.IsFighting = false;
		if (base.Actor)
		{
			ActorMonster actorMonster = base.Actor as ActorMonster;
			base.Actor.SetAllCollider(false);
			for (int i = 0; i < actorMonster.ImitationCollider.get_Count(); i++)
			{
				actorMonster.ImitationCollider.get_Item(i).set_enabled(false);
			}
			ShadowController.ShowShadow(base.ID, base.Actor, true, this.FixModelID);
		}
	}

	protected void ActiveMonster()
	{
		if (this.IsClientDominate)
		{
			base.GetAIManager().Active();
		}
		else
		{
			base.AddNetworkMoveRotateTeleportGoUpAndDownListener();
		}
	}

	protected void DeactiveMonster()
	{
		if (this.IsClientDominate)
		{
			if (base.GetAIManager() != null)
			{
				base.GetAIManager().Deactive();
			}
		}
		else
		{
			base.RemoveNetworkMoveRotateTeleportGoUpAndDownListener();
		}
	}

	public override void BornEnd()
	{
		base.BornEnd();
		if (DataReader<Monster>.Get(this.TypeID).aiStop != 0)
		{
			InstanceManager.IsAIThinking = true;
		}
		EventDispatcher.Broadcast<int>("GuideManager.MonsterBorn", this.TypeID);
	}

	protected override void UpdateHpChangeInfluence(HPChangeMessage data)
	{
		switch (data.hpChangeType)
		{
		case HPChangeMessage.HPChangeType.Normal:
		case HPChangeMessage.HPChangeType.Critical:
		case HPChangeMessage.HPChangeType.Parry:
		case HPChangeMessage.HPChangeType.CriticalAndParry:
			this.CheckHpChangeLiveText();
			this.CheckHpChangeCamera(data.modeType);
			break;
		case HPChangeMessage.HPChangeType.Miss:
			this.CheckHpChangeLiveText();
			break;
		}
		this.CheckHpChangeFlashWhite();
	}

	protected void CheckHpChangeLiveText()
	{
		if (DataReader<Monster>.Get(this.TypeID).hitTips == 0)
		{
			return;
		}
		EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 5, new LiveTextMessage
		{
			contentID = DataReader<Monster>.Get(this.TypeID).hitTips,
			showTime = 2000,
			name = this.Name
		});
	}

	protected void CheckHpChangeCamera(HPChangeMessage.ModeType modeType)
	{
		if (base.IsPlayerMate)
		{
			return;
		}
		if (modeType != HPChangeMessage.ModeType.MonsterBySelf)
		{
			return;
		}
		if (!base.Actor)
		{
			return;
		}
		EventDispatcher.Broadcast<int, Transform>(CameraEvent.MonsterGetHit, this.TypeID, base.Actor.FixTransform);
	}

	protected void CheckHpChangeFlashWhite()
	{
		if (DataReader<Monster>.Get(this.TypeID).flashWhite == 0)
		{
			return;
		}
	}

	public override void DieBegin()
	{
		if (this.IsCloseRenderer)
		{
			this.DisappearDie();
			return;
		}
		if (base.IsClientCreate)
		{
			EventDispatcher.Broadcast<long, int>(LocalInstanceEvent.SetMonsterDie, base.ID, this.TypeID);
		}
		EventDispatcher.Broadcast<int>("GuideManager.MonsterDie", this.TypeID);
		EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 6, this, string.Empty);
		if (base.Actor)
		{
			if (DataReader<Monster>.Get(this.TypeID).flag == 1)
			{
				InstanceManager.SetInstanceDropPreData(this.TypeID, this.FixModelID, base.Actor.FixTransform, XUtility.GetHitRadius(base.Actor.FixTransform), (float)DataReader<AvatarModel>.Get(this.FixModelID).flopRange * 0.01f, (float)DataReader<AvatarModel>.Get(this.FixModelID).flopAngle);
			}
			else
			{
				InstanceManager.SetMonsterDrop(this.typeID, base.Actor.FixTransform, XUtility.GetHitRadius(base.Actor.FixTransform), (float)DataReader<AvatarModel>.Get(this.FixModelID).flopRange * 0.01f, (float)DataReader<AvatarModel>.Get(this.FixModelID).flopAngle);
			}
		}
		if (DataReader<Monster>.Get(this.TypeID).dieTips > 0)
		{
			EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 6, new LiveTextMessage
			{
				contentID = DataReader<Monster>.Get(this.TypeID).dieTips,
				showTime = 2000,
				name = this.Name
			});
		}
		if (this.IsNoumenon)
		{
			this.DeactiveNoumenon();
			using (Dictionary<long, EntityParent>.Enumerator enumerator = this.AliveComponents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<long, EntityParent> current = enumerator.get_Current();
					(current.get_Value() as EntityMonster).NotifyNoumenonDie();
				}
			}
		}
		else if (this.IsComponont)
		{
			this.DeactiveComponont();
			if (this.IsNoumenonDead)
			{
				if (this.IsFighting)
				{
					this.IsFighting = false;
				}
				if (this.IsStatic)
				{
					this.IsStatic = false;
				}
				if (this.IsDizzy)
				{
					this.IsDizzy = false;
				}
				if (this.IsWeak)
				{
					this.IsWeak = false;
				}
				if (base.IsFixed)
				{
					base.IsFixed = false;
				}
				if (this.IsAssault)
				{
					this.IsAssault = false;
				}
				if (this.IsHitMoving)
				{
					this.IsHitMoving = false;
				}
				if (base.IsSuspended)
				{
					base.IsSuspended = false;
				}
				EventDispatcher.Broadcast<long, bool>("BillboardManager.ShowBillboardsInfo", base.ID, false);
				this.DieEnd();
				return;
			}
			(this.Noumenon as EntityMonster).NotifyComponontDie(base.ID);
		}
		if (!base.IsPlayerMate && base.Actor && !this.IsComponont)
		{
			EventDispatcher.Broadcast<Transform>(CameraEvent.MonsterDie, base.Actor.FixTransform);
		}
		base.DieBegin();
	}

	public override void DieEnd()
	{
		if (base.IsLogicBoss)
		{
			EventDispatcher.Broadcast(InstanceManagerEvent.BossDieEnd);
		}
		if (this.IsGolem && base.Actor && DataReader<Monster>.Get(this.TypeID).noCollide > 0)
		{
			base.Actor.SetAllCollider(false);
		}
		base.DieEnd();
	}

	public void DisappearDie()
	{
		if (base.IsClientCreate)
		{
			EventDispatcher.Broadcast<long, int>(LocalInstanceEvent.SetMonsterDie, base.ID, this.TypeID);
		}
		EventDispatcher.Broadcast<int>("GuideManager.MonsterDie", this.TypeID);
		EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 6, this, string.Empty);
		if (DataReader<Monster>.Get(this.TypeID).dieTips > 0)
		{
			EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 6, new LiveTextMessage
			{
				contentID = DataReader<Monster>.Get(this.TypeID).dieTips,
				showTime = 2000,
				name = this.Name
			});
		}
		if (this.IsFighting)
		{
			this.IsFighting = false;
		}
		if (this.IsStatic)
		{
			this.IsStatic = false;
		}
		if (this.IsDizzy)
		{
			this.IsDizzy = false;
		}
		if (this.IsWeak)
		{
			this.IsWeak = false;
		}
		if (base.IsFixed)
		{
			base.IsFixed = false;
		}
		if (this.IsAssault)
		{
			this.IsAssault = false;
		}
		if (this.IsHitMoving)
		{
			this.IsHitMoving = false;
		}
		if (base.IsSuspended)
		{
			base.IsSuspended = false;
		}
		EventDispatcher.Broadcast<long, bool>("BillboardManager.ShowBillboardsInfo", base.ID, false);
		if (base.Actor)
		{
			if (!base.IsPlayerMate && !this.IsComponont)
			{
				EventDispatcher.Broadcast<Transform>(CameraEvent.MonsterDie, base.Actor.FixTransform);
			}
			base.Actor.DeadAnimationEnd();
		}
	}
}
