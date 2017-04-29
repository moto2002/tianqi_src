using System;

public class ExitBattleFieldAnnouncer
{
	protected static ConditionType type = ConditionType.ExitBattleField;

	public static void Announce(EntityParent announcer)
	{
		ConditionMessage conditionMessage = new ConditionMessage();
		conditionMessage.type = ExitBattleFieldAnnouncer.type;
		conditionMessage.announcer = announcer;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, conditionMessage);
	}
}
