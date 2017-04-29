using GameData;
using Package;
using System;
using UnityEngine;

public class BattleBaseAttrs : BattleBaseAttrExtend, IClientBaseAttr, IClientBattleBaseAttr, ISimpleBaseAttr, IDevelopBaseAttr, IBattleBaseAttr
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
			this.SetValue(GameData.AttrType.MoveSpeed, value, true);
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
			this.SetValue(GameData.AttrType.ActSpeed, value, true);
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
			this.SetValue(GameData.AttrType.Lv, value, true);
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
			this.SetValue(GameData.AttrType.Fighting, value, true);
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
			this.SetValue(GameData.AttrType.VipLv, value, true);
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
			this.SetValue(GameData.AttrType.Atk, value, true);
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
			this.SetValue(GameData.AttrType.Defence, value, true);
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
			this.SetValue(GameData.AttrType.HpLmt, value, true);
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
			this.SetValue(GameData.AttrType.PveAtk, value, true);
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
			this.SetValue(GameData.AttrType.PvpAtk, value, true);
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
			this.SetValue(GameData.AttrType.HitRatio, value, true);
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
			this.SetValue(GameData.AttrType.DodgeRatio, value, true);
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
			this.SetValue(GameData.AttrType.CritRatio, value, true);
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
			this.SetValue(GameData.AttrType.DecritRatio, value, true);
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
			this.SetValue(GameData.AttrType.CritHurtAddRatio, value, true);
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
			this.SetValue(GameData.AttrType.ParryRatio, value, true);
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
			this.SetValue(GameData.AttrType.DeparryRatio, value, true);
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
			this.SetValue(GameData.AttrType.ParryHurtDeRatio, value, true);
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
			this.SetValue(GameData.AttrType.SuckBloodScale, value, true);
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
			this.SetValue(GameData.AttrType.HurtAddRatio, value, true);
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
			this.SetValue(GameData.AttrType.HurtDeRatio, value, true);
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
			this.SetValue(GameData.AttrType.PveHurtAddRatio, value, true);
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
			this.SetValue(GameData.AttrType.PveHurtDeRatio, value, true);
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
			this.SetValue(GameData.AttrType.PvpHurtAddRatio, value, true);
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
			this.SetValue(GameData.AttrType.PvpHurtDeRatio, value, true);
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
			this.SetValue(GameData.AttrType.AtkMulAmend, value, true);
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
			this.SetValue(GameData.AttrType.DefMulAmend, value, true);
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
			this.SetValue(GameData.AttrType.HpLmtMulAmend, value, true);
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
			this.SetValue(GameData.AttrType.PveAtkMulAmend, value, true);
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
			this.SetValue(GameData.AttrType.PvpAtkMulAmend, value, true);
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
			this.SetValue(GameData.AttrType.ActPointRecoverSpeedAmend, value, true);
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
			this.SetValue(GameData.AttrType.VpLmt, value, true);
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
			this.SetValue(GameData.AttrType.VpLmtMulAmend, value, true);
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
			this.SetValue(GameData.AttrType.VpAtk, value, true);
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
			this.SetValue(GameData.AttrType.VpAtkMulAmend, value, true);
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
			this.SetValue(GameData.AttrType.VpResume, value, true);
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
			this.SetValue(GameData.AttrType.IdleVpResume, value, true);
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
			this.SetValue(GameData.AttrType.WaterBuffAddProbAddAmend, value, true);
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
			this.SetValue(GameData.AttrType.WaterBuffDurTimeAddAmend, value, true);
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
			this.SetValue(GameData.AttrType.ThunderBuffAddProbAddAmend, value, true);
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
			this.SetValue(GameData.AttrType.ThunderBuffDurTimeAddAmend, value, true);
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
			this.SetValue(GameData.AttrType.HealIncreasePercent, value, true);
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
			this.SetValue(GameData.AttrType.CritAddValue, value, true);
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
			this.SetValue(GameData.AttrType.HpRestore, value, true);
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
			this.SetValue(GameData.AttrType.ActPointLmt, value, true);
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
			this.SetValue(GameData.AttrType.BuffMoveSpeedMulPosAmend, value, true);
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
			this.SetValue(GameData.AttrType.BuffActSpeedMulPosAmend, value, true);
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
			this.SetValue(GameData.AttrType.SkillTreatScaleBOAtk, value, true);
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
			this.SetValue(GameData.AttrType.SkillTreatScaleBOHpLmt, value, true);
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
			this.SetValue(GameData.AttrType.SkillIgnoreDefenceHurt, value, true);
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
			this.SetValue(GameData.AttrType.SkillNmlDmgScale, value, true);
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
			this.SetValue(GameData.AttrType.SkillNmlDmgAddAmend, value, true);
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
			this.SetValue(GameData.AttrType.SkillHolyDmgScaleBOMaxHp, value, true);
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
			this.SetValue(GameData.AttrType.SkillHolyDmgScaleBOCurHp, value, true);
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
			this.SetValue(GameData.AttrType.Affinity, value, true);
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
			this.SetValue(GameData.AttrType.OnlineTime, value, true);
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
			this.SetValue(GameData.AttrType.ActPoint, value, true);
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
			this.SetValue(GameData.AttrType.Hp, value, true);
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
			this.SetValue(GameData.AttrType.Vp, value, true);
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
			this.SetValue(GameData.AttrType.RealHpLmt, (value <= 0L) ? 1L : value, true);
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
			this.SetValue(GameData.AttrType.RealVpLmt, (value <= 0) ? 1 : value, true);
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
			this.SetValue(GameData.AttrType.RealMoveSpeed, value, true);
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
			this.SetValue(GameData.AttrType.RealActionSpeed, value, true);
		}
	}

	public BattleBaseAttrs()
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
		this.hitRatio = CfgFormulaBuilder<int>.Build(CfgFormulaType.AddAccum, 1000, this.attrCoder);
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

	public static BattleBaseAttrs CopyAllAttrs(BattleBaseAttrs origin)
	{
		if (origin == null)
		{
			return null;
		}
		return new BattleBaseAttrs
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
			BuffMoveSpeedMulPosAmend = origin.BuffMoveSpeedMulPosAmend,
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

	public void AssignAllAttrs(CityBaseAttrs origin)
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
		this.ActPointLmt = 10;
		this.ActPointRecoverSpeedAmend = origin.ActPointRecoverSpeedAmend;
		this.VpLmt = origin.VpLmt;
		this.VpLmtMulAmend = origin.VpLmtMulAmend;
		this.VpAtk = origin.VpAtk;
		this.VpAtkMulAmend = origin.VpAtkMulAmend;
		this.VpResume = origin.VpResume;
		this.IdleVpResume = origin.IdleVpResume;
		this.WaterBuffAddProbAddAmend = 0;
		this.WaterBuffDurTimeAddAmend = 0;
		this.ThunderBuffAddProbAddAmend = 0;
		this.ThunderBuffDurTimeAddAmend = 0;
		this.HealIncreasePercent = origin.HealIncreasePercent;
		this.CritAddValue = origin.CritAddValue;
		this.HpRestore = origin.HpRestore;
		this.BuffMoveSpeedMulPosAmend = 0;
		this.BuffActSpeedMulPosAmend = 0;
		this.SkillTreatScaleBOAtk = 0;
		this.SkillTreatScaleBOHpLmt = 0;
		this.SkillIgnoreDefenceHurt = 0;
		this.SkillNmlDmgScale = 0;
		this.SkillNmlDmgAddAmend = 0;
		this.SkillHolyDmgScaleBOMaxHp = 0;
		this.SkillHolyDmgScaleBOCurHp = 0;
		this.Affinity = 0;
		this.OnlineTime = 0;
		this.ActPoint = 0;
		this.Hp = (long)((double)origin.HpLmt * (1.0 + (double)origin.HpLmtMulAmend * 0.001));
		this.Vp = (int)((double)origin.VpLmt * (1.0 + (double)origin.VpLmtMulAmend * 0.001));
	}

	public void AssignAllAttrs(BattleBaseAttrs origin)
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

	public void AssignAllAttrs(BattleBaseInfo origin)
	{
		this.MoveSpeed = origin.publicBaseInfo.simpleInfo.MoveSpeed;
		this.ActSpeed = origin.publicBaseInfo.simpleInfo.AtkSpeed;
		this.Lv = origin.publicBaseInfo.simpleInfo.Lv;
		this.Fighting = origin.publicBaseInfo.simpleInfo.Fighting;
		this.VipLv = origin.publicBaseInfo.simpleInfo.VipLv;
		this.Atk = origin.publicBaseInfo.Atk;
		this.Defence = origin.publicBaseInfo.Defence;
		this.HpLmt = origin.publicBaseInfo.HpLmt;
		this.PveAtk = origin.publicBaseInfo.PveAtk;
		this.PvpAtk = origin.publicBaseInfo.PvpAtk;
		this.HitRatio = origin.publicBaseInfo.HitRatio;
		this.DodgeRatio = origin.publicBaseInfo.DodgeRatio;
		this.CritRatio = origin.publicBaseInfo.CritRatio;
		this.DecritRatio = origin.publicBaseInfo.DecritRatio;
		this.CritHurtAddRatio = origin.publicBaseInfo.CritHurtAddRatio;
		this.ParryRatio = origin.publicBaseInfo.ParryRatio;
		this.DeparryRatio = origin.publicBaseInfo.DeparryRatio;
		this.ParryHurtDeRatio = origin.publicBaseInfo.ParryHurtDeRatio;
		this.SuckBloodScale = origin.publicBaseInfo.SuckBloodScale;
		this.HurtAddRatio = origin.publicBaseInfo.HurtAddRatio;
		this.HurtDeRatio = origin.publicBaseInfo.HurtDeRatio;
		this.PveHurtAddRatio = origin.publicBaseInfo.PveHurtAddRatio;
		this.PveHurtDeRatio = origin.publicBaseInfo.PveHurtDeRatio;
		this.PvpHurtAddRatio = origin.publicBaseInfo.PvpHurtAddRatio;
		this.PvpHurtDeRatio = origin.publicBaseInfo.PvpHurtDeRatio;
		this.AtkMulAmend = origin.publicBaseInfo.AtkMulAmend;
		this.DefMulAmend = origin.publicBaseInfo.DefMulAmend;
		this.HpLmtMulAmend = origin.publicBaseInfo.HpLmtMulAmend;
		this.PveAtkMulAmend = origin.publicBaseInfo.PveAtkMulAmend;
		this.PvpAtkMulAmend = origin.publicBaseInfo.PvpAtkMulAmend;
		this.ActPointLmt = origin.battleBaseAttr.ActPointLmt;
		this.ActPointRecoverSpeedAmend = origin.publicBaseInfo.ActPointRecoverSpeedAmend;
		this.VpLmt = origin.publicBaseInfo.VpLmt;
		this.VpLmtMulAmend = origin.publicBaseInfo.VpLmtMulAmend;
		this.VpAtk = origin.publicBaseInfo.VpAtk;
		this.VpAtkMulAmend = origin.publicBaseInfo.VpAtkMulAmend;
		this.VpResume = origin.publicBaseInfo.VpResume;
		this.IdleVpResume = origin.publicBaseInfo.IdleVpResume;
		this.WaterBuffAddProbAddAmend = origin.publicBaseInfo.WaterBuffAddProbAddAmend;
		this.WaterBuffDurTimeAddAmend = origin.publicBaseInfo.WaterBuffDurTimeAddAmend;
		this.ThunderBuffAddProbAddAmend = origin.publicBaseInfo.ThunderBuffAddProbAddAmend;
		this.ThunderBuffDurTimeAddAmend = origin.publicBaseInfo.ThunderBuffDurTimeAddAmend;
		this.HealIncreasePercent = origin.publicBaseInfo.HealIncreasePercent;
		this.CritAddValue = origin.publicBaseInfo.CritAddValue;
		this.HpRestore = origin.publicBaseInfo.HpRestore;
		this.BuffMoveSpeedMulPosAmend = origin.battleBaseAttr.BuffMoveSpeedMulPosAmend;
		this.BuffActSpeedMulPosAmend = origin.battleBaseAttr.BuffActSpeedMulPosAmend;
		this.SkillTreatScaleBOAtk = origin.battleBaseAttr.SkillTreatScaleBOAtk;
		this.SkillTreatScaleBOHpLmt = origin.battleBaseAttr.SkillTreatScaleBOHpLmt;
		this.SkillIgnoreDefenceHurt = origin.battleBaseAttr.SkillIgnoreDefenceHurt;
		this.SkillNmlDmgScale = origin.battleBaseAttr.SkillNmlDmgScale;
		this.SkillNmlDmgAddAmend = origin.battleBaseAttr.SkillNmlDmgAddAmend;
		this.SkillHolyDmgScaleBOMaxHp = origin.battleBaseAttr.SkillHolyDmgScaleBOMaxHp;
		this.SkillHolyDmgScaleBOCurHp = origin.battleBaseAttr.SkillHolyDmgScaleBOCurHp;
		this.Affinity = origin.battleBaseAttr.Affinity;
		this.OnlineTime = origin.battleBaseAttr.OnlineTime;
		this.ActPoint = origin.battleBaseAttr.ActPoint;
		this.Hp = origin.battleBaseAttr.Hp;
		this.Vp = origin.battleBaseAttr.Vp;
	}

	public void AssignAllAttrs(BackUpBattleBaseAttrs origin)
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

	public override long GetValue(GameData.AttrType type)
	{
		switch (type)
		{
		case GameData.AttrType.PveAtk:
			return (long)this.pveAtk.Val;
		case GameData.AttrType.PvpAtk:
			return (long)this.pvpAtk.Val;
		case GameData.AttrType.HitRatio:
			return (long)this.hitRatio.Val;
		case GameData.AttrType.DodgeRatio:
			return (long)this.dodgeRatio.Val;
		case GameData.AttrType.CritRatio:
			return (long)this.critRatio.Val;
		case GameData.AttrType.DecritRatio:
			return (long)this.decritRatio.Val;
		case GameData.AttrType.CritHurtAddRatio:
			return (long)this.critHurtAddRatio.Val;
		case GameData.AttrType.ParryRatio:
			return (long)this.parryRatio.Val;
		case GameData.AttrType.DeparryRatio:
			return (long)this.deparryRatio.Val;
		case GameData.AttrType.ParryHurtDeRatio:
			return (long)this.parryHurtDeRatio.Val;
		case (GameData.AttrType)1314:
		case (GameData.AttrType)1321:
		case (GameData.AttrType)1322:
		case (GameData.AttrType)1327:
		case (GameData.AttrType)1328:
		case GameData.AttrType.ExpAddRate:
			IL_A2:
			switch (type)
			{
			case GameData.AttrType.SkillNmlDmgScale:
				return (long)this.skillNmlDmgScale.Val;
			case GameData.AttrType.SkillNmlDmgAddAmend:
				return (long)this.skillNmlDmgAddAmend.Val;
			case (GameData.AttrType)503:
			case (GameData.AttrType)504:
			case (GameData.AttrType)505:
			case (GameData.AttrType)506:
			case (GameData.AttrType)507:
			case (GameData.AttrType)508:
			case (GameData.AttrType)509:
			case (GameData.AttrType)510:
				IL_EE:
				switch (type)
				{
				case GameData.AttrType.MoveSpeed:
					return (long)this.moveSpeed.Val;
				case GameData.AttrType.ActSpeed:
					return (long)this.actSpeed.Val;
				case GameData.AttrType.Affinity:
					return (long)this.affinity.Val;
				case (GameData.AttrType)104:
				case (GameData.AttrType)105:
					IL_117:
					switch (type)
					{
					case GameData.AttrType.Hp:
						return this.hp.Val;
					case GameData.AttrType.Fighting:
						return this.fighting.Val;
					case GameData.AttrType.Diamond:
					case GameData.AttrType.Gold:
						IL_137:
						switch (type)
						{
						case GameData.AttrType.WaterBuffAddProbAddAmend:
							return (long)this.waterBuffAddProbAddAmend.Val;
						case (GameData.AttrType)1222:
						case (GameData.AttrType)1223:
							IL_153:
							switch (type)
							{
							case GameData.AttrType.ThunderBuffAddProbAddAmend:
								return (long)this.thunderBuffAddProbAddAmend.Val;
							case (GameData.AttrType)1232:
							case (GameData.AttrType)1233:
								IL_16F:
								switch (type)
								{
								case GameData.AttrType.RealHpLmt:
									return this.realHpLmt.Val;
								case GameData.AttrType.RealVpLmt:
									return (long)this.realVpLmt.Val;
								case GameData.AttrType.RealMoveSpeed:
									return (long)this.realMoveSpeed.Val;
								case GameData.AttrType.RealActionSpeed:
									return (long)this.realActionSpeed.Val;
								default:
									switch (type)
									{
									case GameData.AttrType.BuffMoveSpeedMulPosAmend:
										return (long)this.buffMoveSpeedMulPosAmend.Val;
									case (GameData.AttrType)708:
										IL_1A3:
										if (type == GameData.AttrType.Atk)
										{
											return (long)this.atk.Val;
										}
										if (type == GameData.AttrType.AtkMulAmend)
										{
											return (long)this.atkMulAmend.Val;
										}
										if (type == GameData.AttrType.HpLmt)
										{
											return this.hpLmt.Val;
										}
										if (type == GameData.AttrType.Defence)
										{
											return (long)this.defence.Val;
										}
										if (type != GameData.AttrType.Lv)
										{
											Debug.LogError("未找到属性值:" + type);
											return 0L;
										}
										return (long)this.lv.Val;
									case GameData.AttrType.BuffActSpeedMulPosAmend:
										return (long)this.buffActSpeedMulPosAmend.Val;
									}
									goto IL_1A3;
								}
								break;
							case GameData.AttrType.ThunderBuffDurTimeAddAmend:
								return (long)this.thunderBuffDurTimeAddAmend.Val;
							}
							goto IL_16F;
						case GameData.AttrType.WaterBuffDurTimeAddAmend:
							return (long)this.waterBuffDurTimeAddAmend.Val;
						}
						goto IL_153;
					case GameData.AttrType.VipLv:
						return (long)this.vipLv.Val;
					}
					goto IL_137;
				case GameData.AttrType.ActPoint:
					return (long)this.actPoint.Val;
				case GameData.AttrType.ActPointLmt:
					return (long)this.actPointLmt.Val;
				case GameData.AttrType.ActPointRecoverSpeedAmend:
					return (long)this.actPointRecoverSpeedAmend.Val;
				}
				goto IL_117;
			case GameData.AttrType.SkillHolyDmgScaleBOMaxHp:
				return (long)this.skillHolyDmgScaleBOMaxHp.Val;
			case GameData.AttrType.SkillHolyDmgScaleBOCurHp:
				return (long)this.skillHolyDmgScaleBOCurHp.Val;
			case GameData.AttrType.SuckBloodScale:
				return (long)this.suckBloodScale.Val;
			case GameData.AttrType.SkillTreatScaleBOAtk:
				return (long)this.skillTreatScaleBOAtk.Val;
			case GameData.AttrType.SkillTreatScaleBOHpLmt:
				return (long)this.skillTreatScaleBOHpLmt.Val;
			case GameData.AttrType.SkillIgnoreDefenceHurt:
				return (long)this.skillIgnoreDefenceHurt.Val;
			}
			goto IL_EE;
		case GameData.AttrType.HurtAddRatio:
			return (long)this.hurtAddRatio.Val;
		case GameData.AttrType.HurtDeRatio:
			return (long)this.hurtDeRatio.Val;
		case GameData.AttrType.PveHurtAddRatio:
			return (long)this.pveHurtAddRatio.Val;
		case GameData.AttrType.PveHurtDeRatio:
			return (long)this.pveHurtDeRatio.Val;
		case GameData.AttrType.PvpHurtAddRatio:
			return (long)this.pvpHurtAddRatio.Val;
		case GameData.AttrType.PvpHurtDeRatio:
			return (long)this.pvpHurtDeRatio.Val;
		case GameData.AttrType.DefMulAmend:
			return (long)this.defMulAmend.Val;
		case GameData.AttrType.HpLmtMulAmend:
			return (long)this.hpLmtMulAmend.Val;
		case GameData.AttrType.PveAtkMulAmend:
			return (long)this.pveAtkMulAmend.Val;
		case GameData.AttrType.PvpAtkMulAmend:
			return (long)this.pvpAtkMulAmend.Val;
		case GameData.AttrType.OnlineTime:
			return (long)this.onlineTime.Val;
		case GameData.AttrType.VpLmt:
			return (long)this.vpLmt.Val;
		case GameData.AttrType.VpLmtMulAmend:
			return (long)this.vpLmtMulAmend.Val;
		case GameData.AttrType.VpResume:
			return (long)this.vpResume.Val;
		case GameData.AttrType.VpAtk:
			return (long)this.vpAtk.Val;
		case GameData.AttrType.VpAtkMulAmend:
			return (long)this.vpAtkMulAmend.Val;
		case GameData.AttrType.Vp:
			return (long)this.vp.Val;
		case GameData.AttrType.IdleVpResume:
			return (long)this.idleVpResume.Val;
		case GameData.AttrType.HealIncreasePercent:
			return (long)this.healIncreasePercent.Val;
		case GameData.AttrType.CritAddValue:
			return (long)this.critAddValue.Val;
		case GameData.AttrType.HpRestore:
			return (long)this.hpRestore.Val;
		}
		goto IL_A2;
	}

	public override long TryAddValue(GameData.AttrType type, long tryAddValue)
	{
		switch (type)
		{
		case GameData.AttrType.PveAtk:
			return (long)this.pveAtk.TryAddValue((int)tryAddValue);
		case GameData.AttrType.PvpAtk:
			return (long)this.pvpAtk.TryAddValue((int)tryAddValue);
		case GameData.AttrType.HitRatio:
			return (long)this.hitRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.DodgeRatio:
			return (long)this.dodgeRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.CritRatio:
			return (long)this.critRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.DecritRatio:
			return (long)this.decritRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.CritHurtAddRatio:
			return (long)this.critHurtAddRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.ParryRatio:
			return (long)this.parryRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.DeparryRatio:
			return (long)this.deparryRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.ParryHurtDeRatio:
			return (long)this.parryHurtDeRatio.TryAddValue((int)tryAddValue);
		case (GameData.AttrType)1314:
		case (GameData.AttrType)1321:
		case (GameData.AttrType)1322:
		case (GameData.AttrType)1327:
		case (GameData.AttrType)1328:
		case GameData.AttrType.ExpAddRate:
			IL_A2:
			switch (type)
			{
			case GameData.AttrType.SkillNmlDmgScale:
				return (long)this.skillNmlDmgScale.TryAddValue((int)tryAddValue);
			case GameData.AttrType.SkillNmlDmgAddAmend:
				return (long)this.skillNmlDmgAddAmend.TryAddValue((int)tryAddValue);
			case (GameData.AttrType)503:
			case (GameData.AttrType)504:
			case (GameData.AttrType)505:
			case (GameData.AttrType)506:
			case (GameData.AttrType)507:
			case (GameData.AttrType)508:
			case (GameData.AttrType)509:
			case (GameData.AttrType)510:
				IL_EE:
				switch (type)
				{
				case GameData.AttrType.MoveSpeed:
					return (long)this.moveSpeed.TryAddValue((int)tryAddValue);
				case GameData.AttrType.ActSpeed:
					return (long)this.actSpeed.TryAddValue((int)tryAddValue);
				case GameData.AttrType.Affinity:
					return (long)this.affinity.TryAddValue((int)tryAddValue);
				case (GameData.AttrType)104:
				case (GameData.AttrType)105:
					IL_117:
					switch (type)
					{
					case GameData.AttrType.Hp:
						return this.hp.TryAddValue(tryAddValue);
					case GameData.AttrType.Fighting:
						return this.fighting.TryAddValue(tryAddValue);
					case GameData.AttrType.Diamond:
					case GameData.AttrType.Gold:
						IL_137:
						switch (type)
						{
						case GameData.AttrType.WaterBuffAddProbAddAmend:
							return (long)this.waterBuffAddProbAddAmend.TryAddValue((int)tryAddValue);
						case (GameData.AttrType)1222:
						case (GameData.AttrType)1223:
							IL_153:
							switch (type)
							{
							case GameData.AttrType.ThunderBuffAddProbAddAmend:
								return (long)this.thunderBuffAddProbAddAmend.TryAddValue((int)tryAddValue);
							case (GameData.AttrType)1232:
							case (GameData.AttrType)1233:
								IL_16F:
								switch (type)
								{
								case GameData.AttrType.RealHpLmt:
									return this.realHpLmt.TryAddValue(tryAddValue);
								case GameData.AttrType.RealVpLmt:
									return (long)this.realVpLmt.TryAddValue((int)tryAddValue);
								case GameData.AttrType.RealMoveSpeed:
									return (long)this.realMoveSpeed.TryAddValue((int)tryAddValue);
								case GameData.AttrType.RealActionSpeed:
									return (long)this.realActionSpeed.TryAddValue((int)tryAddValue);
								default:
									switch (type)
									{
									case GameData.AttrType.BuffMoveSpeedMulPosAmend:
										return (long)this.buffMoveSpeedMulPosAmend.TryAddValue((int)tryAddValue);
									case (GameData.AttrType)708:
										IL_1A3:
										if (type == GameData.AttrType.Atk)
										{
											return (long)this.atk.TryAddValue((int)tryAddValue);
										}
										if (type == GameData.AttrType.AtkMulAmend)
										{
											return (long)this.atkMulAmend.TryAddValue((int)tryAddValue);
										}
										if (type == GameData.AttrType.HpLmt)
										{
											return this.hpLmt.TryAddValue(tryAddValue);
										}
										if (type == GameData.AttrType.Defence)
										{
											return (long)this.defence.TryAddValue((int)tryAddValue);
										}
										if (type != GameData.AttrType.Lv)
										{
											Debug.LogError("未找到属性值:" + type);
											return 0L;
										}
										return (long)this.lv.TryAddValue((int)tryAddValue);
									case GameData.AttrType.BuffActSpeedMulPosAmend:
										return (long)this.buffActSpeedMulPosAmend.TryAddValue((int)tryAddValue);
									}
									goto IL_1A3;
								}
								break;
							case GameData.AttrType.ThunderBuffDurTimeAddAmend:
								return (long)this.thunderBuffDurTimeAddAmend.TryAddValue((int)tryAddValue);
							}
							goto IL_16F;
						case GameData.AttrType.WaterBuffDurTimeAddAmend:
							return (long)this.waterBuffDurTimeAddAmend.TryAddValue((int)tryAddValue);
						}
						goto IL_153;
					case GameData.AttrType.VipLv:
						return (long)this.vipLv.TryAddValue((int)tryAddValue);
					}
					goto IL_137;
				case GameData.AttrType.ActPoint:
					return (long)this.actPoint.TryAddValue((int)tryAddValue);
				case GameData.AttrType.ActPointLmt:
					return (long)this.actPointLmt.TryAddValue((int)tryAddValue);
				case GameData.AttrType.ActPointRecoverSpeedAmend:
					return (long)this.actPointRecoverSpeedAmend.TryAddValue((int)tryAddValue);
				}
				goto IL_117;
			case GameData.AttrType.SkillHolyDmgScaleBOMaxHp:
				return (long)this.skillHolyDmgScaleBOMaxHp.TryAddValue((int)tryAddValue);
			case GameData.AttrType.SkillHolyDmgScaleBOCurHp:
				return (long)this.skillHolyDmgScaleBOCurHp.TryAddValue((int)tryAddValue);
			case GameData.AttrType.SuckBloodScale:
				return (long)this.suckBloodScale.TryAddValue((int)tryAddValue);
			case GameData.AttrType.SkillTreatScaleBOAtk:
				return (long)this.skillTreatScaleBOAtk.TryAddValue((int)tryAddValue);
			case GameData.AttrType.SkillTreatScaleBOHpLmt:
				return (long)this.skillTreatScaleBOHpLmt.TryAddValue((int)tryAddValue);
			case GameData.AttrType.SkillIgnoreDefenceHurt:
				return (long)this.skillIgnoreDefenceHurt.TryAddValue((int)tryAddValue);
			}
			goto IL_EE;
		case GameData.AttrType.HurtAddRatio:
			return (long)this.hurtAddRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.HurtDeRatio:
			return (long)this.hurtDeRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.PveHurtAddRatio:
			return (long)this.pveHurtAddRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.PveHurtDeRatio:
			return (long)this.pveHurtDeRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.PvpHurtAddRatio:
			return (long)this.pvpHurtAddRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.PvpHurtDeRatio:
			return (long)this.pvpHurtDeRatio.TryAddValue((int)tryAddValue);
		case GameData.AttrType.DefMulAmend:
			return (long)this.defMulAmend.TryAddValue((int)tryAddValue);
		case GameData.AttrType.HpLmtMulAmend:
			return (long)this.hpLmtMulAmend.TryAddValue((int)tryAddValue);
		case GameData.AttrType.PveAtkMulAmend:
			return (long)this.pveAtkMulAmend.TryAddValue((int)tryAddValue);
		case GameData.AttrType.PvpAtkMulAmend:
			return (long)this.pvpAtkMulAmend.TryAddValue((int)tryAddValue);
		case GameData.AttrType.OnlineTime:
			return (long)this.onlineTime.TryAddValue((int)tryAddValue);
		case GameData.AttrType.VpLmt:
			return (long)this.vpLmt.TryAddValue((int)tryAddValue);
		case GameData.AttrType.VpLmtMulAmend:
			return (long)this.vpLmtMulAmend.TryAddValue((int)tryAddValue);
		case GameData.AttrType.VpResume:
			return (long)this.vpResume.TryAddValue((int)tryAddValue);
		case GameData.AttrType.VpAtk:
			return (long)this.vpAtk.TryAddValue((int)tryAddValue);
		case GameData.AttrType.VpAtkMulAmend:
			return (long)this.vpAtkMulAmend.TryAddValue((int)tryAddValue);
		case GameData.AttrType.Vp:
			return (long)this.vp.TryAddValue((int)tryAddValue);
		case GameData.AttrType.IdleVpResume:
			return (long)this.idleVpResume.TryAddValue((int)tryAddValue);
		case GameData.AttrType.HealIncreasePercent:
			return (long)this.healIncreasePercent.TryAddValue((int)tryAddValue);
		case GameData.AttrType.CritAddValue:
			return (long)this.critAddValue.TryAddValue((int)tryAddValue);
		case GameData.AttrType.HpRestore:
			return (long)this.hpRestore.TryAddValue((int)tryAddValue);
		}
		goto IL_A2;
	}

	protected override CfgFormula<T> GetCfgFormula<T>(GameData.AttrType type)
	{
		switch (type)
		{
		case GameData.AttrType.PveAtk:
			return this.pveAtk as CfgFormula<T>;
		case GameData.AttrType.PvpAtk:
			return this.pvpAtk as CfgFormula<T>;
		case GameData.AttrType.HitRatio:
			return this.hitRatio as CfgFormula<T>;
		case GameData.AttrType.DodgeRatio:
			return this.dodgeRatio as CfgFormula<T>;
		case GameData.AttrType.CritRatio:
			return this.critRatio as CfgFormula<T>;
		case GameData.AttrType.DecritRatio:
			return this.decritRatio as CfgFormula<T>;
		case GameData.AttrType.CritHurtAddRatio:
			return this.critHurtAddRatio as CfgFormula<T>;
		case GameData.AttrType.ParryRatio:
			return this.parryRatio as CfgFormula<T>;
		case GameData.AttrType.DeparryRatio:
			return this.deparryRatio as CfgFormula<T>;
		case GameData.AttrType.ParryHurtDeRatio:
			return this.parryHurtDeRatio as CfgFormula<T>;
		case (GameData.AttrType)1314:
		case (GameData.AttrType)1321:
		case (GameData.AttrType)1322:
		case (GameData.AttrType)1327:
		case (GameData.AttrType)1328:
		case GameData.AttrType.ExpAddRate:
			IL_A2:
			switch (type)
			{
			case GameData.AttrType.SkillNmlDmgScale:
				return this.skillNmlDmgScale as CfgFormula<T>;
			case GameData.AttrType.SkillNmlDmgAddAmend:
				return this.skillNmlDmgAddAmend as CfgFormula<T>;
			case (GameData.AttrType)503:
			case (GameData.AttrType)504:
			case (GameData.AttrType)505:
			case (GameData.AttrType)506:
			case (GameData.AttrType)507:
			case (GameData.AttrType)508:
			case (GameData.AttrType)509:
			case (GameData.AttrType)510:
				IL_EE:
				switch (type)
				{
				case GameData.AttrType.MoveSpeed:
					return this.moveSpeed as CfgFormula<T>;
				case GameData.AttrType.ActSpeed:
					return this.actSpeed as CfgFormula<T>;
				case GameData.AttrType.Affinity:
					return this.affinity as CfgFormula<T>;
				case (GameData.AttrType)104:
				case (GameData.AttrType)105:
					IL_117:
					switch (type)
					{
					case GameData.AttrType.Hp:
						return this.hp as CfgFormula<T>;
					case GameData.AttrType.Fighting:
						return this.fighting as CfgFormula<T>;
					case GameData.AttrType.Diamond:
					case GameData.AttrType.Gold:
						IL_137:
						switch (type)
						{
						case GameData.AttrType.WaterBuffAddProbAddAmend:
							return this.waterBuffAddProbAddAmend as CfgFormula<T>;
						case (GameData.AttrType)1222:
						case (GameData.AttrType)1223:
							IL_153:
							switch (type)
							{
							case GameData.AttrType.ThunderBuffAddProbAddAmend:
								return this.thunderBuffAddProbAddAmend as CfgFormula<T>;
							case (GameData.AttrType)1232:
							case (GameData.AttrType)1233:
								IL_16F:
								switch (type)
								{
								case GameData.AttrType.RealHpLmt:
									return this.realHpLmt as CfgFormula<T>;
								case GameData.AttrType.RealVpLmt:
									return this.realVpLmt as CfgFormula<T>;
								case GameData.AttrType.RealMoveSpeed:
									return this.realMoveSpeed as CfgFormula<T>;
								case GameData.AttrType.RealActionSpeed:
									return this.realActionSpeed as CfgFormula<T>;
								default:
									switch (type)
									{
									case GameData.AttrType.BuffMoveSpeedMulPosAmend:
										return this.buffMoveSpeedMulPosAmend as CfgFormula<T>;
									case (GameData.AttrType)708:
										IL_1A3:
										if (type == GameData.AttrType.Atk)
										{
											return this.atk as CfgFormula<T>;
										}
										if (type == GameData.AttrType.AtkMulAmend)
										{
											return this.atkMulAmend as CfgFormula<T>;
										}
										if (type == GameData.AttrType.HpLmt)
										{
											return this.hpLmt as CfgFormula<T>;
										}
										if (type == GameData.AttrType.Defence)
										{
											return this.defence as CfgFormula<T>;
										}
										if (type != GameData.AttrType.Lv)
										{
											Debug.LogError("未找到属性值:" + type);
											return null;
										}
										return this.lv as CfgFormula<T>;
									case GameData.AttrType.BuffActSpeedMulPosAmend:
										return this.buffActSpeedMulPosAmend as CfgFormula<T>;
									}
									goto IL_1A3;
								}
								break;
							case GameData.AttrType.ThunderBuffDurTimeAddAmend:
								return this.thunderBuffDurTimeAddAmend as CfgFormula<T>;
							}
							goto IL_16F;
						case GameData.AttrType.WaterBuffDurTimeAddAmend:
							return this.waterBuffDurTimeAddAmend as CfgFormula<T>;
						}
						goto IL_153;
					case GameData.AttrType.VipLv:
						return this.vipLv as CfgFormula<T>;
					}
					goto IL_137;
				case GameData.AttrType.ActPoint:
					return this.actPoint as CfgFormula<T>;
				case GameData.AttrType.ActPointLmt:
					return this.actPointLmt as CfgFormula<T>;
				case GameData.AttrType.ActPointRecoverSpeedAmend:
					return this.actPointRecoverSpeedAmend as CfgFormula<T>;
				}
				goto IL_117;
			case GameData.AttrType.SkillHolyDmgScaleBOMaxHp:
				return this.skillHolyDmgScaleBOMaxHp as CfgFormula<T>;
			case GameData.AttrType.SkillHolyDmgScaleBOCurHp:
				return this.skillHolyDmgScaleBOCurHp as CfgFormula<T>;
			case GameData.AttrType.SuckBloodScale:
				return this.suckBloodScale as CfgFormula<T>;
			case GameData.AttrType.SkillTreatScaleBOAtk:
				return this.skillTreatScaleBOAtk as CfgFormula<T>;
			case GameData.AttrType.SkillTreatScaleBOHpLmt:
				return this.skillTreatScaleBOHpLmt as CfgFormula<T>;
			case GameData.AttrType.SkillIgnoreDefenceHurt:
				return this.skillIgnoreDefenceHurt as CfgFormula<T>;
			}
			goto IL_EE;
		case GameData.AttrType.HurtAddRatio:
			return this.hurtAddRatio as CfgFormula<T>;
		case GameData.AttrType.HurtDeRatio:
			return this.hurtDeRatio as CfgFormula<T>;
		case GameData.AttrType.PveHurtAddRatio:
			return this.pveHurtAddRatio as CfgFormula<T>;
		case GameData.AttrType.PveHurtDeRatio:
			return this.pveHurtDeRatio as CfgFormula<T>;
		case GameData.AttrType.PvpHurtAddRatio:
			return this.pvpHurtAddRatio as CfgFormula<T>;
		case GameData.AttrType.PvpHurtDeRatio:
			return this.pvpHurtDeRatio as CfgFormula<T>;
		case GameData.AttrType.DefMulAmend:
			return this.defMulAmend as CfgFormula<T>;
		case GameData.AttrType.HpLmtMulAmend:
			return this.hpLmtMulAmend as CfgFormula<T>;
		case GameData.AttrType.PveAtkMulAmend:
			return this.pveAtkMulAmend as CfgFormula<T>;
		case GameData.AttrType.PvpAtkMulAmend:
			return this.pvpAtkMulAmend as CfgFormula<T>;
		case GameData.AttrType.OnlineTime:
			return this.onlineTime as CfgFormula<T>;
		case GameData.AttrType.VpLmt:
			return this.vpLmt as CfgFormula<T>;
		case GameData.AttrType.VpLmtMulAmend:
			return this.vpLmtMulAmend as CfgFormula<T>;
		case GameData.AttrType.VpResume:
			return this.vpResume as CfgFormula<T>;
		case GameData.AttrType.VpAtk:
			return this.vpAtk as CfgFormula<T>;
		case GameData.AttrType.VpAtkMulAmend:
			return this.vpAtkMulAmend as CfgFormula<T>;
		case GameData.AttrType.Vp:
			return this.vp as CfgFormula<T>;
		case GameData.AttrType.IdleVpResume:
			return this.idleVpResume as CfgFormula<T>;
		case GameData.AttrType.HealIncreasePercent:
			return this.healIncreasePercent as CfgFormula<T>;
		case GameData.AttrType.CritAddValue:
			return this.critAddValue as CfgFormula<T>;
		case GameData.AttrType.HpRestore:
			return this.hpRestore as CfgFormula<T>;
		}
		goto IL_A2;
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
}
