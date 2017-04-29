using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathFOVList : CameraPathPointList
{
	public enum ProjectionType
	{
		FOV,
		Orthographic
	}

	public enum Interpolation
	{
		None,
		Linear,
		SmoothStep
	}

	private const float DEFAULT_FOV = 60f;

	private const float DEFAULT_SIZE = 5f;

	public CameraPathFOVList.Interpolation interpolation = CameraPathFOVList.Interpolation.SmoothStep;

	public bool listEnabled;

	public CameraPathFOV this[int index]
	{
		get
		{
			return (CameraPathFOV)base[index];
		}
	}

	private float defaultFOV
	{
		get
		{
			if (Camera.get_current())
			{
				return Camera.get_current().get_fieldOfView();
			}
			Camera[] allCameras = Camera.get_allCameras();
			bool flag = allCameras.Length > 0;
			if (flag)
			{
				return allCameras[0].get_fieldOfView();
			}
			return 60f;
		}
	}

	private float defaultSize
	{
		get
		{
			if (Camera.get_current())
			{
				return Camera.get_current().get_orthographicSize();
			}
			Camera[] allCameras = Camera.get_allCameras();
			bool flag = allCameras.Length > 0;
			if (flag)
			{
				return allCameras[0].get_orthographicSize();
			}
			return 5f;
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
		this.pointTypeName = "FOV";
		base.Init(_cameraPath);
		this.cameraPath.PathPointAddedEvent += new CameraPath.PathPointAddedHandler(this.AddFOV);
		this.initialised = true;
	}

	public override void CleanUp()
	{
		base.CleanUp();
		this.cameraPath.PathPointAddedEvent -= new CameraPath.PathPointAddedHandler(this.AddFOV);
		this.initialised = false;
	}

	public void AddFOV(CameraPathControlPoint atPoint)
	{
		CameraPathFOV cameraPathFOV = base.get_gameObject().AddComponent<CameraPathFOV>();
		cameraPathFOV.FOV = this.defaultFOV;
		cameraPathFOV.Size = this.defaultSize;
		cameraPathFOV.set_hideFlags(2);
		base.AddPoint(cameraPathFOV, atPoint);
		this.RecalculatePoints();
	}

	public CameraPathFOV AddFOV(CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage, float fov, float size)
	{
		CameraPathFOV cameraPathFOV = base.get_gameObject().AddComponent<CameraPathFOV>();
		cameraPathFOV.set_hideFlags(2);
		cameraPathFOV.FOV = fov;
		cameraPathFOV.Size = size;
		cameraPathFOV.Size = this.defaultSize;
		base.AddPoint(cameraPathFOV, curvePointA, curvePointB, curvePercetage);
		this.RecalculatePoints();
		return cameraPathFOV;
	}

	public float GetValue(float percentage, CameraPathFOVList.ProjectionType type)
	{
		if (base.realNumberOfPoints < 2)
		{
			if (type == CameraPathFOVList.ProjectionType.FOV)
			{
				if (base.realNumberOfPoints == 1)
				{
					return this[0].FOV;
				}
				return this.defaultFOV;
			}
			else
			{
				if (base.realNumberOfPoints == 1)
				{
					return this[0].Size;
				}
				return this.defaultSize;
			}
		}
		else
		{
			percentage = Mathf.Clamp(percentage, 0f, 1f);
			CameraPathFOVList.Interpolation interpolation = this.interpolation;
			if (interpolation == CameraPathFOVList.Interpolation.Linear)
			{
				return this.LinearInterpolation(percentage, type);
			}
			if (interpolation != CameraPathFOVList.Interpolation.SmoothStep)
			{
				return this.LinearInterpolation(percentage, type);
			}
			return this.SmoothStepInterpolation(percentage, type);
		}
	}

	private float LinearInterpolation(float percentage, CameraPathFOVList.ProjectionType projectionType)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathFOV cameraPathFOV = (CameraPathFOV)base.GetPoint(lastPointIndex);
		CameraPathFOV cameraPathFOV2 = (CameraPathFOV)base.GetPoint(lastPointIndex + 1);
		float percent = cameraPathFOV.percent;
		float num = cameraPathFOV2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float num4 = num3 / num2;
		float num5 = (projectionType != CameraPathFOVList.ProjectionType.FOV) ? cameraPathFOV.Size : cameraPathFOV.FOV;
		float num6 = (projectionType != CameraPathFOVList.ProjectionType.FOV) ? cameraPathFOV2.Size : cameraPathFOV2.FOV;
		return Mathf.Lerp(num5, num6, num4);
	}

	private float SmoothStepInterpolation(float percentage, CameraPathFOVList.ProjectionType projectionType)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathFOV cameraPathFOV = (CameraPathFOV)base.GetPoint(lastPointIndex);
		CameraPathFOV cameraPathFOV2 = (CameraPathFOV)base.GetPoint(lastPointIndex + 1);
		float percent = cameraPathFOV.percent;
		float num = cameraPathFOV2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float val = num3 / num2;
		float num4 = (projectionType != CameraPathFOVList.ProjectionType.FOV) ? cameraPathFOV.Size : cameraPathFOV.FOV;
		float num5 = (projectionType != CameraPathFOVList.ProjectionType.FOV) ? cameraPathFOV2.Size : cameraPathFOV2.FOV;
		return Mathf.Lerp(num4, num5, CPMath.SmoothStep(val));
	}
}
