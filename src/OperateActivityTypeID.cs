using System;

public class OperateActivityTypeID
{
	public const int RechargeGift = 1;

	public const int OpenServer = 4;

	public const int Activity7Day = 2;

	public const int UpdateGift = 5;

	public const int ConsumeGift = 6;

	public const int LimitTimeShopping = 10;

	public const int InvestFund = 7;

	public const int GrowUpPlan = 8;

	public static string GetUI(int id)
	{
		switch (id)
		{
		case 1:
			return "RechargeGiftUI";
		case 2:
			return "Activity7Day";
		case 4:
			return "ServeLoginUI";
		case 6:
			return "ConsumeGiftUI";
		case 7:
			return "InvestFundUI";
		case 8:
			return "GrowUpPlanUI";
		case 10:
			return "LimitTimeShoppingUI";
		}
		return string.Empty;
	}
}
