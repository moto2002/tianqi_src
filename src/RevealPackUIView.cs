using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class RevealPackUIView : UIBase
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
		RevealPackUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("Items").get_gameObject().AddComponent<ListBinder>();
		listBinder.PrefabName = "Item2Check";
		listBinder.SourceBinding.MemberName = "Item2Checks";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}
}
