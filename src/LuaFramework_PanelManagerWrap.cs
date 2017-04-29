using LuaFramework;
using LuaInterface;
using System;
using UnityEngine;

public class LuaFramework_PanelManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(PanelManager), typeof(Manager), null);
		L.RegFunction("CreatePanel", new LuaCSFunction(LuaFramework_PanelManagerWrap.CreatePanel));
		L.RegFunction("__eq", new LuaCSFunction(LuaFramework_PanelManagerWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_PanelManagerWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreatePanel(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			PanelManager panelManager = (PanelManager)ToLua.CheckObject(L, 1, typeof(PanelManager));
			string name = ToLua.CheckString(L, 2);
			LuaFunction func = ToLua.CheckLuaFunction(L, 3);
			panelManager.CreatePanel(name, func);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int op_Equality(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Object @object = (Object)ToLua.ToObject(L, 1);
			Object object2 = (Object)ToLua.ToObject(L, 2);
			bool value = @object == object2;
			LuaDLL.lua_pushboolean(L, value);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_ToString(IntPtr L)
	{
		object obj = ToLua.ToObject(L, 1);
		if (obj != null)
		{
			LuaDLL.lua_pushstring(L, obj.ToString());
		}
		else
		{
			LuaDLL.lua_pushnil(L);
		}
		return 1;
	}
}
