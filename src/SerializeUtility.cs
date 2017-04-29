using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SerializeUtility
{
	public static string GetRuntimePlatformFolderName(RuntimePlatform platform)
	{
		switch (platform)
		{
		case 0:
		case 1:
			return "osx";
		case 2:
		case 7:
			return "windows";
		case 3:
		case 4:
		case 5:
		case 6:
		case 9:
		case 10:
			IL_38:
			if (platform != 17)
			{
				Debug.Log("Target not implemented.");
				return null;
			}
			return "webgl";
		case 8:
			return "ios";
		case 11:
			return "android";
		}
		goto IL_38;
	}

	public static void SerializeToJson(IJsonSerializable data, string path)
	{
		using (FileStream fileStream = new FileStream(path, 2))
		{
			using (StreamWriter streamWriter = new StreamWriter(fileStream))
			{
				streamWriter.Write(data.ToJson());
			}
		}
	}

	public static XDict<T, U> DeserializeXDictFromMemory<T, U>(string json)
	{
		if (json.get_Length() > 0)
		{
			return JsonUtility.FromJson<XDict<T, U>>(json);
		}
		return null;
	}

	public static XDict<T, U> DeserializeXDict<T, U>(string path)
	{
		XDict<T, U> result;
		using (StreamReader streamReader = new StreamReader(path))
		{
			string json = streamReader.ReadToEnd();
			result = SerializeUtility.DeserializeXDictFromMemory<T, U>(json);
		}
		return result;
	}

	public static void SerializeList<T>(List<T> list, string indexPath)
	{
		using (FileStream fileStream = new FileStream(indexPath, 4))
		{
			using (StreamWriter streamWriter = new StreamWriter(fileStream))
			{
				for (int i = 0; i < list.get_Count(); i++)
				{
					TextWriter arg_2C_0 = streamWriter;
					T t = list.get_Item(i);
					arg_2C_0.WriteLine(t.ToString());
				}
			}
		}
	}

	public static void CopyFiles(string fromDir, string suffix, string toDir)
	{
		string[] files = Directory.GetFiles(fromDir, suffix, 1);
		string text = string.Empty;
		for (int i = 0; i < files.Length; i++)
		{
			text = files[i].Replace(fromDir, toDir);
			if (!Directory.Exists(Path.GetDirectoryName(text)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(text));
			}
			File.Copy(files[i], text, true);
		}
	}
}
