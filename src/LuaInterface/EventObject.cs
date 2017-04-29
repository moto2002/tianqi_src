using System;

namespace LuaInterface
{
	public class EventObject
	{
		[NoToLua]
		public EventOp op;

		[NoToLua]
		public LuaFunction func;

		[NoToLua]
		public string name = string.Empty;

		[NoToLua]
		public EventObject(string name)
		{
			this.name = name;
		}

		[NoToLua]
		public override string ToString()
		{
			return this.name;
		}

		public static EventObject operator +(EventObject a, LuaFunction b)
		{
			a.op = EventOp.Add;
			a.func = b;
			return a;
		}

		public static EventObject operator -(EventObject a, LuaFunction b)
		{
			a.op = EventOp.Sub;
			a.func = b;
			return a;
		}
	}
}
