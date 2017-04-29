using System;

public class UnderMissAnnouncer
{
	protected static ConditionType type = ConditionType.UnderMiss;

	public static void Announce(EntityParent announcer, EntityParent caster)
	{
		UnderMissConditionMessage underMissConditionMessage = new UnderMissConditionMessage();
		underMissConditionMessage.type = UnderMissAnnouncer.type;
		underMissConditionMessage.announcer = announcer;
		underMissConditionMessage.caster = caster;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, underMissConditionMessage);
	}
}
