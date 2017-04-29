using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class XShopUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ButtonShops = "ButtonShops";

		public const string Attr_BuyTimeName = "BuyTimeName";

		public const string Attr_RefreshTime = "RefreshTime";

		public const string Attr_ItemList = "ItemList";
	}

	private static XShopUIViewModel m_instance;

	private string _BuyTimeName;

	private string _RefreshTime;

	public ObservableCollection<OOButtonToggle2SubUI> ButtonShops = new ObservableCollection<OOButtonToggle2SubUI>();

	public ObservableCollection<OOXShoppingUnit> ItemList = new ObservableCollection<OOXShoppingUnit>();

	public static XShopUIViewModel Instance
	{
		get
		{
			return XShopUIViewModel.m_instance;
		}
	}

	public string BuyTimeName
	{
		get
		{
			return this._BuyTimeName;
		}
		set
		{
			this._BuyTimeName = value;
			base.NotifyProperty("BuyTimeName", value);
		}
	}

	public string RefreshTime
	{
		get
		{
			return this._RefreshTime;
		}
		set
		{
			this._RefreshTime = value;
			base.NotifyProperty("RefreshTime", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		XShopUIViewModel.m_instance = this;
		this.SetButtonShops();
	}

	private void OnEnable()
	{
		BaseMarketManager.CurrentManagerInstance.RefreshShopOnOpen();
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.GoldChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.DiamondChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.CompetitiveCurrencyChanged, new Callback(this.OnGetRoleAttrChangedNty));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.GoldChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.DiamondChanged, new Callback(this.OnGetRoleAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.CompetitiveCurrencyChanged, new Callback(this.OnGetRoleAttrChangedNty));
	}

	private void OnGetRoleAttrChangedNty()
	{
	}

	public void RefreshShop(StoreInfo storeInfo)
	{
		if (storeInfo == null || storeInfo.storeExtra == null)
		{
			return;
		}
		bool flag = storeInfo.storeExtra.vipLmtTimes == -1 || storeInfo.storeExtra.refreshData.get_Count() == 0;
		this.SetBuyTimes(!flag, storeInfo.storeExtra.buyTimes, storeInfo.storeExtra.vipLmtTimes, this.GetLimitRefreshTime(storeInfo));
		this.SetRefreshTime(storeInfo);
		this.RefreshXItems(storeInfo);
	}

	private void SetBuyTimes(bool isShow, int hasBuy = 0, int limitBuy = 0, string time = "")
	{
		if (isShow)
		{
			this.BuyTimeName = string.Format(GameDataUtils.GetChineseContent(508045, false), Mathf.Max(0, limitBuy - hasBuy), limitBuy, time);
		}
		else
		{
			this.BuyTimeName = string.Empty;
		}
	}

	private void SetRefreshTime(StoreInfo storeInfo = null)
	{
		if (storeInfo.storeExtra.refreshData.get_Count() > 0 && !string.IsNullOrEmpty(storeInfo.storeExtra.stockRefreshTime))
		{
			if (storeInfo.storeExtra.refreshData.get_Item(0) == 0)
			{
				this.RefreshTime = string.Format(GameDataUtils.GetChineseContent(508042, false), this.GetStockRefreshTime(storeInfo));
			}
			else
			{
				int num = 0;
				string empty = string.Empty;
				this.CalRefreshTime(storeInfo, ref num, ref empty);
				this.RefreshTime = string.Format(GameDataUtils.GetChineseContent(508046, false), GameDataUtils.GetChineseContent(513518 + num, false), empty);
			}
		}
		else
		{
			this.RefreshTime = string.Empty;
		}
	}

	private void CalRefreshTime(StoreInfo storeInfo, ref int day, ref string time)
	{
		if (storeInfo.storeExtra.refreshData.get_Count() == 0)
		{
			return;
		}
		time = this.GetStockRefreshTime(storeInfo);
		int num = TimeManager.Instance.PreciseServerTime.get_DayOfWeek();
		if (num == 0)
		{
			num = 7;
		}
		if (this.IsContainToday(storeInfo, num) && this.IsTodayRefreshTimeValid(storeInfo))
		{
			day = num;
		}
		else
		{
			day = this.GetRefreshDayNoToday(storeInfo, num);
		}
	}

	private bool IsContainToday(StoreInfo storeInfo, int nowDay)
	{
		for (int i = 0; i < storeInfo.storeExtra.refreshData.get_Count(); i++)
		{
			if (storeInfo.storeExtra.refreshData.get_Item(i) == nowDay)
			{
				return true;
			}
		}
		return false;
	}

	private int GetRefreshDayNoToday(StoreInfo storeInfo, int nowDay)
	{
		for (int i = 0; i < storeInfo.storeExtra.refreshData.get_Count(); i++)
		{
			if (storeInfo.storeExtra.refreshData.get_Item(i) > nowDay)
			{
				return storeInfo.storeExtra.refreshData.get_Item(i);
			}
		}
		return storeInfo.storeExtra.refreshData.get_Item(0);
	}

	private string GetStockRefreshTime(StoreInfo storeInfo)
	{
		return storeInfo.storeExtra.stockRefreshTime;
	}

	private bool IsTodayRefreshTimeValid(StoreInfo storeInfo)
	{
		string[] array = storeInfo.storeExtra.stockRefreshTime.Split(new char[]
		{
			':'
		});
		int num = 0;
		int num2 = 0;
		if (array.Length >= 1)
		{
			num = int.Parse(array[0]);
		}
		if (array.Length >= 2)
		{
			num2 = int.Parse(array[1]);
		}
		int hour = TimeManager.Instance.PreciseServerTime.get_Hour();
		int minute = TimeManager.Instance.PreciseServerTime.get_Minute();
		return hour < num || (hour == num && minute < num2);
	}

	private string GetLimitRefreshTime(StoreInfo storeInfo)
	{
		string text = string.Empty;
		for (int i = 0; i < storeInfo.storeExtra.LmtRefreshTime.get_Count(); i++)
		{
			if (i == 0)
			{
				text += storeInfo.storeExtra.LmtRefreshTime.get_Item(i);
			}
			else
			{
				text = text + "/" + storeInfo.storeExtra.LmtRefreshTime.get_Item(i);
			}
		}
		return text;
	}

	private void SetButtonShops()
	{
		this.ButtonShops.Clear();
		List<SShangChengLeiXing> dataList = DataReader<SShangChengLeiXing>.DataList;
		int shopIndex = XMarketManager.Instance.GetShopIndex(XMarketManager.Instance.CurrentShopID);
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			SShangChengLeiXing sShangChengLeiXing = dataList.get_Item(i);
			if (sShangChengLeiXing != null)
			{
				OOButtonToggle2SubUI oOButtonToggle2SubUI = new OOButtonToggle2SubUI();
				oOButtonToggle2SubUI.ToggleIndex = i;
				oOButtonToggle2SubUI.Action2CallBack = new Action<int>(this.SetShopOn);
				oOButtonToggle2SubUI.Name = GameDataUtils.GetChineseContent(sShangChengLeiXing.title, false);
				oOButtonToggle2SubUI.IsTip = false;
				if (i == shopIndex)
				{
					oOButtonToggle2SubUI.SetIsToggleOn(true);
				}
				else
				{
					oOButtonToggle2SubUI.SetIsToggleOn(false);
				}
				this.ButtonShops.Add(oOButtonToggle2SubUI);
			}
		}
	}

	private void SetShopOn(int index)
	{
		XMarketManager.Instance.CurrentShopID = XMarketManager.Instance.GetShopID(index);
	}

	private void RefreshXItems(StoreInfo storeInfo)
	{
		if (storeInfo.storeId == XMarketManager.Instance.CurrentShopID)
		{
			this.ItemList.Clear();
			if (storeInfo.storeId == 3)
			{
				storeInfo.goodsInfo.Sort(new Comparison<StoreGoodsInfo>(XShopUIViewModel.SortCompare2Fashion));
				for (int i = 0; i < storeInfo.goodsInfo.get_Count(); i++)
				{
					this.ItemList.Add(this.GetItem2Fashion(storeInfo.storeId, storeInfo.goodsInfo.get_Item(i)));
				}
			}
			else
			{
				storeInfo.goodsInfo.Sort(new Comparison<StoreGoodsInfo>(XShopUIViewModel.SortCompare2Item));
				for (int j = 0; j < storeInfo.goodsInfo.get_Count(); j++)
				{
					this.ItemList.Add(this.GetItem2Normal(storeInfo.storeId, storeInfo.goodsInfo.get_Item(j)));
				}
			}
		}
	}

	private OOXShoppingUnit GetItem2Normal(int storeId, StoreGoodsInfo sgi)
	{
		OOXShoppingUnit oOXShoppingUnit = new OOXShoppingUnit();
		oOXShoppingUnit.iStoreId = storeId;
		oOXShoppingUnit.iId = sgi.iId;
		int itemId = sgi.itemId;
		oOXShoppingUnit.ItemFrame = GameDataUtils.GetItemFrame(itemId);
		oOXShoppingUnit.ItemIcon = GameDataUtils.GetItemIcon(itemId);
		oOXShoppingUnit.ItemName = GameDataUtils.GetEquipItemNameAndLV(itemId, false);
		if (sgi.stockCfg == -1)
		{
			oOXShoppingUnit.ItemNum = string.Empty;
			oOXShoppingUnit.ItemFlagOn = false;
		}
		else
		{
			int num = Mathf.Max(0, sgi.stockCfg - sgi.buyTimes);
			oOXShoppingUnit.ItemNum = num + "/" + sgi.stockCfg;
			if (num > 0 && XMarketManager.Instance.GetCurrentShopCanBuyMax() > 0)
			{
				oOXShoppingUnit.ItemFlagOn = false;
			}
			else
			{
				oOXShoppingUnit.ItemFlagOn = true;
			}
		}
		if (sgi.extraInfo.vipLvLmt > 0 && sgi.extraInfo.vipLvLmt > EntityWorld.Instance.EntSelf.VipLv)
		{
			oOXShoppingUnit.LockTip = string.Format(GameDataUtils.GetChineseContent(508054, false), sgi.extraInfo.vipLvLmt);
			oOXShoppingUnit.PriceNowIcon = ResourceManagerBase.GetNullSprite();
			oOXShoppingUnit.PriceNow = string.Empty;
		}
		else
		{
			oOXShoppingUnit.LockTip = string.Empty;
			oOXShoppingUnit.PriceNowIcon = MoneyType.GetIcon(sgi.moneyType);
			oOXShoppingUnit.PriceNow = "x" + BaseMarketManager.CurrentManagerInstance.GetCommodityPrice(sgi.iId, 1);
		}
		if (!oOXShoppingUnit.ItemFlagOn && sgi.extraInfo.discountIds.get_Count() > 0)
		{
			oOXShoppingUnit.DiscountOn = true;
			SZheKouPeiZhi sZheKouPeiZhi = DataReader<SZheKouPeiZhi>.Get(this.GetDiscountNow(sgi));
			if (sZheKouPeiZhi != null)
			{
				oOXShoppingUnit.DiscountNumber = GameDataUtils.GetChineseContent(sZheKouPeiZhi.icon, false);
			}
		}
		else
		{
			oOXShoppingUnit.DiscountOn = false;
		}
		oOXShoppingUnit.ItemFlagOwnOn = false;
		return oOXShoppingUnit;
	}

	private int GetDiscountNow(StoreGoodsInfo sgi)
	{
		int buyTimes = sgi.buyTimes;
		if (buyTimes < sgi.extraInfo.discountIds.get_Count())
		{
			return sgi.extraInfo.discountIds.get_Item(buyTimes);
		}
		return sgi.extraInfo.discountIds.get_Item(sgi.extraInfo.discountIds.get_Count() - 1);
	}

	private static int SortCompare2Item(StoreGoodsInfo sgi1, StoreGoodsInfo sgi2)
	{
		int result = 0;
		if (sgi1.iId == sgi2.iId)
		{
			result = 0;
		}
		else if (sgi1.iId < sgi2.iId)
		{
			result = -1;
		}
		else if (sgi1.iId > sgi2.iId)
		{
			result = 1;
		}
		return result;
	}

	private OOXShoppingUnit GetItem2Fashion(int storeId, StoreGoodsInfo sgi)
	{
		OOXShoppingUnit oOXShoppingUnit = new OOXShoppingUnit();
		oOXShoppingUnit.iStoreId = storeId;
		oOXShoppingUnit.iId = sgi.iId;
		int itemId = sgi.itemId;
		oOXShoppingUnit.ItemFrame = GameDataUtils.GetItemFrame(itemId);
		oOXShoppingUnit.ItemIcon = GameDataUtils.GetItemIcon(itemId);
		oOXShoppingUnit.ItemName = GameDataUtils.GetEquipItemNameAndLV(itemId, false);
		oOXShoppingUnit.ItemNum = string.Empty;
		oOXShoppingUnit.ItemFlagOn = false;
		oOXShoppingUnit.LockTip = string.Empty;
		oOXShoppingUnit.PriceNowIcon = MoneyType.GetIcon(sgi.moneyType);
		oOXShoppingUnit.PriceNow = "x" + BaseMarketManager.CurrentManagerInstance.GetCommodityPrice(sgi.iId, 1);
		oOXShoppingUnit.DiscountOn = false;
		oOXShoppingUnit.ItemFlagOwnOn = FashionManager.Instance.IsHasEternalFashion(sgi.iId);
		return oOXShoppingUnit;
	}

	private static int SortCompare2Fashion(StoreGoodsInfo sgi1, StoreGoodsInfo sgi2)
	{
		int result = 0;
		if (sgi1.iId == sgi2.iId)
		{
			result = 0;
		}
		else
		{
			if (!FashionManager.Instance.IsHasEternalFashion(sgi1.iId) && FashionManager.Instance.IsHasEternalFashion(sgi2.iId))
			{
				return -1;
			}
			if (FashionManager.Instance.IsHasEternalFashion(sgi1.iId) && !FashionManager.Instance.IsHasEternalFashion(sgi2.iId))
			{
				return 1;
			}
			if (sgi1.iId < sgi2.iId)
			{
				result = -1;
			}
			else if (sgi1.iId > sgi2.iId)
			{
				result = 1;
			}
		}
		return result;
	}
}
