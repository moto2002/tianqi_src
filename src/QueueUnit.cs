using System;

public class QueueUnit
{
	public Action action;

	public uint queueId;

	public uint priority;

	public PopCondition condition;

	public void JustCall()
	{
		if (this.action != null)
		{
			this.action.Invoke();
		}
	}
}
