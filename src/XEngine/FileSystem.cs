using System;
using System.Collections;
using UnityEngine;

namespace XEngine
{
	public static class FileSystem
	{
		public const string Resources2JsonName = "resources2json";

		public static string FileHeader = string.Empty;

		private static Hashtable mResourcePaths;

		public static readonly string key_suffix_spine = ".sp";

		public static readonly string key_suffix_prefab = ".prefab";

		public static readonly string key_suffix_controller = ".controller";

		public static readonly string key_suffix_material = ".mat";

		private static Hashtable ResourcePaths
		{
			get
			{
				if (FileSystem.mResourcePaths == null)
				{
					FileSystem.Init();
				}
				return FileSystem.mResourcePaths;
			}
		}

		public static void Init()
		{
			if (FileSystem.mResourcePaths == null)
			{
				FileSystem.mResourcePaths = Utils.ReadFromMemory(XUtility.GetConfigTxt("resources2json", ".txt"));
			}
			Debug.Log("===>resources2json count = " + FileSystem.mResourcePaths.get_Count());
		}

		public static void ResetResourcePaths(Hashtable allResources)
		{
			if (allResources == null)
			{
				return;
			}
			FileSystem.mResourcePaths = allResources;
		}

		public static string FromUniqueNameToPath(string unique_name)
		{
			return unique_name;
		}

		public static string GetPath(string name, string suffix = "")
		{
			name += suffix;
			if (FileSystem.ResourcePaths != null && FileSystem.ResourcePaths.Contains(name))
			{
				return FileSystem.ResourcePaths.get_Item(name).ToString();
			}
			return string.Empty;
		}

		public static bool HasKey(string name)
		{
			return FileSystem.ResourcePaths != null && FileSystem.ResourcePaths.ContainsKey(name);
		}

		public static bool HasValue(string path)
		{
			return FileSystem.ResourcePaths != null && FileSystem.ResourcePaths.ContainsValue(path);
		}

		public static string GetPathOfSpine(string name)
		{
			return FileSystem.GetPath(name, FileSystem.key_suffix_spine);
		}

		public static string GetPathOfPrefab(string name)
		{
			return FileSystem.GetPath(name, FileSystem.key_suffix_prefab);
		}

		public static string GetPathOfController(string name)
		{
			return FileSystem.GetPath(name, FileSystem.key_suffix_controller);
		}
	}
}
