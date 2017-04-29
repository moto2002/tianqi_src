using EntitySubSystem;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XEngine;
using XEngineActor;
using XEngineCommand;

public class EntityPlayer : EntityParent
{
	protected BattleBaseAttrs battleBaseAttrs = new BattleBaseAttrs();

	protected BackUpBattleBaseAttrs backUpBattleBaseAttrs = new BackUpBattleBaseAttrs();

	protected EquipCustomization mEquipCustomizationer;

	protected ExteriorArithmeticUnit exteriorUnit;

	protected int modelIDBackUp;

	protected IndexList<int, int> skillDicBackUp = new IndexList<int, int>();

	protected int fusePetID;

	protected bool isDieEnd;

	protected bool hasServerRemove;

	public override BattleBaseAttrs BattleBaseAttrs
	{
		get
		{
			return this.battleBaseAttrs;
		}
	}

	public override BackUpBattleBaseAttrs BackUpBattleBaseAttrs
	{
		get
		{
			return this.backUpBattleBaseAttrs;
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

	public override int IdleVpResume
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

	public override int VpResume
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

	public override int TypeID
	{
		get
		{
			return base.TypeID;
		}
		set
		{
			base.TypeID = value;
			this.ExteriorUnit.SetType(this.TypeID);
		}
	}

	public override int TypeRank
	{
		get
		{
			return base.TypeRank;
		}
		set
		{
			base.TypeRank = value;
			EventDispatcher.Broadcast<long, int>("BillboardManager.MilitaryRank", base.ID, value);
		}
	}

	public override int ModelID
	{
		get
		{
			return base.ModelID;
		}
		set
		{
			base.ModelID = value;
			this.ExteriorUnit.ServerModelID = value;
		}
	}

	public override MapObjDecorations Decorations
	{
		get
		{
			return base.Decorations;
		}
		set
		{
			base.Decorations = value;
			this.ExteriorUnit.WrapSetData(delegate
			{
				this.TypeID = this.decorations.career;
				this.ModelID = this.decorations.modelId;
				this.ExteriorUnit.EquipIDs = this.decorations.equipIds;
				this.ExteriorUnit.WingID = WingManager.GetWingModel(this.decorations.wingId, this.decorations.wingLv);
				this.ExteriorUnit.IsHideWing = this.decorations.wingHidden;
				this.ExteriorUnit.FashionIDs = this.decorations.fashions;
				this.ExteriorUnit.Gogok = this.decorations.gogokNum;
			});
		}
	}

	public override bool IsFuse
	{
		get
		{
			return base.IsFuse;
		}
		set
		{
			base.IsFuse = value;
			if (base.GetAIManager() != null && base.GetAIManager().IsAIActive())
			{
				EventDispatcher.Broadcast(AIManagerEvent.SelfAIDeactive);
				EventDispatcher.Broadcast(AIManagerEvent.SelfAIActive);
			}
		}
	}

	public override bool IsStatic
	{
		get
		{
			return base.IsStatic;
		}
		set
		{
			base.IsStatic = value;
			if (value)
			{
				this.IsAssault = false;
			}
		}
	}

	public override bool IsDizzy
	{
		get
		{
			return base.IsDizzy;
		}
		set
		{
			base.IsDizzy = value;
			if (value)
			{
				this.IsAssault = false;
			}
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
			if (value)
			{
				this.IsAssault = false;
				if (!this.IsEntitySelfType)
				{
					InstanceManager.CurrentInstance.PlayerDie(this);
				}
			}
			base.IsDead = value;
		}
	}

	public override bool IsInBattle
	{
		get
		{
			return this.isInBattle;
		}
		set
		{
			this.isInBattle = value;
			if (base.Actor)
			{
				base.Actor.ChangeAction("idle", true, false, 1f, 0, 0, string.Empty);
			}
		}
	}

	public override bool IsEntityPlayerType
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
			return 4;
		}
	}

	public override int FixModelID
	{
		get
		{
			return this.ExteriorUnit.FinalModelID;
		}
	}

	public EquipCustomization EquipCustomizationer
	{
		get
		{
			if (this.mEquipCustomizationer == null)
			{
				this.mEquipCustomizationer = new EquipCustomization();
			}
			return this.mEquipCustomizationer;
		}
	}

	public ExteriorArithmeticUnit ExteriorUnit
	{
		get
		{
			if (this.exteriorUnit == null)
			{
				this.exteriorUnit = new ExteriorArithmeticUnit(new Action<Action>(this.UpdateActor), new Action(this.UpdateWeapon), new Action(this.UpdateClothes), new Action(this.UpdateWing));
			}
			return this.exteriorUnit;
		}
	}

	public int ModelIDBackUp
	{
		get
		{
			return this.modelIDBackUp;
		}
		set
		{
			this.modelIDBackUp = value;
		}
	}

	public IndexList<int, int> SkillDicBackUp
	{
		get
		{
			return this.skillDicBackUp;
		}
		set
		{
			if (value == null)
			{
				this.skillDicBackUp = null;
				return;
			}
			this.skillDicBackUp.Clear();
			using (Dictionary<int, int>.Enumerator enumerator = value.GetPairPart().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> current = enumerator.get_Current();
					this.skillDicBackUp.Add(current.get_Key(), current.get_Value());
				}
			}
			List<int> singlePart = value.GetSinglePart();
			for (int i = 0; i < singlePart.get_Count(); i++)
			{
				this.skillDicBackUp.AddValue(singlePart.get_Item(i));
			}
		}
	}

	public int FusePetID
	{
		get
		{
			return this.fusePetID;
		}
		set
		{
			this.fusePetID = value;
		}
	}

	public bool IsDieEnd
	{
		get
		{
			return this.isDieEnd;
		}
		set
		{
			this.isDieEnd = value;
			if (this.HasServerRemove && base.Actor)
			{
				base.Actor.DeadToDestroy();
			}
		}
	}

	public bool HasServerRemove
	{
		get
		{
			return this.hasServerRemove;
		}
		set
		{
			this.hasServerRemove = value;
			if ((!this.IsDead || this.IsDieEnd) && base.Actor)
			{
				base.Actor.DeadToDestroy();
			}
		}
	}

	public EntityPlayer()
	{
		this.battleBaseAttrs.AttrChangedDelegate = new Action<GameData.AttrType, long, long>(this.OnAttrChanged);
	}

	public override void SetDataByMapObjInfo(MapObjInfo info, bool isClientCreate = false)
	{
		base.SetDataByMapObjInfo(info, isClientCreate);
		if (this.IsClientDominate && info != null && info.battleInfo != null)
		{
			this.BackUpBattleBaseAttrs.AssignAllAttrs(info.battleInfo);
		}
	}

	public override void CreateActor()
	{
		this.ExteriorUnit.IsAutoUpdateExterior = true;
	}

	public override void OnLeaveField()
	{
		if (base.Actor)
		{
			if (!base.IsPlayerMate)
			{
				EventDispatcher.Broadcast<Transform>(CameraEvent.PlayerDie, base.Actor.FixTransform);
			}
		}
		else
		{
			EntityWorld.Instance.CancelGetPlayerActorAsync(base.AsyncLoadID);
		}
		base.OnLeaveField();
	}

	protected override void InitManager()
	{
		if (!this.IsEntitySelfType)
		{
			this.m_subSystems.Add("PlayerAI", new PlayerAIManager());
			this.m_subSystems.Add("PlayerBattle", new PlayerBattleManager());
			this.m_subSystems.Add("PlayerBuff", new PlayerBuffManager());
			this.m_subSystems.Add("PlayerCondition", new PlayerConditionManager());
			this.m_subSystems.Add("PlayerSkill", new PlayerSkillManager());
			this.m_subSystems.Add("PlayerWarning", new PlayerWarningManager());
			this.m_subSystems.Add("PlayerFeedback", new PlayerFeedbackManager());
		}
		base.InitManager();
	}

	protected override void InitEntityState()
	{
		base.InitEntityState();
		if (!this.IsEntitySelfType)
		{
			this.IsInBattle = true;
		}
	}

	protected override void AddNetworkListener()
	{
		base.AddNetworkListener();
		if (!this.IsEntitySelfType && !this.IsClientDominate)
		{
			base.AddNetworkMoveRotateTeleportGoUpAndDownListener();
		}
	}

	protected override void RemoveNetworkListener()
	{
		base.RemoveNetworkListener();
		if (!this.IsEntitySelfType && !this.IsClientDominate)
		{
			base.RemoveNetworkMoveRotateTeleportGoUpAndDownListener();
		}
	}

	public override void InitActorState()
	{
		base.InitActorState();
		if (this.IsClientDominate)
		{
			if (InstanceManager.IsDebut)
			{
				this.DebutBattle();
			}
		}
		else
		{
			this.DebutBattle();
		}
		this.ExteriorUnit.IsAutoUpdateExterior = true;
		if (!base.IsPlayerMate)
		{
			EventDispatcher.Broadcast<int, Transform>(CameraEvent.PlayerBorn, this.TypeID, base.Actor.FixTransform);
		}
		if (!this.IsEntitySelfType)
		{
			InstanceManager.PlayerInitEnd(this);
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

	protected override void OnChangeLv(long oldValue, long newValue)
	{
		if (oldValue != newValue)
		{
			HeadInfoManager.Instance.SetName(1, base.ID, (int)newValue, this.Name);
		}
	}

	protected override void OnChangeHp(long oldValue, long newValue)
	{
		base.OnChangeHp(oldValue, newValue);
		if (!this.IsEntitySelfType)
		{
			InstanceManager.CurrentInstance.PlayerHpChange(this);
		}
	}

	protected override void OnChangeRealHpLmt(long oldValue, long newValue)
	{
		base.OnChangeRealHpLmt(oldValue, newValue);
		if (!this.IsEntitySelfType)
		{
			InstanceManager.CurrentInstance.PlayerHpLmtChange(this);
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
			this.ActivePlayer();
		}
		else if (!this.IsDead)
		{
			this.DeactivePlayer();
		}
	}

	protected override void SetIsCloseRenderer(bool state)
	{
		base.SetIsCloseRenderer(state);
		this.ShowWeapon(!state);
	}

	protected override void ChangeDecoration(short state, MapObjDecorationChangedNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (base.ID != down.objId)
		{
			return;
		}
		this.Decorations = down.decorations;
	}

	protected virtual void UpdateActor(Action logicCallback = null)
	{
		this.DestoryOldPlayerActor();
		if (this.IsClientDominate)
		{
			base.AsyncLoadID = EntityWorld.Instance.GetPlayerActorAsync(this.FixModelID, delegate(ActorPlayer newActor)
			{
				this.SetPlayerActor(newActor, logicCallback);
			});
		}
		else
		{
			ActorPlayer playerActor = EntityWorld.Instance.GetPlayerActor(this.FixModelID);
			this.SetPlayerActor(playerActor, logicCallback);
		}
	}

	protected void DestoryOldPlayerActor()
	{
		this.ExteriorUnit.IsAutoUpdateExterior = false;
		if (base.Actor)
		{
			ActorParent actor = base.Actor;
			base.Pos = actor.FixTransform.get_position();
			base.Dir = actor.FixTransform.get_forward();
			if (this.IsInBattle)
			{
				actor.FadeDestroyScript(this.alphaControls);
			}
			else
			{
				actor.DestroyScript();
			}
		}
		else
		{
			EntityWorld.Instance.CancelGetPlayerActorAsync(base.AsyncLoadID);
		}
	}

	protected void SetPlayerActor(ActorPlayer actorPlayer, Action logicCallback)
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.FixModelID);
		base.Actor = actorPlayer;
		actorPlayer.theEntity = this;
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
		if (this.IsInBattle)
		{
			if (this.IsDead)
			{
				CommandCenter.ExecuteCommand(base.Actor.FixTransform, new PlayActionCmd
				{
					actName = "die",
					jumpToPlay = true,
					percent = 1f
				});
			}
			else
			{
				base.Actor.CastAction("born", true, 1f, 0, 0, string.Empty);
			}
		}
		else
		{
			base.Actor.CastAction("idle", true, 1f, 0, 0, string.Empty);
		}
		if (logicCallback != null)
		{
			logicCallback.Invoke();
		}
		this.InitActorState();
	}

	protected void UpdateWeapon()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.EquipGogokNum = this.ExteriorUnit.FinalWeaponGogok;
		this.EquipCustomizationer.EquipOn(this.ExteriorUnit.FinalWeaponID);
	}

	protected void UpdateClothes()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.EquipOn(this.ExteriorUnit.FinalClothesID);
	}

	protected void UpdateWing()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.EquipWingOn(this.ExteriorUnit.FinalWingID);
	}

	public void ChangeWeaponSlot(string name, bool changeSlot, bool resetlocal)
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.ChangeWeaponSlot(name, changeSlot, resetlocal);
	}

	public void CheckWeaponSlot()
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.CheckWeaponSlot("weapon_slot");
	}

	public void ShowWeapon(bool isShow)
	{
		this.EquipCustomizationer.ActorTarget = base.Actor;
		this.EquipCustomizationer.ShowWeapon(isShow);
	}

	public override void EquipSuccess()
	{
		ShaderEffectUtils.InitShaderRenderers(base.Actor.FixTransform, this.shaderRenderers, ref this.shadowRenderer, ref this.shadowSlicePlane);
		ShaderEffectUtils.InitHitEffects(this.shaderRenderers, this.hitControls);
		ShaderEffectUtils.InitTransparencys(this.shaderRenderers, this.alphaControls);
	}

	public override void EquipWingSuccess(Animator wing_tor)
	{
		base.EquipWingSuccess(wing_tor);
		this.EquipSuccess();
	}

	public override void InitBillboard(float height, List<int> bloodBarSize)
	{
		int actorType = (!base.IsPlayerMate) ? 5 : 3;
		BillboardManager.Instance.AddBillboardsInfo(actorType, base.Actor, height, base.ID, false, true, !this.IsDead);
		HeadInfoManager.Instance.SetName(actorType, base.ID, this.Lv, this.Name);
		HeadInfoManager.Instance.SetTitle(base.ID, this.TitleID);
		HeadInfoManager.Instance.SetGuildTitle(base.ID, this.GuildTitle);
		HeadInfoManager.Instance.SetBloodBarSize(base.ID, bloodBarSize);
		this.UpdateBlood();
	}

	protected void ActivePlayer()
	{
		if (this.IsClientDominate)
		{
			base.GetAIManager().Active();
			base.GetFeedbackManager().Active();
		}
	}

	protected void DeactivePlayer()
	{
		if (this.IsClientDominate)
		{
			base.GetAIManager().Deactive();
			base.GetFeedbackManager().Deactive();
		}
	}

	public virtual void BackUpSkill()
	{
		this.SkillDicBackUp = this.SkillInfo;
		this.UpdateSkill();
	}

	protected override void ThrowBloodEffect(HPChangeMessage data)
	{
		if (data.hpChangeType != HPChangeMessage.HPChangeType.KillRecover)
		{
			WaveBloodManager.Instance.ThrowBlood(data);
		}
	}

	protected override void UpdateHpChangeInfluence(HPChangeMessage data)
	{
		switch (data.hpChangeType)
		{
		case HPChangeMessage.HPChangeType.Normal:
		case HPChangeMessage.HPChangeType.Critical:
		case HPChangeMessage.HPChangeType.Parry:
		case HPChangeMessage.HPChangeType.CriticalAndParry:
			if (base.IsPlayerMate)
			{
				if (!this.IsEntitySelfType && DataReader<JJiaoSeSheZhi>.Get(this.TypeID).hitTips > 0)
				{
					EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 1, new LiveTextMessage
					{
						contentID = DataReader<JJiaoSeSheZhi>.Get(this.TypeID).hitTips,
						showTime = 2000,
						name = this.Name
					});
				}
			}
			else if (data.modeType == HPChangeMessage.ModeType.MonsterBySelf && base.Actor)
			{
				EventDispatcher.Broadcast<int, Transform>(CameraEvent.PlayerGetHit, this.TypeID, base.Actor.FixTransform);
			}
			break;
		case HPChangeMessage.HPChangeType.Miss:
			if (base.IsPlayerMate && !this.IsEntitySelfType && DataReader<JJiaoSeSheZhi>.Get(this.TypeID).hitTips > 0)
			{
				EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 1, new LiveTextMessage
				{
					contentID = DataReader<JJiaoSeSheZhi>.Get(this.TypeID).hitTips,
					showTime = 2000,
					name = this.Name
				});
			}
			break;
		case HPChangeMessage.HPChangeType.KillRecover:
			if (base.Actor && !this.IsEntitySelfType)
			{
				int templateId = (int)float.Parse(DataReader<GlobalParams>.Get("killReply").value);
				float speed = float.Parse(DataReader<GlobalParams>.Get("killReplySpeed").value) * 0.01f;
				float lessDistance = float.Parse(DataReader<GlobalParams>.Get("killReplyHeight_y").value) * 0.01f;
				int getFxID = (int)float.Parse(DataReader<GlobalParams>.Get("killReply_get").value);
				FXManager.Instance.PlayFXOfFollow(templateId, data.casterPosition, base.Actor.FixTransform, speed, lessDistance, XUtility.GetHitRadius(base.Actor.FixTransform), delegate
				{
					if (!this.Actor)
					{
						return;
					}
					FXManager.Instance.PlayFX(getFxID, this.Actor.FixTransform, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
					WaveBloodManager.Instance.ThrowBlood(data);
				}, FXClassification.Normal);
			}
			break;
		}
		base.UpdateHpChangeInfluence(data);
	}

	public override void DieBegin()
	{
		if (base.IsPlayerMate && !this.IsEntitySelfType && DataReader<JJiaoSeSheZhi>.Get(this.TypeID).dieTips > 0)
		{
			EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 2, new LiveTextMessage
			{
				contentID = DataReader<JJiaoSeSheZhi>.Get(this.TypeID).dieTips,
				showTime = 2000,
				name = this.Name
			});
		}
		if (!base.IsPlayerMate && base.Actor)
		{
			EventDispatcher.Broadcast<Transform>(CameraEvent.PlayerDie, base.Actor.FixTransform);
		}
		base.DieBegin();
	}

	public override void DieEnd()
	{
		this.IsDieEnd = true;
	}

	public virtual void DieEndDefuse()
	{
		this.FusePetID = 0;
		this.ModelID = this.ModelIDBackUp;
		this.SetSkillDic(this.SkillDicBackUp);
		this.IsFuse = false;
		ShaderEffectUtils.SetFade(this.alphaControls, false, null);
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.ENABLE_SCREEN_LENS, false);
		this.IsDieEnd = true;
	}

	public void ReceiveServerRemove()
	{
		this.HasServerRemove = true;
	}

	protected override void GetInfoString(StringBuilder result)
	{
		base.GetInfoString(result);
		this.ExteriorUnit.GetInfoString(result);
	}
}
