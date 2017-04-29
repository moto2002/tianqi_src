using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamTownUI : BaseUIBehaviour
{
	private Transform quitBtnTrans;

	private List<Transform> teamMemberTransList;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.quitBtnTrans = base.FindTransform("quitBtn");
		base.FindTransform("CreateTeamBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCreateTeamBtn);
		base.FindTransform("FindTeamBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFindTeamBtn);
		base.FindTransform("friendRecruitBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFriendRecruitBtn);
		base.FindTransform("worldRecruitBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickWorldRecruitBtn);
		base.FindTransform("quitBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuitBtn);
		this.teamMemberTransList = new List<Transform>();
		for (int i = 1; i < 3; i++)
		{
			Transform transform = base.FindTransform("Member" + i);
			if (transform != null)
			{
				this.teamMemberTransList.Add(transform);
			}
		}
	}

	private void OnEnable()
	{
		this.RefreshUI();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateTeamBasicInfo, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.LeaveTeamNty, new Callback(this.RefreshUI));
		EventDispatcher.AddListener<int>(EventNames.UpdateWorldRecruiteCDOnSecond, new Callback<int>(this.UpdateWorldRecruitDownCount));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateTeamBasicInfo, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.LeaveTeamNty, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener<int>(EventNames.UpdateWorldRecruiteCDOnSecond, new Callback<int>(this.UpdateWorldRecruitDownCount));
	}

	private void RefreshUI()
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			base.FindTransform("NoTeamRoot").get_gameObject().SetActive(true);
			base.FindTransform("HaveTeamRoot").get_gameObject().SetActive(false);
		}
		else
		{
			base.FindTransform("NoTeamRoot").get_gameObject().SetActive(false);
			base.FindTransform("HaveTeamRoot").get_gameObject().SetActive(true);
			bool panelVisible = TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Count() >= 2;
			this.SetPanelVisible(panelVisible);
			bool quitBtnPos = TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Count() != 2;
			this.SetQuitBtnPos(quitBtnPos);
		}
	}

	private void SetPanelVisible(bool isHave = false)
	{
		if (!isHave)
		{
			this.UpdateWorldRecruitDownCount(-1);
		}
		base.FindTransform("NoMemberRoot").get_gameObject().SetActive(!isHave);
		base.FindTransform("HaveMemberRoot").get_gameObject().SetActive(isHave);
		if (isHave)
		{
			int i = 0;
			int num = 0;
			while (i < TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Count())
			{
				MemberResume memberResume = TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Item(i);
				if (memberResume.roleId != EntityWorld.Instance.EntSelf.ID)
				{
					if (num < this.teamMemberTransList.get_Count())
					{
						this.teamMemberTransList.get_Item(num).get_gameObject().SetActive(true);
						this.teamMemberTransList.get_Item(num).GetComponent<TeamTownMemberItem>().RefreshUI(memberResume);
					}
					num++;
				}
				i++;
			}
			for (int j = num; j < this.teamMemberTransList.get_Count(); j++)
			{
				this.teamMemberTransList.get_Item(j).get_gameObject().SetActive(false);
			}
		}
	}

	private void SetQuitBtnPos(bool isDown = true)
	{
		if (this.quitBtnTrans != null)
		{
			int num = (!isDown) ? -73 : -158;
			this.quitBtnTrans.set_localPosition(new Vector3(this.quitBtnTrans.get_localPosition().x, (float)num, 0f));
		}
	}

	private void OnClickCreateTeamBtn(GameObject go)
	{
		TeamBasicManager.Instance.SendOrganizeTeamReq(DungeonType.ENUM.Other, null, 0);
	}

	private void OnClickFindTeamBtn(GameObject go)
	{
		TeamBasicManager.Instance.OpenSeekTeamUI(DungeonType.ENUM.Other, 0, null);
	}

	private void OnClickWorldRecruitBtn(GameObject go)
	{
		TeamBasicManager.Instance.SendInvitePartnerReq(0L);
	}

	private void OnClickQuitBtn(GameObject go)
	{
		string chineseContent = GameDataUtils.GetChineseContent(516108, false);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), chineseContent, null, delegate
		{
			TeamBasicManager.Instance.SendPartnerLeaveTeamReq();
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void UpdateWorldRecruitDownCount(int countDownSecond)
	{
		if (countDownSecond < 0)
		{
			base.FindTransform("worldRecruitBtn").FindChild("Text").GetComponent<Text>().set_text("世界招募");
			base.FindTransform("worldRecruitBtn").GetComponent<ButtonCustom>().set_enabled(true);
		}
		else
		{
			base.FindTransform("worldRecruitBtn").FindChild("Text").GetComponent<Text>().set_text(string.Format("<color=#ff0000>{0}</color>", countDownSecond));
			base.FindTransform("worldRecruitBtn").GetComponent<ButtonCustom>().set_enabled(false);
		}
	}

	private void OnClickFriendRecruitBtn(GameObject go)
	{
		TeamBasicManager.Instance.SendGetRoleResumeListReq();
		UIManagerControl.Instance.OpenUI("InviteUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}
}
