using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MainTaskItem : MonoBehaviour
{
	public const string UNUSE = "Unuse";

	public const string CLICK_DETAIL = "ClickDetail";

	public const string CLICK_DESC = "ClickDesc";

	public const string CLICK_SHOE = "ClickShoe";

	public const string CLICK_TIPS = "ClickTips";

	private BaseTask mTask;

	public Action<string, MainTaskItem> EventHandler;

	private RectTransform mBackground;

	private ButtonCustom mBtnFlyShoe;

	private Image mImgFlyShoe;

	private Text mTxTitle;

	private Text mTxLine;

	private Text mTxDesc;

	private ButtonCustom mBtnTips;

	private List<RenWuYinDaoPeiZhi> mGuideList;

	private int mGuideId;

	private Image mImgBubbleTips;

	private GameObject mArrowPanel;

	private GameObject mEffectPanel;

	private bool mIsTop;

	private bool mHasGuide;

	private int mBgSpineId1;

	private int mBgSpineId2;

	private int mArrowSpineId1;

	private int mArrowSpineId2;

	private bool mIsInitUI;

	public BaseTask Task
	{
		get
		{
			return this.mTask;
		}
	}

	private int FlyShoeShowType
	{
		get
		{
			if (this.Task.Task.status == Package.Task.TaskStatus.TaskNotOpen)
			{
				return 0;
			}
			if (this.Task.Task.status == Package.Task.TaskStatus.TaskCanAccept && this.Task.Data.quickReceive == 1)
			{
				return 0;
			}
			if (this.Task.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize && (this.Task.Data.quickComplete == 1 || this.Task.Data.quickAchieve == 1))
			{
				return 0;
			}
			bool flag = false;
			switch (this.Task.Data.expediteForward)
			{
			case 0:
				return 0;
			case 1:
				flag = (this.Task.Task.status == Package.Task.TaskStatus.TaskCanAccept || this.Task.Task.status == Package.Task.TaskStatus.TaskReceived || this.Task.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize);
				break;
			case 2:
				flag = (this.Task.Task.status == Package.Task.TaskStatus.TaskCanAccept || this.Task.Task.status == Package.Task.TaskStatus.TaskReceived);
				break;
			case 3:
				flag = (this.Task.Task.status == Package.Task.TaskStatus.TaskCanAccept || this.Task.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize);
				break;
			}
			if (flag)
			{
				return 1;
			}
			return 2;
		}
	}

	private void Awake()
	{
		this.InitUI();
		this.mBackground.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDesc);
		this.mBtnFlyShoe.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFlyShoe);
		this.mBtnTips.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickUpgradeTips);
		this.AddListeners();
	}

	private void InitUI()
	{
		if (this.mIsInitUI)
		{
			return;
		}
		this.mGuideList = new List<RenWuYinDaoPeiZhi>();
		this.mBackground = UIHelper.GetRect(base.get_transform(), "Background");
		this.mBtnFlyShoe = UIHelper.GetCustomButton(this.mBackground.get_transform(), "btnFlyShoe");
		this.mImgFlyShoe = UIHelper.GetImage(this.mBtnFlyShoe.get_transform(), "Image");
		this.mTxTitle = UIHelper.GetText(this.mBackground.get_transform(), "txTitle");
		this.mTxLine = UIHelper.GetText(this.mTxTitle.get_transform(), "txLine");
		this.mTxDesc = UIHelper.GetText(this.mBackground.get_transform(), "txDesc");
		this.mBtnTips = UIHelper.GetCustomButton(this.mBackground.get_transform(), "btnTips");
		this.mImgBubbleTips = UIHelper.GetImage(this.mBackground.get_transform(), "BubbleTips");
		this.mArrowPanel = UIHelper.GetObject(this.mBackground.get_transform(), "ArrowPanel");
		this.mEffectPanel = UIHelper.GetObject(this.mBackground.get_transform(), "EffectPanel");
		this.mIsInitUI = true;
	}

	private void OnDestroy()
	{
		this.RemoveListeners();
	}

	private void OnEnable()
	{
		if (this.mHasGuide)
		{
			if (this.mEffectPanel.get_activeSelf())
			{
				this.PlayBackgroundSpine();
			}
			if (this.mArrowPanel.get_activeSelf())
			{
				this.PlayArrowSpine();
			}
		}
	}

	protected void AddListeners()
	{
		EventDispatcher.AddListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.RefreshUI));
	}

	protected void RemoveListeners()
	{
		EventDispatcher.RemoveListener<Package.Task>(EventNames.UpdateTaskData, new Callback<Package.Task>(this.RefreshUI));
	}

	private void OnClickDetail(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke("ClickDetail", this);
		}
	}

	private void OnClickDesc(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke("ClickDesc", this);
		}
		this.HideGuide();
	}

	private void OnClickFlyShoe(GameObject go)
	{
		if (this.FlyShoeShowType == 1)
		{
			this.EventHandler.Invoke("ClickShoe", this);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310042, false));
		}
	}

	private void OnClickUpgradeTips(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke("ClickTips", this);
		}
	}

	private void RefreshUI(Package.Task task)
	{
		if (task != null && this.mTask != null && this.mTask.Task != null && this.mTask.Task.taskId == task.taskId)
		{
			this.mTask.SetTask(task, false);
			this.SetData(this.mTask);
			if (this.mIsTop)
			{
				this.SetGuide();
			}
			else
			{
				this.HideGuide();
			}
		}
	}

	private void SetTitle()
	{
		this.mTxTitle.set_text(MainTaskItem.GetTaskTitle(this.mTask));
		this.mTxLine.set_text(MainTaskItem.GetUnderline(this.mTxTitle, 0));
	}

	private void SetTarget()
	{
		string text = MainTaskItem.GetTaskTarget(this.mTask, true, string.Empty);
		if (this.mTask.Task.taskType == Package.Task.TaskType.ChangeCareer)
		{
			text = GameDataUtils.GetChineseContent(310026, false);
		}
		this.mTxDesc.set_text(text);
	}

	private void SetButton()
	{
		this.mBtnFlyShoe.get_gameObject().SetActive(this.FlyShoeShowType != 0);
		this.mImgFlyShoe.set_color((this.FlyShoeShowType != 2) ? Color.get_white() : Color.get_red());
		this.mBtnTips.get_gameObject().SetActive(MainTaskManager.Instance.TaskMap.get_Count() == 1 && MainTaskManager.Instance.HasUpgradeTips(this.mTask.Task));
	}

	private void SetGuide()
	{
		if (MainTaskManager.Instance.IsShowGuildTaskGuide && this.mTask.Task.taskType == Package.Task.TaskType.GuildTask)
		{
			this.ShowBackgroundEffect();
			this.ShowArrowEffect();
			return;
		}
		if (this.mGuideId != this.mTask.Task.taskId)
		{
			this.mGuideId = this.mTask.Task.taskId;
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
		this.HideGuide();
		if (this.mGuideList.get_Count() > 0)
		{
			RenWuYinDaoPeiZhi renWuYinDaoPeiZhi = null;
			switch (this.mTask.Task.status)
			{
			case Package.Task.TaskStatus.TaskCanAccept:
				if (this.Task.Data.quickReceive != 1)
				{
					renWuYinDaoPeiZhi = this.mGuideList.Find((RenWuYinDaoPeiZhi e) => e.taskEffects == 1);
				}
				break;
			case Package.Task.TaskStatus.TaskReceived:
				renWuYinDaoPeiZhi = this.mGuideList.Find((RenWuYinDaoPeiZhi e) => e.taskEffects == 2);
				break;
			case Package.Task.TaskStatus.WaitingToClaimPrize:
				if (this.Task.Data.quickComplete != 1 && this.Task.Data.quickAchieve != 1)
				{
					renWuYinDaoPeiZhi = this.mGuideList.Find((RenWuYinDaoPeiZhi e) => e.taskEffects == 3);
				}
				break;
			}
			if (renWuYinDaoPeiZhi != null)
			{
				switch (renWuYinDaoPeiZhi.taskEffects)
				{
				case 1:
				case 2:
				case 3:
					this.ShowBackgroundEffect();
					break;
				}
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
		this.mHasGuide = true;
		this.mEffectPanel.SetActive(true);
		this.PlayBackgroundSpine();
	}

	private void ShowBubbleTipsEffect(string tipscontent)
	{
		this.mHasGuide = true;
		Text text = UIHelper.GetText(this.mImgBubbleTips, "Text");
		text.set_text(tipscontent);
		this.mImgBubbleTips.get_rectTransform().set_sizeDelta(new Vector2(text.get_preferredWidth() + 40f, this.mImgBubbleTips.get_rectTransform().get_sizeDelta().y));
		this.mImgBubbleTips.get_gameObject().SetActive(true);
	}

	private void ShowArrowEffect()
	{
		this.mHasGuide = true;
		this.mArrowPanel.SetActive(true);
		this.PlayArrowSpine();
	}

	private void PlayBackgroundSpine()
	{
		this.mBgSpineId1 = FXSpineManager.Instance.ReplaySpine(this.mBgSpineId1, 4301, this.mEffectPanel.get_transform(), "TownUI", 2002, null, "UI", 0f, -38f, 1f, 1f, true, FXMaskLayer.MaskState.None);
		this.mBgSpineId2 = FXSpineManager.Instance.ReplaySpine(this.mBgSpineId2, 4303, this.mEffectPanel.get_transform(), "TownUI", 2001, null, "UI", 0f, -38f, 1f, 1f, true, FXMaskLayer.MaskState.None);
	}

	private void PlayArrowSpine()
	{
		this.mArrowSpineId1 = FXSpineManager.Instance.ReplaySpine(this.mArrowSpineId1, 4302, this.mArrowPanel.get_transform(), "TownUI", 2002, null, "UI", 50f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
		this.mArrowSpineId2 = FXSpineManager.Instance.ReplaySpine(this.mArrowSpineId2, 4304, this.mArrowPanel.get_transform(), "TownUI", 2001, null, "UI", 50f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
	}

	private void HideGuide()
	{
		this.mHasGuide = false;
		this.mImgBubbleTips.get_gameObject().SetActive(false);
		this.mArrowPanel.SetActive(false);
		this.mEffectPanel.SetActive(false);
	}

	public void SetData(BaseTask task)
	{
		if (task != null)
		{
			this.InitUI();
			this.mTask = task;
			this.SetTitle();
			this.SetTarget();
			this.SetButton();
		}
	}

	public void SetDisable()
	{
		this.SetTop(false);
		this.mTask.isTempTop = true;
		base.get_gameObject().set_name("Unuse");
		base.get_gameObject().SetActive(false);
	}

	public bool SetTop(bool isTop)
	{
		this.mIsTop = isTop;
		if (this.mIsTop)
		{
			this.SetGuide();
		}
		else
		{
			this.HideGuide();
		}
		return this.mHasGuide;
	}

	public static string GetTaskNameById(int id)
	{
		RenWuPeiZhi data = DataReader<RenWuPeiZhi>.Get(id);
		return MainTaskItem.GetTaskNameByData(data);
	}

	public static string GetTaskNameByData(RenWuPeiZhi data)
	{
		if (data != null)
		{
			return GameDataUtils.GetChineseContent(data.dramaIntroduce, false);
		}
		return string.Empty;
	}

	public static string GetTaskTitle(BaseTask task)
	{
		string text = GameDataUtils.GetChineseContent(task.Data.dramaIntroduce, false);
		switch (task.Data.taskType)
		{
		case 1:
			if (!task.hasNextTask && task.Task.status == Package.Task.TaskStatus.TaskFinished)
			{
				text = MainTaskItem.GetTitlePrefix(task.Data.taskType, GameDataUtils.GetChineseContent(310004, false), true, true);
			}
			else
			{
				text = MainTaskItem.GetTitlePrefix(task.Data.taskType, text, true, true);
			}
			return text;
		case 3:
			text = string.Format(MainTaskManager.Instance.STATUS_TXT[6], MainTaskManager.Instance.RingMaxTimes - MainTaskManager.Instance.RingTaskTimes, MainTaskManager.Instance.RingMaxTimes) + text;
			text = MainTaskItem.GetTitlePrefix(task.Data.taskType, text, true, true);
			return text;
		case 4:
		{
			int guildTaskTurnTimes = GuildManager.Instance.GuildTaskTurnTimes;
			int num = guildTaskTurnTimes - MainTaskManager.Instance.GuildTaskTimes + 1;
			num = ((num < guildTaskTurnTimes) ? num : guildTaskTurnTimes);
			text = string.Format(MainTaskManager.Instance.STATUS_TXT[6], num, guildTaskTurnTimes) + text;
			text = MainTaskItem.GetTitlePrefix(task.Data.taskType, text, true, true);
			return text;
		}
		case 5:
			text = string.Format(MainTaskManager.Instance.STATUS_TXT[6], MainTaskManager.Instance.Ring2MaxTimes - MainTaskManager.Instance.RingTask2Times, MainTaskManager.Instance.Ring2MaxTimes) + text;
			text = MainTaskItem.GetTitlePrefix(task.Data.taskType, text, true, true);
			return text;
		case 6:
			if (!task.hasNextTask && task.Task.status == Package.Task.TaskStatus.TaskFinished)
			{
				text = MainTaskItem.GetTitlePrefix(task.Data.taskType, GameDataUtils.GetChineseContent(310007, false), true, true);
			}
			else
			{
				text = MainTaskItem.GetTitlePrefix(task.Data.taskType, text, true, true);
			}
			return text;
		}
		text = MainTaskItem.GetTitlePrefix(task.Data.taskType, text, true, true);
		return text;
	}

	public static string GetUnderline(Text text, int colorType = 0)
	{
		if (text == null)
		{
			return string.Empty;
		}
		int num = Mathf.FloorToInt(text.get_preferredWidth() / (float)text.get_fontSize() * 2f) + 1;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < num; i++)
		{
			stringBuilder.Append("_");
		}
		return (colorType <= 0) ? stringBuilder.ToString() : string.Format(MainTaskManager.Instance.TYPE_COLOR[colorType], stringBuilder);
	}

	public static string GetTaskTarget(BaseTask task, bool hasColor, string color = "")
	{
		string text = GameDataUtils.GetChineseContent(task.Data.targetContent, false);
		switch (task.Task.status)
		{
		case Package.Task.TaskStatus.TaskNotOpen:
			if (EntityWorld.Instance.EntSelf.Lv < task.Data.lv)
			{
				text += string.Format(MainTaskItem.GetStatus(hasColor, color, 0), task.Data.lv);
			}
			else
			{
				text += MainTaskItem.GetStatus(hasColor, color, 1);
			}
			return text;
		case Package.Task.TaskStatus.TaskReceived:
			switch (task.Data.type)
			{
			case 2:
			case 21:
			case 22:
				text += MainTaskItem.GetProgress(task.Data.target, -1, task.Task.count, MainTaskItem.GetStatus(hasColor, color, 6));
				goto IL_23A;
			case 4:
			case 18:
			case 19:
			case 25:
			case 26:
			case 27:
			case 28:
			case 31:
			case 32:
				text += MainTaskItem.GetProgress(task.Data.target, 2, task.Task.count, MainTaskItem.GetStatus(hasColor, color, 6));
				goto IL_23A;
			case 5:
			case 33:
				text += MainTaskItem.GetProgress(task.Data.target, 1, task.Task.count, MainTaskItem.GetStatus(hasColor, color, 6));
				goto IL_23A;
			case 6:
				text += MainTaskItem.GetProgress(task.Data.target, 0, task.Task.count, MainTaskItem.GetStatus(hasColor, color, 6));
				goto IL_23A;
			case 29:
			case 30:
				text += MainTaskItem.GetProgress(task.Data.target, 3, task.Task.count, MainTaskItem.GetStatus(hasColor, color, 6));
				goto IL_23A;
			}
			text += MainTaskItem.GetStatus(hasColor, color, (int)task.Task.status);
			IL_23A:
			return text;
		}
		text += MainTaskItem.GetStatus(hasColor, color, (int)task.Task.status);
		return text;
	}

	private static string GetTitlePrefix(int index, string content, bool hasFlag = true, bool hasColor = true)
	{
		if (hasColor)
		{
			return string.Format(MainTaskManager.Instance.TYPE_COLOR[index], (!hasFlag) ? string.Empty : MainTaskManager.Instance.TYPE_FLAG[index]) + content;
		}
		return ((!hasFlag) ? string.Empty : MainTaskManager.Instance.TYPE_FLAG[index]) + content;
	}

	private static string GetProgress(List<int> targets, int index, int count, string content)
	{
		if (targets != null)
		{
			if (targets.get_Count() == 1)
			{
				return string.Format(content, count, 1);
			}
			if (targets.get_Count() > 1)
			{
				if (index < 0)
				{
					index = targets.get_Count() - 1;
				}
				if (targets.get_Count() > index)
				{
					return string.Format(content, count, targets.get_Item(index));
				}
			}
		}
		Debug.Log("<color=red>Error:</color>任务配置参数target有误!!!");
		return string.Empty;
	}

	private static string GetStatus(bool hasColor, string color, int index = 6)
	{
		if (!hasColor)
		{
			return MainTaskManager.Instance.STATUS_TXT[index];
		}
		if (string.IsNullOrEmpty(color))
		{
			return string.Format(MainTaskManager.Instance.STATUS_COLOR[index], MainTaskManager.Instance.STATUS_TXT[index]);
		}
		return string.Format("<color=#{0}>{1}</color>", color, MainTaskManager.Instance.STATUS_TXT[index]);
	}
}
