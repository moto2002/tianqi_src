using System;
using UnityEngine;

public class NcTimerTool
{
	protected bool m_bEnable;

	private float m_fStartTime;

	private float m_fPauseTime;

	public static float GetEngineTime()
	{
		if (Time.get_time() == 0f)
		{
			return 1E-06f;
		}
		return Time.get_time();
	}

	public float GetTime()
	{
		float engineTime = NcTimerTool.GetEngineTime();
		if (!this.m_bEnable && this.m_fPauseTime != engineTime)
		{
			this.m_fStartTime += NcTimerTool.GetEngineTime() - this.m_fPauseTime;
			this.m_fPauseTime = engineTime;
		}
		return NcTimerTool.GetEngineTime() - this.m_fStartTime;
	}

	public float GetDeltaTime()
	{
		if (this.m_bEnable)
		{
			return Time.get_deltaTime();
		}
		return 0f;
	}

	public bool IsEnable()
	{
		return this.m_bEnable;
	}

	public void Start()
	{
		this.m_bEnable = true;
		this.m_fStartTime = NcTimerTool.GetEngineTime() - 1E-06f;
	}

	public void Reset(float fAdjustTime)
	{
		this.m_fStartTime = NcTimerTool.GetEngineTime() - fAdjustTime;
	}

	public void Pause()
	{
		this.m_bEnable = false;
		this.m_fPauseTime = NcTimerTool.GetEngineTime();
	}

	public void Resume()
	{
		this.GetTime();
		this.m_bEnable = true;
	}
}
