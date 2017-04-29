using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathPointList : MonoBehaviour
{
	[SerializeField]
	private List<CameraPathPoint> _points = new List<CameraPathPoint>();

	[SerializeField]
	protected CameraPath cameraPath;

	protected string pointTypeName = "point";

	[NonSerialized]
	protected bool initialised;

	public CameraPathPoint this[int index]
	{
		get
		{
			if (this.cameraPath.loop && index > this._points.get_Count() - 1)
			{
				index %= this._points.get_Count();
			}
			if (index < 0)
			{
				Debug.LogError("Index can't be minus");
			}
			if (index >= this._points.get_Count())
			{
				Debug.LogError("Index out of range");
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
			return (!this.cameraPath.loop) ? this._points.get_Count() : (this._points.get_Count() + 1);
		}
	}

	public int realNumberOfPoints
	{
		get
		{
			return this._points.get_Count();
		}
	}

	private void OnEnable()
	{
		base.set_hideFlags(2);
	}

	public virtual void Init(CameraPath _cameraPath)
	{
		if (this.initialised)
		{
			return;
		}
		base.set_hideFlags(2);
		this.CheckListIsNull();
		this.cameraPath = _cameraPath;
		this.cameraPath.CleanUpListsEvent += new CameraPath.CleanUpListsHandler(this.CleanUp);
		this.cameraPath.RecalculateCurvesEvent += new CameraPath.RecalculateCurvesHandler(this.RecalculatePoints);
		this.cameraPath.PathPointRemovedEvent += new CameraPath.PathPointRemovedHandler(this.PathPointRemovedEvent);
		this.cameraPath.CheckStartPointCullEvent += new CameraPath.CheckStartPointCullHandler(this.CheckPointCullEventFromStart);
		this.cameraPath.CheckEndPointCullEvent += new CameraPath.CheckEndPointCullHandler(this.CheckPointCullEventFromEnd);
		this.initialised = true;
	}

	public virtual void CleanUp()
	{
		this.cameraPath.CleanUpListsEvent -= new CameraPath.CleanUpListsHandler(this.CleanUp);
		this.cameraPath.RecalculateCurvesEvent -= new CameraPath.RecalculateCurvesHandler(this.RecalculatePoints);
		this.cameraPath.PathPointRemovedEvent -= new CameraPath.PathPointRemovedHandler(this.PathPointRemovedEvent);
		this.cameraPath.CheckStartPointCullEvent -= new CameraPath.CheckStartPointCullHandler(this.CheckPointCullEventFromStart);
		this.cameraPath.CheckEndPointCullEvent -= new CameraPath.CheckEndPointCullHandler(this.CheckPointCullEventFromEnd);
		this.initialised = false;
	}

	public int IndexOf(CameraPathPoint point)
	{
		return this._points.IndexOf(point);
	}

	public void AddPoint(CameraPathPoint newPoint, CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage)
	{
		newPoint.positionModes = CameraPathPoint.PositionModes.Free;
		newPoint.cpointA = curvePointA;
		newPoint.cpointB = curvePointB;
		newPoint.curvePercentage = curvePercetage;
		this._points.Add(newPoint);
		this.RecalculatePoints();
	}

	public void AddPoint(CameraPathPoint newPoint, float fixPercent)
	{
		newPoint.positionModes = CameraPathPoint.PositionModes.FixedToPercent;
		newPoint.percent = fixPercent;
		this._points.Add(newPoint);
		this.RecalculatePoints();
	}

	public void AddPoint(CameraPathPoint newPoint, CameraPathControlPoint atPoint)
	{
		newPoint.positionModes = CameraPathPoint.PositionModes.FixedToPoint;
		newPoint.point = atPoint;
		this._points.Add(newPoint);
		this.RecalculatePoints();
	}

	public void RemovePoint(CameraPathPoint newPoint)
	{
		this._points.Remove(newPoint);
		this.RecalculatePoints();
	}

	public void PathPointAddedEvent(CameraPathControlPoint addedPoint)
	{
		float percentage = addedPoint.percentage;
		for (int i = 0; i < this.realNumberOfPoints; i++)
		{
			CameraPathPoint cameraPathPoint = this._points.get_Item(i);
			if (cameraPathPoint.positionModes == CameraPathPoint.PositionModes.Free)
			{
				float percentage2 = cameraPathPoint.cpointA.percentage;
				float percentage3 = cameraPathPoint.cpointB.percentage;
				if (percentage > percentage2 && percentage < percentage3)
				{
					if (percentage < cameraPathPoint.percent)
					{
						cameraPathPoint.cpointA = addedPoint;
					}
					else
					{
						cameraPathPoint.cpointB = addedPoint;
					}
					this.cameraPath.GetCurvePercentage(cameraPathPoint);
				}
			}
		}
	}

	public void PathPointRemovedEvent(CameraPathControlPoint removedPathPoint)
	{
		for (int i = 0; i < this.realNumberOfPoints; i++)
		{
			CameraPathPoint cameraPathPoint = this._points.get_Item(i);
			switch (cameraPathPoint.positionModes)
			{
			case CameraPathPoint.PositionModes.Free:
				if (cameraPathPoint.cpointA == removedPathPoint)
				{
					CameraPathControlPoint point = this.cameraPath.GetPoint(removedPathPoint.index - 1);
					cameraPathPoint.cpointA = point;
					this.cameraPath.GetCurvePercentage(cameraPathPoint);
				}
				if (cameraPathPoint.cpointB == removedPathPoint)
				{
					CameraPathControlPoint point2 = this.cameraPath.GetPoint(removedPathPoint.index + 1);
					cameraPathPoint.cpointB = point2;
					this.cameraPath.GetCurvePercentage(cameraPathPoint);
				}
				break;
			case CameraPathPoint.PositionModes.FixedToPoint:
				if (cameraPathPoint.point == removedPathPoint)
				{
					this._points.Remove(cameraPathPoint);
					i--;
				}
				break;
			}
		}
		this.RecalculatePoints();
	}

	public void CheckPointCullEventFromStart(float percent)
	{
		int num = this._points.get_Count();
		for (int i = 0; i < num; i++)
		{
			CameraPathPoint cameraPathPoint = this._points.get_Item(i);
			if (cameraPathPoint.positionModes != CameraPathPoint.PositionModes.FixedToPercent)
			{
				if (cameraPathPoint.percent < percent)
				{
					this._points.Remove(cameraPathPoint);
					i--;
					num--;
				}
			}
		}
		this.RecalculatePoints();
	}

	public void CheckPointCullEventFromEnd(float percent)
	{
		int num = this._points.get_Count();
		for (int i = 0; i < num; i++)
		{
			CameraPathPoint cameraPathPoint = this._points.get_Item(i);
			if (cameraPathPoint.positionModes != CameraPathPoint.PositionModes.FixedToPercent)
			{
				if (cameraPathPoint.percent > percent)
				{
					this._points.Remove(cameraPathPoint);
					i--;
					num--;
				}
			}
		}
		this.RecalculatePoints();
	}

	protected int GetNextPointIndex(float percent)
	{
		if (this.realNumberOfPoints == 0)
		{
			Debug.LogError("No points to draw from");
		}
		if (percent == 0f)
		{
			return 1;
		}
		if (percent == 1f)
		{
			return this._points.get_Count() - 1;
		}
		int count = this._points.get_Count();
		int num = 0;
		for (int i = 1; i < count; i++)
		{
			if (this._points.get_Item(i).percent > percent)
			{
				return num + 1;
			}
			num = i;
		}
		return num;
	}

	protected int GetLastPointIndex(float percent)
	{
		if (this.realNumberOfPoints == 0)
		{
			Debug.LogError("No points to draw from");
		}
		if (percent == 0f)
		{
			return 0;
		}
		if (percent == 1f)
		{
			return (!this.cameraPath.loop && !this.cameraPath.shouldInterpolateNextPath) ? (this._points.get_Count() - 2) : (this._points.get_Count() - 1);
		}
		int count = this._points.get_Count();
		int result = 0;
		for (int i = 1; i < count; i++)
		{
			if (this._points.get_Item(i).percent > percent)
			{
				return result;
			}
			result = i;
		}
		return result;
	}

	public CameraPathPoint GetPoint(int index)
	{
		int count = this._points.get_Count();
		if (count == 0)
		{
			return null;
		}
		CameraPathPointList cameraPathPointList = this;
		if (this.cameraPath.shouldInterpolateNextPath)
		{
			string text = this.pointTypeName;
			if (text != null)
			{
				if (CameraPathPointList.<>f__switch$map11 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("Orientation", 0);
					dictionary.Add("FOV", 1);
					dictionary.Add("Tilt", 2);
					CameraPathPointList.<>f__switch$map11 = dictionary;
				}
				int num;
				if (CameraPathPointList.<>f__switch$map11.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						cameraPathPointList = this.cameraPath.nextPath.orientationList;
						break;
					case 1:
						cameraPathPointList = this.cameraPath.nextPath.fovList;
						break;
					case 2:
						cameraPathPointList = this.cameraPath.nextPath.tiltList;
						break;
					}
				}
			}
		}
		if (cameraPathPointList == this)
		{
			if (!this.cameraPath.loop)
			{
				return this._points.get_Item(Mathf.Clamp(index, 0, count - 1));
			}
			if (index >= count)
			{
				index -= count;
			}
			if (index < 0)
			{
				index += count;
			}
		}
		else if (this.cameraPath.loop)
		{
			if (index == count)
			{
				index = 0;
				cameraPathPointList = null;
			}
			else if (index > count)
			{
				index = Mathf.Clamp(index, 0, cameraPathPointList.realNumberOfPoints - 1);
			}
			else if (index < 0)
			{
				index += count;
				cameraPathPointList = null;
			}
			else
			{
				cameraPathPointList = null;
			}
		}
		else if (index > count - 1)
		{
			index = Mathf.Clamp(index - count, 0, cameraPathPointList.realNumberOfPoints - 1);
		}
		else if (index < 0)
		{
			index = 0;
			cameraPathPointList = null;
		}
		else
		{
			index = Mathf.Clamp(index, 0, count - 1);
			cameraPathPointList = null;
		}
		if (cameraPathPointList != null)
		{
			return cameraPathPointList[index];
		}
		return this._points.get_Item(index);
	}

	public CameraPathPoint GetPoint(CameraPathControlPoint atPoint)
	{
		if (this._points.get_Count() == 0)
		{
			return null;
		}
		using (List<CameraPathPoint>.Enumerator enumerator = this._points.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CameraPathPoint current = enumerator.get_Current();
				if (current.positionModes == CameraPathPoint.PositionModes.FixedToPoint && current.point == atPoint)
				{
					return current;
				}
			}
		}
		return null;
	}

	public void Clear()
	{
		this._points.Clear();
	}

	public CameraPathPoint DuplicatePointCheck()
	{
		using (List<CameraPathPoint>.Enumerator enumerator = this._points.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CameraPathPoint current = enumerator.get_Current();
				using (List<CameraPathPoint>.Enumerator enumerator2 = this._points.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						CameraPathPoint current2 = enumerator2.get_Current();
						if (current != current2 && current.percent == current2.percent)
						{
							return current;
						}
					}
				}
			}
		}
		return null;
	}

	protected virtual void RecalculatePoints()
	{
		if (this.cameraPath == null)
		{
			Debug.LogError("Camera Path Point List was not initialised - run Init();");
			return;
		}
		int count = this._points.get_Count();
		if (count == 0)
		{
			return;
		}
		List<CameraPathPoint> list = new List<CameraPathPoint>();
		for (int i = 0; i < count; i++)
		{
			if (!(this._points.get_Item(i) == null))
			{
				CameraPathPoint cameraPathPoint = this._points.get_Item(i);
				if (i == 0)
				{
					list.Add(cameraPathPoint);
				}
				else
				{
					bool flag = false;
					using (List<CameraPathPoint>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CameraPathPoint current = enumerator.get_Current();
							if (current.percent > cameraPathPoint.percent)
							{
								list.Insert(list.IndexOf(current), cameraPathPoint);
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						list.Add(cameraPathPoint);
					}
				}
			}
		}
		count = list.get_Count();
		this._points = list;
		for (int j = 0; j < count; j++)
		{
			CameraPathPoint cameraPathPoint2 = this._points.get_Item(j);
			cameraPathPoint2.givenName = this.pointTypeName + " Point " + j;
			cameraPathPoint2.fullName = string.Concat(new object[]
			{
				this.cameraPath.get_name(),
				" ",
				this.pointTypeName,
				" Point ",
				j
			});
			cameraPathPoint2.index = j;
			if (this.cameraPath.realNumberOfPoints >= 2)
			{
				switch (cameraPathPoint2.positionModes)
				{
				case CameraPathPoint.PositionModes.Free:
					if (cameraPathPoint2.cpointA == cameraPathPoint2.cpointB)
					{
						cameraPathPoint2.positionModes = CameraPathPoint.PositionModes.FixedToPoint;
						cameraPathPoint2.point = cameraPathPoint2.cpointA;
						cameraPathPoint2.cpointA = null;
						cameraPathPoint2.cpointB = null;
						cameraPathPoint2.percent = cameraPathPoint2.point.percentage;
						cameraPathPoint2.animationPercentage = ((!this.cameraPath.normalised) ? cameraPathPoint2.point.percentage : cameraPathPoint2.point.normalisedPercentage);
						cameraPathPoint2.worldPosition = cameraPathPoint2.point.worldPosition;
						return;
					}
					cameraPathPoint2.percent = this.cameraPath.GetPathPercentage(cameraPathPoint2.cpointA, cameraPathPoint2.cpointB, cameraPathPoint2.curvePercentage);
					cameraPathPoint2.animationPercentage = ((!this.cameraPath.normalised) ? cameraPathPoint2.percent : this.cameraPath.CalculateNormalisedPercentage(cameraPathPoint2.percent));
					cameraPathPoint2.worldPosition = this.cameraPath.GetPathPosition(cameraPathPoint2.percent, true);
					break;
				case CameraPathPoint.PositionModes.FixedToPoint:
					if (cameraPathPoint2.point == null)
					{
						cameraPathPoint2.point = this.cameraPath[this.cameraPath.GetNearestPointIndex(cameraPathPoint2.rawPercent)];
					}
					cameraPathPoint2.percent = cameraPathPoint2.point.percentage;
					cameraPathPoint2.animationPercentage = ((!this.cameraPath.normalised) ? cameraPathPoint2.point.percentage : cameraPathPoint2.point.normalisedPercentage);
					cameraPathPoint2.worldPosition = cameraPathPoint2.point.worldPosition;
					break;
				case CameraPathPoint.PositionModes.FixedToPercent:
					cameraPathPoint2.worldPosition = this.cameraPath.GetPathPosition(cameraPathPoint2.percent, true);
					cameraPathPoint2.animationPercentage = ((!this.cameraPath.normalised) ? cameraPathPoint2.percent : this.cameraPath.CalculateNormalisedPercentage(cameraPathPoint2.percent));
					break;
				}
			}
		}
	}

	public void ReassignCP(CameraPathControlPoint from, CameraPathControlPoint to)
	{
		using (List<CameraPathPoint>.Enumerator enumerator = this._points.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CameraPathPoint current = enumerator.get_Current();
				if (current.point == from)
				{
					current.point = to;
				}
				if (current.cpointA == from)
				{
					current.cpointA = to;
				}
				if (current.cpointB == from)
				{
					current.cpointB = to;
				}
			}
		}
	}

	protected void CheckListIsNull()
	{
		if (this._points == null)
		{
			this._points = new List<CameraPathPoint>();
		}
	}
}
