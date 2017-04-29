using System;

public abstract class AbsTimerData
{
	private uint m_nTimerId;

	private int m_nInterval;

	private ulong m_unNextTick;

	public uint NTimerId
	{
		get
		{
			return this.m_nTimerId;
		}
		set
		{
			this.m_nTimerId = value;
		}
	}

	public int NInterval
	{
		get
		{
			return this.m_nInterval;
		}
		set
		{
			this.m_nInterval = value;
		}
	}

	public ulong UnNextTick
	{
		get
		{
			return this.m_unNextTick;
		}
		set
		{
			this.m_unNextTick = value;
		}
	}

	public string StackTrack
	{
		get;
		set;
	}

	public abstract Delegate Action
	{
		get;
		set;
	}

	public abstract void DoAction();
}
