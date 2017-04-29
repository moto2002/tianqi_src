using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LuaInterface
{
	public sealed class LuaUnityLibs
	{
		public static void OpenLibs(IntPtr L)
		{
			LuaUnityLibs.InitMathf(L);
			LuaUnityLibs.InitLayer(L);
		}

		public static void OpenLuaLibs(IntPtr L)
		{
			if (LuaDLL.tolua_openlualibs(L) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				LuaDLL.lua_pop(L, 1);
				throw new LuaException(msg, null, 1);
			}
			LuaUnityLibs.SetOutMethods(L, "Vector3", new LuaCSFunction(LuaUnityLibs.GetOutVector3));
			LuaUnityLibs.SetOutMethods(L, "Vector2", new LuaCSFunction(LuaUnityLibs.GetOutVector2));
			LuaUnityLibs.SetOutMethods(L, "Vector4", new LuaCSFunction(LuaUnityLibs.GetOutVector4));
			LuaUnityLibs.SetOutMethods(L, "Color", new LuaCSFunction(LuaUnityLibs.GetOutColor));
			LuaUnityLibs.SetOutMethods(L, "Quaternion", new LuaCSFunction(LuaUnityLibs.GetOutQuaternion));
			LuaUnityLibs.SetOutMethods(L, "Ray", new LuaCSFunction(LuaUnityLibs.GetOutRay));
			LuaUnityLibs.SetOutMethods(L, "Bounds", new LuaCSFunction(LuaUnityLibs.GetOutBounds));
			LuaUnityLibs.SetOutMethods(L, "Touch", new LuaCSFunction(LuaUnityLibs.GetOutTouch));
			LuaUnityLibs.SetOutMethods(L, "RaycastHit", new LuaCSFunction(LuaUnityLibs.GetOutRaycastHit));
			LuaUnityLibs.SetOutMethods(L, "LayerMask", new LuaCSFunction(LuaUnityLibs.GetOutLayerMask));
		}

		private static void InitMathf(IntPtr L)
		{
			LuaDLL.lua_getglobal(L, "Mathf");
			LuaDLL.lua_pushstring(L, "PerlinNoise");
			LuaDLL.tolua_pushcfunction(L, new LuaCSFunction(LuaUnityLibs.PerlinNoise));
			LuaDLL.lua_rawset(L, -3);
			LuaDLL.lua_pop(L, 1);
		}

		private static void InitLayer(IntPtr L)
		{
			LuaDLL.tolua_createtable(L, "Layer", 0);
			for (int i = 0; i < 32; i++)
			{
				string text = LayerMask.LayerToName(i);
				if (!string.IsNullOrEmpty(text))
				{
					LuaDLL.lua_pushstring(L, text);
					LuaDLL.lua_pushinteger(L, i);
					LuaDLL.lua_rawset(L, -3);
				}
			}
			LuaDLL.lua_pop(L, 1);
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int PerlinNoise(IntPtr L)
		{
			int result;
			try
			{
				float num = (float)LuaDLL.luaL_checknumber(L, 1);
				float num2 = (float)LuaDLL.luaL_checknumber(L, 2);
				float num3 = Mathf.PerlinNoise(num, num2);
				LuaDLL.lua_pushnumber(L, (double)num3);
				result = 1;
			}
			catch (Exception e)
			{
				result = LuaDLL.toluaL_exception(L, e, null);
			}
			return result;
		}

		private static void SetOutMethods(IntPtr L, string table, LuaCSFunction getOutFunc = null)
		{
			LuaDLL.lua_getglobal(L, table);
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(getOutFunc);
			LuaDLL.tolua_variable(L, "out", functionPointerForDelegate, IntPtr.Zero);
			LuaDLL.lua_pop(L, 1);
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutVector3(IntPtr L)
		{
			ToLua.PushOut<Vector3>(L, new LuaOut<Vector3>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutVector2(IntPtr L)
		{
			ToLua.PushOut<Vector2>(L, new LuaOut<Vector2>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutVector4(IntPtr L)
		{
			ToLua.PushOut<Vector4>(L, new LuaOut<Vector4>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutColor(IntPtr L)
		{
			ToLua.PushOut<Color>(L, new LuaOut<Color>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutQuaternion(IntPtr L)
		{
			ToLua.PushOut<Quaternion>(L, new LuaOut<Quaternion>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutRay(IntPtr L)
		{
			ToLua.PushOut<Ray>(L, new LuaOut<Ray>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutBounds(IntPtr L)
		{
			ToLua.PushOut<Bounds>(L, new LuaOut<Bounds>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutRaycastHit(IntPtr L)
		{
			ToLua.PushOut<RaycastHit>(L, new LuaOut<RaycastHit>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutTouch(IntPtr L)
		{
			ToLua.PushOut<Touch>(L, new LuaOut<Touch>());
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetOutLayerMask(IntPtr L)
		{
			ToLua.PushOut<LayerMask>(L, new LuaOut<LayerMask>());
			return 1;
		}
	}
}
