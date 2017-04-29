using Foundation.Core;
using GameData;
using System;
using UnityEngine;

public class OOXShoppingUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_ItemFrame = "ItemFrame";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemName = "ItemName";

		public const string Attr_ItemNum = "ItemNum";

		public const string Attr_PriceNowIcon = "PriceNowIcon";

		public const string Attr_PriceNow = "PriceNow";

		public const string Attr_DiscountOn = "DiscountOn";

		public const string Attr_DiscountNumber = "DiscountNumber";

		public const string Attr_LockTip = "LockTip";

		public const string Attr_ItemFlagOn = "ItemFlagOn";

		public const string Attr_ItemFlagOwnOn = "ItemFlagOwnOn";

		public const string Event_OnBtnBuyUp = "OnBtnBuyUp";
	}

	public int iStoreId;

	public int iId;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer _ItemIcon;

	private string _ItemName;

	private string _ItemNum;

	private SpriteRenderer _PriceNowIcon;

	private string _PriceNow;

	private bool _DiscountOn;

	private string _DiscountNumber;

	private string _LockTip;

	private bool _ItemFlagOn;

	private bool _ItemFlagOwnOn;

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

	public SpriteRenderer PriceNowIcon
	{
		get
		{
			return this._PriceNowIcon;
		}
		set
		{
			this._PriceNowIcon = value;
			base.NotifyProperty("PriceNowIcon", value);
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

	public bool DiscountOn
	{
		get
		{
			return this._DiscountOn;
		}
		set
		{
			this._DiscountOn = value;
			base.NotifyProperty("DiscountOn", value);
		}
	}

	public string DiscountNumber
	{
		get
		{
			return this._DiscountNumber;
		}
		set
		{
			this._DiscountNumber = value;
			base.NotifyProperty("DiscountNumber", value);
		}
	}

	public string LockTip
	{
		get
		{
			return this._LockTip;
		}
		set
		{
			this._LockTip = value;
			base.NotifyProperty("LockTip", value);
		}
	}

	public bool ItemFlagOn
	{
		get
		{
			return this._ItemFlagOn;
		}
		set
		{
			this._ItemFlagOn = value;
			base.NotifyProperty("ItemFlagOn", value);
		}
	}

	public bool ItemFlagOwnOn
	{
		get
		{
			return this._ItemFlagOwnOn;
		}
		set
		{
			this._ItemFlagOwnOn = value;
			base.NotifyProperty("ItemFlagOwnOn", value);
		}
	}

	public void OnBtnBuyUp()
	{
		if (this.iStoreId == 3)
		{
			this.Buy2Fashion();
		}
		else
		{
			this.Buy2Normal();
		}
	}

	private void Buy2Normal()
	{
		if (this.ItemFlagOn)
		{
			UIManagerControl.Instance.ShowToastText("购买数量已用完");
			return;
		}
		SShangPinKu sShangPinKu = DataReader<SShangPinKu>.Get(this.iId);
		if (sShangPinKu == null)
		{
			return;
		}
		UIManagerControl.Instance.OpenUI("BuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		BuyUIViewModel.Instance.BuyNumberAdjustOn = true;
		BuyUIViewModel.Instance.BuyCallback = delegate(int count)
		{
			this.DoOK(count);
		};
		BuyUIViewModel.Instance.RefreshInfo(this.iId, sShangPinKu, BaseMarketManager.CurrentManagerInstance.GetCommodityPrice(this.iId, 1), BaseMarketManager.CurrentManagerInstance.GetCommodityMoneyType(this.iId), XMarketManager.Instance.GetMaxBuyCount(this.iId));
	}

	private void DoOK(int count)
	{
		BaseMarketManager.CurrentManagerInstance.Buy(this.iId, count);
	}

	private void Buy2Fashion()
	{
		FashionManager.Instance.OpenBuyFashionUI(this.iId);
	}
}
