using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_RigidbodyWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Rigidbody), typeof(Component), null);
		L.RegFunction("SetDensity", new LuaCSFunction(UnityEngine_RigidbodyWrap.SetDensity));
		L.RegFunction("AddForce", new LuaCSFunction(UnityEngine_RigidbodyWrap.AddForce));
		L.RegFunction("AddRelativeForce", new LuaCSFunction(UnityEngine_RigidbodyWrap.AddRelativeForce));
		L.RegFunction("AddTorque", new LuaCSFunction(UnityEngine_RigidbodyWrap.AddTorque));
		L.RegFunction("AddRelativeTorque", new LuaCSFunction(UnityEngine_RigidbodyWrap.AddRelativeTorque));
		L.RegFunction("AddForceAtPosition", new LuaCSFunction(UnityEngine_RigidbodyWrap.AddForceAtPosition));
		L.RegFunction("AddExplosionForce", new LuaCSFunction(UnityEngine_RigidbodyWrap.AddExplosionForce));
		L.RegFunction("ClosestPointOnBounds", new LuaCSFunction(UnityEngine_RigidbodyWrap.ClosestPointOnBounds));
		L.RegFunction("GetRelativePointVelocity", new LuaCSFunction(UnityEngine_RigidbodyWrap.GetRelativePointVelocity));
		L.RegFunction("GetPointVelocity", new LuaCSFunction(UnityEngine_RigidbodyWrap.GetPointVelocity));
		L.RegFunction("MovePosition", new LuaCSFunction(UnityEngine_RigidbodyWrap.MovePosition));
		L.RegFunction("MoveRotation", new LuaCSFunction(UnityEngine_RigidbodyWrap.MoveRotation));
		L.RegFunction("Sleep", new LuaCSFunction(UnityEngine_RigidbodyWrap.Sleep));
		L.RegFunction("IsSleeping", new LuaCSFunction(UnityEngine_RigidbodyWrap.IsSleeping));
		L.RegFunction("WakeUp", new LuaCSFunction(UnityEngine_RigidbodyWrap.WakeUp));
		L.RegFunction("ResetCenterOfMass", new LuaCSFunction(UnityEngine_RigidbodyWrap.ResetCenterOfMass));
		L.RegFunction("ResetInertiaTensor", new LuaCSFunction(UnityEngine_RigidbodyWrap.ResetInertiaTensor));
		L.RegFunction("SweepTest", new LuaCSFunction(UnityEngine_RigidbodyWrap.SweepTest));
		L.RegFunction("SweepTestAll", new LuaCSFunction(UnityEngine_RigidbodyWrap.SweepTestAll));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_RigidbodyWrap._CreateUnityEngine_Rigidbody));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_RigidbodyWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_RigidbodyWrap.Lua_ToString));
		L.RegVar("velocity", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_velocity), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_velocity));
		L.RegVar("angularVelocity", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_angularVelocity), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_angularVelocity));
		L.RegVar("drag", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_drag), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_drag));
		L.RegVar("angularDrag", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_angularDrag), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_angularDrag));
		L.RegVar("mass", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_mass), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_mass));
		L.RegVar("useGravity", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_useGravity), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_useGravity));
		L.RegVar("maxDepenetrationVelocity", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_maxDepenetrationVelocity), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_maxDepenetrationVelocity));
		L.RegVar("isKinematic", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_isKinematic), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_isKinematic));
		L.RegVar("freezeRotation", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_freezeRotation), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_freezeRotation));
		L.RegVar("constraints", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_constraints), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_constraints));
		L.RegVar("collisionDetectionMode", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_collisionDetectionMode), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_collisionDetectionMode));
		L.RegVar("centerOfMass", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_centerOfMass), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_centerOfMass));
		L.RegVar("worldCenterOfMass", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_worldCenterOfMass), null);
		L.RegVar("inertiaTensorRotation", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_inertiaTensorRotation), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_inertiaTensorRotation));
		L.RegVar("inertiaTensor", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_inertiaTensor), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_inertiaTensor));
		L.RegVar("detectCollisions", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_detectCollisions), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_detectCollisions));
		L.RegVar("useConeFriction", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_useConeFriction), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_useConeFriction));
		L.RegVar("position", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_position), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_position));
		L.RegVar("rotation", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_rotation), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_rotation));
		L.RegVar("interpolation", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_interpolation), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_interpolation));
		L.RegVar("solverIterationCount", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_solverIterationCount), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_solverIterationCount));
		L.RegVar("sleepThreshold", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_sleepThreshold), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_sleepThreshold));
		L.RegVar("maxAngularVelocity", new LuaCSFunction(UnityEngine_RigidbodyWrap.get_maxAngularVelocity), new LuaCSFunction(UnityEngine_RigidbodyWrap.set_maxAngularVelocity));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Rigidbody(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				Rigidbody obj = new Rigidbody();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Rigidbody.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetDensity(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			float density = (float)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.SetDensity(density);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddForce(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				rigidbody.AddForce(vector);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(ForceMode)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				ForceMode forceMode = (int)ToLua.ToObject(L, 3);
				rigidbody2.AddForce(vector2, forceMode);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float)))
			{
				Rigidbody rigidbody3 = (Rigidbody)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				rigidbody3.AddForce(num2, num3, num4);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float), typeof(ForceMode)))
			{
				Rigidbody rigidbody4 = (Rigidbody)ToLua.ToObject(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				float num7 = (float)LuaDLL.lua_tonumber(L, 4);
				ForceMode forceMode2 = (int)ToLua.ToObject(L, 5);
				rigidbody4.AddForce(num5, num6, num7, forceMode2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.AddForce");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddRelativeForce(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				rigidbody.AddRelativeForce(vector);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(ForceMode)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				ForceMode forceMode = (int)ToLua.ToObject(L, 3);
				rigidbody2.AddRelativeForce(vector2, forceMode);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float)))
			{
				Rigidbody rigidbody3 = (Rigidbody)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				rigidbody3.AddRelativeForce(num2, num3, num4);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float), typeof(ForceMode)))
			{
				Rigidbody rigidbody4 = (Rigidbody)ToLua.ToObject(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				float num7 = (float)LuaDLL.lua_tonumber(L, 4);
				ForceMode forceMode2 = (int)ToLua.ToObject(L, 5);
				rigidbody4.AddRelativeForce(num5, num6, num7, forceMode2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.AddRelativeForce");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddTorque(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				rigidbody.AddTorque(vector);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(ForceMode)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				ForceMode forceMode = (int)ToLua.ToObject(L, 3);
				rigidbody2.AddTorque(vector2, forceMode);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float)))
			{
				Rigidbody rigidbody3 = (Rigidbody)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				rigidbody3.AddTorque(num2, num3, num4);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float), typeof(ForceMode)))
			{
				Rigidbody rigidbody4 = (Rigidbody)ToLua.ToObject(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				float num7 = (float)LuaDLL.lua_tonumber(L, 4);
				ForceMode forceMode2 = (int)ToLua.ToObject(L, 5);
				rigidbody4.AddTorque(num5, num6, num7, forceMode2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.AddTorque");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddRelativeTorque(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				rigidbody.AddRelativeTorque(vector);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(ForceMode)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				ForceMode forceMode = (int)ToLua.ToObject(L, 3);
				rigidbody2.AddRelativeTorque(vector2, forceMode);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float)))
			{
				Rigidbody rigidbody3 = (Rigidbody)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				rigidbody3.AddRelativeTorque(num2, num3, num4);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(float), typeof(float), typeof(ForceMode)))
			{
				Rigidbody rigidbody4 = (Rigidbody)ToLua.ToObject(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				float num7 = (float)LuaDLL.lua_tonumber(L, 4);
				ForceMode forceMode2 = (int)ToLua.ToObject(L, 5);
				rigidbody4.AddRelativeTorque(num5, num6, num7, forceMode2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.AddRelativeTorque");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddForceAtPosition(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(Vector3)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				Vector3 vector2 = ToLua.ToVector3(L, 3);
				rigidbody.AddForceAtPosition(vector, vector2);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(Vector3), typeof(ForceMode)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector3 = ToLua.ToVector3(L, 2);
				Vector3 vector4 = ToLua.ToVector3(L, 3);
				ForceMode forceMode = (int)ToLua.ToObject(L, 4);
				rigidbody2.AddForceAtPosition(vector3, vector4, forceMode);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.AddForceAtPosition");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddExplosionForce(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(Vector3), typeof(float)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector = ToLua.ToVector3(L, 3);
				float num3 = (float)LuaDLL.lua_tonumber(L, 4);
				rigidbody.AddExplosionForce(num2, vector, num3);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(Vector3), typeof(float), typeof(float)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				float num4 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector2 = ToLua.ToVector3(L, 3);
				float num5 = (float)LuaDLL.lua_tonumber(L, 4);
				float num6 = (float)LuaDLL.lua_tonumber(L, 5);
				rigidbody2.AddExplosionForce(num4, vector2, num5, num6);
				result = 0;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(float), typeof(Vector3), typeof(float), typeof(float), typeof(ForceMode)))
			{
				Rigidbody rigidbody3 = (Rigidbody)ToLua.ToObject(L, 1);
				float num7 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector3 = ToLua.ToVector3(L, 3);
				float num8 = (float)LuaDLL.lua_tonumber(L, 4);
				float num9 = (float)LuaDLL.lua_tonumber(L, 5);
				ForceMode forceMode = (int)ToLua.ToObject(L, 6);
				rigidbody3.AddExplosionForce(num7, vector3, num8, num9, forceMode);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.AddExplosionForce");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClosestPointOnBounds(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 v = rigidbody.ClosestPointOnBounds(vector);
			ToLua.Push(L, v);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetRelativePointVelocity(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 relativePointVelocity = rigidbody.GetRelativePointVelocity(vector);
			ToLua.Push(L, relativePointVelocity);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPointVelocity(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 pointVelocity = rigidbody.GetPointVelocity(vector);
			ToLua.Push(L, pointVelocity);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int MovePosition(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			Vector3 vector = ToLua.ToVector3(L, 2);
			rigidbody.MovePosition(vector);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int MoveRotation(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			Quaternion quaternion = ToLua.ToQuaternion(L, 2);
			rigidbody.MoveRotation(quaternion);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Sleep(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			rigidbody.Sleep();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsSleeping(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			bool value = rigidbody.IsSleeping();
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
	private static int WakeUp(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			rigidbody.WakeUp();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetCenterOfMass(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			rigidbody.ResetCenterOfMass();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetInertiaTensor(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Rigidbody rigidbody = (Rigidbody)ToLua.CheckObject(L, 1, typeof(Rigidbody));
			rigidbody.ResetInertiaTensor();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SweepTest(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(LuaOut<RaycastHit>)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				RaycastHit hit;
				bool value = rigidbody.SweepTest(vector, ref hit);
				LuaDLL.lua_pushboolean(L, value);
				ToLua.Push(L, hit);
				result = 2;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 4);
				RaycastHit hit2;
				bool value2 = rigidbody2.SweepTest(vector2, ref hit2, num2);
				LuaDLL.lua_pushboolean(L, value2);
				ToLua.Push(L, hit2);
				result = 2;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float), typeof(QueryTriggerInteraction)))
			{
				Rigidbody rigidbody3 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector3 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				RaycastHit hit3;
				bool value3 = rigidbody3.SweepTest(vector3, ref hit3, num3, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value3);
				ToLua.Push(L, hit3);
				result = 2;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.SweepTest");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SweepTestAll(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3)))
			{
				Rigidbody rigidbody = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector = ToLua.ToVector3(L, 2);
				RaycastHit[] array = rigidbody.SweepTestAll(vector);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(float)))
			{
				Rigidbody rigidbody2 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				RaycastHit[] array2 = rigidbody2.SweepTestAll(vector2, num2);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Rigidbody), typeof(Vector3), typeof(float), typeof(QueryTriggerInteraction)))
			{
				Rigidbody rigidbody3 = (Rigidbody)ToLua.ToObject(L, 1);
				Vector3 vector3 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 4);
				RaycastHit[] array3 = rigidbody3.SweepTestAll(vector3, num3, queryTriggerInteraction);
				ToLua.Push(L, array3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Rigidbody.SweepTestAll");
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
	private static int get_velocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 velocity = rigidbody.get_velocity();
			ToLua.Push(L, velocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index velocity on a nil value");
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
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 angularVelocity = rigidbody.get_angularVelocity();
			ToLua.Push(L, angularVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index angularVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_drag(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float drag = rigidbody.get_drag();
			LuaDLL.lua_pushnumber(L, (double)drag);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index drag on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_angularDrag(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float angularDrag = rigidbody.get_angularDrag();
			LuaDLL.lua_pushnumber(L, (double)angularDrag);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index angularDrag on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mass(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float mass = rigidbody.get_mass();
			LuaDLL.lua_pushnumber(L, (double)mass);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mass on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useGravity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool useGravity = rigidbody.get_useGravity();
			LuaDLL.lua_pushboolean(L, useGravity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useGravity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxDepenetrationVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float maxDepenetrationVelocity = rigidbody.get_maxDepenetrationVelocity();
			LuaDLL.lua_pushnumber(L, (double)maxDepenetrationVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxDepenetrationVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isKinematic(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool isKinematic = rigidbody.get_isKinematic();
			LuaDLL.lua_pushboolean(L, isKinematic);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isKinematic on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_freezeRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool freezeRotation = rigidbody.get_freezeRotation();
			LuaDLL.lua_pushboolean(L, freezeRotation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index freezeRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_constraints(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			RigidbodyConstraints constraints = rigidbody.get_constraints();
			ToLua.Push(L, constraints);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index constraints on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_collisionDetectionMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			CollisionDetectionMode collisionDetectionMode = rigidbody.get_collisionDetectionMode();
			ToLua.Push(L, collisionDetectionMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index collisionDetectionMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_centerOfMass(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 centerOfMass = rigidbody.get_centerOfMass();
			ToLua.Push(L, centerOfMass);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index centerOfMass on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldCenterOfMass(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 worldCenterOfMass = rigidbody.get_worldCenterOfMass();
			ToLua.Push(L, worldCenterOfMass);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldCenterOfMass on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_inertiaTensorRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Quaternion inertiaTensorRotation = rigidbody.get_inertiaTensorRotation();
			ToLua.Push(L, inertiaTensorRotation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index inertiaTensorRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_inertiaTensor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 inertiaTensor = rigidbody.get_inertiaTensor();
			ToLua.Push(L, inertiaTensor);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index inertiaTensor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_detectCollisions(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool detectCollisions = rigidbody.get_detectCollisions();
			LuaDLL.lua_pushboolean(L, detectCollisions);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index detectCollisions on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useConeFriction(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool useConeFriction = rigidbody.get_useConeFriction();
			LuaDLL.lua_pushboolean(L, useConeFriction);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useConeFriction on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_position(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 position = rigidbody.get_position();
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
	private static int get_rotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Quaternion rotation = rigidbody.get_rotation();
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
	private static int get_interpolation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			RigidbodyInterpolation interpolation = rigidbody.get_interpolation();
			ToLua.Push(L, interpolation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index interpolation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_solverIterationCount(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			int solverIterationCount = rigidbody.get_solverIterationCount();
			LuaDLL.lua_pushinteger(L, solverIterationCount);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index solverIterationCount on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sleepThreshold(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float sleepThreshold = rigidbody.get_sleepThreshold();
			LuaDLL.lua_pushnumber(L, (double)sleepThreshold);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sleepThreshold on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxAngularVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float maxAngularVelocity = rigidbody.get_maxAngularVelocity();
			LuaDLL.lua_pushnumber(L, (double)maxAngularVelocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxAngularVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_velocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 velocity = ToLua.ToVector3(L, 2);
			rigidbody.set_velocity(velocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index velocity on a nil value");
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
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 angularVelocity = ToLua.ToVector3(L, 2);
			rigidbody.set_angularVelocity(angularVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index angularVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_drag(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float drag = (float)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.set_drag(drag);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index drag on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_angularDrag(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float angularDrag = (float)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.set_angularDrag(angularDrag);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index angularDrag on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mass(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float mass = (float)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.set_mass(mass);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mass on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useGravity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool useGravity = LuaDLL.luaL_checkboolean(L, 2);
			rigidbody.set_useGravity(useGravity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useGravity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxDepenetrationVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float maxDepenetrationVelocity = (float)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.set_maxDepenetrationVelocity(maxDepenetrationVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxDepenetrationVelocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isKinematic(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool isKinematic = LuaDLL.luaL_checkboolean(L, 2);
			rigidbody.set_isKinematic(isKinematic);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index isKinematic on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_freezeRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool freezeRotation = LuaDLL.luaL_checkboolean(L, 2);
			rigidbody.set_freezeRotation(freezeRotation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index freezeRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_constraints(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			RigidbodyConstraints constraints = (int)ToLua.CheckObject(L, 2, typeof(RigidbodyConstraints));
			rigidbody.set_constraints(constraints);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index constraints on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_collisionDetectionMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			CollisionDetectionMode collisionDetectionMode = (int)ToLua.CheckObject(L, 2, typeof(CollisionDetectionMode));
			rigidbody.set_collisionDetectionMode(collisionDetectionMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index collisionDetectionMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_centerOfMass(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 centerOfMass = ToLua.ToVector3(L, 2);
			rigidbody.set_centerOfMass(centerOfMass);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index centerOfMass on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_inertiaTensorRotation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Quaternion inertiaTensorRotation = ToLua.ToQuaternion(L, 2);
			rigidbody.set_inertiaTensorRotation(inertiaTensorRotation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index inertiaTensorRotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_inertiaTensor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 inertiaTensor = ToLua.ToVector3(L, 2);
			rigidbody.set_inertiaTensor(inertiaTensor);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index inertiaTensor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_detectCollisions(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool detectCollisions = LuaDLL.luaL_checkboolean(L, 2);
			rigidbody.set_detectCollisions(detectCollisions);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index detectCollisions on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useConeFriction(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			bool useConeFriction = LuaDLL.luaL_checkboolean(L, 2);
			rigidbody.set_useConeFriction(useConeFriction);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useConeFriction on a nil value");
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
			Rigidbody rigidbody = (Rigidbody)obj;
			Vector3 position = ToLua.ToVector3(L, 2);
			rigidbody.set_position(position);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index position on a nil value");
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
			Rigidbody rigidbody = (Rigidbody)obj;
			Quaternion rotation = ToLua.ToQuaternion(L, 2);
			rigidbody.set_rotation(rotation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rotation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_interpolation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			RigidbodyInterpolation interpolation = (int)ToLua.CheckObject(L, 2, typeof(RigidbodyInterpolation));
			rigidbody.set_interpolation(interpolation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index interpolation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_solverIterationCount(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			int solverIterationCount = (int)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.set_solverIterationCount(solverIterationCount);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index solverIterationCount on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sleepThreshold(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float sleepThreshold = (float)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.set_sleepThreshold(sleepThreshold);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index sleepThreshold on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxAngularVelocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Rigidbody rigidbody = (Rigidbody)obj;
			float maxAngularVelocity = (float)LuaDLL.luaL_checknumber(L, 2);
			rigidbody.set_maxAngularVelocity(maxAngularVelocity);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index maxAngularVelocity on a nil value");
		}
		return result;
	}
}
