using System;

public class UnderParryAnnouncer
{
	protected static ConditionType type = ConditionType.UnderParry;

	public static void Announce(EntityParent announcer, EntityParent caster)
	{
		UnderParryConditionMessage underParryConditionMessage = new UnderParryConditionMessage();
		underParryConditionMessage.type = UnderParryAnnouncer.type;
		underParryConditionMessage.announcer = announcer;
		underParryConditionMessage.caster = caster;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, underParryConditionMessage);
	}
}
