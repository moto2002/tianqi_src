using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TaskProgressUI : UIBase
{
	private const int SPEED = 500;

	public static int OpenByTaskId;

	private RectTransform mProgressPanel;

	private Text mTextContent;

	private GameObject mFXPool;

	private GameObject mGodWeaponPanel;

	private Image mGodWeaponIcon;

	private DungeonManager.InsType mType;

	private bool mIsWin;

	private uint mDelayId;

	private float mBgWidth;

	private int mWeaponSpineId;

	private bool mStartMove;

	private Vector2 mTargetPoint = new Vector2(-540f, 83f);

	private float mTargetDist;

	public RectTransform ProgressPanel
	{
		get
		{
			return this.mProgressPanel;
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isEndNav = false;
		this.isInterruptStick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshText(null);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mProgressPanel = base.FindTransform("Progress").GetComponent<RectTransform>();
		this.mTextContent = base.FindTransform("ProgressText").GetComponent<Text>();
		this.mBgWidth = this.mProgressPanel.get_sizeDelta().x;
		this.mGodWeaponPanel = base.FindTransform("GodWeaponPanel").get_gameObject();
		this.mGodWeaponIcon = base.FindTransform("GodWeaponIcon").GetComponent<Image>();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		TaskProgressUI.OpenByTaskId = 0;
		if (this.mDelayId > 0u)
		{
			TimerHeap.DelTimer(this.mDelayId);
			this.mDelayId = 0u;
		}
		this.ResetWeaponEffect();
		this.SetLayout(false);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<Task>(EventNames.UpdateTaskData, new Callback<Task>(this.OnUpdateTaskData));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<Task>(EventNames.UpdateTaskData, new Callback<Task>(this.OnUpdateTaskData));
	}

	public void SetLayout(bool hasBoss)
	{
		if (this.mProgressPanel == null)
		{
			return;
		}
		if (hasBoss)
		{
			this.SetLayout(new Vector2(0f, 280f));
		}
		else
		{
			this.SetLayout(new Vector2(0f, 300f));
		}
	}

	public void SetLayout(Vector2 pos)
	{
		this.mProgressPanel.set_anchoredPosition(pos);
	}

	public void PlayFinishSpine(DungeonManager.InsType type, bool isWin)
	{
		this.mIsWin = isWin;
		this.mType = type;
		this.mFXPool = new GameObject("FXPool");
		this.mFXPool.get_transform().set_parent(base.get_transform());
		this.mFXPool.get_transform().set_localScale(Vector3.get_one());
		this.mFXPool.get_transform().set_localPosition(Vector3.get_zero());
		this.mGodWeaponPanel.SetActive(false);
		this.PlaySpineFX();
	}

	public void PlayWeaponEffect(int weaponModelId)
	{
		this.mGodWeaponPanel.SetActive(true);
		ResourceManager.SetSprite(this.mGodWeaponIcon, GameDataUtils.GetIcon(weaponModelId));
		this.mWeaponSpineId = FXSpineManager.Instance.ReplaySpine(this.mWeaponSpineId, 3907, this.mGodWeaponIcon.get_transform(), "TaskProgressUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			this.mTargetDist = Vector2.Distance(this.mGodWeaponIcon.get_rectTransform().get_anchoredPosition(), this.mTargetPoint);
			this.mStartMove = true;
		});
	}

	protected void OnUpdateTaskData(Task task)
	{
		if (base.get_gameObject().get_activeInHierarchy() && task != null && task.taskId == TaskProgressUI.OpenByTaskId)
		{
			BaseTask baseTask = null;
			if (MainTaskManager.Instance.GetTask(task.taskId, out baseTask, true))
			{
				this.RefreshText(baseTask);
				if (baseTask.Task.status == Task.TaskStatus.WaitingToClaimPrize && (baseTask.Data.type == 18 || baseTask.Data.type == 21 || baseTask.Data.type == 22))
				{
					GuideManager.Instance.TriggerPassOfLogic(999);
				}
			}
		}
	}

	private void RefreshText(BaseTask task = null)
	{
		if (task == null)
		{
			task = MainTaskManager.Instance.GetTask(TaskProgressUI.OpenByTaskId, true);
		}
		if (task != null)
		{
			this.mTextContent.set_text(MainTaskItem.GetTaskTarget(task, true, string.Empty));
			this.mProgressPanel.set_sizeDelta(new Vector2(this.mTextContent.get_preferredWidth() + this.mBgWidth, this.mProgressPanel.get_sizeDelta().y));
			this.mProgressPanel.get_gameObject().SetActive(true);
		}
		else
		{
			this.mProgressPanel.get_gameObject().SetActive(false);
		}
	}

	private void PlaySpineFX()
	{
		if (this.mType == DungeonManager.InsType.FIELD)
		{
			return;
		}
		int effectId;
		if (this.mIsWin)
		{
			effectId = ((this.mType != DungeonManager.InsType.MAIN) ? 1880 : 1800);
		}
		else
		{
			effectId = ((this.mType != DungeonManager.InsType.MAIN) ? 1890 : 1850);
		}
		FXSpineManager.Instance.PlaySpine(effectId + 1, this.mFXPool.get_transform(), "TaskProgressUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(effectId + 2, this.mFXPool.get_transform(), "TaskProgressUI", 2000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.mDelayId = TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this.mDelayId > 0u)
			{
				FXSpineManager.Instance.PlaySpine(effectId + 3, this.get_transform(), string.Empty, 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				Object.Destroy(this.mFXPool);
				TimerHeap.DelTimer(this.mDelayId);
				this.mDelayId = 0u;
			}
		});
	}

	private void Update()
	{
		this.MoveGodWeapon();
	}

	private void MoveGodWeapon()
	{
		if (!this.mStartMove)
		{
			return;
		}
		float num = Vector2.Distance(this.mGodWeaponIcon.get_rectTransform().get_anchoredPosition(), this.mTargetPoint);
		if (num <= 1f)
		{
			this.mStartMove = false;
			return;
		}
		this.mGodWeaponIcon.get_rectTransform().set_anchoredPosition(Vector2.MoveTowards(this.mGodWeaponIcon.get_rectTransform().get_anchoredPosition(), this.mTargetPoint, 500f * Time.get_deltaTime()));
		this.mGodWeaponIcon.get_transform().set_localScale(Vector3.get_one() * Mathf.Lerp(0.32f, 1f, num / this.mTargetDist));
	}

	private void ResetWeaponEffect()
	{
		this.mStartMove = false;
		if (this.mGodWeaponIcon != null)
		{
			this.mGodWeaponIcon.get_rectTransform().set_localPosition(Vector3.get_zero());
			this.mGodWeaponIcon.get_rectTransform().set_localScale(Vector3.get_one());
		}
	}
}
