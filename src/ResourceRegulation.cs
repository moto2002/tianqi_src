using GameData;
using System;
using System.Collections.Generic;
using XEngine;

public class ResourceRegulation
{
	private static bool IsInit = false;

	private static List<string> whitelist2uiatlas = new List<string>();

	public static List<string> whitelist2spine = new List<string>();

	private static List<string> whitelist2common = new List<string>();

	public static List<string> temp_uiatlas = new List<string>();

	public static List<string> temp_uilist = new List<string>();

	public static void Init()
	{
		if (ResourceRegulation.IsInit)
		{
			return;
		}
		ResourceRegulation.IsInit = true;
		List<ReleaseResWhiteLists> dataList = DataReader<ReleaseResWhiteLists>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			int id = dataList.get_Item(i).id;
			if (id >= 0 && id <= 1000)
			{
				string name = dataList.get_Item(i).name;
				if (!string.IsNullOrEmpty(name))
				{
					ResourceRegulation.whitelist2uiatlas.Add(name.ToLower());
				}
			}
			else if (id >= 1001 && id <= 2000)
			{
				string name2 = dataList.get_Item(i).name;
				if (!string.IsNullOrEmpty(name2))
				{
					int key = int.Parse(GameDataUtils.SplitString4Dot0(name2));
					FXSpine fXSpine = DataReader<FXSpine>.Get(key);
					if (fXSpine != null)
					{
						ResourceRegulation.whitelist2spine.Add("UGUI/PrefabSpine2d/" + fXSpine.name);
					}
				}
			}
			else if (id >= 2001 && id <= 3000)
			{
				string name3 = dataList.get_Item(i).name;
				if (!string.IsNullOrEmpty(name3))
				{
					int key2 = int.Parse(GameDataUtils.SplitString4Dot0(name3));
					Fx fx = DataReader<Fx>.Get(key2);
					if (fx != null && string.IsNullOrEmpty(fx.path))
					{
						ResourceRegulation.whitelist2common.Add(fx.path);
					}
				}
			}
			else if (id >= 3001 && id <= 4000)
			{
				string name4 = dataList.get_Item(i).name;
				if (!string.IsNullOrEmpty(name4))
				{
					ResourceRegulation.whitelist2common.Add(FileSystem.GetPath(name4, string.Empty));
				}
			}
			else if (id >= 4001 && id < 5000)
			{
				string name5 = dataList.get_Item(i).name;
				if (!string.IsNullOrEmpty(name5))
				{
					int key3 = int.Parse(GameDataUtils.SplitString4Dot0(name5));
					UINameTable uINameTable = DataReader<UINameTable>.Get(key3);
					if (uINameTable != null)
					{
						string path = FileSystem.GetPath(uINameTable.name, string.Empty);
						ResourceRegulation.whitelist2common.Add(path);
						ResourceRegulation.temp_uilist.Add(path);
					}
				}
			}
		}
		ResourceRegulation.Preload();
	}

	public static bool is_inwhite_uiatlas(string src_with_suffix_atlas)
	{
		return ResourceRegulation.whitelist2uiatlas.Contains(src_with_suffix_atlas);
	}

	public static bool is_inwhite_spine(string path)
	{
		return ResourceRegulation.whitelist2spine.Contains(path);
	}

	public static bool is_inwhite_common(string path)
	{
		return ResourceRegulation.whitelist2common.Contains(path);
	}

	public static void Preload()
	{
		LoginLoadingRes.Instance.total_res_count += ResourceRegulation.whitelist2spine.get_Count();
		LoginLoadingRes.Instance.total_res_count += ResourceRegulation.whitelist2uiatlas.get_Count();
		LoginLoadingRes.Instance.total_res_count += ResourceRegulation.temp_uilist.get_Count();
		for (int i = 0; i < ResourceRegulation.whitelist2spine.get_Count(); i++)
		{
			FXSpineManager.Instance.PreloadAsset(ResourceRegulation.whitelist2spine.get_Item(i), delegate
			{
				LoginLoadingRes.Instance.PreloadResourceFinish();
			});
		}
		for (int j = 0; j < ResourceRegulation.whitelist2uiatlas.get_Count(); j++)
		{
			string atlas_no_suffix = ConstTP.suffix_atlas_To_src(ResourceRegulation.whitelist2uiatlas.get_Item(j));
			AssetManager.AssetOfTPManager.LoadAtlas(atlas_no_suffix, delegate(bool isSuccess)
			{
				LoginLoadingRes.Instance.PreloadResourceFinish();
			});
		}
		for (int k = 0; k < ResourceRegulation.temp_uilist.get_Count(); k++)
		{
			AssetManager.LoadAssetWithPool(ResourceRegulation.temp_uilist.get_Item(k), delegate(bool isSuccess)
			{
				LoginLoadingRes.Instance.PreloadResourceFinish();
			});
		}
		ResourceRegulation.Uninit();
	}

	public static void Uninit()
	{
		ResourceRegulation.temp_uilist.Clear();
		ResourceRegulation.temp_uilist = null;
	}
}
