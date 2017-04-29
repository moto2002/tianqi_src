using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_ParticleEmitterWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ParticleEmitter), typeof(Component), null);
		L.RegFunction("ClearParticles", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.ClearParticles));
		L.RegFunction("Emit", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.Emit));
		L.RegFunction("Simulate", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.Simulate));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.Lua_ToString));
		L.RegVar("emit", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_emit), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_emit));
		L.RegVar("minSize", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_minSize), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_minSize));
		L.RegVar("maxSize", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_maxSize), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_maxSize));
		L.RegVar("minEnergy", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_minEnergy), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_minEnergy));
		L.RegVar("maxEnergy", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_maxEnergy), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_maxEnergy));
		L.RegVar("minEmission", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_minEmission), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_minEmission));
		L.RegVar("maxEmission", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_maxEmission), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_maxEmission));
		L.RegVar("emitterVelocityScale", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_emitterVelocityScale), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_emitterVelocityScale));
		L.RegVar("worldVelocity", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_worldVelocity), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_worldVelocity));
		L.RegVar("localVelocity", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_localVelocity), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_localVelocity));
		L.RegVar("rndVelocity", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_rndVelocity), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_rndVelocity));
		L.RegVar("useWorldSpace", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_useWorldSpace), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_useWorldSpace));
		L.RegVar("rndRotation", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_rndRotation), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_rndRotation));
		L.RegVar("angularVelocity", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_angularVelocity), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_angularVelocity));
		L.RegVar("rndAngularVelocity", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_rndAngularVelocity), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_rndAngularVelocity));
		L.RegVar("particles", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_particles), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_particles));
		L.RegVar("particleCount", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_particleCount), null);
		L.RegVar("enabled", new LuaCSFunction(UnityEngine_ParticleEmitterWrap.get_enabled), new LuaCSFunction(UnityEngine_ParticleEmitterWrap.set_enabled));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearParticles(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)ToLua.CheckObject(L, 1, typeof(ParticleEmitter));
			particleEmitter.ClearParticles();
			result = 0;
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
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(ParticleEmitter)))
			{
				ParticleEmitter particleEmitter = (ParticleEmitter)ToLua.ToObject(L, 1);
				particleEmitter.Emit();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(ParticleEmitter), typeof(int)))
			{
				ParticleEmitter particleEmitter2 = (ParticleEmitter)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				particleEmitter2.Emit(num2);
				result = 0;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(ParticleEmitter), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float), typeof(Color)))
			{
				ParticleEmitter particleEmitter3 = (ParticleEmitter)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 vector2 = ToLua.ToVector3(L, 3);
				float num3 = (float)LuaDLL.lua_tonumber(L, 4);
				float num4 = (float)LuaDLL.lua_tonumber(L, 5);
				Color color = ToLua.ToColor(L, 6);
				particleEmitter3.Emit(vector, vector2, num3, num4, color);
				result = 0;
			}
			else if (num == 8 && TypeChecker.CheckTypes(L, 1, typeof(ParticleEmitter), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float), typeof(Color), typeof(float), typeof(float)))
			{
				ParticleEmitter particleEmitter4 = (ParticleEmitter)ToLua.ToObject(L, 1);
				Vector3 vector3 = ToLua.ToVector3(L, 2);
				Vector3 vector4 = ToLua.ToVector3(L, 3);
				float num5 = (float)LuaDLL.lua_tonumber(L, 4);
				float num6 = (float)LuaDLL.lua_tonumber(L, 5);
				Color color2 = ToLua.ToColor(L, 6);
				float num7 = (float)LuaDLL.lua_tonumber(L, 7);
				float num8 = (float)LuaDLL.lua_tonumber(L, 8);
				particleEmitter4.Emit(vector3, vector4, num5, num6, color2, num7, num8);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleEmitter.Emit");
			}
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
			ToLua.CheckArgsCount(L, 2);
			ParticleEmitter particleEmitter = (ParticleEmitter)ToLua.CheckObject(L, 1, typeof(ParticleEmitter));
			float num = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.Simulate(num);
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
	private static int get_emit(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool emit = particleEmitter.get_emit();
			LuaDLL.lua_pushboolean(L, emit);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index emit on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float minSize = particleEmitter.get_minSize();
			LuaDLL.lua_pushnumber(L, (double)minSize);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index minSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float maxSize = particleEmitter.get_maxSize();
			LuaDLL.lua_pushnumber(L, (double)maxSize);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minEnergy(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float minEnergy = particleEmitter.get_minEnergy();
			LuaDLL.lua_pushnumber(L, (double)minEnergy);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index minEnergy on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxEnergy(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float maxEnergy = particleEmitter.get_maxEnergy();
			LuaDLL.lua_pushnumber(L, (double)maxEnergy);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxEnergy on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minEmission(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float minEmission = particleEmitter.get_minEmission();
			LuaDLL.lua_pushnumber(L, (double)minEmission);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index minEmission on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxEmission(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float maxEmission = particleEmitter.get_maxEmission();
			LuaDLL.lua_pushnumber(L, (double)maxEmission);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxEmission on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_emitterVelocityScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float emitterVelocityScale = particleEmitter.get_emitterVelocityScale();
			LuaDLL.lua_pushnumber(L, (double)emitterVelocityScale);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index emitterVelocityScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Vector3 worldVelocity = particleEmitter.get_worldVelocity();
			ToLua.Push(L, worldVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Vector3 localVelocity = particleEmitter.get_localVelocity();
			ToLua.Push(L, localVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rndVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Vector3 rndVelocity = particleEmitter.get_rndVelocity();
			ToLua.Push(L, rndVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useWorldSpace(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool useWorldSpace = particleEmitter.get_useWorldSpace();
			LuaDLL.lua_pushboolean(L, useWorldSpace);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useWorldSpace on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rndRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool rndRotation = particleEmitter.get_rndRotation();
			LuaDLL.lua_pushboolean(L, rndRotation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_angularVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float angularVelocity = particleEmitter.get_angularVelocity();
			LuaDLL.lua_pushnumber(L, (double)angularVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index angularVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rndAngularVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float rndAngularVelocity = particleEmitter.get_rndAngularVelocity();
			LuaDLL.lua_pushnumber(L, (double)rndAngularVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndAngularVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Particle[] particles = particleEmitter.get_particles();
			ToLua.Push(L, particles);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index particles on a nil value");
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
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			int particleCount = particleEmitter.get_particleCount();
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
	private static int get_enabled(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool enabled = particleEmitter.get_enabled();
			LuaDLL.lua_pushboolean(L, enabled);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index enabled on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_emit(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool emit = LuaDLL.luaL_checkboolean(L, 2);
			particleEmitter.set_emit(emit);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index emit on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float minSize = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_minSize(minSize);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index minSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float maxSize = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_maxSize(maxSize);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minEnergy(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float minEnergy = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_minEnergy(minEnergy);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index minEnergy on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxEnergy(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float maxEnergy = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_maxEnergy(maxEnergy);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxEnergy on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minEmission(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float minEmission = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_minEmission(minEmission);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index minEmission on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxEmission(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float maxEmission = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_maxEmission(maxEmission);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxEmission on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_emitterVelocityScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float emitterVelocityScale = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_emitterVelocityScale(emitterVelocityScale);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index emitterVelocityScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_worldVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Vector3 worldVelocity = ToLua.ToVector3(L, 2);
			particleEmitter.set_worldVelocity(worldVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Vector3 localVelocity = ToLua.ToVector3(L, 2);
			particleEmitter.set_localVelocity(localVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index localVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rndVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Vector3 rndVelocity = ToLua.ToVector3(L, 2);
			particleEmitter.set_rndVelocity(rndVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useWorldSpace(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool useWorldSpace = LuaDLL.luaL_checkboolean(L, 2);
			particleEmitter.set_useWorldSpace(useWorldSpace);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useWorldSpace on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rndRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool rndRotation = LuaDLL.luaL_checkboolean(L, 2);
			particleEmitter.set_rndRotation(rndRotation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_angularVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float angularVelocity = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_angularVelocity(angularVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index angularVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rndAngularVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			float rndAngularVelocity = (float)LuaDLL.luaL_checknumber(L, 2);
			particleEmitter.set_rndAngularVelocity(rndAngularVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rndAngularVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_particles(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			Particle[] particles = ToLua.CheckObjectArray<Particle>(L, 2);
			particleEmitter.set_particles(particles);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index particles on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enabled(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			ParticleEmitter particleEmitter = (ParticleEmitter)obj;
			bool enabled = LuaDLL.luaL_checkboolean(L, 2);
			particleEmitter.set_enabled(enabled);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index enabled on a nil value");
		}
		return result;
	}
}
