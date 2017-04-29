using LuaInterface;
using System;

public class System_ObjectWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(object), null, null);
		L.RegFunction("Equals", new LuaCSFunction(System_ObjectWrap.Equals));
		L.RegFunction("GetHashCode", new LuaCSFunction(System_ObjectWrap.GetHashCode));
		L.RegFunction("GetType", new LuaCSFunction(System_ObjectWrap.GetType));
		L.RegFunction("ToString", new LuaCSFunction(System_ObjectWrap.ToString));
		L.RegFunction("ReferenceEquals", new LuaCSFunction(System_ObjectWrap.ReferenceEquals));
		L.RegFunction("Destroy", new LuaCSFunction(System_ObjectWrap.Destroy));
		L.RegFunction("New", new LuaCSFunction(System_ObjectWrap._CreateSystem_Object));
		L.RegFunction("__eq", new LuaCSFunction(System_ObjectWrap.op_Equality));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSystem_Object(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				object obj = new object();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: System.Object.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Equals(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			object obj = ToLua.CheckObject(L, 1);
			object obj2 = ToLua.ToVarObject(L, 2);
			bool value = (obj == null) ? (obj2 == null) : obj.Equals(obj2);
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
	private static int GetHashCode(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			object obj = ToLua.CheckObject(L, 1);
			int hashCode = obj.GetHashCode();
			LuaDLL.lua_pushinteger(L, hashCode);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetType(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			object obj = ToLua.CheckObject(L, 1);
			Type type = obj.GetType();
			ToLua.Push(L, type);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToString(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			object obj = ToLua.CheckObject(L, 1);
			string str = obj.ToString();
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReferenceEquals(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			object obj = ToLua.ToVarObject(L, 1);
			object obj2 = ToLua.ToVarObject(L, 2);
			bool value = object.ReferenceEquals(obj, obj2);
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
	private static int op_Equality(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			object obj = ToLua.ToVarObject(L, 1);
			object obj2 = ToLua.ToVarObject(L, 2);
			bool value = obj == obj2;
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
	private static int Destroy(IntPtr L)
	{
		return ToLua.Destroy(L);
	}
}
