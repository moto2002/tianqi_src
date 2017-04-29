using System;
using System.Collections;
using UnityEngine;

public class XTaskManager : MonoBehaviour
{
	private static bool IsInit;

	private static XTaskManager _instance;

	public static XTaskManager instance
	{
		get
		{
			XTaskManager.Init();
			return XTaskManager._instance;
		}
	}

	private static void Init()
	{
		if (!XTaskManager.IsInit)
		{
			XTaskManager.IsInit = true;
			XTaskManager._instance = (Object.FindObjectOfType(typeof(XTaskManager)) as XTaskManager);
			if (!XTaskManager._instance)
			{
				GameObject gameObject = new GameObject("XTaskManager");
				Object.DontDestroyOnLoad(gameObject);
				XTaskManager._instance = gameObject.AddComponent<XTaskManager>();
			}
			else
			{
				Object.DontDestroyOnLoad(XTaskManager._instance);
			}
		}
	}

	private void OnApplicationQuit()
	{
		XTaskManager._instance = null;
	}

	public XTask StartTask(IEnumerator routine, Action<bool> callback)
	{
		XTask xTask = new XTask(routine);
		xTask.Start();
		xTask.Finished = callback;
		return xTask;
	}
}
