using GameData;
using System;

public class LocalBattleBuffChangeAttrsHandler
{
	public static void HandleBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel)
	{
		LocalBattleBuffPropHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
	}

	public static void IntervalBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel)
	{
		LocalBattleBuffPropHandler.IntervalBuff(buffData, caster, target, fromSkillLevel);
	}

	public static void KillBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel, bool isCommunicateMix)
	{
		LocalBattleBuffPropHandler.KillBuff(buffData, caster, target, fromSkillLevel);
	}
}
