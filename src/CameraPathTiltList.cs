using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathTiltList : CameraPathPointList
{
	public enum Interpolation
	{
		None,
		Linear,
		SmoothStep
	}

	public CameraPathTiltList.Interpolation interpolation = CameraPathTiltList.Interpolation.SmoothStep;

	public bool listEnabled = true;

	public float autoSensitivity = 1f;

	public CameraPathTilt this[int index]
	{
		get
		{
			return (CameraPathTilt)base[index];
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
		this.pointTypeName = "Tilt";
		base.Init(_cameraPath);
		this.cameraPath.PathPointAddedEvent += new CameraPath.PathPointAddedHandler(this.AddTilt);
		this.initialised = true;
	}

	public override void CleanUp()
	{
		base.CleanUp();
		this.cameraPath.PathPointAddedEvent -= new CameraPath.PathPointAddedHandler(this.AddTilt);
		this.initialised = false;
	}

	public void AddTilt(CameraPathControlPoint atPoint)
	{
		CameraPathTilt cameraPathTilt = base.get_gameObject().AddComponent<CameraPathTilt>();
		cameraPathTilt.tilt = 0f;
		cameraPathTilt.set_hideFlags(2);
		base.AddPoint(cameraPathTilt, atPoint);
		this.RecalculatePoints();
	}

	public CameraPathTilt AddTilt(CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage, float tilt)
	{
		CameraPathTilt cameraPathTilt = base.get_gameObject().AddComponent<CameraPathTilt>();
		cameraPathTilt.tilt = tilt;
		cameraPathTilt.set_hideFlags(2);
		base.AddPoint(cameraPathTilt, curvePointA, curvePointB, curvePercetage);
		this.RecalculatePoints();
		return cameraPathTilt;
	}

	public float GetTilt(float percentage)
	{
		if (base.realNumberOfPoints < 2)
		{
			if (base.realNumberOfPoints == 1)
			{
				return this[0].tilt;
			}
			return 0f;
		}
		else
		{
			percentage = Mathf.Clamp(percentage, 0f, 1f);
			switch (this.interpolation)
			{
			case CameraPathTiltList.Interpolation.None:
			{
				CameraPathTilt cameraPathTilt = (CameraPathTilt)base.GetPoint(base.GetNextPointIndex(percentage));
				return cameraPathTilt.tilt;
			}
			case CameraPathTiltList.Interpolation.Linear:
				return this.LinearInterpolation(percentage);
			case CameraPathTiltList.Interpolation.SmoothStep:
				return this.SmoothStepInterpolation(percentage);
			default:
				return this.LinearInterpolation(percentage);
			}
		}
	}

	public void AutoSetTilts()
	{
		for (int i = 0; i < base.realNumberOfPoints; i++)
		{
			this.AutoSetTilt(this[i]);
		}
	}

	public void AutoSetTilt(CameraPathTilt point)
	{
		float percent = point.percent;
		Vector3 pathPosition = this.cameraPath.GetPathPosition(percent - 0.1f);
		Vector3 pathPosition2 = this.cameraPath.GetPathPosition(percent);
		Vector3 pathPosition3 = this.cameraPath.GetPathPosition(percent + 0.1f);
		Vector3 vector = pathPosition2 - pathPosition;
		Vector3 vector2 = pathPosition3 - pathPosition2;
		Quaternion quaternion = Quaternion.LookRotation(-this.cameraPath.GetPathDirection(point.percent));
		Vector3 vector3 = quaternion * (vector2 - vector).get_normalized();
		float num = Vector2.Angle(Vector2.get_up(), new Vector2(vector3.x, vector3.y));
		float num2 = Mathf.Min(Mathf.Abs(vector3.x) + Mathf.Abs(vector3.y) / Mathf.Abs(vector3.z), 1f);
		point.tilt = -num * this.autoSensitivity * num2;
	}

	private float LinearInterpolation(float percentage)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathTilt cameraPathTilt = (CameraPathTilt)base.GetPoint(lastPointIndex);
		CameraPathTilt cameraPathTilt2 = (CameraPathTilt)base.GetPoint(lastPointIndex + 1);
		float percent = cameraPathTilt.percent;
		float num = cameraPathTilt2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float num4 = num3 / num2;
		return Mathf.LerpAngle(cameraPathTilt.tilt, cameraPathTilt2.tilt, num4);
	}

	private float SmoothStepInterpolation(float percentage)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathTilt cameraPathTilt = (CameraPathTilt)base.GetPoint(lastPointIndex);
		CameraPathTilt cameraPathTilt2 = (CameraPathTilt)base.GetPoint(lastPointIndex + 1);
		float percent = cameraPathTilt.percent;
		float num = cameraPathTilt2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float val = num3 / num2;
		return Mathf.LerpAngle(cameraPathTilt.tilt, cameraPathTilt2.tilt, CPMath.SmoothStep(val));
	}
}
