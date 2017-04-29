using EntitySubSystem;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using XEngineActor;
using XEngineCommand;
using XNetwork;

public class EntitySelf : EntityPlayer, IClientBaseAttr, IClientBattleBaseAttr, IStandardBaseAttr
{
	private int fx_uid_levelup;

	protected CityBaseAttrs cityBaseAttrs = new CityBaseAttrs();

	protected int gender;

	protected int loginTimes;

	protected int loginTime;

	protected int lastLoginTime;

	protected int lastLogoutTime;

	protected float totalFuseTimePlus;

	protected int totalBeginActPoint;

	protected IndexList<int, int> staticSkillInfo = new IndexList<int, int>();

	protected Action victoryCallBack;

	protected Dictionary<GameData.AttrType, string> particularCityAttrChangedEvents = new Dictionary<GameData.AttrType, string>();

	protected bool isNavigating;

	protected bool isNavNeedChangeScene;

	protected int navToScene;

	protected Vector3 navToPosition = Vector3.get_zero();

	protected float navEndRadius;

	protected Action navEndCallback;

	protected int attrFighting;

	protected int skillFighting;

	protected int talentFighting;

	protected int petFighting;

	protected bool isSynchronizingServerBattle;

	public override CityBaseAttrs CityBaseAttrs
	{
		get
		{
			return this.cityBaseAttrs;
		}
	}

	public override int MoveSpeed
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.MoveSpeed;
			}
			return this.CityBaseAttrs.MoveSpeed;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.MoveSpeed = value;
			}
			else
			{
				this.CityBaseAttrs.MoveSpeed = value;
			}
		}
	}

	public override int ActSpeed
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.ActSpeed;
			}
			return this.CityBaseAttrs.ActSpeed;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.ActSpeed = value;
			}
			else
			{
				this.CityBaseAttrs.ActSpeed = value;
			}
		}
	}

	public override int Lv
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.Lv;
			}
			return this.CityBaseAttrs.Lv;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.Lv = value;
			}
			else
			{
				this.CityBaseAttrs.Lv = value;
			}
		}
	}

	public override long Fighting
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.Fighting;
			}
			return this.CityBaseAttrs.Fighting;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.Fighting = value;
			}
			else
			{
				this.CityBaseAttrs.Fighting = value;
			}
		}
	}

	public override int VipLv
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.VipLv;
			}
			return this.CityBaseAttrs.VipLv;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.VipLv = value;
			}
			else
			{
				this.CityBaseAttrs.VipLv = value;
			}
		}
	}

	public override int Atk
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.Atk;
			}
			return this.CityBaseAttrs.Atk;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.Atk = value;
			}
			else
			{
				this.CityBaseAttrs.Atk = value;
			}
		}
	}

	public override int Defence
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.Defence;
			}
			return this.CityBaseAttrs.Defence;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.Defence = value;
			}
			else
			{
				this.CityBaseAttrs.Defence = value;
			}
		}
	}

	public override long HpLmt
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.HpLmt;
			}
			return this.CityBaseAttrs.HpLmt;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.HpLmt = value;
			}
			else
			{
				this.CityBaseAttrs.HpLmt = value;
			}
		}
	}

	public override int PveAtk
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PveAtk;
			}
			return this.CityBaseAttrs.PveAtk;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PveAtk = value;
			}
			else
			{
				this.CityBaseAttrs.PveAtk = value;
			}
		}
	}

	public override int PvpAtk
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PvpAtk;
			}
			return this.CityBaseAttrs.PvpAtk;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PvpAtk = value;
			}
			else
			{
				this.CityBaseAttrs.PvpAtk = value;
			}
		}
	}

	public override int HitRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.HitRatio;
			}
			return this.CityBaseAttrs.HitRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.HitRatio = value;
			}
			else
			{
				this.CityBaseAttrs.HitRatio = value;
			}
		}
	}

	public override int DodgeRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.DodgeRatio;
			}
			return this.CityBaseAttrs.DodgeRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.DodgeRatio = value;
			}
			else
			{
				this.CityBaseAttrs.DodgeRatio = value;
			}
		}
	}

	public override int CritRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.CritRatio;
			}
			return this.CityBaseAttrs.CritRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.CritRatio = value;
			}
			else
			{
				this.CityBaseAttrs.CritRatio = value;
			}
		}
	}

	public override int DecritRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.DecritRatio;
			}
			return this.CityBaseAttrs.DecritRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.DecritRatio = value;
			}
			else
			{
				this.CityBaseAttrs.DecritRatio = value;
			}
		}
	}

	public override int CritHurtAddRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.CritHurtAddRatio;
			}
			return this.CityBaseAttrs.CritHurtAddRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.CritHurtAddRatio = value;
			}
			else
			{
				this.CityBaseAttrs.CritHurtAddRatio = value;
			}
		}
	}

	public override int ParryRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.ParryRatio;
			}
			return this.CityBaseAttrs.ParryRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.ParryRatio = value;
			}
			else
			{
				this.CityBaseAttrs.ParryRatio = value;
			}
		}
	}

	public override int DeparryRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.DeparryRatio;
			}
			return this.CityBaseAttrs.DeparryRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.DeparryRatio = value;
			}
			else
			{
				this.CityBaseAttrs.DeparryRatio = value;
			}
		}
	}

	public override int ParryHurtDeRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.ParryHurtDeRatio;
			}
			return this.CityBaseAttrs.ParryHurtDeRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.ParryHurtDeRatio = value;
			}
			else
			{
				this.CityBaseAttrs.ParryHurtDeRatio = value;
			}
		}
	}

	public override int SuckBloodScale
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.SuckBloodScale;
			}
			return this.CityBaseAttrs.SuckBloodScale;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.SuckBloodScale = value;
			}
			else
			{
				this.CityBaseAttrs.SuckBloodScale = value;
			}
		}
	}

	public override int HurtAddRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.HurtAddRatio;
			}
			return this.CityBaseAttrs.HurtAddRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.HurtAddRatio = value;
			}
			else
			{
				this.CityBaseAttrs.HurtAddRatio = value;
			}
		}
	}

	public override int HurtDeRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.HurtDeRatio;
			}
			return this.CityBaseAttrs.HurtDeRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.HurtDeRatio = value;
			}
			else
			{
				this.CityBaseAttrs.HurtDeRatio = value;
			}
		}
	}

	public override int PveHurtAddRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PveHurtAddRatio;
			}
			return this.CityBaseAttrs.PveHurtAddRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PveHurtAddRatio = value;
			}
			else
			{
				this.CityBaseAttrs.PveHurtAddRatio = value;
			}
		}
	}

	public override int PveHurtDeRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PveHurtDeRatio;
			}
			return this.CityBaseAttrs.PveHurtDeRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PveHurtDeRatio = value;
			}
			else
			{
				this.CityBaseAttrs.PveHurtDeRatio = value;
			}
		}
	}

	public override int PvpHurtAddRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PvpHurtAddRatio;
			}
			return this.CityBaseAttrs.PvpHurtAddRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PvpHurtAddRatio = value;
			}
			else
			{
				this.CityBaseAttrs.PvpHurtAddRatio = value;
			}
		}
	}

	public override int PvpHurtDeRatio
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PvpHurtDeRatio;
			}
			return this.CityBaseAttrs.PvpHurtDeRatio;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PvpHurtDeRatio = value;
			}
			else
			{
				this.CityBaseAttrs.PvpHurtDeRatio = value;
			}
		}
	}

	public override int AtkMulAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.AtkMulAmend;
			}
			return this.CityBaseAttrs.AtkMulAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.AtkMulAmend = value;
			}
			else
			{
				this.CityBaseAttrs.AtkMulAmend = value;
			}
		}
	}

	public override int DefMulAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.DefMulAmend;
			}
			return this.CityBaseAttrs.DefMulAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.DefMulAmend = value;
			}
			else
			{
				this.CityBaseAttrs.DefMulAmend = value;
			}
		}
	}

	public override int HpLmtMulAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.HpLmtMulAmend;
			}
			return this.CityBaseAttrs.HpLmtMulAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.HpLmtMulAmend = value;
			}
			else
			{
				this.CityBaseAttrs.HpLmtMulAmend = value;
			}
		}
	}

	public override int PveAtkMulAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PveAtkMulAmend;
			}
			return this.CityBaseAttrs.PveAtkMulAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PveAtkMulAmend = value;
			}
			else
			{
				this.CityBaseAttrs.PveAtkMulAmend = value;
			}
		}
	}

	public override int PvpAtkMulAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.PvpAtkMulAmend;
			}
			return this.CityBaseAttrs.PvpAtkMulAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.PvpAtkMulAmend = value;
			}
			else
			{
				this.CityBaseAttrs.PvpAtkMulAmend = value;
			}
		}
	}

	public override int ActPointRecoverSpeedAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.ActPointRecoverSpeedAmend;
			}
			return this.CityBaseAttrs.ActPointRecoverSpeedAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.ActPointRecoverSpeedAmend = value;
			}
			else
			{
				this.CityBaseAttrs.ActPointRecoverSpeedAmend = value;
			}
		}
	}

	public override int VpLmt
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.VpLmt;
			}
			return this.CityBaseAttrs.VpLmt;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.VpLmt = value;
			}
			else
			{
				this.CityBaseAttrs.VpLmt = value;
			}
		}
	}

	public override int VpLmtMulAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.VpLmtMulAmend;
			}
			return this.CityBaseAttrs.VpLmtMulAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.VpLmtMulAmend = value;
			}
			else
			{
				this.CityBaseAttrs.VpLmtMulAmend = value;
			}
		}
	}

	public override int VpAtk
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.VpAtk;
			}
			return this.CityBaseAttrs.VpAtk;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.VpAtk = value;
			}
			else
			{
				this.CityBaseAttrs.VpAtk = value;
			}
		}
	}

	public override int VpAtkMulAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.VpAtkMulAmend;
			}
			return this.CityBaseAttrs.VpAtkMulAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.VpAtkMulAmend = value;
			}
			else
			{
				this.CityBaseAttrs.VpAtkMulAmend = value;
			}
		}
	}

	public override int VpResume
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.VpResume;
			}
			return this.CityBaseAttrs.VpResume;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.VpResume = value;
			}
			else
			{
				this.CityBaseAttrs.VpResume = value;
			}
		}
	}

	public override int IdleVpResume
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.IdleVpResume;
			}
			return this.CityBaseAttrs.IdleVpResume;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.IdleVpResume = value;
			}
			else
			{
				this.CityBaseAttrs.IdleVpResume = value;
			}
		}
	}

	public override int WaterBuffAddProbAddAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.WaterBuffAddProbAddAmend;
			}
			return this.CityBaseAttrs.WaterBuffAddProbAddAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.WaterBuffAddProbAddAmend = value;
			}
			else
			{
				this.CityBaseAttrs.WaterBuffAddProbAddAmend = value;
			}
		}
	}

	public override int WaterBuffDurTimeAddAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.WaterBuffDurTimeAddAmend;
			}
			return this.CityBaseAttrs.WaterBuffDurTimeAddAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.WaterBuffDurTimeAddAmend = value;
			}
			else
			{
				this.CityBaseAttrs.WaterBuffDurTimeAddAmend = value;
			}
		}
	}

	public override int ThunderBuffAddProbAddAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.ThunderBuffAddProbAddAmend;
			}
			return this.CityBaseAttrs.ThunderBuffAddProbAddAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.ThunderBuffAddProbAddAmend = value;
			}
			else
			{
				this.CityBaseAttrs.ThunderBuffAddProbAddAmend = value;
			}
		}
	}

	public override int ThunderBuffDurTimeAddAmend
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.ThunderBuffDurTimeAddAmend;
			}
			return this.CityBaseAttrs.ThunderBuffDurTimeAddAmend;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.ThunderBuffDurTimeAddAmend = value;
			}
			else
			{
				this.CityBaseAttrs.ThunderBuffDurTimeAddAmend = value;
			}
		}
	}

	public override int HealIncreasePercent
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.HealIncreasePercent;
			}
			return this.CityBaseAttrs.HealIncreasePercent;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.HealIncreasePercent = value;
			}
			else
			{
				this.CityBaseAttrs.HealIncreasePercent = value;
			}
		}
	}

	public override int CritAddValue
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.CritAddValue;
			}
			return this.CityBaseAttrs.CritAddValue;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.CritAddValue = value;
			}
			else
			{
				this.CityBaseAttrs.CritAddValue = value;
			}
		}
	}

	public override int HpRestore
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.HpRestore;
			}
			return this.CityBaseAttrs.HpRestore;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.HpRestore = value;
			}
			else
			{
				this.CityBaseAttrs.HpRestore = value;
			}
		}
	}

	public override long Exp
	{
		get
		{
			return this.CityBaseAttrs.Exp;
		}
		set
		{
			this.CityBaseAttrs.Exp = value;
		}
	}

	public override long ExpLmt
	{
		get
		{
			return this.CityBaseAttrs.ExpLmt;
		}
		set
		{
			this.CityBaseAttrs.ExpLmt = value;
		}
	}

	public override int Energy
	{
		get
		{
			return this.CityBaseAttrs.Energy;
		}
		set
		{
			this.CityBaseAttrs.Energy = value;
		}
	}

	public override int EnergyLmt
	{
		get
		{
			return this.CityBaseAttrs.EnergyLmt;
		}
		set
		{
			this.CityBaseAttrs.EnergyLmt = value;
		}
	}

	public override int Diamond
	{
		get
		{
			return this.CityBaseAttrs.Diamond;
		}
		set
		{
			this.CityBaseAttrs.Diamond = value;
		}
	}

	public override long Gold
	{
		get
		{
			return this.CityBaseAttrs.Gold;
		}
		set
		{
			this.CityBaseAttrs.Gold = value;
		}
	}

	public override int RechargeDiamond
	{
		get
		{
			return this.CityBaseAttrs.RechargeDiamond;
		}
		set
		{
			this.CityBaseAttrs.RechargeDiamond = value;
		}
	}

	public override int Honor
	{
		get
		{
			return this.CityBaseAttrs.Honor;
		}
		set
		{
			this.CityBaseAttrs.Honor = value;
		}
	}

	public override int CompetitiveCurrency
	{
		get
		{
			return this.CityBaseAttrs.CompetitiveCurrency;
		}
		set
		{
			this.CityBaseAttrs.CompetitiveCurrency = value;
		}
	}

	public override long SkillPoint
	{
		get
		{
			return this.CityBaseAttrs.SkillPoint;
		}
		set
		{
			this.CityBaseAttrs.SkillPoint = value;
		}
	}

	public override long Reputation
	{
		get
		{
			return this.CityBaseAttrs.Reputation;
		}
		set
		{
			this.CityBaseAttrs.Reputation = value;
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
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.RealMoveSpeed;
			}
			return this.CityBaseAttrs.RealMoveSpeed;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.RealMoveSpeed = value;
			}
			else
			{
				this.CityBaseAttrs.RealMoveSpeed = value;
			}
		}
	}

	public override int RealActionSpeed
	{
		get
		{
			if (this.IsInBattle)
			{
				return this.BattleBaseAttrs.RealActionSpeed;
			}
			return this.CityBaseAttrs.RealActionSpeed;
		}
		set
		{
			if (this.IsInBattle)
			{
				this.BattleBaseAttrs.RealActionSpeed = value;
			}
			else
			{
				this.CityBaseAttrs.RealActionSpeed = value;
			}
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
			int typeID = this.typeID;
			base.TypeID = value;
			if (typeID != value)
			{
				EventDispatcher.Broadcast(AIManagerEvent.ResetSelfAI);
			}
		}
	}

	public override string Name
	{
		get
		{
			return base.Name;
		}
		set
		{
			base.Name = value;
			BattleBlackboard.Instance.SelfName = value;
		}
	}

	public override int TitleID
	{
		get
		{
			return TitleManager.Instance.OwnCurrId;
		}
		set
		{
			TitleManager.Instance.OwnCurrId = value;
			EventDispatcher.Broadcast<long, int>("BillboardManager.Title", base.ID, value);
		}
	}

	public override string GuildTitle
	{
		get
		{
			return GuildManager.Instance.GetGuildTitle();
		}
	}

	public override int Camp
	{
		get
		{
			return base.Camp;
		}
		set
		{
			int camp = base.Camp;
			base.Camp = value;
			InstanceManager.CurrentInstance.SelfCampChanged(camp, value);
		}
	}

	public override int ModelID
	{
		get
		{
			return this.modelID;
		}
		set
		{
			int modelID = this.modelID;
			base.ModelID = value;
			BattleBlackboard.Instance.SelfHead = base.IconID;
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
			if (value == null)
			{
				this.decorations.career = 0;
				this.decorations.modelId = 0;
				this.decorations.equipIds.Clear();
				this.decorations.petUUId = 0L;
				this.decorations.petId = 0;
				this.decorations.petStar = 0;
				this.decorations.wingId = 0;
				this.decorations.wingHidden = true;
				this.decorations.wingLv = 0;
				this.decorations.gogokNum = 0;
				this.decorations.fashions.Clear();
				return;
			}
			bool flag = this.decorations.petUUId == value.petUUId && this.decorations.petId == value.petId && this.decorations.petStar == value.petStar;
			long petUUId = this.decorations.petUUId;
			this.decorations.career = value.career;
			this.decorations.modelId = value.modelId;
			this.decorations.equipIds.Clear();
			if (value.equipIds != null)
			{
				this.decorations.equipIds.AddRange(value.equipIds);
			}
			this.decorations.petUUId = value.petUUId;
			this.decorations.petId = value.petId;
			this.decorations.petStar = value.petStar;
			this.decorations.wingId = value.wingId;
			this.decorations.gogokNum = value.gogokNum;
			this.decorations.wingLv = value.wingLv;
			this.decorations.wingHidden = value.wingHidden;
			this.decorations.fashions.Clear();
			if (value.fashions != null)
			{
				this.decorations.fashions.AddRange(value.fashions);
			}
			base.ExteriorUnit.WrapSetData(delegate
			{
				this.TypeID = this.decorations.career;
				this.ModelID = this.decorations.modelId;
				base.ExteriorUnit.EquipIDs = this.decorations.equipIds;
				base.ExteriorUnit.WingID = WingManager.GetWingModel(this.decorations.wingId, this.decorations.wingLv);
				base.ExteriorUnit.IsHideWing = this.decorations.wingHidden;
				base.ExteriorUnit.FashionIDs = this.decorations.fashions;
				base.ExteriorUnit.Gogok = this.decorations.gogokNum;
			});
			if (!flag && base.Actor)
			{
				if (petUUId != 0L)
				{
					this.RemoveFollowPet(petUUId);
				}
				if (this.decorations.petUUId != 0L && this.decorations.petId != 0 && !this.IsInBattle)
				{
					this.AddFollowPet(this.decorations.petUUId, this.decorations.petId, this.decorations.petStar);
				}
			}
			EventDispatcher.Broadcast(ExteriorArithmeticUnitEvent.UnitChanged);
			EventDispatcher.Broadcast<int, int, bool>(ExteriorArithmeticUnitEvent.WingChanged, this.decorations.wingId, this.decorations.wingLv, this.decorations.wingHidden);
		}
	}

	public override List<long> OwnedIDs
	{
		get
		{
			return base.OwnedIDs;
		}
		set
		{
			base.OwnedIDs = value;
			if (value == null)
			{
				BattleBlackboard.Instance.SelfPetNum = 0;
			}
			else
			{
				BattleBlackboard.Instance.SelfPetNum = value.get_Count();
			}
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
			BattleBlackboard.Instance.SelfFuse = value;
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
				if (base.Actor)
				{
					(base.Actor as ActorSelf).CheckIsCancelCurSkill();
				}
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
				if (base.Actor)
				{
					(base.Actor as ActorSelf).CheckIsCancelCurSkill();
				}
				this.IsAssault = false;
			}
		}
	}

	public override bool IsAssault
	{
		get
		{
			return base.IsAssault;
		}
		set
		{
			if (base.IsAssault && !value && base.Actor)
			{
				GlobalBattleNetwork.Instance.SendEndAssault(base.ID, base.Actor.FixTransform.get_position(), base.Actor.FixTransform.get_forward());
			}
			if (value)
			{
				if (base.AITarget != null && base.AITarget.Actor)
				{
					EventDispatcher.Broadcast<bool, Vector3>(ShaderEffectEvent.PLAYER_ASSAULT, true, base.AITarget.Actor.FixTransform.get_position());
				}
			}
			else
			{
				EventDispatcher.Broadcast<bool, Vector3>(ShaderEffectEvent.PLAYER_ASSAULT, false, Vector3.get_zero());
			}
			base.IsAssault = value;
			if (!value && this.IsSynchronizingServerBattle)
			{
				this.IsSynchronizingServerBattle = false;
			}
		}
	}

	public override bool IsHitMoving
	{
		get
		{
			return base.IsHitMoving;
		}
		set
		{
			base.IsHitMoving = value;
			if (!value && this.IsSynchronizingServerBattle)
			{
				this.IsSynchronizingServerBattle = false;
			}
		}
	}

	public override bool IsSkillInTrustee
	{
		get
		{
			return base.IsSkillInTrustee;
		}
		set
		{
			base.IsSkillInTrustee = value;
			if (!value && this.IsSynchronizingServerBattle)
			{
				this.IsSynchronizingServerBattle = false;
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
				if (base.Actor)
				{
					(base.Actor as ActorSelf).CheckIsCancelCurSkill();
				}
				this.IsAssault = false;
			}
			base.IsDead = value;
			BattleBlackboard.Instance.SelfDead = value;
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
			if (value)
			{
				this.SetBackUpBattleBaseAttrs();
				this.ResetBattleState();
			}
			else
			{
				UIStackManager.Instance.RemoveStack("BattleUI");
				this.RecoverBackUpBattleBaseAttrs();
				this.HidePointer(0);
				this.ResetBattleState();
				this.Camp = 0;
				this.Hp = this.RealHpLmt;
			}
			if (base.Actor)
			{
				base.Actor.ClearFreezeFrame();
				base.Actor.ClearStraight();
				base.Actor.StopAllMove();
				base.Actor.ChangeAction("idle", true, false, 1f, 0, 0, string.Empty);
			}
			UIQueueManager.Instance.CheckQueue(PopCondition.BackToCity);
		}
	}

	public override bool IsEntitySelfType
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
			return 1;
		}
	}

	public int Gender
	{
		get
		{
			return this.gender;
		}
		set
		{
			this.gender = value;
		}
	}

	public int LoginTimes
	{
		get
		{
			return this.loginTimes;
		}
		set
		{
			this.loginTimes = value;
		}
	}

	public int LoginTime
	{
		get
		{
			return this.loginTime;
		}
		set
		{
			this.loginTime = value;
		}
	}

	public int LastLoginTime
	{
		get
		{
			return this.lastLoginTime;
		}
		set
		{
			this.lastLoginTime = value;
		}
	}

	public int LastLogoutTime
	{
		get
		{
			return this.lastLogoutTime;
		}
		set
		{
			this.lastLogoutTime = value;
		}
	}

	public float TotalFuseTimePlus
	{
		get
		{
			return this.totalFuseTimePlus;
		}
		set
		{
			this.totalFuseTimePlus = value;
		}
	}

	public int TotalBeginActPoint
	{
		get
		{
			return this.totalBeginActPoint;
		}
		set
		{
			this.totalBeginActPoint = value;
		}
	}

	protected virtual IndexList<int, int> StaticSkillInfo
	{
		get
		{
			return this.staticSkillInfo;
		}
		set
		{
			if (value == null)
			{
				this.staticSkillInfo = null;
				return;
			}
			this.staticSkillInfo.Clear();
			using (Dictionary<int, int>.Enumerator enumerator = value.GetPairPart().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> current = enumerator.get_Current();
					this.staticSkillInfo.Add(current.get_Key(), current.get_Value());
				}
			}
			List<int> singlePart = value.GetSinglePart();
			for (int i = 0; i < singlePart.get_Count(); i++)
			{
				this.staticSkillInfo.AddValue(singlePart.get_Item(i));
			}
		}
	}

	public bool IsNavigating
	{
		get
		{
			return this.isNavigating;
		}
		set
		{
			this.isNavigating = value;
		}
	}

	public bool IsNavNeedChangeScene
	{
		get
		{
			return this.isNavNeedChangeScene;
		}
		set
		{
			this.isNavNeedChangeScene = value;
		}
	}

	public int NavToScene
	{
		get
		{
			return this.navToScene;
		}
		set
		{
			this.navToScene = value;
		}
	}

	public Vector3 NavToPosition
	{
		get
		{
			return this.navToPosition;
		}
		set
		{
			this.navToPosition = value;
		}
	}

	public float NavEndRadius
	{
		get
		{
			return this.navEndRadius;
		}
		set
		{
			this.navEndRadius = value;
		}
	}

	public Action NavEndCallback
	{
		get
		{
			return this.navEndCallback;
		}
		set
		{
			this.navEndCallback = value;
		}
	}

	public int AttrFighting
	{
		get
		{
			return this.attrFighting;
		}
		protected set
		{
			this.attrFighting = value;
		}
	}

	public int SkillFighting
	{
		get
		{
			return this.skillFighting;
		}
		protected set
		{
			this.skillFighting = value;
		}
	}

	public int TalentFighting
	{
		get
		{
			return this.talentFighting;
		}
		protected set
		{
			this.talentFighting = value;
		}
	}

	public int PetFighting
	{
		get
		{
			return this.petFighting;
		}
		protected set
		{
			this.petFighting = value;
		}
	}

	public override bool IsClientDominate
	{
		get
		{
			return true;
		}
	}

	public bool IsSynchronizingServerBattle
	{
		get
		{
			return this.isSynchronizingServerBattle;
		}
		set
		{
			if (this.isSynchronizingServerBattle && !value)
			{
				ReconnectManager.Instance.EndSynchronizeServerBattle();
			}
			this.isSynchronizingServerBattle = value;
		}
	}

	public EntitySelf()
	{
		this.cityBaseAttrs.AttrChangedDelegate = new Action<GameData.AttrType, long, long>(this.OnAttrChanged);
		this.battleBaseAttrs.AttrChangedDelegate = new Action<GameData.AttrType, long, long>(this.OnAttrChanged);
		this.InitParticularCityAttrChangedEvents();
		this.InitCommonInfoUpdateData();
	}

	protected void InitParticularCityAttrChangedEvents()
	{
		this.particularCityAttrChangedEvents.Clear();
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.Lv, ParticularCityAttrChangedEvent.LvChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.Fighting, ParticularCityAttrChangedEvent.FightingChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.VipLv, ParticularCityAttrChangedEvent.VipLvChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.Energy, ParticularCityAttrChangedEvent.EnergyChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.Diamond, ParticularCityAttrChangedEvent.DiamondChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.Gold, ParticularCityAttrChangedEvent.GoldChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.RechargeDiamond, ParticularCityAttrChangedEvent.RechargeDiamondChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.CompetitiveCurrency, ParticularCityAttrChangedEvent.CompetitiveCurrencyChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.SkillPoint, ParticularCityAttrChangedEvent.SkillPointChanged);
		this.particularCityAttrChangedEvents.Add(GameData.AttrType.Reputation, ParticularCityAttrChangedEvent.ReputationChanged);
	}

	public void SetDataByRoleInfo(RoleInfo info)
	{
		base.ID = info.roleId;
		this.Name = info.roleName;
		this.Lv = info.lv;
		this.Exp = info.exp;
		this.ExpLmt = info.expLmt;
		this.TypeID = info.typeId;
		this.ModelID = info.modelId;
		this.Gender = (int)info.gender;
		this.TypeRank = info.rankValue;
		this.LoginTimes = info.loginTimes;
		this.LoginTime = info.loginTime;
		this.LastLoginTime = info.lastLoginTime;
		this.LastLogoutTime = info.lastLogoutTime;
		this.SetMapObjCityInfo(info.cityInfo);
	}

	public void RefreshStaticSkills(List<BattleSkillInfo> skills)
	{
		this.ClearStaticSkill();
		for (int i = 0; i < skills.get_Count(); i++)
		{
			if (skills.get_Item(i).skillIdx > 0)
			{
				this.AddStaticSkill(skills.get_Item(i).skillIdx, skills.get_Item(i).skillId);
			}
			else
			{
				this.AddStaticSkill(skills.get_Item(i).skillId);
			}
		}
	}

	public void SetDataByMapObjInfo(MapObjInfo info, bool isClientCreate, int theClientModelID)
	{
		base.ExteriorUnit.ClientModelID = theClientModelID;
		this.SetDataByMapObjInfo(info, isClientCreate);
	}

	public override void SetDataByMapObjInfo(MapObjInfo info, bool isClientCreate = false)
	{
		base.SetDataByMapObjInfo(info, isClientCreate);
		if (base.Actor)
		{
			if (this.IsInBattle)
			{
				base.Actor.EnableBattleAutoSendPos();
			}
			else
			{
				base.Actor.EnableCityAutoSendPos();
			}
		}
		InstanceManager.CurrentInstance.SelfInitEnd(this);
	}

	public override void SetDataByMapObjInfoOnRelive(MapObjInfo info, bool isClientCreate = false)
	{
		base.SetDataByMapObjInfo(info, isClientCreate);
		if (base.Actor)
		{
			if (this.IsInBattle)
			{
				base.Actor.EnableBattleAutoSendPos();
			}
			else
			{
				base.Actor.EnableCityAutoSendPos();
			}
		}
	}

	protected override void InitManager()
	{
		this.m_subSystems.Add("SelfAI", new SelfAIManager());
		this.m_subSystems.Add("SelfBattle", new SelfBattleManager());
		this.m_subSystems.Add("SelfBuff", new SelfBuffManager());
		this.m_subSystems.Add("SelfCondition", new SelfConditionManager());
		this.m_subSystems.Add("SelfSkill", new SelfSkillManager());
		this.m_subSystems.Add("SelfWarning", new SelfWarningManager());
		base.InitManager();
	}

	protected override void AddNetworkListener()
	{
		base.AddNetworkListener();
		this.AddSceneChangeListener();
		this.AddDisplayAttrChangeListener();
		this.AddFightingChangeListener();
		base.AddNetworkMoveRotateTeleportGoUpAndDownListener();
		this.AddNetworkDecorationChangedListener();
		this.AddNetworkCommonInfoChangedListener();
	}

	protected override void RemoveNetworkListener()
	{
		base.RemoveNetworkListener();
		this.RemoveSceneChangeListener();
		this.RemoveDisplayAttrChangeListener();
		this.RemoveFightingChangeListener();
		base.RemoveNetworkMoveRotateTeleportGoUpAndDownListener();
		this.RemoveNetworkDecorationChangedListener();
		this.RemoveNetworkCommonInfoChangedListener();
	}

	public override void InitActorState()
	{
		base.InitActorState();
		this.AddFollowPet(this.Decorations.petUUId, this.Decorations.petId, this.Decorations.petStar);
		XInputManager.Instance.StartSendPosAndDir();
		if (this.IsInBattle)
		{
			base.Actor.EnableBattleAutoSendPos();
		}
		else
		{
			base.Actor.EnableCityAutoSendPos();
			this.HidePointer(0);
		}
	}

	public void ChangeToBattleMode()
	{
		this.IsInBattle = true;
		EventDispatcher.Broadcast<bool>("EventNames.EnableFloating", true);
	}

	public void ChangeToCityMode()
	{
		this.IsInBattle = false;
		XInputManager.Instance.StartSendPosAndDir();
		EventDispatcher.Broadcast<bool>("EventNames.EnableFloating", true);
	}

	protected void SetBackUpBattleBaseAttrs()
	{
		this.BattleBaseAttrs.AssignAllAttrs(this.CityBaseAttrs);
	}

	protected void RecoverBackUpBattleBaseAttrs()
	{
		this.BattleBaseAttrs.ResetAllAttrs();
		this.oldRealHpLmt = 0L;
	}

	public override void AddNetworkAttrChangeListener()
	{
		NetworkManager.AddListenEvent<AllAttrPush>(new NetCallBackMethod<AllAttrPush>(this.UpdateAllAttr));
		base.AddNetworkAttrChangeListener();
	}

	public override void RemoveNetworkAttrChangeListener()
	{
		NetworkManager.RemoveListenEvent<AllAttrPush>(new NetCallBackMethod<AllAttrPush>(this.UpdateAllAttr));
		base.RemoveNetworkAttrChangeListener();
	}

	protected override void UpdateAttr(short state, RoleAttrChangedNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (base.ID != down.id)
		{
			return;
		}
		for (int i = 0; i < down.attrs.get_Count(); i++)
		{
			this.CityBaseAttrs.SetValue((GameData.AttrType)down.attrs.get_Item(i).attrType, down.attrs.get_Item(i).attrValue, true);
			this.BroadcastParticularCityAttrChanged((GameData.AttrType)down.attrs.get_Item(i).attrType);
		}
		EventDispatcher.Broadcast(EventNames.OnGetRoleAttrChangedNty);
	}

	protected void UpdateAllAttr(short state, AllAttrPush down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (down.info == null)
		{
			return;
		}
		if (base.ID != down.info.id)
		{
			return;
		}
		List<GameData.AttrType> attrTypes = this.CheckParticularCityAttrChanged(down.info);
		base.SetDataByMapObjInfo(down.info, false);
		this.BroadcastParticularCityAttrChanged(attrTypes);
		EventDispatcher.Broadcast(EventNames.OnGetRoleAttrChangedNty);
	}

	protected List<GameData.AttrType> CheckParticularCityAttrChanged(MapObjInfo mapObjInfo)
	{
		List<GameData.AttrType> list = new List<GameData.AttrType>();
		if (mapObjInfo.battleInfo != null)
		{
			if (mapObjInfo.otherInfo.Fighting != this.CityBaseAttrs.Fighting)
			{
				list.Add(GameData.AttrType.Fighting);
			}
			if (mapObjInfo.otherInfo.VipLv != this.CityBaseAttrs.VipLv)
			{
				list.Add(GameData.AttrType.VipLv);
			}
		}
		if (mapObjInfo.cityInfo != null)
		{
			if (mapObjInfo.cityInfo.Energy != this.CityBaseAttrs.Energy)
			{
				list.Add(GameData.AttrType.Energy);
			}
			if (mapObjInfo.cityInfo.Diamond != this.CityBaseAttrs.Diamond)
			{
				list.Add(GameData.AttrType.Diamond);
			}
			if (mapObjInfo.cityInfo.Gold != this.CityBaseAttrs.Gold)
			{
				list.Add(GameData.AttrType.Gold);
			}
			if (mapObjInfo.cityInfo.RechargeDiamond != this.CityBaseAttrs.RechargeDiamond)
			{
				list.Add(GameData.AttrType.RechargeDiamond);
			}
			if (mapObjInfo.cityInfo.CompetitiveCurrency != this.CityBaseAttrs.CompetitiveCurrency)
			{
				list.Add(GameData.AttrType.CompetitiveCurrency);
			}
			if ((long)mapObjInfo.cityInfo.SkillPoint != this.CityBaseAttrs.SkillPoint)
			{
				list.Add(GameData.AttrType.SkillPoint);
			}
			if ((long)mapObjInfo.cityInfo.Reputation != this.CityBaseAttrs.Reputation)
			{
				list.Add(GameData.AttrType.Reputation);
			}
		}
		return list;
	}

	protected void BroadcastParticularCityAttrChanged(List<GameData.AttrType> attrTypes)
	{
		if (attrTypes.get_Count() == 0)
		{
			return;
		}
		for (int i = 0; i < attrTypes.get_Count(); i++)
		{
			this.BroadcastParticularCityAttrChanged(attrTypes.get_Item(i));
		}
	}

	protected void BroadcastParticularCityAttrChanged(GameData.AttrType attrType)
	{
		if (!this.particularCityAttrChangedEvents.ContainsKey(attrType))
		{
			return;
		}
		EventDispatcher.Broadcast(this.particularCityAttrChangedEvents.get_Item(attrType));
	}

	public override void OnAttrChanged(GameData.AttrType attrType, long oldValue, long attrValue)
	{
		base.OnAttrChanged(attrType, oldValue, attrValue);
		this.ShowAttrChange(attrType, oldValue, attrValue);
	}

	protected void ShowAttrChange(GameData.AttrType type, long oldValue, long newValue)
	{
		if (type == GameData.AttrType.Diamond || type == GameData.AttrType.Gold || type == GameData.AttrType.Exp)
		{
			if (FloatTextAddManager.IsItemTipOn && oldValue < newValue && oldValue != 0L)
			{
				FloatTextAddManager.Instance.AddFloatText(string.Format("{0}x{1}", AttrUtility.GetAttrName(type), newValue - oldValue), Color.get_white());
			}
		}
	}

	protected void AddDisplayAttrChangeListener()
	{
		NetworkManager.AddListenEvent<DisplayAttrChangedNty>(new NetCallBackMethod<DisplayAttrChangedNty>(this.DisplayAttrChange));
	}

	protected void RemoveDisplayAttrChangeListener()
	{
		NetworkManager.RemoveListenEvent<DisplayAttrChangedNty>(new NetCallBackMethod<DisplayAttrChangedNty>(this.DisplayAttrChange));
	}

	protected void DisplayAttrChange(short state, DisplayAttrChangedNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (down.id != base.ID)
		{
			return;
		}
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		for (int i = 0; i < down.attrs.get_Count(); i++)
		{
			list.Add(down.attrs.get_Item(i).attrType);
			list2.Add(down.attrs.get_Item(i).attrValue);
		}
		DisplayAttrManager.Instance.AddFloatText(list, list2);
	}

	protected void AddFightingChangeListener()
	{
		NetworkManager.AddListenEvent<FightingChangedNty>(new NetCallBackMethod<FightingChangedNty>(this.ChangeFighting));
	}

	protected void RemoveFightingChangeListener()
	{
		NetworkManager.RemoveListenEvent<FightingChangedNty>(new NetCallBackMethod<FightingChangedNty>(this.ChangeFighting));
	}

	protected void ChangeFighting(short state, FightingChangedNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		if (down.oldFighting == 0)
		{
			return;
		}
		if (down.oldFighting == down.newFighting)
		{
			return;
		}
		switch (down.fightingType)
		{
		case FightingChangedNty.FightingType.Panel:
			if (down.asTips && down.oldFighting < down.newFighting)
			{
				FightingUpUI fightingUpUI = UIManagerControl.Instance.OpenUI("FightingUpUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush) as FightingUpUI;
				fightingUpUI.SetPowerUp((long)down.oldFighting, (long)down.newFighting, null);
				EventDispatcher.Broadcast<int, int>(EventNames.FightingChangedNty, down.oldFighting, down.newFighting);
			}
			break;
		case FightingChangedNty.FightingType.RoleAttr:
			this.AttrFighting = down.newFighting;
			break;
		case FightingChangedNty.FightingType.RoleSkill:
			this.SkillFighting = down.newFighting;
			break;
		case FightingChangedNty.FightingType.RoleTalent:
			this.TalentFighting = down.newFighting;
			break;
		case FightingChangedNty.FightingType.PetInBattle:
			this.PetFighting = down.newFighting;
			break;
		}
	}

	protected override void OnChangeLv(long oldValue, long newValue)
	{
		if (oldValue != newValue)
		{
			HeadInfoManager.Instance.SetName(1, base.ID, (int)newValue, this.Name);
			if (base.Actor)
			{
				this.fx_uid_levelup = FXManager.Instance.PlayFXIfNOExist(this.fx_uid_levelup, 900, base.Actor.FixTransform, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, 0, null, null, 1f);
			}
		}
	}

	protected override void OnChangeHp(long oldValue, long newValue)
	{
		base.OnChangeHp(oldValue, newValue);
		InstanceManager.CurrentInstance.SelfHpChange(this);
	}

	protected override void OnChangeRealHpLmt(long oldValue, long newValue)
	{
		base.OnChangeRealHpLmt(oldValue, newValue);
		InstanceManager.CurrentInstance.SelfHpLmtChange(this);
	}

	protected override void OnChangeActPoint(long oldValue, long newValue)
	{
		base.OnChangeActPoint(oldValue, newValue);
		if (base.IsClientCreate && !this.IsFuse)
		{
			LocalInstanceHandler.Instance.CheckPet(base.ID, (float)this.ActPoint);
		}
		InstanceManager.CurrentInstance.SelfActPointChange(this);
	}

	protected override void OnChangeActPointLmt(long oldValue, long newValue)
	{
		base.OnChangeActPointLmt(oldValue, newValue);
		InstanceManager.CurrentInstance.SelfActPointLmtChange(this);
	}

	protected void AddStaticSkill(int skillIndex, int skillID)
	{
		this.StaticSkillInfo.Add(skillIndex, skillID);
	}

	protected void AddStaticSkill(int skillID)
	{
		this.StaticSkillInfo.AddValue(skillID);
	}

	protected void RemoveStaticSkill(int skillID)
	{
		this.StaticSkillInfo.Remove(skillID);
	}

	protected void ClearStaticSkill()
	{
		this.StaticSkillInfo.Clear();
	}

	public List<int> GetStaticSkillSinglePart()
	{
		return this.StaticSkillInfo.GetSinglePart();
	}

	public Dictionary<int, int> GetStaticSkillPairPart()
	{
		return this.StaticSkillInfo.GetPairPart();
	}

	public override void AddSkill(int skillIndex, int skillID, int skillLevel, List<BattleSkillAttrAdd> skillAttrChange)
	{
		if (this.SkillInfo.GetPairPart().ContainsKey(skillIndex) && this.SkillInfo.GetPairPart().get_Item(skillIndex) == skillID)
		{
			return;
		}
		base.AddSkill(skillIndex, skillID, skillLevel, skillAttrChange);
		if (skillIndex == 0)
		{
			return;
		}
		Skill skill = DataReader<Skill>.Get(skillID);
		KeyValuePair<float, DateTime> skillCDByID = base.GetSkillManager().GetSkillCDByID(skillID);
		if (skill != null)
		{
			BattleBlackboard.Instance.AddSelfSkill(skillIndex, skill.icon, (float)(0 - skill.actionPoint - base.GetSkillActionPointVariationByType(skill.skilltype)), skillCDByID);
		}
	}

	public override void RemoveSkill(int skillID)
	{
		int skillIndexByID = base.GetSkillIndexByID(skillID);
		base.RemoveSkill(skillID);
		if (skillIndexByID == 0)
		{
			return;
		}
		BattleBlackboard.Instance.RemoveSelfSkill(skillIndexByID);
	}

	public override void ClearSkill()
	{
		base.ClearSkill();
		BattleBlackboard.Instance.ClearSelfSkill();
	}

	protected override void SetIsFighting(bool state)
	{
	}

	protected override void SetIsCloseRenderer(bool state)
	{
		if (!base.Actor)
		{
			return;
		}
		base.SetRenderers(!state);
		base.Actor.RedererLayerState = ((!state) ? 0 : 1);
		if (state)
		{
			SoundManager.Instance.StopPlayer(base.ID);
		}
		base.ShowWeapon(!state);
	}

	protected override void InitCommonInfoUpdateData()
	{
		base.InitCommonInfoGroupUpdateData(new Action<XDict<KVType.ENUM, int>, XDict<KVType.ENUM, string>>(this.UpdateClientModelID), new KVType.ENUM[1]);
	}

	protected void UpdateClientModelID(XDict<KVType.ENUM, int> pair1, XDict<KVType.ENUM, string> pair2)
	{
		if (!pair1.ContainsKey(KVType.ENUM.TransformId))
		{
			return;
		}
		base.ExteriorUnit.ClientModelID = pair1[KVType.ENUM.TransformId];
	}

	public override void Teleport(Vector3 toPos)
	{
		if (base.Actor)
		{
			if (FollowCamera.instance)
			{
				Vector3 relativeDisplacement = FollowCamera.instance.TeleportingStart();
				base.Actor.Teleport(toPos);
				FollowCamera.instance.TeleportingEnd(relativeDisplacement);
			}
			else
			{
				Debug.LogError("找左总: FollowCamera.instance == null");
				base.Actor.Teleport(toPos);
			}
		}
		else
		{
			base.Pos = toPos;
		}
	}

	protected override void NetMoveEx(short state, MapObjectMoveNtyEx down = null)
	{
	}

	protected override void NetRotateEx(short state, MapObjectRotateNtyEx down = null)
	{
	}

	protected override void UpdateActor(Action logicCallback = null)
	{
		this.DestoryOldSelfActor();
		ActorSelf selfActor = EntityWorld.Instance.GetSelfActor(this.FixModelID);
		this.SetSelfActor(selfActor, logicCallback);
	}

	protected void DestoryOldSelfActor()
	{
		if (base.Actor)
		{
			ActorParent actor = base.Actor;
			base.Pos = actor.FixTransform.get_position();
			base.Dir = actor.FixTransform.get_forward();
			actor.FixTransform.set_position(actor.FixTransform.get_position() + new Vector3(0f, -5000f, 0f));
			actor.DestroyScript();
		}
		else
		{
			EntityWorld.Instance.CancelGetSelfActorAsync(base.AsyncLoadID);
		}
	}

	protected void SetSelfActor(ActorSelf actorSelf, Action logicCallback)
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.FixModelID);
		EntityWorld.Instance.ActSelf = actorSelf;
		EntityWorld.Instance.TraSelf = actorSelf.FixTransform;
		base.Actor = actorSelf;
		actorSelf.theEntity = this;
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
		EventDispatcher.Broadcast(CameraEvent.SelfRebuild);
		if (logicCallback != null)
		{
			logicCallback.Invoke();
		}
		this.InitActorState();
	}

	protected void TrySetCityPet()
	{
		if (!this.IsInBattle)
		{
			this.AddFollowPet(this.Decorations.petUUId, this.Decorations.petId, this.Decorations.petStar);
		}
	}

	protected void AddFollowPet(long petID, int typeID, int rank)
	{
		if (this.IsInBattle)
		{
			return;
		}
		if (this.decorations.petUUId == 0L || this.decorations.petId == 0)
		{
			return;
		}
		EntityWorld.Instance.CreateEntityCityPet(petID, typeID, rank, this);
	}

	protected void RemoveFollowPet(long petID)
	{
		EntityWorld.Instance.RemoveEntityCityPet(petID);
	}

	public void ApplySwitchScene()
	{
		this.TrySetCityPet();
		base.EquipCustomizationer.RefreshEquipFX();
	}

	protected void AddSceneChangeListener()
	{
		EventDispatcher.AddListener(CityManagerEvent.EnteredCity, new Callback(this.CheckContinueNavigationOnSceneChanged));
	}

	protected void RemoveSceneChangeListener()
	{
		EventDispatcher.RemoveListener(CityManagerEvent.EnteredCity, new Callback(this.CheckContinueNavigationOnSceneChanged));
	}

	public void NavToNPC(int npcSceneID, Vector3 npcPosition, float navRadius = 0f, Action callback = null)
	{
		this.NavigateToPoint(npcPosition, npcSceneID, navRadius, callback);
		if (!base.Actor)
		{
			return;
		}
		if (npcSceneID == MySceneManager.Instance.CurSceneID)
		{
			this.IsNavigating = true;
			base.Actor.MoveToPoint(npcPosition, navRadius, callback);
		}
		else
		{
			Vector3 pos;
			if (!MainTaskManager.Instance.GetHearthPosition(out pos))
			{
				ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(MySceneManager.Instance.CurSceneID);
				if (zhuChengPeiZhi == null)
				{
					return;
				}
				if (zhuChengPeiZhi.transferPoint.get_Count() < 3)
				{
					return;
				}
				pos = new Vector3((float)zhuChengPeiZhi.transferPoint.get_Item(0) * 0.01f, (float)zhuChengPeiZhi.transferPoint.get_Item(1) * 0.01f, (float)zhuChengPeiZhi.transferPoint.get_Item(2) * 0.01f);
			}
			this.IsNavigating = true;
			this.IsNavNeedChangeScene = true;
			this.NavToScene = npcSceneID;
			this.NavToPosition = npcPosition;
			this.NavEndRadius = navRadius;
			this.NavEndCallback = callback;
			base.Actor.MoveToPoint(pos, navRadius, null);
		}
	}

	public void StopNavToNPC()
	{
		Debug.Log("<结束自动导航>");
		this.StopNavigation();
	}

	public bool CheckCancelNavToNPC()
	{
		return this.CheckCancelNavigation();
	}

	public void NavToSameScenePoint(float pointX, float pointZ, float navRadius = 0f, Action callback = null)
	{
		if (!base.Actor)
		{
			return;
		}
		Vector3 targetPosition;
		if (!MySceneManager.GetTerrainPoint(pointX, pointZ, (float)this.floor * 30f, out targetPosition))
		{
			return;
		}
		this.NavigateToPoint(targetPosition, 0, navRadius, callback);
	}

	public void StopNavToSameScenePoint()
	{
		this.StopNavigation();
	}

	public void NavigateToPoint(Vector3 targetPosition, int targetSceneID, float navRadius = 0f, Action callback = null)
	{
		if (!base.Actor)
		{
			return;
		}
		if (targetSceneID == 0 || targetSceneID == MySceneManager.Instance.CurSceneID)
		{
			this.IsNavigating = true;
			base.Actor.MoveToPoint(targetPosition, navRadius, callback);
		}
		else
		{
			Vector3 pos;
			if (!MainTaskManager.Instance.GetHearthPosition(out pos))
			{
				ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(MySceneManager.Instance.CurSceneID);
				if (zhuChengPeiZhi == null)
				{
					return;
				}
				if (zhuChengPeiZhi.transferPoint.get_Count() < 3)
				{
					return;
				}
				pos = new Vector3((float)zhuChengPeiZhi.transferPoint.get_Item(0) * 0.01f, (float)zhuChengPeiZhi.transferPoint.get_Item(1) * 0.01f, (float)zhuChengPeiZhi.transferPoint.get_Item(2) * 0.01f);
			}
			this.IsNavigating = true;
			this.IsNavNeedChangeScene = true;
			this.NavToScene = targetSceneID;
			this.NavToPosition = targetPosition;
			this.NavEndRadius = navRadius;
			this.NavEndCallback = callback;
			base.Actor.MoveToPoint(pos, navRadius, null);
		}
	}

	public void StopNavigation()
	{
		this.IsNavigating = false;
		this.IsNavNeedChangeScene = false;
		base.Actor.StopMoveToPoint();
	}

	public bool CheckCancelNavigation()
	{
		if (!this.IsNavigating)
		{
			return false;
		}
		this.IsNavigating = false;
		this.IsNavNeedChangeScene = false;
		return true;
	}

	protected void CheckContinueNavigationOnSceneChanged()
	{
		if (!this.IsNavNeedChangeScene)
		{
			return;
		}
		if (MySceneManager.Instance.CurSceneID != this.NavToScene)
		{
			return;
		}
		this.IsNavNeedChangeScene = false;
		base.Actor.MoveToPoint(this.NavToPosition, this.NavEndRadius, this.NavEndCallback);
	}

	public override void DebutBattle()
	{
		if (base.GetSkillManager() != null)
		{
			base.GetSkillManager().SetDebutCD();
		}
		if (InstanceManager.IsLocalBattle)
		{
			EnterBattleFieldAnnouncer.Announce(this);
		}
	}

	public void ResetBattleState()
	{
		base.GetAIManager().ResetAIManager();
		base.GetConditionManager().UnregistCondition();
		this.ClearSkillData();
		this.ClearWarningMessage();
		this.ClearBuff();
		this.IsDead = false;
		base.IsFixed = false;
		this.IsStatic = false;
		this.IsCloseRenderer = false;
		this.IsDizzy = false;
		if (base.Actor)
		{
			base.Actor.FrameLayerState = 0;
		}
	}

	public void ReloadClientBattleState()
	{
		if (base.GetSkillManager() != null)
		{
			base.GetSkillManager().ResetData();
			BattleBlackboard.Instance.ClearSelfSkillCD();
			base.GetSkillManager().SetDebutCD();
		}
		this.ClearWarningMessage();
		if (base.Actor)
		{
			base.Actor.FrameLayerState = 0;
		}
	}

	public void ReloadServerBattleState()
	{
		InstanceManager.StopAllClientAI(false);
		if (base.GetSkillManager() != null)
		{
			base.GetSkillManager().ResetData();
			BattleBlackboard.Instance.ClearSelfSkillCD();
			base.GetSkillManager().SetDebutCD();
		}
		this.ClearWarningMessage();
		this.ClearBuff();
		if (base.Actor)
		{
			base.Actor.FrameLayerState = 0;
		}
		BattleBlackboard.Instance.ContinueCombo = false;
	}

	protected void ClearSkillData()
	{
		this.ClearSkill();
		base.ClearSkillLevel();
		base.ClearSkillExtend();
		if (base.GetSkillManager() == null)
		{
			return;
		}
		base.GetSkillManager().ResetData();
	}

	protected void ClearWarningMessage()
	{
		if (base.GetWarningManager() == null)
		{
			return;
		}
		base.GetWarningManager().ClearWarningMessage();
	}

	protected void ClearBuff()
	{
		if (base.GetBuffManager() == null)
		{
			return;
		}
		base.GetBuffManager().ClearBuff();
	}

	public List<long> GetPetInBattle()
	{
		List<long> list = new List<long>();
		for (int i = 0; i < this.OwnedIDs.get_Count(); i++)
		{
			if (PetManager.Instance.MaplistPet.ContainsKey(this.OwnedIDs.get_Item(i)))
			{
				list.Add(this.OwnedIDs.get_Item(i));
			}
		}
		return list;
	}

	public void ShowSelf(bool isShow)
	{
		base.IsVisible = isShow;
	}

	public void HidePointer(int time)
	{
		if (!base.Actor)
		{
			return;
		}
		(base.Actor as ActorSelf).HidePointer(time);
	}

	public void ShowBorn()
	{
		if (base.Actor)
		{
			base.Actor.ChangeAction("born", false, true, 1f, 0, 0, string.Empty);
		}
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
		HPChangeMessage.HPChangeType hpChangeType = data.hpChangeType;
		if (hpChangeType == HPChangeMessage.HPChangeType.KillRecover)
		{
			if (base.Actor)
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
		}
		base.UpdateHpChangeInfluence(data);
	}

	public override void DieBegin()
	{
		InstanceManager.SelfDie();
		base.DieBegin();
	}

	public override void DieEndDefuse()
	{
		base.FusePetID = 0;
		this.ModelID = base.ModelIDBackUp;
		this.SetSkillDic(base.SkillDicBackUp);
		this.IsFuse = false;
		this.UpdateSkill();
		ShaderEffectUtils.SetFade(this.alphaControls, false, null);
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.ENABLE_SCREEN_LENS, false);
	}

	public override void Revive()
	{
		InstanceManager.SelfRelive();
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
		if (base.Actor)
		{
			base.Actor.ChangeAction("idle", false, true, 1f, 0, 0, string.Empty);
		}
		EventDispatcher.Broadcast<long, bool>("BillboardManager.ShowBillboardsInfo", base.ID, !this.IsDead);
		if (InstanceManager.IsLocalBattle)
		{
			EnterBattleFieldAnnouncer.Announce(this);
		}
	}

	public void ShowVictory(Vector2 posePos, Vector3 eulerAngles, int poseFloor = 0, Action act = null)
	{
		if (!this.IsDead && base.Actor)
		{
			this.victoryCallBack = act;
			base.Actor.FixTransform.set_position(PosDirUtility.ToTerrainPoint(posePos, (float)poseFloor * 30f));
			base.Actor.FixTransform.set_eulerAngles(eulerAngles);
			base.Actor.ChangeAction("victory", true, true, 1f, 0, 0, string.Empty);
		}
	}

	public override void VictoryEnd()
	{
		if (this.victoryCallBack == null)
		{
			return;
		}
		this.victoryCallBack.Invoke();
	}

	public void GetRealDrop()
	{
		if (base.Actor)
		{
			base.Actor.GotDrop();
		}
	}

	public void GetFakeDrop()
	{
		if (base.Actor)
		{
			base.Actor.GotDrop();
		}
	}

	public override void InitBillboard(float height, List<int> bloodBarSize)
	{
		BillboardManager.Instance.AddBillboardsInfo(1, base.Actor, height, base.ID, false, true, !this.IsDead);
		HeadInfoManager.Instance.SetName(1, base.ID, this.Lv, this.Name);
		HeadInfoManager.Instance.SetTitle(base.ID, this.TitleID);
		HeadInfoManager.Instance.SetGuildTitle(base.ID, this.GuildTitle);
	}

	public override void ResetBillBoard()
	{
		base.ResetBillBoard();
		for (int i = 0; i < EntityWorld.Instance.AllEntities.Values.get_Count(); i++)
		{
			if (!EntityWorld.Instance.AllEntities.Values.get_Item(i).IsEntitySelfType)
			{
				EntityWorld.Instance.AllEntities.Values.get_Item(i).ResetBillBoard();
			}
		}
	}
}
