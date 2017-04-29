using System;
using UnityEngine;

namespace XEngine
{
	public static class XEngineCore
	{
		private class XEngineCoreBehaviour : MonoBehaviour
		{
			private void Awake()
			{
				Object.DontDestroyOnLoad(base.get_gameObject());
			}
		}

		private static XEngineCore.XEngineCoreBehaviour core = new GameObject("XEngineCore").AddUniqueComponent<XEngineCore.XEngineCoreBehaviour>();

		public static T AddPermanentGameObject<T>(string name) where T : Component
		{
			GameObject gameObject = new GameObject(name);
			gameObject.get_transform().set_parent(XEngineCore.core.get_transform());
			return gameObject.AddUniqueComponent<T>();
		}
	}
}
