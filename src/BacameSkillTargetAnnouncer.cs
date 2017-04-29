using System;

public class BacameSkillTargetAnnouncer
{
	protected static ConditionType type = ConditionType.BecameSkillTarget;

	public static void Announce(EntityParent announcer, EntityParent caster, int skillID)
	{
		BecameSkillTargetConditionMessage becameSkillTargetConditionMessage = new BecameSkillTargetConditionMessage();
		becameSkillTargetConditionMessage.type = BacameSkillTargetAnnouncer.type;
		becameSkillTargetConditionMessage.announcer = announcer;
		becameSkillTargetConditionMessage.caster = caster;
		becameSkillTargetConditionMessage.skillID = skillID;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, becameSkillTargetConditionMessage);
	}
}
