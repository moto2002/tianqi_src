using LuaInterface;
using System;
using System.Reflection;
using System.Runtime.Serialization;

public class System_DelegateWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Delegate), typeof(object), null);
		L.RegFunction("CreateDelegate", new LuaCSFunction(System_DelegateWrap.CreateDelegate));
		L.RegFunction("DynamicInvoke", new LuaCSFunction(System_DelegateWrap.DynamicInvoke));
		L.RegFunction("Clone", new LuaCSFunction(System_DelegateWrap.Clone));
		L.RegFunction("GetObjectData", new LuaCSFunction(System_DelegateWrap.GetObjectData));
		L.RegFunction("GetInvocationList", new LuaCSFunction(System_DelegateWrap.GetInvocationList));
		L.RegFunction("Combine", new LuaCSFunction(System_DelegateWrap.Combine));
		L.RegFunction("Remove", new LuaCSFunction(System_DelegateWrap.Remove));
		L.RegFunction("RemoveAll", new LuaCSFunction(System_DelegateWrap.RemoveAll));
		L.RegFunction("Destroy", new LuaCSFunction(System_DelegateWrap.Destroy));
		L.RegFunction("GetHashCode", new LuaCSFunction(System_DelegateWrap.GetHashCode));
		L.RegFunction("Equals", new LuaCSFunction(System_DelegateWrap.Equals));
		L.RegFunction("__add", new LuaCSFunction(System_DelegateWrap.op_Addition));
		L.RegFunction("__sub", new LuaCSFunction(System_DelegateWrap.op_Subtraction));
		L.RegFunction("__eq", new LuaCSFunction(System_DelegateWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(System_DelegateWrap.Lua_ToString));
		L.RegVar("Method", new LuaCSFunction(System_DelegateWrap.get_Method), null);
		L.RegVar("Target", new LuaCSFunction(System_DelegateWrap.get_Target), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreateDelegate(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(MethodInfo)))
			{
				Type type = (Type)ToLua.ToObject(L, 1);
				MethodInfo methodInfo = (MethodInfo)ToLua.ToObject(L, 2);
				Delegate ev = Delegate.CreateDelegate(type, methodInfo);
				ToLua.Push(L, ev);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(MethodInfo), typeof(bool)))
			{
				Type type2 = (Type)ToLua.ToObject(L, 1);
				MethodInfo methodInfo2 = (MethodInfo)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Delegate ev2 = Delegate.CreateDelegate(type2, methodInfo2, flag);
				ToLua.Push(L, ev2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(Type), typeof(string)))
			{
				Type type3 = (Type)ToLua.ToObject(L, 1);
				Type type4 = (Type)ToLua.ToObject(L, 2);
				string text = ToLua.ToString(L, 3);
				Delegate ev3 = Delegate.CreateDelegate(type3, type4, text);
				ToLua.Push(L, ev3);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(object), typeof(string)))
			{
				Type type5 = (Type)ToLua.ToObject(L, 1);
				object obj = ToLua.ToVarObject(L, 2);
				string text2 = ToLua.ToString(L, 3);
				Delegate ev4 = Delegate.CreateDelegate(type5, obj, text2);
				ToLua.Push(L, ev4);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(object), typeof(MethodInfo)))
			{
				Type type6 = (Type)ToLua.ToObject(L, 1);
				object obj2 = ToLua.ToVarObject(L, 2);
				MethodInfo methodInfo3 = (MethodInfo)ToLua.ToObject(L, 3);
				Delegate ev5 = Delegate.CreateDelegate(type6, obj2, methodInfo3);
				ToLua.Push(L, ev5);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(Type), typeof(string), typeof(bool)))
			{
				Type type7 = (Type)ToLua.ToObject(L, 1);
				Type type8 = (Type)ToLua.ToObject(L, 2);
				string text3 = ToLua.ToString(L, 3);
				bool flag2 = LuaDLL.lua_toboolean(L, 4);
				Delegate ev6 = Delegate.CreateDelegate(type7, type8, text3, flag2);
				ToLua.Push(L, ev6);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(object), typeof(string), typeof(bool)))
			{
				Type type9 = (Type)ToLua.ToObject(L, 1);
				object obj3 = ToLua.ToVarObject(L, 2);
				string text4 = ToLua.ToString(L, 3);
				bool flag3 = LuaDLL.lua_toboolean(L, 4);
				Delegate ev7 = Delegate.CreateDelegate(type9, obj3, text4, flag3);
				ToLua.Push(L, ev7);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(object), typeof(MethodInfo), typeof(bool)))
			{
				Type type10 = (Type)ToLua.ToObject(L, 1);
				object obj4 = ToLua.ToVarObject(L, 2);
				MethodInfo methodInfo4 = (MethodInfo)ToLua.ToObject(L, 3);
				bool flag4 = LuaDLL.lua_toboolean(L, 4);
				Delegate ev8 = Delegate.CreateDelegate(type10, obj4, methodInfo4, flag4);
				ToLua.Push(L, ev8);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(Type), typeof(string), typeof(bool), typeof(bool)))
			{
				Type type11 = (Type)ToLua.ToObject(L, 1);
				Type type12 = (Type)ToLua.ToObject(L, 2);
				string text5 = ToLua.ToString(L, 3);
				bool flag5 = LuaDLL.lua_toboolean(L, 4);
				bool flag6 = LuaDLL.lua_toboolean(L, 5);
				Delegate ev9 = Delegate.CreateDelegate(type11, type12, text5, flag5, flag6);
				ToLua.Push(L, ev9);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Type), typeof(object), typeof(string), typeof(bool), typeof(bool)))
			{
				Type type13 = (Type)ToLua.ToObject(L, 1);
				object obj5 = ToLua.ToVarObject(L, 2);
				string text6 = ToLua.ToString(L, 3);
				bool flag7 = LuaDLL.lua_toboolean(L, 4);
				bool flag8 = LuaDLL.lua_toboolean(L, 5);
				Delegate ev10 = Delegate.CreateDelegate(type13, obj5, text6, flag7, flag8);
				ToLua.Push(L, ev10);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.Delegate.CreateDelegate");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DynamicInvoke(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			object[] array = ToLua.ToParamsObject(L, 2, num - 1);
			object obj = @delegate.DynamicInvoke(array);
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
	private static int Clone(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			object obj = @delegate.Clone();
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
	private static int GetObjectData(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			SerializationInfo serializationInfo = (SerializationInfo)ToLua.CheckObject(L, 2, typeof(SerializationInfo));
			StreamingContext streamingContext = (StreamingContext)ToLua.CheckObject(L, 3, typeof(StreamingContext));
			@delegate.GetObjectData(serializationInfo, streamingContext);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetInvocationList(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			Delegate[] invocationList = @delegate.GetInvocationList();
			ToLua.Push(L, invocationList);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Combine(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Delegate), typeof(Delegate)))
			{
				Delegate @delegate = (Delegate)ToLua.ToObject(L, 1);
				Delegate delegate2 = (Delegate)ToLua.ToObject(L, 2);
				Delegate ev = Delegate.Combine(@delegate, delegate2);
				ToLua.Push(L, ev);
				result = 1;
			}
			else if (TypeChecker.CheckParamsType(L, typeof(Delegate), 1, num))
			{
				Delegate[] array = ToLua.ToParamsObject<Delegate>(L, 1, num);
				Delegate ev2 = Delegate.Combine(array);
				ToLua.Push(L, ev2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.Delegate.Combine");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Remove(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			Delegate delegate2 = (Delegate)ToLua.CheckObject(L, 2, typeof(Delegate));
			Delegate ev = Delegate.Remove(@delegate, delegate2);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveAll(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			Delegate delegate2 = (Delegate)ToLua.CheckObject(L, 2, typeof(Delegate));
			Delegate ev = Delegate.RemoveAll(@delegate, delegate2);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int op_Subtraction(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
			if (luaTypes == LuaTypes.LUA_TFUNCTION)
			{
				LuaState luaState = LuaState.Get(L);
				LuaFunction luaFunction = ToLua.ToLuaFunction(L, 2);
				Delegate[] invocationList = @delegate.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					LuaDelegate luaDelegate = invocationList[i].get_Target() as LuaDelegate;
					if (luaDelegate != null && luaDelegate.func == luaFunction)
					{
						@delegate = Delegate.Remove(@delegate, invocationList[i]);
						luaState.DelayDispose(luaDelegate.func);
						break;
					}
				}
				luaFunction.Dispose();
				ToLua.Push(L, @delegate);
				result = 1;
			}
			else
			{
				Delegate delegate2 = (Delegate)ToLua.CheckObject(L, 2, typeof(Delegate));
				@delegate = Delegate.Remove(@delegate, delegate2);
				ToLua.Push(L, @delegate);
				result = 1;
			}
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
			ToLua.CheckArgsCount(L, 2);
			Delegate @delegate = ToLua.CheckObject(L, 1, typeof(Delegate)) as Delegate;
			LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
			if (luaTypes == LuaTypes.LUA_TFUNCTION)
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				Type type = @delegate.GetType();
				Delegate delegate2 = DelegateFactory.CreateDelegate(type, func);
				Delegate ev = Delegate.Combine(@delegate, delegate2);
				ToLua.Push(L, ev);
				result = 1;
			}
			else
			{
				Delegate delegate3 = ToLua.ToObject(L, 2) as Delegate;
				Delegate ev2 = Delegate.Combine(@delegate, delegate3);
				ToLua.Push(L, ev2);
				result = 1;
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
			Delegate @delegate = (Delegate)ToLua.ToObject(L, 1);
			Delegate delegate2 = (Delegate)ToLua.ToObject(L, 2);
			bool value = @delegate == delegate2;
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
		Delegate @delegate = ToLua.CheckObject(L, 1, typeof(Delegate)) as Delegate;
		Delegate[] invocationList = @delegate.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			LuaDelegate luaDelegate = invocationList[i].get_Target() as LuaDelegate;
			if (luaDelegate != null && luaDelegate.func != null)
			{
				luaDelegate.func.Dispose();
			}
		}
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetHashCode(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			int hashCode = @delegate.GetHashCode();
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
	private static int Equals(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Delegate @delegate = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
			object obj = ToLua.ToVarObject(L, 2);
			bool value = (@delegate == null) ? (obj == null) : @delegate.Equals(obj);
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
	private static int get_Method(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Delegate @delegate = (Delegate)obj;
			MethodInfo method = @delegate.get_Method();
			ToLua.PushObject(L, method);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index Method on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Target(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Delegate @delegate = (Delegate)obj;
			object target = @delegate.get_Target();
			ToLua.Push(L, target);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index Target on a nil value");
		}
		return result;
	}
}
