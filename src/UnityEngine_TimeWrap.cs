using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_TimeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Time");
		L.RegVar("time", new LuaCSFunction(UnityEngine_TimeWrap.get_time), null);
		L.RegVar("timeSinceLevelLoad", new LuaCSFunction(UnityEngine_TimeWrap.get_timeSinceLevelLoad), null);
		L.RegVar("deltaTime", new LuaCSFunction(UnityEngine_TimeWrap.get_deltaTime), null);
		L.RegVar("fixedTime", new LuaCSFunction(UnityEngine_TimeWrap.get_fixedTime), null);
		L.RegVar("unscaledTime", new LuaCSFunction(UnityEngine_TimeWrap.get_unscaledTime), null);
		L.RegVar("unscaledDeltaTime", new LuaCSFunction(UnityEngine_TimeWrap.get_unscaledDeltaTime), null);
		L.RegVar("fixedDeltaTime", new LuaCSFunction(UnityEngine_TimeWrap.get_fixedDeltaTime), new LuaCSFunction(UnityEngine_TimeWrap.set_fixedDeltaTime));
		L.RegVar("maximumDeltaTime", new LuaCSFunction(UnityEngine_TimeWrap.get_maximumDeltaTime), new LuaCSFunction(UnityEngine_TimeWrap.set_maximumDeltaTime));
		L.RegVar("smoothDeltaTime", new LuaCSFunction(UnityEngine_TimeWrap.get_smoothDeltaTime), null);
		L.RegVar("timeScale", new LuaCSFunction(UnityEngine_TimeWrap.get_timeScale), new LuaCSFunction(UnityEngine_TimeWrap.set_timeScale));
		L.RegVar("frameCount", new LuaCSFunction(UnityEngine_TimeWrap.get_frameCount), null);
		L.RegVar("renderedFrameCount", new LuaCSFunction(UnityEngine_TimeWrap.get_renderedFrameCount), null);
		L.RegVar("realtimeSinceStartup", new LuaCSFunction(UnityEngine_TimeWrap.get_realtimeSinceStartup), null);
		L.RegVar("captureFramerate", new LuaCSFunction(UnityEngine_TimeWrap.get_captureFramerate), new LuaCSFunction(UnityEngine_TimeWrap.set_captureFramerate));
		L.EndStaticLibs();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_time(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_time());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_timeSinceLevelLoad(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_timeSinceLevelLoad());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_deltaTime(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_deltaTime());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fixedTime(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_fixedTime());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_unscaledTime(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_unscaledTime());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_unscaledDeltaTime(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_unscaledDeltaTime());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fixedDeltaTime(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_fixedDeltaTime());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maximumDeltaTime(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_maximumDeltaTime());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_smoothDeltaTime(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_smoothDeltaTime());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_timeScale(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_timeScale());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_frameCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Time.get_frameCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_renderedFrameCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Time.get_renderedFrameCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_realtimeSinceStartup(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Time.get_realtimeSinceStartup());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_captureFramerate(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Time.get_captureFramerate());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fixedDeltaTime(IntPtr L)
	{
		int result;
		try
		{
			float fixedDeltaTime = (float)LuaDLL.luaL_checknumber(L, 2);
			Time.set_fixedDeltaTime(fixedDeltaTime);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maximumDeltaTime(IntPtr L)
	{
		int result;
		try
		{
			float maximumDeltaTime = (float)LuaDLL.luaL_checknumber(L, 2);
			Time.set_maximumDeltaTime(maximumDeltaTime);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_timeScale(IntPtr L)
	{
		int result;
		try
		{
			float timeScale = (float)LuaDLL.luaL_checknumber(L, 2);
			Time.set_timeScale(timeScale);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_captureFramerate(IntPtr L)
	{
		int result;
		try
		{
			int captureFramerate = (int)LuaDLL.luaL_checknumber(L, 2);
			Time.set_captureFramerate(captureFramerate);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
