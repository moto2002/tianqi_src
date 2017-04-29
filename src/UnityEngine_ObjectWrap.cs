using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_ObjectWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Object), typeof(object), null);
		L.RegFunction("Equals", new LuaCSFunction(UnityEngine_ObjectWrap.Equals));
		L.RegFunction("GetHashCode", new LuaCSFunction(UnityEngine_ObjectWrap.GetHashCode));
		L.RegFunction("GetInstanceID", new LuaCSFunction(UnityEngine_ObjectWrap.GetInstanceID));
		L.RegFunction("FindObjectsOfType", new LuaCSFunction(UnityEngine_ObjectWrap.FindObjectsOfType));
		L.RegFunction("FindObjectOfType", new LuaCSFunction(UnityEngine_ObjectWrap.FindObjectOfType));
		L.RegFunction("DontDestroyOnLoad", new LuaCSFunction(UnityEngine_ObjectWrap.DontDestroyOnLoad));
		L.RegFunction("ToString", new LuaCSFunction(UnityEngine_ObjectWrap.ToString));
		L.RegFunction("Instantiate", new LuaCSFunction(UnityEngine_ObjectWrap.Instantiate));
		L.RegFunction("DestroyImmediate", new LuaCSFunction(UnityEngine_ObjectWrap.DestroyImmediate));
		L.RegFunction("Destroy", new LuaCSFunction(UnityEngine_ObjectWrap.Destroy));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_ObjectWrap._CreateUnityEngine_Object));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_ObjectWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_ObjectWrap.Lua_ToString));
		L.RegVar("name", new LuaCSFunction(UnityEngine_ObjectWrap.get_name), new LuaCSFunction(UnityEngine_ObjectWrap.set_name));
		L.RegVar("hideFlags", new LuaCSFunction(UnityEngine_ObjectWrap.get_hideFlags), new LuaCSFunction(UnityEngine_ObjectWrap.set_hideFlags));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Object(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				Object obj = new Object();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Object.New");
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
			Object @object = (Object)ToLua.CheckObject(L, 1, typeof(Object));
			object obj = ToLua.ToVarObject(L, 2);
			bool value = (!(@object != null)) ? (obj == null) : @object.Equals(obj);
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
			Object @object = (Object)ToLua.CheckObject(L, 1, typeof(Object));
			int hashCode = @object.GetHashCode();
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
	private static int GetInstanceID(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Object @object = (Object)ToLua.CheckObject(L, 1, typeof(Object));
			int instanceID = @object.GetInstanceID();
			LuaDLL.lua_pushinteger(L, instanceID);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindObjectsOfType(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			Object[] array = Object.FindObjectsOfType(type);
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
	private static int FindObjectOfType(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Type type = (Type)ToLua.CheckObject(L, 1, typeof(Type));
			Object obj = Object.FindObjectOfType(type);
			ToLua.Push(L, obj);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DontDestroyOnLoad(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Object @object = ToLua.CheckUnityObject(L, 1, typeof(Object));
			Object.DontDestroyOnLoad(@object);
			result = 0;
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
			Object @object = (Object)ToLua.CheckObject(L, 1, typeof(Object));
			string str = @object.ToString();
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
	private static int Instantiate(IntPtr L)
	{
		int result;
		try
		{
			LuaException.InstantiateCount++;
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Object)))
			{
				Object @object = (Object)ToLua.ToObject(L, 1);
				Object obj = Object.Instantiate(@object);
				if (LuaDLL.lua_toboolean(L, LuaDLL.lua_upvalueindex(1)))
				{
					string msg = LuaDLL.lua_tostring(L, -1);
					LuaDLL.lua_pop(L, 1);
					throw new LuaException(msg, LuaException.luaStack, 1);
				}
				ToLua.Push(L, obj);
				LuaException.InstantiateCount--;
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Object), typeof(Vector3), typeof(Quaternion)))
			{
				Object object2 = (Object)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Quaternion quaternion = ToLua.ToQuaternion(L, 3);
				Object obj2 = Object.Instantiate(object2, vector, quaternion);
				if (LuaDLL.lua_toboolean(L, LuaDLL.lua_upvalueindex(1)))
				{
					string msg2 = LuaDLL.lua_tostring(L, -1);
					LuaDLL.lua_pop(L, 1);
					throw new LuaException(msg2, LuaException.luaStack, 1);
				}
				ToLua.Push(L, obj2);
				LuaException.InstantiateCount--;
				result = 1;
			}
			else
			{
				LuaException.InstantiateCount--;
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Object.Instantiate");
			}
		}
		catch (Exception e)
		{
			LuaException.InstantiateCount--;
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DestroyImmediate(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1)
			{
				Object @object = (Object)ToLua.CheckObject(L, 1);
				ToLua.Destroy(L);
				Object.DestroyImmediate(@object);
				result = 0;
			}
			else if (num == 2)
			{
				Object object2 = (Object)ToLua.CheckObject(L, 1);
				bool flag = LuaDLL.luaL_checkboolean(L, 2);
				ToLua.Destroy(L);
				Object.DestroyImmediate(object2, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: Object.DestroyImmediate");
			}
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
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1)
			{
				Object @object = (Object)ToLua.CheckObject(L, 1);
				ToLua.Destroy(L);
				Object.Destroy(@object);
				result = 0;
			}
			else if (num == 2)
			{
				float time = (float)LuaDLL.luaL_checknumber(L, 2);
				int id = LuaDLL.tolua_rawnetobj(L, 1);
				ObjectTranslator translator = LuaState.GetTranslator(L);
				translator.DelayDestroy(id, time);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: Object.Destroy");
			}
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
	private static int get_name(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Object @object = (Object)obj;
			string name = @object.get_name();
			LuaDLL.lua_pushstring(L, name);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index name on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hideFlags(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Object @object = (Object)obj;
			HideFlags hideFlags = @object.get_hideFlags();
			ToLua.Push(L, hideFlags);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index hideFlags on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_name(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Object @object = (Object)obj;
			string name = ToLua.CheckString(L, 2);
			@object.set_name(name);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index name on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_hideFlags(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Object @object = (Object)obj;
			HideFlags hideFlags = (int)ToLua.CheckObject(L, 2, typeof(HideFlags));
			@object.set_hideFlags(hideFlags);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index hideFlags on a nil value");
		}
		return result;
	}
}
