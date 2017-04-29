using LuaFramework;
using LuaInterface;
using System;
using UnityEngine;

public class LuaFramework_LuaBehaviourWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaBehaviour), typeof(View), null);
		L.RegFunction("AddClick", new LuaCSFunction(LuaFramework_LuaBehaviourWrap.AddClick));
		L.RegFunction("RemoveClick", new LuaCSFunction(LuaFramework_LuaBehaviourWrap.RemoveClick));
		L.RegFunction("ClearClick", new LuaCSFunction(LuaFramework_LuaBehaviourWrap.ClearClick));
		L.RegFunction("__eq", new LuaCSFunction(LuaFramework_LuaBehaviourWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_LuaBehaviourWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddClick(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			LuaBehaviour luaBehaviour = (LuaBehaviour)ToLua.CheckObject(L, 1, typeof(LuaBehaviour));
			GameObject go = (GameObject)ToLua.CheckUnityObject(L, 2, typeof(GameObject));
			LuaFunction luafunc = ToLua.CheckLuaFunction(L, 3);
			luaBehaviour.AddClick(go, luafunc);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveClick(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaBehaviour luaBehaviour = (LuaBehaviour)ToLua.CheckObject(L, 1, typeof(LuaBehaviour));
			GameObject go = (GameObject)ToLua.CheckUnityObject(L, 2, typeof(GameObject));
			luaBehaviour.RemoveClick(go);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearClick(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaBehaviour luaBehaviour = (LuaBehaviour)ToLua.CheckObject(L, 1, typeof(LuaBehaviour));
			luaBehaviour.ClearClick();
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
