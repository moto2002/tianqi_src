using System;

public class AlertAnnouncer
{
	protected static ConditionType type = ConditionType.Alert;

	public static void Announce(EntityParent announcer)
	{
		ConditionMessage conditionMessage = new ConditionMessage();
		conditionMessage.type = AlertAnnouncer.type;
		conditionMessage.announcer = announcer;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, conditionMessage);
	}
}
