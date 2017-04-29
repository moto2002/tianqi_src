using LuaInterface;
using System;
using System.Collections;
using UnityEngine;

public class UnityEngine_TransformWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Transform), typeof(Component), null);
		L.RegFunction("SetParent", new LuaCSFunction(UnityEngine_TransformWrap.SetParent));
		L.RegFunction("Translate", new LuaCSFunction(UnityEngine_TransformWrap.Translate));
		L.RegFunction("Rotate", new LuaCSFunction(UnityEngine_TransformWrap.Rotate));
		L.RegFunction("RotateAround", new LuaCSFunction(UnityEngine_TransformWrap.RotateAround));
		L.RegFunction("LookAt", new LuaCSFunction(UnityEngine_TransformWrap.LookAt));
		L.RegFunction("TransformDirection", new LuaCSFunction(UnityEngine_TransformWrap.TransformDirection));
		L.RegFunction("InverseTransformDirection", new LuaCSFunction(UnityEngine_TransformWrap.InverseTransformDirection));
		L.RegFunction("TransformVector", new LuaCSFunction(UnityEngine_TransformWrap.TransformVector));
		L.RegFunction("InverseTransformVector", new LuaCSFunction(UnityEngine_TransformWrap.InverseTransformVector));
		L.RegFunction("TransformPoint", new LuaCSFunction(UnityEngine_TransformWrap.TransformPoint));
		L.RegFunction("InverseTransformPoint", new LuaCSFunction(UnityEngine_TransformWrap.InverseTransformPoint));
		L.RegFunction("DetachChildren", new LuaCSFunction(UnityEngine_TransformWrap.DetachChildren));
		L.RegFunction("SetAsFirstSibling", new LuaCSFunction(UnityEngine_TransformWrap.SetAsFirstSibling));
		L.RegFunction("SetAsLastSibling", new LuaCSFunction(UnityEngine_TransformWrap.SetAsLastSibling));
		L.RegFunction("SetSiblingIndex", new LuaCSFunction(UnityEngine_TransformWrap.SetSiblingIndex));
		L.RegFunction("GetSiblingIndex", new LuaCSFunction(UnityEngine_TransformWrap.GetSiblingIndex));
		L.RegFunction("Find", new LuaCSFunction(UnityEngine_TransformWrap.Find));
		L.RegFunction("IsChildOf", new LuaCSFunction(UnityEngine_TransformWrap.IsChildOf));
		L.RegFunction("FindChild", new LuaCSFunction(UnityEngine_TransformWrap.FindChild));
		L.RegFunction("GetEnumerator", new LuaCSFunction(UnityEngine_TransformWrap.GetEnumerator));
		L.RegFunction("GetChild", new LuaCSFunction(UnityEngine_TransformWrap.GetChild));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_TransformWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_TransformWrap.Lua_ToString));
		L.RegVar("position", new LuaCSFunction(UnityEngine_TransformWrap.get_position), new LuaCSFunction(UnityEngine_TransformWrap.set_position));
		L.RegVar("localPosition", new LuaCSFunction(UnityEngine_TransformWrap.get_localPosition), new LuaCSFunction(UnityEngine_TransformWrap.set_localPosition));
		L.RegVar("eulerAngles", new LuaCSFunction(UnityEngine_TransformWrap.get_eulerAngles), new LuaCSFunction(UnityEngine_TransformWrap.set_eulerAngles));
		L.RegVar("localEulerAngles", new LuaCSFunction(UnityEngine_TransformWrap.get_localEulerAngles), new LuaCSFunction(UnityEngine_TransformWrap.set_localEulerAngles));
		L.RegVar("right", new LuaCSFunction(UnityEngine_TransformWrap.get_right), new LuaCSFunction(UnityEngine_TransformWrap.set_right));
		L.RegVar("up", new LuaCSFunction(UnityEngine_TransformWrap.get_up), new LuaCSFunction(UnityEngine_TransformWrap.set_up));
		L.RegVar("forward", new LuaCSFunction(UnityEngine_TransformWrap.get_forward), new LuaCSFunction(UnityEngine_TransformWrap.set_forward));
		L.RegVar("rotation", new LuaCSFunction(UnityEngine_TransformWrap.get_rotation), new LuaCSFunction(UnityEngine_TransformWrap.set_rotation));
		L.RegVar("localRotation", new LuaCSFunction(UnityEngine_TransformWrap.get_localRotation), new LuaCSFunction(UnityEngine_TransformWrap.set_localRotation));
		L.RegVar("localScale", new LuaCSFunction(UnityEngine_TransformWrap.get_localScale), new LuaCSFunction(UnityEngine_TransformWrap.set_localScale));
		L.RegVar("parent", new LuaCSFunction(UnityEngine_TransformWrap.get_parent), new LuaCSFunction(UnityEngine_TransformWrap.set_parent));
		L.RegVar("worldToLocalMatrix", new LuaCSFunction(UnityEngine_TransformWrap.get_worldToLocalMatrix), null);
		L.RegVar("localToWorldMatrix", new LuaCSFunction(UnityEngine_TransformWrap.get_localToWorldMatrix), null);
		L.RegVar("root", new LuaCSFunction(UnityEngine_TransformWrap.get_root), null);
		L.RegVar("childCount", new LuaCSFunction(UnityEngine_TransformWrap.get_childCount), null);
		L.RegVar("lossyScale", new LuaCSFunction(UnityEngine_TransformWrap.get_lossyScale), null);
		L.RegVar("hasChanged", new LuaCSFunction(UnityEngine_TransformWrap.get_hasChanged), new LuaCSFunction(UnityEngine_TransformWrap.set_hasChanged));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetParent(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Transform)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Transform parent = (Transform)ToLua.ToObject(L, 2);
				transform.SetParent(parent);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Transform), typeof(bool)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				Transform transform3 = (Transform)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				transform2.SetParent(transform3, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.SetParent");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Translate(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				transform.Translate(vector);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3), typeof(Transform)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				Transform transform3 = (Transform)ToLua.ToObject(L, 3);
				transform2.Translate(vector2, transform3);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3), typeof(Space)))
			{
				Transform transform4 = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector3 = ToLua.ToVector3(L, 2);
				Space space = (int)ToLua.ToObject(L, 3);
				transform4.Translate(vector3, space);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform5 = (Transform)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				transform5.Translate(num2, num3, num4);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float), typeof(Transform)))
			{
				Transform transform6 = (Transform)ToLua.ToObject(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				float num7 = (float)LuaDLL.lua_tonumber(L, 4);
				Transform transform7 = (Transform)ToLua.ToObject(L, 5);
				transform6.Translate(num5, num6, num7, transform7);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float), typeof(Space)))
			{
				Transform transform8 = (Transform)ToLua.ToObject(L, 1);
				float num8 = (float)LuaDLL.lua_tonumber(L, 2);
				float num9 = (float)LuaDLL.lua_tonumber(L, 3);
				float num10 = (float)LuaDLL.lua_tonumber(L, 4);
				Space space2 = (int)ToLua.ToObject(L, 5);
				transform8.Translate(num8, num9, num10, space2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.Translate");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Rotate(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				transform.Rotate(vector);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3), typeof(float)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				transform2.Rotate(vector2, num2);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3), typeof(Space)))
			{
				Transform transform3 = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector3 = ToLua.ToVector3(L, 2);
				Space space = (int)ToLua.ToObject(L, 3);
				transform3.Rotate(vector3, space);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3), typeof(float), typeof(Space)))
			{
				Transform transform4 = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				Space space2 = (int)ToLua.ToObject(L, 4);
				transform4.Rotate(vector4, num3, space2);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform5 = (Transform)ToLua.ToObject(L, 1);
				float num4 = (float)LuaDLL.lua_tonumber(L, 2);
				float num5 = (float)LuaDLL.lua_tonumber(L, 3);
				float num6 = (float)LuaDLL.lua_tonumber(L, 4);
				transform5.Rotate(num4, num5, num6);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float), typeof(Space)))
			{
				Transform transform6 = (Transform)ToLua.ToObject(L, 1);
				float num7 = (float)LuaDLL.lua_tonumber(L, 2);
				float num8 = (float)LuaDLL.lua_tonumber(L, 3);
				float num9 = (float)LuaDLL.lua_tonumber(L, 4);
				Space space3 = (int)ToLua.ToObject(L, 5);
				transform6.Rotate(num7, num8, num9, space3);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.Rotate");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RotateAround(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 4);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 vector2 = ToLua.ToVector3(L, 3);
			float num = (float)LuaDLL.luaL_checknumber(L, 4);
			transform.RotateAround(vector, vector2, num);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LookAt(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				transform.LookAt(vector);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Transform)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				Transform transform3 = (Transform)ToLua.ToObject(L, 2);
				transform2.LookAt(transform3);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3), typeof(Vector3)))
			{
				Transform transform4 = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				Vector3 vector3 = ToLua.ToVector3(L, 3);
				transform4.LookAt(vector2, vector3);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Transform), typeof(Vector3)))
			{
				Transform transform5 = (Transform)ToLua.ToObject(L, 1);
				Transform transform6 = (Transform)ToLua.ToObject(L, 2);
				Vector3 vector4 = ToLua.ToVector3(L, 3);
				transform5.LookAt(transform6, vector4);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.LookAt");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TransformDirection(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 v = transform.TransformDirection(vector);
				ToLua.Push(L, v);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				Vector3 v2 = transform2.TransformDirection(num2, num3, num4);
				ToLua.Push(L, v2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.TransformDirection");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InverseTransformDirection(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 v = transform.InverseTransformDirection(vector);
				ToLua.Push(L, v);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				Vector3 v2 = transform2.InverseTransformDirection(num2, num3, num4);
				ToLua.Push(L, v2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.InverseTransformDirection");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TransformVector(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 v = transform.TransformVector(vector);
				ToLua.Push(L, v);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				Vector3 v2 = transform2.TransformVector(num2, num3, num4);
				ToLua.Push(L, v2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.TransformVector");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InverseTransformVector(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 v = transform.InverseTransformVector(vector);
				ToLua.Push(L, v);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				Vector3 v2 = transform2.InverseTransformVector(num2, num3, num4);
				ToLua.Push(L, v2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.InverseTransformVector");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TransformPoint(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 v = transform.TransformPoint(vector);
				ToLua.Push(L, v);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				Vector3 v2 = transform2.TransformPoint(num2, num3, num4);
				ToLua.Push(L, v2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.TransformPoint");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InverseTransformPoint(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(Vector3)))
			{
				Transform transform = (Transform)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 v = transform.InverseTransformPoint(vector);
				ToLua.Push(L, v);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
			{
				Transform transform2 = (Transform)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				Vector3 v2 = transform2.InverseTransformPoint(num2, num3, num4);
				ToLua.Push(L, v2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Transform.InverseTransformPoint");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DetachChildren(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			transform.DetachChildren();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetAsFirstSibling(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			transform.SetAsFirstSibling();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetAsLastSibling(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			transform.SetAsLastSibling();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetSiblingIndex(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			int siblingIndex = (int)LuaDLL.luaL_checknumber(L, 2);
			transform.SetSiblingIndex(siblingIndex);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSiblingIndex(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			int siblingIndex = transform.GetSiblingIndex();
			LuaDLL.lua_pushinteger(L, siblingIndex);
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
			ToLua.CheckArgsCount(L, 2);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			string text = ToLua.CheckString(L, 2);
			Transform obj = transform.Find(text);
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
	private static int IsChildOf(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			Transform transform2 = (Transform)ToLua.CheckUnityObject(L, 2, typeof(Transform));
			bool value = transform.IsChildOf(transform2);
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
	private static int FindChild(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			string text = ToLua.CheckString(L, 2);
			Transform obj = transform.FindChild(text);
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
	private static int GetEnumerator(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			IEnumerator enumerator = transform.GetEnumerator();
			ToLua.Push(L, enumerator);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetChild(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Transform transform = (Transform)ToLua.CheckObject(L, 1, typeof(Transform));
			int num = (int)LuaDLL.luaL_checknumber(L, 2);
			Transform child = transform.GetChild(num);
			ToLua.Push(L, child);
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
	private static int get_position(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 position = transform.get_position();
			ToLua.Push(L, position);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index position on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localPosition(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 localPosition = transform.get_localPosition();
			ToLua.Push(L, localPosition);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localPosition on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_eulerAngles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 eulerAngles = transform.get_eulerAngles();
			ToLua.Push(L, eulerAngles);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index eulerAngles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localEulerAngles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 localEulerAngles = transform.get_localEulerAngles();
			ToLua.Push(L, localEulerAngles);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localEulerAngles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_right(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 right = transform.get_right();
			ToLua.Push(L, right);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index right on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_up(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 up = transform.get_up();
			ToLua.Push(L, up);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index up on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_forward(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 forward = transform.get_forward();
			ToLua.Push(L, forward);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index forward on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Quaternion rotation = transform.get_rotation();
			ToLua.Push(L, rotation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Quaternion localRotation = transform.get_localRotation();
			ToLua.Push(L, localRotation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 localScale = transform.get_localScale();
			ToLua.Push(L, localScale);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_parent(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Transform parent = transform.get_parent();
			ToLua.Push(L, parent);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index parent on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldToLocalMatrix(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Matrix4x4 worldToLocalMatrix = transform.get_worldToLocalMatrix();
			ToLua.PushValue(L, worldToLocalMatrix);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldToLocalMatrix on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localToWorldMatrix(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Matrix4x4 localToWorldMatrix = transform.get_localToWorldMatrix();
			ToLua.PushValue(L, localToWorldMatrix);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localToWorldMatrix on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_root(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Transform root = transform.get_root();
			ToLua.Push(L, root);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index root on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_childCount(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			int childCount = transform.get_childCount();
			LuaDLL.lua_pushinteger(L, childCount);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index childCount on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lossyScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 lossyScale = transform.get_lossyScale();
			ToLua.Push(L, lossyScale);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index lossyScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hasChanged(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			bool hasChanged = transform.get_hasChanged();
			LuaDLL.lua_pushboolean(L, hasChanged);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index hasChanged on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_position(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 position = ToLua.ToVector3(L, 2);
			transform.set_position(position);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index position on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localPosition(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 localPosition = ToLua.ToVector3(L, 2);
			transform.set_localPosition(localPosition);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localPosition on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_eulerAngles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 eulerAngles = ToLua.ToVector3(L, 2);
			transform.set_eulerAngles(eulerAngles);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index eulerAngles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localEulerAngles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 localEulerAngles = ToLua.ToVector3(L, 2);
			transform.set_localEulerAngles(localEulerAngles);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localEulerAngles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_right(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 right = ToLua.ToVector3(L, 2);
			transform.set_right(right);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index right on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_up(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 up = ToLua.ToVector3(L, 2);
			transform.set_up(up);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index up on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_forward(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 forward = ToLua.ToVector3(L, 2);
			transform.set_forward(forward);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index forward on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Quaternion rotation = ToLua.ToQuaternion(L, 2);
			transform.set_rotation(rotation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Quaternion localRotation = ToLua.ToQuaternion(L, 2);
			transform.set_localRotation(localRotation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Vector3 localScale = ToLua.ToVector3(L, 2);
			transform.set_localScale(localScale);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_parent(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			Transform parent = (Transform)ToLua.CheckUnityObject(L, 2, typeof(Transform));
			transform.set_parent(parent);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index parent on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_hasChanged(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Transform transform = (Transform)obj;
			bool hasChanged = LuaDLL.luaL_checkboolean(L, 2);
			transform.set_hasChanged(hasChanged);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index hasChanged on a nil value");
		}
		return result;
	}
}
