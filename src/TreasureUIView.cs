using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class TreasureUIView : UIBase
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
		TreasureUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			TreasureUIView.Instance = null;
			TreasureUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("ObtainTip").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508029, false));
		base.FindTransform("ConsumeName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508030, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("BtnOKText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnOKText";
		textBinder = base.FindTransform("ConsumeNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ConsumeNum";
		textBinder = base.FindTransform("SpecialItemText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "SpecialItemText";
		ImageBinder imageBinder = base.FindTransform("ConsumeIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ConsumeIcon";
		imageBinder = base.FindTransform("VIPLevel10").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPLevel10";
		imageBinder.SetLayoutIgnoreWhenEmpty = true;
		imageBinder = base.FindTransform("VIPLevel1").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPLevel1";
		imageBinder.SetLayoutIgnoreWhenEmpty = true;
		ListBinder listBinder = base.FindTransform("ItemList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "Item2Draw";
		listBinder.SourceBinding.MemberName = "ObatinItems";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("Consume").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ConsumeOn";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("OpenRegion").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickOK";
	}
}
