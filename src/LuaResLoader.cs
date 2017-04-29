using LuaInterface;
using System;
using System.IO;
using UnityEngine;

public class LuaResLoader : LuaFileUtils
{
	public LuaResLoader()
	{
		LuaFileUtils.instance = this;
		this.beZip = false;
	}

	public override byte[] ReadFile(string fileName)
	{
		byte[] array = this.ReadDownLoadFile(fileName);
		if (array == null)
		{
			array = this.ReadResourceFile(fileName);
		}
		if (array == null)
		{
			array = base.ReadFile(fileName);
		}
		return array;
	}

	private byte[] ReadResourceFile(string fileName)
	{
		byte[] result = null;
		string text = "Lua/" + fileName;
		TextAsset textAsset = Resources.Load(text, typeof(TextAsset)) as TextAsset;
		if (textAsset != null)
		{
			result = textAsset.get_bytes();
			Resources.UnloadAsset(textAsset);
		}
		return result;
	}

	private byte[] ReadDownLoadFile(string fileName)
	{
		string text = fileName;
		if (!Path.IsPathRooted(fileName))
		{
			string text2 = Application.get_persistentDataPath().Replace('\\', '/');
			text = string.Format("{0}/{1}/Lua/{2}", text2, LuaFileUtils.GetOSDir(), fileName);
		}
		if (File.Exists(text))
		{
			return File.ReadAllBytes(text);
		}
		return null;
	}
}
