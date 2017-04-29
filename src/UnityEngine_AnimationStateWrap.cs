using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_AnimationStateWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AnimationState), typeof(TrackedReference), null);
		L.RegFunction("AddMixingTransform", new LuaCSFunction(UnityEngine_AnimationStateWrap.AddMixingTransform));
		L.RegFunction("RemoveMixingTransform", new LuaCSFunction(UnityEngine_AnimationStateWrap.RemoveMixingTransform));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_AnimationStateWrap._CreateUnityEngine_AnimationState));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_AnimationStateWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_AnimationStateWrap.Lua_ToString));
		L.RegVar("enabled", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_enabled), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_enabled));
		L.RegVar("weight", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_weight), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_weight));
		L.RegVar("wrapMode", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_wrapMode), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_wrapMode));
		L.RegVar("time", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_time), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_time));
		L.RegVar("normalizedTime", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_normalizedTime), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_normalizedTime));
		L.RegVar("speed", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_speed), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_speed));
		L.RegVar("normalizedSpeed", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_normalizedSpeed), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_normalizedSpeed));
		L.RegVar("length", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_length), null);
		L.RegVar("layer", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_layer), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_layer));
		L.RegVar("clip", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_clip), null);
		L.RegVar("name", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_name), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_name));
		L.RegVar("blendMode", new LuaCSFunction(UnityEngine_AnimationStateWrap.get_blendMode), new LuaCSFunction(UnityEngine_AnimationStateWrap.set_blendMode));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_AnimationState(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				AnimationState obj = new AnimationState();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.AnimationState.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddMixingTransform(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(AnimationState), typeof(Transform)))
			{
				AnimationState animationState = (AnimationState)ToLua.ToObject(L, 1);
				Transform transform = (Transform)ToLua.ToObject(L, 2);
				animationState.AddMixingTransform(transform);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(AnimationState), typeof(Transform), typeof(bool)))
			{
				AnimationState animationState2 = (AnimationState)ToLua.ToObject(L, 1);
				Transform transform2 = (Transform)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				animationState2.AddMixingTransform(transform2, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AnimationState.AddMixingTransform");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveMixingTransform(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			AnimationState animationState = (AnimationState)ToLua.CheckObject(L, 1, typeof(AnimationState));
			Transform transform = (Transform)ToLua.CheckUnityObject(L, 2, typeof(Transform));
			animationState.RemoveMixingTransform(transform);
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
			TrackedReference trackedReference = (TrackedReference)ToLua.ToObject(L, 1);
			TrackedReference trackedReference2 = (TrackedReference)ToLua.ToObject(L, 2);
			bool value = trackedReference == trackedReference2;
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
			AnimationState animationState = (AnimationState)obj;
			bool enabled = animationState.get_enabled();
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
	private static int get_weight(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float weight = animationState.get_weight();
			LuaDLL.lua_pushnumber(L, (double)weight);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index weight on a nil value");
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
			AnimationState animationState = (AnimationState)obj;
			WrapMode wrapMode = animationState.get_wrapMode();
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
	private static int get_time(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float time = animationState.get_time();
			LuaDLL.lua_pushnumber(L, (double)time);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index time on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_normalizedTime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float normalizedTime = animationState.get_normalizedTime();
			LuaDLL.lua_pushnumber(L, (double)normalizedTime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index normalizedTime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_speed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float speed = animationState.get_speed();
			LuaDLL.lua_pushnumber(L, (double)speed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index speed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_normalizedSpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float normalizedSpeed = animationState.get_normalizedSpeed();
			LuaDLL.lua_pushnumber(L, (double)normalizedSpeed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index normalizedSpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_length(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float length = animationState.get_length();
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
	private static int get_layer(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			int layer = animationState.get_layer();
			LuaDLL.lua_pushinteger(L, layer);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layer on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clip(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			AnimationClip clip = animationState.get_clip();
			ToLua.Push(L, clip);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index clip on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_name(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			string name = animationState.get_name();
			LuaDLL.lua_pushstring(L, name);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index name on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_blendMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			AnimationBlendMode blendMode = animationState.get_blendMode();
			ToLua.Push(L, blendMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index blendMode on a nil value");
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
			AnimationState animationState = (AnimationState)obj;
			bool enabled = LuaDLL.luaL_checkboolean(L, 2);
			animationState.set_enabled(enabled);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index enabled on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_weight(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float weight = (float)LuaDLL.luaL_checknumber(L, 2);
			animationState.set_weight(weight);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index weight on a nil value");
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
			AnimationState animationState = (AnimationState)obj;
			WrapMode wrapMode = (int)ToLua.CheckObject(L, 2, typeof(WrapMode));
			animationState.set_wrapMode(wrapMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index wrapMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_time(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float time = (float)LuaDLL.luaL_checknumber(L, 2);
			animationState.set_time(time);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index time on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_normalizedTime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float normalizedTime = (float)LuaDLL.luaL_checknumber(L, 2);
			animationState.set_normalizedTime(normalizedTime);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index normalizedTime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_speed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float speed = (float)LuaDLL.luaL_checknumber(L, 2);
			animationState.set_speed(speed);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index speed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_normalizedSpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			float normalizedSpeed = (float)LuaDLL.luaL_checknumber(L, 2);
			animationState.set_normalizedSpeed(normalizedSpeed);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index normalizedSpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_layer(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			int layer = (int)LuaDLL.luaL_checknumber(L, 2);
			animationState.set_layer(layer);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layer on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_name(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			string name = ToLua.CheckString(L, 2);
			animationState.set_name(name);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index name on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_blendMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AnimationState animationState = (AnimationState)obj;
			AnimationBlendMode blendMode = (int)ToLua.CheckObject(L, 2, typeof(AnimationBlendMode));
			animationState.set_blendMode(blendMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index blendMode on a nil value");
		}
		return result;
	}
}
