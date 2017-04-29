using Package;
using System;
using UnityEngine;

public class LinkTask : BaseTask
{
	protected int mSysIndex = -1;

	protected Action mExecuteAction;

	public int SystemId
	{
		get;
		protected set;
	}

	public LinkTask(Task task, int systemId = 0, int sysIndex = -1) : base(task)
	{
		this.SystemId = systemId;
		this.mSysIndex = sysIndex;
	}

	public LinkTask(Task task, Action action) : base(task)
	{
		this.mExecuteAction = action;
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (this.mExecuteAction != null)
		{
			this.mExecuteAction.Invoke();
		}
		else if (this.SystemId > 0)
		{
			this.LinkSystemById(this.SystemId);
		}
		else if (this.mSysIndex > -1)
		{
			this.LinkSystemByIndex(this.mSysIndex);
		}
	}

	protected virtual void LinkSystemById(int id)
	{
		if (id > 0)
		{
			LinkNavigationManager.SystemLink(id, true, null);
		}
	}

	protected virtual void LinkSystemByIndex(int index)
	{
		if (base.Data != null && base.Targets != null)
		{
			if (base.Targets.get_Count() > index)
			{
				this.SystemId = base.Targets.get_Item(index);
				LinkNavigationManager.SystemLink(this.SystemId, true, null);
			}
			else
			{
				Debug.LogError(string.Format("任务[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
			}
		}
	}
}
