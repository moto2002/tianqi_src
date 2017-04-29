using System;
using System.Collections.Generic;

public class TeamChatGlobal
{
	public int ShowTeamChatTipTime;

	private static TeamChatGlobal instance;

	private List<ChatManager.ChatInfo> teamChatList;

	public bool IsInCD;

	private TimeCountDown timeCountDown;

	public static TeamChatGlobal Instance
	{
		get
		{
			if (TeamChatGlobal.instance == null)
			{
				TeamChatGlobal.instance = new TeamChatGlobal();
			}
			return TeamChatGlobal.instance;
		}
	}

	public List<ChatManager.ChatInfo> TeamChatList
	{
		get
		{
			return this.teamChatList;
		}
	}

	public void Init()
	{
		this.ShowTeamChatTipTime = 5;
		this.IsInCD = false;
		this.teamChatList = new List<ChatManager.ChatInfo>();
		this.AddListeners();
	}

	public void Release()
	{
		this.teamChatList = null;
		this.IsInCD = false;
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
		this.RemoveListeners();
	}

	private void AddListeners()
	{
		EventDispatcher.AddListener<ChatManager.ChatInfo>("ChatManager.TeamMessage", new Callback<ChatManager.ChatInfo>(this.AddToChatList));
	}

	private void RemoveListeners()
	{
		EventDispatcher.RemoveListener<ChatManager.ChatInfo>("ChatManager.TeamMessage", new Callback<ChatManager.ChatInfo>(this.AddToChatList));
	}

	public void AddToChatList(ChatManager.ChatInfo chatInfo)
	{
		if (this.teamChatList != null && chatInfo != null)
		{
			this.teamChatList.Add(chatInfo);
			this.HandleToShowFirstChat();
		}
	}

	private void HandleToShowFirstChat()
	{
		if (this.IsInCD)
		{
			return;
		}
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
		if (this.teamChatList == null || this.teamChatList.get_Count() <= 0)
		{
			return;
		}
		EventDispatcher.Broadcast<ChatManager.ChatInfo>(EventNames.UpdateTeamChatTip, this.teamChatList.get_Item(0));
		this.timeCountDown = new TimeCountDown(this.ShowTeamChatTipTime, TimeFormat.SECOND, delegate
		{
			this.IsInCD = true;
		}, delegate
		{
			this.IsInCD = false;
			this.RemoveFirstChat();
		}, true);
	}

	public void RemoveFirstChat()
	{
		if (this.teamChatList != null && this.teamChatList.get_Count() > 0)
		{
			this.teamChatList.RemoveAt(0);
			if (this.teamChatList.get_Count() > 0)
			{
				this.HandleToShowFirstChat();
			}
			else
			{
				EventDispatcher.Broadcast<ChatManager.ChatInfo>(EventNames.UpdateTeamChatTip, null);
			}
		}
		else
		{
			EventDispatcher.Broadcast<ChatManager.ChatInfo>(EventNames.UpdateTeamChatTip, null);
		}
	}
}
