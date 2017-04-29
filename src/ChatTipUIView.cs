using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BindingContext))]
public class ChatTipUIView : UIBase
{
	public const int UP_Y = 37;

	public const int DOWN_Y = -58;

	private const float LINE_HEIGHT = 22f;

	private const float MAX_HEIGHT = 88f;

	public static ChatTipUIView Instance;

	private Transform m_tranContent;

	private List<ChatInfoBase> Chats = new List<ChatInfoBase>();

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isEndNav = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		ChatTipUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_tranContent = base.FindTransform("Content");
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.Target = base.FindTransform("ButtonPrivate").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ButtonPrivateOn";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("Background").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnClick";
		buttonBinder = base.FindTransform("ButtonMsg").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnButtonMsg";
		buttonBinder = base.FindTransform("ButtonPrivate").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.OnClickBinding.MemberName = "OnButtonPrivate";
	}

	public void ShowAsBottomCenter(bool up = true)
	{
		if (base.get_gameObject() == null || this == null)
		{
			return;
		}
		int num = (!up) ? -58 : 37;
		if (UIManagerControl.Instance.IsOpen("BattleUI"))
		{
			num -= 10;
		}
		RectTransform component = base.GetComponent<RectTransform>();
		component.set_anchorMin(new Vector2(0.5f, 0f));
		component.set_anchorMax(new Vector2(0.5f, 0f));
		component.set_anchoredPosition(new Vector3(0f, (float)num));
		component.set_localScale(Vector3.get_one());
	}

	public void RefreshChats(List<ChatManager.ChatInfo> chatInfos)
	{
		this.ClearChats();
		if (chatInfos.get_Count() == 0)
		{
			return;
		}
		for (int i = 0; i < chatInfos.get_Count(); i++)
		{
			ChatInfoBase chatInfoBase = ChatManager.CreatePrefab2TipChatInfo(string.Empty);
			chatInfoBase.ShowInfo(chatInfos.get_Item(i));
			this.Chats.Add(chatInfoBase);
		}
		while (this.Chats.get_Count() > 0 && this.CalHeight4Chats(this.Chats.get_Count()) > 88f)
		{
			ChatManager.Reuse2TipChatInfoPool(this.Chats.get_Item(0));
			this.Chats.RemoveAt(0);
		}
		for (int j = 0; j < this.Chats.get_Count(); j++)
		{
			RectTransform rectTransform = this.Chats.get_Item(j).get_transform() as RectTransform;
			UGUITools.ResetTransform(rectTransform.get_transform(), this.m_tranContent);
			rectTransform.set_anchoredPosition(new Vector2(0f, -this.CalHeight4Chats(j)));
		}
	}

	public void ClearChats()
	{
		for (int i = 0; i < this.Chats.get_Count(); i++)
		{
			ChatManager.Reuse2TipChatInfoPool(this.Chats.get_Item(i));
		}
		this.Chats.Clear();
	}

	private float CalHeight4Chats(int cal_count)
	{
		float num = 0f;
		int num2 = 0;
		while (num2 < this.Chats.get_Count() && num2 < cal_count)
		{
			num += Mathf.Max(22f, this.Chats.get_Item(num2).m_height);
			num2++;
		}
		return num;
	}
}
