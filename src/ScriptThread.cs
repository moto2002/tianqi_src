using System;
using System.Collections;
using UnityEngine;

public class ScriptThread : MonoBehaviour
{
	private static ScriptThread instance;

	private static GameObject InstanceObj;

	public static ScriptThread Instance
	{
		get
		{
			if (ScriptThread.instance == null)
			{
				ScriptThread.CreateInstance();
			}
			return ScriptThread.instance;
		}
	}

	public static ScriptThread CreateInstance()
	{
		if (ScriptThread.instance == null)
		{
			ScriptThread.InstanceObj = new GameObject(typeof(ScriptThread).get_Name());
			ScriptThread.instance = ScriptThread.InstanceObj.AddComponent<ScriptThread>();
		}
		return ScriptThread.instance;
	}

	private void Awake()
	{
		if (ScriptThread.instance != null)
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			Object.DontDestroyOnLoad(this);
			ScriptThread.instance = this;
		}
	}

	public static Coroutine Start(IEnumerator routine)
	{
		return ScriptThread.Instance.StartCoroutine(routine);
	}

	public static void Stop(IEnumerator routine)
	{
		ScriptThread.Instance.StopCoroutine(routine);
	}

	public static void StopAll()
	{
		ScriptThread.Instance.StopAllCoroutines();
	}
}
