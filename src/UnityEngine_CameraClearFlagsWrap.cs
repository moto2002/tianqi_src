using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_CameraClearFlagsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(CameraClearFlags));
		L.RegVar("Skybox", new LuaCSFunction(UnityEngine_CameraClearFlagsWrap.get_Skybox), null);
		L.RegVar("Color", new LuaCSFunction(UnityEngine_CameraClearFlagsWrap.get_Color), null);
		L.RegVar("SolidColor", new LuaCSFunction(UnityEngine_CameraClearFlagsWrap.get_SolidColor), null);
		L.RegVar("Depth", new LuaCSFunction(UnityEngine_CameraClearFlagsWrap.get_Depth), null);
		L.RegVar("Nothing", new LuaCSFunction(UnityEngine_CameraClearFlagsWrap.get_Nothing), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_CameraClearFlagsWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Skybox(IntPtr L)
	{
		ToLua.Push(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Color(IntPtr L)
	{
		ToLua.Push(L, 2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_SolidColor(IntPtr L)
	{
		ToLua.Push(L, 2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Depth(IntPtr L)
	{
		ToLua.Push(L, 3);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Nothing(IntPtr L)
	{
		ToLua.Push(L, 4);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		CameraClearFlags cameraClearFlags = num;
		ToLua.Push(L, cameraClearFlags);
		return 1;
	}
}
