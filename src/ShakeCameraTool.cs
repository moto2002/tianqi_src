using GameData;
using System;
using UnityEngine;

public class ShakeCameraTool : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnMoveCamera(int id)
	{
		Debuger.Info("---->?", new object[0]);
		CameraAnimation cameraData = DataReader<CameraAnimation>.Get(id);
		if (CameraMove.intance != null)
		{
			CameraMove.intance.StartShake(cameraData);
		}
	}

	private void OnShakeCamera(int id)
	{
		Debuger.Info("---->", new object[0]);
		if (ShakeCamera.instance != null)
		{
			ShakeCamera.instance.HandleCameraEffect(id);
		}
	}
}
