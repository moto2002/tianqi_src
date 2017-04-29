using System;
using System.IO;
using UnityEngine;

public class ClientLogFileWriter : LogFileWriter
{
	protected string logDirectoryName = Application.get_persistentDataPath() + "/log/";

	protected string logFileName = "log_{0}.txt";

	protected FileStream fs;

	protected new StreamWriter sw;

	protected new Action<string> writeAction;

	private static readonly object theLock = new object();

	public ClientLogFileWriter()
	{
		string file = this.logDirectoryName + string.Format(this.logFileName, DateTime.get_Today().ToString("yyyyMMdd"));
		base.Init(file, 2);
	}
}
