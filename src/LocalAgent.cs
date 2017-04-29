using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalAgent
{
	protected static ILocalDimension LocalDimensionLiaison;

	protected static ILocalBattleData LocalBattleDataLiaison;

	public static void Intergrate(ILocalDimension theLocalInstanceWorldLiaison, ILocalBattleData theLocalBattleDataLiaison)
	{
		LocalAgent.LocalDimensionLiaison = theLocalInstanceWorldLiaison;
		LocalAgent.LocalBattleDataLiaison = theLocalBattleDataLiaison;
	}

	public static Skill GetSkillDataByID(int id)
	{
		return DataReader<Skill>.Get(id);
	}

	public static Effect GetEffectDataByID(int id)
	{
		return DataReader<Effect>.Get(id);
	}

	public static Buff GetBuffDataByID(int id)
	{
		return DataReader<Buff>.Get(id);
	}

	public static Condition GetConditionDataByID(int id)
	{
		return DataReader<Condition>.Get(id);
	}

	public static EntityParent GetEntityByID(long id)
	{
		return EntityWorld.Instance.GetEntityByID(id);
	}

	public static void AppDirectAttrChangeByTemplateID(BattleBaseAttrs entityBase, int templateID, bool isAdd = true)
	{
		if (entityBase == null)
		{
			return;
		}
		if (templateID == 0)
		{
			return;
		}
		if (isAdd)
		{
			entityBase.AddValuesByTemplateID(templateID);
		}
		else
		{
			entityBase.RemoveValuesByTemplateID(templateID);
		}
	}

	public static void Update(float deltaTime)
	{
		if (LocalAgent.LocalDimensionLiaison != null)
		{
			LocalAgent.LocalDimensionLiaison.Update(deltaTime);
		}
		if (LocalAgent.LocalBattleDataLiaison != null)
		{
			LocalAgent.LocalBattleDataLiaison.Update(deltaTime);
		}
	}

	public static Vector3 GetSpawnPosition(int groupID)
	{
		if (LocalAgent.LocalDimensionLiaison == null)
		{
			return Vector3.get_zero();
		}
		return LocalAgent.LocalDimensionLiaison.GetSpawnPosition(groupID);
	}

	public static bool GetEntityUsable(EntityParent entity, bool isCommunicateMix)
	{
		return entity != null && entity.IsFighting && !LocalAgent.GetSpiritIsDead(entity, isCommunicateMix);
	}

	public static bool GetEntityCalculatable(EntityParent entity, bool isCommunicateMix)
	{
		return (LocalAgent.LocalBattleDataLiaison == null || LocalAgent.LocalBattleDataLiaison.IsEnableCalculate) && entity != null && entity.IsFighting && !LocalAgent.GetSpiritIsDead(entity, isCommunicateMix) && entity.BattleBaseAttrs != null;
	}

	public static bool GetEntityIsCurable(EntityParent entity, bool isCommunicateMix)
	{
		return LocalAgent.GetEntityCalculatable(entity, isCommunicateMix) && !entity.IsIncurable;
	}

	public static long GetSpiritCurHp(EntityParent entity, bool isCommunicateMix)
	{
		return (!isCommunicateMix) ? ((LocalAgent.LocalDimensionLiaison != null) ? LocalAgent.LocalDimensionLiaison.GetSpiritCurHp(entity) : 0L) : entity.Hp;
	}

	public static void SetSpiritCurHp(EntityParent entity, long curHp, bool isCommunicateMix)
	{
		if (!isCommunicateMix && LocalAgent.LocalDimensionLiaison != null)
		{
			LocalAgent.LocalDimensionLiaison.SetSpiritCurHp(entity, curHp);
		}
	}

	public static bool GetSpiritIsDead(EntityParent entity, bool isCommunicateMix)
	{
		return (!isCommunicateMix) ? (LocalAgent.LocalDimensionLiaison != null && LocalAgent.LocalDimensionLiaison.GetSpiritIsDead(entity)) : entity.IsDead;
	}

	public static XDict<int, LocalDimensionPetSpirit> GetPetSpiritByOwnerID(long ownerID)
	{
		if (LocalAgent.LocalDimensionLiaison == null)
		{
			return null;
		}
		return LocalAgent.LocalDimensionLiaison.GetPetSpiritByOwnerID(ownerID);
	}

	public static void SummonPet(long ownerID, LocalDimensionPetSpirit spirit)
	{
		if (LocalAgent.LocalDimensionLiaison == null)
		{
			return;
		}
		LocalAgent.LocalDimensionLiaison.SummonPet(ownerID, spirit);
	}

	public static void ReleasePet(LocalDimensionPetSpirit spirit, bool isDead = false)
	{
		if (LocalAgent.LocalDimensionLiaison == null)
		{
			return;
		}
		LocalAgent.LocalDimensionLiaison.ReleasePet(spirit, isDead);
	}

	public static void RemovePetSummonRitualSkill(long ownerID, LocalDimensionPetSpirit spirit)
	{
		if (LocalAgent.LocalDimensionLiaison == null)
		{
			return;
		}
		LocalAgent.LocalDimensionLiaison.RemovePetSummonRitualSkill(ownerID, spirit);
	}

	public static void SummonMonster(int monsterTypeID, int monsterLevel, long ownerID, int camp, int pointGroupID, Quaternion casterRotation, List<int> offset)
	{
		if (LocalAgent.LocalDimensionLiaison == null)
		{
			return;
		}
		LocalAgent.LocalDimensionLiaison.SummonMonster(monsterTypeID, monsterLevel, ownerID, camp, pointGroupID, casterRotation, offset);
	}

	public static void SummonMonster(int monsterTypeID, int monsterLevel, long ownerID, int camp, Vector3 pos)
	{
		if (LocalAgent.LocalDimensionLiaison == null)
		{
			return;
		}
		LocalAgent.LocalDimensionLiaison.SummonMonster(monsterTypeID, monsterLevel, ownerID, camp, pos);
	}

	public static void AddGlobalBuff(EntityParent target)
	{
		if (LocalAgent.LocalBattleDataLiaison == null)
		{
			return;
		}
		LocalAgent.LocalBattleDataLiaison.AddGlobalBuff(target);
	}

	public static void AppClearBuff(long id)
	{
		if (LocalAgent.LocalBattleDataLiaison == null)
		{
			return;
		}
		LocalAgent.LocalBattleDataLiaison.AppClearBuff(id);
	}

	public static bool CheckBuffByTargetIDAndBuffID(long targetID, int buffID)
	{
		return LocalAgent.LocalBattleDataLiaison != null && LocalAgent.LocalBattleDataLiaison.CheckBuffByTargetIDAndBuffID(targetID, buffID);
	}

	public static bool CheckBuffTypeContainOther(Buff buffData, long targetID)
	{
		return LocalAgent.LocalBattleDataLiaison != null && LocalAgent.LocalBattleDataLiaison.CheckBuffTypeContainOther(buffData, targetID);
	}

	public static List<ClientDrvBuffInfo> MakeClientDrvBuffInfo(long id)
	{
		if (LocalAgent.LocalBattleDataLiaison == null)
		{
			return new List<ClientDrvBuffInfo>();
		}
		return LocalAgent.LocalBattleDataLiaison.MakeClientDrvBuffInfo(id);
	}

	public static void ClearFuseDataByEntityID(long id, bool isDeadDefuse)
	{
		if (LocalAgent.LocalBattleDataLiaison == null)
		{
			return;
		}
		LocalAgent.LocalBattleDataLiaison.AppClearFuse(id, isDeadDefuse);
	}
}
