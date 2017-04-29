using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NetworkDialogUIView : UIBase
{
	public delegate void VoidDelegateObj();

	public static NetworkDialogUIView Instance;

	public NetworkDialogUIView.VoidDelegateObj MaskAction;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		this.isInterruptStick = true;
	}

	private void Awake()
	{
		NetworkDialogUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		WaitUI.CloseUI(0u);
		base.get_transform().SetAsFirstSibling();
		GuideUIView.IsDownOn = false;
		this.isClick = true;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		GuideUIView.IsDownOn = true;
	}

	protected override void OnClickMaskAction()
	{
		if (!this.isClick)
		{
			return;
		}
		this.Show(false);
		if (this.MaskAction != null)
		{
			this.MaskAction();
		}
		SoundManager.PlayUI(10037, false);
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
		textBinder = base.FindTransform("GoodNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "GoodNum";
		textBinder = base.FindTransform("FinalContent").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "FinalContent";
		textBinder = base.FindTransform("TwoContent").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "DescContent";
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
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ContentVisibility";
		visibilityBinder.Target = base.FindTransform("Content").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "Content1Visibility";
		visibilityBinder.Target = base.FindTransform("Content1").get_gameObject();
		ImageBinder imageBinder = base.FindTransform("BtnLeftBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageBtnL";
		imageBinder = base.FindTransform("BtnRightBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageBtnR";
		imageBinder = base.FindTransform("BtnConfirmBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageBtnC";
		imageBinder = base.FindTransform("Good").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageGood";
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

	public void SetSibling()
	{
		if (base.get_transform().get_parent() == UINodesManager.T3RootOfSpecial)
		{
			base.SetAsFirstSibling();
		}
		else
		{
			base.SetAsLastSibling();
		}
	}

	public void SetContentTextFormat(int fontSize, TextAnchor align)
	{
		base.FindTransform("Content").GetComponent<Text>().set_fontSize(fontSize);
		base.FindTransform("Content").GetComponent<Text>().set_alignment(align);
	}

	public void ResetContentTextFormat()
	{
		base.FindTransform("Content").GetComponent<Text>().set_fontSize(28);
		base.FindTransform("Content").GetComponent<Text>().set_alignment(4);
	}
}
