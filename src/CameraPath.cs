using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPath : MonoBehaviour
{
	public enum PointModes
	{
		Transform,
		ControlPoints,
		FOV,
		Events,
		Speed,
		Delay,
		Ease,
		Orientations,
		Tilt,
		AddPathPoints,
		RemovePathPoints,
		AddOrientations,
		RemoveOrientations,
		TargetOrientation,
		AddFovs,
		RemoveFovs,
		AddTilts,
		RemoveTilts,
		AddEvents,
		RemoveEvents,
		AddSpeeds,
		RemoveSpeeds,
		AddDelays,
		RemoveDelays,
		Options
	}

	public enum Interpolation
	{
		Linear,
		SmoothStep,
		CatmullRom,
		Hermite,
		Bezier
	}

	public delegate void RecalculateCurvesHandler();

	public delegate void PathPointAddedHandler(CameraPathControlPoint point);

	public delegate void PathPointRemovedHandler(CameraPathControlPoint point);

	public delegate void CheckStartPointCullHandler(float percentage);

	public delegate void CheckEndPointCullHandler(float percentage);

	public delegate void CleanUpListsHandler();

	public static float CURRENT_VERSION_NUMBER = 3.5f;

	public float version = CameraPath.CURRENT_VERSION_NUMBER;

	[SerializeField]
	private List<CameraPathControlPoint> _points = new List<CameraPathControlPoint>();

	[SerializeField]
	private CameraPath.Interpolation _interpolation = CameraPath.Interpolation.Bezier;

	[SerializeField]
	private bool initialised;

	[SerializeField]
	private float _storedTotalArcLength;

	[SerializeField]
	private float[] _storedArcLengths;

	[SerializeField]
	private float[] _storedArcLengthsFull;

	[SerializeField]
	private Vector3[] _storedPoints;

	[SerializeField]
	private float[] _normalisedPercentages;

	[SerializeField]
	private float _storedPointResolution = 0.1f;

	[SerializeField]
	private int _storedValueArraySize;

	[SerializeField]
	private Vector3[] _storedPathDirections;

	[SerializeField]
	private float _directionWidth = 0.05f;

	[SerializeField]
	private CameraPathControlPoint[] _pointALink;

	[SerializeField]
	private CameraPathControlPoint[] _pointBLink;

	[SerializeField]
	private CameraPathOrientationList _orientationList;

	[SerializeField]
	private CameraPathFOVList _fovList;

	[SerializeField]
	private CameraPathTiltList _tiltList;

	[SerializeField]
	private CameraPathSpeedList _speedList;

	[SerializeField]
	private CameraPathEventList _eventList;

	[SerializeField]
	private CameraPathDelayList _delayList;

	[SerializeField]
	private bool _addOrientationsWithPoints = true;

	[SerializeField]
	private bool _looped;

	[SerializeField]
	private bool _normalised = true;

	[SerializeField]
	private Bounds _pathBounds = default(Bounds);

	public float hermiteTension;

	public float hermiteBias;

	public GameObject editorPreview;

	public int selectedPoint;

	public CameraPath.PointModes pointMode;

	public float addPointAtPercent;

	[SerializeField]
	private float _aspect = 1.7778f;

	[SerializeField]
	private int _previewResolution = 800;

	public float drawDistance = 1000f;

	[SerializeField]
	private int _displayHeight = 225;

	[SerializeField]
	private CameraPath _nextPath;

	[SerializeField]
	private bool _interpolateNextPath;

	public bool showGizmos = true;

	public Color selectedPathColour = CameraPathColours.GREEN;

	public Color unselectedPathColour = CameraPathColours.GREY;

	public Color selectedPointColour = CameraPathColours.RED;

	public Color unselectedPointColour = CameraPathColours.GREEN;

	public bool showOrientationIndicators;

	public float orientationIndicatorUnitLength = 2.5f;

	public Color orientationIndicatorColours = CameraPathColours.PURPLE;

	public bool autoSetStoedPointRes = true;

	public bool enableUndo = true;

	public bool showPreview = true;

	public bool enablePreviews = true;

	public event CameraPath.RecalculateCurvesHandler RecalculateCurvesEvent
	{
		[MethodImpl(32)]
		add
		{
			this.RecalculateCurvesEvent = (CameraPath.RecalculateCurvesHandler)Delegate.Combine(this.RecalculateCurvesEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.RecalculateCurvesEvent = (CameraPath.RecalculateCurvesHandler)Delegate.Remove(this.RecalculateCurvesEvent, value);
		}
	}

	public event CameraPath.PathPointAddedHandler PathPointAddedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.PathPointAddedEvent = (CameraPath.PathPointAddedHandler)Delegate.Combine(this.PathPointAddedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.PathPointAddedEvent = (CameraPath.PathPointAddedHandler)Delegate.Remove(this.PathPointAddedEvent, value);
		}
	}

	public event CameraPath.PathPointRemovedHandler PathPointRemovedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.PathPointRemovedEvent = (CameraPath.PathPointRemovedHandler)Delegate.Combine(this.PathPointRemovedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.PathPointRemovedEvent = (CameraPath.PathPointRemovedHandler)Delegate.Remove(this.PathPointRemovedEvent, value);
		}
	}

	public event CameraPath.CheckStartPointCullHandler CheckStartPointCullEvent
	{
		[MethodImpl(32)]
		add
		{
			this.CheckStartPointCullEvent = (CameraPath.CheckStartPointCullHandler)Delegate.Combine(this.CheckStartPointCullEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.CheckStartPointCullEvent = (CameraPath.CheckStartPointCullHandler)Delegate.Remove(this.CheckStartPointCullEvent, value);
		}
	}

	public event CameraPath.CheckEndPointCullHandler CheckEndPointCullEvent
	{
		[MethodImpl(32)]
		add
		{
			this.CheckEndPointCullEvent = (CameraPath.CheckEndPointCullHandler)Delegate.Combine(this.CheckEndPointCullEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.CheckEndPointCullEvent = (CameraPath.CheckEndPointCullHandler)Delegate.Remove(this.CheckEndPointCullEvent, value);
		}
	}

	public event CameraPath.CleanUpListsHandler CleanUpListsEvent
	{
		[MethodImpl(32)]
		add
		{
			this.CleanUpListsEvent = (CameraPath.CleanUpListsHandler)Delegate.Combine(this.CleanUpListsEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.CleanUpListsEvent = (CameraPath.CleanUpListsHandler)Delegate.Remove(this.CleanUpListsEvent, value);
		}
	}

	public CameraPathControlPoint this[int index]
	{
		get
		{
			int count = this._points.get_Count();
			if (this._looped)
			{
				if (this.shouldInterpolateNextPath)
				{
					if (index == count)
					{
						index = 0;
					}
					else
					{
						if (index > count)
						{
							return this._nextPath[index % count];
						}
						if (index < 0)
						{
							Debug.LogError("Index out of range");
						}
					}
				}
				else
				{
					index %= count;
				}
			}
			else
			{
				if (index < 0)
				{
					Debug.LogError("Index can't be minus");
				}
				if (index >= this._points.get_Count())
				{
					if (index >= this._points.get_Count() && this.shouldInterpolateNextPath)
					{
						return this.nextPath[index % count];
					}
					Debug.LogError("Index out of range");
				}
			}
			return this._points.get_Item(index);
		}
	}

	public int numberOfPoints
	{
		get
		{
			if (this._points.get_Count() == 0)
			{
				return 0;
			}
			int num = (!this._looped) ? this._points.get_Count() : (this._points.get_Count() + 1);
			if (this.shouldInterpolateNextPath)
			{
				num++;
			}
			return num;
		}
	}

	public int realNumberOfPoints
	{
		get
		{
			return this._points.get_Count();
		}
	}

	public int numberOfCurves
	{
		get
		{
			if (this._points.get_Count() < 2)
			{
				return 0;
			}
			return this.numberOfPoints - 1;
		}
	}

	public bool loop
	{
		get
		{
			return this._looped;
		}
		set
		{
			if (this._looped != value)
			{
				this._looped = value;
				this.RecalculateStoredValues();
			}
		}
	}

	public float pathLength
	{
		get
		{
			return this._storedTotalArcLength;
		}
	}

	public CameraPathOrientationList orientationList
	{
		get
		{
			return this._orientationList;
		}
	}

	public CameraPathFOVList fovList
	{
		get
		{
			return this._fovList;
		}
	}

	public CameraPathTiltList tiltList
	{
		get
		{
			return this._tiltList;
		}
	}

	public CameraPathSpeedList speedList
	{
		get
		{
			return this._speedList;
		}
	}

	public CameraPathEventList eventList
	{
		get
		{
			return this._eventList;
		}
	}

	public CameraPathDelayList delayList
	{
		get
		{
			return this._delayList;
		}
	}

	public Bounds bounds
	{
		get
		{
			return this._pathBounds;
		}
	}

	public int storedValueArraySize
	{
		get
		{
			return this._storedValueArraySize;
		}
	}

	public CameraPathControlPoint[] pointALink
	{
		get
		{
			return this._pointALink;
		}
	}

	public CameraPathControlPoint[] pointBLink
	{
		get
		{
			return this._pointBLink;
		}
	}

	public Vector3[] storedPoints
	{
		get
		{
			return this._storedPoints;
		}
	}

	public bool normalised
	{
		get
		{
			return this._normalised;
		}
		set
		{
			this._normalised = value;
		}
	}

	public CameraPath.Interpolation interpolation
	{
		get
		{
			return this._interpolation;
		}
		set
		{
			if (value != this._interpolation)
			{
				this._interpolation = value;
				this.RecalculateStoredValues();
			}
		}
	}

	public CameraPath nextPath
	{
		get
		{
			return this._nextPath;
		}
		set
		{
			if (value != this._nextPath)
			{
				if (value == this)
				{
					Debug.LogError("Do not link a path to itself! The Universe would crumble and it would be your fault!! If you want to loop a path, just toggle the loop option...");
					return;
				}
				this._nextPath = value;
				this._nextPath.GetComponent<CameraPathAnimator>().playOnStart = false;
				this.RecalculateStoredValues();
			}
		}
	}

	public bool interpolateNextPath
	{
		get
		{
			return this._interpolateNextPath;
		}
		set
		{
			if (this._interpolateNextPath != value)
			{
				this._interpolateNextPath = value;
				this.RecalculateStoredValues();
			}
		}
	}

	public bool shouldInterpolateNextPath
	{
		get
		{
			return this.nextPath != null && this.interpolateNextPath;
		}
	}

	public float storedPointResolution
	{
		get
		{
			return this._storedPointResolution;
		}
		set
		{
			this._storedPointResolution = Mathf.Clamp(value, this._storedTotalArcLength / 10000f, 10f);
		}
	}

	public float directionWidth
	{
		get
		{
			return this._directionWidth;
		}
		set
		{
			this._directionWidth = value;
		}
	}

	public int displayHeight
	{
		get
		{
			return this._displayHeight;
		}
		set
		{
			this._displayHeight = Mathf.Clamp(value, 100, 500);
		}
	}

	public float aspect
	{
		get
		{
			return this._aspect;
		}
		set
		{
			this._aspect = Mathf.Clamp(value, 0.1f, 10f);
		}
	}

	public int previewResolution
	{
		get
		{
			return this._previewResolution;
		}
		set
		{
			this._previewResolution = Mathf.Clamp(value, 1, 1024);
		}
	}

	public float StoredArcLength(int curve)
	{
		if (this._looped)
		{
			curve %= this.numberOfCurves - 1;
		}
		else
		{
			curve = Mathf.Clamp(curve, 0, this.numberOfCurves - 1);
		}
		curve = Mathf.Clamp(curve, 0, this._storedArcLengths.Length - 1);
		return this._storedArcLengths[curve];
	}

	public int StoredValueIndex(float percentage)
	{
		int num = this.storedValueArraySize - 1;
		return Mathf.Clamp(Mathf.RoundToInt((float)num * percentage), 0, num);
	}

	public CameraPathControlPoint AddPoint(Vector3 position)
	{
		CameraPathControlPoint cameraPathControlPoint = base.get_gameObject().AddComponent<CameraPathControlPoint>();
		cameraPathControlPoint.set_hideFlags(2);
		cameraPathControlPoint.localPosition = position;
		this._points.Add(cameraPathControlPoint);
		if (this._addOrientationsWithPoints)
		{
			this.orientationList.AddOrientation(cameraPathControlPoint);
		}
		this.RecalculateStoredValues();
		this.PathPointAddedEvent(cameraPathControlPoint);
		return cameraPathControlPoint;
	}

	public void AddPoint(CameraPathControlPoint point)
	{
		this._points.Add(point);
		this.RecalculateStoredValues();
		this.PathPointAddedEvent(point);
	}

	public void InsertPoint(CameraPathControlPoint point, int index)
	{
		this._points.Insert(index, point);
		this.RecalculateStoredValues();
		this.PathPointAddedEvent(point);
	}

	public CameraPathControlPoint InsertPoint(int index)
	{
		CameraPathControlPoint cameraPathControlPoint = base.get_gameObject().AddComponent<CameraPathControlPoint>();
		cameraPathControlPoint.set_hideFlags(2);
		this._points.Insert(index, cameraPathControlPoint);
		this.RecalculateStoredValues();
		this.PathPointAddedEvent(cameraPathControlPoint);
		return cameraPathControlPoint;
	}

	public void RemovePoint(int index)
	{
		this.RemovePoint(this[index]);
	}

	public bool RemovePoint(string pointName)
	{
		using (List<CameraPathControlPoint>.Enumerator enumerator = this._points.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CameraPathControlPoint current = enumerator.get_Current();
				if (current.displayName == pointName)
				{
					this.RemovePoint(current);
					return true;
				}
			}
		}
		return false;
	}

	public void RemovePoint(Vector3 pointPosition)
	{
		using (List<CameraPathControlPoint>.Enumerator enumerator = this._points.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CameraPathControlPoint current = enumerator.get_Current();
				if (current.worldPosition == pointPosition)
				{
					this.RemovePoint(current);
				}
			}
		}
		float nearestPoint = this.GetNearestPoint(pointPosition, true);
		this.RemovePoint(this.GetNearestPointIndex(nearestPoint));
	}

	public void RemovePoint(CameraPathControlPoint point)
	{
		if (this._points.get_Count() < 3)
		{
			Debug.Log("We can't see any point in allowing you to delete any more points so we're not going to do it.");
			return;
		}
		this.PathPointRemovedEvent(point);
		int num = this._points.IndexOf(point);
		if (num == 0)
		{
			float pathPercentage = this.GetPathPercentage(1);
			this.CheckStartPointCullEvent(pathPercentage);
		}
		if (num == this.realNumberOfPoints - 1)
		{
			float pathPercentage2 = this.GetPathPercentage(this.realNumberOfPoints - 2);
			this.CheckEndPointCullEvent(pathPercentage2);
		}
		this._points.Remove(point);
		this.RecalculateStoredValues();
	}

	private float ParsePercentage(float percentage)
	{
		if (percentage == 0f)
		{
			return 0f;
		}
		if (percentage == 1f)
		{
			return 1f;
		}
		if (this._looped)
		{
			percentage %= 1f;
		}
		else
		{
			percentage = Mathf.Clamp01(percentage);
		}
		if (this._normalised)
		{
			int num = this.storedValueArraySize - 1;
			float num2 = 1f / (float)this.storedValueArraySize;
			int num3 = Mathf.Clamp(Mathf.FloorToInt((float)this.storedValueArraySize * percentage), 0, num);
			int num4 = Mathf.Clamp(num3 + 1, 0, num);
			float num5 = (float)num3 * num2;
			float num6 = (float)num4 * num2;
			float num7 = this._normalisedPercentages[num3];
			float num8 = this._normalisedPercentages[num4];
			if (num7 == num8)
			{
				return num7;
			}
			float num9 = (percentage - num5) / (num6 - num5);
			percentage = Mathf.Lerp(num7, num8, num9);
		}
		return percentage;
	}

	public float CalculateNormalisedPercentage(float percentage)
	{
		if (this.realNumberOfPoints < 2)
		{
			return percentage;
		}
		if (percentage <= 0f)
		{
			return 0f;
		}
		if (percentage >= 1f)
		{
			return 1f;
		}
		if (this._storedTotalArcLength == 0f)
		{
			return percentage;
		}
		float num = percentage * this._storedTotalArcLength;
		int i = 0;
		int num2 = this.storedValueArraySize - 1;
		int num3 = 0;
		while (i < num2)
		{
			num3 = i + (num2 - i) / 2;
			if (this._storedArcLengthsFull[num3] < num)
			{
				i = num3 + 1;
			}
			else
			{
				num2 = num3;
			}
		}
		if (this._storedArcLengthsFull[num3] > num && num3 > 0)
		{
			num3--;
		}
		float num4 = this._storedArcLengthsFull[num3];
		float result = (float)num3 / (float)(this.storedValueArraySize - 1);
		if (num4 == num)
		{
			return result;
		}
		return ((float)num3 + (num - num4) / (this._storedArcLengthsFull[num3 + 1] - num4)) / (float)this.storedValueArraySize;
	}

	public float DeNormalisePercentage(float normalisedPercent)
	{
		int num = this._normalisedPercentages.Length;
		int i = 0;
		while (i < num)
		{
			if (this._normalisedPercentages[i] > normalisedPercent)
			{
				if (i == 0)
				{
					return 0f;
				}
				float num2 = (float)(i - 1) / (float)num;
				float num3 = (float)i / (float)num;
				float num4 = this._normalisedPercentages[i - 1];
				float num5 = this._normalisedPercentages[i];
				float num6 = (normalisedPercent - num4) / (num5 - num4);
				return Mathf.Lerp(num2, num3, num6);
			}
			else
			{
				i++;
			}
		}
		return 1f;
	}

	public int GetPointNumber(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		float num = 1f / (float)this.numberOfCurves;
		return Mathf.Clamp(Mathf.FloorToInt(percentage / num), 0, this._points.get_Count() - 1);
	}

	public Vector3 GetPathPosition(float percentage)
	{
		return this.GetPathPosition(percentage, false);
	}

	public Vector3 GetPathPosition(float percentage, bool ignoreNormalisation)
	{
		if (this.realNumberOfPoints < 2)
		{
			Debug.LogError("Not enough points to define a curve");
			if (this.realNumberOfPoints == 1)
			{
				return this._points.get_Item(0).worldPosition;
			}
			return Vector3.get_zero();
		}
		else
		{
			if (!ignoreNormalisation)
			{
				percentage = this.ParsePercentage(percentage);
			}
			float num = 1f / (float)this.numberOfCurves;
			int num2 = Mathf.FloorToInt(percentage / num);
			float num3 = Mathf.Clamp01((percentage - (float)num2 * num) * (float)this.numberOfCurves);
			CameraPathControlPoint point = this.GetPoint(num2);
			CameraPathControlPoint point2 = this.GetPoint(num2 + 1);
			if (point == null || point2 == null)
			{
				return Vector3.get_zero();
			}
			switch (this.interpolation)
			{
			case CameraPath.Interpolation.Linear:
				return Vector3.Lerp(point.worldPosition, point2.worldPosition, num3);
			case CameraPath.Interpolation.SmoothStep:
				return Vector3.Lerp(point.worldPosition, point2.worldPosition, CPMath.SmoothStep(num3));
			case CameraPath.Interpolation.CatmullRom:
			{
				CameraPathControlPoint point3 = this.GetPoint(num2 - 1);
				CameraPathControlPoint point4 = this.GetPoint(num2 + 2);
				return CPMath.CalculateCatmullRom(point3.worldPosition, point.worldPosition, point2.worldPosition, point4.worldPosition, num3);
			}
			case CameraPath.Interpolation.Hermite:
			{
				CameraPathControlPoint point3 = this.GetPoint(num2 - 1);
				CameraPathControlPoint point4 = this.GetPoint(num2 + 2);
				return CPMath.CalculateHermite(point3.worldPosition, point.worldPosition, point2.worldPosition, point4.worldPosition, num3, this.hermiteTension, this.hermiteBias);
			}
			case CameraPath.Interpolation.Bezier:
				return CPMath.CalculateBezier(num3, point.worldPosition, point.forwardControlPointWorld, point2.backwardControlPointWorld, point2.worldPosition);
			default:
				return Vector3.get_zero();
			}
		}
	}

	public Quaternion GetPathRotation(float percentage, bool ignoreNormalisation)
	{
		if (!ignoreNormalisation)
		{
			percentage = this.ParsePercentage(percentage);
		}
		return this.orientationList.GetOrientation(percentage);
	}

	public Vector3 GetPathDirection(float percentage)
	{
		return this.GetPathDirection(percentage, true);
	}

	public Vector3 GetPathDirection(float percentage, bool normalisePercent)
	{
		int num = this.storedValueArraySize - 1;
		int num2 = Mathf.Clamp(Mathf.FloorToInt((float)num * percentage), 0, num);
		int num3 = Mathf.Clamp(Mathf.CeilToInt((float)num * percentage), 0, num);
		if (num2 == num3)
		{
			return this._storedPathDirections[num2];
		}
		float num4 = (float)num2 / (float)this.storedValueArraySize;
		float num5 = (float)num3 / (float)this.storedValueArraySize;
		float num6 = (percentage - num4) / (num5 - num4);
		Vector3 vector = this._storedPathDirections[num2];
		Vector3 vector2 = this._storedPathDirections[num3];
		return Vector3.Lerp(vector, vector2, num6);
	}

	public float GetPathTilt(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		return this._tiltList.GetTilt(percentage);
	}

	public float GetPathFOV(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		return this._fovList.GetValue(percentage, CameraPathFOVList.ProjectionType.FOV);
	}

	public float GetPathOrthographicSize(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		return this._fovList.GetValue(percentage, CameraPathFOVList.ProjectionType.Orthographic);
	}

	public float GetPathSpeed(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		float speed = this._speedList.GetSpeed(percentage);
		return speed * this._delayList.CheckEase(percentage);
	}

	public float GetPathEase(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		return this._delayList.CheckEase(percentage);
	}

	public void CheckEvents(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		this._eventList.CheckEvents(percentage);
		this._delayList.CheckEvents(percentage);
	}

	public float GetPathPercentage(CameraPathControlPoint point)
	{
		int num = this._points.IndexOf(point);
		return (float)num / (float)this.numberOfCurves;
	}

	public float GetPathPercentage(int pointIndex)
	{
		return (float)pointIndex / (float)this.numberOfCurves;
	}

	public int GetNearestPointIndex(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		return Mathf.RoundToInt((float)this.numberOfCurves * percentage);
	}

	public int GetLastPointIndex(float percentage, bool isNormalised)
	{
		if (isNormalised)
		{
			percentage = this.ParsePercentage(percentage);
		}
		return Mathf.FloorToInt((float)this.numberOfCurves * percentage);
	}

	public int GetNextPointIndex(float percentage, bool isNormalised)
	{
		if (isNormalised)
		{
			percentage = this.ParsePercentage(percentage);
		}
		return Mathf.CeilToInt((float)this.numberOfCurves * percentage);
	}

	public float GetCurvePercentage(CameraPathControlPoint pointA, CameraPathControlPoint pointB, float percentage)
	{
		float num = this.GetPathPercentage(pointA);
		float num2 = this.GetPathPercentage(pointB);
		if (num == num2)
		{
			return num;
		}
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		return Mathf.Clamp01((percentage - num) / (num2 - num));
	}

	public float GetCurvePercentage(CameraPathPoint pointA, CameraPathPoint pointB, float percentage)
	{
		float num = pointA.percent;
		float num2 = pointB.percent;
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		return Mathf.Clamp01((percentage - num) / (num2 - num));
	}

	public float GetCurvePercentage(CameraPathPoint point)
	{
		float num = this.GetPathPercentage(point.cpointA);
		float num2 = this.GetPathPercentage(point.cpointB);
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		point.curvePercentage = Mathf.Clamp01((point.percent - num) / (num2 - num));
		return point.curvePercentage;
	}

	public float GetOutroEasePercentage(CameraPathDelay point)
	{
		float num = point.percent;
		float num2 = this._delayList.GetPoint(point.index + 1).percent;
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		return Mathf.Lerp(num, num2, point.outroEndEasePercentage);
	}

	public float GetIntroEasePercentage(CameraPathDelay point)
	{
		float percent = this._delayList.GetPoint(point.index - 1).percent;
		float percent2 = point.percent;
		return Mathf.Lerp(percent, percent2, 1f - point.introStartEasePercentage);
	}

	public float GetPathPercentage(CameraPathControlPoint pointA, CameraPathControlPoint pointB, float curvePercentage)
	{
		float pathPercentage = this.GetPathPercentage(pointA);
		float pathPercentage2 = this.GetPathPercentage(pointB);
		return Mathf.Lerp(pathPercentage, pathPercentage2, curvePercentage);
	}

	public float GetPathPercentage(float pointA, float pointB, float curvePercentage)
	{
		return Mathf.Lerp(pointA, pointB, curvePercentage);
	}

	public int GetStoredPoint(float percentage)
	{
		percentage = this.ParsePercentage(percentage);
		return Mathf.Clamp(Mathf.FloorToInt((float)this.storedValueArraySize * percentage), 0, this.storedValueArraySize - 1);
	}

	private void Awake()
	{
		this.Init();
	}

	private void Start()
	{
		if (!Application.get_isPlaying())
		{
			if (this.version == CameraPath.CURRENT_VERSION_NUMBER)
			{
				return;
			}
			if (this.version > CameraPath.CURRENT_VERSION_NUMBER)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Camera Path v.",
					this.version,
					": Great scot! This data is from the future! (version:",
					CameraPath.CURRENT_VERSION_NUMBER,
					") - need to avoid contact to ensure the survival of the universe..."
				}));
				return;
			}
			Debug.Log(string.Concat(new object[]
			{
				"Camera Path v.",
				this.version,
				" Upgrading to version ",
				CameraPath.CURRENT_VERSION_NUMBER,
				"\nRemember to backup your data!"
			}));
			this.version = CameraPath.CURRENT_VERSION_NUMBER;
		}
	}

	private void OnValidate()
	{
		this.InitialiseLists();
		if (!Application.get_isPlaying())
		{
			this.RecalculateStoredValues();
		}
	}

	private void OnDestroy()
	{
		this.Clear();
		if (this.CleanUpListsEvent != null)
		{
			this.CleanUpListsEvent();
		}
	}

	public void RecalculateStoredValues()
	{
		if (!this._normalised)
		{
			this._storedTotalArcLength = 0f;
			this._storedArcLengths = new float[0];
			this._storedArcLengthsFull = new float[0];
			this._storedPoints = new Vector3[0];
			this._normalisedPercentages = new float[0];
			this._storedPathDirections = new Vector3[0];
			return;
		}
		if (this.autoSetStoedPointRes && this._storedTotalArcLength > 0f)
		{
			this._storedPointResolution = this._storedTotalArcLength / 1000f;
		}
		for (int i = 0; i < this.realNumberOfPoints; i++)
		{
			CameraPathControlPoint cameraPathControlPoint = this._points.get_Item(i);
			cameraPathControlPoint.percentage = this.GetPathPercentage(i);
			cameraPathControlPoint.normalisedPercentage = this.CalculateNormalisedPercentage(this._points.get_Item(i).percentage);
			cameraPathControlPoint.givenName = "Point " + i;
			cameraPathControlPoint.fullName = base.get_name() + " Point " + i;
			cameraPathControlPoint.index = i;
			cameraPathControlPoint.set_hideFlags(2);
		}
		if (this._points.get_Count() < 2)
		{
			return;
		}
		this._storedTotalArcLength = 0f;
		for (int j = 0; j < this.numberOfCurves; j++)
		{
			CameraPathControlPoint point = this.GetPoint(j);
			CameraPathControlPoint point2 = this.GetPoint(j + 1);
			float num = 0f;
			num += Vector3.Distance(point.worldPosition, point.forwardControlPointWorld);
			num += Vector3.Distance(point.forwardControlPointWorld, point2.backwardControlPointWorld);
			num += Vector3.Distance(point2.backwardControlPointWorld, point2.worldPosition);
			this._storedTotalArcLength += num;
		}
		this._storedValueArraySize = Mathf.Max(Mathf.RoundToInt(this._storedTotalArcLength / this._storedPointResolution), 1);
		float num2 = 1f / (float)(this._storedValueArraySize * 10);
		float num3 = 0f;
		float num4 = this._storedTotalArcLength / (float)(this._storedValueArraySize - 1);
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		List<float> list3 = new List<float>();
		List<float> list4 = new List<float>();
		List<float> list5 = new List<float>();
		float num5 = 0f;
		float num6 = num4;
		float num7 = 0f;
		Vector3 vector = this.GetPathPosition(0f, true);
		list.Add(vector);
		list2.Add((this.GetPathPosition(num2, true) - vector).get_normalized());
		list3.Add(0f);
		while (num3 < 1f)
		{
			Vector3 pathPosition = this.GetPathPosition(num3, true);
			float num8 = Vector3.Distance(vector, pathPosition);
			if (num5 + num8 >= num6)
			{
				float num9 = Mathf.Clamp01((num6 - num5) / num8);
				float num10 = Mathf.Lerp(num3, num3 + num2, num9);
				list3.Add(num10);
				list.Add(pathPosition);
				float percentage = Mathf.Clamp(num3 - this._directionWidth, 0f, 1f);
				Vector3 pathPosition2 = this.GetPathPosition(percentage, true);
				float percentage2 = Mathf.Clamp(num3 + this._directionWidth, 0f, 1f);
				Vector3 pathPosition3 = this.GetPathPosition(percentage2, true);
				Vector3 normalized = (vector - pathPosition2 + (pathPosition3 - vector)).get_normalized();
				list2.Add(normalized);
				list4.Add(num5);
				list5.Add(num7);
				num5 = num6;
				num6 += num4;
			}
			num5 += num8;
			num7 += num8;
			vector = pathPosition;
			num3 += num2;
		}
		list3.Add(1f);
		list.Add(this.GetPathPosition(1f, true));
		Vector3 pathPosition4 = this.GetPathPosition(1f, true);
		Vector3 pathPosition5 = this.GetPathPosition(1f - num2, true);
		Vector3 normalized2 = (pathPosition4 - pathPosition5).get_normalized();
		list2.Add(normalized2);
		this._storedValueArraySize = list3.get_Count();
		this._normalisedPercentages = list3.ToArray();
		this._storedTotalArcLength = num7;
		this._storedPoints = list.ToArray();
		this._storedPathDirections = list2.ToArray();
		this._storedArcLengths = list4.ToArray();
		this._storedArcLengthsFull = list5.ToArray();
		if (this.RecalculateCurvesEvent != null)
		{
			this.RecalculateCurvesEvent();
		}
	}

	public float GetNearestPoint(Vector3 fromPostition)
	{
		return this.GetNearestPoint(fromPostition, false, 4);
	}

	public float GetNearestPoint(Vector3 fromPostition, bool ignoreNormalisation)
	{
		return this.GetNearestPoint(fromPostition, ignoreNormalisation, 4);
	}

	public float GetNearestPoint(Vector3 fromPostition, bool ignoreNormalisation, int refinments)
	{
		int num = 10;
		float num2 = 1f / (float)num;
		float num3 = 0f;
		float num4 = float.PositiveInfinity;
		for (float num5 = 0f; num5 < 1f; num5 += num2)
		{
			Vector3 pathPosition = this.GetPathPosition(num5, ignoreNormalisation);
			Vector3 vector = pathPosition - fromPostition;
			float num6 = Vector3.SqrMagnitude(vector);
			if (num4 > num6)
			{
				num3 = num5;
				num4 = num6;
			}
		}
		float num7 = num3;
		float num8 = num4;
		for (int i = 0; i < refinments; i++)
		{
			float num9 = num2 / 1.8f;
			float num10 = num3 - num9;
			float num11 = num3 + num9;
			float num12 = num2 / (float)num;
			for (float num13 = num10; num13 < num11; num13 += num12)
			{
				float num14 = num13 % 1f;
				if (num14 < 0f)
				{
					num14 += 1f;
				}
				Vector3 pathPosition2 = this.GetPathPosition(num14, ignoreNormalisation);
				Vector3 vector2 = pathPosition2 - fromPostition;
				float num15 = Vector3.SqrMagnitude(vector2);
				if (num4 > num15)
				{
					num7 = num3;
					num8 = num4;
					num3 = num14;
					num4 = num15;
				}
				else if (num8 > num15)
				{
					num7 = num14;
					num8 = num15;
				}
			}
			num2 = num12;
		}
		float num16 = num4 / (num4 + num8);
		return Mathf.Clamp01(Mathf.Lerp(num3, num7, num16));
	}

	public float GetNearestPointNear(Vector3 fromPostition, float prevPercentage, Vector3 prevPosition, bool ignoreNormalisation, int refinments)
	{
		int num = 10;
		float num2 = 1f / (float)num;
		float num3 = prevPercentage;
		float num4 = num3;
		float num5 = Vector3.SqrMagnitude(prevPosition - fromPostition);
		float num6 = num5;
		for (int i = 0; i < refinments; i++)
		{
			float num7 = num2 / 1.8f;
			float num8 = num3 - num7;
			float num9 = num3 + num7;
			float num10 = num2 / (float)num;
			for (float num11 = num8; num11 < num9; num11 += num10)
			{
				float num12 = num11 % 1f;
				if (num12 < 0f)
				{
					num12 += 1f;
				}
				Vector3 pathPosition = this.GetPathPosition(num12, ignoreNormalisation);
				Vector3 vector = pathPosition - fromPostition;
				float num13 = Vector3.SqrMagnitude(vector);
				if (num5 > num13)
				{
					num4 = num3;
					num6 = num5;
					num3 = num12;
					num5 = num13;
				}
				else if (num6 > num13)
				{
					num4 = num12;
					num6 = num13;
				}
			}
			num2 = num10;
		}
		float num14 = num5 / (num5 + num6);
		return Mathf.Clamp01(Mathf.Lerp(num3, num4, num14));
	}

	public void Clear()
	{
		this._points.Clear();
	}

	public CameraPathControlPoint GetPoint(int index)
	{
		return this[this.GetPointIndex(index)];
	}

	public int GetPointIndex(int index)
	{
		if (this._points.get_Count() == 0)
		{
			return -1;
		}
		if (!this._looped)
		{
			return Mathf.Clamp(index, 0, this.numberOfCurves);
		}
		if (index >= this.numberOfCurves)
		{
			index -= this.numberOfCurves;
		}
		if (index < 0)
		{
			index += this.numberOfCurves;
		}
		return index;
	}

	public int GetCurveIndex(int startPointIndex)
	{
		if (this._points.get_Count() == 0)
		{
			return -1;
		}
		if (!this._looped)
		{
			return Mathf.Clamp(startPointIndex, 0, this.numberOfCurves - 1);
		}
		if (startPointIndex >= this.numberOfCurves - 1)
		{
			startPointIndex = startPointIndex - this.numberOfCurves - 1;
		}
		if (startPointIndex < 0)
		{
			startPointIndex = startPointIndex + this.numberOfCurves - 1;
		}
		return startPointIndex;
	}

	private void Init()
	{
		this.InitialiseLists();
		if (this.initialised)
		{
			return;
		}
		CameraPathControlPoint cameraPathControlPoint = base.get_gameObject().AddComponent<CameraPathControlPoint>();
		CameraPathControlPoint cameraPathControlPoint2 = base.get_gameObject().AddComponent<CameraPathControlPoint>();
		CameraPathControlPoint cameraPathControlPoint3 = base.get_gameObject().AddComponent<CameraPathControlPoint>();
		CameraPathControlPoint cameraPathControlPoint4 = base.get_gameObject().AddComponent<CameraPathControlPoint>();
		cameraPathControlPoint.set_hideFlags(2);
		cameraPathControlPoint2.set_hideFlags(2);
		cameraPathControlPoint3.set_hideFlags(2);
		cameraPathControlPoint4.set_hideFlags(2);
		cameraPathControlPoint.localPosition = new Vector3(-20f, 0f, -20f);
		cameraPathControlPoint2.localPosition = new Vector3(20f, 0f, -20f);
		cameraPathControlPoint3.localPosition = new Vector3(20f, 0f, 20f);
		cameraPathControlPoint4.localPosition = new Vector3(-20f, 0f, 20f);
		cameraPathControlPoint.forwardControlPoint = new Vector3(0f, 0f, -20f);
		cameraPathControlPoint2.forwardControlPoint = new Vector3(40f, 0f, -20f);
		cameraPathControlPoint3.forwardControlPoint = new Vector3(0f, 0f, 20f);
		cameraPathControlPoint4.forwardControlPoint = new Vector3(-40f, 0f, 20f);
		this.AddPoint(cameraPathControlPoint);
		this.AddPoint(cameraPathControlPoint2);
		this.AddPoint(cameraPathControlPoint3);
		this.AddPoint(cameraPathControlPoint4);
		this.initialised = true;
	}

	private void InitialiseLists()
	{
		if (this._orientationList == null)
		{
			this._orientationList = base.get_gameObject().AddComponent<CameraPathOrientationList>();
		}
		if (this._fovList == null)
		{
			this._fovList = base.get_gameObject().AddComponent<CameraPathFOVList>();
		}
		if (this._tiltList == null)
		{
			this._tiltList = base.get_gameObject().AddComponent<CameraPathTiltList>();
		}
		if (this._speedList == null)
		{
			this._speedList = base.get_gameObject().AddComponent<CameraPathSpeedList>();
		}
		if (this._eventList == null)
		{
			this._eventList = base.get_gameObject().AddComponent<CameraPathEventList>();
		}
		if (this._delayList == null)
		{
			this._delayList = base.get_gameObject().AddComponent<CameraPathDelayList>();
		}
		this._orientationList.Init(this);
		this._fovList.Init(this);
		this._tiltList.Init(this);
		this._speedList.Init(this);
		this._eventList.Init(this);
		this._delayList.Init(this);
	}
}
