using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStats : MonoBehaviour
{
	public static bool IsSceneUI;

	public static bool IsCameraControllUI;

	public static bool IsFXUI;

	private bool fps;

	private string m_sceneInput = string.Empty;

	public static Camera cameraController;

	private string m_fxInput = string.Empty;

	private void Awake()
	{
		Object.DontDestroyOnLoad(this);
	}

	private void OnGUI()
	{
		int num = -1;
		num++;
		if (GUI.Button(this.GetRectOfSwitch(num), "SceneUI"))
		{
			SceneStats.IsSceneUI = !SceneStats.IsSceneUI;
			if (SceneStats.IsSceneUI && SceneStats.cameraController == null)
			{
				this.CreateCamera();
				SceneStats.cameraController.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
			}
		}
		num++;
		if (GUI.Button(this.GetRectOfSwitch(num), "CameraUI"))
		{
			SceneStats.IsCameraControllUI = !SceneStats.IsCameraControllUI;
		}
		num++;
		if (GUI.Button(this.GetRectOfSwitch(num), "FXUI"))
		{
			SceneStats.IsFXUI = !SceneStats.IsFXUI;
			if (SceneStats.IsFXUI && SceneStats.cameraController == null)
			{
				this.CreateCamera();
				SceneStats.cameraController.get_transform().set_localPosition(new Vector3(0f, 0f, -30f));
			}
		}
		this.SceneUI();
		this.FXUI();
	}

	private void SceneUI()
	{
		if (!SceneStats.IsSceneUI)
		{
			return;
		}
		int num = -1;
		num++;
		this.m_sceneInput = GUI.TextField(this.GetRectOfChild(num), this.m_sceneInput);
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "LoadScene"))
		{
			SceneManager.LoadScene(this.m_sceneInput.Trim());
		}
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "CreateCamera"))
		{
			this.CreateCamera();
		}
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "FPS"))
		{
			this.fps = !this.fps;
			this.SetFPS(this.fps);
		}
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "FPS_X"))
		{
			float num2 = float.Parse(this.m_sceneInput);
			UIUtils.AdvancedFPS.set_localPosition(new Vector3(num2, UIUtils.AdvancedFPS.get_localPosition().y, 0f));
		}
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "FPS_Y"))
		{
			float num3 = float.Parse(this.m_sceneInput);
			UIUtils.AdvancedFPS.set_localPosition(new Vector3(UIUtils.AdvancedFPS.get_localPosition().x, num3, 0f));
		}
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "Frame"))
		{
			FPSManager.Instance.FPSLimitOff();
			FPSManager.Instance.vSyncOff();
		}
	}

	private void CreateCamera()
	{
		if (SceneStats.cameraController == null)
		{
			GameObject gameObject = new GameObject("CameraController");
			Object.DontDestroyOnLoad(gameObject);
			gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
			gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
			gameObject.get_transform().set_localRotation(Quaternion.get_identity());
			gameObject.AddComponent<CameraControl>();
			SceneStats.cameraController = gameObject.AddComponent<Camera>();
			SceneStats.cameraController.get_gameObject().AddComponent<GUILayer>();
			SceneStats.cameraController.set_clearFlags(1);
			SceneStats.cameraController.set_backgroundColor(new Color(0f, 0f, 0f, 0f));
			SceneStats.cameraController.set_orthographic(false);
			SceneStats.cameraController.set_farClipPlane(10000f);
			SceneStats.cameraController.set_nearClipPlane(0.3f);
			SceneStats.cameraController.set_fieldOfView(60f);
			SceneStats.cameraController.set_depth(1f);
		}
	}

	private void SetFPS(bool on)
	{
		UIUtils.AdvancedFPS.set_localPosition(new Vector3(0f, 0f, 0f));
		UIUtils.AdvancedFPS.get_gameObject().SetActive(on);
		Object.DontDestroyOnLoad(UIUtils.AdvancedFPS);
	}

	private void FXUI()
	{
		if (!SceneStats.IsFXUI)
		{
			return;
		}
		int num = -1;
		num++;
		this.m_fxInput = GUI.TextField(this.GetRectOfChild(num), this.m_fxInput);
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "PlayFX"))
		{
			int templateId = 0;
			if (!string.IsNullOrEmpty(this.m_fxInput))
			{
				templateId = int.Parse(this.m_fxInput);
			}
			FXManager.Instance.PlayFXOfDisplay(templateId, null, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, null, null);
		}
		num++;
		if (GUI.Button(this.GetRectOfChild(num), "ResetCamera"))
		{
			SceneStats.cameraController.get_transform().set_localPosition(new Vector3(0f, 0f, -30f));
		}
	}

	private Rect GetRectOfChild(int index)
	{
		return new Rect((float)(20 + 160 * (index % 2)), (float)(150 + 50 * (index / 2)), 150f, 45f);
	}

	private Rect GetRectOfSwitch(int index)
	{
		return new Rect((float)(20 + 160 * index), 0f, 150f, 55f);
	}
}
