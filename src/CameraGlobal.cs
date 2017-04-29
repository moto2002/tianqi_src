using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class CameraGlobal
{
	public static bool isAreaPointBActive;

	public static Vector3 areaPointB;

	public static CameraType cameraType;

	private static Object cameraAreaLast;

	public static bool IsPlayerRole(Transform transform)
	{
		return transform == EntityWorld.Instance.TraSelf;
	}

	public static List<float> GetMapPointB()
	{
		if (MySceneManager.IsMainScene(MySceneManager.Instance.CurSceneID))
		{
			return null;
		}
		if (InstanceManager.CurrentInstanceType == InstanceType.Arena)
		{
			return null;
		}
		if (InstanceManager.CurrentInstanceType == InstanceType.GangFight)
		{
			return null;
		}
		if (InstanceManager.CurrentInstanceType == InstanceType.ClientTest)
		{
			return null;
		}
		if (InstanceManager.CurrentInstanceType == InstanceType.ServerTest)
		{
			return null;
		}
		if (InstanceManager.CurrentInstanceType == InstanceType.SurvivalChallenge)
		{
			return null;
		}
		string text = InstanceManager.CurrentInstanceDataID.ToString() + InstanceManager.CurrentInstanceBatch.ToString();
		int num = int.Parse(text);
		BoCiBiao boCiBiao = null;
		if (DataReader<BoCiBiao>.Contains(num))
		{
			boCiBiao = DataReader<BoCiBiao>.Get(num);
		}
		else if (DataReader<BoCiBiao>.Contains(num - 1))
		{
			boCiBiao = DataReader<BoCiBiao>.Get(num - 1);
		}
		return (boCiBiao == null || boCiBiao.pointB.get_Count() != 3) ? null : boCiBiao.pointB;
	}

	public static bool IsCanSwitchToBossCameraType()
	{
		string text = InstanceManager.CurrentInstanceDataID.ToString() + InstanceManager.CurrentInstanceBatch.ToString();
		int key = int.Parse(text);
		BoCiBiao boCiBiao = DataReader<BoCiBiao>.Get(key);
		return boCiBiao != null && boCiBiao.cameraType == 1;
	}

	private static void LoadCameraArea(int sceneId)
	{
		if (CameraGlobal.cameraAreaLast)
		{
			Object.Destroy(CameraGlobal.cameraAreaLast);
		}
		Scene scene = DataReader<Scene>.Get(sceneId);
		if (scene == null)
		{
			return;
		}
		if (scene.CameraPath == null)
		{
			return;
		}
		Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(scene.CameraPath, typeof(Object));
		if (@object != null)
		{
			CameraGlobal.cameraAreaLast = Object.Instantiate(@object, Vector3.get_zero(), Quaternion.get_identity());
		}
		else
		{
			Debug.LogError("asset is null, path is " + scene.CameraPath);
		}
	}

	public static void DestroyCamera()
	{
		if (FollowCamera.instance != null)
		{
			FollowCamera.instance.RemoveListeners();
			Object.Destroy(FollowCamera.instance);
			FollowCamera.instance = null;
		}
	}

	public static void CreateCamera()
	{
		CameraGlobal.LoadCameraArea(MySceneManager.Instance.CurSceneID);
		CameraGlobal.cameraType = CameraType.Follow;
		CamerasMgr.MainCameraRoot.get_gameObject().AddUniqueComponent<FollowCamera>().OnResetCamera();
		EventDispatcher.Broadcast(CameraEvent.CameraInited);
		CamerasMgr.MainCameraRoot.get_gameObject().AddUniqueComponent<CameraRevolve>();
	}
}
