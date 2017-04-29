using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMemberItem : BaseUIBehaviour
{
	private MemberResume m_memberResume;

	private Text roleNameText;

	private Text roleLvText;

	private Text roleFightingText;

	private Image leadIconImg;

	private Image headIconImg;

	private Image m_spVIPLevel1Img;

	private Image m_spVIPLevel2Img;

	private ButtonCustom quitBtn;

	private ButtonCustom changeLeaderBtn;

	private ButtonCustom kickOffBtnm;

	private GameObject noMemberObj;

	private GameObject haveMemberObj;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.noMemberObj = base.FindTransform("NoMember").get_gameObject();
		this.haveMemberObj = base.FindTransform("HaveMember").get_gameObject();
		this.roleNameText = base.FindTransform("HaveMember").FindChild("roleName").GetComponent<Text>();
		this.roleLvText = base.FindTransform("HaveMember").FindChild("roleLV").GetComponent<Text>();
		this.roleFightingText = base.FindTransform("HaveMember").FindChild("roleFighting").GetComponent<Text>();
		this.leadIconImg = base.FindTransform("HaveMember").FindChild("leaderIcon").GetComponent<Image>();
		this.headIconImg = base.FindTransform("HaveMember").FindChild("ImageIcon").GetComponent<Image>();
		this.m_spVIPLevel1Img = base.FindTransform("HaveMember").FindChild("roleVip").FindChild("VIPLevel1").GetComponent<Image>();
		this.m_spVIPLevel2Img = base.FindTransform("HaveMember").FindChild("roleVip").FindChild("VIPLevel2").GetComponent<Image>();
		base.FindTransform("HaveMember").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHaveMemberBtn);
		base.FindTransform("NoMember").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickNoMemberBtn);
		this.quitBtn = base.FindTransform("BtnQuit").GetComponent<ButtonCustom>();
		this.kickOffBtnm = base.FindTransform("BtnKickOff").GetComponent<ButtonCustom>();
		this.changeLeaderBtn = base.FindTransform("BtnChangeLeader").GetComponent<ButtonCustom>();
		this.quitBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuitBtn);
		this.kickOffBtnm.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickKickOffBtn);
		this.changeLeaderBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangeLeaderBtn);
		this.isInit = true;
	}

	public void RefreshUI(MemberResume memberResume)
	{
		this.m_memberResume = memberResume;
		if (!this.isInit)
		{
			this.InitUI();
		}
		bool flag = memberResume != null;
		if (this.noMemberObj.get_activeSelf() == flag)
		{
			this.noMemberObj.SetActive(!flag);
		}
		if (this.haveMemberObj.get_activeSelf() != flag)
		{
			this.haveMemberObj.SetActive(flag);
		}
		if (this.m_memberResume != null)
		{
			ResourceManager.SetSprite(this.headIconImg, UIUtils.GetRoleSmallIcon((int)memberResume.career));
			this.roleNameText.set_text(memberResume.name);
			this.roleLvText.set_text("Lv." + memberResume.level + string.Empty);
			this.roleFightingText.set_text(memberResume.fighting + string.Empty);
			if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.LeaderID == memberResume.roleId)
			{
				this.leadIconImg.set_enabled(true);
			}
			else
			{
				this.leadIconImg.set_enabled(false);
			}
			ResourceManager.SetSprite(this.m_spVIPLevel1Img, GameDataUtils.GetNumIcon10(memberResume.vipLv, NumType.Yellow_light));
			ResourceManager.SetSprite(this.m_spVIPLevel2Img, GameDataUtils.GetNumIcon10(memberResume.vipLv, NumType.Yellow_light));
		}
		this.SetButtonVisible();
	}

	private void SetButtonVisible()
	{
		if (this.m_memberResume != null && this.m_memberResume.roleId == EntityWorld.Instance.EntSelf.ID)
		{
			if (!this.quitBtn.get_gameObject().get_activeSelf())
			{
				this.quitBtn.get_gameObject().SetActive(true);
			}
			if (this.kickOffBtnm.get_gameObject().get_activeSelf())
			{
				this.kickOffBtnm.get_gameObject().SetActive(false);
			}
			if (this.changeLeaderBtn.get_gameObject().get_activeSelf())
			{
				this.changeLeaderBtn.get_gameObject().SetActive(false);
			}
		}
		else
		{
			if (this.quitBtn.get_gameObject().get_activeSelf())
			{
				this.quitBtn.get_gameObject().SetActive(false);
			}
			bool flag = TeamBasicManager.Instance.IsTeamLeader();
			if (this.changeLeaderBtn.get_gameObject().get_activeSelf() != flag)
			{
				this.changeLeaderBtn.get_gameObject().SetActive(flag);
			}
			if (this.kickOffBtnm.get_gameObject().get_activeSelf() != flag)
			{
				this.kickOffBtnm.get_gameObject().SetActive(flag);
			}
		}
	}

	private void OnClickHaveMemberBtn(GameObject go)
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
		list.Add(PopButtonTabsManager.GetButtonData2Show(this.m_memberResume.roleId, delegate
		{
			UIManagerControl.Instance.HideUI("TeamBasicUI");
		}));
		if (!FriendManager.Instance.IsRelationOfBuddy(this.m_memberResume.roleId))
		{
			list.Add(PopButtonTabsManager.GetButtonData2AddFriend(this.m_memberResume.roleId));
		}
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIViewModel.Open(UINodesManager.MiddleUIRoot);
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	private void OnClickNoMemberBtn(GameObject go)
	{
		if (this.m_memberResume != null)
		{
			return;
		}
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			TeamBasicManager.Instance.SendGetRoleResumeListReq();
			UIManagerControl.Instance.OpenUI("InviteUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
	}

	private void OnClickQuitBtn(GameObject go)
	{
		string chineseContent = GameDataUtils.GetChineseContent(516108, false);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), chineseContent, null, delegate
		{
			TeamBasicManager.Instance.SendPartnerLeaveTeamReq();
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickChangeLeaderBtn(GameObject go)
	{
		if (this.m_memberResume != null && TeamBasicManager.Instance.MyTeamData != null && EntityWorld.Instance.EntSelf != null && TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			string content = string.Format(GameDataUtils.GetChineseContent(516105, false), this.m_memberResume.name);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, null, delegate
			{
				TeamBasicManager.Instance.SendTeamChangeLeaderReq(this.m_memberResume.roleId);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
	}

	private void OnClickKickOffBtn(GameObject go)
	{
		if (this.m_memberResume != null && TeamBasicManager.Instance.MyTeamData != null && EntityWorld.Instance.EntSelf != null && TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			string content = string.Format(GameDataUtils.GetChineseContent(516104, false), this.m_memberResume.name);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, null, delegate
			{
				TeamBasicManager.Instance.SendKickoffMemberReq(this.m_memberResume.roleId);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
	}
}
