using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_ShaderWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Shader), typeof(Object), null);
		L.RegFunction("Find", new LuaCSFunction(UnityEngine_ShaderWrap.Find));
		L.RegFunction("EnableKeyword", new LuaCSFunction(UnityEngine_ShaderWrap.EnableKeyword));
		L.RegFunction("DisableKeyword", new LuaCSFunction(UnityEngine_ShaderWrap.DisableKeyword));
		L.RegFunction("IsKeywordEnabled", new LuaCSFunction(UnityEngine_ShaderWrap.IsKeywordEnabled));
		L.RegFunction("SetGlobalColor", new LuaCSFunction(UnityEngine_ShaderWrap.SetGlobalColor));
		L.RegFunction("SetGlobalVector", new LuaCSFunction(UnityEngine_ShaderWrap.SetGlobalVector));
		L.RegFunction("SetGlobalFloat", new LuaCSFunction(UnityEngine_ShaderWrap.SetGlobalFloat));
		L.RegFunction("SetGlobalInt", new LuaCSFunction(UnityEngine_ShaderWrap.SetGlobalInt));
		L.RegFunction("SetGlobalTexture", new LuaCSFunction(UnityEngine_ShaderWrap.SetGlobalTexture));
		L.RegFunction("SetGlobalMatrix", new LuaCSFunction(UnityEngine_ShaderWrap.SetGlobalMatrix));
		L.RegFunction("SetGlobalBuffer", new LuaCSFunction(UnityEngine_ShaderWrap.SetGlobalBuffer));
		L.RegFunction("PropertyToID", new LuaCSFunction(UnityEngine_ShaderWrap.PropertyToID));
		L.RegFunction("WarmupAllShaders", new LuaCSFunction(UnityEngine_ShaderWrap.WarmupAllShaders));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_ShaderWrap._CreateUnityEngine_Shader));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_ShaderWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_ShaderWrap.Lua_ToString));
		L.RegVar("isSupported", new LuaCSFunction(UnityEngine_ShaderWrap.get_isSupported), null);
		L.RegVar("maximumLOD", new LuaCSFunction(UnityEngine_ShaderWrap.get_maximumLOD), new LuaCSFunction(UnityEngine_ShaderWrap.set_maximumLOD));
		L.RegVar("globalMaximumLOD", new LuaCSFunction(UnityEngine_ShaderWrap.get_globalMaximumLOD), new LuaCSFunction(UnityEngine_ShaderWrap.set_globalMaximumLOD));
		L.RegVar("renderQueue", new LuaCSFunction(UnityEngine_ShaderWrap.get_renderQueue), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Shader(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				Shader obj = new Shader();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Shader.New");
			}
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
			Shader obj = Shader.Find(text);
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
	private static int EnableKeyword(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			Shader.EnableKeyword(text);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DisableKeyword(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			Shader.DisableKeyword(text);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsKeywordEnabled(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			bool value = Shader.IsKeywordEnabled(text);
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
	private static int SetGlobalColor(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(Color)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				Color color = ToLua.ToColor(L, 2);
				Shader.SetGlobalColor(num2, color);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(Color)))
			{
				string text = ToLua.ToString(L, 1);
				Color color2 = ToLua.ToColor(L, 2);
				Shader.SetGlobalColor(text, color2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Shader.SetGlobalColor");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalVector(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(Vector4)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				Vector4 vector = ToLua.ToVector4(L, 2);
				Shader.SetGlobalVector(num2, vector);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(Vector4)))
			{
				string text = ToLua.ToString(L, 1);
				Vector4 vector2 = ToLua.ToVector4(L, 2);
				Shader.SetGlobalVector(text, vector2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Shader.SetGlobalVector");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalFloat(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(float)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				Shader.SetGlobalFloat(num2, num3);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(float)))
			{
				string text = ToLua.ToString(L, 1);
				float num4 = (float)LuaDLL.lua_tonumber(L, 2);
				Shader.SetGlobalFloat(text, num4);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Shader.SetGlobalFloat");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalInt(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				Shader.SetGlobalInt(num2, num3);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				string text = ToLua.ToString(L, 1);
				int num4 = (int)LuaDLL.lua_tonumber(L, 2);
				Shader.SetGlobalInt(text, num4);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Shader.SetGlobalInt");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalTexture(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(Texture)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				Texture texture = (Texture)ToLua.ToObject(L, 2);
				Shader.SetGlobalTexture(num2, texture);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(Texture)))
			{
				string text = ToLua.ToString(L, 1);
				Texture texture2 = (Texture)ToLua.ToObject(L, 2);
				Shader.SetGlobalTexture(text, texture2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Shader.SetGlobalTexture");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalMatrix(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(Matrix4x4)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				Matrix4x4 matrix4x = (Matrix4x4)ToLua.ToObject(L, 2);
				Shader.SetGlobalMatrix(num2, matrix4x);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(Matrix4x4)))
			{
				string text = ToLua.ToString(L, 1);
				Matrix4x4 matrix4x2 = (Matrix4x4)ToLua.ToObject(L, 2);
				Shader.SetGlobalMatrix(text, matrix4x2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Shader.SetGlobalMatrix");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalBuffer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string text = ToLua.CheckString(L, 1);
			ComputeBuffer computeBuffer = (ComputeBuffer)ToLua.CheckObject(L, 2, typeof(ComputeBuffer));
			Shader.SetGlobalBuffer(text, computeBuffer);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PropertyToID(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			int n = Shader.PropertyToID(text);
			LuaDLL.lua_pushinteger(L, n);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WarmupAllShaders(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			Shader.WarmupAllShaders();
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
	private static int get_isSupported(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Shader shader = (Shader)obj;
			bool isSupported = shader.get_isSupported();
			LuaDLL.lua_pushboolean(L, isSupported);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isSupported on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maximumLOD(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Shader shader = (Shader)obj;
			int maximumLOD = shader.get_maximumLOD();
			LuaDLL.lua_pushinteger(L, maximumLOD);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maximumLOD on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_globalMaximumLOD(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Shader.get_globalMaximumLOD());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_renderQueue(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Shader shader = (Shader)obj;
			int renderQueue = shader.get_renderQueue();
			LuaDLL.lua_pushinteger(L, renderQueue);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index renderQueue on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maximumLOD(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Shader shader = (Shader)obj;
			int maximumLOD = (int)LuaDLL.luaL_checknumber(L, 2);
			shader.set_maximumLOD(maximumLOD);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maximumLOD on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_globalMaximumLOD(IntPtr L)
	{
		int result;
		try
		{
			int globalMaximumLOD = (int)LuaDLL.luaL_checknumber(L, 2);
			Shader.set_globalMaximumLOD(globalMaximumLOD);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
