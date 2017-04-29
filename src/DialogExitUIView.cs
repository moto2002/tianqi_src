using Foundation.Core.Databinding;
using System;

public class DialogExitUIView : UIBase
{
	public static UIBase Instance;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		this.isInterruptStick = true;
	}

	private void Awake()
	{
		DialogExitUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		GuideUIView.IsDownOn = false;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		GuideUIView.IsDownOn = true;
	}

	protected override void InitUI()
	{
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("titleName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Title";
		textBinder = base.FindTransform("Content").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Content";
		textBinder = base.FindTransform("BtnLeftText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnLeftText";
		textBinder = base.FindTransform("BtnRightText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnRightText";
		textBinder = base.FindTransform("BtnConfirmText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnConfirmText";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnLeftVisibility";
		visibilityBinder.Target = base.FindTransform("BtnLeft").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnRightVisibility";
		visibilityBinder.Target = base.FindTransform("BtnRight").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnConfirmVisibility";
		visibilityBinder.Target = base.FindTransform("BtnConfirm").get_gameObject();
		ImageBinder imageBinder = base.FindTransform("BtnLeftBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageBtnL";
		imageBinder = base.FindTransform("BtnRightBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageBtnR";
		imageBinder = base.FindTransform("BtnConfirmBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageBtnC";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnLeft").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnLeftUp";
		buttonBinder = base.FindTransform("BtnRight").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnRightUp";
		buttonBinder = base.FindTransform("BtnConfirm").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnConfirmUp";
	}
}
