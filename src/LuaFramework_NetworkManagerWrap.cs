using LuaFramework;
using LuaInterface;
using System;
using UnityEngine;

public class LuaFramework_NetworkManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(NetworkManager), typeof(Manager), null);
		L.RegFunction("OnInit", new LuaCSFunction(LuaFramework_NetworkManagerWrap.OnInit));
		L.RegFunction("Unload", new LuaCSFunction(LuaFramework_NetworkManagerWrap.Unload));
		L.RegFunction("CallMethod", new LuaCSFunction(LuaFramework_NetworkManagerWrap.CallMethod));
		L.RegFunction("AddEvent", new LuaCSFunction(LuaFramework_NetworkManagerWrap.AddEvent));
		L.RegFunction("SendConnect", new LuaCSFunction(LuaFramework_NetworkManagerWrap.SendConnect));
		L.RegFunction("SendMessage", new LuaCSFunction(LuaFramework_NetworkManagerWrap.SendMessage));
		L.RegFunction("__eq", new LuaCSFunction(LuaFramework_NetworkManagerWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_NetworkManagerWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnInit(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			NetworkManager networkManager = (NetworkManager)ToLua.CheckObject(L, 1, typeof(NetworkManager));
			networkManager.OnInit();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Unload(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			NetworkManager networkManager = (NetworkManager)ToLua.CheckObject(L, 1, typeof(NetworkManager));
			networkManager.Unload();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CallMethod(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			NetworkManager networkManager = (NetworkManager)ToLua.CheckObject(L, 1, typeof(NetworkManager));
			string func = ToLua.CheckString(L, 2);
			object[] args = ToLua.ToParamsObject(L, 3, num - 2);
			object[] array = networkManager.CallMethod(func, args);
			ToLua.Push(L, array);
			result = 1;
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
			int @event = (int)LuaDLL.luaL_checknumber(L, 1);
			ByteBuffer data = (ByteBuffer)ToLua.CheckObject(L, 2, typeof(ByteBuffer));
			NetworkManager.AddEvent(@event, data);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SendConnect(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			NetworkManager networkManager = (NetworkManager)ToLua.CheckObject(L, 1, typeof(NetworkManager));
			networkManager.SendConnect();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SendMessage(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			NetworkManager networkManager = (NetworkManager)ToLua.CheckObject(L, 1, typeof(NetworkManager));
			ByteBuffer buffer = (ByteBuffer)ToLua.CheckObject(L, 2, typeof(ByteBuffer));
			networkManager.SendMessage(buffer);
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
