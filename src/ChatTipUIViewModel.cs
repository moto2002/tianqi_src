using Foundation.Core;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatTipUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string ButtonPrivateOn = "ButtonPrivateOn";

		public const string Event_OnClick = "OnClick";

		public const string Event_OnButtonMsg = "OnButtonMsg";

		public const string Event_OnButtonPrivate = "OnButtonPrivate";
	}

	public const int MAX_NEW = 4;

	private static ChatTipUIViewModel m_instance;

	private bool IsUp = true;

	private bool _ButtonPrivateOn;

	private static bool IsNeedRefresh = false;

	private static List<ChatManager.ChatInfo> chatNews = new List<ChatManager.ChatInfo>();

	public static ChatTipUIViewModel Instance
	{
		get
		{
			return ChatTipUIViewModel.m_instance;
		}
	}

	public bool ButtonPrivateOn
	{
		get
		{
			return this._ButtonPrivateOn;
		}
		set
		{
			this._ButtonPrivateOn = value;
			base.NotifyProperty("ButtonPrivateOn", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ChatTipUIViewModel.m_instance = this;
	}

	private void OnEnable()
	{
		if (ChatTipUIViewModel.IsNeedRefresh)
		{
			ChatTipUIViewModel.IsNeedRefresh = false;
			ChatTipUIView.Instance.RefreshChats(ChatTipUIViewModel.chatNews);
		}
	}

	public void OnClick()
	{
		ChatManager.Instance.OpenChatUI(1);
	}

	public void OnButtonMsg()
	{
		if (TownUI.Instance != null)
		{
			TownUI.Instance.IsForceOpenRightBottom = false;
		}
		this.IsUp = !this.IsUp;
		this.ShowAsBottomCenter(this.IsUp);
	}

	public void OnButtonPrivate()
	{
		ChatManager.Instance.OpenChatUI2ChannelPrivate(0L, string.Empty);
	}

	public static void ForceOff()
	{
		if (ChatTipUIViewModel.Instance != null)
		{
			ChatTipUIViewModel.Instance.IsUp = false;
			ChatTipUIView.Instance.ShowAsBottomCenter(false);
		}
	}

	private void ShowAsBottomCenter(bool isUp)
	{
		if (TownUI.Instance != null && TownUI.Instance.IsForceOpenRightBottom)
		{
			isUp = false;
		}
		this.IsUp = isUp;
		ChatTipUIView.Instance.ShowAsBottomCenter(isUp);
		if (UIManagerControl.Instance.IsOpen("TownUI") && isUp)
		{
			TownUI.Instance.SwitchRightBottom(!isUp, false);
		}
	}

	public void Release()
	{
		ChatTipUIViewModel.chatNews.Clear();
		ChatTipUIView.Instance.ClearChats();
	}

	public static void Open(Transform root, bool isUp)
	{
		UIManagerControl.Instance.OpenUI("ChatTipUI", root, false, UIType.NonPush);
		ChatTipUIViewModel.Instance.ShowAsBottomCenter(isUp);
	}

	public static void Close()
	{
		if (ChatTipUIView.Instance != null)
		{
			ChatTipUIView.Instance.Show(false);
		}
	}

	public static void AddChat(ChatManager.ChatInfo chatInfo)
	{
		ChatManager.ChatInfo chatInfo2 = chatInfo;
		if (chatInfo.src_channel == 2 && chatInfo.sender_uid <= 0L)
		{
			for (int i = 0; i < chatInfo.items.get_Count(); i++)
			{
				DetailInfo detailInfo = chatInfo.items.get_Item(i);
				if (detailInfo.type == DetailType.DT.GuildQuestionNotice)
				{
					return;
				}
				if (detailInfo.type == DetailType.DT.GuildQuestion)
				{
					string[] array = chatInfo.chat_content.Split(new char[]
					{
						'|'
					});
					if (i == 0 && array.Length > 0)
					{
						chatInfo2 = new ChatManager.ChatInfo();
						chatInfo2.chat_type = chatInfo.chat_type;
						chatInfo2.sender_uid = chatInfo.sender_uid;
						chatInfo2.src_channel = chatInfo.src_channel;
						chatInfo2.sender_occupation = chatInfo.sender_occupation;
						chatInfo2.viplevel = chatInfo.viplevel;
						chatInfo2.time = chatInfo.time;
						chatInfo2.module = chatInfo.module;
						chatInfo2.items = chatInfo.items;
						chatInfo2.sender_name = string.Format("第{0}题", array[0]);
						chatInfo2.chat_content = array[1];
					}
				}
			}
		}
		if (ChatTipUIViewModel.chatNews.get_Count() >= 4)
		{
			ChatTipUIViewModel.chatNews.RemoveAt(0);
		}
		ChatTipUIViewModel.chatNews.Add(chatInfo2);
		if (ChatTipUIView.Instance != null && ChatTipUIView.Instance.get_gameObject() != null && ChatTipUIView.Instance.get_gameObject().get_activeInHierarchy())
		{
			ChatTipUIView.Instance.RefreshChats(ChatTipUIViewModel.chatNews);
		}
		else
		{
			ChatTipUIViewModel.IsNeedRefresh = true;
		}
	}
}
