using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class FirstPayManager : BaseSubSystemManager
{
	public int State = -1;

	public bool IsFirstPayUIHasOpened;

	private int firstRechargeTask = -1;

	private bool _IsPayButRewardNoObtain;

	public List<AccumulateRechargeInfo> FirstPayRechargeInfo;

	public int AllRechargeDiamond;

	private static FirstPayManager instance;

	public int StateTOVIP;

	public int FirstRechargeTask
	{
		get
		{
			if (this.firstRechargeTask == -1)
			{
				GlobalParams globalParams = DataReader<GlobalParams>.Get("firstRechargeTask");
				if (globalParams != null)
				{
					this.firstRechargeTask = int.Parse(GameDataUtils.SplitString4Dot0(globalParams.value));
				}
				else
				{
					this.firstRechargeTask = 0;
				}
			}
			return this.firstRechargeTask;
		}
	}

	public bool IsPayButRewardNoObtain
	{
		get
		{
			return this._IsPayButRewardNoObtain;
		}
		set
		{
			this._IsPayButRewardNoObtain = value;
			EventDispatcher.Broadcast<bool>(EventNames.FirstChargeBadge, value);
		}
	}

	public static FirstPayManager Instance
	{
		get
		{
			if (FirstPayManager.instance == null)
			{
				FirstPayManager.instance = new FirstPayManager();
			}
			return FirstPayManager.instance;
		}
	}

	private FirstPayManager()
	{
	}

	private bool IsFirstPay()
	{
		return this.State != 1 && this.State != 2;
	}

	public override void Init()
	{
		base.Init();
		this.InitGameData();
	}

	public override void Release()
	{
		this.IsFirstPayUIHasOpened = false;
		this.StateTOVIP = 0;
		this.firstRechargeTask = -1;
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener<int>("GuideManager.TaskFinish", new Callback<int>(this.OnTaskFinish));
		NetworkManager.AddListenEvent<FirstRechargePush>(new NetCallBackMethod<FirstRechargePush>(this.OnFirstRechargePush));
		NetworkManager.AddListenEvent<GetFirstPrizeRes>(new NetCallBackMethod<GetFirstPrizeRes>(this.OnGetFirstPrizeRes));
		NetworkManager.AddListenEvent<AccumulateRechargeNty>(new NetCallBackMethod<AccumulateRechargeNty>(this.OnAccumulateRechargeNty));
		NetworkManager.AddListenEvent<RechargeNumNty>(new NetCallBackMethod<RechargeNumNty>(this.OnRechargeNumNty));
	}

	private void OnTaskFinish(int taskId)
	{
		if (!this.CheckFirstPayOn())
		{
			return;
		}
		if (taskId == this.FirstRechargeTask)
		{
			LinkNavigationManager.OpenFirstPayUI(null);
		}
	}

	public void CheckFirstPayToVIPUp()
	{
		if (!SystemOpenManager.IsSystemHideEntrance(9) && this.CheckFirstPayOn() && this.IsFirstPayBeforeVIPUp())
		{
			this.StateTOVIP = this.State;
			LinkNavigationManager.OpenFirstPayUI(null);
		}
	}

	private bool IsFirstPayBeforeVIPUp()
	{
		return this.StateTOVIP != 1 && this.StateTOVIP != 2;
	}

	public void SendGetFirstPrize(int id)
	{
		NetworkManager.Send(new GetFirstPrizeReq
		{
			id = id
		}, ServerType.Data);
	}

	public void OnFirstRechargePush(short state, FirstRechargePush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.State = down.flag;
			this.IsPayButRewardNoObtain = this.CheckIsPayButRewardNoObtain();
			UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("TownUI");
			if (uIIfExist != null)
			{
				TownUI townUI = uIIfExist as TownUI;
				townUI.ControlSystemOpens(false, 9);
			}
		}
	}

	public void OnGetFirstPrizeRes(short state, GetFirstPrizeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			List<ItemBriefInfo> items = down.items;
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
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
			rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
		}
	}

	public void OnAccumulateRechargeNty(short state, AccumulateRechargeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.FirstPayRechargeInfo != null && down.info != null)
		{
			for (int i = 0; i < down.info.get_Count(); i++)
			{
				int id = down.info.get_Item(i).id;
				for (int j = 0; j < this.FirstPayRechargeInfo.get_Count(); j++)
				{
					if (this.FirstPayRechargeInfo.get_Item(j).id == id)
					{
						this.FirstPayRechargeInfo.get_Item(j).status = down.info.get_Item(i).status;
					}
				}
			}
		}
		if (FirstPayGiftUI.Instance != null && FirstPayGiftUI.Instance.get_gameObject().get_activeSelf())
		{
			FirstPayGiftUI.Instance.UpdateBtnsState();
		}
	}

	public void OnRechargeNumNty(short state, RechargeNumNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.AllRechargeDiamond = down.rechargeDiamond;
		if (FirstPayGiftUI.Instance != null && FirstPayGiftUI.Instance.get_gameObject().get_activeSelf())
		{
			FirstPayGiftUI.Instance.UpdateRewardDiamond();
		}
	}

	public bool CheckIsPayButRewardNoObtain()
	{
		return this.State == 1;
	}

	public bool CheckFirstPayOn()
	{
		return this.State != 2;
	}

	public bool CheckShowSpine()
	{
		if (this.IsFirstPay())
		{
			return !this.IsFirstPayUIHasOpened;
		}
		return !this.IsFirstPayUIHasOpened;
	}

	private void InitGameData()
	{
		DataReader<ShouChongSongLi>.Init();
		DataReader<DiaoLuo>.Init();
		DataReader<Zu>.Init();
	}

	public XDict<int, long> GetRewardItems(int id)
	{
		XDict<int, long> xDict = new XDict<int, long>();
		List<DiaoLuo> dropsByRuleId = DropUtil.GetDropsByRuleId(id);
		if (dropsByRuleId == null || dropsByRuleId.get_Count() <= 0)
		{
			return xDict;
		}
		for (int i = 0; i < dropsByRuleId.get_Count(); i++)
		{
			DiaoLuo dataDropCfg = dropsByRuleId.get_Item(i);
			int id2 = dataDropCfg.id;
			if (!xDict.ContainsKey(dataDropCfg.goodsId))
			{
				if (dataDropCfg.dropType == 1)
				{
					xDict.Add(dataDropCfg.goodsId, dataDropCfg.maxNum);
				}
				else if (dataDropCfg.dropType == 2)
				{
					int carrer = EntityWorld.Instance.EntSelf.TypeID;
					Zu zu = DataReader<Zu>.DataList.Find((Zu a) => a.groupId == dataDropCfg.goodsId && a.profession == carrer);
					if (zu != null)
					{
						xDict.Add(zu.itemId, dataDropCfg.maxNum);
					}
				}
			}
		}
		return xDict;
	}
}
