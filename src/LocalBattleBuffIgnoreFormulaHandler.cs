using GameData;
using System;

public class LocalBattleBuffIgnoreFormulaHandler
{
	public static void HandleBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel)
	{
		LocalBattleBuffPropHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
		LocalBattleProtocolSimulator.SendIgnoreFormula(target.ID);
	}

	public static void IntervalBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel)
	{
		LocalBattleBuffPropHandler.IntervalBuff(buffData, caster, target, fromSkillLevel);
	}

	public static void KillBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel, bool isCommunicateMix)
	{
		LocalBattleBuffPropHandler.KillBuff(buffData, caster, target, fromSkillLevel);
		if (target == null)
		{
			return;
		}
		if (LocalAgent.GetSpiritIsDead(target, isCommunicateMix))
		{
			return;
		}
		if (!LocalAgent.CheckBuffTypeContainOther(buffData, target.ID))
		{
			LocalBattleProtocolSimulator.SendEndIgnoreFormula(target.ID);
		}
	}
}
