using System;

namespace System.Diagnostics
{
	internal static class StopwatchExtension
	{
		public static void Restart(this Stopwatch stopWatch)
		{
			stopWatch.Reset();
			stopWatch.Start();
		}
	}
}
