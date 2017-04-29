using EntitySubSystem;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XEngineActor;
using XNetwork;

public class EntityParent : Entity, ISimpleBaseAttrExtend, IStandardBaseAttrExtend, IBattleBaseAttrExtend, IClientBaseAttr, IClientBattleBaseAttr, IStandardBaseAttr
{
	public class CommonInfoUpdator
	{
		public List<KVType.ENUM> ConnectedType = new List<KVType.ENUM>();

		public Action<XDict<KVType.ENUM, int>, XDict<KVType.ENUM, string>> Update;

		public override bool Equals(object obj)
		{
			if (!(obj is EntityParent.CommonInfoUpdator))
			{
				return false;
			}
			EntityParent.CommonInfoUpdator commonInfoUpdator = obj as EntityParent.CommonInfoUpdator;
			if (this.Update != commonInfoUpdator.Update)
			{
				return false;
			}
			if (this.ConnectedType.get_Count() != commonInfoUpdator.ConnectedType.get_Count())
			{
				return false;
			}
			for (int i = 0; i < this.ConnectedType.get_Count(); i++)
			{
				if (this.ConnectedType.get_Item(i) != commonInfoUpdator.ConnectedType.get_Item(i))
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public enum MonsterRankType
	{
		Rookie = 1,
		Elite,
		Boss
	}

	protected XDict<KVType.ENUM, List<int>> CommonInfoUpdateTable = new XDict<KVType.ENUM, List<int>>();

	protected XDict<int, EntityParent.CommonInfoUpdator> CommonInfoUpdatorMappingTable = new XDict<int, EntityParent.CommonInfoUpdator>();

	protected long id;

	protected string name = string.Empty;

	protected int iconID;

	protected int titleID;

	protected string guildTitle;

	protected int camp;

	protected long teamID;

	protected int typeID;

	protected int typeRank;

	protected int modelID;

	protected MapObjDecorations decorations = new MapObjDecorations();

	protected int element;

	protected int function;

	protected bool isInBattle;

	protected int objType;

	protected long ownerID;

	protected bool isClientDrive;

	protected int floor;

	protected Vector3 pos = Vector3.get_zero();

	protected Vector3 dir = Vector3.get_zero();

	protected int wrapType;

	protected List<long> ownedIDs = new List<long>();

	protected int ownerListIdx;

	protected long finalOwnerID;

	protected IndexList<int, int> skillInfo = new IndexList<int, int>();

	protected Dictionary<int, int> skillLevel = new Dictionary<int, int>();

	protected XDict<int, XDict<GameData.AttrType, BattleSkillAttrAdd>> skillAttrChange = new XDict<int, XDict<GameData.AttrType, BattleSkillAttrAdd>>();

	protected List<BattleSkillExtend> skillExtend = new List<BattleSkillExtend>();

	protected bool isFuse;

	protected bool isFusing;

	protected bool isFighting;

	protected bool isStatic;

	protected bool isDizzy;

	protected bool isFixed;

	protected bool isTaunt;

	protected bool isEndure;

	protected bool isIgnoreFormula;

	protected bool isCloseRenderer;

	protected bool isMoveCast;

	protected bool isAssault;

	protected bool isHitMoving;

	protected bool isSuspended;

	protected bool isSkillInTrustee;

	protected bool isSkillPressing;

	protected int pressingSkillID;

	protected bool isLoading;

	protected bool isUnconspicuous;

	protected bool isWeak;

	protected bool isIncurable;

	protected int asyncLoadID;

	protected ActorParent actor;

	public List<Renderer> shaderRenderers = new List<Renderer>();

	public Renderer shadowRenderer;

	public ShadowSlicePlane shadowSlicePlane;

	public List<ActorHitEffectBase> hitControls = new List<ActorHitEffectBase>();

	public List<AdjustTransparency> alphaControls = new List<AdjustTransparency>();

	protected XDict<string, ISubSystem> m_subSystems = new XDict<string, ISubSystem>();

	protected long oldRealHpLmt;

	protected int oldRealVpLmt;

	protected bool isClientCreate;

	protected bool isShowBornAction;

	protected bool isDead;

	protected EntityParent.MonsterRankType monsterRank = EntityParent.MonsterRankType.Rookie;

	protected bool isBuffEntity;

	protected bool hasMarkHurt;

	protected XPoint aiToPoint;

	protected EntityParent followTarget;

	protected EnemyTargetFixType curEnemyTargetFixType;

	protected EntityParent owner;

	protected long aiTargetID;

	protected EntityParent aiTarget;

	protected long damageSourceID;

	protected int lastTriggerConditionID;

	protected ThinkConditionItem lastTriggerConditionMessage;

	protected EntityParent moveAroundCenter;

	protected bool isNPC;

	public virtual SimpleBaseAttrs SimpleBaseAttrs
	{
		get
		{
			return null;
		}
	}

	public virtual CityBaseAttrs CityBaseAttrs
	{
		get
		{
			return null;
		}
	}

	public virtual BattleBaseAttrs BattleBaseAttrs
	{
		get
		{
			return null;
		}
	}

	public virtual BackUpBattleBaseAttrs BackUpBattleBaseAttrs
	{
		get
		{
			return null;
		}
	}

	public virtual int MoveSpeed
	{
		get
		{
			return 1;
		}
		set
		{
		}
	}

	public virtual int ActSpeed
	{
		get
		{
			return 1;
		}
		set
		{
		}
	}

	public virtual int Lv
	{
		get
		{
			return 1;
		}
		set
		{
		}
	}

	public virtual long Fighting
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual int VipLv
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int Atk
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int Defence
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual long HpLmt
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual int PveAtk
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int PvpAtk
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int HitRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int DodgeRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int CritRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int DecritRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int CritHurtAddRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int ParryRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int DeparryRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int ParryHurtDeRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SuckBloodScale
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int HurtAddRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int HurtDeRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int PveHurtAddRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int PveHurtDeRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int PvpHurtAddRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int PvpHurtDeRatio
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int AtkMulAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int DefMulAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int HpLmtMulAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int PveAtkMulAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int PvpAtkMulAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int ActPointLmt
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int ActPointRecoverSpeedAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int VpLmt
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int VpLmtMulAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int VpAtk
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int VpAtkMulAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int VpResume
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int IdleVpResume
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int ThunderBuffAddProbAddAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int ThunderBuffDurTimeAddAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int WaterBuffAddProbAddAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int WaterBuffDurTimeAddAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int HealIncreasePercent
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int CritAddValue
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int HpRestore
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual long Exp
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual long ExpLmt
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual int Energy
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int EnergyLmt
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int Diamond
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual long Gold
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual int RechargeDiamond
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int Honor
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int CompetitiveCurrency
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual long SkillPoint
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual long Reputation
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual int BuffMoveSpeedMulPosAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int BuffActSpeedMulPosAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SkillTreatScaleBOAtk
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SkillTreatScaleBOHpLmt
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SkillIgnoreDefenceHurt
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SkillNmlDmgScale
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SkillNmlDmgAddAmend
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SkillHolyDmgScaleBOMaxHp
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int SkillHolyDmgScaleBOCurHp
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int Affinity
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int OnlineTime
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int ActPoint
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual long Hp
	{
		get
		{
			return 0L;
		}
		set
		{
		}
	}

	public virtual int Vp
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual long RealHpLmt
	{
		get
		{
			return 1L;
		}
		set
		{
		}
	}

	public virtual int RealVpLmt
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual int RealMoveSpeed
	{
		get
		{
			return 1;
		}
		set
		{
		}
	}

	public virtual int RealActionSpeed
	{
		get
		{
			return 1;
		}
		set
		{
		}
	}

	public long ID
	{
		get
		{
			return this.id;
		}
		protected set
		{
			this.id = value;
		}
	}

	public virtual string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.name = value;
		}
	}

	public int IconID
	{
		get
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.ModelID);
			if (avatarModel != null)
			{
				return avatarModel.icon;
			}
			return 0;
		}
	}

	public virtual int TitleID
	{
		get
		{
			return this.titleID;
		}
		set
		{
			this.titleID = value;
			EventDispatcher.Broadcast<long, int>("BillboardManager.Title", this.ID, value);
		}
	}

	public virtual string GuildTitle
	{
		get
		{
			return this.guildTitle;
		}
		set
		{
			this.guildTitle = value;
			EventDispatcher.Broadcast<long, string>("BillboardManager.GuildTitle", this.ID, value);
		}
	}

	public virtual int Camp
	{
		get
		{
			return this.camp;
		}
		set
		{
			this.camp = value;
			if (this.Actor && this.FixModelID != 0)
			{
				this.ResetBillBoard();
				this.Actor.UpdateLayer();
			}
		}
	}

	public long TeamID
	{
		get
		{
			return this.teamID;
		}
		set
		{
			this.teamID = value;
		}
	}

	public virtual int TypeID
	{
		get
		{
			return this.typeID;
		}
		set
		{
			this.typeID = value;
		}
	}

	public virtual int TypeRank
	{
		get
		{
			return this.typeRank;
		}
		set
		{
			this.typeRank = value;
		}
	}

	public virtual int ModelID
	{
		get
		{
			return this.modelID;
		}
		set
		{
			this.modelID = value;
		}
	}

	public virtual MapObjDecorations Decorations
	{
		get
		{
			return this.decorations;
		}
		set
		{
			this.decorations = value;
		}
	}

	public int Element
	{
		get
		{
			return this.element;
		}
		set
		{
			this.element = value;
		}
	}

	public int Function
	{
		get
		{
			return this.function;
		}
		set
		{
			this.function = value;
		}
	}

	public virtual bool IsInBattle
	{
		get
		{
			return this.isInBattle;
		}
		set
		{
			this.isInBattle = value;
		}
	}

	public virtual bool IsEntitySelfType
	{
		get
		{
			return false;
		}
	}

	public virtual bool IsEntityCityPlayerType
	{
		get
		{
			return false;
		}
	}

	public virtual bool IsEntityPlayerType
	{
		get
		{
			return false;
		}
	}

	public virtual bool IsEntityPetType
	{
		get
		{
			return false;
		}
	}

	public virtual bool IsEntityMonsterType
	{
		get
		{
			return false;
		}
	}

	public int ObjType
	{
		get
		{
			return this.objType;
		}
		set
		{
			this.objType = value;
		}
	}

	public long OwnerID
	{
		get
		{
			return this.ownerID;
		}
		set
		{
			this.ownerID = value;
			this.SetOwner(this.OwnerID);
		}
	}

	public bool IsClientDrive
	{
		get
		{
			return this.isClientDrive;
		}
		set
		{
			this.isClientDrive = value;
		}
	}

	public int Floor
	{
		get
		{
			return this.floor;
		}
		set
		{
			this.floor = value;
			this.SetFloor(value);
		}
	}

	public float CurFloorStandardHeight
	{
		get
		{
			return (float)this.Floor * 30f;
		}
	}

	public Vector3 Pos
	{
		get
		{
			return this.pos;
		}
		set
		{
			this.pos = value;
			this.SetPos(value);
		}
	}

	public Vector3 Dir
	{
		get
		{
			return this.dir;
		}
		set
		{
			this.dir = value;
			this.SetDir(value);
		}
	}

	public int WrapType
	{
		get
		{
			return this.wrapType;
		}
		set
		{
			this.wrapType = value;
		}
	}

	public virtual List<long> OwnedIDs
	{
		get
		{
			return this.ownedIDs;
		}
		set
		{
			this.ownedIDs.Clear();
			if (value == null)
			{
				return;
			}
			this.ownedIDs.AddRange(value);
		}
	}

	public int OwnerListIdx
	{
		get
		{
			return this.ownerListIdx;
		}
		set
		{
			this.ownerListIdx = value;
		}
	}

	public long FinalOwnerID
	{
		get
		{
			return this.finalOwnerID;
		}
		set
		{
			this.finalOwnerID = value;
		}
	}

	protected virtual IndexList<int, int> SkillInfo
	{
		get
		{
			return this.skillInfo;
		}
		set
		{
			if (value == null)
			{
				this.skillInfo = null;
				return;
			}
			this.skillInfo.Clear();
			using (Dictionary<int, int>.Enumerator enumerator = value.GetPairPart().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> current = enumerator.get_Current();
					this.skillInfo.Add(current.get_Key(), current.get_Value());
				}
			}
			List<int> singlePart = value.GetSinglePart();
			for (int i = 0; i < singlePart.get_Count(); i++)
			{
				this.skillInfo.AddValue(singlePart.get_Item(i));
			}
		}
	}

	protected KeyValuePair<int, int> SkillLevel
	{
		set
		{
			if (this.skillLevel.ContainsKey(value.get_Key()))
			{
				this.skillLevel.set_Item(value.get_Key(), value.get_Value());
			}
			else
			{
				this.skillLevel.Add(value.get_Key(), value.get_Value());
			}
		}
	}

	protected KeyValuePair<int, List<BattleSkillAttrAdd>> SkillAttrChange
	{
		set
		{
			if (this.skillAttrChange.ContainsKey(value.get_Key()))
			{
				this.skillAttrChange[value.get_Key()].Clear();
			}
			else
			{
				this.skillAttrChange.Add(value.get_Key(), new XDict<GameData.AttrType, BattleSkillAttrAdd>());
			}
			if (value.get_Value() == null)
			{
				return;
			}
			for (int i = 0; i < value.get_Value().get_Count(); i++)
			{
				if (this.skillAttrChange[value.get_Key()].ContainsKey((GameData.AttrType)value.get_Value().get_Item(i).attrType))
				{
					this.skillAttrChange[value.get_Key()][(GameData.AttrType)value.get_Value().get_Item(i).attrType].multiAdd += value.get_Value().get_Item(i).multiAdd;
					this.skillAttrChange[value.get_Key()][(GameData.AttrType)value.get_Value().get_Item(i).attrType].addiAdd += value.get_Value().get_Item(i).addiAdd;
				}
				else
				{
					this.skillAttrChange[value.get_Key()].Add((GameData.AttrType)value.get_Value().get_Item(i).attrType, value.get_Value().get_Item(i));
				}
			}
		}
	}

	public virtual bool IsFuse
	{
		get
		{
			return this.isFuse;
		}
		set
		{
			this.isFuse = value;
		}
	}

	public bool IsFusing
	{
		get
		{
			return this.isFusing;
		}
		set
		{
			this.isFusing = value;
		}
	}

	public virtual bool IsFighting
	{
		get
		{
			return this.isFighting;
		}
		set
		{
			this.isFighting = value;
			this.SetIsFighting(value);
		}
	}

	public virtual bool IsStatic
	{
		get
		{
			return this.isStatic;
		}
		set
		{
			if (value)
			{
				this.isStatic = value;
				this.IsAssault = false;
				this.IsHitMoving = false;
				this.IsSuspended = false;
				if (this.Actor)
				{
					this.Actor.ClearFreezeFrame();
					this.Actor.ClearStraight();
					this.Actor.UpdateMoveSpeed();
					this.Actor.UpdateActionSpeed();
				}
			}
			else if (this.isStatic)
			{
				this.isStatic = value;
				if (this.Actor)
				{
					this.Actor.CastAction("idle", true, 1f, 0, 0, string.Empty);
					this.Actor.UpdateMoveSpeed();
					this.Actor.UpdateActionSpeed();
				}
			}
			else
			{
				this.isStatic = value;
			}
		}
	}

	public virtual bool IsDizzy
	{
		get
		{
			return this.isDizzy;
		}
		set
		{
			this.isDizzy = value;
			if (value)
			{
				this.IsAssault = false;
				this.IsHitMoving = false;
				this.IsSuspended = false;
			}
			if (this.Actor)
			{
				this.Actor.UpdateMoveSpeed();
			}
		}
	}

	public bool IsFixed
	{
		get
		{
			return this.isFixed;
		}
		set
		{
			this.isFixed = value;
			if (value)
			{
				this.IsAssault = false;
			}
			if (this.Actor)
			{
				this.Actor.UpdateMoveSpeed();
			}
		}
	}

	public bool IsTaunt
	{
		get
		{
			return this.isTaunt;
		}
		set
		{
			this.isTaunt = value;
		}
	}

	public bool IsEndure
	{
		get
		{
			return this.isEndure;
		}
		set
		{
			this.isEndure = value;
		}
	}

	public bool IsIgnoreFormula
	{
		get
		{
			return this.isIgnoreFormula;
		}
		set
		{
			this.isIgnoreFormula = value;
		}
	}

	public virtual bool IsCloseRenderer
	{
		get
		{
			return this.isCloseRenderer;
		}
		set
		{
			this.isCloseRenderer = value;
			this.SetIsCloseRenderer(value);
		}
	}

	public bool IsMoveCast
	{
		get
		{
			return this.isMoveCast;
		}
		set
		{
			this.isMoveCast = value;
		}
	}

	public virtual bool IsAssault
	{
		get
		{
			return this.isAssault;
		}
		set
		{
			this.isAssault = value;
			if (this.Actor)
			{
				this.Actor.UpdateMoveSpeed();
			}
		}
	}

	public virtual bool IsHitMoving
	{
		get
		{
			return this.isHitMoving;
		}
		set
		{
			if (this.isHitMoving && value && this.Actor)
			{
				this.Actor.EndHitMove();
			}
			this.isHitMoving = value;
			if (value)
			{
				this.IsAssault = false;
			}
		}
	}

	public bool IsSuspended
	{
		get
		{
			return this.isSuspended;
		}
		set
		{
			this.isSuspended = value;
		}
	}

	public virtual bool IsSkillInTrustee
	{
		get
		{
			return this.isSkillInTrustee;
		}
		set
		{
			this.isSkillInTrustee = value;
			if (!value && this.GetSkillManager() != null)
			{
				this.GetSkillManager().ClearSkillTrusteeMessage();
			}
		}
	}

	public bool IsSkillPressing
	{
		get
		{
			return this.isSkillPressing;
		}
		set
		{
			this.isSkillPressing = value;
		}
	}

	public int PressingSkillID
	{
		get
		{
			return this.pressingSkillID;
		}
		set
		{
			this.pressingSkillID = value;
		}
	}

	public bool IsLoading
	{
		get
		{
			return this.isLoading;
		}
		set
		{
			this.isLoading = value;
		}
	}

	public bool IsUnconspicuous
	{
		get
		{
			return this.isUnconspicuous;
		}
		set
		{
			this.isUnconspicuous = value;
		}
	}

	public virtual bool IsWeak
	{
		get
		{
			return this.isWeak;
		}
		set
		{
			this.isWeak = value;
			if (this.Actor)
			{
				this.Actor.UpdateMoveSpeed();
			}
		}
	}

	public bool IsIncurable
	{
		get
		{
			return this.isIncurable;
		}
		set
		{
			this.isIncurable = value;
		}
	}

	public int AsyncLoadID
	{
		get
		{
			return this.asyncLoadID;
		}
		set
		{
			this.asyncLoadID = value;
		}
	}

	public ActorParent Actor
	{
		get
		{
			return this.actor;
		}
		set
		{
			if (this.actor != value)
			{
				this.actor = value;
				if (this.GetSkillManager() != null)
				{
					this.GetSkillManager().UpdateActor(value);
				}
				if (this.GetAIManager() != null)
				{
					this.GetAIManager().UpdateActor(value);
				}
				if (this.GetWarningManager() != null)
				{
					this.GetWarningManager().UpdateActor(value);
				}
			}
		}
	}

	public virtual int FixModelID
	{
		get
		{
			return this.ModelID;
		}
	}

	public bool IsVisible
	{
		get
		{
			return this.Actor && this.Actor.get_gameObject().get_activeSelf();
		}
		set
		{
			if (this.Actor)
			{
				this.Actor.get_gameObject().SetActive(value);
			}
		}
	}

	public long OldRealHpLmt
	{
		get
		{
			return this.oldRealHpLmt;
		}
	}

	public int OldRealVpLmt
	{
		get
		{
			return this.oldRealVpLmt;
		}
	}

	public virtual bool IsClientDominate
	{
		get
		{
			return this.IsClientCreate || this.IsClientDrive;
		}
	}

	public virtual bool IsMixEntity
	{
		get
		{
			return !this.IsClientCreate && this.IsClientDrive;
		}
	}

	public bool IsClientCreate
	{
		get
		{
			return this.isClientCreate;
		}
		set
		{
			this.isClientCreate = value;
		}
	}

	public bool IsShowBornAction
	{
		get
		{
			return this.isShowBornAction;
		}
		set
		{
			this.isShowBornAction = value;
		}
	}

	public virtual bool IsDead
	{
		get
		{
			return this.isDead;
		}
		set
		{
			if (value && this.IsClientDominate)
			{
				ExitBattleFieldAnnouncer.Announce(this);
			}
			this.isDead = value;
			if (value)
			{
				this.DieBegin();
			}
			else
			{
				this.Revive();
			}
		}
	}

	public virtual int LayerEntityNumber
	{
		get
		{
			return 0;
		}
	}

	public EntityParent.MonsterRankType MonsterRank
	{
		get
		{
			return this.monsterRank;
		}
		protected set
		{
			this.monsterRank = value;
		}
	}

	public bool IsLogicBoss
	{
		get
		{
			return this.MonsterRank != EntityParent.MonsterRankType.Rookie;
		}
	}

	public bool IsDisplayBoss
	{
		get
		{
			return this.MonsterRank == EntityParent.MonsterRankType.Boss;
		}
	}

	public bool IsBuffEntity
	{
		get
		{
			return this.isBuffEntity;
		}
		protected set
		{
			this.isBuffEntity = value;
		}
	}

	public bool HasMarkHurt
	{
		get
		{
			return this.hasMarkHurt;
		}
		set
		{
			this.hasMarkHurt = value;
		}
	}

	public XPoint AIToPoint
	{
		get
		{
			return this.aiToPoint;
		}
		set
		{
			this.aiToPoint = value;
		}
	}

	public EntityParent FollowTarget
	{
		get
		{
			return this.followTarget;
		}
		set
		{
			this.followTarget = value;
		}
	}

	public EnemyTargetFixType CurEnemyTargetFixType
	{
		get
		{
			return this.curEnemyTargetFixType;
		}
		set
		{
			this.curEnemyTargetFixType = value;
		}
	}

	public EntityParent Owner
	{
		get
		{
			return this.SetOwner(this.OwnerID);
		}
	}

	public bool IsPlayerMate
	{
		get
		{
			return EntityWorld.Instance.EntSelf != null && this.Camp == EntityWorld.Instance.EntSelf.Camp;
		}
	}

	public bool IsPlayerTeamMate
	{
		get
		{
			return EntityWorld.Instance.EntSelf != null && this.Camp == EntityWorld.Instance.EntSelf.Camp && this.TeamID == EntityWorld.Instance.EntSelf.TeamID;
		}
	}

	public long AITargetID
	{
		get
		{
			return this.aiTargetID;
		}
		set
		{
			this.aiTargetID = value;
		}
	}

	public EntityParent AITarget
	{
		get
		{
			if (this.aiTarget != null && !this.aiTarget.IsFighting)
			{
				return null;
			}
			return this.aiTarget;
		}
		set
		{
			this.aiTarget = value;
		}
	}

	public long DamageSourceID
	{
		get
		{
			return this.damageSourceID;
		}
		set
		{
			this.damageSourceID = value;
			if (this.damageSourceID == EntityWorld.Instance.EntSelf.ID || (this.DamageSource != null && this.DamageSource.Owner != null && this.DamageSource.Owner.ID == EntityWorld.Instance.EntSelf.ID))
			{
				this.HasMarkHurt = true;
			}
		}
	}

	public EntityParent DamageSource
	{
		get
		{
			return EntityWorld.Instance.GetEntityByID(this.DamageSourceID);
		}
	}

	public int LastTriggerConditionID
	{
		get
		{
			return this.lastTriggerConditionID;
		}
		set
		{
			this.lastTriggerConditionID = value;
		}
	}

	public ThinkConditionItem LastTriggerConditionMessage
	{
		get
		{
			return this.lastTriggerConditionMessage;
		}
		set
		{
			this.lastTriggerConditionMessage = value;
		}
	}

	public EntityParent MoveAroundCenter
	{
		get
		{
			if (this.moveAroundCenter != null && (!this.moveAroundCenter.IsFighting || this.moveAroundCenter.IsDead))
			{
				return null;
			}
			return this.moveAroundCenter;
		}
		set
		{
			this.moveAroundCenter = value;
		}
	}

	public bool IsNPC
	{
		get
		{
			return this.isNPC;
		}
		set
		{
			this.isNPC = value;
		}
	}

	public override void OnCreate(MapObjInfo info, bool isClientCreate = false)
	{
		this.SetDataByMapObjInfo(info, isClientCreate);
	}

	public virtual void SetDataByMapObjInfo(MapObjInfo info, bool isClientCreate = false)
	{
		if (info == null)
		{
			return;
		}
		this.IsClientCreate = isClientCreate;
		this.ObjType = (int)info.objType;
		this.ID = info.id;
		this.OwnerID = info.ownerId;
		this.TypeID = info.typeId;
		this.ModelID = info.modelId;
		this.Name = info.name;
		this.TypeRank = info.rankValue;
		this.TitleID = info.titleId;
		this.GuildTitle = HeadInfoManager.GetGuildTitle(info.guildInfo);
		this.Floor = info.mapLayer;
		if (info.pos != null)
		{
			this.Pos = PosDirUtility.ToTerrainPoint(info.pos, this.CurFloorStandardHeight);
			this.Dir = new Vector3(info.vector.x, 0f, info.vector.y);
		}
		this.Decorations = info.decorations;
		this.SetMapObjSimpleInfo(info.otherInfo);
		this.SetMapObjCityInfo(info.cityInfo);
		this.SetMapObjBattleInfo(info.battleInfo);
		if (this.IsClientDominate)
		{
			LocalAgent.AddGlobalBuff(this);
		}
	}

	public virtual void SetMapObjSimpleInfo(SimpleBaseInfo info)
	{
		if (info == null)
		{
			return;
		}
		if (this.SimpleBaseAttrs != null)
		{
			this.SimpleBaseAttrs.AssignAllAttrs(info);
		}
	}

	public virtual void SetMapObjCityInfo(CityBaseInfo info)
	{
		if (info == null)
		{
			return;
		}
		if (this.CityBaseAttrs != null)
		{
			this.CityBaseAttrs.AssignAllAttrs(info);
		}
	}

	public virtual void SetMapObjBattleInfo(BattleBaseInfo info)
	{
		if (info == null)
		{
			return;
		}
		this.WrapType = (int)info.wrapType;
		this.Camp = info.camp;
		this.IsClientDrive = info.clientDrive;
		if (this.BattleBaseAttrs != null)
		{
			this.BattleBaseAttrs.AssignAllAttrs(info);
		}
		this.OwnerListIdx = info.ownedListIdx;
		this.OwnedIDs = info.ownedIds;
		this.FinalOwnerID = info.finalOwnerId;
		this.ClearSkill();
		this.ClearSkillLevel();
		this.ClearSkillExtend();
		for (int i = 0; i < info.skills.get_Count(); i++)
		{
			if (info.skills.get_Item(i).skillIdx > 0)
			{
				this.AddSkill(info.skills.get_Item(i).skillIdx, info.skills.get_Item(i).skillId, info.skills.get_Item(i).skillLv, info.skills.get_Item(i).attrAdd);
			}
			else
			{
				this.AddSkill(info.skills.get_Item(i).skillId, info.skills.get_Item(i).skillLv, info.skills.get_Item(i).attrAdd);
			}
		}
		for (int j = 0; j < info.skillExs.get_Count(); j++)
		{
			this.skillExtend.Add(info.skillExs.get_Item(j));
		}
		this.IsLoading = info.isLoading;
		this.IsFuse = info.isFit;
		this.IsFusing = info.isInFit;
		this.isFighting = info.isFighting;
		this.IsFixed = info.isFixed;
		this.IsStatic = info.isStatic;
		this.IsTaunt = info.isTaunt;
		this.IsEndure = info.isSuperArmor;
		this.IsIgnoreFormula = info.isIgnoreDmgFormula;
		this.IsCloseRenderer = info.isCloseRenderer;
		this.IsDizzy = info.isStun;
		this.IsMoveCast = info.isMoveCast;
		this.IsAssault = info.isAssaulting;
		this.IsHitMoving = info.isKnocking;
		this.IsSuspended = info.isSuspended;
		this.IsSkillInTrustee = info.isSkillManaging;
		this.IsSkillPressing = info.isSkillPressing;
		this.PressingSkillID = info.pressingSkillId;
	}

	public virtual void SetDataByMapObjInfoOnRelive(MapObjInfo info, bool isClientCreate = false)
	{
		this.SetDataByMapObjInfo(info, isClientCreate);
	}

	public override void CreateActor()
	{
		this.InitActorState();
	}

	public override void OnDestroy()
	{
	}

	public override void OnEnterField()
	{
		EntityWorld.Instance.AddEntity<EntityParent>(this);
		this.InitManager();
		this.InitEntityState();
	}

	public override void OnLeaveField()
	{
		this.ReleaseActorState();
		this.ReleaseEntityState();
		this.DestroyManager();
		this.LeaveField();
		Transform arg = null;
		if (this.Actor != null)
		{
			arg = this.Actor.get_transform();
		}
		EventDispatcher.Broadcast<long, Transform>("BillboardManager.RemoveBillboards", this.ID, arg);
		if (this.Actor)
		{
			this.Actor.ClearData();
			this.Actor.DestroyScript();
			this.Actor = null;
		}
	}

	protected virtual void LeaveField()
	{
		EntityWorld.Instance.RemoveEntity<EntityParent>(this);
	}

	protected virtual void ResetEntity()
	{
		if (this.SimpleBaseAttrs != null)
		{
			this.SimpleBaseAttrs.ResetAllAttrs();
		}
		if (this.CityBaseAttrs != null)
		{
			this.CityBaseAttrs.ResetAllAttrs();
		}
		if (this.BattleBaseAttrs != null)
		{
			this.BattleBaseAttrs.ResetAllAttrs();
		}
		if (this.BackUpBattleBaseAttrs != null)
		{
			this.BackUpBattleBaseAttrs.ResetAllAttrs();
		}
		this.id = 0L;
		this.name = string.Empty;
		this.iconID = 0;
		this.titleID = 0;
		this.guildTitle = string.Empty;
		this.camp = 0;
		this.teamID = 0L;
		this.typeID = 0;
		this.typeRank = 0;
		this.modelID = 0;
		this.decorations.career = 0;
		this.decorations.modelId = 0;
		this.decorations.equipIds.Clear();
		this.decorations.petUUId = 0L;
		this.decorations.petId = 0;
		this.decorations.petStar = 0;
		this.decorations.wingId = 0;
		this.decorations.wingHidden = true;
		this.decorations.wingLv = 0;
		this.decorations.fashions.Clear();
		this.element = 0;
		this.function = 0;
		this.isInBattle = false;
		this.objType = 0;
		this.ownerID = 0L;
		this.isClientDrive = false;
		this.floor = 0;
		this.pos = Vector3.get_zero();
		this.dir = Vector3.get_zero();
		this.wrapType = 0;
		this.ownedIDs.Clear();
		this.ownerListIdx = 0;
		this.finalOwnerID = 0L;
		this.skillInfo.Clear();
		this.skillLevel.Clear();
		for (int i = 0; i < this.skillAttrChange.Values.get_Count(); i++)
		{
			this.skillAttrChange.Values.get_Item(i).Clear();
		}
		this.skillAttrChange.Clear();
		this.skillExtend.Clear();
		this.isFuse = false;
		this.isFusing = false;
		this.isFighting = false;
		this.isStatic = false;
		this.isDizzy = false;
		this.isFixed = false;
		this.isTaunt = false;
		this.isEndure = false;
		this.isIgnoreFormula = false;
		this.isCloseRenderer = false;
		this.isMoveCast = false;
		this.isAssault = false;
		this.isHitMoving = false;
		this.isSuspended = false;
		this.isSkillInTrustee = false;
		this.isSkillPressing = false;
		this.pressingSkillID = 0;
		this.isLoading = false;
		this.isUnconspicuous = false;
		this.isWeak = false;
		this.isIncurable = false;
		this.asyncLoadID = 0;
		this.actor = null;
		this.shaderRenderers.Clear();
		this.shadowRenderer = null;
		this.shadowSlicePlane = null;
		this.hitControls.Clear();
		this.alphaControls.Clear();
		this.m_subSystems.Clear();
		this.oldRealHpLmt = 0L;
		this.oldRealVpLmt = 0;
		this.isClientCreate = false;
		this.isShowBornAction = false;
		this.isDead = false;
		this.monsterRank = EntityParent.MonsterRankType.Rookie;
		this.hasMarkHurt = false;
		this.aiToPoint = null;
		this.followTarget = null;
		this.curEnemyTargetFixType = EnemyTargetFixType.None;
		this.owner = null;
		this.aiTargetID = 0L;
		this.aiTarget = null;
		this.damageSourceID = 0L;
		this.lastTriggerConditionID = 0;
		this.lastTriggerConditionMessage = null;
		this.moveAroundCenter = null;
		this.isNPC = false;
	}

	protected virtual void InitManager()
	{
		for (int i = 0; i < this.m_subSystems.Values.get_Count(); i++)
		{
			this.m_subSystems.Values.get_Item(i).OnCreate(this);
		}
	}

	protected void DestroyManager()
	{
		for (int i = 0; i < this.m_subSystems.Values.get_Count(); i++)
		{
			this.m_subSystems.Values.get_Item(i).OnDestroy();
		}
		this.m_subSystems.Clear();
	}

	public IBattleManager GetBattleManager()
	{
		string key = base.GetType().get_Name().Replace("Entity", string.Empty) + typeof(BattleManager).get_Name().Replace("Manager", string.Empty);
		if (!this.m_subSystems.ContainsKey(key))
		{
			return null;
		}
		return this.m_subSystems[key] as IBattleManager;
	}

	public ISkillManager GetSkillManager()
	{
		string key = base.GetType().get_Name().Replace("Entity", string.Empty) + typeof(SkillManager).get_Name().Replace("Manager", string.Empty);
		if (!this.m_subSystems.ContainsKey(key))
		{
			return null;
		}
		return this.m_subSystems[key] as ISkillManager;
	}

	public IBuffManager GetBuffManager()
	{
		string key = base.GetType().get_Name().Replace("Entity", string.Empty) + typeof(BuffManager).get_Name().Replace("Manager", string.Empty);
		if (!this.m_subSystems.ContainsKey(key))
		{
			return null;
		}
		return this.m_subSystems[key] as IBuffManager;
	}

	public IAIManager GetAIManager()
	{
		string key = base.GetType().get_Name().Replace("Entity", string.Empty) + typeof(AIManager).get_Name().Replace("Manager", string.Empty);
		if (!this.m_subSystems.ContainsKey(key))
		{
			return null;
		}
		return this.m_subSystems[key] as IAIManager;
	}

	public IConditionManager GetConditionManager()
	{
		string key = base.GetType().get_Name().Replace("Entity", string.Empty) + typeof(ConditionManager).get_Name().Replace("Manager", string.Empty);
		if (!this.m_subSystems.ContainsKey(key))
		{
			return null;
		}
		return this.m_subSystems[key] as IConditionManager;
	}

	public IWarningManager GetWarningManager()
	{
		string key = base.GetType().get_Name().Replace("Entity", string.Empty) + typeof(WarningManager).get_Name().Replace("Manager", string.Empty);
		if (!this.m_subSystems.ContainsKey(key))
		{
			return null;
		}
		return this.m_subSystems[key] as IWarningManager;
	}

	public IFeedbackManager GetFeedbackManager()
	{
		string key = base.GetType().get_Name().Replace("Entity", string.Empty) + typeof(FeedbackManager).get_Name().Replace("Manager", string.Empty);
		if (!this.m_subSystems.ContainsKey(key))
		{
			return null;
		}
		return this.m_subSystems[key] as IFeedbackManager;
	}

	protected virtual void InitEntityState()
	{
		this.AddNetworkListener();
	}

	protected virtual void ReleaseEntityState()
	{
		this.RemoveNetworkListener();
	}

	protected virtual void AddNetworkListener()
	{
		this.AddNetworkAttrChangeListener();
	}

	protected virtual void RemoveNetworkListener()
	{
		this.RemoveNetworkAttrChangeListener();
	}

	public virtual void InitActorState()
	{
		this.SetCheckDead(this.Hp);
		this.SetPos(this.Pos);
		this.SetDir(this.Dir);
		this.SetFloor(this.Floor);
		this.SetIsFighting(this.IsFighting);
		this.SetIsCloseRenderer(this.IsCloseRenderer);
		this.SetMoveSpeed((long)this.RealMoveSpeed);
		this.SetDefaultActionSpeed((long)this.ActSpeed);
		this.SetRunActionSpeed((long)this.RealActionSpeed);
		this.SetPressSkill(this.IsSkillPressing, this.PressingSkillID, this.Dir, this.IsSkillInTrustee);
		if (this.IsClientDominate)
		{
			this.GetConditionManager().RegistCounterSkillCondition(this.GetSkillAllValue());
		}
	}

	protected virtual void ReleaseActorState()
	{
	}

	public virtual void AddNetworkAttrChangeListener()
	{
		NetworkManager.AddListenEvent<RoleAttrChangedNty>(new NetCallBackMethod<RoleAttrChangedNty>(this.UpdateAttr));
	}

	public virtual void RemoveNetworkAttrChangeListener()
	{
		NetworkManager.RemoveListenEvent<RoleAttrChangedNty>(new NetCallBackMethod<RoleAttrChangedNty>(this.UpdateAttr));
	}

	protected virtual void UpdateAttr(short state, RoleAttrChangedNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (this.ID != down.id)
		{
			return;
		}
		for (int i = 0; i < down.attrs.get_Count(); i++)
		{
			this.SetValue((GameData.AttrType)down.attrs.get_Item(i).attrType, down.attrs.get_Item(i).attrValue, true);
		}
	}

	public virtual void AddValue(GameData.AttrType type, int value, bool isFirstTry)
	{
	}

	public virtual void AddValue(GameData.AttrType type, long value, bool isFirstTry)
	{
	}

	public virtual void RemoveValue(GameData.AttrType type, int value, bool isFirstTry)
	{
	}

	public virtual void RemoveValue(GameData.AttrType type, long value, bool isFirstTry)
	{
	}

	public virtual void SetValue(GameData.AttrType type, int value, bool isFirstTry)
	{
	}

	public virtual void SetValue(GameData.AttrType type, long value, bool isFirstTry)
	{
	}

	public virtual long GetValue(GameData.AttrType type)
	{
		return 0L;
	}

	public virtual long TryAddValue(GameData.AttrType type, long tryAddValue)
	{
		return 0L;
	}

	public long TryAddValue(GameData.AttrType type, XDict<GameData.AttrType, long> tryAddValues)
	{
		if (tryAddValues != null && tryAddValues.ContainsKey(type))
		{
			return this.TryAddValue(type, tryAddValues[type]);
		}
		return this.GetValue(type);
	}

	public virtual void SwapValue(GameData.AttrType type, long oldValue, long newValue)
	{
	}

	public virtual void OnAttrChanged(GameData.AttrType attrType, long oldValue, long newValue)
	{
		switch (attrType)
		{
		case GameData.AttrType.MoveSpeed:
			this.OnChangeMoveSpeed(oldValue, newValue);
			return;
		case GameData.AttrType.ActSpeed:
			this.OnChangeAtkSpeed(oldValue, newValue);
			return;
		case GameData.AttrType.Affinity:
		case (GameData.AttrType)104:
		case (GameData.AttrType)105:
			IL_27:
			switch (attrType)
			{
			case GameData.AttrType.VpLmt:
				this.OnChangeVpLmt(oldValue, newValue);
				return;
			case GameData.AttrType.VpLmtMulAmend:
				this.OnChangeVpLmtMulAmend(oldValue, newValue);
				return;
			case GameData.AttrType.VpResume:
			case GameData.AttrType.VpAtk:
			case GameData.AttrType.VpAtkMulAmend:
				IL_4B:
				switch (attrType)
				{
				case GameData.AttrType.RealHpLmt:
					this.OnChangeRealHpLmt(oldValue, newValue);
					return;
				case GameData.AttrType.RealVpLmt:
					this.OnChangeRealVpLmt(oldValue, newValue);
					return;
				case GameData.AttrType.RealMoveSpeed:
					this.OnChangeRealMoveSpeed(oldValue, newValue);
					return;
				case GameData.AttrType.RealActionSpeed:
					this.OnChangeRealActionSpeed(oldValue, newValue);
					return;
				default:
					switch (attrType)
					{
					case GameData.AttrType.BuffMoveSpeedMulPosAmend:
						this.OnChangeBuffMoveSpeedMulPosAmend(oldValue, newValue);
						return;
					case (GameData.AttrType)708:
						IL_7F:
						if (attrType == GameData.AttrType.HpLmt)
						{
							this.OnChangeHpLmt(oldValue, newValue);
							return;
						}
						if (attrType == GameData.AttrType.Lv)
						{
							this.OnChangeLv(oldValue, newValue);
							return;
						}
						if (attrType == GameData.AttrType.Hp)
						{
							this.OnChangeHp(oldValue, newValue);
							return;
						}
						if (attrType != GameData.AttrType.HpLmtMulAmend)
						{
							return;
						}
						this.OnChangeHpLmtMulAmend(oldValue, newValue);
						return;
					case GameData.AttrType.BuffActSpeedMulPosAmend:
						this.OnChangeActSpeedMulPosAmend(oldValue, newValue);
						return;
					}
					goto IL_7F;
				}
				break;
			case GameData.AttrType.Vp:
				this.OnChangeVp(oldValue, newValue);
				return;
			}
			goto IL_4B;
		case GameData.AttrType.ActPoint:
			this.OnChangeActPoint(oldValue, newValue);
			return;
		case GameData.AttrType.ActPointLmt:
			this.OnChangeActPointLmt(oldValue, newValue);
			return;
		}
		goto IL_27;
	}

	public virtual void AddValuesByTemplateID(int templateID)
	{
	}

	public virtual void RemoveValuesByTemplateID(int templateID)
	{
	}

	public virtual BuffCtrlAttrs GetBuffCtrlAttrs(int elementType)
	{
		return null;
	}

	public virtual void SetBuffCtrlAttrs(BuffCtrlAttrs attrs)
	{
	}

	protected virtual void OnChangeLv(long oldValue, long newValue)
	{
	}

	protected virtual void OnChangeHp(long oldValue, long newValue)
	{
		if (newValue <= 0L)
		{
			this.Hp = 0L;
		}
		else if (newValue >= this.RealHpLmt)
		{
			this.Hp = this.RealHpLmt;
		}
		if (this.Actor)
		{
			this.UpdateBlood();
		}
		if (this.IsDead && this.Hp > 0L)
		{
			this.IsDead = false;
		}
		else if (this.Hp == 0L && !this.IsDead)
		{
			this.IsDead = true;
		}
		AttrChangeAnnouncer.Announce(this, GameData.AttrType.Hp, (double)oldValue / (double)this.RealHpLmt, (double)this.Hp / (double)this.RealHpLmt, oldValue, this.Hp);
	}

	protected void OnChangeHpLmt(long oldValue, long newValue)
	{
		this.CalculateRealHpLmt();
	}

	protected void OnChangeHpLmtMulAmend(long oldValue, long newValue)
	{
		this.CalculateRealHpLmt();
	}

	protected virtual void OnChangeRealHpLmt(long oldValue, long newValue)
	{
		this.oldRealHpLmt = oldValue;
		if (this.OldRealHpLmt > 0L)
		{
			AttrChangeAnnouncer.Announce(this, GameData.AttrType.Hp, (double)this.Hp / (double)this.OldRealHpLmt, (double)this.Hp / (double)this.RealHpLmt, this.Hp, this.Hp);
		}
		this.UpdateBlood();
	}

	protected virtual void OnChangeActPoint(long oldValue, long newValue)
	{
		if (newValue <= 0L)
		{
			this.ActPoint = 0;
		}
		else if (newValue >= (long)this.ActPointLmt)
		{
			this.ActPoint = this.ActPointLmt;
		}
		AttrChangeAnnouncer.Announce(this, GameData.AttrType.ActPoint, (double)oldValue / (double)this.ActPointLmt, (double)this.ActPoint / (double)this.ActPointLmt, oldValue, (long)this.ActPoint);
	}

	protected virtual void OnChangeActPointLmt(long oldValue, long newValue)
	{
		AttrChangeAnnouncer.Announce(this, GameData.AttrType.ActPoint, (double)this.ActPoint / (double)oldValue, (double)this.ActPoint / (double)newValue, oldValue, newValue);
	}

	protected virtual void OnChangeVp(long oldValue, long newValue)
	{
		if (this.RealVpLmt > 0)
		{
			AttrChangeAnnouncer.Announce(this, GameData.AttrType.Vp, (double)(oldValue / (long)this.RealVpLmt), (double)(newValue / (long)this.RealVpLmt), oldValue, newValue);
		}
	}

	protected void OnChangeVpLmt(long oldValue, long newValue)
	{
		this.CalculateRealVpLmt();
	}

	protected void OnChangeVpLmtMulAmend(long oldValue, long newValue)
	{
		this.CalculateRealVpLmt();
	}

	protected virtual void OnChangeRealVpLmt(long oldValue, long newValue)
	{
		this.oldRealVpLmt = (int)oldValue;
	}

	protected void OnChangeMoveSpeed(long oldValue, long newValue)
	{
		this.CalculateRealMoveSpeed();
	}

	protected void OnChangeBuffMoveSpeedMulPosAmend(long oldValue, long newValue)
	{
		this.CalculateRealMoveSpeed();
	}

	protected virtual void OnChangeRealMoveSpeed(long oldValue, long newValue)
	{
		this.SetMoveSpeed(newValue);
	}

	protected void OnChangeAtkSpeed(long oldValue, long newValue)
	{
		this.SetDefaultActionSpeed(newValue);
		this.CalculateRealActionSpeed();
	}

	protected void OnChangeActSpeedMulPosAmend(long oldValue, long newValue)
	{
		this.CalculateRealActionSpeed();
	}

	protected virtual void OnChangeRealActionSpeed(long oldValue, long newValue)
	{
		this.SetRunActionSpeed(newValue);
	}

	protected void CalculateRealHpLmt()
	{
		this.RealHpLmt = (long)((double)this.HpLmt * (1.0 + (double)this.HpLmtMulAmend * 0.001));
	}

	protected void CalculateRealVpLmt()
	{
		this.RealVpLmt = (int)((double)this.VpLmt * (1.0 + (double)this.VpLmtMulAmend * 0.001));
	}

	protected void CalculateRealMoveSpeed()
	{
		this.RealMoveSpeed = (int)((double)this.MoveSpeed * (1.0 + (double)this.BuffMoveSpeedMulPosAmend * 0.001));
	}

	protected void CalculateRealActionSpeed()
	{
		this.RealActionSpeed = (int)((double)this.ActSpeed * (1.0 + (double)this.BuffActSpeedMulPosAmend * 0.001));
	}

	public virtual void AddSkill(int skillIndex, int skillID, int skillLevel, List<BattleSkillAttrAdd> skillAttrChange)
	{
		if (skillIndex > 0)
		{
			this.SkillInfo.Add(skillIndex, skillID);
		}
		else
		{
			this.SkillInfo.AddValue(skillID);
		}
		this.SkillLevel = new KeyValuePair<int, int>(skillID, (skillLevel <= 0) ? 1 : skillLevel);
		this.SkillAttrChange = new KeyValuePair<int, List<BattleSkillAttrAdd>>(skillID, skillAttrChange);
		this.UpdateSkill();
	}

	public virtual void AddSkill(int skillID, int skillLevel = 1, List<BattleSkillAttrAdd> skillAttrChange = null)
	{
		this.SkillInfo.AddValue(skillID);
		this.SkillLevel = new KeyValuePair<int, int>(skillID, (skillLevel <= 0) ? 1 : skillLevel);
		this.SkillAttrChange = new KeyValuePair<int, List<BattleSkillAttrAdd>>(skillID, skillAttrChange);
		this.UpdateSkill();
	}

	public virtual void RemoveSkill(int skillID)
	{
		this.SkillInfo.Remove(skillID);
		this.UpdateSkill();
	}

	public virtual void ClearSkill()
	{
		this.SkillInfo.Clear();
		this.UpdateSkill();
	}

	public virtual void SetSkillDic(IndexList<int, int> newSkillDic)
	{
		this.SkillInfo = newSkillDic;
		this.UpdateSkill();
	}

	public virtual void UpdateSkill()
	{
		if (this.GetConditionManager() != null)
		{
			this.GetConditionManager().RegistCounterSkillCondition(this.GetSkillAllValue());
		}
	}

	protected void ClearSkillLevel()
	{
		this.skillLevel.Clear();
	}

	protected void ClearSkillExtend()
	{
		this.skillExtend.Clear();
	}

	public List<int> GetSkillSinglePart()
	{
		return this.SkillInfo.GetSinglePart();
	}

	public Dictionary<int, int> GetSkillPairPart()
	{
		return this.SkillInfo.GetPairPart();
	}

	public List<int> GetSkillAllValue()
	{
		return this.SkillInfo.GetAllValue();
	}

	public bool ContainSkillIndex(int index)
	{
		return this.SkillInfo.ContainsKey(index);
	}

	public bool ContainSkillID(int skillID)
	{
		return this.SkillInfo.ContainsValue(skillID);
	}

	public int GetSkillIDByIndex(int index)
	{
		return (!this.SkillInfo.ContainsKey(index)) ? 0 : this.SkillInfo[index];
	}

	public int GetSkillIndexByID(int skillID)
	{
		using (Dictionary<int, int>.Enumerator enumerator = this.SkillInfo.GetPairPart().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				if (current.get_Value() == skillID)
				{
					return current.get_Key();
				}
			}
		}
		return 0;
	}

	public int GetSkillLevelByID(int skillID)
	{
		return (!this.skillLevel.ContainsKey(skillID)) ? 1 : this.skillLevel.get_Item(skillID);
	}

	public XDict<GameData.AttrType, BattleSkillAttrAdd> GetSkillAttrChangeByID(int skillID)
	{
		return (!this.skillAttrChange.ContainsKey(skillID)) ? null : this.skillAttrChange[skillID];
	}

	public int GetSkillCDVariationByType(int skillType)
	{
		int num = 0;
		for (int i = 0; i < this.skillExtend.get_Count(); i++)
		{
			if (this.skillExtend.get_Item(i).skillType == skillType)
			{
				num += this.skillExtend.get_Item(i).cdOffset;
			}
		}
		return num;
	}

	public int GetSkillActionPointVariationByType(int skillType)
	{
		int num = 0;
		for (int i = 0; i < this.skillExtend.get_Count(); i++)
		{
			if (this.skillExtend.get_Item(i).skillType == skillType)
			{
				num += this.skillExtend.get_Item(i).actPointOffset;
			}
		}
		return num;
	}

	public void SetCheckDead(long state)
	{
		if (!this.Actor)
		{
			return;
		}
		if (this.IsInBattle && state == 0L)
		{
			this.Actor.CastAction("die", true, 1f, 0, 0, string.Empty);
		}
	}

	protected EntityParent SetOwner(long id)
	{
		if (this.owner == null)
		{
			this.owner = EntityWorld.Instance.GetEntityByID(id);
		}
		return this.owner;
	}

	public void SetPos(Vector3 state)
	{
		if (!this.Actor)
		{
			return;
		}
		this.Actor.FixTransform.set_position(state);
	}

	public void SetDir(Vector3 state)
	{
		if (!this.Actor)
		{
			return;
		}
		if (this.Actor.IsLockModelDir)
		{
			return;
		}
		this.Actor.FixTransform.set_forward(state);
	}

	public void SetFloor(int state)
	{
		if (!this.Actor)
		{
			return;
		}
		this.Actor.SetFloor((float)state * 30f);
	}

	protected virtual void SetIsFighting(bool state)
	{
	}

	protected virtual void SetIsCloseRenderer(bool state)
	{
		if (!this.Actor)
		{
			return;
		}
		this.SetRenderers(!state);
		this.Actor.RedererLayerState = ((!state) ? 0 : 1);
		SoundManager.Instance.SetPlayerMute(this.ID, state);
		BillboardManager.Instance.ShowBillboardsInfo(this.ID, !this.IsDead && !state);
	}

	protected void SetMoveSpeed(long state)
	{
		if (!this.Actor)
		{
			return;
		}
		this.Actor.LogicMoveSpeed = (float)((int)state);
	}

	protected void SetDefaultActionSpeed(long state)
	{
		if (!this.Actor)
		{
			return;
		}
		this.Actor.LogicDefaultActionSpeed = (float)((int)state);
	}

	protected void SetRunActionSpeed(long state)
	{
		if (!this.Actor)
		{
			return;
		}
		this.Actor.LogicRunActionSpeed = (float)((int)state);
	}

	protected void SetPressSkill(bool isPressing, int skillID, Vector3 skillDir, bool isManage)
	{
		if (!this.Actor)
		{
			return;
		}
		if (!isPressing)
		{
			return;
		}
		if (skillID == 0)
		{
			return;
		}
		if (this.GetSkillManager() == null)
		{
			return;
		}
		this.GetSkillManager().ServerCastSkillByID(skillID, 0, skillDir, isManage, 0);
	}

	protected virtual void AddNetworkCommonInfoChangedListener()
	{
		NetworkManager.AddListenEvent<CommonInfoNty>(new NetCallBackMethod<CommonInfoNty>(this.ChangeCommonInfo));
	}

	protected virtual void RemoveNetworkCommonInfoChangedListener()
	{
		NetworkManager.RemoveListenEvent<CommonInfoNty>(new NetCallBackMethod<CommonInfoNty>(this.ChangeCommonInfo));
	}

	protected virtual void InitCommonInfoUpdateData()
	{
	}

	protected void InitCommonInfoGroupUpdateData(Action<XDict<KVType.ENUM, int>, XDict<KVType.ENUM, string>> callback, params KVType.ENUM[] args)
	{
		if (args == null)
		{
			return;
		}
		if (args.Length == 0)
		{
			return;
		}
		EntityParent.CommonInfoUpdator commonInfoUpdator = new EntityParent.CommonInfoUpdator();
		commonInfoUpdator.Update = callback;
		for (int i = 0; i < args.Length; i++)
		{
			commonInfoUpdator.ConnectedType.Add(args[i]);
		}
		int num = this.AddCommonInfoUpdator(commonInfoUpdator);
		for (int j = 0; j < args.Length; j++)
		{
			if (!this.CommonInfoUpdateTable.ContainsKey(args[j]))
			{
				this.CommonInfoUpdateTable.Add(args[j], new List<int>());
			}
			if (!this.CommonInfoUpdateTable[args[j]].Contains(num))
			{
				this.CommonInfoUpdateTable[args[j]].Add(num);
			}
		}
	}

	protected int AddCommonInfoUpdator(EntityParent.CommonInfoUpdator commonInfoUpdator)
	{
		int num = 0;
		for (int i = 0; i < this.CommonInfoUpdatorMappingTable.Count; i++)
		{
			num = this.CommonInfoUpdatorMappingTable.ElementKeyAt(i);
			if (this.CommonInfoUpdatorMappingTable.ElementValueAt(i).Equals(commonInfoUpdator))
			{
				return num;
			}
		}
		num++;
		this.CommonInfoUpdatorMappingTable.Add(num, commonInfoUpdator);
		return num;
	}

	public void ChangeCommonInfo(short state, CommonInfoNty down = null)
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
		this.HandelCommonInfoChange(down.kvs1, down.kvs2);
	}

	protected void HandelCommonInfoChange(List<CommonKV1> pair1, List<CommonKV2> pair2)
	{
		if (this.CommonInfoUpdateTable.Count == 0 || this.CommonInfoUpdatorMappingTable.Count == 0)
		{
			return;
		}
		List<int> commonInfoUpdateList = this.GetCommonInfoUpdateList(pair1, pair2);
		XDict<KVType.ENUM, int> xDict = new XDict<KVType.ENUM, int>();
		for (int i = 0; i < pair1.get_Count(); i++)
		{
			if (xDict.ContainsKey(pair1.get_Item(i).ck))
			{
				xDict[pair1.get_Item(i).ck] = pair1.get_Item(i).cv;
			}
			else
			{
				xDict.Add(pair1.get_Item(i).ck, pair1.get_Item(i).cv);
			}
		}
		XDict<KVType.ENUM, string> xDict2 = new XDict<KVType.ENUM, string>();
		for (int j = 0; j < pair2.get_Count(); j++)
		{
			if (xDict2.ContainsKey(pair2.get_Item(j).ck))
			{
				xDict2[pair2.get_Item(j).ck] = pair2.get_Item(j).cv;
			}
			else
			{
				xDict2.Add(pair2.get_Item(j).ck, pair2.get_Item(j).cv);
			}
		}
		XDict<KVType.ENUM, int> xDict3 = new XDict<KVType.ENUM, int>();
		XDict<KVType.ENUM, string> xDict4 = new XDict<KVType.ENUM, string>();
		for (int k = 0; k < commonInfoUpdateList.get_Count(); k++)
		{
			if (!this.CommonInfoUpdatorMappingTable.ContainsKey(commonInfoUpdateList.get_Item(k)))
			{
				return;
			}
			xDict3.Clear();
			xDict4.Clear();
			for (int l = 0; l < this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Count(); l++)
			{
				if (xDict.ContainsKey(this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)))
				{
					if (xDict3.ContainsKey(this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)))
					{
						xDict3[this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)] = xDict[this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)];
					}
					else
					{
						xDict3.Add(this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l), xDict[this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)]);
					}
				}
				if (xDict2.ContainsKey(this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)))
				{
					if (xDict4.ContainsKey(this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)))
					{
						xDict4[this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)] = xDict2[this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)];
					}
					else
					{
						xDict4.Add(this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l), xDict2[this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].ConnectedType.get_Item(l)]);
					}
				}
				this.CommonInfoUpdatorMappingTable[commonInfoUpdateList.get_Item(k)].Update.Invoke(xDict3, xDict4);
			}
		}
	}

	protected List<int> GetCommonInfoUpdateList(List<CommonKV1> pair1, List<CommonKV2> pair2)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < pair1.get_Count(); i++)
		{
			if (this.CommonInfoUpdateTable.ContainsKey(pair1.get_Item(i).ck))
			{
				for (int j = 0; j < this.CommonInfoUpdateTable[pair1.get_Item(i).ck].get_Count(); j++)
				{
					if (!list.Contains(this.CommonInfoUpdateTable[pair1.get_Item(i).ck].get_Item(j)))
					{
						list.Add(this.CommonInfoUpdateTable[pair1.get_Item(i).ck].get_Item(j));
					}
				}
			}
		}
		for (int k = 0; k < pair2.get_Count(); k++)
		{
			if (this.CommonInfoUpdateTable.ContainsKey(pair1.get_Item(k).ck))
			{
				for (int l = 0; l < this.CommonInfoUpdateTable[pair1.get_Item(k).ck].get_Count(); l++)
				{
					if (!list.Contains(this.CommonInfoUpdateTable[pair1.get_Item(k).ck].get_Item(l)))
					{
						list.Add(this.CommonInfoUpdateTable[pair1.get_Item(k).ck].get_Item(l));
					}
				}
			}
		}
		return list;
	}

	protected void AddNetworkMoveRotateTeleportGoUpAndDownListener()
	{
		NetworkManager.AddListenEvent<MapObjectMoveNty>(new NetCallBackMethod<MapObjectMoveNty>(this.NetMove));
		NetworkManager.AddListenEvent<MapObjectRotateNty>(new NetCallBackMethod<MapObjectRotateNty>(this.NetRotate));
		NetworkManager.AddListenEvent<RoleLayerChangedNty>(new NetCallBackMethod<RoleLayerChangedNty>(this.NetGoUpAndDown));
		NetworkManager.AddListenEvent<MapObjectMoveNty2>(new NetCallBackMethod<MapObjectMoveNty2>(this.NetTeleport));
		NetworkManager.AddListenEvent<MapObjectMoveNtyEx>(new NetCallBackMethod<MapObjectMoveNtyEx>(this.NetMoveEx));
		NetworkManager.AddListenEvent<MapObjectRotateNtyEx>(new NetCallBackMethod<MapObjectRotateNtyEx>(this.NetRotateEx));
	}

	protected void RemoveNetworkMoveRotateTeleportGoUpAndDownListener()
	{
		NetworkManager.RemoveListenEvent<MapObjectMoveNty>(new NetCallBackMethod<MapObjectMoveNty>(this.NetMove));
		NetworkManager.RemoveListenEvent<MapObjectRotateNty>(new NetCallBackMethod<MapObjectRotateNty>(this.NetRotate));
		NetworkManager.RemoveListenEvent<RoleLayerChangedNty>(new NetCallBackMethod<RoleLayerChangedNty>(this.NetGoUpAndDown));
		NetworkManager.RemoveListenEvent<MapObjectMoveNty2>(new NetCallBackMethod<MapObjectMoveNty2>(this.NetTeleport));
		NetworkManager.RemoveListenEvent<MapObjectMoveNtyEx>(new NetCallBackMethod<MapObjectMoveNtyEx>(this.NetMoveEx));
		NetworkManager.RemoveListenEvent<MapObjectRotateNtyEx>(new NetCallBackMethod<MapObjectRotateNtyEx>(this.NetRotateEx));
	}

	protected void NetMove(short state, MapObjectMoveNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (this.ID != down.objId)
		{
			return;
		}
		if (this.Actor)
		{
			Vector3 vector = new Vector3(down.toPos.x * 0.01f, this.Actor.FixTransform.get_position().y, down.toPos.y * 0.01f);
			if (this.IsInBattle && this.Actor.MoveDeviationEstimated(vector))
			{
				this.Actor.SetAndFixPosition(vector, "NetMove瞬移", 1000003);
			}
			else
			{
				this.Actor.MoveToPoint(PosDirUtility.ToTerrainPoint(down.toPos, this.Actor.FixTransform.get_position().y), 0f, null);
			}
		}
		else
		{
			this.Pos = PosDirUtility.ToTerrainPoint(down.toPos, this.CurFloorStandardHeight);
		}
	}

	protected void NetRotate(short state, MapObjectRotateNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (this.ID != down.objId)
		{
			return;
		}
		if (this.Actor)
		{
			ActorParent arg_5F_0 = this.Actor;
			Vector3 vector = new Vector3(down.vector.x, 0f, down.vector.y);
			arg_5F_0.MovingDirection = vector.get_normalized();
			this.Actor.ApplyMovingDirAsForward();
		}
		else
		{
			this.Dir = new Vector3(down.vector.x, 0f, down.vector.y);
		}
	}

	protected void NetGoUpAndDown(short state, RoleLayerChangedNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (this.ID != down.roleId)
		{
			return;
		}
		if (this.Actor)
		{
			this.Actor.SetFloor((float)down.layer * 30f);
		}
		else
		{
			this.Floor = down.layer;
		}
	}

	protected void NetTeleport(short state, MapObjectMoveNty2 down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (this.ID != down.objId)
		{
			return;
		}
		this.Teleport(down.toPos);
	}

	protected void Teleport(Pos toPos)
	{
		this.Teleport(PosDirUtility.ToTerrainPoint(toPos, this.Actor.FixTransform.get_position().y));
	}

	public virtual void Teleport(Vector3 toPos)
	{
		if (this.Actor)
		{
			this.Actor.Teleport(toPos);
		}
		else
		{
			this.Pos = toPos;
		}
	}

	protected virtual void NetMoveEx(short state, MapObjectMoveNtyEx down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		for (int i = 0; i < down.infos.get_Count(); i++)
		{
			if (down.infos.get_Item(i).objId == this.ID)
			{
				if (this.Actor)
				{
					Vector3 vector = new Vector3(down.infos.get_Item(i).toPos.x * 0.01f, this.Actor.FixTransform.get_position().y, down.infos.get_Item(i).toPos.y * 0.01f);
					if (this.Actor.MoveDeviationEstimated(vector))
					{
						this.Actor.SetAndFixPosition(vector, null, 301);
					}
					else
					{
						this.Actor.MoveToPoint(PosDirUtility.ToTerrainPoint(down.infos.get_Item(i).toPos, this.Actor.FixTransform.get_position().y), 0f, null);
					}
				}
				else
				{
					this.Pos = PosDirUtility.ToTerrainPoint(down.infos.get_Item(i).toPos, this.CurFloorStandardHeight);
				}
				break;
			}
		}
	}

	protected virtual void NetRotateEx(short state, MapObjectRotateNtyEx down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		for (int i = 0; i < down.infos.get_Count(); i++)
		{
			if (down.infos.get_Item(i).objId == this.ID)
			{
				if (this.Actor)
				{
					ActorParent arg_8B_0 = this.Actor;
					Vector3 vector = new Vector3(down.infos.get_Item(i).vector.x, 0f, down.infos.get_Item(i).vector.y);
					arg_8B_0.MovingDirection = vector.get_normalized();
					this.Actor.ApplyMovingDirAsForward();
				}
				else
				{
					this.Dir = new Vector3(down.infos.get_Item(i).vector.x, 0f, down.infos.get_Item(i).vector.y);
				}
			}
		}
	}

	protected virtual void AddNetworkDecorationChangedListener()
	{
		NetworkManager.AddListenEvent<MapObjDecorationChangedNty>(new NetCallBackMethod<MapObjDecorationChangedNty>(this.ChangeDecoration));
	}

	protected virtual void RemoveNetworkDecorationChangedListener()
	{
		NetworkManager.RemoveListenEvent<MapObjDecorationChangedNty>(new NetCallBackMethod<MapObjDecorationChangedNty>(this.ChangeDecoration));
	}

	protected virtual void InitModel()
	{
	}

	protected virtual void ChangeDecoration(short state, MapObjDecorationChangedNty down = null)
	{
	}

	public virtual void EquipSuccess()
	{
	}

	public virtual void EquipWingSuccess(Animator wing_tor)
	{
		this.Actor.WingAnimator = wing_tor;
		this.Actor.PlayWing();
	}

	public virtual void ShowEquipFX(bool isShow)
	{
	}

	public virtual void InitBillboard(float height, List<int> bloodBarSize)
	{
	}

	public virtual void ResetBillBoard()
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.FixModelID);
		this.InitBillboard((float)avatarModel.height_HP, avatarModel.bloodBar);
		EventDispatcher.Broadcast<long, bool>("BillboardManager.ShowBillboardsInfo", this.ID, !this.IsDead && !this.IsCloseRenderer);
	}

	public virtual void DebutBattle()
	{
		if (this.GetSkillManager() != null)
		{
			this.GetSkillManager().SetDebutCD();
		}
		EnterBattleFieldAnnouncer.Announce(this);
	}

	public EntityParent GetTarget(TargetRangeType rangeType, int targetType, float outerDistance, float innerDistance, int angle, int forwardFixAngle, int altitude, List<int> comparers)
	{
		if (!this.Actor)
		{
			return null;
		}
		switch (targetType)
		{
		case 1:
		{
			EnemyTargetFixType enemyTargetFixType = this.CurEnemyTargetFixType;
			if (enemyTargetFixType == EnemyTargetFixType.Monster)
			{
				return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(EntityWorld.Instance.GetEntities<EntityMonster>(), this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, this.Camp, false, false, comparers);
			}
			if (enemyTargetFixType != EnemyTargetFixType.Avatar_OtherPlayer_MonsterWithoutBoss)
			{
				return EntityWorld.Instance.GetOneSkillTarget(this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, this.Camp, false, false, comparers);
			}
			XDict<long, EntityParent> xDict = new XDict<long, EntityParent>();
			List<EntityParent> values = EntityWorld.Instance.GetEntities<EntitySelf>().Values;
			for (int i = 0; i < values.get_Count(); i++)
			{
				if (values.get_Item(i) != null)
				{
					xDict.Add(values.get_Item(i).ID, values.get_Item(i));
				}
			}
			List<EntityParent> values2 = EntityWorld.Instance.GetEntities<EntityPlayer>().Values;
			for (int j = 0; j < values2.get_Count(); j++)
			{
				if (values2.get_Item(j) != null)
				{
					xDict.Add(values2.get_Item(j).ID, values2.get_Item(j));
				}
			}
			List<EntityParent> values3 = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
			for (int k = 0; k < values3.get_Count(); k++)
			{
				if (values3.get_Item(k) != null)
				{
					if (!values3.get_Item(k).IsLogicBoss)
					{
						xDict.Add(values3.get_Item(k).ID, values3.get_Item(k));
					}
				}
			}
			return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(xDict, this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, this.Camp, false, false, comparers);
		}
		case 2:
			return EntityWorld.Instance.GetOneSkillTarget(this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, this.Camp, true, false, comparers);
		case 3:
			if (!EntityWorld.Instance.AltitudeFilter<EntityParent>(this, altitude))
			{
				return null;
			}
			return this;
		case 4:
			return EntityWorld.Instance.GetOneSkillTarget(this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, this.Camp, true, true, comparers);
		case 5:
		{
			if (this.Owner == null)
			{
				return null;
			}
			XDict<long, EntityParent> xDict2 = new XDict<long, EntityParent>();
			xDict2.Add(this.Owner.ID, this.Owner);
			return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(xDict2, this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, comparers);
		}
		case 6:
			return EntityWorld.Instance.GetOneSkillTarget(this, -1f, innerDistance, angle, forwardFixAngle, altitude, -1, false, true, comparers);
		case 7:
		{
			List<EntityParent> values4 = EntityWorld.Instance.GetEntities<EntityPet>().Values;
			XDict<long, EntityParent> xDict3 = new XDict<long, EntityParent>();
			for (int l = 0; l < values4.get_Count(); l++)
			{
				if (values4.get_Item(l).OwnerID == this.ID)
				{
					if (!values4.get_Item(l).IsDead)
					{
						if (values4.get_Item(l).IsFighting)
						{
							if (values4.get_Item(l).Actor)
							{
								xDict3.Add(values4.get_Item(l).ID, values4.get_Item(l));
							}
						}
					}
				}
			}
			return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(xDict3, this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, comparers);
		}
		case 8:
		{
			if (this.DamageSource == null)
			{
				return null;
			}
			XDict<long, EntityParent> xDict4 = new XDict<long, EntityParent>();
			xDict4.Add(this.DamageSourceID, this.DamageSource);
			return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(xDict4, this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, comparers);
		}
		case 9:
		{
			XDict<long, EntityParent> xDict5 = new XDict<long, EntityParent>();
			xDict5.Add(EntityWorld.Instance.EntSelf.ID, EntityWorld.Instance.EntSelf);
			return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(xDict5, this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, comparers);
		}
		case 10:
		{
			List<EntityParent> values5 = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
			XDict<long, EntityParent> xDict6 = new XDict<long, EntityParent>();
			for (int m = 0; m < values5.get_Count(); m++)
			{
				if (values5.get_Item(m) != null)
				{
					if (values5.get_Item(m).IsLogicBoss)
					{
						xDict6.Add(values5.get_Item(m).ID, values5.get_Item(m));
					}
				}
			}
			return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(xDict6, this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, comparers);
		}
		case 11:
		{
			List<EntityParent> values6 = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
			XDict<long, EntityParent> xDict7 = new XDict<long, EntityParent>();
			for (int n = 0; n < values6.get_Count(); n++)
			{
				if (values6.get_Item(n) != null)
				{
					if (values6.get_Item(n).IsBuffEntity)
					{
						xDict7.Add(values6.get_Item(n).ID, values6.get_Item(n));
					}
				}
			}
			return EntityWorld.Instance.GetOneSkillTarget<EntityParent>(xDict7, this, outerDistance, innerDistance, angle, forwardFixAngle, altitude, comparers);
		}
		default:
			return null;
		}
	}

	protected EntityParent GetTargetByGivenEntity(EntityParent entity, TargetRangeType rangeType, float outerDistance, float innerDistance, int angle, int altitude)
	{
		if (!EntityWorld.Instance.AltitudeFilter<EntityParent>(entity, altitude))
		{
			return null;
		}
		if (entity.Actor == null || entity.Actor.FixTransform == null)
		{
			return null;
		}
		switch (rangeType)
		{
		case TargetRangeType.SkillRange:
		case TargetRangeType.Configure:
		{
			Vector3 position = this.Actor.FixTransform.get_position();
			Vector3 entityPosition = new Vector3(entity.Actor.FixTransform.get_position().x, position.y, entity.Actor.FixTransform.get_position().z);
			float hitRadius = XUtility.GetHitRadius(entity.Actor.FixTransform);
			Vector3 forward = this.Actor.FixTransform.get_forward();
			if (EntityWorld.Instance.RangeAndAngleFilter(outerDistance, innerDistance, angle, position, entityPosition, hitRadius, forward))
			{
				return entity;
			}
			break;
		}
		case TargetRangeType.World:
			return entity;
		}
		return null;
	}

	public bool CheckTarget(EntityParent entity, TargetRangeType rangeType, int targetType, float outerDistance, float innerDistance, int angle, int altitude)
	{
		if (!this.Actor)
		{
			return false;
		}
		switch (targetType)
		{
		case 1:
			return EntityWorld.Instance.CheckOneTargetFromEntityCollection<EntityParent, EntityParent>(entity, this, outerDistance, innerDistance, angle, altitude, this.Camp, false, false);
		case 2:
			return EntityWorld.Instance.CheckOneTargetFromEntityCollection<EntityParent, EntityParent>(entity, this, outerDistance, innerDistance, angle, altitude, this.Camp, true, false);
		case 3:
			return this.ID == entity.ID && EntityWorld.Instance.AltitudeFilter<EntityParent>(entity, altitude);
		case 4:
			return EntityWorld.Instance.CheckOneTargetFromEntityCollection<EntityParent, EntityParent>(entity, this, outerDistance, innerDistance, angle, altitude, this.Camp, true, true);
		case 5:
			return this.OwnerID == entity.ID && this.CheckTargetByGivenEntity(entity, rangeType, outerDistance, innerDistance, angle, altitude);
		case 6:
			return EntityWorld.Instance.CheckOneTargetFromEntityCollection<EntityParent, EntityParent>(entity, this, -1f, innerDistance, angle, altitude, -1, false, true);
		case 7:
			return !entity.IsDead && entity.IsFighting && entity.OwnerID == this.ID && this.CheckTargetByGivenEntity(entity, rangeType, outerDistance, innerDistance, angle, altitude);
		case 8:
			return entity.ID == this.DamageSourceID && this.CheckTargetByGivenEntity(entity, rangeType, outerDistance, innerDistance, angle, altitude);
		case 9:
			return entity.ID == EntityWorld.Instance.EntSelf.ID && this.CheckTargetByGivenEntity(entity, rangeType, outerDistance, innerDistance, angle, altitude);
		case 10:
			return entity.IsLogicBoss && this.CheckTargetByGivenEntity(entity, rangeType, outerDistance, innerDistance, angle, altitude);
		case 11:
			return entity.IsBuffEntity && this.CheckTargetByGivenEntity(entity, rangeType, outerDistance, innerDistance, angle, altitude);
		default:
			return false;
		}
	}

	protected bool CheckTargetByGivenEntity(EntityParent entity, TargetRangeType rangeType, float outerDistance, float innerDistance, int angle, int altitude)
	{
		if (!EntityWorld.Instance.AltitudeFilter<EntityParent>(entity, altitude))
		{
			return false;
		}
		switch (rangeType)
		{
		case TargetRangeType.SkillRange:
		case TargetRangeType.Configure:
		{
			Vector3 position = this.Actor.FixTransform.get_position();
			Vector3 entityPosition = new Vector3(entity.Actor.FixTransform.get_position().x, position.y, entity.Actor.FixTransform.get_position().z);
			float hitRadius = XUtility.GetHitRadius(entity.Actor.FixTransform);
			Vector3 forward = this.Actor.FixTransform.get_forward();
			if (EntityWorld.Instance.RangeAndAngleFilter(outerDistance, innerDistance, angle, position, entityPosition, hitRadius, forward))
			{
				return true;
			}
			break;
		}
		case TargetRangeType.World:
			return true;
		}
		return false;
	}

	public virtual void BornEnd()
	{
	}

	public void CheckCancelManage(long casterID, int canceledType, bool isCheckCancelSkillInTrusteeTip = false)
	{
		if (canceledType == 0)
		{
			return;
		}
		if (this.IsAssault)
		{
			this.IsAssault = false;
		}
		if (this.IsHitMoving)
		{
			this.IsHitMoving = false;
		}
		if (this.IsSuspended)
		{
			this.IsSuspended = false;
		}
		if (this.IsSkillInTrustee)
		{
			if (isCheckCancelSkillInTrusteeTip && canceledType == 3 && casterID == EntityWorld.Instance.EntSelf.ID && EntityWorld.Instance.EntSelf.Actor && (this.IsEntityPlayerType || (this.IsEntityMonsterType && this.IsLogicBoss)))
			{
				WaveBloodManager.Instance.ThrowBlood(HPChangeMessage.GetBreakMessage(EntityWorld.Instance.EntSelf, this));
			}
			this.IsSkillInTrustee = false;
		}
	}

	protected void CancelAssault()
	{
		if (this.Actor)
		{
			this.Actor.CancelAssault();
		}
		this.IsAssault = false;
	}

	protected void CancelHitMove()
	{
		if (this.Actor)
		{
			this.Actor.CancelHitMove();
		}
		this.IsHitMoving = false;
		this.IsSuspended = false;
	}

	protected void CancelSkillTrustee()
	{
		if (this.Actor)
		{
			this.Actor.CancelSkillTrustee();
		}
		this.IsSkillInTrustee = false;
	}

	public void SetHPChange(HPChangeMessage data)
	{
		if (this.IsDead)
		{
			return;
		}
		if (this.Actor == null)
		{
			return;
		}
		if (this.Actor.get_gameObject() == null)
		{
			return;
		}
		this.ThrowBloodEffect(data);
		this.UpdateHpChangeInfluence(data);
	}

	protected virtual void ThrowBloodEffect(HPChangeMessage data)
	{
		WaveBloodManager.Instance.ThrowBlood(data);
	}

	public virtual void UpdateBlood()
	{
		HeadInfoManager.Instance.SetBloodBar(this.ID, (float)((double)this.Hp / (double)this.RealHpLmt), (this.IsPlayerMate && (this.IsEntityPlayerType || this.IsNPC)) || this.HasMarkHurt);
	}

	protected virtual void UpdateHpChangeInfluence(HPChangeMessage data)
	{
	}

	public virtual void DieBegin()
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
		if (this.IsFixed)
		{
			this.IsFixed = false;
		}
		if (this.IsAssault)
		{
			this.IsAssault = false;
		}
		if (this.IsHitMoving)
		{
			this.IsHitMoving = false;
		}
		if (this.Actor)
		{
			this.Actor.SetDie();
		}
		EventDispatcher.Broadcast<long, bool>("BillboardManager.ShowBillboardsInfo", this.ID, false);
	}

	public virtual void DieEnd()
	{
	}

	public virtual void Revive()
	{
		if (!this.IsFighting)
		{
			this.IsFighting = true;
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
		if (this.IsFixed)
		{
			this.IsFixed = false;
		}
		if (this.IsAssault)
		{
			this.IsAssault = false;
		}
		if (this.IsHitMoving)
		{
			this.IsHitMoving = false;
		}
		if (this.IsSuspended)
		{
			this.IsSuspended = false;
		}
		if (this.Actor)
		{
			this.Actor.ChangeAction("idle", false, true, 1f, 0, 0, string.Empty);
		}
		EventDispatcher.Broadcast<long, bool>("BillboardManager.ShowBillboardsInfo", this.ID, !this.IsDead && !this.IsCloseRenderer);
		EnterBattleFieldAnnouncer.Announce(this);
	}

	public virtual void VictoryEnd()
	{
	}

	public string GetString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.GetInfoString(stringBuilder);
		stringBuilder.Append("\n");
		this.GetAttrString(stringBuilder);
		stringBuilder.Append("\n");
		this.GetActorString(stringBuilder);
		stringBuilder.Append("\n");
		stringBuilder.Append("\n==========================================");
		return stringBuilder.ToString();
	}

	public string GetInfoString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.GetInfoString(stringBuilder);
		stringBuilder.Append("\n==========================================");
		return stringBuilder.ToString();
	}

	protected virtual void GetInfoString(StringBuilder result)
	{
		result.Append("ID:");
		result.Append(this.ID);
		result.Append("  ");
		result.Append("Name:");
		result.Append(this.Name);
		result.Append("  ");
		result.Append("ObjType:");
		result.Append(this.ObjType);
		result.Append("  ");
		result.Append("Camp:");
		result.Append(this.Camp);
		result.Append("  ");
		result.Append("TypeID:");
		result.Append(this.TypeID);
		result.Append("  ");
		result.Append("ModelID:");
		result.Append(this.ModelID);
		result.Append("  ");
		result.Append("FixModelID");
		result.Append(this.FixModelID);
		result.Append("  ");
		result.Append("Pos:");
		result.Append(this.Pos);
		result.Append("  ");
		result.Append("Dir:");
		result.Append(this.Dir);
		result.Append("\n");
		result.Append("OwnerID:");
		result.Append(this.OwnerID);
		result.Append("  ");
		result.Append("WrapType:");
		result.Append(this.WrapType);
		result.Append("  ");
		result.Append("Hp:");
		result.Append(this.Hp);
		result.Append("  ");
		result.Append("RealHpLmt:");
		result.Append(this.RealHpLmt);
		result.Append("  ");
		result.Append("ActPoint:");
		result.Append(this.ActPoint);
		result.Append("  ");
		result.Append("ActPointLmt:");
		result.Append(this.ActPointLmt);
		result.Append("  ");
		result.Append("OwnerListIdx:");
		result.Append(this.OwnerListIdx);
		result.Append("  ");
		result.Append("OwnedIDs:{");
		for (int i = 0; i < this.OwnedIDs.get_Count(); i++)
		{
			result.Append(this.OwnedIDs.get_Item(i));
			result.Append(",");
		}
		result.Append("}  ");
		result.Append("Skill:{");
		Dictionary<int, int> skillPairPart = this.GetSkillPairPart();
		using (Dictionary<int, int>.Enumerator enumerator = skillPairPart.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				result.Append("{");
				result.Append(current.get_Key());
				result.Append(",");
				result.Append(current.get_Value());
				result.Append("} ");
			}
		}
		List<int> skillSinglePart = this.GetSkillSinglePart();
		for (int j = 0; j < skillSinglePart.get_Count(); j++)
		{
			result.Append(skillSinglePart.get_Item(j));
			result.Append(",");
		}
		result.Append("}  ");
		result.Append("IsFuse:");
		result.Append(this.IsFuse);
		result.Append("  ");
		result.Append("IsFusing:");
		result.Append(this.IsFusing);
		result.Append("  ");
		result.Append("isFighting:");
		result.Append(this.isFighting);
		result.Append("  ");
		result.Append("IsFixed:");
		result.Append(this.IsFixed);
		result.Append("  ");
		result.Append("IsStatic:");
		result.Append(this.IsStatic);
		result.Append("  ");
		result.Append("IsDizzy:");
		result.Append(this.IsDizzy);
		result.Append("  ");
		result.Append("IsWeak:");
		result.Append(this.IsWeak);
		result.Append("  ");
		result.Append("IsTaunt:");
		result.Append(this.IsTaunt);
		result.Append("  ");
		result.Append("IsEndure:");
		result.Append(this.IsEndure);
		result.Append("  ");
		result.Append("IsIgnoreFormula:");
		result.Append(this.IsIgnoreFormula);
		result.Append("  ");
		result.Append("IsCloseRenderer:");
		result.Append(this.IsCloseRenderer);
		result.Append("  ");
		result.Append("IsMoveCast:");
		result.Append(this.IsMoveCast);
		result.Append("  ");
		result.Append("IsAssault:");
		result.Append(this.IsAssault);
		result.Append("  ");
		result.Append("IsHitMoving:");
		result.Append(this.IsHitMoving);
		result.Append("  ");
		result.Append("IsSuspended:");
		result.Append(this.IsSuspended);
		result.Append("  ");
		result.Append("IsSkillManaging:");
		result.Append(this.IsSkillInTrustee);
		result.Append("  ");
		result.Append("IsSkillPressing:");
		result.Append(this.IsSkillPressing);
		result.Append("  ");
		result.Append("PressingSkillID:");
		result.Append(this.PressingSkillID);
		result.Append("  ");
		result.Append("IsLoading:");
		result.Append(this.IsLoading);
		result.Append("  ");
		result.Append("IsClientDominate:");
		result.Append(this.IsClientDominate);
		result.Append("  ");
		result.Append("IsMixEntity:");
		result.Append(this.IsMixEntity);
		result.Append("  ");
		result.Append("IsClientCreate:");
		result.Append(this.IsClientCreate);
		result.Append("  ");
		result.Append("IsClientDrive:");
		result.Append(this.IsClientDrive);
		result.Append("  ");
		result.Append("IsDead:");
		result.Append(this.IsDead);
		result.Append("  ");
		result.Append("IsLogicBoss:");
		result.Append(this.IsLogicBoss);
		result.Append("  ");
		result.Append("IsDisplayBoss:");
		result.Append(this.IsDisplayBoss);
		result.Append("  ");
		result.Append("IsNPC:");
		result.Append(this.IsNPC);
		result.Append("  ");
		IBuffManager buffManager = this.GetBuffManager();
		if (buffManager != null)
		{
			result.Append("Buff:{");
			List<int> buffList = buffManager.GetBuffList();
			for (int k = 0; k < buffList.get_Count(); k++)
			{
				result.Append(buffList.get_Item(k));
				result.Append(",");
			}
			result.Append("}  ");
		}
		IAIManager aIManager = this.GetAIManager();
		if (aIManager != null)
		{
			result.Append("IsAIActive:");
			result.Append(aIManager.IsAIActive());
			result.Append("  ");
			result.Append("AIType:");
			result.Append(aIManager.AIType);
			result.Append("  ");
		}
		ISkillManager skillManager = this.GetSkillManager();
		if (skillManager != null)
		{
			result.Append("DebutTime:");
			result.Append(skillManager.DebutTime);
			result.Append("  ");
		}
	}

	public string GetAttrString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.GetAttrString(stringBuilder);
		stringBuilder.Append("\n==========================================");
		return stringBuilder.ToString();
	}

	protected void GetAttrString(StringBuilder result)
	{
		result.Append("ID:");
		result.Append(this.ID);
		result.Append("  ");
		result.Append("MoveSpeed:");
		result.Append(this.MoveSpeed);
		result.Append("  ");
		result.Append("AtkSpeed:");
		result.Append(this.ActSpeed);
		result.Append("  ");
		result.Append("Lv:");
		result.Append(this.Lv);
		result.Append("  ");
		result.Append("Fighting:");
		result.Append(this.Fighting);
		result.Append("  ");
		result.Append("VipLv:");
		result.Append(this.VipLv);
		result.Append("  ");
		result.Append("Atk:");
		result.Append(this.Atk);
		result.Append("  ");
		result.Append("Defence:");
		result.Append(this.Defence);
		result.Append("  ");
		result.Append("HpLmt:");
		result.Append(this.HpLmt);
		result.Append("  ");
		result.Append("PveAtk:");
		result.Append(this.PveAtk);
		result.Append("  ");
		result.Append("PvpAtk:");
		result.Append(this.PvpAtk);
		result.Append("  ");
		result.Append("HitRatio:");
		result.Append(this.HitRatio);
		result.Append("  ");
		result.Append("DodgeRatio:");
		result.Append(this.DodgeRatio);
		result.Append("  ");
		result.Append("CritRatio:");
		result.Append(this.CritRatio);
		result.Append("  ");
		result.Append("DecritRatio:");
		result.Append(this.DecritRatio);
		result.Append("  ");
		result.Append("CritHurtAddRatio:");
		result.Append(this.CritHurtAddRatio);
		result.Append("  ");
		result.Append("ParryRatio:");
		result.Append(this.ParryRatio);
		result.Append("  ");
		result.Append("DeparryRatio:");
		result.Append(this.DeparryRatio);
		result.Append("  ");
		result.Append("ParryHurtDeRatio:");
		result.Append(this.ParryHurtDeRatio);
		result.Append("  ");
		result.Append("SuckBloodScale:");
		result.Append(this.SuckBloodScale);
		result.Append("  ");
		result.Append("HurtAddRatio:");
		result.Append(this.HurtAddRatio);
		result.Append("  ");
		result.Append("HurtDeRatio:");
		result.Append(this.HurtDeRatio);
		result.Append("  ");
		result.Append("PveHurtAddRatio:");
		result.Append(this.PveHurtAddRatio);
		result.Append("  ");
		result.Append("PveHurtDeRatio:");
		result.Append(this.PveHurtDeRatio);
		result.Append("  ");
		result.Append("PvpHurtAddRatio:");
		result.Append(this.PvpHurtAddRatio);
		result.Append("  ");
		result.Append("PvpHurtDeRatio:");
		result.Append(this.PvpHurtDeRatio);
		result.Append("  ");
		result.Append("AtkMulAmend:");
		result.Append(this.AtkMulAmend);
		result.Append("  ");
		result.Append("DefMulAmend:");
		result.Append(this.DefMulAmend);
		result.Append("  ");
		result.Append("HpLmtMulAmend:");
		result.Append(this.HpLmtMulAmend);
		result.Append("  ");
		result.Append("PveAtkMulAmend:");
		result.Append(this.PveAtkMulAmend);
		result.Append("  ");
		result.Append("PvpAtkMulAmend:");
		result.Append(this.PvpAtkMulAmend);
		result.Append("  ");
		result.Append("ActPointLmt:");
		result.Append(this.ActPointLmt);
		result.Append("  ");
		result.Append("ActPointRecoverSpeedAmend:");
		result.Append(this.ActPointRecoverSpeedAmend);
		result.Append("  ");
		result.Append("VpLmt:");
		result.Append(this.VpLmt);
		result.Append("  ");
		result.Append("VpLmtMulAmend:");
		result.Append(this.VpLmtMulAmend);
		result.Append("  ");
		result.Append("VpAtk:");
		result.Append(this.VpAtk);
		result.Append("  ");
		result.Append("VpAtkMulAmend:");
		result.Append(this.VpAtkMulAmend);
		result.Append("  ");
		result.Append("VpResume:");
		result.Append(this.VpResume);
		result.Append("  ");
		result.Append("IdleVpResume:");
		result.Append(this.IdleVpResume);
		result.Append("  ");
		result.Append("Affinity:");
		result.Append(this.Affinity);
		result.Append("  ");
		result.Append("OnlineTime:");
		result.Append(this.OnlineTime);
		result.Append("  ");
		result.Append("HealIncreasePercent:");
		result.Append(this.HealIncreasePercent);
		result.Append("  ");
		result.Append("CritAddValue:");
		result.Append(this.CritAddValue);
		result.Append("  ");
		result.Append("HpRestore:");
		result.Append(this.HpRestore);
		result.Append("  ");
		result.Append("ActPoint:");
		result.Append(this.ActPoint);
		result.Append("  ");
		result.Append("Hp:");
		result.Append(this.Hp);
		result.Append("  ");
		result.Append("Vp:");
		result.Append(this.Vp);
		result.Append("  ");
		result.Append("RealHpLmt:");
		result.Append(this.RealHpLmt);
		result.Append("  ");
		result.Append("RealVpLmt:");
		result.Append(this.RealVpLmt);
		result.Append("  ");
		result.Append("RealMoveSpeed:");
		result.Append(this.RealMoveSpeed);
		result.Append("  ");
		result.Append("RealActionSpeed:");
		result.Append(this.RealActionSpeed);
		result.Append("  ");
	}

	public string GetActorString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.GetActorString(stringBuilder);
		stringBuilder.Append("\n==========================================");
		return stringBuilder.ToString();
	}

	public void GetActorString(StringBuilder result)
	{
		result.Append("ID:");
		result.Append(this.ID);
		result.Append("  ");
		if (this.Actor)
		{
			result.Append("CurActionStatus:");
			result.Append(this.Actor.CurActionStatus);
			result.Append("  ");
			result.Append("CurOutPutAction:");
			result.Append(this.Actor.CurOutPutAction);
			result.Append("  ");
			result.Append("ActionPriorityTable:{");
			for (int i = 0; i < this.Actor.ActionPriorityTable.Count; i++)
			{
				result.Append("{");
				result.Append(this.Actor.ActionPriorityTable.ElementKeyAt(i));
				result.Append(",");
				result.Append(this.Actor.ActionPriorityTable.ElementValueAt(i));
				result.Append("} ");
			}
			result.Append("}  ");
			result.Append("StageSuffix:");
			result.Append(this.Actor.StageSuffix);
			result.Append("  ");
		}
	}

	protected void SetRenderers(bool isOn)
	{
		for (int i = 0; i < this.shaderRenderers.get_Count(); i++)
		{
			Renderer renderer = this.shaderRenderers.get_Item(i);
			if (renderer != null)
			{
				renderer.set_enabled(isOn);
			}
		}
		ShadowController.ShowShadow(this.ID, this.Actor.FixTransform, !isOn, this.FixModelID);
		if (this.shadowRenderer != null)
		{
			this.shadowRenderer.set_enabled(isOn);
		}
	}
}
