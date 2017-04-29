using LuaInterface;
using System;

public class LuaInterface_LuaOutWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaOutMetatable), null, null);
		L.EndClass();
	}
}
