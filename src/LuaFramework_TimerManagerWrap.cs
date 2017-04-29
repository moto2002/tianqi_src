using LuaFramework;
using LuaInterface;
using System;
using UnityEngine;

public class LuaFramework_TimerManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(TimerManager), typeof(Manager), null);
		L.RegFunction("StartTimer", new LuaCSFunction(LuaFramework_TimerManagerWrap.StartTimer));
		L.RegFunction("StopTimer", new LuaCSFunction(LuaFramework_TimerManagerWrap.StopTimer));
		L.RegFunction("AddTimerEvent", new LuaCSFunction(LuaFramework_TimerManagerWrap.AddTimerEvent));
		L.RegFunction("RemoveTimerEvent", new LuaCSFunction(LuaFramework_TimerManagerWrap.RemoveTimerEvent));
		L.RegFunction("StopTimerEvent", new LuaCSFunction(LuaFramework_TimerManagerWrap.StopTimerEvent));
		L.RegFunction("ResumeTimerEvent", new LuaCSFunction(LuaFramework_TimerManagerWrap.ResumeTimerEvent));
		L.RegFunction("__eq", new LuaCSFunction(LuaFramework_TimerManagerWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_TimerManagerWrap.Lua_ToString));
		L.RegVar("Interval", new LuaCSFunction(LuaFramework_TimerManagerWrap.get_Interval), new LuaCSFunction(LuaFramework_TimerManagerWrap.set_Interval));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartTimer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			TimerManager timerManager = (TimerManager)ToLua.CheckObject(L, 1, typeof(TimerManager));
			float value = (float)LuaDLL.luaL_checknumber(L, 2);
			timerManager.StartTimer(value);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopTimer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			TimerManager timerManager = (TimerManager)ToLua.CheckObject(L, 1, typeof(TimerManager));
			timerManager.StopTimer();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddTimerEvent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			TimerManager timerManager = (TimerManager)ToLua.CheckObject(L, 1, typeof(TimerManager));
			TimerInfo info = (TimerInfo)ToLua.CheckObject(L, 2, typeof(TimerInfo));
			timerManager.AddTimerEvent(info);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveTimerEvent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			TimerManager timerManager = (TimerManager)ToLua.CheckObject(L, 1, typeof(TimerManager));
			TimerInfo info = (TimerInfo)ToLua.CheckObject(L, 2, typeof(TimerInfo));
			timerManager.RemoveTimerEvent(info);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopTimerEvent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			TimerManager timerManager = (TimerManager)ToLua.CheckObject(L, 1, typeof(TimerManager));
			TimerInfo info = (TimerInfo)ToLua.CheckObject(L, 2, typeof(TimerInfo));
			timerManager.StopTimerEvent(info);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResumeTimerEvent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			TimerManager timerManager = (TimerManager)ToLua.CheckObject(L, 1, typeof(TimerManager));
			TimerInfo info = (TimerInfo)ToLua.CheckObject(L, 2, typeof(TimerInfo));
			timerManager.ResumeTimerEvent(info);
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

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Interval(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			TimerManager timerManager = (TimerManager)obj;
			float interval = timerManager.Interval;
			LuaDLL.lua_pushnumber(L, (double)interval);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index Interval on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_Interval(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			TimerManager timerManager = (TimerManager)obj;
			float interval = (float)LuaDLL.luaL_checknumber(L, 2);
			timerManager.Interval = interval;
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index Interval on a nil value");
		}
		return result;
	}
}
