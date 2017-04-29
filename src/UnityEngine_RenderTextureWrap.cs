using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_RenderTextureWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(RenderTexture), typeof(Texture), null);
		L.RegFunction("GetTemporary", new LuaCSFunction(UnityEngine_RenderTextureWrap.GetTemporary));
		L.RegFunction("ReleaseTemporary", new LuaCSFunction(UnityEngine_RenderTextureWrap.ReleaseTemporary));
		L.RegFunction("Create", new LuaCSFunction(UnityEngine_RenderTextureWrap.Create));
		L.RegFunction("Release", new LuaCSFunction(UnityEngine_RenderTextureWrap.Release));
		L.RegFunction("IsCreated", new LuaCSFunction(UnityEngine_RenderTextureWrap.IsCreated));
		L.RegFunction("DiscardContents", new LuaCSFunction(UnityEngine_RenderTextureWrap.DiscardContents));
		L.RegFunction("MarkRestoreExpected", new LuaCSFunction(UnityEngine_RenderTextureWrap.MarkRestoreExpected));
		L.RegFunction("SetGlobalShaderProperty", new LuaCSFunction(UnityEngine_RenderTextureWrap.SetGlobalShaderProperty));
		L.RegFunction("GetTexelOffset", new LuaCSFunction(UnityEngine_RenderTextureWrap.GetTexelOffset));
		L.RegFunction("SupportsStencil", new LuaCSFunction(UnityEngine_RenderTextureWrap.SupportsStencil));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_RenderTextureWrap._CreateUnityEngine_RenderTexture));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_RenderTextureWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_RenderTextureWrap.Lua_ToString));
		L.RegVar("width", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_width), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_width));
		L.RegVar("height", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_height), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_height));
		L.RegVar("depth", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_depth), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_depth));
		L.RegVar("isPowerOfTwo", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_isPowerOfTwo), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_isPowerOfTwo));
		L.RegVar("sRGB", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_sRGB), null);
		L.RegVar("format", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_format), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_format));
		L.RegVar("useMipMap", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_useMipMap), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_useMipMap));
		L.RegVar("generateMips", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_generateMips), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_generateMips));
		L.RegVar("isCubemap", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_isCubemap), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_isCubemap));
		L.RegVar("isVolume", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_isVolume), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_isVolume));
		L.RegVar("volumeDepth", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_volumeDepth), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_volumeDepth));
		L.RegVar("antiAliasing", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_antiAliasing), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_antiAliasing));
		L.RegVar("enableRandomWrite", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_enableRandomWrite), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_enableRandomWrite));
		L.RegVar("colorBuffer", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_colorBuffer), null);
		L.RegVar("depthBuffer", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_depthBuffer), null);
		L.RegVar("active", new LuaCSFunction(UnityEngine_RenderTextureWrap.get_active), new LuaCSFunction(UnityEngine_RenderTextureWrap.set_active));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_RenderTexture(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3)
			{
				int num2 = (int)LuaDLL.luaL_checknumber(L, 1);
				int num3 = (int)LuaDLL.luaL_checknumber(L, 2);
				int num4 = (int)LuaDLL.luaL_checknumber(L, 3);
				RenderTexture obj = new RenderTexture(num2, num3, num4);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(int), typeof(RenderTextureFormat)))
			{
				int num5 = (int)LuaDLL.luaL_checknumber(L, 1);
				int num6 = (int)LuaDLL.luaL_checknumber(L, 2);
				int num7 = (int)LuaDLL.luaL_checknumber(L, 3);
				RenderTextureFormat renderTextureFormat = (int)ToLua.CheckObject(L, 4, typeof(RenderTextureFormat));
				RenderTexture obj2 = new RenderTexture(num5, num6, num7, renderTextureFormat);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(int), typeof(RenderTextureFormat), typeof(RenderTextureReadWrite)))
			{
				int num8 = (int)LuaDLL.luaL_checknumber(L, 1);
				int num9 = (int)LuaDLL.luaL_checknumber(L, 2);
				int num10 = (int)LuaDLL.luaL_checknumber(L, 3);
				RenderTextureFormat renderTextureFormat2 = (int)ToLua.CheckObject(L, 4, typeof(RenderTextureFormat));
				RenderTextureReadWrite renderTextureReadWrite = (int)ToLua.CheckObject(L, 5, typeof(RenderTextureReadWrite));
				RenderTexture obj3 = new RenderTexture(num8, num9, num10, renderTextureFormat2, renderTextureReadWrite);
				ToLua.Push(L, obj3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.RenderTexture.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTemporary(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				RenderTexture temporary = RenderTexture.GetTemporary(num2, num3);
				ToLua.Push(L, temporary);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(int)))
			{
				int num4 = (int)LuaDLL.lua_tonumber(L, 1);
				int num5 = (int)LuaDLL.lua_tonumber(L, 2);
				int num6 = (int)LuaDLL.lua_tonumber(L, 3);
				RenderTexture temporary2 = RenderTexture.GetTemporary(num4, num5, num6);
				ToLua.Push(L, temporary2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(int), typeof(RenderTextureFormat)))
			{
				int num7 = (int)LuaDLL.lua_tonumber(L, 1);
				int num8 = (int)LuaDLL.lua_tonumber(L, 2);
				int num9 = (int)LuaDLL.lua_tonumber(L, 3);
				RenderTextureFormat renderTextureFormat = (int)ToLua.ToObject(L, 4);
				RenderTexture temporary3 = RenderTexture.GetTemporary(num7, num8, num9, renderTextureFormat);
				ToLua.Push(L, temporary3);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(int), typeof(RenderTextureFormat), typeof(RenderTextureReadWrite)))
			{
				int num10 = (int)LuaDLL.lua_tonumber(L, 1);
				int num11 = (int)LuaDLL.lua_tonumber(L, 2);
				int num12 = (int)LuaDLL.lua_tonumber(L, 3);
				RenderTextureFormat renderTextureFormat2 = (int)ToLua.ToObject(L, 4);
				RenderTextureReadWrite renderTextureReadWrite = (int)ToLua.ToObject(L, 5);
				RenderTexture temporary4 = RenderTexture.GetTemporary(num10, num11, num12, renderTextureFormat2, renderTextureReadWrite);
				ToLua.Push(L, temporary4);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(int), typeof(RenderTextureFormat), typeof(RenderTextureReadWrite), typeof(int)))
			{
				int num13 = (int)LuaDLL.lua_tonumber(L, 1);
				int num14 = (int)LuaDLL.lua_tonumber(L, 2);
				int num15 = (int)LuaDLL.lua_tonumber(L, 3);
				RenderTextureFormat renderTextureFormat3 = (int)ToLua.ToObject(L, 4);
				RenderTextureReadWrite renderTextureReadWrite2 = (int)ToLua.ToObject(L, 5);
				int num16 = (int)LuaDLL.lua_tonumber(L, 6);
				RenderTexture temporary5 = RenderTexture.GetTemporary(num13, num14, num15, renderTextureFormat3, renderTextureReadWrite2, num16);
				ToLua.Push(L, temporary5);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.RenderTexture.GetTemporary");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReleaseTemporary(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckUnityObject(L, 1, typeof(RenderTexture));
			RenderTexture.ReleaseTemporary(renderTexture);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Create(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckObject(L, 1, typeof(RenderTexture));
			bool value = renderTexture.Create();
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
	private static int Release(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckObject(L, 1, typeof(RenderTexture));
			renderTexture.Release();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsCreated(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckObject(L, 1, typeof(RenderTexture));
			bool value = renderTexture.IsCreated();
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
	private static int DiscardContents(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(RenderTexture)))
			{
				RenderTexture renderTexture = (RenderTexture)ToLua.ToObject(L, 1);
				renderTexture.DiscardContents();
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(RenderTexture), typeof(bool), typeof(bool)))
			{
				RenderTexture renderTexture2 = (RenderTexture)ToLua.ToObject(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				bool flag2 = LuaDLL.lua_toboolean(L, 3);
				renderTexture2.DiscardContents(flag, flag2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.RenderTexture.DiscardContents");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int MarkRestoreExpected(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckObject(L, 1, typeof(RenderTexture));
			renderTexture.MarkRestoreExpected();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalShaderProperty(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckObject(L, 1, typeof(RenderTexture));
			string globalShaderProperty = ToLua.CheckString(L, 2);
			renderTexture.SetGlobalShaderProperty(globalShaderProperty);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTexelOffset(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckObject(L, 1, typeof(RenderTexture));
			Vector2 texelOffset = renderTexture.GetTexelOffset();
			ToLua.Push(L, texelOffset);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SupportsStencil(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			RenderTexture renderTexture = (RenderTexture)ToLua.CheckUnityObject(L, 1, typeof(RenderTexture));
			bool value = RenderTexture.SupportsStencil(renderTexture);
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
	private static int get_width(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int width = renderTexture.get_width();
			LuaDLL.lua_pushinteger(L, width);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index width on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_height(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int height = renderTexture.get_height();
			LuaDLL.lua_pushinteger(L, height);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index height on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_depth(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int depth = renderTexture.get_depth();
			LuaDLL.lua_pushinteger(L, depth);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index depth on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPowerOfTwo(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool isPowerOfTwo = renderTexture.get_isPowerOfTwo();
			LuaDLL.lua_pushboolean(L, isPowerOfTwo);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isPowerOfTwo on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sRGB(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool sRGB = renderTexture.get_sRGB();
			LuaDLL.lua_pushboolean(L, sRGB);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sRGB on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_format(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			RenderTextureFormat format = renderTexture.get_format();
			ToLua.Push(L, format);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index format on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useMipMap(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool useMipMap = renderTexture.get_useMipMap();
			LuaDLL.lua_pushboolean(L, useMipMap);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useMipMap on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_generateMips(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool generateMips = renderTexture.get_generateMips();
			LuaDLL.lua_pushboolean(L, generateMips);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index generateMips on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isCubemap(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool isCubemap = renderTexture.get_isCubemap();
			LuaDLL.lua_pushboolean(L, isCubemap);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isCubemap on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isVolume(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool isVolume = renderTexture.get_isVolume();
			LuaDLL.lua_pushboolean(L, isVolume);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isVolume on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_volumeDepth(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int volumeDepth = renderTexture.get_volumeDepth();
			LuaDLL.lua_pushinteger(L, volumeDepth);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index volumeDepth on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_antiAliasing(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int antiAliasing = renderTexture.get_antiAliasing();
			LuaDLL.lua_pushinteger(L, antiAliasing);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index antiAliasing on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_enableRandomWrite(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool enableRandomWrite = renderTexture.get_enableRandomWrite();
			LuaDLL.lua_pushboolean(L, enableRandomWrite);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index enableRandomWrite on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_colorBuffer(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			RenderBuffer colorBuffer = renderTexture.get_colorBuffer();
			ToLua.PushValue(L, colorBuffer);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index colorBuffer on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_depthBuffer(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			RenderBuffer depthBuffer = renderTexture.get_depthBuffer();
			ToLua.PushValue(L, depthBuffer);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index depthBuffer on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_active(IntPtr L)
	{
		ToLua.Push(L, RenderTexture.get_active());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_width(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int width = (int)LuaDLL.luaL_checknumber(L, 2);
			renderTexture.set_width(width);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index width on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_height(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int height = (int)LuaDLL.luaL_checknumber(L, 2);
			renderTexture.set_height(height);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index height on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_depth(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int depth = (int)LuaDLL.luaL_checknumber(L, 2);
			renderTexture.set_depth(depth);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index depth on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isPowerOfTwo(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool isPowerOfTwo = LuaDLL.luaL_checkboolean(L, 2);
			renderTexture.set_isPowerOfTwo(isPowerOfTwo);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isPowerOfTwo on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_format(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			RenderTextureFormat format = (int)ToLua.CheckObject(L, 2, typeof(RenderTextureFormat));
			renderTexture.set_format(format);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index format on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useMipMap(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool useMipMap = LuaDLL.luaL_checkboolean(L, 2);
			renderTexture.set_useMipMap(useMipMap);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useMipMap on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_generateMips(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool generateMips = LuaDLL.luaL_checkboolean(L, 2);
			renderTexture.set_generateMips(generateMips);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index generateMips on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isCubemap(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool isCubemap = LuaDLL.luaL_checkboolean(L, 2);
			renderTexture.set_isCubemap(isCubemap);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isCubemap on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isVolume(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool isVolume = LuaDLL.luaL_checkboolean(L, 2);
			renderTexture.set_isVolume(isVolume);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isVolume on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_volumeDepth(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int volumeDepth = (int)LuaDLL.luaL_checknumber(L, 2);
			renderTexture.set_volumeDepth(volumeDepth);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index volumeDepth on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_antiAliasing(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			int antiAliasing = (int)LuaDLL.luaL_checknumber(L, 2);
			renderTexture.set_antiAliasing(antiAliasing);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index antiAliasing on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enableRandomWrite(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			RenderTexture renderTexture = (RenderTexture)obj;
			bool enableRandomWrite = LuaDLL.luaL_checkboolean(L, 2);
			renderTexture.set_enableRandomWrite(enableRandomWrite);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index enableRandomWrite on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_active(IntPtr L)
	{
		int result;
		try
		{
			RenderTexture active = (RenderTexture)ToLua.CheckUnityObject(L, 2, typeof(RenderTexture));
			RenderTexture.set_active(active);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
