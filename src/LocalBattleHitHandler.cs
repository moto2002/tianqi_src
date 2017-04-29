using GameData;
using System;

public class LocalBattleHitHandler
{
	public static void AppDead(long targetID)
	{
		if (LocalAgent.GetEntityByID(targetID) == null)
		{
			return;
		}
		LocalAgent.AppClearBuff(targetID);
		LocalAgent.ClearFuseDataByEntityID(targetID, true);
	}

	public static void AppHit(Effect effectData, EntityParent caster, EntityParent target, XPoint basePoint, bool isAddEffect)
	{
		if (target == null)
		{
			return;
		}
		if (!isAddEffect && effectData.cycleHit == 0)
		{
			return;
		}
		if (target.IsStatic)
		{
			return;
		}
		if (target.IsEndure)
		{
			return;
		}
		if (target.GetSkillManager() == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(effectData.hitAction))
		{
			return;
		}
		target.GetSkillManager().ClientHandleHit(caster, effectData, basePoint);
	}

	public static void AppHitAudio(Effect effectData, EntityParent target)
	{
		if (!target.Actor)
		{
			return;
		}
		target.Actor.PlayHitSound(effectData.hitAudio);
	}

	public static void AppParryFx(EntityParent target)
	{
		if (!target.Actor)
		{
			return;
		}
		target.Actor.PlayParryFx();
	}

	public static void AppWeakCalculate(EntityParent caster, EntityParent target, XDict<AttrType, long> casterTempAttrs)
	{
		if (target.IsWeak)
		{
			return;
		}
		int num = target.Vp - BattleCalculator.CalculateWeak(caster.BattleBaseAttrs, casterTempAttrs);
		if (num < 0)
		{
			num = 0;
		}
		else if (num > target.RealVpLmt)
		{
			num = target.RealVpLmt;
		}
		target.SetValue(AttrType.Vp, num, true);
	}
}
