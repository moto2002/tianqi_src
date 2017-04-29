using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_MeshRendererWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MeshRenderer), typeof(Renderer), null);
		L.RegFunction("New", new LuaCSFunction(UnityEngine_MeshRendererWrap._CreateUnityEngine_MeshRenderer));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_MeshRendererWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_MeshRendererWrap.Lua_ToString));
		L.RegVar("additionalVertexStreams", new LuaCSFunction(UnityEngine_MeshRendererWrap.get_additionalVertexStreams), new LuaCSFunction(UnityEngine_MeshRendererWrap.set_additionalVertexStreams));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_MeshRenderer(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				MeshRenderer obj = new MeshRenderer();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.MeshRenderer.New");
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
	private static int get_additionalVertexStreams(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			MeshRenderer meshRenderer = (MeshRenderer)obj;
			Mesh additionalVertexStreams = meshRenderer.get_additionalVertexStreams();
			ToLua.Push(L, additionalVertexStreams);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index additionalVertexStreams on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_additionalVertexStreams(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			MeshRenderer meshRenderer = (MeshRenderer)obj;
			Mesh additionalVertexStreams = (Mesh)ToLua.CheckUnityObject(L, 2, typeof(Mesh));
			meshRenderer.set_additionalVertexStreams(additionalVertexStreams);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index additionalVertexStreams on a nil value");
		}
		return result;
	}
}
