using System;

public abstract class BaseMarketManager : BaseSubSystemManager
{
	public static BaseMarketManager CurrentManagerInstance;

	public static MarketClass mMarketClass
	{
		get
		{
			if (BaseMarketManager.CurrentManagerInstance == null)
			{
				return MarketClass.MarketManager;
			}
			if (BaseMarketManager.CurrentManagerInstance is MarketManager)
			{
				return MarketClass.MarketManager;
			}
			if (BaseMarketManager.CurrentManagerInstance is GuildMarketManager)
			{
				return MarketClass.GuildMarketManager;
			}
			if (BaseMarketManager.CurrentManagerInstance is XMarketManager)
			{
				return MarketClass.XMarketManager;
			}
			return MarketClass.NONE;
		}
	}

	public abstract void RefreshShopOnOpen();

	public abstract void RefreshShop();

	public abstract void OnRefresh();

	public abstract int GetCommodityPrice(int iId, int group = 1);

	public abstract int GetCommodityMoneyType(int iId);

	public abstract void Buy(int commodityId, int count);
}
