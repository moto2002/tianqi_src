using System;

public class CGCompleteAnnouncer
{
	protected static ConditionType type = ConditionType.CGComplete;

	public static void Announce(int cgID)
	{
		CGCompleteConditionMessage cGCompleteConditionMessage = new CGCompleteConditionMessage();
		cGCompleteConditionMessage.type = CGCompleteAnnouncer.type;
		cGCompleteConditionMessage.announcer = null;
		cGCompleteConditionMessage.cgID = cgID;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, cGCompleteConditionMessage);
	}
}
