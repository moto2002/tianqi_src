using System;
using UnityEngine;

public class TimeCountDown : IDisposable
{
	private bool m_isMinus = true;

	private int m_maxOfAdd;

	private TimeFormat m_timeFormat;

	private Action m_actionSecondPast;

	private Action m_actionEnd;

	private int m_seconds;

	private bool IsDispose;

	public bool IsStop;

	private int Seconds
	{
		get
		{
			return this.m_seconds;
		}
		set
		{
			this.m_seconds = Mathf.Max(0, value);
		}
	}

	public TimeCountDown(int seconds, TimeFormat format = TimeFormat.SECOND, Action actionSecondPast = null, Action actionEnd = null, bool isMinus = true)
	{
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondPast));
		this.m_timeFormat = format;
		this.m_actionSecondPast = actionSecondPast;
		this.m_actionEnd = actionEnd;
		this.m_isMinus = isMinus;
		if (this.m_isMinus)
		{
			this.Seconds = seconds;
		}
		else
		{
			this.m_maxOfAdd = seconds;
			this.Seconds = 0;
		}
	}

	~TimeCountDown()
	{
	}

	public void Dispose()
	{
		if (!this.IsDispose)
		{
			this.IsDispose = true;
			EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondPast));
			GC.SuppressFinalize(this);
		}
	}

	private void OnSecondPast()
	{
		if (this.IsStop)
		{
			return;
		}
		if (this.m_isMinus)
		{
			this.Seconds--;
		}
		else
		{
			this.Seconds++;
		}
		this.DealSecondChange();
	}

	private void DealSecondChange()
	{
		if (this.m_isMinus)
		{
			if (this.Seconds <= 0)
			{
				this.StopTimer();
				if (this.m_actionEnd != null)
				{
					this.m_actionEnd.Invoke();
				}
			}
			else if (this.m_actionSecondPast != null)
			{
				this.m_actionSecondPast.Invoke();
			}
		}
		else if (this.Seconds >= this.m_maxOfAdd)
		{
			this.StopTimer();
			if (this.m_actionEnd != null)
			{
				this.m_actionEnd.Invoke();
			}
		}
		else if (this.m_actionSecondPast != null)
		{
			this.m_actionSecondPast.Invoke();
		}
	}

	public void StopTimer()
	{
		this.IsStop = true;
	}

	public string GetTime()
	{
		return TimeConverter.GetTime(this.Seconds, this.m_timeFormat);
	}

	public void ResetSeconds(int seconds)
	{
		this.Seconds = seconds;
		this.IsStop = false;
	}

	public int GetSeconds()
	{
		return this.Seconds;
	}
}
