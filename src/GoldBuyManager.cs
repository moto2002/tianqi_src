using Package;
using System;
using XNetwork;

public class GoldBuyManager : BaseSubSystemManager
{
	private int mRemainingBuyTimes;

	private int mRemainingFreeTimes;

	private int mExtPrize = 1;

	public static GoldBuyManager m_Instance;

	public int remainingBuyTimes
	{
		get
		{
			return this.mRemainingBuyTimes;
		}
	}

	public int remainingFreeTimes
	{
		get
		{
			return this.mRemainingFreeTimes;
		}
	}

	public int extPrize
	{
		get
		{
			return this.mExtPrize;
		}
	}

	public static GoldBuyManager Instance
	{
		get
		{
			if (GoldBuyManager.m_Instance == null)
			{
				GoldBuyManager.m_Instance = new GoldBuyManager();
			}
			return GoldBuyManager.m_Instance;
		}
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GoldBuyLoginPush>(new NetCallBackMethod<GoldBuyLoginPush>(this.OnGetGoldBuyLoginPush));
		NetworkManager.AddListenEvent<GoldBuyChangedNty>(new NetCallBackMethod<GoldBuyChangedNty>(this.OnGetGoldBuyChangedNty));
		NetworkManager.AddListenEvent<BuyGoldRes>(new NetCallBackMethod<BuyGoldRes>(this.OnGetBuyGoldRes));
	}

	public void SendBuyGoldReq()
	{
		NetworkManager.Send(new BuyGoldReq(), ServerType.Data);
	}

	private void OnGetBuyGoldRes(short state, BuyGoldRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast(EventNames.GoldBuyFail);
			return;
		}
		if (down != null)
		{
			this.mExtPrize = down.extPrize;
			EventDispatcher.Broadcast(EventNames.OnGetBuyGoldRes);
		}
		else
		{
			Debuger.Info("down == null OnGetBuyGoldRes", new object[0]);
		}
	}

	private void OnGetGoldBuyLoginPush(short state, GoldBuyLoginPush down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.mRemainingBuyTimes = down.remainingBuyTimes;
			this.mRemainingFreeTimes = down.remainingFreeTimes;
		}
		else
		{
			Debuger.Info("down == null GoldBuyLoginPush", new object[0]);
		}
	}

	private void OnGetGoldBuyChangedNty(short state, GoldBuyChangedNty down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.mRemainingBuyTimes = down.remainingBuyTimes;
			this.mRemainingFreeTimes = down.remainingFreeTimes;
			EventDispatcher.Broadcast(EventNames.GoldBuyChangedNty);
		}
		else
		{
			Debuger.Info("down == null GoldBuyChangedNty", new object[0]);
		}
	}
}
