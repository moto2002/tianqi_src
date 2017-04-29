using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildMemberItem : BaseUIBehaviour
{
	private MemberInfo myMemberInfo;

	private Image m_headIcon;

	private Gradient positionGradient;

	private Outline positionOutLine;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_headIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		base.FindTransform("ImageFrame").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHeadIcon);
		this.positionGradient = base.FindTransform("Position").GetComponent<Gradient>();
		this.positionOutLine = base.FindTransform("Position").GetComponent<Outline>();
	}

	public void RefreshUI(MemberInfo memberInfo)
	{
		this.myMemberInfo = memberInfo;
		if (this.myMemberInfo == null)
		{
			return;
		}
		ResourceManager.SetSprite(this.m_headIcon, UIUtils.GetRoleSmallIcon(memberInfo.career));
		string text = string.Empty;
		if (memberInfo.offlineSec >= 0)
		{
			ImageColorMgr.SetImageColor(this.m_headIcon, true);
			text = "<color=#7D7D7D>{0}</color>";
		}
		else
		{
			ImageColorMgr.SetImageColor(this.m_headIcon, false);
			text = "<color=#78503c>{0}</color>";
		}
		long fighting = memberInfo.fighting;
		int lv = memberInfo.lv;
		int num = memberInfo.vipLv;
		if (memberInfo.roleId == EntityWorld.Instance.EntSelf.ID)
		{
			fighting = EntityWorld.Instance.EntSelf.Fighting;
			lv = EntityWorld.Instance.EntSelf.Lv;
			num = EntityWorld.Instance.EntSelf.VipLv;
			if (!VIPManager.Instance.IsVIPPrivilegeOn())
			{
				num = 0;
			}
		}
		base.FindTransform("Position").GetComponent<Text>().set_text(string.Format(text, GuildManager.Instance.GetTitleName(memberInfo.title.get_Item(0))));
		base.FindTransform("Name").GetComponent<Text>().set_text(string.Format(text, memberInfo.name));
		this.SetTextEffect(memberInfo.title.get_Item(0));
		base.FindTransform("lv").GetComponent<Text>().set_text(string.Format(text, lv.ToString()));
		base.FindTransform("fighting").GetComponent<Text>().set_text(string.Format(text, fighting.ToString()));
		base.FindTransform("contribution").GetComponent<Text>().set_text(string.Format(text, memberInfo.fund.ToString()));
		base.FindTransform("VipLevel").GetComponent<Text>().set_text(string.Format(text, num.ToString()));
		base.FindTransform("Status").GetComponent<Text>().set_text(string.Format(text, GuildManager.Instance.GetOffLineStatus(memberInfo.offlineSec)));
	}

	private void OnClickHeadIcon(GameObject go)
	{
		if (this.myMemberInfo == null)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf.ID == this.myMemberInfo.roleId)
		{
			return;
		}
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		list.Add(PopButtonTabsManager.GetButtonData2Show(this.myMemberInfo.roleId, null));
		if (!FriendManager.Instance.IsRelationOfBuddy(this.myMemberInfo.roleId))
		{
			list.Add(PopButtonTabsManager.GetButtonData2AddFriend(this.myMemberInfo.roleId));
		}
		if (SystemOpenManager.IsSystemOn(59))
		{
			list.Add(PopButtonTabsManager.GetButtonData2TeamInvite(this.myMemberInfo.roleId));
		}
		if (GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AppointmenOfChairman) || GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AppointmenOfViceChairman) || GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AppointmenOfManager))
		{
			if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Chairman)
			{
				list.Add(PopButtonTabsManager.GetButtonData2GuildAppointment(this.myMemberInfo.roleId));
			}
			else if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.ViceChairman && this.myMemberInfo.title.get_Item(0) != MemberTitleType.MTT.Chairman)
			{
				list.Add(PopButtonTabsManager.GetButtonData2GuildAppointment(this.myMemberInfo.roleId));
			}
			else if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Manager && this.myMemberInfo.title.get_Item(0) != MemberTitleType.MTT.Chairman && this.myMemberInfo.title.get_Item(0) != MemberTitleType.MTT.ViceChairman)
			{
				list.Add(PopButtonTabsManager.GetButtonData2GuildAppointment(this.myMemberInfo.roleId));
			}
		}
		if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) != MemberTitleType.MTT.Normal)
		{
		}
		if (GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.AcceptOrRefuseMember))
		{
			if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Chairman)
			{
				list.Add(PopButtonTabsManager.GetButtonData2GuildKick(this.myMemberInfo.roleId, this.myMemberInfo.name));
			}
			else if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.ViceChairman && this.myMemberInfo.title.get_Item(0) != MemberTitleType.MTT.Chairman)
			{
				list.Add(PopButtonTabsManager.GetButtonData2GuildKick(this.myMemberInfo.roleId, this.myMemberInfo.name));
			}
			else if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Manager && this.myMemberInfo.title.get_Item(0) != MemberTitleType.MTT.Chairman && this.myMemberInfo.title.get_Item(0) != MemberTitleType.MTT.ViceChairman)
			{
				list.Add(PopButtonTabsManager.GetButtonData2GuildKick(this.myMemberInfo.roleId, this.myMemberInfo.name));
			}
		}
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIViewModel.Open(UINodesManager.MiddleUIRoot);
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	private void SetTextEffect(MemberTitleType.MTT title)
	{
		switch (title)
		{
		case MemberTitleType.MTT.Normal:
			this.positionGradient.topColor = new Color(0.9843137f, 0.8627451f, 0.58431375f);
			this.positionGradient.bottomColor = Color.get_white();
			this.positionOutLine.set_effectColor(new Color(0.6313726f, 0.466666669f, 0.3254902f));
			break;
		case MemberTitleType.MTT.Chairman:
			this.positionGradient.topColor = new Color(1f, 0.9529412f, 0.227450982f);
			this.positionGradient.bottomColor = new Color(1f, 0.980392158f, 0.435294122f);
			this.positionOutLine.set_effectColor(new Color(0.6431373f, 0.274509817f, 0.03137255f));
			break;
		case MemberTitleType.MTT.ViceChairman:
			this.positionGradient.topColor = new Color(1f, 0.7764706f, 0.235294119f);
			this.positionGradient.bottomColor = new Color(0.9411765f, 0.980392158f, 0f);
			this.positionOutLine.set_effectColor(new Color(0.627451f, 0.407843143f, 0.219607845f));
			break;
		case MemberTitleType.MTT.Manager:
			this.positionGradient.topColor = new Color(0.980392158f, 0.7882353f, 0.6f);
			this.positionGradient.bottomColor = new Color(1f, 0.8627451f, 0.223529413f);
			this.positionOutLine.set_effectColor(new Color(0.6039216f, 0.4392157f, 0.3019608f));
			break;
		default:
			this.positionGradient.topColor = new Color(0.9843137f, 0.8627451f, 0.58431375f);
			this.positionGradient.bottomColor = Color.get_white();
			this.positionOutLine.set_effectColor(new Color(0.6313726f, 0.466666669f, 0.3254902f));
			break;
		}
	}
}
