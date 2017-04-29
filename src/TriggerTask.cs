using Package;
using System;

public class TriggerTask : BaseTask
{
	public bool IsAutoNav
	{
		get;
		protected set;
	}

	public bool IsTrigger
	{
		get;
		protected set;
	}

	public TriggerTask(Task task) : base(task)
	{
	}

	protected override void OnStopExecuteTask()
	{
		base.OnStopExecuteTask();
		if (this.IsActive)
		{
			this.IsAutoNav = false;
		}
	}
}
