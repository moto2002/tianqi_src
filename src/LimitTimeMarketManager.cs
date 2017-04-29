using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class LimitTimeMarketManager : BaseSubSystemManager
{
	public class EventNames
	{
		public const string FleaShopBadgeTip = "MarketManager.FleaShopBadgeTip";

		public const string FleaShopOpen = "MarketManager.FleaShopOpen";
	}

	private TimeLimitedSalesRes m_TimeLimitedSalesRes;

	private List<BuyGoodsInfo> m_BuyGoodsInfos = new List<BuyGoodsInfo>();

	private static LimitTimeMarketManager instance;

	public bool IsPush;

	public static LimitTimeMarketManager Instance
	{
		get
		{
			if (LimitTimeMarketManager.instance == null)
			{
				LimitTimeMarketManager.instance = new LimitTimeMarketManager();
			}
			return LimitTimeMarketManager.instance;
		}
	}

	private LimitTimeMarketManager()
	{
	}

	public List<SingleGoods> GetCurrentShowGoods()
	{
		if (this.m_TimeLimitedSalesRes == null)
		{
			return null;
		}
		List<SingleGoods> list = new List<SingleGoods>();
		for (int i = 0; i < this.m_TimeLimitedSalesRes.goods.get_Count(); i++)
		{
			SingleGoods singleGoods = this.m_TimeLimitedSalesRes.goods.get_Item(i);
			if (this.IsGoodShow(singleGoods))
			{
				list.Add(singleGoods);
			}
		}
		return list;
	}

	public SingleGoods GetGood(long goodsNumber)
	{
		if (this.m_TimeLimitedSalesRes == null)
		{
			return null;
		}
		for (int i = 0; i < this.m_TimeLimitedSalesRes.goods.get_Count(); i++)
		{
			if (this.m_TimeLimitedSalesRes.goods.get_Item(i).goodsNumber == goodsNumber)
			{
				return this.m_TimeLimitedSalesRes.goods.get_Item(i);
			}
		}
		return null;
	}

	private bool IsGoodShow(SingleGoods good)
	{
		return TimeManager.Instance.GetRemainSecond(good.beginUtc) <= 0 && TimeManager.Instance.GetRemainSecond(good.endUtc) > 0;
	}

	private void UpdateBuyGoodsInfo(long goodsNumber, int remainBuyCount)
	{
		for (int i = 0; i < this.m_BuyGoodsInfos.get_Count(); i++)
		{
			if (this.m_BuyGoodsInfos.get_Item(i).goodsNumber == goodsNumber)
			{
				this.m_BuyGoodsInfos.get_Item(i).remainBuyCount = remainBuyCount;
				return;
			}
		}
		BuyGoodsInfo buyGoodsInfo = new BuyGoodsInfo();
		buyGoodsInfo.goodsNumber = goodsNumber;
		buyGoodsInfo.remainBuyCount = remainBuyCount;
		this.m_BuyGoodsInfos.Add(buyGoodsInfo);
	}

	public int GetRemainBuyCount(long goodsNumber)
	{
		for (int i = 0; i < this.m_BuyGoodsInfos.get_Count(); i++)
		{
			if (this.m_BuyGoodsInfos.get_Item(i).goodsNumber == goodsNumber)
			{
				return this.m_BuyGoodsInfos.get_Item(i).remainBuyCount;
			}
		}
		SingleGoods good = this.GetGood(goodsNumber);
		if (good != null)
		{
			return good.limitBuyCount;
		}
		return 0;
	}

	public override void Init()
	{
		base.Init();
		this.IsPush = false;
	}

	public override void Release()
	{
		this.m_TimeLimitedSalesRes = null;
		this.m_BuyGoodsInfos.Clear();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener("TimeManagerEvent.ZeroPointTrigger", new Callback(this.OnZeroPointTrigger));
		NetworkManager.AddListenEvent<TimeLimitedSalesRes>(new NetCallBackMethod<TimeLimitedSalesRes>(this.OnTimeLimitedSalesRes));
		NetworkManager.AddListenEvent<TimeLimitedSalesInfo>(new NetCallBackMethod<TimeLimitedSalesInfo>(this.OnTimeLimitedSalesInfo));
		NetworkManager.AddListenEvent<TimeLimitedSalesBuyRes>(new NetCallBackMethod<TimeLimitedSalesBuyRes>(this.OnTimeLimitedSalesBuyRes));
		NetworkManager.AddListenEvent<TimeLimitedSalesNty>(new NetCallBackMethod<TimeLimitedSalesNty>(this.OnTimeLimitedSalesNty));
	}

	private void OnZeroPointTrigger()
	{
	}

	public void SendTimeLimitedSales(int page = 1)
	{
		if (this.IsPush)
		{
			return;
		}
		this.IsPush = true;
		this.m_TimeLimitedSalesRes = null;
		this.m_BuyGoodsInfos.Clear();
		NetworkManager.Send(new TimeLimitedSalesReq
		{
			nPage = page
		}, ServerType.Data);
	}

	public void SendTimeLimitedSalesBuy(long goodsNumber)
	{
		NetworkManager.Send(new TimeLimitedSalesBuyReq
		{
			goodsNumber = goodsNumber
		}, ServerType.Data);
	}

	private void OnTimeLimitedSalesRes(short state, TimeLimitedSalesRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_TimeLimitedSalesRes = down;
			this.RefreshUI();
		}
	}

	private void OnTimeLimitedSalesInfo(short state, TimeLimitedSalesInfo down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.m_BuyGoodsInfos = down.buyGoodsInfo;
			this.RefreshUI();
		}
	}

	private void OnTimeLimitedSalesBuyRes(short state, TimeLimitedSalesBuyRes down = null)
	{
		if (state != 0 && down != null)
		{
			StateManager.Instance.StateShow(state, down.itemId);
			return;
		}
		if (down != null)
		{
			this.UpdateBuyGoodsInfo(down.goodsNumber, down.remainBuyCount);
			UIManagerControl.Instance.ShowToastText("购买成功");
			this.RefreshUI();
		}
	}

	private void OnTimeLimitedSalesNty(short state, TimeLimitedSalesNty down = null)
	{
		if (state != 0 && down != null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.ReRequestData();
		}
	}

	private void RefreshUI()
	{
		if (UIManagerControl.Instance.IsOpen("LimitTimeShoppingUI"))
		{
			LimitTimeShoppingUIView.Instance.RefreshUI();
		}
	}

	private void ReRequestData()
	{
		this.IsPush = false;
		if (UIManagerControl.Instance.IsOpen("LimitTimeShoppingUI"))
		{
			this.SendTimeLimitedSales(1);
		}
	}

	public string GetActivityTimeBegin()
	{
		if (this.m_TimeLimitedSalesRes == null)
		{
			return string.Empty;
		}
		return TimeConverter.GetTime(TimeManager.Instance.CalculateLocalServerTimeBySecond(this.m_TimeLimitedSalesRes.beginTime), TimeFormat.MDHHMM);
	}

	public string GetActivityTimeEnd()
	{
		if (this.m_TimeLimitedSalesRes == null)
		{
			return string.Empty;
		}
		return TimeConverter.GetTime(TimeManager.Instance.CalculateLocalServerTimeBySecond(this.m_TimeLimitedSalesRes.endTime), TimeFormat.MDHHMM);
	}
}
