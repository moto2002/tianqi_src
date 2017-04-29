using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_ParticleAnimatorWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ParticleAnimator), typeof(Component), null);
		L.RegFunction("New", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap._CreateUnityEngine_ParticleAnimator));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.Lua_ToString));
		L.RegVar("doesAnimateColor", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_doesAnimateColor), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_doesAnimateColor));
		L.RegVar("worldRotationAxis", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_worldRotationAxis), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_worldRotationAxis));
		L.RegVar("localRotationAxis", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_localRotationAxis), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_localRotationAxis));
		L.RegVar("sizeGrow", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_sizeGrow), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_sizeGrow));
		L.RegVar("rndForce", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_rndForce), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_rndForce));
		L.RegVar("force", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_force), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_force));
		L.RegVar("damping", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_damping), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_damping));
		L.RegVar("autodestruct", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_autodestruct), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_autodestruct));
		L.RegVar("colorAnimation", new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.get_colorAnimation), new LuaCSFunction(UnityEngine_ParticleAnimatorWrap.set_colorAnimation));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_ParticleAnimator(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				ParticleAnimator obj = new ParticleAnimator();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.ParticleAnimator.New");
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
	private static int get_doesAnimateColor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			bool doesAnimateColor = particleAnimator.get_doesAnimateColor();
			LuaDLL.lua_pushboolean(L, doesAnimateColor);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index doesAnimateColor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldRotationAxis(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 worldRotationAxis = particleAnimator.get_worldRotationAxis();
			ToLua.Push(L, worldRotationAxis);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldRotationAxis on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localRotationAxis(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 localRotationAxis = particleAnimator.get_localRotationAxis();
			ToLua.Push(L, localRotationAxis);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localRotationAxis on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sizeGrow(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			float sizeGrow = particleAnimator.get_sizeGrow();
			LuaDLL.lua_pushnumber(L, (double)sizeGrow);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sizeGrow on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rndForce(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 rndForce = particleAnimator.get_rndForce();
			ToLua.Push(L, rndForce);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndForce on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_force(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 force = particleAnimator.get_force();
			ToLua.Push(L, force);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index force on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_damping(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			float damping = particleAnimator.get_damping();
			LuaDLL.lua_pushnumber(L, (double)damping);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index damping on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_autodestruct(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			bool autodestruct = particleAnimator.get_autodestruct();
			LuaDLL.lua_pushboolean(L, autodestruct);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index autodestruct on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_colorAnimation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Color[] colorAnimation = particleAnimator.get_colorAnimation();
			ToLua.Push(L, colorAnimation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index colorAnimation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_doesAnimateColor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			bool doesAnimateColor = LuaDLL.luaL_checkboolean(L, 2);
			particleAnimator.set_doesAnimateColor(doesAnimateColor);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index doesAnimateColor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_worldRotationAxis(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 worldRotationAxis = ToLua.ToVector3(L, 2);
			particleAnimator.set_worldRotationAxis(worldRotationAxis);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldRotationAxis on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localRotationAxis(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 localRotationAxis = ToLua.ToVector3(L, 2);
			particleAnimator.set_localRotationAxis(localRotationAxis);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localRotationAxis on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sizeGrow(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			float sizeGrow = (float)LuaDLL.luaL_checknumber(L, 2);
			particleAnimator.set_sizeGrow(sizeGrow);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sizeGrow on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rndForce(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 rndForce = ToLua.ToVector3(L, 2);
			particleAnimator.set_rndForce(rndForce);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndForce on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_force(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Vector3 force = ToLua.ToVector3(L, 2);
			particleAnimator.set_force(force);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index force on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_damping(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			float damping = (float)LuaDLL.luaL_checknumber(L, 2);
			particleAnimator.set_damping(damping);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index damping on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_autodestruct(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			bool autodestruct = LuaDLL.luaL_checkboolean(L, 2);
			particleAnimator.set_autodestruct(autodestruct);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index autodestruct on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_colorAnimation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleAnimator particleAnimator = (ParticleAnimator)obj;
			Color[] colorAnimation = ToLua.CheckObjectArray<Color>(L, 2);
			particleAnimator.set_colorAnimation(colorAnimation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index colorAnimation on a nil value");
		}
		return result;
	}
}
