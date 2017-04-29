using Foundation.Core;
using GameData;
using System;
using UnityEngine;

public class OOShoppingUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_ItemFrame = "ItemFrame";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemName = "ItemName";

		public const string Attr_ItemNum = "ItemNum";

		public const string Attr_ItemFlagIcon = "ItemFlagIcon";

		public const string Attr_ItemFlagIconBg = "ItemFlagIconBg";

		public const string Attr_CoinIcon = "CoinIcon";

		public const string Attr_PriceNow = "PriceNow";

		public const string Attr_PriceOld = "PriceOld";

		public const string Attr_EnableBtnBuy = "EnableBtnBuy";

		public const string Event_OnBtnBuyUp = "OnBtnBuyUp";
	}

	public int iId;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer _ItemIcon;

	private string _ItemName;

	private string _ItemNum;

	private SpriteRenderer _ItemFlagIcon;

	private bool _ItemFlagIconBg;

	private bool _EnableBtnBuy;

	private SpriteRenderer _CoinIcon;

	private string _PriceNow;

	private string _PriceOld;

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
			this.DoOK(count);
		};
		BuyUIViewModel.Instance.RefreshInfo(this.iId, shangPinBiao, BaseMarketManager.CurrentManagerInstance.GetCommodityPrice(this.iId, 1), BaseMarketManager.CurrentManagerInstance.GetCommodityMoneyType(this.iId));
	}

	private void DoOK(int count)
	{
		BaseMarketManager.CurrentManagerInstance.Buy(this.iId, count);
	}
}
