using System;
using UnityEngine;

public class TestLoadCreateRole : MonoBehaviour
{
	private string nameText = string.Empty;

	private void OnGUI()
	{
		this.nameText = GUI.TextField(new Rect(10f, 10f, 300f, 50f), this.nameText);
		if (GUI.Button(new Rect(320f, 10f, 50f, 50f), "加载"))
		{
			CamerasMgr.CameraUI.set_enabled(false);
			GameObject gameObject = Object.Instantiate(Resources.Load(this.nameText)) as GameObject;
			gameObject.get_transform().set_position(Vector3.get_zero());
			gameObject.get_transform().set_rotation(Quaternion.get_identity());
		}
		if (GUI.Button(new Rect(380f, 10f, 50f, 50f), "加载2"))
		{
			CamerasMgr.CameraMain.set_enabled(false);
			CamerasMgr.CameraUI.set_enabled(false);
			GameObject gameObject2 = Object.Instantiate(Resources.Load(this.nameText)) as GameObject;
			gameObject2.get_transform().set_position(Vector3.get_zero());
			gameObject2.get_transform().set_rotation(Quaternion.get_identity());
		}
		if (GUI.Button(new Rect(440f, 10f, 100f, 50f), "Rolecreate"))
		{
			CamerasMgr.CameraUI.set_enabled(false);
			CamerasMgr.CameraMain.set_enabled(false);
		}
	}
}
