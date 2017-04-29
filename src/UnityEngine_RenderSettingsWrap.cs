using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class UnityEngine_RenderSettingsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("RenderSettings");
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_RenderSettingsWrap.op_Equality));
		L.RegVar("fog", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_fog), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_fog));
		L.RegVar("fogMode", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_fogMode), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_fogMode));
		L.RegVar("fogColor", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_fogColor), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_fogColor));
		L.RegVar("fogDensity", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_fogDensity), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_fogDensity));
		L.RegVar("fogStartDistance", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_fogStartDistance), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_fogStartDistance));
		L.RegVar("fogEndDistance", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_fogEndDistance), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_fogEndDistance));
		L.RegVar("ambientMode", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_ambientMode), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_ambientMode));
		L.RegVar("ambientSkyColor", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_ambientSkyColor), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_ambientSkyColor));
		L.RegVar("ambientEquatorColor", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_ambientEquatorColor), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_ambientEquatorColor));
		L.RegVar("ambientGroundColor", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_ambientGroundColor), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_ambientGroundColor));
		L.RegVar("ambientLight", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_ambientLight), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_ambientLight));
		L.RegVar("ambientIntensity", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_ambientIntensity), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_ambientIntensity));
		L.RegVar("ambientProbe", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_ambientProbe), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_ambientProbe));
		L.RegVar("reflectionIntensity", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_reflectionIntensity), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_reflectionIntensity));
		L.RegVar("reflectionBounces", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_reflectionBounces), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_reflectionBounces));
		L.RegVar("haloStrength", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_haloStrength), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_haloStrength));
		L.RegVar("flareStrength", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_flareStrength), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_flareStrength));
		L.RegVar("flareFadeSpeed", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_flareFadeSpeed), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_flareFadeSpeed));
		L.RegVar("skybox", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_skybox), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_skybox));
		L.RegVar("defaultReflectionMode", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_defaultReflectionMode), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_defaultReflectionMode));
		L.RegVar("defaultReflectionResolution", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_defaultReflectionResolution), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_defaultReflectionResolution));
		L.RegVar("customReflection", new LuaCSFunction(UnityEngine_RenderSettingsWrap.get_customReflection), new LuaCSFunction(UnityEngine_RenderSettingsWrap.set_customReflection));
		L.EndStaticLibs();
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
	private static int get_fog(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, RenderSettings.get_fog());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogMode(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_fogMode());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogColor(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_fogColor());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogDensity(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_fogDensity());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogStartDistance(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_fogStartDistance());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fogEndDistance(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_fogEndDistance());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientMode(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_ambientMode());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientSkyColor(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_ambientSkyColor());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientEquatorColor(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_ambientEquatorColor());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientGroundColor(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_ambientGroundColor());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientLight(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_ambientLight());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientIntensity(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_ambientIntensity());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ambientProbe(IntPtr L)
	{
		ToLua.PushValue(L, RenderSettings.get_ambientProbe());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reflectionIntensity(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_reflectionIntensity());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reflectionBounces(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, RenderSettings.get_reflectionBounces());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_haloStrength(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_haloStrength());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_flareStrength(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_flareStrength());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_flareFadeSpeed(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)RenderSettings.get_flareFadeSpeed());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_skybox(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_skybox());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultReflectionMode(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_defaultReflectionMode());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultReflectionResolution(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, RenderSettings.get_defaultReflectionResolution());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_customReflection(IntPtr L)
	{
		ToLua.Push(L, RenderSettings.get_customReflection());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fog(IntPtr L)
	{
		int result;
		try
		{
			bool fog = LuaDLL.luaL_checkboolean(L, 2);
			RenderSettings.set_fog(fog);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogMode(IntPtr L)
	{
		int result;
		try
		{
			FogMode fogMode = (int)ToLua.CheckObject(L, 2, typeof(FogMode));
			RenderSettings.set_fogMode(fogMode);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogColor(IntPtr L)
	{
		int result;
		try
		{
			Color fogColor = ToLua.ToColor(L, 2);
			RenderSettings.set_fogColor(fogColor);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogDensity(IntPtr L)
	{
		int result;
		try
		{
			float fogDensity = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_fogDensity(fogDensity);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogStartDistance(IntPtr L)
	{
		int result;
		try
		{
			float fogStartDistance = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_fogStartDistance(fogStartDistance);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fogEndDistance(IntPtr L)
	{
		int result;
		try
		{
			float fogEndDistance = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_fogEndDistance(fogEndDistance);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientMode(IntPtr L)
	{
		int result;
		try
		{
			AmbientMode ambientMode = (int)ToLua.CheckObject(L, 2, typeof(AmbientMode));
			RenderSettings.set_ambientMode(ambientMode);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientSkyColor(IntPtr L)
	{
		int result;
		try
		{
			Color ambientSkyColor = ToLua.ToColor(L, 2);
			RenderSettings.set_ambientSkyColor(ambientSkyColor);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientEquatorColor(IntPtr L)
	{
		int result;
		try
		{
			Color ambientEquatorColor = ToLua.ToColor(L, 2);
			RenderSettings.set_ambientEquatorColor(ambientEquatorColor);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientGroundColor(IntPtr L)
	{
		int result;
		try
		{
			Color ambientGroundColor = ToLua.ToColor(L, 2);
			RenderSettings.set_ambientGroundColor(ambientGroundColor);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientLight(IntPtr L)
	{
		int result;
		try
		{
			Color ambientLight = ToLua.ToColor(L, 2);
			RenderSettings.set_ambientLight(ambientLight);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientIntensity(IntPtr L)
	{
		int result;
		try
		{
			float ambientIntensity = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_ambientIntensity(ambientIntensity);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ambientProbe(IntPtr L)
	{
		int result;
		try
		{
			SphericalHarmonicsL2 ambientProbe = (SphericalHarmonicsL2)ToLua.CheckObject(L, 2, typeof(SphericalHarmonicsL2));
			RenderSettings.set_ambientProbe(ambientProbe);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reflectionIntensity(IntPtr L)
	{
		int result;
		try
		{
			float reflectionIntensity = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_reflectionIntensity(reflectionIntensity);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reflectionBounces(IntPtr L)
	{
		int result;
		try
		{
			int reflectionBounces = (int)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_reflectionBounces(reflectionBounces);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_haloStrength(IntPtr L)
	{
		int result;
		try
		{
			float haloStrength = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_haloStrength(haloStrength);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_flareStrength(IntPtr L)
	{
		int result;
		try
		{
			float flareStrength = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_flareStrength(flareStrength);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_flareFadeSpeed(IntPtr L)
	{
		int result;
		try
		{
			float flareFadeSpeed = (float)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_flareFadeSpeed(flareFadeSpeed);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_skybox(IntPtr L)
	{
		int result;
		try
		{
			Material skybox = (Material)ToLua.CheckUnityObject(L, 2, typeof(Material));
			RenderSettings.set_skybox(skybox);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultReflectionMode(IntPtr L)
	{
		int result;
		try
		{
			DefaultReflectionMode defaultReflectionMode = (int)ToLua.CheckObject(L, 2, typeof(DefaultReflectionMode));
			RenderSettings.set_defaultReflectionMode(defaultReflectionMode);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultReflectionResolution(IntPtr L)
	{
		int result;
		try
		{
			int defaultReflectionResolution = (int)LuaDLL.luaL_checknumber(L, 2);
			RenderSettings.set_defaultReflectionResolution(defaultReflectionResolution);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_customReflection(IntPtr L)
	{
		int result;
		try
		{
			Cubemap customReflection = (Cubemap)ToLua.CheckUnityObject(L, 2, typeof(Cubemap));
			RenderSettings.set_customReflection(customReflection);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
