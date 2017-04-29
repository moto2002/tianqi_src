using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_SpaceWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(Space));
		L.RegVar("World", new LuaCSFunction(UnityEngine_SpaceWrap.get_World), null);
		L.RegVar("Self", new LuaCSFunction(UnityEngine_SpaceWrap.get_Self), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_SpaceWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_World(IntPtr L)
	{
		ToLua.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Self(IntPtr L)
	{
		ToLua.Push(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		Space space = num;
		ToLua.Push(L, space);
		return 1;
	}
}
