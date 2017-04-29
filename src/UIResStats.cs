using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngine;

[ExecuteInEditMode]
public class UIResStats : MonoBehaviour
{
	public void CheckResourceStats()
	{
		UIResStats.DoCheckResourceStats(base.get_gameObject(), true);
	}

	public void CheckNodeCount()
	{
		Debug.LogError("Node Count = " + base.GetComponentsInChildren<Transform>(true).Length);
	}

	public static List<string> DoCheckResourceStats(GameObject go, bool output_log = true)
	{
		if (go == null)
		{
			return null;
		}
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		Image[] componentsInChildren = go.GetComponentsInChildren<Image>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Image image = componentsInChildren[i];
			if (image.get_sprite() != null && image.get_sprite().get_texture() != null && !list.Contains(image.get_sprite().get_texture().get_name()))
			{
				list.Add(image.get_sprite().get_texture().get_name());
			}
			else
			{
				UIImageRef component = image.GetComponent<UIImageRef>();
				if (component != null && component.IsBinding())
				{
					string path = FileSystem.GetPath(component.sprite_name, string.Empty);
					if (!string.IsNullOrEmpty(path) && !list.Contains(path))
					{
						list.Add(path);
					}
				}
			}
		}
		RawImage[] componentsInChildren2 = go.GetComponentsInChildren<RawImage>(true);
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			RawImage rawImage = componentsInChildren2[j];
			if (rawImage != null && rawImage.get_texture() != null && !list2.Contains(rawImage.get_texture().get_name()))
			{
				list2.Add(rawImage.get_texture().get_name());
			}
			else
			{
				UIImageRef component2 = rawImage.GetComponent<UIImageRef>();
				if (component2 != null && component2.IsBinding() && !list2.Contains(component2.sprite_name))
				{
					list2.Add(component2.sprite_name);
				}
			}
		}
		if (output_log)
		{
			for (int k = 0; k < list.get_Count(); k++)
			{
				Debug.LogError("m_list_atlas : " + list.get_Item(k));
			}
			for (int l = 0; l < list2.get_Count(); l++)
			{
				Debug.LogError("m_list_texture : " + list2.get_Item(l));
			}
		}
		return list;
	}
}
