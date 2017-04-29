using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityEngine_GameObjectWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameObject), typeof(Object), null);
		L.RegFunction("CreatePrimitive", new LuaCSFunction(UnityEngine_GameObjectWrap.CreatePrimitive));
		L.RegFunction("GetComponent", new LuaCSFunction(UnityEngine_GameObjectWrap.GetComponent));
		L.RegFunction("GetComponentInChildren", new LuaCSFunction(UnityEngine_GameObjectWrap.GetComponentInChildren));
		L.RegFunction("GetComponentInParent", new LuaCSFunction(UnityEngine_GameObjectWrap.GetComponentInParent));
		L.RegFunction("GetComponents", new LuaCSFunction(UnityEngine_GameObjectWrap.GetComponents));
		L.RegFunction("GetComponentsInChildren", new LuaCSFunction(UnityEngine_GameObjectWrap.GetComponentsInChildren));
		L.RegFunction("GetComponentsInParent", new LuaCSFunction(UnityEngine_GameObjectWrap.GetComponentsInParent));
		L.RegFunction("SetActive", new LuaCSFunction(UnityEngine_GameObjectWrap.SetActive));
		L.RegFunction("CompareTag", new LuaCSFunction(UnityEngine_GameObjectWrap.CompareTag));
		L.RegFunction("FindGameObjectWithTag", new LuaCSFunction(UnityEngine_GameObjectWrap.FindGameObjectWithTag));
		L.RegFunction("FindWithTag", new LuaCSFunction(UnityEngine_GameObjectWrap.FindWithTag));
		L.RegFunction("FindGameObjectsWithTag", new LuaCSFunction(UnityEngine_GameObjectWrap.FindGameObjectsWithTag));
		L.RegFunction("SendMessageUpwards", new LuaCSFunction(UnityEngine_GameObjectWrap.SendMessageUpwards));
		L.RegFunction("BroadcastMessage", new LuaCSFunction(UnityEngine_GameObjectWrap.BroadcastMessage));
		L.RegFunction("AddComponent", new LuaCSFunction(UnityEngine_GameObjectWrap.AddComponent));
		L.RegFunction("Find", new LuaCSFunction(UnityEngine_GameObjectWrap.Find));
		L.RegFunction("SendMessage", new LuaCSFunction(UnityEngine_GameObjectWrap.SendMessage));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_GameObjectWrap._CreateUnityEngine_GameObject));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_GameObjectWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_GameObjectWrap.Lua_ToString));
		L.RegVar("transform", new LuaCSFunction(UnityEngine_GameObjectWrap.get_transform), null);
		L.RegVar("layer", new LuaCSFunction(UnityEngine_GameObjectWrap.get_layer), new LuaCSFunction(UnityEngine_GameObjectWrap.set_layer));
		L.RegVar("activeSelf", new LuaCSFunction(UnityEngine_GameObjectWrap.get_activeSelf), null);
		L.RegVar("activeInHierarchy", new LuaCSFunction(UnityEngine_GameObjectWrap.get_activeInHierarchy), null);
		L.RegVar("isStatic", new LuaCSFunction(UnityEngine_GameObjectWrap.get_isStatic), new LuaCSFunction(UnityEngine_GameObjectWrap.set_isStatic));
		L.RegVar("tag", new LuaCSFunction(UnityEngine_GameObjectWrap.get_tag), new LuaCSFunction(UnityEngine_GameObjectWrap.set_tag));
		L.RegVar("scene", new LuaCSFunction(UnityEngine_GameObjectWrap.get_scene), null);
		L.RegVar("gameObject", new LuaCSFunction(UnityEngine_GameObjectWrap.get_gameObject), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_GameObject(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 0)
			{
				GameObject obj = new GameObject();
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.CheckString(L, 1);
				GameObject obj2 = new GameObject(text);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else if (TypeChecker.CheckTypes(L, 1, typeof(string)) && TypeChecker.CheckParamsType(L, typeof(Type), 2, num - 1))
			{
				string text2 = ToLua.CheckString(L, 1);
				Type[] array = ToLua.CheckParamsObject<Type>(L, 2, num - 1);
				GameObject obj3 = new GameObject(text2, array);
				ToLua.Push(L, obj3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.GameObject.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreatePrimitive(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PrimitiveType primitiveType = (int)ToLua.CheckObject(L, 1, typeof(PrimitiveType));
			GameObject obj = GameObject.CreatePrimitive(primitiveType);
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
	private static int GetComponent(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Component component = gameObject.GetComponent(text);
				ToLua.Push(L, component);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component component2 = gameObject2.GetComponent(type);
				ToLua.Push(L, component2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.GetComponent");
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
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component componentInChildren = gameObject.GetComponentInChildren(type);
				ToLua.Push(L, componentInChildren);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type), typeof(bool)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Component componentInChildren2 = gameObject2.GetComponentInChildren(type2, flag);
				ToLua.Push(L, componentInChildren2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.GetComponentInChildren");
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
			GameObject gameObject = (GameObject)ToLua.CheckObject(L, 1, typeof(GameObject));
			Type type = (Type)ToLua.CheckObject(L, 2, typeof(Type));
			Component componentInParent = gameObject.GetComponentInParent(type);
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
	private static int GetComponents(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component[] components = gameObject.GetComponents(type);
				ToLua.Push(L, components);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type), typeof(List<Component>)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				List<Component> list = (List<Component>)ToLua.ToObject(L, 3);
				gameObject2.GetComponents(type2, list);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.GetComponents");
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
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component[] componentsInChildren = gameObject.GetComponentsInChildren(type);
				ToLua.Push(L, componentsInChildren);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type), typeof(bool)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Component[] componentsInChildren2 = gameObject2.GetComponentsInChildren(type2, flag);
				ToLua.Push(L, componentsInChildren2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.GetComponentsInChildren");
			}
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
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Component[] componentsInParent = gameObject.GetComponentsInParent(type);
				ToLua.Push(L, componentsInParent);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(Type), typeof(bool)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				Type type2 = (Type)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Component[] componentsInParent2 = gameObject2.GetComponentsInParent(type2, flag);
				ToLua.Push(L, componentsInParent2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.GetComponentsInParent");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetActive(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GameObject gameObject = (GameObject)ToLua.CheckObject(L, 1, typeof(GameObject));
			bool active = LuaDLL.luaL_checkboolean(L, 2);
			gameObject.SetActive(active);
			result = 0;
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
			GameObject gameObject = (GameObject)ToLua.CheckObject(L, 1, typeof(GameObject));
			string text = ToLua.CheckString(L, 2);
			bool value = gameObject.CompareTag(text);
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
	private static int FindGameObjectWithTag(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			GameObject obj = GameObject.FindGameObjectWithTag(text);
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
	private static int FindWithTag(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			GameObject obj = GameObject.FindWithTag(text);
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
	private static int FindGameObjectsWithTag(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			GameObject[] array = GameObject.FindGameObjectsWithTag(text);
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
	private static int SendMessageUpwards(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				gameObject.SendMessageUpwards(text);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				SendMessageOptions sendMessageOptions = (int)ToLua.ToObject(L, 3);
				gameObject2.SendMessageUpwards(text2, sendMessageOptions);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object)))
			{
				GameObject gameObject3 = (GameObject)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				gameObject3.SendMessageUpwards(text3, obj);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object), typeof(SendMessageOptions)))
			{
				GameObject gameObject4 = (GameObject)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				object obj2 = ToLua.ToVarObject(L, 3);
				SendMessageOptions sendMessageOptions2 = (int)ToLua.ToObject(L, 4);
				gameObject4.SendMessageUpwards(text4, obj2, sendMessageOptions2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.SendMessageUpwards");
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
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				gameObject.BroadcastMessage(text);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				SendMessageOptions sendMessageOptions = (int)ToLua.ToObject(L, 3);
				gameObject2.BroadcastMessage(text2, sendMessageOptions);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object)))
			{
				GameObject gameObject3 = (GameObject)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				gameObject3.BroadcastMessage(text3, obj);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object), typeof(SendMessageOptions)))
			{
				GameObject gameObject4 = (GameObject)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				object obj2 = ToLua.ToVarObject(L, 3);
				SendMessageOptions sendMessageOptions2 = (int)ToLua.ToObject(L, 4);
				gameObject4.BroadcastMessage(text4, obj2, sendMessageOptions2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.BroadcastMessage");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddComponent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GameObject gameObject = (GameObject)ToLua.CheckObject(L, 1, typeof(GameObject));
			Type type = (Type)ToLua.CheckObject(L, 2, typeof(Type));
			Component obj = gameObject.AddComponent(type);
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
	private static int Find(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			GameObject obj = GameObject.Find(text);
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
	private static int SendMessage(IntPtr L)
	{
		int result;
		try
		{
			LuaException.SendMsgCount++;
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
			{
				GameObject gameObject = (GameObject)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				gameObject.SendMessage(text);
				LuaException.SendMsgCount--;
				if (LuaDLL.lua_toboolean(L, LuaDLL.lua_upvalueindex(1)))
				{
					string msg = LuaDLL.lua_tostring(L, -1);
					LuaDLL.lua_pop(L, 1);
					throw new LuaException(msg, LuaException.luaStack, 1);
				}
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
			{
				GameObject gameObject2 = (GameObject)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				SendMessageOptions sendMessageOptions = (int)ToLua.ToObject(L, 3);
				gameObject2.SendMessage(text2, sendMessageOptions);
				LuaException.SendMsgCount--;
				if (LuaDLL.lua_toboolean(L, LuaDLL.lua_upvalueindex(1)))
				{
					string msg2 = LuaDLL.lua_tostring(L, -1);
					LuaDLL.lua_pop(L, 1);
					throw new LuaException(msg2, LuaException.luaStack, 1);
				}
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object)))
			{
				GameObject gameObject3 = (GameObject)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				object obj = ToLua.ToVarObject(L, 3);
				gameObject3.SendMessage(text3, obj);
				LuaException.SendMsgCount--;
				if (LuaDLL.lua_toboolean(L, LuaDLL.lua_upvalueindex(1)))
				{
					string msg3 = LuaDLL.lua_tostring(L, -1);
					LuaDLL.lua_pop(L, 1);
					throw new LuaException(msg3, LuaException.luaStack, 1);
				}
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object), typeof(SendMessageOptions)))
			{
				GameObject gameObject4 = (GameObject)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				object obj2 = ToLua.ToVarObject(L, 3);
				SendMessageOptions sendMessageOptions2 = (int)ToLua.ToObject(L, 4);
				gameObject4.SendMessage(text4, obj2, sendMessageOptions2);
				LuaException.SendMsgCount--;
				if (LuaDLL.lua_toboolean(L, LuaDLL.lua_upvalueindex(1)))
				{
					string msg4 = LuaDLL.lua_tostring(L, -1);
					LuaDLL.lua_pop(L, 1);
					throw new LuaException(msg4, LuaException.luaStack, 1);
				}
				result = 0;
			}
			else
			{
				LuaException.SendMsgCount--;
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.GameObject.SendMessage");
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
			GameObject gameObject = (GameObject)obj;
			Transform transform = gameObject.get_transform();
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
	private static int get_layer(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			GameObject gameObject = (GameObject)obj;
			int layer = gameObject.get_layer();
			LuaDLL.lua_pushinteger(L, layer);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layer on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_activeSelf(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			GameObject gameObject = (GameObject)obj;
			bool activeSelf = gameObject.get_activeSelf();
			LuaDLL.lua_pushboolean(L, activeSelf);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index activeSelf on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_activeInHierarchy(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			GameObject gameObject = (GameObject)obj;
			bool activeInHierarchy = gameObject.get_activeInHierarchy();
			LuaDLL.lua_pushboolean(L, activeInHierarchy);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index activeInHierarchy on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isStatic(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			GameObject gameObject = (GameObject)obj;
			bool isStatic = gameObject.get_isStatic();
			LuaDLL.lua_pushboolean(L, isStatic);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isStatic on a nil value");
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
			GameObject gameObject = (GameObject)obj;
			string tag = gameObject.get_tag();
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
	private static int get_scene(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			GameObject gameObject = (GameObject)obj;
			Scene scene = gameObject.get_scene();
			ToLua.PushValue(L, scene);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index scene on a nil value");
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
			GameObject gameObject = (GameObject)obj;
			GameObject gameObject2 = gameObject.get_gameObject();
			ToLua.Push(L, gameObject2);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index gameObject on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_layer(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			GameObject gameObject = (GameObject)obj;
			int layer = (int)LuaDLL.luaL_checknumber(L, 2);
			gameObject.set_layer(layer);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layer on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isStatic(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			GameObject gameObject = (GameObject)obj;
			bool isStatic = LuaDLL.luaL_checkboolean(L, 2);
			gameObject.set_isStatic(isStatic);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isStatic on a nil value");
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
			GameObject gameObject = (GameObject)obj;
			string tag = ToLua.CheckString(L, 2);
			gameObject.set_tag(tag);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index tag on a nil value");
		}
		return result;
	}
}
