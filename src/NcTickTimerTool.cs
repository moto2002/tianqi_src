using System;
using UnityEngine;

public class NcTickTimerTool
{
	protected int m_nStartTickCount;

	protected int m_nCheckTickCount;

	public NcTickTimerTool()
	{
		this.StartTickCount();
	}

	public static NcTickTimerTool GetTickTimer()
	{
		return new NcTickTimerTool();
	}

	public void StartTickCount()
	{
		this.m_nStartTickCount = Environment.get_TickCount();
		this.m_nCheckTickCount = this.m_nStartTickCount;
	}

	public int GetStartedTickCount()
	{
		return Environment.get_TickCount() - this.m_nStartTickCount;
	}

	public int GetElipsedTickCount()
	{
		int result = Environment.get_TickCount() - this.m_nCheckTickCount;
		this.m_nCheckTickCount = Environment.get_TickCount();
		return result;
	}

	public void LogElipsedTickCount()
	{
		Debug.Log(this.GetElipsedTickCount());
	}
}
