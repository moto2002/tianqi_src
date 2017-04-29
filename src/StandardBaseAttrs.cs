using GameData;
using System;
using UnityEngine;

public class StandardBaseAttrs : BattleBaseAttrExtend, IClientBaseAttr, IClientBattleBaseAttr, IStandardBaseAttr
{
	protected AttrCoder attrCoder = new AttrCoder();

	private CfgFormula<int> moveSpeed;

	private CfgFormula<int> actSpeed;

	private CfgFormula<int> lv;

	private CfgFormula<long> fighting;

	private CfgFormula<int> vipLv;

	private CfgFormula<int> atk;

	private CfgFormula<int> defence;

	private CfgFormula<long> hpLmt;

	private CfgFormula<int> pveAtk;

	private CfgFormula<int> pvpAtk;

	private CfgFormula<int> hitRatio;

	private CfgFormula<int> dodgeRatio;

	private CfgFormula<int> critRatio;

	private CfgFormula<int> decritRatio;

	private CfgFormula<int> critHurtAddRatio;

	private CfgFormula<int> parryRatio;

	private CfgFormula<int> deparryRatio;

	private CfgFormula<int> parryHurtDeRatio;

	private CfgFormula<int> suckBloodScale;

	private CfgFormula<int> hurtAddRatio;

	private CfgFormula<int> hurtDeRatio;

	private CfgFormula<int> pveHurtAddRatio;

	private CfgFormula<int> pveHurtDeRatio;

	private CfgFormula<int> pvpHurtAddRatio;

	private CfgFormula<int> pvpHurtDeRatio;

	private CfgFormula<int> atkMulAmend;

	private CfgFormula<int> defMulAmend;

	private CfgFormula<int> hpLmtMulAmend;

	private CfgFormula<int> pveAtkMulAmend;

	private CfgFormula<int> pvpAtkMulAmend;

	private CfgFormula<int> actPointRecoverSpeedAmend;

	private CfgFormula<int> vpLmt;

	private CfgFormula<int> vpLmtMulAmend;

	private CfgFormula<int> vpAtk;

	private CfgFormula<int> vpAtkMulAmend;

	private CfgFormula<int> vpResume;

	private CfgFormula<int> idleVpResume;

	private CfgFormula<int> waterBuffAddProbAddAmend;

	private CfgFormula<int> waterBuffDurTimeAddAmend;

	private CfgFormula<int> thunderBuffAddProbAddAmend;

	private CfgFormula<int> thunderBuffDurTimeAddAmend;

	private CfgFormula<int> healIncreasePercent;

	private CfgFormula<int> critAddValue;

	private CfgFormula<int> hpRestore;

	private CfgFormula<long> exp;

	private CfgFormula<long> expLmt;

	private CfgFormula<int> energy;

	private CfgFormula<int> energyLmt;

	private CfgFormula<int> diamond;

	private CfgFormula<long> gold;

	private CfgFormula<int> rechargeDiamond;

	private CfgFormula<int> honor;

	private CfgFormula<int> competitiveCurrency;

	private CfgFormula<int> skillPoint;

	private CfgFormula<int> reputation;

	private CfgFormula<int> actPointLmt;

	private CfgFormula<int> buffMoveSpeedMulPosAmend;

	private CfgFormula<int> buffActSpeedMulPosAmend;

	private CfgFormula<int> skillTreatScaleBOAtk;

	private CfgFormula<int> skillTreatScaleBOHpLmt;

	private CfgFormula<int> skillIgnoreDefenceHurt;

	private CfgFormula<int> skillNmlDmgScale;

	private CfgFormula<int> skillNmlDmgAddAmend;

	private CfgFormula<int> skillHolyDmgScaleBOMaxHp;

	private CfgFormula<int> skillHolyDmgScaleBOCurHp;

	private CfgFormula<int> affinity;

	private CfgFormula<int> onlineTime;

	private CfgFormula<int> actPoint;

	private CfgFormula<long> hp;

	private CfgFormula<int> vp;

	private CfgFormula<long> realHpLmt;

	private CfgFormula<int> realVpLmt;

	private CfgFormula<int> realMoveSpeed;

	private CfgFormula<int> realActionSpeed;

	public int MoveSpeed
	{
		get
		{
			return this.moveSpeed.Val;
		}
		set
		{
			this.SetValue(AttrType.MoveSpeed, value, true);
		}
	}

	public int ActSpeed
	{
		get
		{
			return this.actSpeed.Val;
		}
		set
		{
			this.SetValue(AttrType.ActSpeed, value, true);
		}
	}

	public int Lv
	{
		get
		{
			return this.lv.Val;
		}
		set
		{
			this.SetValue(AttrType.Lv, value, true);
		}
	}

	public long Fighting
	{
		get
		{
			return this.fighting.Val;
		}
		set
		{
			this.SetValue(AttrType.Fighting, value, true);
		}
	}

	public int VipLv
	{
		get
		{
			return this.vipLv.Val;
		}
		set
		{
			this.SetValue(AttrType.VipLv, value, true);
		}
	}

	public int Atk
	{
		get
		{
			return this.atk.Val;
		}
		set
		{
			this.SetValue(AttrType.Atk, value, true);
		}
	}

	public int Defence
	{
		get
		{
			return this.defence.Val;
		}
		set
		{
			this.SetValue(AttrType.Defence, value, true);
		}
	}

	public long HpLmt
	{
		get
		{
			return this.hpLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.HpLmt, value, true);
		}
	}

	public int PveAtk
	{
		get
		{
			return this.pveAtk.Val;
		}
		set
		{
			this.SetValue(AttrType.PveAtk, value, true);
		}
	}

	public int PvpAtk
	{
		get
		{
			return this.pvpAtk.Val;
		}
		set
		{
			this.SetValue(AttrType.PvpAtk, value, true);
		}
	}

	public int HitRatio
	{
		get
		{
			return this.hitRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.HitRatio, value, true);
		}
	}

	public int DodgeRatio
	{
		get
		{
			return this.dodgeRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.DodgeRatio, value, true);
		}
	}

	public int CritRatio
	{
		get
		{
			return this.critRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.CritRatio, value, true);
		}
	}

	public int DecritRatio
	{
		get
		{
			return this.decritRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.DecritRatio, value, true);
		}
	}

	public int CritHurtAddRatio
	{
		get
		{
			return this.critHurtAddRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.CritHurtAddRatio, value, true);
		}
	}

	public int ParryRatio
	{
		get
		{
			return this.parryRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.ParryRatio, value, true);
		}
	}

	public int DeparryRatio
	{
		get
		{
			return this.deparryRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.DeparryRatio, value, true);
		}
	}

	public int ParryHurtDeRatio
	{
		get
		{
			return this.parryHurtDeRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.ParryHurtDeRatio, value, true);
		}
	}

	public int SuckBloodScale
	{
		get
		{
			return this.suckBloodScale.Val;
		}
		set
		{
			this.SetValue(AttrType.SuckBloodScale, value, true);
		}
	}

	public int HurtAddRatio
	{
		get
		{
			return this.hurtAddRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.HurtAddRatio, value, true);
		}
	}

	public int HurtDeRatio
	{
		get
		{
			return this.hurtDeRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.HurtDeRatio, value, true);
		}
	}

	public int PveHurtAddRatio
	{
		get
		{
			return this.pveHurtAddRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.PveHurtAddRatio, value, true);
		}
	}

	public int PveHurtDeRatio
	{
		get
		{
			return this.pveHurtDeRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.PveHurtDeRatio, value, true);
		}
	}

	public int PvpHurtAddRatio
	{
		get
		{
			return this.pvpHurtAddRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.PvpHurtAddRatio, value, true);
		}
	}

	public int PvpHurtDeRatio
	{
		get
		{
			return this.pvpHurtDeRatio.Val;
		}
		set
		{
			this.SetValue(AttrType.PvpHurtDeRatio, value, true);
		}
	}

	public int AtkMulAmend
	{
		get
		{
			return this.atkMulAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.AtkMulAmend, value, true);
		}
	}

	public int DefMulAmend
	{
		get
		{
			return this.defMulAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.DefMulAmend, value, true);
		}
	}

	public int HpLmtMulAmend
	{
		get
		{
			return this.hpLmtMulAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.HpLmtMulAmend, value, true);
		}
	}

	public int PveAtkMulAmend
	{
		get
		{
			return this.pveAtkMulAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.PveAtkMulAmend, value, true);
		}
	}

	public int PvpAtkMulAmend
	{
		get
		{
			return this.pvpAtkMulAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.PvpAtkMulAmend, value, true);
		}
	}

	public int ActPointRecoverSpeedAmend
	{
		get
		{
			return this.actPointRecoverSpeedAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.ActPointRecoverSpeedAmend, value, true);
		}
	}

	public int VpLmt
	{
		get
		{
			return this.vpLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.VpLmt, value, true);
		}
	}

	public int VpLmtMulAmend
	{
		get
		{
			return this.vpLmtMulAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.VpLmtMulAmend, value, true);
		}
	}

	public int VpAtk
	{
		get
		{
			return this.vpAtk.Val;
		}
		set
		{
			this.SetValue(AttrType.VpAtk, value, true);
		}
	}

	public int VpAtkMulAmend
	{
		get
		{
			return this.vpAtkMulAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.VpAtkMulAmend, value, true);
		}
	}

	public int VpResume
	{
		get
		{
			return this.vpResume.Val;
		}
		set
		{
			this.SetValue(AttrType.VpResume, value, true);
		}
	}

	public int IdleVpResume
	{
		get
		{
			return this.idleVpResume.Val;
		}
		set
		{
			this.SetValue(AttrType.IdleVpResume, value, true);
		}
	}

	public int WaterBuffAddProbAddAmend
	{
		get
		{
			return this.waterBuffAddProbAddAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.WaterBuffAddProbAddAmend, value, true);
		}
	}

	public int WaterBuffDurTimeAddAmend
	{
		get
		{
			return this.waterBuffDurTimeAddAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.WaterBuffDurTimeAddAmend, value, true);
		}
	}

	public int ThunderBuffAddProbAddAmend
	{
		get
		{
			return this.thunderBuffAddProbAddAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.ThunderBuffAddProbAddAmend, value, true);
		}
	}

	public int ThunderBuffDurTimeAddAmend
	{
		get
		{
			return this.thunderBuffDurTimeAddAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.ThunderBuffDurTimeAddAmend, value, true);
		}
	}

	public int HealIncreasePercent
	{
		get
		{
			return this.healIncreasePercent.Val;
		}
		set
		{
			this.SetValue(AttrType.HealIncreasePercent, value, true);
		}
	}

	public int CritAddValue
	{
		get
		{
			return this.critAddValue.Val;
		}
		set
		{
			this.SetValue(AttrType.CritAddValue, value, true);
		}
	}

	public int HpRestore
	{
		get
		{
			return this.hpRestore.Val;
		}
		set
		{
			this.SetValue(AttrType.HpRestore, value, true);
		}
	}

	public long Exp
	{
		get
		{
			return this.exp.Val;
		}
		set
		{
			this.SetValue(AttrType.Exp, value, true);
		}
	}

	public long ExpLmt
	{
		get
		{
			return this.expLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.ExpLmt, value, true);
		}
	}

	public int Energy
	{
		get
		{
			return this.energy.Val;
		}
		set
		{
			this.SetValue(AttrType.Energy, value, true);
		}
	}

	public int EnergyLmt
	{
		get
		{
			return this.energyLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.EnergyLmt, value, true);
		}
	}

	public int Diamond
	{
		get
		{
			return this.diamond.Val;
		}
		set
		{
			this.SetValue(AttrType.Diamond, value, true);
		}
	}

	public long Gold
	{
		get
		{
			return this.gold.Val;
		}
		set
		{
			this.SetValue(AttrType.Gold, value, true);
		}
	}

	public int RechargeDiamond
	{
		get
		{
			return this.rechargeDiamond.Val;
		}
		set
		{
			this.SetValue(AttrType.RechargeDiamond, value, true);
		}
	}

	public int Honor
	{
		get
		{
			return this.honor.Val;
		}
		set
		{
			this.SetValue(AttrType.Honor, value, true);
		}
	}

	public int CompetitiveCurrency
	{
		get
		{
			return this.competitiveCurrency.Val;
		}
		set
		{
			this.SetValue(AttrType.CompetitiveCurrency, value, true);
		}
	}

	public int SkillPoint
	{
		get
		{
			return this.skillPoint.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillPoint, value, true);
		}
	}

	public int Reputation
	{
		get
		{
			return this.reputation.Val;
		}
		set
		{
			this.SetValue(AttrType.Reputation, value, true);
		}
	}

	public int ActPointLmt
	{
		get
		{
			return this.actPointLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.ActPointLmt, value, true);
		}
	}

	public int BuffMoveSpeedMulPosAmend
	{
		get
		{
			return this.buffMoveSpeedMulPosAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.BuffMoveSpeedMulPosAmend, value, true);
		}
	}

	public int BuffActSpeedMulPosAmend
	{
		get
		{
			return this.buffActSpeedMulPosAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.BuffActSpeedMulPosAmend, value, true);
		}
	}

	public int SkillTreatScaleBOAtk
	{
		get
		{
			return this.skillTreatScaleBOAtk.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillTreatScaleBOAtk, value, true);
		}
	}

	public int SkillTreatScaleBOHpLmt
	{
		get
		{
			return this.skillTreatScaleBOHpLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillTreatScaleBOHpLmt, value, true);
		}
	}

	public int SkillIgnoreDefenceHurt
	{
		get
		{
			return this.skillIgnoreDefenceHurt.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillIgnoreDefenceHurt, value, true);
		}
	}

	public int SkillNmlDmgScale
	{
		get
		{
			return this.skillNmlDmgScale.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillNmlDmgScale, value, true);
		}
	}

	public int SkillNmlDmgAddAmend
	{
		get
		{
			return this.skillNmlDmgAddAmend.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillNmlDmgAddAmend, value, true);
		}
	}

	public int SkillHolyDmgScaleBOMaxHp
	{
		get
		{
			return this.skillHolyDmgScaleBOMaxHp.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillHolyDmgScaleBOMaxHp, value, true);
		}
	}

	public int SkillHolyDmgScaleBOCurHp
	{
		get
		{
			return this.skillHolyDmgScaleBOCurHp.Val;
		}
		set
		{
			this.SetValue(AttrType.SkillHolyDmgScaleBOCurHp, value, true);
		}
	}

	public int Affinity
	{
		get
		{
			return this.affinity.Val;
		}
		set
		{
			this.SetValue(AttrType.Affinity, value, true);
		}
	}

	public int OnlineTime
	{
		get
		{
			return this.onlineTime.Val;
		}
		set
		{
			this.SetValue(AttrType.OnlineTime, value, true);
		}
	}

	public int ActPoint
	{
		get
		{
			return this.actPoint.Val;
		}
		set
		{
			this.SetValue(AttrType.ActPoint, value, true);
		}
	}

	public long Hp
	{
		get
		{
			return this.hp.Val;
		}
		set
		{
			this.SetValue(AttrType.Hp, value, true);
		}
	}

	public int Vp
	{
		get
		{
			return this.vp.Val;
		}
		set
		{
			this.SetValue(AttrType.Vp, value, true);
		}
	}

	public long RealHpLmt
	{
		get
		{
			return this.realHpLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.RealHpLmt, (value <= 0L) ? 1L : value, true);
		}
	}

	public int RealVpLmt
	{
		get
		{
			return this.realVpLmt.Val;
		}
		set
		{
			this.SetValue(AttrType.RealVpLmt, (value <= 0) ? 1 : value, true);
		}
	}

	public int RealMoveSpeed
	{
		get
		{
			return this.realMoveSpeed.Val;
		}
		set
		{
			this.SetValue(AttrType.RealMoveSpeed, value, true);
		}
	}

	public int RealActionSpeed
	{
		get
		{
			return this.realActionSpeed.Val;
		}
		set
		{
			this.SetValue(AttrType.RealActionSpeed, value, true);
		}
	}

	public StandardBaseAttrs()
	{
		this.moveSpeed = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1000, this.attrCoder);
		this.actSpeed = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1000, this.attrCoder);
		this.lv = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1, this.attrCoder);
		this.fighting = CfgFormulaBuilder<long>.Build(CfgFormulaType.AddAccum, 0L, this.attrCoder);
		this.vipLv = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.atk = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.defence = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.hpLmt = CfgFormulaBuilder<long>.Build(CfgFormulaType.AddAccum, 0L, this.attrCoder);
		this.pveAtk = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.pvpAtk = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.hitRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1, this.attrCoder);
		this.dodgeRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.critRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.decritRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.critHurtAddRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.parryRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.deparryRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.parryHurtDeRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.suckBloodScale = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.hurtAddRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.hurtDeRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.pveHurtAddRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.pveHurtDeRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.pvpHurtAddRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.pvpHurtDeRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.atkMulAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.defMulAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.hpLmtMulAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.pveAtkMulAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.pvpAtkMulAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.actPointLmt = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.actPointRecoverSpeedAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1, this.attrCoder);
		this.vpLmt = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.vpLmtMulAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.vpAtk = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.vpAtkMulAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.vpResume = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.idleVpResume = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.waterBuffAddProbAddAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.waterBuffDurTimeAddAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.thunderBuffAddProbAddAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.thunderBuffDurTimeAddAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.healIncreasePercent = CfgFormulaBuilder<int>.Build(CfgFormulaType.MulAccum, 0, this.attrCoder);
		this.critAddValue = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.hpRestore = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.exp = CfgFormulaBuilder<long>.Build(CfgFormulaType.AddAccum, 0L, this.attrCoder);
		this.expLmt = CfgFormulaBuilder<long>.Build(CfgFormulaType.AddAccum, 0L, this.attrCoder);
		this.energy = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.energyLmt = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.diamond = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.gold = CfgFormulaBuilder<long>.Build(CfgFormulaType.AddAccum, 0L, this.attrCoder);
		this.rechargeDiamond = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.honor = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.competitiveCurrency = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.buffMoveSpeedMulPosAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.buffActSpeedMulPosAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.skillTreatScaleBOAtk = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.skillTreatScaleBOHpLmt = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.skillIgnoreDefenceHurt = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.skillNmlDmgScale = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.skillNmlDmgAddAmend = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.skillHolyDmgScaleBOMaxHp = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.skillHolyDmgScaleBOCurHp = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.affinity = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.onlineTime = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.actPoint = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.hp = CfgFormulaBuilder<long>.Build(CfgFormulaType.AddAccum, 0L, this.attrCoder);
		this.vp = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.realHpLmt = CfgFormulaBuilder<long>.Build(CfgFormulaType.AddAccum, 1L, this.attrCoder);
		this.realVpLmt = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 0, this.attrCoder);
		this.realMoveSpeed = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1000, this.attrCoder);
		this.realActionSpeed = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1000, this.attrCoder);
	}

	public StandardBaseAttrs(StandardBaseAttrs other)
	{
		this.ResetAllAttrs();
		this.AssignAllAttrs(other);
	}

	public static StandardBaseAttrs CopyAllAttrs(StandardBaseAttrs origin)
	{
		if (origin == null)
		{
			return null;
		}
		return new StandardBaseAttrs
		{
			MoveSpeed = origin.MoveSpeed,
			ActSpeed = origin.ActSpeed,
			Lv = origin.Lv,
			Fighting = origin.Fighting,
			VipLv = origin.VipLv,
			Atk = origin.Atk,
			Defence = origin.Defence,
			HpLmt = origin.HpLmt,
			PveAtk = origin.PveAtk,
			PvpAtk = origin.PvpAtk,
			HitRatio = origin.HitRatio,
			DodgeRatio = origin.DodgeRatio,
			CritRatio = origin.CritRatio,
			DecritRatio = origin.DecritRatio,
			CritHurtAddRatio = origin.CritHurtAddRatio,
			ParryRatio = origin.ParryRatio,
			DeparryRatio = origin.DeparryRatio,
			ParryHurtDeRatio = origin.ParryHurtDeRatio,
			SuckBloodScale = origin.SuckBloodScale,
			HurtAddRatio = origin.HurtAddRatio,
			HurtDeRatio = origin.HurtDeRatio,
			PveHurtAddRatio = origin.PveHurtAddRatio,
			PvpHurtAddRatio = origin.PvpHurtAddRatio,
			PvpHurtDeRatio = origin.PvpHurtDeRatio,
			AtkMulAmend = origin.AtkMulAmend,
			DefMulAmend = origin.DefMulAmend,
			HpLmtMulAmend = origin.HpLmtMulAmend,
			PveAtkMulAmend = origin.PveAtkMulAmend,
			PvpAtkMulAmend = origin.PvpAtkMulAmend,
			ActPointLmt = origin.ActPointLmt,
			ActPointRecoverSpeedAmend = origin.ActPointRecoverSpeedAmend,
			VpLmt = origin.VpLmt,
			VpLmtMulAmend = origin.VpLmtMulAmend,
			VpAtk = origin.VpAtk,
			VpAtkMulAmend = origin.VpAtkMulAmend,
			VpResume = origin.VpResume,
			IdleVpResume = origin.IdleVpResume,
			WaterBuffAddProbAddAmend = origin.WaterBuffAddProbAddAmend,
			WaterBuffDurTimeAddAmend = origin.WaterBuffDurTimeAddAmend,
			ThunderBuffAddProbAddAmend = origin.ThunderBuffAddProbAddAmend,
			ThunderBuffDurTimeAddAmend = origin.ThunderBuffDurTimeAddAmend,
			HealIncreasePercent = origin.HealIncreasePercent,
			CritAddValue = origin.CritAddValue,
			HpRestore = origin.HpRestore,
			Energy = origin.Energy,
			EnergyLmt = origin.EnergyLmt,
			Exp = origin.Exp,
			ExpLmt = origin.ExpLmt,
			Diamond = origin.Diamond,
			Gold = origin.Gold,
			RechargeDiamond = origin.RechargeDiamond,
			Honor = origin.Honor,
			BuffMoveSpeedMulPosAmend = origin.BuffMoveSpeedMulPosAmend,
			BuffActSpeedMulPosAmend = origin.BuffActSpeedMulPosAmend,
			SkillTreatScaleBOAtk = origin.SkillTreatScaleBOAtk,
			SkillTreatScaleBOHpLmt = origin.SkillTreatScaleBOHpLmt,
			SkillIgnoreDefenceHurt = origin.SkillIgnoreDefenceHurt,
			SkillNmlDmgScale = origin.SkillNmlDmgScale,
			SkillNmlDmgAddAmend = origin.SkillNmlDmgAddAmend,
			SkillHolyDmgScaleBOMaxHp = origin.SkillHolyDmgScaleBOMaxHp,
			SkillHolyDmgScaleBOCurHp = origin.SkillHolyDmgScaleBOCurHp,
			Affinity = origin.Affinity,
			OnlineTime = origin.OnlineTime,
			ActPoint = origin.ActPoint,
			Hp = origin.Hp,
			Vp = origin.Vp
		};
	}

	public void AssignAllAttrs(StandardBaseAttrs origin)
	{
		this.MoveSpeed = origin.MoveSpeed;
		this.ActSpeed = origin.ActSpeed;
		this.Lv = origin.Lv;
		this.Fighting = origin.Fighting;
		this.VipLv = origin.VipLv;
		this.Atk = origin.Atk;
		this.Defence = origin.Defence;
		this.HpLmt = origin.HpLmt;
		this.PveAtk = origin.PveAtk;
		this.PvpAtk = origin.PvpAtk;
		this.HitRatio = origin.HitRatio;
		this.DodgeRatio = origin.DodgeRatio;
		this.CritRatio = origin.CritRatio;
		this.DecritRatio = origin.DecritRatio;
		this.CritHurtAddRatio = origin.CritHurtAddRatio;
		this.ParryRatio = origin.ParryRatio;
		this.DeparryRatio = origin.DeparryRatio;
		this.ParryHurtDeRatio = origin.ParryHurtDeRatio;
		this.SuckBloodScale = origin.SuckBloodScale;
		this.HurtAddRatio = origin.HurtAddRatio;
		this.HurtDeRatio = origin.HurtDeRatio;
		this.PveHurtAddRatio = origin.PveHurtAddRatio;
		this.PveHurtDeRatio = origin.PveHurtDeRatio;
		this.PvpHurtAddRatio = origin.PvpHurtAddRatio;
		this.PvpHurtDeRatio = origin.PvpHurtDeRatio;
		this.AtkMulAmend = origin.AtkMulAmend;
		this.DefMulAmend = origin.DefMulAmend;
		this.HpLmtMulAmend = origin.HpLmtMulAmend;
		this.PveAtkMulAmend = origin.PveAtkMulAmend;
		this.PvpAtkMulAmend = origin.PvpAtkMulAmend;
		this.ActPointLmt = origin.ActPointLmt;
		this.ActPointRecoverSpeedAmend = origin.ActPointRecoverSpeedAmend;
		this.VpLmt = origin.VpLmt;
		this.VpLmtMulAmend = origin.VpLmtMulAmend;
		this.VpAtk = origin.VpAtk;
		this.VpAtkMulAmend = origin.VpAtkMulAmend;
		this.VpResume = origin.VpResume;
		this.IdleVpResume = origin.IdleVpResume;
		this.WaterBuffAddProbAddAmend = origin.WaterBuffAddProbAddAmend;
		this.WaterBuffDurTimeAddAmend = origin.WaterBuffDurTimeAddAmend;
		this.ThunderBuffAddProbAddAmend = origin.ThunderBuffAddProbAddAmend;
		this.ThunderBuffDurTimeAddAmend = origin.ThunderBuffDurTimeAddAmend;
		this.HealIncreasePercent = origin.HealIncreasePercent;
		this.CritAddValue = origin.CritAddValue;
		this.HpRestore = origin.HpRestore;
		this.Energy = origin.Energy;
		this.EnergyLmt = origin.EnergyLmt;
		this.Exp = origin.Exp;
		this.ExpLmt = origin.ExpLmt;
		this.Diamond = origin.Diamond;
		this.Gold = origin.Gold;
		this.RechargeDiamond = origin.RechargeDiamond;
		this.Honor = origin.Honor;
		this.BuffMoveSpeedMulPosAmend = origin.BuffMoveSpeedMulPosAmend;
		this.BuffActSpeedMulPosAmend = origin.BuffActSpeedMulPosAmend;
		this.SkillTreatScaleBOAtk = origin.SkillTreatScaleBOAtk;
		this.SkillTreatScaleBOHpLmt = origin.SkillTreatScaleBOHpLmt;
		this.SkillIgnoreDefenceHurt = origin.SkillIgnoreDefenceHurt;
		this.SkillNmlDmgScale = origin.SkillNmlDmgScale;
		this.SkillNmlDmgAddAmend = origin.SkillNmlDmgAddAmend;
		this.SkillHolyDmgScaleBOMaxHp = origin.SkillHolyDmgScaleBOMaxHp;
		this.SkillHolyDmgScaleBOCurHp = origin.SkillHolyDmgScaleBOCurHp;
		this.Affinity = origin.Affinity;
		this.OnlineTime = origin.OnlineTime;
		this.ActPoint = origin.ActPoint;
		this.Hp = origin.Hp;
		this.Vp = origin.Vp;
	}

	public void ResetAllAttrs()
	{
		this.moveSpeed.Val = 1000;
		this.actSpeed.Val = 1000;
		this.lv.Val = 1;
		this.fighting.Val = 0L;
		this.vipLv.Val = 0;
		this.atk.Val = 0;
		this.defence.Val = 0;
		this.hpLmt.Val = 0L;
		this.pveAtk.Val = 0;
		this.pvpAtk.Val = 0;
		this.hitRatio.Val = 1;
		this.dodgeRatio.Val = 0;
		this.critRatio.Val = 0;
		this.decritRatio.Val = 0;
		this.critHurtAddRatio.Val = 0;
		this.parryRatio.Val = 0;
		this.deparryRatio.Val = 0;
		this.parryHurtDeRatio.Val = 0;
		this.suckBloodScale.Val = 0;
		this.hurtAddRatio.Val = 0;
		this.hurtDeRatio.Val = 0;
		this.pveHurtAddRatio.Val = 0;
		this.pveHurtDeRatio.Val = 0;
		this.pvpHurtAddRatio.Val = 0;
		this.pvpHurtDeRatio.Val = 0;
		this.atkMulAmend.Val = 0;
		this.defMulAmend.Val = 0;
		this.hpLmtMulAmend.Val = 0;
		this.pveAtkMulAmend.Val = 0;
		this.pvpAtkMulAmend.Val = 0;
		this.actPointLmt.Val = 0;
		this.actPointRecoverSpeedAmend.Val = 1;
		this.vpLmt.Val = 0;
		this.vpLmtMulAmend.Val = 0;
		this.vpAtk.Val = 0;
		this.vpAtkMulAmend.Val = 0;
		this.vpResume.Val = 0;
		this.idleVpResume.Val = 0;
		this.waterBuffAddProbAddAmend.Val = 0;
		this.waterBuffDurTimeAddAmend.Val = 0;
		this.thunderBuffAddProbAddAmend.Val = 0;
		this.thunderBuffDurTimeAddAmend.Val = 0;
		this.healIncreasePercent.Val = 0;
		this.critAddValue.Val = 0;
		this.hpRestore.Val = 0;
		this.energy.Val = 0;
		this.energyLmt.Val = 0;
		this.exp.Val = 0L;
		this.expLmt.Val = 0L;
		this.diamond.Val = 0;
		this.gold.Val = 0L;
		this.rechargeDiamond.Val = 0;
		this.honor.Val = 0;
		this.buffMoveSpeedMulPosAmend.Val = 0;
		this.buffActSpeedMulPosAmend.Val = 0;
		this.skillTreatScaleBOAtk.Val = 0;
		this.skillTreatScaleBOHpLmt.Val = 0;
		this.skillIgnoreDefenceHurt.Val = 0;
		this.skillNmlDmgScale.Val = 0;
		this.skillNmlDmgAddAmend.Val = 0;
		this.skillHolyDmgScaleBOMaxHp.Val = 0;
		this.skillHolyDmgScaleBOCurHp.Val = 0;
		this.affinity.Val = 0;
		this.onlineTime.Val = 0;
		this.actPoint.Val = 0;
		this.hp.Val = 0L;
		this.vp.Val = 0;
		this.realHpLmt.Val = 1L;
		this.realVpLmt.Val = 0;
		this.realMoveSpeed.Val = 1000;
		this.realActionSpeed.Val = 1000;
	}

	public override long GetValue(AttrType type)
	{
		switch (type)
		{
		case AttrType.PveAtk:
			return (long)this.pveAtk.Val;
		case AttrType.PvpAtk:
			return (long)this.pvpAtk.Val;
		case AttrType.HitRatio:
			return (long)this.hitRatio.Val;
		case AttrType.DodgeRatio:
			return (long)this.dodgeRatio.Val;
		case AttrType.CritRatio:
			return (long)this.critRatio.Val;
		case AttrType.DecritRatio:
			return (long)this.decritRatio.Val;
		case AttrType.CritHurtAddRatio:
			return (long)this.critHurtAddRatio.Val;
		case AttrType.ParryRatio:
			return (long)this.parryRatio.Val;
		case AttrType.DeparryRatio:
			return (long)this.deparryRatio.Val;
		case AttrType.ParryHurtDeRatio:
			return (long)this.parryHurtDeRatio.Val;
		case (AttrType)1314:
		case (AttrType)1321:
		case (AttrType)1322:
		case (AttrType)1327:
		case (AttrType)1328:
		case AttrType.ExpAddRate:
			IL_A6:
			switch (type)
			{
			case AttrType.SkillNmlDmgScale:
				return (long)this.skillNmlDmgScale.Val;
			case AttrType.SkillNmlDmgAddAmend:
				return (long)this.skillNmlDmgAddAmend.Val;
			case (AttrType)503:
			case (AttrType)504:
			case (AttrType)505:
			case (AttrType)506:
			case (AttrType)507:
			case (AttrType)508:
			case (AttrType)509:
			case (AttrType)510:
				IL_F2:
				switch (type)
				{
				case AttrType.Lv:
					return (long)this.lv.Val;
				case AttrType.Exp:
					return this.exp.Val;
				case AttrType.ExpLmt:
					return this.expLmt.Val;
				case AttrType.Energy:
					return (long)this.energy.Val;
				case AttrType.EnergyLmt:
					return (long)this.energyLmt.Val;
				case AttrType.Hp:
					return this.hp.Val;
				case AttrType.Fighting:
					return this.fighting.Val;
				case AttrType.Diamond:
					return (long)this.diamond.Val;
				case AttrType.Gold:
					return this.gold.Val;
				case AttrType.VipLv:
					return (long)this.vipLv.Val;
				case AttrType.RechargeDiamond:
					return (long)this.rechargeDiamond.Val;
				case AttrType.Honor:
					return (long)this.honor.Val;
				case AttrType.CompetitiveCurrency:
					return (long)this.competitiveCurrency.Val;
				case (AttrType)1014:
					IL_13A:
					switch (type)
					{
					case AttrType.MoveSpeed:
						return (long)this.moveSpeed.Val;
					case AttrType.ActSpeed:
						return (long)this.actSpeed.Val;
					case AttrType.Affinity:
						return (long)this.affinity.Val;
					case (AttrType)104:
					case (AttrType)105:
						IL_163:
						switch (type)
						{
						case AttrType.WaterBuffAddProbAddAmend:
							return (long)this.waterBuffAddProbAddAmend.Val;
						case (AttrType)1222:
						case (AttrType)1223:
							IL_17F:
							switch (type)
							{
							case AttrType.ThunderBuffAddProbAddAmend:
								return (long)this.thunderBuffAddProbAddAmend.Val;
							case (AttrType)1232:
							case (AttrType)1233:
								IL_19B:
								switch (type)
								{
								case AttrType.RealHpLmt:
									return this.realHpLmt.Val;
								case AttrType.RealVpLmt:
									return (long)this.realVpLmt.Val;
								case AttrType.RealMoveSpeed:
									return (long)this.realMoveSpeed.Val;
								case AttrType.RealActionSpeed:
									return (long)this.realActionSpeed.Val;
								default:
									if (type == AttrType.Atk)
									{
										return (long)this.atk.Val;
									}
									if (type == AttrType.AtkMulAmend)
									{
										return (long)this.atkMulAmend.Val;
									}
									if (type == AttrType.HpLmt)
									{
										return this.hpLmt.Val;
									}
									if (type == AttrType.Defence)
									{
										return (long)this.defence.Val;
									}
									if (type != AttrType.BuffMoveSpeedMulPosAmend)
									{
										Debug.LogError("未找到属性值:" + type);
										return 0L;
									}
									return (long)this.buffMoveSpeedMulPosAmend.Val;
								}
								break;
							case AttrType.ThunderBuffDurTimeAddAmend:
								return (long)this.thunderBuffDurTimeAddAmend.Val;
							}
							goto IL_19B;
						case AttrType.WaterBuffDurTimeAddAmend:
							return (long)this.waterBuffDurTimeAddAmend.Val;
						}
						goto IL_17F;
					case AttrType.ActPoint:
						return (long)this.actPoint.Val;
					case AttrType.ActPointLmt:
						return (long)this.actPointLmt.Val;
					case AttrType.ActPointRecoverSpeedAmend:
						return (long)this.actPointRecoverSpeedAmend.Val;
					}
					goto IL_163;
				case AttrType.SkillPoint:
					return (long)this.skillPoint.Val;
				}
				goto IL_13A;
			case AttrType.SkillHolyDmgScaleBOMaxHp:
				return (long)this.skillHolyDmgScaleBOMaxHp.Val;
			case AttrType.SkillHolyDmgScaleBOCurHp:
				return (long)this.skillHolyDmgScaleBOCurHp.Val;
			case AttrType.SuckBloodScale:
				return (long)this.suckBloodScale.Val;
			case AttrType.SkillTreatScaleBOAtk:
				return (long)this.skillTreatScaleBOAtk.Val;
			case AttrType.SkillTreatScaleBOHpLmt:
				return (long)this.skillTreatScaleBOHpLmt.Val;
			case AttrType.SkillIgnoreDefenceHurt:
				return (long)this.skillIgnoreDefenceHurt.Val;
			}
			goto IL_F2;
		case AttrType.HurtAddRatio:
			return (long)this.hurtAddRatio.Val;
		case AttrType.HurtDeRatio:
			return (long)this.hurtDeRatio.Val;
		case AttrType.PveHurtAddRatio:
			return (long)this.pveHurtAddRatio.Val;
		case AttrType.PveHurtDeRatio:
			return (long)this.pveHurtDeRatio.Val;
		case AttrType.PvpHurtAddRatio:
			return (long)this.pvpHurtAddRatio.Val;
		case AttrType.PvpHurtDeRatio:
			return (long)this.pvpHurtDeRatio.Val;
		case AttrType.DefMulAmend:
			return (long)this.defMulAmend.Val;
		case AttrType.HpLmtMulAmend:
			return (long)this.hpLmtMulAmend.Val;
		case AttrType.PveAtkMulAmend:
			return (long)this.pveAtkMulAmend.Val;
		case AttrType.PvpAtkMulAmend:
			return (long)this.pvpAtkMulAmend.Val;
		case AttrType.OnlineTime:
			return (long)this.onlineTime.Val;
		case AttrType.VpLmt:
			return (long)this.vpLmt.Val;
		case AttrType.VpLmtMulAmend:
			return (long)this.vpLmtMulAmend.Val;
		case AttrType.VpResume:
			return (long)this.vpResume.Val;
		case AttrType.VpAtk:
			return (long)this.vpAtk.Val;
		case AttrType.VpAtkMulAmend:
			return (long)this.vpAtkMulAmend.Val;
		case AttrType.Vp:
			return (long)this.vp.Val;
		case AttrType.IdleVpResume:
			return (long)this.idleVpResume.Val;
		case AttrType.HealIncreasePercent:
			return (long)this.healIncreasePercent.Val;
		case AttrType.CritAddValue:
			return (long)this.critAddValue.Val;
		case AttrType.HpRestore:
			return (long)this.hpRestore.Val;
		case AttrType.Reputation:
			return (long)this.reputation.Val;
		}
		goto IL_A6;
	}

	protected override CfgFormula<T> GetCfgFormula<T>(AttrType type)
	{
		switch (type)
		{
		case AttrType.PveAtk:
			return this.pveAtk as CfgFormula<T>;
		case AttrType.PvpAtk:
			return this.pvpAtk as CfgFormula<T>;
		case AttrType.HitRatio:
			return this.hitRatio as CfgFormula<T>;
		case AttrType.DodgeRatio:
			return this.dodgeRatio as CfgFormula<T>;
		case AttrType.CritRatio:
			return this.critRatio as CfgFormula<T>;
		case AttrType.DecritRatio:
			return this.decritRatio as CfgFormula<T>;
		case AttrType.CritHurtAddRatio:
			return this.critHurtAddRatio as CfgFormula<T>;
		case AttrType.ParryRatio:
			return this.parryRatio as CfgFormula<T>;
		case AttrType.DeparryRatio:
			return this.deparryRatio as CfgFormula<T>;
		case AttrType.ParryHurtDeRatio:
			return this.parryHurtDeRatio as CfgFormula<T>;
		case (AttrType)1314:
		case (AttrType)1321:
		case (AttrType)1322:
		case (AttrType)1327:
		case (AttrType)1328:
		case AttrType.ExpAddRate:
			IL_A6:
			switch (type)
			{
			case AttrType.SkillNmlDmgScale:
				return this.skillNmlDmgScale as CfgFormula<T>;
			case AttrType.SkillNmlDmgAddAmend:
				return this.skillNmlDmgAddAmend as CfgFormula<T>;
			case (AttrType)503:
			case (AttrType)504:
			case (AttrType)505:
			case (AttrType)506:
			case (AttrType)507:
			case (AttrType)508:
			case (AttrType)509:
			case (AttrType)510:
				IL_F2:
				switch (type)
				{
				case AttrType.Lv:
					return this.lv as CfgFormula<T>;
				case AttrType.Exp:
					return this.exp as CfgFormula<T>;
				case AttrType.ExpLmt:
					return this.expLmt as CfgFormula<T>;
				case AttrType.Energy:
					return this.energy as CfgFormula<T>;
				case AttrType.EnergyLmt:
					return this.energyLmt as CfgFormula<T>;
				case AttrType.Hp:
					return this.hp as CfgFormula<T>;
				case AttrType.Fighting:
					return this.fighting as CfgFormula<T>;
				case AttrType.Diamond:
					return this.diamond as CfgFormula<T>;
				case AttrType.Gold:
					return this.gold as CfgFormula<T>;
				case AttrType.VipLv:
					return this.vipLv as CfgFormula<T>;
				case AttrType.RechargeDiamond:
					return this.rechargeDiamond as CfgFormula<T>;
				case AttrType.Honor:
					return this.honor as CfgFormula<T>;
				case AttrType.CompetitiveCurrency:
					return this.competitiveCurrency as CfgFormula<T>;
				case (AttrType)1014:
					IL_13A:
					switch (type)
					{
					case AttrType.MoveSpeed:
						return this.moveSpeed as CfgFormula<T>;
					case AttrType.ActSpeed:
						return this.actSpeed as CfgFormula<T>;
					case AttrType.Affinity:
						return this.affinity as CfgFormula<T>;
					case (AttrType)104:
					case (AttrType)105:
						IL_163:
						switch (type)
						{
						case AttrType.WaterBuffAddProbAddAmend:
							return this.waterBuffAddProbAddAmend as CfgFormula<T>;
						case (AttrType)1222:
						case (AttrType)1223:
							IL_17F:
							switch (type)
							{
							case AttrType.ThunderBuffAddProbAddAmend:
								return this.thunderBuffAddProbAddAmend as CfgFormula<T>;
							case (AttrType)1232:
							case (AttrType)1233:
								IL_19B:
								switch (type)
								{
								case AttrType.RealHpLmt:
									return this.realHpLmt as CfgFormula<T>;
								case AttrType.RealVpLmt:
									return this.realVpLmt as CfgFormula<T>;
								case AttrType.RealMoveSpeed:
									return this.realMoveSpeed as CfgFormula<T>;
								case AttrType.RealActionSpeed:
									return this.realActionSpeed as CfgFormula<T>;
								default:
									switch (type)
									{
									case AttrType.BuffMoveSpeedMulPosAmend:
										return this.buffMoveSpeedMulPosAmend as CfgFormula<T>;
									case (AttrType)708:
										IL_1CF:
										if (type == AttrType.Atk)
										{
											return this.atk as CfgFormula<T>;
										}
										if (type == AttrType.AtkMulAmend)
										{
											return this.atkMulAmend as CfgFormula<T>;
										}
										if (type == AttrType.HpLmt)
										{
											return this.hpLmt as CfgFormula<T>;
										}
										if (type != AttrType.Defence)
										{
											Debug.LogError("未找到属性值:" + type);
											return null;
										}
										return this.defence as CfgFormula<T>;
									case AttrType.BuffActSpeedMulPosAmend:
										return this.buffActSpeedMulPosAmend as CfgFormula<T>;
									}
									goto IL_1CF;
								}
								break;
							case AttrType.ThunderBuffDurTimeAddAmend:
								return this.thunderBuffDurTimeAddAmend as CfgFormula<T>;
							}
							goto IL_19B;
						case AttrType.WaterBuffDurTimeAddAmend:
							return this.waterBuffDurTimeAddAmend as CfgFormula<T>;
						}
						goto IL_17F;
					case AttrType.ActPoint:
						return this.actPoint as CfgFormula<T>;
					case AttrType.ActPointLmt:
						return this.actPointLmt as CfgFormula<T>;
					case AttrType.ActPointRecoverSpeedAmend:
						return this.actPointRecoverSpeedAmend as CfgFormula<T>;
					}
					goto IL_163;
				case AttrType.SkillPoint:
					return this.skillPoint as CfgFormula<T>;
				}
				goto IL_13A;
			case AttrType.SkillHolyDmgScaleBOMaxHp:
				return this.skillHolyDmgScaleBOMaxHp as CfgFormula<T>;
			case AttrType.SkillHolyDmgScaleBOCurHp:
				return this.skillHolyDmgScaleBOCurHp as CfgFormula<T>;
			case AttrType.SuckBloodScale:
				return this.suckBloodScale as CfgFormula<T>;
			case AttrType.SkillTreatScaleBOAtk:
				return this.skillTreatScaleBOAtk as CfgFormula<T>;
			case AttrType.SkillTreatScaleBOHpLmt:
				return this.skillTreatScaleBOHpLmt as CfgFormula<T>;
			case AttrType.SkillIgnoreDefenceHurt:
				return this.skillIgnoreDefenceHurt as CfgFormula<T>;
			}
			goto IL_F2;
		case AttrType.HurtAddRatio:
			return this.hurtAddRatio as CfgFormula<T>;
		case AttrType.HurtDeRatio:
			return this.hurtDeRatio as CfgFormula<T>;
		case AttrType.PveHurtAddRatio:
			return this.pveHurtAddRatio as CfgFormula<T>;
		case AttrType.PveHurtDeRatio:
			return this.pveHurtDeRatio as CfgFormula<T>;
		case AttrType.PvpHurtAddRatio:
			return this.pvpHurtAddRatio as CfgFormula<T>;
		case AttrType.PvpHurtDeRatio:
			return this.pvpHurtDeRatio as CfgFormula<T>;
		case AttrType.DefMulAmend:
			return this.defMulAmend as CfgFormula<T>;
		case AttrType.HpLmtMulAmend:
			return this.hpLmtMulAmend as CfgFormula<T>;
		case AttrType.PveAtkMulAmend:
			return this.pveAtkMulAmend as CfgFormula<T>;
		case AttrType.PvpAtkMulAmend:
			return this.pvpAtkMulAmend as CfgFormula<T>;
		case AttrType.OnlineTime:
			return this.onlineTime as CfgFormula<T>;
		case AttrType.VpLmt:
			return this.vpLmt as CfgFormula<T>;
		case AttrType.VpLmtMulAmend:
			return this.vpLmtMulAmend as CfgFormula<T>;
		case AttrType.VpResume:
			return this.vpResume as CfgFormula<T>;
		case AttrType.VpAtk:
			return this.vpAtk as CfgFormula<T>;
		case AttrType.VpAtkMulAmend:
			return this.vpAtkMulAmend as CfgFormula<T>;
		case AttrType.Vp:
			return this.vp as CfgFormula<T>;
		case AttrType.IdleVpResume:
			return this.idleVpResume as CfgFormula<T>;
		case AttrType.HealIncreasePercent:
			return this.healIncreasePercent as CfgFormula<T>;
		case AttrType.CritAddValue:
			return this.critAddValue as CfgFormula<T>;
		case AttrType.HpRestore:
			return this.hpRestore as CfgFormula<T>;
		case AttrType.Reputation:
			return this.reputation as CfgFormula<T>;
		}
		goto IL_A6;
	}

	public override BuffCtrlAttrs GetBuffCtrlAttrs(int elementType)
	{
		BuffCtrlAttrs buffCtrlAttrs = new BuffCtrlAttrs(elementType);
		if (elementType != 1)
		{
			if (elementType == 2)
			{
				buffCtrlAttrs.AddProbAddAmend = this.ThunderBuffAddProbAddAmend;
				buffCtrlAttrs.DurTimeAddAmend = this.ThunderBuffDurTimeAddAmend;
			}
		}
		else
		{
			buffCtrlAttrs.AddProbAddAmend = this.WaterBuffAddProbAddAmend;
			buffCtrlAttrs.DurTimeAddAmend = this.WaterBuffDurTimeAddAmend;
		}
		return buffCtrlAttrs;
	}

	public override void SetBuffCtrlAttrs(BuffCtrlAttrs attrs)
	{
		int elemType = attrs.ElemType;
		if (elemType != 1)
		{
			if (elemType == 2)
			{
				this.ThunderBuffAddProbAddAmend = attrs.AddProbAddAmend;
				this.ThunderBuffDurTimeAddAmend = attrs.DurTimeAddAmend;
			}
		}
		else
		{
			this.WaterBuffAddProbAddAmend = attrs.AddProbAddAmend;
			this.WaterBuffDurTimeAddAmend = attrs.DurTimeAddAmend;
		}
	}

	public override long TryAddValue(AttrType type, long tryAddValue)
	{
		switch (type)
		{
		case AttrType.PveAtk:
			return (long)this.pveAtk.TryAddValue((int)tryAddValue);
		case AttrType.PvpAtk:
			return (long)this.pvpAtk.TryAddValue((int)tryAddValue);
		case AttrType.HitRatio:
			return (long)this.hitRatio.TryAddValue((int)tryAddValue);
		case AttrType.DodgeRatio:
			return (long)this.dodgeRatio.TryAddValue((int)tryAddValue);
		case AttrType.CritRatio:
			return (long)this.critRatio.TryAddValue((int)tryAddValue);
		case AttrType.DecritRatio:
			return (long)this.decritRatio.TryAddValue((int)tryAddValue);
		case AttrType.CritHurtAddRatio:
			return (long)this.critHurtAddRatio.TryAddValue((int)tryAddValue);
		case AttrType.ParryRatio:
			return (long)this.parryRatio.TryAddValue((int)tryAddValue);
		case AttrType.DeparryRatio:
			return (long)this.deparryRatio.TryAddValue((int)tryAddValue);
		case AttrType.ParryHurtDeRatio:
			return (long)this.parryHurtDeRatio.TryAddValue((int)tryAddValue);
		case (AttrType)1314:
		case (AttrType)1321:
		case (AttrType)1322:
		case (AttrType)1327:
		case (AttrType)1328:
		case AttrType.ExpAddRate:
			IL_A6:
			switch (type)
			{
			case AttrType.SkillNmlDmgScale:
				return (long)this.skillNmlDmgScale.TryAddValue((int)tryAddValue);
			case AttrType.SkillNmlDmgAddAmend:
				return (long)this.skillNmlDmgAddAmend.TryAddValue((int)tryAddValue);
			case (AttrType)503:
			case (AttrType)504:
			case (AttrType)505:
			case (AttrType)506:
			case (AttrType)507:
			case (AttrType)508:
			case (AttrType)509:
			case (AttrType)510:
				IL_F2:
				switch (type)
				{
				case AttrType.Lv:
					return (long)this.lv.TryAddValue((int)tryAddValue);
				case AttrType.Exp:
					return this.exp.TryAddValue(tryAddValue);
				case AttrType.ExpLmt:
					return this.expLmt.TryAddValue(tryAddValue);
				case AttrType.Energy:
					return (long)this.energy.TryAddValue((int)tryAddValue);
				case AttrType.EnergyLmt:
					return (long)this.energyLmt.TryAddValue((int)tryAddValue);
				case AttrType.Hp:
					return this.hp.TryAddValue(tryAddValue);
				case AttrType.Fighting:
					return this.fighting.TryAddValue(tryAddValue);
				case AttrType.Diamond:
					return (long)this.diamond.TryAddValue((int)tryAddValue);
				case AttrType.Gold:
					return this.gold.TryAddValue(tryAddValue);
				case AttrType.VipLv:
					return (long)this.vipLv.TryAddValue((int)tryAddValue);
				case AttrType.RechargeDiamond:
					return (long)this.rechargeDiamond.TryAddValue((int)tryAddValue);
				case AttrType.Honor:
					return (long)this.honor.TryAddValue((int)tryAddValue);
				case AttrType.CompetitiveCurrency:
					return (long)this.competitiveCurrency.TryAddValue((int)tryAddValue);
				case (AttrType)1014:
					IL_13A:
					switch (type)
					{
					case AttrType.MoveSpeed:
						return (long)this.moveSpeed.TryAddValue((int)tryAddValue);
					case AttrType.ActSpeed:
						return (long)this.actSpeed.TryAddValue((int)tryAddValue);
					case AttrType.Affinity:
						return (long)this.affinity.TryAddValue((int)tryAddValue);
					case (AttrType)104:
					case (AttrType)105:
						IL_163:
						switch (type)
						{
						case AttrType.WaterBuffAddProbAddAmend:
							return (long)this.waterBuffAddProbAddAmend.TryAddValue((int)tryAddValue);
						case (AttrType)1222:
						case (AttrType)1223:
							IL_17F:
							switch (type)
							{
							case AttrType.ThunderBuffAddProbAddAmend:
								return (long)this.thunderBuffAddProbAddAmend.TryAddValue((int)tryAddValue);
							case (AttrType)1232:
							case (AttrType)1233:
								IL_19B:
								switch (type)
								{
								case AttrType.RealHpLmt:
									return this.realHpLmt.TryAddValue(tryAddValue);
								case AttrType.RealVpLmt:
									return (long)this.realVpLmt.TryAddValue((int)tryAddValue);
								case AttrType.RealMoveSpeed:
									return (long)this.realMoveSpeed.TryAddValue((int)tryAddValue);
								case AttrType.RealActionSpeed:
									return (long)this.realActionSpeed.TryAddValue((int)tryAddValue);
								default:
									if (type == AttrType.Atk)
									{
										return (long)this.atk.TryAddValue((int)tryAddValue);
									}
									if (type == AttrType.AtkMulAmend)
									{
										return (long)this.atkMulAmend.TryAddValue((int)tryAddValue);
									}
									if (type == AttrType.HpLmt)
									{
										return this.hpLmt.TryAddValue(tryAddValue);
									}
									if (type == AttrType.Defence)
									{
										return (long)this.defence.TryAddValue((int)tryAddValue);
									}
									if (type != AttrType.BuffMoveSpeedMulPosAmend)
									{
										return 0L;
									}
									return (long)this.buffMoveSpeedMulPosAmend.TryAddValue((int)tryAddValue);
								}
								break;
							case AttrType.ThunderBuffDurTimeAddAmend:
								return (long)this.thunderBuffDurTimeAddAmend.TryAddValue((int)tryAddValue);
							}
							goto IL_19B;
						case AttrType.WaterBuffDurTimeAddAmend:
							return (long)this.waterBuffDurTimeAddAmend.TryAddValue((int)tryAddValue);
						}
						goto IL_17F;
					case AttrType.ActPoint:
						return (long)this.actPoint.TryAddValue((int)tryAddValue);
					case AttrType.ActPointLmt:
						return (long)this.actPointLmt.TryAddValue((int)tryAddValue);
					case AttrType.ActPointRecoverSpeedAmend:
						return (long)this.actPointRecoverSpeedAmend.TryAddValue((int)tryAddValue);
					}
					goto IL_163;
				case AttrType.SkillPoint:
					return (long)this.skillPoint.TryAddValue((int)tryAddValue);
				}
				goto IL_13A;
			case AttrType.SkillHolyDmgScaleBOMaxHp:
				return (long)this.skillHolyDmgScaleBOMaxHp.TryAddValue((int)tryAddValue);
			case AttrType.SkillHolyDmgScaleBOCurHp:
				return (long)this.skillHolyDmgScaleBOCurHp.TryAddValue((int)tryAddValue);
			case AttrType.SuckBloodScale:
				return (long)this.suckBloodScale.TryAddValue((int)tryAddValue);
			case AttrType.SkillTreatScaleBOAtk:
				return (long)this.skillTreatScaleBOAtk.TryAddValue((int)tryAddValue);
			case AttrType.SkillTreatScaleBOHpLmt:
				return (long)this.skillTreatScaleBOHpLmt.TryAddValue((int)tryAddValue);
			case AttrType.SkillIgnoreDefenceHurt:
				return (long)this.skillIgnoreDefenceHurt.TryAddValue((int)tryAddValue);
			}
			goto IL_F2;
		case AttrType.HurtAddRatio:
			return (long)this.hurtAddRatio.TryAddValue((int)tryAddValue);
		case AttrType.HurtDeRatio:
			return (long)this.hurtDeRatio.TryAddValue((int)tryAddValue);
		case AttrType.PveHurtAddRatio:
			return (long)this.pveHurtAddRatio.TryAddValue((int)tryAddValue);
		case AttrType.PveHurtDeRatio:
			return (long)this.pveHurtDeRatio.TryAddValue((int)tryAddValue);
		case AttrType.PvpHurtAddRatio:
			return (long)this.pvpHurtAddRatio.TryAddValue((int)tryAddValue);
		case AttrType.PvpHurtDeRatio:
			return (long)this.pvpHurtDeRatio.TryAddValue((int)tryAddValue);
		case AttrType.DefMulAmend:
			return (long)this.defMulAmend.TryAddValue((int)tryAddValue);
		case AttrType.HpLmtMulAmend:
			return (long)this.hpLmtMulAmend.TryAddValue((int)tryAddValue);
		case AttrType.PveAtkMulAmend:
			return (long)this.pveAtkMulAmend.TryAddValue((int)tryAddValue);
		case AttrType.PvpAtkMulAmend:
			return (long)this.pvpAtkMulAmend.TryAddValue((int)tryAddValue);
		case AttrType.OnlineTime:
			return (long)this.onlineTime.TryAddValue((int)tryAddValue);
		case AttrType.VpLmt:
			return (long)this.vpLmt.TryAddValue((int)tryAddValue);
		case AttrType.VpLmtMulAmend:
			return (long)this.vpLmtMulAmend.TryAddValue((int)tryAddValue);
		case AttrType.VpResume:
			return (long)this.vpResume.TryAddValue((int)tryAddValue);
		case AttrType.VpAtk:
			return (long)this.vpAtk.TryAddValue((int)tryAddValue);
		case AttrType.VpAtkMulAmend:
			return (long)this.vpAtkMulAmend.TryAddValue((int)tryAddValue);
		case AttrType.Vp:
			return (long)this.vp.TryAddValue((int)tryAddValue);
		case AttrType.IdleVpResume:
			return (long)this.idleVpResume.TryAddValue((int)tryAddValue);
		case AttrType.HealIncreasePercent:
			return (long)this.healIncreasePercent.TryAddValue((int)tryAddValue);
		case AttrType.CritAddValue:
			return (long)this.critAddValue.TryAddValue((int)tryAddValue);
		case AttrType.HpRestore:
			return (long)this.hpRestore.TryAddValue((int)tryAddValue);
		case AttrType.Reputation:
			return (long)this.reputation.TryAddValue((int)tryAddValue);
		}
		goto IL_A6;
	}
}
