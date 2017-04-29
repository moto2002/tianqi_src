using GameData;
using Package;
using System;

public class LocalBattleCalculatorCollector
{
	protected static GameObjectType.ENUM casterServerType;

	protected static GameObjectType.ENUM targetServerType;

	protected static bool isFuse;

	protected static int fusePetID;

	public static void CollectCalculator(EntityParent caster, EntityParent target, int skillID, long calculateValue, BattleDmgTreatRcd.ENUM calculateType)
	{
		if (caster == null)
		{
			return;
		}
		if (target == null)
		{
			return;
		}
		if (skillID == 0)
		{
			return;
		}
		if (caster.IsEntitySelfType || caster.IsEntityPlayerType)
		{
			LocalBattleCalculatorCollector.casterServerType = GameObjectType.ENUM.Role;
		}
		else if (caster.IsEntityPetType)
		{
			LocalBattleCalculatorCollector.casterServerType = GameObjectType.ENUM.Pet;
		}
		else
		{
			if (!caster.IsEntityMonsterType)
			{
				return;
			}
			if (DataReader<Monster>.Get(caster.TypeID).damageCollect != 1)
			{
				return;
			}
			LocalBattleCalculatorCollector.casterServerType = GameObjectType.ENUM.Monster;
		}
		if (target.IsEntitySelfType || target.IsEntityPlayerType)
		{
			LocalBattleCalculatorCollector.targetServerType = GameObjectType.ENUM.Role;
		}
		else if (target.IsEntityPetType)
		{
			LocalBattleCalculatorCollector.targetServerType = GameObjectType.ENUM.Pet;
		}
		else
		{
			if (!target.IsEntityMonsterType)
			{
				return;
			}
			if (DataReader<Monster>.Get(target.TypeID).damageCollect != 1)
			{
				return;
			}
			LocalBattleCalculatorCollector.targetServerType = GameObjectType.ENUM.Monster;
		}
		if (caster.IsEntityPlayerType)
		{
			LocalBattleCalculatorCollector.isFuse = (caster as EntitySelf).IsFuse;
			LocalBattleCalculatorCollector.fusePetID = (caster as EntitySelf).FusePetID;
		}
		else if (caster.IsEntitySelfType)
		{
			LocalBattleCalculatorCollector.isFuse = (caster as EntityPlayer).IsFuse;
			LocalBattleCalculatorCollector.fusePetID = (caster as EntityPlayer).FusePetID;
		}
		else
		{
			LocalBattleCalculatorCollector.isFuse = false;
			LocalBattleCalculatorCollector.fusePetID = 0;
		}
		BattleDmgCollectManager.Instance.CollectDamageHeal(LocalBattleCalculatorCollector.casterServerType, (CampType.ENUM)caster.Camp, caster.IsLogicBoss, caster.ID, caster.OwnerID, caster.TypeID, caster.Name, LocalBattleCalculatorCollector.targetServerType, (CampType.ENUM)target.Camp, target.IsLogicBoss, target.ID, target.OwnerID, target.TypeID, target.Name, calculateType, target.ID, target.OwnerID, skillID, LocalBattleCalculatorCollector.fusePetID, LocalBattleCalculatorCollector.isFuse, calculateValue);
	}
}
