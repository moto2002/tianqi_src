using LuaFramework;
using LuaInterface;
using System;
using UnityEngine;

public class LuaFramework_LuaManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaManager), typeof(Manager), null);
		L.RegFunction("InitStart", new LuaCSFunction(LuaFramework_LuaManagerWrap.InitStart));
		L.RegFunction("DoFile", new LuaCSFunction(LuaFramework_LuaManagerWrap.DoFile));
		L.RegFunction("CallFunction", new LuaCSFunction(LuaFramework_LuaManagerWrap.CallFunction));
		L.RegFunction("LuaGC", new LuaCSFunction(LuaFramework_LuaManagerWrap.LuaGC));
		L.RegFunction("Close", new LuaCSFunction(LuaFramework_LuaManagerWrap.Close));
		L.RegFunction("__eq", new LuaCSFunction(LuaFramework_LuaManagerWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_LuaManagerWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InitStart(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaManager luaManager = (LuaManager)ToLua.CheckObject(L, 1, typeof(LuaManager));
			luaManager.InitStart();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DoFile(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaManager luaManager = (LuaManager)ToLua.CheckObject(L, 1, typeof(LuaManager));
			string filename = ToLua.CheckString(L, 2);
			object[] array = luaManager.DoFile(filename);
			ToLua.Push(L, array);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CallFunction(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			LuaManager luaManager = (LuaManager)ToLua.CheckObject(L, 1, typeof(LuaManager));
			string funcName = ToLua.CheckString(L, 2);
			object[] args = ToLua.ToParamsObject(L, 3, num - 2);
			object[] array = luaManager.CallFunction(funcName, args);
			ToLua.Push(L, array);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaGC(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaManager luaManager = (LuaManager)ToLua.CheckObject(L, 1, typeof(LuaManager));
			luaManager.LuaGC();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Close(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaManager luaManager = (LuaManager)ToLua.CheckObject(L, 1, typeof(LuaManager));
			luaManager.Close();
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
