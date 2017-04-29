using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultiInviteListUI : UIBase
{
	public ListPool pool;

	private ButtonInviteType currentInviteType;

	private List<MemberResume> m_friendList;

	private List<MemberResume> m_guildFriendList;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.FindTransform("friend").GetComponent<Toggle>().set_isOn(true);
	}

	protected override void OnEnable()
	{
		base.get_transform().SetAsLastSibling();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateCanInviteListByTeamBasic, new Callback(this.OnUpdateInviteListByTeamBasic));
		base.FindTransform("friend").GetComponent<Toggle>().onValueChanged.AddListener(new UnityAction<bool>(this.OnClickFriend));
		base.FindTransform("guild").GetComponent<Toggle>().onValueChanged.AddListener(new UnityAction<bool>(this.OnClickGuild));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateCanInviteListByTeamBasic, new Callback(this.OnUpdateInviteListByTeamBasic));
		base.FindTransform("friend").GetComponent<Toggle>().onValueChanged.RemoveListener(new UnityAction<bool>(this.OnClickFriend));
		base.FindTransform("guild").GetComponent<Toggle>().onValueChanged.RemoveListener(new UnityAction<bool>(this.OnClickGuild));
	}

	private void OnUpdateInviteListByTeamBasic()
	{
		this.m_friendList = TeamBasicManager.Instance.BuddyRoleList;
		this.m_guildFriendList = TeamBasicManager.Instance.GuildRoleList;
		this.InitUIData();
	}

	protected override void OnClickMaskAction()
	{
		this.OnClickCanle(null);
	}

	private void OnClickCanle(GameObject go)
	{
		UIManagerControl.Instance.UnLoadUIPrefab("InviteUI");
	}

	private void OnClickGuild(bool check)
	{
		if (check)
		{
			if (this.currentInviteType == ButtonInviteType.guild)
			{
				return;
			}
			this.currentInviteType = ButtonInviteType.guild;
			this.InitUIData();
		}
	}

	private void OnClickFriend(bool check)
	{
		if (check)
		{
			if (this.currentInviteType == ButtonInviteType.friend)
			{
				return;
			}
			this.currentInviteType = ButtonInviteType.friend;
			this.InitUIData();
		}
	}

	private void SetButton()
	{
		string text = "#687690";
		string text2 = "#ffffff";
		string text3 = string.Empty;
		string text4 = string.Empty;
		ButtonInviteType buttonInviteType = this.currentInviteType;
		if (buttonInviteType != ButtonInviteType.friend)
		{
			if (buttonInviteType == ButtonInviteType.guild)
			{
				text3 = text2;
				text4 = text;
			}
		}
		else
		{
			text4 = text2;
			text3 = text;
		}
		base.FindTransform("friendText").GetComponent<Text>().set_text(string.Format("<color={0}> 在线好友</color>", text4));
		base.FindTransform("guildText").GetComponent<Text>().set_text(string.Format("<color={0}> 公会成员</color>", text3));
	}

	public void InitUIData()
	{
		List<InviteData> list;
		if (this.currentInviteType == ButtonInviteType.friend)
		{
			list = TeamBasicManager.Instance.UpdateInviteList(this.m_friendList);
		}
		else
		{
			list = TeamBasicManager.Instance.UpdateInviteList(this.m_guildFriendList);
		}
		list.Sort((InviteData a, InviteData b) => b.role.fighting.CompareTo(a.role.fighting));
		Transform transform = base.FindTransform("Content").FindChild("Contair");
		int i;
		for (i = 0; i < list.get_Count(); i++)
		{
			if (transform.get_childCount() > i)
			{
				GameObject gameObject = transform.GetChild(i).get_gameObject();
				if (gameObject != null && gameObject.GetComponent<MultiInviteItem>() != null)
				{
					gameObject.GetComponent<MultiInviteItem>().UpdateRow(list.get_Item(i));
				}
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("MultiItem");
				instantiate2Prefab.set_name("MultiItem_" + i);
				MultiInviteItem component = instantiate2Prefab.GetComponent<MultiInviteItem>();
				instantiate2Prefab.get_transform().SetParent(transform);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				component.UpdateRow(list.get_Item(i));
			}
		}
		for (int j = i; j < transform.get_childCount(); j++)
		{
			GameObject gameObject2 = transform.GetChild(j).get_gameObject();
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		Transform transform2 = base.FindTransform("Tips");
		if (list.get_Count() > 0)
		{
			transform2.get_gameObject().SetActive(false);
		}
		else
		{
			transform2.get_gameObject().SetActive(true);
			transform2.GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(507002, false));
		}
	}
}
