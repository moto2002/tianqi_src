using GameData;
using Package;
using System;

public class LocalBattleBuffDrainHandler : LocalBattleBuffCalculatorHandler
{
	public static void HandleBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		LocalBattleBuffDrainHandler.AppBuffDrain(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
	}

	public static void IntervalBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
		LocalBattleBuffDrainHandler.AppBuffDrain(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
	}

	public static void KillBuff(int buffID, long casterID, long targetID, int fromSkillLevel, bool isCommunicateMix)
	{
	}

	protected static void AppBuffDrain(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix)
	{
	}
}
