using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadUIBase2File
{
	private static Hashtable _AllResources;

	public static Hashtable AllResources
	{
		get
		{
			if (PreloadUIBase2File._AllResources == null)
			{
				PreloadUIBase2File._AllResources = new Hashtable();
			}
			return PreloadUIBase2File._AllResources;
		}
	}

	public static Hashtable GetAllResources()
	{
		PreloadUIBase2File.AllResources.Clear();
		GameObject[] array = Resources.LoadAll<GameObject>(PathSystem.RootOfResources.UIPrefabPath);
		for (int i = 0; i < array.Length; i++)
		{
			List<string> list = UIResStats.DoCheckResourceStats(array[i], false);
			if (list != null && list.get_Count() != 0)
			{
				string text = string.Empty;
				int num = 0;
				for (int j = 0; j < list.get_Count(); j++)
				{
					string text2 = list.get_Item(j);
					if (text2.Contains("_atlas"))
					{
						Debug.LogError(text2 + ", gameobject = " + array[i].get_name());
					}
					else
					{
						if (num == 0)
						{
							text += text2;
						}
						else
						{
							text = text + ";" + text2;
						}
						num++;
					}
				}
				PreloadUIBase2File.AllResources.set_Item(array[i].get_name(), text);
			}
		}
		return PreloadUIBase2File.AllResources;
	}

	public static Hashtable ExportJSON_PreloadUIBase(string path)
	{
		Hashtable allResources = PreloadUIBase2File.GetAllResources();
		ExportJsonTools.ExportJSON(path, allResources);
		return allResources;
	}
}
