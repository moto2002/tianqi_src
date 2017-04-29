using LuaInterface;
using System;
using System.Collections;
using UnityEngine;

public class UnityEngine_AnimationWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Animation), typeof(Behaviour), null);
		L.RegFunction("Stop", new LuaCSFunction(UnityEngine_AnimationWrap.Stop));
		L.RegFunction("Rewind", new LuaCSFunction(UnityEngine_AnimationWrap.Rewind));
		L.RegFunction("Sample", new LuaCSFunction(UnityEngine_AnimationWrap.Sample));
		L.RegFunction("IsPlaying", new LuaCSFunction(UnityEngine_AnimationWrap.IsPlaying));
		L.RegFunction("get_Item", new LuaCSFunction(UnityEngine_AnimationWrap.get_Item));
		L.RegFunction("Play", new LuaCSFunction(UnityEngine_AnimationWrap.Play));
		L.RegFunction("CrossFade", new LuaCSFunction(UnityEngine_AnimationWrap.CrossFade));
		L.RegFunction("Blend", new LuaCSFunction(UnityEngine_AnimationWrap.Blend));
		L.RegFunction("CrossFadeQueued", new LuaCSFunction(UnityEngine_AnimationWrap.CrossFadeQueued));
		L.RegFunction("PlayQueued", new LuaCSFunction(UnityEngine_AnimationWrap.PlayQueued));
		L.RegFunction("AddClip", new LuaCSFunction(UnityEngine_AnimationWrap.AddClip));
		L.RegFunction("RemoveClip", new LuaCSFunction(UnityEngine_AnimationWrap.RemoveClip));
		L.RegFunction("GetClipCount", new LuaCSFunction(UnityEngine_AnimationWrap.GetClipCount));
		L.RegFunction("SyncLayer", new LuaCSFunction(UnityEngine_AnimationWrap.SyncLayer));
		L.RegFunction("GetEnumerator", new LuaCSFunction(UnityEngine_AnimationWrap.GetEnumerator));
		L.RegFunction("GetClip", new LuaCSFunction(UnityEngine_AnimationWrap.GetClip));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_AnimationWrap._CreateUnityEngine_Animation));
		L.RegVar("this", new LuaCSFunction(UnityEngine_AnimationWrap._this), null);
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_AnimationWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_AnimationWrap.Lua_ToString));
		L.RegVar("clip", new LuaCSFunction(UnityEngine_AnimationWrap.get_clip), new LuaCSFunction(UnityEngine_AnimationWrap.set_clip));
		L.RegVar("playAutomatically", new LuaCSFunction(UnityEngine_AnimationWrap.get_playAutomatically), new LuaCSFunction(UnityEngine_AnimationWrap.set_playAutomatically));
		L.RegVar("wrapMode", new LuaCSFunction(UnityEngine_AnimationWrap.get_wrapMode), new LuaCSFunction(UnityEngine_AnimationWrap.set_wrapMode));
		L.RegVar("isPlaying", new LuaCSFunction(UnityEngine_AnimationWrap.get_isPlaying), null);
		L.RegVar("animatePhysics", new LuaCSFunction(UnityEngine_AnimationWrap.get_animatePhysics), new LuaCSFunction(UnityEngine_AnimationWrap.set_animatePhysics));
		L.RegVar("cullingType", new LuaCSFunction(UnityEngine_AnimationWrap.get_cullingType), new LuaCSFunction(UnityEngine_AnimationWrap.set_cullingType));
		L.RegVar("localBounds", new LuaCSFunction(UnityEngine_AnimationWrap.get_localBounds), new LuaCSFunction(UnityEngine_AnimationWrap.set_localBounds));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Animation(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				Animation obj = new Animation();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Animation.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _get_this(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			string text = ToLua.CheckString(L, 2);
			AnimationState obj = animation.get_Item(text);
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
	private static int _this(IntPtr L)
	{
		int result;
		try
		{
			LuaDLL.lua_pushvalue(L, 1);
			LuaDLL.tolua_bindthis(L, new LuaCSFunction(UnityEngine_AnimationWrap._get_this), null);
			result = 1;
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
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Animation)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				animation.Stop();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				animation2.Stop(text);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.Stop");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Rewind(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Animation)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				animation.Rewind();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				animation2.Rewind(text);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.Rewind");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Sample(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			animation.Sample();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsPlaying(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			string text = ToLua.CheckString(L, 2);
			bool value = animation.IsPlaying(text);
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
	private static int get_Item(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			string text = ToLua.CheckString(L, 2);
			AnimationState obj = animation.get_Item(text);
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
	private static int Play(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Animation)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				bool value = animation.Play();
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				bool value2 = animation2.Play(text);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(PlayMode)))
			{
				Animation animation3 = (Animation)ToLua.ToObject(L, 1);
				PlayMode playMode = (int)ToLua.ToObject(L, 2);
				bool value3 = animation3.Play(playMode);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(PlayMode)))
			{
				Animation animation4 = (Animation)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				PlayMode playMode2 = (int)ToLua.ToObject(L, 3);
				bool value4 = animation4.Play(text2, playMode2);
				LuaDLL.lua_pushboolean(L, value4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.Play");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CrossFade(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				animation.CrossFade(text);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(float)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				animation2.CrossFade(text2, num2);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(float), typeof(PlayMode)))
			{
				Animation animation3 = (Animation)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				PlayMode playMode = (int)ToLua.ToObject(L, 4);
				animation3.CrossFade(text3, num3, playMode);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.CrossFade");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Blend(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				animation.Blend(text);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(float)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				animation2.Blend(text2, num2);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(float), typeof(float)))
			{
				Animation animation3 = (Animation)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				animation3.Blend(text3, num3, num4);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.Blend");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CrossFadeQueued(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				AnimationState obj = animation.CrossFadeQueued(text);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(float)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				AnimationState obj2 = animation2.CrossFadeQueued(text2, num2);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(float), typeof(QueueMode)))
			{
				Animation animation3 = (Animation)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				QueueMode queueMode = (int)ToLua.ToObject(L, 4);
				AnimationState obj3 = animation3.CrossFadeQueued(text3, num3, queueMode);
				ToLua.Push(L, obj3);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(float), typeof(QueueMode), typeof(PlayMode)))
			{
				Animation animation4 = (Animation)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				float num4 = (float)LuaDLL.lua_tonumber(L, 3);
				QueueMode queueMode2 = (int)ToLua.ToObject(L, 4);
				PlayMode playMode = (int)ToLua.ToObject(L, 5);
				AnimationState obj4 = animation4.CrossFadeQueued(text4, num4, queueMode2, playMode);
				ToLua.Push(L, obj4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.CrossFadeQueued");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayQueued(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				AnimationState obj = animation.PlayQueued(text);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(QueueMode)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				QueueMode queueMode = (int)ToLua.ToObject(L, 3);
				AnimationState obj2 = animation2.PlayQueued(text2, queueMode);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string), typeof(QueueMode), typeof(PlayMode)))
			{
				Animation animation3 = (Animation)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				QueueMode queueMode2 = (int)ToLua.ToObject(L, 3);
				PlayMode playMode = (int)ToLua.ToObject(L, 4);
				AnimationState obj3 = animation3.PlayQueued(text3, queueMode2, playMode);
				ToLua.Push(L, obj3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.PlayQueued");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddClip(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(AnimationClip), typeof(string)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				AnimationClip animationClip = (AnimationClip)ToLua.ToObject(L, 2);
				string text = ToLua.ToString(L, 3);
				animation.AddClip(animationClip, text);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(AnimationClip), typeof(string), typeof(int), typeof(int)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				AnimationClip animationClip2 = (AnimationClip)ToLua.ToObject(L, 2);
				string text2 = ToLua.ToString(L, 3);
				int num2 = (int)LuaDLL.lua_tonumber(L, 4);
				int num3 = (int)LuaDLL.lua_tonumber(L, 5);
				animation2.AddClip(animationClip2, text2, num2, num3);
				result = 0;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(AnimationClip), typeof(string), typeof(int), typeof(int), typeof(bool)))
			{
				Animation animation3 = (Animation)ToLua.ToObject(L, 1);
				AnimationClip animationClip3 = (AnimationClip)ToLua.ToObject(L, 2);
				string text3 = ToLua.ToString(L, 3);
				int num4 = (int)LuaDLL.lua_tonumber(L, 4);
				int num5 = (int)LuaDLL.lua_tonumber(L, 5);
				bool flag = LuaDLL.lua_toboolean(L, 6);
				animation3.AddClip(animationClip3, text3, num4, num5, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.AddClip");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveClip(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation = (Animation)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				animation.RemoveClip(text);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Animation), typeof(AnimationClip)))
			{
				Animation animation2 = (Animation)ToLua.ToObject(L, 1);
				AnimationClip animationClip = (AnimationClip)ToLua.ToObject(L, 2);
				animation2.RemoveClip(animationClip);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Animation.RemoveClip");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClipCount(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			int clipCount = animation.GetClipCount();
			LuaDLL.lua_pushinteger(L, clipCount);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SyncLayer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			int num = (int)LuaDLL.luaL_checknumber(L, 2);
			animation.SyncLayer(num);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetEnumerator(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			IEnumerator enumerator = animation.GetEnumerator();
			ToLua.Push(L, enumerator);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClip(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Animation animation = (Animation)ToLua.CheckObject(L, 1, typeof(Animation));
			string text = ToLua.CheckString(L, 2);
			AnimationClip clip = animation.GetClip(text);
			ToLua.Push(L, clip);
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

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clip(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			AnimationClip clip = animation.get_clip();
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
	private static int get_playAutomatically(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			bool playAutomatically = animation.get_playAutomatically();
			LuaDLL.lua_pushboolean(L, playAutomatically);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index playAutomatically on a nil value");
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
			Animation animation = (Animation)obj;
			WrapMode wrapMode = animation.get_wrapMode();
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
	private static int get_isPlaying(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			bool isPlaying = animation.get_isPlaying();
			LuaDLL.lua_pushboolean(L, isPlaying);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isPlaying on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_animatePhysics(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			bool animatePhysics = animation.get_animatePhysics();
			LuaDLL.lua_pushboolean(L, animatePhysics);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index animatePhysics on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cullingType(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			AnimationCullingType cullingType = animation.get_cullingType();
			ToLua.Push(L, cullingType);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cullingType on a nil value");
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
			Animation animation = (Animation)obj;
			Bounds localBounds = animation.get_localBounds();
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
	private static int set_clip(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			AnimationClip clip = (AnimationClip)ToLua.CheckUnityObject(L, 2, typeof(AnimationClip));
			animation.set_clip(clip);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index clip on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_playAutomatically(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			bool playAutomatically = LuaDLL.luaL_checkboolean(L, 2);
			animation.set_playAutomatically(playAutomatically);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index playAutomatically on a nil value");
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
			Animation animation = (Animation)obj;
			WrapMode wrapMode = (int)ToLua.CheckObject(L, 2, typeof(WrapMode));
			animation.set_wrapMode(wrapMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index wrapMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_animatePhysics(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			bool animatePhysics = LuaDLL.luaL_checkboolean(L, 2);
			animation.set_animatePhysics(animatePhysics);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index animatePhysics on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cullingType(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Animation animation = (Animation)obj;
			AnimationCullingType cullingType = (int)ToLua.CheckObject(L, 2, typeof(AnimationCullingType));
			animation.set_cullingType(cullingType);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cullingType on a nil value");
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
			Animation animation = (Animation)obj;
			Bounds localBounds = ToLua.ToBounds(L, 2);
			animation.set_localBounds(localBounds);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localBounds on a nil value");
		}
		return result;
	}
}
