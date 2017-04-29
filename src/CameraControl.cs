using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	private string m_input = string.Empty;

	private float m_fdistance = 1f;

	private float m_fangle = 1f;

	private void OnGUI()
	{
		if (!SceneStats.IsCameraControllUI)
		{
			return;
		}
		int num = -1;
		num++;
		this.m_input = GUI.TextField(this.GetRect(num), this.m_input);
		num++;
		if (GUI.Button(this.GetRect(num), "SetDistance"))
		{
			this.m_fdistance = float.Parse(this.m_input);
			this.m_input = string.Empty;
		}
		num++;
		if (GUI.Button(this.GetRect(num), "SetAngle"))
		{
			this.m_fangle = float.Parse(this.m_input);
			this.m_input = string.Empty;
		}
		num++;
		if (GUI.Button(this.GetRect(num), "SetFiledOfView"))
		{
			SceneStats.cameraController.set_fieldOfView(float.Parse(this.m_input));
			this.m_input = string.Empty;
		}
		num++;
		if (GUI.Button(this.GetRect(num), "X-"))
		{
			SceneStats.cameraController.get_transform().set_localPosition(new Vector3(SceneStats.cameraController.get_transform().get_localPosition().x - this.m_fdistance, SceneStats.cameraController.get_transform().get_localPosition().y, SceneStats.cameraController.get_transform().get_localPosition().z));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "X+"))
		{
			SceneStats.cameraController.get_transform().set_localPosition(new Vector3(SceneStats.cameraController.get_transform().get_localPosition().x + this.m_fdistance, SceneStats.cameraController.get_transform().get_localPosition().y, SceneStats.cameraController.get_transform().get_localPosition().z));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Y-"))
		{
			SceneStats.cameraController.get_transform().set_localPosition(new Vector3(SceneStats.cameraController.get_transform().get_localPosition().x, SceneStats.cameraController.get_transform().get_localPosition().y - this.m_fdistance, SceneStats.cameraController.get_transform().get_localPosition().z));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Y+"))
		{
			SceneStats.cameraController.get_transform().set_localPosition(new Vector3(SceneStats.cameraController.get_transform().get_localPosition().x, SceneStats.cameraController.get_transform().get_localPosition().y + this.m_fdistance, SceneStats.cameraController.get_transform().get_localPosition().z));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Z-"))
		{
			SceneStats.cameraController.get_transform().set_localPosition(new Vector3(SceneStats.cameraController.get_transform().get_localPosition().x, SceneStats.cameraController.get_transform().get_localPosition().y, SceneStats.cameraController.get_transform().get_localPosition().z - this.m_fdistance));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "Z+"))
		{
			SceneStats.cameraController.get_transform().set_localPosition(new Vector3(SceneStats.cameraController.get_transform().get_localPosition().x, SceneStats.cameraController.get_transform().get_localPosition().y, SceneStats.cameraController.get_transform().get_localPosition().z + this.m_fdistance));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "RX-"))
		{
			SceneStats.cameraController.get_transform().set_localEulerAngles(new Vector3(SceneStats.cameraController.get_transform().get_localEulerAngles().x - this.m_fangle, SceneStats.cameraController.get_transform().get_localEulerAngles().y, SceneStats.cameraController.get_transform().get_localEulerAngles().z));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "RX+"))
		{
			SceneStats.cameraController.get_transform().set_localEulerAngles(new Vector3(SceneStats.cameraController.get_transform().get_localEulerAngles().x + this.m_fangle, SceneStats.cameraController.get_transform().get_localEulerAngles().y, SceneStats.cameraController.get_transform().get_localEulerAngles().z));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "RY-"))
		{
			SceneStats.cameraController.get_transform().set_localEulerAngles(new Vector3(SceneStats.cameraController.get_transform().get_localEulerAngles().x, SceneStats.cameraController.get_transform().get_localEulerAngles().y - this.m_fangle, SceneStats.cameraController.get_transform().get_localEulerAngles().z));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "RY+"))
		{
			SceneStats.cameraController.get_transform().set_localEulerAngles(new Vector3(SceneStats.cameraController.get_transform().get_localEulerAngles().x, SceneStats.cameraController.get_transform().get_localEulerAngles().y + this.m_fangle, SceneStats.cameraController.get_transform().get_localEulerAngles().z));
		}
	}

	private Rect GetRect(int index)
	{
		return new Rect((float)(20 + 160 * (index % 2)), (float)(150 + 50 * (index / 2)), 150f, 45f);
	}
}
