using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LuaInterface
{
	public class LuaFileUtils
	{
		protected static LuaFileUtils instance;

		public bool beZip;

		protected List<string> searchPaths = new List<string>();

		protected Dictionary<string, AssetBundle> zipMap = new Dictionary<string, AssetBundle>();

		public static LuaFileUtils Instance
		{
			get
			{
				if (LuaFileUtils.instance == null)
				{
					LuaFileUtils.instance = new LuaFileUtils();
				}
				return LuaFileUtils.instance;
			}
			protected set
			{
				LuaFileUtils.instance = value;
			}
		}

		public LuaFileUtils()
		{
			LuaFileUtils.instance = this;
		}

		public virtual void Dispose()
		{
			if (LuaFileUtils.instance != null)
			{
				LuaFileUtils.instance = null;
				this.searchPaths.Clear();
				using (Dictionary<string, AssetBundle>.Enumerator enumerator = this.zipMap.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, AssetBundle> current = enumerator.get_Current();
						current.get_Value().Unload(true);
					}
				}
				this.zipMap.Clear();
			}
		}

		public bool AddSearchPath(string path, bool front = false)
		{
			if (path.get_Length() > 0 && path.get_Chars(path.get_Length() - 1) != '/')
			{
				path += "/";
			}
			int num = this.searchPaths.IndexOf(path);
			if (num >= 0)
			{
				return false;
			}
			if (front)
			{
				this.searchPaths.Insert(0, path);
			}
			else
			{
				this.searchPaths.Add(path);
			}
			return true;
		}

		public void RemoveSearchPath(string path)
		{
			if (path.get_Length() > 0 && path.get_Chars(path.get_Length() - 1) != '/')
			{
				path += "/";
			}
			int num = this.searchPaths.IndexOf(path);
			if (num >= 0)
			{
				this.searchPaths.RemoveAt(num);
			}
		}

		public void AddSearchBundle(string name, AssetBundle bundle)
		{
			this.zipMap.set_Item(name, bundle);
			Debugger.Log("Add Lua bundle: " + name);
		}

		public string GetFullPathFileName(string fileName)
		{
			if (fileName == string.Empty)
			{
				return string.Empty;
			}
			if (Path.IsPathRooted(fileName))
			{
				return fileName;
			}
			for (int i = 0; i < this.searchPaths.get_Count(); i++)
			{
				string text = Path.Combine(this.searchPaths.get_Item(i), fileName);
				if (File.Exists(text))
				{
					return text;
				}
			}
			return null;
		}

		public virtual byte[] ReadFile(string fileName)
		{
			if (!this.beZip)
			{
				string fullPathFileName = this.GetFullPathFileName(fileName);
				byte[] result = null;
				if (File.Exists(fullPathFileName))
				{
					result = File.ReadAllBytes(fullPathFileName);
				}
				return result;
			}
			return this.ReadZipFile(fileName);
		}

		private byte[] ReadZipFile(string fileName)
		{
			AssetBundle assetBundle = null;
			byte[] result = null;
			string text = "Lua";
			int num = fileName.LastIndexOf('/');
			if (num > 0)
			{
				text = fileName.Substring(0, num);
				text = text.Replace('/', '_');
				text = string.Format("Lua_{0}", text);
				fileName = fileName.Substring(num + 1);
			}
			this.zipMap.TryGetValue(text.ToLower(), ref assetBundle);
			if (assetBundle != null)
			{
				string[] allAssetNames = assetBundle.GetAllAssetNames();
				for (int i = 0; i < allAssetNames.Length; i++)
				{
					if (allAssetNames[i].EndsWith(fileName.ToLower() + ".bytes"))
					{
						fileName = allAssetNames[i];
						break;
					}
				}
				TextAsset textAsset = assetBundle.LoadAsset<TextAsset>(fileName);
				if (textAsset != null)
				{
					result = textAsset.get_bytes();
					Resources.UnloadAsset(textAsset);
				}
			}
			return result;
		}

		public static string GetOSDir()
		{
			return "Android";
		}
	}
}
