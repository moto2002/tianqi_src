using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_QueueModeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(QueueMode));
		L.RegVar("CompleteOthers", new LuaCSFunction(UnityEngine_QueueModeWrap.get_CompleteOthers), null);
		L.RegVar("PlayNow", new LuaCSFunction(UnityEngine_QueueModeWrap.get_PlayNow), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_QueueModeWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_CompleteOthers(IntPtr L)
	{
		ToLua.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_PlayNow(IntPtr L)
	{
		ToLua.Push(L, 2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		QueueMode queueMode = num;
		ToLua.Push(L, queueMode);
		return 1;
	}
}
