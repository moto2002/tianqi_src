using Package;
using System;
using UnityEngine;

public class UpgradeTask : TriggerTask
{
	public UpgradeTask(Task task) : base(task)
	{
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (base.Targets != null && base.Targets.get_Count() > 1)
		{
			int num = base.Targets.get_Item(1);
			if (MainTaskManager.Instance.HasNpcId(num))
			{
				this.OpenUpgradeUI();
			}
			else
			{
				base.IsTrigger = true;
				if (MainTaskManager.Instance.GoToNPC(num, base.Task.taskId, isFastNav, 1f))
				{
					this.OnEnterNPC(num);
				}
			}
		}
		else
		{
			Debug.LogError(string.Format("进阶任务[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
		}
	}

	private void OpenUpgradeUI()
	{
		int state = 3;
		if (!base.hasNextTask && base.Task.status == Task.TaskStatus.TaskReceived)
		{
			state = 4;
		}
		RankUpManager.Instance.OpenRankUpUI(state);
	}

	protected override void EnterNPC(int npcId)
	{
		if (base.IsTrigger)
		{
			base.IsTrigger = false;
			this.OpenUpgradeUI();
		}
	}

	public override void CommitBefore()
	{
		if (!UIManagerControl.Instance.IsOpen("RankUpUI"))
		{
			base.CommitBefore();
		}
	}
}
