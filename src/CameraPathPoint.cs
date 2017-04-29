using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathPoint : MonoBehaviour
{
	public enum PositionModes
	{
		Free,
		FixedToPoint,
		FixedToPercent
	}

	public CameraPathPoint.PositionModes positionModes;

	public string givenName = string.Empty;

	public string customName = string.Empty;

	public string fullName = string.Empty;

	[SerializeField]
	protected float _percent;

	[SerializeField]
	protected float _animationPercentage;

	public CameraPathControlPoint point;

	public int index;

	public CameraPathControlPoint cpointA;

	public CameraPathControlPoint cpointB;

	public float curvePercentage;

	public Vector3 worldPosition;

	public bool lockPoint;

	public float percent
	{
		get
		{
			switch (this.positionModes)
			{
			case CameraPathPoint.PositionModes.Free:
				return this._percent;
			case CameraPathPoint.PositionModes.FixedToPoint:
				return this.point.percentage;
			case CameraPathPoint.PositionModes.FixedToPercent:
				return this._percent;
			default:
				return this._percent;
			}
		}
		set
		{
			this._percent = value;
		}
	}

	public float rawPercent
	{
		get
		{
			return this._percent;
		}
	}

	public float animationPercentage
	{
		get
		{
			switch (this.positionModes)
			{
			case CameraPathPoint.PositionModes.Free:
				return this._animationPercentage;
			case CameraPathPoint.PositionModes.FixedToPoint:
				return this.point.normalisedPercentage;
			case CameraPathPoint.PositionModes.FixedToPercent:
				return this._animationPercentage;
			default:
				return this._percent;
			}
		}
		set
		{
			this._animationPercentage = value;
		}
	}

	public string displayName
	{
		get
		{
			if (this.customName != string.Empty)
			{
				return this.customName;
			}
			return this.givenName;
		}
	}

	private void OnEnable()
	{
		base.set_hideFlags(2);
	}
}
