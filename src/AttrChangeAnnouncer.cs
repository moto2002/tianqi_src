using GameData;
using System;

public class AttrChangeAnnouncer
{
	protected static ConditionType type = ConditionType.AttrChange;

	public static void Announce(EntityParent announcer, AttrType attrType, double oldPercentage, double curPercentage, long oldValue, long curValue)
	{
		AttrChangeConditionMessage attrChangeConditionMessage = new AttrChangeConditionMessage();
		attrChangeConditionMessage.type = AttrChangeAnnouncer.type;
		attrChangeConditionMessage.announcer = announcer;
		attrChangeConditionMessage.attrType = attrType;
		attrChangeConditionMessage.oldPercentage = oldPercentage;
		attrChangeConditionMessage.curPercentage = curPercentage;
		attrChangeConditionMessage.oldValue = oldValue;
		attrChangeConditionMessage.curValue = curValue;
		EventDispatcher.Broadcast<ConditionMessage>(ConditionManagerEvent.CheckCondition, attrChangeConditionMessage);
	}
}
