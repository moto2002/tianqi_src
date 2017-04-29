using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathEventList : CameraPathPointList
{
	public delegate void CameraPathEventPointHandler(string name);

	private float _lastPercentage;

	public event CameraPathEventList.CameraPathEventPointHandler CameraPathEventPoint
	{
		[MethodImpl(32)]
		add
		{
			this.CameraPathEventPoint = (CameraPathEventList.CameraPathEventPointHandler)Delegate.Combine(this.CameraPathEventPoint, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.CameraPathEventPoint = (CameraPathEventList.CameraPathEventPointHandler)Delegate.Remove(this.CameraPathEventPoint, value);
		}
	}

	public CameraPathEvent this[int index]
	{
		get
		{
			return (CameraPathEvent)base[index];
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
		this.pointTypeName = "Event";
		base.Init(_cameraPath);
	}

	public void AddEvent(CameraPathControlPoint atPoint)
	{
		CameraPathEvent cameraPathEvent = base.get_gameObject().AddComponent<CameraPathEvent>();
		cameraPathEvent.set_hideFlags(2);
		base.AddPoint(cameraPathEvent, atPoint);
		this.RecalculatePoints();
	}

	public CameraPathEvent AddEvent(CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage)
	{
		CameraPathEvent cameraPathEvent = base.get_gameObject().AddComponent<CameraPathEvent>();
		cameraPathEvent.set_hideFlags(2);
		base.AddPoint(cameraPathEvent, curvePointA, curvePointB, curvePercetage);
		this.RecalculatePoints();
		return cameraPathEvent;
	}

	public void OnAnimationStart(float startPercentage)
	{
		this._lastPercentage = startPercentage;
	}

	public void CheckEvents(float percentage)
	{
		if (Mathf.Abs(percentage - this._lastPercentage) > 0.5f)
		{
			this._lastPercentage = percentage;
			return;
		}
		for (int i = 0; i < base.realNumberOfPoints; i++)
		{
			CameraPathEvent cameraPathEvent = this[i];
			bool flag = (cameraPathEvent.percent >= this._lastPercentage && cameraPathEvent.percent <= percentage) || (cameraPathEvent.percent >= percentage && cameraPathEvent.percent <= this._lastPercentage);
			if (flag)
			{
				CameraPathEvent.Types type = cameraPathEvent.type;
				if (type != CameraPathEvent.Types.Broadcast)
				{
					if (type == CameraPathEvent.Types.Call)
					{
						this.Call(cameraPathEvent);
					}
				}
				else
				{
					this.BroadCast(cameraPathEvent);
				}
			}
		}
		this._lastPercentage = percentage;
	}

	public void BroadCast(CameraPathEvent eventPoint)
	{
		if (this.CameraPathEventPoint != null)
		{
			this.CameraPathEventPoint(eventPoint.eventName);
		}
	}

	public void Call(CameraPathEvent eventPoint)
	{
		if (eventPoint.target == null)
		{
			Debug.LogError("Camera Path Event Error: There is an event call without a specified target. Please check " + eventPoint.displayName, this.cameraPath);
			return;
		}
		switch (eventPoint.argumentType)
		{
		case CameraPathEvent.ArgumentTypes.None:
			eventPoint.target.SendMessage(eventPoint.methodName, 1);
			break;
		case CameraPathEvent.ArgumentTypes.Float:
		{
			float num = float.Parse(eventPoint.methodArgument);
			if (float.IsNaN(num))
			{
				Debug.LogError("Camera Path Aniamtor: The argument specified is not a float");
			}
			eventPoint.target.SendMessage(eventPoint.methodName, num, 1);
			break;
		}
		case CameraPathEvent.ArgumentTypes.Int:
		{
			int num2;
			if (int.TryParse(eventPoint.methodArgument, ref num2))
			{
				eventPoint.target.SendMessage(eventPoint.methodName, num2, 1);
			}
			else
			{
				Debug.LogError("Camera Path Aniamtor: The argument specified is not an integer");
			}
			break;
		}
		case CameraPathEvent.ArgumentTypes.String:
		{
			string methodArgument = eventPoint.methodArgument;
			eventPoint.target.SendMessage(eventPoint.methodName, methodArgument, 1);
			break;
		}
		}
	}
}
