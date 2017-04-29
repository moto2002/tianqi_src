using LuaInterface;
using System;

public class LuaInterface_EventObjectWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(EventObject), typeof(object), null);
		L.RegFunction("__add", new LuaCSFunction(LuaInterface_EventObjectWrap.op_Addition));
		L.RegFunction("__sub", new LuaCSFunction(LuaInterface_EventObjectWrap.op_Subtraction));
		L.RegFunction("__tostring", new LuaCSFunction(LuaInterface_EventObjectWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int op_Subtraction(IntPtr L)
	{
		int result;
		try
		{
			EventObject eventObject = (EventObject)ToLua.CheckObject(L, 1, typeof(EventObject));
			eventObject.func = ToLua.CheckLuaFunction(L, 2);
			eventObject.op = EventOp.Sub;
			ToLua.Push(L, eventObject);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int op_Addition(IntPtr L)
	{
		int result;
		try
		{
			EventObject eventObject = (EventObject)ToLua.CheckObject(L, 1, typeof(EventObject));
			eventObject.func = ToLua.CheckLuaFunction(L, 2);
			eventObject.op = EventOp.Add;
			ToLua.Push(L, eventObject);
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
