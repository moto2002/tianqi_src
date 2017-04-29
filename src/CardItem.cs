using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class CardItem : BaseUIBehaviour
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
		ImageBinder imageBinder = base.FindTransform("GoodsIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "GoodsIcon";
		imageBinder.SetNativeSize = true;
		TextBinder textBinder = base.FindTransform("ItemName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "CardName";
		textBinder = base.FindTransform("Price").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Price";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("SelectPicture").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageSelectVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("SelectBackground").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageBgSelectVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageName1").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageName1Visibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageName2").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageName2Visibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageName3").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageName3Visibility";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnButtonClick";
	}
}
