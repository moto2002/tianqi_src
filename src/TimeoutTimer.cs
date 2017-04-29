using System;
using System.Runtime.CompilerServices;
using System.Threading;

public class TimeoutTimer
{
	private DateTime _startTime;

	private TimeSpan _timeout = new TimeSpan(0, 0, 10);

	private bool _hasStarted;

	private object _userdata;

	public event TimeoutCaller TimeOver
	{
		[MethodImpl(32)]
		add
		{
			this.TimeOver = (TimeoutCaller)Delegate.Combine(this.TimeOver, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.TimeOver = (TimeoutCaller)Delegate.Remove(this.TimeOver, value);
		}
	}

	public int Timeout
	{
		get
		{
			return this._timeout.get_Seconds();
		}
		set
		{
			if (value <= 0)
			{
				return;
			}
			this._timeout = new TimeSpan(0, 0, value);
		}
	}

	public bool HasStarted
	{
		get
		{
			return this._hasStarted;
		}
	}

	public TimeoutTimer(object userdata)
	{
		this.TimeOver = (TimeoutCaller)Delegate.Combine(this.TimeOver, new TimeoutCaller(this.OnTimeOver));
		this._userdata = userdata;
	}

	public virtual void OnTimeOver(object userdata)
	{
		this.Stop();
	}

	public void Start()
	{
		this.Reset();
		this._hasStarted = true;
		Thread thread = new Thread(new ThreadStart(this.WaitCall));
		thread.set_IsBackground(true);
		thread.Start();
	}

	private void WaitCall()
	{
		try
		{
			while (this._hasStarted && !this.checkTimeout())
			{
				Thread.Sleep(1000);
			}
			if (this.TimeOver != null)
			{
				this.TimeOver(this._userdata);
			}
		}
		catch (Exception)
		{
			this.Stop();
		}
	}

	public void Reset()
	{
		this._startTime = DateTime.get_Now();
	}

	public void Stop()
	{
		this._hasStarted = false;
	}

	private bool checkTimeout()
	{
		return (DateTime.get_Now() - this._startTime).get_Seconds() >= this.Timeout;
	}
}
