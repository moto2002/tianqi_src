using System;
using UnityEngine;

namespace LuaFramework
{
	public class TimerInfo
	{
		public long tick;

		public bool stop;

		public bool delete;

		public Object target;

		public string className;

		public TimerInfo(string className, Object target)
		{
			this.className = className;
			this.target = target;
			this.delete = false;
		}
	}
}
