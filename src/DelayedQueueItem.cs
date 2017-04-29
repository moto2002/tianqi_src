using System;

public struct DelayedQueueItem
{
	public float time;

	public Action action;
}
