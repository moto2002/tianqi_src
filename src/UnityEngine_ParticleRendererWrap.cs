using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_ParticleRendererWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ParticleRenderer), typeof(Renderer), null);
		L.RegFunction("New", new LuaCSFunction(UnityEngine_ParticleRendererWrap._CreateUnityEngine_ParticleRenderer));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_ParticleRendererWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_ParticleRendererWrap.Lua_ToString));
		L.RegVar("particleRenderMode", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_particleRenderMode), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_particleRenderMode));
		L.RegVar("lengthScale", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_lengthScale), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_lengthScale));
		L.RegVar("velocityScale", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_velocityScale), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_velocityScale));
		L.RegVar("cameraVelocityScale", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_cameraVelocityScale), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_cameraVelocityScale));
		L.RegVar("maxParticleSize", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_maxParticleSize), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_maxParticleSize));
		L.RegVar("uvAnimationXTile", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_uvAnimationXTile), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_uvAnimationXTile));
		L.RegVar("uvAnimationYTile", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_uvAnimationYTile), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_uvAnimationYTile));
		L.RegVar("uvAnimationCycles", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_uvAnimationCycles), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_uvAnimationCycles));
		L.RegVar("maxPartileSize", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_maxPartileSize), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_maxPartileSize));
		L.RegVar("uvTiles", new LuaCSFunction(UnityEngine_ParticleRendererWrap.get_uvTiles), new LuaCSFunction(UnityEngine_ParticleRendererWrap.set_uvTiles));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_ParticleRenderer(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				ParticleRenderer obj = new ParticleRenderer();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.ParticleRenderer.New");
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
	private static int get_particleRenderMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			ParticleRenderMode particleRenderMode = particleRenderer.get_particleRenderMode();
			ToLua.Push(L, particleRenderMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index particleRenderMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lengthScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float lengthScale = particleRenderer.get_lengthScale();
			LuaDLL.lua_pushnumber(L, (double)lengthScale);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index lengthScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocityScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float velocityScale = particleRenderer.get_velocityScale();
			LuaDLL.lua_pushnumber(L, (double)velocityScale);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index velocityScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cameraVelocityScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float cameraVelocityScale = particleRenderer.get_cameraVelocityScale();
			LuaDLL.lua_pushnumber(L, (double)cameraVelocityScale);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cameraVelocityScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxParticleSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float maxParticleSize = particleRenderer.get_maxParticleSize();
			LuaDLL.lua_pushnumber(L, (double)maxParticleSize);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxParticleSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationXTile(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			int uvAnimationXTile = particleRenderer.get_uvAnimationXTile();
			LuaDLL.lua_pushinteger(L, uvAnimationXTile);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvAnimationXTile on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationYTile(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			int uvAnimationYTile = particleRenderer.get_uvAnimationYTile();
			LuaDLL.lua_pushinteger(L, uvAnimationYTile);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvAnimationYTile on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationCycles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float uvAnimationCycles = particleRenderer.get_uvAnimationCycles();
			LuaDLL.lua_pushnumber(L, (double)uvAnimationCycles);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvAnimationCycles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxPartileSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float maxPartileSize = particleRenderer.get_maxPartileSize();
			LuaDLL.lua_pushnumber(L, (double)maxPartileSize);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxPartileSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvTiles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			Rect[] uvTiles = particleRenderer.get_uvTiles();
			ToLua.Push(L, uvTiles);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvTiles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_particleRenderMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			ParticleRenderMode particleRenderMode = (int)ToLua.CheckObject(L, 2, typeof(ParticleRenderMode));
			particleRenderer.set_particleRenderMode(particleRenderMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index particleRenderMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lengthScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float lengthScale = (float)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_lengthScale(lengthScale);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index lengthScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_velocityScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float velocityScale = (float)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_velocityScale(velocityScale);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index velocityScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cameraVelocityScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float cameraVelocityScale = (float)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_cameraVelocityScale(cameraVelocityScale);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cameraVelocityScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxParticleSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float maxParticleSize = (float)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_maxParticleSize(maxParticleSize);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxParticleSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationXTile(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			int uvAnimationXTile = (int)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_uvAnimationXTile(uvAnimationXTile);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvAnimationXTile on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationYTile(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			int uvAnimationYTile = (int)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_uvAnimationYTile(uvAnimationYTile);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvAnimationYTile on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationCycles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float uvAnimationCycles = (float)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_uvAnimationCycles(uvAnimationCycles);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvAnimationCycles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxPartileSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			float maxPartileSize = (float)LuaDLL.luaL_checknumber(L, 2);
			particleRenderer.set_maxPartileSize(maxPartileSize);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxPartileSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvTiles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleRenderer particleRenderer = (ParticleRenderer)obj;
			Rect[] uvTiles = ToLua.CheckObjectArray<Rect>(L, 2);
			particleRenderer.set_uvTiles(uvTiles);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index uvTiles on a nil value");
		}
		return result;
	}
}
