using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_QualitySettingsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("QualitySettings");
		L.RegFunction("GetQualityLevel", new LuaCSFunction(UnityEngine_QualitySettingsWrap.GetQualityLevel));
		L.RegFunction("SetQualityLevel", new LuaCSFunction(UnityEngine_QualitySettingsWrap.SetQualityLevel));
		L.RegFunction("IncreaseLevel", new LuaCSFunction(UnityEngine_QualitySettingsWrap.IncreaseLevel));
		L.RegFunction("DecreaseLevel", new LuaCSFunction(UnityEngine_QualitySettingsWrap.DecreaseLevel));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_QualitySettingsWrap.op_Equality));
		L.RegVar("names", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_names), null);
		L.RegVar("pixelLightCount", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_pixelLightCount), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_pixelLightCount));
		L.RegVar("shadowProjection", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_shadowProjection), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_shadowProjection));
		L.RegVar("shadowCascades", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_shadowCascades), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_shadowCascades));
		L.RegVar("shadowDistance", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_shadowDistance), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_shadowDistance));
		L.RegVar("shadowNearPlaneOffset", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_shadowNearPlaneOffset), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_shadowNearPlaneOffset));
		L.RegVar("shadowCascade2Split", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_shadowCascade2Split), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_shadowCascade2Split));
		L.RegVar("shadowCascade4Split", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_shadowCascade4Split), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_shadowCascade4Split));
		L.RegVar("masterTextureLimit", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_masterTextureLimit), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_masterTextureLimit));
		L.RegVar("anisotropicFiltering", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_anisotropicFiltering), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_anisotropicFiltering));
		L.RegVar("lodBias", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_lodBias), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_lodBias));
		L.RegVar("maximumLODLevel", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_maximumLODLevel), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_maximumLODLevel));
		L.RegVar("particleRaycastBudget", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_particleRaycastBudget), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_particleRaycastBudget));
		L.RegVar("softVegetation", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_softVegetation), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_softVegetation));
		L.RegVar("realtimeReflectionProbes", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_realtimeReflectionProbes), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_realtimeReflectionProbes));
		L.RegVar("billboardsFaceCameraPosition", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_billboardsFaceCameraPosition), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_billboardsFaceCameraPosition));
		L.RegVar("maxQueuedFrames", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_maxQueuedFrames), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_maxQueuedFrames));
		L.RegVar("vSyncCount", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_vSyncCount), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_vSyncCount));
		L.RegVar("antiAliasing", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_antiAliasing), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_antiAliasing));
		L.RegVar("desiredColorSpace", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_desiredColorSpace), null);
		L.RegVar("activeColorSpace", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_activeColorSpace), null);
		L.RegVar("blendWeights", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_blendWeights), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_blendWeights));
		L.RegVar("asyncUploadTimeSlice", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_asyncUploadTimeSlice), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_asyncUploadTimeSlice));
		L.RegVar("asyncUploadBufferSize", new LuaCSFunction(UnityEngine_QualitySettingsWrap.get_asyncUploadBufferSize), new LuaCSFunction(UnityEngine_QualitySettingsWrap.set_asyncUploadBufferSize));
		L.EndStaticLibs();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetQualityLevel(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			int qualityLevel = QualitySettings.GetQualityLevel();
			LuaDLL.lua_pushinteger(L, qualityLevel);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetQualityLevel(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(int)))
			{
				int qualityLevel = (int)LuaDLL.lua_tonumber(L, 1);
				QualitySettings.SetQualityLevel(qualityLevel);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(bool)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				QualitySettings.SetQualityLevel(num2, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.QualitySettings.SetQualityLevel");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IncreaseLevel(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 0)
			{
				QualitySettings.IncreaseLevel();
				result = 0;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(bool)))
			{
				bool flag = LuaDLL.lua_toboolean(L, 1);
				QualitySettings.IncreaseLevel(flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.QualitySettings.IncreaseLevel");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DecreaseLevel(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 0)
			{
				QualitySettings.DecreaseLevel();
				result = 0;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(bool)))
			{
				bool flag = LuaDLL.lua_toboolean(L, 1);
				QualitySettings.DecreaseLevel(flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.QualitySettings.DecreaseLevel");
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
	private static int get_names(IntPtr L)
	{
		ToLua.Push(L, QualitySettings.get_names());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pixelLightCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_pixelLightCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowProjection(IntPtr L)
	{
		ToLua.Push(L, QualitySettings.get_shadowProjection());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCascades(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_shadowCascades());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowDistance(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)QualitySettings.get_shadowDistance());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowNearPlaneOffset(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)QualitySettings.get_shadowNearPlaneOffset());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCascade2Split(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)QualitySettings.get_shadowCascade2Split());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCascade4Split(IntPtr L)
	{
		ToLua.Push(L, QualitySettings.get_shadowCascade4Split());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_masterTextureLimit(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_masterTextureLimit());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anisotropicFiltering(IntPtr L)
	{
		ToLua.Push(L, QualitySettings.get_anisotropicFiltering());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lodBias(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)QualitySettings.get_lodBias());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maximumLODLevel(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_maximumLODLevel());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particleRaycastBudget(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_particleRaycastBudget());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_softVegetation(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, QualitySettings.get_softVegetation());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_realtimeReflectionProbes(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, QualitySettings.get_realtimeReflectionProbes());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_billboardsFaceCameraPosition(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, QualitySettings.get_billboardsFaceCameraPosition());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxQueuedFrames(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_maxQueuedFrames());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_vSyncCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_vSyncCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_antiAliasing(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_antiAliasing());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_desiredColorSpace(IntPtr L)
	{
		ToLua.Push(L, QualitySettings.get_desiredColorSpace());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_activeColorSpace(IntPtr L)
	{
		ToLua.Push(L, QualitySettings.get_activeColorSpace());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_blendWeights(IntPtr L)
	{
		ToLua.Push(L, QualitySettings.get_blendWeights());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_asyncUploadTimeSlice(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_asyncUploadTimeSlice());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_asyncUploadBufferSize(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, QualitySettings.get_asyncUploadBufferSize());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pixelLightCount(IntPtr L)
	{
		int result;
		try
		{
			int pixelLightCount = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_pixelLightCount(pixelLightCount);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowProjection(IntPtr L)
	{
		int result;
		try
		{
			ShadowProjection shadowProjection = (int)ToLua.CheckObject(L, 2, typeof(ShadowProjection));
			QualitySettings.set_shadowProjection(shadowProjection);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCascades(IntPtr L)
	{
		int result;
		try
		{
			int shadowCascades = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_shadowCascades(shadowCascades);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowDistance(IntPtr L)
	{
		int result;
		try
		{
			float shadowDistance = (float)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_shadowDistance(shadowDistance);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowNearPlaneOffset(IntPtr L)
	{
		int result;
		try
		{
			float shadowNearPlaneOffset = (float)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_shadowNearPlaneOffset(shadowNearPlaneOffset);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCascade2Split(IntPtr L)
	{
		int result;
		try
		{
			float shadowCascade2Split = (float)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_shadowCascade2Split(shadowCascade2Split);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCascade4Split(IntPtr L)
	{
		int result;
		try
		{
			Vector3 shadowCascade4Split = ToLua.ToVector3(L, 2);
			QualitySettings.set_shadowCascade4Split(shadowCascade4Split);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_masterTextureLimit(IntPtr L)
	{
		int result;
		try
		{
			int masterTextureLimit = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_masterTextureLimit(masterTextureLimit);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anisotropicFiltering(IntPtr L)
	{
		int result;
		try
		{
			AnisotropicFiltering anisotropicFiltering = (int)ToLua.CheckObject(L, 2, typeof(AnisotropicFiltering));
			QualitySettings.set_anisotropicFiltering(anisotropicFiltering);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lodBias(IntPtr L)
	{
		int result;
		try
		{
			float lodBias = (float)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_lodBias(lodBias);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maximumLODLevel(IntPtr L)
	{
		int result;
		try
		{
			int maximumLODLevel = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_maximumLODLevel(maximumLODLevel);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_particleRaycastBudget(IntPtr L)
	{
		int result;
		try
		{
			int particleRaycastBudget = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_particleRaycastBudget(particleRaycastBudget);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_softVegetation(IntPtr L)
	{
		int result;
		try
		{
			bool softVegetation = LuaDLL.luaL_checkboolean(L, 2);
			QualitySettings.set_softVegetation(softVegetation);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_realtimeReflectionProbes(IntPtr L)
	{
		int result;
		try
		{
			bool realtimeReflectionProbes = LuaDLL.luaL_checkboolean(L, 2);
			QualitySettings.set_realtimeReflectionProbes(realtimeReflectionProbes);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_billboardsFaceCameraPosition(IntPtr L)
	{
		int result;
		try
		{
			bool billboardsFaceCameraPosition = LuaDLL.luaL_checkboolean(L, 2);
			QualitySettings.set_billboardsFaceCameraPosition(billboardsFaceCameraPosition);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxQueuedFrames(IntPtr L)
	{
		int result;
		try
		{
			int maxQueuedFrames = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_maxQueuedFrames(maxQueuedFrames);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_vSyncCount(IntPtr L)
	{
		int result;
		try
		{
			int vSyncCount = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_vSyncCount(vSyncCount);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_antiAliasing(IntPtr L)
	{
		int result;
		try
		{
			int antiAliasing = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_antiAliasing(antiAliasing);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_blendWeights(IntPtr L)
	{
		int result;
		try
		{
			BlendWeights blendWeights = (int)ToLua.CheckObject(L, 2, typeof(BlendWeights));
			QualitySettings.set_blendWeights(blendWeights);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_asyncUploadTimeSlice(IntPtr L)
	{
		int result;
		try
		{
			int asyncUploadTimeSlice = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_asyncUploadTimeSlice(asyncUploadTimeSlice);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_asyncUploadBufferSize(IntPtr L)
	{
		int result;
		try
		{
			int asyncUploadBufferSize = (int)LuaDLL.luaL_checknumber(L, 2);
			QualitySettings.set_asyncUploadBufferSize(asyncUploadBufferSize);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
