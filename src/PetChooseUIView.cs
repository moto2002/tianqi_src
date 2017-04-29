using Foundation.Core.Databinding;
using System;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class PetChooseUIView : UIBase
{
	public const float NormalSpacing = 180f;

	public static PetChooseUIView Instance;

	public ListViewBinder m_listView;

	protected override void Preprocessing()
	{
	}

	private void Awake()
	{
		PetChooseUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			PetChooseUIView.Instance = null;
			PetChooseUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		this.m_listView = base.FindTransform("ListScroll").get_gameObject().AddComponent<ListViewBinder>();
		this.m_listView.PrefabName = WidgetName.PetChooseUnits;
		this.m_listView.m_spacing = 180f;
		this.m_listView.m_scrollStype = ListView.ListViewScrollStyle.Up;
		this.m_listView.SourceBinding.MemberName = "PetChooseLines";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}
}
