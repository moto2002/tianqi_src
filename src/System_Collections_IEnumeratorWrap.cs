using LuaInterface;
using System;
using System.Collections;

public class System_Collections_IEnumeratorWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(IEnumerator), null, null);
		L.RegFunction("MoveNext", new LuaCSFunction(System_Collections_IEnumeratorWrap.MoveNext));
		L.RegFunction("Reset", new LuaCSFunction(System_Collections_IEnumeratorWrap.Reset));
		L.RegVar("Current", new LuaCSFunction(System_Collections_IEnumeratorWrap.get_Current), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int MoveNext(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			IEnumerator enumerator = (IEnumerator)ToLua.CheckObject(L, 1, typeof(IEnumerator));
			bool value = enumerator.MoveNext();
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
	private static int Reset(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			IEnumerator enumerator = (IEnumerator)ToLua.CheckObject(L, 1, typeof(IEnumerator));
			enumerator.Reset();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Current(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			IEnumerator enumerator = (IEnumerator)obj;
			object current = enumerator.get_Current();
			ToLua.Push(L, current);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index Current on a nil value");
		}
		return result;
	}
}
