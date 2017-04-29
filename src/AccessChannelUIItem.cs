using Foundation.Core.Databinding;
using System;

public class AccessChannelUIItem : BaseUIBehaviour
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
		ImageBinder imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "Icon";
		TextBinder textBinder = base.FindTransform("Title").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Title";
		textBinder = base.FindTransform("TitleDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TitleDesc";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnChannelBtnUp";
	}
}
