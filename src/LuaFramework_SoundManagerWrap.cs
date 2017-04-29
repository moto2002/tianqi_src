using LuaFramework;
using LuaInterface;
using System;
using UnityEngine;

public class LuaFramework_SoundManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(SoundManager), typeof(Manager), null);
		L.RegFunction("LoadAudioClip", new LuaCSFunction(LuaFramework_SoundManagerWrap.LoadAudioClip));
		L.RegFunction("CanPlayBackSound", new LuaCSFunction(LuaFramework_SoundManagerWrap.CanPlayBackSound));
		L.RegFunction("PlayBacksound", new LuaCSFunction(LuaFramework_SoundManagerWrap.PlayBacksound));
		L.RegFunction("CanPlaySoundEffect", new LuaCSFunction(LuaFramework_SoundManagerWrap.CanPlaySoundEffect));
		L.RegFunction("Play", new LuaCSFunction(LuaFramework_SoundManagerWrap.Play));
		L.RegFunction("__eq", new LuaCSFunction(LuaFramework_SoundManagerWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_SoundManagerWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAudioClip(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SoundManager soundManager = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			string path = ToLua.CheckString(L, 2);
			AudioClip obj = soundManager.LoadAudioClip(path);
			ToLua.Push(L, obj);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CanPlayBackSound(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SoundManager soundManager = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			bool value = soundManager.CanPlayBackSound();
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
	private static int PlayBacksound(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SoundManager soundManager = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			string name = ToLua.CheckString(L, 2);
			bool canPlay = LuaDLL.luaL_checkboolean(L, 3);
			soundManager.PlayBacksound(name, canPlay);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CanPlaySoundEffect(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SoundManager soundManager = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			bool value = soundManager.CanPlaySoundEffect();
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
	private static int Play(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SoundManager soundManager = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			AudioClip clip = (AudioClip)ToLua.CheckUnityObject(L, 2, typeof(AudioClip));
			Vector3 position = ToLua.ToVector3(L, 3);
			soundManager.Play(clip, position);
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
