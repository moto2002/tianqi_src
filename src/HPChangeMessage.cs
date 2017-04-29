using Package;
using System;
using UnityEngine;

public struct HPChangeMessage
{
	public enum ModeType
	{
		None,
		Self,
		SelfPet,
		MonsterBySelf,
		MonsterBySelfPet,
		MonsterByMonster
	}

	public enum CauseType
	{
		Attack,
		Buff,
		Elem
	}

	public enum ElementType
	{
		Normal,
		Earth,
		Fire,
		Water,
		Thunder
	}

	public enum HPChangeType
	{
		Normal = 1,
		Critical,
		Parry,
		CriticalAndParry,
		Treat,
		Drain,
		Miss,
		KillRecover,
		Break,
		HpRestore
	}

	public long targetID
	{
		get;
		private set;
	}

	public HPChangeMessage.ModeType modeType
	{
		get;
		private set;
	}

	public HPChangeMessage.CauseType causeType
	{
		get;
		private set;
	}

	public HPChangeMessage.ElementType elementType
	{
		get;
		private set;
	}

	public HPChangeMessage.HPChangeType hpChangeType
	{
		get;
		private set;
	}

	public float hpChangeValue
	{
		get;
		private set;
	}

	public bool hasCasterPosition
	{
		get;
		private set;
	}

	public Vector3 casterPosition
	{
		get;
		private set;
	}

	public static HPChangeMessage GetDamageMessage(long damageValue, BattleAction_Bleed.DmgSrcType causeType, ElemType.ENUM elementType, EntityParent target, EntityParent source, bool isCritical, bool isParry, bool isMiss)
	{
		return new HPChangeMessage
		{
			targetID = target.ID,
			modeType = HPChangeMessage.GetModeType(target, source),
			causeType = HPChangeMessage.GetCauseType(causeType),
			elementType = HPChangeMessage.GetElementType(elementType),
			hpChangeType = HPChangeMessage.GetHPChangeType(isCritical, isParry, isMiss),
			hpChangeValue = (float)damageValue
		};
	}

	public static HPChangeMessage GetTreatMessage(long treatValue, BattleAction_Treat.TreatSrcType causeType, EntityParent target, EntityParent source, bool hasCasterPosition, Vector3 casterPosition)
	{
		return new HPChangeMessage
		{
			targetID = target.ID,
			modeType = HPChangeMessage.GetModeType(target, source),
			hpChangeType = HPChangeMessage.GetHPChangeType(causeType),
			hpChangeValue = (float)treatValue,
			hasCasterPosition = hasCasterPosition,
			casterPosition = casterPosition
		};
	}

	public static HPChangeMessage GetBreakMessage(EntityParent target, EntityParent source)
	{
		return new HPChangeMessage
		{
			targetID = source.ID,
			modeType = HPChangeMessage.GetModeType(target, source),
			hpChangeType = HPChangeMessage.HPChangeType.Break
		};
	}

	private static HPChangeMessage.ModeType GetModeType(EntityParent target, EntityParent source)
	{
		if (target.IsEntitySelfType)
		{
			return HPChangeMessage.ModeType.Self;
		}
		if (target.IsEntityPetType)
		{
			return HPChangeMessage.ModeType.SelfPet;
		}
		if (target.IsEntityMonsterType || target.IsEntityPlayerType)
		{
			if (source != null && source.IsEntitySelfType)
			{
				return HPChangeMessage.ModeType.MonsterBySelf;
			}
			if (source != null && source.IsEntityPetType && source.OwnerID == EntityWorld.Instance.EntSelf.ID)
			{
				return HPChangeMessage.ModeType.MonsterBySelfPet;
			}
			if (source != null && target != null && source.IsEntityMonsterType && target.IsEntityMonsterType)
			{
				return HPChangeMessage.ModeType.MonsterByMonster;
			}
		}
		return HPChangeMessage.ModeType.None;
	}

	private static HPChangeMessage.CauseType GetCauseType(BattleAction_Bleed.DmgSrcType causeType)
	{
		return (HPChangeMessage.CauseType)causeType;
	}

	private static HPChangeMessage.ElementType GetElementType(ElemType.ENUM elementType)
	{
		return (HPChangeMessage.ElementType)elementType;
	}

	private static HPChangeMessage.HPChangeType GetHPChangeType(bool isCritical, bool isParry, bool isMiss)
	{
		if (isCritical)
		{
			if (isParry)
			{
				return HPChangeMessage.HPChangeType.CriticalAndParry;
			}
			return HPChangeMessage.HPChangeType.Critical;
		}
		else
		{
			if (isParry)
			{
				return HPChangeMessage.HPChangeType.Parry;
			}
			if (isMiss)
			{
				return HPChangeMessage.HPChangeType.Miss;
			}
			return HPChangeMessage.HPChangeType.Normal;
		}
	}

	private static HPChangeMessage.HPChangeType GetHPChangeType(BattleAction_Treat.TreatSrcType causeType)
	{
		switch (causeType)
		{
		case BattleAction_Treat.TreatSrcType.Treat:
			return HPChangeMessage.HPChangeType.Treat;
		case BattleAction_Treat.TreatSrcType.SuckBlood:
			return HPChangeMessage.HPChangeType.Drain;
		case BattleAction_Treat.TreatSrcType.KillRecover:
			return HPChangeMessage.HPChangeType.KillRecover;
		case BattleAction_Treat.TreatSrcType.HpRestore:
			return HPChangeMessage.HPChangeType.HpRestore;
		default:
			return HPChangeMessage.HPChangeType.Normal;
		}
	}
}
