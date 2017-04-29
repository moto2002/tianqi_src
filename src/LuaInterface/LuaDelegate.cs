using System;

namespace LuaInterface
{
	public class LuaDelegate
	{
		public LuaFunction func;

		public LuaDelegate(LuaFunction func)
		{
			this.func = func;
		}
	}
}
