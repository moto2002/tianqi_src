using System;

public class UseSkillAnnouncer
{
	protected static ConditionType type = ConditionType.UseSkill;

	public static void Announce(EntityParent announcer, EntityParent target, int skillID)
	{
		UseSkillConditionMessage useSkillConditionMessage = new UseSkillConditionMessage();
		useSkillConditionMessage.type = UseSkillAnnouncer.type;
		useSkillConditionMessage.announcer = announcer;
		useSkillConditionMessage.target = target;
		useSkillConditionMessage.skillID = skillID;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, useSkillConditionMessage);
	}
}
