using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class SignInManager : BaseSubSystemManager
{
	public MonthSignInfo monthSignInfo;

	public List<MonthTotalInfo> monthToalInfo;

	private MonthSign monthSignCache;

	private OpenServer openServerCache;

	public List<EveryDayInfo> loginWelfareList = new List<EveryDayInfo>();

	public bool IsSevenDayUIOpened;

	private static SignInManager instance;

	public static SignInManager Instance
	{
		get
		{
			if (SignInManager.instance == null)
			{
				SignInManager.instance = new SignInManager();
			}
			return SignInManager.instance;
		}
	}

	private SignInManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.IsSevenDayUIOpened = false;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<SignLoginPush>(new NetCallBackMethod<SignLoginPush>(this.OnGetSignLoginPush));
		NetworkManager.AddListenEvent<SignChangedNty>(new NetCallBackMethod<SignChangedNty>(this.OnGetSignChangedNty));
		NetworkManager.AddListenEvent<SignRes>(new NetCallBackMethod<SignRes>(this.OnGetSignRes));
		NetworkManager.AddListenEvent<AcceptOpenAwardsRes>(new NetCallBackMethod<AcceptOpenAwardsRes>(this.OnGetAcceptOpenAwardsRes));
		NetworkManager.AddListenEvent<LoginWelfarePush>(new NetCallBackMethod<LoginWelfarePush>(this.OnLoginWelfarePush));
		NetworkManager.AddListenEvent<LoginWelfareNty>(new NetCallBackMethod<LoginWelfareNty>(this.OnLoginWelfareNty));
		NetworkManager.AddListenEvent<LoginWelfareCloseNty>(new NetCallBackMethod<LoginWelfareCloseNty>(this.OnLoginWelfareCloseNty));
		NetworkManager.AddListenEvent<GetLoginWelfareRes>(new NetCallBackMethod<GetLoginWelfareRes>(this.OnGetLoginWelfareRes));
		NetworkManager.AddListenEvent<AcceptMonthTotalRes>(new NetCallBackMethod<AcceptMonthTotalRes>(this.OnGetAcceptMonthTotalRes));
		NetworkManager.AddListenEvent<MonthTotalChangeNty>(new NetCallBackMethod<MonthTotalChangeNty>(this.OnGetMonthTotalChangeNty));
	}

	public void SendSignReq(int signFlag, MonthSign monthSign)
	{
		this.monthSignCache = monthSign;
		NetworkManager.Send(new SignReq
		{
			signFlag = signFlag
		}, ServerType.Data);
	}

	public void SendAcceptOpenAwardsReq(int daySerial, OpenServer openServer)
	{
		this.openServerCache = openServer;
		NetworkManager.Send(new AcceptOpenAwardsReq
		{
			daySerial = daySerial
		}, ServerType.Data);
	}

	public void SendGetLoginWelfareReq(int days)
	{
		NetworkManager.Send(new GetLoginWelfareReq
		{
			days = days
		}, ServerType.Data);
	}

	public void SendAcceptMonthTotalReq(int id)
	{
		NetworkManager.Send(new AcceptMonthTotalReq
		{
			id = id
		}, ServerType.Data);
	}

	private void OnGetSignLoginPush(short state, SignLoginPush down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.monthSignInfo = down.monthSignInfo;
				this.monthToalInfo = down.monthTotalInfo;
				this.DebugInfo();
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetSignChangedNty(short state, SignChangedNty down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.monthSignInfo = down.monthSignInfo;
				EventDispatcher.Broadcast(EventNames.OnGetSignChangedNty);
				TownUI townUI = UIManagerControl.Instance.GetUIIfExist("TownUI") as TownUI;
				if (townUI != null)
				{
					townUI.CheckBeniftRedPoint();
				}
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetSignRes(short state, SignRes down = null)
	{
		if (state == 0)
		{
			if (this.monthSignCache != null)
			{
				string text = GameDataUtils.GetChineseContent(502217, false);
				int num = this.monthSignCache.itemNum;
				if (EntityWorld.Instance.EntSelf.VipLv >= this.monthSignCache.doubleMinVip && this.monthSignCache.doubleMinVip != 0)
				{
					num *= 2;
				}
				Items items = DataReader<Items>.Get(this.monthSignCache.itemId);
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					GameDataUtils.GetChineseContent(items.name, false),
					" x",
					num
				});
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502080, false), 1f, 2f);
				this.ShowGetItems(down.items);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetAcceptOpenAwardsRes(short state, AcceptOpenAwardsRes down = null)
	{
		if (state == 0)
		{
			if (this.openServerCache != null)
			{
				string text = GameDataUtils.GetChineseContent(502217, false);
				for (int i = 0; i < this.openServerCache.itemId.get_Count(); i++)
				{
					Items items = DataReader<Items>.Get(this.openServerCache.itemId.get_Item(i));
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						GameDataUtils.GetChineseContent(items.name, false),
						" x",
						this.openServerCache.num.get_Item(i),
						"\n"
					});
				}
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502081, false), 1f, 2f);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnLoginWelfarePush(short state, LoginWelfarePush down = null)
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
		this.loginWelfareList = down.everyDayInfo;
		EventDispatcher.Broadcast(EventNames.OnLoginWelfareUpdate);
		OperateActivityManager.Instance.OnGetSignChangedNty();
	}

	private void OnLoginWelfareNty(short state, LoginWelfareNty down = null)
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
		bool flag = false;
		for (int i = 0; i < this.loginWelfareList.get_Count(); i++)
		{
			if (this.loginWelfareList.get_Item(i).loginDays == down.everyDayInfo.loginDays)
			{
				this.loginWelfareList.set_Item(i, down.everyDayInfo);
				flag = true;
			}
		}
		if (!flag)
		{
			this.loginWelfareList.Add(down.everyDayInfo);
		}
		EventDispatcher.Broadcast(EventNames.OnLoginWelfareUpdate);
		OperateActivityManager.Instance.OnGetSignChangedNty();
	}

	private void OnLoginWelfareCloseNty(short state, LoginWelfareCloseNty down = null)
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
		ActivityInfo activityInfo = OperateActivityManager.Instance.GetActivityInfo(4);
		if (activityInfo != null)
		{
			activityInfo.overdueFlag = true;
		}
	}

	private void OnGetLoginWelfareRes(short state, GetLoginWelfareRes down = null)
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
		this.ShowGetItems(down.items);
	}

	private void OnGetMonthTotalChangeNty(short state, MonthTotalChangeNty down = null)
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
		this.monthToalInfo = down.monthTotalInfo;
		EventDispatcher.Broadcast(EventNames.OnGetMonthTotalChangeNty);
	}

	private void OnGetAcceptMonthTotalRes(short state, AcceptMonthTotalRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502081, false), 1f, 2f);
		if (down == null)
		{
			return;
		}
		this.ShowGetItems(down.items);
	}

	private void ShowGetItems(List<ItemBriefInfo> items)
	{
		if (items == null || items.get_Count() < 1)
		{
			return;
		}
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		List<long> list3 = new List<long>();
		for (int i = 0; i < items.get_Count(); i++)
		{
			ItemBriefInfo itemBriefInfo = items.get_Item(i);
			list.Add(itemBriefInfo.cfgId);
			list2.Add(itemBriefInfo.count);
			list3.Add(itemBriefInfo.uId);
		}
		if (list != null && list.get_Count() > 0)
		{
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
			rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
		}
	}

	private void DebugInfo()
	{
	}

	public bool CheckMonthSignBadage()
	{
		bool flag = false;
		if (this.monthSignInfo != null && !this.monthSignInfo.isSign)
		{
			flag = true;
		}
		if (!flag && this.monthToalInfo != null)
		{
			for (int i = 0; i < this.monthToalInfo.get_Count(); i++)
			{
				if (this.monthToalInfo.get_Item(i).flag == 1)
				{
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	public bool CheckLoginSignBadage()
	{
		return false;
	}

	public bool CheckSeverSignBadage()
	{
		for (int i = 0; i < this.loginWelfareList.get_Count(); i++)
		{
			if (this.loginWelfareList.get_Item(i).status == 1)
			{
				return true;
			}
		}
		return false;
	}

	public bool ChckeBadage()
	{
		return this.CheckMonthSignBadage();
	}

	public bool CheckShowSevenDaySpine()
	{
		return !this.IsSevenDayUIOpened;
	}
}
