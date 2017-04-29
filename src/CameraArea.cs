using System;
using UnityEngine;

public class CameraArea : MonoBehaviour
{
	public Vector3 pointB;

	public bool isClearWhenExit;

	private void OnTriggerEnter(Collider other)
	{
		if (!CameraGlobal.IsPlayerRole(other.get_transform()))
		{
			return;
		}
		CameraGlobal.isAreaPointBActive = true;
		CameraGlobal.areaPointB = this.pointB;
	}

	private void OnTriggerExit(Collider other)
	{
		if (!CameraGlobal.IsPlayerRole(other.get_transform()))
		{
			return;
		}
		if (this.isClearWhenExit)
		{
			CameraGlobal.isAreaPointBActive = false;
			CameraGlobal.areaPointB = this.pointB;
		}
	}
}
