using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_RectTransformWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(RectTransform), typeof(Transform), null);
		L.RegFunction("GetLocalCorners", new LuaCSFunction(UnityEngine_RectTransformWrap.GetLocalCorners));
		L.RegFunction("GetWorldCorners", new LuaCSFunction(UnityEngine_RectTransformWrap.GetWorldCorners));
		L.RegFunction("SetInsetAndSizeFromParentEdge", new LuaCSFunction(UnityEngine_RectTransformWrap.SetInsetAndSizeFromParentEdge));
		L.RegFunction("SetSizeWithCurrentAnchors", new LuaCSFunction(UnityEngine_RectTransformWrap.SetSizeWithCurrentAnchors));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_RectTransformWrap._CreateUnityEngine_RectTransform));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_RectTransformWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_RectTransformWrap.Lua_ToString));
		L.RegVar("rect", new LuaCSFunction(UnityEngine_RectTransformWrap.get_rect), null);
		L.RegVar("anchorMin", new LuaCSFunction(UnityEngine_RectTransformWrap.get_anchorMin), new LuaCSFunction(UnityEngine_RectTransformWrap.set_anchorMin));
		L.RegVar("anchorMax", new LuaCSFunction(UnityEngine_RectTransformWrap.get_anchorMax), new LuaCSFunction(UnityEngine_RectTransformWrap.set_anchorMax));
		L.RegVar("anchoredPosition3D", new LuaCSFunction(UnityEngine_RectTransformWrap.get_anchoredPosition3D), new LuaCSFunction(UnityEngine_RectTransformWrap.set_anchoredPosition3D));
		L.RegVar("anchoredPosition", new LuaCSFunction(UnityEngine_RectTransformWrap.get_anchoredPosition), new LuaCSFunction(UnityEngine_RectTransformWrap.set_anchoredPosition));
		L.RegVar("sizeDelta", new LuaCSFunction(UnityEngine_RectTransformWrap.get_sizeDelta), new LuaCSFunction(UnityEngine_RectTransformWrap.set_sizeDelta));
		L.RegVar("pivot", new LuaCSFunction(UnityEngine_RectTransformWrap.get_pivot), new LuaCSFunction(UnityEngine_RectTransformWrap.set_pivot));
		L.RegVar("offsetMin", new LuaCSFunction(UnityEngine_RectTransformWrap.get_offsetMin), new LuaCSFunction(UnityEngine_RectTransformWrap.set_offsetMin));
		L.RegVar("offsetMax", new LuaCSFunction(UnityEngine_RectTransformWrap.get_offsetMax), new LuaCSFunction(UnityEngine_RectTransformWrap.set_offsetMax));
		L.RegVar("reapplyDrivenProperties", new LuaCSFunction(UnityEngine_RectTransformWrap.get_reapplyDrivenProperties), new LuaCSFunction(UnityEngine_RectTransformWrap.set_reapplyDrivenProperties));
		L.RegFunction("ReapplyDrivenProperties", new LuaCSFunction(UnityEngine_RectTransformWrap.UnityEngine_RectTransform_ReapplyDrivenProperties));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_RectTransform(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				RectTransform obj = new RectTransform();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.RectTransform.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetLocalCorners(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			RectTransform rectTransform = (RectTransform)ToLua.CheckObject(L, 1, typeof(RectTransform));
			Vector3[] array = ToLua.CheckObjectArray<Vector3>(L, 2);
			rectTransform.GetLocalCorners(array);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetWorldCorners(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			RectTransform rectTransform = (RectTransform)ToLua.CheckObject(L, 1, typeof(RectTransform));
			Vector3[] array = ToLua.CheckObjectArray<Vector3>(L, 2);
			rectTransform.GetWorldCorners(array);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetInsetAndSizeFromParentEdge(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 4);
			RectTransform rectTransform = (RectTransform)ToLua.CheckObject(L, 1, typeof(RectTransform));
			RectTransform.Edge edge = (int)ToLua.CheckObject(L, 2, typeof(RectTransform.Edge));
			float num = (float)LuaDLL.luaL_checknumber(L, 3);
			float num2 = (float)LuaDLL.luaL_checknumber(L, 4);
			rectTransform.SetInsetAndSizeFromParentEdge(edge, num, num2);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetSizeWithCurrentAnchors(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			RectTransform rectTransform = (RectTransform)ToLua.CheckObject(L, 1, typeof(RectTransform));
			RectTransform.Axis axis = (int)ToLua.CheckObject(L, 2, typeof(RectTransform.Axis));
			float num = (float)LuaDLL.luaL_checknumber(L, 3);
			rectTransform.SetSizeWithCurrentAnchors(axis, num);
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
	private static int get_rect(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Rect rect = rectTransform.get_rect();
			ToLua.PushValue(L, rect);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rect on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchorMin(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 anchorMin = rectTransform.get_anchorMin();
			ToLua.Push(L, anchorMin);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchorMin on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchorMax(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 anchorMax = rectTransform.get_anchorMax();
			ToLua.Push(L, anchorMax);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchorMax on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchoredPosition3D(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector3 anchoredPosition3D = rectTransform.get_anchoredPosition3D();
			ToLua.Push(L, anchoredPosition3D);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchoredPosition3D on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchoredPosition(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 anchoredPosition = rectTransform.get_anchoredPosition();
			ToLua.Push(L, anchoredPosition);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchoredPosition on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sizeDelta(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 sizeDelta = rectTransform.get_sizeDelta();
			ToLua.Push(L, sizeDelta);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sizeDelta on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pivot(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 pivot = rectTransform.get_pivot();
			ToLua.Push(L, pivot);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index pivot on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_offsetMin(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 offsetMin = rectTransform.get_offsetMin();
			ToLua.Push(L, offsetMin);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index offsetMin on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_offsetMax(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 offsetMax = rectTransform.get_offsetMax();
			ToLua.Push(L, offsetMax);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index offsetMax on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reapplyDrivenProperties(IntPtr L)
	{
		ToLua.Push(L, new EventObject("UnityEngine.RectTransform.reapplyDrivenProperties"));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchorMin(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 anchorMin = ToLua.ToVector2(L, 2);
			rectTransform.set_anchorMin(anchorMin);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchorMin on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchorMax(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 anchorMax = ToLua.ToVector2(L, 2);
			rectTransform.set_anchorMax(anchorMax);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchorMax on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchoredPosition3D(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector3 anchoredPosition3D = ToLua.ToVector3(L, 2);
			rectTransform.set_anchoredPosition3D(anchoredPosition3D);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchoredPosition3D on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchoredPosition(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 anchoredPosition = ToLua.ToVector2(L, 2);
			rectTransform.set_anchoredPosition(anchoredPosition);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index anchoredPosition on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sizeDelta(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 sizeDelta = ToLua.ToVector2(L, 2);
			rectTransform.set_sizeDelta(sizeDelta);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sizeDelta on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pivot(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 pivot = ToLua.ToVector2(L, 2);
			rectTransform.set_pivot(pivot);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index pivot on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_offsetMin(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 offsetMin = ToLua.ToVector2(L, 2);
			rectTransform.set_offsetMin(offsetMin);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index offsetMin on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_offsetMax(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RectTransform rectTransform = (RectTransform)obj;
			Vector2 offsetMax = ToLua.ToVector2(L, 2);
			rectTransform.set_offsetMax(offsetMax);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index offsetMax on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reapplyDrivenProperties(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				EventObject eventObject = (EventObject)ToLua.ToObject(L, 2);
				if (eventObject.op == EventOp.Add)
				{
					RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = (RectTransform.ReapplyDrivenProperties)DelegateFactory.CreateDelegate(typeof(RectTransform.ReapplyDrivenProperties), eventObject.func);
					RectTransform.add_reapplyDrivenProperties(reapplyDrivenProperties);
				}
				else if (eventObject.op == EventOp.Sub)
				{
					RectTransform.ReapplyDrivenProperties reapplyDrivenProperties2 = (RectTransform.ReapplyDrivenProperties)LuaMisc.GetEventHandler(null, typeof(RectTransform), "reapplyDrivenProperties");
					Delegate[] invocationList = reapplyDrivenProperties2.GetInvocationList();
					LuaState luaState = LuaState.Get(L);
					for (int i = 0; i < invocationList.Length; i++)
					{
						reapplyDrivenProperties2 = (RectTransform.ReapplyDrivenProperties)invocationList[i];
						LuaDelegate luaDelegate = reapplyDrivenProperties2.get_Target() as LuaDelegate;
						if (luaDelegate != null && luaDelegate.func == eventObject.func)
						{
							RectTransform.remove_reapplyDrivenProperties(reapplyDrivenProperties2);
							luaState.DelayDispose(luaDelegate.func);
							break;
						}
					}
					eventObject.func.Dispose();
				}
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "The event 'UnityEngine.RectTransform.reapplyDrivenProperties' can only appear on the left hand side of += or -= when used outside of the type 'UnityEngine.RectTransform'");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_RectTransform_ReapplyDrivenProperties(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(RectTransform.ReapplyDrivenProperties), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
