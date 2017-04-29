using Foundation.Core.Databinding;
using System;

public class PetChooseItems : BaseUIBehaviour
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
		ListBinder listBinder = base.FindTransform("List").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.LoadNumberFrame = 3;
		listBinder.PrefabName = WidgetName.PetChooseUnit;
		listBinder.SourceBinding.MemberName = "Items";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "LineRegion";
		visibilityBinder.Target = base.FindTransform("LineRegion").get_gameObject();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}
}
