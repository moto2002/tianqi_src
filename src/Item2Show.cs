using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class Item2Show : BaseUIBehaviour
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
		ImageBinder imageBinder = base.FindTransform("Frame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "Frame";
		imageBinder = base.FindTransform("Icon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "Icon";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnBtnUp";
	}
}
