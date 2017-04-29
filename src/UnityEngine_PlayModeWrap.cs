using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_PlayModeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(PlayMode));
		L.RegVar("StopSameLayer", new LuaCSFunction(UnityEngine_PlayModeWrap.get_StopSameLayer), null);
		L.RegVar("StopAll", new LuaCSFunction(UnityEngine_PlayModeWrap.get_StopAll), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_PlayModeWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_StopSameLayer(IntPtr L)
	{
		ToLua.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_StopAll(IntPtr L)
	{
		ToLua.Push(L, 4);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		PlayMode playMode = num;
		ToLua.Push(L, playMode);
		return 1;
	}
}
