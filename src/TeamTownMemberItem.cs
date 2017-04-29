using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamTownMemberItem : BaseUIBehaviour
{
	private Image m_headIcon;

	private Image m_spVIPLevel1;

	private Image m_spVIPLevel2;

	private Transform m_leaderIcon;

	private Transform m_popButtonRoot;

	private MemberResume m_memberResume;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_headIcon = base.FindTransform("HeadIcon").GetComponent<Image>();
		this.m_spVIPLevel1 = base.FindTransform("VIPLevel1").GetComponent<Image>();
		this.m_spVIPLevel2 = base.FindTransform("VIPLevel2").GetComponent<Image>();
		this.m_leaderIcon = base.FindTransform("leaderIcon");
		base.FindTransform("HeadIcon").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHeadIcon);
		this.m_popButtonRoot = base.FindTransform("PopButtonRoot");
		this.isInit = true;
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

	public void RefreshUI(MemberResume member)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.UpdateTeamChatTip(null);
		this.m_memberResume = member;
		if (this.m_headIcon != null)
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(DataReader<RoleCreate>.Get((int)member.career).modle);
			ResourceManager.SetSprite(this.m_headIcon, GameDataUtils.GetIcon(avatarModel.icon));
		}
		base.FindTransform("num").GetComponent<Text>().set_text(member.hp + string.Empty);
		base.FindTransform("NameLab").GetComponent<Text>().set_text(member.name);
		base.FindTransform("LevelLab").GetComponent<Text>().set_text("Lv." + member.level.ToString());
		ResourceManager.SetSprite(this.m_spVIPLevel1, GameDataUtils.GetNumIcon10(member.vipLv, NumType.Yellow_light));
		ResourceManager.SetSprite(this.m_spVIPLevel2, GameDataUtils.GetNumIcon1(member.vipLv, NumType.Yellow_light));
		if (member.roleId == TeamBasicManager.Instance.MyTeamData.LeaderID)
		{
			this.m_leaderIcon.get_gameObject().SetActive(true);
		}
		else
		{
			this.m_leaderIcon.get_gameObject().SetActive(false);
		}
		base.FindTransform("InFightingStatusIcon").get_gameObject().SetActive(member.inFighting);
	}

	private void OnClickHeadIcon(GameObject go)
	{
		if (this.m_memberResume == null)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf.ID == this.m_memberResume.roleId)
		{
			return;
		}
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		list.Add(PopButtonTabsManager.GetButtonData2Show(this.m_memberResume.roleId, null));
		if (!FriendManager.Instance.IsRelationOfBuddy(this.m_memberResume.roleId))
		{
			list.Add(PopButtonTabsManager.GetButtonData2AddFriend(this.m_memberResume.roleId));
		}
		if (TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			list.Add(PopButtonTabsManager.GetButtonData2ChangeLeader(this.m_memberResume.roleId, this.m_memberResume.name));
			list.Add(PopButtonTabsManager.GetButtonData2LeaveTeam(this.m_memberResume.roleId, this.m_memberResume.name));
		}
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIViewModel.Open(this.m_popButtonRoot);
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	private void UpdateTeamChatTip(ChatManager.ChatInfo chatInfo)
	{
		if (!base.get_gameObject().get_activeInHierarchy())
		{
			return;
		}
		Transform transform = base.FindTransform("TeamTalkToot");
		int num = 0;
		if (chatInfo != null && this.m_memberResume.roleId == chatInfo.sender_uid)
		{
			if (transform.get_childCount() > num)
			{
				transform.GetChild(num).get_gameObject().SetActive(true);
				ChatInfoBase component = transform.GetChild(num).GetComponent<ChatInfo2Bubble>();
				if (component != null)
				{
					component.Clear();
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
