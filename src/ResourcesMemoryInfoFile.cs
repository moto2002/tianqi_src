using System;
using System.IO;
using UnityEngine;

internal class ResourcesMemoryInfoFile : Singleton<ResourcesMemoryInfoFile>
{
	private LogFileWriter LogFile;

	public void Begin()
	{
		string text = Path.Combine(Application.get_persistentDataPath(), "MemoryInfo");
		string text2 = string.Format("{0}.txt", DateTime.get_Now().ToString("yyyyMMdd_HHmmss"));
		string file = Path.Combine(text, text2);
		this.LogFile = new LogFileWriter();
		this.LogFile.Init(file, 2);
	}

	public void WriteLine(string msg)
	{
		Debug.Log(msg);
		this.LogFile.WriteLog(msg);
	}

	public void End()
	{
		this.LogFile.Release();
		this.LogFile = null;
	}
}
