using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class NewPeoperGiftPackageManager : BaseSubSystemManager
{
	private TimeCountDown timeCoundDown;

	private NovicePacksPush novicePacksPush;

	private bool isBuyFinish;

	public static NewPeoperGiftPackageManager instance;

	public bool IsBuyFinish
	{
		get
		{
			return this.isBuyFinish;
		}
		set
		{
			this.isBuyFinish = value;
		}
	}

	public static NewPeoperGiftPackageManager Instance
	{
		get
		{
			if (NewPeoperGiftPackageManager.instance == null)
			{
				NewPeoperGiftPackageManager.instance = new NewPeoperGiftPackageManager();
			}
			return NewPeoperGiftPackageManager.instance;
		}
	}

	private NewPeoperGiftPackageManager()
	{
	}

	private void UpdateData(NovicePacksPush packsPush)
	{
		this.novicePacksPush = packsPush;
	}

	public override void Init()
	{
		base.Init();
		this.isBuyFinish = false;
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<NovicePacksPush>(new NetCallBackMethod<NovicePacksPush>(this.OnUpdatePanel));
		NetworkManager.AddListenEvent<BuyPacksRes>(new NetCallBackMethod<BuyPacksRes>(this.OnBuyDiscountItemRes));
		NetworkManager.AddListenEvent<ExpireNty>(new NetCallBackMethod<ExpireNty>(this.OnBuyOverNty));
	}

	public void sendBuyDiscountItemReq()
	{
		if (this.novicePacksPush != null)
		{
			NetworkManager.Send(new BuyPacksReq
			{
				id = this.novicePacksPush.pack.id
			}, ServerType.Data);
		}
	}

	public void OnUpdatePanel(short state, NovicePacksPush novicePacksPush = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.UpdateData(novicePacksPush);
		if (NewPeoperGiftPackage.Instance != null)
		{
			NewPeoperGiftPackage.Instance.showEffect(true);
		}
		this.startTimeCoundDown(novicePacksPush.time);
	}

	public void OnBuyDiscountItemRes(short state, BuyPacksRes buyPacksRes = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (buyPacksRes != null && buyPacksRes.itemsInfo != null)
		{
			List<int> list = new List<int>();
			List<long> list2 = new List<long>();
			List<long> list3 = new List<long>();
			for (int i = 0; i < buyPacksRes.itemsInfo.get_Count(); i++)
			{
				list.Add(buyPacksRes.itemsInfo.get_Item(i).cfgId);
				list2.Add(buyPacksRes.itemsInfo.get_Item(i).count);
				list3.Add(buyPacksRes.itemsInfo.get_Item(i).uId);
			}
			if (list.get_Count() > 0)
			{
				RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
				rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
			}
		}
	}

	public void OnBuyOverNty(short state, ExpireNty expireNty = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.IsBuyFinish = true;
		EventDispatcher.Broadcast(EventNames.OnNewPeoperGiftPackage);
		if (NewPeoperGiftPackage.Instance != null)
		{
			NewPeoperGiftPackage.Instance.OnCloseCurrentUI();
		}
	}

	private void startTimeCoundDown(int time)
	{
		this.ClearTimeCoundDown();
		if (time <= 0)
		{
			return;
		}
		this.timeCoundDown = new TimeCountDown(time, TimeFormat.SECOND, delegate
		{
			if (NewPeoperGiftPackage.Instance != null)
			{
				NewPeoperGiftPackage.Instance.updateTimeCoundDown(TimeConverter.GetTime(this.timeCoundDown.GetSeconds(), TimeFormat.HHMMSS));
			}
		}, delegate
		{
			if (NewPeoperGiftPackage.Instance != null)
			{
				NewPeoperGiftPackage.Instance.updateTimeCoundDown(string.Empty);
			}
		}, true);
	}

	private void ClearTimeCoundDown()
	{
		if (this.timeCoundDown != null)
		{
			this.timeCoundDown.Dispose();
			this.timeCoundDown = null;
		}
	}

	public NovicePacksPush getNovicePacksPush()
	{
		return this.novicePacksPush;
	}
}
