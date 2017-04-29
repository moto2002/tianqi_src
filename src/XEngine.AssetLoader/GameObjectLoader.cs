using System;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public class GameObjectLoader
	{
		private static ObjectPool _instance;

		public static ObjectPool Instance
		{
			get
			{
				if (GameObjectLoader._instance == null)
				{
					GameObjectLoader._instance = new ObjectPool();
					GameObject gameObject = new GameObject("GameObjectLoader");
					Object.DontDestroyOnLoad(gameObject);
					GameObjectLoader._instance.SetRootNode(gameObject.get_transform());
				}
				return GameObjectLoader._instance;
			}
		}
	}
}
