using System;
using System.IO;
using UnityEngine;

public class LogFileWriter
{
	protected StreamWriter sw;

	protected Action<string> writeAction;

	private static readonly object theLock = new object();

	public void Init(string file, FileMode fileMode = 2)
	{
		string directoryName = Path.GetDirectoryName(file);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		try
		{
			FileStream fileStream = new FileStream(file, fileMode, 2, 3);
			this.sw = new StreamWriter(fileStream);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.get_Message());
		}
	}

	public void Release()
	{
		object obj = LogFileWriter.theLock;
		lock (obj)
		{
			if (this.sw != null)
			{
				this.sw.Close();
				this.sw.Dispose();
				this.sw = null;
			}
		}
	}

	public void WriteLog(string msg)
	{
		object obj = LogFileWriter.theLock;
		lock (obj)
		{
			try
			{
				if (this.sw != null)
				{
					this.sw.WriteLine(msg);
					this.sw.Flush();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.get_Message());
			}
		}
	}
}
