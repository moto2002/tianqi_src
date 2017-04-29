using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class StopwatchLog : IDisposable
{
	private static LogFileWriter p_logFile;

	private Stopwatch Timer = new Stopwatch();

	private string Name = string.Empty;

	private bool IsLogBegin = true;

	private int LogCount;

	public bool IsBegin;

	private static LogFileWriter LogFile
	{
		get
		{
			if (StopwatchLog.p_logFile == null)
			{
				string text = Path.Combine(Application.get_persistentDataPath(), "StopwatchLog.log");
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				StopwatchLog.p_logFile = new LogFileWriter();
				StopwatchLog.p_logFile.Init(text, 2);
			}
			return StopwatchLog.p_logFile;
		}
	}

	public static StopwatchLog Begin(string name, bool logBegin = true)
	{
		StopwatchLog stopwatchLog = new StopwatchLog();
		stopwatchLog.DoBegin(name, logBegin);
		return stopwatchLog;
	}

	public static void Release()
	{
		if (StopwatchLog.p_logFile != null)
		{
			StopwatchLog.p_logFile.Release();
			StopwatchLog.p_logFile = null;
		}
	}

	public void DoBegin(string name, bool logBegin = true)
	{
		if (this.IsBegin)
		{
			Debug.LogError("重复Begin的Stopwatch!");
			return;
		}
		this.IsBegin = true;
		this.Name = name;
		this.IsLogBegin = logBegin;
		this.LogCount = 0;
		if (this.IsLogBegin)
		{
			this.Log("{0} {1}: ----------begin----------", new object[]
			{
				DateTime.get_Now(),
				this.Name
			});
		}
		this.Timer.Restart();
	}

	public void Sample(string flag = null)
	{
		TimeSpan elapsed = this.Timer.get_Elapsed();
		this.LogCount++;
		this.Log("{0} {1}: ----------log({2}) elapse :{3}", new object[]
		{
			DateTime.get_Now(),
			this.Name,
			(!string.IsNullOrEmpty(flag)) ? flag : this.LogCount.ToString(),
			elapsed
		});
		this.Timer.Restart();
	}

	public TimeSpan DoEnd()
	{
		if (!this.IsBegin)
		{
			Debug.LogError("重复End的Stopwatch!");
		}
		this.IsBegin = false;
		TimeSpan elapsed = this.Timer.get_Elapsed();
		if (this.IsLogBegin)
		{
			this.Log("{0} {1}: ----------end elapse :{2}----------", new object[]
			{
				DateTime.get_Now(),
				this.Name,
				elapsed
			});
		}
		else
		{
			this.Log("{0} {1}: ----------elapse :{2}----------", new object[]
			{
				DateTime.get_Now(),
				this.Name,
				elapsed
			});
		}
		return elapsed;
	}

	private void Log(string format, params object[] args)
	{
		string text = string.Format(format, args);
		Debug.Log(text);
		StopwatchLog.LogFile.WriteLog(text);
	}

	public void Dispose()
	{
		this.DoEnd();
	}
}
