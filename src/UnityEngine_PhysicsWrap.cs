using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_PhysicsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Physics");
		L.RegFunction("Raycast", new LuaCSFunction(UnityEngine_PhysicsWrap.Raycast));
		L.RegFunction("RaycastAll", new LuaCSFunction(UnityEngine_PhysicsWrap.RaycastAll));
		L.RegFunction("RaycastNonAlloc", new LuaCSFunction(UnityEngine_PhysicsWrap.RaycastNonAlloc));
		L.RegFunction("Linecast", new LuaCSFunction(UnityEngine_PhysicsWrap.Linecast));
		L.RegFunction("OverlapSphere", new LuaCSFunction(UnityEngine_PhysicsWrap.OverlapSphere));
		L.RegFunction("OverlapSphereNonAlloc", new LuaCSFunction(UnityEngine_PhysicsWrap.OverlapSphereNonAlloc));
		L.RegFunction("CapsuleCast", new LuaCSFunction(UnityEngine_PhysicsWrap.CapsuleCast));
		L.RegFunction("SphereCast", new LuaCSFunction(UnityEngine_PhysicsWrap.SphereCast));
		L.RegFunction("CapsuleCastAll", new LuaCSFunction(UnityEngine_PhysicsWrap.CapsuleCastAll));
		L.RegFunction("CapsuleCastNonAlloc", new LuaCSFunction(UnityEngine_PhysicsWrap.CapsuleCastNonAlloc));
		L.RegFunction("SphereCastAll", new LuaCSFunction(UnityEngine_PhysicsWrap.SphereCastAll));
		L.RegFunction("SphereCastNonAlloc", new LuaCSFunction(UnityEngine_PhysicsWrap.SphereCastNonAlloc));
		L.RegFunction("CheckSphere", new LuaCSFunction(UnityEngine_PhysicsWrap.CheckSphere));
		L.RegFunction("CheckCapsule", new LuaCSFunction(UnityEngine_PhysicsWrap.CheckCapsule));
		L.RegFunction("CheckBox", new LuaCSFunction(UnityEngine_PhysicsWrap.CheckBox));
		L.RegFunction("OverlapBox", new LuaCSFunction(UnityEngine_PhysicsWrap.OverlapBox));
		L.RegFunction("OverlapBoxNonAlloc", new LuaCSFunction(UnityEngine_PhysicsWrap.OverlapBoxNonAlloc));
		L.RegFunction("BoxCastAll", new LuaCSFunction(UnityEngine_PhysicsWrap.BoxCastAll));
		L.RegFunction("BoxCastNonAlloc", new LuaCSFunction(UnityEngine_PhysicsWrap.BoxCastNonAlloc));
		L.RegFunction("BoxCast", new LuaCSFunction(UnityEngine_PhysicsWrap.BoxCast));
		L.RegFunction("IgnoreCollision", new LuaCSFunction(UnityEngine_PhysicsWrap.IgnoreCollision));
		L.RegFunction("IgnoreLayerCollision", new LuaCSFunction(UnityEngine_PhysicsWrap.IgnoreLayerCollision));
		L.RegFunction("GetIgnoreLayerCollision", new LuaCSFunction(UnityEngine_PhysicsWrap.GetIgnoreLayerCollision));
		L.RegConstant("IgnoreRaycastLayer", 4.0);
		L.RegConstant("DefaultRaycastLayers", -5.0);
		L.RegConstant("AllLayers", -1.0);
		L.RegVar("gravity", new LuaCSFunction(UnityEngine_PhysicsWrap.get_gravity), new LuaCSFunction(UnityEngine_PhysicsWrap.set_gravity));
		L.RegVar("defaultContactOffset", new LuaCSFunction(UnityEngine_PhysicsWrap.get_defaultContactOffset), new LuaCSFunction(UnityEngine_PhysicsWrap.set_defaultContactOffset));
		L.RegVar("bounceThreshold", new LuaCSFunction(UnityEngine_PhysicsWrap.get_bounceThreshold), new LuaCSFunction(UnityEngine_PhysicsWrap.set_bounceThreshold));
		L.RegVar("solverIterationCount", new LuaCSFunction(UnityEngine_PhysicsWrap.get_solverIterationCount), new LuaCSFunction(UnityEngine_PhysicsWrap.set_solverIterationCount));
		L.RegVar("sleepThreshold", new LuaCSFunction(UnityEngine_PhysicsWrap.get_sleepThreshold), new LuaCSFunction(UnityEngine_PhysicsWrap.set_sleepThreshold));
		L.RegVar("queriesHitTriggers", new LuaCSFunction(UnityEngine_PhysicsWrap.get_queriesHitTriggers), new LuaCSFunction(UnityEngine_PhysicsWrap.set_queriesHitTriggers));
		L.EndStaticLibs();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Raycast(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Ray)))
			{
				Ray ray = ToLua.ToRay(L, 1);
				bool value = Physics.Raycast(ray);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(LuaOut<RaycastHit>)))
			{
				Ray ray2 = ToLua.ToRay(L, 1);
				RaycastHit hit;
				bool value2 = Physics.Raycast(ray2, ref hit);
				LuaDLL.lua_pushboolean(L, value2);
				ToLua.Push(L, hit);
				result = 2;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				bool value3 = Physics.Raycast(vector, vector2);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float)))
			{
				Ray ray3 = ToLua.ToRay(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				bool value4 = Physics.Raycast(ray3, num2);
				LuaDLL.lua_pushboolean(L, value4);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(int)))
			{
				Ray ray4 = ToLua.ToRay(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				bool value5 = Physics.Raycast(ray4, num3, num4);
				LuaDLL.lua_pushboolean(L, value5);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				RaycastHit hit2;
				bool value6 = Physics.Raycast(vector3, vector4, ref hit2);
				LuaDLL.lua_pushboolean(L, value6);
				ToLua.Push(L, hit2);
				result = 2;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				float num5 = (float)LuaDLL.lua_tonumber(L, 3);
				bool value7 = Physics.Raycast(vector5, vector6, num5);
				LuaDLL.lua_pushboolean(L, value7);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(LuaOut<RaycastHit>), typeof(float)))
			{
				Ray ray5 = ToLua.ToRay(L, 1);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				RaycastHit hit3;
				bool value8 = Physics.Raycast(ray5, ref hit3, num6);
				LuaDLL.lua_pushboolean(L, value8);
				ToLua.Push(L, hit3);
				result = 2;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray6 = ToLua.ToRay(L, 1);
				float num7 = (float)LuaDLL.lua_tonumber(L, 2);
				int num8 = (int)LuaDLL.lua_tonumber(L, 3);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 4);
				bool value9 = Physics.Raycast(ray6, num7, num8, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value9);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				float num9 = (float)LuaDLL.lua_tonumber(L, 3);
				int num10 = (int)LuaDLL.lua_tonumber(L, 4);
				bool value10 = Physics.Raycast(vector7, vector8, num9, num10);
				LuaDLL.lua_pushboolean(L, value10);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float)))
			{
				Vector3 vector9 = ToLua.ToVector3(L, 1);
				Vector3 vector10 = ToLua.ToVector3(L, 2);
				float num11 = (float)LuaDLL.lua_tonumber(L, 4);
				RaycastHit hit4;
				bool value11 = Physics.Raycast(vector9, vector10, ref hit4, num11);
				LuaDLL.lua_pushboolean(L, value11);
				ToLua.Push(L, hit4);
				result = 2;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int)))
			{
				Ray ray7 = ToLua.ToRay(L, 1);
				float num12 = (float)LuaDLL.lua_tonumber(L, 3);
				int num13 = (int)LuaDLL.lua_tonumber(L, 4);
				RaycastHit hit5;
				bool value12 = Physics.Raycast(ray7, ref hit5, num12, num13);
				LuaDLL.lua_pushboolean(L, value12);
				ToLua.Push(L, hit5);
				result = 2;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector11 = ToLua.ToVector3(L, 1);
				Vector3 vector12 = ToLua.ToVector3(L, 2);
				float num14 = (float)LuaDLL.lua_tonumber(L, 3);
				int num15 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 5);
				bool value13 = Physics.Raycast(vector11, vector12, num14, num15, queryTriggerInteraction2);
				LuaDLL.lua_pushboolean(L, value13);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int)))
			{
				Vector3 vector13 = ToLua.ToVector3(L, 1);
				Vector3 vector14 = ToLua.ToVector3(L, 2);
				float num16 = (float)LuaDLL.lua_tonumber(L, 4);
				int num17 = (int)LuaDLL.lua_tonumber(L, 5);
				RaycastHit hit6;
				bool value14 = Physics.Raycast(vector13, vector14, ref hit6, num16, num17);
				LuaDLL.lua_pushboolean(L, value14);
				ToLua.Push(L, hit6);
				result = 2;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray8 = ToLua.ToRay(L, 1);
				float num18 = (float)LuaDLL.lua_tonumber(L, 3);
				int num19 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction3 = (int)ToLua.ToObject(L, 5);
				RaycastHit hit7;
				bool value15 = Physics.Raycast(ray8, ref hit7, num18, num19, queryTriggerInteraction3);
				LuaDLL.lua_pushboolean(L, value15);
				ToLua.Push(L, hit7);
				result = 2;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector15 = ToLua.ToVector3(L, 1);
				Vector3 vector16 = ToLua.ToVector3(L, 2);
				float num20 = (float)LuaDLL.lua_tonumber(L, 4);
				int num21 = (int)LuaDLL.lua_tonumber(L, 5);
				QueryTriggerInteraction queryTriggerInteraction4 = (int)ToLua.ToObject(L, 6);
				RaycastHit hit8;
				bool value16 = Physics.Raycast(vector15, vector16, ref hit8, num20, num21, queryTriggerInteraction4);
				LuaDLL.lua_pushboolean(L, value16);
				ToLua.Push(L, hit8);
				result = 2;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.Raycast");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RaycastAll(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Ray)))
			{
				Ray ray = ToLua.ToRay(L, 1);
				RaycastHit[] array = Physics.RaycastAll(ray);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				RaycastHit[] array2 = Physics.RaycastAll(vector, vector2);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float)))
			{
				Ray ray2 = ToLua.ToRay(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				RaycastHit[] array3 = Physics.RaycastAll(ray2, num2);
				ToLua.Push(L, array3);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				RaycastHit[] array4 = Physics.RaycastAll(vector3, vector4, num3);
				ToLua.Push(L, array4);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(int)))
			{
				Ray ray3 = ToLua.ToRay(L, 1);
				float num4 = (float)LuaDLL.lua_tonumber(L, 2);
				int num5 = (int)LuaDLL.lua_tonumber(L, 3);
				RaycastHit[] array5 = Physics.RaycastAll(ray3, num4, num5);
				ToLua.Push(L, array5);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				int num7 = (int)LuaDLL.lua_tonumber(L, 4);
				RaycastHit[] array6 = Physics.RaycastAll(vector5, vector6, num6, num7);
				ToLua.Push(L, array6);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray4 = ToLua.ToRay(L, 1);
				float num8 = (float)LuaDLL.lua_tonumber(L, 2);
				int num9 = (int)LuaDLL.lua_tonumber(L, 3);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 4);
				RaycastHit[] array7 = Physics.RaycastAll(ray4, num8, num9, queryTriggerInteraction);
				ToLua.Push(L, array7);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				float num10 = (float)LuaDLL.lua_tonumber(L, 3);
				int num11 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 5);
				RaycastHit[] array8 = Physics.RaycastAll(vector7, vector8, num10, num11, queryTriggerInteraction2);
				ToLua.Push(L, array8);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.RaycastAll");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RaycastNonAlloc(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(RaycastHit[])))
			{
				Ray ray = ToLua.ToRay(L, 1);
				RaycastHit[] array = ToLua.CheckObjectArray<RaycastHit>(L, 2);
				int n = Physics.RaycastNonAlloc(ray, array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(RaycastHit[])))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				RaycastHit[] array2 = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				int n2 = Physics.RaycastNonAlloc(vector, vector2, array2);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(RaycastHit[]), typeof(float)))
			{
				Ray ray2 = ToLua.ToRay(L, 1);
				RaycastHit[] array3 = ToLua.CheckObjectArray<RaycastHit>(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				int n3 = Physics.RaycastNonAlloc(ray2, array3, num2);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(float)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				RaycastHit[] array4 = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				float num3 = (float)LuaDLL.lua_tonumber(L, 4);
				int n4 = Physics.RaycastNonAlloc(vector3, vector4, array4, num3);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(RaycastHit[]), typeof(float), typeof(int)))
			{
				Ray ray3 = ToLua.ToRay(L, 1);
				RaycastHit[] array5 = ToLua.CheckObjectArray<RaycastHit>(L, 2);
				float num4 = (float)LuaDLL.lua_tonumber(L, 3);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				int n5 = Physics.RaycastNonAlloc(ray3, array5, num4, num5);
				LuaDLL.lua_pushinteger(L, n5);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(float), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				RaycastHit[] array6 = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				float num6 = (float)LuaDLL.lua_tonumber(L, 4);
				int num7 = (int)LuaDLL.lua_tonumber(L, 5);
				int n6 = Physics.RaycastNonAlloc(vector5, vector6, array6, num6, num7);
				LuaDLL.lua_pushinteger(L, n6);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray4 = ToLua.ToRay(L, 1);
				RaycastHit[] array7 = ToLua.CheckObjectArray<RaycastHit>(L, 2);
				float num8 = (float)LuaDLL.lua_tonumber(L, 3);
				int num9 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				int n7 = Physics.RaycastNonAlloc(ray4, array7, num8, num9, queryTriggerInteraction);
				LuaDLL.lua_pushinteger(L, n7);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				RaycastHit[] array8 = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				float num10 = (float)LuaDLL.lua_tonumber(L, 4);
				int num11 = (int)LuaDLL.lua_tonumber(L, 5);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 6);
				int n8 = Physics.RaycastNonAlloc(vector7, vector8, array8, num10, num11, queryTriggerInteraction2);
				LuaDLL.lua_pushinteger(L, n8);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.RaycastNonAlloc");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Linecast(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				bool value = Physics.Linecast(vector, vector2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				RaycastHit hit;
				bool value2 = Physics.Linecast(vector3, vector4, ref hit);
				LuaDLL.lua_pushboolean(L, value2);
				ToLua.Push(L, hit);
				result = 2;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				bool value3 = Physics.Linecast(vector5, vector6, num2);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(int)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 4);
				RaycastHit hit2;
				bool value4 = Physics.Linecast(vector7, vector8, ref hit2, num3);
				LuaDLL.lua_pushboolean(L, value4);
				ToLua.Push(L, hit2);
				result = 2;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector9 = ToLua.ToVector3(L, 1);
				Vector3 vector10 = ToLua.ToVector3(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 4);
				bool value5 = Physics.Linecast(vector9, vector10, num4, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value5);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector11 = ToLua.ToVector3(L, 1);
				Vector3 vector12 = ToLua.ToVector3(L, 2);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 5);
				RaycastHit hit3;
				bool value6 = Physics.Linecast(vector11, vector12, ref hit3, num5, queryTriggerInteraction2);
				LuaDLL.lua_pushboolean(L, value6);
				ToLua.Push(L, hit3);
				result = 2;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.Linecast");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapSphere(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				Collider[] array = Physics.OverlapSphere(vector, num2);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector2 = ToLua.ToVector3(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				Collider[] array2 = Physics.OverlapSphere(vector2, num3, num4);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				int num6 = (int)LuaDLL.lua_tonumber(L, 3);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 4);
				Collider[] array3 = Physics.OverlapSphere(vector3, num5, num6, queryTriggerInteraction);
				ToLua.Push(L, array3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.OverlapSphere");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapSphereNonAlloc(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Collider[])))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				Collider[] array = ToLua.CheckObjectArray<Collider>(L, 3);
				int n = Physics.OverlapSphereNonAlloc(vector, num2, array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Collider[]), typeof(int)))
			{
				Vector3 vector2 = ToLua.ToVector3(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				Collider[] array2 = ToLua.CheckObjectArray<Collider>(L, 3);
				int num4 = (int)LuaDLL.lua_tonumber(L, 4);
				int n2 = Physics.OverlapSphereNonAlloc(vector2, num3, array2, num4);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Collider[]), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				Collider[] array3 = ToLua.CheckObjectArray<Collider>(L, 3);
				int num6 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				int n3 = Physics.OverlapSphereNonAlloc(vector3, num5, array3, num6, queryTriggerInteraction);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.OverlapSphereNonAlloc");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCast(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector3 = ToLua.ToVector3(L, 4);
				bool value = Physics.CapsuleCast(vector, vector2, num2, vector3);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>)))
			{
				Vector3 vector4 = ToLua.ToVector3(L, 1);
				Vector3 vector5 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector6 = ToLua.ToVector3(L, 4);
				RaycastHit hit;
				bool value2 = Physics.CapsuleCast(vector4, vector5, num3, vector6, ref hit);
				LuaDLL.lua_pushboolean(L, value2);
				ToLua.Push(L, hit);
				result = 2;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(float)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				float num4 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector9 = ToLua.ToVector3(L, 4);
				float num5 = (float)LuaDLL.lua_tonumber(L, 5);
				bool value3 = Physics.CapsuleCast(vector7, vector8, num4, vector9, num5);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float)))
			{
				Vector3 vector10 = ToLua.ToVector3(L, 1);
				Vector3 vector11 = ToLua.ToVector3(L, 2);
				float num6 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector12 = ToLua.ToVector3(L, 4);
				float num7 = (float)LuaDLL.lua_tonumber(L, 6);
				RaycastHit hit2;
				bool value4 = Physics.CapsuleCast(vector10, vector11, num6, vector12, ref hit2, num7);
				LuaDLL.lua_pushboolean(L, value4);
				ToLua.Push(L, hit2);
				result = 2;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector13 = ToLua.ToVector3(L, 1);
				Vector3 vector14 = ToLua.ToVector3(L, 2);
				float num8 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector15 = ToLua.ToVector3(L, 4);
				float num9 = (float)LuaDLL.lua_tonumber(L, 5);
				int num10 = (int)LuaDLL.lua_tonumber(L, 6);
				bool value5 = Physics.CapsuleCast(vector13, vector14, num8, vector15, num9, num10);
				LuaDLL.lua_pushboolean(L, value5);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector16 = ToLua.ToVector3(L, 1);
				Vector3 vector17 = ToLua.ToVector3(L, 2);
				float num11 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector18 = ToLua.ToVector3(L, 4);
				float num12 = (float)LuaDLL.lua_tonumber(L, 5);
				int num13 = (int)LuaDLL.lua_tonumber(L, 6);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 7);
				bool value6 = Physics.CapsuleCast(vector16, vector17, num11, vector18, num12, num13, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value6);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int)))
			{
				Vector3 vector19 = ToLua.ToVector3(L, 1);
				Vector3 vector20 = ToLua.ToVector3(L, 2);
				float num14 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector21 = ToLua.ToVector3(L, 4);
				float num15 = (float)LuaDLL.lua_tonumber(L, 6);
				int num16 = (int)LuaDLL.lua_tonumber(L, 7);
				RaycastHit hit3;
				bool value7 = Physics.CapsuleCast(vector19, vector20, num14, vector21, ref hit3, num15, num16);
				LuaDLL.lua_pushboolean(L, value7);
				ToLua.Push(L, hit3);
				result = 2;
			}
			else if (num == 8 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector22 = ToLua.ToVector3(L, 1);
				Vector3 vector23 = ToLua.ToVector3(L, 2);
				float num17 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector24 = ToLua.ToVector3(L, 4);
				float num18 = (float)LuaDLL.lua_tonumber(L, 6);
				int num19 = (int)LuaDLL.lua_tonumber(L, 7);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 8);
				RaycastHit hit4;
				bool value8 = Physics.CapsuleCast(vector22, vector23, num17, vector24, ref hit4, num18, num19, queryTriggerInteraction2);
				LuaDLL.lua_pushboolean(L, value8);
				ToLua.Push(L, hit4);
				result = 2;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.CapsuleCast");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCast(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float)))
			{
				Ray ray = ToLua.ToRay(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				bool value = Physics.SphereCast(ray, num2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(float)))
			{
				Ray ray2 = ToLua.ToRay(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				float num4 = (float)LuaDLL.lua_tonumber(L, 3);
				bool value2 = Physics.SphereCast(ray2, num3, num4);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(LuaOut<RaycastHit>)))
			{
				Ray ray3 = ToLua.ToRay(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				RaycastHit hit;
				bool value3 = Physics.SphereCast(ray3, num5, ref hit);
				LuaDLL.lua_pushboolean(L, value3);
				ToLua.Push(L, hit);
				result = 2;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				float num6 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector2 = ToLua.ToVector3(L, 3);
				RaycastHit hit2;
				bool value4 = Physics.SphereCast(vector, num6, vector2, ref hit2);
				LuaDLL.lua_pushboolean(L, value4);
				ToLua.Push(L, hit2);
				result = 2;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(float), typeof(int)))
			{
				Ray ray4 = ToLua.ToRay(L, 1);
				float num7 = (float)LuaDLL.lua_tonumber(L, 2);
				float num8 = (float)LuaDLL.lua_tonumber(L, 3);
				int num9 = (int)LuaDLL.lua_tonumber(L, 4);
				bool value5 = Physics.SphereCast(ray4, num7, num8, num9);
				LuaDLL.lua_pushboolean(L, value5);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(LuaOut<RaycastHit>), typeof(float)))
			{
				Ray ray5 = ToLua.ToRay(L, 1);
				float num10 = (float)LuaDLL.lua_tonumber(L, 2);
				float num11 = (float)LuaDLL.lua_tonumber(L, 4);
				RaycastHit hit3;
				bool value6 = Physics.SphereCast(ray5, num10, ref hit3, num11);
				LuaDLL.lua_pushboolean(L, value6);
				ToLua.Push(L, hit3);
				result = 2;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int)))
			{
				Ray ray6 = ToLua.ToRay(L, 1);
				float num12 = (float)LuaDLL.lua_tonumber(L, 2);
				float num13 = (float)LuaDLL.lua_tonumber(L, 4);
				int num14 = (int)LuaDLL.lua_tonumber(L, 5);
				RaycastHit hit4;
				bool value7 = Physics.SphereCast(ray6, num12, ref hit4, num13, num14);
				LuaDLL.lua_pushboolean(L, value7);
				ToLua.Push(L, hit4);
				result = 2;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray7 = ToLua.ToRay(L, 1);
				float num15 = (float)LuaDLL.lua_tonumber(L, 2);
				float num16 = (float)LuaDLL.lua_tonumber(L, 3);
				int num17 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				bool value8 = Physics.SphereCast(ray7, num15, num16, num17, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value8);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				float num18 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector4 = ToLua.ToVector3(L, 3);
				float num19 = (float)LuaDLL.lua_tonumber(L, 5);
				RaycastHit hit5;
				bool value9 = Physics.SphereCast(vector3, num18, vector4, ref hit5, num19);
				LuaDLL.lua_pushboolean(L, value9);
				ToLua.Push(L, hit5);
				result = 2;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				float num20 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector6 = ToLua.ToVector3(L, 3);
				float num21 = (float)LuaDLL.lua_tonumber(L, 5);
				int num22 = (int)LuaDLL.lua_tonumber(L, 6);
				RaycastHit hit6;
				bool value10 = Physics.SphereCast(vector5, num20, vector6, ref hit6, num21, num22);
				LuaDLL.lua_pushboolean(L, value10);
				ToLua.Push(L, hit6);
				result = 2;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray8 = ToLua.ToRay(L, 1);
				float num23 = (float)LuaDLL.lua_tonumber(L, 2);
				float num24 = (float)LuaDLL.lua_tonumber(L, 4);
				int num25 = (int)LuaDLL.lua_tonumber(L, 5);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 6);
				RaycastHit hit7;
				bool value11 = Physics.SphereCast(ray8, num23, ref hit7, num24, num25, queryTriggerInteraction2);
				LuaDLL.lua_pushboolean(L, value11);
				ToLua.Push(L, hit7);
				result = 2;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				float num26 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector8 = ToLua.ToVector3(L, 3);
				float num27 = (float)LuaDLL.lua_tonumber(L, 5);
				int num28 = (int)LuaDLL.lua_tonumber(L, 6);
				QueryTriggerInteraction queryTriggerInteraction3 = (int)ToLua.ToObject(L, 7);
				RaycastHit hit8;
				bool value12 = Physics.SphereCast(vector7, num26, vector8, ref hit8, num27, num28, queryTriggerInteraction3);
				LuaDLL.lua_pushboolean(L, value12);
				ToLua.Push(L, hit8);
				result = 2;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.SphereCast");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCastAll(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector3 = ToLua.ToVector3(L, 4);
				RaycastHit[] array = Physics.CapsuleCastAll(vector, vector2, num2, vector3);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(float)))
			{
				Vector3 vector4 = ToLua.ToVector3(L, 1);
				Vector3 vector5 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector6 = ToLua.ToVector3(L, 4);
				float num4 = (float)LuaDLL.lua_tonumber(L, 5);
				RaycastHit[] array2 = Physics.CapsuleCastAll(vector4, vector5, num3, vector6, num4);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				float num5 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector9 = ToLua.ToVector3(L, 4);
				float num6 = (float)LuaDLL.lua_tonumber(L, 5);
				int num7 = (int)LuaDLL.lua_tonumber(L, 6);
				RaycastHit[] array3 = Physics.CapsuleCastAll(vector7, vector8, num5, vector9, num6, num7);
				ToLua.Push(L, array3);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector10 = ToLua.ToVector3(L, 1);
				Vector3 vector11 = ToLua.ToVector3(L, 2);
				float num8 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector12 = ToLua.ToVector3(L, 4);
				float num9 = (float)LuaDLL.lua_tonumber(L, 5);
				int num10 = (int)LuaDLL.lua_tonumber(L, 6);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 7);
				RaycastHit[] array4 = Physics.CapsuleCastAll(vector10, vector11, num8, vector12, num9, num10, queryTriggerInteraction);
				ToLua.Push(L, array4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.CapsuleCastAll");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCastNonAlloc(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[])))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector3 = ToLua.ToVector3(L, 4);
				RaycastHit[] array = ToLua.CheckObjectArray<RaycastHit>(L, 5);
				int n = Physics.CapsuleCastNonAlloc(vector, vector2, num2, vector3, array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[]), typeof(float)))
			{
				Vector3 vector4 = ToLua.ToVector3(L, 1);
				Vector3 vector5 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector6 = ToLua.ToVector3(L, 4);
				RaycastHit[] array2 = ToLua.CheckObjectArray<RaycastHit>(L, 5);
				float num4 = (float)LuaDLL.lua_tonumber(L, 6);
				int n2 = Physics.CapsuleCastNonAlloc(vector4, vector5, num3, vector6, array2, num4);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[]), typeof(float), typeof(int)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				float num5 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector9 = ToLua.ToVector3(L, 4);
				RaycastHit[] array3 = ToLua.CheckObjectArray<RaycastHit>(L, 5);
				float num6 = (float)LuaDLL.lua_tonumber(L, 6);
				int num7 = (int)LuaDLL.lua_tonumber(L, 7);
				int n3 = Physics.CapsuleCastNonAlloc(vector7, vector8, num5, vector9, array3, num6, num7);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 8 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector10 = ToLua.ToVector3(L, 1);
				Vector3 vector11 = ToLua.ToVector3(L, 2);
				float num8 = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector12 = ToLua.ToVector3(L, 4);
				RaycastHit[] array4 = ToLua.CheckObjectArray<RaycastHit>(L, 5);
				float num9 = (float)LuaDLL.lua_tonumber(L, 6);
				int num10 = (int)LuaDLL.lua_tonumber(L, 7);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 8);
				int n4 = Physics.CapsuleCastNonAlloc(vector10, vector11, num8, vector12, array4, num9, num10, queryTriggerInteraction);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.CapsuleCastNonAlloc");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCastAll(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float)))
			{
				Ray ray = ToLua.ToRay(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				RaycastHit[] array = Physics.SphereCastAll(ray, num2);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(float)))
			{
				Ray ray2 = ToLua.ToRay(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				float num4 = (float)LuaDLL.lua_tonumber(L, 3);
				RaycastHit[] array2 = Physics.SphereCastAll(ray2, num3, num4);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector2 = ToLua.ToVector3(L, 3);
				RaycastHit[] array3 = Physics.SphereCastAll(vector, num5, vector2);
				ToLua.Push(L, array3);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(float), typeof(int)))
			{
				Ray ray3 = ToLua.ToRay(L, 1);
				float num6 = (float)LuaDLL.lua_tonumber(L, 2);
				float num7 = (float)LuaDLL.lua_tonumber(L, 3);
				int num8 = (int)LuaDLL.lua_tonumber(L, 4);
				RaycastHit[] array4 = Physics.SphereCastAll(ray3, num6, num7, num8);
				ToLua.Push(L, array4);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(float)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				float num9 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector4 = ToLua.ToVector3(L, 3);
				float num10 = (float)LuaDLL.lua_tonumber(L, 4);
				RaycastHit[] array5 = Physics.SphereCastAll(vector3, num9, vector4, num10);
				ToLua.Push(L, array5);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				float num11 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector6 = ToLua.ToVector3(L, 3);
				float num12 = (float)LuaDLL.lua_tonumber(L, 4);
				int num13 = (int)LuaDLL.lua_tonumber(L, 5);
				RaycastHit[] array6 = Physics.SphereCastAll(vector5, num11, vector6, num12, num13);
				ToLua.Push(L, array6);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray4 = ToLua.ToRay(L, 1);
				float num14 = (float)LuaDLL.lua_tonumber(L, 2);
				float num15 = (float)LuaDLL.lua_tonumber(L, 3);
				int num16 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				RaycastHit[] array7 = Physics.SphereCastAll(ray4, num14, num15, num16, queryTriggerInteraction);
				ToLua.Push(L, array7);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				float num17 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector8 = ToLua.ToVector3(L, 3);
				float num18 = (float)LuaDLL.lua_tonumber(L, 4);
				int num19 = (int)LuaDLL.lua_tonumber(L, 5);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 6);
				RaycastHit[] array8 = Physics.SphereCastAll(vector7, num17, vector8, num18, num19, queryTriggerInteraction2);
				ToLua.Push(L, array8);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.SphereCastAll");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCastNonAlloc(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(RaycastHit[])))
			{
				Ray ray = ToLua.ToRay(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				RaycastHit[] array = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				int n = Physics.SphereCastNonAlloc(ray, num2, array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(RaycastHit[]), typeof(float)))
			{
				Ray ray2 = ToLua.ToRay(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				RaycastHit[] array2 = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				float num4 = (float)LuaDLL.lua_tonumber(L, 4);
				int n2 = Physics.SphereCastNonAlloc(ray2, num3, array2, num4);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[])))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector2 = ToLua.ToVector3(L, 3);
				RaycastHit[] array3 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				int n3 = Physics.SphereCastNonAlloc(vector, num5, vector2, array3);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(RaycastHit[]), typeof(float), typeof(int)))
			{
				Ray ray3 = ToLua.ToRay(L, 1);
				float num6 = (float)LuaDLL.lua_tonumber(L, 2);
				RaycastHit[] array4 = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				float num7 = (float)LuaDLL.lua_tonumber(L, 4);
				int num8 = (int)LuaDLL.lua_tonumber(L, 5);
				int n4 = Physics.SphereCastNonAlloc(ray3, num6, array4, num7, num8);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[]), typeof(float)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				float num9 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector4 = ToLua.ToVector3(L, 3);
				RaycastHit[] array5 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				float num10 = (float)LuaDLL.lua_tonumber(L, 5);
				int n5 = Physics.SphereCastNonAlloc(vector3, num9, vector4, array5, num10);
				LuaDLL.lua_pushinteger(L, n5);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[]), typeof(float), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				float num11 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector6 = ToLua.ToVector3(L, 3);
				RaycastHit[] array6 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				float num12 = (float)LuaDLL.lua_tonumber(L, 5);
				int num13 = (int)LuaDLL.lua_tonumber(L, 6);
				int n6 = Physics.SphereCastNonAlloc(vector5, num11, vector6, array6, num12, num13);
				LuaDLL.lua_pushinteger(L, n6);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Ray), typeof(float), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Ray ray4 = ToLua.ToRay(L, 1);
				float num14 = (float)LuaDLL.lua_tonumber(L, 2);
				RaycastHit[] array7 = ToLua.CheckObjectArray<RaycastHit>(L, 3);
				float num15 = (float)LuaDLL.lua_tonumber(L, 4);
				int num16 = (int)LuaDLL.lua_tonumber(L, 5);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 6);
				int n7 = Physics.SphereCastNonAlloc(ray4, num14, array7, num15, num16, queryTriggerInteraction);
				LuaDLL.lua_pushinteger(L, n7);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(Vector3), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				float num17 = (float)LuaDLL.lua_tonumber(L, 2);
				Vector3 vector8 = ToLua.ToVector3(L, 3);
				RaycastHit[] array8 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				float num18 = (float)LuaDLL.lua_tonumber(L, 5);
				int num19 = (int)LuaDLL.lua_tonumber(L, 6);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 7);
				int n8 = Physics.SphereCastNonAlloc(vector7, num17, vector8, array8, num18, num19, queryTriggerInteraction2);
				LuaDLL.lua_pushinteger(L, n8);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.SphereCastNonAlloc");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckSphere(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				float num2 = (float)LuaDLL.lua_tonumber(L, 2);
				bool value = Physics.CheckSphere(vector, num2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector2 = ToLua.ToVector3(L, 1);
				float num3 = (float)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				bool value2 = Physics.CheckSphere(vector2, num3, num4);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				float num5 = (float)LuaDLL.lua_tonumber(L, 2);
				int num6 = (int)LuaDLL.lua_tonumber(L, 3);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 4);
				bool value3 = Physics.CheckSphere(vector3, num5, num6, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.CheckSphere");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckCapsule(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				float num2 = (float)LuaDLL.lua_tonumber(L, 3);
				bool value = Physics.CheckCapsule(vector, vector2, num2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(int)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				int num4 = (int)LuaDLL.lua_tonumber(L, 4);
				bool value2 = Physics.CheckCapsule(vector3, vector4, num3, num4);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				float num5 = (float)LuaDLL.lua_tonumber(L, 3);
				int num6 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				bool value3 = Physics.CheckCapsule(vector5, vector6, num5, num6, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.CheckCapsule");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckBox(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				bool value = Physics.CheckBox(vector, vector2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Quaternion)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				Quaternion quaternion = ToLua.ToQuaternion(L, 3);
				bool value2 = Physics.CheckBox(vector3, vector4, quaternion);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				Quaternion quaternion2 = ToLua.ToQuaternion(L, 3);
				int num2 = (int)LuaDLL.lua_tonumber(L, 4);
				bool value3 = Physics.CheckBox(vector5, vector6, quaternion2, num2);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				Quaternion quaternion3 = ToLua.ToQuaternion(L, 3);
				int num3 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				bool value4 = Physics.CheckBox(vector7, vector8, quaternion3, num3, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.CheckBox");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapBox(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				Collider[] array = Physics.OverlapBox(vector, vector2);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Quaternion)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				Quaternion quaternion = ToLua.ToQuaternion(L, 3);
				Collider[] array2 = Physics.OverlapBox(vector3, vector4, quaternion);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				Quaternion quaternion2 = ToLua.ToQuaternion(L, 3);
				int num2 = (int)LuaDLL.lua_tonumber(L, 4);
				Collider[] array3 = Physics.OverlapBox(vector5, vector6, quaternion2, num2);
				ToLua.Push(L, array3);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				Quaternion quaternion3 = ToLua.ToQuaternion(L, 3);
				int num3 = (int)LuaDLL.lua_tonumber(L, 4);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 5);
				Collider[] array4 = Physics.OverlapBox(vector7, vector8, quaternion3, num3, queryTriggerInteraction);
				ToLua.Push(L, array4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.OverlapBox");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapBoxNonAlloc(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Collider[])))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				Collider[] array = ToLua.CheckObjectArray<Collider>(L, 3);
				int n = Physics.OverlapBoxNonAlloc(vector, vector2, array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Collider[]), typeof(Quaternion)))
			{
				Vector3 vector3 = ToLua.ToVector3(L, 1);
				Vector3 vector4 = ToLua.ToVector3(L, 2);
				Collider[] array2 = ToLua.CheckObjectArray<Collider>(L, 3);
				Quaternion quaternion = ToLua.ToQuaternion(L, 4);
				int n2 = Physics.OverlapBoxNonAlloc(vector3, vector4, array2, quaternion);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Collider[]), typeof(Quaternion), typeof(int)))
			{
				Vector3 vector5 = ToLua.ToVector3(L, 1);
				Vector3 vector6 = ToLua.ToVector3(L, 2);
				Collider[] array3 = ToLua.CheckObjectArray<Collider>(L, 3);
				Quaternion quaternion2 = ToLua.ToQuaternion(L, 4);
				int num2 = (int)LuaDLL.lua_tonumber(L, 5);
				int n3 = Physics.OverlapBoxNonAlloc(vector5, vector6, array3, quaternion2, num2);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Collider[]), typeof(Quaternion), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				Collider[] array4 = ToLua.CheckObjectArray<Collider>(L, 3);
				Quaternion quaternion3 = ToLua.ToQuaternion(L, 4);
				int num3 = (int)LuaDLL.lua_tonumber(L, 5);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 6);
				int n4 = Physics.OverlapBoxNonAlloc(vector7, vector8, array4, quaternion3, num3, queryTriggerInteraction);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.OverlapBoxNonAlloc");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BoxCastAll(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				Vector3 vector3 = ToLua.ToVector3(L, 3);
				RaycastHit[] array = Physics.BoxCastAll(vector, vector2, vector3);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion)))
			{
				Vector3 vector4 = ToLua.ToVector3(L, 1);
				Vector3 vector5 = ToLua.ToVector3(L, 2);
				Vector3 vector6 = ToLua.ToVector3(L, 3);
				Quaternion quaternion = ToLua.ToQuaternion(L, 4);
				RaycastHit[] array2 = Physics.BoxCastAll(vector4, vector5, vector6, quaternion);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(float)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				Vector3 vector9 = ToLua.ToVector3(L, 3);
				Quaternion quaternion2 = ToLua.ToQuaternion(L, 4);
				float num2 = (float)LuaDLL.lua_tonumber(L, 5);
				RaycastHit[] array3 = Physics.BoxCastAll(vector7, vector8, vector9, quaternion2, num2);
				ToLua.Push(L, array3);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(float), typeof(int)))
			{
				Vector3 vector10 = ToLua.ToVector3(L, 1);
				Vector3 vector11 = ToLua.ToVector3(L, 2);
				Vector3 vector12 = ToLua.ToVector3(L, 3);
				Quaternion quaternion3 = ToLua.ToQuaternion(L, 4);
				float num3 = (float)LuaDLL.lua_tonumber(L, 5);
				int num4 = (int)LuaDLL.lua_tonumber(L, 6);
				RaycastHit[] array4 = Physics.BoxCastAll(vector10, vector11, vector12, quaternion3, num3, num4);
				ToLua.Push(L, array4);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector13 = ToLua.ToVector3(L, 1);
				Vector3 vector14 = ToLua.ToVector3(L, 2);
				Vector3 vector15 = ToLua.ToVector3(L, 3);
				Quaternion quaternion4 = ToLua.ToQuaternion(L, 4);
				float num5 = (float)LuaDLL.lua_tonumber(L, 5);
				int num6 = (int)LuaDLL.lua_tonumber(L, 6);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 7);
				RaycastHit[] array5 = Physics.BoxCastAll(vector13, vector14, vector15, quaternion4, num5, num6, queryTriggerInteraction);
				ToLua.Push(L, array5);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.BoxCastAll");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BoxCastNonAlloc(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(RaycastHit[])))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				Vector3 vector3 = ToLua.ToVector3(L, 3);
				RaycastHit[] array = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				int n = Physics.BoxCastNonAlloc(vector, vector2, vector3, array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(Quaternion)))
			{
				Vector3 vector4 = ToLua.ToVector3(L, 1);
				Vector3 vector5 = ToLua.ToVector3(L, 2);
				Vector3 vector6 = ToLua.ToVector3(L, 3);
				RaycastHit[] array2 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				Quaternion quaternion = ToLua.ToQuaternion(L, 5);
				int n2 = Physics.BoxCastNonAlloc(vector4, vector5, vector6, array2, quaternion);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(Quaternion), typeof(float)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				Vector3 vector9 = ToLua.ToVector3(L, 3);
				RaycastHit[] array3 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				Quaternion quaternion2 = ToLua.ToQuaternion(L, 5);
				float num2 = (float)LuaDLL.lua_tonumber(L, 6);
				int n3 = Physics.BoxCastNonAlloc(vector7, vector8, vector9, array3, quaternion2, num2);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(Quaternion), typeof(float), typeof(int)))
			{
				Vector3 vector10 = ToLua.ToVector3(L, 1);
				Vector3 vector11 = ToLua.ToVector3(L, 2);
				Vector3 vector12 = ToLua.ToVector3(L, 3);
				RaycastHit[] array4 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				Quaternion quaternion3 = ToLua.ToQuaternion(L, 5);
				float num3 = (float)LuaDLL.lua_tonumber(L, 6);
				int num4 = (int)LuaDLL.lua_tonumber(L, 7);
				int n4 = Physics.BoxCastNonAlloc(vector10, vector11, vector12, array4, quaternion3, num3, num4);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else if (num == 8 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(Quaternion), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector13 = ToLua.ToVector3(L, 1);
				Vector3 vector14 = ToLua.ToVector3(L, 2);
				Vector3 vector15 = ToLua.ToVector3(L, 3);
				RaycastHit[] array5 = ToLua.CheckObjectArray<RaycastHit>(L, 4);
				Quaternion quaternion4 = ToLua.ToQuaternion(L, 5);
				float num5 = (float)LuaDLL.lua_tonumber(L, 6);
				int num6 = (int)LuaDLL.lua_tonumber(L, 7);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 8);
				int n5 = Physics.BoxCastNonAlloc(vector13, vector14, vector15, array5, quaternion4, num5, num6, queryTriggerInteraction);
				LuaDLL.lua_pushinteger(L, n5);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.BoxCastNonAlloc");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BoxCast(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3)))
			{
				Vector3 vector = ToLua.ToVector3(L, 1);
				Vector3 vector2 = ToLua.ToVector3(L, 2);
				Vector3 vector3 = ToLua.ToVector3(L, 3);
				bool value = Physics.BoxCast(vector, vector2, vector3);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>)))
			{
				Vector3 vector4 = ToLua.ToVector3(L, 1);
				Vector3 vector5 = ToLua.ToVector3(L, 2);
				Vector3 vector6 = ToLua.ToVector3(L, 3);
				RaycastHit hit;
				bool value2 = Physics.BoxCast(vector4, vector5, vector6, ref hit);
				LuaDLL.lua_pushboolean(L, value2);
				ToLua.Push(L, hit);
				result = 2;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion)))
			{
				Vector3 vector7 = ToLua.ToVector3(L, 1);
				Vector3 vector8 = ToLua.ToVector3(L, 2);
				Vector3 vector9 = ToLua.ToVector3(L, 3);
				Quaternion quaternion = ToLua.ToQuaternion(L, 4);
				bool value3 = Physics.BoxCast(vector7, vector8, vector9, quaternion);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(float)))
			{
				Vector3 vector10 = ToLua.ToVector3(L, 1);
				Vector3 vector11 = ToLua.ToVector3(L, 2);
				Vector3 vector12 = ToLua.ToVector3(L, 3);
				Quaternion quaternion2 = ToLua.ToQuaternion(L, 4);
				float num2 = (float)LuaDLL.lua_tonumber(L, 5);
				bool value4 = Physics.BoxCast(vector10, vector11, vector12, quaternion2, num2);
				LuaDLL.lua_pushboolean(L, value4);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(Quaternion)))
			{
				Vector3 vector13 = ToLua.ToVector3(L, 1);
				Vector3 vector14 = ToLua.ToVector3(L, 2);
				Vector3 vector15 = ToLua.ToVector3(L, 3);
				Quaternion quaternion3 = ToLua.ToQuaternion(L, 5);
				RaycastHit hit2;
				bool value5 = Physics.BoxCast(vector13, vector14, vector15, ref hit2, quaternion3);
				LuaDLL.lua_pushboolean(L, value5);
				ToLua.Push(L, hit2);
				result = 2;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(Quaternion), typeof(float)))
			{
				Vector3 vector16 = ToLua.ToVector3(L, 1);
				Vector3 vector17 = ToLua.ToVector3(L, 2);
				Vector3 vector18 = ToLua.ToVector3(L, 3);
				Quaternion quaternion4 = ToLua.ToQuaternion(L, 5);
				float num3 = (float)LuaDLL.lua_tonumber(L, 6);
				RaycastHit hit3;
				bool value6 = Physics.BoxCast(vector16, vector17, vector18, ref hit3, quaternion4, num3);
				LuaDLL.lua_pushboolean(L, value6);
				ToLua.Push(L, hit3);
				result = 2;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(float), typeof(int)))
			{
				Vector3 vector19 = ToLua.ToVector3(L, 1);
				Vector3 vector20 = ToLua.ToVector3(L, 2);
				Vector3 vector21 = ToLua.ToVector3(L, 3);
				Quaternion quaternion5 = ToLua.ToQuaternion(L, 4);
				float num4 = (float)LuaDLL.lua_tonumber(L, 5);
				int num5 = (int)LuaDLL.lua_tonumber(L, 6);
				bool value7 = Physics.BoxCast(vector19, vector20, vector21, quaternion5, num4, num5);
				LuaDLL.lua_pushboolean(L, value7);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(Quaternion), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector22 = ToLua.ToVector3(L, 1);
				Vector3 vector23 = ToLua.ToVector3(L, 2);
				Vector3 vector24 = ToLua.ToVector3(L, 3);
				Quaternion quaternion6 = ToLua.ToQuaternion(L, 4);
				float num6 = (float)LuaDLL.lua_tonumber(L, 5);
				int num7 = (int)LuaDLL.lua_tonumber(L, 6);
				QueryTriggerInteraction queryTriggerInteraction = (int)ToLua.ToObject(L, 7);
				bool value8 = Physics.BoxCast(vector22, vector23, vector24, quaternion6, num6, num7, queryTriggerInteraction);
				LuaDLL.lua_pushboolean(L, value8);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(Quaternion), typeof(float), typeof(int)))
			{
				Vector3 vector25 = ToLua.ToVector3(L, 1);
				Vector3 vector26 = ToLua.ToVector3(L, 2);
				Vector3 vector27 = ToLua.ToVector3(L, 3);
				Quaternion quaternion7 = ToLua.ToQuaternion(L, 5);
				float num8 = (float)LuaDLL.lua_tonumber(L, 6);
				int num9 = (int)LuaDLL.lua_tonumber(L, 7);
				RaycastHit hit4;
				bool value9 = Physics.BoxCast(vector25, vector26, vector27, ref hit4, quaternion7, num8, num9);
				LuaDLL.lua_pushboolean(L, value9);
				ToLua.Push(L, hit4);
				result = 2;
			}
			else if (num == 8 && TypeChecker.CheckTypes(L, 1, typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(LuaOut<RaycastHit>), typeof(Quaternion), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
			{
				Vector3 vector28 = ToLua.ToVector3(L, 1);
				Vector3 vector29 = ToLua.ToVector3(L, 2);
				Vector3 vector30 = ToLua.ToVector3(L, 3);
				Quaternion quaternion8 = ToLua.ToQuaternion(L, 5);
				float num10 = (float)LuaDLL.lua_tonumber(L, 6);
				int num11 = (int)LuaDLL.lua_tonumber(L, 7);
				QueryTriggerInteraction queryTriggerInteraction2 = (int)ToLua.ToObject(L, 8);
				RaycastHit hit5;
				bool value10 = Physics.BoxCast(vector28, vector29, vector30, ref hit5, quaternion8, num10, num11, queryTriggerInteraction2);
				LuaDLL.lua_pushboolean(L, value10);
				ToLua.Push(L, hit5);
				result = 2;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.BoxCast");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IgnoreCollision(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Collider), typeof(Collider)))
			{
				Collider collider = (Collider)ToLua.ToObject(L, 1);
				Collider collider2 = (Collider)ToLua.ToObject(L, 2);
				Physics.IgnoreCollision(collider, collider2);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Collider), typeof(Collider), typeof(bool)))
			{
				Collider collider3 = (Collider)ToLua.ToObject(L, 1);
				Collider collider4 = (Collider)ToLua.ToObject(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Physics.IgnoreCollision(collider3, collider4, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.IgnoreCollision");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IgnoreLayerCollision(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				Physics.IgnoreLayerCollision(num2, num3);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(bool)))
			{
				int num4 = (int)LuaDLL.lua_tonumber(L, 1);
				int num5 = (int)LuaDLL.lua_tonumber(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				Physics.IgnoreLayerCollision(num4, num5, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Physics.IgnoreLayerCollision");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetIgnoreLayerCollision(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			int num = (int)LuaDLL.luaL_checknumber(L, 1);
			int num2 = (int)LuaDLL.luaL_checknumber(L, 2);
			bool ignoreLayerCollision = Physics.GetIgnoreLayerCollision(num, num2);
			LuaDLL.lua_pushboolean(L, ignoreLayerCollision);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gravity(IntPtr L)
	{
		ToLua.Push(L, Physics.get_gravity());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultContactOffset(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Physics.get_defaultContactOffset());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bounceThreshold(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Physics.get_bounceThreshold());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_solverIterationCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Physics.get_solverIterationCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sleepThreshold(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, (double)Physics.get_sleepThreshold());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_queriesHitTriggers(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Physics.get_queriesHitTriggers());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_gravity(IntPtr L)
	{
		int result;
		try
		{
			Vector3 gravity = ToLua.ToVector3(L, 2);
			Physics.set_gravity(gravity);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultContactOffset(IntPtr L)
	{
		int result;
		try
		{
			float defaultContactOffset = (float)LuaDLL.luaL_checknumber(L, 2);
			Physics.set_defaultContactOffset(defaultContactOffset);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bounceThreshold(IntPtr L)
	{
		int result;
		try
		{
			float bounceThreshold = (float)LuaDLL.luaL_checknumber(L, 2);
			Physics.set_bounceThreshold(bounceThreshold);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_solverIterationCount(IntPtr L)
	{
		int result;
		try
		{
			int solverIterationCount = (int)LuaDLL.luaL_checknumber(L, 2);
			Physics.set_solverIterationCount(solverIterationCount);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sleepThreshold(IntPtr L)
	{
		int result;
		try
		{
			float sleepThreshold = (float)LuaDLL.luaL_checknumber(L, 2);
			Physics.set_sleepThreshold(sleepThreshold);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_queriesHitTriggers(IntPtr L)
	{
		int result;
		try
		{
			bool queriesHitTriggers = LuaDLL.luaL_checkboolean(L, 2);
			Physics.set_queriesHitTriggers(queriesHitTriggers);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
