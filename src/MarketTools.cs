using GameData;
using Package;
using System;

public class MarketTools
{
	public class EventNames
	{
		public const string FleaShopBadgeTip = "MarketManager.FleaShopBadgeTip";

		public const string FleaShopOpen = "MarketManager.FleaShopOpen";

		public const string XMarketBadgeTip = "MarketManager.XMarketBadgeTip";
	}

	public static bool IsEnoughMoney(int price, int money_type)
	{
		return (long)price <= MoneyType.GetNum(money_type);
	}

	public static bool IsEnoughIntergral(int commodityId)
	{
		CommodityInfo commodityInfo = MarketManager.Instance.GetCommodityInfo(commodityId);
		if (commodityInfo != null)
		{
			ShangPinBiao shangPinBiao = DataReader<ShangPinBiao>.Get(commodityInfo.commodityId);
			if (shangPinBiao != null && shangPinBiao.pvpLevel > 0)
			{
				return PVPManager.Instance.GetIntegralLevel().id >= shangPinBiao.pvpLevel;
			}
		}
		return true;
	}
}
