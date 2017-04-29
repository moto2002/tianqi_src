using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine
{
	public static class PreloadUIBaseSystem
	{
		public const string preloaduibse2json = "preload_uibase2json";

		private static Hashtable mResourcePaths;

		public static Dictionary<string, string[]> preload_uibase_map = new Dictionary<string, string[]>();

		private static Hashtable ResourcePaths
		{
			get
			{
				if (PreloadUIBaseSystem.mResourcePaths == null)
				{
					PreloadUIBaseSystem.Init();
				}
				return PreloadUIBaseSystem.mResourcePaths;
			}
		}

		public static void Init()
		{
			if (PreloadUIBaseSystem.mResourcePaths == null)
			{
				PreloadUIBaseSystem.preload_uibase_map.Clear();
				PreloadUIBaseSystem.mResourcePaths = Utils.ReadFromMemory(XUtility.GetConfigTxt("preload_uibase2json", ".txt"));
				Debug.Log("===>preloaduibse2json count = " + PreloadUIBaseSystem.mResourcePaths.get_Count());
				IEnumerator enumerator = PreloadUIBaseSystem.mResourcePaths.get_Keys().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object current = enumerator.get_Current();
						PreloadUIBaseSystem.preload_uibase_map.set_Item(current.ToString(), PreloadUIBaseSystem.mResourcePaths.get_Item(current).ToString().Split(new char[]
						{
							';'
						}));
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				PreloadUIBaseSystem.mResourcePaths.Clear();
			}
		}

		public static string[] GetPreloads(string name)
		{
			if (PreloadUIBaseSystem.preload_uibase_map.ContainsKey(name))
			{
				return PreloadUIBaseSystem.preload_uibase_map.get_Item(name);
			}
			return null;
		}
	}
}
