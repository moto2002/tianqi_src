using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class GuildMarketManager : BaseMarketManager
{
	private GuildShopExtraData mGuildShopExtraData;

	private GuildMarketInformation mGuildMarketInformation;

	private bool mIsHasRequestGuildShopInfo;

	private static GuildMarketManager instance;

	public static GuildMarketManager Instance
	{
		get
		{
			if (GuildMarketManager.instance == null)
			{
				GuildMarketManager.instance = new GuildMarketManager();
			}
			return GuildMarketManager.instance;
		}
	}

	private GuildMarketManager()
	{
	}

	public GongHuiShangDian GetLVShop()
	{
		if (this.mGuildMarketInformation == null)
		{
			Debug.Log("mGuildMarketInformation is null.");
			return null;
		}
		List<GongHuiShangDian> dataList = DataReader<GongHuiShangDian>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			GongHuiShangDian gongHuiShangDian = dataList.get_Item(i);
			if (gongHuiShangDian.lv == this.mGuildMarketInformation.shopInfo.shopLv)
			{
				return gongHuiShangDian;
			}
		}
		return null;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.mGuildShopExtraData = null;
		this.mGuildMarketInformation = null;
		this.mIsHasRequestGuildShopInfo = false;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GuildShopPush>(new NetCallBackMethod<GuildShopPush>(this.OnGuildShopPush));
		NetworkManager.AddListenEvent<QueryGuildShopInfoRes>(new NetCallBackMethod<QueryGuildShopInfoRes>(this.OnQueryGuildShopInfoRes));
		NetworkManager.AddListenEvent<BuyRes>(new NetCallBackMethod<BuyRes>(this.OnBuyRes));
		NetworkManager.AddListenEvent<CostRefreshGuildShopRes>(new NetCallBackMethod<CostRefreshGuildShopRes>(this.OnCostRefreshGuildShopRes));
	}

	public void SendQueryGuildShopInfo()
	{
		if (!this.mIsHasRequestGuildShopInfo || (this.mGuildShopExtraData != null && this.mGuildShopExtraData.refreshFlag))
		{
			WaitUI.OpenUI(3000u);
			this.mIsHasRequestGuildShopInfo = true;
			NetworkManager.Send(new QueryGuildShopInfoReq(), ServerType.Data);
		}
	}

	public void SendBuy(int _goodsId)
	{
		NetworkManager.Send(new BuyReq
		{
			goodsId = _goodsId
		}, ServerType.Data);
	}

	public void SendCostRefreshGuildShop()
	{
		NetworkManager.Send(new CostRefreshGuildShopReq(), ServerType.Data);
	}

	private void OnGuildShopPush(short state, GuildShopPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.mGuildShopExtraData = down.data;
		}
	}

	private void OnQueryGuildShopInfoRes(short state, QueryGuildShopInfoRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.mGuildMarketInformation == null)
			{
				this.mGuildMarketInformation = new GuildMarketInformation();
			}
			this.mGuildMarketInformation.shopInfo = down;
			this.mGuildMarketInformation.ResetTimeCountDown(down.remainingRefreshTime);
			this.RefreshShop();
		}
	}

	private void OnBuyRes(short state, BuyRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.BuySuccess(down.goodsId);
		}
	}

	private void OnCostRefreshGuildShopRes(short state, CostRefreshGuildShopRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.mGuildMarketInformation.shopInfo.info.Clear();
			this.mGuildMarketInformation.shopInfo.info.AddRange(down.info);
			this.mGuildMarketInformation.shopInfo.refreshLimit = down.refreshLimit;
			this.mGuildMarketInformation.shopInfo.remainingRefreshTime = down.remainingRefreshTime;
			this.mGuildMarketInformation.shopInfo.shopLv = down.shopLv;
			this.mGuildMarketInformation.shopInfo.useRefresh = down.useRefresh;
			this.mGuildMarketInformation.ResetTimeCountDown(down.remainingRefreshTime);
			this.RefreshShop();
		}
	}

	public void OpenShop()
	{
		this.SendQueryGuildShopInfo();
		BaseMarketManager.CurrentManagerInstance = this;
		UIManagerControl.Instance.OpenUI("ShoppingUI", UINodesManager.NormalUIRoot, false, UIType.Pop);
	}

	public CommodityInfo GetCommodityInfo(int commodityId)
	{
		if (this.mGuildMarketInformation != null)
		{
			List<CommodityInfo> info = this.mGuildMarketInformation.shopInfo.info;
			for (int i = 0; i < info.get_Count(); i++)
			{
				if (info.get_Item(i).commodityId == commodityId)
				{
					return info.get_Item(i);
				}
			}
		}
		return null;
	}

	private void BuySuccess(int commodityId)
	{
		CommodityInfo commodityInfo = this.GetCommodityInfo(commodityId);
		if (commodityInfo != null)
		{
			commodityInfo.sell = true;
		}
		this.RefreshItem(commodityInfo);
	}

	public int GetRemainRefreshTimes()
	{
		return this.mGuildMarketInformation.shopInfo.refreshLimit - this.mGuildMarketInformation.shopInfo.useRefresh;
	}

	public int GetRefreshPrices()
	{
		int useRefresh = this.mGuildMarketInformation.shopInfo.useRefresh;
		GongHuiShangDian lVShop = this.GetLVShop();
		string[] array = GameDataUtils.SplitString4Dot0(lVShop.RefreshPrice).Split(new char[]
		{
			';'
		});
		if (array.Length <= 0)
		{
			return 0;
		}
		if (useRefresh < array.Length)
		{
			return int.Parse(array[useRefresh]);
		}
		return int.Parse(array[array.Length - 1]);
	}

	public void RefreshItem(CommodityInfo commodityInfo)
	{
		if (UIManagerControl.Instance.IsOpen("ShoppingUI") && this.mGuildMarketInformation != null)
		{
			ShoppingUIViewModel.Instance.RefreshItem(this.mGuildMarketInformation.shopInfo, commodityInfo);
		}
	}

	public override void RefreshShopOnOpen()
	{
		if (UIManagerControl.Instance.IsOpen("ShoppingUI"))
		{
			ShoppingUIViewModel.Instance.CurrentPageIndex = 0;
			this.RefreshShop();
		}
	}

	public override void RefreshShop()
	{
		if (UIManagerControl.Instance.IsOpen("ShoppingUI") && this.mGuildMarketInformation != null)
		{
			ShoppingUIViewModel.Instance.RefreshShop(this.mGuildMarketInformation.shopInfo);
		}
	}

	public override void OnRefresh()
	{
		GongHuiShangDian lVShop = this.GetLVShop();
		if (lVShop != null && lVShop.initiativeRefresh == 1)
		{
			if (this.GetRemainRefreshTimes() <= 0)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(508034, false));
				return;
			}
			int refreshPrices = this.GetRefreshPrices();
			string name = MoneyType.GetName(lVShop.refreshCostType);
			string content = string.Format(GameDataUtils.GetChineseContent(508021, false), refreshPrices, name);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("刷新", content, delegate
			{
			}, delegate
			{
				this.SendCostRefreshGuildShop();
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
	}

	public override int GetCommodityPrice(int iId, int group = 1)
	{
		CommodityInfo commodityInfo = this.GetCommodityInfo(iId);
		return commodityInfo.unitPrice * commodityInfo.itemNum;
	}

	public override int GetCommodityMoneyType(int iId)
	{
		CommodityInfo commodityInfo = this.GetCommodityInfo(iId);
		return commodityInfo.moneyType;
	}

	public override void Buy(int commodityId, int count)
	{
		CommodityInfo commodityInfo = this.GetCommodityInfo(commodityId);
		if (commodityInfo != null)
		{
			this.SendBuy(commodityId);
		}
	}
}
