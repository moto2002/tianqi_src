using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_LightTypeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(LightType));
		L.RegVar("Spot", new LuaCSFunction(UnityEngine_LightTypeWrap.get_Spot), null);
		L.RegVar("Directional", new LuaCSFunction(UnityEngine_LightTypeWrap.get_Directional), null);
		L.RegVar("Point", new LuaCSFunction(UnityEngine_LightTypeWrap.get_Point), null);
		L.RegVar("Area", new LuaCSFunction(UnityEngine_LightTypeWrap.get_Area), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_LightTypeWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Spot(IntPtr L)
	{
		ToLua.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Directional(IntPtr L)
	{
		ToLua.Push(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Point(IntPtr L)
	{
		ToLua.Push(L, 2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Area(IntPtr L)
	{
		ToLua.Push(L, 3);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		LightType lightType = num;
		ToLua.Push(L, lightType);
		return 1;
	}
}
