using System;
using System.Net;

public class WebClientWithTimeout : WebClient
{
	private TimeoutTimer _timer;

	private int _timeOut = 10;

	public int Timeout
	{
		get
		{
			return this._timeOut;
		}
		set
		{
			if (value <= 0)
			{
				this._timeOut = 10;
			}
			this._timeOut = value;
		}
	}

	protected override WebRequest GetWebRequest(Uri address)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)base.GetWebRequest(address);
		httpWebRequest.set_Proxy(null);
		httpWebRequest.set_Timeout(1000 * this.Timeout);
		httpWebRequest.set_ReadWriteTimeout(1000 * this.Timeout);
		return httpWebRequest;
	}

	public void DownloadFileAsyncWithTimeout(Uri address, string fileName, object userToken)
	{
		if (this._timer == null)
		{
			this._timer = new TimeoutTimer(this);
			this._timer.Timeout = this.Timeout;
			this._timer.TimeOver += new TimeoutCaller(this._timer_TimeOver);
			base.add_DownloadProgressChanged(new DownloadProgressChangedEventHandler(this.CNNWebClient_DownloadProgressChanged));
		}
		base.DownloadFileAsync(address, fileName, userToken);
		this._timer.Start();
	}

	private void CNNWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
		this._timer.Reset();
	}

	private void _timer_TimeOver(object userdata)
	{
		base.CancelAsync();
	}
}
