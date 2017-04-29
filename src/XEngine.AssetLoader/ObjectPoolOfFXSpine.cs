using System;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public class ObjectPoolOfFXSpine : ObjectPool
	{
		protected override void LoadAssetWithPool(string path, Action<bool> finish_callback)
		{
			AssetManager.AssetOfSpineManager.LoadAssetWithPool(path, finish_callback);
		}

		protected override Object GetAssetWithPool(string path)
		{
			return AssetManager.AssetOfSpineManager.GetAssetWithPool(path);
		}
	}
}
