using System;

public class CauseCritAnnouncer
{
	protected static ConditionType type = ConditionType.CauseCrit;

	public static void Announce(EntityParent announcer, EntityParent target)
	{
		CauseCritConditionMessage causeCritConditionMessage = new CauseCritConditionMessage();
		causeCritConditionMessage.type = CauseCritAnnouncer.type;
		causeCritConditionMessage.announcer = announcer;
		causeCritConditionMessage.target = target;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, causeCritConditionMessage);
	}
}
