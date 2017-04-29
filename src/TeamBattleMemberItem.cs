using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TeamBattleMemberItem : BaseUIBehaviour
{
	protected Image TeamMemberHeadIcon;

	protected Text TeamMemberNameText;

	protected GameObject TeamMemberVIP;

	protected Image TeamMemberVIPLevel10;

	protected Image TeamMemberVIPLevel1;

	protected Text TeamMemberLevelText;

	protected Image TeamMemberBlood;

	protected Text TeamMemberBloodText;

	protected long id;

	public void Init()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TeamMemberHeadIcon = base.FindTransform("TeamMemberHeadIcon").GetComponent<Image>();
		this.TeamMemberNameText = base.FindTransform("TeamMemberNameText").GetComponent<Text>();
		this.TeamMemberVIP = base.FindTransform("TeamMemberVIP").get_gameObject();
		this.TeamMemberVIPLevel10 = base.FindTransform("TeamMemberVIPLevel10").GetComponent<Image>();
		this.TeamMemberVIPLevel1 = base.FindTransform("TeamMemberVIPLevel1").GetComponent<Image>();
		this.TeamMemberLevelText = base.FindTransform("TeamMemberLevelText").GetComponent<Text>();
		this.TeamMemberBlood = base.FindTransform("TeamMemberBlood").GetComponent<Image>();
		this.TeamMemberBloodText = base.FindTransform("TeamMemberBloodText").GetComponent<Text>();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<ChatManager.ChatInfo>(EventNames.UpdateTeamChatTip, new Callback<ChatManager.ChatInfo>(this.UpdateTeamChatTip));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<ChatManager.ChatInfo>(EventNames.UpdateTeamChatTip, new Callback<ChatManager.ChatInfo>(this.UpdateTeamChatTip));
	}

	public void SetPlayerID(long playerID)
	{
		this.id = playerID;
	}

	public void SetPlayerHead(int icon)
	{
		ResourceManager.SetSprite(this.TeamMemberHeadIcon, GameDataUtils.GetIcon(icon));
	}

	public void SetPlayerName(string name)
	{
		this.TeamMemberNameText.set_text(name);
	}

	public void SetPlayerVipLv(int vipLevel)
	{
		if (vipLevel == 0)
		{
			this.TeamMemberVIP.SetActive(false);
		}
		else
		{
			this.TeamMemberVIP.SetActive(true);
			ResourceManager.SetSprite(this.TeamMemberVIPLevel10, GameDataUtils.GetNumIcon10(vipLevel, NumType.Yellow_light));
			ResourceManager.SetSprite(this.TeamMemberVIPLevel1, GameDataUtils.GetNumIcon1(vipLevel, NumType.Yellow_light));
		}
	}

	public void SetPlayerLevel(int level)
	{
		this.TeamMemberLevelText.set_text(level.ToString());
	}

	public void SetPlayerHp(long curHp, long hpLmt)
	{
		if (hpLmt == 0L)
		{
			return;
		}
		if (curHp < 0L)
		{
			curHp = 0L;
		}
		this.TeamMemberBlood.set_fillAmount((float)((double)curHp / (double)hpLmt));
		this.TeamMemberBloodText.set_text(string.Format("{0}/{1}", curHp, hpLmt));
	}

	protected void UpdateTeamChatTip(ChatManager.ChatInfo chatInfo)
	{
		if (!base.get_gameObject().get_activeInHierarchy())
		{
			return;
		}
		Transform transform = base.FindTransform("TeamMemberTalkRoot");
		int num = 0;
		if (chatInfo != null && this.id == chatInfo.sender_uid)
		{
			if (transform.get_childCount() > num)
			{
				transform.GetChild(num).get_gameObject().SetActive(true);
				ChatInfoBase component = transform.GetChild(num).GetComponent<ChatInfo2Bubble>();
				if (component != null)
				{
					component.ShowInfo(chatInfo);
				}
			}
			else
			{
				GameObject chatInfo2Bubble = ChatManager.Instance.GetChatInfo2Bubble(chatInfo, transform);
				chatInfo2Bubble.set_name("chatInfoBubble");
				chatInfo2Bubble.get_transform().set_localPosition(Vector3.get_zero());
			}
			num++;
		}
		for (int i = num; i < transform.get_childCount(); i++)
		{
			GameObject gameObject = transform.GetChild(i).get_gameObject();
			gameObject.SetActive(false);
		}
	}
}
