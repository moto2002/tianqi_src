using System;

public class RangeTriggerAnnouncer
{
	protected static ConditionType type = ConditionType.RangeTrigger;

	public static void Announce(int rangeID)
	{
		RangeTriggerConditionMessage rangeTriggerConditionMessage = new RangeTriggerConditionMessage();
		rangeTriggerConditionMessage.type = RangeTriggerAnnouncer.type;
		rangeTriggerConditionMessage.announcer = null;
		rangeTriggerConditionMessage.rangeID = rangeID;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, rangeTriggerConditionMessage);
	}
}
