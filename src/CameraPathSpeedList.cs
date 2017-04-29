using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathSpeedList : CameraPathPointList
{
	public enum Interpolation
	{
		None,
		Linear,
		SmoothStep
	}

	public CameraPathSpeedList.Interpolation interpolation = CameraPathSpeedList.Interpolation.SmoothStep;

	[SerializeField]
	private bool _enabled = true;

	public CameraPathSpeed this[int index]
	{
		get
		{
			return (CameraPathSpeed)base[index];
		}
	}

	public bool listEnabled
	{
		get
		{
			return this._enabled && base.realNumberOfPoints > 0;
		}
		set
		{
			this._enabled = value;
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
		this.pointTypeName = "Speed";
		base.Init(_cameraPath);
	}

	public void AddSpeedPoint(CameraPathControlPoint atPoint)
	{
		CameraPathSpeed cameraPathSpeed = base.get_gameObject().AddComponent<CameraPathSpeed>();
		cameraPathSpeed.set_hideFlags(2);
		base.AddPoint(cameraPathSpeed, atPoint);
		this.RecalculatePoints();
	}

	public CameraPathSpeed AddSpeedPoint(CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage)
	{
		CameraPathSpeed cameraPathSpeed = base.get_gameObject().AddComponent<CameraPathSpeed>();
		cameraPathSpeed.set_hideFlags(2);
		base.AddPoint(cameraPathSpeed, curvePointA, curvePointB, Mathf.Clamp01(curvePercetage));
		this.RecalculatePoints();
		return cameraPathSpeed;
	}

	public float GetLowesetSpeed()
	{
		float num = float.PositiveInfinity;
		int numberOfPoints = base.numberOfPoints;
		for (int i = 0; i < numberOfPoints; i++)
		{
			if (this[i].speed < num)
			{
				num = this[i].speed;
			}
		}
		return num;
	}

	public float GetSpeed(float percentage)
	{
		if (base.realNumberOfPoints < 2)
		{
			if (base.realNumberOfPoints == 1)
			{
				return this[0].speed;
			}
			Debug.Log("Not enough points to define a speed");
			return 0f;
		}
		else
		{
			if (percentage >= 1f)
			{
				return ((CameraPathSpeed)base.GetPoint(base.realNumberOfPoints - 1)).speed;
			}
			percentage = Mathf.Clamp(percentage, 0f, 0.999f);
			switch (this.interpolation)
			{
			case CameraPathSpeedList.Interpolation.None:
			{
				CameraPathSpeed cameraPathSpeed = (CameraPathSpeed)base.GetPoint(base.GetNextPointIndex(percentage));
				return cameraPathSpeed.speed;
			}
			case CameraPathSpeedList.Interpolation.Linear:
				return this.LinearInterpolation(percentage);
			case CameraPathSpeedList.Interpolation.SmoothStep:
				return this.SmoothStepInterpolation(percentage);
			default:
				return this.LinearInterpolation(percentage);
			}
		}
	}

	private float LinearInterpolation(float percentage)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathSpeed cameraPathSpeed = (CameraPathSpeed)base.GetPoint(lastPointIndex);
		CameraPathSpeed cameraPathSpeed2 = (CameraPathSpeed)base.GetPoint(lastPointIndex + 1);
		if (percentage < cameraPathSpeed.percent)
		{
			return cameraPathSpeed.speed;
		}
		if (percentage > cameraPathSpeed2.percent)
		{
			return cameraPathSpeed2.speed;
		}
		float percent = cameraPathSpeed.percent;
		float num = cameraPathSpeed2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float num4 = num3 / num2;
		return Mathf.Lerp(cameraPathSpeed.speed, cameraPathSpeed2.speed, num4);
	}

	private float SmoothStepInterpolation(float percentage)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathSpeed cameraPathSpeed = (CameraPathSpeed)base.GetPoint(lastPointIndex);
		CameraPathSpeed cameraPathSpeed2 = (CameraPathSpeed)base.GetPoint(lastPointIndex + 1);
		if (percentage < cameraPathSpeed.percent)
		{
			return cameraPathSpeed.speed;
		}
		if (percentage > cameraPathSpeed2.percent)
		{
			return cameraPathSpeed2.speed;
		}
		float percent = cameraPathSpeed.percent;
		float num = cameraPathSpeed2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float val = num3 / num2;
		return Mathf.Lerp(cameraPathSpeed.speed, cameraPathSpeed2.speed, CPMath.SmoothStep(val));
	}
}
