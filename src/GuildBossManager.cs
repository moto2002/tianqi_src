using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class GuildBossManager : BaseSubSystemManager
{
	private GuildBossActivityInfo guildBossActivityInfo;

	public int ShowBattleCDRemainSecond = 20;

	public int GuildCallBossTotalTimesPerWeek;

	public bool IsCallGuildBoss;

	private static GuildBossManager instance;

	public bool ShowGuildBossTest;

	public int GuildBossID;

	private bool isCheck;

	private TimeCountDown guildBossBattleCD;

	public GuildBossActivityInfo GuildBossActivityInfo
	{
		get
		{
			return this.guildBossActivityInfo;
		}
		set
		{
			this.guildBossActivityInfo = value;
		}
	}

	public static GuildBossManager Instance
	{
		get
		{
			if (GuildBossManager.instance == null)
			{
				GuildBossManager.instance = new GuildBossManager();
			}
			return GuildBossManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
		this.ShowBattleCDRemainSecond = (int)float.Parse(DataReader<GongHuiXinXi>.Get("RemainingTime").value);
		this.GuildCallBossTotalTimesPerWeek = (int)float.Parse(DataReader<GongHuiXinXi>.Get("SummonTime").value);
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GuildBossInfoRes>(new NetCallBackMethod<GuildBossInfoRes>(this.OnGuildBossInfoRes));
		NetworkManager.AddListenEvent<CallGuildBossRes>(new NetCallBackMethod<CallGuildBossRes>(this.OnCallGuildBossRes));
		NetworkManager.AddListenEvent<ChallengeGuildBossRes>(new NetCallBackMethod<ChallengeGuildBossRes>(this.OnChallengeGuildBossRes));
		NetworkManager.AddListenEvent<ChallengeGuildBossNty>(new NetCallBackMethod<ChallengeGuildBossNty>(this.OnChallengeGuildBossNty));
		NetworkManager.AddListenEvent<CleanGuildBossCdRes>(new NetCallBackMethod<CleanGuildBossCdRes>(this.OnCleanGuildBossCDRes));
		NetworkManager.AddListenEvent<ExitGuildBossBattleRes>(new NetCallBackMethod<ExitGuildBossBattleRes>(this.OnExitGuildBossBattleRes));
		NetworkManager.AddListenEvent<GuildBossRefreshNty>(new NetCallBackMethod<GuildBossRefreshNty>(this.OnGuildBossRefreshNty));
	}

	public override void Release()
	{
		this.guildBossActivityInfo = null;
		this.IsCallGuildBoss = false;
		this.RemoveGuildBossBattleCountDown();
	}

	private void OnGuildBossRefreshNty(short state, GuildBossRefreshNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.IsCallGuildBoss = down.guildBossStatus;
		EventDispatcher.Broadcast(EventNames.OnGuildBossStatusNty);
		if (UIManagerControl.Instance.IsOpen("GuildBossUI") && GuildManager.Instance.MyGuildnfo != null)
		{
			GuildBossManager.instance.SendGuildBossInfoReq();
		}
	}

	private void OnGuildBossInfoRes(short state, GuildBossInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.guildBossActivityInfo == null)
			{
				this.guildBossActivityInfo = new GuildBossActivityInfo();
			}
			this.guildBossActivityInfo.RemainCallBossTimes = down.canCallTimes;
			this.guildBossActivityInfo.IsChallenging = down.challenging;
			this.guildBossActivityInfo.CanKillBossCD = down.canKillBossCD;
			this.guildBossActivityInfo.WillChallengeBossTimes = down.willChallengeBossTimes;
			this.guildBossActivityInfo.CleanCDTimes = down.rmCleanTimes;
			this.guildBossActivityInfo.GuildBossID = down.bossInfo.bossId;
			this.guildBossActivityInfo.GuildBossCurrentBlood = down.bossInfo.bossHp;
			this.guildBossActivityInfo.GuildBossToEndCd = down.bossInfo.endCD;
			this.guildBossActivityInfo.GuildBossTotalBlood = down.bossInfo.bossHpLmt;
			this.guildBossActivityInfo.GuildBossOpenCD = down.openCD;
			if (this.guildBossActivityInfo.HurtRankingList == null)
			{
				this.guildBossActivityInfo.HurtRankingList = new List<GuildBossClientHurtRankingInfo>();
			}
			this.guildBossActivityInfo.HurtRankingList.Clear();
			for (int i = 0; i < down.hurtInfos.get_Count(); i++)
			{
				GuildBossHurtInfo guildBossHurtInfo = down.hurtInfos.get_Item(i);
				MemberInfo myGuildMemberInfoByRoleID = GuildManager.Instance.GetMyGuildMemberInfoByRoleID(guildBossHurtInfo.roleId);
				if (guildBossHurtInfo.roleId > 0L && myGuildMemberInfoByRoleID != null)
				{
					GuildBossClientHurtRankingInfo guildBossClientHurtRankingInfo = new GuildBossClientHurtRankingInfo();
					guildBossClientHurtRankingInfo.RoleID = guildBossHurtInfo.roleId;
					guildBossClientHurtRankingInfo.HurtValue = guildBossHurtInfo.hurtValue;
					guildBossClientHurtRankingInfo.RoleName = myGuildMemberInfoByRoleID.name;
					guildBossClientHurtRankingInfo.RoleCarrer = myGuildMemberInfoByRoleID.career;
					this.guildBossActivityInfo.HurtRankingList.Add(guildBossClientHurtRankingInfo);
				}
			}
			this.guildBossActivityInfo.HurtRankingList.Sort(new Comparison<GuildBossClientHurtRankingInfo>(GuildBossManager.GuildBossHPRankingSort));
			this.guildBossActivityInfo.FinalHurtInfo = null;
			if (down.fatal2BossRoleId > 0L)
			{
				MemberInfo myGuildMemberInfoByRoleID2 = GuildManager.Instance.GetMyGuildMemberInfoByRoleID(down.fatal2BossRoleId);
				if (myGuildMemberInfoByRoleID2 != null)
				{
					GuildBossClientHurtRankingInfo guildBossClientHurtRankingInfo2 = new GuildBossClientHurtRankingInfo();
					guildBossClientHurtRankingInfo2.RoleID = down.fatal2BossRoleId;
					guildBossClientHurtRankingInfo2.RoleName = myGuildMemberInfoByRoleID2.name;
					guildBossClientHurtRankingInfo2.RoleCarrer = myGuildMemberInfoByRoleID2.career;
					this.guildBossActivityInfo.FinalHurtInfo = guildBossClientHurtRankingInfo2;
				}
			}
			EventDispatcher.Broadcast(EventNames.OnGetGuildBossInfo);
		}
	}

	private void OnCallGuildBossRes(short state, CallGuildBossRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.guildBossActivityInfo != null)
			{
				this.guildBossActivityInfo.RemainCallBossTimes = down.canCallTimes;
				this.guildBossActivityInfo.GuildBossID = down.bossId;
			}
			EventDispatcher.Broadcast(EventNames.OnCallGuildBossRes);
		}
	}

	private void OnChallengeGuildBossRes(short state, ChallengeGuildBossRes down = null)
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

	private void OnChallengeGuildBossNty(short state, ChallengeGuildBossNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			GuildBossInstance.Instance.GetInstanceResult(down);
		}
	}

	private void OnCleanGuildBossCDRes(short state, CleanGuildBossCdRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.guildBossActivityInfo != null)
		{
			this.guildBossActivityInfo.CleanCDTimes = down.rmCleanTimes;
			this.guildBossActivityInfo.CanKillBossCD = 0;
			EventDispatcher.Broadcast(EventNames.OnGetGuildBossInfo);
		}
	}

	private void OnExitGuildBossBattleRes(short state, ExitGuildBossBattleRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.RemoveGuildBossBattleCountDown();
	}

	public void SendGuildBossInfoReq()
	{
		NetworkManager.Send(new GuildBossInfoReq(), ServerType.Data);
	}

	public void SendCallGuildBossReq(int guildBossID)
	{
		NetworkManager.Send(new CallGuildBossReq
		{
			bossId = guildBossID
		}, ServerType.Data);
	}

	public void SendChallengeGuildBossReq()
	{
		NetworkManager.Send(new ChallengeGuildBossReq(), ServerType.Data);
	}

	public void SendCleanGuildBossCDReq()
	{
		NetworkManager.Send(new CleanGuildBossCdReq(), ServerType.Data);
	}

	public void SendExitGuildBossBattleReq()
	{
		NetworkManager.Send(new ExitGuildBossBattleReq(), ServerType.Data);
	}

	private static int GuildBossHPRankingSort(GuildBossClientHurtRankingInfo GBDRI1, GuildBossClientHurtRankingInfo GBDRI2)
	{
		if (GBDRI1.HurtValue > GBDRI2.HurtValue)
		{
			return -1;
		}
		return 1;
	}

	public int GetCleanGuildBossCDCost()
	{
		string value = DataReader<GongHuiXinXi>.Get("RefurbishTime").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		if (this.guildBossActivityInfo != null)
		{
			int num = (this.guildBossActivityInfo.WillChallengeBossTimes - 2 < 0) ? 0 : (this.guildBossActivityInfo.WillChallengeBossTimes - 2);
			if (num >= array.Length)
			{
				num = array.Length - 1;
			}
			string text = array[num];
			string[] array2 = text.Split(new char[]
			{
				','
			});
			return (int)float.Parse(array2[1]);
		}
		return 0;
	}

	public bool CheckCanCleanCDTime()
	{
		if (this.guildBossActivityInfo != null)
		{
			int num = (int)float.Parse(DataReader<GongHuiXinXi>.Get("ClearTime").value);
			if (this.guildBossActivityInfo.CleanCDTimes < num)
			{
				return true;
			}
		}
		return false;
	}

	public List<KeyValuePair<int, int>> GetRewardListByRank(int rank)
	{
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
		if (this.guildBossActivityInfo == null || this.guildBossActivityInfo.GuildBossID <= 0)
		{
			return list;
		}
		string text = string.Empty;
		if (rank <= 0 && DataReader<JunTuanBOSSMoXing>.Contains(this.guildBossActivityInfo.GuildBossID))
		{
			text = DataReader<JunTuanBOSSMoXing>.Get(this.guildBossActivityInfo.GuildBossID).LastReward;
			int num = (int)float.Parse(text);
			list.Add(new KeyValuePair<int, int>(num, -1));
			return list;
		}
		switch (rank)
		{
		case 1:
			text = DataReader<JunTuanBOSSMoXing>.Get(this.guildBossActivityInfo.GuildBossID).OneReward;
			break;
		case 2:
			text = DataReader<JunTuanBOSSMoXing>.Get(this.guildBossActivityInfo.GuildBossID).TwoReward;
			break;
		case 3:
			text = DataReader<JunTuanBOSSMoXing>.Get(this.guildBossActivityInfo.GuildBossID).ThreeReward;
			break;
		}
		string[] array = text.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				','
			});
			int num2 = int.Parse(array2[0]);
			int num3 = int.Parse(array2[1]);
			list.Add(new KeyValuePair<int, int>(num2, num3));
		}
		return list;
	}

	public void CheckShowGuildBossBattleCD()
	{
		this.RemoveGuildBossBattleCountDown();
		if (BattleTimeManager.Instance.HasGotTime && InstanceManager.CurrentInstanceType == InstanceType.GuildBoss && !GuildBossInstance.Instance.IsGetResult)
		{
			int num = (int)(BattleTimeManager.Instance.EndTime - TimeManager.Instance.PreciseServerTime).get_TotalSeconds();
			if (num <= this.ShowBattleCDRemainSecond + 1)
			{
				this.isCheck = true;
			}
			else
			{
				this.isCheck = false;
			}
			this.AddGuildBossBattleCountDown(num);
		}
	}

	public void AddGuildBossBattleCountDown(int remainTime)
	{
		this.guildBossBattleCD = new TimeCountDown(remainTime, TimeFormat.SECOND, delegate
		{
			if (this.guildBossBattleCD != null && this.guildBossBattleCD.GetSeconds() <= this.ShowBattleCDRemainSecond + 1 && !this.isCheck)
			{
				this.CheckShowGuildBossBattleCD();
			}
			if (this.guildBossBattleCD != null && this.isCheck && UIManagerControl.Instance.IsOpen("BattleUI"))
			{
				string guildBossBattleTimeText = "距离战斗结束还有：" + this.guildBossBattleCD.GetSeconds() + "秒";
				BattleUI.Instance.ShowBattleTimeUI(false);
				BattleUI.Instance.ShowGuildBossBattleTime(true);
				BattleUI.Instance.SetGuildBossBattleTimeText(guildBossBattleTimeText);
			}
		}, delegate
		{
			this.RemoveGuildBossBattleCountDown();
		}, true);
	}

	public void RemoveGuildBossBattleCountDown()
	{
		if (this.guildBossBattleCD != null)
		{
			this.guildBossBattleCD.Dispose();
			this.guildBossBattleCD = null;
		}
	}
}
