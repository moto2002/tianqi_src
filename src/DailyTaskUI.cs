using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyTaskUI : UIBase
{
	private GameObject mItemList;

	private Text mTxLiveness;

	private RectTransform mContent;

	private GameObject mBuyType;

	private Toggle mTogGold;

	private DailyTaskType mCurTaskType;

	private Text mTxTips;

	private Slider mSlider;

	private ButtonCustom mBtnDailyTask;

	private ButtonCustom mBtnFindTask;

	private ButtonCustom mBtnLimitTask;

	private GameObject mDailyTaskSelect;

	private GameObject mFindTaskSelect;

	private GameObject mLimitTaskSelect;

	private GameObject mDailyTaskPoint;

	private GameObject mFindTaskPoint;

	private GameObject mLimitTaskPoint;

	private Transform mFxMask;

	private List<DailyTaskItem> mItems = new List<DailyTaskItem>();

	private List<HuoYueDuJiangLi> mBoxDatas;

	private BoxItem[] mBoxList;

	private DailyTaskItem mLastFindItem;

	private int mLastFindTimes;

	public Transform FxMask
	{
		get
		{
			return this.mFxMask;
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mBoxDatas = DataReader<HuoYueDuJiangLi>.DataList;
		this.mItemList = base.FindTransform("Grid").get_gameObject();
		this.mTxLiveness = UIHelper.GetText(base.get_transform(), "Liveness/Text");
		this.mContent = UIHelper.Get<RectTransform>(base.get_transform(), "Content");
		this.mTxTips = UIHelper.GetText(this.mContent.get_transform(), "Tips");
		this.mBtnDailyTask = base.FindTransform("BtnDailyTask").GetComponent<ButtonCustom>();
		this.mBtnFindTask = base.FindTransform("BtnFindTask").GetComponent<ButtonCustom>();
		this.mBtnLimitTask = base.FindTransform("BtnLimitTask").GetComponent<ButtonCustom>();
		this.mDailyTaskSelect = UIHelper.GetObject(this.mBtnDailyTask.get_transform(), "Select");
		this.mFindTaskSelect = UIHelper.GetObject(this.mBtnFindTask.get_transform(), "Select");
		this.mLimitTaskSelect = UIHelper.GetObject(this.mBtnLimitTask.get_transform(), "Select");
		this.mDailyTaskPoint = UIHelper.GetObject(this.mBtnDailyTask.get_transform(), "Point");
		this.mFindTaskPoint = UIHelper.GetObject(this.mBtnFindTask.get_transform(), "Point");
		this.mLimitTaskPoint = UIHelper.GetObject(this.mBtnLimitTask.get_transform(), "Point");
		UIHelper.GetText(this.mBtnDailyTask.get_transform(), "Text").set_text(GameDataUtils.GetChineseContent(301021, false));
		UIHelper.GetText(this.mBtnLimitTask.get_transform(), "Text").set_text(GameDataUtils.GetChineseContent(301022, false));
		UIHelper.GetText(this.mBtnFindTask.get_transform(), "Text").set_text(GameDataUtils.GetChineseContent(301023, false));
		UIHelper.GetText(this.mDailyTaskSelect.get_transform(), "Text").set_text(GameDataUtils.GetChineseContent(301021, false));
		UIHelper.GetText(this.mLimitTaskSelect.get_transform(), "Text").set_text(GameDataUtils.GetChineseContent(301022, false));
		UIHelper.GetText(this.mFindTaskSelect.get_transform(), "Text").set_text(GameDataUtils.GetChineseContent(301023, false));
		this.mSlider = UIHelper.Get<Slider>(base.get_transform(), "RightPanel/Slider");
		this.mBoxList = UIHelper.GetObject(base.get_transform(), "RightPanel/Boxs").GetComponentsInChildren<BoxItem>(true);
		this.mBuyType = UIHelper.GetObject(base.get_transform(), "BuyType");
		this.mTogGold = UIHelper.Get<Toggle>(this.mBuyType, "Gold");
		this.mTogGold.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeToggle));
		UIHelper.GetButton(base.get_transform(), "BtnTips").get_onClick().AddListener(new UnityAction(this.ShowTips));
		this.mFxMask = base.FindTransform("FxMask");
		this.mBtnFindTask.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSwitchButton);
		this.mBtnDailyTask.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSwitchButton);
		this.mBtnLimitTask.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSwitchButton);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110007), string.Empty, delegate
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshTaskUI();
		WaitUI.CloseUI(0u);
	}

	protected override void OnDisable()
	{
		CurrenciesUIViewModel.Show(false);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.DailyTaskNty, new Callback(this.RefreshTaskUI));
		EventDispatcher.AddListener(EventNames.DailyTaskActivityNty, new Callback(this.RefreshSlider));
		EventDispatcher.AddListener(EventNames.DailyTaskResetNty, new Callback(this.OnDailyTaskResetNty));
		EventDispatcher.AddListener(EventNames.DailyTaskFindRes, new Callback(this.OnDailyTaskFindRes));
		EventDispatcher.AddListener(EventNames.OnDissolveGuildRes, new Callback(this.ExecuteGuildTask));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.DailyTaskNty, new Callback(this.RefreshTaskUI));
		EventDispatcher.RemoveListener(EventNames.DailyTaskActivityNty, new Callback(this.RefreshSlider));
		EventDispatcher.RemoveListener(EventNames.DailyTaskResetNty, new Callback(this.OnDailyTaskResetNty));
		EventDispatcher.RemoveListener(EventNames.DailyTaskFindRes, new Callback(this.OnDailyTaskFindRes));
		EventDispatcher.RemoveListener(EventNames.OnDissolveGuildRes, new Callback(this.ExecuteGuildTask));
	}

	private void OnDailyTaskFindRes()
	{
		if (this.mLastFindItem != null)
		{
			XDict<int, long> itemDataDict = DisplayItemManager.Instance.ItemDataDict;
			itemDataDict.Clear();
			for (int i = 0; i < this.mLastFindItem.FindReward.get_Count(); i++)
			{
				itemDataDict.Add(this.mLastFindItem.FindReward.get_Item(i).goodsId, this.mLastFindItem.FindReward.get_Item(i).minNum * (long)this.mLastFindTimes);
			}
			DisplayItemManager.Instance.AddItemBubble();
		}
		this.RefreshList();
	}

	private void OnClickSwitchButton(GameObject go)
	{
		if (go == this.mBtnDailyTask.get_gameObject())
		{
			this.mCurTaskType = DailyTaskType.DAILY;
		}
		else if (go == this.mBtnFindTask.get_gameObject())
		{
			this.mCurTaskType = DailyTaskType.FIND;
		}
		else if (go == this.mBtnLimitTask.get_gameObject())
		{
			this.mCurTaskType = DailyTaskType.LIMIT;
		}
		this.SwitchButton(this.mCurTaskType);
	}

	private void SwitchButton(DailyTaskType type)
	{
		this.mDailyTaskSelect.SetActive(type == DailyTaskType.DAILY);
		this.mFindTaskSelect.SetActive(type == DailyTaskType.FIND);
		this.mLimitTaskSelect.SetActive(type == DailyTaskType.LIMIT);
		this.mBuyType.SetActive(type == DailyTaskType.FIND);
		this.mContent.set_sizeDelta(new Vector2(this.mContent.get_sizeDelta().x, (type != DailyTaskType.FIND) ? 514f : 465f));
		this.RefreshList();
	}

	private void RefreshTaskUI()
	{
		this.mTxLiveness.set_text(string.Format(GameDataUtils.GetChineseContent(301024, false), DailyTaskManager.Instance.totalActivity));
		this.RefreshList();
		this.RefreshSlider();
	}

	private void RefreshList()
	{
		bool flag = true;
		bool flag2 = false;
		bool flag3 = false;
		this.ClearAllList();
		List<DailyTask> list = DailyTaskManager.Instance.SortDailyList(this.mCurTaskType);
		for (int i = 0; i < list.get_Count(); i++)
		{
			MeiRiMuBiao meiRiMuBiao = DataReader<MeiRiMuBiao>.Get(list.get_Item(i).taskId);
			if (meiRiMuBiao.Retrieve == 1)
			{
				SystemOpen systemOpen = DataReader<SystemOpen>.Get(meiRiMuBiao.sysId);
				flag3 = (EntityWorld.Instance.EntSelf.Lv >= systemOpen.level && MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId));
			}
			if (this.mCurTaskType == DailyTaskType.DAILY && meiRiMuBiao.activity == 2)
			{
				flag = false;
				this.CreateItem(list.get_Item(i), this.mCurTaskType);
			}
			else if (this.mCurTaskType == DailyTaskType.FIND && flag3 && list.get_Item(i).canFindTimes >= 0)
			{
				flag = false;
				this.CreateItem(list.get_Item(i), this.mCurTaskType);
			}
			else if (this.mCurTaskType == DailyTaskType.LIMIT && meiRiMuBiao.activity == 1)
			{
				flag = false;
				this.CreateItem(list.get_Item(i), this.mCurTaskType);
			}
			if (!flag2 && list.get_Item(i).canFindTimes > 0 && flag3)
			{
				flag2 = true;
			}
		}
		this.mTxTips.get_gameObject().SetActive(flag);
		if (flag)
		{
			this.mTxTips.set_text(GameDataUtils.GetChineseContent((this.mCurTaskType != DailyTaskType.DAILY) ? 300022 : 300023, false));
		}
		this.mFindTaskPoint.SetActive(flag2);
		this.mLimitTaskPoint.SetActive(DailyTaskManager.Instance.HasLimitTaskOpen);
	}

	private DailyTaskItem CreateItem(DailyTask data, DailyTaskType type)
	{
		DailyTaskItem dailyTaskItem = this.mItems.Find((DailyTaskItem e) => e.get_gameObject().get_name() == "Unused");
		if (dailyTaskItem == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("DailyTaskItem");
			UGUITools.SetParent(this.mItemList.get_gameObject(), instantiate2Prefab, false);
			dailyTaskItem = instantiate2Prefab.GetComponent<DailyTaskItem>();
			this.mItems.Add(dailyTaskItem);
		}
		dailyTaskItem.get_gameObject().SetActive(true);
		dailyTaskItem.get_gameObject().set_name(data.taskId.ToString());
		dailyTaskItem.SetData(data, type, this.mTogGold.get_isOn(), new Action<DailyTaskItem>(this.OnClickButton));
		return dailyTaskItem;
	}

	private void ClearAllList()
	{
		for (int i = 0; i < this.mItems.get_Count(); i++)
		{
			this.mItems.get_Item(i).SetUnused();
		}
	}

	private void RefreshSlider()
	{
		bool flag = false;
		for (int i = 0; i < this.mBoxDatas.get_Count(); i++)
		{
			BoxItem.BoxState boxState = this.mBoxList[i].SetData(this.mBoxDatas.get_Item(i));
			this.mBoxList[i].get_gameObject().SetActive(true);
			if (!flag)
			{
				flag = (boxState == BoxItem.BoxState.canBox);
			}
		}
		int numericalValue = this.mBoxDatas.get_Item(this.mBoxDatas.get_Count() - 1).numericalValue;
		this.mSlider.set_value((float)DailyTaskManager.Instance.totalActivity / (float)numericalValue);
		this.mDailyTaskPoint.SetActive(flag);
	}

	private void OnClickButton(DailyTaskItem item)
	{
		if (item.Type == DailyTaskType.DAILY)
		{
			if (item.CanShowBuy)
			{
				switch (item.DailyData.system)
				{
				case 3:
					DefendFightManager.Instance.OnBuyDefendTimes(SpecialFightMode.Protect);
					goto IL_E4;
				case 4:
					DefendFightManager.Instance.OnBuyDefendTimes(SpecialFightMode.Save);
					goto IL_E4;
				case 5:
					DefendFightManager.Instance.OnBuyDefendTimes(SpecialFightMode.Hold);
					goto IL_E4;
				case 9:
					SpecialFightManager.Instance.OnBuyExperienceTimes();
					goto IL_E4;
				case 12:
					MemCollabManager.Instance.BuyExtentTimes();
					goto IL_E4;
				}
				LinkNavigationManager.SystemLink(item.DailyData.sysId, true, null);
				IL_E4:;
			}
			else if (item.DailyData.id == 12040)
			{
				if (MainTaskManager.Instance.GuildTaskId > 0)
				{
					this.ExecuteGuildTask();
				}
				else if (SystemOpenManager.IsSystemClickOpen(45, 0, true))
				{
					if (GuildManager.Instance.MyGuildnfo == null)
					{
						return;
					}
					if (GuildManager.Instance.GuildTaskTotalTimes <= GuildManager.Instance.MyGuildnfo.taskedCount)
					{
						UIManagerControl.Instance.ShowToastText(GuildManager.Instance.GetTipContentByKey("BuildWord"));
						return;
					}
					GuildManager.Instance.SendGuildBuildReq(GuildBuildType.GBT.GUILD_TASK);
				}
			}
			else
			{
				LinkNavigationManager.SystemLink(item.DailyData.sysId, true, null);
			}
		}
		else if (item.Type == DailyTaskType.FIND)
		{
			if (item.Task.canFindTimes > 0)
			{
				FindTaskUI uibase = UIManagerControl.Instance.OpenUI("FindTaskUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as FindTaskUI;
				uibase.OnOpen("找回任务", (float)item.Task.canFindTimes, (float)((!this.mTogGold.get_isOn()) ? item.DiamondPrice : item.GoldPrice), item.TaskName, this.mTogGold.get_isOn(), delegate(float value)
				{
					uibase.SetDetailFindTask();
				}, delegate(int value)
				{
					this.mLastFindItem = item;
					this.mLastFindTimes = value;
					DailyTaskManager.Instance.SendDailyTaskPrizeReq(item.Task.taskId, (!this.mTogGold.get_isOn()) ? 1 : 2, value);
				});
			}
		}
		else if (item.Type == DailyTaskType.LIMIT)
		{
			switch (item.CurrentLimit)
			{
			case 10001:
				InstanceManagerUI.OpenGangFightUI();
				break;
			case 10002:
				MultiPlayerManager.Instance.OpenMultiPlayerUI(10002, "多人活动");
				break;
			case 10003:
				LinkNavigationManager.OpenMushroomHitUI();
				break;
			case 10006:
				LinkNavigationManager.OpenMultiPVPUI();
				break;
			}
			if (item.DailyData.id == 12030)
			{
				if (!GuildManager.Instance.IsJoinInGuild())
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(34157, false));
					return;
				}
				if (!GuildManager.Instance.IsGuildFieldOpen)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513118, false));
					return;
				}
				if (!SystemOpenManager.IsSystemClickOpen(45, 0, true))
				{
					return;
				}
				UIManagerControl.Instance.OpenUI("GuildUI", null, false, UIType.FullScreen);
				UIManagerControl.Instance.OpenUI("GuildActivityCenterUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			}
			else if (item.DailyData.id == 12050)
			{
				if (!GuildManager.Instance.IsJoinInGuild())
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(34157, false));
					return;
				}
				if (!item.GuildWarOpen)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513118, false));
					return;
				}
				if (!SystemOpenManager.IsSystemClickOpen(45, 0, true))
				{
					return;
				}
				UIManagerControl.Instance.OpenUI("GuildUI", null, false, UIType.FullScreen);
				UIManagerControl.Instance.OpenUI("GuildWarVSInfoUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
			}
		}
	}

	private void OnChangeToggle(bool value)
	{
		this.RefreshList();
	}

	private void ShowTips()
	{
		FindTipsUI findTipsUI = UIManagerControl.Instance.OpenUI("FindTipsUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as FindTipsUI;
		findTipsUI.OnOpen(GameDataUtils.GetChineseContent(301025, false), GameDataUtils.GetChineseContent(300021, false));
	}

	private void OnDailyTaskResetNty()
	{
		if (base.get_gameObject().get_activeInHierarchy())
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(300023, false), new Action(this.RefreshTaskUI), GameDataUtils.GetChineseContent(500011, false), "button_yellow_1", UINodesManager.MiddleUIRoot);
		}
	}

	private void ExecuteGuildTask()
	{
		UIStackManager.Instance.PopTownUI();
		MainTaskManager.Instance.ExecuteTask(MainTaskManager.Instance.GuildTaskId, false);
	}
}
