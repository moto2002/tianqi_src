using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTask : TriggerTask
{
	protected int mEnterNpcId;

	protected int mTargetNpcId;

	protected List<int> mTalkIds = new List<int>();

	public ChangeTask(Task task) : base(task)
	{
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (base.Data == null || base.Targets == null)
		{
			return;
		}
		if (base.Targets.get_Count() > 3)
		{
			this.mTargetNpcId = base.Targets.get_Item(1);
			this.mTalkIds.Clear();
			for (int i = 3; i < base.Targets.get_Count(); i++)
			{
				this.mTalkIds.Add(base.Targets.get_Item(i));
			}
			if (MainTaskManager.Instance.GoToNPC(this.mTargetNpcId, base.Task.taskId, isFastNav, 1f))
			{
				this.OnEnterNPC(this.mTargetNpcId);
			}
		}
		else
		{
			Debug.LogError(string.Format("变身任务[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
		}
	}

	protected override void EnterNPC(int npcId)
	{
		if (this.mEnterNpcId == 0 && MainTaskManager.Instance.IsSameNpc(this.mTargetNpcId, npcId))
		{
			this.mEnterNpcId = npcId;
			MainTaskManager.Instance.OpenTalkUI(this.mTalkIds, true, new Action(this.SendFinish), 0);
			SoundManager.Instance.PlayNPC(npcId);
		}
	}

	public override void SendFinish()
	{
		if (this.IsActive)
		{
			MainTaskManager.Instance.SendFinishTaskReq(base.Task.taskId, this.mEnterNpcId);
		}
		this.mEnterNpcId = 0;
	}
}
