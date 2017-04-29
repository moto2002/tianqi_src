using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ItemList1 = "ItemList1";

		public const string Attr_ItemList2 = "ItemList2";

		public const string Attr_ShiftType = "ShiftType";

		public const string Attr_CurrentCoinVisibility = "CurrentCoinVisibility";

		public const string Attr_CurrentCoinIcon = "CurrentCoinIcon";

		public const string Attr_CurrentCoinNum = "CurrentCoinNum";

		public const string Attr_MarketName = "MarketName";

		public const string Attr_BtnRefreshVisibility = "BtnRefreshVisibility";

		public const string Attr_RefreshTime = "RefreshTime";

		public const string Attr_RemainRefreshTimesRegion = "RemainRefreshTimesRegion";

		public const string Attr_RemainRefreshTimes = "RemainRefreshTimes";

		public const string Attr_BtnRefreshName = "BtnRefreshName";

		public const string Attr_RefreshCoinIcon = "RefreshCoinIcon";

		public const string Attr_RefreshCoinNum = "RefreshCoinNum";

		public const string Event_OnArrowLUp = "OnArrowLUp";

		public const string Event_OnArrowRUp = "OnArrowRUp";

		public const string Event_OnBtnRefreshUp = "OnBtnRefreshUp";

		public const string Event_OnBtnCloseUp = "OnBtnCloseUp";

		public const string Event_OnBtnSwtichUp = "OnBtnSwtichUp";
	}

	private const int PAGE_NUM = 6;

	private static ShoppingUIViewModel m_instance;

	public int CurrentPageIndex;

	private Vector3 _ShiftType;

	private bool _CurrentCoinVisibility;

	private SpriteRenderer _CurrentCoinIcon;

	private string _CurrentCoinNum;

	private string _MarketName;

	private bool _BtnRefreshVisibility = true;

	private string _RefreshTime;

	private bool _RemainRefreshTimesRegion;

	private string _RemainRefreshTimes;

	private string _BtnRefreshName;

	private SpriteRenderer _RefreshCoinIcon;

	private string _RefreshCoinNum;

	public ObservableCollection<OOShoppingPage> ItemList1 = new ObservableCollection<OOShoppingPage>();

	public ObservableCollection<OOShoppingPage2> ItemList2 = new ObservableCollection<OOShoppingPage2>();

	private TimeCountDown m_TimeCountDown;

	public static ShoppingUIViewModel Instance
	{
		get
		{
			return ShoppingUIViewModel.m_instance;
		}
	}

	public Vector3 ShiftType
	{
		get
		{
			return this._ShiftType;
		}
		set
		{
			this._ShiftType = value;
			base.NotifyProperty("ShiftType", value);
		}
	}

	public bool CurrentCoinVisibility
	{
		get
		{
			return this._CurrentCoinVisibility;
		}
		set
		{
			this._CurrentCoinVisibility = value;
			base.NotifyProperty("CurrentCoinVisibility", value);
		}
	}

	public SpriteRenderer CurrentCoinIcon
	{
		get
		{
			return this._CurrentCoinIcon;
		}
		set
		{
			this._CurrentCoinIcon = value;
			base.NotifyProperty("CurrentCoinIcon", value);
		}
	}

	public string CurrentCoinNum
	{
		get
		{
			return this._CurrentCoinNum;
		}
		set
		{
			this._CurrentCoinNum = value;
			base.NotifyProperty("CurrentCoinNum", value);
		}
	}

	public string MarketName
	{
		get
		{
			return this._MarketName;
		}
		set
		{
			this._MarketName = value;
			base.NotifyProperty("MarketName", value);
		}
	}

	public bool BtnRefreshVisibility
	{
		get
		{
			return this._BtnRefreshVisibility;
		}
		set
		{
			this._BtnRefreshVisibility = value;
			base.NotifyProperty("BtnRefreshVisibility", value);
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

	public bool RemainRefreshTimesRegion
	{
		get
		{
			return this._RemainRefreshTimesRegion;
		}
		set
		{
			this._RemainRefreshTimesRegion = value;
			base.NotifyProperty("RemainRefreshTimesRegion", value);
		}
	}

	public string RemainRefreshTimes
	{
		get
		{
			return this._RemainRefreshTimes;
		}
		set
		{
			this._RemainRefreshTimes = value;
			base.NotifyProperty("RemainRefreshTimes", value);
		}
	}

	public string BtnRefreshName
	{
		get
		{
			return this._BtnRefreshName;
		}
		set
		{
			this._BtnRefreshName = value;
			base.NotifyProperty("BtnRefreshName", value);
		}
	}

	public SpriteRenderer RefreshCoinIcon
	{
		get
		{
			return this._RefreshCoinIcon;
		}
		set
		{
			this._RefreshCoinIcon = value;
			base.NotifyProperty("RefreshCoinIcon", value);
		}
	}

	public string RefreshCoinNum
	{
		get
		{
			return this._RefreshCoinNum;
		}
		set
		{
			this._RefreshCoinNum = value;
			base.NotifyProperty("RefreshCoinNum", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ShoppingUIViewModel.m_instance = this;
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
		this.RefreshCurrentCoin();
	}

	public void OnArrowLUp()
	{
		this.ShiftType = ListShifts.GetShift(2, 0, false);
	}

	public void OnArrowRUp()
	{
		this.ShiftType = ListShifts.GetShift(1, 0, false);
	}

	public void OnBtnRefreshUp()
	{
		BaseMarketManager.CurrentManagerInstance.OnRefresh();
	}

	public void OnBtnCloseUp()
	{
		UIManagerControl.Instance.HideUI("ShoppingUI");
		UIStackManager.Instance.PopUIPrevious(UIType.Pop);
	}

	public void OnBtnSwtichUp()
	{
		if (BaseMarketManager.mMarketClass == MarketClass.MarketManager)
		{
			List<int> shopIds = MarketShopID.GetShopIds();
			List<ButtonInfoData> list = new List<ButtonInfoData>();
			for (int i = 0; i < shopIds.get_Count(); i++)
			{
				int type = shopIds.get_Item(i);
				list.Add(new ButtonInfoData
				{
					buttonName = type.ToString(),
					color = "button_yellow_1",
					onCall = delegate
					{
						MarketManager.Instance.OpenShop(type);
						PopButtonsUIViewModel.Instance.Close();
					}
				});
			}
			if (list.get_Count() > 0)
			{
				ShoppingUIView.Instance.Node2SwitchShops.SetAsLastSibling();
				PopButtonsUIViewModel.Open(ShoppingUIView.Instance.Node2SwitchShops);
				PopButtonsUIViewModel.Instance.SetButtonInfos(list);
			}
		}
		else if (BaseMarketManager.mMarketClass == MarketClass.GuildMarketManager)
		{
		}
	}

	public void RefreshShop(GetShopInfos shop)
	{
		if (BaseMarketManager.mMarketClass != MarketClass.MarketManager)
		{
			return;
		}
		if (shop == null)
		{
			return;
		}
		if (shop.shopId == MarketManager.Instance.CurrentShopID)
		{
			this.ShiftType = ListShifts.GetShift(5, this.CurrentPageIndex, true);
			this.RefreshInfo(shop.remainingRefreshTime, shop.remainRefresh);
			if (shop.shopId == 3)
			{
				this.RefreshItemList1(shop.commodities);
			}
			else if (shop.shopId == 2)
			{
				this.RefreshItemList2(shop.commodities);
			}
			else
			{
				Debug.LogError("请根据商店类型设置商品列表类型!");
			}
		}
	}

	public void RefreshShop(QueryGuildShopInfoRes shop)
	{
		if (BaseMarketManager.mMarketClass != MarketClass.GuildMarketManager)
		{
			return;
		}
		if (shop == null)
		{
			return;
		}
		this.ShiftType = ListShifts.GetShift(5, this.CurrentPageIndex, true);
		this.RefreshInfo(shop.remainingRefreshTime, GuildMarketManager.Instance.GetRemainRefreshTimes());
		this.RefreshItemList1(shop.info);
	}

	public void RefreshItem(QueryGuildShopInfoRes shop, CommodityInfo commodityInfo)
	{
		if (BaseMarketManager.mMarketClass != MarketClass.GuildMarketManager)
		{
			return;
		}
		if (shop == null)
		{
			return;
		}
		this.RefreshInfo(shop.remainingRefreshTime, GuildMarketManager.Instance.GetRemainRefreshTimes());
		if (commodityInfo == null)
		{
			return;
		}
		for (int i = 0; i < this.ItemList1.Count; i++)
		{
			OOShoppingPage oOShoppingPage = this.ItemList1[i];
			for (int j = 0; j < oOShoppingPage.Items.Count; j++)
			{
				if (oOShoppingPage.Items[j].iId == commodityInfo.commodityId)
				{
					this.SetShoppingUnit1(oOShoppingPage.Items[j], commodityInfo);
					return;
				}
			}
		}
	}

	public void RefreshInfo(int remainingRefreshTime, int remainTimes)
	{
		if (BaseMarketManager.mMarketClass == MarketClass.MarketManager)
		{
			ShangChengBiao shangChengBiao = DataReader<ShangChengBiao>.Get(MarketManager.Instance.CurrentShopID);
			if (shangChengBiao != null)
			{
				this.RefreshInfo(remainingRefreshTime, remainTimes, shangChengBiao.title, shangChengBiao.heading, shangChengBiao.initiativeRefresh, shangChengBiao.refreshCostType, MarketManager.Instance.GetRefreshPrices(MarketManager.Instance.CurrentShopID, shangChengBiao.refreshPrice));
			}
		}
		else if (BaseMarketManager.mMarketClass == MarketClass.GuildMarketManager)
		{
			GongHuiShangDian lVShop = GuildMarketManager.Instance.GetLVShop();
			if (lVShop != null)
			{
				this.RefreshInfo(remainingRefreshTime, remainTimes, lVShop.title, lVShop.heading, lVShop.initiativeRefresh, lVShop.refreshCostType, GuildMarketManager.Instance.GetRefreshPrices());
			}
		}
	}

	private void RefreshInfo(int remainingRefreshTime, int remainTimes, int titleId_image, int titleId_chinese, int initiativeRefresh, int refreshCostType, int price)
	{
		this.MarketName = GameDataUtils.GetChineseContent(titleId_chinese, false);
		this.RefreshCurrentCoin();
		if (remainTimes >= 0)
		{
			this.RemainRefreshTimesRegion = true;
			this.RemainRefreshTimes = remainTimes.ToString();
		}
		else
		{
			this.RemainRefreshTimesRegion = false;
			this.RemainRefreshTimes = string.Empty;
		}
		if (this.m_TimeCountDown != null)
		{
			this.m_TimeCountDown.ResetSeconds(remainingRefreshTime);
		}
		else
		{
			this.m_TimeCountDown = new TimeCountDown(remainingRefreshTime, TimeFormat.HHMMSS, delegate
			{
				if (UIManagerControl.Instance.IsOpen("ShoppingUI"))
				{
					this.RefreshTime = this.m_TimeCountDown.GetTime();
				}
			}, delegate
			{
				if (UIManagerControl.Instance.IsOpen("ShoppingUI"))
				{
					this.RefreshTime = string.Empty;
				}
			}, true);
		}
		if (initiativeRefresh == 1)
		{
			this.BtnRefreshVisibility = true;
			this.BtnRefreshName = GameDataUtils.GetChineseContent(508011, false);
			this.RefreshCoinNum = string.Concat(new string[]
			{
				"(       " + price + ")"
			});
			this.RefreshCoinIcon = MoneyType.GetIcon(refreshCostType);
		}
		else
		{
			this.BtnRefreshVisibility = false;
		}
	}

	private void RefreshCurrentCoin()
	{
		if (BaseMarketManager.mMarketClass == MarketClass.MarketManager)
		{
			ShangChengBiao shangChengBiao = DataReader<ShangChengBiao>.Get(MarketManager.Instance.CurrentShopID);
			if (shangChengBiao != null && shangChengBiao.moneyType > 0)
			{
				this.CurrentCoinVisibility = true;
				this.CurrentCoinIcon = MoneyType.GetIcon(shangChengBiao.moneyType);
				this.CurrentCoinNum = MoneyType.GetNum(shangChengBiao.moneyType).ToString();
			}
			else
			{
				this.CurrentCoinVisibility = false;
			}
		}
		else if (BaseMarketManager.mMarketClass == MarketClass.GuildMarketManager)
		{
			GongHuiShangDian lVShop = GuildMarketManager.Instance.GetLVShop();
			if (lVShop != null)
			{
				this.CurrentCoinVisibility = true;
				this.CurrentCoinIcon = MoneyType.GetIcon(lVShop.moneyType);
				this.CurrentCoinNum = MoneyType.GetNum(lVShop.moneyType).ToString();
			}
		}
	}

	private void RefreshItemList1(List<CommodityInfo> commodities)
	{
		ShoppingUIView.Instance.Show2ItemSR.get_gameObject().SetActive(false);
		ShoppingUIView.Instance.Show1ItemSR.get_gameObject().SetActive(true);
		for (int i = 0; i < this.ItemList1.Count; i++)
		{
			this.ItemList1[i].Items.Clear();
		}
		this.ItemList1.Clear();
		if (commodities == null)
		{
			return;
		}
		commodities.Sort(new Comparison<CommodityInfo>(ShoppingUIViewModel.CommoditySortCompare));
		for (int j = 0; j < commodities.get_Count(); j++)
		{
			OOShoppingUnit shoppingUnit = this.GetShoppingUnit1(commodities.get_Item(j));
			int i2 = j / 6;
			if (j % 6 == 0)
			{
				OOShoppingPage o = new OOShoppingPage();
				this.ItemList1.Add(o);
			}
			this.ItemList1[i2].Items.Add(shoppingUnit);
		}
	}

	private OOShoppingUnit GetShoppingUnit1(CommodityInfo commodityInfo)
	{
		OOShoppingUnit oOShoppingUnit = new OOShoppingUnit();
		this.SetShoppingUnit1(oOShoppingUnit, commodityInfo);
		return oOShoppingUnit;
	}

	private void SetShoppingUnit1(OOShoppingUnit gridData, CommodityInfo commodityInfo)
	{
		int itemId = commodityInfo.itemId;
		int commodityId = commodityInfo.commodityId;
		gridData.iId = commodityId;
		if (commodityInfo.sell)
		{
			gridData.EnableBtnBuy = false;
			gridData.ItemFlagIconBg = true;
			gridData.ItemFlagIcon = ResourceManager.GetIconSprite("font_yishouwan");
		}
		else
		{
			gridData.EnableBtnBuy = true;
			gridData.ItemFlagIconBg = false;
			gridData.ItemFlagIcon = ResourceManagerBase.GetNullSprite();
		}
		int num = commodityInfo.unitPrice * commodityInfo.itemNum;
		if ((long)num > MoneyType.GetNum(commodityInfo.moneyType))
		{
			gridData.PriceNow = "x" + num;
		}
		else
		{
			gridData.PriceNow = "x" + num;
		}
		gridData.ItemFrame = GameDataUtils.GetItemFrame(itemId);
		gridData.ItemIcon = GameDataUtils.GetItemIcon(itemId);
		gridData.ItemName = GameDataUtils.GetEquipItemNameAndLV(itemId, false);
		gridData.ItemNum = string.Empty + commodityInfo.itemNum;
		gridData.CoinIcon = MoneyType.GetIcon(commodityInfo.moneyType);
		gridData.PriceOld = string.Empty;
	}

	private void RefreshItemList2(List<CommodityInfo> commodities)
	{
		ShoppingUIView.Instance.Show1ItemSR.get_gameObject().SetActive(false);
		ShoppingUIView.Instance.Show2ItemSR.get_gameObject().SetActive(true);
		for (int i = 0; i < this.ItemList2.Count; i++)
		{
			this.ItemList2[i].Items.Clear();
		}
		this.ItemList2.Clear();
		if (commodities == null)
		{
			return;
		}
		commodities.Sort(new Comparison<CommodityInfo>(ShoppingUIViewModel.CommoditySortCompare));
		for (int j = 0; j < commodities.get_Count(); j++)
		{
			OOShoppingUnit2 shoppingUnit = this.GetShoppingUnit2(commodities.get_Item(j));
			int i2 = j / 6;
			if (j % 6 == 0)
			{
				OOShoppingPage2 o = new OOShoppingPage2();
				this.ItemList2.Add(o);
			}
			this.ItemList2[i2].Items.Add(shoppingUnit);
		}
	}

	private OOShoppingUnit2 GetShoppingUnit2(CommodityInfo commodityInfo)
	{
		int itemId = commodityInfo.itemId;
		int commodityId = commodityInfo.commodityId;
		OOShoppingUnit2 oOShoppingUnit = new OOShoppingUnit2();
		oOShoppingUnit.iId = commodityId;
		if (commodityInfo.sell)
		{
			oOShoppingUnit.EnableBtnBuy = false;
			oOShoppingUnit.ItemFlagIconBg = true;
			oOShoppingUnit.ItemFlagIcon = ResourceManager.GetIconSprite("font_yishouwan");
		}
		else
		{
			oOShoppingUnit.EnableBtnBuy = true;
			oOShoppingUnit.ItemFlagIconBg = false;
			oOShoppingUnit.ItemFlagIcon = ResourceManagerBase.GetNullSprite();
		}
		int num = commodityInfo.unitPrice * commodityInfo.itemNum;
		if ((long)num > MoneyType.GetNum(commodityInfo.moneyType))
		{
			oOShoppingUnit.PriceNow = "x" + num;
		}
		else
		{
			oOShoppingUnit.PriceNow = "x" + num;
		}
		oOShoppingUnit.ItemFrame = GameDataUtils.GetItemFrame(itemId);
		oOShoppingUnit.ItemIcon = GameDataUtils.GetItemIcon(itemId);
		oOShoppingUnit.ItemName = GameDataUtils.GetEquipItemNameAndLV(itemId, false);
		oOShoppingUnit.ItemNum = string.Empty + commodityInfo.itemNum;
		oOShoppingUnit.CoinIcon = MoneyType.GetIcon(commodityInfo.moneyType);
		oOShoppingUnit.PriceOld = string.Empty;
		ShangPinBiao shangPinBiao = DataReader<ShangPinBiao>.Get(commodityInfo.commodityId);
		if (shangPinBiao != null)
		{
			if (shangPinBiao.pvpLevel > 0)
			{
				oOShoppingUnit.PVPIcon = ResourceManager.GetIconSprite(PVPManager.Instance.GetGetIntegralByLevel(shangPinBiao.pvpLevel, false));
				JingJiChangFenDuan jingJiChangFenDuan = DataReader<JingJiChangFenDuan>.Get(shangPinBiao.pvpLevel);
				if (jingJiChangFenDuan != null)
				{
					oOShoppingUnit.PVPName = GameDataUtils.GetChineseContent(jingJiChangFenDuan.name, false);
				}
				oOShoppingUnit.TipShow = (shangPinBiao.pvpLevel > PVPManager.Instance.GetIntegralLevel().id);
			}
			else
			{
				oOShoppingUnit.PVPIcon = ResourceManagerBase.GetNullSprite();
				oOShoppingUnit.PVPName = string.Empty;
				oOShoppingUnit.TipShow = false;
			}
		}
		return oOShoppingUnit;
	}

	private static int CommoditySortCompare(CommodityInfo cInfo1, CommodityInfo cInfo2)
	{
		int result = 0;
		if (cInfo1.commodityId == cInfo2.commodityId)
		{
			result = 0;
		}
		else if (MarketManager.IsHighPriority2PVPLevel(cInfo1.commodityId, cInfo2.commodityId))
		{
			result = -1;
		}
		else if (MarketManager.IsHighPriority2PVPLevel(cInfo2.commodityId, cInfo1.commodityId))
		{
			result = 1;
		}
		else if (MarketManager.IsHighPriority2Pool(cInfo1.commodityId, cInfo2.commodityId))
		{
			result = -1;
		}
		else if (MarketManager.IsHighPriority2Pool(cInfo2.commodityId, cInfo1.commodityId))
		{
			result = 1;
		}
		else if (MarketManager.IsHighPriority2Weight(cInfo1.commodityId, cInfo2.commodityId))
		{
			result = -1;
		}
		else if (MarketManager.IsHighPriority2Weight(cInfo2.commodityId, cInfo1.commodityId))
		{
			result = 1;
		}
		else if (cInfo1.commodityId < cInfo2.commodityId)
		{
			result = -1;
		}
		else if (cInfo1.commodityId > cInfo2.commodityId)
		{
			return 1;
		}
		return result;
	}
}
