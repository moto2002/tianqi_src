using LuaInterface;
using System;

public class System_EnumWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Enum), null, null);
		L.RegFunction("GetTypeCode", new LuaCSFunction(System_EnumWrap.GetTypeCode));
		L.RegFunction("GetValues", new LuaCSFunction(System_EnumWrap.GetValues));
		L.RegFunction("GetNames", new LuaCSFunction(System_EnumWrap.GetNames));
		L.RegFunction("GetName", new LuaCSFunction(System_EnumWrap.GetName));
		L.RegFunction("IsDefined", new LuaCSFunction(System_EnumWrap.IsDefined));
		L.RegFunction("GetUnderlyingType", new LuaCSFunction(System_EnumWrap.GetUnderlyingType));
		L.RegFunction("Parse", new LuaCSFunction(System_EnumWrap.Parse));
		L.RegFunction("CompareTo", new LuaCSFunction(System_EnumWrap.CompareTo));
		L.RegFunction("ToString", new LuaCSFunction(System_EnumWrap.ToString));
		L.RegFunction("ToObject", new LuaCSFunction(System_EnumWrap.ToObject));
		L.RegFunction("Equals", new LuaCSFunction(System_EnumWrap.Equals));
		L.RegFunction("GetHashCode", new LuaCSFunction(System_EnumWrap.GetHashCode));
		L.RegFunction("Format", new LuaCSFunction(System_EnumWrap.Format));
		L.RegFunction("ToInt", new LuaCSFunction(System_EnumWrap.ToInt));
		L.RegFunction("__tostring", new LuaCSFunction(System_EnumWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTypeCode(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Enum @enum = (Enum)ToLua.CheckObject(L, 1, typeof(Enum));
			TypeCode typeCode = @enum.GetTypeCode();
			ToLua.Push(L, typeCode);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetValues(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			Array values = Enum.GetValues(type);
			ToLua.Push(L, values);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetNames(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			string[] names = Enum.GetNames(type);
			ToLua.Push(L, names);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetName(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			object obj = ToLua.ToVarObject(L, 2);
			string name = Enum.GetName(type, obj);
			LuaDLL.lua_pushstring(L, name);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsDefined(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			object obj = ToLua.ToVarObject(L, 2);
			bool value = Enum.IsDefined(type, obj);
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
	private static int GetUnderlyingType(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			Type underlyingType = Enum.GetUnderlyingType(type);
			ToLua.Push(L, underlyingType);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Parse(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(string)))
			{
				Type type = (Type)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				object obj = Enum.Parse(type, text);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(string), typeof(bool)))
			{
				Type type2 = (Type)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				object obj2 = Enum.Parse(type2, text2, flag);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.Enum.Parse");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareTo(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Enum @enum = (Enum)ToLua.CheckObject(L, 1, typeof(Enum));
			object obj = ToLua.ToVarObject(L, 2);
			int n = @enum.CompareTo(obj);
			LuaDLL.lua_pushinteger(L, n);
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
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Enum)))
			{
				Enum @enum = (Enum)ToLua.ToObject(L, 1);
				string str = @enum.ToString();
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Enum), typeof(string)))
			{
				Enum enum2 = (Enum)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				string str2 = enum2.ToString(text);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.Enum.ToString");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToObject(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(long)))
			{
				Type type = (Type)ToLua.ToObject(L, 1);
				long num2 = (long)LuaDLL.lua_tonumber(L, 2);
				object obj = Enum.ToObject(type, num2);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(object)))
			{
				Type type2 = (Type)ToLua.ToObject(L, 1);
				object obj2 = ToLua.ToVarObject(L, 2);
				object obj3 = Enum.ToObject(type2, obj2);
				ToLua.Push(L, obj3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.Enum.ToObject");
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
			Enum @enum = (Enum)ToLua.CheckObject(L, 1, typeof(Enum));
			object obj = ToLua.ToVarObject(L, 2);
			bool value = (@enum == null) ? (obj == null) : @enum.Equals(obj);
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
			Enum @enum = (Enum)ToLua.CheckObject(L, 1, typeof(Enum));
			int hashCode = @enum.GetHashCode();
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
	private static int Format(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			object obj = ToLua.ToVarObject(L, 2);
			string text = ToLua.CheckString(L, 3);
			string str = Enum.Format(type, obj, text);
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
	private static int ToInt(IntPtr L)
	{
		int result;
		try
		{
			object obj = ToLua.CheckObject(L, 1, typeof(Enum));
			int n = Convert.ToInt32(obj);
			LuaDLL.lua_pushinteger(L, n);
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
