using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathOrientation : CameraPathPoint
{
	public Quaternion rotation = Quaternion.get_identity();

	public Transform lookAt;

	private void OnEnable()
	{
		base.set_hideFlags(2);
	}
}
