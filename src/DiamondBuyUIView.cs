using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class DiamondBuyUIView : UIBase
{
	public static DiamondBuyUIView Instance;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		DiamondBuyUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			DiamondBuyUIView.Instance = null;
			DiamondBuyUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnCancelName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505113, false));
		base.FindTransform("BtnOKName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508013, false));
		base.FindTransform("Info2_0").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508027, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("Info1").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Info1";
		textBinder = base.FindTransform("Info2_1").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Info2_1";
		ImageBinder imageBinder = base.FindTransform("Icon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "Icon";
		imageBinder.SetNativeSize = true;
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnCancel").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnCancelClick";
		buttonBinder = base.FindTransform("BtnOK").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnOKClick";
	}
}
