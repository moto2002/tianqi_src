using System;

public class CauseParryAnnouncer
{
	protected static ConditionType type = ConditionType.CauseParry;

	public static void Announce(EntityParent announcer, EntityParent target)
	{
		CauseParryConditionMessage causeParryConditionMessage = new CauseParryConditionMessage();
		causeParryConditionMessage.type = CauseParryAnnouncer.type;
		causeParryConditionMessage.announcer = announcer;
		causeParryConditionMessage.target = target;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, causeParryConditionMessage);
	}
}
