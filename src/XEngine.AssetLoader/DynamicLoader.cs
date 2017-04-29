using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace XEngine.AssetLoader
{
	internal class DynamicLoader : ILoadAsset
	{
		private SingleLoader coroutineQueue;

		public void AsyncLoadAssetWithType(string resName, Type type, AssetCallback callback)
		{
			if (resName.Contains("Scenes/s") && Path.GetDirectoryName(resName) == "Scenes")
			{
				if (callback != null)
				{
					callback(null);
				}
			}
			else
			{
				AssetBundleLoader.Instance.AsyncLoadAB(resName, delegate(bool isSuccess)
				{
					if (this.coroutineQueue == null)
					{
						this.coroutineQueue = new SingleLoader(new Func<IEnumerator, Coroutine>(GameManager.Instance.StartCoroutine));
					}
					this.coroutineQueue.Enqueue(this.AsyncOnLoadAsset(resName, type, callback));
				}, null);
			}
		}

		[DebuggerHidden]
		private IEnumerator AsyncOnLoadAsset(string resName, Type type, AssetCallback callback)
		{
			DynamicLoader.<AsyncOnLoadAsset>c__Iterator20 <AsyncOnLoadAsset>c__Iterator = new DynamicLoader.<AsyncOnLoadAsset>c__Iterator20();
			<AsyncOnLoadAsset>c__Iterator.resName = resName;
			<AsyncOnLoadAsset>c__Iterator.type = type;
			<AsyncOnLoadAsset>c__Iterator.callback = callback;
			<AsyncOnLoadAsset>c__Iterator.<$>resName = resName;
			<AsyncOnLoadAsset>c__Iterator.<$>type = type;
			<AsyncOnLoadAsset>c__Iterator.<$>callback = callback;
			return <AsyncOnLoadAsset>c__Iterator;
		}

		public Object SyncLoadAssetWithType(string resName, Type type)
		{
			if (!AssetBundleLoader.Instance.SyncLoadAB(resName))
			{
				Debug.LogError("加载AB失败 " + resName);
				return null;
			}
			if (AssetBundleLoader.Instance.GetAssetBundle(resName) != null)
			{
				return AssetBundleLoader.Instance.GetAssetBundle(resName).LoadAsset(Path.GetFileNameWithoutExtension(resName), type);
			}
			Debug.LogError("加载Asset失败 " + resName);
			return null;
		}

		public void UnloadAsset(string resName, Action<bool> callback, Action<float> loadingPercent = null)
		{
			AssetBundleLoader.Instance.UnloadAB(resName, callback, loadingPercent);
		}

		public void UnloadUnusedAssets(Action callback)
		{
			AssetBundleLoader.Instance.UnloadUnusedAssets(callback);
		}

		public void Release()
		{
			AssetBundleLoader.Instance.Release();
		}
	}
}
