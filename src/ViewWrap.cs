using LuaInterface;
using System;
using UnityEngine;

public class ViewWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(View), typeof(Base), null);
		L.RegFunction("OnMessage", new LuaCSFunction(ViewWrap.OnMessage));
		L.RegFunction("__eq", new LuaCSFunction(ViewWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(ViewWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnMessage(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			View view = (View)ToLua.CheckObject(L, 1, typeof(View));
			IMessage message = (IMessage)ToLua.CheckObject(L, 2, typeof(IMessage));
			view.OnMessage(message);
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
