using System;

public class ConditionRangeChecker : RangeChecker
{
	public void SetRange(int theID, float theRange, float theInterval)
	{
		this.ID = theID;
		this.range = theRange;
		this.interval = theInterval;
	}

	protected override void EnterRange()
	{
		RangeTriggerAnnouncer.Announce(this.ID);
	}
}
