using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class PetObtainUIView : UIBase
{
	public static PetObtainUIView Instance;

	private RawImage m_spBackgroundImage;

	private Transform NodeBackground;

	private RawImage actorRawImage;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.2f;
		this.isClick = false;
	}

	private void Awake()
	{
		PetObtainUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIQueueManager.Instance.Islocked = false;
		base.FindTransform("PanelInfo").GetComponent<CanvasGroup>().set_alpha(0f);
		RTManager.SetRT(this.m_spBackgroundImage, ResourceManagerBase.GetNullTexture());
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			PetObtainUIView.Instance = null;
			PetObtainUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spBackgroundImage = base.FindTransform("BackgroundImage").GetComponent<RawImage>();
		this.NodeBackground = base.FindTransform("NodeBackground");
		this.actorRawImage = base.FindTransform("RawImageModel").GetComponent<RawImage>();
		RTManager.Instance.SetModelRawImage1(this.actorRawImage, false);
		EventTriggerListener expr_69 = EventTriggerListener.Get(base.FindTransform("ImageTouchPlace").get_gameObject());
		expr_69.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_69.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		UIAnimatorEventReceiver component = base.FindTransform("PanelRoot").GetComponent<UIAnimatorEventReceiver>();
		component.CallBackOfEnd = new Action(this.animEndOfObtain);
		UIAnimatorDisableWhenNoactive component2 = base.FindTransform("PanelRoot").GetComponent<UIAnimatorDisableWhenNoactive>();
		component2.DisableCallBack = new Action(this.animEndOfObtain);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ImageBinder imageBinder = base.FindTransform("PetStar").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PetStar";
		imageBinder.SetNativeSize = true;
		TextBinder textBinder = base.FindTransform("PetName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "PetName";
		textBinder = base.FindTransform("TipName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TipName";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowTip";
		visibilityBinder.Target = base.FindTransform("Tip").get_gameObject();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnConfirm").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnComfirmClick";
	}

	public void SetBackground(int type)
	{
		ResourceManager.SetCodeTexture(this.m_spBackgroundImage, PetManagerBase.GetBackground(type));
	}

	public void SetRawImageModelLayer(bool skillPlaying, bool petShow, bool isActivation)
	{
		this.actorRawImage.set_enabled(petShow);
		this.EnableAnimOfObtain(false);
		if (skillPlaying)
		{
			this.NodeBackground.SetAsLastSibling();
			if (this.actorRawImage != null)
			{
				this.actorRawImage.set_material(RTManager.Instance.RTNoAlphaMat);
			}
			if (isActivation)
			{
				this.actorRawImage.get_rectTransform().set_anchoredPosition(new Vector2(0f, 0f));
				this.m_spBackgroundImage.set_uvRect(new Rect(0f, 0f, 1f, 1f));
			}
		}
		else
		{
			this.NodeBackground.SetAsFirstSibling();
			if (this.actorRawImage != null)
			{
				this.actorRawImage.set_material(RTManager.Instance.RTNoAlphaMat);
			}
			if (isActivation)
			{
				this.EnableAnimOfObtain(true);
			}
		}
	}

	private void animEndOfObtain()
	{
		this.EnableAnimOfObtain(false);
	}

	private void EnableAnimOfObtain(bool play)
	{
		base.FindTransform("PanelRoot").GetComponent<Animator>().set_enabled(play);
		if (!play)
		{
			base.FindTransform("PanelInfo").GetComponent<CanvasGroup>().set_alpha(1f);
		}
	}
}
