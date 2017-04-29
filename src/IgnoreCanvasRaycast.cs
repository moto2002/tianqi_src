using System;
using UnityEngine;

public class IgnoreCanvasRaycast : MonoBehaviour, ICanvasRaycastFilter
{
	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		return false;
	}
}
