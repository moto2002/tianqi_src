using LuaInterface;
using System;
using System.Collections;
using UnityEngine;

public class UnityEngine_MonoBehaviourWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MonoBehaviour), typeof(Behaviour), null);
		L.RegFunction("Invoke", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.Invoke));
		L.RegFunction("InvokeRepeating", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.InvokeRepeating));
		L.RegFunction("CancelInvoke", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.CancelInvoke));
		L.RegFunction("IsInvoking", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.IsInvoking));
		L.RegFunction("StartCoroutine", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.StartCoroutine));
		L.RegFunction("StartCoroutine_Auto", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.StartCoroutine_Auto));
		L.RegFunction("StopCoroutine", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.StopCoroutine));
		L.RegFunction("StopAllCoroutines", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.StopAllCoroutines));
		L.RegFunction("print", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.print));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.Lua_ToString));
		L.RegVar("useGUILayout", new LuaCSFunction(UnityEngine_MonoBehaviourWrap.get_useGUILayout), new LuaCSFunction(UnityEngine_MonoBehaviourWrap.set_useGUILayout));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Invoke(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.CheckObject(L, 1, typeof(MonoBehaviour));
			string text = ToLua.CheckString(L, 2);
			float num = (float)LuaDLL.luaL_checknumber(L, 3);
			monoBehaviour.Invoke(text, num);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InvokeRepeating(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 4);
			MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.CheckObject(L, 1, typeof(MonoBehaviour));
			string text = ToLua.CheckString(L, 2);
			float num = (float)LuaDLL.luaL_checknumber(L, 3);
			float num2 = (float)LuaDLL.luaL_checknumber(L, 4);
			monoBehaviour.InvokeRepeating(text, num, num2);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CancelInvoke(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour)))
			{
				MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.ToObject(L, 1);
				monoBehaviour.CancelInvoke();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(string)))
			{
				MonoBehaviour monoBehaviour2 = (MonoBehaviour)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				monoBehaviour2.CancelInvoke(text);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.MonoBehaviour.CancelInvoke");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsInvoking(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour)))
			{
				MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.ToObject(L, 1);
				bool value = monoBehaviour.IsInvoking();
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(string)))
			{
				MonoBehaviour monoBehaviour2 = (MonoBehaviour)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				bool value2 = monoBehaviour2.IsInvoking(text);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.MonoBehaviour.IsInvoking");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartCoroutine(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(string)))
			{
				MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Coroutine o = monoBehaviour.StartCoroutine(text);
				ToLua.PushObject(L, o);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(IEnumerator)))
			{
				MonoBehaviour monoBehaviour2 = (MonoBehaviour)ToLua.ToObject(L, 1);
				IEnumerator enumerator = (IEnumerator)ToLua.ToObject(L, 2);
				Coroutine o2 = monoBehaviour2.StartCoroutine(enumerator);
				ToLua.PushObject(L, o2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(string), typeof(object)))
			{
				MonoBehaviour monoBehaviour3 = (MonoBehaviour)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				Coroutine o3 = monoBehaviour3.StartCoroutine(text2, obj);
				ToLua.PushObject(L, o3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.MonoBehaviour.StartCoroutine");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartCoroutine_Auto(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.CheckObject(L, 1, typeof(MonoBehaviour));
			IEnumerator enumerator = (IEnumerator)ToLua.CheckObject(L, 2, typeof(IEnumerator));
			Coroutine o = monoBehaviour.StartCoroutine_Auto(enumerator);
			ToLua.PushObject(L, o);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopCoroutine(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(Coroutine)))
			{
				MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.ToObject(L, 1);
				Coroutine coroutine = (Coroutine)ToLua.ToObject(L, 2);
				monoBehaviour.StopCoroutine(coroutine);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(IEnumerator)))
			{
				MonoBehaviour monoBehaviour2 = (MonoBehaviour)ToLua.ToObject(L, 1);
				IEnumerator enumerator = (IEnumerator)ToLua.ToObject(L, 2);
				monoBehaviour2.StopCoroutine(enumerator);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(string)))
			{
				MonoBehaviour monoBehaviour3 = (MonoBehaviour)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				monoBehaviour3.StopCoroutine(text);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.MonoBehaviour.StopCoroutine");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopAllCoroutines(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MonoBehaviour monoBehaviour = (MonoBehaviour)ToLua.CheckObject(L, 1, typeof(MonoBehaviour));
			monoBehaviour.StopAllCoroutines();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int print(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			object obj = ToLua.ToVarObject(L, 1);
			MonoBehaviour.print(obj);
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

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useGUILayout(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			MonoBehaviour monoBehaviour = (MonoBehaviour)obj;
			bool useGUILayout = monoBehaviour.get_useGUILayout();
			LuaDLL.lua_pushboolean(L, useGUILayout);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useGUILayout on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useGUILayout(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			MonoBehaviour monoBehaviour = (MonoBehaviour)obj;
			bool useGUILayout = LuaDLL.luaL_checkboolean(L, 2);
			monoBehaviour.set_useGUILayout(useGUILayout);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useGUILayout on a nil value");
		}
		return result;
	}
}
