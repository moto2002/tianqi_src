using Package;
using System;
using UnityEngine;

public class WeaponTask : LinkTask
{
	public WeaponTask(Task task) : base(task, 0, -1)
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
			base.SystemId = base.Targets.get_Item(0);
			GodWeaponManager.Instance.OpenDescId = base.Targets.get_Item(1);
			LinkNavigationManager.SystemLink(base.SystemId, true, null);
		}
		else
		{
			Debug.LogError(string.Format("收集神器任务[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
		}
	}
}
