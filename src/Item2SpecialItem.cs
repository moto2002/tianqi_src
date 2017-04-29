using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class Item2SpecialItem : BaseUIBehaviour
{
	public Text m_num;

	public Image m_icon;

	private void Awake()
	{
		this.m_num = base.FindTransform("Num").get_gameObject().GetComponent<Text>();
		this.m_icon = base.FindTransform("Icon").get_gameObject().GetComponent<Image>();
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("Num").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Content";
		ImageBinder imageBinder = base.FindTransform("Icon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "Icon";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}
}
