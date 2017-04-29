using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.Experimental.Director;

public class UnityEngine_Experimental_Director_DirectorPlayerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(DirectorPlayer), typeof(Behaviour), null);
		L.RegFunction("Play", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.Play));
		L.RegFunction("Stop", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.Stop));
		L.RegFunction("SetTime", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.SetTime));
		L.RegFunction("GetTime", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.GetTime));
		L.RegFunction("SetTimeUpdateMode", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.SetTimeUpdateMode));
		L.RegFunction("GetTimeUpdateMode", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.GetTimeUpdateMode));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap._CreateUnityEngine_Experimental_Director_DirectorPlayer));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_Experimental_Director_DirectorPlayerWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Experimental_Director_DirectorPlayer(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				DirectorPlayer obj = new DirectorPlayer();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Experimental.Director.DirectorPlayer.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Play(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(DirectorPlayer), typeof(Playable)))
			{
				DirectorPlayer directorPlayer = (DirectorPlayer)ToLua.ToObject(L, 1);
				Playable playable = (Playable)ToLua.ToObject(L, 2);
				directorPlayer.Play(playable);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(DirectorPlayer), typeof(Playable), typeof(object)))
			{
				DirectorPlayer directorPlayer2 = (DirectorPlayer)ToLua.ToObject(L, 1);
				Playable playable2 = (Playable)ToLua.ToObject(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				directorPlayer2.Play(playable2, obj);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Experimental.Director.DirectorPlayer.Play");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Stop(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			DirectorPlayer directorPlayer = (DirectorPlayer)ToLua.CheckObject(L, 1, typeof(DirectorPlayer));
			directorPlayer.Stop();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTime(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			DirectorPlayer directorPlayer = (DirectorPlayer)ToLua.CheckObject(L, 1, typeof(DirectorPlayer));
			double time = LuaDLL.luaL_checknumber(L, 2);
			directorPlayer.SetTime(time);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTime(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			DirectorPlayer directorPlayer = (DirectorPlayer)ToLua.CheckObject(L, 1, typeof(DirectorPlayer));
			double time = directorPlayer.GetTime();
			LuaDLL.lua_pushnumber(L, time);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTimeUpdateMode(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			DirectorPlayer directorPlayer = (DirectorPlayer)ToLua.CheckObject(L, 1, typeof(DirectorPlayer));
			DirectorUpdateMode timeUpdateMode = (int)ToLua.CheckObject(L, 2, typeof(DirectorUpdateMode));
			directorPlayer.SetTimeUpdateMode(timeUpdateMode);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTimeUpdateMode(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			DirectorPlayer directorPlayer = (DirectorPlayer)ToLua.CheckObject(L, 1, typeof(DirectorPlayer));
			DirectorUpdateMode timeUpdateMode = directorPlayer.GetTimeUpdateMode();
			ToLua.Push(L, timeUpdateMode);
			result = 1;
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
