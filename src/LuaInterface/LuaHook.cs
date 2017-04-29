using System;

namespace LuaInterface
{
	public delegate void LuaHook(IntPtr L, ref Lua_Debug ar);
}
