using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class ActivityCenterManager : BaseSubSystemManager
{
	public static Dictionary<int, ActiveCenterInfo> infoDict = new Dictionary<int, ActiveCenterInfo>();

	public static long serverTime = 0L;

	private Dictionary<int, ActiveCenterInfo> currentACInfoDic;

	private static ActivityCenterManager instance;

	public Dictionary<int, ActiveCenterInfo> CurrentACInfoDic
	{
		get
		{
			return this.currentACInfoDic;
		}
		set
		{
			this.currentACInfoDic = value;
		}
	}

	public static ActivityCenterManager Instance
	{
		get
		{
			if (ActivityCenterManager.instance == null)
			{
				ActivityCenterManager.instance = new ActivityCenterManager();
			}
			return ActivityCenterManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
		this.CurrentACInfoDic = new Dictionary<int, ActiveCenterInfo>();
	}

	public override void Release()
	{
		ActivityCenterManager.infoDict.Clear();
		this.CurrentACInfoDic = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ActiveCenterLoginPush>(new NetCallBackMethod<ActiveCenterLoginPush>(this.OnActiveCenterLoginPush));
		NetworkManager.AddListenEvent<ActiveChangeNty>(new NetCallBackMethod<ActiveChangeNty>(this.OnActiveChangeNty));
		NetworkManager.AddListenEvent<ActiveCenterPushRes>(new NetCallBackMethod<ActiveCenterPushRes>(this.OnRecvActiveCenterPush));
	}

	private void SendActiveCenterPushReq()
	{
		NetworkManager.Send(new ActiveCenterPushReq(), ServerType.Data);
	}

	private void OnActiveCenterLoginPush(short state, ActiveCenterLoginPush msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg != null)
		{
			if (this.CurrentACInfoDic == null)
			{
				this.CurrentACInfoDic = new Dictionary<int, ActiveCenterInfo>();
			}
			for (int i = 0; i < msg.activeInfos.get_Count(); i++)
			{
				ActiveCenterInfo activeCenterInfo = msg.activeInfos.get_Item(i);
				ActivityCenterManager.infoDict.set_Item(activeCenterInfo.id, activeCenterInfo);
				if (activeCenterInfo.status == ActiveCenterInfo.ActiveStatus.AS.Start && !this.CurrentACInfoDic.ContainsKey(activeCenterInfo.id) && this.CheckCanShowTownTip(activeCenterInfo.id))
				{
					this.CurrentACInfoDic.Add(activeCenterInfo.id, activeCenterInfo);
				}
			}
		}
		if (this.CurrentACInfoDic != null && this.CurrentACInfoDic.get_Count() > 0)
		{
			EventDispatcher.Broadcast(EventNames.ActivityAnnounce);
		}
	}

	private void OnActiveChangeNty(short state, ActiveChangeNty msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		for (int i = 0; i < msg.activeInfos.get_Count(); i++)
		{
			ActiveCenterInfo activeCenterInfo = msg.activeInfos.get_Item(i);
			ActivityCenterManager.infoDict.set_Item(activeCenterInfo.id, activeCenterInfo);
			if (this.CurrentACInfoDic.ContainsKey(activeCenterInfo.id) && activeCenterInfo.status != ActiveCenterInfo.ActiveStatus.AS.Start)
			{
				this.CurrentACInfoDic.Remove(activeCenterInfo.id);
			}
			if (!this.CurrentACInfoDic.ContainsKey(activeCenterInfo.id) && activeCenterInfo.status == ActiveCenterInfo.ActiveStatus.AS.Start && this.CheckCanShowTownTip(activeCenterInfo.id))
			{
				this.CurrentACInfoDic.Add(activeCenterInfo.id, activeCenterInfo);
			}
		}
		EventDispatcher.Broadcast(EventNames.ActivityAnnounce);
	}

	private void OnRecvActiveCenterPush(short state, ActiveCenterPushRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg != null)
		{
			ActivityCenterManager.serverTime = msg.serverTime;
		}
	}

	public bool CheckCanShowTownTip(int typeID)
	{
		if (DataReader<HuoDongZhongXin>.Contains(typeID))
		{
			int show = DataReader<HuoDongZhongXin>.Get(typeID).show;
			return show != 1;
		}
		return false;
	}

	public HuoDongZhongXin GetActivityCfgData(ActivityType type)
	{
		return DataReader<HuoDongZhongXin>.Get((int)type);
	}

	public string GetActivityOpenTimeByActivityType(ActivityType type)
	{
		HuoDongZhongXin activityCfgData = this.GetActivityCfgData(type);
		if (activityCfgData == null)
		{
			return string.Empty;
		}
		return this.GetFormatOpenTime(activityCfgData, false, false, string.Empty);
	}

	public string GetFormatOpenTime(HuoDongZhongXin activityInfo, bool showAll = false, bool showWeek = false, string weekdayGap = "")
	{
		string text = string.Empty;
		if (activityInfo.activityid == 10005)
		{
			return GuildWarManager.Instance.GetGuildWarCurrentShowTime();
		}
		if (activityInfo.activityid == 10004)
		{
			return string.Empty + GuildManager.Instance.GetGuildFieldOpenTime();
		}
		if (showWeek)
		{
			if (activityInfo.date.get_Count() >= 7)
			{
				text = "每天";
			}
			else
			{
				text = GameDataUtils.GetChineseContent(513518 + activityInfo.date.get_Item(0), false) + "至" + GameDataUtils.GetChineseContent(513518 + activityInfo.date.get_Item(activityInfo.date.get_Count() - 1), false);
			}
		}
		if (showAll)
		{
			string text2 = string.Empty;
			for (int i = 0; i < activityInfo.starttime.get_Count(); i++)
			{
				if (text2 == string.Empty)
				{
					text2 = text2 + activityInfo.starttime.get_Item(i) + "-" + activityInfo.endtime.get_Item(i);
				}
				else
				{
					string text3 = text2;
					text2 = string.Concat(new string[]
					{
						text3,
						"、",
						activityInfo.starttime.get_Item(i),
						"-",
						activityInfo.endtime.get_Item(i)
					});
				}
			}
			return text + weekdayGap + text2;
		}
		DateTime preciseServerTime = TimeManager.Instance.PreciseServerTime;
		for (int j = activityInfo.starttime.get_Count() - 1; j >= 0; j--)
		{
			string[] array = activityInfo.starttime.get_Item(j).Split(new char[]
			{
				':'
			});
			int num = int.Parse((!array[0].StartsWith("0")) ? array[0] : array[0].Substring(1));
			int num2 = int.Parse((!array[1].StartsWith("0")) ? array[1] : array[1].Substring(1));
			string[] array2 = activityInfo.endtime.get_Item(j).Split(new char[]
			{
				':'
			});
			int num3 = int.Parse((!array2[0].StartsWith("0")) ? array2[0] : array2[0].Substring(1));
			int num4 = int.Parse((!array2[1].StartsWith("0")) ? array2[1] : array2[1].Substring(1));
			DateTime dateTime = new DateTime(preciseServerTime.get_Year(), preciseServerTime.get_Month(), preciseServerTime.get_Day(), num, num2, 0);
			DateTime dateTime2 = new DateTime(preciseServerTime.get_Year(), preciseServerTime.get_Month(), preciseServerTime.get_Day(), num3, num4, 0);
			if (ActivityCenterManager.Instance.CheckActivityIsOpen(activityInfo.activityid) && dateTime <= preciseServerTime && preciseServerTime <= dateTime2)
			{
				return string.Concat(new string[]
				{
					text,
					weekdayGap,
					activityInfo.starttime.get_Item(j),
					"-",
					activityInfo.endtime.get_Item(j)
				});
			}
		}
		return string.Concat(new string[]
		{
			text,
			weekdayGap,
			activityInfo.starttime.get_Item(0),
			"-",
			activityInfo.endtime.get_Item(0)
		});
	}

	public bool CheckActivityIsOpen(int activityID)
	{
		return ActivityCenterManager.infoDict.ContainsKey(activityID) && ActivityCenterManager.infoDict.get_Item(activityID).status == ActiveCenterInfo.ActiveStatus.AS.Start;
	}

	public List<ActiveCenterInfo> GetActivityInfoList()
	{
		List<ActiveCenterInfo> list = new List<ActiveCenterInfo>();
		if (ActivityCenterManager.infoDict.get_Count() <= 0)
		{
			List<HuoDongZhongXin> dataList = DataReader<HuoDongZhongXin>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				ActiveCenterInfo activeCenterInfo = new ActiveCenterInfo();
				activeCenterInfo.id = dataList.get_Item(i).activityid;
				activeCenterInfo.status = ActiveCenterInfo.ActiveStatus.AS.NotOpen;
				activeCenterInfo.remainTimes = ((dataList.get_Item(i).num <= 0) ? -1 : dataList.get_Item(i).num);
				ActivityCenterManager.infoDict.Add(activeCenterInfo.id, activeCenterInfo);
			}
		}
		using (Dictionary<int, ActiveCenterInfo>.Enumerator enumerator = ActivityCenterManager.infoDict.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, ActiveCenterInfo> current = enumerator.get_Current();
				list.Add(current.get_Value());
			}
		}
		return list;
	}

	public void OpenCurrentActivityUI(int activityID)
	{
		if (!this.CurrentACInfoDic.ContainsKey(activityID) || this.CurrentACInfoDic.get_Item(activityID).status != ActiveCenterInfo.ActiveStatus.AS.Start)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513526, false), 2f, 2f);
			return;
		}
		if (!DataReader<HuoDongZhongXin>.Contains(activityID))
		{
			return;
		}
		HuoDongZhongXin huoDongZhongXin = DataReader<HuoDongZhongXin>.Get(activityID);
		if (huoDongZhongXin == null)
		{
			return;
		}
		if (activityID == 10004 || activityID == 10005)
		{
			if (!GuildManager.Instance.IsJoinInGuild())
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(34157, false));
				return;
			}
			if (activityID == 10004)
			{
				GuildActivityCenterUI guildActivityCenterUI = UIManagerControl.Instance.OpenUI("GuildActivityCenterUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildActivityCenterUI;
				guildActivityCenterUI.get_transform().SetAsLastSibling();
			}
			else if (activityID == 10005)
			{
				GuildWarVSInfoUI guildWarVSInfoUI = UIManagerControl.Instance.OpenUI("GuildWarVSInfoUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildWarVSInfoUI;
				guildWarVSInfoUI.get_transform().SetAsLastSibling();
			}
			return;
		}
		else
		{
			if (huoDongZhongXin.minLv > EntityWorld.Instance.EntSelf.Lv)
			{
				string text = string.Format(GameDataUtils.GetChineseContent(513512, false), huoDongZhongXin.minLv);
				UIManagerControl.Instance.ShowToastText(text);
				return;
			}
			if (activityID == 10001)
			{
				InstanceManagerUI.OpenGangFightUI();
			}
			else if (activityID == 10002)
			{
				MultiPlayerManager.Instance.OpenMultiPlayerUI(10002, "多人活动");
			}
			else if (activityID == 10003)
			{
				LinkNavigationManager.OpenMushroomHitUI();
			}
			else if (activityID == 10006)
			{
				LinkNavigationManager.OpenMultiPVPUI();
			}
			else if (activityID == 10007)
			{
				LinkNavigationManager.SystemLink(55, true, null);
			}
			return;
		}
	}

	public void RemoveCurrentActivityTip(int activityID)
	{
		if (this.CurrentACInfoDic != null && this.CurrentACInfoDic.ContainsKey(activityID))
		{
			this.CurrentACInfoDic.Remove(activityID);
			EventDispatcher.Broadcast(EventNames.ActivityAnnounce);
		}
	}

	public void ChangeGuildWarActivityTip(GuildWarTimeStep.GWTS guildWarStep)
	{
		if (this.CurrentACInfoDic == null)
		{
			this.CurrentACInfoDic = new Dictionary<int, ActiveCenterInfo>();
		}
		if (guildWarStep == GuildWarTimeStep.GWTS.FINAL_MATCH_BEG || guildWarStep == GuildWarTimeStep.GWTS.HALF_MATCH1_BEG || guildWarStep == GuildWarTimeStep.GWTS.HALF_MATCH2_BEG)
		{
			if (this.CurrentACInfoDic != null && !this.CurrentACInfoDic.ContainsKey(10005) && GuildManager.Instance.IsJoinInGuild())
			{
				ActiveCenterInfo activeCenterInfo = new ActiveCenterInfo();
				activeCenterInfo.id = 10005;
				activeCenterInfo.status = ActiveCenterInfo.ActiveStatus.AS.Start;
				this.CurrentACInfoDic.Add(10005, activeCenterInfo);
			}
			else if (this.CurrentACInfoDic != null && this.CurrentACInfoDic.ContainsKey(10005) && !GuildManager.Instance.IsJoinInGuild())
			{
				this.CurrentACInfoDic.Remove(10005);
			}
		}
		else if (this.CurrentACInfoDic != null && this.CurrentACInfoDic.ContainsKey(10005))
		{
			this.CurrentACInfoDic.Remove(10005);
		}
		EventDispatcher.Broadcast(EventNames.ActivityAnnounce);
	}

	public void ChangeGuildFieldActivityTip(bool isOpen)
	{
		if (this.CurrentACInfoDic == null)
		{
			this.CurrentACInfoDic = new Dictionary<int, ActiveCenterInfo>();
		}
		if (isOpen)
		{
			if (this.CurrentACInfoDic != null && !this.CurrentACInfoDic.ContainsKey(10004) && GuildManager.Instance.IsJoinInGuild())
			{
				ActiveCenterInfo activeCenterInfo = new ActiveCenterInfo();
				activeCenterInfo.id = 10004;
				activeCenterInfo.status = ActiveCenterInfo.ActiveStatus.AS.Start;
				this.CurrentACInfoDic.Add(10004, activeCenterInfo);
			}
			else if (this.CurrentACInfoDic != null && this.CurrentACInfoDic.ContainsKey(10004) && !GuildManager.Instance.IsJoinInGuild())
			{
				this.CurrentACInfoDic.Remove(10004);
			}
		}
		else if (this.CurrentACInfoDic != null && this.CurrentACInfoDic.ContainsKey(10004))
		{
			this.CurrentACInfoDic.Remove(10004);
		}
		EventDispatcher.Broadcast(EventNames.ActivityAnnounce);
	}

	public bool CheckHasActivityOpen()
	{
		if (ActivityCenterManager.infoDict != null && ActivityCenterManager.infoDict.get_Count() > 0)
		{
			using (Dictionary<int, ActiveCenterInfo>.Enumerator enumerator = ActivityCenterManager.infoDict.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, ActiveCenterInfo> current = enumerator.get_Current();
					if (current.get_Value().status == ActiveCenterInfo.ActiveStatus.AS.Start)
					{
						return true;
					}
				}
			}
		}
		if (GuildManager.Instance.IsJoinInGuild())
		{
			if (GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.FINAL_MATCH_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH1_BEG || GuildWarManager.Instance.GuildWarTimeStep == GuildWarTimeStep.GWTS.HALF_MATCH2_BEG)
			{
				return true;
			}
			if (GuildManager.Instance.IsGuildFieldOpen)
			{
				return true;
			}
		}
		return false;
	}
}
