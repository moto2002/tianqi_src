using Foundation.Core;
using GameData;
using Package;
using System;
using UnityEngine;

public class OORechargeUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_PriceNum = "PriceNum";

		public const string Attr_ExtraNum = "ExtraNum";

		public const string Attr_ExtraEveryDayNum = "ExtraEveryDayNum";

		public const string Attr_DiamondNum = "DiamondNum";

		public const string Attr_VIPTipVisibility = "VIPTipVisibility";

		public const string Attr_VIPTipNum = "VIPTipNum";

		public const string Attr_VIPExtraVisibility = "VIPExtraVisibility";

		public const string Attr_MCDayVisibility = "MCDayVisibility";

		public const string Attr_PriceVisibility = "PriceVisibility";

		public const string Attr_ExtraEveryDayVisibility = "ExtraEveryDayVisibility";

		public const string Attr_BigIcon = "BigIcon";

		public const string Attr_MCDayNum = "MCDayNum";

		public const string Attr_DaysNum = "DaysNum";

		public const string Attr_RefreshFXOfMonthCard = "RefreshFXOfMonthCard";

		public const string Attr_DailyLimitIconVisibility = "DailyLimitIconVisibility";

		public const string Attr_TodayBoughtVisibility = "TodayBoughtVisibility";

		public const string Attr_TreasureName = "TreasureName";

		public const string Attr_TreasureIcon = "TreasureIcon";

		public const string Attr_TreasureVisibility = "TreasureVisibility";

		public const string Attr_DiamondVisibility = "DiamondVisibility";

		public const string Event_OnBtnUp = "OnBtnUp";
	}

	private RechargeManager.RechargeType _RType = RechargeManager.RechargeType.Diamond;

	public int ID;

	private SpriteRenderer _BigIcon;

	private string _PriceNum;

	private string _ExtraNum;

	private string _ExtraEveryDayNum;

	private string _DiamondNum;

	private bool _VIPTipVisibility;

	private string _VIPTipNum;

	private bool _VIPExtraVisibility;

	private bool _ExtraEveryDayVisibility;

	private bool _MCDayVisibility;

	private string _MCDayNum;

	private string _DaysNum;

	private bool _PriceVisibility;

	private bool _RefreshFXOfMonthCard;

	private bool _DailyLimitIconVisibility;

	private bool _TodayBoughtVisibility;

	private SpriteRenderer _TreasureIcon;

	private string _TreasureName;

	private bool _TreasureVisibility;

	private bool _DiamondVisibility;

	public RechargeManager.RechargeType RType
	{
		get
		{
			return this._RType;
		}
		set
		{
			this._RType = value;
		}
	}

	public SpriteRenderer BigIcon
	{
		get
		{
			return this._BigIcon;
		}
		set
		{
			this._BigIcon = value;
			base.NotifyProperty("BigIcon", value);
		}
	}

	public string PriceNum
	{
		get
		{
			return this._PriceNum;
		}
		set
		{
			this._PriceNum = value;
			base.NotifyProperty("PriceNum", value);
		}
	}

	public string ExtraNum
	{
		get
		{
			return this._ExtraNum;
		}
		set
		{
			this._ExtraNum = value;
			base.NotifyProperty("ExtraNum", value);
		}
	}

	public string ExtraEveryDayNum
	{
		get
		{
			return this._ExtraEveryDayNum;
		}
		set
		{
			this._ExtraEveryDayNum = value;
			base.NotifyProperty("ExtraEveryDayNum", value);
		}
	}

	public string DiamondNum
	{
		get
		{
			return this._DiamondNum;
		}
		set
		{
			this._DiamondNum = value;
			base.NotifyProperty("DiamondNum", value);
		}
	}

	public bool VIPTipVisibility
	{
		get
		{
			return this._VIPTipVisibility;
		}
		set
		{
			this._VIPTipVisibility = value;
			base.NotifyProperty("VIPTipVisibility", value);
		}
	}

	public string VIPTipNum
	{
		get
		{
			return this._VIPTipNum;
		}
		set
		{
			this._VIPTipNum = value;
			base.NotifyProperty("VIPTipNum", value);
		}
	}

	public bool VIPExtraVisibility
	{
		get
		{
			return this._VIPExtraVisibility;
		}
		set
		{
			this._VIPExtraVisibility = value;
			base.NotifyProperty("VIPExtraVisibility", value);
		}
	}

	public bool ExtraEveryDayVisibility
	{
		get
		{
			return this._ExtraEveryDayVisibility;
		}
		set
		{
			this._ExtraEveryDayVisibility = value;
			base.NotifyProperty("ExtraEveryDayVisibility", value);
		}
	}

	public bool MCDayVisibility
	{
		get
		{
			return this._MCDayVisibility;
		}
		set
		{
			this._MCDayVisibility = value;
			base.NotifyProperty("MCDayVisibility", value);
		}
	}

	public string MCDayNum
	{
		get
		{
			return this._MCDayNum;
		}
		set
		{
			this._MCDayNum = value;
			base.NotifyProperty("MCDayNum", value);
		}
	}

	public string DaysNum
	{
		get
		{
			return this._DaysNum;
		}
		set
		{
			this._DaysNum = value;
			base.NotifyProperty("DaysNum", value);
		}
	}

	public bool PriceVisibility
	{
		get
		{
			return this._PriceVisibility;
		}
		set
		{
			this._PriceVisibility = value;
			base.NotifyProperty("PriceVisibility", value);
		}
	}

	public bool RefreshFXOfMonthCard
	{
		get
		{
			return this._RefreshFXOfMonthCard;
		}
		set
		{
			this._RefreshFXOfMonthCard = value;
			base.NotifyProperty("RefreshFXOfMonthCard", value);
		}
	}

	public bool DailyLimitIconVisibility
	{
		get
		{
			return this._DailyLimitIconVisibility;
		}
		set
		{
			this._DailyLimitIconVisibility = value;
			base.NotifyProperty("DailyLimitIconVisibility", value);
		}
	}

	public bool TodayBoughtVisibility
	{
		get
		{
			return this._TodayBoughtVisibility;
		}
		set
		{
			this._TodayBoughtVisibility = value;
			base.NotifyProperty("TodayBoughtVisibility", value);
		}
	}

	public SpriteRenderer TreasureIcon
	{
		get
		{
			return this._TreasureIcon;
		}
		set
		{
			this._TreasureIcon = value;
			base.NotifyProperty("TreasureIcon", value);
		}
	}

	public string TreasureName
	{
		get
		{
			return this._TreasureName;
		}
		set
		{
			this._TreasureName = value;
			base.NotifyProperty("TreasureName", value);
		}
	}

	public bool TreasureVisibility
	{
		get
		{
			return this._TreasureVisibility;
		}
		set
		{
			this._TreasureVisibility = value;
			base.NotifyProperty("TreasureVisibility", value);
		}
	}

	public bool DiamondVisibility
	{
		get
		{
			return this._DiamondVisibility;
		}
		set
		{
			this._DiamondVisibility = value;
			base.NotifyProperty("DiamondVisibility", value);
		}
	}

	public void OnBtnUp()
	{
		if (this.RType == RechargeManager.RechargeType.Diamond)
		{
			this.Diamond();
		}
		else if (this.RType == RechargeManager.RechargeType.MonthCard)
		{
			this.MonthCard();
		}
		else if (this.RType == RechargeManager.RechargeType.Box)
		{
			this.BuyBox();
		}
	}

	private void Diamond()
	{
		RechargeGoodsInfo rechargeGoodsInfo = RechargeManager.Instance.GetRechargeGoodsInfo(this.ID);
		if (rechargeGoodsInfo != null)
		{
			UIManagerControl.Instance.OpenUI("DiamondBuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			DiamondBuyUIViewModel.Instance.Icon = GameDataUtils.GetIcon(rechargeGoodsInfo.diamondsIcon);
			DiamondBuyUIViewModel.Instance.Info1 = string.Format(GameDataUtils.GetChineseContent(508008, false), rechargeGoodsInfo.diamonds);
			DiamondBuyUIViewModel.Instance.Info2_1 = string.Format(GameDataUtils.GetChineseContent(508028, false), rechargeGoodsInfo.rmb);
			DiamondBuyUIViewModel.Instance.CallBack = delegate
			{
				RechargeManager.Instance.ExecutionToRechargeDiamond(this.ID);
			};
		}
	}

	private void MonthCard()
	{
		MonthCardInfo monthCardInfo = RechargeManager.Instance.GetMonthCardInfo(this.ID);
		if (monthCardInfo != null)
		{
			if (!monthCardInfo.hadBuyFlag)
			{
				YueQia yueQia = DataReader<YueQia>.Get(this.ID);
				if (yueQia != null)
				{
					UIManagerControl.Instance.OpenUI("DiamondBuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
					DiamondBuyUIViewModel.Instance.Icon = ResourceManager.GetIconSprite("icon_yueka");
					DiamondBuyUIViewModel.Instance.Info1 = string.Format(GameDataUtils.GetChineseContent(508035, false), GameDataUtils.GetChineseContent(yueQia.name, false));
					DiamondBuyUIViewModel.Instance.Info2_1 = string.Format(GameDataUtils.GetChineseContent(508028, false), yueQia.rmb);
					DiamondBuyUIViewModel.Instance.CallBack = delegate
					{
						RechargeManager.Instance.SendBuyMonthCard(this.ID);
					};
				}
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513006, false));
			}
		}
	}

	private void BuyBox()
	{
		RechargeGoodsInfo rechargeGoodsInfo = RechargeManager.Instance.GetRechargeGoodsInfo(this.ID);
		if (rechargeGoodsInfo != null)
		{
			int todayRechargeCount = RechargeManager.Instance.GetTodayRechargeCount(this.ID);
			if (todayRechargeCount > 0)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(517002, false));
				return;
			}
			Dict dict = rechargeGoodsInfo.dropID.get_Item(0);
			BoxBuyUI boxBuyUI = UIManagerControl.Instance.OpenUI("BoxBuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BoxBuyUI;
			boxBuyUI.SetShowItem(dict.key, delegate
			{
				RechargeManager.Instance.ExecutionToRechargeDiamond(this.ID);
			});
		}
	}
}
