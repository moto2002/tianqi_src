using System;
using UnityEngine;

public class CameraPathEvent : CameraPathPoint
{
	public enum Types
	{
		Broadcast,
		Call
	}

	public enum ArgumentTypes
	{
		None,
		Float,
		Int,
		String
	}

	public CameraPathEvent.Types type;

	public string eventName = "Camera Path Event";

	public GameObject target;

	public string methodName;

	public string methodArgument;

	public CameraPathEvent.ArgumentTypes argumentType;
}
