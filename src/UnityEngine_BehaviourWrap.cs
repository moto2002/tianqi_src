using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_BehaviourWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Behaviour), typeof(Component), null);
		L.RegFunction("New", new LuaCSFunction(UnityEngine_BehaviourWrap._CreateUnityEngine_Behaviour));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_BehaviourWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_BehaviourWrap.Lua_ToString));
		L.RegVar("enabled", new LuaCSFunction(UnityEngine_BehaviourWrap.get_enabled), new LuaCSFunction(UnityEngine_BehaviourWrap.set_enabled));
		L.RegVar("isActiveAndEnabled", new LuaCSFunction(UnityEngine_BehaviourWrap.get_isActiveAndEnabled), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Behaviour(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				Behaviour obj = new Behaviour();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Behaviour.New");
			}
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

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_enabled(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Behaviour behaviour = (Behaviour)obj;
			bool enabled = behaviour.get_enabled();
			LuaDLL.lua_pushboolean(L, enabled);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index enabled on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isActiveAndEnabled(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Behaviour behaviour = (Behaviour)obj;
			bool isActiveAndEnabled = behaviour.get_isActiveAndEnabled();
			LuaDLL.lua_pushboolean(L, isActiveAndEnabled);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isActiveAndEnabled on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enabled(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Behaviour behaviour = (Behaviour)obj;
			bool enabled = LuaDLL.luaL_checkboolean(L, 2);
			behaviour.set_enabled(enabled);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index enabled on a nil value");
		}
		return result;
	}
}
