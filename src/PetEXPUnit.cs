using Foundation.Core.Databinding;
using System;

public class PetEXPUnit : BaseUIBehaviour
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
		ImageBinder imageBinder = base.FindTransform("ItemFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemFrame";
		imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemIcon";
		imageBinder.SetNativeSize = true;
		TextBinder textBinder = base.FindTransform("ItemNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemNum";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "Checked";
		visibilityBinder.Target = base.FindTransform("Checked").get_gameObject();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonCustomBinder buttonCustomBinder = base.FindTransform("BtnUse").get_gameObject().AddComponent<ButtonCustomBinder>();
		buttonCustomBinder.BindingProxy = base.get_gameObject();
		buttonCustomBinder.IsDownSuccession = true;
		buttonCustomBinder.OnClickBinding.MemberName = "OnBtnUseClick";
		buttonCustomBinder.OnDownBinding.MemberName = "OnBtnUseDown";
		buttonCustomBinder.OnUpBinding.MemberName = "OnBtnUseUp";
		buttonCustomBinder.EnabledBinding.MemberName = "BtnUseEnable";
		ButtonBinder buttonBinder = base.FindTransform("BtnItem").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnItemUp";
	}
}
