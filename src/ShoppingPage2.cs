using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class ShoppingPage2 : BaseUIBehaviour
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
		ListBinder listBinder = base.FindTransform("ItemList").get_gameObject().AddComponent<ListBinder>();
		listBinder.PrefabName = "ShoppingUnit2";
		listBinder.SourceBinding.MemberName = "Items";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}
}
