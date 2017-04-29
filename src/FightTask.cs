using GameData;
using Package;
using System;
using UnityEngine;

public class FightTask : TriggerTask
{
	protected int mMonsterId;

	protected int mMaxCount;

	public bool IsVictory
	{
		get;
		set;
	}

	public FightTask(Package.Task task) : base(task)
	{
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<bool>(EventNames.MirrorResultNty, new Callback<bool>(this.OnExitFightResultNty));
		EventDispatcher.AddListener<bool>(EventNames.DungeonResultNty, new Callback<bool>(this.OnExitDungeonResultNty));
		EventDispatcher.AddListener<int>(LocalInstanceEvent.LocalInstanceMonsterDie, new Callback<int>(this.OnMonsterDie));
		EventDispatcher.AddListener(EventNames.ExitTaskFightResult, new Callback(this.OnExitFightResult));
		EventDispatcher.AddListener<bool>(EventNames.ChallengeDungeonResult, new Callback<bool>(this.OnChallengeDungeonResult));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<bool>(EventNames.MirrorResultNty, new Callback<bool>(this.OnExitFightResultNty));
		EventDispatcher.RemoveListener<bool>(EventNames.DungeonResultNty, new Callback<bool>(this.OnExitDungeonResultNty));
		EventDispatcher.RemoveListener<int>(LocalInstanceEvent.LocalInstanceMonsterDie, new Callback<int>(this.OnMonsterDie));
		EventDispatcher.RemoveListener(EventNames.ExitTaskFightResult, new Callback(this.OnExitFightResult));
		EventDispatcher.RemoveListener<bool>(EventNames.ChallengeDungeonResult, new Callback<bool>(this.OnChallengeDungeonResult));
	}

	protected override void StartExecute(bool isFastNav)
	{
		if (base.Data == null || base.Targets == null || base.Targets.get_Count() <= 0)
		{
			return;
		}
		int instanceID = base.Targets.get_Item(0);
		if (!base.IsTrigger && MainTaskManager.Instance.HasNpcId(base.Data.instanceNpc))
		{
			base.IsTrigger = true;
			InstanceManager.SecurityCheck(delegate
			{
				WaitUI.OpenUI(0u);
				if (this.Data.type == 6)
				{
					DungeonManager.Instance.DungeonInstanceType = DungeonManager.InsType.MAIN;
					DungeonManager.Instance.SendChallengeDungeonReq(instanceID);
					Debug.Log("<进入普通副本[" + instanceID + "]>");
				}
				else if (this.Data.type == 5 || this.Data.type == 4 || this.Data.type == 16)
				{
					FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(instanceID);
					if (fuBenJiChuPeiZhi != null)
					{
						DungeonManager.Instance.DungeonInstanceType = DungeonManager.InsType.FIELD;
						DungeonManager.Instance.DungeonTarget.Clear();
						DungeonManager.Instance.DungeonTarget.Add(fuBenJiChuPeiZhi.time);
						if (this.Data.type == 5 && this.Targets.get_Count() == 2)
						{
							DungeonManager.Instance.DungeonTarget.Add(4);
							this.mMaxCount = this.Targets.get_Item(1);
							DungeonManager.Instance.DungeonTarget.Add(this.mMaxCount - this.Task.count);
						}
						else if (this.Data.type == 4 && this.Targets.get_Count() == 3)
						{
							DungeonManager.Instance.DungeonTarget.Add(2);
							this.mMonsterId = this.Targets.get_Item(1);
							DungeonManager.Instance.DungeonTarget.Add(this.mMonsterId);
							this.mMaxCount = this.Targets.get_Item(2);
							DungeonManager.Instance.DungeonTarget.Add(this.mMaxCount - this.Task.count);
						}
						else
						{
							if (this.Data.type != 16 || this.Targets.get_Count() != 4)
							{
								Debug.Log(string.Concat(new object[]
								{
									"<color=red>Error:</color>任务副本[",
									this.Data.id,
									"]配置参数[",
									this.Data.target,
									"]有误！"
								}));
								return;
							}
							DungeonManager.Instance.DungeonTarget.Add(this.Targets.get_Item(2));
							DungeonManager.Instance.DungeonTarget.Add(this.Targets.get_Item(3));
						}
						DungeonManager.Instance.SendChallengeDungeonReq(fuBenJiChuPeiZhi.id);
						Debug.Log("<进入野外副本[" + instanceID + "]>");
					}
				}
			}, null);
		}
		else if (!base.IsTrigger)
		{
			base.IsAutoNav = true;
			if (MainTaskManager.Instance.GoToNPC(base.Data.instanceNpc, base.Task.taskId, isFastNav, 0f))
			{
				this.OnEnterNPC(base.Data.instanceNpc);
			}
		}
	}

	protected override void EnterNPC(int npcId)
	{
		if ((base.IsAutoNav || CityManager.Instance.NeedDelayEnterNPC) && MainTaskManager.Instance.IsTaskNpc(npcId))
		{
			base.Execute(false, false);
			MainTaskManager.Instance.StopToNPC(false);
		}
	}

	private void OnExitDungeonResultNty(bool isWin)
	{
		if (DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.MAIN || DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.FIELD)
		{
			this.OnExitFightResultNty(isWin);
		}
	}

	private void OnExitFightResultNty(bool isWin)
	{
		if (this.IsActive && base.IsTrigger)
		{
			uint timeMS = (uint)float.Parse(DataReader<GlobalParams>.Get("mirrorCopyDelayEndTime").value);
			if (base.Data.type == 6)
			{
				timeMS = (uint)float.Parse(DataReader<GlobalParams>.Get("singleCopyDelayEndTime").value);
			}
			FollowCamera.instance.ResetCameraAsync(timeMS, new Action(this.ExitFight));
			this.IsVictory = isWin;
			base.IsTrigger = false;
		}
	}

	private void OnMonsterDie(int id)
	{
		if (this.IsActive && base.IsTrigger && (this.mMonsterId == 0 || (this.mMonsterId > 0 && this.mMonsterId == id)))
		{
			base.Task.count++;
			if (base.Task.count > this.mMaxCount)
			{
				base.Task.count = this.mMaxCount;
			}
			else
			{
				EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, base.Task);
			}
		}
	}

	private void ExitFight()
	{
		if (base.Data.type != 6)
		{
			DungeonManager.Instance.SendExitDungeonReq();
		}
	}

	private void OnExitFightResult()
	{
		if (this.IsActive && base.IsTrigger)
		{
			base.IsTrigger = false;
			Debug.Log("<退出野外副本返回>");
		}
	}

	private void OnChallengeDungeonResult(bool isSuccess)
	{
		if (isSuccess)
		{
			MainTaskManager.Instance.ClearNpcIds();
		}
		else
		{
			base.IsTrigger = false;
		}
	}
}
