using GameData;
using System;

public class AttrChangeConditionMessage : ConditionMessage
{
	public AttrType attrType;

	public double oldPercentage;

	public double curPercentage;

	public long oldValue;

	public long curValue;
}
