using GameData;
using Package;
using System;
using UnityEngine;

public class LocalBattleEffectBlinkHandler
{
	public static void AppBlink(Effect effectData, EntityParent caster, XPoint basePoint, bool isCommunicateMix)
	{
		Pos pos = new Pos();
		Vector2 vector = new Vector2();
		Vector3 vector2 = Vector3.get_zero();
		Vector3 vector3 = caster.Actor.FixTransform.get_forward();
		EffectBasePointType @base = (EffectBasePointType)effectData.@base;
		if (@base != EffectBasePointType.SpawnPoint)
		{
			vector2 = basePoint.ApplyOffset(effectData.offset).position;
		}
		else
		{
			vector2 = LocalAgent.GetSpawnPosition(effectData.summonId);
		}
		if (effectData.blinkPoint != 0)
		{
			vector3 = LocalAgent.GetSpawnPosition(effectData.blinkPoint) - vector2;
		}
		pos.x = (float)((int)vector2.x * 100);
		pos.y = (float)((int)vector2.z * 100);
		vector.x = vector3.x;
		vector.y = vector3.z;
		LocalBattleProtocolSimulator.SendTeleport(caster.ID, pos);
	}
}
