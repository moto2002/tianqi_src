using EntitySubSystem;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using XEngineActor;

public class EntityPet : EntityParent
{
	protected BattleBaseAttrs battleBaseAttrs = new BattleBaseAttrs();

	protected BackUpBattleBaseAttrs backUpBattleBaseAttrs = new BackUpBattleBaseAttrs();

	protected float existTime;

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

	public override bool IsDead
	{
		get
		{
			return base.IsDead;
		}
		set
		{
			base.IsDead = value;
			if (value)
			{
				this.DeactivePet();
				if (base.Owner.IsEntitySelfType)
				{
					BattleBlackboard.Instance.IsAllPetAlive = false;
					EventDispatcher.Broadcast<EntityPet>(LocalInstanceEvent.UnmarkSelfPet, this);
				}
			}
		}
	}

	public override bool IsEntityPetType
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
			return 2;
		}
	}

	public float ExistTime
	{
		get
		{
			return this.existTime;
		}
		set
		{
			this.existTime = value;
		}
	}

	public float FuseTime
	{
		get
		{
			return (float)(this.Affinity * 120 / (this.Lv + 180));
		}
	}

	public EntityPet()
	{
		this.battleBaseAttrs.AttrChangedDelegate = new Action<GameData.AttrType, long, long>(this.OnAttrChanged);
	}

	public override void SetDataByMapObjInfo(MapObjInfo info, bool isClientCreate = false)
	{
		base.SetDataByMapObjInfo(info, isClientCreate);
		this.InitModel();
		if (this.IsClientDominate)
		{
			this.BackUpBattleBaseAttrs.AssignAllAttrs(info.battleInfo);
		}
	}

	public override void CreateActor()
	{
		ActorPet petActor = EntityWorld.Instance.GetPetActor(this.FixModelID);
		Pet pet = DataReader<Pet>.Get(this.TypeID);
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.FixModelID);
		base.Actor = petActor;
		petActor.theEntity = this;
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
		ShaderEffectUtils.SetHSV(base.Actor.FixTransform, pet.colour);
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
		this.InitActorState();
	}

	protected override void InitEntityState()
	{
		if (!base.Actor)
		{
			EntityWorld.Instance.CancelGetPetActorAsync(base.AsyncLoadID);
		}
		base.InitEntityState();
		this.IsInBattle = true;
	}

	public override void OnLeaveField()
	{
		this.DeactivePet();
		base.OnLeaveField();
	}

	protected override void InitManager()
	{
		this.m_subSystems.Add("PetAI", new PetAIManager());
		this.m_subSystems.Add("PetBattle", new PetBattleManager());
		this.m_subSystems.Add("PetBuff", new PetBuffManager());
		this.m_subSystems.Add("PetCondition", new PetConditionManager());
		this.m_subSystems.Add("PetSkill", new PetSkillManager());
		this.m_subSystems.Add("PetWarning", new PetWarningManager());
		base.InitManager();
	}

	public override void InitActorState()
	{
		base.SetCheckDead(this.Hp);
		if (base.IsClientDrive && base.Owner != null && !base.Owner.IsEntitySelfType && base.Owner.Actor)
		{
			base.SetPos(LocalInstanceHandler.Instance.GetPetRandomPos(this.TypeID, base.Owner.Actor.FixTransform.get_position(), base.Owner.Actor.FixTransform.get_rotation()));
			base.SetDir(LocalInstanceHandler.Instance.GetPetDir(base.Owner.Actor.FixTransform.get_rotation()));
		}
		else
		{
			base.SetPos(base.Pos);
			base.SetDir(base.Dir);
		}
		base.SetFloor(base.Floor);
		this.SetIsFighting(this.IsFighting);
		this.SetIsCloseRenderer(this.IsCloseRenderer);
		base.SetMoveSpeed((long)this.RealMoveSpeed);
		base.SetDefaultActionSpeed((long)this.ActSpeed);
		base.SetRunActionSpeed((long)this.RealActionSpeed);
		base.SetPressSkill(base.IsSkillPressing, base.PressingSkillID, base.Dir, this.IsSkillInTrustee);
		if (this.IsClientDominate)
		{
			base.GetConditionManager().RegistCounterSkillCondition(base.GetSkillAllValue());
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
		if (base.Owner != null && base.Owner.IsEntitySelfType && (this.IsFighting || this.IsDead))
		{
			BattleBlackboard.Instance.PetHpMessage = new KeyValuePair<long, long>(this.Hp, this.RealHpLmt);
		}
	}

	protected override void OnChangeRealHpLmt(long oldValue, long newValue)
	{
		base.OnChangeRealHpLmt(oldValue, newValue);
		if (base.Owner != null && base.Owner.IsEntitySelfType && (this.IsFighting || this.IsDead))
		{
			BattleBlackboard.Instance.PetHpMessage = new KeyValuePair<long, long>(this.Hp, this.RealHpLmt);
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
			base.IsVisible = true;
			this.ActivePet();
			if (base.Owner != null && base.Owner.IsEntitySelfType)
			{
				EventDispatcher.Broadcast<EntityPet>(LocalInstanceEvent.MarkSelfPet, this);
			}
		}
		else if (!this.IsDead)
		{
			base.IsVisible = false;
			this.DeactivePet();
		}
	}

	protected override void InitModel()
	{
		this.ModelID = PetManagerBase.GetPlayerPetModel(this.TypeID, this.TypeRank);
	}

	public override void InitBillboard(float height, List<int> bloodBarSize)
	{
		int actorType = (!base.IsPlayerMate) ? 6 : ((base.OwnerID != EntityWorld.Instance.EntSelf.ID) ? 4 : 2);
		BillboardManager.Instance.AddBillboardsInfo(actorType, base.Actor, height, base.ID, false, true, !this.IsDead);
		HeadInfoManager.Instance.SetName(actorType, base.ID, this.Name);
		this.UpdateBlood();
	}

	protected void ActivePet()
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

	protected void DeactivePet()
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

	protected override void UpdateHpChangeInfluence(HPChangeMessage data)
	{
		switch (data.hpChangeType)
		{
		case HPChangeMessage.HPChangeType.Normal:
		case HPChangeMessage.HPChangeType.Critical:
		case HPChangeMessage.HPChangeType.Parry:
		case HPChangeMessage.HPChangeType.CriticalAndParry:
		case HPChangeMessage.HPChangeType.Miss:
			if (base.IsPlayerMate && base.OwnerID != EntityWorld.Instance.EntSelf.ID && DataReader<Pet>.Get(this.TypeID).hitTips > 0)
			{
				EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 3, new LiveTextMessage
				{
					contentID = DataReader<Pet>.Get(this.TypeID).hitTips,
					showTime = 2000,
					name = this.Name
				});
			}
			break;
		}
		base.UpdateHpChangeInfluence(data);
	}

	public override void DieBegin()
	{
		if (base.IsPlayerMate && base.OwnerID != EntityWorld.Instance.EntSelf.ID && DataReader<Pet>.Get(this.TypeID).dieTips > 0)
		{
			EventDispatcher.Broadcast<int, LiveTextMessage>(BattleUIEvent.LiveText, 4, new LiveTextMessage
			{
				contentID = DataReader<Pet>.Get(this.TypeID).dieTips,
				showTime = 2000,
				name = this.Name
			});
		}
		base.DieBegin();
	}
}
