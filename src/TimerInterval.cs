using System;
using UnityEngine;

public class TimerInterval : MonoBehaviour
{
	private float m_fCircle1 = 5f;

	private float m_fCurrentTime1;

	private float m_fCircle2 = 5f;

	private float m_fCurrentTime2;

	public float Circle1
	{
		get
		{
			return this.m_fCircle1;
		}
		set
		{
			this.m_fCircle1 = value;
		}
	}

	public float Circle2
	{
		get
		{
			return this.m_fCircle2;
		}
		set
		{
			this.m_fCircle2 = value;
		}
	}

	protected bool IsTime1Over()
	{
		this.m_fCurrentTime1 += Time.get_deltaTime();
		if (this.m_fCurrentTime1 >= this.Circle1)
		{
			this.m_fCurrentTime1 = 0f;
			return true;
		}
		return false;
	}

	protected void ResetTime1()
	{
		this.m_fCircle1 = 0f;
		this.m_fCurrentTime1 = 0f;
	}

	protected bool IsTime2Over()
	{
		this.m_fCurrentTime2 += Time.get_deltaTime();
		if (this.m_fCurrentTime2 >= this.Circle2)
		{
			this.m_fCurrentTime2 = 0f;
			return true;
		}
		return false;
	}

	protected void ResetTime2()
	{
		this.m_fCircle2 = 0f;
		this.m_fCurrentTime2 = 0f;
	}
}
