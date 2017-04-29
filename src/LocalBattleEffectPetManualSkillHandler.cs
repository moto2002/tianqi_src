using System;
using System.Collections.Generic;

public class LocalBattleEffectPetManualSkillHandler
{
	public static void AppManualSkill(EntityParent caster, List<long> effectTargetIDs, int skillID, bool isCommunicateMix)
	{
		if (caster == null)
		{
			return;
		}
		if (!caster.IsEntitySelfType)
		{
			return;
		}
		XDict<int, LocalDimensionPetSpirit> petSpiritByOwnerID = LocalAgent.GetPetSpiritByOwnerID(caster.ID);
		if (petSpiritByOwnerID == null)
		{
			return;
		}
		if (petSpiritByOwnerID.Count == 0)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < petSpiritByOwnerID.Count; i++)
		{
			if (petSpiritByOwnerID.ElementValueAt(i).manualSkillInfo != null)
			{
				if (petSpiritByOwnerID.ElementValueAt(i).manualSkillInfo.skillId == skillID)
				{
					num = petSpiritByOwnerID.ElementKeyAt(i);
				}
			}
		}
		List<EntityParent> values = EntityWorld.Instance.GetEntities<EntityPet>().Values;
		EntityPet entityPet = null;
		for (int j = 0; j < values.get_Count(); j++)
		{
			if (values.get_Item(j).IsFighting && values.get_Item(j).OwnerListIdx == num && values.get_Item(j).OwnerID == caster.ID)
			{
				entityPet = (values.get_Item(j) as EntityPet);
				break;
			}
		}
		if (entityPet == null)
		{
			return;
		}
		effectTargetIDs.Add(entityPet.ID);
	}
}
