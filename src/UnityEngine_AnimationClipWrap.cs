using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_AnimationClipWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AnimationClip), typeof(Object), null);
		L.RegFunction("SampleAnimation", new LuaCSFunction(UnityEngine_AnimationClipWrap.SampleAnimation));
		L.RegFunction("SetCurve", new LuaCSFunction(UnityEngine_AnimationClipWrap.SetCurve));
		L.RegFunction("EnsureQuaternionContinuity", new LuaCSFunction(UnityEngine_AnimationClipWrap.EnsureQuaternionContinuity));
		L.RegFunction("ClearCurves", new LuaCSFunction(UnityEngine_AnimationClipWrap.ClearCurves));
		L.RegFunction("AddEvent", new LuaCSFunction(UnityEngine_AnimationClipWrap.AddEvent));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_AnimationClipWrap._CreateUnityEngine_AnimationClip));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_AnimationClipWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_AnimationClipWrap.Lua_ToString));
		L.RegVar("length", new LuaCSFunction(UnityEngine_AnimationClipWrap.get_length), null);
		L.RegVar("frameRate", new LuaCSFunction(UnityEngine_AnimationClipWrap.get_frameRate), new LuaCSFunction(UnityEngine_AnimationClipWrap.set_frameRate));
		L.RegVar("wrapMode", new LuaCSFunction(UnityEngine_AnimationClipWrap.get_wrapMode), new LuaCSFunction(UnityEngine_AnimationClipWrap.set_wrapMode));
		L.RegVar("localBounds", new LuaCSFunction(UnityEngine_AnimationClipWrap.get_localBounds), new LuaCSFunction(UnityEngine_AnimationClipWrap.set_localBounds));
		L.RegVar("legacy", new LuaCSFunction(UnityEngine_AnimationClipWrap.get_legacy), new LuaCSFunction(UnityEngine_AnimationClipWrap.set_legacy));
		L.RegVar("humanMotion", new LuaCSFunction(UnityEngine_AnimationClipWrap.get_humanMotion), null);
		L.RegVar("events", new LuaCSFunction(UnityEngine_AnimationClipWrap.get_events), new LuaCSFunction(UnityEngine_AnimationClipWrap.set_events));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_AnimationClip(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				AnimationClip obj = new AnimationClip();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.AnimationClip.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SampleAnimation(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			AnimationClip animationClip = (AnimationClip)ToLua.CheckObject(L, 1, typeof(AnimationClip));
			GameObject gameObject = (GameObject)ToLua.CheckUnityObject(L, 2, typeof(GameObject));
			float num = (float)LuaDLL.luaL_checknumber(L, 3);
			animationClip.SampleAnimation(gameObject, num);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetCurve(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 5);
			AnimationClip animationClip = (AnimationClip)ToLua.CheckObject(L, 1, typeof(AnimationClip));
			string text = ToLua.CheckString(L, 2);
			Type type = (Type)ToLua.CheckObject(L, 3, typeof(Type));
			string text2 = ToLua.CheckString(L, 4);
			AnimationCurve animationCurve = (AnimationCurve)ToLua.CheckObject(L, 5, typeof(AnimationCurve));
			animationClip.SetCurve(text, type, text2, animationCurve);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EnsureQuaternionContinuity(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AnimationClip animationClip = (AnimationClip)ToLua.CheckObject(L, 1, typeof(AnimationClip));
			animationClip.EnsureQuaternionContinuity();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearCurves(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AnimationClip animationClip = (AnimationClip)ToLua.CheckObject(L, 1, typeof(AnimationClip));
			animationClip.ClearCurves();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddEvent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			AnimationClip animationClip = (AnimationClip)ToLua.CheckObject(L, 1, typeof(AnimationClip));
			AnimationEvent animationEvent = (AnimationEvent)ToLua.CheckObject(L, 2, typeof(AnimationEvent));
			animationClip.AddEvent(animationEvent);
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
	private static int get_length(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			float length = animationClip.get_length();
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
	private static int get_frameRate(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			float frameRate = animationClip.get_frameRate();
			LuaDLL.lua_pushnumber(L, (double)frameRate);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index frameRate on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_wrapMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			WrapMode wrapMode = animationClip.get_wrapMode();
			ToLua.Push(L, wrapMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index wrapMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localBounds(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			Bounds localBounds = animationClip.get_localBounds();
			ToLua.Push(L, localBounds);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localBounds on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_legacy(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			bool legacy = animationClip.get_legacy();
			LuaDLL.lua_pushboolean(L, legacy);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index legacy on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_humanMotion(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			bool humanMotion = animationClip.get_humanMotion();
			LuaDLL.lua_pushboolean(L, humanMotion);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index humanMotion on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_events(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			AnimationEvent[] events = animationClip.get_events();
			ToLua.Push(L, events);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index events on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_frameRate(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			float frameRate = (float)LuaDLL.luaL_checknumber(L, 2);
			animationClip.set_frameRate(frameRate);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index frameRate on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_wrapMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			WrapMode wrapMode = (int)ToLua.CheckObject(L, 2, typeof(WrapMode));
			animationClip.set_wrapMode(wrapMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index wrapMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localBounds(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			Bounds localBounds = ToLua.ToBounds(L, 2);
			animationClip.set_localBounds(localBounds);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localBounds on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_legacy(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			bool legacy = LuaDLL.luaL_checkboolean(L, 2);
			animationClip.set_legacy(legacy);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index legacy on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_events(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationClip animationClip = (AnimationClip)obj;
			AnimationEvent[] events = ToLua.CheckObjectArray<AnimationEvent>(L, 2);
			animationClip.set_events(events);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index events on a nil value");
		}
		return result;
	}
}
