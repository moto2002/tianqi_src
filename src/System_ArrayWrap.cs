using LuaInterface;
using System;

public class System_ArrayWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Array), null, null);
		L.RegFunction(".geti", new LuaCSFunction(System_ArrayWrap.get_Item));
		L.RegFunction(".seti", new LuaCSFunction(System_ArrayWrap.set_Item));
		L.RegFunction("ToTable", new LuaCSFunction(System_ArrayWrap.ToTable));
		L.RegVar("Length", new LuaCSFunction(System_ArrayWrap.get_Length), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Item(IntPtr L)
	{
		int result;
		try
		{
			Array array = ToLua.ToObject(L, 1) as Array;
			if (array == null)
			{
				throw new LuaException("trying to index an invalid object reference", null, 1);
			}
			int num = LuaDLL.lua_tointeger(L, 2);
			if (num >= array.get_Length())
			{
				throw new LuaException(string.Concat(new object[]
				{
					"array index out of bounds: ",
					num,
					" ",
					array.get_Length()
				}), null, 1);
			}
			object value = array.GetValue(num);
			if (value == null)
			{
				throw new LuaException(string.Format("array index {0} is null", num), null, 1);
			}
			ToLua.Push(L, value);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_Item(IntPtr L)
	{
		int result;
		try
		{
			Array array = ToLua.ToObject(L, 1) as Array;
			if (array == null)
			{
				throw new LuaException("trying to index an invalid object reference", null, 1);
			}
			int num = LuaDLL.lua_tointeger(L, 2);
			object obj = ToLua.ToVarObject(L, 3);
			Type elementType = array.GetType().GetElementType();
			if (!TypeChecker.CheckType(L, elementType, 3))
			{
				throw new LuaException("trying to set object type is not correct", null, 1);
			}
			obj = Convert.ChangeType(obj, elementType);
			array.SetValue(obj, num);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Length(IntPtr L)
	{
		int result;
		try
		{
			Array array = ToLua.ToObject(L, 1) as Array;
			if (array == null)
			{
				throw new LuaException("trying to index an invalid object reference", null, 1);
			}
			LuaDLL.lua_pushinteger(L, array.get_Length());
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToTable(IntPtr L)
	{
		int result;
		try
		{
			Array array = ToLua.ToObject(L, 1) as Array;
			if (array == null)
			{
				throw new LuaException("trying to index an invalid object reference", null, 1);
			}
			LuaDLL.lua_createtable(L, array.get_Length(), 0);
			for (int i = 0; i < array.get_Length(); i++)
			{
				object value = array.GetValue(i);
				ToLua.Push(L, value);
				LuaDLL.lua_rawseti(L, -2, i);
			}
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
