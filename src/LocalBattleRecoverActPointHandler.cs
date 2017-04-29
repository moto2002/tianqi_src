using GameData;
using System;
using System.Collections.Generic;

public class LocalBattleRecoverActPointHandler
{
	public static void UpdateActPoint(float deltaTime)
	{
		List<EntityParent> values = EntityWorld.Instance.GetEntities<EntitySelf>().Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (!values.get_Item(i).IsDead)
			{
				LocalBattleRecoverActPointHandler.RecoverSelfActPoint(values.get_Item(i), deltaTime);
			}
		}
		List<EntityParent> values2 = EntityWorld.Instance.GetEntities<EntityPet>().Values;
		for (int j = 0; j < values2.get_Count(); j++)
		{
			if (!values2.get_Item(j).IsDead)
			{
				if (values2.get_Item(j).IsFighting)
				{
					LocalBattleRecoverActPointHandler.RecoverFightingPetActPoint(values2.get_Item(j), deltaTime);
				}
				else
				{
					LocalBattleRecoverActPointHandler.RecoverRestingPetActPoint(values2.get_Item(j), deltaTime);
				}
			}
		}
		List<EntityParent> values3 = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
		for (int k = 0; k < values3.get_Count(); k++)
		{
			if (!values3.get_Item(k).IsDead)
			{
				LocalBattleRecoverActPointHandler.RecoverMonsterActPoint(values3.get_Item(k), deltaTime);
			}
		}
	}

	protected static void RecoverSelfActPoint(EntityParent target, float deltaTime)
	{
		if (target.ActPoint == target.ActPointLmt)
		{
			return;
		}
		GlobalParams globalParams = DataReader<GlobalParams>.Get("actionpoint_restore_i");
		if (globalParams.value == string.Empty)
		{
			return;
		}
		int num = (int)((double)target.ActPoint + (double)float.Parse(globalParams.value) * 0.001 * (double)deltaTime);
		if (num > target.ActPointLmt)
		{
			num = target.ActPointLmt;
		}
		target.SetValue(AttrType.ActPoint, num, true);
	}

	protected static void RecoverFightingPetActPoint(EntityParent target, float deltaTime)
	{
		if (target.ActPoint == target.ActPointLmt)
		{
			return;
		}
		int num = (int)((double)target.ActPoint + (double)target.ActPointRecoverSpeedAmend * 0.001 / (double)((float)target.Lv + 300f) * (double)deltaTime);
		if (num > target.ActPointLmt)
		{
			num = target.ActPointLmt;
		}
		target.SetValue(AttrType.ActPoint, num, true);
	}

	protected static void RecoverRestingPetActPoint(EntityParent target, float deltaTime)
	{
		if (target.ActPoint == target.ActPointLmt)
		{
			return;
		}
		int num = (int)((double)target.ActPoint + (double)(2 * target.ActPointRecoverSpeedAmend) * 0.001 / (double)((float)target.Lv + 300f) * (double)deltaTime);
		if (num > target.ActPointLmt)
		{
			num = target.ActPointLmt;
		}
		target.SetValue(AttrType.ActPoint, num, true);
	}

	protected static void RecoverMonsterActPoint(EntityParent target, float deltaTime)
	{
		if (target.ActPoint == target.ActPointLmt)
		{
			return;
		}
		if (DataReader<Monster>.Get(target.TypeID) == null)
		{
			return;
		}
		int num = (int)((double)target.ActPoint + (double)target.ActPointRecoverSpeedAmend * 0.001 * (double)deltaTime);
		if (num > target.ActPointLmt)
		{
			num = target.ActPointLmt;
		}
		target.SetValue(AttrType.ActPoint, num, true);
	}
}
