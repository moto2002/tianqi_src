using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZeroTaskUI : UIBase
{
	private GameObject mTaskPanel;

	private Text mTxProgress;

	private Text mTxFreeTimes;

	private Text mTxNextTime;

	private TimeCountDown mFreeTimesCountDown;

	private List<ZeroTaskItem> mZeroTaskList;

	private int mCanAcceptTaskCount;

	private int mReceivedTaskCount;

	private int mCommintTaskCount;

	private int mFinishedTaskCount;

	private bool mIsUseFreeTime;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mZeroTaskList = new List<ZeroTaskItem>();
		this.mTxProgress = base.FindTransform("txFinishTimes").GetComponent<Text>();
		this.mTxFreeTimes = base.FindTransform("txFreeTimes").GetComponent<Text>();
		this.mTxNextTime = base.FindTransform("txNextTime").GetComponent<Text>();
		this.mTaskPanel = base.FindTransform("Grid").get_gameObject();
		base.FindTransform("BtnRefresh").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefresh);
		base.FindTransform("BtnExtraReward").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickExtraReward);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
		WaitUI.CloseUI(0u);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.RemoveZeroTaskCountDown();
		this.ClearStatus();
	}

	private void ClearStatus()
	{
		this.mCanAcceptTaskCount = 0;
		this.mReceivedTaskCount = 0;
		this.mCommintTaskCount = 0;
		this.mFinishedTaskCount = 0;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.SortTaskList, new Callback(this.RefreshUI));
		EventDispatcher.AddListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.RefreshTaskList));
		EventDispatcher.AddListener(EventNames.PushOtherData, new Callback(this.RefreshInfoPanel));
		EventDispatcher.AddListener(EventNames.RefreshZeroTaskRes, new Callback(this.OnRefreshZeroTaskRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.SortTaskList, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.RefreshTaskList));
		EventDispatcher.RemoveListener(EventNames.PushOtherData, new Callback(this.RefreshInfoPanel));
		EventDispatcher.RemoveListener(EventNames.RefreshZeroTaskRes, new Callback(this.OnRefreshZeroTaskRes));
	}

	private void RefreshUI()
	{
		this.RefreshTaskList(null);
		this.RefreshInfoPanel();
	}

	private void RefreshInfoPanel()
	{
		this.mTxProgress.set_text(string.Format("今日已完成：<color=#50321e>{0}/{1}</color>", MainTaskManager.Instance.ZeroMaxTimes - MainTaskManager.Instance.ZeroTaskTimes, MainTaskManager.Instance.ZeroMaxTimes));
		if (MainTaskManager.Instance.ZeroTaskFreeTimes > 0)
		{
			this.mTxFreeTimes.set_text(string.Format("今日免费刷新次数：<color=#50321e>{0}</color>", MainTaskManager.Instance.ZeroTaskFreeTimes));
		}
		else
		{
			this.mTxFreeTimes.set_text(string.Format("当前拥有零城任务刷新券：<color=#50321e>{0}</color>张", BackpackManager.Instance.OnGetGoodCount(39003)));
		}
		this.mTxNextTime.set_text(string.Format("下次获得次数倒计时：<color=#50321e>00:00:00</color>", new object[0]));
		if (MainTaskManager.Instance.ZeroTaskRefreshTime >= 0)
		{
			long num = (long)MainTaskManager.Instance.ZeroTaskRefreshTime - (DateTime.get_Now().ToUniversalTime().get_Ticks() - 621355968000000000L) / 10000000L;
			if (num > 0L)
			{
				this.AddZeroTaskCountDown((int)num);
			}
		}
	}

	private void RefreshTaskList(Package.Task task = null)
	{
		this.CheckTaskGroupState(MainTaskManager.Instance.ZeroTaskId);
		this.CreateTaskList(MainTaskManager.Instance.ZeroTaskId);
	}

	private void CheckTaskGroupState(int[] ids)
	{
		this.ClearStatus();
		for (int i = 1; i < ids.Length; i++)
		{
			this.CheckTaskGroupState(MainTaskManager.Instance.GetTask(ids[i], true));
		}
	}

	private void CheckTaskGroupState(BaseTask task)
	{
		if (task == null)
		{
			return;
		}
		if (task.Task.extParams.get_Item(4) <= 2)
		{
			this.mCanAcceptTaskCount++;
		}
		if (task.Task.extParams.get_Item(4) == 3)
		{
			this.mReceivedTaskCount++;
		}
		if (task.Task.extParams.get_Item(4) == 4)
		{
			this.mCommintTaskCount++;
		}
		if (task.Task.extParams.get_Item(4) == 5)
		{
			this.mFinishedTaskCount++;
		}
	}

	private void CreateTaskList(int[] ids)
	{
		for (int i = 0; i < this.mZeroTaskList.get_Count(); i++)
		{
			this.mZeroTaskList.get_Item(i).SetUnused();
		}
		bool canAccept = MainTaskManager.Instance.ZeroTaskTimes > this.mCommintTaskCount + this.mReceivedTaskCount;
		for (int j = 1; j < ids.Length; j++)
		{
			this.CreateTaskItem(MainTaskManager.Instance.GetTask(ids[j], true), canAccept);
		}
	}

	private void CreateTaskItem(BaseTask task, bool canAccept)
	{
		if (task != null)
		{
			ZeroTaskItem zeroTaskItem = this.mZeroTaskList.Find((ZeroTaskItem e) => e.get_gameObject().get_name() == "Unused");
			if (zeroTaskItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ZeroTaskItem");
				UGUITools.SetParent(this.mTaskPanel, instantiate2Prefab, false);
				zeroTaskItem = instantiate2Prefab.GetComponent<ZeroTaskItem>();
				zeroTaskItem.EventHandler = new Action<BaseTask>(this.OnClickTask);
				this.mZeroTaskList.Add(zeroTaskItem);
			}
			zeroTaskItem.SetData(task, canAccept);
			zeroTaskItem.get_gameObject().set_name("ZeroTaskItem" + task.Task.extParams.get_Item(0));
			zeroTaskItem.get_gameObject().SetActive(true);
		}
	}

	private void AddZeroTaskCountDown(int remianTime)
	{
		this.RemoveZeroTaskCountDown();
		if (MainTaskManager.Instance.ZeroTaskFreeTimes >= MainTaskManager.Instance.ZeroTaskFreeMaxTimes)
		{
			return;
		}
		this.mFreeTimesCountDown = new TimeCountDown(remianTime, TimeFormat.SECOND, delegate
		{
			this.mTxNextTime.set_text(string.Format("下次获得次数倒计时：<color=#50321e>{0}</color>", TimeConverter.GetTime(this.mFreeTimesCountDown.GetSeconds(), TimeFormat.HHMMSS)));
		}, null, true);
	}

	public void RemoveZeroTaskCountDown()
	{
		if (this.mFreeTimesCountDown != null)
		{
			this.mFreeTimesCountDown.Dispose();
			this.mFreeTimesCountDown = null;
		}
	}

	private void OnClickTask(BaseTask task)
	{
		if (MainTaskManager.Instance.ZeroTaskTimes <= 0)
		{
			return;
		}
		switch (task.Task.extParams.get_Item(4))
		{
		case 1:
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310032, false));
			break;
		case 2:
			if (MainTaskManager.Instance.ZeroTaskTimes > this.mCommintTaskCount + this.mReceivedTaskCount)
			{
				MainTaskManager.Instance.SendTaskGroupAcceptReq(task.Task.extParams.get_Item(0));
			}
			break;
		case 3:
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621270, false), GameDataUtils.GetChineseContent(310037, false), null, delegate
			{
				MainTaskManager.Instance.SendAbandonTaskReq(task.Task.extParams.get_Item(0));
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			break;
		case 4:
			MainTaskManager.Instance.SendGetZeroCityPrizeReq(task.Task.extParams.get_Item(0));
			break;
		}
	}

	private void OnClickRefresh(GameObject go)
	{
		if (MainTaskManager.Instance.ZeroTaskTimes <= 0)
		{
			UIManagerControl.Instance.ShowToastText("今日次数已用完，明日再来！");
			return;
		}
		if (MainTaskManager.Instance.ZeroTaskRefreshLock)
		{
			UIManagerControl.Instance.ShowToastText("刷新过于频繁，请稍后！");
			return;
		}
		if (this.mCanAcceptTaskCount <= 0 && this.mFinishedTaskCount <= 0)
		{
			UIManagerControl.Instance.ShowToastText("当前所有任务已被接取，无法刷新！");
			return;
		}
		this.mIsUseFreeTime = (MainTaskManager.Instance.ZeroTaskFreeTimes > 0);
		if (this.mIsUseFreeTime || BackpackManager.Instance.OnGetGoodCount(39003) > 0L)
		{
			MainTaskManager.Instance.SendZeroCityRefreshReq();
		}
		else
		{
			LinkNavigationManager.ItemNotEnoughToLink(39003, true, delegate
			{
				LinkNavigationManager.SystemLink(102, true, null);
				this.Show(false);
			}, true);
		}
	}

	private void OnClickExtraReward(GameObject go)
	{
		EWaiRenWuJiangLi eWaiRenWuJiangLi = MainTaskManager.Instance.ExtraRewardDatas.Find((EWaiRenWuJiangLi e) => e.taskType == 8);
		if (eWaiRenWuJiangLi != null)
		{
			XDict<int, long> xDict = new XDict<int, long>();
			for (int i = 0; i < eWaiRenWuJiangLi.reward.get_Count(); i++)
			{
				xDict.Add(eWaiRenWuJiangLi.reward.get_Item(i).key, eWaiRenWuJiangLi.reward.get_Item(i).value);
			}
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
			rewardUI.SetRewardItem("零城任务额外奖励", xDict.Keys, xDict.Values, MainTaskManager.Instance.ZeroTaskTimes > 0, true, null, null);
		}
	}

	private void OnRefreshZeroTaskRes()
	{
		if (!this.mIsUseFreeTime)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310045, false));
		}
	}
}
