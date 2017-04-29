using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class AccessChannelUIView : UIBase
{
	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.FindTransform("Title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(500010, false));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("Channels").get_gameObject().AddComponent<ListBinder>();
		listBinder.PrefabName = "AccessChannelUIItem";
		listBinder.SourceBinding.MemberName = "AccessChannelUIItems";
		ImageBinder imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "ItemIcon";
		imageBinder = base.FindTransform("ItemIconBg").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "ItemIconBg";
		TextBinder textBinder = base.FindTransform("ItemName").get_gameObject().AddComponent<TextBinder>();
		textBinder.LabelBinding.MemberName = "ItemName";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BgMask").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnCloseBtnUp";
	}
}
