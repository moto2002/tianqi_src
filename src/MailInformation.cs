using Package;
using System;

public class MailInformation
{
	public MailInfo mailInfo;

	private TimeCountDown timeCountDown;

	public void ResetTimeCountDown(int seconds)
	{
		if (this.timeCountDown == null)
		{
			if (seconds > 0)
			{
				this.timeCountDown = new TimeCountDown(seconds, TimeFormat.SECOND, delegate
				{
					this.mailInfo.timeoutSec = this.timeCountDown.GetSeconds();
				}, delegate
				{
					this.mailInfo.timeoutSec = this.timeCountDown.GetSeconds();
					MailManager.Instance.SendDelMail(this.mailInfo.id);
				}, true);
			}
		}
		else if (seconds > 0)
		{
			this.timeCountDown.ResetSeconds(seconds);
		}
		else
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
	}
}
