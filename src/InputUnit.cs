using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class InputUnit : BaseUIBehaviour
{
	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("InputPlaceholder").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502074, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		InputFieldBinder inputFieldBinder = base.FindTransform("Input").get_gameObject().AddComponent<InputFieldBinder>();
		inputFieldBinder.BindingProxy = base.get_gameObject();
		inputFieldBinder.TextBinding.MemberName = "Input";
		TextBinder textBinder = base.FindTransform("Title").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Title";
		textBinder = base.FindTransform("BtnCommitText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnText";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnCommit").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnCommitUp";
	}
}
