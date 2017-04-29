using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class StoveBuildEquipPartCheck : BaseUIBehaviour
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
		ImageBinder imageBinder = base.FindTransform("Name").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "Name";
		imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "ItemIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("ItemIconCm").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "ItemIconCm";
		imageBinder.SetNativeSize = true;
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ToggleBinder toggleBinder = base.get_gameObject().AddComponent<ToggleBinder>();
		toggleBinder.ValueBinding.MemberName = "IsOn";
	}
}
