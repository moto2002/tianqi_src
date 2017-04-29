using System;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
	private void Update()
	{
		if (CamerasMgr.CameraMain != null && CamerasMgr.CameraMain.get_enabled())
		{
			base.get_transform().LookAt(base.get_transform().get_position() + CamerasMgr.CameraMain.get_transform().get_rotation() * Vector3.get_back(), CamerasMgr.CameraMain.get_transform().get_rotation() * Vector3.get_up());
		}
	}
}
