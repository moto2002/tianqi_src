using Foundation.Core.Databinding;
using System;

public class MultiCheckUIView : UIBase
{
	public static MultiCheckUIView Instance;

	public Action MaskAction;

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
		this.isMask = true;
		this.alpha = 0.75f;
		this.isClick = true;
	}

	private void Awake()
	{
		MultiCheckUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		GuideUIView.IsDownOn = false;
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
			this.MaskAction.Invoke();
		}
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
		textBinder = base.FindTransform("BtnLeftText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnLeftText";
		textBinder = base.FindTransform("BtnRightText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnRightText";
		textBinder = base.FindTransform("BtnConfirmText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnConfirmText";
		textBinder = base.FindTransform("ContentTipText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ContentTipText";
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
		ListBinder component = base.FindTransform("ItemList").get_gameObject().GetComponent<ListBinder>();
		component.BindingProxy = base.get_gameObject();
		component.PrefabName = "MultiCheckItem";
		component.SourceBinding.MemberName = "Items";
		component.ITEM_NAME = "_Item";
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
		buttonBinder = base.FindTransform("BtnClose").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnCloseUp";
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
}
