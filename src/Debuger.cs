using System;
using UnityEngine;

public class Debuger
{
	public const string EnableLogTag = "EnableLog";

	protected static bool enableLog = true;

	public static bool EnableLog
	{
		get
		{
			return Debuger.enableLog;
		}
		set
		{
			Debuger.enableLog = value;
		}
	}

	private static void Log(LogType type, object message)
	{
	}

	public static void Info(object message)
	{
		Debuger.Log(3, message);
	}

	public static void Warning(object message)
	{
		Debuger.Log(2, message);
	}

	public static void Error(object message)
	{
		Debuger.Log(0, message);
	}

	public static void Info(string format, params object[] arg)
	{
		Debuger.Log(3, string.Format(format, arg));
	}

	public static void Warning(string format, params object[] arg)
	{
		Debuger.Log(2, string.Format(format, arg));
	}

	public static void Error(string format, params object[] arg)
	{
		Debuger.Log(0, string.Format(format, arg));
	}
}
