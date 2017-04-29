using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTask : IDisposable
{
	public const uint DEALY_EXECUTE_1 = 600u;

	public const uint DEALY_EXECUTE_2 = 2000u;

	public bool IsActive;

	protected bool mInCountDown;

	protected uint mCountDownId;

	protected uint mDelayExecuteId;

	protected bool mAlreadyRefresh;

	public Package.Task Task
	{
		get;
		protected set;
	}

	public RenWuPeiZhi Data
	{
		get;
		protected set;
	}

	public List<int> Targets
	{
		get;
		protected set;
	}

	public bool hasPrevTask
	{
		get
		{
			return this.Data != null && this.Data.frontTask > 0;
		}
	}

	public bool hasNextTask
	{
		get
		{
			return this.Data != null && this.Data.nextTask > 0;
		}
	}

	public bool isTempTop
	{
		get
		{
			return !this.mAlreadyRefresh && this.Data != null && this.Data.stickie == 1;
		}
		set
		{
			if (this.IsActive)
			{
				if (value && GuideManager.Instance.guide_lock)
				{
					this.mAlreadyRefresh = false;
				}
				else
				{
					this.mAlreadyRefresh = value;
				}
			}
		}
	}

	public BaseTask(Package.Task task)
	{
		if (task != null && task.taskId > 0)
		{
			this.SetData(task);
			this.SetTask(task, true);
			this.AddListeners();
		}
	}

	~BaseTask()
	{
		this.Dispose();
	}

	public virtual void Dispose()
	{
		if (this.Task != null)
		{
			this.RemoveListeners();
			this.KillTask();
			this.StopDelayExecuteTask();
			this.Task = null;
			this.Data = null;
		}
	}

	protected virtual void SetData(Package.Task task)
	{
		this.Data = DataReader<RenWuPeiZhi>.Get(task.taskId);
		if (this.Data != null && this.Data.target != null && this.Data.target.get_Count() > 0)
		{
			this.Targets = this.Data.target;
		}
		else
		{
			Debug.LogError(string.Format("任务[{0}]目标配表有误!!!", task.taskId));
		}
	}

	public virtual void SetTask(Package.Task task, bool isRecycle = true)
	{
		if (isRecycle)
		{
			this.IsActive = true;
		}
		this.Task = task;
	}

	protected virtual void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.EndNav, new Callback(this.OnStopExecuteTask));
	}

	protected virtual void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.EndNav, new Callback(this.OnStopExecuteTask));
	}

	public bool Execute(bool isFastNav = false, bool isFromClick = false)
	{
		if (this.IsActive)
		{
			switch (this.Task.status)
			{
			case Package.Task.TaskStatus.TaskCanAccept:
				if (MainTaskManager.Instance.HasNpcId(this.Data.linkNpc1))
				{
					TaskDescUI.OpenByNpc = this.Data.linkNpc1;
					this.AcceptBefore();
				}
				else if (MainTaskManager.Instance.GoToNPC(this.Data.linkNpc1, this.Task.taskId, isFastNav, 1f))
				{
					this.OnEnterNPC(this.Data.linkNpc1);
				}
				return true;
			case Package.Task.TaskStatus.TaskReceived:
				TaskProgressUI.OpenByTaskId = this.Task.taskId;
				this.StartExecute(isFastNav);
				return true;
			case Package.Task.TaskStatus.WaitingToClaimPrize:
				if (this.Data.quickComplete == 1)
				{
					if (this.Data.quickAchieve != 1)
					{
						LinkNavigationManager.OpenTaskDescUI(this.Task.taskId, isFromClick, false);
					}
				}
				else if (MainTaskManager.Instance.HasNpcId(this.Data.linkNpc2))
				{
					TaskDescUI.OpenByNpc = this.Data.linkNpc2;
					this.CommitBefore();
				}
				else if (MainTaskManager.Instance.GoToNPC(this.Data.linkNpc2, this.Task.taskId, isFastNav, 1f))
				{
					this.OnEnterNPC(this.Data.linkNpc2);
				}
				return true;
			}
		}
		else if (isFromClick && this.Task.taskType == Package.Task.TaskType.MainTask)
		{
			Package.Task.TaskStatus status = this.Task.status;
			if (status == Package.Task.TaskStatus.TaskNotOpen)
			{
				if (MainTaskManager.Instance.HasUpgradeTips(this.Task))
				{
					MainTaskManager.Instance.ShowUpgradeTips();
				}
				else if (MainTaskManager.Instance.DelayExecuteTaskId == 0)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310008, false));
				}
				return false;
			}
			if (status == Package.Task.TaskStatus.TaskFinished)
			{
				if (MainTaskManager.Instance.DelayExecuteTaskId == 0)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310009, false));
				}
				return false;
			}
		}
		else
		{
			Debug.Log("任务[" + this.Task.taskId + "]已失活!!! by Execute");
		}
		return false;
	}

	protected virtual void StartExecute(bool isFastNav)
	{
	}

	public virtual void FinishBefore()
	{
	}

	public virtual void SendFinish()
	{
		MainTaskManager.Instance.SendFinishTaskReq(this.Task.taskId, 0);
	}

	public virtual void FinishAfter()
	{
		this.Execute(false, false);
		this.ResetCD();
	}

	public virtual void AcceptBefore()
	{
		LinkNavigationManager.OpenTaskDescUI(this.Task.taskId, false, false);
	}

	public virtual void Accept()
	{
		if (this.Task.taskType == Package.Task.TaskType.ZeroCity)
		{
			MainTaskManager.Instance.SendAcceptTaskReq(this.Task.taskId, this.Task.extParams.get_Item(0));
		}
		else
		{
			MainTaskManager.Instance.SendAcceptTaskReq(this.Task.taskId, 0);
		}
	}

	public virtual void AcceptAfter()
	{
		this.ResetCD();
		if (GuideManager.Instance.guide_lock)
		{
			Debug.Log("接取任务后操作被指引打断!!!");
			return;
		}
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("接取任务后操作被神器打断!!!");
			return;
		}
		if (this.Data != null)
		{
			if (this.Data.quickReceive == 1)
			{
				this.DelayExecute();
			}
			else
			{
				MainTaskManager.Instance.ShowTalkUINpc = this.Data.linkNpc1;
				MainTaskManager.Instance.OpenTalkUI(this.Data.dialogue1, true, new Action(this.DelayExecute), 0);
				SoundManager.Instance.PlayNPC(this.Data.linkNpc1);
			}
		}
	}

	public virtual void CommitBefore()
	{
		if (this.Data != null)
		{
			MainTaskManager.Instance.ShowTalkUINpc = this.Data.linkNpc2;
			MainTaskManager.Instance.OpenTalkUI(this.Data.dialogue2, true, new Action(this.CommitTalk), 0);
			SoundManager.Instance.PlayNPC(this.Data.linkNpc2);
		}
	}

	public virtual void Commit(bool isUseDiamond = false)
	{
		if (this.Task != null)
		{
			if (this.Task.taskType == Package.Task.TaskType.ZeroCity)
			{
				MainTaskManager.Instance.SendCommitTaskReq(this.Task.taskId, isUseDiamond, this.Task.extParams.get_Item(0));
			}
			else
			{
				MainTaskManager.Instance.SendCommitTaskReq(this.Task.taskId, isUseDiamond, 0);
			}
		}
		else
		{
			Debug.Log("<color=red>Error:</color>后端数据已被清除!!!");
		}
	}

	public virtual void CommitAfter()
	{
		this.Task.status = Package.Task.TaskStatus.TaskFinished;
		this.ResetCD();
		if (GuideManager.Instance.guide_lock)
		{
			Debug.Log("提交任务后操作被指引打断!!!");
			return;
		}
		if (GodWeaponManager.Instance.WeaponLock)
		{
			Debug.Log("提交任务后操作被神器打断!!!");
			return;
		}
		if (this.hasNextTask)
		{
			RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(this.Data.nextTask);
			if (renWuPeiZhi != null && renWuPeiZhi.quickReceive == 1)
			{
				return;
			}
		}
		this.DelayExecute();
	}

	public virtual void OnEnterNPC(int npcId)
	{
		if (this.IsActive)
		{
			if (MainTaskManager.Instance.CareerTaskId <= 0 || (MainTaskManager.Instance.CareerTaskId > 0 && (EntityWorld.Instance.EntSelf.Lv < MainTaskManager.Instance.CareerMaxLevel || this.Task.taskType == Package.Task.TaskType.ChangeCareer)))
			{
				switch (this.Task.status)
				{
				case Package.Task.TaskStatus.TaskCanAccept:
					MainTaskManager.Instance.StopToNPC(false);
					TaskDescUI.OpenByNpc = npcId;
					this.AcceptBefore();
					break;
				case Package.Task.TaskStatus.TaskReceived:
					this.EnterNPC(npcId);
					break;
				case Package.Task.TaskStatus.WaitingToClaimPrize:
					MainTaskManager.Instance.StopToNPC(false);
					TaskDescUI.OpenByNpc = npcId;
					this.CommitBefore();
					break;
				}
			}
		}
		else
		{
			Debug.Log("任务[" + this.Task.taskId + "]已失活!!! by OnEnterNPC");
		}
		CityManager.Instance.NeedDelayEnterNPC = false;
	}

	protected virtual void EnterNPC(int npcId)
	{
	}

	public virtual void OnExitNPC(int npcId)
	{
		if (MainTaskManager.Instance.ShowTalkUINpc == npcId && UIManagerControl.Instance.IsOpen("TalkUI") && this.Data.type != 13)
		{
			UIManagerControl.Instance.HideUI("TalkUI");
		}
	}

	protected void CommitTalk()
	{
		if (TaskDescUI.OpenByNpc > 0)
		{
			LinkNavigationManager.OpenTaskDescUI(this.Task.taskId, false, false);
		}
	}

	protected void DelayExecute()
	{
		if (MainTaskManager.Instance.AutoTaskId == -1)
		{
			return;
		}
		if (this.Task.taskType == Package.Task.TaskType.AdvancedTask)
		{
			return;
		}
		if (this.Data.pathfinding == null || this.Data.pathfinding.get_Count() < 1 || this.Data.pathfinding.get_Item(0) != 1)
		{
			return;
		}
		MainTaskManager.Instance.DelayExecuteTaskId = this.Task.taskId;
		this.mDelayExecuteId = TimerHeap.AddTimer(600u, 0, new Action(this.OnDelayExecuteTask));
	}

	protected void OnDelayExecuteTask()
	{
		if (this.mDelayExecuteId > 0u)
		{
			if (GuideManager.Instance.guide_lock)
			{
				Debug.Log("任务延迟后操作被指引打断!!!");
				return;
			}
			if (this.Task.status == Package.Task.TaskStatus.TaskReceived)
			{
				MainTaskManager.Instance.ExecuteTask(this.Task.taskId, false);
			}
			else if (this.Task.status == Package.Task.TaskStatus.TaskFinished)
			{
				BaseTask task = MainTaskManager.Instance.GetTask(MainTaskManager.Instance.CurTaskId, true);
				if (task != null && task.Task.taskType == this.Task.taskType)
				{
					MainTaskManager.Instance.ExecuteTask(0, false);
				}
			}
			MainTaskManager.Instance.DelayExecuteTaskId = 0;
			TimerHeap.DelTimer(this.mDelayExecuteId);
			this.mDelayExecuteId = 0u;
		}
	}

	protected void StopDelayExecuteTask()
	{
		if (this.mDelayExecuteId > 0u)
		{
			TimerHeap.DelTimer(this.mDelayExecuteId);
		}
		this.mDelayExecuteId = 0u;
	}

	protected virtual void OnStopExecuteTask()
	{
		this.StopDelayExecuteTask();
	}

	public virtual void KillTask()
	{
		this.Task.status = Package.Task.TaskStatus.TaskFinished;
		this.IsActive = false;
		this.mAlreadyRefresh = false;
	}

	public void ResetCD()
	{
		this.mInCountDown = false;
		if (this.mCountDownId > 0u)
		{
			TimerHeap.DelTimer(this.mCountDownId);
			this.mCountDownId = 0u;
		}
	}

	public void GetTaskRewards(ref XDict<int, long> rewards, bool justRes = false)
	{
		rewards.Clear();
		for (int i = 0; i < this.Data.reward.get_Count(); i++)
		{
			if (!justRes || this.Data.reward.get_Item(i).key <= 100)
			{
				rewards.Add(this.Data.reward.get_Item(i).key, this.Data.reward.get_Item(i).value * (long)this.Task.ratio);
			}
		}
		List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
		int lv = EntityWorld.Instance.EntSelf.Lv;
		for (int j = 0; j < this.Data.rewardId.get_Count(); j++)
		{
			int num = this.Data.rewardId.get_Item(j);
			for (int k = 0; k < dataList.get_Count(); k++)
			{
				DiaoLuo diaoLuo = dataList.get_Item(k);
				if (diaoLuo.ruleId == num)
				{
					if (!justRes || diaoLuo.goodsId <= 100)
					{
						if (diaoLuo.minLv == diaoLuo.maxLv && diaoLuo.minLv == 0)
						{
							rewards.Add(diaoLuo.goodsId, diaoLuo.minNum * (long)this.Task.ratio);
						}
						else if (diaoLuo.minLv == diaoLuo.maxLv && lv == diaoLuo.minLv)
						{
							rewards.Add(diaoLuo.goodsId, diaoLuo.minNum * (long)this.Task.ratio);
						}
						else if (diaoLuo.minLv < diaoLuo.maxLv && lv >= diaoLuo.minLv && lv < diaoLuo.maxLv)
						{
							rewards.Add(diaoLuo.goodsId, diaoLuo.minNum * (long)this.Task.ratio);
						}
					}
				}
			}
		}
	}
}
