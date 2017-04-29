using System;

public class EnterBattleFieldAnnouncer
{
	protected static ConditionType type = ConditionType.EnterBattleField;

	public static void Announce(EntityParent announcer)
	{
		ConditionMessage conditionMessage = new ConditionMessage();
		conditionMessage.type = EnterBattleFieldAnnouncer.type;
		conditionMessage.announcer = announcer;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, conditionMessage);
	}
}
