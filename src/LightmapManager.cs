using GameData;
using System;
using UnityEngine;
using XEngine;

public class LightmapManager
{
	public static void UpdateLightmap(int sceneId)
	{
		string text = null;
		Scene scene = DataReader<Scene>.Get(sceneId);
		if (scene == null)
		{
			return;
		}
		if (!string.IsNullOrEmpty(scene.lightmap))
		{
			text = scene.lightmap;
		}
		else
		{
			string[] array = scene.path.Split(new char[]
			{
				'/'
			});
			if (array.Length > 0)
			{
				text = array[array.Length - 1];
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		string pathOfPrefab = FileSystem.GetPathOfPrefab(text);
		if (string.IsNullOrEmpty(pathOfPrefab))
		{
			return;
		}
		GameObject gameObject = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(pathOfPrefab, typeof(Object)) as GameObject;
		if (gameObject != null)
		{
			PrefabLightmap component = gameObject.GetComponent<PrefabLightmap>();
			if (component == null)
			{
				return;
			}
			LightmapData[] array2 = new LightmapData[component.lightmap_far.get_Count()];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new LightmapData();
				array2[i].set_lightmapFar(component.lightmap_far.get_Item(i));
			}
			LightmapSettings.set_lightmaps(array2);
			LightmapSettings.set_lightmapsMode(0);
		}
	}
}
