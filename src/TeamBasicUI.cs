using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBasicUI : UIBase
{
	private Text addExpText;

	private Text teamLvText;

	private Text teamTargetText;

	private ListPool teamMemberListPool;

	private TeamBasicBtnState currentState;

	private ButtonCustom btnAutoJoin;

	private ButtonCustom goToDungeonBtn;

	private List<int> addExpCfgList;

	private Text noTeamTipText;

	private Text btnAutoJoinTeamTipText;

	private bool isAutoJoin;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.noTeamTipText = base.FindTransform("NoTeamTipText").GetComponent<Text>();
		this.addExpText = base.FindTransform("AddExpText").GetComponent<Text>();
		this.teamLvText = base.FindTransform("TeamLvText").GetComponent<Text>();
		this.teamTargetText = base.FindTransform("TeamTargetText").GetComponent<Text>();
		this.goToDungeonBtn = base.FindTransform("GoToDungeonBtn").GetComponent<ButtonCustom>();
		this.goToDungeonBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGoToDungeon);
		this.btnAutoJoin = base.FindTransform("BtnAutoJoinTeam").GetComponent<ButtonCustom>();
		this.btnAutoJoin.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnAutoJoin);
		base.FindTransform("TeamInfoBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTeamInofoBtn);
		base.FindTransform("ApplicationListBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickApplicationBtn);
		base.FindTransform("WorldRecruiteBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRecruitBtn);
		base.FindTransform("QuitBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuitBtn);
		base.FindTransform("BtnManager").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSettingBtn);
		base.FindTransform("ClearBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickClearBtn);
		base.FindTransform("RefreshBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefreshBtn);
		this.teamMemberListPool = base.FindTransform("TeamMemberList").GetComponent<ListPool>();
		this.teamMemberListPool.Clear();
		this.addExpCfgList = new List<int>();
		string value = DataReader<team>.Get("PeopleExp").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				','
			});
			int num = (int)float.Parse(array2[0]);
			int num2 = (int)float.Parse(array2[1]);
			this.addExpCfgList.Add(num2);
		}
		this.addExpText.set_text(string.Empty);
		this.ResetTeamAddExpImg();
		base.FindTransform("AddExpTipText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(516127, false));
		this.btnAutoJoinTeamTipText = base.FindTransform("BtnAutoJoinTeamTip").GetComponent<Text>();
		this.btnAutoJoinTeamTipText.set_text(GameDataUtils.GetChineseContent(50734, false));
		this.SetDungeonType(DungeonType.ENUM.Other);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.currentState = TeamBasicBtnState.TeamInfoBtn;
		this.SetBtnState();
		this.RefreshUI();
		this.UpadateAskForJoinList();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateTeamBasicInfo, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.LeaveTeamNty, new Callback(this.OnLeaveTeamNty));
		EventDispatcher.AddListener(EventNames.UpadateAskForJoinList, new Callback(this.RefreshApplicationUI));
		EventDispatcher.AddListener<int>(EventNames.UpdateWorldRecruiteCDOnSecond, new Callback<int>(this.UpdateWorldRecruitDownCount));
		EventDispatcher.AddListener(EventNames.UpadateAskForJoinList, new Callback(this.UpadateAskForJoinList));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateTeamBasicInfo, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.LeaveTeamNty, new Callback(this.OnLeaveTeamNty));
		EventDispatcher.RemoveListener(EventNames.UpadateAskForJoinList, new Callback(this.RefreshApplicationUI));
		EventDispatcher.RemoveListener<int>(EventNames.UpdateWorldRecruiteCDOnSecond, new Callback<int>(this.UpdateWorldRecruitDownCount));
		EventDispatcher.RemoveListener(EventNames.UpadateAskForJoinList, new Callback(this.UpadateAskForJoinList));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.teamMemberListPool.Clear();
	}

	private void OnClickBtnAutoJoin(GameObject go)
	{
		this.isAutoJoin = !this.isAutoJoin;
		this.btnAutoJoin.get_transform().FindChild("BtnAutoJoinSelect").get_gameObject().SetActive(this.isAutoJoin);
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.IsTeamLeader() && TeamBasicManager.Instance.MyTeamData.IsAutoAgree != this.isAutoJoin)
		{
			TeamBasicManager.Instance.SendAutoAgreedSettingReq(this.isAutoJoin);
		}
	}

	private void OnClickGoToDungeon(GameObject go)
	{
		TeamBasicManager.Instance.OnGoToDungeon();
	}

	private void OnClickTeamInofoBtn(GameObject go)
	{
		if (this.currentState == TeamBasicBtnState.TeamInfoBtn)
		{
			return;
		}
		this.currentState = TeamBasicBtnState.TeamInfoBtn;
		this.SetBtnState();
		this.RefreshUI();
	}

	private void OnClickApplicationBtn(GameObject go)
	{
		if (this.currentState == TeamBasicBtnState.TeamApplicationBtn)
		{
			return;
		}
		this.currentState = TeamBasicBtnState.TeamApplicationBtn;
		this.SetBtnState();
		this.RefreshApplicationUI();
	}

	private void OnClickRecruitBtn(GameObject go)
	{
		TeamBasicManager.Instance.SendInvitePartnerReq(0L);
	}

	private void OnClickSettingBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			TeamSettingUI teamSettingUI = UIManagerControl.Instance.OpenUI("TeamSettingUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TeamSettingUI;
			if (teamSettingUI != null)
			{
				teamSettingUI.get_transform().SetAsLastSibling();
			}
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

	private void OnClickClearBtn(GameObject go)
	{
		TeamBasicManager.Instance.ClearAskForJoinList();
		this.RefreshApplicationUI();
	}

	private void OnClickRefreshBtn(GameObject go)
	{
		this.RefreshApplicationUI();
	}

	public void SetDungeonType(DungeonType.ENUM type = DungeonType.ENUM.Other)
	{
		List<int> dungeonParams = (TeamBasicManager.Instance.MyTeamData == null) ? new List<int>() : TeamBasicManager.Instance.MyTeamData.ChallengeIDParams;
		DuiWuMuBiao teamTargetCfg = TeamBasicManager.Instance.GetTeamTargetCfg(type, dungeonParams);
		if (teamTargetCfg == null || teamTargetCfg.Button != 1 || !TeamBasicManager.Instance.IsTeamLeader())
		{
			if (this.goToDungeonBtn.get_gameObject().get_activeSelf())
			{
				this.goToDungeonBtn.get_gameObject().SetActive(false);
			}
		}
		else if (!this.goToDungeonBtn.get_gameObject().get_activeSelf())
		{
			this.goToDungeonBtn.get_gameObject().SetActive(true);
		}
	}

	private void RefreshUI()
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			bool btnVisible = EntityWorld.Instance.EntSelf.ID == TeamBasicManager.Instance.MyTeamData.LeaderID;
			this.SetBtnVisible(btnVisible);
			this.RefreshMemberList(TeamBasicManager.Instance.MyTeamData.TeamRoleList);
			this.RefreshTeamInfoText();
			this.RefreshTeamAddExp();
			this.SetTeamAutoJoin();
		}
	}

	private void RefreshApplicationUI()
	{
		if (TeamBasicManager.Instance.AskForJoinTeamList != null)
		{
			Transform transform = base.FindTransform("TeamAskForJoinList").FindChild("Contair");
			List<MemberResume> askForJoinTeamList = TeamBasicManager.Instance.AskForJoinTeamList;
			int i = 0;
			if (askForJoinTeamList != null && askForJoinTeamList.get_Count() > 0)
			{
				if (this.noTeamTipText != null && this.noTeamTipText.get_gameObject().get_activeSelf())
				{
					this.noTeamTipText.get_gameObject().SetActive(false);
				}
				while (i < askForJoinTeamList.get_Count())
				{
					if (transform.get_childCount() > i)
					{
						GameObject gameObject = transform.GetChild(i).get_gameObject();
						if (gameObject != null && gameObject.GetComponent<TeamApplicationItem>() != null)
						{
							gameObject.SetActive(true);
							gameObject.GetComponent<TeamApplicationItem>().RefreshUI(askForJoinTeamList.get_Item(i));
						}
					}
					else
					{
						GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("TeamApplicationItem");
						instantiate2Prefab.set_name("TeamApplicationItem_" + i);
						TeamApplicationItem component = instantiate2Prefab.GetComponent<TeamApplicationItem>();
						instantiate2Prefab.get_transform().SetParent(transform);
						instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
						component.RefreshUI(askForJoinTeamList.get_Item(i));
					}
					i++;
				}
			}
			else if (this.noTeamTipText != null && !this.noTeamTipText.get_gameObject().get_activeSelf())
			{
				this.noTeamTipText.get_gameObject().SetActive(true);
			}
			for (int j = i; j < transform.get_childCount(); j++)
			{
				GameObject gameObject2 = transform.GetChild(j).get_gameObject();
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
		}
	}

	private void RefreshTeamInfoText()
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			return;
		}
		this.teamTargetText.set_text(TeamBasicManager.Instance.GetTeamTargetName(TeamBasicManager.Instance.MyTeamData.TargetDungeonType, TeamBasicManager.Instance.MyTeamData.ChallengeIDParams));
		this.teamLvText.set_text(TeamBasicManager.Instance.MyTeamData.MinLV + "-" + TeamBasicManager.Instance.MyTeamData.MaxLV);
		this.SetDungeonType(TeamBasicManager.Instance.MyTeamData.TargetDungeonType);
	}

	private void RefreshMemberList(List<MemberResume> memberList)
	{
		this.teamMemberListPool.Clear();
		this.teamMemberListPool.Create(3, delegate(int index)
		{
			if (index < this.teamMemberListPool.Items.get_Count())
			{
				this.teamMemberListPool.Items.get_Item(index).get_gameObject().SetActive(true);
				TeamMemberItem component = this.teamMemberListPool.Items.get_Item(index).GetComponent<TeamMemberItem>();
				if (component != null)
				{
					if (memberList != null && index < memberList.get_Count())
					{
						component.RefreshUI(memberList.get_Item(index));
					}
					else
					{
						component.RefreshUI(null);
					}
				}
			}
		});
	}

	private void RefreshTeamAddExp()
	{
		this.addExpText.set_text(string.Empty);
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.TeamRoleList != null)
		{
			int num = TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Count();
			if (num > 0)
			{
				num = ((num <= this.addExpCfgList.get_Count()) ? num : this.addExpCfgList.get_Count());
				int num2 = this.addExpCfgList.get_Item(num - 1);
				this.addExpText.set_text(num2 + "%");
			}
			for (int i = 1; i <= num; i++)
			{
				Transform transform = base.FindTransform("ImageStar" + i + "_1");
				if (transform != null)
				{
					transform.GetComponent<Image>().set_enabled(true);
				}
			}
		}
	}

	private void ResetTeamAddExpImg()
	{
		int num = 1;
		while ((long)num <= 3L)
		{
			Transform transform = base.FindTransform("ImageStar" + num + "_1");
			if (transform != null)
			{
				transform.GetComponent<Image>().set_enabled(false);
			}
			num++;
		}
	}

	private void SetTeamAutoJoin()
	{
		this.isAutoJoin = !TeamBasicManager.Instance.MyTeamData.IsAutoAgree;
		this.OnClickBtnAutoJoin(this.btnAutoJoin.get_gameObject());
	}

	private void SetBtnVisible(bool isShow = false)
	{
		base.FindTransform("ApplicationListBtn").get_gameObject().SetActive(isShow);
		base.FindTransform("WorldRecruiteBtn").get_gameObject().SetActive(isShow);
		base.FindTransform("BtnManager").get_gameObject().SetActive(isShow);
		this.btnAutoJoin.get_gameObject().SetActive(isShow);
		this.btnAutoJoinTeamTipText.set_text((!isShow) ? string.Empty : GameDataUtils.GetChineseContent(50734, false));
	}

	private void SetBtnState()
	{
		if (this.currentState == TeamBasicBtnState.TeamInfoBtn)
		{
			this.SetBtnLightAndDim(base.FindTransform("TeamInfoBtn").get_gameObject(), "fenleianniu_1", true);
			this.SetBtnLightAndDim(base.FindTransform("ApplicationListBtn").get_gameObject(), "fenleianniu_2", false);
			base.FindTransform("TeamInfoPanel").get_gameObject().SetActive(true);
			base.FindTransform("TeamAskForJoinPanel").get_gameObject().SetActive(false);
		}
		else if (this.currentState == TeamBasicBtnState.TeamApplicationBtn)
		{
			this.SetBtnLightAndDim(base.FindTransform("ApplicationListBtn").get_gameObject(), "fenleianniu_1", true);
			this.SetBtnLightAndDim(base.FindTransform("TeamInfoBtn").get_gameObject(), "fenleianniu_2", false);
			base.FindTransform("TeamInfoPanel").get_gameObject().SetActive(false);
			base.FindTransform("TeamAskForJoinPanel").get_gameObject().SetActive(true);
		}
	}

	private void SetBtnLightAndDim(GameObject go, string btnIcon, bool isLight)
	{
		ResourceManager.SetSprite(go.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
		go.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? new Color(1f, 0.843137264f, 0.549019635f) : new Color(1f, 0.980392158f, 0.9019608f));
	}

	private void OnLeaveTeamNty()
	{
		this.OnDestroy();
	}

	private void UpdateWorldRecruitDownCount(int countDownSecond)
	{
		if (countDownSecond < 0)
		{
			base.FindTransform("WorldRecruiteBtn").FindChild("btnText").GetComponent<Text>().set_text("世界招募");
			base.FindTransform("WorldRecruiteBtn").GetComponent<ButtonCustom>().set_enabled(true);
		}
		else
		{
			base.FindTransform("WorldRecruiteBtn").FindChild("btnText").GetComponent<Text>().set_text(string.Format("<color=#ff0000>{0}</color>", countDownSecond));
			base.FindTransform("WorldRecruiteBtn").GetComponent<ButtonCustom>().set_enabled(false);
		}
	}

	private void UpadateAskForJoinList()
	{
		if (TeamBasicManager.Instance.AskForJoinTeamList == null)
		{
			base.FindTransform("ButtonOpenTeamBadge").get_gameObject().SetActive(false);
		}
		else
		{
			base.FindTransform("ButtonOpenTeamBadge").get_gameObject().SetActive(TeamBasicManager.Instance.AskForJoinTeamList.get_Count() > 0);
		}
	}
}
