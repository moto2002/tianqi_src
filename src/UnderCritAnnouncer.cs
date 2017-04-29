using System;

public class UnderCritAnnouncer
{
	protected static ConditionType type = ConditionType.UnderCrit;

	public static void Announce(EntityParent announcer, EntityParent caster)
	{
		UnderCritConditionMessage underCritConditionMessage = new UnderCritConditionMessage();
		underCritConditionMessage.type = UnderCritAnnouncer.type;
		underCritConditionMessage.announcer = announcer;
		underCritConditionMessage.caster = caster;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, underCritConditionMessage);
	}
}
