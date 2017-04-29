using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MainTaskUI : BaseUIBehaviour
{
	private RectTransform mMainTask;

	private RectTransform mContent;

	private RectTransform mContentMask;

	private GameObject mGoZeroTaskItem;

	private List<MainTaskItem> mItems = new List<MainTaskItem>();

	private float mHeight;

	private List<int> mTaskIds = new List<int>();

	private MainTaskItem mTopTaskItem;

	private uint mDelayId;

	private int mFXId1;

	private int mFXId2;

	private bool mIsInitUI;

	private int mFxId1;

	private int mFxId2;

	private int mFxId3;

	public void AwakeSelf()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.CheckInitUI();
	}

	private void CheckInitUI()
	{
		if (!this.mIsInitUI)
		{
			this.mMainTask = (base.FindTransform("MainTask") as RectTransform);
			this.mContent = base.FindTransform("Content").GetComponent<RectTransform>();
			this.mContentMask = base.FindTransform("Mask").GetComponent<RectTransform>();
			this.mGoZeroTaskItem = ResourceManager.GetInstantiate2Prefab("ZeroTaskProgressUI");
			UGUITools.SetParent(this.mContent.get_gameObject(), this.mGoZeroTaskItem, false);
			this.mGoZeroTaskItem.GetComponent<ZeroTaskProgressUI>().AwakeSelf();
			this.mIsInitUI = true;
		}
	}

	private void Start()
	{
		this.RefreshTaskList();
	}

	private void OnEnable()
	{
		if (MainTaskManager.Instance.IsRelease)
		{
			MainTaskManager.Instance.IsRelease = false;
			this.RefreshTaskList();
		}
		if (this.mTopTaskItem != null)
		{
			this.mTopTaskItem.SetTop(true);
			this.mTopTaskItem = null;
		}
		this.CheckExtraReward();
	}

	private void OnDisable()
	{
		this.mTopTaskItem = null;
		EventDispatcher.Broadcast(EventNames.EndNav);
	}

	private void OnApplicationPause(bool isPause)
	{
		if (isPause)
		{
			MainTaskManager.Instance.SaveTaskCounts();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		MainTaskManager.Instance.SaveTaskCounts();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.SystemOpenProgressChange, new Callback(this.SetTaskMaskHeight));
		EventDispatcher.AddListener<BaseTask>(EventNames.FinishTaskNty, new Callback<BaseTask>(this.OnFinishTaskNty));
		EventDispatcher.AddListener(EventNames.SortTaskList, new Callback(this.RefreshTaskList));
		EventDispatcher.AddListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.RefreshTaskList));
		EventDispatcher.AddListener<bool>(EventNames.ShowPayUI, new Callback<bool>(this.OnShowPayUI));
		EventDispatcher.AddListener<bool>(EventNames.ShowTaskDescUI, new Callback<bool>(this.OnShowPayUI));
		EventDispatcher.AddListener<bool>(RankUpChangeUIEvent.ShowUI, new Callback<bool>(this.OnShowPayUI));
		EventDispatcher.AddListener(EventNames.PushOtherData, new Callback(this.CheckExtraReward));
		EventDispatcher.AddListener(EventNames.GetZeroTaskRewardRes, new Callback(this.OnGetZeroTaskRewardRes));
		EventDispatcher.AddListener<int, List<ItemBriefInfo>>(EventNames.GetExtraRewardRes, new Callback<int, List<ItemBriefInfo>>(this.OnGetExtraRewardRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.SystemOpenProgressChange, new Callback(this.SetTaskMaskHeight));
		EventDispatcher.RemoveListener<BaseTask>(EventNames.FinishTaskNty, new Callback<BaseTask>(this.OnFinishTaskNty));
		EventDispatcher.RemoveListener(EventNames.SortTaskList, new Callback(this.RefreshTaskList));
		EventDispatcher.RemoveListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.RefreshTaskList));
		EventDispatcher.RemoveListener<bool>(EventNames.ShowPayUI, new Callback<bool>(this.OnShowPayUI));
		EventDispatcher.RemoveListener<bool>(EventNames.ShowTaskDescUI, new Callback<bool>(this.OnShowPayUI));
		EventDispatcher.RemoveListener<bool>(RankUpChangeUIEvent.ShowUI, new Callback<bool>(this.OnShowPayUI));
		EventDispatcher.RemoveListener(EventNames.PushOtherData, new Callback(this.CheckExtraReward));
		EventDispatcher.RemoveListener(EventNames.GetZeroTaskRewardRes, new Callback(this.OnGetZeroTaskRewardRes));
		EventDispatcher.RemoveListener<int, List<ItemBriefInfo>>(EventNames.GetExtraRewardRes, new Callback<int, List<ItemBriefInfo>>(this.OnGetExtraRewardRes));
	}

	private void OnFinishTaskNty(BaseTask cell)
	{
		if (cell != null)
		{
			if (cell is CollectTask)
			{
				CaiJiPeiZhi curGoods = (cell as CollectTask).CurGoods;
				if (curGoods != null && curGoods.model == 1240)
				{
					return;
				}
			}
			DisplayItemManager.Instance.AddItemBubble();
			if (cell.Data.finishEffect != 1)
			{
				this.PlayCommitEffect();
			}
		}
	}

	private void RefreshTaskList()
	{
		this.CheckInitUI();
		this.ClearAllItems();
		this.CreateTaskList(this.SortTaskList());
		this.ShowZeroTaskProgressUI(true);
		this.SetTaskMaskHeight();
	}

	private List<int> SortTaskList()
	{
		Dictionary<int, BaseTask> taskMap = MainTaskManager.Instance.TaskMap;
		List<int> list = new List<int>(taskMap.get_Keys());
		List<int> list2 = null;
		List<int> list3 = null;
		this.mTaskIds.Clear();
		int num = 0;
		for (int i = 0; i < list.get_Count(); i++)
		{
			int num2 = list.get_Item(i);
			if (num2 > 0)
			{
				BaseTask baseTask = taskMap.get_Item(num2);
				if (baseTask.Task.taskType == Package.Task.TaskType.ZeroCity && baseTask.Task.extParams.get_Item(4) <= 2)
				{
					list.set_Item(i, -num2);
				}
				else if (baseTask.Task.status == Package.Task.TaskStatus.TaskFinished && baseTask.Task.taskType != Package.Task.TaskType.ChangeCareer)
				{
					this.mTaskIds.Add(num2);
					list.set_Item(i, -num2);
				}
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			int num2 = list.get_Item(j);
			if (num2 > 0 && taskMap.get_Item(num2).Task.status == Package.Task.TaskStatus.TaskNotOpen)
			{
				this.mTaskIds.Add(num2);
				list.set_Item(j, -num2);
			}
		}
		if (MainTaskManager.Instance.GuildTaskId > 0 && MainTaskManager.Instance.IsFirstGuildTask)
		{
			for (int k = 0; k < list.get_Count(); k++)
			{
				int num2 = list.get_Item(k);
				if (num2 == MainTaskManager.Instance.GuildTaskId)
				{
					num = num2;
					list.set_Item(k, -num2);
					break;
				}
			}
			if (num > 0)
			{
				MainTaskManager.Instance.IsShowGuildTaskGuide = true;
			}
		}
		else
		{
			int num3 = -1;
			for (int l = 0; l < list.get_Count(); l++)
			{
				int num2 = list.get_Item(l);
				if (num2 > 0 && taskMap.get_Item(num2).isTempTop)
				{
					if (num3 < 0)
					{
						num3 = l;
					}
					else if (num2 > list.get_Item(num3))
					{
						num3 = l;
					}
				}
			}
			if (num3 >= 0)
			{
				num = list.get_Item(num3);
				list.set_Item(num3, -num);
			}
		}
		for (int m = 0; m < list.get_Count(); m++)
		{
			int num2 = list.get_Item(m);
			if (num2 > 0 && taskMap.get_Item(num2).Task.status == Package.Task.TaskStatus.WaitingToClaimPrize)
			{
				if (list3 == null)
				{
					list3 = new List<int>();
				}
				list3.Add(num2);
				list.set_Item(m, -num2);
			}
		}
		for (int n = 0; n < list.get_Count(); n++)
		{
			int num2 = list.get_Item(n);
			if (num2 > 0 && taskMap.get_Item(num2).Task.taskType == Package.Task.TaskType.BranchTask)
			{
				if (list2 == null)
				{
					list2 = new List<int>();
				}
				list2.Add(num2);
				list.set_Item(n, -num2);
			}
		}
		if (list2 != null)
		{
			list2.Sort(delegate(int a, int b)
			{
				if (a > b)
				{
					return 1;
				}
				if (a < b)
				{
					return -1;
				}
				return 0;
			});
		}
		int[] taskSortRult = MainTaskManager.Instance.TaskSortRult;
		if (taskSortRult != null)
		{
			for (int num4 = taskSortRult.Length - 1; num4 >= 0; num4--)
			{
				if (taskSortRult[num4] == 2)
				{
					if (list2 != null)
					{
						for (int num5 = 0; num5 < list2.get_Count(); num5++)
						{
							int num2 = list2.get_Item(num5);
							if (taskMap.get_Item(num2).Task.status != Package.Task.TaskStatus.WaitingToClaimPrize)
							{
								this.mTaskIds.Add(num2);
							}
						}
						for (int num6 = 0; num6 < list2.get_Count(); num6++)
						{
							int num2 = list2.get_Item(num6);
							if (taskMap.get_Item(num2).Task.status == Package.Task.TaskStatus.WaitingToClaimPrize)
							{
								this.mTaskIds.Add(num2);
							}
						}
					}
				}
				else
				{
					for (int num7 = 0; num7 < list.get_Count(); num7++)
					{
						int num2 = list.get_Item(num7);
						if (num2 > 0 && taskMap.get_Item(num2).Task.taskType == (Package.Task.TaskType)taskSortRult[num4])
						{
							this.mTaskIds.Add(num2);
							list.set_Item(num7, -num2);
						}
					}
				}
			}
		}
		if (taskSortRult != null && list3 != null)
		{
			for (int num8 = taskSortRult.Length - 1; num8 >= 0; num8--)
			{
				for (int num9 = 0; num9 < list3.get_Count(); num9++)
				{
					int num2 = list3.get_Item(num9);
					if (num2 > 0 && taskMap.get_Item(num2).Task.taskType == (Package.Task.TaskType)taskSortRult[num8])
					{
						this.mTaskIds.Add(num2);
						list3.set_Item(num9, -num2);
					}
				}
			}
		}
		if (num > 0)
		{
			this.mTaskIds.Add(num);
			Debug.Log("临时置顶[" + num + "]任务!!!");
		}
		return this.mTaskIds;
	}

	private void CreateTaskList(List<int> taskIds)
	{
		this.mTopTaskItem = null;
		for (int i = taskIds.get_Count() - 1; i >= 0; i--)
		{
			BaseTask task = MainTaskManager.Instance.GetTask(taskIds.get_Item(i), true);
			if (task != null)
			{
				if (task.IsActive)
				{
					if (task.Task.taskType == Package.Task.TaskType.MainTask && MainTaskManager.Instance.GodWeaponTaskId > 0)
					{
						task.IsActive = false;
					}
					else
					{
						this.CheckCreateTask(task);
					}
				}
				else if (task.Task.taskType == Package.Task.TaskType.MainTask && MainTaskManager.Instance.GodWeaponTaskId == 0)
				{
					if (task.Task.status == Package.Task.TaskStatus.TaskNotOpen || (task.Task.status == Package.Task.TaskStatus.TaskFinished && !task.hasNextTask))
					{
						this.CheckCreateTask(task);
					}
				}
				else if (task.Task.taskType == Package.Task.TaskType.ChangeCareer && !task.hasNextTask)
				{
					this.CheckCreateTask(task);
				}
			}
		}
	}

	private void CheckCreateTask(BaseTask cell)
	{
		if (this.mTopTaskItem == null)
		{
			this.mTopTaskItem = this.CreateItem(cell);
			if (this.mTopTaskItem != null)
			{
				this.mTopTaskItem.SetTop(true);
			}
		}
		else
		{
			this.CreateItem(cell);
		}
	}

	private bool GetTaskItem(int taskId, out MainTaskItem item)
	{
		if (this.mItems.get_Count() <= 0)
		{
			item = null;
			return false;
		}
		for (int i = 0; i < this.mItems.get_Count(); i++)
		{
			item = this.mItems.get_Item(i);
			if (!(item == null) && item.Task != null)
			{
				if (!(item.get_gameObject().get_name() == "Unuse"))
				{
					if (item.Task.Task.taskId == taskId)
					{
						return true;
					}
				}
			}
		}
		item = null;
		return false;
	}

	private MainTaskItem CreateItem(BaseTask data)
	{
		if (data == null)
		{
			return null;
		}
		MainTaskItem mainTaskItem = this.mItems.Find((MainTaskItem e) => e.get_name() == "Unuse");
		if (mainTaskItem == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("MainTaskItem");
			UGUITools.SetParent(this.mContent.get_gameObject(), instantiate2Prefab, false);
			mainTaskItem = instantiate2Prefab.GetComponent<MainTaskItem>();
			mainTaskItem.EventHandler = new Action<string, MainTaskItem>(this.OnClickTask);
			this.mItems.Add(mainTaskItem);
		}
		mainTaskItem.get_gameObject().set_name(data.Task.taskId.ToString());
		mainTaskItem.get_gameObject().SetActive(true);
		mainTaskItem.SetData(data);
		this.mHeight += 75f;
		return mainTaskItem;
	}

	private void OnClickTask(string type, MainTaskItem item)
	{
		if (MainTaskManager.Instance.ClickLock)
		{
			Debug.Log("等待任务返回，点慢些!!!");
			return;
		}
		if (item == null || item.Task == null || item.Task.Task == null)
		{
			Debug.Log("后端数据已经被清!!!");
			return;
		}
		BaseTask baseTask = null;
		if (MainTaskManager.Instance.GetTask(item.Task.Task.taskId, out baseTask, true) && type != null)
		{
			if (MainTaskUI.<>f__switch$map17 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
				dictionary.Add("ClickDetail", 0);
				dictionary.Add("ClickDesc", 1);
				dictionary.Add("ClickShoe", 2);
				dictionary.Add("ClickTips", 3);
				MainTaskUI.<>f__switch$map17 = dictionary;
			}
			int num;
			if (MainTaskUI.<>f__switch$map17.TryGetValue(type, ref num))
			{
				switch (num)
				{
				case 0:
					if (baseTask.Task.status == Package.Task.TaskStatus.TaskReceived || baseTask.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize)
					{
						LinkNavigationManager.OpenTaskDescUI(baseTask.Task.taskId, true, false);
					}
					break;
				case 1:
					if (!this.IsExecutingTask(baseTask.Task.taskId))
					{
						if (baseTask.Task.taskType == Package.Task.TaskType.ChangeCareer && baseTask.Task.status == Package.Task.TaskStatus.TaskFinished && !baseTask.hasNextTask)
						{
							LinkNavigationManager.OpenChangeCareerUI();
						}
						else
						{
							MainTaskManager.Instance.ExecuteTask(baseTask, false, true);
						}
					}
					break;
				case 2:
				{
					int num2 = Mathf.Max(VIPPrivilegeManager.Instance.GetVipTimesByType(16) - CityManager.Instance.UsedFlyShoeFreeTime, 0);
					if (num2 > 0 || BackpackManager.Instance.OnGetGoodCount(37001) > 0L)
					{
						this.FastExecuteTask(baseTask);
					}
					else
					{
						LinkNavigationManager.ItemNotEnoughToLink(37001, true, null, true);
					}
					break;
				}
				case 3:
					if (MainTaskManager.Instance.HasUpgradeTips(item.Task.Task))
					{
					}
					break;
				}
			}
		}
	}

	private bool IsExecutingTask(int taskId)
	{
		bool flag = this.IsSelfNav();
		if (MainTaskManager.Instance.CurTaskId == taskId && flag)
		{
			return true;
		}
		BaseTask baseTask = null;
		if (flag)
		{
			MainTaskManager.Instance.StopToNPC(false);
		}
		else if (MainTaskManager.Instance.GetTask(MainTaskManager.Instance.CurTaskId, out baseTask, true) && baseTask is CollectTask && (baseTask as CollectTask).IsCollecting)
		{
			if (MainTaskManager.Instance.CurTaskId == taskId)
			{
				return true;
			}
			(baseTask as CollectTask).OnStopCollect(false);
		}
		return false;
	}

	private bool IsSelfNav()
	{
		return EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsNavigating;
	}

	private void FastExecuteTask(BaseTask cell)
	{
		if (MainTaskManager.Instance.SwitchCityLock)
		{
			Debug.Log("嘿嘿嘿！你想干嘛？搞事吗？[手动滑稽]");
			return;
		}
		EventDispatcher.Broadcast(EventNames.StopSwitchCity);
		MainTaskManager.Instance.ExecuteTask(cell, true, true);
	}

	private void ClearAllItems()
	{
		this.mHeight = 0f;
		for (int i = 0; i < this.mItems.get_Count(); i++)
		{
			this.mItems.get_Item(i).SetDisable();
		}
	}

	private void SetTaskMaskHeight()
	{
		float num = 265f;
		if (GodWeaponProgressManager.Instance.IsSystemProgressOn())
		{
			num = 190f;
		}
		float num2 = this.mHeight;
		if (num2 > num)
		{
			num2 = num;
		}
		this.mMainTask.set_sizeDelta(new Vector2(this.mMainTask.get_sizeDelta().x, num2));
	}

	private void OnVipTimeLimitNty()
	{
		this.RefreshRingTaskTime();
		this.RefreshTaskList();
	}

	private void RefreshRingTaskTime()
	{
		int num = 0;
		if (VIPManager.Instance.LimitCardData.Times > TimeManager.Instance.UnscaleServerSecond && EntityWorld.Instance.EntSelf != null)
		{
			VipDengJi vipDengJi = DataReader<VipDengJi>.Get(EntityWorld.Instance.EntSelf.VipLv);
			if (vipDengJi != null && vipDengJi.effect.get_Count() > 1)
			{
				VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(vipDengJi.effect.get_Item(1));
				if (vipXiaoGuo != null)
				{
					num = vipXiaoGuo.value1;
				}
			}
		}
		MainTaskManager.Instance.RingMaxTimes = (int)float.Parse(DataReader<PaoHuanRenWuPeiZhi>.Get("challengeNum").value) + num;
		MainTaskManager.Instance.Ring2MaxTimes = (int)float.Parse(DataReader<PaoHuanRenWuPeiZhi>.Get("challengeNum2").value);
	}

	private void PlayCommitEffect()
	{
		if (this.mDelayId <= 0u)
		{
			this.mFXId1 = FXSpineManager.Instance.PlaySpine(1871, TownUI.Instance.FXNav, "TownUI", 2001, null, "UI", 0f, 200f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.mFXId2 = FXSpineManager.Instance.PlaySpine(1872, TownUI.Instance.FXNav, "TownUI", 2000, null, "UI", 0f, 200f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.mDelayId = TimerHeap.AddTimer(2000u, 0, new Action(this.CloseFinishSpine));
		}
	}

	private void CloseFinishSpine()
	{
		if (this.mDelayId > 0u)
		{
			FXSpineManager.Instance.DeleteSpine(this.mFXId1, true);
			FXSpineManager.Instance.DeleteSpine(this.mFXId2, true);
			FXSpineManager.Instance.PlaySpine(1873, TownUI.Instance.FXNav, "TownUI", 2000, delegate
			{
				Transform transform = TownUI.Instance.FXNav.FindChild("1872");
				if (transform != null)
				{
					Object.Destroy(transform.get_gameObject());
				}
			}, "UI", 0f, 200f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			TimerHeap.DelTimer(this.mDelayId);
			this.mDelayId = 0u;
		}
	}

	private void OnShowPayUI(bool isShow)
	{
		this.mContentMask.get_gameObject().SetActive(!isShow);
	}

	private void ShowZeroTaskProgressUI(bool isShow)
	{
		if (this.mGoZeroTaskItem != null)
		{
			if (isShow && SystemOpenManager.IsSystemOn(104))
			{
				this.mGoZeroTaskItem.SetActive(true);
			}
			else
			{
				this.mGoZeroTaskItem.SetActive(false);
			}
		}
	}

	public void CheckExtraReward()
	{
		if (MainTaskManager.Instance.ExtraRewardQueue.get_Count() > 0 && UIManagerControl.Instance.IsOpen("TownUI"))
		{
			EWaiRenWuJiangLi eWaiRenWuJiangLi = MainTaskManager.Instance.ExtraRewardQueue.Peek();
			MainTaskManager.Instance.SendGetExtraReward(eWaiRenWuJiangLi.taskType, eWaiRenWuJiangLi.time, false);
		}
	}

	private void OnGetZeroTaskRewardRes()
	{
		if (base.get_gameObject().get_activeInHierarchy() && MainTaskManager.Instance.LastGetRewardZeroTaskGroupId > 0)
		{
			int id = MainTaskManager.Instance.ZeroTaskId[MainTaskManager.Instance.LastGetRewardZeroTaskGroupId];
			MainTaskManager.Instance.LastGetRewardZeroTaskGroupId = 0;
			BaseTask baseTask;
			if (MainTaskManager.Instance.GetTask(id, out baseTask, true))
			{
				LingChengRenWuZuPeiZhi lingChengRenWuZuPeiZhi = DataReader<LingChengRenWuZuPeiZhi>.Get(baseTask.Task.extParams.get_Item(2));
				if (lingChengRenWuZuPeiZhi != null)
				{
					XDict<int, long> itemDataDict = DisplayItemManager.Instance.ItemDataDict;
					for (int i = 0; i < lingChengRenWuZuPeiZhi.reward.get_Count(); i++)
					{
						itemDataDict.Add(lingChengRenWuZuPeiZhi.reward.get_Item(i).key, (long)lingChengRenWuZuPeiZhi.reward.get_Item(i).value);
					}
					List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
					for (int j = 0; j < lingChengRenWuZuPeiZhi.rewardId.get_Count(); j++)
					{
						this.FindDropReward(dataList, lingChengRenWuZuPeiZhi.rewardId.get_Item(j), itemDataDict);
					}
				}
			}
			DisplayItemManager.Instance.AddItemBubble();
		}
	}

	private void FindDropReward(List<DiaoLuo> diaoluos, int dropId, XDict<int, long> rewards)
	{
		int lv = EntityWorld.Instance.EntSelf.Lv;
		for (int i = 0; i < diaoluos.get_Count(); i++)
		{
			DiaoLuo diaoLuo = diaoluos.get_Item(i);
			if (diaoLuo.ruleId == dropId)
			{
				if (diaoLuo.minLv == diaoLuo.maxLv && diaoLuo.minLv == 0)
				{
					rewards.Add(diaoLuo.goodsId, diaoLuo.minNum);
				}
				else if (diaoLuo.minLv == diaoLuo.maxLv && lv == diaoLuo.minLv)
				{
					rewards.Add(diaoLuo.goodsId, diaoLuo.minNum);
				}
				else if (diaoLuo.minLv < diaoLuo.maxLv && lv >= diaoLuo.minLv && lv < diaoLuo.maxLv)
				{
					rewards.Add(diaoLuo.goodsId, diaoLuo.minNum);
				}
			}
		}
	}

	private void OnGetExtraRewardRes(int state, List<ItemBriefInfo> list)
	{
		if (state >= 0)
		{
			if (state > 0 && list != null)
			{
				XDict<int, long> rewards = new XDict<int, long>();
				for (int i = 0; i < list.get_Count(); i++)
				{
					rewards.Add(list.get_Item(i).cfgId, list.get_Item(i).count);
				}
				if (rewards.Count > 0)
				{
					this.mFxId3 = FXSpineManager.Instance.ReplaySpine(this.mFxId3, 803, TownUI.Instance.FXNav, "TownUI", 14002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
					this.mFxId1 = FXSpineManager.Instance.ReplaySpine(this.mFxId1, 801, TownUI.Instance.FXNav, "TownUI", 14000, delegate
					{
						this.mFxId2 = FXSpineManager.Instance.ReplaySpine(this.mFxId2, 802, TownUI.Instance.FXNav, "TownUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
						string title = "获得任务额外奖励";
						if (MainTaskManager.Instance.LastGetExtraRewardTaskType == 8)
						{
							title = GameDataUtils.GetChineseContent(310047, false);
						}
						else if (MainTaskManager.Instance.LastGetExtraRewardTaskType == 3)
						{
							title = GameDataUtils.GetChineseContent(310046, false);
						}
						RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
						rewardUI.isClick = false;
						rewardUI.SetRewardItem(title, rewards.Keys, rewards.Values, true, false, delegate
						{
							FXSpineManager.Instance.DeleteSpine(this.mFxId1, true);
							FXSpineManager.Instance.DeleteSpine(this.mFxId2, true);
							FXSpineManager.Instance.DeleteSpine(this.mFxId3, true);
						}, null);
					}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				}
			}
			MainTaskManager.Instance.ExtraRewardQueue.Dequeue();
			this.CheckExtraReward();
		}
	}

	private void PlayGetRewardEffect()
	{
	}
}
