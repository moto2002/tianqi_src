using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NcSafeTool : MonoBehaviour
{
	public static bool m_bShuttingDown;

	public static bool m_bLoadLevel;

	private static NcSafeTool s_Instance;

	private static void Instance()
	{
		if (NcSafeTool.s_Instance == null)
		{
			GameObject gameObject = GameObject.Find("_GlobalManager");
			if (gameObject == null)
			{
				gameObject = new GameObject("_GlobalManager");
			}
			else
			{
				NcSafeTool.s_Instance = (NcSafeTool)gameObject.GetComponent(typeof(NcSafeTool));
			}
			if (NcSafeTool.s_Instance == null)
			{
				NcSafeTool.s_Instance = (NcSafeTool)gameObject.AddComponent(typeof(NcSafeTool));
			}
		}
	}

	public static bool IsSafe()
	{
		return !NcSafeTool.m_bShuttingDown && !NcSafeTool.m_bLoadLevel;
	}

	public static Object SafeInstantiate(Object original)
	{
		if (NcSafeTool.m_bShuttingDown)
		{
			return null;
		}
		if (NcSafeTool.s_Instance == null)
		{
			NcSafeTool.Instance();
		}
		return Object.Instantiate(original);
	}

	public static Object SafeInstantiate(Object original, Vector3 position, Quaternion rotation)
	{
		if (NcSafeTool.m_bShuttingDown)
		{
			return null;
		}
		if (NcSafeTool.s_Instance == null)
		{
			NcSafeTool.Instance();
		}
		return Object.Instantiate(original, position, rotation);
	}

	public static void LoadLevel(int nLoadLevel)
	{
		if (NcSafeTool.m_bShuttingDown)
		{
			return;
		}
		if (NcSafeTool.s_Instance == null)
		{
			NcSafeTool.Instance();
		}
		NcSafeTool.m_bLoadLevel = true;
		Debuger.Info("Safe LoadLevel start " + nLoadLevel, new object[0]);
		SceneManager.LoadScene(nLoadLevel);
		Debuger.Info("Safe LoadLevel end", new object[0]);
		NcSafeTool.m_bLoadLevel = false;
	}

	public void OnApplicationQuit()
	{
		NcSafeTool.m_bShuttingDown = true;
	}
}
