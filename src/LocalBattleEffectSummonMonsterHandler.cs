using GameData;
using System;
using UnityEngine;

public class LocalBattleEffectSummonMonsterHandler
{
	public static void AppSummonMonster(Effect effectData, EntityParent caster, XPoint basePoint, bool isCommunicateMix)
	{
		if (isCommunicateMix)
		{
			return;
		}
		switch (effectData.type2)
		{
		case 2:
		case 4:
			LocalBattleEffectSummonMonsterHandler.SummonMonster(effectData, caster, basePoint, effectData.monsterId, effectData.summonId);
			break;
		}
	}

	protected static void SummonMonster(Effect effectData, EntityParent caster, XPoint basePoint, int monsterTypeID, int pointGroupID)
	{
		if (caster == null)
		{
			return;
		}
		switch (effectData.@base)
		{
		case 1:
		case 2:
			LocalAgent.SummonMonster(monsterTypeID, caster.Lv, caster.ID, caster.Camp, basePoint.ApplyOffset(effectData.offset).position);
			break;
		case 3:
			if (caster.Actor)
			{
				LocalAgent.SummonMonster(monsterTypeID, caster.Lv, caster.ID, caster.Camp, pointGroupID, caster.Actor.FixTransform.get_rotation(), effectData.offset);
			}
			break;
		case 5:
			if (effectData.coord != null && effectData.coord.get_Count() >= 3)
			{
				Vector3 vector = new Vector3((float)effectData.coord.get_Item(0) * 0.01f, (float)effectData.coord.get_Item(1) * 0.01f, (float)effectData.coord.get_Item(2) * 0.01f);
				Vector3 vector2 = Vector3.get_forward();
				if (effectData.orientation != null && effectData.orientation.get_Count() >= 3)
				{
					vector2 = new Vector3((float)effectData.orientation.get_Item(0) * 0.01f, (float)effectData.orientation.get_Item(1) * 0.01f, (float)effectData.orientation.get_Item(2) * 0.01f) - vector;
				}
				LocalAgent.SummonMonster(monsterTypeID, caster.Lv, caster.ID, caster.Camp, new XPoint
				{
					position = vector,
					rotation = Quaternion.LookRotation(vector2)
				}.ApplyOffset(effectData.offset).position);
			}
			break;
		}
	}
}
