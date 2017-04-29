using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathOrientationList : CameraPathPointList
{
	public enum Interpolation
	{
		None,
		Linear,
		SmoothStep,
		Hermite,
		Cubic
	}

	public CameraPathOrientationList.Interpolation interpolation = CameraPathOrientationList.Interpolation.Cubic;

	public CameraPathOrientation this[int index]
	{
		get
		{
			return (CameraPathOrientation)base[index];
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
		this.pointTypeName = "Orientation";
		base.Init(_cameraPath);
		this.cameraPath.PathPointAddedEvent += new CameraPath.PathPointAddedHandler(this.AddOrientation);
		this.initialised = true;
	}

	public override void CleanUp()
	{
		base.CleanUp();
		this.cameraPath.PathPointAddedEvent -= new CameraPath.PathPointAddedHandler(this.AddOrientation);
		this.initialised = false;
	}

	public void AddOrientation(CameraPathControlPoint atPoint)
	{
		CameraPathOrientation cameraPathOrientation = base.get_gameObject().AddComponent<CameraPathOrientation>();
		if (atPoint.forwardControlPoint != Vector3.get_zero())
		{
			cameraPathOrientation.rotation = Quaternion.LookRotation(atPoint.forwardControlPoint);
		}
		else
		{
			cameraPathOrientation.rotation = Quaternion.LookRotation(this.cameraPath.GetPathDirection(atPoint.percentage));
		}
		cameraPathOrientation.set_hideFlags(2);
		base.AddPoint(cameraPathOrientation, atPoint);
		this.RecalculatePoints();
	}

	public CameraPathOrientation AddOrientation(CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage, Quaternion rotation)
	{
		CameraPathOrientation cameraPathOrientation = base.get_gameObject().AddComponent<CameraPathOrientation>();
		cameraPathOrientation.rotation = rotation;
		cameraPathOrientation.set_hideFlags(2);
		base.AddPoint(cameraPathOrientation, curvePointA, curvePointB, curvePercetage);
		this.RecalculatePoints();
		return cameraPathOrientation;
	}

	public void RemovePoint(CameraPathOrientation orientation)
	{
		base.RemovePoint(orientation);
		this.RecalculatePoints();
	}

	public Quaternion GetOrientation(float percentage)
	{
		if (base.realNumberOfPoints < 2)
		{
			if (base.realNumberOfPoints == 1)
			{
				return this[0].rotation;
			}
			return Quaternion.get_identity();
		}
		else
		{
			if (float.IsNaN(percentage))
			{
				percentage = 0f;
			}
			percentage = Mathf.Clamp(percentage, 0f, 1f);
			Quaternion result = Quaternion.get_identity();
			switch (this.interpolation)
			{
			case CameraPathOrientationList.Interpolation.None:
			{
				CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)base.GetPoint(base.GetNextPointIndex(percentage));
				result = cameraPathOrientation.rotation;
				break;
			}
			case CameraPathOrientationList.Interpolation.Linear:
				result = this.LinearInterpolation(percentage);
				break;
			case CameraPathOrientationList.Interpolation.SmoothStep:
				result = this.SmootStepInterpolation(percentage);
				break;
			case CameraPathOrientationList.Interpolation.Hermite:
				result = this.CubicInterpolation(percentage);
				break;
			case CameraPathOrientationList.Interpolation.Cubic:
				result = this.CubicInterpolation(percentage);
				break;
			default:
				result = Quaternion.LookRotation(Vector3.get_forward());
				break;
			}
			if (float.IsNaN(result.x))
			{
				return Quaternion.get_identity();
			}
			return result;
		}
	}

	private Quaternion LinearInterpolation(float percentage)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)base.GetPoint(lastPointIndex);
		CameraPathOrientation cameraPathOrientation2 = (CameraPathOrientation)base.GetPoint(lastPointIndex + 1);
		float percent = cameraPathOrientation.percent;
		float num = cameraPathOrientation2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float num4 = num3 / num2;
		return Quaternion.Lerp(cameraPathOrientation.rotation, cameraPathOrientation2.rotation, num4);
	}

	private Quaternion SmootStepInterpolation(float percentage)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)base.GetPoint(lastPointIndex);
		CameraPathOrientation cameraPathOrientation2 = (CameraPathOrientation)base.GetPoint(lastPointIndex + 1);
		float percent = cameraPathOrientation.percent;
		float num = cameraPathOrientation2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float val = num3 / num2;
		return Quaternion.Lerp(cameraPathOrientation.rotation, cameraPathOrientation2.rotation, CPMath.SmoothStep(val));
	}

	private Quaternion CubicInterpolation(float percentage)
	{
		int lastPointIndex = base.GetLastPointIndex(percentage);
		CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)base.GetPoint(lastPointIndex);
		CameraPathOrientation cameraPathOrientation2 = (CameraPathOrientation)base.GetPoint(lastPointIndex + 1);
		CameraPathOrientation cameraPathOrientation3 = (CameraPathOrientation)base.GetPoint(lastPointIndex - 1);
		CameraPathOrientation cameraPathOrientation4 = (CameraPathOrientation)base.GetPoint(lastPointIndex + 2);
		float percent = cameraPathOrientation.percent;
		float num = cameraPathOrientation2.percent;
		if (percent > num)
		{
			num += 1f;
		}
		float num2 = num - percent;
		float num3 = percentage - percent;
		float t = num3 / num2;
		Quaternion result = CPMath.CalculateCubic(cameraPathOrientation.rotation, cameraPathOrientation3.rotation, cameraPathOrientation4.rotation, cameraPathOrientation2.rotation, t);
		if (float.IsNaN(result.x))
		{
			Debug.Log(string.Concat(new object[]
			{
				percentage,
				" ",
				cameraPathOrientation.fullName,
				" ",
				cameraPathOrientation2.fullName,
				" ",
				cameraPathOrientation3.fullName,
				" ",
				cameraPathOrientation4.fullName
			}));
			result = cameraPathOrientation.rotation;
		}
		return result;
	}

	protected override void RecalculatePoints()
	{
		base.RecalculatePoints();
		for (int i = 0; i < base.realNumberOfPoints; i++)
		{
			CameraPathOrientation cameraPathOrientation = this[i];
			if (cameraPathOrientation.lookAt != null)
			{
				cameraPathOrientation.rotation = Quaternion.LookRotation(cameraPathOrientation.lookAt.get_transform().get_position() - cameraPathOrientation.worldPosition);
			}
		}
	}
}
