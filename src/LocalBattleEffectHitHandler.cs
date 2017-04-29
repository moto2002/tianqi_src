using GameData;
using System;
using System.Collections.Generic;

public class LocalBattleEffectHitHandler
{
	public static void AppHit(Effect effectData, EntityParent caster, List<long> effectTargetIDs, XPoint basePoint, bool isAddEffect)
	{
		for (int i = 0; i < effectTargetIDs.get_Count(); i++)
		{
			LocalBattleHitHandler.AppHit(effectData, caster, LocalAgent.GetEntityByID(effectTargetIDs.get_Item(i)), basePoint, isAddEffect);
		}
	}
}
