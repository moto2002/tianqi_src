using System;
using UnityEngine;

public class PPToSelectRoleSetting
{
	private static Camera focusCamera;

	public static void SelectRoleSettingOn()
	{
		if (PPToSelectRoleSetting.focusCamera == null)
		{
			GameObject gameObject = new GameObject();
			gameObject.set_name("FocusCamera");
			gameObject.get_transform().set_parent(CamerasMgr.MainCameraRoot.get_transform());
			gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
			gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
			gameObject.get_transform().set_localRotation(Quaternion.get_identity());
			PPToSelectRoleSetting.focusCamera = gameObject.AddComponent<Camera>();
			PPToSelectRoleSetting.focusCamera.set_cullingMask(LayerSystem.GetMask(Utils.no_camerarange_layers));
			PPToSelectRoleSetting.focusCamera.set_clearFlags(3);
			PPToSelectRoleSetting.focusCamera.set_orthographic(false);
			PPToSelectRoleSetting.focusCamera.set_farClipPlane(CamerasMgr.CameraMain.get_farClipPlane());
			PPToSelectRoleSetting.focusCamera.set_nearClipPlane(CamerasMgr.CameraMain.get_nearClipPlane());
			PPToSelectRoleSetting.focusCamera.set_fieldOfView(CamerasMgr.CameraMain.get_fieldOfView());
			PPToSelectRoleSetting.focusCamera.set_depth(CamerasMgr.CameraMain.get_depth() + 1f);
			PPToSelectRoleSetting.focusCamera.set_useOcclusionCulling(false);
			PPToSelectRoleSetting.focusCamera.set_enabled(true);
		}
		CamerasMgr.CameraMain.set_cullingMask(Utils.GetCullingMask(8));
	}

	public static void SelectRoleSettingOff()
	{
		if (PPToSelectRoleSetting.focusCamera != null && PPToSelectRoleSetting.focusCamera.get_gameObject() != null)
		{
			Object.Destroy(PPToSelectRoleSetting.focusCamera.get_gameObject());
		}
		Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 1);
	}
}
