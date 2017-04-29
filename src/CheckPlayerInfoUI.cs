using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlayerInfoUI : UIBase
{
	public static CheckPlayerInfoUI Instance;

	private Image m_spVIPLevel1;

	private Image m_spVIPLevel2;

	private void Start()
	{
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		CheckPlayerInfoUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spVIPLevel1 = base.FindTransform("VIPLevel1").GetComponent<Image>();
		this.m_spVIPLevel2 = base.FindTransform("VIPLevel2").GetComponent<Image>();
		base.FindTransform("BtnCheckInfo").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnCheckInfoClicked);
		base.FindTransform("BtnAddFriend").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnAddFriendClicked);
		base.FindTransform("BtnPrivateChat").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnPrivateChatClicked);
		base.FindTransform("BtnInvitationTeam").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnInvitationTeamClicked);
		base.FindTransform("BtnApplyTeam").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnApplyTeamClicked);
		base.FindTransform("BtnAddBlackList").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnAddBlackListClicked);
		if (CheckPlayerInfoManager.Instance.SelectEntityParent != null)
		{
			EntityParent selectEntityParent = CheckPlayerInfoManager.Instance.SelectEntityParent;
			Image component = base.FindTransform("HeadIcon").GetComponent<Image>();
			if (component != null)
			{
				ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(selectEntityParent.TypeID);
				if (zhuanZhiJiChuPeiZhi != null)
				{
					ResourceManager.SetSprite(component, GameDataUtils.GetIcon(zhuanZhiJiChuPeiZhi.jobPic1));
				}
			}
			base.FindTransform("IsOnline").GetComponent<Text>().set_text("在线");
			base.FindTransform("RoleName").GetComponent<Text>().set_text(selectEntityParent.Name);
			base.FindTransform("Lv").GetComponent<Text>().set_text("Lv." + selectEntityParent.Lv);
			base.FindTransform("RoleTypeName").GetComponent<Text>().set_text(UIUtils.GetRoleName(selectEntityParent.TypeID));
			base.FindTransform("FightValue").GetComponent<Text>().set_text(selectEntityParent.Fighting.ToString());
			base.FindTransform("TeamValue").GetComponent<Text>().set_text(selectEntityParent.GuildTitle.ToString());
			ResourceManager.SetSprite(this.m_spVIPLevel2, GameDataUtils.GetNumIcon1(selectEntityParent.VipLv, NumType.Yellow));
		}
		CheckPlayerInfoManager.Instance.IsAfterTeamCreateTeam = false;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void ActionClose()
	{
		base.ActionClose();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
	}

	private void OnClosePanel(GameObject go)
	{
		this.OnClickMaskAction();
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnCheckInfoClicked(GameObject go)
	{
		if (CheckPlayerInfoManager.Instance.SelectEntityParent != null)
		{
			this.OnClickMaskAction();
			FriendManager.Instance.SendFindBuddy(CheckPlayerInfoManager.Instance.SelectEntityParent.ID);
		}
	}

	private void OnAddFriendClicked(GameObject go)
	{
		if (!SystemOpenManager.IsSystemClickOpen(79, 0, true))
		{
			return;
		}
		if (CheckPlayerInfoManager.Instance.SelectEntityParent != null)
		{
			this.OnClickMaskAction();
			FriendManager.Instance.SendAddBuddy(CheckPlayerInfoManager.Instance.SelectEntityParent.ID, false);
		}
	}

	private void OnPrivateChatClicked(GameObject go)
	{
		if (FriendManager.Instance.IsRelationOfBlack(CheckPlayerInfoManager.Instance.SelectEntityParent.ID))
		{
			UIManagerControl.Instance.ShowToastText("该玩家已在黑名单");
			return;
		}
		if (CheckPlayerInfoManager.Instance.SelectEntityParent != null)
		{
			this.OnClickMaskAction();
			ChatManager.Instance.OpenChatUI2ChannelPrivate(CheckPlayerInfoManager.Instance.SelectEntityParent.ID, CheckPlayerInfoManager.Instance.SelectEntityParent.Name);
		}
	}

	private void OnInvitationTeamClicked(GameObject go)
	{
		if (!SystemOpenManager.IsSystemClickOpen(59, 0, true))
		{
			return;
		}
		this.invitePlayer();
	}

	protected void OnCreateTeamSuccess()
	{
		this.invitePlayer();
	}

	protected void OnLeaveTeamNty()
	{
	}

	private void invitePlayer()
	{
		bool isHaveTeam = TeamBasicManager.Instance.IsHaveTeam();
		bool isLeader = false;
		int teamMinLevel = 1;
		int teamMaxLevel = 1;
		bool isTeamFull;
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			isLeader = (TeamBasicManager.Instance.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID);
			isTeamFull = ((long)TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Count() >= 3L);
			teamMinLevel = TeamBasicManager.Instance.MyTeamData.MinLV;
			teamMaxLevel = TeamBasicManager.Instance.MyTeamData.MaxLV;
		}
		else
		{
			isTeamFull = false;
		}
		CheckPlayerInfoManager.Instance.invitePlayer(isHaveTeam, isLeader, isTeamFull, teamMinLevel, teamMaxLevel, delegate
		{
			this.OnClickMaskAction();
		});
	}

	private void OnApplyTeamClicked(GameObject go)
	{
		if (!SystemOpenManager.IsSystemClickOpen(45, 0, true))
		{
			return;
		}
		if (CheckPlayerInfoManager.Instance.SelectEntityParent != null)
		{
			this.OnClickMaskAction();
			GuildManager.Instance.SendJoinInToGuildByRoleID(CheckPlayerInfoManager.Instance.SelectEntityParent.ID);
		}
	}

	private void OnAddBlackListClicked(GameObject go)
	{
		if (CheckPlayerInfoManager.Instance.SelectEntityParent != null)
		{
			if (FriendManager.Instance.IsRelationOfBlack(CheckPlayerInfoManager.Instance.SelectEntityParent.ID))
			{
				UIManagerControl.Instance.ShowToastText("该玩家已在黑名单");
				return;
			}
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("系统提示", GameDataUtils.GetChineseContent(502057, false), delegate
			{
			}, delegate
			{
				this.OnClickMaskAction();
				FriendManager.Instance.SendMoveToBlackList(CheckPlayerInfoManager.Instance.SelectEntityParent.ID);
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(502019, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
	}
}
