using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class TitleUIView : UIBase
{
	public static UIBase Instance;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		TitleUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("TitleName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502034, false));
		base.FindTransform("ConditionTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502035, false));
		base.FindTransform("LimitTimeTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502036, false));
		base.FindTransform("BonusesTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(502037, false));
		base.FindTransform("BtnComfirmText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(500011, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("Titles").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "TitleInfoUnit";
		listBinder.SourceBinding.MemberName = "TitleInfoUnits";
		TextBinder textBinder = base.FindTransform("ConditionDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ConditionDesc";
		textBinder = base.FindTransform("LimitTimeDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "LimitTimeDesc";
		textBinder = base.FindTransform("BonusesDesc1").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BonusesDesc1";
		textBinder = base.FindTransform("BonusesDesc2").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BonusesDesc2";
		textBinder = base.FindTransform("BonusesDesc3").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BonusesDesc3";
		textBinder = base.FindTransform("BonusesDesc4").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BonusesDesc4";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnComfirm").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnComfirmUp";
	}
}
