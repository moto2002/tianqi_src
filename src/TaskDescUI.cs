using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDescUI : UIBase
{
	public static TaskDescUI Instance;

	public static int OpenByNpc;

	public static int OpenByTaskId;

	private List<GameObject> mItems;

	private Text mTxTaskName;

	private Text mTxTaskDesc;

	private Text mTxGetButton;

	private Text mTxGetMultiplePrice;

	private Text mTxGetMultipleButton;

	private Text mTxAutoTaskTime;

	private GameObject mGrid;

	private ButtonCustom mBtnGet;

	private ButtonCustom mBtnGetMultiple;

	private uint mDelayId;

	private int mDelayTime;

	private int mGuideId;

	private XDict<int, long> mRewards;

	private List<RenWuYinDaoPeiZhi> mGuideList;

	private Image mImgBubbleTips;

	private GameObject mEffectPanel;

	private GameObject mArrowPanel;

	private int mBgSpineId1;

	private int mBgSpineId2;

	private int mArrowSpineId1;

	private int mArrowSpineId2;

	public ButtonCustom BtnGet
	{
		get
		{
			return this.mBtnGet;
		}
	}

	public ButtonCustom BtnGetMultiple
	{
		get
		{
			return this.mBtnGetMultiple;
		}
	}

	private void Awake()
	{
		TaskDescUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isClick = false;
		this.isEndNav = false;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mItems = new List<GameObject>();
		this.mGuideList = new List<RenWuYinDaoPeiZhi>();
		this.mGrid = UIHelper.GetObject(base.get_transform(), "View/Rewards/Grid");
		this.mTxTaskName = UIHelper.GetText(base.get_transform(), "View/txTaskName");
		this.mTxTaskDesc = UIHelper.GetText(base.get_transform(), "View/DescPanel/txTaskDesc");
		this.mBtnGet = UIHelper.GetCustomButton(base.get_transform(), "View/Buttons/BtnGet");
		this.mTxGetButton = UIHelper.GetText(this.mBtnGet.get_transform(), "Text");
		this.mBtnGetMultiple = UIHelper.GetCustomButton(base.get_transform(), "View/Buttons/BtnGetMultiple");
		this.mTxGetMultiplePrice = UIHelper.GetText(this.mBtnGetMultiple.get_transform(), "txPrice");
		this.mTxGetMultipleButton = UIHelper.GetText(this.mBtnGetMultiple.get_transform(), "Text");
		this.mTxAutoTaskTime = UIHelper.GetText(base.get_transform(), "View/txAutoTaskTime");
		this.mImgBubbleTips = UIHelper.GetImage(base.get_transform(), "View/GuidePanel/BubbleTips");
		this.mEffectPanel = UIHelper.GetObject(base.get_transform(), "View/GuidePanel/EffectPanel");
		this.mArrowPanel = UIHelper.GetObject(base.get_transform(), "View/GuidePanel/ArrowPanel");
		this.mBtnGet.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet);
		this.mBtnGetMultiple.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet);
		this.mRewards = new XDict<int, long>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.RefreshDescUI(null);
		if (MainTaskManager.Instance.AutoTaskId == TaskDescUI.OpenByTaskId)
		{
			this.DelayClickGet();
		}
		else
		{
			this.mTxAutoTaskTime.set_text(string.Empty);
		}
		EventDispatcher.Broadcast<bool>(EventNames.ShowTaskDescUI, true);
	}

	protected override void OnDisable()
	{
		TaskDescUI.OpenByNpc = 0;
		TaskDescUI.OpenByTaskId = 0;
		base.OnDisable();
		TimerHeap.DelTimer(this.mDelayId);
		if (TownUI.Instance != null && TownUI.Instance.FXGodWeaponProgressUI != null)
		{
			for (int i = 0; i < TownUI.Instance.FXGodWeaponProgressUI.get_childCount(); i++)
			{
				Object.Destroy(TownUI.Instance.FXGodWeaponProgressUI.GetChild(i).get_gameObject());
			}
		}
		EventDispatcher.Broadcast<bool>(EventNames.ShowTaskDescUI, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			TaskDescUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.RefreshDescUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.RefreshDescUI));
	}

	private void RefreshDescUI(Package.Task data = null)
	{
		if (!base.get_gameObject().get_activeInHierarchy())
		{
			return;
		}
		if (MainTaskManager.Instance.CurTaskId != TaskDescUI.OpenByTaskId)
		{
			return;
		}
		this.HideGuideTips();
		BaseTask task = MainTaskManager.Instance.GetTask(TaskDescUI.OpenByTaskId, true);
		if (task != null)
		{
			if (data != null && (task.Data.quickReceive == 1 || task.Data.quickComplete == 1))
			{
				this.Show(false);
			}
			else
			{
				this.RefreshReward(task);
				this.RefreshInfo(task);
			}
		}
	}

	private void RefreshInfo(BaseTask cell)
	{
		this.mTxTaskName.set_text(GameDataUtils.GetChineseContent(cell.Data.dramaIntroduce, false));
		this.mTxTaskDesc.set_text(GameDataUtils.GetChineseContent(cell.Data.introduction, false));
		this.mBtnGetMultiple.get_gameObject().SetActive(false);
		string chineseContent = GameDataUtils.GetChineseContent(310010, false);
		if (cell.Task.status == Package.Task.TaskStatus.TaskCanAccept)
		{
			chineseContent = GameDataUtils.GetChineseContent(310011, false);
			this.SetGuideTips(cell, 4);
		}
		else if (cell.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize && (MainTaskManager.Instance.IsTaskNpc(MainTaskManager.Instance.NpcId) || (cell.Data != null && cell.Data.quickComplete == 1)))
		{
			chineseContent = GameDataUtils.GetChineseContent(310012, false);
			this.SetGuideTips(cell, 5);
			if (cell.Data.doubleReward == 1)
			{
				this.mBtnGetMultiple.get_gameObject().SetActive(true);
				this.mTxGetMultiplePrice.set_text("x" + MainTaskManager.Instance.UseDiamondCount);
				this.mTxGetMultipleButton.set_text(GameDataUtils.GetChineseContent(505305, false));
			}
		}
		this.mTxGetButton.set_text(chineseContent);
	}

	private void RefreshReward(BaseTask cell)
	{
		this.ClearReward();
		cell.GetTaskRewards(ref this.mRewards, cell.Task.taskType != Package.Task.TaskType.RingTask);
		for (int i = 0; i < this.mRewards.Count; i++)
		{
			this.CreateRewards(this.mRewards.ElementKeyAt(i), this.mRewards.ElementValueAt(i));
		}
	}

	private void OnClickGet(GameObject go)
	{
		if (MainTaskManager.Instance.ClickLock)
		{
			return;
		}
		BaseTask task = MainTaskManager.Instance.GetTask(TaskDescUI.OpenByTaskId, true);
		if (task != null)
		{
			switch (task.Task.status)
			{
			case Package.Task.TaskStatus.TaskCanAccept:
				task.Accept();
				this.Show(false);
				break;
			case Package.Task.TaskStatus.TaskReceived:
				task.Execute(false, false);
				this.Show(false);
				break;
			case Package.Task.TaskStatus.WaitingToClaimPrize:
				if (TaskDescUI.OpenByNpc > 0 || task.Data.quickComplete == 1)
				{
					bool isUseDiamond = go.Equals(this.mBtnGetMultiple.get_gameObject());
					if (GodWeaponProgressManager.Instance.NeedPlayCommitEffect(task, new Action<BaseTask, bool>(this.CommitTask), isUseDiamond))
					{
						MainTaskManager.Instance.ClickLock = true;
					}
					else
					{
						this.CommitTask(task, isUseDiamond);
					}
				}
				else
				{
					task.Execute(false, false);
					this.Show(false);
				}
				break;
			}
		}
	}

	private void CommitTask(BaseTask cell, bool isUseDiamond)
	{
		cell.Commit(isUseDiamond);
		this.Show(false);
	}

	private void ClearReward()
	{
		this.mRewards.Clear();
		for (int i = 0; i < this.mItems.get_Count(); i++)
		{
			this.mItems.get_Item(i).set_name("Unused");
			this.mItems.get_Item(i).SetActive(false);
		}
	}

	private GameObject GetUnusedItem()
	{
		for (int i = 0; i < this.mItems.get_Count(); i++)
		{
			if (this.mItems.get_Item(i).get_name() == "Unused")
			{
				return this.mItems.get_Item(i);
			}
		}
		return null;
	}

	private GameObject CreateRewards(int id, long num)
	{
		GameObject go = this.GetUnusedItem();
		if (go == null)
		{
			go = ResourceManager.GetInstantiate2Prefab("TaskRewardItem");
			go.GetComponent<Button>().get_onClick().AddListener(delegate
			{
				int num2 = int.Parse(go.get_name());
				if (num2 != 1)
				{
					ItemTipUIViewModel.ShowItem(num2, null);
				}
			});
			UGUITools.SetParent(this.mGrid, go, false);
			this.mItems.Add(go);
		}
		go.set_name(id.ToString());
		ResourceManager.SetSprite(go.GetComponent<Image>(), GameDataUtils.GetItemFrame(id));
		ResourceManager.SetSprite(go.get_transform().FindChild("Image").GetComponent<Image>(), GameDataUtils.GetItemIcon(id));
		string text = num.ToString();
		if (id == 1)
		{
			text = AttrUtility.GetExpValueStr(num);
		}
		else if (id == 2)
		{
			text = AttrUtility.GetGoldValueStr(num);
		}
		go.get_transform().FindChild("Text").GetComponent<Text>().set_text(text);
		Items items = DataReader<Items>.Get(id);
		if (items == null || items.step <= 0)
		{
			go.get_transform().FindChild("ItemStep").get_gameObject().SetActive(false);
		}
		else
		{
			go.get_transform().FindChild("ItemStep").get_gameObject().SetActive(true);
			go.get_transform().FindChild("ItemStep").FindChild("ItemStepText").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
		go.SetActive(true);
		return go;
	}

	private void DelayClickGet()
	{
		this.mDelayTime = MainTaskManager.Instance.AutoTaskTime / 1000;
		if (this.mDelayTime < 1 || MainTaskManager.Instance.IsAutoFast)
		{
			this.mTxAutoTaskTime.set_text(string.Empty);
			this.mDelayId = TimerHeap.AddTimer(600u, 0, new Action(this.ClickGetBtn));
		}
		else
		{
			this.mTxAutoTaskTime.set_text(string.Format(GameDataUtils.GetChineseContent(310028, false), this.mDelayTime));
			this.mDelayId = TimerHeap.AddTimer(1000u, 1000, new Action(this.TickTock));
		}
	}

	private void TickTock()
	{
		this.mDelayTime--;
		this.mTxAutoTaskTime.set_text(string.Format(GameDataUtils.GetChineseContent(310028, false), this.mDelayTime));
		if (this.mDelayTime <= 0)
		{
			this.ClickGetBtn();
		}
	}

	private void ClickGetBtn()
	{
		if (this.mDelayId > 0u)
		{
			this.OnClickGet(this.mBtnGet.get_gameObject());
			TimerHeap.DelTimer(this.mDelayId);
		}
	}

	private void SetGuideTips(BaseTask cell, int type)
	{
		if (MainTaskManager.Instance.IsShowGuildTaskGuide && cell.Task.taskType == Package.Task.TaskType.GuildTask)
		{
			this.ShowBackgroundEffect();
			this.ShowArrowEffect();
			return;
		}
		if (this.mGuideId != cell.Task.taskId)
		{
			this.mGuideId = cell.Task.taskId;
			this.mGuideList.Clear();
			List<RenWuYinDaoPeiZhi> dataList = DataReader<RenWuYinDaoPeiZhi>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				if (dataList.get_Item(i).taskId == this.mGuideId)
				{
					this.mGuideList.Add(dataList.get_Item(i));
				}
			}
		}
		if (this.mGuideList.get_Count() > 0)
		{
			RenWuYinDaoPeiZhi renWuYinDaoPeiZhi = null;
			if (type != 4)
			{
				if (type == 5)
				{
					if (cell.Data.quickAchieve != 1)
					{
						renWuYinDaoPeiZhi = this.mGuideList.Find((RenWuYinDaoPeiZhi e) => e.taskEffects == 5);
					}
				}
			}
			else if (cell.Data.quickReceive != 1)
			{
				renWuYinDaoPeiZhi = this.mGuideList.Find((RenWuYinDaoPeiZhi e) => e.taskEffects == 4);
			}
			if (renWuYinDaoPeiZhi != null)
			{
				this.ShowBackgroundEffect();
				int guide = renWuYinDaoPeiZhi.guide;
				if (guide != 1)
				{
					if (guide == 2)
					{
						this.ShowArrowEffect();
					}
				}
				else
				{
					this.ShowBubbleTipsEffect(GameDataUtils.GetChineseContent(renWuYinDaoPeiZhi.explain, false));
				}
			}
		}
	}

	private void ShowBackgroundEffect()
	{
		this.mBgSpineId1 = FXSpineManager.Instance.ReplaySpine(this.mBgSpineId1, 4305, this.mEffectPanel.get_transform(), "TaskDescUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.mBgSpineId2 = FXSpineManager.Instance.ReplaySpine(this.mBgSpineId2, 4306, this.mEffectPanel.get_transform(), "TaskDescUI", 3000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.mEffectPanel.SetActive(true);
	}

	private void ShowBubbleTipsEffect(string tipscontent)
	{
		Text component = base.FindTransform("Text").GetComponent<Text>();
		component.set_text(tipscontent);
		this.mImgBubbleTips.get_rectTransform().set_sizeDelta(new Vector2(component.get_preferredWidth() + 40f, this.mImgBubbleTips.get_rectTransform().get_sizeDelta().y));
		this.mImgBubbleTips.get_gameObject().SetActive(true);
	}

	private void ShowArrowEffect()
	{
		this.mArrowSpineId1 = FXSpineManager.Instance.ReplaySpine(this.mArrowSpineId1, 4302, this.mArrowPanel.get_transform(), "TaskDescUI", 3001, null, "UI", 120f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.mArrowSpineId2 = FXSpineManager.Instance.ReplaySpine(this.mArrowSpineId2, 4304, this.mArrowPanel.get_transform(), "TaskDescUI", 3000, null, "UI", 120f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.mArrowPanel.SetActive(true);
	}

	private void HideGuideTips()
	{
		this.mImgBubbleTips.get_gameObject().SetActive(false);
		this.mEffectPanel.SetActive(false);
		this.mArrowPanel.SetActive(false);
	}
}
