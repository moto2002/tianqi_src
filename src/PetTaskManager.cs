using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class PetTaskManager : BaseSubSystemManager
{
	public List<PetTaskInfo> m_listPetTaskInfo = new List<PetTaskInfo>();

	private static PetTaskManager instance;

	private bool IsSendPickUpPetTask;

	private bool IsCheckTaskIsAchieve;

	public static PetTaskManager Instance
	{
		get
		{
			if (PetTaskManager.instance == null)
			{
				PetTaskManager.instance = new PetTaskManager();
			}
			return PetTaskManager.instance;
		}
	}

	private PetTaskManager()
	{
	}

	private void UpdatePetTaskInfo(PetTaskInfo pti)
	{
		for (int i = 0; i < this.m_listPetTaskInfo.get_Count(); i++)
		{
			if (this.m_listPetTaskInfo.get_Item(i).idx == pti.idx)
			{
				this.m_listPetTaskInfo.set_Item(i, pti);
				break;
			}
		}
		if (UIManagerControl.Instance.IsOpen("PetTaskUI"))
		{
			PetTaskUIView.Instance.RefreshUI();
		}
	}

	private void AddPetTaskInfo(PetTaskInfo pti)
	{
		if (pti == null)
		{
			return;
		}
		for (int i = 0; i < this.m_listPetTaskInfo.get_Count(); i++)
		{
			PetTaskInfo petTaskInfo = this.m_listPetTaskInfo.get_Item(i);
			if (petTaskInfo.idx == pti.idx)
			{
				Debug.LogError("宠物任务重复下发, idx = " + pti.idx);
				this.m_listPetTaskInfo.RemoveAt(i);
				break;
			}
		}
		this.m_listPetTaskInfo.Add(pti);
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.m_listPetTaskInfo.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<PetTaskInfoNty>(new NetCallBackMethod<PetTaskInfoNty>(this.OnPetTaskInfoNty));
		NetworkManager.AddListenEvent<PickUpPetTaskRes>(new NetCallBackMethod<PickUpPetTaskRes>(this.OnPickUpPetTaskRes));
		NetworkManager.AddListenEvent<AchievePetTaskNty>(new NetCallBackMethod<AchievePetTaskNty>(this.OnAchievePetTaskNty));
		NetworkManager.AddListenEvent<GetPetTaskRewardRes>(new NetCallBackMethod<GetPetTaskRewardRes>(this.OnGetPetTaskRewardRes));
	}

	public void SendPickUpPetTask(long taskuid, List<int> pets)
	{
		if (this.IsSendPickUpPetTask)
		{
			UIManagerControl.Instance.ShowToastText("正在接取任务中...");
			return;
		}
		this.IsSendPickUpPetTask = true;
		PickUpPetTaskReq pickUpPetTaskReq = new PickUpPetTaskReq();
		pickUpPetTaskReq.idx = taskuid;
		pickUpPetTaskReq.pets.AddRange(pets);
		NetworkManager.Send(pickUpPetTaskReq, ServerType.Data);
	}

	public void SendGetPetTaskReward(long taskuid)
	{
		NetworkManager.Send(new GetPetTaskRewardReq
		{
			idx = taskuid
		}, ServerType.Data);
	}

	private void OnPetTaskInfoNty(short state, PetTaskInfoNty down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < down.tasks.get_Count(); i++)
		{
			this.AddPetTaskInfo(down.tasks.get_Item(i));
		}
	}

	private void OnPickUpPetTaskRes(short state, PickUpPetTaskRes down = null)
	{
		this.IsSendPickUpPetTask = false;
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.UpdatePetTaskInfo(down.task);
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513703, false));
		PetTaskFormationUIView.Close();
	}

	private void OnAchievePetTaskNty(short state, AchievePetTaskNty down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.UpdatePetTaskInfo(down.task);
	}

	private void OnGetPetTaskRewardRes(short state, GetPetTaskRewardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.UpdatePetTaskInfo(down.task);
			this.ShowPetTaskResultUI(down);
		}
	}

	public PetTaskInfo GetPetTaskInfo(long taskuid)
	{
		for (int i = 0; i < this.m_listPetTaskInfo.get_Count(); i++)
		{
			if (this.m_listPetTaskInfo.get_Item(i).idx == taskuid)
			{
				return this.m_listPetTaskInfo.get_Item(i);
			}
		}
		return null;
	}

	public List<PetTaskInfo> GetPetTaskInfos(bool isUnPickUp)
	{
		List<PetTaskInfo> list = new List<PetTaskInfo>();
		for (int i = 0; i < this.m_listPetTaskInfo.get_Count(); i++)
		{
			if (isUnPickUp)
			{
				if (this.m_listPetTaskInfo.get_Item(i).Status == PetTaskInfo.PetTaskStatus.UnPickUp)
				{
					list.Add(this.m_listPetTaskInfo.get_Item(i));
				}
			}
			else if (this.m_listPetTaskInfo.get_Item(i).Status != PetTaskInfo.PetTaskStatus.UnPickUp && this.m_listPetTaskInfo.get_Item(i).Status != PetTaskInfo.PetTaskStatus.receive)
			{
				list.Add(this.m_listPetTaskInfo.get_Item(i));
			}
		}
		return list;
	}

	public bool IsInFormation(int petId)
	{
		for (int i = 0; i < this.m_listPetTaskInfo.get_Count(); i++)
		{
			PetTaskInfo petTaskInfo = this.m_listPetTaskInfo.get_Item(i);
			if (petTaskInfo.Status == PetTaskInfo.PetTaskStatus.undone && petTaskInfo.choosePets.Contains(petId))
			{
				return true;
			}
		}
		return false;
	}

	public void CheckTaskIsAchieve(bool isOpen)
	{
		if (isOpen)
		{
			this.IsCheckTaskIsAchieve = true;
		}
		if (!this.IsCheckTaskIsAchieve)
		{
			return;
		}
		for (int i = 0; i < this.m_listPetTaskInfo.get_Count(); i++)
		{
			PetTaskInfo petTaskInfo = this.m_listPetTaskInfo.get_Item(i);
			if (petTaskInfo.Status == PetTaskInfo.PetTaskStatus.achieve)
			{
				this.SendGetPetTaskReward(petTaskInfo.idx);
				return;
			}
		}
		this.IsCheckTaskIsAchieve = false;
	}

	private void ShowPetTaskResultUI(GetPetTaskRewardRes down)
	{
		ChongWuRenWuPeiZhi chongWuRenWuPeiZhi = DataReader<ChongWuRenWuPeiZhi>.Get("monster");
		ChongWuRenWuPeiZhi chongWuRenWuPeiZhi2 = DataReader<ChongWuRenWuPeiZhi>.Get("model");
		if (chongWuRenWuPeiZhi == null || chongWuRenWuPeiZhi.value.get_Count() == 0)
		{
			Debug.LogError("data_monster is null");
			return;
		}
		if (chongWuRenWuPeiZhi2 == null || chongWuRenWuPeiZhi2.value.get_Count() == 0)
		{
			Debug.LogError("data_model is null");
			return;
		}
		int num = down.task.monsterId.get_Item(0);
		int num2 = -1;
		for (int i = 0; i < chongWuRenWuPeiZhi.value.get_Count(); i++)
		{
			if (int.Parse(GameDataUtils.SplitString4Dot0(chongWuRenWuPeiZhi.value.get_Item(i))) == num)
			{
				num2 = i;
				break;
			}
		}
		if (num2 < 0)
		{
			this.ShowRewards(down);
			Debug.LogError(string.Concat(new object[]
			{
				"find_index < 0, taskId = ",
				down.task.taskId,
				", monster_id = ",
				num
			}));
			return;
		}
		if (down.task.choosePets.get_Count() == 0)
		{
			this.ShowRewards(down);
			Debug.LogError("find_index < 0, taskId = " + down.task.taskId + ", down.task.choosePets.Count == 0");
			return;
		}
		int monster_modelId = int.Parse(chongWuRenWuPeiZhi2.value.get_Item(num2));
		int petId = down.task.choosePets.get_Item(0);
		UIManagerControl.Instance.OpenUI("PetTaskResultUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		if (down.success)
		{
			this.ShowAsSuccess(down, monster_modelId, petId);
		}
		else
		{
			this.ShowAsFail(down, monster_modelId, petId);
		}
	}

	private void ShowAsSuccess(GetPetTaskRewardRes down, int monster_modelId, int petId)
	{
		PetTaskResultUIView.Instance.ShowAsSuccess(petId, monster_modelId, delegate
		{
			PetTaskResultUIView.Instance.ShowSuccessSpine();
			FXSpineManager.Instance.ShowBoxSpine1(delegate
			{
				this.ShowRewards(down);
				TimerHeap.AddTimer(1000u, 0, delegate
				{
					UIManagerControl.Instance.HideUI("PetTaskResultUI");
					this.CheckTaskIsAchieve(false);
				});
			}, 0.65f, 0f, -50f);
		});
	}

	private void ShowAsFail(GetPetTaskRewardRes down, int monster_modelId, int petId)
	{
		PetTaskResultUIView.Instance.ShowAsFail(petId, monster_modelId, delegate
		{
			PetTaskResultUIView.Instance.ShowFailSpine();
			FXSpineManager.Instance.ShowBoxSpine1(delegate
			{
				this.ShowRewards(down);
				TimerHeap.AddTimer(1000u, 0, delegate
				{
					UIManagerControl.Instance.HideUI("PetTaskResultUI");
					this.CheckTaskIsAchieve(false);
				});
			}, 0.65f, 0f, -50f);
		});
	}

	private void ShowRewards(GetPetTaskRewardRes down)
	{
		for (int i = 0; i < down.rewards.get_Count(); i++)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetItemName(down.rewards.get_Item(i).cfgId, true, 0L) + "x" + down.rewards.get_Item(i).count);
		}
	}
}
