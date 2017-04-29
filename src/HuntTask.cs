using Package;
using System;
using UnityEngine;

public class HuntTask : LinkTask
{
	public HuntTask(Task task) : base(task, 0, -1)
	{
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (base.Data == null || base.Targets == null)
		{
			return;
		}
		if (base.Targets.get_Count() > 1)
		{
			LinkNavigationManager.OpenHuntUI(base.Targets.get_Item(1));
		}
		else
		{
			Debug.LogError(string.Format("挂机任务[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
		}
	}
}
