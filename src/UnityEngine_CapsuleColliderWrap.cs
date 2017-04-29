using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_CapsuleColliderWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(CapsuleCollider), typeof(Collider), null);
		L.RegFunction("New", new LuaCSFunction(UnityEngine_CapsuleColliderWrap._CreateUnityEngine_CapsuleCollider));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_CapsuleColliderWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_CapsuleColliderWrap.Lua_ToString));
		L.RegVar("center", new LuaCSFunction(UnityEngine_CapsuleColliderWrap.get_center), new LuaCSFunction(UnityEngine_CapsuleColliderWrap.set_center));
		L.RegVar("radius", new LuaCSFunction(UnityEngine_CapsuleColliderWrap.get_radius), new LuaCSFunction(UnityEngine_CapsuleColliderWrap.set_radius));
		L.RegVar("height", new LuaCSFunction(UnityEngine_CapsuleColliderWrap.get_height), new LuaCSFunction(UnityEngine_CapsuleColliderWrap.set_height));
		L.RegVar("direction", new LuaCSFunction(UnityEngine_CapsuleColliderWrap.get_direction), new LuaCSFunction(UnityEngine_CapsuleColliderWrap.set_direction));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_CapsuleCollider(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				CapsuleCollider obj = new CapsuleCollider();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.CapsuleCollider.New");
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
	private static int get_center(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			Vector3 center = capsuleCollider.get_center();
			ToLua.Push(L, center);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index center on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_radius(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			float radius = capsuleCollider.get_radius();
			LuaDLL.lua_pushnumber(L, (double)radius);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index radius on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_height(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			float height = capsuleCollider.get_height();
			LuaDLL.lua_pushnumber(L, (double)height);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index height on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_direction(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			int direction = capsuleCollider.get_direction();
			LuaDLL.lua_pushinteger(L, direction);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index direction on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_center(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			Vector3 center = ToLua.ToVector3(L, 2);
			capsuleCollider.set_center(center);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index center on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_radius(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			float radius = (float)LuaDLL.luaL_checknumber(L, 2);
			capsuleCollider.set_radius(radius);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index radius on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_height(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			float height = (float)LuaDLL.luaL_checknumber(L, 2);
			capsuleCollider.set_height(height);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index height on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_direction(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			int direction = (int)LuaDLL.luaL_checknumber(L, 2);
			capsuleCollider.set_direction(direction);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index direction on a nil value");
		}
		return result;
	}
}
