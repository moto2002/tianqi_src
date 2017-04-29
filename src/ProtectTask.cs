using GameData;
using Package;
using System;
using UnityEngine;

public class ProtectTask : TriggerTask
{
	protected int mBeginNpc;

	protected int mEndNpc;

	protected bool mIsStart;

	protected bool mIsEnd;

	protected int mFinishTime = -1;

	public ProtectTask(Package.Task task) : base(task)
	{
		if (base.Data != null && base.Targets != null)
		{
			if (base.Targets.get_Count() > 1)
			{
				this.mBeginNpc = base.Targets.get_Item(0);
				this.mEndNpc = base.Targets.get_Item(1);
			}
			else
			{
				Debug.LogError(string.Format("护送任务[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
			}
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.OnRefreshNpc));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.OnRefreshNpc));
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (this.mBeginNpc > 0 && this.mEndNpc > 0)
		{
			this.mIsStart = true;
			this.OnEnterNPC(this.mBeginNpc);
		}
	}

	public override void OnEnterNPC(int npcId)
	{
		if (this.mFinishTime > -1)
		{
			this.mFinishTime++;
		}
		if (this.mFinishTime < 2)
		{
			base.OnEnterNPC(npcId);
		}
	}

	protected override void EnterNPC(int npcId)
	{
		if (this.mIsStart && MainTaskManager.Instance.IsSameNpc(this.mBeginNpc, npcId))
		{
			this.mIsStart = false;
			this.mIsEnd = true;
			if (MainTaskManager.Instance.GoToNPC(this.mEndNpc, base.Task.taskId, false, 1f))
			{
				this.OnEnterNPC(this.mEndNpc);
			}
		}
		else if (this.mIsEnd && MainTaskManager.Instance.IsSameNpc(this.mEndNpc, npcId))
		{
			this.mIsEnd = false;
			this.SendFinish();
			this.mFinishTime = 0;
		}
	}

	protected override void OnStopExecuteTask()
	{
		base.OnStopExecuteTask();
		if (this.IsActive && !this.mIsStart && this.mIsEnd)
		{
			NPC nPC = DataReader<NPC>.Get(this.mEndNpc);
			if (nPC != null)
			{
				Vector3 npcPosition = MainTaskManager.Instance.GetNpcPosition(nPC, 1f);
				if (EntityWorld.Instance.ActSelf == null || EntityWorld.Instance.ActSelf.FixTransform == null)
				{
					return;
				}
				float distanceNoY = XUtility.GetDistanceNoY(EntityWorld.Instance.ActSelf.FixTransform.get_position(), npcPosition);
				if (distanceNoY <= 1f && nPC.scene == MySceneManager.Instance.CurSceneID)
				{
					Debug.Log("更新NPC状态，自动移动到点：" + npcPosition);
					Vector3 zero;
					if (nPC.position.get_Count() == 3)
					{
						zero = new Vector3((float)nPC.position.get_Item(0), (float)nPC.position.get_Item(1), (float)nPC.position.get_Item(2));
					}
					else
					{
						zero = Vector3.get_zero();
					}
					TaskNPCManager.Instance.UpdateNPC(this.mBeginNpc, new NPCInformation
					{
						id = this.mBeginNpc,
						position = zero,
						status = NPCStatus.NAV_TO_POINT
					});
				}
			}
		}
	}

	private void OnRefreshNpc(Package.Task task)
	{
		if (this.IsActive && task != null && task.taskId == base.Task.taskId && task.taskType == Package.Task.TaskType.MainTask && task.status == Package.Task.TaskStatus.TaskReceived)
		{
			TaskNPCManager.Instance.UpdateNPC(this.mBeginNpc, new NPCInformation
			{
				id = this.mBeginNpc,
				position = Vector3.get_zero(),
				status = NPCStatus.FOLLOW
			});
		}
	}
}
