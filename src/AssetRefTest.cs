using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngine.AssetLoader;

public class AssetRefTest : MonoBehaviour
{
	private const string pb_name = "UGUI/Res/TPAtlas/PetUI_pb";

	public GameObject Target;

	private string m_input = string.Empty;

	public static Object PrefabAsset;

	public static GameObject PrefabInstantiate;

	public static Dictionary<string, Dictionary<string, SpriteRenderer>> mapsprs = new Dictionary<string, Dictionary<string, SpriteRenderer>>();

	private void OnGUI()
	{
		int num = -1;
		num++;
		this.m_input = GUI.TextField(this.GetRect(num), this.m_input);
		num++;
		if (GUI.Button(this.GetRect(num), "AssetManager日志"))
		{
			AssetManager.PrintMessage(this.m_input);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "AssetOfTPManager日志"))
		{
			AssetManager.AssetOfTPManager.PrintMessage();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "AssetManager\nRef"))
		{
			AssetManager.PrintMessageRef(this.m_input);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "AssetOfTPManager\nRef"))
		{
			AssetManager.AssetOfTPManager.PrintMessageRef();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "AssetOfTPManager\n清除列表 "))
		{
			AssetManager.AssetOfTPManager.ReleaseAll();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "AssetManager\n清除列表 "))
		{
			AssetManager.ReleaseAll();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "UIManager\n清除列表 "))
		{
			UIManagerControl.Instance.ClearAllUI();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "UnloadUnusedAssets"))
		{
			AssetLoader.UnloadUnusedAssets(null);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "delete ugui"))
		{
			Object.Destroy(UINodesManager.UIRoot.get_gameObject());
		}
		num++;
		if (GUI.Button(this.GetRect(num), "日志开"))
		{
			SystemConfig.LogSetting(true);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "日志关"))
		{
			SystemConfig.LogSetting(false);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "查找引用Image"))
		{
			string input = this.m_input;
			if (!string.IsNullOrEmpty(input))
			{
				Image[] componentsInChildren = UINodesManager.UIRoot.GetComponentsInChildren<Image>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Image image = componentsInChildren[i];
					if (image.get_sprite() != null && image.get_sprite().get_texture() != null && image.get_sprite().get_texture().get_name().ToLower().Contains(input.ToLower()))
					{
						Debug.LogError("image.transform.name = " + image.get_transform().get_name());
					}
				}
			}
		}
		num++;
		if (GUI.Button(this.GetRect(num), "加载 "))
		{
			AssetRefTest.PrefabAsset = Resources.Load("AssetTestUI");
			AssetRefTest.PrefabInstantiate = (Object.Instantiate(AssetRefTest.PrefabAsset) as GameObject);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "卸载 "))
		{
			Object.Destroy(AssetRefTest.PrefabInstantiate);
			AssetRefTest.PrefabInstantiate = null;
			AssetRefTest.PrefabAsset = null;
		}
	}

	private static void AddToUiSprites(string atlas, Dictionary<string, SpriteRenderer> sprites)
	{
		if (sprites == null)
		{
			return;
		}
		AssetRefTest.mapsprs.set_Item(atlas, sprites);
	}

	private Rect GetRect(int index)
	{
		return new Rect((float)(20 + 160 * (index % 2)), (float)(150 + 50 * (index / 2 - 1)), 150f, 45f);
	}
}
