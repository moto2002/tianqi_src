using System;

public class CauseMissAnnouncer
{
	protected static ConditionType type = ConditionType.CauseMiss;

	public static void Announce(EntityParent announcer, EntityParent target)
	{
		CauseMissConditionMessage causeMissConditionMessage = new CauseMissConditionMessage();
		causeMissConditionMessage.type = CauseMissAnnouncer.type;
		causeMissConditionMessage.announcer = announcer;
		causeMissConditionMessage.target = target;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, causeMissConditionMessage);
	}
}
