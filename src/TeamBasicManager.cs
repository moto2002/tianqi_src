using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class TeamBasicManager : BaseSubSystemManager
{
	public const uint TeamMemberFullNum = 3u;

	private TeamData myTeamData;

	private List<MemberResume> buddyRoleList;

	private List<MemberResume> guildRoleList;

	private List<TeamBaseInfo> queryTeamList;

	private List<MemberResume> askForJoinTeamList;

	private List<BeInviteData> beInviteList;

	public int CdTime;

	public DungeonType.ENUM CreateTeamDungeonType;

	private static TeamBasicManager instance;

	public int NPage;

	public bool CanNotRequire;

	public List<TeamMemberStartFightReplyState> teamMemberStartFightReplyStateList;

	private bool isAutoAgree;

	private bool isAgree;

	private TimeCountDown fightInviteCoundDown;

	public List<InviteCdTimeData> inviteRoleId = new List<InviteCdTimeData>();

	public bool IsInCD;

	private TimeCountDown timeCoundDown;

	private List<InviteCdTimeData> joinTeamCDTimeList = new List<InviteCdTimeData>();

	public TeamData MyTeamData
	{
		get
		{
			return this.myTeamData;
		}
	}

	public List<MemberResume> BuddyRoleList
	{
		get
		{
			return this.buddyRoleList;
		}
	}

	public List<MemberResume> GuildRoleList
	{
		get
		{
			return this.guildRoleList;
		}
	}

	public List<TeamBaseInfo> QueryTeamList
	{
		get
		{
			return this.queryTeamList;
		}
		set
		{
			this.queryTeamList = value;
		}
	}

	public List<MemberResume> AskForJoinTeamList
	{
		get
		{
			return this.askForJoinTeamList;
		}
		set
		{
			this.askForJoinTeamList = value;
		}
	}

	public List<BeInviteData> BeInviteList
	{
		get
		{
			if (this.beInviteList == null)
			{
				this.beInviteList = new List<BeInviteData>();
			}
			return this.beInviteList;
		}
	}

	public static TeamBasicManager Instance
	{
		get
		{
			if (TeamBasicManager.instance == null)
			{
				TeamBasicManager.instance = new TeamBasicManager();
			}
			return TeamBasicManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
		this.CdTime = (int)float.Parse(DataReader<team>.Get("cd_invite").value);
		this.CdTime /= 1000;
		TeamChatGlobal.Instance.Init();
	}

	public override void Release()
	{
		this.ClearTeamData();
		TeamChatGlobal.Instance.Release();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<OrganizeTeamRes>(new NetCallBackMethod<OrganizeTeamRes>(this.OnOrganizeTeamRes));
		NetworkManager.AddListenEvent<TeamSettingRes>(new NetCallBackMethod<TeamSettingRes>(this.OnTeamSettingRes));
		NetworkManager.AddListenEvent<TeamSettingNty>(new NetCallBackMethod<TeamSettingNty>(this.OnTeamSettingNty));
		NetworkManager.AddListenEvent<TeamChangeLeaderRes>(new NetCallBackMethod<TeamChangeLeaderRes>(this.OnTeamChangeLeaderRes));
		NetworkManager.AddListenEvent<GetRoleResumeListRes>(new NetCallBackMethod<GetRoleResumeListRes>(this.OnGetRoleResumeListRes));
		NetworkManager.AddListenEvent<InvitePartnerRes>(new NetCallBackMethod<InvitePartnerRes>(this.OnInvitePartnerRes));
		NetworkManager.AddListenEvent<PartnerBeInviteRoleNty>(new NetCallBackMethod<PartnerBeInviteRoleNty>(this.OnPartnerBeInviteRoleNty));
		NetworkManager.AddListenEvent<PartnerAnswerRes>(new NetCallBackMethod<PartnerAnswerRes>(this.OnPartnerAnswerRes));
		NetworkManager.AddListenEvent<TeamPartnerRefreshNty>(new NetCallBackMethod<TeamPartnerRefreshNty>(this.OnTeamPartnerRefreshNty));
		NetworkManager.AddListenEvent<FindTeamInfoRes>(new NetCallBackMethod<FindTeamInfoRes>(this.OnFindTeamInfoRes));
		NetworkManager.AddListenEvent<AppointJoinTeamInfoRes>(new NetCallBackMethod<AppointJoinTeamInfoRes>(this.OnAppointJoinTeamInfoRes));
		NetworkManager.AddListenEvent<PartnerLeaveTeamRes>(new NetCallBackMethod<PartnerLeaveTeamRes>(this.OnPartnerLeaveTeamRes));
		NetworkManager.AddListenEvent<TeamChangeLeaderNty>(new NetCallBackMethod<TeamChangeLeaderNty>(this.OnTeamChangeLeaderNty));
		NetworkManager.AddListenEvent<KickoffMemberRes>(new NetCallBackMethod<KickoffMemberRes>(this.OnKickoffMemberRes));
		NetworkManager.AddListenEvent<KickoffNty>(new NetCallBackMethod<KickoffNty>(this.OnKickoffNty));
		NetworkManager.AddListenEvent<TeamStartFightAnswerNty>(new NetCallBackMethod<TeamStartFightAnswerNty>(this.OnTeamStartFightAnswerNty));
		NetworkManager.AddListenEvent<TeamStartFightAnswerRes>(new NetCallBackMethod<TeamStartFightAnswerRes>(this.OnTeamStartFightAnswerRes));
		NetworkManager.AddListenEvent<TeamStartFightReply2AllNty>(new NetCallBackMethod<TeamStartFightReply2AllNty>(this.OnTeamStartFightReply2AllNty));
		NetworkManager.AddListenEvent<AppointJoinTeamInfoNty>(new NetCallBackMethod<AppointJoinTeamInfoNty>(this.OnAppointJoinTeamInfoNty));
		NetworkManager.AddListenEvent<JoinTeamNty>(new NetCallBackMethod<JoinTeamNty>(this.OnJoinTeamNty));
		NetworkManager.AddListenEvent<LeaderProcessAppointRes>(new NetCallBackMethod<LeaderProcessAppointRes>(this.OnLeaderProcessAppointRes));
		NetworkManager.AddListenEvent<PartnerRejectNty>(new NetCallBackMethod<PartnerRejectNty>(this.OnPartnerRejectNty));
		NetworkManager.AddListenEvent<TeamAutoMatchFailedNty>(new NetCallBackMethod<TeamAutoMatchFailedNty>(this.OnTeamAutoMatchFailedNty));
		NetworkManager.AddListenEvent<TeamMemFightReqConfirmNty>(new NetCallBackMethod<TeamMemFightReqConfirmNty>(this.OnTeamMemFightReqConfirmNty));
		NetworkManager.AddListenEvent<AutoAgreedSettingRes>(new NetCallBackMethod<AutoAgreedSettingRes>(this.OnAutoAgreedSettingRes));
		NetworkManager.AddListenEvent<QuickEnterTeamRes>(new NetCallBackMethod<QuickEnterTeamRes>(this.OnQuickEnterTeamRes));
	}

	private void OnOrganizeTeamRes(short state, OrganizeTeamRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516101, false));
			long iD = EntityWorld.Instance.EntSelf.ID;
			this.myTeamData = new TeamData((long)down.teamId, iD);
			this.myTeamData.TeamName = down.teamName;
			this.myTeamData.MinLV = down.minLv;
			this.myTeamData.MaxLV = down.maxLv;
			this.myTeamData.IsAutoAgree = down.autoAgreed;
			if (down.dungeonInfo != null)
			{
				this.myTeamData.TargetDungeonType = down.dungeonInfo.dungeonType;
				this.myTeamData.ChallengeIDParams = down.dungeonInfo.dungeonParams;
			}
			else
			{
				this.myTeamData.TargetDungeonType = DungeonType.ENUM.Other;
				this.myTeamData.ChallengeIDParams = new List<int>();
			}
			MemberResume memberResume = new MemberResume();
			memberResume.roleId = EntityWorld.Instance.EntSelf.ID;
			memberResume.career = (CareerType.CT)EntityWorld.Instance.EntSelf.TypeID;
			memberResume.fighting = EntityWorld.Instance.EntSelf.Fighting;
			memberResume.inFighting = EntityWorld.Instance.EntSelf.IsInBattle;
			memberResume.level = EntityWorld.Instance.EntSelf.Lv;
			memberResume.vipLv = EntityWorld.Instance.EntSelf.VipLv;
			memberResume.name = EntityWorld.Instance.EntSelf.Name;
			this.myTeamData.Add2RoleMemberList(memberResume);
			EventDispatcher.Broadcast(EventNames.CreateTeamSuccess);
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
			if (!UIManagerControl.Instance.IsOpen("TeamBasicUI"))
			{
				TeamBasicUI teamBasicUI = UIManagerControl.Instance.OpenUI("TeamBasicUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TeamBasicUI;
				teamBasicUI.get_transform().SetAsLastSibling();
			}
		}
	}

	private void OnTeamSettingRes(short state, TeamSettingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnTeamSettingNty(short state, TeamSettingNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.myTeamData != null)
		{
			if (this.myTeamData.TeamID != (long)down.teamId)
			{
				return;
			}
			this.myTeamData.MinLV = down.minLv;
			this.myTeamData.MaxLV = down.maxLv;
			this.myTeamData.TeamName = down.teamName;
			this.myTeamData.LeaderID = down.leaderId;
			this.myTeamData.TargetDungeonType = down.dungeonInfo.dungeonType;
			this.myTeamData.ChallengeIDParams = down.dungeonInfo.dungeonParams;
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
		}
	}

	private void OnTeamChangeLeaderRes(short state, TeamChangeLeaderRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.myTeamData != null)
		{
			this.myTeamData.LeaderID = down.roleId;
			if (this.myTeamData.LeaderID != EntityWorld.Instance.EntSelf.ID && this.askForJoinTeamList != null)
			{
				this.askForJoinTeamList.Clear();
				EventDispatcher.Broadcast(EventNames.UpadateAskForJoinList);
			}
			this.myTeamData.PutTeamLeaderToFirst();
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
		}
	}

	private void OnGetRoleResumeListRes(short state, GetRoleResumeListRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			List<MemberResume> list = new List<MemberResume>();
			List<MemberResume> list2 = new List<MemberResume>();
			if (this.MyTeamData == null || this.MyTeamData.TeamRoleList == null)
			{
				return;
			}
			if (down.buddyRoleList != null)
			{
				for (int i = 0; i < down.buddyRoleList.get_Count(); i++)
				{
					long roleID = down.buddyRoleList.get_Item(i).roleId;
					int num = this.MyTeamData.TeamRoleList.FindIndex((MemberResume a) => a.roleId == roleID);
					if (num < 0)
					{
						list.Add(down.buddyRoleList.get_Item(i));
					}
				}
			}
			if (down.guildRoleList != null)
			{
				for (int j = 0; j < down.guildRoleList.get_Count(); j++)
				{
					long roleID = down.guildRoleList.get_Item(j).roleId;
					int num2 = this.MyTeamData.TeamRoleList.FindIndex((MemberResume a) => a.roleId == roleID);
					if (num2 < 0)
					{
						list2.Add(down.guildRoleList.get_Item(j));
					}
				}
			}
			this.buddyRoleList = list;
			this.guildRoleList = list2;
			EventDispatcher.Broadcast(EventNames.UpdateCanInviteListByTeamBasic);
		}
	}

	private void OnInvitePartnerRes(short state, InvitePartnerRes down = null)
	{
		if ((int)state == Status.TEAM_LV_HAS_NOT_FIT)
		{
			string chineseContent = GameDataUtils.GetChineseContent(516114, false);
			UIManagerControl.Instance.ShowToastText(chineseContent);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.roleId == 0L)
			{
				this.HandeWorldRecruitCoundDown(down.cdTime);
			}
			else
			{
				InviteRoleRes inviteRoleRes = new InviteRoleRes();
				inviteRoleRes.roleId = down.roleId;
				inviteRoleRes.cdTime = down.cdTime;
				EventDispatcher.Broadcast<InviteRoleRes>(EventNames.MultiInviteItemUpdate, inviteRoleRes);
			}
		}
	}

	private void OnPartnerBeInviteRoleNty(short state, PartnerBeInviteRoleNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.beInviteList == null)
			{
				this.beInviteList = new List<BeInviteData>();
			}
			BeInviteData beInviteData = new BeInviteData((long)down.teamId, down.teamName);
			beInviteData.TeamMemberResume = down.inviteResume;
			this.beInviteList.Add(beInviteData);
			EventDispatcher.Broadcast(EventNames.UpdateBeInvitedCount);
		}
	}

	private void OnPartnerAnswerRes(short state, PartnerAnswerRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
		}
	}

	private void OnTeamPartnerRefreshNty(short state, TeamPartnerRefreshNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.myTeamData != null)
		{
			if (down.refreshType == TeamRefreshType.ENUM.Enter)
			{
				this.myTeamData.Add2RoleMemberList(down.memberResume);
			}
			else if (down.refreshType == TeamRefreshType.ENUM.Leave)
			{
				UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(50738, false), down.memberResume.name));
				this.myTeamData.RemoveRoleMemberByID(down.memberResume.roleId);
			}
			else if (down.refreshType == TeamRefreshType.ENUM.Update)
			{
				this.myTeamData.Update2RoleMemberList(down.memberResume);
			}
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
		}
	}

	private void OnFindTeamInfoRes(short state, FindTeamInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.NPage = down.nPage;
			this.CanNotRequire = (down.teamInfo.get_Count() < 10);
			if (this.queryTeamList == null)
			{
				this.queryTeamList = new List<TeamBaseInfo>();
				this.queryTeamList = down.teamInfo;
			}
			else
			{
				int i;
				for (i = 0; i < down.teamInfo.get_Count(); i++)
				{
					int num = this.queryTeamList.FindIndex((TeamBaseInfo a) => a.teamId == down.teamInfo.get_Item(i).teamId);
					if (num < 0)
					{
						this.queryTeamList.Add(down.teamInfo.get_Item(i));
					}
				}
			}
			EventDispatcher.Broadcast(EventNames.UpdateQueryTeamBaseInfoList);
		}
	}

	private void OnAppointJoinTeamInfoRes(short state, AppointJoinTeamInfoRes down = null)
	{
		if ((int)state == Status.TEAM_LV_HAS_NOT_FIT)
		{
			string chineseContent = GameDataUtils.GetChineseContent(516103, false);
			UIManagerControl.Instance.ShowToastText(chineseContent);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502058, false));
			int cdTime = down.cdTime;
			if (down.cdTime <= 0)
			{
				cdTime = this.CdTime;
			}
			TeamBasicManager.Instance.AddJoinTeamTimer(down.teamId, cdTime);
			EventDispatcher.Broadcast<int, int>(EventNames.UpdateJoinInTeamCoolDown, down.teamId, cdTime);
		}
	}

	private void OnPartnerLeaveTeamRes(short state, PartnerLeaveTeamRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.myTeamData != null && (long)down.teamId == this.myTeamData.TeamID)
		{
			this.ClearTeamData();
			EventDispatcher.Broadcast(EventNames.LeaveTeamNty);
		}
	}

	private void OnTeamChangeLeaderNty(short state, TeamChangeLeaderNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && (long)down.teamId != this.myTeamData.TeamID)
		{
			Debuger.Error("更换队长之后，队伍ID变更了，请通知王赛团", new object[0]);
		}
		if (down != null && this.myTeamData != null && (long)down.teamId == this.myTeamData.TeamID)
		{
			this.myTeamData.LeaderID = down.roleId;
			if (this.myTeamData.LeaderID != EntityWorld.Instance.EntSelf.ID && this.askForJoinTeamList != null)
			{
				this.askForJoinTeamList.Clear();
				EventDispatcher.Broadcast(EventNames.UpadateAskForJoinList);
			}
			this.myTeamData.PutTeamLeaderToFirst();
			Debuger.Error("更换队长2", new object[0]);
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
		}
	}

	private void OnKickoffMemberRes(short state, KickoffMemberRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.myTeamData != null && this.myTeamData.TeamRoleList != null)
		{
			this.myTeamData.RemoveRoleMemberByID(down.roleId);
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
		}
	}

	private void OnKickoffNty(short state, KickoffNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.myTeamData != null)
		{
			this.ClearTeamData();
			EventDispatcher.Broadcast(EventNames.LeaveTeamNty);
		}
	}

	private void OnTeamStartFightAnswerNty(short state, TeamStartFightAnswerNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (EntityWorld.Instance.EntSelf.IsInBattle)
			{
				return;
			}
			this.HandleTeamFightInvite(down.dungeonId, down.dungeonType, down.cd);
		}
	}

	private void OnTeamStartFightAnswerRes(short state, TeamStartFightAnswerRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnTeamStartFightReply2AllNty(short state, TeamStartFightReply2AllNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.teamMemberStartFightReplyStateList == null)
			{
				this.teamMemberStartFightReplyStateList = new List<TeamMemberStartFightReplyState>();
			}
			if (this.myTeamData == null)
			{
				this.CloseTeamStartFightReplyUI();
				return;
			}
			if (!down.agree)
			{
				this.CloseTeamStartFightReplyUI();
				if (!EntityWorld.Instance.EntSelf.IsInBattle)
				{
					UIManagerControl.Instance.ShowToastText("已取消战斗");
				}
				return;
			}
			if (down.roleId == EntityWorld.Instance.EntSelf.ID && down.agree)
			{
				this.isAgree = true;
			}
			int num = this.myTeamData.TeamRoleList.FindIndex((MemberResume a) => a.roleId == down.roleId);
			if (num >= 0)
			{
				int num2 = this.teamMemberStartFightReplyStateList.FindIndex((TeamMemberStartFightReplyState a) => a.RoleID == down.roleId);
				if (num2 >= 0)
				{
					this.teamMemberStartFightReplyStateList.get_Item(num2).IsAgree = down.agree;
				}
				else
				{
					TeamMemberStartFightReplyState teamMemberStartFightReplyState = new TeamMemberStartFightReplyState(down.roleId);
					teamMemberStartFightReplyState.IsAgree = down.agree;
					this.teamMemberStartFightReplyStateList.Add(teamMemberStartFightReplyState);
				}
				EventDispatcher.Broadcast(EventNames.UpdateMemberStartFightState);
			}
		}
	}

	private void OnAppointJoinTeamInfoNty(short state, AppointJoinTeamInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.askForJoinTeamList == null)
			{
				this.askForJoinTeamList = new List<MemberResume>();
				this.askForJoinTeamList.Add(down.resume);
			}
			else
			{
				int num = this.askForJoinTeamList.FindIndex((MemberResume a) => a.roleId == down.resume.roleId);
				if (num < 0)
				{
					this.askForJoinTeamList.Add(down.resume);
				}
				else
				{
					this.askForJoinTeamList.RemoveAt(num);
					this.askForJoinTeamList.Add(down.resume);
				}
			}
			EventDispatcher.Broadcast(EventNames.UpadateAskForJoinList);
		}
	}

	private void OnJoinTeamNty(short state, JoinTeamNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.myTeamData = null;
			this.myTeamData = new TeamData((long)down.teamInfo.teamId, (long)down.teamInfo.leaderId);
			this.myTeamData.TeamName = down.teamInfo.teamName;
			this.myTeamData.MinLV = down.teamInfo.minLv;
			this.myTeamData.MaxLV = down.teamInfo.maxLv;
			this.myTeamData.TeamRoleList = down.teamInfo.memberResume;
			this.myTeamData.PutTeamLeaderToFirst();
			this.myTeamData.IsAutoAgree = down.teamInfo.autoAgreed;
			if (down.dungeonInfo != null)
			{
				this.myTeamData.TargetDungeonType = down.dungeonInfo.dungeonType;
				this.myTeamData.ChallengeIDParams = down.dungeonInfo.dungeonParams;
			}
			else
			{
				this.myTeamData.TargetDungeonType = DungeonType.ENUM.Other;
				this.myTeamData.ChallengeIDParams = new List<int>();
			}
			if (down.teamInfo.leaderId == (ulong)EntityWorld.Instance.EntSelf.ID)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516101, false));
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516106, false));
			}
			EventDispatcher.Broadcast(EventNames.CreateTeamSuccess);
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
		}
	}

	private void OnLeaderProcessAppointRes(short state, LeaderProcessAppointRes down = null)
	{
		if (state != 0)
		{
			if (down != null && this.askForJoinTeamList != null)
			{
				int num = this.askForJoinTeamList.FindIndex((MemberResume a) => a.roleId == down.roleId);
				if (num >= 0)
				{
					this.askForJoinTeamList.RemoveAt(num);
				}
				EventDispatcher.Broadcast(EventNames.UpadateAskForJoinList);
			}
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.askForJoinTeamList != null)
		{
			int num2 = this.askForJoinTeamList.FindIndex((MemberResume a) => a.roleId == down.roleId);
			if (num2 >= 0)
			{
				this.askForJoinTeamList.RemoveAt(num2);
			}
			EventDispatcher.Broadcast(EventNames.UpadateAskForJoinList);
		}
	}

	private void OnPartnerRejectNty(short state, PartnerRejectNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			string text = string.Format(GameDataUtils.GetChineseContent(516107, false), down.roleName);
			UIManagerControl.Instance.ShowToastText(text);
		}
	}

	private void OnTeamAutoMatchFailedNty(short state, TeamAutoMatchFailedNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		WaitUI.CloseUI(0u);
		this.OnMatchFailedAction(down.dungeonType);
	}

	private void OnTeamMemFightReqConfirmNty(short state, TeamMemFightReqConfirmNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.dungeonType == DungeonType.ENUM.MultiPvp && down.allAgree)
		{
			this.CloseTeamStartFightReplyUI();
			MultiPVPManager.Instance.OpenMatchUI(30);
		}
	}

	private void OnAutoAgreedSettingRes(short state, AutoAgreedSettingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.MyTeamData != null)
		{
			this.MyTeamData.IsAutoAgree = this.isAutoAgree;
			EventDispatcher.Broadcast(EventNames.UpdateTeamBasicInfo);
		}
	}

	private void OnQuickEnterTeamRes(short state, QuickEnterTeamRes down = null)
	{
		if (down != null & down.teamJoinCd != null)
		{
			for (int i = 0; i < down.teamJoinCd.get_Count(); i++)
			{
				int num = down.teamJoinCd.get_Item(i).cd;
				int teamId = down.teamJoinCd.get_Item(i).teamId;
				if (num <= 0)
				{
					num = this.CdTime;
				}
				TeamBasicManager.Instance.AddJoinTeamTimer(teamId, num);
				EventDispatcher.Broadcast<int, int>(EventNames.UpdateJoinInTeamCoolDown, teamId, num);
			}
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502058, false));
			EventDispatcher.Broadcast(EventNames.OnQuickEnterTeamRes);
		}
	}

	public void SendOrganizeTeamReq(DungeonType.ENUM dungeonType = DungeonType.ENUM.Other, List<int> dungeonParams = null, int systemID = 0)
	{
		this.CreateTeamDungeonType = dungeonType;
		TeamDungeonInfo teamDungeonInfo = new TeamDungeonInfo();
		teamDungeonInfo.dungeonType = dungeonType;
		if (dungeonParams != null)
		{
			teamDungeonInfo.dungeonParams.AddRange(dungeonParams);
		}
		NetworkManager.Send(new OrganizeTeamReq
		{
			dungeonInfo = teamDungeonInfo,
			systemId = systemID
		}, ServerType.Data);
	}

	public void SendTeamSettingReq(int _minLV, int _maxLV, DungeonType.ENUM dungeonType = DungeonType.ENUM.Other, List<int> dungeonParams = null)
	{
		this.CreateTeamDungeonType = dungeonType;
		TeamDungeonInfo teamDungeonInfo = new TeamDungeonInfo();
		teamDungeonInfo.dungeonType = dungeonType;
		if (dungeonParams != null)
		{
			teamDungeonInfo.dungeonParams.AddRange(dungeonParams);
		}
		NetworkManager.Send(new TeamSettingReq
		{
			minLv = _minLV,
			maxLv = _maxLV,
			dungeonInfo = teamDungeonInfo
		}, ServerType.Data);
	}

	public void SendTeamChangeLeaderReq(long _roleID)
	{
		NetworkManager.Send(new TeamChangeLeaderReq
		{
			roleId = _roleID
		}, ServerType.Data);
	}

	public void SendGetRoleResumeListReq()
	{
		NetworkManager.Send(new GetRoleResumeListReq(), ServerType.Data);
	}

	public void SendInvitePartnerReq(long _roleID = 0L)
	{
		NetworkManager.Send(new InvitePartnerReq
		{
			roleId = _roleID
		}, ServerType.Data);
	}

	public void SendPartnerAnswerReq(long _teamID, long _inviteRoleID, bool _agree = true)
	{
		NetworkManager.Send(new PartnerAnswerReq
		{
			inviteRoleId = _inviteRoleID,
			agree = _agree,
			teamId = (int)_teamID
		}, ServerType.Data);
	}

	public void SendFindTeamInfoReq(int _nPage, DungeonType.ENUM dungeonType = DungeonType.ENUM.Other, List<int> dungeonParams = null)
	{
		TeamDungeonInfo teamDungeonInfo = new TeamDungeonInfo();
		teamDungeonInfo.dungeonType = dungeonType;
		if (dungeonParams != null)
		{
			teamDungeonInfo.dungeonParams.AddRange(dungeonParams);
		}
		NetworkManager.Send(new FindTeamInfoReq
		{
			nPage = _nPage,
			dungeonInfo = teamDungeonInfo
		}, ServerType.Data);
	}

	public void SendAppointJoinTeamInfoReq(int _teamID)
	{
		NetworkManager.Send(new AppointJoinTeamInfoReq
		{
			teamId = _teamID
		}, ServerType.Data);
	}

	public void SendPartnerLeaveTeamReq()
	{
		if (!TeamBasicManager.instance.IsHaveTeam())
		{
			return;
		}
		NetworkManager.Send(new PartnerLeaveTeamReq(), ServerType.Data);
	}

	public void SendKickoffMemberReq(long _roleID)
	{
		NetworkManager.Send(new KickoffMemberReq
		{
			roleId = _roleID
		}, ServerType.Data);
	}

	public void SendTeamStartFightAnswerReq(bool _agree = false)
	{
		InstanceManager.SecurityCheck(delegate
		{
			NetworkManager.Send(new TeamStartFightAnswerReq
			{
				agree = _agree
			}, ServerType.Data);
		}, null);
	}

	public void SendLeaderProcessAppointReq(long _roleID, bool _agree = false)
	{
		NetworkManager.Send(new LeaderProcessAppointReq
		{
			roleId = _roleID,
			agree = _agree
		}, ServerType.Data);
	}

	public void SendAutoAgreedSettingReq(bool _isAutoAgree = false)
	{
		this.isAutoAgree = _isAutoAgree;
		NetworkManager.Send(new AutoAgreedSettingReq
		{
			autoAgreed = this.isAutoAgree
		}, ServerType.Data);
	}

	public void SendQuickEnterTeamReq(DungeonType.ENUM type, List<int> dungeonParams = null)
	{
		TeamDungeonInfo teamDungeonInfo = new TeamDungeonInfo();
		teamDungeonInfo.dungeonType = type;
		if (dungeonParams != null)
		{
			teamDungeonInfo.dungeonParams.AddRange(dungeonParams);
		}
		NetworkManager.Send(new QuickEnterTeamReq
		{
			dungeonInfo = teamDungeonInfo
		}, ServerType.Data);
	}

	private void ClearTeamData()
	{
		this.myTeamData = null;
		this.buddyRoleList = null;
		this.guildRoleList = null;
		this.queryTeamList = null;
		this.askForJoinTeamList = null;
		this.RemoveWorldRecruitCD();
	}

	public void ClearAskForJoinList()
	{
		if (TeamBasicManager.Instance.AskForJoinTeamList != null)
		{
			TeamBasicManager.Instance.AskForJoinTeamList.Clear();
			EventDispatcher.Broadcast(EventNames.UpadateAskForJoinList);
		}
	}

	public void TeamInviteTimeOut()
	{
		if (this.beInviteList != null && this.beInviteList.get_Count() > 0)
		{
			this.beInviteList.RemoveAt(0);
			EventDispatcher.Broadcast(EventNames.UpdateBeInvitedCount);
		}
	}

	public void HandleTeamInvite()
	{
		if (this.beInviteList != null && this.beInviteList.get_Count() > 0)
		{
			MemberResume mem = this.beInviteList.get_Item(0).TeamMemberResume;
			long teamID = this.beInviteList.get_Item(0).TeamID;
			string content = mem.name + "邀请您一起加入" + this.beInviteList.get_Item(0).TeamName + "队伍，是否接受邀请？";
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("组队邀请", content, delegate
			{
				this.SendPartnerAnswerReq(teamID, mem.roleId, false);
			}, delegate
			{
				this.SendPartnerAnswerReq(teamID, mem.roleId, true);
			}, GameDataUtils.GetChineseContent(502027, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
			this.beInviteList.RemoveAt(0);
			if (this.beInviteList.get_Count() > 0)
			{
				EventDispatcher.Broadcast(EventNames.UpdateBeInvitedCount);
			}
		}
	}

	public void HandleWorldInvite(long roleID, string name, uint teamID = 0u, string activityName = "")
	{
		if (roleID == EntityWorld.Instance.EntSelf.ID && this.myTeamData != null)
		{
			return;
		}
		this.SendAppointJoinTeamInfoReq((int)teamID);
	}

	public void HandleTeamFightInvite(int dungeonID, DungeonType.ENUM dungeonType, int countDown)
	{
		this.fightInviteCoundDown = null;
		this.HideMatchUI();
		bool flag = false;
		if (this.MyTeamData != null && this.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID)
		{
			flag = true;
		}
		string content = string.Empty;
		string coolDownContent = string.Format(GameDataUtils.GetChineseContent(516126, false), countDown);
		TeamStartFightReplyUI teamReplyUI = UIManagerControl.Instance.OpenUI("TeamStartFightReplyUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as TeamStartFightReplyUI;
		teamReplyUI.get_transform().SetAsLastSibling();
		teamReplyUI.CoolDownText = coolDownContent;
		if (flag)
		{
			teamReplyUI.ShowAsOk(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(516125, false), delegate
			{
				if (this.fightInviteCoundDown != null)
				{
					this.fightInviteCoundDown.Dispose();
					this.fightInviteCoundDown = null;
				}
				TeamBasicManager.Instance.SendTeamStartFightAnswerReq(false);
			}, GameDataUtils.GetChineseContent(505113, false), delegate
			{
				if (this.fightInviteCoundDown != null)
				{
					this.fightInviteCoundDown.Dispose();
					this.fightInviteCoundDown = null;
				}
				TeamBasicManager.Instance.SendTeamStartFightAnswerReq(false);
			});
		}
		else
		{
			content = string.Format(GameDataUtils.GetChineseContent(516116, false), string.Empty);
			teamReplyUI.ShowAsOkAndCancel(GameDataUtils.GetChineseContent(621264, false), content, delegate
			{
				if (this.fightInviteCoundDown != null)
				{
					this.fightInviteCoundDown.Dispose();
					this.fightInviteCoundDown = null;
				}
				TeamBasicManager.Instance.SendTeamStartFightAnswerReq(false);
			}, delegate
			{
				teamReplyUI.LeftBtnCanClick = false;
				teamReplyUI.RightBtnCanClick = false;
				TeamBasicManager.Instance.SendTeamStartFightAnswerReq(true);
			}, delegate
			{
				this.CloseTeamStartFightReplyUI();
				if (!this.isAgree)
				{
					TeamBasicManager.Instance.SendTeamStartFightAnswerReq(false);
				}
			}, "取 消", "确 定");
		}
		this.fightInviteCoundDown = new TimeCountDown(countDown, TimeFormat.SECOND, delegate
		{
			if (teamReplyUI != null && this.fightInviteCoundDown != null)
			{
				coolDownContent = string.Format(GameDataUtils.GetChineseContent(516126, false), this.fightInviteCoundDown.GetSeconds());
				teamReplyUI.CoolDownText = coolDownContent;
			}
		}, delegate
		{
			this.CloseTeamStartFightReplyUI();
			if (!this.isAgree)
			{
				TeamBasicManager.Instance.SendTeamStartFightAnswerReq(false);
			}
		}, true);
	}

	private void CloseTeamStartFightReplyUI()
	{
		if (this.fightInviteCoundDown != null)
		{
			this.fightInviteCoundDown.Dispose();
			this.fightInviteCoundDown = null;
		}
		this.isAgree = false;
		this.teamMemberStartFightReplyStateList = null;
		UIManagerControl.Instance.HideUI("TeamStartFightReplyUI");
	}

	public List<InviteData> UpdateInviteList(List<MemberResume> roleResumeList)
	{
		TeamBasicManager.<UpdateInviteList>c__AnonStorey1B1 <UpdateInviteList>c__AnonStorey1B = new TeamBasicManager.<UpdateInviteList>c__AnonStorey1B1();
		<UpdateInviteList>c__AnonStorey1B.roleResumeList = roleResumeList;
		List<InviteData> list = new List<InviteData>();
		if (<UpdateInviteList>c__AnonStorey1B.roleResumeList == null)
		{
			return list;
		}
		int i;
		for (i = 0; i < <UpdateInviteList>c__AnonStorey1B.roleResumeList.get_Count(); i++)
		{
			int num = 0;
			bool isInvited = false;
			int num2 = this.inviteRoleId.FindIndex((InviteCdTimeData e) => e.roleId == <UpdateInviteList>c__AnonStorey1B.roleResumeList.get_Item(i).roleId);
			if (num2 >= 0)
			{
				InviteCdTimeData inviteCdTimeData = this.inviteRoleId.get_Item(num2);
				num = TimeManager.Instance.GetTimeDiff(inviteCdTimeData.overTime, DateTime.get_UtcNow());
				Debug.LogError("对邀请列表进行筛选  " + num);
				if (num > 0)
				{
					isInvited = true;
				}
				else
				{
					isInvited = false;
					num = 0;
					this.inviteRoleId.RemoveAt(num2);
				}
			}
			InviteData inviteData = new InviteData
			{
				isInvited = isInvited,
				cdTime = num,
				role = <UpdateInviteList>c__AnonStorey1B.roleResumeList.get_Item(i)
			};
			list.Add(inviteData);
		}
		return list;
	}

	public void AddTime(long id, int cdTime)
	{
		this.RemoveRoleCd(id);
		InviteCdTimeData inviteCdTimeData;
		inviteCdTimeData.roleId = id;
		inviteCdTimeData.overTime = DateTime.get_UtcNow().AddMilliseconds((double)cdTime);
		this.inviteRoleId.Add(inviteCdTimeData);
	}

	public void RemoveRoleCd(long id)
	{
		if (this.inviteRoleId.Exists((InviteCdTimeData e) => e.roleId == id))
		{
			this.inviteRoleId.RemoveAll((InviteCdTimeData e) => e.roleId == id);
		}
	}

	private void HandeWorldRecruitCoundDown(int countDown)
	{
		this.RemoveWorldRecruitCD();
		if (countDown <= 0)
		{
			return;
		}
		this.timeCoundDown = new TimeCountDown(countDown, TimeFormat.SECOND, delegate
		{
			this.IsInCD = true;
			EventDispatcher.Broadcast<int>(EventNames.UpdateWorldRecruiteCDOnSecond, this.timeCoundDown.GetSeconds());
		}, delegate
		{
			this.RemoveWorldRecruitCD();
			EventDispatcher.Broadcast<int>(EventNames.UpdateWorldRecruiteCDOnSecond, -1);
			this.IsInCD = false;
		}, true);
	}

	private void RemoveWorldRecruitCD()
	{
		if (this.timeCoundDown != null)
		{
			this.timeCoundDown.Dispose();
			this.timeCoundDown = null;
		}
	}

	public void AddJoinTeamTimer(int teamID, int cdTime)
	{
		this.RemoveJoinTeamCDTime(teamID);
		InviteCdTimeData inviteCdTimeData;
		inviteCdTimeData.roleId = (long)teamID;
		inviteCdTimeData.overTime = DateTime.get_UtcNow().AddMilliseconds((double)(cdTime * 1000));
		this.joinTeamCDTimeList.Add(inviteCdTimeData);
	}

	public void RemoveJoinTeamCDTime(int teamID)
	{
		int num = this.joinTeamCDTimeList.FindIndex((InviteCdTimeData a) => a.roleId == (long)teamID);
		if (num >= 0)
		{
			this.joinTeamCDTimeList.RemoveAt(num);
		}
	}

	public int GetJoinTeamTime(int teamID)
	{
		if (teamID < 0)
		{
			return 0;
		}
		int num = this.joinTeamCDTimeList.FindIndex((InviteCdTimeData a) => a.roleId == (long)teamID);
		if (num >= 0)
		{
			InviteCdTimeData inviteCdTimeData = this.joinTeamCDTimeList.get_Item(num);
			return TimeManager.Instance.GetTimeDiff(inviteCdTimeData.overTime, DateTime.get_UtcNow());
		}
		return 0;
	}

	public bool IsHaveTeam()
	{
		return this.myTeamData != null;
	}

	public bool IsTeamLeader()
	{
		return this.IsHaveTeam() && EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.ID == this.MyTeamData.LeaderID;
	}

	public bool CheckTeamMateIsInFight()
	{
		if (this.myTeamData != null)
		{
			for (int i = 0; i < this.myTeamData.TeamRoleList.get_Count(); i++)
			{
				MemberResume memberResume = this.myTeamData.TeamRoleList.get_Item(i);
				if (memberResume.roleId != EntityWorld.Instance.EntSelf.ID && memberResume.inFighting)
				{
					return true;
				}
			}
		}
		return false;
	}

	public int GetTeamLowestLv()
	{
		int num = EntityWorld.Instance.EntSelf.Lv;
		if (this.myTeamData != null)
		{
			for (int i = 0; i < this.myTeamData.TeamRoleList.get_Count(); i++)
			{
				if (this.myTeamData.TeamRoleList.get_Item(i).level < num)
				{
					num = this.myTeamData.TeamRoleList.get_Item(i).level;
				}
			}
		}
		return num;
	}

	public int GetTeamHighestLv()
	{
		int num = EntityWorld.Instance.EntSelf.Lv;
		if (this.myTeamData != null)
		{
			for (int i = 0; i < this.myTeamData.TeamRoleList.get_Count(); i++)
			{
				if (this.myTeamData.TeamRoleList.get_Item(i).level > num)
				{
					num = this.myTeamData.TeamRoleList.get_Item(i).level;
				}
			}
		}
		return num;
	}

	public void OpenSeekTeamUI(DungeonType.ENUM dungeonType = DungeonType.ENUM.Other, int param = 0, Transform transParent = null)
	{
		if (transParent == null)
		{
			transParent = UINodesManager.MiddleUIRoot;
		}
		TeamJoinUI teamJoinUI = UIManagerControl.Instance.OpenUI("TeamJoinUI", transParent, false, UIType.NonPush) as TeamJoinUI;
		List<int> list = new List<int>();
		if (param > 0)
		{
			list.Add(param);
		}
		DuiWuMuBiao teamTargetCfg = this.GetTeamTargetCfg(dungeonType, list);
		if (teamTargetCfg != null)
		{
			teamJoinUI.selectTeamTargetID = teamTargetCfg.Id;
		}
		else
		{
			teamJoinUI.selectTeamTargetID = 1;
		}
		teamJoinUI.get_transform().SetAsLastSibling();
	}

	public List<DuiWuMuBiao> GetTeamTargetFirstTypeCfgList()
	{
		List<DuiWuMuBiao> list = DataReader<DuiWuMuBiao>.DataList.FindAll((DuiWuMuBiao a) => a.label == 1);
		List<DuiWuMuBiao> list2 = new List<DuiWuMuBiao>();
		if (list == null || list.get_Count() <= 0)
		{
			return list2;
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			DuiWuMuBiao duiWuMuBiao = list.get_Item(i);
			int systemId = duiWuMuBiao.SystemId;
			if (systemId == 0 || SystemOpenManager.IsSystemOn(systemId))
			{
				list2.Add(duiWuMuBiao);
			}
		}
		return list2;
	}

	public List<DuiWuMuBiao> GetAllTeamTargetCfgList()
	{
		List<DuiWuMuBiao> list = new List<DuiWuMuBiao>();
		List<DuiWuMuBiao> teamTargetFirstTypeCfgList = this.GetTeamTargetFirstTypeCfgList();
		if (teamTargetFirstTypeCfgList != null)
		{
			for (int i = 0; i < teamTargetFirstTypeCfgList.get_Count(); i++)
			{
				DuiWuMuBiao teamTargetCfg = teamTargetFirstTypeCfgList.get_Item(i);
				List<DuiWuMuBiao> list2 = DataReader<DuiWuMuBiao>.DataList.FindAll((DuiWuMuBiao a) => a.Type == teamTargetCfg.Type && a.label != 1 && teamTargetCfg.Group == a.Group);
				if (list2 == null || list2.get_Count() <= 0)
				{
					if (teamTargetCfg.SystemId == 0 || SystemOpenManager.IsSystemOn(teamTargetCfg.SystemId))
					{
						list.Add(teamTargetCfg);
					}
				}
				else
				{
					for (int j = 0; j < list2.get_Count(); j++)
					{
						if (list2.get_Item(j).Lv <= EntityWorld.Instance.EntSelf.Lv)
						{
							list.Add(list2.get_Item(j));
						}
					}
				}
			}
		}
		return list;
	}

	public string GetTeamTargetName(DungeonType.ENUM dungeonType, List<int> dungeonParams)
	{
		string result = string.Empty;
		DuiWuMuBiao teamTargetCfg = this.GetTeamTargetCfg(dungeonType, dungeonParams);
		if (teamTargetCfg != null)
		{
			result = GameDataUtils.GetChineseContent(teamTargetCfg.Word, false);
		}
		return result;
	}

	public DuiWuMuBiao GetTeamTargetCfg(DungeonType.ENUM dungeonType, List<int> dungeonParams)
	{
		DuiWuMuBiao result = null;
		List<DuiWuMuBiao> list = DataReader<DuiWuMuBiao>.DataList.FindAll((DuiWuMuBiao a) => a.Type == (int)dungeonType);
		if (list != null && list.get_Count() == 1)
		{
			return list.get_Item(0);
		}
		if (list != null && list.get_Count() > 1 && dungeonParams != null && dungeonParams.get_Count() > 0)
		{
			for (int i = 0; i < list.get_Count(); i++)
			{
				List<int> fuBen = list.get_Item(i).FuBen;
				if (fuBen != null && fuBen.get_Count() > 0)
				{
					if (fuBen.get_Item(0) == dungeonParams.get_Item(0))
					{
						return list.get_Item(i);
					}
				}
			}
		}
		return result;
	}

	public void OnMakeTeamByDungeonType(DungeonType.ENUM dungeonType, List<int> dungeonParam, int systemID)
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			return;
		}
		TeamBasicManager.Instance.SendOrganizeTeamReq(dungeonType, dungeonParam, systemID);
	}

	public void OnGoToDungeon()
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			return;
		}
		if (TeamBasicManager.Instance.MyTeamData.LeaderID != EntityWorld.Instance.EntSelf.ID)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516124, false));
			return;
		}
		if (TeamBasicManager.Instance.MyTeamData.TargetDungeonType == DungeonType.ENUM.Team)
		{
			MultiPlayerManager.Instance.CheckCanStartFight();
		}
		else if (TeamBasicManager.Instance.MyTeamData.TargetDungeonType == DungeonType.ENUM.Elite)
		{
			int num = 0;
			if (TeamBasicManager.Instance.MyTeamData.ChallengeIDParams != null && TeamBasicManager.Instance.MyTeamData.ChallengeIDParams.get_Count() > 0)
			{
				num = TeamBasicManager.Instance.MyTeamData.ChallengeIDParams.get_Item(0);
			}
			if (num > 0)
			{
				EliteDungeonManager.Instance.CheckCanStarFight(num);
			}
		}
		else if (TeamBasicManager.Instance.MyTeamData.TargetDungeonType != DungeonType.ENUM.WildBoss)
		{
			if (TeamBasicManager.Instance.MyTeamData.TargetDungeonType == DungeonType.ENUM.MultiPve)
			{
				DarkTrialManager.Instance.StartDarkTrial();
			}
		}
	}

	public void OnClickStart(DungeonType.ENUM dungeonType, Action sendMatchReq, Action sendChallengeReq)
	{
		if (TeamBasicManager.Instance.MyTeamData == null)
		{
			return;
		}
		if (TeamBasicManager.Instance.MyTeamData.LeaderID != EntityWorld.Instance.EntSelf.ID)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516124, false));
			return;
		}
		if (TeamBasicManager.Instance.CheckTeamMateIsInFight())
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516115, false));
			return;
		}
		if (sendChallengeReq != null)
		{
			sendChallengeReq.Invoke();
		}
	}

	public void OnClickMatch(DungeonType.ENUM dungeonType, Action sendMatchReq)
	{
		if (sendMatchReq != null)
		{
			sendMatchReq.Invoke();
		}
	}

	public void OnChallengeSuccessCallBack(DungeonType.ENUM dungeonType, int dungeonID = 1, int inviteCD = 20, Action callBack = null)
	{
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return;
		}
		if (this.MyTeamData != null && this.MyTeamData.LeaderID == EntityWorld.Instance.EntSelf.ID && this.MyTeamData.TeamRoleList.get_Count() > 1)
		{
			WaitUI.CloseUI(0u);
			this.HandleTeamFightInvite(dungeonID, dungeonType, inviteCD);
			if (callBack != null)
			{
				callBack.Invoke();
			}
		}
	}

	public void OnMatchRes(int countDown, bool isOrder = false, Action callEnd = null)
	{
		this.OpenMatchUI(countDown, isOrder, callEnd);
	}

	public void OnMatchFailedAction(DungeonType.ENUM dungeonType)
	{
		this.HideMatchUI();
		if (dungeonType == DungeonType.ENUM.MultiPvp)
		{
			MultiPVPManager.Instance.OnMatchFailedCallBack();
		}
		else if (dungeonType == DungeonType.ENUM.Elite)
		{
			EliteDungeonManager.Instance.OnMatchFailedCallBack();
		}
		else if (dungeonType == DungeonType.ENUM.Team)
		{
			MultiPlayerManager.Instance.OnMatchFailedCallBack();
		}
	}

	public void OpenMatchUI(int countDown, bool isOrder = false, Action matchEndCB = null)
	{
		MatchUI matchUI = UIManagerControl.Instance.OpenUI("MatchUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as MatchUI;
		matchUI.SetData(countDown, isOrder, delegate
		{
			if (matchEndCB != null)
			{
				matchEndCB.Invoke();
			}
		});
	}

	public void HideMatchUI()
	{
		UIManagerControl.Instance.HideUI("MatchUI");
	}
}
