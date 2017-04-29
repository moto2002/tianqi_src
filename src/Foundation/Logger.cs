using System;

namespace Foundation
{
	public static class Logger
	{
		public static void Log(object message)
		{
			TaskManager.Log(new TaskManager.TaskLog
			{
				Message = message,
				Type = 3
			});
		}

		public static void LogError(object message)
		{
			TaskManager.Log(new TaskManager.TaskLog
			{
				Message = message,
				Type = 0
			});
		}

		public static void LogWarning(object message)
		{
			TaskManager.Log(new TaskManager.TaskLog
			{
				Message = message,
				Type = 2
			});
		}

		public static void LogException(Exception message)
		{
			TaskManager.Log(new TaskManager.TaskLog
			{
				Message = message,
				Type = 4
			});
		}
	}
}
