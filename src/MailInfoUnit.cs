using Foundation.Core.Databinding;
using System;
using UnityEngine;

public class MailInfoUnit : BaseUIBehaviour
{
	public const uint delDuration = 350u;

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
		ImageBinder imageBinder = base.FindTransform("StatusIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "StatusIcon";
		imageBinder.SetNativeSize = true;
		TextBinder textBinder = base.FindTransform("SendDate").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "SendDate";
		textBinder = base.FindTransform("SendName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "SendName";
		textBinder = base.FindTransform("MailDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MailContent";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "IsSelected";
		visibilityBinder.Target = base.FindTransform("Selected").get_gameObject();
		ActionBinder actionBinder = base.get_gameObject().AddComponent<ActionBinder>();
		actionBinder.BindingProxy = base.get_gameObject();
		actionBinder.CallActionOfBoolBinding.MemberName = "CallAction";
		actionBinder.actoncall_bool = new Action<bool>(this.DelAnim);
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnUp";
	}

	private void DelAnim(bool arg)
	{
		if (this == null || base.get_gameObject() == null)
		{
			return;
		}
		if (!arg)
		{
			return;
		}
		ListViewBinder src = base.get_transform().get_parent().get_parent().GetComponent<ListViewBinder>();
		if (src != null)
		{
			src.set_enabled(false);
			RectTransform rectTransform = base.get_transform() as RectTransform;
			base.StartCoroutine(rectTransform.MoveToAnchoredPosition(new Vector3(rectTransform.get_anchoredPosition().x - 800f, rectTransform.get_anchoredPosition().y), 0.35f, EaseType.Linear, null));
			TimerHeap.AddTimer(350u, 0, delegate
			{
				if (src != null)
				{
					src.set_enabled(true);
				}
			});
		}
	}
}
