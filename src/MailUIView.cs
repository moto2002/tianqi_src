using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class MailUIView : UIBase
{
	public static UIBase Instance;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		MailUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110009), "BACK", delegate
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			MailUIView.Instance = null;
			MailUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.FindTransform("SendUIBtnSendText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502015, false));
		base.FindTransform("MailItemTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502071, false));
		base.FindTransform("MailNoTip").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502075, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "MailListUIVisibility";
		visibilityBinder.Target = base.FindTransform("MailListUI").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "MailDetailUIVisibility";
		visibilityBinder.Target = base.FindTransform("MailDetailUI").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "MailSendUIVisibility";
		visibilityBinder.Target = base.FindTransform("MailSendUI").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "MailNoTipUIVisibility";
		visibilityBinder.Target = base.FindTransform("MailNoTipUI").get_gameObject();
		visibilityBinder = base.FindTransform("Buttons").get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "MailBtn1Visibility";
		visibilityBinder.Target = base.FindTransform("MailBtn1").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("MailNoTip").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "NoMailTip";
		ListViewBinder listViewBinder = base.FindTransform("MailListScroll").get_gameObject().AddComponent<ListViewBinder>();
		listViewBinder.BindingProxy = base.get_gameObject();
		listViewBinder.PrefabName = "MailInfoUnit";
		listViewBinder.m_spacing = 120f;
		listViewBinder.m_scrollStype = ListView.ListViewScrollStyle.Up;
		listViewBinder.SourceBinding.MemberName = "MailInfoUnits";
		TextBinder textBinder = base.FindTransform("MailSender").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MailSender";
		textBinder = base.FindTransform("MailDate").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MailDate";
		textBinder = base.FindTransform("MailContent").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MailContent";
		textBinder.SetHeight = true;
		textBinder = base.FindTransform("MailBtn1Text").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MailBtn1Name";
		textBinder = base.FindTransform("DownTime").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "DownTime";
		ListBinder listBinder = base.FindTransform("Items").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "Item2Draw";
		listBinder.SourceBinding.MemberName = "MailItems";
		InputFieldBinder inputFieldBinder = base.FindTransform("MailInput").get_gameObject().AddComponent<InputFieldBinder>();
		inputFieldBinder.BindingProxy = base.get_gameObject();
		inputFieldBinder.TextBinding.MemberName = "MailSendContent";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("MailBtn1").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnMailBtn1Up";
		buttonBinder = base.FindTransform("MailBtnOneKey").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnMailBtnOneKeyUp";
		buttonBinder = base.FindTransform("SendUIBtnSend").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnSendUp";
		buttonBinder = base.FindTransform("DetailUIBtnBack").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnBackUp";
		buttonBinder = base.FindTransform("SendUIBtnBack").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnBackUp";
	}
}
