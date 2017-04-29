using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class GuildWarManager : BaseSubSystemManager
{
	public enum MineState
	{
		None,
		My,
		Enemy,
		NoData
	}

	public struct MineInfo
	{
		public int id;

		public GuildWarManager.MineState state;

		public string ownerName;
	}

	private List<GuildParticipantInfo> qualitificationGuildInfoList;

	private GuildParticipantInfo myGuildParticipantInfo;

	private WeekVsInfosRes lastWeekVSInfoData;

	private WeekVsInfosRes thisWeekVSInfoData;

	private List<GuildMemberInfoInGuildWarScene> myGuildMemberInSceneList;

	private GuildId2Name enemyGuildIDAndName;

	public GuildWarTimeStep.GWTS GuildWarTimeStep;

	public bool IsMyGuildInSceneLeft;

	public bool IsCanGetChampionPrize;

	public int GuildWarMatchEndUtc;

	private static GuildWarManager instance;

	public GetGuildWarChampionRes GuildWarChampionInfo;

	public int LastBattleNo;

	public int BattleNo;

	private TimeCountDown countDown;

	protected TownUI townUI;

	protected BattleUI battleUI;

	protected int MaxResouceNum;

	protected GuildResourceInfo myGuildWarResourceInfo;

	protected GuildResourceInfo enemyGuildWarResourceInfo;

	protected int myGuildPeopleCount = -1;

	protected int enemyGuildPeopleCount = -1;

	protected int roleTotalResourceNum;

	protected int roleInFieldResourceNum;

	protected int roleKillCount;

	private XDict<int, GuildWarResBriefNty.GuildWarResBrief> guildWarResourceBriefDic;

	protected int mineEnteredID;

	protected long roleHp = -1L;

	protected int roleReliveTime = -1;

	protected bool isNavToMine;

	protected int navToMineID;

	protected int MaxReportNum = 10;

	protected List<string> reportCache = new List<string>();

	public List<GuildParticipantInfo> QualitificationGuildInfoList
	{
		get
		{
			return this.qualitificationGuildInfoList;
		}
	}

	public GuildParticipantInfo MyGuildParticipantInfo
	{
		get
		{
			return this.myGuildParticipantInfo;
		}
	}

	public WeekVsInfosRes LastWeekVSInfoData
	{
		get
		{
			return this.lastWeekVSInfoData;
		}
	}

	public WeekVsInfosRes ThisWeekVSInfoData
	{
		get
		{
			return this.thisWeekVSInfoData;
		}
	}

	public List<GuildMemberInfoInGuildWarScene> MyGuildMemberInSceneList
	{
		get
		{
			if (this.myGuildMemberInSceneList != null)
			{
				this.myGuildMemberInSceneList.Sort(new Comparison<GuildMemberInfoInGuildWarScene>(GuildWarManager.GuildMemberResourceSortCompare));
			}
			return this.myGuildMemberInSceneList;
		}
	}

	public static GuildWarManager Instance
	{
		get
		{
			if (GuildWarManager.instance == null)
			{
				GuildWarManager.instance = new GuildWarManager();
			}
			return GuildWarManager.instance;
		}
	}

	public long MyGuildID
	{
		get
		{
			return GuildManager.Instance.GetGuildId();
		}
	}

	public string MyGuildName
	{
		get
		{
			return GuildManager.Instance.GetGuildName();
		}
	}

	public long EnemyGuildID
	{
		get
		{
			return (this.enemyGuildIDAndName != null) ? this.enemyGuildIDAndName.guildId : 0L;
		}
	}

	public string EnemyGuildName
	{
		get
		{
			return (this.enemyGuildIDAndName != null) ? this.enemyGuildIDAndName.guildName : string.Empty;
		}
	}

	public GuildResourceInfo MyGuildWarResourceInfo
	{
		get
		{
			return this.myGuildWarResourceInfo;
		}
	}

	public GuildResourceInfo EnemyGuildWarResourceInfo
	{
		get
		{
			return this.enemyGuildWarResourceInfo;
		}
		set
		{
			this.enemyGuildWarResourceInfo = value;
		}
	}

	public int MyGuildPeopleCount
	{
		get
		{
			return this.myGuildPeopleCount;
		}
		protected set
		{
			this.myGuildPeopleCount = value;
			if (this.myGuildWarResourceInfo != null)
			{
				this.myGuildWarResourceInfo.GuildMemberNum = value;
			}
		}
	}

	public int EnemyGuildPeopleCount
	{
		get
		{
			return this.enemyGuildPeopleCount;
		}
		protected set
		{
			this.enemyGuildPeopleCount = value;
			if (this.enemyGuildWarResourceInfo != null)
			{
				this.enemyGuildWarResourceInfo.GuildMemberNum = value;
			}
		}
	}

	public int RoleTotalResourceNum
	{
		get
		{
			return this.roleTotalResourceNum;
		}
		set
		{
			this.roleTotalResourceNum = value;
			this.TryUpdateRoleTotalResource(value);
			this.TryUpdateRoleInFieldResource(value);
			this.TryUpdateRoleKillCount(this.RoleKillCount, value);
		}
	}

	public int RoleInFieldResourceNum
	{
		get
		{
			return this.roleInFieldResourceNum;
		}
		set
		{
			this.roleInFieldResourceNum = value;
		}
	}

	public int RoleKillCount
	{
		get
		{
			return this.roleKillCount;
		}
		set
		{
			this.roleKillCount = value;
			this.TryUpdateRoleKillCount(value, this.RoleTotalResourceNum);
		}
	}

	public XDict<int, GuildWarResBriefNty.GuildWarResBrief> GuildWarResourceBriefDic
	{
		get
		{
			return this.guildWarResourceBriefDic;
		}
	}

	protected int MineEnteredID
	{
		get
		{
			return this.mineEnteredID;
		}
		set
		{
			this.mineEnteredID = value;
		}
	}

	public bool IsWaitForUI
	{
		get
		{
			return this.mineEnteredID > 0;
		}
	}

	public long RoleHp
	{
		get
		{
			return this.roleHp;
		}
		set
		{
			this.roleHp = value;
		}
	}

	protected int RoleReliveTime
	{
		get
		{
			return this.roleReliveTime;
		}
		set
		{
			this.roleReliveTime = value;
		}
	}

	protected bool IsNavToMine
	{
		get
		{
			return this.isNavToMine;
		}
		set
		{
			this.isNavToMine = value;
		}
	}

	protected int NavToMineID
	{
		get
		{
			return this.navToMineID;
		}
		set
		{
			this.navToMineID = value;
		}
	}

	public List<string> ReportCache
	{
		get
		{
			return this.reportCache;
		}
	}

	public override void Init()
	{
		this.MaxResouceNum = (int)float.Parse(DataReader<JunTuanZhanXinXi>.Get("ResourcesLimit").value);
		this.MaxReportNum = (int)float.Parse(DataReader<JunTuanZhanXinXi>.Get("Report").value);
		this.guildWarResourceBriefDic = new XDict<int, GuildWarResBriefNty.GuildWarResBrief>();
		for (int i = 1; i <= 5; i++)
		{
			GuildWarResBriefNty.GuildWarResBrief guildWarResBrief = new GuildWarResBriefNty.GuildWarResBrief();
			guildWarResBrief.resourceId = i;
			guildWarResBrief.myMemberCount = 0;
			guildWarResBrief.faceMemberCount = 0;
			guildWarResBrief.ownerGuildId = 0L;
			this.guildWarResourceBriefDic.Add(i, guildWarResBrief);
		}
		this.LastBattleNo = 0;
		base.Init();
	}

	public override void Release()
	{
		this.qualitificationGuildInfoList = null;
		this.myGuildParticipantInfo = null;
		this.lastWeekVSInfoData = null;
		this.thisWeekVSInfoData = null;
		this.myGuildWarResourceInfo = null;
		this.enemyGuildWarResourceInfo = null;
		this.guildWarResourceBriefDic = null;
		this.enemyGuildIDAndName = null;
		this.GuildWarChampionInfo = null;
		if (this.reportCache != null)
		{
			this.reportCache.Clear();
		}
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GuildWarLoginPush>(new NetCallBackMethod<GuildWarLoginPush>(this.OnGuildWarLoginPush));
		NetworkManager.AddListenEvent<GuildWarMatchEndNty>(new NetCallBackMethod<GuildWarMatchEndNty>(this.OnGuildWarMatchEndNty));
		NetworkManager.AddListenEvent<GuildWarStepNty>(new NetCallBackMethod<GuildWarStepNty>(this.OnGuildWarStepNty));
		NetworkManager.AddListenEvent<EligibilityGuildInfoRes>(new NetCallBackMethod<EligibilityGuildInfoRes>(this.OnEligibilityGuildInfoRes));
		NetworkManager.AddListenEvent<WeekVsInfosRes>(new NetCallBackMethod<WeekVsInfosRes>(this.OnWeekVsInfosRes));
		NetworkManager.AddListenEvent<GetMemberInGuildWarRes>(new NetCallBackMethod<GetMemberInGuildWarRes>(this.OnGetMemberInGuildWarRes));
		NetworkManager.AddListenEvent<EnterWaitingRoomRes>(new NetCallBackMethod<EnterWaitingRoomRes>(this.OnEnterWaitingRoomRes));
		NetworkManager.AddListenEvent<LeaveWaitingRoomRes>(new NetCallBackMethod<LeaveWaitingRoomRes>(this.OnLeaveWaitingRoomRes));
		NetworkManager.AddListenEvent<EnterGuildBattleRes>(new NetCallBackMethod<EnterGuildBattleRes>(this.OnEnterGuildBattleRes));
		NetworkManager.AddListenEvent<LeaveGuildBattleRes>(new NetCallBackMethod<LeaveGuildBattleRes>(this.OnLeaveGuildBattleRes));
		NetworkManager.AddListenEvent<ReliveInGuildWarRes>(new NetCallBackMethod<ReliveInGuildWarRes>(this.OnReliveInGuildWarRes));
		NetworkManager.AddListenEvent<GuildWarSceneRoleInfoNty>(new NetCallBackMethod<GuildWarSceneRoleInfoNty>(this.OnGuildWarSceneRoleInfoNty));
		NetworkManager.AddListenEvent<GuildWarRoleResourceNty>(new NetCallBackMethod<GuildWarRoleResourceNty>(this.OnGuildWarRoleResourceNty));
		NetworkManager.AddListenEvent<GuildWarResourceInfoNty>(new NetCallBackMethod<GuildWarResourceInfoNty>(this.OnGuildWarResourceInfoNty));
		NetworkManager.AddListenEvent<GuildWarResBriefNty>(new NetCallBackMethod<GuildWarResBriefNty>(this.OnGuildWarResBriefNty));
		NetworkManager.AddListenEvent<GetGuildWarChampionDailyPrizeRes>(new NetCallBackMethod<GetGuildWarChampionDailyPrizeRes>(this.OnGetGuildWarChampionDailyPrizeRes));
		NetworkManager.AddListenEvent<GetGuildWarChampionRes>(new NetCallBackMethod<GetGuildWarChampionRes>(this.OnGetGuildWarChampionRes));
		NetworkManager.AddListenEvent<GuildWarMisNty>(new NetCallBackMethod<GuildWarMisNty>(this.OnGuildWarMisNty));
		NetworkManager.AddListenEvent<GuildBattleWarResultNty>(new NetCallBackMethod<GuildBattleWarResultNty>(this.OnGuildBattleWarResultNty));
		NetworkManager.AddListenEvent<GuildMatchResultNty>(new NetCallBackMethod<GuildMatchResultNty>(this.OnGuildMatchResultNty));
		EventDispatcher.AddListener(SceneManagerEvent.ClearSceneDependentLogic, new Callback(this.ClearSceneDependentLogic));
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
		EventDispatcher.AddListener<int>(GuildWarMineNPCBehavior.OnEnterNPC, new Callback<int>(this.OnEnterMineNPC));
		EventDispatcher.AddListener<int>(GuildWarMineNPCBehavior.OnExitNPC, new Callback<int>(this.OnExitMineNPC));
		NetworkManager.AddListenEvent<GuildWarLogNty>(new NetCallBackMethod<GuildWarLogNty>(this.OnGuildReportNty));
	}

	public void SendEligibilityGuildInfoReq()
	{
		NetworkManager.Send(new EligibilityGuildInfoReq(), ServerType.Data);
	}

	public void SendWeekVsInfosReq(int m_week = 0)
	{
		NetworkManager.Send(new WeekVsInfosReq
		{
			week = m_week
		}, ServerType.Data);
	}

	public void SendGetMemberInGuildReq(int resourceID = -1)
	{
		NetworkManager.Send(new GetMemberInGuildWarReq
		{
			resourceId = resourceID
		}, ServerType.Data);
	}

	public void SendEnterWaitingRoomReq()
	{
		NetworkManager.Send(new EnterWaitingRoomReq(), ServerType.Data);
	}

	public void SendLeaveWaitingRoomReq()
	{
		NetworkManager.Send(new LeaveWaitingRoomReq(), ServerType.Data);
	}

	public void SendEnterGuildBattleReq(int battleNum)
	{
		Debug.Log("battleNum=============" + battleNum);
		this.BattleNo = battleNum;
		NetworkManager.Send(new EnterGuildBattleReq
		{
			battleNo = battleNum
		}, ServerType.Data);
	}

	public void SendReliveInGuildWarReq()
	{
		NetworkManager.Send(new ReliveInGuildWarReq(), ServerType.Data);
	}

	public void SendLeaveGuildBattleReq()
	{
		NetworkManager.Send(new LeaveGuildBattleReq(), ServerType.Data);
	}

	public void SendGetGuildWarChampionDailyPrizeReq()
	{
		NetworkManager.Send(new GetGuildWarChampionDailyPrizeReq(), ServerType.Data);
	}

	public void SendGetGuildWarChampionReq()
	{
		NetworkManager.Send(new GetGuildWarChampionReq(), ServerType.Data);
	}

	private void OnGuildWarLoginPush(short state, GuildWarLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsMyGuildInSceneLeft = (down.bornPointId == 8);
			this.RoleHp = down.hp;
			this.RoleReliveTime = down.reliveCD;
			for (int i = 0; i < down.guildId2Name.get_Count(); i++)
			{
				GuildId2Name guildId2Name = down.guildId2Name.get_Item(i);
				if (GuildManager.Instance.MyGuildnfo == null || GuildManager.Instance.MyGuildnfo.guildId != guildId2Name.guildId)
				{
					this.enemyGuildIDAndName = guildId2Name;
					Debug.Log("enemyGuildIDAndName=========" + this.enemyGuildIDAndName.guildName);
					this.UpdateEnemyGuildName();
				}
			}
			this.TryUpdateHp();
		}
	}

	private void OnGuildWarMatchEndNty(short state, GuildWarMatchEndNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.GuildWarMatchEndUtc = down.endUtc;
		}
	}

	private void OnGuildWarStepNty(short state, GuildWarStepNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.GuildWarTimeStep = down.step;
			if (this.GuildWarTimeStep != Package.GuildWarTimeStep.GWTS.HALF_MATCH2_BEG && this.GuildWarTimeStep != Package.GuildWarTimeStep.GWTS.HALF_MATCH1_BEG && this.GuildWarTimeStep != Package.GuildWarTimeStep.GWTS.FINAL_MATCH_BEG)
			{
				this.ClearGuildWarBattleData();
			}
			ActivityCenterManager.Instance.ChangeGuildWarActivityTip(down.step);
			Debug.Log("GuildWarStepNty-------活动时间阶段:  " + down.step);
			EventDispatcher.Broadcast(EventNames.OnGuildWarStepNty);
		}
	}

	private void OnGuildWarMisNty(short state, GuildWarMisNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsCanGetChampionPrize = down.championDailyPrize;
			EventDispatcher.Broadcast(EventNames.OnGuildWarMisNty);
		}
	}

	private void OnGetGuildWarChampionDailyPrizeRes(short state, GetGuildWarChampionDailyPrizeRes down = null)
	{
		if ((int)state == Status.GUILD_WAR_NOT_CHAMPION_MEMBER)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515084, false));
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsCanGetChampionPrize = false;
			List<int> list = new List<int>();
			List<long> list2 = new List<long>();
			List<long> list3 = new List<long>();
			for (int i = 0; i < down.itemInfo.get_Count(); i++)
			{
				int cfgId = down.itemInfo.get_Item(i).cfgId;
				long count = down.itemInfo.get_Item(i).count;
				list.Add(cfgId);
				list2.Add(count);
				list3.Add(down.itemInfo.get_Item(i).uId);
			}
			if (list != null && list.get_Count() > 0)
			{
				RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
				rewardUI.get_transform().SetAsLastSibling();
				rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
			}
			EventDispatcher.Broadcast(EventNames.OnGuildWarMisNty);
		}
	}

	private void OnGetGuildWarChampionRes(short state, GetGuildWarChampionRes down = null)
	{
		if (state != 0)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"状态错误：错误码为: ",
				state,
				" ",
				Status.GetStatusDesc((int)state)
			}));
		}
		if (state == 0)
		{
			this.GuildWarChampionInfo = down;
			EventDispatcher.Broadcast(EventNames.OnGetGuildWarChampionRes);
		}
	}

	private void OnEligibilityGuildInfoRes(short state, EligibilityGuildInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.qualitificationGuildInfoList = down.guildInfo;
			if (GuildManager.Instance.IsJoinInGuild())
			{
				this.myGuildParticipantInfo = down.ownerGuildInfo;
			}
			EventDispatcher.Broadcast(EventNames.OnEligibilityGuildInfoRes);
		}
	}

	private void OnWeekVsInfosRes(short state, WeekVsInfosRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.week == 0)
			{
				this.thisWeekVSInfoData = down;
			}
			else if (down.week == 1)
			{
				this.lastWeekVSInfoData = down;
			}
			EventDispatcher.Broadcast<int>(EventNames.OnWeekVsInfosRes, down.week);
		}
	}

	private void OnGetMemberInGuildWarRes(short state, GetMemberInGuildWarRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.inResourceId == -1)
			{
				this.myGuildMemberInSceneList = null;
				this.myGuildMemberInSceneList = new List<GuildMemberInfoInGuildWarScene>();
				for (int i = 0; i < down.myMembersInfo.get_Count(); i++)
				{
					MemberInGuildScene memberInGuildScene = down.myMembersInfo.get_Item(i);
					long roleId = memberInGuildScene.memberInfo.roleId;
					GuildMemberInfoInGuildWarScene guildMemberInfoInGuildWarScene = new GuildMemberInfoInGuildWarScene(roleId);
					guildMemberInfoInGuildWarScene.ResourceNum = memberInGuildScene.resValue;
					guildMemberInfoInGuildWarScene.RoleFighting = memberInGuildScene.memberInfo.fighting;
					guildMemberInfoInGuildWarScene.RoleLv = memberInGuildScene.memberInfo.lv;
					guildMemberInfoInGuildWarScene.Status = memberInGuildScene.status;
					guildMemberInfoInGuildWarScene.RoleName = memberInGuildScene.memberInfo.name;
					this.myGuildMemberInSceneList.Add(guildMemberInfoInGuildWarScene);
				}
				EventDispatcher.Broadcast(EventNames.OnGetMyGuildMembersInGuildWar);
			}
			else
			{
				EventDispatcher.Broadcast<GetMemberInGuildWarRes>(EventNames.OnUpdateCurrentResourceInfo, down);
			}
		}
	}

	private void OnEnterWaitingRoomRes(short state, EnterWaitingRoomRes down = null)
	{
		if ((int)state == Status.GUILD_WAR_JOIN_TIME_LIMIT)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515095, false));
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsMyGuildInSceneLeft = (down.bornPointId == 8);
			for (int i = 0; i < down.guildId2Name.get_Count(); i++)
			{
				GuildId2Name guildId2Name = down.guildId2Name.get_Item(i);
				if (GuildManager.Instance.MyGuildnfo == null || GuildManager.Instance.MyGuildnfo.guildId != guildId2Name.guildId)
				{
					this.enemyGuildIDAndName = guildId2Name;
					this.UpdateEnemyGuildName();
				}
			}
		}
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
	}

	private void OnLeaveWaitingRoomRes(short state, LeaveWaitingRoomRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.LastBattleNo = 0;
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
	}

	private void OnEnterGuildBattleRes(short state, EnterGuildBattleRes down = null)
	{
		if ((int)state == Status.GUILD_WAR_SOLDIER_NUM_LMT)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515096, false));
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.BattleNo = down.battleNo;
		this.LastBattleNo = down.battleNo;
		Debug.Log("down.battleNo----" + down.battleNo);
		this.RoleInFieldResourceNum = 0;
	}

	private void OnLeaveGuildBattleRes(short state, LeaveGuildBattleRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.LastBattleNo = this.BattleNo;
		this.BattleNo = 0;
		InstanceManager.ChangeInstanceManager(CityInstance.Instance, false);
	}

	private void OnReliveInGuildWarRes(short state, ReliveInGuildWarRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.HideUI("GuildWarReliveUI");
		}
	}

	private void OnGuildWarSceneRoleInfoNty(short state, GuildWarSceneRoleInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RoleHp = down.hp;
			this.RoleReliveTime = down.reliveCD;
			this.TryUpdateHp();
		}
	}

	private void OnGuildWarRoleResourceNty(short state, GuildWarRoleResourceNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RoleTotalResourceNum = down.totalResource;
			this.RoleInFieldResourceNum = down.fieldResource;
			this.RoleKillCount = down.killCount;
		}
	}

	private void OnGuildWarResourceInfoNty(short state, GuildWarResourceInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.guildResourceInfo.get_Count(); i++)
			{
				long guildId = down.guildResourceInfo.get_Item(i).guildId;
				if (GuildManager.Instance.MyGuildnfo != null && GuildManager.Instance.MyGuildnfo.guildId == guildId)
				{
					if (this.myGuildWarResourceInfo == null)
					{
						this.myGuildWarResourceInfo = new GuildResourceInfo(guildId, GuildManager.Instance.MyGuildnfo.name, this.MyGuildPeopleCount, down.guildResourceInfo.get_Item(i).resource, this.MaxResouceNum, this.IsMyGuildInSceneLeft);
					}
					else
					{
						this.myGuildWarResourceInfo.TotalResourceNum = down.guildResourceInfo.get_Item(i).resource;
					}
				}
				else if (this.enemyGuildWarResourceInfo == null)
				{
					this.enemyGuildWarResourceInfo = new GuildResourceInfo(guildId, this.GetEnemyGuildName(), this.EnemyGuildPeopleCount, down.guildResourceInfo.get_Item(i).resource, this.MaxResouceNum, !this.IsMyGuildInSceneLeft);
				}
				else
				{
					this.enemyGuildWarResourceInfo.TotalResourceNum = down.guildResourceInfo.get_Item(i).resource;
				}
			}
			this.TryUpdateGuildWarCityResouceInfo(this.myGuildWarResourceInfo, this.enemyGuildWarResourceInfo);
		}
	}

	private void OnGuildWarResBriefNty(short state, GuildWarResBriefNty down = null)
	{
		Debug.LogError("OnGuildWarResBriefNty");
		for (int i = 0; i < down.guildWarResBrief.get_Count(); i++)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"OnGuildWarResBriefNty: ",
				down.guildWarResBrief.get_Item(i).resourceId,
				" ",
				down.guildWarResBrief.get_Item(i).ownerGuildId
			}));
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.guildWarResourceBriefDic == null)
			{
				this.guildWarResourceBriefDic = new XDict<int, GuildWarResBriefNty.GuildWarResBrief>();
			}
			for (int j = 0; j < down.guildWarResBrief.get_Count(); j++)
			{
				int resourceId = down.guildWarResBrief.get_Item(j).resourceId;
				if (resourceId == 0)
				{
					this.MyGuildPeopleCount = down.guildWarResBrief.get_Item(j).myMemberCount;
					if (this.MyGuildWarResourceInfo != null)
					{
						this.MyGuildWarResourceInfo.GuildMemberNum = this.MyGuildPeopleCount;
					}
					this.EnemyGuildPeopleCount = down.guildWarResBrief.get_Item(j).faceMemberCount;
					if (this.EnemyGuildWarResourceInfo != null)
					{
						this.EnemyGuildWarResourceInfo.GuildMemberNum = this.EnemyGuildPeopleCount;
					}
				}
				else
				{
					if (this.guildWarResourceBriefDic.ContainsKey(resourceId))
					{
						this.guildWarResourceBriefDic[resourceId] = down.guildWarResBrief.get_Item(j);
					}
					else
					{
						this.guildWarResourceBriefDic.Add(resourceId, down.guildWarResBrief.get_Item(j));
					}
					Debug.Log(string.Concat(new object[]
					{
						"资源点 ：",
						resourceId,
						"号 当前是 ",
						down.guildWarResBrief.get_Item(j).faceMemberCount,
						" VS ",
						down.guildWarResBrief.get_Item(j).myMemberCount
					}));
				}
			}
			EventDispatcher.Broadcast(GuildWarManagerEvent.UpdateAllMineLiveData);
			this.TryUpdateGuildWarCityResouceInfo(this.myGuildWarResourceInfo, this.enemyGuildWarResourceInfo);
			this.TryUpdateMineInfo();
		}
	}

	private void OnGuildBattleWarResultNty(short state, GuildBattleWarResultNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.hp <= 0L && InstanceManager.CurrentInstanceType == InstanceType.GuildWar)
		{
			this.ShowDeadTipsInField(down.killerName);
		}
	}

	private void OnGuildMatchResultNty(short state, GuildMatchResultNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Debug.LogError("GuildMatchResultNty");
			if (InstanceManager.CurrentInstanceType == InstanceType.GuildWar || MySceneManager.Instance.IsCurrentGuildWarCityScene)
			{
				UIManagerControl.Instance.HideUI("GuildWarReliveUI");
				GuildWarResultUI guildWarResultUI = UIManagerControl.Instance.OpenUI("GuildWarResultUI", null, false, UIType.NonPush) as GuildWarResultUI;
				guildWarResultUI.RefreshUI(down);
			}
		}
	}

	private void ClearGuildWarBattleData()
	{
		if (this.GuildWarResourceBriefDic != null)
		{
			this.guildWarResourceBriefDic.Clear();
			for (int i = 1; i <= 5; i++)
			{
				GuildWarResBriefNty.GuildWarResBrief guildWarResBrief = new GuildWarResBriefNty.GuildWarResBrief();
				guildWarResBrief.resourceId = i;
				guildWarResBrief.myMemberCount = 0;
				guildWarResBrief.faceMemberCount = 0;
				guildWarResBrief.ownerGuildId = 0L;
				this.guildWarResourceBriefDic.Add(i, guildWarResBrief);
			}
		}
		if (this.reportCache != null)
		{
			this.reportCache.Clear();
		}
	}

	private static int GuildMemberResourceSortCompare(GuildMemberInfoInGuildWarScene guildmember1, GuildMemberInfoInGuildWarScene guildmember2)
	{
		int result;
		if (guildmember1.ResourceNum > guildmember2.ResourceNum)
		{
			result = -1;
		}
		else if (guildmember1.ResourceNum == guildmember2.ResourceNum && guildmember1.RoleFighting > guildmember2.RoleFighting)
		{
			result = -1;
		}
		else
		{
			result = 1;
		}
		return result;
	}

	private void ShowDeadTipsInField(string killName)
	{
		this.RemoveDieCountDown();
		string content = "少年，您被" + killName + "杀死了!";
		string content2 = content + "5秒后离开战斗";
		DialogBoxUIViewModel.Instance.ShowAsConfirm("死亡提示", content2, delegate
		{
			this.RemoveDieCountDown();
			GuildWarManager.Instance.SendLeaveGuildBattleReq();
		}, "立即离开", "button_orange_1", null);
		DialogBoxUIView.Instance.SetMask(0.7f, true, false);
		DialogBoxUIViewModel.Instance.SetActionClose(delegate
		{
			this.RemoveDieCountDown();
			GuildWarManager.Instance.SendLeaveGuildBattleReq();
		});
		this.countDown = new TimeCountDown(5, TimeFormat.SECOND, delegate
		{
			if (this.countDown != null && DialogBoxUIViewModel.Instance != null)
			{
				DialogBoxUIViewModel.Instance.Content = string.Concat(new object[]
				{
					content,
					string.Empty,
					this.countDown.GetSeconds(),
					"秒后离开战斗"
				});
			}
		}, delegate
		{
			this.RemoveDieCountDown();
			GuildWarManager.Instance.SendLeaveGuildBattleReq();
		}, true);
	}

	public void RemoveDieCountDown()
	{
		if (this.countDown != null)
		{
			this.countDown.Dispose();
			this.countDown = null;
		}
	}

	public string GetEnemyGuildName()
	{
		if (this.enemyGuildIDAndName != null)
		{
			return this.enemyGuildIDAndName.guildName;
		}
		return string.Empty;
	}

	public void UpdateEnemyGuildName()
	{
		if (this.enemyGuildWarResourceInfo != null)
		{
			this.enemyGuildWarResourceInfo.GuildName = this.GetEnemyGuildName();
			this.TryUpdateGuildWarCityResouceInfo(this.myGuildWarResourceInfo, this.enemyGuildWarResourceInfo);
		}
	}

	public string GetMemberInSceneStatusDes(int status)
	{
		if (status == 0)
		{
			return "空闲中";
		}
		if (status == -1)
		{
			return "死亡中";
		}
		return status + "号厅";
	}

	public string GetResourceAddNumDescByID(int resourceID)
	{
		JunTuanZhanDou junTuanZhanDou = DataReader<JunTuanZhanDou>.Get(resourceID);
		if (junTuanZhanDou != null)
		{
			int coordinateTime = junTuanZhanDou.CoordinateTime;
			int addResourceNumByID = this.GetAddResourceNumByID(resourceID);
			return string.Concat(new object[]
			{
				resourceID,
				"号矿洞：每",
				coordinateTime,
				"秒<color=#56cc2d>+",
				addResourceNumByID,
				"</color>点资源"
			});
		}
		return string.Empty;
	}

	public int GetVSGroupByRank(int rank)
	{
		switch (rank)
		{
		case 1:
		case 8:
			return 1;
		case 2:
		case 7:
			return 2;
		case 3:
		case 6:
			return 3;
		case 4:
		case 5:
			return 4;
		default:
			return 1;
		}
	}

	public int GetVSGroup2ByRank(int rank)
	{
		switch (rank)
		{
		case 1:
		case 4:
		case 5:
		case 8:
			return 1;
		case 2:
		case 3:
		case 6:
		case 7:
			return 2;
		default:
			return 1;
		}
	}

	public string GetGuildWarCurrentShowTime()
	{
		string empty = string.Empty;
		int competeIndex;
		if (GuildWarManager.Instance.GuildWarTimeStep >= Package.GuildWarTimeStep.GWTS.HALF_MATCH2_END && GuildWarManager.Instance.GuildWarTimeStep < Package.GuildWarTimeStep.GWTS.FINAL_MATCH_END)
		{
			competeIndex = 4;
		}
		else if (GuildWarManager.Instance.GuildWarTimeStep >= Package.GuildWarTimeStep.GWTS.HALF_MATCH1_END && GuildWarManager.Instance.GuildWarTimeStep < Package.GuildWarTimeStep.GWTS.HALF_MATCH2_END)
		{
			competeIndex = 3;
		}
		else
		{
			competeIndex = 2;
		}
		return this.GetGuildWarOpenTime(competeIndex, string.Empty);
	}

	public string GetGuildWarOpenTime(int competeIndex, string color)
	{
		string[] guildWarOpenTime = this.GetGuildWarOpenTime(competeIndex);
		if (guildWarOpenTime != null)
		{
			int num = int.Parse(guildWarOpenTime[0]);
			string text = GameDataUtils.GetChineseContent(513518 + num, false);
			if (color == string.Empty)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"：",
					guildWarOpenTime[1],
					"-",
					guildWarOpenTime[3]
				});
			}
			else
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"：<color=#",
					color,
					">",
					guildWarOpenTime[1],
					"-",
					guildWarOpenTime[3],
					"</color>"
				});
			}
			return text;
		}
		return string.Empty;
	}

	public string[] GetGuildWarOpenTime(int competeIndex)
	{
		JunTuanZhanShiJian junTuanZhanShiJian = DataReader<JunTuanZhanShiJian>.Get(competeIndex);
		if (junTuanZhanShiJian == null)
		{
			return null;
		}
		string openTime = junTuanZhanShiJian.OpenTime;
		string[] array = openTime.Split(new char[]
		{
			';'
		});
		string text = array[0].Replace('"', ' ');
		string[] array2 = text.Split(new char[]
		{
			','
		});
		string text2 = array[1].Replace('"', ' ');
		string[] array3 = text2.Split(new char[]
		{
			','
		});
		return new string[]
		{
			array2[0],
			array2[1],
			array3[0],
			array3[1]
		};
	}

	public int GetGuildWarMatchEndSecond()
	{
		if (this.GuildWarMatchEndUtc <= 0)
		{
			return 0;
		}
		int num = this.GuildWarMatchEndUtc - TimeManager.Instance.PreciseServerSecond;
		if (num >= 0)
		{
			return num;
		}
		return 0;
	}

	public int GetAddResourceNumByID(int resourceID)
	{
		JunTuanZhanDou junTuanZhanDou = DataReader<JunTuanZhanDou>.Get(resourceID);
		if (junTuanZhanDou != null)
		{
			int result = junTuanZhanDou.Resource;
			if (this.CheckResourceIsEnemy(resourceID))
			{
				result = junTuanZhanDou.Resource + junTuanZhanDou.AddResource;
			}
			return result;
		}
		return 0;
	}

	public bool CheckResourceIsEnemy(int resourceID)
	{
		if (resourceID == 1 || resourceID == 2 || resourceID == 3)
		{
			return !this.IsMyGuildInSceneLeft;
		}
		return this.IsMyGuildInSceneLeft;
	}

	public bool CheckCanJoinInGuildWar()
	{
		if (this.ThisWeekVSInfoData != null && GuildManager.Instance.MyGuildnfo != null)
		{
			if (this.GuildWarTimeStep >= Package.GuildWarTimeStep.GWTS.ELIGIBILITY && this.GuildWarTimeStep < Package.GuildWarTimeStep.GWTS.HALF_MATCH1_END)
			{
				for (int i = 0; i < this.ThisWeekVSInfoData.first8Infos.get_Count(); i++)
				{
					List<GuildParticipantInfo> guildsInfo = this.ThisWeekVSInfoData.first8Infos.get_Item(i).guildsInfo;
					for (int j = 0; j < guildsInfo.get_Count(); j++)
					{
						if (guildsInfo.get_Item(j).guildId == GuildManager.Instance.MyGuildnfo.guildId)
						{
							return true;
						}
					}
				}
			}
			else if (this.GuildWarTimeStep >= Package.GuildWarTimeStep.GWTS.HALF_MATCH1_END && this.GuildWarTimeStep < Package.GuildWarTimeStep.GWTS.HALF_MATCH2_END)
			{
				for (int k = 0; k < this.ThisWeekVSInfoData.second4Infos.get_Count(); k++)
				{
					List<GuildParticipantInfo> guildsInfo2 = this.ThisWeekVSInfoData.second4Infos.get_Item(k).guildsInfo;
					for (int l = 0; l < guildsInfo2.get_Count(); l++)
					{
						if (guildsInfo2.get_Item(l).guildId == GuildManager.Instance.MyGuildnfo.guildId)
						{
							return true;
						}
					}
				}
			}
			else if (this.GuildWarTimeStep >= Package.GuildWarTimeStep.GWTS.HALF_MATCH2_END && this.GuildWarTimeStep < Package.GuildWarTimeStep.GWTS.FINAL_MATCH_END)
			{
				for (int m = 0; m < this.ThisWeekVSInfoData.third2Infos.get_Count(); m++)
				{
					List<GuildParticipantInfo> guildsInfo3 = this.ThisWeekVSInfoData.third2Infos.get_Item(m).guildsInfo;
					for (int n = 0; n < guildsInfo3.get_Count(); n++)
					{
						if (guildsInfo3.get_Item(n).guildId == GuildManager.Instance.MyGuildnfo.guildId)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		return false;
	}

	public int GetMatch1ToMatch2RemainTime()
	{
		int num = 0;
		int num2 = 0;
		JunTuanZhanShiJian junTuanZhanShiJian = DataReader<JunTuanZhanShiJian>.Get(3);
		if (junTuanZhanShiJian != null)
		{
			string openTime = junTuanZhanShiJian.OpenTime;
			string[] array = openTime.Split(new char[]
			{
				';'
			});
			string text = array[0].Replace('"', ' ');
			string[] array2 = text.Split(new char[]
			{
				','
			});
			string[] array3 = array2[1].Split(new char[]
			{
				':'
			});
			num = (int)float.Parse(array3[0]);
			num2 = (int)float.Parse(array3[1]);
		}
		DateTime dateTime = new DateTime(TimeManager.Instance.PreciseServerTime.get_Year(), TimeManager.Instance.PreciseServerTime.get_Month(), TimeManager.Instance.PreciseServerTime.get_Day(), num, num2, 0);
		int num3 = (int)(dateTime - TimeManager.Instance.PreciseServerTime).get_TotalSeconds();
		return (num3 < 0) ? 0 : num3;
	}

	public int GetRewardWingID(int type)
	{
		JunTuanZhanJiangLi junTuanZhanJiangLi = DataReader<JunTuanZhanJiangLi>.Get(1);
		if (junTuanZhanJiangLi != null)
		{
			string wing = junTuanZhanJiangLi.wing;
			string[] array = wing.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					':'
				});
				int num = (int)float.Parse(array2[0]);
				if (num == type)
				{
					return (int)float.Parse(array2[1]);
				}
			}
		}
		return 0;
	}

	protected void TryUpdateGuildWarCityResouceInfo(GuildResourceInfo myGuildInfo, GuildResourceInfo enemyGuildInfo)
	{
		if (this.townUI == null)
		{
			this.townUI = (UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI);
		}
		if (this.townUI)
		{
			this.townUI.SetGuildWarCityResouceInfo(myGuildInfo, enemyGuildInfo);
		}
		if (this.battleUI == null)
		{
			this.battleUI = (UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI);
		}
		if (this.battleUI)
		{
			this.battleUI.SetGuildWarCityResouceInfo(myGuildInfo, enemyGuildInfo);
		}
	}

	protected void TryUpdateRoleTotalResource(int value)
	{
		if (this.townUI == null)
		{
			this.townUI = (UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI);
		}
		if (this.townUI == null)
		{
			return;
		}
		this.townUI.SetCurResoucePointNum(value.ToString());
	}

	protected void TryUpdateRoleInFieldResource(int value)
	{
		if (this.battleUI == null)
		{
			this.battleUI = (UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI);
		}
		if (this.battleUI == null)
		{
			return;
		}
		this.battleUI.SetRewardPreviewDefaultExpText((long)value);
	}

	protected void TryUpdateRoleKillCount(int killCount, int totalResourceNum)
	{
		MineAndReportUI mineAndReportUI = UIManagerControl.Instance.GetUIIfExist("MineAndReportUI") as MineAndReportUI;
		if (mineAndReportUI)
		{
			mineAndReportUI.SetKillData(killCount, totalResourceNum);
		}
	}

	public GuildWarManager.MineState GetMineState(int mineID)
	{
		if (this.GuildWarResourceBriefDic == null)
		{
			return GuildWarManager.MineState.NoData;
		}
		if (!this.GuildWarResourceBriefDic.ContainsKey(mineID))
		{
			return GuildWarManager.MineState.NoData;
		}
		if (this.GuildWarResourceBriefDic[mineID].ownerGuildId == 0L)
		{
			return GuildWarManager.MineState.None;
		}
		if (this.GuildWarResourceBriefDic[mineID].ownerGuildId == this.MyGuildID)
		{
			return GuildWarManager.MineState.My;
		}
		if (this.GuildWarResourceBriefDic[mineID].ownerGuildId == this.EnemyGuildID)
		{
			return GuildWarManager.MineState.Enemy;
		}
		return GuildWarManager.MineState.NoData;
	}

	public GuildWarManager.MineState GetCurBattleMineState()
	{
		return this.GetMineState(this.BattleNo);
	}

	protected string GetMineOwnerName(int mineID)
	{
		return this.GetMineOwnerName(this.GetMineState(mineID));
	}

	protected string GetMineOwnerName(GuildWarManager.MineState state)
	{
		switch (state)
		{
		case GuildWarManager.MineState.None:
			return GameDataUtils.GetChineseContent(515110, false);
		case GuildWarManager.MineState.My:
			return TextColorMgr.GetColor(this.MyGuildName, "6adc32", string.Empty);
		case GuildWarManager.MineState.Enemy:
			return TextColorMgr.GetColor(this.EnemyGuildName, "ff4040", string.Empty);
		default:
			return GameDataUtils.GetChineseContent(515110, false);
		}
	}

	public List<string> GetMineLiveData(int mineID)
	{
		if (this.GuildWarResourceBriefDic == null)
		{
			return null;
		}
		if (!this.GuildWarResourceBriefDic.ContainsKey(mineID))
		{
			return null;
		}
		List<string> list = new List<string>();
		list.Add(TextColorMgr.GetColor(this.GuildWarResourceBriefDic[mineID].myMemberCount.ToString(), "6adc32", string.Empty));
		list.Add(TextColorMgr.GetColor(this.GuildWarResourceBriefDic[mineID].faceMemberCount.ToString(), "ff4040", string.Empty));
		list.Add(this.GetMineOwnerName(mineID));
		return list;
	}

	protected void TryUpdateMineInfo()
	{
		GuildWarInstance.Instance.TryUpdateOwnProgressUI();
		MineAndReportUI mineAndReportUI = UIManagerControl.Instance.GetUIIfExist("MineAndReportUI") as MineAndReportUI;
		if (mineAndReportUI)
		{
			mineAndReportUI.SetMineData();
		}
	}

	public XDict<int, GuildWarManager.MineInfo> GetMineInfo()
	{
		if (this.GuildWarResourceBriefDic == null)
		{
			return null;
		}
		XDict<int, GuildWarManager.MineInfo> xDict = new XDict<int, GuildWarManager.MineInfo>();
		for (int i = 0; i < this.GuildWarResourceBriefDic.Count; i++)
		{
			GuildWarManager.MineInfo value = default(GuildWarManager.MineInfo);
			value.id = this.GuildWarResourceBriefDic.ElementKeyAt(i);
			value.state = this.GetMineState(value.id);
			value.ownerName = this.GetMineOwnerName(value.state);
			xDict.Add(value.id, value);
		}
		return xDict;
	}

	protected void OnEnterMineNPC(int id)
	{
		if (this.GuildWarResourceBriefDic == null || !this.GuildWarResourceBriefDic.ContainsKey(id))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515111, false), 1f, 1f);
			return;
		}
		if (this.IsNavToMine && this.NavToMineID == id)
		{
			this.StopNavToMine();
		}
		this.ShowChallengeUI(id);
	}

	protected void OnExitMineNPC(int id)
	{
		this.CheckHideChallengeUI(id);
	}

	protected void ShowChallengeUI(int id)
	{
		this.MineEnteredID = id;
		TownUI townUI = UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI;
		if (townUI != null)
		{
			townUI.ShowGuildWarBubble(true, new Action(this.OnClickChallengeUI));
		}
	}

	protected void CheckHideChallengeUI(int id)
	{
		if (id != this.MineEnteredID)
		{
			return;
		}
		this.HideChallengeUI();
	}

	protected void HideChallengeUI()
	{
		this.MineEnteredID = 0;
		TownUI townUI = UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI;
		if (townUI != null)
		{
			townUI.ShowGuildWarBubble(false, null);
		}
	}

	public void OnClickChallengeUI()
	{
		if (!DataReader<JunTuanZhanDou>.Contains(this.MineEnteredID))
		{
			return;
		}
		JunTuanZhanDou junTuanZhanDou = DataReader<JunTuanZhanDou>.Get(this.MineEnteredID);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), string.Format(GameDataUtils.GetChineseContent(515068, false), junTuanZhanDou.CoordinateTime, this.GetAddResourceNumByID(this.mineEnteredID), junTuanZhanDou.KillResource), delegate
		{
		}, delegate
		{
			this.SendEnterGuildBattleReq(this.MineEnteredID);
		}, GameDataUtils.GetChineseContent(621272, false), GameDataUtils.GetChineseContent(621271, false), "button_orange_1", "button_yellow_1", null, true, true);
		DialogBoxUIView.Instance.isClick = false;
	}

	protected void TryUpdateHp()
	{
		if (this.townUI == null)
		{
			this.townUI = (UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI);
		}
		if (this.townUI == null)
		{
			return;
		}
		this.townUI.UpdateDataUI();
	}

	protected void LoadSceneEnd(int sceneID)
	{
		if (!MySceneManager.IsGuildWarCityScene(sceneID))
		{
			return;
		}
		this.TryShowReliveUI();
	}

	protected void TryShowReliveUI()
	{
		if (this.RoleReliveTime < 0)
		{
			return;
		}
		if (TimeManager.Instance.PreciseServerSecond >= this.RoleReliveTime)
		{
			return;
		}
		GuildWarReliveUI guildWarReliveUI = UIManagerControl.Instance.OpenUI("GuildWarReliveUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWarReliveUI;
		guildWarReliveUI.SetCountDown(this.RoleReliveTime - TimeManager.Instance.PreciseServerSecond);
	}

	public void NavToMine(int id)
	{
		JunTuanZhanCaiJi junTuanZhanCaiJi = DataReader<JunTuanZhanCaiJi>.Get(id);
		if (junTuanZhanCaiJi == null)
		{
			return;
		}
		if (junTuanZhanCaiJi.CoordinatePoint.get_Count() >= 3)
		{
			this.IsNavToMine = true;
			this.NavToMineID = id;
			if (EntityWorld.Instance.EntSelf != null && !EntityWorld.Instance.EntSelf.IsInBattle)
			{
				EntityWorld.Instance.EntSelf.NavToSameScenePoint((float)junTuanZhanCaiJi.CoordinatePoint.get_Item(0) * 0.01f, (float)junTuanZhanCaiJi.CoordinatePoint.get_Item(2) * 0.01f, 0f, delegate
				{
				});
			}
		}
	}

	public void StopNavToMine()
	{
		this.IsNavToMine = false;
		this.NavToMineID = 0;
		if (EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.StopNavToSameScenePoint();
		}
	}

	public bool CheckIsNavToMine()
	{
		return this.IsNavToMine;
	}

	protected void ClearSceneDependentLogic()
	{
		this.MineEnteredID = 0;
		if (this.IsNavToMine)
		{
			this.StopNavToMine();
		}
	}

	protected void OnGuildReportNty(short state, GuildWarLogNty down = null)
	{
		Debug.LogError("OnGuildReportNty: " + down.ChinesexId);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			try
			{
				string text = string.Format(GameDataUtils.GetChineseContent(down.ChinesexId, false), down.Parameters.ToArray());
				this.reportCache.Insert(0, text);
				BroadcastManager.Instance.AddQueue(text);
			}
			catch (Exception var_1_6D)
			{
				string text2 = string.Empty;
				for (int i = 0; i < down.Parameters.get_Count(); i++)
				{
					text2 = text2 + "_" + down.Parameters.get_Item(i);
				}
				Debug.LogError(string.Concat(new object[]
				{
					"配置错误：中文表ID：",
					down.ChinesexId,
					" 中文：",
					GameDataUtils.GetChineseContent(down.ChinesexId, false),
					" 参数：",
					text2
				}));
			}
			if (this.reportCache.get_Count() >= this.MaxReportNum)
			{
				this.reportCache.RemoveRange(this.MaxReportNum, this.reportCache.get_Count() - this.MaxReportNum);
			}
			this.TryUpdateMineReport();
		}
	}

	protected void TryUpdateMineReport()
	{
		MineAndReportUI mineAndReportUI = UIManagerControl.Instance.GetUIIfExist("MineAndReportUI") as MineAndReportUI;
		if (mineAndReportUI)
		{
			mineAndReportUI.SetReportData();
		}
	}
}
