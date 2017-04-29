using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class ButtonToggle2SubUI : BaseUIBehaviour
{
	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("ButtonToggleText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Name";
		textBinder = base.FindTransform("ButtonToggleCmText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Name";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "IsTip";
		visibilityBinder.Target = base.FindTransform("Tip").get_gameObject();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ToggleBinder toggleBinder = base.get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.BindingProxy = base.get_gameObject();
		toggleBinder.ValueBinding.MemberName = "IsToggleOn";
	}
}
