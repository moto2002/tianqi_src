using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class VipBtn : BaseUIBehaviour
{
	private Image m_ImageGrey;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_ImageGrey = base.FindTransform("ImageGrey").GetComponent<Image>();
		ButtonCustom expr_22 = base.GetComponent<ButtonCustom>();
		expr_22.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_22.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClick));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("BtnText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnName";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageGrey").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageGreyVisibility";
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnButtonClick";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	private void OnClick(GameObject go)
	{
		this.m_ImageGrey.get_gameObject().SetActive(false);
	}
}
