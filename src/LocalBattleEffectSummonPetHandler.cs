using GameData;
using Package;
using System;

public class LocalBattleEffectSummonPetHandler
{
	public static void AppSummonPet(Effect effectData, EntityParent caster, int skillID, bool isCommunicateMix)
	{
		if (isCommunicateMix)
		{
			return;
		}
		if (caster == null)
		{
			return;
		}
		if (!caster.Actor)
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
		bool flag = true;
		for (int i = 0; i < petSpiritByOwnerID.Count; i++)
		{
			LocalDimensionPetSpirit localDimensionPetSpirit = petSpiritByOwnerID.ElementValueAt(i);
			if (localDimensionPetSpirit.summonSkillInfo != null)
			{
				if (localDimensionPetSpirit.summonSkillInfo.skillId == skillID)
				{
					SummonPetAnnouncer.Announce(caster, DataReader<Pet>.Get(localDimensionPetSpirit.TypeID).petType);
					flag = localDimensionPetSpirit.IsSummonMonopolize;
					LocalAgent.SummonPet(caster.ID, localDimensionPetSpirit);
					break;
				}
			}
		}
		if (flag)
		{
			for (int j = 0; j < petSpiritByOwnerID.Count; j++)
			{
				LocalDimensionPetSpirit localDimensionPetSpirit2 = petSpiritByOwnerID.ElementValueAt(j);
				if (localDimensionPetSpirit2.summonSkillInfo != null)
				{
					if (localDimensionPetSpirit2.summonSkillInfo.skillId != skillID)
					{
						if (EntityWorld.Instance.EntCurPet.ContainsKey(localDimensionPetSpirit2.ID))
						{
							LocalAgent.ReleasePet(localDimensionPetSpirit2, false);
						}
					}
				}
			}
		}
	}

	public static void AppSendPetEnterBattleField(long petID, Pos pos, Vector2 dir, float existTime)
	{
		LocalBattleProtocolSimulator.SendPetEnterBattleField(petID, pos, dir, existTime);
	}

	public static void AppSendPetLeaveBattleField(long petID)
	{
		LocalBattleProtocolSimulator.SendPetLeaveBattleField(petID);
	}
}
