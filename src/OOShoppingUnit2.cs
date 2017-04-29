using Foundation.Core;
using GameData;
using Package;
using System;
using UnityEngine;

public class OOShoppingUnit2 : ObservableObject
{
	public class Names
	{
		public const string Attr_ItemFrame = "ItemFrame";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemName = "ItemName";

		public const string Attr_ItemNum = "ItemNum";

		public const string Attr_ItemFlagIcon = "ItemFlagIcon";

		public const string Attr_ItemFlagIconBg = "ItemFlagIconBg";

		public const string Attr_TipShow = "TipShow";

		public const string Attr_CoinIcon = "CoinIcon";

		public const string Attr_PriceNow = "PriceNow";

		public const string Attr_PriceOld = "PriceOld";

		public const string Attr_EnableBtnBuy = "EnableBtnBuy";

		public const string Attr_PVPIcon = "PVPIcon";

		public const string Attr_PVPName = "PVPName";

		public const string Event_OnBtnBuyUp = "OnBtnBuyUp";
	}

	public int iId;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer _ItemIcon;

	private string _ItemName;

	private string _ItemNum;

	private SpriteRenderer _ItemFlagIcon;

	private bool _ItemFlagIconBg;

	private SpriteRenderer _CoinIcon;

	private string _PriceNow;

	private string _PriceOld;

	private bool _EnableBtnBuy;

	private SpriteRenderer _PVPIcon;

	private string _PVPName;

	private bool _TipShow;

	public SpriteRenderer ItemFrame
	{
		get
		{
			return this._ItemFrame;
		}
		set
		{
			this._ItemFrame = value;
			base.NotifyProperty("ItemFrame", value);
		}
	}

	public SpriteRenderer ItemIcon
	{
		get
		{
			return this._ItemIcon;
		}
		set
		{
			this._ItemIcon = value;
			base.NotifyProperty("ItemIcon", value);
		}
	}

	public string ItemName
	{
		get
		{
			return this._ItemName;
		}
		set
		{
			this._ItemName = value;
			base.NotifyProperty("ItemName", value);
		}
	}

	public string ItemNum
	{
		get
		{
			return this._ItemNum;
		}
		set
		{
			this._ItemNum = value;
			base.NotifyProperty("ItemNum", value);
		}
	}

	public SpriteRenderer ItemFlagIcon
	{
		get
		{
			return this._ItemFlagIcon;
		}
		set
		{
			this._ItemFlagIcon = value;
			base.NotifyProperty("ItemFlagIcon", value);
		}
	}

	public bool ItemFlagIconBg
	{
		get
		{
			return this._ItemFlagIconBg;
		}
		set
		{
			this._ItemFlagIconBg = value;
			base.NotifyProperty("ItemFlagIconBg", value);
		}
	}

	public SpriteRenderer CoinIcon
	{
		get
		{
			return this._CoinIcon;
		}
		set
		{
			this._CoinIcon = value;
			base.NotifyProperty("CoinIcon", value);
		}
	}

	public string PriceNow
	{
		get
		{
			return this._PriceNow;
		}
		set
		{
			this._PriceNow = value;
			base.NotifyProperty("PriceNow", value);
		}
	}

	public string PriceOld
	{
		get
		{
			return this._PriceOld;
		}
		set
		{
			this._PriceOld = value;
			base.NotifyProperty("PriceOld", value);
		}
	}

	public bool EnableBtnBuy
	{
		get
		{
			return this._EnableBtnBuy;
		}
		set
		{
			this._EnableBtnBuy = value;
			base.NotifyProperty("EnableBtnBuy", value);
		}
	}

	public SpriteRenderer PVPIcon
	{
		get
		{
			return this._PVPIcon;
		}
		set
		{
			this._PVPIcon = value;
			base.NotifyProperty("PVPIcon", value);
		}
	}

	public string PVPName
	{
		get
		{
			return this._PVPName;
		}
		set
		{
			this._PVPName = value;
			base.NotifyProperty("PVPName", value);
		}
	}

	public bool TipShow
	{
		get
		{
			return this._TipShow;
		}
		set
		{
			this._TipShow = value;
			base.NotifyProperty("TipShow", value);
		}
	}

	public void OnBtnBuyUp()
	{
		ShangPinBiao shangPinBiao = DataReader<ShangPinBiao>.Get(this.iId);
		if (shangPinBiao == null)
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("BuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		BuyUIViewModel.Instance.BuyCallback = delegate(int count)
		{
			this.DoOK();
		};
		BuyUIViewModel.Instance.RefreshInfo(this.iId, shangPinBiao, BaseMarketManager.CurrentManagerInstance.GetCommodityPrice(this.iId, 1), BaseMarketManager.CurrentManagerInstance.GetCommodityMoneyType(this.iId));
	}

	private void DoOK()
	{
		CommodityInfo commodityInfo = MarketManager.Instance.GetCommodityInfo(this.iId);
		if (commodityInfo != null)
		{
			if (!MarketTools.IsEnoughIntergral(this.iId))
			{
				UIManagerControl.Instance.ShowToastText("段位不足");
				return;
			}
			if (!MarketTools.IsEnoughMoney(BaseMarketManager.CurrentManagerInstance.GetCommodityPrice(this.iId, 1), BaseMarketManager.CurrentManagerInstance.GetCommodityMoneyType(this.iId)))
			{
				UIManagerControl.Instance.OpenSourceReferenceUI(MoneyType.GetItemId(commodityInfo.moneyType), null);
				return;
			}
			MarketManager.Instance.SendBuyShopCommodity(MarketManager.Instance.CurrentShopID, this.iId);
		}
	}
}
