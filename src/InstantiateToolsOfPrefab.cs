using System;
using UnityEngine;

public class InstantiateToolsOfPrefab
{
	public static GameObject Get(string name)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(name);
		if (instantiate2Prefab != null)
		{
			instantiate2Prefab.set_name(name);
			instantiate2Prefab.get_transform().set_localPosition(Vector3.get_zero());
			instantiate2Prefab.get_transform().set_localRotation(Quaternion.get_identity());
			instantiate2Prefab.get_transform().set_localScale(Vector3.get_one());
			instantiate2Prefab.SetActive(true);
		}
		return instantiate2Prefab;
	}

	public static GameObject Get(string name, Transform parent)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(name);
		if (instantiate2Prefab != null)
		{
			instantiate2Prefab.set_name(name);
			if (parent != null)
			{
				instantiate2Prefab.get_transform().SetParent(parent, false);
				instantiate2Prefab.get_transform().set_localPosition(Vector3.get_zero());
				instantiate2Prefab.get_transform().set_localRotation(Quaternion.get_identity());
				instantiate2Prefab.get_transform().set_localScale(Vector3.get_one());
				instantiate2Prefab.SetActive(true);
			}
		}
		return instantiate2Prefab;
	}
}
