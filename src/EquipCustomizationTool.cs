using GameData;
using System;
using UnityEngine;
using XEngineActor;

public class EquipCustomizationTool
{
	public static GameObject GetInstantiate(string path)
	{
		path = path.Replace("\\", "/");
		Object @object = AssetManager.LoadAssetNowWithPool(path);
		if (@object == null)
		{
			Debug.LogError("asset is null, path = " + path);
			return null;
		}
		GameObject gameObject = Object.Instantiate(@object) as GameObject;
		if (gameObject == null)
		{
			Debug.LogError("Instantiate is null, name = " + path);
			return null;
		}
		ResourceManager.SetAssetRef(gameObject, path);
		return gameObject;
	}

	public static void GetInstantiateClothes(EquipBody dataEB, Action<SkinnedMeshRenderer> loaded)
	{
		if (string.IsNullOrEmpty(dataEB.mesh))
		{
			if (loaded != null)
			{
				loaded.Invoke(null);
			}
			return;
		}
		AssetManager.LoadEquipCustomizationAsset(dataEB.mesh.Replace("\\", "/"), delegate(Object meshAsset)
		{
			if (meshAsset == null)
			{
				if (loaded != null)
				{
					loaded.Invoke(null);
				}
				return;
			}
			GameObject gameObject = meshAsset as GameObject;
			if (gameObject != null)
			{
				Transform transform = XUtility.RecursiveFindTransform(gameObject.get_transform(), dataEB.slot);
				if (transform != null)
				{
					SkinnedMeshRenderer component = transform.GetComponent<SkinnedMeshRenderer>();
					if (loaded != null)
					{
						loaded.Invoke(component);
					}
					return;
				}
			}
			if (loaded != null)
			{
				loaded.Invoke(null);
			}
		});
	}

	public static void SetLayer(GameObject go, Actor actor)
	{
		if (actor == null)
		{
			return;
		}
		if ((actor is ActorSelf && RTManager.Instance.RTIsUI()) || actor is ActorModel)
		{
			LayerSystem.SetGameObjectLayer(go, "CameraRange", 1);
		}
	}
}
