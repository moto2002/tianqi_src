using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace XEngine.AssetLoader
{
	internal class StaticLoader : ILoadAsset
	{
		private SingleLoader coroutineQueue;

		public void AsyncLoadAssetWithType(string resName, Type type, AssetCallback callback)
		{
			if (resName.Contains("Scenes/s") && Path.GetDirectoryName(resName) == "Scenes")
			{
				callback(null);
			}
			else
			{
				if (this.coroutineQueue == null)
				{
					this.coroutineQueue = new SingleLoader(new Func<IEnumerator, Coroutine>(GameManager.Instance.StartCoroutine));
				}
				this.coroutineQueue.Enqueue(this.AsyncOnLoadAsset(resName, type, callback));
			}
		}

		[DebuggerHidden]
		private IEnumerator AsyncOnLoadAsset(string resName, Type type, AssetCallback callback)
		{
			StaticLoader.<AsyncOnLoadAsset>c__Iterator23 <AsyncOnLoadAsset>c__Iterator = new StaticLoader.<AsyncOnLoadAsset>c__Iterator23();
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
			Object @object = Resources.Load(resName, type);
			if (@object == null)
			{
				Debug.LogErrorFormat("资源加载失败 ：{0}", new object[]
				{
					resName
				});
			}
			return @object;
		}

		public void UnloadAsset(string resName, Action<bool> callback, Action<float> loadingPercent = null)
		{
			if (callback != null)
			{
				callback.Invoke(true);
			}
		}

		public void UnloadUnusedAssets(Action callback)
		{
			if (callback != null)
			{
				callback.Invoke();
			}
		}
	}
}
