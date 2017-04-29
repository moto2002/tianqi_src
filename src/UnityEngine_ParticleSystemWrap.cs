using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_ParticleSystemWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ParticleSystem), typeof(Component), null);
		L.RegFunction("SetParticles", new LuaCSFunction(UnityEngine_ParticleSystemWrap.SetParticles));
		L.RegFunction("GetParticles", new LuaCSFunction(UnityEngine_ParticleSystemWrap.GetParticles));
		L.RegFunction("Simulate", new LuaCSFunction(UnityEngine_ParticleSystemWrap.Simulate));
		L.RegFunction("Play", new LuaCSFunction(UnityEngine_ParticleSystemWrap.Play));
		L.RegFunction("Stop", new LuaCSFunction(UnityEngine_ParticleSystemWrap.Stop));
		L.RegFunction("Pause", new LuaCSFunction(UnityEngine_ParticleSystemWrap.Pause));
		L.RegFunction("Clear", new LuaCSFunction(UnityEngine_ParticleSystemWrap.Clear));
		L.RegFunction("IsAlive", new LuaCSFunction(UnityEngine_ParticleSystemWrap.IsAlive));
		L.RegFunction("Emit", new LuaCSFunction(UnityEngine_ParticleSystemWrap.Emit));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_ParticleSystemWrap._CreateUnityEngine_ParticleSystem));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_ParticleSystemWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_ParticleSystemWrap.Lua_ToString));
		L.RegVar("startDelay", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_startDelay), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_startDelay));
		L.RegVar("isPlaying", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_isPlaying), null);
		L.RegVar("isStopped", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_isStopped), null);
		L.RegVar("isPaused", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_isPaused), null);
		L.RegVar("loop", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_loop), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_loop));
		L.RegVar("playOnAwake", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_playOnAwake), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_playOnAwake));
		L.RegVar("time", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_time), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_time));
		L.RegVar("duration", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_duration), null);
		L.RegVar("playbackSpeed", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_playbackSpeed), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_playbackSpeed));
		L.RegVar("particleCount", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_particleCount), null);
		L.RegVar("startSpeed", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_startSpeed), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_startSpeed));
		L.RegVar("startSize", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_startSize), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_startSize));
		L.RegVar("startColor", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_startColor), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_startColor));
		L.RegVar("startRotation", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_startRotation), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_startRotation));
		L.RegVar("startRotation3D", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_startRotation3D), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_startRotation3D));
		L.RegVar("startLifetime", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_startLifetime), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_startLifetime));
		L.RegVar("gravityModifier", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_gravityModifier), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_gravityModifier));
		L.RegVar("maxParticles", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_maxParticles), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_maxParticles));
		L.RegVar("simulationSpace", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_simulationSpace), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_simulationSpace));
		L.RegVar("scalingMode", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_scalingMode), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_scalingMode));
		L.RegVar("randomSeed", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_randomSeed), new LuaCSFunction(UnityEngine_ParticleSystemWrap.set_randomSeed));
		L.RegVar("emission", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_emission), null);
		L.RegVar("shape", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_shape), null);
		L.RegVar("velocityOverLifetime", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_velocityOverLifetime), null);
		L.RegVar("limitVelocityOverLifetime", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_limitVelocityOverLifetime), null);
		L.RegVar("inheritVelocity", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_inheritVelocity), null);
		L.RegVar("forceOverLifetime", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_forceOverLifetime), null);
		L.RegVar("colorOverLifetime", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_colorOverLifetime), null);
		L.RegVar("colorBySpeed", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_colorBySpeed), null);
		L.RegVar("sizeOverLifetime", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_sizeOverLifetime), null);
		L.RegVar("sizeBySpeed", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_sizeBySpeed), null);
		L.RegVar("rotationOverLifetime", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_rotationOverLifetime), null);
		L.RegVar("rotationBySpeed", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_rotationBySpeed), null);
		L.RegVar("externalForces", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_externalForces), null);
		L.RegVar("collision", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_collision), null);
		L.RegVar("subEmitters", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_subEmitters), null);
		L.RegVar("textureSheetAnimation", new LuaCSFunction(UnityEngine_ParticleSystemWrap.get_textureSheetAnimation), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_ParticleSystem(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				ParticleSystem obj = new ParticleSystem();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.ParticleSystem.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetParticles(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ParticleSystem particleSystem = (ParticleSystem)ToLua.CheckObject(L, 1, typeof(ParticleSystem));
			ParticleSystem.Particle[] array = ToLua.CheckObjectArray<ParticleSystem.Particle>(L, 2);
			int num = (int)LuaDLL.luaL_checknumber(L, 3);
			particleSystem.SetParticles(array, num);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetParticles(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ParticleSystem particleSystem = (ParticleSystem)ToLua.CheckObject(L, 1, typeof(ParticleSystem));
			ParticleSystem.Particle[] array = ToLua.CheckObjectArray<ParticleSystem.Particle>(L, 2);
			int particles = particleSystem.GetParticles(array);
			LuaDLL.lua_pushinteger(L, particles);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Simulate(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(float)))
			{
				ParticleSystem particleSystem = (ParticleSystem)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				particleSystem.Simulate(num2);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(float), typeof(bool)))
			{
				ParticleSystem particleSystem2 = (ParticleSystem)ToLua.ToObject(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				particleSystem2.Simulate(num3, flag);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(float), typeof(bool), typeof(bool)))
			{
				ParticleSystem particleSystem3 = (ParticleSystem)ToLua.ToObject(L, 1);
				float num4 = (float)LuaDLL.lua_tonumber(L, 2);
				bool flag2 = LuaDLL.lua_toboolean(L, 3);
				bool flag3 = LuaDLL.lua_toboolean(L, 4);
				particleSystem3.Simulate(num4, flag2, flag3);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Simulate");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Play(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem)))
			{
				ParticleSystem particleSystem = (ParticleSystem)ToLua.ToObject(L, 1);
				particleSystem.Play();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(bool)))
			{
				ParticleSystem particleSystem2 = (ParticleSystem)ToLua.ToObject(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				particleSystem2.Play(flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Play");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Stop(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem)))
			{
				ParticleSystem particleSystem = (ParticleSystem)ToLua.ToObject(L, 1);
				particleSystem.Stop();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(bool)))
			{
				ParticleSystem particleSystem2 = (ParticleSystem)ToLua.ToObject(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				particleSystem2.Stop(flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Stop");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Pause(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem)))
			{
				ParticleSystem particleSystem = (ParticleSystem)ToLua.ToObject(L, 1);
				particleSystem.Pause();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(bool)))
			{
				ParticleSystem particleSystem2 = (ParticleSystem)ToLua.ToObject(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				particleSystem2.Pause(flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Pause");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Clear(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem)))
			{
				ParticleSystem particleSystem = (ParticleSystem)ToLua.ToObject(L, 1);
				particleSystem.Clear();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(bool)))
			{
				ParticleSystem particleSystem2 = (ParticleSystem)ToLua.ToObject(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				particleSystem2.Clear(flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Clear");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsAlive(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem)))
			{
				ParticleSystem particleSystem = (ParticleSystem)ToLua.ToObject(L, 1);
				bool value = particleSystem.IsAlive();
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(bool)))
			{
				ParticleSystem particleSystem2 = (ParticleSystem)ToLua.ToObject(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				bool value2 = particleSystem2.IsAlive(flag);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.IsAlive");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Emit(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(int)))
			{
				ParticleSystem particleSystem = (ParticleSystem)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				particleSystem.Emit(num2);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(ParticleSystem), typeof(ParticleSystem.EmitParams), typeof(int)))
			{
				ParticleSystem particleSystem2 = (ParticleSystem)ToLua.ToObject(L, 1);
				ParticleSystem.EmitParams emitParams = (ParticleSystem.EmitParams)ToLua.ToObject(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				particleSystem2.Emit(emitParams, num3);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Emit");
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
	private static int get_startDelay(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startDelay = particleSystem.get_startDelay();
			LuaDLL.lua_pushnumber(L, (double)startDelay);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startDelay on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPlaying(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			bool isPlaying = particleSystem.get_isPlaying();
			LuaDLL.lua_pushboolean(L, isPlaying);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isPlaying on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isStopped(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			bool isStopped = particleSystem.get_isStopped();
			LuaDLL.lua_pushboolean(L, isStopped);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isStopped on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPaused(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			bool isPaused = particleSystem.get_isPaused();
			LuaDLL.lua_pushboolean(L, isPaused);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isPaused on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loop(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			bool loop = particleSystem.get_loop();
			LuaDLL.lua_pushboolean(L, loop);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index loop on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_playOnAwake(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			bool playOnAwake = particleSystem.get_playOnAwake();
			LuaDLL.lua_pushboolean(L, playOnAwake);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index playOnAwake on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_time(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float time = particleSystem.get_time();
			LuaDLL.lua_pushnumber(L, (double)time);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index time on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_duration(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float duration = particleSystem.get_duration();
			LuaDLL.lua_pushnumber(L, (double)duration);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index duration on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_playbackSpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float playbackSpeed = particleSystem.get_playbackSpeed();
			LuaDLL.lua_pushnumber(L, (double)playbackSpeed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index playbackSpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particleCount(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			int particleCount = particleSystem.get_particleCount();
			LuaDLL.lua_pushinteger(L, particleCount);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index particleCount on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_startSpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startSpeed = particleSystem.get_startSpeed();
			LuaDLL.lua_pushnumber(L, (double)startSpeed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startSpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_startSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startSize = particleSystem.get_startSize();
			LuaDLL.lua_pushnumber(L, (double)startSize);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_startColor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			Color startColor = particleSystem.get_startColor();
			ToLua.Push(L, startColor);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startColor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_startRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startRotation = particleSystem.get_startRotation();
			LuaDLL.lua_pushnumber(L, (double)startRotation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_startRotation3D(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			Vector3 startRotation3D = particleSystem.get_startRotation3D();
			ToLua.Push(L, startRotation3D);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startRotation3D on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_startLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startLifetime = particleSystem.get_startLifetime();
			LuaDLL.lua_pushnumber(L, (double)startLifetime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gravityModifier(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float gravityModifier = particleSystem.get_gravityModifier();
			LuaDLL.lua_pushnumber(L, (double)gravityModifier);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index gravityModifier on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxParticles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			int maxParticles = particleSystem.get_maxParticles();
			LuaDLL.lua_pushinteger(L, maxParticles);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxParticles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_simulationSpace(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystemSimulationSpace simulationSpace = particleSystem.get_simulationSpace();
			ToLua.Push(L, simulationSpace);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index simulationSpace on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_scalingMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystemScalingMode scalingMode = particleSystem.get_scalingMode();
			ToLua.Push(L, scalingMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index scalingMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_randomSeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			uint randomSeed = particleSystem.get_randomSeed();
			LuaDLL.lua_pushnumber(L, randomSeed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index randomSeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_emission(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.EmissionModule emission = particleSystem.get_emission();
			ToLua.PushValue(L, emission);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index emission on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shape(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.ShapeModule shape = particleSystem.get_shape();
			ToLua.PushValue(L, shape);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index shape on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocityOverLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = particleSystem.get_velocityOverLifetime();
			ToLua.PushValue(L, velocityOverLifetime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index velocityOverLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_limitVelocityOverLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime = particleSystem.get_limitVelocityOverLifetime();
			ToLua.PushValue(L, limitVelocityOverLifetime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index limitVelocityOverLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_inheritVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.InheritVelocityModule inheritVelocity = particleSystem.get_inheritVelocity();
			ToLua.PushValue(L, inheritVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index inheritVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_forceOverLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.ForceOverLifetimeModule forceOverLifetime = particleSystem.get_forceOverLifetime();
			ToLua.PushValue(L, forceOverLifetime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index forceOverLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_colorOverLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.ColorOverLifetimeModule colorOverLifetime = particleSystem.get_colorOverLifetime();
			ToLua.PushValue(L, colorOverLifetime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index colorOverLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_colorBySpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.ColorBySpeedModule colorBySpeed = particleSystem.get_colorBySpeed();
			ToLua.PushValue(L, colorBySpeed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index colorBySpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sizeOverLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetime = particleSystem.get_sizeOverLifetime();
			ToLua.PushValue(L, sizeOverLifetime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sizeOverLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sizeBySpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.SizeBySpeedModule sizeBySpeed = particleSystem.get_sizeBySpeed();
			ToLua.PushValue(L, sizeBySpeed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sizeBySpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rotationOverLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.RotationOverLifetimeModule rotationOverLifetime = particleSystem.get_rotationOverLifetime();
			ToLua.PushValue(L, rotationOverLifetime);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rotationOverLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rotationBySpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.RotationBySpeedModule rotationBySpeed = particleSystem.get_rotationBySpeed();
			ToLua.PushValue(L, rotationBySpeed);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rotationBySpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_externalForces(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.ExternalForcesModule externalForces = particleSystem.get_externalForces();
			ToLua.PushValue(L, externalForces);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index externalForces on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_collision(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.CollisionModule collision = particleSystem.get_collision();
			ToLua.PushValue(L, collision);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index collision on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_subEmitters(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.SubEmittersModule subEmitters = particleSystem.get_subEmitters();
			ToLua.PushValue(L, subEmitters);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index subEmitters on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_textureSheetAnimation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystem.TextureSheetAnimationModule textureSheetAnimation = particleSystem.get_textureSheetAnimation();
			ToLua.PushValue(L, textureSheetAnimation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index textureSheetAnimation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startDelay(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startDelay = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_startDelay(startDelay);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startDelay on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_loop(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			bool loop = LuaDLL.luaL_checkboolean(L, 2);
			particleSystem.set_loop(loop);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index loop on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_playOnAwake(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			bool playOnAwake = LuaDLL.luaL_checkboolean(L, 2);
			particleSystem.set_playOnAwake(playOnAwake);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index playOnAwake on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_time(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float time = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_time(time);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index time on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_playbackSpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float playbackSpeed = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_playbackSpeed(playbackSpeed);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index playbackSpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startSpeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startSpeed = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_startSpeed(startSpeed);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startSpeed on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startSize = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_startSize(startSize);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startColor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			Color startColor = ToLua.ToColor(L, 2);
			particleSystem.set_startColor(startColor);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startColor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startRotation = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_startRotation(startRotation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startRotation3D(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			Vector3 startRotation3D = ToLua.ToVector3(L, 2);
			particleSystem.set_startRotation3D(startRotation3D);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startRotation3D on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startLifetime(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float startLifetime = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_startLifetime(startLifetime);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index startLifetime on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_gravityModifier(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			float gravityModifier = (float)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_gravityModifier(gravityModifier);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index gravityModifier on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxParticles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			int maxParticles = (int)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_maxParticles(maxParticles);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxParticles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_simulationSpace(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystemSimulationSpace simulationSpace = (int)ToLua.CheckObject(L, 2, typeof(ParticleSystemSimulationSpace));
			particleSystem.set_simulationSpace(simulationSpace);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index simulationSpace on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_scalingMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			ParticleSystemScalingMode scalingMode = (int)ToLua.CheckObject(L, 2, typeof(ParticleSystemScalingMode));
			particleSystem.set_scalingMode(scalingMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index scalingMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_randomSeed(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleSystem particleSystem = (ParticleSystem)obj;
			uint randomSeed = (uint)LuaDLL.luaL_checknumber(L, 2);
			particleSystem.set_randomSeed(randomSeed);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index randomSeed on a nil value");
		}
		return result;
	}
}
