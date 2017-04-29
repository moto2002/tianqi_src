using GameData;
using Package;
using System;

public class BackUpBattleBaseAttrs : ISimpleBaseAttr, IDevelopBaseAttr
{
	protected IntCoder intCoder = new IntCoder();

	protected LongCoder longCoder = new LongCoder();

	private int moveSpeed = 1000;

	private int actSpeed = 1000;

	private int lv = 1;

	private long fighting;

	private int vipLv;

	private int atk;

	private int defence;

	private long hpLmt;

	private int pveAtk;

	private int pvpAtk;

	private int hitRatio = 1;

	private int dodgeRatio;

	private int critRatio;

	private int decritRatio;

	private int critHurtAddRatio;

	private int parryRatio;

	private int deparryRatio;

	private int parryHurtDeRatio;

	private int suckBloodScale;

	private int hurtAddRatio;

	private int hurtDeRatio;

	private int pveHurtAddRatio;

	private int pveHurtDeRatio;

	private int pvpHurtAddRatio;

	private int pvpHurtDeRatio;

	private int atkMulAmend = 1;

	private int defMulAmend = 1;

	private int hpMulAmend = 1;

	private int pveAtkMulAmend = 1;

	private int pvpAtkMulAmend = 1;

	private int actPointRecoverSpeedAmend = 1;

	private int vpLmt;

	private int vpLmtMulAmend;

	private int vpAtk;

	private int vpAtkMulAmend;

	private int idleVpResume;

	private int vpResume;

	private int waterBuffAddProbAddAmend;

	private int waterBuffDurTimeAddAmend;

	private int thunderBuffAddProbAddAmend;

	private int thunderBuffDurTimeAddAmend;

	private int healIncreasePercent;

	private int critAddValue;

	private int hpRestore;

	private int actPointLmt;

	private int buffMoveSpeedMulPosAmend;

	private int buffActSpeedMulPosAmend;

	private int skillTreatScaleBOAtk;

	private int skillTreatScaleBOHpLmt;

	private int skillIgnoreDefenceHurt;

	private int skillNmlDmgScale;

	private int skillNmlDmgAddAmend;

	private int skillHolyDmgScaleBOMaxHp;

	private int skillHolyDmgScaleBOCurHp;

	private int affinity;

	private int onlineTime;

	public int MoveSpeed
	{
		get
		{
			return this.intCoder.Decode(this.moveSpeed);
		}
		set
		{
			this.moveSpeed = this.intCoder.Encode(value);
		}
	}

	public int ActSpeed
	{
		get
		{
			return this.intCoder.Decode(this.actSpeed);
		}
		set
		{
			this.actSpeed = this.intCoder.Encode(value);
		}
	}

	public int Lv
	{
		get
		{
			return this.intCoder.Decode(this.lv);
		}
		set
		{
			this.lv = this.intCoder.Encode(value);
		}
	}

	public long Fighting
	{
		get
		{
			return this.longCoder.Decode(this.fighting);
		}
		set
		{
			this.fighting = this.longCoder.Encode(value);
		}
	}

	public int VipLv
	{
		get
		{
			return this.intCoder.Decode(this.vipLv);
		}
		set
		{
			this.vipLv = this.intCoder.Encode(value);
		}
	}

	public int Atk
	{
		get
		{
			return this.intCoder.Decode(this.atk);
		}
		set
		{
			this.atk = this.intCoder.Encode(value);
		}
	}

	public int Defence
	{
		get
		{
			return this.intCoder.Decode(this.defence);
		}
		set
		{
			this.defence = this.intCoder.Encode(value);
		}
	}

	public long HpLmt
	{
		get
		{
			return this.longCoder.Decode(this.hpLmt);
		}
		set
		{
			this.hpLmt = this.longCoder.Encode(value);
		}
	}

	public int PveAtk
	{
		get
		{
			return this.intCoder.Decode(this.pveAtk);
		}
		set
		{
			this.pveAtk = this.intCoder.Encode(value);
		}
	}

	public int PvpAtk
	{
		get
		{
			return this.intCoder.Decode(this.pvpAtk);
		}
		set
		{
			this.pvpAtk = this.intCoder.Encode(value);
		}
	}

	public int HitRatio
	{
		get
		{
			return this.intCoder.Decode(this.hitRatio);
		}
		set
		{
			this.hitRatio = this.intCoder.Encode(value);
		}
	}

	public int DodgeRatio
	{
		get
		{
			return this.intCoder.Decode(this.dodgeRatio);
		}
		set
		{
			this.dodgeRatio = this.intCoder.Encode(value);
		}
	}

	public int CritRatio
	{
		get
		{
			return this.intCoder.Decode(this.critRatio);
		}
		set
		{
			this.critRatio = this.intCoder.Encode(value);
		}
	}

	public int DecritRatio
	{
		get
		{
			return this.intCoder.Decode(this.decritRatio);
		}
		set
		{
			this.decritRatio = this.intCoder.Encode(value);
		}
	}

	public int CritHurtAddRatio
	{
		get
		{
			return this.intCoder.Decode(this.critHurtAddRatio);
		}
		set
		{
			this.critHurtAddRatio = this.intCoder.Encode(value);
		}
	}

	public int ParryRatio
	{
		get
		{
			return this.intCoder.Decode(this.parryRatio);
		}
		set
		{
			this.parryRatio = this.intCoder.Encode(value);
		}
	}

	public int DeparryRatio
	{
		get
		{
			return this.intCoder.Decode(this.deparryRatio);
		}
		set
		{
			this.deparryRatio = this.intCoder.Encode(value);
		}
	}

	public int ParryHurtDeRatio
	{
		get
		{
			return this.intCoder.Decode(this.parryHurtDeRatio);
		}
		set
		{
			this.parryHurtDeRatio = this.intCoder.Encode(value);
		}
	}

	public int SuckBloodScale
	{
		get
		{
			return this.intCoder.Decode(this.suckBloodScale);
		}
		set
		{
			this.suckBloodScale = this.intCoder.Encode(value);
		}
	}

	public int HurtAddRatio
	{
		get
		{
			return this.intCoder.Decode(this.hurtAddRatio);
		}
		set
		{
			this.hurtAddRatio = this.intCoder.Encode(value);
		}
	}

	public int HurtDeRatio
	{
		get
		{
			return this.intCoder.Decode(this.hurtDeRatio);
		}
		set
		{
			this.hurtDeRatio = this.intCoder.Encode(value);
		}
	}

	public int PveHurtAddRatio
	{
		get
		{
			return this.intCoder.Decode(this.pveHurtAddRatio);
		}
		set
		{
			this.pveHurtAddRatio = this.intCoder.Encode(value);
		}
	}

	public int PveHurtDeRatio
	{
		get
		{
			return this.intCoder.Decode(this.pveHurtDeRatio);
		}
		set
		{
			this.pveHurtDeRatio = this.intCoder.Encode(value);
		}
	}

	public int PvpHurtAddRatio
	{
		get
		{
			return this.intCoder.Decode(this.pvpHurtAddRatio);
		}
		set
		{
			this.pvpHurtAddRatio = this.intCoder.Encode(value);
		}
	}

	public int PvpHurtDeRatio
	{
		get
		{
			return this.intCoder.Decode(this.pvpHurtDeRatio);
		}
		set
		{
			this.pvpHurtDeRatio = this.intCoder.Encode(value);
		}
	}

	public int AtkMulAmend
	{
		get
		{
			return this.intCoder.Decode(this.atkMulAmend);
		}
		set
		{
			this.atkMulAmend = this.intCoder.Encode(value);
		}
	}

	public int DefMulAmend
	{
		get
		{
			return this.intCoder.Decode(this.defMulAmend);
		}
		set
		{
			this.defMulAmend = this.intCoder.Encode(value);
		}
	}

	public int HpLmtMulAmend
	{
		get
		{
			return this.intCoder.Decode(this.hpMulAmend);
		}
		set
		{
			this.hpMulAmend = this.intCoder.Encode(value);
		}
	}

	public int PveAtkMulAmend
	{
		get
		{
			return this.intCoder.Decode(this.pveAtkMulAmend);
		}
		set
		{
			this.pveAtkMulAmend = this.intCoder.Encode(value);
		}
	}

	public int PvpAtkMulAmend
	{
		get
		{
			return this.intCoder.Decode(this.pvpAtkMulAmend);
		}
		set
		{
			this.pvpAtkMulAmend = this.intCoder.Encode(value);
		}
	}

	public int ActPointRecoverSpeedAmend
	{
		get
		{
			return this.intCoder.Decode(this.actPointRecoverSpeedAmend);
		}
		set
		{
			this.actPointRecoverSpeedAmend = this.intCoder.Encode(value);
		}
	}

	public int VpLmt
	{
		get
		{
			return this.intCoder.Decode(this.vpLmt);
		}
		set
		{
			this.vpLmt = this.intCoder.Encode(value);
		}
	}

	public int VpLmtMulAmend
	{
		get
		{
			return this.intCoder.Decode(this.vpLmtMulAmend);
		}
		set
		{
			this.vpLmtMulAmend = this.intCoder.Encode(value);
		}
	}

	public int VpAtk
	{
		get
		{
			return this.intCoder.Decode(this.vpAtk);
		}
		set
		{
			this.vpAtk = this.intCoder.Encode(value);
		}
	}

	public int VpAtkMulAmend
	{
		get
		{
			return this.intCoder.Decode(this.vpAtkMulAmend);
		}
		set
		{
			this.vpAtkMulAmend = this.intCoder.Encode(value);
		}
	}

	public int VpResume
	{
		get
		{
			return this.intCoder.Decode(this.idleVpResume);
		}
		set
		{
			this.idleVpResume = this.intCoder.Encode(value);
		}
	}

	public int IdleVpResume
	{
		get
		{
			return this.intCoder.Decode(this.vpResume);
		}
		set
		{
			this.vpResume = this.intCoder.Encode(value);
		}
	}

	public int WaterBuffAddProbAddAmend
	{
		get
		{
			return this.intCoder.Decode(this.waterBuffAddProbAddAmend);
		}
		set
		{
			this.waterBuffAddProbAddAmend = this.intCoder.Encode(value);
		}
	}

	public int WaterBuffDurTimeAddAmend
	{
		get
		{
			return this.intCoder.Decode(this.waterBuffDurTimeAddAmend);
		}
		set
		{
			this.waterBuffDurTimeAddAmend = this.intCoder.Encode(value);
		}
	}

	public int ThunderBuffAddProbAddAmend
	{
		get
		{
			return this.intCoder.Decode(this.thunderBuffAddProbAddAmend);
		}
		set
		{
			this.thunderBuffAddProbAddAmend = this.intCoder.Encode(value);
		}
	}

	public int ThunderBuffDurTimeAddAmend
	{
		get
		{
			return this.intCoder.Decode(this.thunderBuffDurTimeAddAmend);
		}
		set
		{
			this.thunderBuffDurTimeAddAmend = this.intCoder.Encode(value);
		}
	}

	public int HealIncreasePercent
	{
		get
		{
			return this.intCoder.Decode(this.healIncreasePercent);
		}
		set
		{
			this.healIncreasePercent = this.intCoder.Encode(value);
		}
	}

	public int CritAddValue
	{
		get
		{
			return this.intCoder.Decode(this.critAddValue);
		}
		set
		{
			this.critAddValue = this.intCoder.Encode(value);
		}
	}

	public int HpRestore
	{
		get
		{
			return this.intCoder.Decode(this.hpRestore);
		}
		set
		{
			this.hpRestore = this.intCoder.Encode(value);
		}
	}

	public int ActPointLmt
	{
		get
		{
			return this.intCoder.Decode(this.actPointLmt);
		}
		set
		{
			this.actPointLmt = this.intCoder.Encode(value);
		}
	}

	public int BuffMoveSpeedMulPosAmend
	{
		get
		{
			return this.intCoder.Decode(this.buffMoveSpeedMulPosAmend);
		}
		set
		{
			this.buffMoveSpeedMulPosAmend = this.intCoder.Encode(value);
		}
	}

	public int BuffActSpeedMulPosAmend
	{
		get
		{
			return this.intCoder.Decode(this.buffActSpeedMulPosAmend);
		}
		set
		{
			this.buffActSpeedMulPosAmend = this.intCoder.Encode(value);
		}
	}

	public int SkillTreatScaleBOAtk
	{
		get
		{
			return this.intCoder.Decode(this.skillTreatScaleBOAtk);
		}
		set
		{
			this.skillTreatScaleBOAtk = this.intCoder.Encode(value);
		}
	}

	public int SkillTreatScaleBOHpLmt
	{
		get
		{
			return this.intCoder.Decode(this.skillTreatScaleBOHpLmt);
		}
		set
		{
			this.skillTreatScaleBOHpLmt = this.intCoder.Encode(value);
		}
	}

	public int SkillIgnoreDefenceHurt
	{
		get
		{
			return this.intCoder.Decode(this.skillIgnoreDefenceHurt);
		}
		set
		{
			this.skillIgnoreDefenceHurt = this.intCoder.Encode(value);
		}
	}

	public int SkillNmlDmgScale
	{
		get
		{
			return this.intCoder.Decode(this.skillNmlDmgScale);
		}
		set
		{
			this.skillNmlDmgScale = this.intCoder.Encode(value);
		}
	}

	public int SkillNmlDmgAddAmend
	{
		get
		{
			return this.intCoder.Decode(this.skillNmlDmgAddAmend);
		}
		set
		{
			this.skillNmlDmgAddAmend = this.intCoder.Encode(value);
		}
	}

	public int SkillHolyDmgScaleBOMaxHp
	{
		get
		{
			return this.intCoder.Decode(this.skillHolyDmgScaleBOMaxHp);
		}
		set
		{
			this.skillHolyDmgScaleBOMaxHp = this.intCoder.Encode(value);
		}
	}

	public int SkillHolyDmgScaleBOCurHp
	{
		get
		{
			return this.intCoder.Decode(this.skillHolyDmgScaleBOCurHp);
		}
		set
		{
			this.skillHolyDmgScaleBOCurHp = this.intCoder.Encode(value);
		}
	}

	public int Affinity
	{
		get
		{
			return this.intCoder.Decode(this.affinity);
		}
		set
		{
			this.affinity = this.intCoder.Encode(value);
		}
	}

	public int OnlineTime
	{
		get
		{
			return this.intCoder.Decode(this.onlineTime);
		}
		set
		{
			this.onlineTime = this.intCoder.Encode(value);
		}
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
		this.IdleVpResume = origin.publicBaseInfo.IdleVpResume;
		this.VpResume = origin.publicBaseInfo.VpResume;
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
		this.SkillNmlDmgScale = origin.battleBaseAttr.SkillNmlDmgScale;
		this.SkillNmlDmgAddAmend = origin.battleBaseAttr.SkillNmlDmgAddAmend;
		this.SkillHolyDmgScaleBOMaxHp = origin.battleBaseAttr.SkillHolyDmgScaleBOMaxHp;
		this.SkillHolyDmgScaleBOCurHp = origin.battleBaseAttr.SkillHolyDmgScaleBOCurHp;
		this.Affinity = origin.battleBaseAttr.Affinity;
		this.OnlineTime = origin.battleBaseAttr.OnlineTime;
	}

	public void ResetAllAttrs()
	{
		this.moveSpeed = 1000;
		this.actSpeed = 1000;
		this.lv = 1;
		this.fighting = 0L;
		this.vipLv = 0;
		this.atk = 0;
		this.defence = 0;
		this.hpLmt = 0L;
		this.pveAtk = 0;
		this.pvpAtk = 0;
		this.hitRatio = 1;
		this.dodgeRatio = 0;
		this.critRatio = 0;
		this.decritRatio = 0;
		this.critHurtAddRatio = 0;
		this.parryRatio = 0;
		this.deparryRatio = 0;
		this.parryHurtDeRatio = 0;
		this.suckBloodScale = 0;
		this.hurtAddRatio = 0;
		this.hurtDeRatio = 0;
		this.pveHurtAddRatio = 0;
		this.pveHurtDeRatio = 0;
		this.pvpHurtAddRatio = 0;
		this.pvpHurtDeRatio = 0;
		this.atkMulAmend = 1;
		this.defMulAmend = 1;
		this.hpMulAmend = 1;
		this.pveAtkMulAmend = 1;
		this.pvpAtkMulAmend = 1;
		this.actPointLmt = 0;
		this.actPointRecoverSpeedAmend = 1;
		this.vpLmt = 0;
		this.vpLmtMulAmend = 0;
		this.vpAtk = 0;
		this.vpAtkMulAmend = 0;
		this.idleVpResume = 0;
		this.vpResume = 0;
		this.waterBuffAddProbAddAmend = 0;
		this.waterBuffDurTimeAddAmend = 0;
		this.thunderBuffAddProbAddAmend = 0;
		this.thunderBuffDurTimeAddAmend = 0;
		this.healIncreasePercent = 0;
		this.critAddValue = 0;
		this.hpRestore = 0;
		this.buffMoveSpeedMulPosAmend = 0;
		this.buffActSpeedMulPosAmend = 0;
		this.skillTreatScaleBOAtk = 0;
		this.skillTreatScaleBOHpLmt = 0;
		this.skillIgnoreDefenceHurt = 0;
		this.skillNmlDmgScale = 0;
		this.skillNmlDmgAddAmend = 0;
		this.skillHolyDmgScaleBOMaxHp = 0;
		this.skillHolyDmgScaleBOCurHp = 0;
		this.affinity = 0;
		this.onlineTime = 0;
	}
}
