using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatChannelView : BaseUIBehaviour
{
	private RectTransform m_rChannelChats;

	private Transform m_rChannelChatsOffset;

	private Transform m_rBtnlock;

	private Text m_lblBtnlock;

	private static float Offset;

	private float _VIEW_HEIGHT;

	[SetProperty("Islock"), SerializeField]
	private bool islock;

	private int newNum;

	private List<ChatInfoBase> m_listChats = new List<ChatInfoBase>();

	private float VIEW_HEIGHT
	{
		get
		{
			if (this._VIEW_HEIGHT <= 100f)
			{
				RectTransform rectTransform = base.FindTransform("ChannelChatsScroll") as RectTransform;
				this._VIEW_HEIGHT = rectTransform.get_rect().get_size().y;
			}
			if (this._VIEW_HEIGHT <= 100f)
			{
				return 500f;
			}
			return this._VIEW_HEIGHT;
		}
	}

	public bool Islock
	{
		get
		{
			return this.islock;
		}
		set
		{
			this.islock = value;
		}
	}

	public int NewNum
	{
		get
		{
			return this.newNum;
		}
		set
		{
			this.newNum = value;
			this.m_lblBtnlock.set_text(string.Format("你有{0}条新消息", this.newNum));
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.None, false);
	}

	private void OnEnable()
	{
		if (MainTaskManager.Instance.AutoTaskId > 0 && EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null)
		{
			MainTaskManager.Instance.StopToNPC(true);
		}
	}

	private void OnDisable()
	{
		ChatManager.Instance.VoiceReqReset();
		VoiceSDKManager.Instance.SpeechStopPlayWithResetMusic();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_rChannelChats = (base.FindTransform("ChannelChats") as RectTransform);
		this.m_rChannelChatsOffset = base.FindTransform("ChannelChatsOffset");
		this.m_rChannelChatsOffset.set_localPosition(new Vector3(0f, ChatChannelView.Offset, 0f));
		this.m_rBtnlock = base.FindTransform("Btnlock");
		this.m_lblBtnlock = base.FindTransform("BtnlockText").GetComponent<Text>();
		this.Islock = false;
		EventTriggerListener.Get(base.FindTransform("Btnlock").get_gameObject()).onClick = new EventTriggerListener.VoidDelegateGameObject(this.OnBtnlockClick);
		EventTriggerListener.Get(base.FindTransform("ChannelChatsScroll").get_gameObject()).onDrag = new EventTriggerListener.VoidDelegateData(this.OnDrag);
	}

	private void OnBtnlockClick(GameObject sender)
	{
		this.NewNum = 0;
		this.Islock = false;
		this.ShowLock(false);
		this.SetContentPos();
	}

	private void OnDrag(PointerEventData eventData)
	{
		if (this.IsMoreOnePage() && !this.IsOnTheBottom())
		{
			this.Islock = true;
		}
		else
		{
			this.Islock = false;
			this.ShowLock(false);
		}
	}

	public void AddChat2Channel(ChatManager.ChatInfo chatInfo)
	{
		this.CheckNums();
		ChatInfoBase chatInfoBase = ChatManager.CreatePrefab2ChannelChatInfo("_ChatItem" + this.m_listChats.get_Count());
		UGUITools.ResetTransform(chatInfoBase.get_transform(), this.m_rChannelChatsOffset);
		chatInfoBase.get_transform().set_localPosition(new Vector3(0f, this.CalHeight4Chats(this.m_listChats.get_Count()), 0f));
		chatInfoBase.ShowInfo(chatInfo);
		this.m_listChats.Add(chatInfoBase);
		this.m_rChannelChats.set_sizeDelta(new Vector2(0f, Mathf.Abs(this.CalHeight4Chats(this.m_listChats.get_Count()))));
		this.SetContentPos();
		if (chatInfo.sender_uid == EntityWorld.Instance.EntSelf.ID)
		{
			this.OnBtnlockClick(null);
		}
		else if (this.Islock)
		{
			this.NewNum++;
			this.ShowLock(true);
		}
	}

	public void ClearAll()
	{
		for (int i = 0; i < this.m_listChats.get_Count(); i++)
		{
			ChatManager.Reuse2ChannelChatInfoPool(this.m_listChats.get_Item(i));
		}
		this.m_listChats.Clear();
	}

	private void CheckNums()
	{
		if (this.m_listChats.get_Count() > 0 && this.m_listChats.get_Count() >= ChatManager.MAX_CHAT_SHOWNUM)
		{
			ChatManager.Reuse2ChannelChatInfoPool(this.m_listChats.get_Item(0));
			this.m_listChats.RemoveAt(0);
			this.ResetChatsPos();
		}
	}

	private void ResetChatsPos()
	{
		for (int i = 0; i < this.m_listChats.get_Count(); i++)
		{
			ChatInfoBase chatInfoBase = this.m_listChats.get_Item(i);
			chatInfoBase.get_transform().set_localPosition(new Vector3(0f, this.CalHeight4Chats(i), 0f));
		}
	}

	private void SetContentPos()
	{
		if (!this.Islock && this.IsMoreOnePage())
		{
			this.m_rChannelChats.set_anchoredPosition(new Vector2(this.m_rChannelChats.get_anchoredPosition().x, this.m_rChannelChats.get_sizeDelta().y - this.VIEW_HEIGHT));
		}
	}

	private bool IsMoreOnePage()
	{
		return this.m_rChannelChats.get_sizeDelta().y > this.VIEW_HEIGHT;
	}

	private bool IsOnTheBottom()
	{
		return !this.IsMoreOnePage() || this.m_rChannelChats.get_anchoredPosition().y >= this.m_rChannelChats.get_sizeDelta().y - this.VIEW_HEIGHT - 20f;
	}

	private void ShowLock(bool isShow)
	{
		this.m_rBtnlock.get_gameObject().SetActive(isShow);
	}

	private float CalHeight4Chats(int num)
	{
		float num2 = 0f;
		int num3 = 0;
		while (num3 < this.m_listChats.get_Count() && num3 < num)
		{
			num2 += this.m_listChats.get_Item(num3).m_height;
			num3++;
		}
		return num2;
	}
}
