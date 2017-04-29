using LuaInterface;
using System;
using System.IO;
using UnityEngine;

namespace LuaFramework
{
	public class LuaLoader : LuaFileUtils
	{
		public LuaLoader()
		{
			LuaFileUtils.instance = this;
			this.beZip = AppConst.LuaBundleMode;
		}

		public void AddBundle(string bundleName)
		{
			string text = Util.DataPath + bundleName.ToLower();
			if (File.Exists(text))
			{
				AssetBundle assetBundle = AssetBundle.LoadFromFile(text);
				if (assetBundle != null)
				{
					bundleName = bundleName.Replace("Lua/", string.Empty);
					bundleName = bundleName.Replace(".unity3d", string.Empty);
					base.AddSearchBundle(bundleName.ToLower(), assetBundle);
				}
			}
		}

		public override byte[] ReadFile(string fileName)
		{
			return base.ReadFile(fileName);
		}
	}
}
