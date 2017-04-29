using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideUIView : UIBase
{
	private const int DRIFT_SPEED = 850;

	private const float OFFSET_ARROW_X = 7.2f;

	private const float HEIGHT_MORE_TEXT = 30f;

	private const float HEIGHT_MORE_BG = 50f;

	private const int MaxInvalidClick = 20;

	public static GuideUIView Instance;

	private Transform Pause;

	private GameObject m_goButtonSkip;

	private Transform MaskScreen;

	private Image m_spMaskScreen;

	private Transform JumpContinueUI;

	private Image m_spContinueIcon;

	private Image m_spContinueIconName;

	private Image m_spContinueIconBg;

	private Text m_lblContinueTip;

	private Transform ContinueFX;

	private RectTransform JumpDriftUI;

	private RectTransform JumpDriftUIPos;

	private Image m_spJumpDriftUIIcon;

	private Image m_spJumpDriftUIIconBg;

	private RectTransform FingerUI;

	private RectTransform FingerUIPos;

	private RectTransform InstructionBubbleUI;

	private RectTransform InstructionBubbleBg1;

	private RectTransform InstructionBubbleBg2;

	private Text m_lblInstructionBubbleDesc;

	private RectTransform InstructionRoleUI;

	private RectTransform InstructionRoleBg;

	private Transform InstructionRoleIcon;

	private Text m_lblInstructionRoleDesc;

	private RectTransform ImageShowUI;

	private ScrollRectCustom m_src;

	private ListPool m_listPool;

	public static bool IsDownOn = true;

	public static bool IsPopUIPrevious;

	public int mStepType;

	public static bool IsTriggerNextStep;

	private bool IsSystemOpen = true;

	private bool IsContinueLock;

	private int continue_fx11;

	private int continue_fx12;

	private int continue_fx2;

	private int skillopen_continue_fx11;

	private int skillopen_continue_fx12;

	private int skillopen_continue_fx2;

	private uint skill_open_timer_id;

	private SystemOpen m_dataSO;

	private Transform mTargetDriftTransform;

	private Vector2 TargetDriftOffset = Vector2.get_zero();

	private Vector3 mTargetDriftPosition;

	private bool m_IsDrift;

	private readonly Vector2 SYSTEM_OPEN_SRC = new Vector2(156f, 20f);

	private bool IsDriftLock;

	private int drift_fx1;

	private int drift_fx2;

	private Transform m_targetToFinger;

	private bool m_is_finger_move_lock;

	private bool IsShowFingerUI;

	private int finger_fx1;

	private int finger_fx2;

	private int role_fx;

	private int InvalidClickNum;

	private Vector3 TargetDriftPosition
	{
		get
		{
			if (this.mTargetDriftTransform != null)
			{
				this.mTargetDriftPosition = this.mTargetDriftTransform.get_position();
			}
			return this.mTargetDriftPosition;
		}
		set
		{
			this.mTargetDriftPosition = value;
		}
	}

	private bool IsDrift
	{
		get
		{
			return this.m_IsDrift;
		}
		set
		{
			if (this.m_IsDrift && !value)
			{
				this.m_IsDrift = value;
				this.Show(false);
				EventDispatcher.Broadcast("GuideManager.NextStep");
			}
			else
			{
				this.m_IsDrift = value;
			}
		}
	}

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.isInterruptStick = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		GuideUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		DepthManager.SetDepth(base.FindTransform("Layer1").get_gameObject(), 10001);
		DepthManager.SetGraphicRaycaster(base.FindTransform("Layer1").get_gameObject());
		DepthManager.SetDepth(base.FindTransform("Layer2").get_gameObject(), 10003);
		this.Pause = base.FindTransform("Pause");
		this.m_goButtonSkip = base.FindTransform("ButtonSkip").get_gameObject();
		this.MaskScreen = base.FindTransform("MaskScreen");
		this.m_spMaskScreen = base.FindTransform("MaskScreen").GetComponent<Image>();
		this.JumpContinueUI = base.FindTransform("JumpContinueUI");
		this.m_spContinueIcon = base.FindTransform("ContinueIcon").GetComponent<Image>();
		this.m_spContinueIconName = base.FindTransform("ContinueIconName").GetComponent<Image>();
		this.m_spContinueIconBg = base.FindTransform("ContinueIconBg").GetComponent<Image>();
		this.m_lblContinueTip = base.FindTransform("ContinueTip").GetComponent<Text>();
		this.ContinueFX = base.FindTransform("ContinueFX");
		this.JumpDriftUI = (base.FindTransform("JumpDriftUI") as RectTransform);
		this.JumpDriftUIPos = (base.FindTransform("JumpDriftUIPos") as RectTransform);
		this.m_spJumpDriftUIIcon = base.FindTransform("JumpDriftUIIcon").GetComponent<Image>();
		this.m_spJumpDriftUIIconBg = base.FindTransform("JumpDriftUIIconBg").GetComponent<Image>();
		this.FingerUI = (base.FindTransform("FingerUI") as RectTransform);
		this.FingerUIPos = (base.FindTransform("FingerUIPos") as RectTransform);
		this.InstructionBubbleUI = (base.FindTransform("InstructionBubbleUI") as RectTransform);
		this.InstructionBubbleBg1 = (base.FindTransform("InstructionBubbleBg1") as RectTransform);
		this.InstructionBubbleBg2 = (base.FindTransform("InstructionBubbleBg2") as RectTransform);
		this.m_lblInstructionBubbleDesc = base.FindTransform("InstructionBubbleDesc").GetComponent<Text>();
		this.InstructionRoleUI = (base.FindTransform("InstructionRoleUI") as RectTransform);
		this.InstructionRoleBg = (base.FindTransform("InstructionRoleBg") as RectTransform);
		this.InstructionRoleIcon = base.FindTransform("InstructionRoleIcon");
		this.m_lblInstructionRoleDesc = base.FindTransform("InstructionRoleDesc").GetComponent<Text>();
		this.ImageShowUI = (base.FindTransform("ImageShowUI") as RectTransform);
		this.m_src = base.FindTransform("ISUIImageSR").GetComponent<ScrollRectCustom>();
		this.m_src.movePage = true;
		this.m_src.Arrow2First = base.FindTransform("ISUIArrowL");
		this.m_src.Arrow2Last = base.FindTransform("ISUIArrowR");
		this.m_src.OnPageChanged = delegate(int pageIndex)
		{
			PageUIView.Instance.SetPage(this.m_src.GetPageNum(), pageIndex);
		};
		this.m_listPool = base.FindTransform("ISUIImageList").GetComponent<ListPool>();
		this.m_listPool.SetItem("GuideUIImageShowItem");
		this.m_listPool.isAnimation = false;
		this.ResetGuideUI();
		EventTriggerListener.Get(base.FindTransform("ButtonSkip").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnButtonSkip);
		EventTriggerListener.Get(base.FindTransform("MaskScreen").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnMaskScreen);
		EventTriggerListener.Get(base.FindTransform("ButtonContinue").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnClickButtonContinue);
		EventTriggerListener.Get(base.FindTransform("ISUIButtonClose").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnClickButtonClose);
		EventTriggerListener.Get(base.FindTransform("ISUIArrowL").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnClickButtonArrowL);
		EventTriggerListener.Get(base.FindTransform("ISUIArrowR").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnClickButtonArrowR);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.ResetInvalidClick();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ResetGuideUI();
		this.RemoveFingerFX();
		this.m_is_finger_move_lock = false;
		GuideUIView.IsTriggerNextStep = false;
		this.ResetSystemOpen();
		if (GuideUIView.IsPopUIPrevious)
		{
			GuideUIView.IsPopUIPrevious = false;
			UIStackManager.Instance.PopUIPrevious(UIType.FullScreen);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("GuideManager.ResetInvalidClick", new Callback(this.ResetInvalidClick));
		EventDispatcher.AddListener("GuideManager.ResetGuideUI", new Callback(this.ResetGuideUI));
		EventDispatcher.AddListener<bool>("GuideManager.PauseGuideSystem", new Callback<bool>(this.PauseGuideSystem));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener("GuideManager.ResetInvalidClick", new Callback(this.ResetInvalidClick));
		EventDispatcher.RemoveListener("GuideManager.ResetGuideUI", new Callback(this.ResetGuideUI));
		EventDispatcher.RemoveListener<bool>("GuideManager.PauseGuideSystem", new Callback<bool>(this.PauseGuideSystem));
	}

	private void ResetGuideUI()
	{
		if (this == null || this.Pause == null || this.MaskScreen == null)
		{
			return;
		}
		this.mStepType = 0;
		this.Pause.get_gameObject().SetActive(true);
		this.MaskScreen.get_gameObject().SetActive(true);
		this.ShowFingerUI(false);
		this.ShowInstructionRoleUI(false);
		this.ShowInstructionBubbleUI(false);
		this.ShowJumpContinueUI(false);
		this.ShowJumpDriftUI(false);
		this.ShowImageShowUI(false);
	}

	private void PauseGuideSystem(bool isPause)
	{
		this.Pause.get_gameObject().SetActive(!isPause);
	}

	private void OnButtonSkip(GameObject sender)
	{
		GuideManager.Instance.StopGuideOfFinish();
	}

	private void OnMaskScreen(GameObject sender)
	{
		this.OnInvalidClick();
		this.JustOnMaskScreen();
	}

	private void OnClickButtonContinue(GameObject sender)
	{
		if (this.IsContinueLock)
		{
			return;
		}
		this.ShowJumpContinueUI(false);
		if (this.IsDrift)
		{
			this.SetFingerMoveLock(true);
			this.ShowJumpDriftUI(true);
		}
		else
		{
			EventDispatcher.Broadcast("GuideManager.NextStep");
		}
	}

	private void OnClickButtonClose(GameObject sender)
	{
		this.ShowImageShowUI(false);
		EventDispatcher.Broadcast("GuideManager.NextStep");
	}

	private void OnClickButtonArrowL(GameObject sender)
	{
		this.m_src.Move2Previous();
	}

	private void OnClickButtonArrowR(GameObject sender)
	{
		this.m_src.Move2Next();
	}

	private void Update()
	{
		this.UpdateFingerUIIsShow();
		this.UpdateFingerUIPos();
		this.DriftToTheTarget();
		if (InputManager.IsInputDownOn() && GuideUIView.IsDownOn && !GuideManager.Instance.out_system_lock)
		{
			this.JustOnMaskScreen();
		}
	}

	public void ShowButtonSkip(bool isShow, int pos)
	{
		if (this.m_goButtonSkip != null)
		{
			this.m_goButtonSkip.SetActive(isShow);
			if (isShow)
			{
				RectTransform rectTransform = this.m_goButtonSkip.get_transform() as RectTransform;
				if (pos == 0)
				{
					rectTransform.set_anchorMin(Vector2.get_one());
					rectTransform.set_anchorMax(Vector2.get_one());
					rectTransform.set_anchoredPosition(new Vector2(-80f, -40f));
				}
				else if (pos == 1)
				{
					rectTransform.set_anchorMin(new Vector2(1f, 0f));
					rectTransform.set_anchorMax(new Vector2(1f, 0f));
					rectTransform.set_anchoredPosition(new Vector2(-80f, 40f));
				}
			}
		}
	}

	public void LockScreen(bool islock, int successMode, int skipPosition)
	{
		if (successMode == 2)
		{
			GuideUIView.IsTriggerNextStep = true;
		}
		else if (successMode == 5)
		{
			GuideUIView.IsTriggerNextStep = false;
		}
		else
		{
			GuideUIView.IsTriggerNextStep = false;
		}
		if (islock)
		{
			this.MaskScreen.get_gameObject().SetActive(true);
			this.ShowButtonSkip(true, skipPosition);
			this.SetMaskA(0.5f);
		}
		else
		{
			this.MaskScreen.get_gameObject().SetActive(false);
			this.ShowButtonSkip(false, skipPosition);
			this.SetMaskA(0f);
		}
	}

	public void SetSystemOpen(SystemOpen dataSO)
	{
		this.IsSystemOpen = true;
		this.mTargetDriftTransform = null;
		GuideManager.Instance.talk_desc_ui_lock = true;
		UIManagerControl.Instance.HideUI("TaskDescUI");
		if (UIManagerControl.Instance.IsOpen("TownUI"))
		{
			TownUI.Instance.ForceOpenRightBottom();
		}
		GuideUIView.IsTriggerNextStep = false;
		this.SystemOpen_JumpSetting(dataSO);
		this.SystemOpen_SetJumpContinueUI(dataSO);
		this.SystemOpen_SetJumpDriftUI(dataSO);
		this.ShowJumpContinueUI(true);
		this.ShowJumpDriftUI(false);
	}

	private void SystemOpen_JumpSetting(SystemOpen dataSO)
	{
		this.MaskScreen.get_gameObject().SetActive(true);
		this.SetMaskA(0.05f);
		this.m_dataSO = dataSO;
		if (this.m_dataSO != null && this.m_dataSO.area > 0)
		{
			this.IsDrift = true;
		}
	}

	private void SystemOpen_SetJumpContinueUI(SystemOpen dataSO)
	{
		this.m_spContinueIconBg.get_gameObject().SetActive(false);
		this.m_spContinueIcon.get_rectTransform().set_anchoredPosition(this.SYSTEM_OPEN_SRC);
		ResourceManager.SetSprite(this.m_spContinueIcon, GameDataUtils.GetIcon(dataSO.icon));
		this.m_spContinueIcon.SetNativeSize();
		this.m_spContinueIconName.set_enabled(true);
		ResourceManager.SetSprite(this.m_spContinueIconName, GameDataUtils.GetIcon(dataSO.icon2));
		this.m_spContinueIconName.SetNativeSize();
		this.m_lblContinueTip.set_text(string.Format("{0}系统已经开启", GameDataUtils.GetChineseContent(dataSO.name, false)));
	}

	private void SystemOpen_SetJumpDriftUI(SystemOpen dataSO)
	{
		this.m_spJumpDriftUIIconBg.get_gameObject().SetActive(false);
		ResourceManager.SetSprite(this.m_spJumpDriftUIIcon, GameDataUtils.GetIcon(dataSO.icon));
		this.m_spJumpDriftUIIcon.SetNativeSize();
	}

	private void ResetSystemOpen()
	{
		this.IsContinueLock = false;
		this.DeleteJumpSpine();
		TimerHeap.DelTimer(this.skill_open_timer_id);
	}

	public void SetSkillOpen(Transform target)
	{
		this.IsSystemOpen = false;
		this.mTargetDriftTransform = target;
		this.TargetDriftOffset = Vector2.get_zero();
		GuideUIView.IsTriggerNextStep = false;
		this.SkillOpen_JumpSetting();
		Transform transform = target.Find("Icon");
		Image image = null;
		if (transform != null)
		{
			image = transform.GetComponent<Image>();
		}
		this.SkillOpen_SetJumpContinueUI(image);
		this.SkillOpen_SetJumpDriftUI(image);
		this.ShowJumpContinueUI(true);
		this.ShowJumpDriftUI(false);
	}

	private void SkillOpen_JumpSetting()
	{
		this.MaskScreen.get_gameObject().SetActive(true);
		this.SetMaskA(0.05f);
		this.IsDrift = true;
	}

	private void SkillOpen_SetJumpContinueUI(Image image)
	{
		this.m_spContinueIconBg.get_rectTransform().set_anchoredPosition(Vector2.get_zero());
		ResourceManager.SetSprite(this.m_spContinueIconBg, ResourceManager.GetIconSprite("new_zd_jinengkuang"));
		this.m_spContinueIconBg.get_rectTransform().set_sizeDelta(new Vector2(95f, 95f));
		this.m_spContinueIconName.set_enabled(false);
		this.m_spContinueIcon.get_rectTransform().set_anchoredPosition(Vector2.get_zero());
		ResourceManager.SetSprite(this.m_spContinueIcon, image);
		this.m_spContinueIcon.get_rectTransform().set_sizeDelta(new Vector2(79f, 79f));
		this.m_lblContinueTip.set_text(string.Empty);
	}

	private void SkillOpen_SetJumpDriftUI(Image image)
	{
		ResourceManager.SetSprite(this.m_spJumpDriftUIIconBg, ResourceManager.GetIconSprite("new_zd_jinengkuang"));
		this.m_spJumpDriftUIIconBg.get_rectTransform().set_sizeDelta(new Vector2(95f, 95f));
		ResourceManager.SetSprite(this.m_spJumpDriftUIIcon, image);
		this.m_spJumpDriftUIIcon.get_rectTransform().set_sizeDelta(new Vector2(79f, 79f));
	}

	private void ShowJumpContinueUI(bool isShow)
	{
		if (this.JumpContinueUI == null)
		{
			return;
		}
		this.JumpContinueUI.get_gameObject().SetActive(isShow);
		if (isShow)
		{
			this.StartJumpSpine();
		}
		else
		{
			this.SystemOpen_RemoveContinue01();
			this.SkillOpen_RemoveContinue01();
		}
	}

	private void StartJumpSpine()
	{
		if (this.IsSystemOpen)
		{
			this.SystemOpen_AddContinue01();
		}
		else
		{
			this.SkillOpen_AddContinue01();
		}
	}

	private void DeleteJumpSpine()
	{
		this.SystemOpen_RemoveContinue01();
		this.SystemOpen_RemoveContinue02();
		this.RemoveDrift01();
		this.RemoveDrift02();
		this.SkillOpen_RemoveContinue01();
		this.SkillOpen_RemoveContinue02();
	}

	private void SystemOpen_AddContinue01()
	{
		this.IsContinueLock = true;
		this.continue_fx11 = FXSpineManager.Instance.ReplaySpine(this.continue_fx11, 3303, this.ContinueFX, string.Empty, 10004, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.continue_fx12 = FXSpineManager.Instance.ReplaySpine(this.continue_fx2, 3304, this.ContinueFX.get_transform(), string.Empty, 10006, delegate
		{
			this.SystemOpen_AddContinue02();
			this.IsContinueLock = false;
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void SystemOpen_RemoveContinue01()
	{
		FXSpineManager.Instance.DeleteSpine(this.continue_fx11, true);
		FXSpineManager.Instance.DeleteSpine(this.continue_fx12, true);
	}

	private void SystemOpen_AddContinue02()
	{
		this.continue_fx2 = FXSpineManager.Instance.ReplaySpine(this.continue_fx2, 3301, this.m_spContinueIcon.get_transform(), string.Empty, 10006, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void SystemOpen_RemoveContinue02()
	{
		FXSpineManager.Instance.DeleteSpine(this.continue_fx2, true);
	}

	private void SkillOpen_AddContinue01()
	{
		this.IsContinueLock = true;
		this.skillopen_continue_fx11 = FXSpineManager.Instance.ReplaySpine(this.skillopen_continue_fx11, 3907, this.ContinueFX, string.Empty, 10004, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.skillopen_continue_fx12 = FXSpineManager.Instance.ReplaySpine(this.skillopen_continue_fx12, 4006, this.ContinueFX.get_transform(), string.Empty, 10006, delegate
		{
			this.SkillOpen_AddContinue02();
			this.IsContinueLock = false;
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void SkillOpen_RemoveContinue01()
	{
		FXSpineManager.Instance.DeleteSpine(this.skillopen_continue_fx11, true);
		FXSpineManager.Instance.DeleteSpine(this.skillopen_continue_fx12, true);
	}

	private void SkillOpen_AddContinue02()
	{
		this.skillopen_continue_fx2 = FXSpineManager.Instance.ReplaySpine(this.skillopen_continue_fx2, 4003, this.m_spContinueIcon.get_transform(), string.Empty, 10006, delegate
		{
			this.skill_open_timer_id = TimerHeap.AddTimer(2000u, 0, delegate
			{
				this.IsContinueLock = false;
				this.OnClickButtonContinue(null);
			});
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void SkillOpen_RemoveContinue02()
	{
		FXSpineManager.Instance.DeleteSpine(this.skillopen_continue_fx2, true);
	}

	private void ShowJumpDriftUI(bool isShow)
	{
		if (this.JumpDriftUI == null)
		{
			return;
		}
		if (isShow)
		{
			this.SetDriftTarget();
			this.JumpDriftUI.get_gameObject().SetActive(true);
			this.AddDrift01();
		}
		else
		{
			this.JumpDriftUI.get_gameObject().SetActive(false);
			if (this.IsSystemOpen)
			{
				this.JumpDriftUI.set_anchoredPosition(this.SYSTEM_OPEN_SRC);
			}
			else
			{
				this.JumpDriftUI.set_anchoredPosition(Vector2.get_zero());
			}
			this.RemoveDrift02();
		}
	}

	private void SetDriftTarget()
	{
		if (this.IsSystemOpen)
		{
			this.SetDriftTarget2SystemOpen();
		}
		else
		{
			this.SetDriftTarget2SkillOpen();
		}
	}

	private void SetDriftTarget2SystemOpen()
	{
		if (this.m_dataSO == null || !SystemOpenManager.GetTargetPosition(this.m_dataSO.area, this.m_dataSO.widgetId, ref this.mTargetDriftPosition, ref this.TargetDriftOffset))
		{
			this.ShowJumpDriftUI(false);
			this.IsDrift = false;
			return;
		}
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("TownUI");
		if (uIIfExist == null)
		{
			Debug.LogError("TownUI is null");
			return;
		}
		List<Vector2> offsets;
		(uIIfExist as TownUI).MoveAreaButtons(this.m_dataSO.area, SystemOpenManager.GetBehinds(this.m_dataSO.area, this.m_dataSO.areaIndex, out offsets), offsets);
		this.JumpDriftUIPos.set_position(this.TargetDriftPosition);
		this.JumpDriftUIPos.set_localPosition(new Vector3(this.JumpDriftUIPos.get_localPosition().x, this.JumpDriftUIPos.get_localPosition().y, 0f));
		RectTransform expr_F3 = this.JumpDriftUIPos;
		expr_F3.set_anchoredPosition(expr_F3.get_anchoredPosition() + this.TargetDriftOffset);
	}

	private void SetDriftTarget2SkillOpen()
	{
		this.JumpDriftUIPos.set_position(this.TargetDriftPosition);
		this.JumpDriftUIPos.set_localPosition(new Vector3(this.JumpDriftUIPos.get_localPosition().x, this.JumpDriftUIPos.get_localPosition().y, 0f));
		RectTransform expr_52 = this.JumpDriftUIPos;
		expr_52.set_anchoredPosition(expr_52.get_anchoredPosition() + this.TargetDriftOffset);
	}

	private void DriftToTheTarget()
	{
		if (this.JumpDriftUI == null || !this.JumpDriftUI.get_gameObject().get_activeSelf())
		{
			return;
		}
		if (this.IsDriftLock)
		{
			return;
		}
		if (Vector2.Distance(this.JumpDriftUI.get_anchoredPosition(), this.JumpDriftUIPos.get_anchoredPosition()) <= 1f)
		{
			this.DriftFinished();
			return;
		}
		this.JumpDriftUI.set_anchoredPosition(Vector2.MoveTowards(this.JumpDriftUI.get_anchoredPosition(), this.JumpDriftUIPos.get_anchoredPosition(), 850f * Time.get_deltaTime()));
	}

	private void DriftFinished()
	{
		if (this.IsSystemOpen)
		{
			UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("TownUI");
			if (uIIfExist == null)
			{
				Debug.LogError("TownUI is null");
			}
			else
			{
				(uIIfExist as TownUI).ControlSystemOpens(false, this.m_dataSO.id);
			}
			this.RemoveDrift01();
			this.AddDrift02(delegate
			{
				this.SetFingerMoveLock(false);
				this.IsDrift = false;
				this.ShowJumpDriftUI(false);
			});
		}
		else
		{
			this.RemoveDrift01();
			this.AddDrift02(delegate
			{
				this.SetFingerMoveLock(false);
				this.IsDrift = false;
				this.ShowJumpDriftUI(false);
			});
		}
	}

	private void AddDrift01()
	{
		this.IsDriftLock = true;
		TimerHeap.AddTimer(50u, 0, delegate
		{
			this.IsDriftLock = false;
		});
		this.drift_fx1 = FXSpineManager.Instance.ReplaySpine(this.drift_fx1, 3301, this.m_spJumpDriftUIIcon.get_transform(), string.Empty, 10004, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveDrift01()
	{
		FXSpineManager.Instance.DeleteSpine(this.drift_fx1, true);
	}

	private void AddDrift02(Action finishCallback)
	{
		if (this.IsSystemOpen)
		{
			this.drift_fx2 = FXSpineManager.Instance.ReplaySpine(this.drift_fx2, 3305, this.m_spJumpDriftUIIcon.get_transform(), string.Empty, 10004, finishCallback, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			this.drift_fx2 = FXSpineManager.Instance.ReplaySpine(this.drift_fx2, 4004, this.m_spJumpDriftUIIcon.get_transform(), string.Empty, 10004, finishCallback, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void RemoveDrift02()
	{
		FXSpineManager.Instance.DeleteSpine(this.drift_fx2, true);
	}

	public void SetFingerUI(Transform targetTransform)
	{
		this.FingerUI.set_anchoredPosition(Vector2.get_zero());
		this.SetFingerMoveLock(true);
		this.m_targetToFinger = targetTransform;
		this.ShowFingerUI(true);
	}

	private void ShowFingerUI(bool isShow)
	{
		this.IsShowFingerUI = isShow;
		if (isShow)
		{
			this.AddFingerFX_Shouzhi();
		}
		else
		{
			this.RemoveFingerFX();
		}
	}

	private void SetFingerMoveLock(bool islock)
	{
		GuideManager.Instance.finger_move_lock = islock;
		this.m_is_finger_move_lock = islock;
	}

	private void UpdateFingerUIIsShow()
	{
		if (this.m_targetToFinger != null && !this.m_targetToFinger.get_gameObject().get_activeInHierarchy())
		{
			if (this.mStepType == 2)
			{
				this.ShowInstructionRoleUI(false);
			}
			if (this.mStepType == 7)
			{
				this.ShowInstructionBubbleUI(false);
			}
			this.FingerUI.get_gameObject().SetActive(false);
			this.SetFingerMoveLock(false);
		}
		else
		{
			if (this.mStepType == 2)
			{
				this.ShowInstructionRoleUI(true);
			}
			if (this.mStepType == 7)
			{
				this.ShowInstructionBubbleUI(true);
			}
			this.FingerUI.get_gameObject().SetActive(this.IsShowFingerUI);
			GuideManager.Instance.finger_move_lock = this.m_is_finger_move_lock;
		}
	}

	private void UpdateFingerUIPos()
	{
		if (this.FingerUI == null || !this.FingerUI.get_gameObject().get_activeSelf() || this.m_targetToFinger == null)
		{
			return;
		}
		this.FingerUIPos.set_position(this.m_targetToFinger.get_position());
		RectTransform rectTransform = this.m_targetToFinger as RectTransform;
		if (rectTransform != null)
		{
			rectTransform.set_localPosition(new Vector3(rectTransform.get_localPosition().x, rectTransform.get_localPosition().y, 0f));
			if (rectTransform.get_pivot().x == 0f)
			{
				this.FingerUIPos.set_anchoredPosition(new Vector2(this.FingerUIPos.get_anchoredPosition().x + rectTransform.get_sizeDelta().x / 2f, this.FingerUIPos.get_anchoredPosition().y));
			}
			else if (rectTransform.get_pivot().x == 1f)
			{
				this.FingerUIPos.set_anchoredPosition(new Vector2(this.FingerUIPos.get_anchoredPosition().x - rectTransform.get_sizeDelta().x / 2f, this.FingerUIPos.get_anchoredPosition().y));
			}
			if (rectTransform.get_pivot().y == 0f)
			{
				this.FingerUIPos.set_anchoredPosition(new Vector2(this.FingerUIPos.get_anchoredPosition().x, this.FingerUIPos.get_anchoredPosition().y + rectTransform.get_sizeDelta().y / 2f));
			}
			else if (rectTransform.get_pivot().y == 1f)
			{
				this.FingerUIPos.set_anchoredPosition(new Vector2(this.FingerUIPos.get_anchoredPosition().x, this.FingerUIPos.get_anchoredPosition().y - rectTransform.get_sizeDelta().y / 2f));
			}
		}
		if (Vector2.Distance(this.FingerUI.get_anchoredPosition(), this.FingerUIPos.get_anchoredPosition()) <= 1f)
		{
			this.SetFingerMoveLock(false);
			this.AddFingerFX_Quan();
			this.AddFingerFX_Shouzhi();
			return;
		}
		if (EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle)
		{
			Time.set_timeScale(1f);
		}
		this.FingerUI.set_anchoredPosition(Vector2.MoveTowards(this.FingerUI.get_anchoredPosition(), this.FingerUIPos.get_anchoredPosition(), 680f * Time.get_deltaTime() * 1.5f));
	}

	private void AddFingerFX_Quan()
	{
		this.finger_fx1 = FXSpineManager.Instance.ReplaySpine(this.finger_fx1, 3201, this.FingerUI, string.Empty, 10004, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void AddFingerFX_Shouzhi()
	{
		this.finger_fx2 = FXSpineManager.Instance.ReplaySpine(this.finger_fx2, 3202, this.FingerUI, string.Empty, 10005, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveFingerFX()
	{
		Debug.Log("==>指引RemoveFingerFX");
		FXSpineManager.Instance.DeleteSpine(this.finger_fx1, true);
		FXSpineManager.Instance.DeleteSpine(this.finger_fx2, true);
		this.finger_fx1 = 0;
		this.finger_fx2 = 0;
	}

	public void SetInstructionBubbleUI(Vector2 localPos, string instruction, int direction)
	{
		this.InstructionBubbleUI.set_anchoredPosition(localPos);
		this.SetInstructionBubbleUI(instruction);
		switch (direction)
		{
		case 1:
			this.InstructionBubbleBg2.set_localRotation(Quaternion.Euler(0f, 180f, 270f));
			this.InstructionBubbleBg2.set_anchorMin(new Vector2(0.5f, 1f));
			this.InstructionBubbleBg2.set_anchorMax(new Vector2(0.5f, 1f));
			this.InstructionBubbleBg2.set_pivot(new Vector2(1f, 0.5f));
			this.InstructionBubbleBg2.set_anchoredPosition(new Vector2(0f, -7.2f));
			break;
		case 2:
			this.InstructionBubbleBg2.set_localRotation(Quaternion.Euler(0f, 0f, 90f));
			this.InstructionBubbleBg2.set_anchorMin(new Vector2(0.5f, 0f));
			this.InstructionBubbleBg2.set_anchorMax(new Vector2(0.5f, 0f));
			this.InstructionBubbleBg2.set_pivot(new Vector2(1f, 0.5f));
			this.InstructionBubbleBg2.set_anchoredPosition(new Vector2(0f, 7.2f));
			break;
		case 3:
			this.InstructionBubbleBg2.set_localRotation(Quaternion.Euler(0f, 0f, 0f));
			this.InstructionBubbleBg2.set_anchorMin(new Vector2(0f, 0.5f));
			this.InstructionBubbleBg2.set_anchorMax(new Vector2(0f, 0.5f));
			this.InstructionBubbleBg2.set_pivot(new Vector2(1f, 0.5f));
			this.InstructionBubbleBg2.set_anchoredPosition(new Vector2(7.2f, 0f));
			break;
		case 4:
			this.InstructionBubbleBg2.set_localRotation(Quaternion.Euler(0f, 180f, 0f));
			this.InstructionBubbleBg2.set_anchorMin(new Vector2(1f, 0.5f));
			this.InstructionBubbleBg2.set_anchorMax(new Vector2(1f, 0.5f));
			this.InstructionBubbleBg2.set_pivot(new Vector2(1f, 0.5f));
			this.InstructionBubbleBg2.set_anchoredPosition(new Vector2(-7.2f, 0f));
			break;
		}
	}

	private void SetInstructionBubbleUI(string instruction)
	{
		this.ShowInstructionBubbleUI(true);
		this.m_lblInstructionBubbleDesc.set_text(instruction);
		this.m_lblInstructionBubbleDesc.get_rectTransform().set_sizeDelta(new Vector2(this.m_lblInstructionBubbleDesc.get_rectTransform().get_sizeDelta().x, this.m_lblInstructionBubbleDesc.get_preferredHeight() + 30f));
		this.InstructionBubbleBg1.set_sizeDelta(new Vector2(this.InstructionBubbleBg1.get_sizeDelta().x, this.m_lblInstructionBubbleDesc.get_preferredHeight() + 50f));
	}

	private void ShowInstructionBubbleUI(bool isShow)
	{
		if (this.InstructionBubbleUI == null)
		{
			return;
		}
		this.InstructionBubbleUI.get_gameObject().SetActive(isShow);
	}

	public void SetInstructionRoleUI(Vector2 localPos, string instruction)
	{
		this.InstructionRoleUI.set_anchoredPosition(localPos);
		this.SetInstructionRoleUI(instruction);
	}

	private void SetInstructionRoleUI(string instruction)
	{
		this.ShowInstructionRoleUI(true);
		this.m_lblInstructionRoleDesc.set_text(instruction);
		this.m_lblInstructionRoleDesc.get_rectTransform().set_sizeDelta(new Vector2(this.m_lblInstructionRoleDesc.get_rectTransform().get_sizeDelta().x, this.m_lblInstructionRoleDesc.get_preferredHeight() + 30f));
		this.InstructionRoleBg.set_sizeDelta(new Vector2(this.InstructionRoleBg.get_sizeDelta().x, this.m_lblInstructionRoleDesc.get_preferredHeight() + 50f));
	}

	private void ShowInstructionRoleUI(bool isShow)
	{
		if (this.InstructionRoleUI == null)
		{
			this.RemoveRoleFX();
			return;
		}
		this.InstructionRoleUI.get_gameObject().SetActive(isShow);
		if (isShow)
		{
			this.AddRoleFX();
		}
		else
		{
			this.RemoveRoleFX();
		}
	}

	private void AddRoleFX()
	{
		this.role_fx = FXSpineManager.Instance.ReplaySpine(this.role_fx, 3203, this.InstructionRoleIcon, string.Empty, 10004, null, "UI", 0f, -185f, 0.75f, 0.75f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveRoleFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.role_fx, true);
		this.role_fx = 0;
	}

	public void JustLockScreen()
	{
		this.ShowFingerUI(false);
		this.ShowInstructionBubbleUI(false);
		this.ShowInstructionRoleUI(false);
		GuideUIView.IsTriggerNextStep = false;
		this.SetMaskA(0f);
	}

	public void SetImageShowUI(GuideStep dataStep)
	{
		this.SetImages(dataStep);
		this.ShowImageShowUI(true);
		UIManagerControl.Instance.OpenUI("PageUI", base.FindTransform("ISUIPageNode"), false, UIType.NonPush);
	}

	private void ShowImageShowUI(bool isShow)
	{
		if (this.ImageShowUI == null)
		{
			return;
		}
		this.ImageShowUI.get_gameObject().SetActive(isShow);
	}

	private void SetImages(GuideStep dataStep)
	{
		string[] list = dataStep.picture.Split(new char[]
		{
			';'
		});
		this.m_listPool.Create(list.Length, delegate(int index)
		{
			if (index < list.Length && index < this.m_listPool.Items.get_Count())
			{
				RawImage component = this.m_listPool.Items.get_Item(index).get_transform().FindChild("Image").GetComponent<RawImage>();
				ResourceManager.SetTexture(component, list[index]);
			}
		});
	}

	private void ResetInvalidClick()
	{
		this.InvalidClickNum = 0;
	}

	private void OnInvalidClick()
	{
		this.InvalidClickNum++;
		if (!GuideManager.Instance.step_attempt_lock)
		{
			if (this.InvalidClickNum >= 20)
			{
				GuideManager.Instance.StopGuideOfFinish();
			}
		}
		else if (this.InvalidClickNum >= 40)
		{
			GuideManager.Instance.StopGuideOfFinish();
		}
	}

	private void SetMaskA(float a)
	{
		this.m_spMaskScreen.set_color(new Color(0f, 0f, 0f, a));
	}

	private void JustOnMaskScreen()
	{
		if (GuideManager.Instance.step_attempt_lock)
		{
			return;
		}
		if (GuideManager.Instance.mintime_lock)
		{
			return;
		}
		if (GuideManager.Instance.finger_move_lock)
		{
			return;
		}
		if (GuideUIView.IsTriggerNextStep)
		{
			this.Show(false);
			EventDispatcher.Broadcast("GuideManager.NextStep");
		}
	}
}
