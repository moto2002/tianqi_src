using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityEngine_ComponentWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Component), typeof(Object), null);
		L.RegFunction("GetComponent", new LuaCSFunction(UnityEngine_ComponentWrap.GetComponent));
		L.RegFunction("GetComponentInChildren", new LuaCSFunction(UnityEngine_ComponentWrap.GetComponentInChildren));
		L.RegFunction("GetComponentsInChildren", new LuaCSFunction(UnityEngine_ComponentWrap.GetComponentsInChildren));
		L.RegFunction("GetComponentInParent", new LuaCSFunction(UnityEngine_ComponentWrap.GetComponentInParent));
		L.RegFunction("GetComponentsInParent", new LuaCSFunction(UnityEngine_ComponentWrap.GetComponentsInParent));
		L.RegFunction("GetComponents", new LuaCSFunction(UnityEngine_ComponentWrap.GetComponents));
		L.RegFunction("CompareTag", new LuaCSFunction(UnityEngine_ComponentWrap.CompareTag));
		L.RegFunction("SendMessageUpwards", new LuaCSFunction(UnityEngine_ComponentWrap.SendMessageUpwards));
		L.RegFunction("SendMessage", new LuaCSFunction(UnityEngine_ComponentWrap.SendMessage));
		L.RegFunction("BroadcastMessage", new LuaCSFunction(UnityEngine_ComponentWrap.BroadcastMessage));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_ComponentWrap._CreateUnityEngine_Component));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_ComponentWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_ComponentWrap.Lua_ToString));
		L.RegVar("transform", new LuaCSFunction(UnityEngine_ComponentWrap.get_transform), null);
		L.RegVar("gameObject", new LuaCSFunction(UnityEngine_ComponentWrap.get_gameObject), null);
		L.RegVar("tag", new LuaCSFunction(UnityEngine_ComponentWrap.get_tag), new LuaCSFunction(UnityEngine_ComponentWrap.set_tag));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Component(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				Component obj = new Component();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Component.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponent(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Component component2 = component.GetComponent(text);
				ToLua.Push(L, component2);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type)))
			{
				Component component3 = (Component)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component component4 = component3.GetComponent(type);
				ToLua.Push(L, component4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.GetComponent");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentInChildren(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component componentInChildren = component.GetComponentInChildren(type);
				ToLua.Push(L, componentInChildren);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type), typeof(bool)))
			{
				Component component2 = (Component)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Component componentInChildren2 = component2.GetComponentInChildren(type2, flag);
				ToLua.Push(L, componentInChildren2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.GetComponentInChildren");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentsInChildren(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component[] componentsInChildren = component.GetComponentsInChildren(type);
				ToLua.Push(L, componentsInChildren);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type), typeof(bool)))
			{
				Component component2 = (Component)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Component[] componentsInChildren2 = component2.GetComponentsInChildren(type2, flag);
				ToLua.Push(L, componentsInChildren2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.GetComponentsInChildren");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentInParent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Component component = (Component)ToLua.CheckObject(L, 1, typeof(Component));
			Type type = (Type)ToLua.CheckObject(L, 2, typeof(Type));
			Component componentInParent = component.GetComponentInParent(type);
			ToLua.Push(L, componentInParent);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentsInParent(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component[] componentsInParent = component.GetComponentsInParent(type);
				ToLua.Push(L, componentsInParent);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type), typeof(bool)))
			{
				Component component2 = (Component)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Component[] componentsInParent2 = component2.GetComponentsInParent(type2, flag);
				ToLua.Push(L, componentsInParent2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.GetComponentsInParent");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponents(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component[] components = component.GetComponents(type);
				ToLua.Push(L, components);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(Type), typeof(List<Component>)))
			{
				Component component2 = (Component)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				List<Component> list = (List<Component>)ToLua.ToObject(L, 3);
				component2.GetComponents(type2, list);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.GetComponents");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareTag(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Component component = (Component)ToLua.CheckObject(L, 1, typeof(Component));
			string text = ToLua.CheckString(L, 2);
			bool value = component.CompareTag(text);
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
	private static int SendMessageUpwards(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				component.SendMessageUpwards(text);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
			{
				Component component2 = (Component)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				SendMessageOptions sendMessageOptions = (int)ToLua.ToObject(L, 3);
				component2.SendMessageUpwards(text2, sendMessageOptions);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object)))
			{
				Component component3 = (Component)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				component3.SendMessageUpwards(text3, obj);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object), typeof(SendMessageOptions)))
			{
				Component component4 = (Component)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				object obj2 = ToLua.ToVarObject(L, 3);
				SendMessageOptions sendMessageOptions2 = (int)ToLua.ToObject(L, 4);
				component4.SendMessageUpwards(text4, obj2, sendMessageOptions2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.SendMessageUpwards");
			}
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
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				component.SendMessage(text);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
			{
				Component component2 = (Component)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				SendMessageOptions sendMessageOptions = (int)ToLua.ToObject(L, 3);
				component2.SendMessage(text2, sendMessageOptions);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object)))
			{
				Component component3 = (Component)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				component3.SendMessage(text3, obj);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object), typeof(SendMessageOptions)))
			{
				Component component4 = (Component)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				object obj2 = ToLua.ToVarObject(L, 3);
				SendMessageOptions sendMessageOptions2 = (int)ToLua.ToObject(L, 4);
				component4.SendMessage(text4, obj2, sendMessageOptions2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.SendMessage");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BroadcastMessage(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string)))
			{
				Component component = (Component)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				component.BroadcastMessage(text);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
			{
				Component component2 = (Component)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				SendMessageOptions sendMessageOptions = (int)ToLua.ToObject(L, 3);
				component2.BroadcastMessage(text2, sendMessageOptions);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object)))
			{
				Component component3 = (Component)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				component3.BroadcastMessage(text3, obj);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object), typeof(SendMessageOptions)))
			{
				Component component4 = (Component)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				object obj2 = ToLua.ToVarObject(L, 3);
				SendMessageOptions sendMessageOptions2 = (int)ToLua.ToObject(L, 4);
				component4.BroadcastMessage(text4, obj2, sendMessageOptions2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Component.BroadcastMessage");
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
	private static int get_transform(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Component component = (Component)obj;
			Transform transform = component.get_transform();
			ToLua.Push(L, transform);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index transform on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gameObject(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Component component = (Component)obj;
			GameObject gameObject = component.get_gameObject();
			ToLua.Push(L, gameObject);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index gameObject on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_tag(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Component component = (Component)obj;
			string tag = component.get_tag();
			LuaDLL.lua_pushstring(L, tag);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index tag on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_tag(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Component component = (Component)obj;
			string tag = ToLua.CheckString(L, 2);
			component.set_tag(tag);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index tag on a nil value");
		}
		return result;
	}
}
