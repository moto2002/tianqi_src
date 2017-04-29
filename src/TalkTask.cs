using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TalkTask : TriggerTask
{
	public TalkTask(Task task) : base(task)
	{
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (base.Targets != null && base.Targets.get_Count() > 0)
		{
			int num = base.Targets.get_Item(0);
			if (MainTaskManager.Instance.HasNpcId(num))
			{
				this.TaskTalkUI(num);
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
			Debug.LogError(string.Format("任务对话[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
		}
	}

	private void TaskTalkUI(int npcId)
	{
		if (base.Targets != null && base.Targets.get_Count() > 1)
		{
			List<int> list = new List<int>();
			for (int i = 1; i < base.Targets.get_Count(); i++)
			{
				list.Add(base.Targets.get_Item(i));
			}
			MainTaskManager.Instance.ShowTalkUINpc = npcId;
			MainTaskManager.Instance.OpenTalkUI(list, true, delegate
			{
				this.SendFinish();
			}, 0);
			SoundManager.Instance.PlayNPC(npcId);
		}
		else
		{
			Debug.LogError(string.Format("任务对话[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
		}
	}

	protected override void EnterNPC(int npcId)
	{
		if (base.IsTrigger)
		{
			base.IsTrigger = false;
			this.TaskTalkUI(npcId);
		}
	}
}
