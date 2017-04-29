using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : BaseUIBehaviour
{
	private const int depth_default = -100;

	public const float DEFAULT_MASK_A = 0.7f;

	protected UIType _uiType;

	private bool _isVisible = true;

	private string _prefabName;

	private int _uiDepth = -100;

	[HideInInspector]
	private bool _inited;

	[HideInInspector]
	private bool _hideMainCamera;

	[HideInInspector]
	protected bool isInterruptStick = true;

	[HideInInspector]
	protected bool isEndNav = true;

	[HideInInspector]
	protected bool isIgnoreToSpine;

	protected string ButtonCloseName = "CloseBtn";

	private bool hasReleased;

	private bool isIgnoreHideMainCamera = true;

	public bool isMask;

	[Range(0f, 1f)]
	public float alpha;

	private bool _isClick;

	public bool isClick;

	protected bool isIgnoreCollider;

	private GameObject goMask;

	[HideInInspector]
	public bool isBeenPushed;

	[HideInInspector]
	private bool _isMotion;

	public UIType uiType
	{
		get
		{
			return this._uiType;
		}
		set
		{
			this._uiType = value;
		}
	}

	public bool isVisible
	{
		get
		{
			return this._isVisible;
		}
		set
		{
			this._isVisible = value;
		}
	}

	public string prefabName
	{
		get
		{
			return this._prefabName;
		}
		set
		{
			this._prefabName = value;
		}
	}

	public int uiDepth
	{
		get
		{
			if (this._uiDepth == -100)
			{
				DepthOfUI componentInParent = base.GetComponentInParent<DepthOfUI>();
				if (componentInParent != null)
				{
					this._uiDepth = componentInParent.SortingOrder;
				}
				else
				{
					Canvas componentInParent2 = base.GetComponentInParent<Canvas>();
					if (componentInParent2 != null)
					{
						this._uiDepth = componentInParent2.get_sortingOrder();
					}
				}
			}
			return this._uiDepth;
		}
	}

	public bool inited
	{
		get
		{
			return this._inited;
		}
		set
		{
			this._inited = value;
		}
	}

	public bool hideMainCamera
	{
		get
		{
			return !this.isMotion && !this.isIgnoreHideMainCamera && this._hideMainCamera;
		}
		set
		{
			this._hideMainCamera = value;
		}
	}

	private bool isMotion
	{
		get
		{
			return this._isMotion;
		}
		set
		{
			this._isMotion = value;
		}
	}

	public bool GetIsIgnoreToSpine()
	{
		return this.uiType == UIType.FullScreen || this.isIgnoreToSpine;
	}

	public void Init(string name)
	{
		this.prefabName = name;
		this.inited = true;
		this.Preprocessing();
		this.SetMask();
		this.isVisible = false;
	}

	protected override void InitUI()
	{
		base.InitUI();
		Transform transform = base.FindTransform(this.ButtonCloseName);
		if (transform != null && transform.GetComponent<ButtonCustom>() != null)
		{
			transform.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCloseBtn);
		}
	}

	protected virtual void OnClickCloseBtn(GameObject go)
	{
		this.OnClickMaskAction();
	}

	protected virtual void Preprocessing()
	{
	}

	protected virtual void OnEnable()
	{
		if (this.isInterruptStick)
		{
			EventDispatcher.Broadcast("ControlStick.InterruptStick");
		}
		if (GuideManager.IsGuideSystemPause(this.prefabName))
		{
			EventDispatcher.Broadcast<bool>("GuideManager.PauseGuideSystem", true);
		}
		if (this.isEndNav && EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.CheckCancelNavToNPC())
		{
			MainTaskManager.Instance.StopToNPC(true);
		}
		UIManagerControl.Instance.RefreshlistOpenUI(this, true);
	}

	protected virtual void OnDisable()
	{
		if (GuideManager.IsGuideSystemPause(this.prefabName))
		{
			EventDispatcher.Broadcast<bool>("GuideManager.PauseGuideSystem", false);
		}
		UIManagerControl.Instance.RefreshlistOpenUI(this, false);
	}

	protected virtual void ReleaseSelf(bool destroy)
	{
		if (this.hasReleased)
		{
			return;
		}
		if (!destroy)
		{
			return;
		}
		this.hasReleased = true;
		if (!string.IsNullOrEmpty(this.prefabName))
		{
			UIManagerControl.Instance.UnLoadUIPrefab(this.prefabName);
		}
	}

	protected virtual void OnClickMaskAction()
	{
		this.Show(false);
		SoundManager.PlayUI(10037, false);
	}

	protected sealed override void OnDestroy()
	{
		base.OnDestroy();
		this.ReleaseSelf(true);
	}

	public virtual void Show(bool isShow)
	{
		this.isIgnoreHideMainCamera = false;
		this.isVisible = isShow;
		if (isShow)
		{
			this.isMotion = true;
			if (!this.ShowSetting())
			{
				this.SetMainCameraEnable();
			}
		}
		else
		{
			if (!this.DisAppear())
			{
				this.HideSetting();
			}
			this.SetMainCameraEnable();
		}
	}

	private bool ShowSetting()
	{
		this.ShowContent();
		bool result = this.Appear();
		this.UpdateDataUI();
		return result;
	}

	private void ShowContent()
	{
		if (this != null && base.get_gameObject() != null)
		{
			base.get_gameObject().SetActive(true);
		}
		if (this.prefabName != "WaitingUI" && this.prefabName != "WaitUI")
		{
			EventDispatcher.Broadcast<string>("UIManagerControl.UIOpenOfSuccess", this.prefabName);
		}
	}

	private bool Appear()
	{
		return this.PrivateMotionOpen();
	}

	public virtual void UpdateDataUI()
	{
	}

	private void HideSetting()
	{
		this.HideContent();
		this.ReleaseSelf(false);
		if (!UIManagerControl.IsSpecialUI(this.prefabName))
		{
			GuideManager.Instance.CheckCurrentGuide();
		}
		if (!UIQueueManager.Instance.CheckQueue(PopCondition.None))
		{
			if (this.uiType == UIType.FullScreen)
			{
				GuideManager.Instance.CheckQueue(false, false);
			}
			else
			{
				GuideManager.Instance.CheckQueue(true, false);
			}
		}
	}

	private bool DisAppear()
	{
		return this.PrivateMotionClose();
	}

	private void HideContent()
	{
		if (this != null && base.get_gameObject() != null)
		{
			base.get_gameObject().SetActive(false);
		}
	}

	public void ShowMask(bool isShow)
	{
		if (this.goMask != null)
		{
			this.goMask.SetActive(isShow);
		}
	}

	public void SetMask()
	{
		if (this.isMask)
		{
			if (this.goMask == null)
			{
				this.goMask = new GameObject("Mask2Common");
				this.goMask.set_layer(LayerMask.NameToLayer("UI"));
				RectTransform rectTransform = this.goMask.AddComponent<RectTransform>();
				rectTransform.SetParent(base.get_gameObject().get_transform());
				rectTransform.set_localPosition(Vector3.get_zero());
				rectTransform.set_localRotation(Quaternion.get_identity());
				rectTransform.set_localScale(Vector3.get_one());
				rectTransform.set_sizeDelta(new Vector2(20000f, 20000f));
				rectTransform.SetAsFirstSibling();
				this.goMask.AddComponent<Image>();
			}
			if (this.isIgnoreCollider)
			{
				IgnoreCanvasRaycast ignoreCanvasRaycast = this.goMask.AddMissingComponent<IgnoreCanvasRaycast>();
				ignoreCanvasRaycast.set_enabled(true);
			}
			else
			{
				IgnoreCanvasRaycast component = this.goMask.GetComponent<IgnoreCanvasRaycast>();
				if (component != null)
				{
					component.set_enabled(false);
				}
				if (this.isClick)
				{
					ButtonCustom buttonCustom = this.goMask.AddMissingComponent<ButtonCustom>();
					buttonCustom.set_enabled(true);
					buttonCustom.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMask);
				}
				else
				{
					ButtonCustom component2 = this.goMask.GetComponent<ButtonCustom>();
					if (component2 != null)
					{
						component2.set_enabled(false);
					}
				}
			}
			this.goMask.GetComponent<Image>().set_color(new Color(0f, 0f, 0f, this.alpha));
			this.goMask.SetActive(true);
		}
		else if (this.goMask != null)
		{
			this.goMask.SetActive(false);
		}
	}

	public void SetMask(float alph, bool mask, bool click)
	{
		this.isMask = mask;
		this.alpha = alph;
		this.isClick = click;
		this.SetMask();
	}

	private void OnClickMask(GameObject go)
	{
		if (!this.isClick)
		{
			return;
		}
		this.OnClickMaskAction();
	}

	private void SetMaskNonLink()
	{
		if (this.goMask != null)
		{
			this.goMask.get_transform().SetParent(base.get_transform().get_parent());
			this.goMask.get_transform().SetSiblingIndex(base.get_transform().GetSiblingIndex() - 1);
		}
	}

	private void SetMaskToLink()
	{
		if (this.goMask != null)
		{
			this.goMask.get_transform().SetParent(base.get_transform());
			this.goMask.get_transform().SetAsFirstSibling();
		}
	}

	protected virtual void ActionOpen()
	{
		this.SetMaskToLink();
		this.SetMainCameraEnable();
	}

	protected virtual void ActionClose()
	{
		this.SetMaskToLink();
		this.HideSetting();
	}

	private void PanelMotionOpen()
	{
		UISwitchAnim anim = UIMotionSystem.GetAnim(this.prefabName);
		if (anim != null && anim.ShowAnim > 0)
		{
			this.SetMaskNonLink();
			PanelSwitcher.DoAnim(base.get_transform(), anim.ShowAnim, delegate
			{
				this.ActionOpen();
			});
		}
	}

	private bool PanelMotionClose()
	{
		UISwitchAnim anim = UIMotionSystem.GetAnim(this.prefabName);
		if (anim != null && anim.HideAnim > 0)
		{
			this.SetMaskNonLink();
			PanelSwitcher.DoAnim(base.get_transform(), anim.HideAnim, delegate
			{
				this.ActionClose();
			});
			return true;
		}
		return false;
	}

	private bool PrivateMotionOpen()
	{
		if (!UIMotionSystem.IsUIMotionSystemOn)
		{
			this.ActionOpen();
			return false;
		}
		if (this == null)
		{
			return false;
		}
		UIMotion component = base.GetComponent<UIMotion>();
		return component != null && component.get_enabled() && component.MotionOpen(delegate
		{
			if (this != null)
			{
				this.ActionOpen();
			}
		});
	}

	private bool PrivateMotionClose()
	{
		if (!UIMotionSystem.IsUIMotionSystemOn)
		{
			this.ActionClose();
			return false;
		}
		if (this.isBeenPushed)
		{
			this.isBeenPushed = false;
			return false;
		}
		if (this == null)
		{
			return false;
		}
		UIMotion component = base.GetComponent<UIMotion>();
		return component != null && component.get_enabled() && component.MotionClose(delegate
		{
			if (this != null)
			{
				this.ActionClose();
			}
		});
	}

	public static bool IsNeedPush(UIType uiType)
	{
		return uiType == UIType.FullScreen || uiType == UIType.Pop;
	}

	private void SetMainCameraEnable()
	{
		this.isMotion = false;
		if (!UIManagerControl.IsSpecialUI(this.prefabName))
		{
			UIManagerControl.Instance.SetMainCameraEnable();
		}
	}

	protected void SetAsFirstSibling()
	{
		base.get_transform().SetAsFirstSibling();
	}

	protected void SetAsLastSibling()
	{
		base.get_transform().SetAsLastSibling();
	}

	protected void SetSiblingIndex(int index)
	{
		base.get_transform().SetSiblingIndex(index);
	}
}
