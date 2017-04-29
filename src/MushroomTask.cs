using Package;
using System;

public class MushroomTask : LinkTask
{
	public MushroomTask(Task task) : base(task, 0, -1)
	{
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (base.Data == null || base.Targets == null)
		{
			return;
		}
		ActiveCenterInfo activeCenterInfo = null;
		if (ActivityCenterManager.infoDict.TryGetValue(10003, ref activeCenterInfo) && activeCenterInfo.status == ActiveCenterInfo.ActiveStatus.AS.Start)
		{
			LinkNavigationManager.OpenMushroomHitUI();
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("活动未开启！");
		}
	}
}
