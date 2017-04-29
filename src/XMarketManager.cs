using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class XMarketManager : BaseMarketManager
{
	public const int BUY_TIME_NO_LIMIT = -1;

	private const int BUY_MAX_COUNT = 9999;

	public const int SHOP_FASHION_ID = 3;

	private int mCurrentShopID;

	private static XMarketManager instance;

	private List<StoreInfo> mStoreInfo = new List<StoreInfo>();

	public List<StoreRefreshInfo> mStoreRefreshFlags = new List<StoreRefreshInfo>();

	private List<int> m_HasRequestShopIds = new List<int>();

	private Action m_actionCheckFashionServerData;

	public int CurrentShopID
	{
		get
		{
			return this.mCurrentShopID;
		}
		set
		{
			this.mCurrentShopID = value;
			this.SendGetStoreInfo(this.mCurrentShopID);
		}
	}

	public static XMarketManager Instance
	{
		get
		{
			if (XMarketManager.instance == null)
			{
				XMarketManager.instance = new XMarketManager();
			}
			return XMarketManager.instance;
		}
	}

	private XMarketManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.mCurrentShopID = 0;
		this.mStoreInfo.Clear();
		this.mStoreRefreshFlags.Clear();
		this.m_HasRequestShopIds.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<StoreLoginPush>(new NetCallBackMethod<StoreLoginPush>(this.OnStoreLoginPush));
		NetworkManager.AddListenEvent<StoreRefreshPush>(new NetCallBackMethod<StoreRefreshPush>(this.OnStoreRefreshPush));
		NetworkManager.AddListenEvent<GetStoreInfoRes>(new NetCallBackMethod<GetStoreInfoRes>(this.OnGetStoreInfoRes));
		NetworkManager.AddListenEvent<BuyGoodsRes>(new NetCallBackMethod<BuyGoodsRes>(this.OnBuyGoodsRes));
	}

	private void UpdateFlags(List<StoreRefreshInfo> flags)
	{
		for (int i = 0; i < flags.get_Count(); i++)
		{
			this.UpdateOrAddFlag(flags.get_Item(i));
		}
	}

	private void UpdateOrAddFlag(StoreRefreshInfo flag)
	{
		bool flag2 = false;
		for (int i = 0; i < this.mStoreRefreshFlags.get_Count(); i++)
		{
			if (this.mStoreRefreshFlags.get_Item(i).storeId == flag.storeId)
			{
				flag2 = true;
				this.mStoreRefreshFlags.set_Item(i, flag);
				break;
			}
		}
		if (!flag2)
		{
			this.mStoreRefreshFlags.Add(flag);
		}
	}

	private void UpdateFlagsToFlase(int shopId)
	{
		for (int i = 0; i < this.mStoreRefreshFlags.get_Count(); i++)
		{
			if (this.mStoreRefreshFlags.get_Item(i).storeId == shopId)
			{
				this.mStoreRefreshFlags.get_Item(i).sysRefreshFlag = false;
				break;
			}
		}
	}

	private void CheckBadgeTip()
	{
		for (int i = 0; i < this.mStoreRefreshFlags.get_Count(); i++)
		{
			if (this.mStoreRefreshFlags.get_Item(i).sysRefreshFlag)
			{
				EventDispatcher.Broadcast<bool>("MarketManager.XMarketBadgeTip", true);
				return;
			}
		}
		EventDispatcher.Broadcast<bool>("MarketManager.XMarketBadgeTip", false);
	}

	private void AddToHasRequestShopIds(int shopId)
	{
		if (!this.m_HasRequestShopIds.Contains(shopId))
		{
			this.m_HasRequestShopIds.Add(shopId);
		}
	}

	private void UpdateOrAddShop(StoreInfo storeInfo)
	{
		bool flag = false;
		for (int i = 0; i < this.mStoreInfo.get_Count(); i++)
		{
			if (this.mStoreInfo.get_Item(i).storeId == storeInfo.storeId)
			{
				flag = true;
				this.mStoreInfo.set_Item(i, storeInfo);
				break;
			}
		}
		if (!flag)
		{
			this.mStoreInfo.Add(storeInfo);
		}
		if (UIManagerControl.Instance.IsOpen("XShopUI"))
		{
			XShopUIViewModel.Instance.RefreshShop(storeInfo);
		}
	}

	private void UpdateShopBuyTimes(int shopId, int buyTimes)
	{
		StoreInfo shop = this.GetShop(shopId);
		if (shop != null)
		{
			shop.storeExtra.buyTimes = buyTimes;
		}
	}

	private void UpdateSGI(int shopId, StoreGoodsInfo sgi)
	{
		if (sgi == null)
		{
			return;
		}
		StoreInfo shop = this.GetShop(shopId);
		if (shop != null)
		{
			List<StoreGoodsInfo> goodsInfo = shop.goodsInfo;
			for (int i = 0; i < goodsInfo.get_Count(); i++)
			{
				if (goodsInfo.get_Item(i).iId == sgi.iId)
				{
					goodsInfo.set_Item(i, sgi);
				}
			}
		}
	}

	private StoreInfo GetShop(int shopId)
	{
		for (int i = 0; i < this.mStoreInfo.get_Count(); i++)
		{
			if (this.mStoreInfo.get_Item(i).storeId == shopId)
			{
				return this.mStoreInfo.get_Item(i);
			}
		}
		return null;
	}

	public int GetCurrentShopCanBuyMax()
	{
		StoreInfo shop = this.GetShop(this.CurrentShopID);
		if (shop != null && shop.storeExtra.vipLmtTimes != -1)
		{
			return Mathf.Max(0, shop.storeExtra.vipLmtTimes - shop.storeExtra.buyTimes);
		}
		return 9999;
	}

	public StoreGoodsInfo GetCommodityInfo(int iId)
	{
		StoreInfo shop = this.GetShop(this.CurrentShopID);
		if (shop != null)
		{
			List<StoreGoodsInfo> goodsInfo = shop.goodsInfo;
			for (int i = 0; i < goodsInfo.get_Count(); i++)
			{
				if (goodsInfo.get_Item(i).iId == iId)
				{
					return goodsInfo.get_Item(i);
				}
			}
		}
		return null;
	}

	public void SendGetStoreInfo(int _shopId)
	{
		if (this.IsNeedRequestFromServer(_shopId))
		{
			WaitUI.OpenUI(3000u);
			this.AddToHasRequestShopIds(_shopId);
			NetworkManager.Send(new GetStoreInfoReq
			{
				storeId = _shopId
			}, ServerType.Data);
		}
		else
		{
			this.RefreshShop();
		}
	}

	public void SendBuyGoods(int _shopId, int _iId, int _count, int _price = 0)
	{
		NetworkManager.Send(new BuyGoodsReq
		{
			storeId = _shopId,
			iId = _iId,
			count = _count,
			price = _price
		}, ServerType.Data);
	}

	private void OnStoreLoginPush(short state, StoreLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.info != null)
		{
			this.UpdateFlags(down.info);
			this.CheckBadgeTip();
		}
	}

	private void OnStoreRefreshPush(short state, StoreRefreshPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.info != null)
		{
			this.UpdateFlags(down.info);
			this.CheckBadgeTip();
			if (UIManagerControl.Instance.IsOpen("XShopUI") && this.CurrentShopID > 0)
			{
				this.SendGetStoreInfo(this.CurrentShopID);
			}
		}
	}

	private void OnGetStoreInfoRes(short state, GetStoreInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			this.m_actionCheckFashionServerData = null;
			return;
		}
		if (down != null)
		{
			WaitUI.CloseUI(0u);
			this.UpdateOrAddShop(down.storeInfo);
			this.UpdateFlagsToFlase(down.storeInfo.storeId);
			this.CheckBadgeTip();
		}
		if (this.m_actionCheckFashionServerData != null)
		{
			this.m_actionCheckFashionServerData.Invoke();
			this.m_actionCheckFashionServerData = null;
		}
	}

	private void OnBuyGoodsRes(short state, BuyGoodsRes down = null)
	{
		if (state != 0)
		{
			if (state == 4713)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(1005027, false));
			}
			else
			{
				StateManager.Instance.StateShow(state, 0);
			}
			return;
		}
		if (down != null)
		{
			this.UpdateSGI(down.storeId, down.goodsInfo);
			if (down.extra != null)
			{
				this.UpdateShopBuyTimes(down.storeId, down.extra.buyTimes);
			}
			this.RefreshShop();
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(508038, false), 1f, 2f);
		}
	}

	public void OpenShop(int index = 0)
	{
		BaseMarketManager.CurrentManagerInstance = this;
		this.CurrentShopID = this.GetShopID(index);
		UIManagerControl.Instance.OpenUI("XShopUI", null, false, UIType.FullScreen);
	}

	public void OpenFashion()
	{
		BaseMarketManager.CurrentManagerInstance = this;
		this.CurrentShopID = 3;
		UIManagerControl.Instance.OpenUI("XShopUI", null, false, UIType.FullScreen);
	}

	public void CheckFashionServerData(Action action)
	{
		if (this.IsNeedRequestFromServer(3))
		{
			this.m_actionCheckFashionServerData = action;
			this.CurrentShopID = 3;
		}
		else
		{
			this.CurrentShopID = 3;
			if (action != null)
			{
				action.Invoke();
			}
		}
	}

	public int GetShopID(int index)
	{
		List<SShangChengLeiXing> dataList = DataReader<SShangChengLeiXing>.DataList;
		if (index < dataList.get_Count())
		{
			return dataList.get_Item(index).shopId;
		}
		return 0;
	}

	public int GetShopIndex(int shopId)
	{
		List<SShangChengLeiXing> dataList = DataReader<SShangChengLeiXing>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).shopId == shopId)
			{
				return i;
			}
		}
		return 0;
	}

	public int GetMaxBuyCount(int iId)
	{
		StoreGoodsInfo commodityInfo = this.GetCommodityInfo(iId);
		if (commodityInfo.stockCfg == -1)
		{
			return 9999;
		}
		return Mathf.Min(commodityInfo.stockCfg - commodityInfo.buyTimes, this.GetCurrentShopCanBuyMax());
	}

	public void BuyFashion(int iId, int index)
	{
		StoreGoodsInfo commodityInfo = this.GetCommodityInfo(iId);
		if (commodityInfo != null && index < commodityInfo.unitPrice.get_Count())
		{
			this.SendBuyGoods(3, iId, 1, commodityInfo.unitPrice.get_Item(index));
		}
	}

	private bool IsNeedRefresh(int shopId)
	{
		for (int i = 0; i < this.mStoreRefreshFlags.get_Count(); i++)
		{
			if (this.mStoreRefreshFlags.get_Item(i).storeId == shopId && this.mStoreRefreshFlags.get_Item(i).sysRefreshFlag)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsNeedRequestFromServer(int shopId)
	{
		return !this.m_HasRequestShopIds.Contains(shopId) || this.IsNeedRefresh(shopId);
	}

	public override void RefreshShopOnOpen()
	{
		BaseMarketManager.CurrentManagerInstance = this;
		this.RefreshShop();
	}

	public override void RefreshShop()
	{
		if (UIManagerControl.Instance.IsOpen("XShopUI"))
		{
			XShopUIViewModel.Instance.RefreshShop(this.GetShop(this.CurrentShopID));
		}
	}

	public override void OnRefresh()
	{
	}

	public override int GetCommodityPrice(int iId, int group = 1)
	{
		int num = 0;
		StoreGoodsInfo commodityInfo = this.GetCommodityInfo(iId);
		int num2 = 0;
		if (commodityInfo.unitPrice.get_Count() > 0)
		{
			num2 = commodityInfo.unitPrice.get_Item(0);
		}
		int num3 = 0;
		if (commodityInfo.extraInfo != null && commodityInfo.extraInfo.discountIds != null)
		{
			num3 = commodityInfo.extraInfo.discountIds.get_Count();
		}
		if (num3 > 0)
		{
			for (int i = 0; i < group; i++)
			{
				int num4 = commodityInfo.buyTimes + i;
				if (num4 < num3)
				{
					num += num2 * commodityInfo.extraInfo.discountIds.get_Item(num4) / 10;
				}
				else
				{
					num += num2 * commodityInfo.extraInfo.discountIds.get_Item(num3 - 1) / 10;
				}
			}
		}
		else
		{
			num = num2 * group;
		}
		return num;
	}

	public override int GetCommodityMoneyType(int iId)
	{
		StoreGoodsInfo commodityInfo = this.GetCommodityInfo(iId);
		return commodityInfo.moneyType;
	}

	public override void Buy(int commodityId, int count)
	{
		this.SendBuyGoods(this.CurrentShopID, commodityId, count, 0);
	}
}
