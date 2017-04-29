using System;
using UnityEngine;

namespace LuaFramework
{
	public class Main : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod]
		public static void Initialize()
		{
			GameObject gameObject = GameObject.Find("UGUI Root");
			if (gameObject == null)
			{
				return;
			}
			Object.DontDestroyOnLoad(new GameObject("GameManager", new Type[]
			{
				typeof(Main)
			}));
			Debug.Log("RuntimeInitializeOnLoadMethod");
			AppFacade.Instance.StartUp();
		}
	}
}
