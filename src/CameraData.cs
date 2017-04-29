using System;
using UnityEngine;

public class CameraData : ScriptableObject
{
	public Vector3 pos;

	public Vector3 rot;

	public bool useFromPrefab = true;

	public bool appendToCurrentMode;

	public int type;

	public bool useY = true;
}
