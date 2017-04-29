using Foundation.Core.Databinding;
using System;

public class BackpackItem : BaseUIBehaviour
{
	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemRootNullOn";
		visibilityBinder.Target = base.FindTransform("ItemRootNull").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemRootOn";
		visibilityBinder.Target = base.FindTransform("ItemRoot").get_gameObject();
		ImageBinder imageBinder = base.FindTransform("ItemFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemFrame";
		imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemIcon";
		TextBinder textBinder = base.FindTransform("ItemNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemNum";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemFlag";
		visibilityBinder.Target = base.FindTransform("ItemFlag").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemStepOn";
		visibilityBinder.Target = base.FindTransform("ItemStep").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "EquipIsBinding";
		visibilityBinder.Target = base.FindTransform("ImageBinding").get_gameObject();
		textBinder = base.FindTransform("ItemStepNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemStepNum";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "IsSelected01";
		visibilityBinder.Target = base.FindTransform("Selected01").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "IsSelected02ModeOn";
		visibilityBinder.Targets.Add(base.FindTransform("Selected02").get_gameObject());
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "IsSelected02";
		visibilityBinder.Targets.Add(base.FindTransform("Selected02Cm").get_gameObject());
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentAttrVisibility";
		visibilityBinder.Target = base.FindTransform("ExcellentAttrIconList").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentImage1";
		visibilityBinder.Target = base.FindTransform("ExcellentAttrIconList").FindChild("Image1").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentImage2";
		visibilityBinder.Target = base.FindTransform("ExcellentAttrIconList").FindChild("Image2").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentImage3";
		visibilityBinder.Target = base.FindTransform("ExcellentAttrIconList").FindChild("Image3").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "RedPointOn";
		visibilityBinder.Target = base.FindTransform("RedPoint").get_gameObject();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonParamater buttonParamater = base.get_gameObject().AddComponent<ButtonParamater>();
		buttonParamater.BindingProxy = base.get_gameObject();
		buttonParamater.ParamaterType = ButtonParamater.ParamaterTypeEnum.Context;
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnClickItem";
	}
}
