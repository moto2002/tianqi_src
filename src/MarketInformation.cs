using Package;
using System;

public class MarketInformation
{
	public GetShopInfos shopInfo;

	private TimeCountDown timeCountDown;

	public void ResetTimeCountDown(int seconds)
	{
		if (this.timeCountDown == null)
		{
			this.timeCountDown = new TimeCountDown(seconds, TimeFormat.SECOND, delegate
			{
				this.shopInfo.remainingRefreshTime = this.timeCountDown.GetSeconds();
			}, delegate
			{
				this.shopInfo.remainingRefreshTime = this.timeCountDown.GetSeconds();
			}, true);
		}
		else
		{
			this.timeCountDown.ResetSeconds(seconds);
		}
	}
}
