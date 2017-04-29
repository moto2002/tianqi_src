using GameData;
using Package;
using System;
using UnityEngine;

public class CityBaseAttrs : SimpleBaseAttrExtend, IClientBaseAttr, ISimpleBaseAttr, IDevelopBaseAttr, ICityBaseAttr
{
	protected IntCoder intCoder = new IntCoder();

	protected LongCoder longCoder = new LongCoder();

	private int moveSpeed = 1;

	private int actSpeed = 1;

	private int lv = 1;

	private int vipLv;

	private long fighting;

	private int atk;

	private int defence;

	private long hpLmt;

	private int pveAtk;

	private int pvpAtk;

	private int hitRatio;

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

	private int atkMulAmend;

	private int defMulAmend;

	private int hpMulAmend;

	private int pveAtkMulAmend;

	private int pvpAtkMulAmend;

	private int actPointRecoverSpeedAmend = 1;

	private int vpLmt;

	private int vpLmtMulAmend = 1;

	private int vpAtk;

	private int vpAtkMulAmend = 1;

	private int vpResume;

	private int idleVpResume;

	private int waterBuffAddProbAddAmend;

	private int waterBuffDurTimeAddAmend;

	private int thunderBuffAddProbAddAmend;

	private int thunderBuffDurTimeAddAmend;

	private int healIncreasePercent;

	private int critAddValue;

	private int hpRestore;

	private int energy;

	private int energyLmt;

	private long exp;

	private long expLmt;

	private int diamond;

	private long gold;

	private int rechargeDiamond;

	private int honor;

	private int competitiveCurrency;

	private long skillPoint;

	private long reputation;

	public int MoveSpeed
	{
		get
		{
			return this.intCoder.Decode(this.moveSpeed);
		}
		set
		{
			int num = this.moveSpeed;
			this.moveSpeed = this.intCoder.Encode(value);
			if (num != this.moveSpeed)
			{
				this.OnAttrChanged(GameData.AttrType.MoveSpeed, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.actSpeed;
			this.actSpeed = this.intCoder.Encode(value);
			if (num != this.actSpeed)
			{
				this.OnAttrChanged(GameData.AttrType.ActSpeed, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.lv;
			this.lv = this.intCoder.Encode(value);
			if (num != this.lv)
			{
				this.OnAttrChanged(GameData.AttrType.Lv, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.vipLv;
			this.vipLv = this.intCoder.Encode(value);
			if (num != this.vipLv)
			{
				this.OnAttrChanged(GameData.AttrType.VipLv, (long)this.intCoder.Decode(num), (long)value);
			}
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
			long num = this.fighting;
			this.fighting = this.longCoder.Encode(value);
			if (num != this.fighting)
			{
				this.OnAttrChanged(GameData.AttrType.Fighting, this.longCoder.Decode(num), value);
			}
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
			int num = this.atk;
			this.atk = this.intCoder.Encode(value);
			if (num != this.atk)
			{
				this.OnAttrChanged(GameData.AttrType.Atk, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.defence;
			this.defence = this.intCoder.Encode(value);
			if (num != this.defence)
			{
				this.OnAttrChanged(GameData.AttrType.Defence, (long)this.intCoder.Decode(num), (long)value);
			}
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
			long num = this.hpLmt;
			this.hpLmt = this.longCoder.Encode(value);
			if (num != this.hpLmt)
			{
				this.OnAttrChanged(GameData.AttrType.HpLmt, this.longCoder.Decode(num), value);
			}
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
			int num = this.pveAtk;
			this.pveAtk = this.intCoder.Encode(value);
			if (num != this.pveAtk)
			{
				this.OnAttrChanged(GameData.AttrType.PveAtk, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.pvpAtk;
			this.pvpAtk = this.intCoder.Encode(value);
			if (num != this.pvpAtk)
			{
				this.OnAttrChanged(GameData.AttrType.PvpAtk, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.hitRatio;
			this.hitRatio = this.intCoder.Encode(value);
			if (num != this.hitRatio)
			{
				this.OnAttrChanged(GameData.AttrType.HitRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.dodgeRatio;
			this.dodgeRatio = this.intCoder.Encode(value);
			if (num != this.dodgeRatio)
			{
				this.OnAttrChanged(GameData.AttrType.DodgeRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.critRatio;
			this.critRatio = this.intCoder.Encode(value);
			if (num != this.critRatio)
			{
				this.OnAttrChanged(GameData.AttrType.CritRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.decritRatio;
			this.decritRatio = this.intCoder.Encode(value);
			if (num != this.decritRatio)
			{
				this.OnAttrChanged(GameData.AttrType.DecritRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.critHurtAddRatio;
			this.critHurtAddRatio = this.intCoder.Encode(value);
			if (num != this.critHurtAddRatio)
			{
				this.OnAttrChanged(GameData.AttrType.CritHurtAddRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.parryRatio;
			this.parryRatio = this.intCoder.Encode(value);
			if (num != this.parryRatio)
			{
				this.OnAttrChanged(GameData.AttrType.ParryRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.deparryRatio;
			this.deparryRatio = this.intCoder.Encode(value);
			if (num != this.deparryRatio)
			{
				this.OnAttrChanged(GameData.AttrType.DeparryRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.parryHurtDeRatio;
			this.parryHurtDeRatio = this.intCoder.Encode(value);
			if (num != this.parryHurtDeRatio)
			{
				this.OnAttrChanged(GameData.AttrType.ParryHurtDeRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.suckBloodScale;
			this.suckBloodScale = this.intCoder.Encode(value);
			if (num != this.suckBloodScale)
			{
				this.OnAttrChanged(GameData.AttrType.SuckBloodScale, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.hurtAddRatio;
			this.hurtAddRatio = this.intCoder.Encode(value);
			if (num != this.hurtAddRatio)
			{
				this.OnAttrChanged(GameData.AttrType.HurtAddRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.hurtDeRatio;
			this.hurtDeRatio = this.intCoder.Encode(value);
			if (num != this.hurtDeRatio)
			{
				this.OnAttrChanged(GameData.AttrType.HurtDeRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.pveHurtAddRatio;
			this.pveHurtAddRatio = this.intCoder.Encode(value);
			if (num != this.pveHurtAddRatio)
			{
				this.OnAttrChanged(GameData.AttrType.PveHurtAddRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.pveHurtDeRatio;
			this.pveHurtDeRatio = this.intCoder.Encode(value);
			if (num != this.pveHurtDeRatio)
			{
				this.OnAttrChanged(GameData.AttrType.PveHurtDeRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.pvpHurtAddRatio;
			this.pvpHurtAddRatio = this.intCoder.Encode(value);
			if (num != this.pvpHurtAddRatio)
			{
				this.OnAttrChanged(GameData.AttrType.PvpHurtAddRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.pvpHurtDeRatio;
			this.pvpHurtDeRatio = this.intCoder.Encode(value);
			if (num != this.pvpHurtDeRatio)
			{
				this.OnAttrChanged(GameData.AttrType.PvpHurtDeRatio, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.atkMulAmend;
			this.atkMulAmend = this.intCoder.Encode(value);
			if (num != this.atkMulAmend)
			{
				this.OnAttrChanged(GameData.AttrType.AtkMulAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.defMulAmend;
			this.defMulAmend = this.intCoder.Encode(value);
			if (num != this.defMulAmend)
			{
				this.OnAttrChanged(GameData.AttrType.DefMulAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.hpMulAmend;
			this.hpMulAmend = this.intCoder.Encode(value);
			if (num != this.hpMulAmend)
			{
				this.OnAttrChanged(GameData.AttrType.HpLmtMulAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.pveAtkMulAmend;
			this.pveAtkMulAmend = this.intCoder.Encode(value);
			if (num != this.pveAtkMulAmend)
			{
				this.OnAttrChanged(GameData.AttrType.PveAtkMulAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.pvpAtkMulAmend;
			this.pvpAtkMulAmend = this.intCoder.Encode(value);
			if (num != this.pvpAtkMulAmend)
			{
				this.OnAttrChanged(GameData.AttrType.PvpAtkMulAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.actPointRecoverSpeedAmend;
			this.actPointRecoverSpeedAmend = this.intCoder.Encode(value);
			if (num != this.actPointRecoverSpeedAmend)
			{
				this.OnAttrChanged(GameData.AttrType.ActPointRecoverSpeedAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.vpLmt;
			this.vpLmt = this.intCoder.Encode(value);
			if (num != this.vpLmt)
			{
				this.OnAttrChanged(GameData.AttrType.VpLmt, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.vpLmtMulAmend;
			this.vpLmtMulAmend = this.intCoder.Encode(value);
			if (num != this.vpLmtMulAmend)
			{
				this.OnAttrChanged(GameData.AttrType.VpLmtMulAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.vpAtk;
			this.vpAtk = this.intCoder.Encode(value);
			if (num != this.vpAtk)
			{
				this.OnAttrChanged(GameData.AttrType.VpAtk, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.vpAtkMulAmend;
			this.vpAtkMulAmend = this.intCoder.Encode(value);
			if (num != this.vpAtkMulAmend)
			{
				this.OnAttrChanged(GameData.AttrType.VpAtkMulAmend, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public int VpResume
	{
		get
		{
			return this.intCoder.Decode(this.vpResume);
		}
		set
		{
			int num = this.vpResume;
			this.vpResume = this.intCoder.Encode(value);
			if (num != this.vpResume)
			{
				this.OnAttrChanged(GameData.AttrType.VpResume, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public int IdleVpResume
	{
		get
		{
			return this.intCoder.Decode(this.idleVpResume);
		}
		set
		{
			int num = this.idleVpResume;
			this.idleVpResume = this.intCoder.Encode(value);
			if (num != this.idleVpResume)
			{
				this.OnAttrChanged(GameData.AttrType.IdleVpResume, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.waterBuffAddProbAddAmend;
			this.waterBuffAddProbAddAmend = this.intCoder.Encode(value);
			if (num != this.waterBuffAddProbAddAmend)
			{
				this.OnAttrChanged(GameData.AttrType.WaterBuffAddProbAddAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.waterBuffDurTimeAddAmend;
			this.waterBuffDurTimeAddAmend = this.intCoder.Encode(value);
			if (num != this.waterBuffDurTimeAddAmend)
			{
				this.OnAttrChanged(GameData.AttrType.WaterBuffDurTimeAddAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.thunderBuffAddProbAddAmend;
			this.thunderBuffAddProbAddAmend = this.intCoder.Encode(value);
			if (num != this.thunderBuffAddProbAddAmend)
			{
				this.OnAttrChanged(GameData.AttrType.ThunderBuffAddProbAddAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.thunderBuffDurTimeAddAmend;
			this.thunderBuffDurTimeAddAmend = this.intCoder.Encode(value);
			if (num != this.thunderBuffDurTimeAddAmend)
			{
				this.OnAttrChanged(GameData.AttrType.ThunderBuffDurTimeAddAmend, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.healIncreasePercent;
			this.healIncreasePercent = this.intCoder.Encode(value);
			if (num != this.healIncreasePercent)
			{
				this.OnAttrChanged(GameData.AttrType.HealIncreasePercent, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.critAddValue;
			this.critAddValue = this.intCoder.Encode(value);
			if (num != this.critAddValue)
			{
				this.OnAttrChanged(GameData.AttrType.CritAddValue, (long)this.intCoder.Decode(num), (long)value);
			}
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
			int num = this.hpRestore;
			this.hpRestore = this.intCoder.Encode(value);
			if (num != this.hpRestore)
			{
				this.OnAttrChanged(GameData.AttrType.HpRestore, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public int Energy
	{
		get
		{
			return this.intCoder.Decode(this.energy);
		}
		set
		{
			int num = this.energy;
			this.energy = this.intCoder.Encode(value);
			if (num != this.energy)
			{
				this.OnAttrChanged(GameData.AttrType.Energy, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public int EnergyLmt
	{
		get
		{
			return this.intCoder.Decode(this.energyLmt);
		}
		set
		{
			int num = this.energyLmt;
			this.energyLmt = this.intCoder.Encode(value);
			if (num != this.energyLmt)
			{
				this.OnAttrChanged(GameData.AttrType.EnergyLmt, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public long Exp
	{
		get
		{
			return this.longCoder.Decode(this.exp);
		}
		set
		{
			long num = this.exp;
			this.exp = this.longCoder.Encode(value);
			if (num != this.exp)
			{
				this.OnAttrChanged(GameData.AttrType.Exp, this.longCoder.Decode(num), value);
			}
		}
	}

	public long ExpLmt
	{
		get
		{
			return this.longCoder.Decode(this.expLmt);
		}
		set
		{
			long num = this.expLmt;
			this.expLmt = this.longCoder.Encode(value);
			if (num != this.expLmt)
			{
				this.OnAttrChanged(GameData.AttrType.ExpLmt, this.longCoder.Decode(num), value);
			}
		}
	}

	public int Diamond
	{
		get
		{
			return this.intCoder.Decode(this.diamond);
		}
		set
		{
			int num = this.diamond;
			this.diamond = this.intCoder.Encode(value);
			if (num != this.diamond)
			{
				this.OnAttrChanged(GameData.AttrType.Diamond, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public long Gold
	{
		get
		{
			return this.longCoder.Decode(this.gold);
		}
		set
		{
			long num = this.gold;
			this.gold = this.longCoder.Encode(value);
			if (num != this.gold)
			{
				this.OnAttrChanged(GameData.AttrType.Gold, this.longCoder.Decode(num), value);
			}
		}
	}

	public int RechargeDiamond
	{
		get
		{
			return this.intCoder.Decode(this.rechargeDiamond);
		}
		set
		{
			int num = this.rechargeDiamond;
			this.rechargeDiamond = this.intCoder.Encode(value);
			if (num != this.rechargeDiamond)
			{
				this.OnAttrChanged(GameData.AttrType.RechargeDiamond, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public int Honor
	{
		get
		{
			return this.intCoder.Decode(this.honor);
		}
		set
		{
			int num = this.honor;
			this.honor = this.intCoder.Encode(value);
			if (num != this.honor)
			{
				this.OnAttrChanged(GameData.AttrType.Honor, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public int CompetitiveCurrency
	{
		get
		{
			return this.intCoder.Decode(this.competitiveCurrency);
		}
		set
		{
			int num = this.competitiveCurrency;
			this.competitiveCurrency = this.intCoder.Encode(value);
			if (num != this.competitiveCurrency)
			{
				this.OnAttrChanged(GameData.AttrType.CompetitiveCurrency, (long)this.intCoder.Decode(num), (long)value);
			}
		}
	}

	public long SkillPoint
	{
		get
		{
			return this.longCoder.Decode(this.skillPoint);
		}
		set
		{
			long num = this.skillPoint;
			this.skillPoint = this.longCoder.Encode(value);
			if (num != this.skillPoint)
			{
				this.OnAttrChanged(GameData.AttrType.SkillPoint, this.longCoder.Decode(num), value);
			}
		}
	}

	public long Reputation
	{
		get
		{
			return this.longCoder.Decode(this.reputation);
		}
		set
		{
			long num = this.reputation;
			this.reputation = this.longCoder.Encode(value);
			if (num != this.reputation)
			{
				this.OnAttrChanged(GameData.AttrType.Reputation, this.longCoder.Decode(num), value);
			}
		}
	}

	public int RealMoveSpeed
	{
		get
		{
			return this.MoveSpeed;
		}
		set
		{
			this.OnAttrChanged(GameData.AttrType.RealMoveSpeed, (long)this.MoveSpeed, (long)this.MoveSpeed);
		}
	}

	public int RealActionSpeed
	{
		get
		{
			return this.ActSpeed;
		}
		set
		{
			this.OnAttrChanged(GameData.AttrType.RealActionSpeed, (long)this.ActSpeed, (long)this.ActSpeed);
		}
	}

	public void AssignAllAttrs(CityBaseInfo origin)
	{
		this.MoveSpeed = origin.baseInfo.simpleInfo.MoveSpeed;
		this.ActSpeed = origin.baseInfo.simpleInfo.AtkSpeed;
		this.Lv = origin.baseInfo.simpleInfo.Lv;
		this.Fighting = origin.baseInfo.simpleInfo.Fighting;
		this.VipLv = origin.baseInfo.simpleInfo.VipLv;
		this.Atk = origin.baseInfo.Atk;
		this.Defence = origin.baseInfo.Defence;
		this.HpLmt = origin.baseInfo.HpLmt;
		this.PveAtk = origin.baseInfo.PveAtk;
		this.PvpAtk = origin.baseInfo.PvpAtk;
		this.HitRatio = origin.baseInfo.HitRatio;
		this.DodgeRatio = origin.baseInfo.DodgeRatio;
		this.CritRatio = origin.baseInfo.CritRatio;
		this.DecritRatio = origin.baseInfo.DecritRatio;
		this.CritHurtAddRatio = origin.baseInfo.CritHurtAddRatio;
		this.ParryRatio = origin.baseInfo.ParryRatio;
		this.DeparryRatio = origin.baseInfo.DeparryRatio;
		this.ParryHurtDeRatio = origin.baseInfo.ParryHurtDeRatio;
		this.SuckBloodScale = origin.baseInfo.SuckBloodScale;
		this.HurtAddRatio = origin.baseInfo.HurtAddRatio;
		this.HurtDeRatio = origin.baseInfo.HurtDeRatio;
		this.PveHurtAddRatio = origin.baseInfo.PveHurtAddRatio;
		this.PveHurtDeRatio = origin.baseInfo.PveHurtDeRatio;
		this.PvpHurtAddRatio = origin.baseInfo.PvpHurtAddRatio;
		this.PvpHurtDeRatio = origin.baseInfo.PvpHurtDeRatio;
		this.AtkMulAmend = origin.baseInfo.AtkMulAmend;
		this.DefMulAmend = origin.baseInfo.DefMulAmend;
		this.HpLmtMulAmend = origin.baseInfo.HpLmtMulAmend;
		this.PveAtkMulAmend = origin.baseInfo.PveAtkMulAmend;
		this.PvpAtkMulAmend = origin.baseInfo.PvpAtkMulAmend;
		this.ActPointRecoverSpeedAmend = origin.baseInfo.ActPointRecoverSpeedAmend;
		this.VpLmt = origin.baseInfo.VpLmt;
		this.VpLmtMulAmend = origin.baseInfo.VpLmtMulAmend;
		this.VpAtk = origin.baseInfo.VpAtk;
		this.VpAtkMulAmend = origin.baseInfo.VpAtkMulAmend;
		this.IdleVpResume = origin.baseInfo.IdleVpResume;
		this.VpResume = origin.baseInfo.VpResume;
		this.WaterBuffAddProbAddAmend = origin.baseInfo.WaterBuffAddProbAddAmend;
		this.WaterBuffDurTimeAddAmend = origin.baseInfo.WaterBuffDurTimeAddAmend;
		this.ThunderBuffAddProbAddAmend = origin.baseInfo.ThunderBuffAddProbAddAmend;
		this.ThunderBuffDurTimeAddAmend = origin.baseInfo.ThunderBuffDurTimeAddAmend;
		this.HealIncreasePercent = origin.baseInfo.HealIncreasePercent;
		this.CritAddValue = origin.baseInfo.CritAddValue;
		this.HpRestore = origin.baseInfo.HpRestore;
		this.Energy = origin.Energy;
		this.EnergyLmt = origin.EnergyLmt;
		this.Exp = origin.Exp;
		this.ExpLmt = origin.ExpLmt;
		this.Diamond = origin.Diamond;
		this.Gold = origin.Gold;
		this.RechargeDiamond = origin.RechargeDiamond;
		this.Honor = origin.Honor;
		this.CompetitiveCurrency = origin.CompetitiveCurrency;
		this.SkillPoint = (long)origin.SkillPoint;
		this.Reputation = (long)origin.Reputation;
	}

	public void ResetAllAttrs()
	{
		this.moveSpeed = 1;
		this.actSpeed = 1;
		this.lv = 1;
		this.vipLv = 0;
		this.fighting = 0L;
		this.atk = 0;
		this.defence = 0;
		this.hpLmt = 0L;
		this.pveAtk = 0;
		this.pvpAtk = 0;
		this.hitRatio = 0;
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
		this.atkMulAmend = 0;
		this.defMulAmend = 0;
		this.hpMulAmend = 0;
		this.pveAtkMulAmend = 0;
		this.pvpAtkMulAmend = 0;
		this.actPointRecoverSpeedAmend = 1;
		this.vpLmt = 0;
		this.vpLmtMulAmend = 1;
		this.vpAtk = 0;
		this.vpAtkMulAmend = 1;
		this.vpResume = 0;
		this.idleVpResume = 0;
		this.waterBuffAddProbAddAmend = 0;
		this.waterBuffDurTimeAddAmend = 0;
		this.thunderBuffAddProbAddAmend = 0;
		this.thunderBuffDurTimeAddAmend = 0;
		this.healIncreasePercent = 0;
		this.critAddValue = 0;
		this.hpRestore = 0;
		this.energy = 0;
		this.energyLmt = 0;
		this.exp = 0L;
		this.expLmt = 0L;
		this.diamond = 0;
		this.gold = 0L;
		this.rechargeDiamond = 0;
		this.honor = 0;
		this.competitiveCurrency = 0;
		this.skillPoint = 0L;
	}

	public override void SetValue(GameData.AttrType type, int value, bool isFirstTry)
	{
		switch (type)
		{
		case GameData.AttrType.PveAtk:
			this.PveAtk = value;
			return;
		case GameData.AttrType.PvpAtk:
			this.PvpAtk = value;
			return;
		case GameData.AttrType.HitRatio:
			this.HitRatio = value;
			return;
		case GameData.AttrType.DodgeRatio:
			this.DodgeRatio = value;
			return;
		case GameData.AttrType.CritRatio:
			this.CritRatio = value;
			return;
		case GameData.AttrType.DecritRatio:
			this.DecritRatio = value;
			return;
		case GameData.AttrType.CritHurtAddRatio:
			this.CritHurtAddRatio = value;
			return;
		case GameData.AttrType.ParryRatio:
			this.ParryRatio = value;
			return;
		case GameData.AttrType.DeparryRatio:
			this.DeparryRatio = value;
			return;
		case GameData.AttrType.ParryHurtDeRatio:
			this.ParryHurtDeRatio = value;
			return;
		case (GameData.AttrType)1314:
		case (GameData.AttrType)1321:
		case (GameData.AttrType)1322:
		case (GameData.AttrType)1327:
		case (GameData.AttrType)1328:
		case GameData.AttrType.OnlineTime:
		case GameData.AttrType.Vp:
		case GameData.AttrType.ExpAddRate:
			IL_A6:
			switch (type)
			{
			case GameData.AttrType.Lv:
				this.Lv = value;
				return;
			case GameData.AttrType.Exp:
				this.Exp = (long)value;
				return;
			case GameData.AttrType.ExpLmt:
				this.ExpLmt = (long)value;
				return;
			case GameData.AttrType.Energy:
				this.Energy = value;
				return;
			case GameData.AttrType.EnergyLmt:
				this.EnergyLmt = value;
				return;
			case GameData.AttrType.Hp:
			case (GameData.AttrType)1014:
				IL_EE:
				switch (type)
				{
				case GameData.AttrType.WaterBuffAddProbAddAmend:
					this.WaterBuffAddProbAddAmend = value;
					return;
				case (GameData.AttrType)1222:
				case (GameData.AttrType)1223:
					IL_10A:
					switch (type)
					{
					case GameData.AttrType.ThunderBuffAddProbAddAmend:
						this.ThunderBuffAddProbAddAmend = value;
						return;
					case (GameData.AttrType)1232:
					case (GameData.AttrType)1233:
						IL_126:
						if (type == GameData.AttrType.MoveSpeed)
						{
							this.MoveSpeed = value;
							return;
						}
						if (type == GameData.AttrType.ActSpeed)
						{
							this.ActSpeed = value;
							return;
						}
						if (type == GameData.AttrType.Atk)
						{
							this.Atk = value;
							return;
						}
						if (type == GameData.AttrType.AtkMulAmend)
						{
							this.AtkMulAmend = value;
							return;
						}
						if (type == GameData.AttrType.ActPointRecoverSpeedAmend)
						{
							this.ActPointRecoverSpeedAmend = value;
							return;
						}
						if (type == GameData.AttrType.HpLmt)
						{
							this.HpLmt = (long)value;
							return;
						}
						if (type == GameData.AttrType.SuckBloodScale)
						{
							this.SuckBloodScale = value;
							return;
						}
						if (type != GameData.AttrType.Defence)
						{
							return;
						}
						this.Defence = value;
						return;
					case GameData.AttrType.ThunderBuffDurTimeAddAmend:
						this.ThunderBuffDurTimeAddAmend = value;
						return;
					}
					goto IL_126;
				case GameData.AttrType.WaterBuffDurTimeAddAmend:
					this.WaterBuffDurTimeAddAmend = value;
					return;
				}
				goto IL_10A;
			case GameData.AttrType.Fighting:
				this.Fighting = (long)value;
				return;
			case GameData.AttrType.Diamond:
				this.Diamond = value;
				return;
			case GameData.AttrType.Gold:
				this.Gold = (long)value;
				return;
			case GameData.AttrType.VipLv:
				this.VipLv = value;
				return;
			case GameData.AttrType.RechargeDiamond:
				this.RechargeDiamond = value;
				return;
			case GameData.AttrType.Honor:
				this.Honor = value;
				return;
			case GameData.AttrType.CompetitiveCurrency:
				this.CompetitiveCurrency = value;
				return;
			case GameData.AttrType.SkillPoint:
				this.SkillPoint = (long)value;
				return;
			}
			goto IL_EE;
		case GameData.AttrType.HurtAddRatio:
			this.HurtAddRatio = value;
			return;
		case GameData.AttrType.HurtDeRatio:
			this.HurtDeRatio = value;
			return;
		case GameData.AttrType.PveHurtAddRatio:
			this.PveHurtAddRatio = value;
			return;
		case GameData.AttrType.PveHurtDeRatio:
			this.PveHurtDeRatio = value;
			return;
		case GameData.AttrType.PvpHurtAddRatio:
			this.PvpHurtAddRatio = value;
			return;
		case GameData.AttrType.PvpHurtDeRatio:
			this.PvpHurtDeRatio = value;
			return;
		case GameData.AttrType.DefMulAmend:
			this.DefMulAmend = value;
			return;
		case GameData.AttrType.HpLmtMulAmend:
			this.HpLmtMulAmend = value;
			return;
		case GameData.AttrType.PveAtkMulAmend:
			this.PveAtkMulAmend = value;
			return;
		case GameData.AttrType.PvpAtkMulAmend:
			this.PvpAtkMulAmend = value;
			return;
		case GameData.AttrType.VpLmt:
			this.VpLmt = value;
			return;
		case GameData.AttrType.VpLmtMulAmend:
			this.VpLmtMulAmend = value;
			return;
		case GameData.AttrType.VpResume:
			this.VpResume = value;
			return;
		case GameData.AttrType.VpAtk:
			this.VpAtk = value;
			return;
		case GameData.AttrType.VpAtkMulAmend:
			this.VpAtkMulAmend = value;
			return;
		case GameData.AttrType.IdleVpResume:
			this.IdleVpResume = value;
			return;
		case GameData.AttrType.HealIncreasePercent:
			this.HealIncreasePercent = value;
			return;
		case GameData.AttrType.CritAddValue:
			this.CritAddValue = value;
			return;
		case GameData.AttrType.HpRestore:
			this.HpRestore = value;
			return;
		case GameData.AttrType.Reputation:
			this.Reputation = (long)value;
			return;
		}
		goto IL_A6;
	}

	public override void SetValue(GameData.AttrType type, long value, bool isFirstTry)
	{
		switch (type)
		{
		case GameData.AttrType.PveAtk:
			this.PveAtk = (int)value;
			return;
		case GameData.AttrType.PvpAtk:
			this.PvpAtk = (int)value;
			return;
		case GameData.AttrType.HitRatio:
			this.HitRatio = (int)value;
			return;
		case GameData.AttrType.DodgeRatio:
			this.DodgeRatio = (int)value;
			return;
		case GameData.AttrType.CritRatio:
			this.CritRatio = (int)value;
			return;
		case GameData.AttrType.DecritRatio:
			this.DecritRatio = (int)value;
			return;
		case GameData.AttrType.CritHurtAddRatio:
			this.CritHurtAddRatio = (int)value;
			return;
		case GameData.AttrType.ParryRatio:
			this.ParryRatio = (int)value;
			return;
		case GameData.AttrType.DeparryRatio:
			this.DeparryRatio = (int)value;
			return;
		case GameData.AttrType.ParryHurtDeRatio:
			this.ParryHurtDeRatio = (int)value;
			return;
		case (GameData.AttrType)1314:
		case (GameData.AttrType)1321:
		case (GameData.AttrType)1322:
		case (GameData.AttrType)1327:
		case (GameData.AttrType)1328:
		case GameData.AttrType.OnlineTime:
		case GameData.AttrType.Vp:
		case GameData.AttrType.ExpAddRate:
			IL_A6:
			switch (type)
			{
			case GameData.AttrType.Lv:
				this.Lv = (int)value;
				return;
			case GameData.AttrType.Exp:
				this.Exp = value;
				return;
			case GameData.AttrType.ExpLmt:
				this.ExpLmt = value;
				return;
			case GameData.AttrType.Energy:
				this.Energy = (int)value;
				return;
			case GameData.AttrType.EnergyLmt:
				this.EnergyLmt = (int)value;
				return;
			case GameData.AttrType.Hp:
			case (GameData.AttrType)1014:
				IL_EE:
				switch (type)
				{
				case GameData.AttrType.WaterBuffAddProbAddAmend:
					this.WaterBuffAddProbAddAmend = (int)value;
					return;
				case (GameData.AttrType)1222:
				case (GameData.AttrType)1223:
					IL_10A:
					switch (type)
					{
					case GameData.AttrType.ThunderBuffAddProbAddAmend:
						this.ThunderBuffAddProbAddAmend = (int)value;
						return;
					case (GameData.AttrType)1232:
					case (GameData.AttrType)1233:
						IL_126:
						if (type == GameData.AttrType.MoveSpeed)
						{
							this.MoveSpeed = (int)value;
							return;
						}
						if (type == GameData.AttrType.ActSpeed)
						{
							this.ActSpeed = (int)value;
							return;
						}
						if (type == GameData.AttrType.Atk)
						{
							this.Atk = (int)value;
							return;
						}
						if (type == GameData.AttrType.AtkMulAmend)
						{
							this.AtkMulAmend = (int)value;
							return;
						}
						if (type == GameData.AttrType.ActPointRecoverSpeedAmend)
						{
							this.ActPointRecoverSpeedAmend = (int)value;
							return;
						}
						if (type == GameData.AttrType.HpLmt)
						{
							this.HpLmt = value;
							return;
						}
						if (type == GameData.AttrType.SuckBloodScale)
						{
							this.SuckBloodScale = (int)value;
							return;
						}
						if (type != GameData.AttrType.Defence)
						{
							return;
						}
						this.Defence = (int)value;
						return;
					case GameData.AttrType.ThunderBuffDurTimeAddAmend:
						this.ThunderBuffDurTimeAddAmend = (int)value;
						return;
					}
					goto IL_126;
				case GameData.AttrType.WaterBuffDurTimeAddAmend:
					this.WaterBuffDurTimeAddAmend = (int)value;
					return;
				}
				goto IL_10A;
			case GameData.AttrType.Fighting:
				this.Fighting = value;
				return;
			case GameData.AttrType.Diamond:
				this.Diamond = (int)value;
				return;
			case GameData.AttrType.Gold:
				this.Gold = value;
				return;
			case GameData.AttrType.VipLv:
				this.VipLv = (int)value;
				return;
			case GameData.AttrType.RechargeDiamond:
				this.RechargeDiamond = (int)value;
				return;
			case GameData.AttrType.Honor:
				this.Honor = (int)value;
				return;
			case GameData.AttrType.CompetitiveCurrency:
				this.CompetitiveCurrency = (int)value;
				return;
			case GameData.AttrType.SkillPoint:
				this.SkillPoint = value;
				return;
			}
			goto IL_EE;
		case GameData.AttrType.HurtAddRatio:
			this.HurtAddRatio = (int)value;
			return;
		case GameData.AttrType.HurtDeRatio:
			this.HurtDeRatio = (int)value;
			return;
		case GameData.AttrType.PveHurtAddRatio:
			this.PveHurtAddRatio = (int)value;
			return;
		case GameData.AttrType.PveHurtDeRatio:
			this.PveHurtDeRatio = (int)value;
			return;
		case GameData.AttrType.PvpHurtAddRatio:
			this.PvpHurtAddRatio = (int)value;
			return;
		case GameData.AttrType.PvpHurtDeRatio:
			this.PvpHurtDeRatio = (int)value;
			return;
		case GameData.AttrType.DefMulAmend:
			this.DefMulAmend = (int)value;
			return;
		case GameData.AttrType.HpLmtMulAmend:
			this.HpLmtMulAmend = (int)value;
			return;
		case GameData.AttrType.PveAtkMulAmend:
			this.PveAtkMulAmend = (int)value;
			return;
		case GameData.AttrType.PvpAtkMulAmend:
			this.PvpAtkMulAmend = (int)value;
			return;
		case GameData.AttrType.VpLmt:
			this.VpLmt = (int)value;
			return;
		case GameData.AttrType.VpLmtMulAmend:
			this.VpLmtMulAmend = (int)value;
			return;
		case GameData.AttrType.VpResume:
			this.VpResume = (int)value;
			return;
		case GameData.AttrType.VpAtk:
			this.VpAtk = (int)value;
			return;
		case GameData.AttrType.VpAtkMulAmend:
			this.VpAtkMulAmend = (int)value;
			return;
		case GameData.AttrType.IdleVpResume:
			this.IdleVpResume = (int)value;
			return;
		case GameData.AttrType.HealIncreasePercent:
			this.HealIncreasePercent = (int)value;
			return;
		case GameData.AttrType.CritAddValue:
			this.CritAddValue = (int)value;
			return;
		case GameData.AttrType.HpRestore:
			this.HpRestore = (int)value;
			return;
		case GameData.AttrType.Reputation:
			this.Reputation = value;
			return;
		}
		goto IL_A6;
	}

	public override long GetValue(GameData.AttrType type)
	{
		switch (type)
		{
		case GameData.AttrType.PveAtk:
			return (long)this.PveAtk;
		case GameData.AttrType.PvpAtk:
			return (long)this.PvpAtk;
		case GameData.AttrType.HitRatio:
			return (long)this.HitRatio;
		case GameData.AttrType.DodgeRatio:
			return (long)this.DodgeRatio;
		case GameData.AttrType.CritRatio:
			return (long)this.CritRatio;
		case GameData.AttrType.DecritRatio:
			return (long)this.DecritRatio;
		case GameData.AttrType.CritHurtAddRatio:
			return (long)this.CritHurtAddRatio;
		case GameData.AttrType.ParryRatio:
			return (long)this.ParryRatio;
		case GameData.AttrType.DeparryRatio:
			return (long)this.DeparryRatio;
		case GameData.AttrType.ParryHurtDeRatio:
			return (long)this.ParryHurtDeRatio;
		case (GameData.AttrType)1314:
		case (GameData.AttrType)1321:
		case (GameData.AttrType)1322:
		case (GameData.AttrType)1327:
		case (GameData.AttrType)1328:
		case GameData.AttrType.OnlineTime:
		case GameData.AttrType.Vp:
		case GameData.AttrType.ExpAddRate:
			IL_A6:
			switch (type)
			{
			case GameData.AttrType.Lv:
				return (long)this.Lv;
			case GameData.AttrType.Exp:
				return this.Exp;
			case GameData.AttrType.ExpLmt:
				return this.ExpLmt;
			case GameData.AttrType.Energy:
				return (long)this.Energy;
			case GameData.AttrType.EnergyLmt:
				return (long)this.EnergyLmt;
			case GameData.AttrType.Hp:
			case (GameData.AttrType)1014:
				IL_EE:
				switch (type)
				{
				case GameData.AttrType.WaterBuffAddProbAddAmend:
					return (long)this.WaterBuffAddProbAddAmend;
				case (GameData.AttrType)1222:
				case (GameData.AttrType)1223:
					IL_10A:
					switch (type)
					{
					case GameData.AttrType.ThunderBuffAddProbAddAmend:
						return (long)this.ThunderBuffAddProbAddAmend;
					case (GameData.AttrType)1232:
					case (GameData.AttrType)1233:
						IL_126:
						if (type == GameData.AttrType.MoveSpeed)
						{
							return (long)this.MoveSpeed;
						}
						if (type == GameData.AttrType.ActSpeed)
						{
							return (long)this.ActSpeed;
						}
						if (type == GameData.AttrType.Atk)
						{
							return (long)this.Atk;
						}
						if (type == GameData.AttrType.AtkMulAmend)
						{
							return (long)this.AtkMulAmend;
						}
						if (type == GameData.AttrType.RealMoveSpeed)
						{
							return (long)this.RealMoveSpeed;
						}
						if (type == GameData.AttrType.RealActionSpeed)
						{
							return (long)this.RealActionSpeed;
						}
						if (type == GameData.AttrType.ActPointRecoverSpeedAmend)
						{
							return (long)this.ActPointRecoverSpeedAmend;
						}
						if (type == GameData.AttrType.HpLmt)
						{
							return this.HpLmt;
						}
						if (type == GameData.AttrType.SuckBloodScale)
						{
							return (long)this.SuckBloodScale;
						}
						if (type != GameData.AttrType.Defence)
						{
							Debug.LogError("未找到属性值:" + type);
							return 0L;
						}
						return (long)this.Defence;
					case GameData.AttrType.ThunderBuffDurTimeAddAmend:
						return (long)this.ThunderBuffDurTimeAddAmend;
					}
					goto IL_126;
				case GameData.AttrType.WaterBuffDurTimeAddAmend:
					return (long)this.WaterBuffDurTimeAddAmend;
				}
				goto IL_10A;
			case GameData.AttrType.Fighting:
				return this.Fighting;
			case GameData.AttrType.Diamond:
				return (long)this.Diamond;
			case GameData.AttrType.Gold:
				return this.Gold;
			case GameData.AttrType.VipLv:
				return (long)this.VipLv;
			case GameData.AttrType.RechargeDiamond:
				return (long)this.RechargeDiamond;
			case GameData.AttrType.Honor:
				return (long)this.Honor;
			case GameData.AttrType.CompetitiveCurrency:
				return (long)this.CompetitiveCurrency;
			case GameData.AttrType.SkillPoint:
				return this.SkillPoint;
			}
			goto IL_EE;
		case GameData.AttrType.HurtAddRatio:
			return (long)this.HurtAddRatio;
		case GameData.AttrType.HurtDeRatio:
			return (long)this.HurtDeRatio;
		case GameData.AttrType.PveHurtAddRatio:
			return (long)this.PveHurtAddRatio;
		case GameData.AttrType.PveHurtDeRatio:
			return (long)this.PveHurtDeRatio;
		case GameData.AttrType.PvpHurtAddRatio:
			return (long)this.PvpHurtAddRatio;
		case GameData.AttrType.PvpHurtDeRatio:
			return (long)this.PvpHurtDeRatio;
		case GameData.AttrType.DefMulAmend:
			return (long)this.DefMulAmend;
		case GameData.AttrType.HpLmtMulAmend:
			return (long)this.HpLmtMulAmend;
		case GameData.AttrType.PveAtkMulAmend:
			return (long)this.PveAtkMulAmend;
		case GameData.AttrType.PvpAtkMulAmend:
			return (long)this.PvpAtkMulAmend;
		case GameData.AttrType.VpLmt:
			return (long)this.VpLmt;
		case GameData.AttrType.VpLmtMulAmend:
			return (long)this.VpLmtMulAmend;
		case GameData.AttrType.VpResume:
			return (long)this.VpResume;
		case GameData.AttrType.VpAtk:
			return (long)this.VpAtk;
		case GameData.AttrType.VpAtkMulAmend:
			return (long)this.VpAtkMulAmend;
		case GameData.AttrType.IdleVpResume:
			return (long)this.IdleVpResume;
		case GameData.AttrType.HealIncreasePercent:
			return (long)this.HealIncreasePercent;
		case GameData.AttrType.CritAddValue:
			return (long)this.CritAddValue;
		case GameData.AttrType.HpRestore:
			return (long)this.HpRestore;
		case GameData.AttrType.Reputation:
			return this.Reputation;
		}
		goto IL_A6;
	}
}
