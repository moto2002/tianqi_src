using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class MarketManager : BaseMarketManager
{
	private List<MarketInformation> mShopInformations = new List<MarketInformation>();

	private List<RefreshShopInfo> mShopRefreshFlags = new List<RefreshShopInfo>();

	private List<int> mHasRequestShopIds = new List<int>();

	public int _CurrentShopID;

	private static MarketManager instance;

	private bool _IsFleaShopBadgeTip;

	public int CurrentShopID
	{
		get
		{
			return this._CurrentShopID;
		}
		set
		{
			this._CurrentShopID = value;
			this.SendOpenShop(this._CurrentShopID);
		}
	}

	public static MarketManager Instance
	{
		get
		{
			if (MarketManager.instance == null)
			{
				MarketManager.instance = new MarketManager();
			}
			return MarketManager.instance;
		}
	}

	public bool IsFleaShopBadgeTip
	{
		get
		{
			return this._IsFleaShopBadgeTip;
		}
		set
		{
			this._IsFleaShopBadgeTip = value;
			EventDispatcher.Broadcast<bool>("MarketManager.FleaShopBadgeTip", this._IsFleaShopBadgeTip);
		}
	}

	private MarketManager()
	{
	}

	private void AddToHasRequestShopIds(int shopId)
	{
		if (!this.mHasRequestShopIds.Contains(shopId))
		{
			this.mHasRequestShopIds.Add(shopId);
		}
	}

	private GetShopInfos GetShop(int shopId)
	{
		for (int i = 0; i < this.mShopInformations.get_Count(); i++)
		{
			if (this.mShopInformations.get_Item(i).shopInfo.shopId == shopId)
			{
				return this.mShopInformations.get_Item(i).shopInfo;
			}
		}
		return null;
	}

	public CommodityInfo GetCommodityInfo(int commodityId)
	{
		GetShopInfos shop = this.GetShop(this.CurrentShopID);
		if (shop != null)
		{
			List<CommodityInfo> commodities = shop.commodities;
			for (int i = 0; i < commodities.get_Count(); i++)
			{
				if (commodities.get_Item(i).commodityId == commodityId)
				{
					return commodities.get_Item(i);
				}
			}
		}
		return null;
	}

	private void UpdateShops(List<GetShopInfos> shopInfos)
	{
		for (int i = 0; i < shopInfos.get_Count(); i++)
		{
			this.UpdateOrAddShop(shopInfos.get_Item(i));
		}
	}

	private void UpdateOrAddShop(GetShopInfos shopInfo)
	{
		bool flag = false;
		for (int i = 0; i < this.mShopInformations.get_Count(); i++)
		{
			if (this.mShopInformations.get_Item(i).shopInfo.shopId == shopInfo.shopId)
			{
				flag = true;
				this.mShopInformations.get_Item(i).shopInfo = shopInfo;
				this.mShopInformations.get_Item(i).ResetTimeCountDown(shopInfo.remainingRefreshTime);
				break;
			}
		}
		if (!flag)
		{
			MarketInformation marketInformation = new MarketInformation();
			marketInformation.shopInfo = shopInfo;
			marketInformation.ResetTimeCountDown(shopInfo.remainingRefreshTime);
			this.mShopInformations.Add(marketInformation);
		}
		if (ShoppingUIViewModel.Instance != null && ShoppingUIViewModel.Instance.get_gameObject().get_activeSelf())
		{
			ShoppingUIViewModel.Instance.RefreshShop(shopInfo);
		}
	}

	private void BuySuccess(int commodityId)
	{
		CommodityInfo commodityInfo = this.GetCommodityInfo(commodityId);
		if (commodityInfo != null)
		{
			commodityInfo.sell = true;
		}
	}

	private void UpdateFlags(List<RefreshShopInfo> flags)
	{
		for (int i = 0; i < flags.get_Count(); i++)
		{
			this.UpdateOrAddFlag(flags.get_Item(i));
		}
	}

	private void UpdateOrAddFlag(RefreshShopInfo flag)
	{
		bool flag2 = false;
		for (int i = 0; i < this.mShopRefreshFlags.get_Count(); i++)
		{
			if (this.mShopRefreshFlags.get_Item(i).shopId == flag.shopId)
			{
				flag2 = true;
				this.mShopRefreshFlags.set_Item(i, flag);
				break;
			}
		}
		if (!flag2)
		{
			this.mShopRefreshFlags.Add(flag);
		}
	}

	private void UpdateFlagsToFlase(int shopId)
	{
		for (int i = 0; i < this.mShopRefreshFlags.get_Count(); i++)
		{
			if (this.mShopRefreshFlags.get_Item(i).shopId == shopId)
			{
				this.mShopRefreshFlags.get_Item(i).sysRefreshFlag = false;
				break;
			}
		}
	}

	private void CheckBadgeTip()
	{
		for (int i = 0; i < this.mShopRefreshFlags.get_Count(); i++)
		{
			if (this.mShopRefreshFlags.get_Item(i).shopId == 3 && this.mShopRefreshFlags.get_Item(i).sysRefreshFlag)
			{
				this.IsFleaShopBadgeTip = true;
			}
		}
	}

	private bool IsShopFlagExist(int shopId)
	{
		for (int i = 0; i < this.mShopRefreshFlags.get_Count(); i++)
		{
			if (this.mShopRefreshFlags.get_Item(i).shopId == 3)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsNeedRefresh(int shopId)
	{
		for (int i = 0; i < this.mShopRefreshFlags.get_Count(); i++)
		{
			if (this.mShopRefreshFlags.get_Item(i).shopId == shopId && this.mShopRefreshFlags.get_Item(i).sysRefreshFlag)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsNeedRequestFromServer(int shopId)
	{
		return !this.mHasRequestShopIds.Contains(shopId) || this.IsNeedRefresh(shopId);
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.mShopInformations.Clear();
		this.mShopRefreshFlags.Clear();
		this.mHasRequestShopIds.Clear();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.RoleSelfLevelUp));
		NetworkManager.AddListenEvent<ShopRefreshPush>(new NetCallBackMethod<ShopRefreshPush>(this.OnShopRefreshPush));
		NetworkManager.AddListenEvent<ShopInfoLoginPush>(new NetCallBackMethod<ShopInfoLoginPush>(this.OnShopInfoLoginPush));
		NetworkManager.AddListenEvent<GetShopRes>(new NetCallBackMethod<GetShopRes>(this.OnGetShopRes));
		NetworkManager.AddListenEvent<BuyShopCommodityRes>(new NetCallBackMethod<BuyShopCommodityRes>(this.OnBuyShopCommodityRes));
		NetworkManager.AddListenEvent<OpenShopRes>(new NetCallBackMethod<OpenShopRes>(this.OnOpenShopRes));
	}

	private void RoleSelfLevelUp()
	{
		EventDispatcher.Broadcast<bool>("MarketManager.FleaShopOpen", this.IsFleaShopOpen());
	}

	public void SendOpenShop(int _shopId)
	{
		if (this.IsNeedRequestFromServer(_shopId))
		{
			WaitUI.OpenUI(3000u);
			this.AddToHasRequestShopIds(_shopId);
			NetworkManager.Send(new OpenShopReq
			{
				shopId = _shopId
			}, ServerType.Data);
		}
	}

	public void SendGetShop(int _shopId)
	{
		NetworkManager.Send(new GetShopReq
		{
			shopId = _shopId
		}, ServerType.Data);
	}

	public void SendBuyShopCommodity(int _shopId, int _commodityId)
	{
		NetworkManager.Send(new BuyShopCommodityReq
		{
			shopId = _shopId,
			commodityId = _commodityId
		}, ServerType.Data);
	}

	private void OnShopRefreshPush(short state, ShopRefreshPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.shopInfos != null)
		{
			this.UpdateFlags(down.shopInfos);
			this.CheckBadgeTip();
		}
	}

	private void OnShopInfoLoginPush(short state, ShopInfoLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.shopInfos != null)
		{
			this.UpdateFlags(down.shopInfos);
			this.CheckBadgeTip();
		}
	}

	private void OnGetShopRes(short state, GetShopRes down = null)
	{
		if (state != 0)
		{
			ShangChengBiao shangChengBiao = DataReader<ShangChengBiao>.Get(MarketManager.Instance.CurrentShopID);
			if (shangChengBiao != null && shangChengBiao.refreshCostType == 3)
			{
				UIManagerControl.Instance.OpenSourceReferenceUI(MoneyType.GetItemId(3), null);
			}
			else
			{
				StateManager.Instance.StateShow(state, 0);
			}
			return;
		}
		if (down != null && down.shopInfos != null)
		{
			this.UpdateShops(down.shopInfos);
		}
	}

	private void OnOpenShopRes(short state, OpenShopRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.UpdateShops(down.shopInfos);
			for (int i = 0; i < down.shopInfos.get_Count(); i++)
			{
				this.UpdateFlagsToFlase(down.shopInfos.get_Item(i).shopId);
			}
			this.CheckBadgeTip();
		}
	}

	private void OnBuyShopCommodityRes(short state, BuyShopCommodityRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.BuySuccess(down.commodityId);
			this.RefreshShop();
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(508038, false), 1f, 2f);
		}
	}

	public void OpenShop(int shopId)
	{
		BaseMarketManager.CurrentManagerInstance = this;
		this.CurrentShopID = shopId;
		UIManagerControl.Instance.OpenUI("ShoppingUI", UINodesManager.NormalUIRoot, false, UIType.Pop);
	}

	public bool IsFleaShopOpen()
	{
		GetShopInfos shop = this.GetShop(3);
		if (shop != null)
		{
			int openLv = shop.openLv;
			if (EntityWorld.Instance.EntSelf.Lv >= openLv)
			{
				return true;
			}
		}
		return this.IsShopFlagExist(3);
	}

	public int GetRemainRefreshTime(int shopId)
	{
		for (int i = 0; i < this.mShopInformations.get_Count(); i++)
		{
			if (this.mShopInformations.get_Item(i).shopInfo.shopId == shopId)
			{
				return this.mShopInformations.get_Item(i).shopInfo.remainingRefreshTime;
			}
		}
		return 0;
	}

	public int GetRemainRefreshTimes(int shopId)
	{
		for (int i = 0; i < this.mShopInformations.get_Count(); i++)
		{
			if (this.mShopInformations.get_Item(i).shopInfo.shopId == shopId)
			{
				return this.mShopInformations.get_Item(i).shopInfo.remainRefresh;
			}
		}
		return 0;
	}

	public int GetRefreshPrices(int shopId, int priceId)
	{
		int num = 0;
		for (int i = 0; i < this.mShopInformations.get_Count(); i++)
		{
			if (this.mShopInformations.get_Item(i).shopInfo.shopId == shopId)
			{
				num = this.mShopInformations.get_Item(i).shopInfo.useRefresh;
			}
		}
		int num2 = 0;
		List<ShouDongShuaXinFeiYong> dataList = DataReader<ShouDongShuaXinFeiYong>.DataList;
		for (int j = 0; j < dataList.get_Count(); j++)
		{
			ShouDongShuaXinFeiYong shouDongShuaXinFeiYong = dataList.get_Item(j);
			if (shouDongShuaXinFeiYong.refreshPriceId == priceId)
			{
				if (shouDongShuaXinFeiYong.refreshTime == num + 1)
				{
					num2 = shouDongShuaXinFeiYong.num;
					break;
				}
				if (shouDongShuaXinFeiYong.num > num2)
				{
					num2 = shouDongShuaXinFeiYong.num;
				}
			}
		}
		return num2;
	}

	public override void RefreshShopOnOpen()
	{
		if (this.CurrentShopID == 3)
		{
			this.IsFleaShopBadgeTip = false;
		}
		if (UIManagerControl.Instance.IsOpen("ShoppingUI"))
		{
			ShoppingUIViewModel.Instance.CurrentPageIndex = 0;
			this.RefreshShop();
		}
	}

	public override void RefreshShop()
	{
		if (UIManagerControl.Instance.IsOpen("ShoppingUI"))
		{
			ShoppingUIViewModel.Instance.RefreshShop(this.GetShop(this.CurrentShopID));
		}
	}

	public override void OnRefresh()
	{
		ShangChengBiao shangChengBiao = DataReader<ShangChengBiao>.Get(this.CurrentShopID);
		if (shangChengBiao != null && shangChengBiao.initiativeRefresh == 1)
		{
			if (this.GetRemainRefreshTimes(this.CurrentShopID) <= 0)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(508034, false));
				return;
			}
			int refreshPrices = this.GetRefreshPrices(this.CurrentShopID, shangChengBiao.refreshPrice);
			string name = MoneyType.GetName(shangChengBiao.refreshCostType);
			string content = string.Format(GameDataUtils.GetChineseContent(508021, false), refreshPrices, name);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("刷新", content, delegate
			{
			}, delegate
			{
				this.SendGetShop(this.CurrentShopID);
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
			this.SendBuyShopCommodity(this.CurrentShopID, commodityId);
		}
	}

	public static ShangPinKu GetCommodityPool(int commodityId)
	{
		List<ShangPinKu> dataList = DataReader<ShangPinKu>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).commodityId == commodityId)
			{
				return dataList.get_Item(i);
			}
		}
		return null;
	}

	public static bool IsHighPriority2PVPLevel(int commodityId1, int commodityId2)
	{
		ShangPinBiao shangPinBiao = DataReader<ShangPinBiao>.Get(commodityId1);
		ShangPinBiao shangPinBiao2 = DataReader<ShangPinBiao>.Get(commodityId2);
		return shangPinBiao != null && shangPinBiao2 != null && shangPinBiao.pvpLevel < shangPinBiao2.pvpLevel;
	}

	public static bool IsHighPriority2Pool(int commodityId1, int commodityId2)
	{
		ShangPinKu commodityPool = MarketManager.GetCommodityPool(commodityId1);
		ShangPinKu commodityPool2 = MarketManager.GetCommodityPool(commodityId2);
		return commodityPool != null && commodityPool2 != null && commodityPool.commodityPool < commodityPool2.commodityPool;
	}

	public static bool IsHighPriority2Weight(int commodityId1, int commodityId2)
	{
		ShangPinKu commodityPool = MarketManager.GetCommodityPool(commodityId1);
		ShangPinKu commodityPool2 = MarketManager.GetCommodityPool(commodityId2);
		return commodityPool != null && commodityPool2 != null && commodityPool.weight > commodityPool2.weight;
	}
}
