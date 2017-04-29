using GameData;
using System;
using UnityEngine;

public class CameraSetting
{
	public static bool EnableCameraRange = true;

	public CameraSetting()
	{
		this.AddListeners();
	}

	private void AddListeners()
	{
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.OnLoadSceneEnd));
	}

	private void OnLoadSceneEnd(int sceneId)
	{
		CameraSetting.SettingCamera(CamerasMgr.CameraMain, sceneId);
		this.ResetCameraComponent(sceneId);
	}

	private void ResetCameraComponent(int nextId)
	{
		if (CameraSetting.EnableCameraRange)
		{
			CamerasMgr.MainCameraRoot.get_gameObject().AddMissingComponent<CameraRange>();
		}
		if (DataReader<Scene>.Get(nextId) == null)
		{
			return;
		}
		if (MySceneManager.Instance.CurSceneID.Equals("10125"))
		{
			if (CameraMove.intance == null)
			{
				CamerasMgr.MainCameraRoot.get_gameObject().AddComponent<CameraMove>();
			}
			CameraMove.intance.set_enabled(true);
		}
		else if (CameraMove.intance != null)
		{
			CameraMove.intance.Destroy();
		}
	}

	public static void SettingCamera(Camera camera, int sceneID)
	{
		Scene scene = DataReader<Scene>.Get(sceneID);
		if (scene == null)
		{
			return;
		}
		string[] array = scene.clip.Split(";".ToCharArray());
		if (array.Length >= 2)
		{
			CamerasMgr.CameraMain.set_nearClipPlane(float.Parse(array[0]));
			CamerasMgr.CameraMain.set_farClipPlane(float.Parse(array[1]));
		}
		CamerasMgr.CameraMain.set_fieldOfView((float)scene.CameraWideAngle);
		CameraSetting.SetCameraLayerCullDistances(scene);
	}

	private static void SetCameraLayerCullDistances(Scene dataScene)
	{
		float[] array = new float[32];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (float)dataScene.visualField;
		}
		if (GameLevelManager.GameLevelVariable.GetRealLODLEVEL() == 200 || GameLevelManager.GameLevelVariable.GetRealLODLEVEL() == 250)
		{
			array[28] = 40f;
			array[29] = 80f;
		}
		else
		{
			array[28] = 60f;
			array[29] = 100f;
		}
		CamerasMgr.CameraMain.set_layerCullDistances(array);
		CamerasMgr.CameraMain.set_layerCullSpherical(true);
	}

	public static void InitManagers()
	{
		CameraSetting.InitQualityMgr();
		CameraSetting.InitOcclusionComponent();
	}

	private static void InitOcclusionComponent()
	{
		if (SystemConfig.IsFindOcclusionByCameraOn)
		{
			CamerasMgr.MainCameraRoot.get_gameObject().AddComponent<FindOcclusionByCamera>();
		}
	}

	private static void InitQualityMgr()
	{
		ClientApp.Instance.get_gameObject().AddComponent<PostProcessManager>();
		ClientApp.Instance.get_gameObject().AddComponent<RTManager>();
		ClientApp.Instance.get_gameObject().AddComponent<RTManagerOfPostProcess>();
	}
}
