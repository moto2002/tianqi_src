using System;
using System.IO;
using UnityEngine;

internal class SwitchFile : Singleton<SwitchFile>
{
	public enum FileName
	{
		ABCheck,
		NoUpdate
	}

	private string GetFilePath(SwitchFile.FileName fileName)
	{
		return PathUtil.Combine(new string[]
		{
			Application.get_persistentDataPath(),
			fileName.ToString()
		});
	}

	public bool IsFileExist(SwitchFile.FileName fileName)
	{
		return File.Exists(this.GetFilePath(fileName));
	}

	public void CreateFile(SwitchFile.FileName fileName)
	{
		FileHelper.CreateIfNotExist(this.GetFilePath(fileName));
	}
}
