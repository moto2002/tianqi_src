using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class DailyTaskManager : BaseSubSystemManager
{
	private const int SORT_TYPE = 6;

	private List<DailyTask> mDailyList = new List<DailyTask>();

	public List<int> getActivityIds = new List<int>();

	public int totalActivity;

	private int mLastFindTaskId;

	private int mLastFindTimes;

	private static DailyTaskManager instance;

	private bool mIsOperability;

	public static DailyTaskManager Instance
	{
		get
		{
			if (DailyTaskManager.instance == null)
			{
				DailyTaskManager.instance = new DailyTaskManager();
			}
			return DailyTaskManager.instance;
		}
	}

	public bool IsOperability
	{
		get
		{
			return this.mIsOperability;
		}
		set
		{
			this.mIsOperability = value;
			EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsModuleDailyTask, this.mIsOperability);
		}
	}

	public bool HasLimitTaskOpen
	{
		get
		{
			return ActivityCenterManager.Instance.CheckHasActivityOpen();
		}
	}

	public bool IsOpenGuildWar
	{
		get
		{
			string[] guildWarOpenTime;
			if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH2_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.FINAL_MATCH_END)
			{
				guildWarOpenTime = GuildWarManager.Instance.GetGuildWarOpenTime(4);
			}
			else if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH1_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.HALF_MATCH2_END)
			{
				guildWarOpenTime = GuildWarManager.Instance.GetGuildWarOpenTime(3);
			}
			else
			{
				guildWarOpenTime = GuildWarManager.Instance.GetGuildWarOpenTime(2);
			}
			string[] array = guildWarOpenTime[1].Split(new char[]
			{
				':'
			});
			int num = int.Parse((!array[0].StartsWith("0")) ? array[0] : array[0].Substring(1));
			int num2 = int.Parse((!array[1].StartsWith("0")) ? array[1] : array[1].Substring(1));
			DateTime dateTime = new DateTime(DateTime.get_Now().get_Year(), DateTime.get_Now().get_Month(), DateTime.get_Now().get_Day(), num, num2, 0);
			string[] array2 = guildWarOpenTime[3].Split(new char[]
			{
				':'
			});
			int num3 = int.Parse((!array2[0].StartsWith("0")) ? array2[0] : array2[0].Substring(1));
			int num4 = int.Parse((!array2[1].StartsWith("0")) ? array2[1] : array2[1].Substring(1));
			DateTime dateTime2 = new DateTime(DateTime.get_Now().get_Year(), DateTime.get_Now().get_Month(), DateTime.get_Now().get_Day(), num3, num4, 0);
			return dateTime <= DateTime.get_Now() && DateTime.get_Now() <= dateTime2;
		}
	}

	private DailyTaskManager()
	{
	}

	public override void Release()
	{
		this.mDailyList.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<DailyTaskNotice>(new NetCallBackMethod<DailyTaskNotice>(this.OnDailyTaskNotice));
		NetworkManager.AddListenEvent<DailyTaskNty>(new NetCallBackMethod<DailyTaskNty>(this.OnDailyTaskNty));
		NetworkManager.AddListenEvent<DailyTaskActivityNty>(new NetCallBackMethod<DailyTaskActivityNty>(this.OnDailyTaskActivityNty));
		NetworkManager.AddListenEvent<ClaimActivityPrizeRes>(new NetCallBackMethod<ClaimActivityPrizeRes>(this.OnClaimActivityPrizeRes));
		NetworkManager.AddListenEvent<GetDailyTaskPrizeRes>(new NetCallBackMethod<GetDailyTaskPrizeRes>(this.OnGetDailyTaskPrizeRes));
		NetworkManager.AddListenEvent<DailyTaskResetNty>(new NetCallBackMethod<DailyTaskResetNty>(this.OnDailyTaskResetNty));
	}

	public void SendActivity(int id)
	{
		NetworkManager.Send(new ClaimActivityPrizeReq
		{
			activity = id
		}, ServerType.Data);
		Debug.Log("领取活跃度：" + id);
	}

	public void SendDailyTaskPrizeReq(int id, int type, int count)
	{
		this.mLastFindTaskId = id;
		this.mLastFindTimes = count;
		NetworkManager.Send(new GetDailyTaskPrizeReq
		{
			taskId = this.mLastFindTaskId,
			opType = type,
			findTimes = this.mLastFindTimes
		}, ServerType.Data);
		Debug.Log("找回任务：" + id);
	}

	private void OnDailyTaskNotice(short state, DailyTaskNotice down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.getActivityIds = down.activityIds;
			this.totalActivity = down.totalActivity;
			this.mDailyList = down.dailyTasks;
			this.CheckTownDailyTaskPoint();
		}
	}

	private void OnDailyTaskNty(short state, DailyTaskNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		DailyTask dailyTask = this.mDailyList.Find((DailyTask e) => e.taskId == down.dailyTask.taskId);
		if (dailyTask != null)
		{
			this.mDailyList.Remove(dailyTask);
			this.mDailyList.Add(down.dailyTask);
		}
		this.totalActivity = down.totalActivity;
		EventDispatcher.Broadcast(EventNames.DailyTaskNty);
		this.CheckTownDailyTaskPoint();
	}

	private void OnDailyTaskActivityNty(short state, DailyTaskActivityNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.getActivityIds = down.activityIds;
		this.totalActivity = down.totalActivity;
		EventDispatcher.Broadcast(EventNames.DailyTaskActivityNty);
		this.CheckTownDailyTaskPoint();
	}

	private void OnClaimActivityPrizeRes(short state, ClaimActivityPrizeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnGetDailyTaskPrizeRes(short state, GetDailyTaskPrizeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		DailyTask dailyTask = this.mDailyList.Find((DailyTask e) => e.taskId == this.mLastFindTaskId);
		if (dailyTask != null)
		{
			dailyTask.canFindTimes -= this.mLastFindTimes;
			if (dailyTask.canFindTimes < 0)
			{
				dailyTask.canFindTimes = 0;
			}
			EventDispatcher.Broadcast(EventNames.DailyTaskFindRes);
			this.CheckTownDailyTaskPoint();
		}
	}

	private void OnDailyTaskResetNty(short state, DailyTaskResetNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.DailyTaskResetNty);
		this.CheckTownDailyTaskPoint();
	}

	private int SortDailyByPoint(DailyTask a, DailyTask b)
	{
		MeiRiMuBiao meiRiMuBiao = DataReader<MeiRiMuBiao>.Get(a.taskId);
		MeiRiMuBiao meiRiMuBiao2 = DataReader<MeiRiMuBiao>.Get(b.taskId);
		if (meiRiMuBiao == null || meiRiMuBiao2 == null)
		{
			return 0;
		}
		if (meiRiMuBiao.priority < meiRiMuBiao2.priority)
		{
			return 1;
		}
		if (meiRiMuBiao.priority > meiRiMuBiao2.priority)
		{
			return -1;
		}
		return 0;
	}

	public DailyTask GetDailyData(int id)
	{
		return this.mDailyList.Find((DailyTask e) => e.taskId == id);
	}

	public void CloseEveryDayUI()
	{
		UIStackManager.Instance.PopUIPrevious(UIType.FullScreen);
	}

	public void OpenEveryDayUI()
	{
		UIManagerControl.Instance.OpenUI("EveryDayUI", null, false, UIType.FullScreen);
	}

	public List<DailyTask> SortDailyList(DailyTaskType type)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return null;
		}
		List<List<DailyTask>> list = new List<List<DailyTask>>();
		for (int i = 0; i < 6; i++)
		{
			list.Add(new List<DailyTask>());
		}
		int lv = EntityWorld.Instance.EntSelf.Lv;
		for (int j = 0; j < this.mDailyList.get_Count(); j++)
		{
			MeiRiMuBiao meiRiMuBiao = DataReader<MeiRiMuBiao>.Get(this.mDailyList.get_Item(j).taskId);
			if (meiRiMuBiao != null)
			{
				SystemOpen systemOpen = DataReader<SystemOpen>.Get(meiRiMuBiao.sysId);
				if (systemOpen != null)
				{
					switch (type)
					{
					case DailyTaskType.DAILY:
					case DailyTaskType.LIMIT:
						if (lv < systemOpen.level || (systemOpen.taskId > 0 && !MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId)))
						{
							list.get_Item(0).Add(this.mDailyList.get_Item(j));
						}
						else if (this.mDailyList.get_Item(j).count == meiRiMuBiao.completeTime)
						{
							if ((meiRiMuBiao.buyTime == 1 && this.IsCanBuy(meiRiMuBiao.system)) || meiRiMuBiao.buyTime == 3)
							{
								list.get_Item(2).Add(this.mDailyList.get_Item(j));
							}
							else
							{
								list.get_Item(1).Add(this.mDailyList.get_Item(j));
							}
						}
						else if ((meiRiMuBiao.id == 12030 || meiRiMuBiao.id == 12040 || meiRiMuBiao.id == 12050) && !GuildManager.Instance.IsJoinInGuild())
						{
							list.get_Item(0).Add(this.mDailyList.get_Item(j));
						}
						else if (this.mDailyList.get_Item(j).count < meiRiMuBiao.completeTime)
						{
							list.get_Item(3).Add(this.mDailyList.get_Item(j));
						}
						break;
					case DailyTaskType.FIND:
						if (this.mDailyList.get_Item(j).canFindTimes == 0)
						{
							list.get_Item(4).Add(this.mDailyList.get_Item(j));
						}
						else if (this.mDailyList.get_Item(j).canFindTimes > 0)
						{
							list.get_Item(5).Add(this.mDailyList.get_Item(j));
						}
						break;
					}
				}
				else
				{
					list.get_Item(5).Add(this.mDailyList.get_Item(j));
				}
			}
		}
		for (int k = 0; k < 6; k++)
		{
			list.get_Item(k).Sort(new Comparison<DailyTask>(this.SortDailyByPoint));
		}
		List<DailyTask> list2 = new List<DailyTask>();
		for (int l = 5; l >= 0; l--)
		{
			for (int m = list.get_Item(l).get_Count() - 1; m >= 0; m--)
			{
				list2.Add(list.get_Item(l).get_Item(m));
			}
		}
		return list2;
	}

	public bool IsCanBuy(int system)
	{
		bool result = false;
		switch (system)
		{
		case 3:
			result = DefendFightManager.Instance.CanShowBuyBtnInDailyTask(SpecialFightMode.Protect);
			break;
		case 4:
			result = DefendFightManager.Instance.CanShowBuyBtnInDailyTask(SpecialFightMode.Save);
			break;
		case 5:
			result = DefendFightManager.Instance.CanShowBuyBtnInDailyTask(SpecialFightMode.Hold);
			break;
		case 9:
			result = SpecialFightManager.Instance.CanShowBuyExperienceTimesInDailyTask();
			break;
		case 12:
			result = (MemCollabManager.Instance.TodayRestTimes <= 0);
			break;
		}
		return result;
	}

	public bool IsFinish(int system)
	{
		bool result = false;
		switch (system)
		{
		case 3:
			result = DefendFightManager.Instance.IsFinishInDailyTask(SpecialFightMode.Protect);
			break;
		case 4:
			result = DefendFightManager.Instance.IsFinishInDailyTask(SpecialFightMode.Save);
			break;
		case 5:
			result = DefendFightManager.Instance.IsFinishInDailyTask(SpecialFightMode.Hold);
			break;
		case 9:
			result = SpecialFightManager.Instance.IsFinishExperienceTimes();
			break;
		case 12:
			result = (MemCollabManager.Instance.TodayExtendTimes >= MemCollabManager.Instance.MaxVipTimes);
			break;
		}
		return result;
	}

	public void CheckTownDailyTaskPoint()
	{
		this.IsOperability = this.HasDailyTaskPoint();
	}

	private bool HasDailyTaskPoint()
	{
		DailyTaskManager.<HasDailyTaskPoint>c__AnonStoreyE6 <HasDailyTaskPoint>c__AnonStoreyE = new DailyTaskManager.<HasDailyTaskPoint>c__AnonStoreyE6();
		int lv = EntityWorld.Instance.EntSelf.Lv;
		for (int k = 0; k < this.mDailyList.get_Count(); k++)
		{
			MeiRiMuBiao meiRiMuBiao = DataReader<MeiRiMuBiao>.Get(this.mDailyList.get_Item(k).taskId);
			if (meiRiMuBiao != null && meiRiMuBiao.Retrieve == 1)
			{
				SystemOpen systemOpen = DataReader<SystemOpen>.Get(meiRiMuBiao.sysId);
				if (systemOpen != null && lv >= systemOpen.level && MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId) && this.mDailyList.get_Item(k).canFindTimes > 0)
				{
					return true;
				}
			}
		}
		<HasDailyTaskPoint>c__AnonStoreyE.boxDatas = DataReader<HuoYueDuJiangLi>.DataList;
		int i;
		for (i = 0; i < <HasDailyTaskPoint>c__AnonStoreyE.boxDatas.get_Count(); i++)
		{
			int num = this.getActivityIds.Find((int e) => e == <HasDailyTaskPoint>c__AnonStoreyE.boxDatas.get_Item(i).id);
			if (num <= 0 && <HasDailyTaskPoint>c__AnonStoreyE.boxDatas.get_Item(i).numericalValue <= this.totalActivity)
			{
				return true;
			}
		}
		for (int j = 0; j < this.mDailyList.get_Count(); j++)
		{
			MeiRiMuBiao meiRiMuBiao = DataReader<MeiRiMuBiao>.Get(this.mDailyList.get_Item(j).taskId);
			if (meiRiMuBiao != null && meiRiMuBiao.activity == 2)
			{
				SystemOpen systemOpen = DataReader<SystemOpen>.Get(meiRiMuBiao.sysId);
				if (systemOpen != null)
				{
					if (lv >= systemOpen.level && (systemOpen.taskId <= 0 || MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId)))
					{
						if ((meiRiMuBiao.id != 12030 && meiRiMuBiao.id != 12040 && meiRiMuBiao.id != 12050) || GuildManager.Instance.IsJoinInGuild())
						{
							if (this.mDailyList.get_Item(j).count < meiRiMuBiao.completeTime)
							{
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}
}
