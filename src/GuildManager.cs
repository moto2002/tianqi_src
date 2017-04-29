using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class GuildManager : BaseSubSystemManager
{
	public const int Npage = 10;

	private GuildBaseInfo m_guildInfo;

	private MemberInfo m_memberInfo;

	private List<MemberInfo> m_guildMemberList;

	private InviteSetting m_guildInviteSetting;

	private List<ApplicantInfo> m_ApplicationPlayers;

	private List<GuildBriefInfo> searchApplicationGuilds;

	private List<GuildBriefInfo> m_OwnApplicationGuilds;

	private List<GuildLogTrace> guildLogList;

	public bool IsHideTitle;

	public bool IsInGuildTask;

	protected bool isGuildFieldOpen;

	protected uint autoExitTimer = 4294967295u;

	private EquipSmeltInfoPush mEquipSmeltInfoPush;

	public int GuildDonateTotalTimes;

	public int GuildTaskTotalTimes;

	public int GuildTaskTurnTimes;

	private static GuildManager instance;

	public bool IsBuildEquipWaitting;

	public bool CanNotRequire;

	private uint mBuildSuccessDelayId;

	private int mBuildSuccessFXId1;

	private int mBuildSuccessFXId2;

	private long mBuildSuccessEquipUUID;

	public GuildBaseInfo MyGuildnfo
	{
		get
		{
			return this.m_guildInfo;
		}
	}

	public MemberInfo MyMemberInfo
	{
		get
		{
			return this.m_memberInfo;
		}
	}

	public List<MemberInfo> MyGuildMemberList
	{
		get
		{
			if (this.m_guildMemberList != null)
			{
				this.m_guildMemberList.Sort(new Comparison<MemberInfo>(GuildManager.MemberSortCompare));
			}
			return this.m_guildMemberList;
		}
	}

	public InviteSetting MyGuildInviteSetting
	{
		get
		{
			return this.m_guildInviteSetting;
		}
	}

	public List<ApplicantInfo> ApplicationPlayers
	{
		get
		{
			return this.m_ApplicationPlayers;
		}
		set
		{
			this.m_ApplicationPlayers = value;
		}
	}

	public List<ApplicantInfo> SortApplicationPlayers
	{
		get
		{
			if (this.ApplicationPlayers != null)
			{
				this.ApplicationPlayers.Sort(new Comparison<ApplicantInfo>(GuildManager.ApplicationSortCompare));
			}
			return this.ApplicationPlayers;
		}
	}

	public List<GuildBriefInfo> SearchApplicationGuilds
	{
		get
		{
			return this.searchApplicationGuilds;
		}
		set
		{
			this.searchApplicationGuilds = null;
		}
	}

	public List<GuildBriefInfo> OwnApplicationGuilds
	{
		get
		{
			return this.m_OwnApplicationGuilds;
		}
		set
		{
			this.m_OwnApplicationGuilds = value;
		}
	}

	public List<GuildLogTrace> GuildLogList
	{
		get
		{
			if (this.guildLogList == null)
			{
				this.guildLogList = new List<GuildLogTrace>();
			}
			else
			{
				this.guildLogList.Sort(new Comparison<GuildLogTrace>(GuildManager.GuildLogListSortCompare));
			}
			return this.guildLogList;
		}
	}

	public bool IsGuildFieldOpen
	{
		get
		{
			return this.isGuildFieldOpen;
		}
		set
		{
			this.isGuildFieldOpen = value;
		}
	}

	public static GuildManager Instance
	{
		get
		{
			if (GuildManager.instance == null)
			{
				GuildManager.instance = new GuildManager();
			}
			return GuildManager.instance;
		}
	}

	private GuildManager()
	{
	}

	public MemberInfo GetMember(long roleId)
	{
		List<MemberInfo> guildMemberList = this.m_guildMemberList;
		for (int i = 0; i < guildMemberList.get_Count(); i++)
		{
			if (guildMemberList.get_Item(i).roleId == roleId)
			{
				return guildMemberList.get_Item(i);
			}
		}
		return null;
	}

	private static int MemberSortCompare(MemberInfo AF1, MemberInfo AF2)
	{
		int result = 1;
		if (AF1.offlineSec < 0 && AF2.offlineSec >= 0)
		{
			return -1;
		}
		if (AF1.offlineSec >= 0 && AF2.offlineSec < 0)
		{
			return 1;
		}
		if (GuildManager.IsTitleToper(AF1, AF2))
		{
			return -1;
		}
		if (GuildManager.IsTitleToper(AF2, AF1))
		{
			return 1;
		}
		if (AF1.offlineSec >= 0 && AF2.offlineSec >= 0 && AF1.fighting > AF2.fighting)
		{
			return -1;
		}
		if (AF1.offlineSec < 0 && AF2.offlineSec < 0 && AF1.fighting > AF2.fighting)
		{
			return -1;
		}
		return result;
	}

	private static bool IsTitleToper(MemberInfo AF1, MemberInfo AF2)
	{
		return (AF1.title.get_Item(0) == MemberTitleType.MTT.Chairman && AF2.title.get_Item(0) != MemberTitleType.MTT.Chairman) || (AF1.title.get_Item(0) == MemberTitleType.MTT.ViceChairman && AF2.title.get_Item(0) != MemberTitleType.MTT.Chairman && AF2.title.get_Item(0) != MemberTitleType.MTT.ViceChairman) || (AF1.title.get_Item(0) == MemberTitleType.MTT.Manager && AF2.title.get_Item(0) == MemberTitleType.MTT.Normal);
	}

	private void Remove2ApplicationPlayers(List<long> roleIdList)
	{
		for (int i = 0; i < roleIdList.get_Count(); i++)
		{
			long roleID = roleIdList.get_Item(i);
			int num = this.ApplicationPlayers.FindIndex((ApplicantInfo a) => a.roleId == roleID);
			if (num >= 0)
			{
				this.ApplicationPlayers.RemoveAt(num);
			}
		}
	}

	private static int ApplicationSortCompare(ApplicantInfo AP1, ApplicantInfo AP2)
	{
		if (AP1.fighting > AP2.fighting)
		{
			return -1;
		}
		if (AP1.fighting == AP2.fighting && AP1.lv > AP2.lv)
		{
			return -1;
		}
		return 1;
	}

	public List<GuildBriefInfo> GetApplicationGuilds(bool isFilter, bool isSearch = false)
	{
		if (isSearch)
		{
			return this.GetApplicationGuilds2All(this.searchApplicationGuilds);
		}
		if (!isFilter)
		{
			return this.GetApplicationGuilds2All(this.m_OwnApplicationGuilds);
		}
		return this.GetApplicationGuilds2Available();
	}

	private List<GuildBriefInfo> GetApplicationGuilds2All(List<GuildBriefInfo> guildList)
	{
		if (guildList == null)
		{
			return new List<GuildBriefInfo>();
		}
		guildList.Sort(new Comparison<GuildBriefInfo>(GuildManager.ApplicationGuildSortCompare));
		return guildList;
	}

	private List<GuildBriefInfo> GetApplicationGuilds2Available()
	{
		List<GuildBriefInfo> list = new List<GuildBriefInfo>();
		for (int i = 0; i < this.m_OwnApplicationGuilds.get_Count(); i++)
		{
			GuildBriefInfo guildBriefInfo = this.m_OwnApplicationGuilds.get_Item(i);
			if (guildBriefInfo.status == GuildBriefInfo.ApplyStatus.Available && EntityWorld.Instance.EntSelf.Lv >= guildBriefInfo.roleMinLv)
			{
				list.Add(guildBriefInfo);
			}
		}
		list.Sort(new Comparison<GuildBriefInfo>(GuildManager.ApplicationGuildSortCompare));
		return list;
	}

	private static int ApplicationGuildSortCompare(GuildBriefInfo AF1, GuildBriefInfo AF2)
	{
		int result = 1;
		if (AF1.fighting > AF2.fighting)
		{
			result = -1;
		}
		else if (AF1.fighting == AF2.fighting && AF1.roleMinLv < AF2.roleMinLv)
		{
			result = -1;
		}
		return result;
	}

	public List<GuildBriefInfo> GetOtherGuildInfoList()
	{
		return this.OwnApplicationGuilds;
	}

	public static int OtherGuildSortCompare(GuildBriefInfo GBI1, GuildBriefInfo GBI2)
	{
		int result = 1;
		if (GBI1.fighting > GBI2.fighting)
		{
			return -1;
		}
		return result;
	}

	private static int GuildLogListSortCompare(GuildLogTrace AF1, GuildLogTrace AF2)
	{
		if (AF1.logTimeUtc > AF2.logTimeUtc)
		{
			return -1;
		}
		if (AF1.logTimeUtc == AF2.logTimeUtc && AF1.logType > AF2.logType)
		{
			return -1;
		}
		return 1;
	}

	public override void Init()
	{
		base.Init();
		this.GuildDonateTotalTimes = (int)float.Parse(DataReader<GongHuiXinXi>.Get("DonateTime").value);
		this.GuildTaskTotalTimes = (int)float.Parse(DataReader<GongHuiXinXi>.Get("TaskTime").value);
		this.GuildTaskTurnTimes = (int)float.Parse(DataReader<GongHuiXinXi>.Get("TaskNum").value);
	}

	public override void Release()
	{
		this.ClearGuildInfo();
		this.isGuildFieldOpen = false;
		TimerHeap.DelTimer(this.autoExitTimer);
		this.autoExitTimer = 0u;
		this.GuildDonateTotalTimes = 0;
		this.GuildTaskTotalTimes = 0;
		this.GuildTaskTurnTimes = 0;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GuildInfoNty>(new NetCallBackMethod<GuildInfoNty>(this.OnGuildInfoNty));
		NetworkManager.AddListenEvent<GetGuildInfoRes>(new NetCallBackMethod<GetGuildInfoRes>(this.OnGetGuildInfoRes));
		NetworkManager.AddListenEvent<SetUpGuildRes>(new NetCallBackMethod<SetUpGuildRes>(this.OnSetUpGuildRes));
		NetworkManager.AddListenEvent<QueryGuildInfoRes>(new NetCallBackMethod<QueryGuildInfoRes>(this.OnQueryGuildInfoRes));
		NetworkManager.AddListenEvent<SearchGuildInfoRes>(new NetCallBackMethod<SearchGuildInfoRes>(this.OnSearchGuildInfoRes));
		NetworkManager.AddListenEvent<InviteSettingRes>(new NetCallBackMethod<InviteSettingRes>(this.OnInviteSettingRes));
		NetworkManager.AddListenEvent<QueryApplicantInfoRes>(new NetCallBackMethod<QueryApplicantInfoRes>(this.OnQueryApplicantInfoRes));
		NetworkManager.AddListenEvent<AcceptGuildApplicantRes>(new NetCallBackMethod<AcceptGuildApplicantRes>(this.OnAcceptGuildApplicantRes));
		NetworkManager.AddListenEvent<RefuseGuildApplicantRes>(new NetCallBackMethod<RefuseGuildApplicantRes>(this.OnRefuseGuildApplicantRes));
		NetworkManager.AddListenEvent<RefuseGuildApplicantNty>(new NetCallBackMethod<RefuseGuildApplicantNty>(this.OnRefuseGuildApplicantNty));
		NetworkManager.AddListenEvent<KickOffGuildMemberRes>(new NetCallBackMethod<KickOffGuildMemberRes>(this.OnKickOffGuildMemberRes));
		NetworkManager.AddListenEvent<KickedOffGuildNty>(new NetCallBackMethod<KickedOffGuildNty>(this.OnKickedOffGuildNty));
		NetworkManager.AddListenEvent<AppointMemberRes>(new NetCallBackMethod<AppointMemberRes>(this.OnAppointMemberRes));
		NetworkManager.AddListenEvent<AppointMemberNty>(new NetCallBackMethod<AppointMemberNty>(this.OnAppointMemberNty));
		NetworkManager.AddListenEvent<MakeAnApplicationForAGuildRes>(new NetCallBackMethod<MakeAnApplicationForAGuildRes>(this.OnMakeAnApplicationForAGuildRes));
		NetworkManager.AddListenEvent<GuildDataChangeNty>(new NetCallBackMethod<GuildDataChangeNty>(this.OnGuildDataChangeNty));
		NetworkManager.AddListenEvent<DissolveGuildRes>(new NetCallBackMethod<DissolveGuildRes>(this.OnDissolveGuildRes));
		NetworkManager.AddListenEvent<DissolveGuildNty>(new NetCallBackMethod<DissolveGuildNty>(this.OnDissolveGuildNty));
		NetworkManager.AddListenEvent<GetGuildLogRes>(new NetCallBackMethod<GetGuildLogRes>(this.OnGetGuildLogRes));
		NetworkManager.AddListenEvent<GuildExitRes>(new NetCallBackMethod<GuildExitRes>(this.OnGuildExitRes));
		NetworkManager.AddListenEvent<UpgradeGuildRes>(new NetCallBackMethod<UpgradeGuildRes>(this.OnUpgradeGuildRes));
		NetworkManager.AddListenEvent<GuildTitleSetRes>(new NetCallBackMethod<GuildTitleSetRes>(this.OnGuildTitleSetRes));
		NetworkManager.AddListenEvent<GuildJoinTipsNty>(new NetCallBackMethod<GuildJoinTipsNty>(this.OnGuildJoinTipsNty));
		NetworkManager.AddListenEvent<GuildBuildRes>(new NetCallBackMethod<GuildBuildRes>(this.OnGuildBuildRes));
		NetworkManager.AddListenEvent<JoinGuildByRoleRes>(new NetCallBackMethod<JoinGuildByRoleRes>(this.OnJoinGuildByRoleRes));
		NetworkManager.AddListenEvent<GuildFieldOpenStatusNty>(new NetCallBackMethod<GuildFieldOpenStatusNty>(this.OnGuildFieldOpenStatusNty));
		NetworkManager.AddListenEvent<EquipSmeltInfoPush>(new NetCallBackMethod<EquipSmeltInfoPush>(this.OnEquipSmeltInfoPush));
		NetworkManager.AddListenEvent<SmeltEquipRes>(new NetCallBackMethod<SmeltEquipRes>(this.OnSmeltEquipRes));
		NetworkManager.AddListenEvent<BuildEquipRes>(new NetCallBackMethod<BuildEquipRes>(this.OnBuildEquipRes));
		NetworkManager.AddListenEvent<GuildAttrNty>(new NetCallBackMethod<GuildAttrNty>(this.OnGuildAttrNty));
	}

	public void SendJoinInToGuildByRoleID(long _roleID)
	{
		if (!SystemOpenManager.IsSystemClickOpen(45, 0, true))
		{
			return;
		}
		if (this.IsJoinInGuild())
		{
			UIManagerControl.Instance.ShowToastText("您已加入军团");
			return;
		}
		NetworkManager.Send(new JoinGuildByRoleReq
		{
			roleId = _roleID
		}, ServerType.Data);
	}

	public void SendInvitePlayerToGuild(long _roleId)
	{
	}

	public void SendGetGuildInfoReq()
	{
		NetworkManager.Send(new GetGuildInfoReq(), ServerType.Data);
	}

	public void SendSetUpGuildReq(string name, int roleMinLv = 0, bool verify = true, string notice = "")
	{
		NetworkManager.Send(new SetUpGuildReq
		{
			name = name,
			roleMinLv = roleMinLv,
			verify = verify,
			notice = notice
		}, ServerType.Data);
	}

	public void SendMakeAnApplicationForAGuild(long _guildId)
	{
		NetworkManager.Send(new MakeAnApplicationForAGuildReq
		{
			guildId = _guildId
		}, ServerType.Data);
	}

	public void SendInviteSettingReq(int m_roleMinlv, bool m_verify = false, string m_notice = "")
	{
		InviteSettingReq inviteSettingReq = new InviteSettingReq();
		if (this.MyGuildInviteSetting != null && this.MyGuildInviteSetting.roleMinLv != m_roleMinlv)
		{
			inviteSettingReq.roleMinLv = m_roleMinlv;
		}
		if (this.MyGuildnfo != null && this.MyGuildnfo.notice != m_notice)
		{
			inviteSettingReq.notice = m_notice;
		}
		inviteSettingReq.verify = m_verify;
		NetworkManager.Send(inviteSettingReq, ServerType.Data);
	}

	public void SendSearchGuildInfoReq(string searchName, int page = 1, bool showCanjoin = false)
	{
		NetworkManager.Send(new SearchGuildInfoReq
		{
			name = searchName,
			canJoin = showCanjoin,
			nPage = page
		}, ServerType.Data);
	}

	public void SendQueryGuildInfoReq(int _fromIndex, int _toIndex, bool _canJoin)
	{
		NetworkManager.Send(new QueryGuildInfoReq
		{
			fromIndex = _fromIndex,
			toIndex = _toIndex,
			canJoin = _canJoin
		}, ServerType.Data);
	}

	public void SendQueryApplicantInfoReq()
	{
		NetworkManager.Send(new QueryApplicantInfoReq(), ServerType.Data);
	}

	public void SendAcceptGuildApplicant(List<long> _roleIdList)
	{
		if (_roleIdList == null || _roleIdList.get_Count() <= 0)
		{
			return;
		}
		this.Remove2ApplicationPlayers(_roleIdList);
		AcceptGuildApplicantReq acceptGuildApplicantReq = new AcceptGuildApplicantReq();
		acceptGuildApplicantReq.roleId.AddRange(_roleIdList);
		NetworkManager.Send(acceptGuildApplicantReq, ServerType.Data);
	}

	public void SendRefuseGuildApplicant(List<long> _roleIdList)
	{
		if (_roleIdList == null || _roleIdList.get_Count() <= 0)
		{
			return;
		}
		this.Remove2ApplicationPlayers(_roleIdList);
		RefuseGuildApplicantReq refuseGuildApplicantReq = new RefuseGuildApplicantReq();
		refuseGuildApplicantReq.roleId.AddRange(_roleIdList);
		NetworkManager.Send(refuseGuildApplicantReq, ServerType.Data);
	}

	public void SendKickOffGuildMember(long _roleId)
	{
		NetworkManager.Send(new KickOffGuildMemberReq
		{
			roleId = _roleId
		}, ServerType.Data);
	}

	public void SendAppointMemberReq(long _roleID, MemberTitleType.MTT _title)
	{
		NetworkManager.Send(new AppointMemberReq
		{
			roleId = _roleID,
			title = _title
		}, ServerType.Data);
	}

	public void SendGetGuildLogReq(int _nPage = 0)
	{
		NetworkManager.Send(new GetGuildLogReq
		{
			nPage = _nPage
		}, ServerType.Data);
	}

	public void SendDissolveGuildReq()
	{
		if (this.m_guildInfo != null)
		{
			NetworkManager.Send(new DissolveGuildReq
			{
				guildId = this.m_guildInfo.guildId
			}, ServerType.Data);
		}
	}

	public void SendUpgradeGuildReq()
	{
		NetworkManager.Send(new UpgradeGuildReq(), ServerType.Data);
	}

	public void SendGuildExitReq()
	{
		NetworkManager.Send(new GuildExitReq(), ServerType.Data);
	}

	public void SendGuildTitleSetReq(bool isHide)
	{
		NetworkManager.Send(new GuildTitleSetReq
		{
			hidden = isHide
		}, ServerType.Data);
	}

	public void SendGuildBuildReq(GuildBuildType.GBT type)
	{
		NetworkManager.Send(new GuildBuildReq
		{
			buildType = type
		}, ServerType.Data);
	}

	public void SendSmeltEquip(List<DecomposeEquipInfo> infos)
	{
		SmeltEquipReq smeltEquipReq = new SmeltEquipReq();
		smeltEquipReq.equipInfo.AddRange(infos);
		NetworkManager.Send(smeltEquipReq, ServerType.Data);
	}

	public void SendBuildEquip(int _position)
	{
		if (this.IsBuildEquipWaitting)
		{
			UIManagerControl.Instance.ShowToastText("正在打造, 请等待");
			return;
		}
		this.IsBuildEquipWaitting = true;
		NetworkManager.Send(new BuildEquipReq
		{
			position = _position
		}, ServerType.Data);
	}

	private void OnGuildInfoNty(short state, GuildInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_guildInfo = down.baseInfo;
			this.m_memberInfo = down.memberInfo;
			this.IsHideTitle = down.hidden;
			this.GuildTitleNotify();
			EventDispatcher.Broadcast(EventNames.OnGuildInfoNty);
			ActivityCenterManager.Instance.ChangeGuildFieldActivityTip(this.IsGuildFieldOpen);
			ActivityCenterManager.Instance.ChangeGuildWarActivityTip(GuildWarManager.Instance.GuildWarTimeStep);
		}
	}

	private void OnGetGuildInfoRes(short state, GetGuildInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_guildInfo = down.baseInfo;
			this.m_guildMemberList = down.members;
			this.m_guildInviteSetting = down.setting;
			EventDispatcher.Broadcast(EventNames.UpdateGuildInfo);
		}
	}

	private void OnSetUpGuildRes(short state, SetUpGuildRes down = null)
	{
		if ((int)state == Status.ITEM_NOT_ENOUGH_COUNT)
		{
			GongHuiDengJi gongHuiDengJi = DataReader<GongHuiDengJi>.Get(1);
			StateManager.Instance.StateShow(state, gongHuiDengJi.gold);
			return;
		}
		if ((int)state == Status.GUILD_NAME_ALREADY_EXIST)
		{
			string tipContentByKey = this.GetTipContentByKey("nameprompt");
			UIManagerControl.Instance.ShowToastText(tipContentByKey);
			return;
		}
		if ((int)state == Status.GUILD_DISSOLVE_GUILD_CD || (int)state == Status.GUILD_EXIT_GUILD_CD || (int)state == Status.GUILD_SYSTEM_DISSOLVE_CD || (int)state == Status.GUILD_KICK_OFF_CD)
		{
			int totalSeconds = 0;
			if (down != null)
			{
				totalSeconds = down.coolDown;
			}
			string time = TimeConverter.GetTime(totalSeconds, TimeFormat.HHMMSS);
			string text = string.Format(this.GetTipContentByKey("wait"), time);
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			string tipContentByKey2 = this.GetTipContentByKey("setup");
			UIManagerControl.Instance.ShowToastText(tipContentByKey2);
		}
	}

	private void OnInviteSettingRes(short state, InviteSettingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.MyGuildnfo != null && this.MyGuildInviteSetting != null)
		{
			this.m_guildInfo.verify = down.verify;
			this.m_guildInfo.notice = down.notice;
			this.MyGuildInviteSetting.verify = down.verify;
			this.MyGuildInviteSetting.available = down.available;
			this.MyGuildInviteSetting.roleMinLv = down.roleMinLv;
			UIManagerControl.Instance.ShowToastText("修改成功！");
			EventDispatcher.Broadcast(EventNames.OnGuildSettingNty);
		}
	}

	private void OnQueryGuildInfoRes(short state, QueryGuildInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.CanNotRequire = (down.guildInfos.get_Count() < 10);
			if (this.m_OwnApplicationGuilds == null)
			{
				this.m_OwnApplicationGuilds = new List<GuildBriefInfo>();
				this.m_OwnApplicationGuilds = down.guildInfos;
			}
			else
			{
				int i;
				for (i = 0; i < down.guildInfos.get_Count(); i++)
				{
					int num = this.m_OwnApplicationGuilds.FindIndex((GuildBriefInfo a) => a.guildId == down.guildInfos.get_Item(i).guildId);
					if (num < 0)
					{
						this.m_OwnApplicationGuilds.Add(down.guildInfos.get_Item(i));
					}
					else
					{
						this.m_OwnApplicationGuilds.set_Item(num, down.guildInfos.get_Item(i));
					}
				}
			}
			EventDispatcher.Broadcast(EventNames.UpdateGuildInfoList);
		}
	}

	private void OnSearchGuildInfoRes(short state, SearchGuildInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.guildInfos == null || down.guildInfos.get_Count() == 0)
			{
				string tipContentByKey = this.GetTipContentByKey("search");
				UIManagerControl.Instance.ShowToastText(tipContentByKey);
			}
			else
			{
				this.CanNotRequire = (down.guildInfos.get_Count() < 8);
				if (this.searchApplicationGuilds == null)
				{
					this.searchApplicationGuilds = down.guildInfos;
				}
				else
				{
					this.searchApplicationGuilds.Clear();
					if (down.nPage > 0)
					{
						int i;
						for (i = 0; i < down.guildInfos.get_Count(); i++)
						{
							int num = this.searchApplicationGuilds.FindIndex((GuildBriefInfo a) => a.guildId == down.guildInfos.get_Item(i).guildId);
							if (num < 0)
							{
								this.searchApplicationGuilds.Add(down.guildInfos.get_Item(i));
							}
						}
					}
				}
				EventDispatcher.Broadcast(EventNames.UpdateSearchGuildList);
			}
		}
		else
		{
			FloatTextAddManager.Instance.AddFloatText(GameDataUtils.GetChineseContent(506046, false), Color.get_green());
		}
	}

	private void OnMakeAnApplicationForAGuildRes(short state, MakeAnApplicationForAGuildRes down = null)
	{
		if ((int)state == Status.GUILD_IS_FULL)
		{
			string tipContentByKey = this.GetTipContentByKey("full");
			UIManagerControl.Instance.ShowToastText(tipContentByKey);
			return;
		}
		if ((int)state == Status.ALREADY_APPLIED_FOR_THIS_GUILD)
		{
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast<long, int>(EventNames.UpdateGuildInfoCoolDown, down.guildId, down.coolDown);
			return;
		}
		if ((int)state == Status.GUILD_DISSOLVE_GUILD_CD || (int)state == Status.GUILD_EXIT_GUILD_CD || (int)state == Status.GUILD_SYSTEM_DISSOLVE_CD || (int)state == Status.GUILD_KICK_OFF_CD)
		{
			int totalSeconds = 0;
			if (down != null)
			{
				totalSeconds = down.coolDown;
			}
			string time = TimeConverter.GetTime(totalSeconds, TimeFormat.HHMMSS);
			string text = string.Format(this.GetTipContentByKey("wait"), time);
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.ShowToastText("申请成功，等待审核");
			EventDispatcher.Broadcast<long, int>(EventNames.UpdateGuildInfoCoolDown, down.guildId, down.coolDown);
		}
	}

	private void OnJoinGuildByRoleRes(short state, JoinGuildByRoleRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnGuildDataChangeNty(short state, GuildDataChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.changeType == GuildDataChangeNty.ChangeType.ApplicantList && this.m_memberInfo != null && this.CheckMemberHasPrivilege(GuildPrivilegeState.AcceptOrRefuseMember))
		{
			this.SendQueryApplicantInfoReq();
		}
	}

	private void OnQueryApplicantInfoRes(short state, QueryApplicantInfoRes down = null)
	{
		if ((int)state == Status.GUILD_OTHER_MANAGER_DONE)
		{
			if (this.ApplicationPlayers != null)
			{
				this.ApplicationPlayers.Clear();
			}
			EventDispatcher.Broadcast(EventNames.UpdateGuildApplication);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.applicants.get_Count() >= 0)
		{
			this.ApplicationPlayers = down.applicants;
			EventDispatcher.Broadcast(EventNames.UpdateGuildApplication);
		}
	}

	private void OnAcceptGuildApplicantRes(short state, AcceptGuildApplicantRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			if (this.ApplicationPlayers != null && this.ApplicationPlayers.get_Count() <= 0)
			{
				this.ApplicationPlayers = null;
				EventDispatcher.Broadcast(EventNames.UpdateGuildApplication);
			}
			return;
		}
		if (down != null)
		{
			this.ApplicationPlayers = down.applicants;
			EventDispatcher.Broadcast(EventNames.UpdateGuildApplication);
		}
	}

	private void OnRefuseGuildApplicantRes(short state, RefuseGuildApplicantRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			if (this.ApplicationPlayers != null && this.ApplicationPlayers.get_Count() <= 0)
			{
				this.ApplicationPlayers = null;
				EventDispatcher.Broadcast(EventNames.UpdateGuildApplication);
			}
			return;
		}
		if (down != null)
		{
			this.ApplicationPlayers = down.applicants;
			EventDispatcher.Broadcast(EventNames.UpdateGuildApplication);
		}
	}

	private void OnRefuseGuildApplicantNty(short state, RefuseGuildApplicantNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.ShowToastText(down.reason);
		}
	}

	private void OnKickOffGuildMemberRes(short state, KickOffGuildMemberRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			int num = this.m_guildMemberList.FindIndex((MemberInfo a) => a.roleId == down.roleId);
			if (num >= 0)
			{
				this.m_guildInfo.memberSize--;
				this.m_guildMemberList.RemoveAt(num);
				EventDispatcher.Broadcast(EventNames.UpdateGuildMemberList);
			}
		}
	}

	private void OnKickedOffGuildNty(short state, KickedOffGuildNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.roleId == EntityWorld.Instance.EntSelf.ID)
		{
			this.ClearGuildInfo();
			UIManagerControl.Instance.ShowToastText("您已被踢出军团");
			EventDispatcher.Broadcast(EventNames.OnDissolveGuildRes);
			this.GuildTitleNotify();
			ActivityCenterManager.Instance.ChangeGuildFieldActivityTip(false);
			ActivityCenterManager.Instance.ChangeGuildWarActivityTip(GuildWarManager.Instance.GuildWarTimeStep);
		}
	}

	private void OnAppointMemberRes(short state, AppointMemberRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			int num = this.m_guildMemberList.FindIndex((MemberInfo a) => a.roleId == down.roleId);
			if (num >= 0)
			{
				this.m_guildMemberList.get_Item(num).title.set_Item(0, down.titles.get_Item(0));
			}
			int num2 = this.m_guildMemberList.FindIndex((MemberInfo a) => a.roleId == this.m_memberInfo.roleId);
			if (num2 >= 0)
			{
				this.m_memberInfo.title.set_Item(0, down.myTitles.get_Item(0));
				this.m_guildMemberList.set_Item(num2, this.m_memberInfo);
				this.GuildTitleNotify();
			}
			EventDispatcher.Broadcast(EventNames.OnGuildInfoChangeNty);
		}
	}

	private void OnAppointMemberNty(short state, AppointMemberNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null || this.m_memberInfo == null)
		{
			return;
		}
		this.m_memberInfo.title.set_Item(0, down.titles.get_Item(0));
		if (this.m_guildMemberList == null)
		{
			EventDispatcher.Broadcast(EventNames.OnGuildInfoChangeNty);
			return;
		}
		int num = this.m_guildMemberList.FindIndex((MemberInfo a) => a.roleId == down.roleId);
		if (num >= 0)
		{
			this.m_guildMemberList.set_Item(num, this.m_memberInfo);
			EventDispatcher.Broadcast(EventNames.OnGuildInfoChangeNty);
		}
		this.GuildTitleNotify();
	}

	private void OnGetGuildLogRes(short state, GetGuildLogRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.guildLogList = down.logTraces;
			EventDispatcher.Broadcast(EventNames.UpdateGuildLogList);
		}
	}

	private void OnGuildExitRes(short state, GuildExitRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.ClearGuildInfo();
		UIManagerControl.Instance.ShowToastText("退出军团成功");
		EventDispatcher.Broadcast(EventNames.OnDissolveGuildRes);
		this.GuildTitleNotify();
		ActivityCenterManager.Instance.ChangeGuildFieldActivityTip(false);
		ActivityCenterManager.Instance.ChangeGuildWarActivityTip(GuildWarManager.Instance.GuildWarTimeStep);
	}

	private void OnUpgradeGuildRes(short state, UpgradeGuildRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			string tipContentByKey = this.GetTipContentByKey("UpdateSuccessful");
			UIManagerControl.Instance.ShowToastText(tipContentByKey);
			this.m_guildInfo.lv = down.guildLv;
			this.m_guildInfo.guildFund = down.fund;
			EventDispatcher.Broadcast(EventNames.OnUpgradeGuildRes);
		}
	}

	private void OnDissolveGuildRes(short state, DissolveGuildRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.ClearGuildInfo();
		UIManagerControl.Instance.ShowToastText("解散成功！");
		EventDispatcher.Broadcast(EventNames.OnDissolveGuildRes);
		this.GuildTitleNotify();
		ActivityCenterManager.Instance.ChangeGuildFieldActivityTip(false);
		ActivityCenterManager.Instance.ChangeGuildWarActivityTip(GuildWarManager.Instance.GuildWarTimeStep);
	}

	private void OnDissolveGuildNty(short state, DissolveGuildNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.ClearGuildInfo();
			EventDispatcher.Broadcast(EventNames.OnDissolveGuildRes);
			this.GuildTitleNotify();
			ActivityCenterManager.Instance.ChangeGuildFieldActivityTip(false);
			ActivityCenterManager.Instance.ChangeGuildWarActivityTip(GuildWarManager.Instance.GuildWarTimeStep);
		}
	}

	private void OnGuildTitleSetRes(short state, GuildTitleSetRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsHideTitle = down.hidden;
			this.GuildTitleNotify();
		}
	}

	private void OnGuildJoinTipsNty(short state, GuildJoinTipsNty down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down != null)
		{
			if (down.type == GuildJoinTipsNty.TipsType.JoinIn)
			{
				TimerHeap.AddTimer(2000u, 0, delegate
				{
					string text = string.Format(this.GetTipContentByKey("join"), down.guildName);
					UIManagerControl.Instance.ShowToastText(text);
				});
			}
			else if (down.type == GuildJoinTipsNty.TipsType.Reject)
			{
				UIManagerControl.Instance.ShowToastText("您被" + down.guildName + "军团拒绝了");
			}
		}
	}

	private void OnGuildBuildRes(short state, GuildBuildRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.MyGuildnfo != null)
		{
			this.m_guildInfo.guildFund = down.fund;
			this.m_memberInfo.contribution = down.contribution;
			if (this.m_guildMemberList != null)
			{
				int num = this.m_guildMemberList.FindIndex((MemberInfo a) => a.roleId == this.m_memberInfo.roleId);
				if (num >= 0)
				{
					this.m_guildMemberList.get_Item(num).contribution = down.contribution;
				}
			}
			if (down.buildType == GuildBuildType.GBT.GUILD_TASK)
			{
				this.IsInGuildTask = true;
				this.m_guildInfo.taskedCount = down.builtedCount;
				string tipContentByKey = this.GetTipContentByKey("ExecuteWord");
				DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), tipContentByKey, delegate
				{
					EventDispatcher.Broadcast(EventNames.OnDissolveGuildRes);
				}, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
			}
			else if (down.buildType == GuildBuildType.GBT.GUILD_DONATE)
			{
				string tipContentByKey2 = this.GetTipContentByKey("DonateWord");
				UIManagerControl.Instance.ShowToastText(tipContentByKey2);
				this.m_guildInfo.builtedCount = down.builtedCount;
			}
			EventDispatcher.Broadcast(EventNames.OnGuildBuildRes);
			EventDispatcher.Broadcast(EventNames.SortTaskList);
		}
	}

	private void OnGuildFieldOpenStatusNty(short state, GuildFieldOpenStatusNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.IsGuildFieldOpen = down.isOpen;
		ActivityCenterManager.Instance.ChangeGuildFieldActivityTip(down.isOpen);
		if (!this.IsGuildFieldOpen && MySceneManager.Instance.IsCurrentGuildFieldScene && UIManagerControl.Instance.IsOpen("TownUI"))
		{
			this.ShowForceExitGuildFieldUI();
		}
	}

	private void OnGuildAttrNty(short state, GuildAttrNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.MyGuildnfo != null)
		{
			switch (down.attrType)
			{
			case GuildAttrNty.AttrType.contribute:
				if (this.MyMemberInfo != null)
				{
					this.MyMemberInfo.contribution = (int)down.value;
					if (this.MyGuildMemberList != null)
					{
						int num = this.MyGuildMemberList.FindIndex((MemberInfo a) => a.roleId == this.MyMemberInfo.roleId);
						if (num >= 0)
						{
							this.MyGuildMemberList.get_Item(num).contribution = (int)down.value;
						}
					}
					EventDispatcher.Broadcast(EventNames.UpdateGuildInfo);
				}
				break;
			case GuildAttrNty.AttrType.equipEssence:
				this.MyGuildnfo.equipEssence = down.value;
				EventDispatcher.Broadcast(EventNames.OnGuildEquipEssenceNty);
				break;
			case GuildAttrNty.AttrType.activity:
				this.MyGuildnfo.activity = down.value;
				EventDispatcher.Broadcast(EventNames.OnGuildActivityNty);
				break;
			case GuildAttrNty.AttrType.guildFund:
				this.MyGuildnfo.guildFund = (int)down.value;
				break;
			case GuildAttrNty.AttrType.guildLv:
				this.MyGuildnfo.lv = (int)down.value;
				EventDispatcher.Broadcast(EventNames.UpdateGuildInfo);
				break;
			}
		}
	}

	private void OnEquipSmeltInfoPush(short state, EquipSmeltInfoPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.mEquipSmeltInfoPush = down;
		}
	}

	private void OnSmeltEquipRes(short state, SmeltEquipRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EquipmentManager.Instance.RemoveEquipIds(down.equipIds);
			if (UIManagerControl.Instance.IsOpen("GuildStoveUI"))
			{
				GuildStoveUIViewModel.Instance.RefreshUI();
				GuildStoveUI.Instance.PlaySpineSmelt(delegate
				{
					List<int> list = new List<int>();
					List<long> list2 = new List<long>();
					List<long> list3 = new List<long>();
					for (int i = 0; i < down.itemInfo.get_Count(); i++)
					{
						list.Add(down.itemInfo.get_Item(i).cfgId);
						list2.Add(down.itemInfo.get_Item(i).count);
						list3.Add(down.itemInfo.get_Item(i).uId);
					}
					RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
					rewardUI.SetRewardItem("熔炼返回物品", list, list2, true, false, null, list3);
				});
			}
		}
	}

	private void OnBuildEquipRes(short state, BuildEquipRes down = null)
	{
		this.IsBuildEquipWaitting = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && UIManagerControl.Instance.IsOpen("GuildStoveUI"))
		{
			GuildStoveUIViewModel.Instance.RefreshUI();
			this.mBuildSuccessEquipUUID = down.equipId;
			GuildStoveUI.Instance.PlaySpineBuild(delegate
			{
				this.PlaySpine4BuildSuccess();
				UIManagerControl.Instance.OpenUI("EquipDetailedPopUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
				EquipDetailedPopUI.Instance.SetBuildSuccess(this.mBuildSuccessEquipUUID);
			});
		}
	}

	private void PlaySpine4BuildSuccess()
	{
		if (this.mBuildSuccessDelayId <= 0u)
		{
			this.mBuildSuccessFXId1 = FXSpineManager.Instance.PlaySpine(4201, UINodesManager.MiddleUIRoot, string.Empty, 3002, null, "UI", 0f, 200f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.mBuildSuccessFXId2 = FXSpineManager.Instance.PlaySpine(4202, UINodesManager.MiddleUIRoot, string.Empty, 3001, null, "UI", 0f, 200f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.mBuildSuccessDelayId = TimerHeap.AddTimer(2000u, 0, new Action(this.CloseBuildSuccessSpine));
		}
	}

	private void CloseBuildSuccessSpine()
	{
		if (this.mBuildSuccessDelayId > 0u)
		{
			FXSpineManager.Instance.DeleteSpine(this.mBuildSuccessFXId1, true);
			FXSpineManager.Instance.DeleteSpine(this.mBuildSuccessFXId2, true);
			if (UIManagerControl.Instance.IsOpen("GuildStoveUI"))
			{
				FXSpineManager.Instance.PlaySpine(4203, UINodesManager.MiddleUIRoot, string.Empty, 3001, null, "UI", 0f, 200f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			TimerHeap.DelTimer(this.mBuildSuccessDelayId);
			this.mBuildSuccessDelayId = 0u;
		}
	}

	public bool IsJoinInGuild()
	{
		return this.m_guildInfo != null;
	}

	public string GetTitleName(MemberTitleType.MTT title)
	{
		string result = string.Empty;
		switch (title)
		{
		case MemberTitleType.MTT.Normal:
			result = GameDataUtils.GetChineseContent(515101, false);
			break;
		case MemberTitleType.MTT.Chairman:
			result = GameDataUtils.GetChineseContent(515102, false);
			break;
		case MemberTitleType.MTT.ViceChairman:
			result = GameDataUtils.GetChineseContent(515103, false);
			break;
		case MemberTitleType.MTT.Manager:
			result = GameDataUtils.GetChineseContent(515104, false);
			break;
		}
		return result;
	}

	public string GetOffLineStatus(int offlineSec)
	{
		if (offlineSec < 0)
		{
			return GameDataUtils.GetChineseContent(506016, false);
		}
		int num = offlineSec / 60;
		if (num <= 0)
		{
			return GameDataUtils.GetChineseContent(506017, false);
		}
		if (num > 0 && num < 60)
		{
			return GameDataUtils.GetChineseContent(506017, false) + num + GameDataUtils.GetChineseContent(509002, false);
		}
		if (num >= 60 && num < 1440)
		{
			int num2 = num / 60;
			return GameDataUtils.GetChineseContent(506017, false) + num2 + GameDataUtils.GetChineseContent(509001, false);
		}
		if (num >= 1440)
		{
			int num3 = num / 1440;
			return GameDataUtils.GetChineseContent(506017, false) + num3 + GameDataUtils.GetChineseContent(509000, false);
		}
		return GameDataUtils.GetChineseContent(506017, false);
	}

	public bool CheckMemberHasPrivilege(GuildPrivilegeState state)
	{
		if (this.m_memberInfo != null)
		{
			string key = string.Empty;
			switch (this.m_memberInfo.title.get_Item(0))
			{
			case MemberTitleType.MTT.Normal:
				key = "memberprovince";
				break;
			case MemberTitleType.MTT.Chairman:
				key = "chairmanprovince";
				break;
			case MemberTitleType.MTT.ViceChairman:
				key = "vicechairmanprovince";
				break;
			case MemberTitleType.MTT.Manager:
				key = "directorprovince";
				break;
			}
			string value = DataReader<GongHuiXinXi>.Get(key).value;
			string[] array = value.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				int num = (int)float.Parse(array[i]);
				if (state == (GuildPrivilegeState)num)
				{
					return true;
				}
			}
		}
		return false;
	}

	public string GetTipContentByKey(string key)
	{
		string empty = string.Empty;
		if (DataReader<GongHuiXinXi>.Get(key) == null)
		{
			return empty;
		}
		string value = DataReader<GongHuiXinXi>.Get(key).value;
		int id = (int)float.Parse(value);
		return GameDataUtils.GetChineseContent(id, false);
	}

	public string GetGuildTitle()
	{
		if (this.m_guildInfo == null || this.m_memberInfo == null || this.m_memberInfo.title.get_Count() == 0 || this.IsHideTitle)
		{
			return string.Empty;
		}
		return HeadInfoManager.GetGuildTitle(this.m_guildInfo.name, (int)this.m_memberInfo.title.get_Item(0));
	}

	private void GuildTitleNotify()
	{
		if (EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null)
		{
			EventDispatcher.Broadcast<long, string>("BillboardManager.GuildTitle", EntityWorld.Instance.EntSelf.ID, this.GetGuildTitle());
		}
	}

	public long GetGuildId()
	{
		if (this.m_guildInfo != null)
		{
			return this.m_guildInfo.guildId;
		}
		return 0L;
	}

	public string GetGuildName()
	{
		if (this.m_guildInfo != null)
		{
			return this.m_guildInfo.name;
		}
		return string.Empty;
	}

	public int GetGuildLevel()
	{
		if (this.m_guildInfo != null)
		{
			return this.m_guildInfo.lv;
		}
		return 0;
	}

	public int GetGuildSDKTitleID()
	{
		if (this.MyGuildnfo != null && this.MyMemberInfo != null && this.MyMemberInfo.title != null && this.MyMemberInfo.title.get_Count() > 0)
		{
			if (this.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Chairman)
			{
				return 1;
			}
			if (this.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.ViceChairman)
			{
				return 2;
			}
			if (this.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Manager)
			{
				return 3;
			}
			if (this.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Normal)
			{
				return 4;
			}
		}
		return 0;
	}

	public string GetGuilSDKdTitleName()
	{
		if (this.MyGuildnfo != null && this.MyMemberInfo != null && this.MyMemberInfo.title != null && this.MyMemberInfo.title.get_Count() > 0)
		{
			return this.GetTitleName(this.MyMemberInfo.title.get_Item(0));
		}
		return "无";
	}

	public void ShowForceExitGuildFieldUI()
	{
		DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(515052, false), delegate
		{
			EventDispatcher.Broadcast(CityManagerEvent.ExitGuildField);
		}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1", null);
		DialogBoxUIView.Instance.isClick = false;
		this.autoExitTimer = TimerHeap.AddTimer(5000u, 0, delegate
		{
			TimerHeap.DelTimer(this.autoExitTimer);
			DialogBoxUIViewModel.Instance.Close();
			EventDispatcher.Broadcast(CityManagerEvent.ExitGuildField);
		});
	}

	public bool IsGuildCaptain(long roleID)
	{
		return this.MyGuildnfo != null && this.MyGuildnfo.chairmanId == roleID;
	}

	public MemberInfo GetMyGuildMemberInfoByRoleID(long roleID)
	{
		MemberInfo memberInfo = null;
		if (this.MyGuildMemberList != null)
		{
			for (int i = 0; i < this.MyGuildMemberList.get_Count(); i++)
			{
				memberInfo = this.MyGuildMemberList.get_Item(i);
				long roleId = memberInfo.roleId;
				if (roleId == roleID)
				{
					return memberInfo;
				}
			}
		}
		return memberInfo;
	}

	public int GetEquipSmeltDayFund()
	{
		if (this.mEquipSmeltInfoPush != null)
		{
			return this.mEquipSmeltInfoPush.dayFund;
		}
		return 0;
	}

	public int GetEquipBuildDayTimes()
	{
		if (this.mEquipSmeltInfoPush != null)
		{
			return this.mEquipSmeltInfoPush.dayBuildTimes;
		}
		return 0;
	}

	public string GetGuildFieldOpenTime()
	{
		string empty = string.Empty;
		if (!DataReader<GongHuiXinXi>.Contains("QuestionTime"))
		{
			return empty;
		}
		string[] array = DataReader<GongHuiXinXi>.Get("QuestionTime").value.Split(new char[]
		{
			','
		});
		string[] array2 = array[0].Split(new char[]
		{
			':'
		});
		int num = int.Parse((!array2[0].StartsWith("0")) ? array2[0] : array2[0].Substring(1));
		int num2 = int.Parse((!array2[1].StartsWith("0")) ? array2[1] : array2[1].Substring(1));
		DateTime dateTime = new DateTime(DateTime.get_Now().get_Year(), DateTime.get_Now().get_Month(), DateTime.get_Now().get_Day(), num, num2, 0);
		string[] array3 = array[1].Split(new char[]
		{
			':'
		});
		int num3 = int.Parse((!array3[0].StartsWith("0")) ? array3[0] : array3[0].Substring(1));
		int num4 = int.Parse((!array3[1].StartsWith("0")) ? array3[1] : array3[1].Substring(1));
		DateTime dateTime2 = new DateTime(DateTime.get_Now().get_Year(), DateTime.get_Now().get_Month(), DateTime.get_Now().get_Day(), num3, num4, 0);
		return array[0] + "--" + array[1];
	}

	private void ClearGuildInfo()
	{
		this.m_guildInfo = null;
		this.m_memberInfo = null;
		this.m_guildMemberList = null;
		this.m_OwnApplicationGuilds = null;
		this.m_ApplicationPlayers = null;
	}
}
