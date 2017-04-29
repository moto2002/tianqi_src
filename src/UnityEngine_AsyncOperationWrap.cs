using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_AsyncOperationWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AsyncOperation), typeof(object), null);
		L.RegFunction("New", new LuaCSFunction(UnityEngine_AsyncOperationWrap._CreateUnityEngine_AsyncOperation));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_AsyncOperationWrap.Lua_ToString));
		L.RegVar("isDone", new LuaCSFunction(UnityEngine_AsyncOperationWrap.get_isDone), null);
		L.RegVar("progress", new LuaCSFunction(UnityEngine_AsyncOperationWrap.get_progress), null);
		L.RegVar("priority", new LuaCSFunction(UnityEngine_AsyncOperationWrap.get_priority), new LuaCSFunction(UnityEngine_AsyncOperationWrap.set_priority));
		L.RegVar("allowSceneActivation", new LuaCSFunction(UnityEngine_AsyncOperationWrap.get_allowSceneActivation), new LuaCSFunction(UnityEngine_AsyncOperationWrap.set_allowSceneActivation));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_AsyncOperation(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				AsyncOperation o = new AsyncOperation();
				ToLua.PushObject(L, o);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.AsyncOperation.New");
			}
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
	private static int get_isDone(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AsyncOperation asyncOperation = (AsyncOperation)obj;
			bool isDone = asyncOperation.get_isDone();
			LuaDLL.lua_pushboolean(L, isDone);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isDone on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_progress(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AsyncOperation asyncOperation = (AsyncOperation)obj;
			float progress = asyncOperation.get_progress();
			LuaDLL.lua_pushnumber(L, (double)progress);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index progress on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_priority(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AsyncOperation asyncOperation = (AsyncOperation)obj;
			int priority = asyncOperation.get_priority();
			LuaDLL.lua_pushinteger(L, priority);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index priority on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_allowSceneActivation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AsyncOperation asyncOperation = (AsyncOperation)obj;
			bool allowSceneActivation = asyncOperation.get_allowSceneActivation();
			LuaDLL.lua_pushboolean(L, allowSceneActivation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index allowSceneActivation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_priority(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AsyncOperation asyncOperation = (AsyncOperation)obj;
			int priority = (int)LuaDLL.luaL_checknumber(L, 2);
			asyncOperation.set_priority(priority);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index priority on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_allowSceneActivation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AsyncOperation asyncOperation = (AsyncOperation)obj;
			bool allowSceneActivation = LuaDLL.luaL_checkboolean(L, 2);
			asyncOperation.set_allowSceneActivation(allowSceneActivation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index allowSceneActivation on a nil value");
		}
		return result;
	}
}
