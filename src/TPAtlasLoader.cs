using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;

public class TPAtlasLoader
{
	public static Dictionary<string, SpriteRenderer> LoadAtlasNow(string atlas_no_suffix)
	{
		string text = atlas_no_suffix + "_pb";
		text = text.ToLower();
		if (!FileSystem.HasKey(text))
		{
			return null;
		}
		GameObject gameObject = AssetManager.LoadAssetNowWithPool(FileSystem.GetPath(text, string.Empty)) as GameObject;
		if (gameObject == null)
		{
			Debug.LogError("atlas prefab is null :" + text);
			return null;
		}
		return TPAtlasLoader.ExtractSprites(gameObject);
	}

	public static void LoadAtlasToSprites(string name, Action<Dictionary<string, SpriteRenderer>> finish_callback)
	{
		name = name.ToLower();
		TPAtlasLoader.LoadAtlas(name, delegate(GameObject go_prefab)
		{
			if (go_prefab == null)
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(null);
				}
			}
			else
			{
				if (go_prefab == null)
				{
					Debug.LogError("atlas prefab is null");
					if (finish_callback != null)
					{
						finish_callback.Invoke(null);
					}
					return;
				}
				Dictionary<string, SpriteRenderer> dictionary = TPAtlasLoader.ExtractSprites(go_prefab);
				if (finish_callback != null)
				{
					finish_callback.Invoke(dictionary);
				}
			}
		});
	}

	private static void LoadAtlas(string name, Action<GameObject> finish_callback)
	{
		if (string.IsNullOrEmpty(name))
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(null);
			}
			return;
		}
		string name2 = name + "_pb";
		if (!FileSystem.HasKey(name2))
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(null);
			}
			return;
		}
		string path = FileSystem.GetPath(name2, string.Empty);
		AssetManager.LoadAssetWithPoolNoAB(path, delegate(bool isSuccess)
		{
			if (!isSuccess)
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(null);
				}
				return;
			}
			Object @object = AssetManager.LoadAssetNowWithPoolNoAB(path);
			if (@object == null)
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(null);
				}
				return;
			}
			if (finish_callback != null)
			{
				finish_callback.Invoke(@object as GameObject);
			}
		});
	}

	public static bool IsInPool(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return false;
		}
		string name2 = name.ToLower() + "_pb";
		string path = FileSystem.GetPath(name2, string.Empty);
		return AssetManager.IsInPool(path);
	}

	private static Dictionary<string, SpriteRenderer> ExtractSprites(GameObject go_prefab)
	{
		Dictionary<string, SpriteRenderer> dictionary = new Dictionary<string, SpriteRenderer>();
		for (int i = 0; i < go_prefab.get_transform().get_childCount(); i++)
		{
			Transform child = go_prefab.get_transform().GetChild(i);
			if (child != null)
			{
				SpriteRenderer component = child.GetComponent<SpriteRenderer>();
				if (component != null && component.get_sprite() != null)
				{
					dictionary.set_Item(component.get_sprite().get_name(), component);
				}
			}
		}
		return dictionary;
	}
}
