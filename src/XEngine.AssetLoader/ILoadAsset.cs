using System;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public interface ILoadAsset
	{
		void AsyncLoadAssetWithType(string resName, Type type, AssetCallback callback);

		Object SyncLoadAssetWithType(string resName, Type type);

		void UnloadAsset(string resName, Action<bool> callback, Action<float> loadingPercent = null);

		void UnloadUnusedAssets(Action callback);
	}
}
