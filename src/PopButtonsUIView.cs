using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class PopButtonsUIView : UIBase
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
		PopButtonsUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("Privates").get_gameObject().AddComponent<ListBinder>();
		listBinder.PrefabName = "ButtonInfo2Privates";
		listBinder.SourceBinding.MemberName = "ButtonInfos";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}
}
