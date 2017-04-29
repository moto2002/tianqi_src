using System;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public static class AssetLoader
	{
		private static readonly DynamicLoader m_dynamicLoader = new DynamicLoader();

		private static readonly StaticLoader m_staticLoader = new StaticLoader();

		private static readonly SceneLoader m_sceneLoader = new SceneLoader();

		public static bool useDynamicLoader = false;

		private static ILoadAsset GetCurrentLoader()
		{
			if (AssetLoader.useDynamicLoader)
			{
				return AssetLoader.m_dynamicLoader;
			}
			return AssetLoader.m_staticLoader;
		}

		public static Object LoadAssetNow(string resName, Type type)
		{
			return AssetLoader.GetCurrentLoader().SyncLoadAssetWithType(resName, type);
		}

		public static void LoadAsset(string resName, Type type, AssetCallback callback)
		{
			AssetLoader.GetCurrentLoader().AsyncLoadAssetWithType(resName, type, callback);
		}

		public static void UnloadAsset(string resName, Action<bool> callback)
		{
			AssetLoader.GetCurrentLoader().UnloadAsset(resName, callback, null);
		}

		public static void UnloadUnusedAssets(Action callback)
		{
			AssetLoader.GetCurrentLoader().UnloadUnusedAssets(callback);
		}

		public static void LoadScene(string resName, Action<bool> callback, bool isAdditive = false, Action<float> loadingPercent = null)
		{
			if (AssetLoader.useDynamicLoader)
			{
				Debug.Log("LoadScene: " + resName);
				AssetBundleLoader.Instance.AsyncLoadAB(resName, delegate(bool isSuccess)
				{
					if (isSuccess)
					{
						AssetLoader.m_sceneLoader.AsyncLoadScene(resName, callback, isAdditive);
					}
					else
					{
						Debug.LogError("加载AB失败 " + resName);
						callback.Invoke(false);
					}
				}, loadingPercent);
			}
			else
			{
				AssetLoader.m_sceneLoader.AsyncLoadScene(resName, callback, isAdditive);
			}
		}

		public static void UnloadScene(string resName, Action<bool> callback, bool isAdditive = false, Action<float> loadingPercent = null)
		{
			AssetLoader.m_sceneLoader.UnloadScene(resName, isAdditive);
			AssetLoader.GetCurrentLoader().UnloadAsset(resName, callback, loadingPercent);
		}

		public static void ReleaseLoader()
		{
			if (AssetLoader.m_dynamicLoader != null)
			{
				AssetLoader.m_dynamicLoader.Release();
			}
		}
	}
}
