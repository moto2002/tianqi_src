using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class Activity7DayManager : BaseSubSystemManager
{
	public Dictionary<int, ActivityItemInfo> activityInfos = new Dictionary<int, ActivityItemInfo>();

	public int startDay;

	public List<int> endTimeouts = new List<int>();

	public int boxFlag = -1;

	public List<ItemInfo1> boxItems = new List<ItemInfo1>();

	public static Activity7DayManager instance;

	public static Activity7DayManager Instance
	{
		get
		{
			if (Activity7DayManager.instance == null)
			{
				Activity7DayManager.instance = new Activity7DayManager();
				TimerHeap.AddTimer(0u, 1000, new Action(Activity7DayManager.instance.TimerCallback));
			}
			return Activity7DayManager.instance;
		}
	}

	private Activity7DayManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.endTimeouts.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ActivityItemChangeNty>(new NetCallBackMethod<ActivityItemChangeNty>(this.OnActivityItemChangeNty));
		NetworkManager.AddListenEvent<GetActivityItemPrizeRes>(new NetCallBackMethod<GetActivityItemPrizeRes>(this.OnGetActivityItemPrizeRes));
		NetworkManager.AddListenEvent<ActivityUpdateInfo>(new NetCallBackMethod<ActivityUpdateInfo>(this.OnActivityUpdateInfo));
		NetworkManager.AddListenEvent<OpenServerBoxPush>(new NetCallBackMethod<OpenServerBoxPush>(this.OnOpenServerBoxPush));
		NetworkManager.AddListenEvent<GetOpenServerBoxRes>(new NetCallBackMethod<GetOpenServerBoxRes>(this.OnGetOpenServerBoxRes));
	}

	public void SendGetActivityItemPrizeReq(int typeId, int activityItemId)
	{
		NetworkManager.Send(new GetActivityItemPrizeReq
		{
			typeId = typeId,
			activityItemId = activityItemId
		}, ServerType.Data);
	}

	public void SendGetOpenServerBoxReq()
	{
		NetworkManager.Send(new GetOpenServerBoxReq(), ServerType.Data);
	}

	private void OnActivityItemChangeNty(short state, ActivityItemChangeNty msg = null)
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
		for (int i = 0; i < msg.activitiesInfo.get_Count(); i++)
		{
			ActivityItemInfo activityItemInfo = msg.activitiesInfo.get_Item(i);
			if (activityItemInfo.typeId == 2)
			{
				this.UpdateActivityItemInfo(activityItemInfo.activityId, activityItemInfo);
			}
		}
		OperateActivityManager.Instance.OnUpdateActivity7DayReward();
	}

	private void OnGetActivityItemPrizeRes(short state, GetActivityItemPrizeRes msg = null)
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
		int typeId = msg.typeId;
		if (typeId != 2)
		{
			if (typeId == 8)
			{
				GrowUpPlanManager.Instance.OnGetRewardRes(state, msg);
			}
		}
		else
		{
			EventDispatcher.Broadcast<int>(EventNames.GetActivityItemPrize, msg.typeId);
		}
	}

	private void OnActivityUpdateInfo(short state, ActivityUpdateInfo msg = null)
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
		this.startDay = msg.startDay;
	}

	private void OnOpenServerBoxPush(short state, OpenServerBoxPush msg = null)
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
		this.boxItems = msg.infos;
		this.boxFlag = msg.boxFlag;
		EventDispatcher.Broadcast(EventNames.OpenServerBoxUpdate);
	}

	private void OnGetOpenServerBoxRes(short state, GetOpenServerBoxRes msg = null)
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
		this.boxFlag = msg.boxFlag;
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513103, false), 1f, 2f);
		EventDispatcher.Broadcast(EventNames.OpenServerBoxUpdate);
	}

	private void TimerCallback()
	{
		for (int i = 0; i < this.endTimeouts.get_Count(); i++)
		{
			if (this.endTimeouts.get_Item(i) > 0)
			{
				List<int> list;
				List<int> expr_1F = list = this.endTimeouts;
				int num;
				int expr_22 = num = i;
				num = list.get_Item(num);
				expr_1F.set_Item(expr_22, num - 1);
			}
		}
	}

	private void UpdateActivityItemInfo(int activityId, ActivityItemInfo newInfo)
	{
		ActivityItemInfo activityItemInfo;
		this.activityInfos.TryGetValue(activityId, ref activityItemInfo);
		if (activityItemInfo != null && newInfo.rawInfo == null)
		{
			newInfo.rawInfo = activityItemInfo.rawInfo;
		}
		this.activityInfos.set_Item(activityId, newInfo);
	}

	public List<RawInfo> GetRawInfoList(int openDay = 0, int type = 0, bool isSort = false)
	{
		List<RawInfo> list = new List<RawInfo>();
		using (Dictionary<int, ActivityItemInfo>.Enumerator enumerator = this.activityInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, ActivityItemInfo> current = enumerator.get_Current();
				RawInfo rawInfo = current.get_Value().rawInfo;
				if (rawInfo != null && (type == 0 || rawInfo.tab == (Tab.TAB)type) && (openDay == 0 || rawInfo.startDay == openDay))
				{
					list.Add(rawInfo);
				}
			}
		}
		if (isSort)
		{
			list.Sort(delegate(RawInfo node1, RawInfo node2)
			{
				ActivityItemInfo activityItemInfo = this.activityInfos.get_Item(node1.acId);
				ActivityItemInfo activityItemInfo2 = this.activityInfos.get_Item(node2.acId);
				if (activityItemInfo.canGetFlag != activityItemInfo2.canGetFlag)
				{
					return activityItemInfo2.canGetFlag.CompareTo(activityItemInfo.canGetFlag);
				}
				return node1.acId.CompareTo(node2.acId);
			});
		}
		return list;
	}

	public bool GetSubPageRedPoint(int type, int dayNum)
	{
		if (dayNum > this.startDay)
		{
			return false;
		}
		List<RawInfo> rawInfoList = this.GetRawInfoList(dayNum, type, false);
		for (int i = 0; i < rawInfoList.get_Count(); i++)
		{
			RawInfo rawInfo = rawInfoList.get_Item(i);
			ActivityItemInfo activityItemInfo = this.activityInfos.get_Item(rawInfo.acId);
			if (activityItemInfo.canGetFlag)
			{
				return true;
			}
		}
		return false;
	}

	public bool GetDayRedPoint(int dayNum)
	{
		List<RawInfo> rawInfoList = this.GetRawInfoList(dayNum, 0, false);
		for (int i = 0; i < rawInfoList.get_Count(); i++)
		{
			RawInfo rawInfo = rawInfoList.get_Item(i);
			ActivityItemInfo activityItemInfo = this.activityInfos.get_Item(rawInfo.acId);
			if (activityItemInfo.canGetFlag)
			{
				return true;
			}
		}
		return false;
	}

	public bool GetRedPoint()
	{
		using (Dictionary<int, ActivityItemInfo>.Enumerator enumerator = this.activityInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, ActivityItemInfo> current = enumerator.get_Current();
				if (current.get_Value().canGetFlag)
				{
					RawInfo rawInfo = current.get_Value().rawInfo;
					if (rawInfo != null && rawInfo.startDay <= this.startDay)
					{
						return true;
					}
				}
			}
		}
		return false;
	}
}
