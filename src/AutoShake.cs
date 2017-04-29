using System;
using UnityEngine;

[ExecuteInEditMode]
public class AutoShake : MonoBehaviour
{
	private const int DEFAULT_LOOP_TIMES = -1;

	public Vector2 range = new Vector2(-30f, 30f);

	public float angle = 5f;

	public int playTimes = -1;

	public Action callback;

	private Transform m_myTransform;

	private uint fxUid;

	private bool IsShake;

	private bool isAdd = true;

	private float totalOffset;

	private int totalTimes;

	private void OnEnable()
	{
		this.DoShake(this.IsShake);
	}

	private void OnDisable()
	{
		this.DoShake(false);
	}

	public void SetShake(bool shake, Action action = null)
	{
		if (shake)
		{
			this.callback = action;
		}
		this.IsShake = shake;
		this.DoShake(this.IsShake);
	}

	public void DoShake(bool shake)
	{
		if (shake)
		{
			this.ResetAll();
			TimerHeap.DelTimer(this.fxUid);
			this.m_myTransform = base.get_transform();
			this.fxUid = TimerHeap.AddTimer(0u, 20, delegate
			{
				this.SelfRotate();
			});
		}
		else
		{
			this.ResetAll();
			TimerHeap.DelTimer(this.fxUid);
		}
	}

	private void SelfRotate()
	{
		if (this.m_myTransform != null)
		{
			if (this.isAdd)
			{
				Transform expr_22 = this.m_myTransform;
				expr_22.set_localEulerAngles(expr_22.get_localEulerAngles() + new Vector3(0f, 0f, this.angle));
				this.totalOffset += this.angle;
				if (this.totalOffset >= this.range.y)
				{
					this.isAdd = false;
					this.SetTotalTimes(this.totalTimes + 1);
				}
			}
			else
			{
				Transform expr_90 = this.m_myTransform;
				expr_90.set_localEulerAngles(expr_90.get_localEulerAngles() - new Vector3(0f, 0f, this.angle));
				this.totalOffset -= this.angle;
				if (this.totalOffset <= this.range.x)
				{
					this.isAdd = true;
					this.SetTotalTimes(this.totalTimes + 1);
				}
			}
		}
	}

	private void ResetAll()
	{
		if (this.m_myTransform != null)
		{
			this.m_myTransform.set_localEulerAngles(new Vector3(0f, 0f, 0f));
		}
		this.isAdd = true;
		this.totalOffset = 0f;
		this.totalTimes = 0;
	}

	private bool IsLoop()
	{
		return this.playTimes == -1;
	}

	private void SetTotalTimes(int times)
	{
		this.totalTimes = times;
		if (!this.IsLoop() && this.totalTimes >= this.playTimes && this.callback != null)
		{
			this.callback.Invoke();
		}
	}
}
