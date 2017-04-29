using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathDelayList : CameraPathPointList
{
	public delegate void CameraPathDelayEventHandler(float time);

	public float MINIMUM_EASE_VALUE = 0.01f;

	private float _lastPercentage;

	[SerializeField]
	private CameraPathDelay _introPoint;

	[SerializeField]
	private CameraPathDelay _outroPoint;

	[SerializeField]
	private bool delayInitialised;

	public event CameraPathDelayList.CameraPathDelayEventHandler CameraPathDelayEvent
	{
		[MethodImpl(32)]
		add
		{
			this.CameraPathDelayEvent = (CameraPathDelayList.CameraPathDelayEventHandler)Delegate.Combine(this.CameraPathDelayEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.CameraPathDelayEvent = (CameraPathDelayList.CameraPathDelayEventHandler)Delegate.Remove(this.CameraPathDelayEvent, value);
		}
	}

	public CameraPathDelay this[int index]
	{
		get
		{
			return (CameraPathDelay)base[index];
		}
	}

	public CameraPathDelay introPoint
	{
		get
		{
			return this._introPoint;
		}
	}

	public CameraPathDelay outroPoint
	{
		get
		{
			return this._outroPoint;
		}
	}

	private void OnEnable()
	{
		base.set_hideFlags(2);
	}

	public override void Init(CameraPath _cameraPath)
	{
		if (this.initialised)
		{
			return;
		}
		this.pointTypeName = "Delay";
		base.Init(_cameraPath);
		if (!this.delayInitialised)
		{
			this._introPoint = base.get_gameObject().AddComponent<CameraPathDelay>();
			this._introPoint.customName = "Start Point";
			this._introPoint.set_hideFlags(2);
			base.AddPoint(this.introPoint, 0f);
			this._outroPoint = base.get_gameObject().AddComponent<CameraPathDelay>();
			this._outroPoint.customName = "End Point";
			this._outroPoint.set_hideFlags(2);
			base.AddPoint(this.outroPoint, 1f);
			this.RecalculatePoints();
			this.delayInitialised = true;
		}
	}

	public void AddDelayPoint(CameraPathControlPoint atPoint)
	{
		CameraPathDelay cameraPathDelay = base.get_gameObject().AddComponent<CameraPathDelay>();
		cameraPathDelay.set_hideFlags(2);
		base.AddPoint(cameraPathDelay, atPoint);
		this.RecalculatePoints();
	}

	public CameraPathDelay AddDelayPoint(CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage)
	{
		CameraPathDelay cameraPathDelay = base.get_gameObject().AddComponent<CameraPathDelay>();
		cameraPathDelay.set_hideFlags(2);
		base.AddPoint(cameraPathDelay, curvePointA, curvePointB, curvePercetage);
		this.RecalculatePoints();
		return cameraPathDelay;
	}

	public void OnAnimationStart(float startPercentage)
	{
		this._lastPercentage = startPercentage;
	}

	public void CheckEvents(float percentage)
	{
		if (Mathf.Abs(percentage - this._lastPercentage) > 0.1f)
		{
			this._lastPercentage = percentage;
			return;
		}
		if (this._lastPercentage == percentage)
		{
			return;
		}
		for (int i = 0; i < base.realNumberOfPoints; i++)
		{
			CameraPathDelay cameraPathDelay = this[i];
			if (!(cameraPathDelay == this.outroPoint))
			{
				if (cameraPathDelay.percent >= this._lastPercentage && cameraPathDelay.percent <= percentage)
				{
					if (cameraPathDelay != this.introPoint)
					{
						this.FireDelay(cameraPathDelay);
					}
					else if (cameraPathDelay.time > 0f)
					{
						this.FireDelay(cameraPathDelay);
					}
				}
				else if (cameraPathDelay.percent >= percentage && cameraPathDelay.percent <= this._lastPercentage)
				{
					if (cameraPathDelay != this.introPoint)
					{
						this.FireDelay(cameraPathDelay);
					}
					else if (cameraPathDelay.time > 0f)
					{
						this.FireDelay(cameraPathDelay);
					}
				}
			}
		}
		this._lastPercentage = percentage;
	}

	public float CheckEase(float percent)
	{
		float num = 1f;
		for (int i = 0; i < base.realNumberOfPoints; i++)
		{
			CameraPathDelay cameraPathDelay = this[i];
			if (cameraPathDelay != this.introPoint)
			{
				CameraPathDelay cameraPathDelay2 = (CameraPathDelay)base.GetPoint(i - 1);
				float pathPercentage = this.cameraPath.GetPathPercentage(cameraPathDelay2.percent, cameraPathDelay.percent, 1f - cameraPathDelay.introStartEasePercentage);
				if (pathPercentage < percent && cameraPathDelay.percent > percent)
				{
					float num2 = (percent - pathPercentage) / (cameraPathDelay.percent - pathPercentage);
					num = cameraPathDelay.introCurve.Evaluate(num2);
				}
			}
			if (cameraPathDelay != this.outroPoint)
			{
				CameraPathDelay cameraPathDelay3 = (CameraPathDelay)base.GetPoint(i + 1);
				float pathPercentage2 = this.cameraPath.GetPathPercentage(cameraPathDelay.percent, cameraPathDelay3.percent, cameraPathDelay.outroEndEasePercentage);
				if (cameraPathDelay.percent < percent && pathPercentage2 > percent)
				{
					float num3 = (percent - cameraPathDelay.percent) / (pathPercentage2 - cameraPathDelay.percent);
					num = cameraPathDelay.outroCurve.Evaluate(num3);
				}
			}
		}
		return Math.Max(num, this.MINIMUM_EASE_VALUE);
	}

	public void FireDelay(CameraPathDelay eventPoint)
	{
		if (this.CameraPathDelayEvent != null)
		{
			this.CameraPathDelayEvent(eventPoint.time);
		}
	}
}
