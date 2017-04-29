using GameData;
using System;

public class LocalBattleBuffPropHandler
{
	public static void HandleBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel)
	{
		LocalBattleBuffPropHandler.HandleBuffCasterProp(buffData, caster, fromSkillLevel, true);
		LocalBattleBuffPropHandler.HandleBuffTargetProp(buffData, target, fromSkillLevel, true);
	}

	public static void IntervalBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel)
	{
	}

	public static void KillBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillLevel)
	{
		LocalBattleBuffPropHandler.HandleBuffCasterProp(buffData, caster, fromSkillLevel, false);
		LocalBattleBuffPropHandler.HandleBuffTargetProp(buffData, target, fromSkillLevel, false);
	}

	protected static void HandleBuffCasterProp(Buff buffData, EntityParent caster, int fromSkillLevel, bool isAdd)
	{
		if (caster == null)
		{
			return;
		}
		if (caster.BattleBaseAttrs == null)
		{
			return;
		}
		if ((caster.IsEntitySelfType || caster.IsEntityPlayerType || caster.IsEntityPetType) && buffData.rolePropId.get_Count() > 0 && fromSkillLevel > 0)
		{
			for (int i = 0; i < buffData.rolePropId.get_Count(); i++)
			{
				if (buffData.rolePropId.get_Item(i).key == fromSkillLevel)
				{
					LocalAgent.AppDirectAttrChangeByTemplateID(caster.BattleBaseAttrs, buffData.rolePropId.get_Item(i).value, isAdd);
					break;
				}
			}
		}
		else if (buffData.propId > 0)
		{
			LocalAgent.AppDirectAttrChangeByTemplateID(caster.BattleBaseAttrs, buffData.propId, isAdd);
		}
	}

	protected static void HandleBuffTargetProp(Buff buffData, EntityParent target, int fromSkillLevel, bool isAdd)
	{
		if (target == null)
		{
			return;
		}
		if (target.BattleBaseAttrs == null)
		{
			return;
		}
		if ((target.IsEntitySelfType || target.IsEntityPlayerType || target.IsEntityPetType) && buffData.roleTargetPropId.get_Count() > 0 && fromSkillLevel > 0)
		{
			for (int i = 0; i < buffData.roleTargetPropId.get_Count(); i++)
			{
				if (buffData.roleTargetPropId.get_Item(i).key == fromSkillLevel)
				{
					LocalAgent.AppDirectAttrChangeByTemplateID(target.BattleBaseAttrs, buffData.roleTargetPropId.get_Item(i).value, isAdd);
					break;
				}
			}
		}
		else if (buffData.targetPropId > 0)
		{
			LocalAgent.AppDirectAttrChangeByTemplateID(target.BattleBaseAttrs, buffData.targetPropId, isAdd);
		}
	}
}
