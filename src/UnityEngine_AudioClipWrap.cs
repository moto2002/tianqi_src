using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_AudioClipWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AudioClip), typeof(Object), null);
		L.RegFunction("LoadAudioData", new LuaCSFunction(UnityEngine_AudioClipWrap.LoadAudioData));
		L.RegFunction("UnloadAudioData", new LuaCSFunction(UnityEngine_AudioClipWrap.UnloadAudioData));
		L.RegFunction("GetData", new LuaCSFunction(UnityEngine_AudioClipWrap.GetData));
		L.RegFunction("SetData", new LuaCSFunction(UnityEngine_AudioClipWrap.SetData));
		L.RegFunction("Create", new LuaCSFunction(UnityEngine_AudioClipWrap.Create));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_AudioClipWrap._CreateUnityEngine_AudioClip));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_AudioClipWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_AudioClipWrap.Lua_ToString));
		L.RegVar("length", new LuaCSFunction(UnityEngine_AudioClipWrap.get_length), null);
		L.RegVar("samples", new LuaCSFunction(UnityEngine_AudioClipWrap.get_samples), null);
		L.RegVar("channels", new LuaCSFunction(UnityEngine_AudioClipWrap.get_channels), null);
		L.RegVar("frequency", new LuaCSFunction(UnityEngine_AudioClipWrap.get_frequency), null);
		L.RegVar("loadType", new LuaCSFunction(UnityEngine_AudioClipWrap.get_loadType), null);
		L.RegVar("preloadAudioData", new LuaCSFunction(UnityEngine_AudioClipWrap.get_preloadAudioData), null);
		L.RegVar("loadState", new LuaCSFunction(UnityEngine_AudioClipWrap.get_loadState), null);
		L.RegVar("loadInBackground", new LuaCSFunction(UnityEngine_AudioClipWrap.get_loadInBackground), null);
		L.RegFunction("PCMReaderCallback", new LuaCSFunction(UnityEngine_AudioClipWrap.UnityEngine_AudioClip_PCMReaderCallback));
		L.RegFunction("PCMSetPositionCallback", new LuaCSFunction(UnityEngine_AudioClipWrap.UnityEngine_AudioClip_PCMSetPositionCallback));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_AudioClip(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				AudioClip obj = new AudioClip();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.AudioClip.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAudioData(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AudioClip audioClip = (AudioClip)ToLua.CheckObject(L, 1, typeof(AudioClip));
			bool value = audioClip.LoadAudioData();
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
	private static int UnloadAudioData(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AudioClip audioClip = (AudioClip)ToLua.CheckObject(L, 1, typeof(AudioClip));
			bool value = audioClip.UnloadAudioData();
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
	private static int GetData(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			AudioClip audioClip = (AudioClip)ToLua.CheckObject(L, 1, typeof(AudioClip));
			float[] array = ToLua.CheckNumberArray<float>(L, 2);
			int num = (int)LuaDLL.luaL_checknumber(L, 3);
			bool data = audioClip.GetData(array, num);
			LuaDLL.lua_pushboolean(L, data);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetData(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			AudioClip audioClip = (AudioClip)ToLua.CheckObject(L, 1, typeof(AudioClip));
			float[] array = ToLua.CheckNumberArray<float>(L, 2);
			int num = (int)LuaDLL.luaL_checknumber(L, 3);
			bool value = audioClip.SetData(array, num);
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
	private static int Create(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int), typeof(int), typeof(bool)))
			{
				string text = ToLua.ToString(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				int num4 = (int)LuaDLL.lua_tonumber(L, 4);
				bool flag = LuaDLL.lua_toboolean(L, 5);
				AudioClip obj = AudioClip.Create(text, num2, num3, num4, flag);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int), typeof(int), typeof(bool), typeof(AudioClip.PCMReaderCallback)))
			{
				string text2 = ToLua.ToString(L, 1);
				int num5 = (int)LuaDLL.lua_tonumber(L, 2);
				int num6 = (int)LuaDLL.lua_tonumber(L, 3);
				int num7 = (int)LuaDLL.lua_tonumber(L, 4);
				bool flag2 = LuaDLL.lua_toboolean(L, 5);
				LuaTypes luaTypes = LuaDLL.lua_type(L, 6);
				AudioClip.PCMReaderCallback pCMReaderCallback;
				if (luaTypes != LuaTypes.LUA_TFUNCTION)
				{
					pCMReaderCallback = (AudioClip.PCMReaderCallback)ToLua.ToObject(L, 6);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 6);
					pCMReaderCallback = (DelegateFactory.CreateDelegate(typeof(AudioClip.PCMReaderCallback), func) as AudioClip.PCMReaderCallback);
				}
				AudioClip obj2 = AudioClip.Create(text2, num5, num6, num7, flag2, pCMReaderCallback);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int), typeof(int), typeof(bool), typeof(AudioClip.PCMReaderCallback), typeof(AudioClip.PCMSetPositionCallback)))
			{
				string text3 = ToLua.ToString(L, 1);
				int num8 = (int)LuaDLL.lua_tonumber(L, 2);
				int num9 = (int)LuaDLL.lua_tonumber(L, 3);
				int num10 = (int)LuaDLL.lua_tonumber(L, 4);
				bool flag3 = LuaDLL.lua_toboolean(L, 5);
				LuaTypes luaTypes2 = LuaDLL.lua_type(L, 6);
				AudioClip.PCMReaderCallback pCMReaderCallback2;
				if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
				{
					pCMReaderCallback2 = (AudioClip.PCMReaderCallback)ToLua.ToObject(L, 6);
				}
				else
				{
					LuaFunction func2 = ToLua.ToLuaFunction(L, 6);
					pCMReaderCallback2 = (DelegateFactory.CreateDelegate(typeof(AudioClip.PCMReaderCallback), func2) as AudioClip.PCMReaderCallback);
				}
				LuaTypes luaTypes3 = LuaDLL.lua_type(L, 7);
				AudioClip.PCMSetPositionCallback pCMSetPositionCallback;
				if (luaTypes3 != LuaTypes.LUA_TFUNCTION)
				{
					pCMSetPositionCallback = (AudioClip.PCMSetPositionCallback)ToLua.ToObject(L, 7);
				}
				else
				{
					LuaFunction func3 = ToLua.ToLuaFunction(L, 7);
					pCMSetPositionCallback = (DelegateFactory.CreateDelegate(typeof(AudioClip.PCMSetPositionCallback), func3) as AudioClip.PCMSetPositionCallback);
				}
				AudioClip obj3 = AudioClip.Create(text3, num8, num9, num10, flag3, pCMReaderCallback2, pCMSetPositionCallback);
				ToLua.Push(L, obj3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AudioClip.Create");
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
	private static int get_length(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			float length = audioClip.get_length();
			LuaDLL.lua_pushnumber(L, (double)length);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index length on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_samples(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			int samples = audioClip.get_samples();
			LuaDLL.lua_pushinteger(L, samples);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index samples on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_channels(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			int channels = audioClip.get_channels();
			LuaDLL.lua_pushinteger(L, channels);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index channels on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_frequency(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			int frequency = audioClip.get_frequency();
			LuaDLL.lua_pushinteger(L, frequency);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index frequency on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadType(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			AudioClipLoadType loadType = audioClip.get_loadType();
			ToLua.Push(L, loadType);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index loadType on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_preloadAudioData(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			bool preloadAudioData = audioClip.get_preloadAudioData();
			LuaDLL.lua_pushboolean(L, preloadAudioData);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index preloadAudioData on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadState(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			AudioDataLoadState loadState = audioClip.get_loadState();
			ToLua.Push(L, loadState);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index loadState on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadInBackground(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AudioClip audioClip = (AudioClip)obj;
			bool loadInBackground = audioClip.get_loadInBackground();
			LuaDLL.lua_pushboolean(L, loadInBackground);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index loadInBackground on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_AudioClip_PCMReaderCallback(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(AudioClip.PCMReaderCallback), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_AudioClip_PCMSetPositionCallback(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(AudioClip.PCMSetPositionCallback), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
