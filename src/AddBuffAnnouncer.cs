using System;

public class AddBuffAnnouncer
{
	protected static ConditionType type = ConditionType.AddBuff;

	public static void Announce(EntityParent announcer, int buffID)
	{
		AddBuffConditionMessage addBuffConditionMessage = new AddBuffConditionMessage();
		addBuffConditionMessage.type = AddBuffAnnouncer.type;
		addBuffConditionMessage.announcer = announcer;
		addBuffConditionMessage.buffID = buffID;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, addBuffConditionMessage);
	}
}
