using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace LuaInterface
{
	public static class ToLua
	{
		private static Type monoType = typeof(Type).GetType();

		public static void OpenLibs(IntPtr L)
		{
			LuaDLL.lua_getglobal(L, "tolua");
			LuaDLL.lua_pushstring(L, "isnull");
			LuaDLL.lua_pushcfunction(L, new LuaCSFunction(ToLua.IsNull));
			LuaDLL.lua_rawset(L, -3);
			LuaDLL.lua_pushstring(L, "tolstring");
			LuaDLL.tolua_pushcfunction(L, new LuaCSFunction(ToLua.BufferToString));
			LuaDLL.lua_rawset(L, -3);
			LuaDLL.lua_pushstring(L, "typeof");
			LuaDLL.lua_pushcfunction(L, new LuaCSFunction(ToLua.GetClassType));
			LuaDLL.lua_rawset(L, -3);
			int metaReference = LuaStatic.GetMetaReference(L, typeof(NullObject));
			LuaDLL.lua_pushstring(L, "null");
			LuaDLL.tolua_pushnewudata(L, metaReference, 1);
			LuaDLL.lua_rawset(L, -3);
			LuaDLL.lua_pop(L, 1);
			LuaDLL.tolua_pushudata(L, 1);
			LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "null");
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int IsNull(IntPtr L)
		{
			if (LuaDLL.lua_type(L, 1) == LuaTypes.LUA_TNIL)
			{
				LuaDLL.lua_pushboolean(L, true);
			}
			else
			{
				object obj = ToLua.ToObject(L, -1);
				if (obj == null)
				{
					LuaDLL.lua_pushboolean(L, true);
				}
				else if (obj is Object)
				{
					Object @object = (Object)obj;
					LuaDLL.lua_pushboolean(L, @object == null);
				}
				else
				{
					LuaDLL.lua_pushboolean(L, false);
				}
			}
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int BufferToString(IntPtr L)
		{
			try
			{
				object obj = ToLua.CheckObject(L, 1);
				if (obj is byte[])
				{
					byte[] array = (byte[])obj;
					LuaDLL.lua_pushlstring(L, array, array.Length);
				}
				else if (obj is char[])
				{
					byte[] bytes = Encoding.get_UTF8().GetBytes((char[])obj);
					LuaDLL.lua_pushlstring(L, bytes, bytes.Length);
				}
				else if (obj is string)
				{
					LuaDLL.lua_pushstring(L, (string)obj);
				}
				else
				{
					LuaDLL.luaL_typerror(L, 1, "byte[] or char[]", null);
				}
			}
			catch (Exception e)
			{
				LuaDLL.toluaL_exception(L, e, null);
			}
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int GetClassType(IntPtr L)
		{
			int num = LuaDLL.tolua_getmetatableref(L, -1);
			if (num > 0)
			{
				Type classType = LuaStatic.GetClassType(L, num);
				ToLua.Push(L, classType);
			}
			else
			{
				switch (LuaDLL.tolua_getvaluetype(L, -1))
				{
				case LuaValueType.Vector3:
					ToLua.Push(L, typeof(Vector3));
					return 1;
				case LuaValueType.Quaternion:
					ToLua.Push(L, typeof(Quaternion));
					return 1;
				case LuaValueType.Vector2:
					ToLua.Push(L, typeof(Vector2));
					return 1;
				case LuaValueType.Color:
					ToLua.Push(L, typeof(Color));
					return 1;
				case LuaValueType.Vector4:
					ToLua.Push(L, typeof(Vector4));
					return 1;
				case LuaValueType.Ray:
					ToLua.Push(L, typeof(Ray));
					return 1;
				case LuaValueType.Bounds:
					ToLua.Push(L, typeof(Bounds));
					return 1;
				case LuaValueType.LayerMask:
					ToLua.Push(L, typeof(LayerMask));
					return 1;
				case LuaValueType.RaycastHit:
					ToLua.Push(L, typeof(RaycastHit));
					return 1;
				}
				Debugger.LogError("type not register to lua");
				LuaDLL.lua_pushnil(L);
			}
			return 1;
		}

		public static string ToString(IntPtr L, int stackPos)
		{
			if (LuaDLL.lua_isstring(L, stackPos) == 1)
			{
				return LuaDLL.lua_tostring(L, stackPos);
			}
			if (LuaDLL.lua_isuserdata(L, stackPos) == 1)
			{
				return (string)ToLua.ToObject(L, stackPos);
			}
			return null;
		}

		public static object ToObject(IntPtr L, int stackPos)
		{
			int num = LuaDLL.tolua_rawnetobj(L, stackPos);
			if (num != -1)
			{
				ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
				return objectTranslator.GetObject(num);
			}
			return null;
		}

		public static LuaFunction ToLuaFunction(IntPtr L, int stackPos)
		{
			stackPos = LuaDLL.abs_index(L, stackPos);
			LuaDLL.lua_pushvalue(L, stackPos);
			int reference = LuaDLL.toluaL_ref(L);
			return LuaStatic.GetFunction(L, reference);
		}

		public static LuaTable ToLuaTable(IntPtr L, int stackPos)
		{
			stackPos = LuaDLL.abs_index(L, stackPos);
			LuaDLL.lua_pushvalue(L, stackPos);
			int reference = LuaDLL.toluaL_ref(L);
			return LuaStatic.GetTable(L, reference);
		}

		public static LuaThread ToLuaThread(IntPtr L, int stackPos)
		{
			stackPos = LuaDLL.abs_index(L, stackPos);
			LuaDLL.lua_pushvalue(L, stackPos);
			int reference = LuaDLL.toluaL_ref(L);
			return LuaStatic.GetLuaThread(L, reference);
		}

		public static Vector3 ToVector3(IntPtr L, int stackPos)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			LuaDLL.tolua_getvec3(L, stackPos, out num, out num2, out num3);
			return new Vector3(num, num2, num3);
		}

		public static Vector4 ToVector4(IntPtr L, int stackPos)
		{
			float num;
			float num2;
			float num3;
			float num4;
			LuaDLL.tolua_getvec4(L, stackPos, out num, out num2, out num3, out num4);
			return new Vector4(num, num2, num3, num4);
		}

		public static Vector2 ToVector2(IntPtr L, int stackPos)
		{
			float num;
			float num2;
			LuaDLL.tolua_getvec2(L, stackPos, out num, out num2);
			return new Vector2(num, num2);
		}

		public static Quaternion ToQuaternion(IntPtr L, int stackPos)
		{
			float num;
			float num2;
			float num3;
			float num4;
			LuaDLL.tolua_getquat(L, stackPos, out num, out num2, out num3, out num4);
			return new Quaternion(num, num2, num3, num4);
		}

		public static Color ToColor(IntPtr L, int stackPos)
		{
			float num;
			float num2;
			float num3;
			float num4;
			LuaDLL.tolua_getclr(L, stackPos, out num, out num2, out num3, out num4);
			return new Color(num, num2, num3, num4);
		}

		public static Ray ToRay(IntPtr L, int stackPos)
		{
			int num = LuaDLL.lua_gettop(L);
			LuaStatic.GetUnpackRayRef(L);
			LuaDLL.lua_pushvalue(L, stackPos);
			if (LuaDLL.lua_pcall(L, 1, 2, 0) == 0)
			{
				Vector3 vector = ToLua.ToVector3(L, num + 1);
				Vector3 vector2 = ToLua.ToVector3(L, num + 2);
				return new Ray(vector, vector2);
			}
			string msg = LuaDLL.lua_tostring(L, -1);
			throw new LuaException(msg, null, 1);
		}

		public static Bounds ToBounds(IntPtr L, int stackPos)
		{
			int num = LuaDLL.lua_gettop(L);
			LuaStatic.GetUnpackBounds(L);
			LuaDLL.lua_pushvalue(L, stackPos);
			if (LuaDLL.lua_pcall(L, 1, 2, 0) == 0)
			{
				Vector3 vector = ToLua.ToVector3(L, num + 1);
				Vector3 vector2 = ToLua.ToVector3(L, num + 2);
				return new Bounds(vector, vector2);
			}
			string msg = LuaDLL.lua_tostring(L, -1);
			throw new LuaException(msg, null, 1);
		}

		public static LayerMask ToLayerMask(IntPtr L, int stackPos)
		{
			return LuaDLL.tolua_getlayermask(L, stackPos);
		}

		public static LuaInteger64 CheckLuaInteger64(IntPtr L, int stackPos)
		{
			if (!LuaDLL.tolua_isint64(L, stackPos))
			{
				LuaDLL.luaL_typerror(L, stackPos, "int64", null);
				return 0L;
			}
			return LuaDLL.tolua_toint64(L, stackPos);
		}

		public static object ToVarObject(IntPtr L, int stackPos)
		{
			switch (LuaDLL.lua_type(L, stackPos))
			{
			case LuaTypes.LUA_TNIL:
				return null;
			case LuaTypes.LUA_TBOOLEAN:
				return LuaDLL.lua_toboolean(L, stackPos);
			case LuaTypes.LUA_TLIGHTUSERDATA:
				return LuaDLL.lua_touserdata(L, stackPos);
			case LuaTypes.LUA_TNUMBER:
				return LuaDLL.lua_tonumber(L, stackPos);
			case LuaTypes.LUA_TSTRING:
				return LuaDLL.lua_tostring(L, stackPos);
			case LuaTypes.LUA_TTABLE:
				return ToLua.ToVarTable(L, stackPos);
			case LuaTypes.LUA_TFUNCTION:
				return ToLua.ToLuaFunction(L, stackPos);
			case LuaTypes.LUA_TUSERDATA:
				return ToLua.ToObject(L, stackPos);
			case LuaTypes.LUA_TTHREAD:
				return ToLua.ToLuaThread(L, stackPos);
			default:
				return null;
			}
		}

		public static object ToVarTable(IntPtr L, int stackPos)
		{
			stackPos = LuaDLL.abs_index(L, stackPos);
			switch (LuaDLL.tolua_getvaluetype(L, stackPos))
			{
			case LuaValueType.Vector3:
				return ToLua.ToVector3(L, stackPos);
			case LuaValueType.Quaternion:
				return ToLua.ToQuaternion(L, stackPos);
			case LuaValueType.Vector2:
				return ToLua.ToVector2(L, stackPos);
			case LuaValueType.Color:
				return ToLua.ToColor(L, stackPos);
			case LuaValueType.Vector4:
				return ToLua.ToVector4(L, stackPos);
			case LuaValueType.Ray:
				return ToLua.ToRay(L, stackPos);
			case LuaValueType.Bounds:
				return ToLua.ToBounds(L, stackPos);
			case LuaValueType.LayerMask:
				return ToLua.ToLayerMask(L, stackPos);
			}
			LuaDLL.lua_pushvalue(L, stackPos);
			int reference = LuaDLL.toluaL_ref(L);
			return LuaStatic.GetTable(L, reference);
		}

		public static LuaFunction CheckLuaFunction(IntPtr L, int stackPos)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
			if (luaTypes == LuaTypes.LUA_TNIL)
			{
				return null;
			}
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				LuaDLL.luaL_typerror(L, stackPos, "function", null);
				return null;
			}
			return ToLua.ToLuaFunction(L, stackPos);
		}

		public static LuaTable CheckLuaTable(IntPtr L, int stackPos)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
			if (luaTypes == LuaTypes.LUA_TNIL)
			{
				return null;
			}
			if (luaTypes != LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_typerror(L, stackPos, "table", null);
				return null;
			}
			return ToLua.ToLuaTable(L, stackPos);
		}

		public static LuaThread CheckLuaThread(IntPtr L, int stackPos)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
			if (luaTypes == LuaTypes.LUA_TNIL)
			{
				return null;
			}
			if (luaTypes != LuaTypes.LUA_TTHREAD)
			{
				LuaDLL.luaL_typerror(L, stackPos, "thread", null);
				return null;
			}
			return ToLua.ToLuaThread(L, stackPos);
		}

		public static string CheckString(IntPtr L, int stackPos)
		{
			if (LuaDLL.lua_isstring(L, stackPos) == 1)
			{
				return LuaDLL.lua_tostring(L, stackPos);
			}
			if (LuaDLL.lua_isuserdata(L, stackPos) == 1)
			{
				return (string)ToLua.CheckObject(L, stackPos, typeof(string));
			}
			if (LuaDLL.lua_isnil(L, stackPos))
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, "string", null);
			return null;
		}

		public static object CheckObject(IntPtr L, int stackPos)
		{
			int num = LuaDLL.tolua_rawnetobj(L, stackPos);
			if (num != -1)
			{
				ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
				return objectTranslator.GetObject(num);
			}
			if (LuaDLL.lua_isnil(L, stackPos))
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, "object", null);
			return null;
		}

		public static object CheckObject(IntPtr L, int stackPos, Type type)
		{
			int num = LuaDLL.tolua_rawnetobj(L, stackPos);
			if (num != -1)
			{
				ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
				object @object = objectTranslator.GetObject(num);
				if (@object != null)
				{
					Type type2 = @object.GetType();
					if (type == type2 || type.IsAssignableFrom(type2))
					{
						return @object;
					}
					LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got {1}", type.get_FullName(), type2.get_FullName()));
				}
				return null;
			}
			if (LuaDLL.lua_isnil(L, stackPos) && !TypeChecker.IsValueType(type))
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, type.get_FullName(), null);
			return null;
		}

		public static Object CheckUnityObject(IntPtr L, int stackPos, Type type)
		{
			int num = LuaDLL.tolua_rawnetobj(L, stackPos);
			if (num != -1)
			{
				ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
				object @object = objectTranslator.GetObject(num);
				if (@object != null)
				{
					Object object2 = (Object)@object;
					if (object2 == null)
					{
						LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type.get_FullName()));
						return null;
					}
					Type type2 = object2.GetType();
					if (type == type2 || type2.IsSubclassOf(type))
					{
						return object2;
					}
					LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got {1}", type.get_FullName(), type2.get_FullName()));
				}
				return null;
			}
			if (LuaDLL.lua_isnil(L, stackPos))
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, type.get_FullName(), null);
			return null;
		}

		public static TrackedReference CheckTrackedReference(IntPtr L, int stackPos, Type type)
		{
			int num = LuaDLL.tolua_rawnetobj(L, stackPos);
			if (num != -1)
			{
				ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
				object @object = objectTranslator.GetObject(num);
				if (@object != null)
				{
					TrackedReference trackedReference = (TrackedReference)@object;
					if (trackedReference == null)
					{
						LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type.get_FullName()));
						return null;
					}
					Type type2 = trackedReference.GetType();
					if (type == type2 || type2.IsSubclassOf(type))
					{
						return trackedReference;
					}
					LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got {1}", type.get_FullName(), type2.get_FullName()));
				}
				return null;
			}
			if (LuaDLL.lua_isnil(L, stackPos))
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, type.get_FullName(), null);
			return null;
		}

		public static object[] CheckObjectArray(IntPtr L, int stackPos)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				int num = 1;
				List<object> list = new List<object>();
				LuaDLL.lua_pushvalue(L, stackPos);
				while (true)
				{
					LuaDLL.lua_rawgeti(L, -1, num);
					if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
					{
						break;
					}
					object obj = ToLua.ToVarObject(L, -1);
					list.Add(obj);
					LuaDLL.lua_pop(L, 1);
					num++;
				}
				LuaDLL.lua_pop(L, 1);
				return list.ToArray();
			}
			if (luaTypes == LuaTypes.LUA_TUSERDATA)
			{
				return (object[])ToLua.CheckObject(L, stackPos, typeof(object[]));
			}
			if (luaTypes == LuaTypes.LUA_TNIL)
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, "object[] or table", null);
			return null;
		}

		public static T[] CheckObjectArray<T>(IntPtr L, int stackPos)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				int num = 1;
				T t = default(T);
				Type typeFromHandle = typeof(T);
				List<T> list = new List<T>();
				LuaDLL.lua_pushvalue(L, stackPos);
				while (true)
				{
					LuaDLL.lua_rawgeti(L, -1, num);
					if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
					{
						break;
					}
					if (!TypeChecker.CheckType(L, typeFromHandle, -1))
					{
						goto Block_3;
					}
					T t2 = (T)((object)ToLua.ToVarObject(L, -1));
					list.Add(t2);
					LuaDLL.lua_pop(L, 1);
					num++;
				}
				LuaDLL.lua_pop(L, 1);
				return list.ToArray();
				Block_3:
				LuaDLL.lua_pop(L, 1);
			}
			else
			{
				if (luaTypes == LuaTypes.LUA_TUSERDATA)
				{
					return (T[])ToLua.CheckObject(L, stackPos, typeof(T[]));
				}
				if (luaTypes == LuaTypes.LUA_TNIL)
				{
					return null;
				}
			}
			LuaDLL.luaL_typerror(L, stackPos, typeof(T[]).get_FullName(), null);
			return null;
		}

		public static char[] CheckCharBuffer(IntPtr L, int stackPos)
		{
			if (LuaDLL.lua_isstring(L, stackPos) != 0)
			{
				string text = LuaDLL.lua_tostring(L, stackPos);
				return text.ToCharArray();
			}
			if (LuaDLL.lua_isuserdata(L, stackPos) != 0)
			{
				return (char[])ToLua.CheckObject(L, stackPos, typeof(char[]));
			}
			if (LuaDLL.lua_isnil(L, stackPos))
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, "string or char[]", null);
			return null;
		}

		public static byte[] CheckByteBuffer(IntPtr L, int stackPos)
		{
			if (LuaDLL.lua_isstring(L, stackPos) != 0)
			{
				int num;
				IntPtr intPtr = LuaDLL.lua_tolstring(L, stackPos, out num);
				byte[] array = new byte[num];
				Marshal.Copy(intPtr, array, 0, num);
				return array;
			}
			if (LuaDLL.lua_isuserdata(L, stackPos) != 0)
			{
				return (byte[])ToLua.CheckObject(L, stackPos, typeof(byte[]));
			}
			if (LuaDLL.lua_isnil(L, stackPos))
			{
				return null;
			}
			LuaDLL.luaL_typerror(L, stackPos, "string or byte[]", null);
			return null;
		}

		public static T[] CheckNumberArray<T>(IntPtr L, int stackPos)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				int num = 1;
				T t = default(T);
				List<T> list = new List<T>();
				LuaDLL.lua_pushvalue(L, stackPos);
				while (true)
				{
					LuaDLL.lua_rawgeti(L, -1, num);
					luaTypes = LuaDLL.lua_type(L, -1);
					if (luaTypes == LuaTypes.LUA_TNIL)
					{
						break;
					}
					if (luaTypes != LuaTypes.LUA_TNUMBER)
					{
						goto Block_3;
					}
					T t2 = (T)((object)Convert.ChangeType(LuaDLL.lua_tonumber(L, -1), typeof(T)));
					list.Add(t2);
					LuaDLL.lua_pop(L, 1);
					num++;
				}
				LuaDLL.lua_pop(L, 1);
				return list.ToArray();
				Block_3:;
			}
			else
			{
				if (luaTypes == LuaTypes.LUA_TUSERDATA)
				{
					return (T[])ToLua.CheckObject(L, stackPos, typeof(T[]));
				}
				if (luaTypes == LuaTypes.LUA_TNIL)
				{
					return null;
				}
			}
			LuaDLL.luaL_typerror(L, stackPos, LuaMisc.GetTypeName(typeof(T[])), null);
			return null;
		}

		public static bool[] CheckBoolArray(IntPtr L, int stackPos)
		{
			throw new LuaException("not defined CheckBoolArray", null, 1);
		}

		public static string[] CheckStringArray(IntPtr L, int stackPos)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				int num = 1;
				Type typeFromHandle = typeof(string);
				List<string> list = new List<string>();
				LuaDLL.lua_pushvalue(L, stackPos);
				while (true)
				{
					LuaDLL.lua_rawgeti(L, -1, num);
					if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
					{
						break;
					}
					if (!TypeChecker.CheckType(L, typeFromHandle, -1))
					{
						goto Block_3;
					}
					string text = ToLua.ToString(L, -1);
					list.Add(text);
					LuaDLL.lua_pop(L, 1);
					num++;
				}
				LuaDLL.lua_pop(L, 1);
				return list.ToArray();
				Block_3:
				LuaDLL.lua_pop(L, 1);
			}
			else
			{
				if (luaTypes == LuaTypes.LUA_TUSERDATA)
				{
					return (string[])ToLua.CheckObject(L, stackPos, typeof(string[]));
				}
				if (luaTypes == LuaTypes.LUA_TNIL)
				{
					return null;
				}
			}
			LuaDLL.luaL_typerror(L, stackPos, "string[]", null);
			return null;
		}

		public static object[] ToParamsObject(IntPtr L, int stackPos, int count)
		{
			List<object> list = new List<object>(count);
			while (count > 0)
			{
				object obj = ToLua.ToVarObject(L, stackPos);
				list.Add(obj);
				count--;
				stackPos++;
			}
			return list.ToArray();
		}

		public static T[] ToParamsObject<T>(IntPtr L, int stackPos, int count) where T : class
		{
			List<T> list = new List<T>(count);
			T t = (T)((object)null);
			while (count > 0)
			{
				object obj = ToLua.ToObject(L, stackPos);
				t = (T)((object)obj);
				list.Add(t);
				count--;
				stackPos++;
			}
			return list.ToArray();
		}

		public static string[] ToParamsString(IntPtr L, int stackPos, int count)
		{
			List<string> list = new List<string>(count);
			while (count > 0)
			{
				string text = ToLua.ToString(L, stackPos);
				stackPos++;
				count--;
				list.Add(text);
			}
			return list.ToArray();
		}

		public static T[] ToParamsNumber<T>(IntPtr L, int stackPos, int count)
		{
			List<T> list = new List<T>(count);
			while (count > 0)
			{
				double num = LuaDLL.lua_tonumber(L, stackPos);
				stackPos++;
				count--;
				T t = (T)((object)Convert.ChangeType(num, typeof(T)));
				list.Add(t);
			}
			return list.ToArray();
		}

		public static char[] ToParamsChar(IntPtr L, int stackPos, int count)
		{
			char[] array = new char[count];
			int i = 0;
			while (i < count)
			{
				char c = (char)LuaDLL.lua_tointeger(L, stackPos);
				array[i++] = c;
				stackPos++;
			}
			return array;
		}

		public static bool[] CheckParamsBool(IntPtr L, int stackPos, int count)
		{
			throw new LuaException("not defined CheckParamsBool", null, 1);
		}

		public static T[] CheckParamsNumber<T>(IntPtr L, int stackPos, int count)
		{
			double[] array = new double[count];
			int i = 0;
			while (i < count)
			{
				if (LuaDLL.lua_isnumber(L, stackPos) == 0)
				{
					LuaDLL.luaL_typerror(L, stackPos, LuaMisc.GetTypeName(typeof(T)), null);
					return null;
				}
				array[i] = LuaDLL.lua_tonumber(L, stackPos);
				i++;
				stackPos++;
			}
			return (T[])Convert.ChangeType(array, typeof(T[]));
		}

		public static char[] CheckParamsChar(IntPtr L, int stackPos, int count)
		{
			char[] array = new char[count];
			int i = 0;
			while (i < count)
			{
				if (LuaDLL.lua_isnumber(L, stackPos) == 0)
				{
					LuaDLL.luaL_typerror(L, stackPos, "char", null);
					return null;
				}
				array[i] = (char)LuaDLL.lua_tointeger(L, stackPos);
				i++;
				stackPos++;
			}
			return array;
		}

		public static string[] CheckParamsString(IntPtr L, int stackPos, int count)
		{
			string[] array = new string[count];
			int i = 0;
			while (i < count)
			{
				array[i++] = ToLua.CheckString(L, stackPos++);
			}
			return array;
		}

		public static T[] CheckParamsObject<T>(IntPtr L, int stackPos, int count)
		{
			List<T> list = new List<T>(count);
			T t = default(T);
			Type typeFromHandle = typeof(T);
			while (count > 0)
			{
				object obj = ToLua.ToVarObject(L, stackPos);
				if (!TypeChecker.CheckType(L, typeFromHandle, stackPos))
				{
					LuaDLL.luaL_typerror(L, stackPos, LuaMisc.GetTypeName(typeFromHandle), null);
					break;
				}
				T t2 = (T)((object)obj);
				list.Add(t2);
				count--;
				stackPos++;
			}
			return list.ToArray();
		}

		public static void Push(IntPtr L, Vector3 v3)
		{
			LuaDLL.tolua_pushvec3(L, v3.x, v3.y, v3.z);
		}

		public static void Push(IntPtr L, Vector2 v2)
		{
			LuaDLL.tolua_pushvec2(L, v2.x, v2.y);
		}

		public static void Push(IntPtr L, Vector4 v4)
		{
			LuaDLL.tolua_pushvec4(L, v4.x, v4.y, v4.z, v4.w);
		}

		public static void Push(IntPtr L, Quaternion q)
		{
			LuaDLL.tolua_pushquat(L, q.x, q.y, q.z, q.w);
		}

		public static void Push(IntPtr L, Color clr)
		{
			LuaDLL.tolua_pushclr(L, clr.r, clr.g, clr.b, clr.a);
		}

		public static void Push(IntPtr L, Ray ray)
		{
			LuaStatic.GetPackRay(L);
			ToLua.Push(L, ray.get_direction());
			ToLua.Push(L, ray.get_origin());
			if (LuaDLL.lua_pcall(L, 2, 1, 0) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		public static void Push(IntPtr L, Bounds bound)
		{
			LuaStatic.GetPackBounds(L);
			ToLua.Push(L, bound.get_center());
			ToLua.Push(L, bound.get_size());
			if (LuaDLL.lua_pcall(L, 2, 1, 0) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		public static void Push(IntPtr L, RaycastHit hit)
		{
			ToLua.Push(L, hit, 31);
		}

		public static void Push(IntPtr L, RaycastHit hit, int flag)
		{
			LuaStatic.GetPackRaycastHit(L);
			if ((flag & 1) != 0)
			{
				ToLua.Push(L, hit.get_collider());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			LuaDLL.lua_pushnumber(L, (double)hit.get_distance());
			if ((flag & 2) != 0)
			{
				ToLua.Push(L, hit.get_normal());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			if ((flag & 4) != 0)
			{
				ToLua.Push(L, hit.get_point());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			if ((flag & 8) != 0)
			{
				ToLua.Push(L, hit.get_rigidbody());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			if ((flag & 16) != 0)
			{
				ToLua.Push(L, hit.get_transform());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			if (LuaDLL.lua_pcall(L, 6, 1, 0) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		public static void Push(IntPtr L, Touch t)
		{
			ToLua.Push(L, t, 7);
		}

		public static void Push(IntPtr L, Touch t, int flag)
		{
			LuaStatic.GetPackTouch(L);
			LuaDLL.lua_pushinteger(L, t.get_fingerId());
			if ((flag & 2) != 0)
			{
				ToLua.Push(L, t.get_position());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			if ((flag & 4) != 0)
			{
				ToLua.Push(L, t.get_rawPosition());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			if ((flag & 1) != 0)
			{
				ToLua.Push(L, t.get_deltaPosition());
			}
			else
			{
				LuaDLL.lua_pushnil(L);
			}
			LuaDLL.lua_pushnumber(L, (double)t.get_deltaTime());
			LuaDLL.lua_pushinteger(L, t.get_tapCount());
			LuaDLL.lua_pushinteger(L, t.get_phase());
			if (LuaDLL.lua_pcall(L, 7, -1, 0) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		public static void PushLayerMask(IntPtr L, LayerMask l)
		{
			LuaDLL.tolua_pushlayermask(L, l.get_value());
		}

		public static void Push(IntPtr L, LuaByteBuffer bb)
		{
			LuaDLL.lua_pushlstring(L, bb.buffer, bb.buffer.Length);
		}

		public static void Push(IntPtr L, Array array)
		{
			if (array == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				int arrayMetatable = LuaStatic.GetArrayMetatable(L);
				ToLua.PushUserData(L, array, arrayMetatable);
			}
		}

		public static void Push(IntPtr L, LuaBaseRef lbr)
		{
			if (lbr == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				LuaDLL.lua_getref(L, lbr.GetReference());
			}
		}

		public static void Push(IntPtr L, Type t)
		{
			if (t == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				int typeMetatable = LuaStatic.GetTypeMetatable(L);
				ToLua.PushUserData(L, t, typeMetatable);
			}
		}

		public static void Push(IntPtr L, Delegate ev)
		{
			if (ev == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				int delegateMetatable = LuaStatic.GetDelegateMetatable(L);
				ToLua.PushUserData(L, ev, delegateMetatable);
			}
		}

		public static void Push(IntPtr L, EventObject ev)
		{
			if (ev == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				int eventMetatable = LuaStatic.GetEventMetatable(L);
				ToLua.PushUserData(L, ev, eventMetatable);
			}
		}

		public static void Push(IntPtr L, IEnumerator iter)
		{
			if (iter == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				int iterMetatable = LuaStatic.GetIterMetatable(L);
				ToLua.PushUserData(L, iter, iterMetatable);
			}
		}

		public static void Push(IntPtr L, Enum e)
		{
			if (e == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				object o = null;
				int enumObject = LuaStatic.GetEnumObject(L, e, out o);
				ToLua.PushUserData(L, o, enumObject);
			}
		}

		public static void PushOut<T>(IntPtr L, LuaOut<T> lo)
		{
			ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
			int index = objectTranslator.AddObject(lo);
			int outMetatable = LuaStatic.GetOutMetatable(L);
			LuaDLL.tolua_pushnewudata(L, outMetatable, index);
		}

		public static void PushValue(IntPtr L, ValueType v)
		{
			if (v == null)
			{
				LuaDLL.lua_pushnil(L);
				return;
			}
			Type type = v.GetType();
			int metaReference = LuaStatic.GetMetaReference(L, type);
			ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
			if (metaReference > 0)
			{
				int index = objectTranslator.AddObject(v);
				LuaDLL.tolua_pushnewudata(L, metaReference, index);
			}
			else
			{
				LuaCSFunction preModule = LuaStatic.GetPreModule(L, type);
				if (preModule != null)
				{
					preModule(L);
					metaReference = LuaStatic.GetMetaReference(L, type);
					if (metaReference > 0)
					{
						int index2 = objectTranslator.AddObject(v);
						LuaDLL.tolua_pushnewudata(L, metaReference, index2);
						return;
					}
				}
				LuaDLL.lua_pushnil(L);
				Debugger.LogError("Type {0} not wrap to lua", LuaMisc.GetTypeName(type));
			}
		}

		private static void PushUserData(IntPtr L, object o, int reference)
		{
			ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
			int index;
			if (objectTranslator.Getudata(o, out index) && LuaDLL.tolua_pushudata(L, index))
			{
				return;
			}
			index = objectTranslator.AddObject(o);
			LuaDLL.tolua_pushnewudata(L, reference, index);
		}

		private static void LuaPCall(IntPtr L, LuaCSFunction func)
		{
			LuaDLL.tolua_pushcfunction(L, func);
			if (LuaDLL.lua_pcall(L, 0, -1, 0) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				Exception luaStack = LuaException.luaStack;
				LuaException.luaStack = null;
				throw new LuaException(msg, luaStack, 1);
			}
		}

		private static void PushPreLoadType(IntPtr L, object o, Type type)
		{
			LuaCSFunction preModule = LuaStatic.GetPreModule(L, type);
			if (preModule != null)
			{
				ToLua.LuaPCall(L, preModule);
				int metaReference = LuaStatic.GetMetaReference(L, type);
				if (metaReference > 0)
				{
					ToLua.PushUserData(L, o, metaReference);
					return;
				}
			}
			LuaDLL.lua_pushnil(L);
			Debugger.LogError("Type {0} not wrap to lua", LuaMisc.GetTypeName(type));
		}

		private static void PushUserObject(IntPtr L, object o)
		{
			Type type = o.GetType();
			int metaReference = LuaStatic.GetMetaReference(L, type);
			if (metaReference > 0)
			{
				ToLua.PushUserData(L, o, metaReference);
			}
			else
			{
				ToLua.PushPreLoadType(L, o, type);
			}
		}

		public static void Push(IntPtr L, Object obj)
		{
			if (obj == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				ToLua.PushUserObject(L, obj);
			}
		}

		public static void Push(IntPtr L, TrackedReference obj)
		{
			if (obj == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				ToLua.PushUserObject(L, obj);
			}
		}

		public static void PushObject(IntPtr L, object o)
		{
			if (o == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				ToLua.PushUserObject(L, o);
			}
		}

		public static void Push(IntPtr L, object obj)
		{
			if (obj == null)
			{
				LuaDLL.lua_pushnil(L);
				return;
			}
			Type type = obj.GetType();
			if (type.get_IsValueType())
			{
				if (type == typeof(bool))
				{
					bool value = (bool)obj;
					LuaDLL.lua_pushboolean(L, value);
				}
				else if (type.get_IsEnum())
				{
					ToLua.Push(L, (Enum)obj);
				}
				else if (type.get_IsPrimitive())
				{
					double number = LuaMisc.ToDouble(obj);
					LuaDLL.lua_pushnumber(L, number);
				}
				else if (type == typeof(Vector3))
				{
					ToLua.Push(L, (Vector3)obj);
				}
				else if (type == typeof(Quaternion))
				{
					ToLua.Push(L, (Quaternion)obj);
				}
				else if (type == typeof(Vector2))
				{
					ToLua.Push(L, (Vector2)obj);
				}
				else if (type == typeof(Vector4))
				{
					ToLua.Push(L, (Vector4)obj);
				}
				else if (type == typeof(Color))
				{
					ToLua.Push(L, (Color)obj);
				}
				else if (type == typeof(RaycastHit))
				{
					ToLua.Push(L, (RaycastHit)obj);
				}
				else if (type == typeof(Touch))
				{
					ToLua.Push(L, (Touch)obj);
				}
				else if (type == typeof(Ray))
				{
					ToLua.Push(L, (Ray)obj);
				}
				else if (type == typeof(Bounds))
				{
					ToLua.Push(L, (Bounds)obj);
				}
				else if (type == typeof(LayerMask))
				{
					ToLua.PushLayerMask(L, (LayerMask)obj);
				}
				else
				{
					ToLua.PushValue(L, (ValueType)obj);
				}
			}
			else if (type.get_IsArray())
			{
				ToLua.Push(L, (Array)obj);
			}
			else if (type == typeof(string))
			{
				LuaDLL.lua_pushstring(L, (string)obj);
			}
			else if (type.IsSubclassOf(typeof(LuaBaseRef)))
			{
				ToLua.Push(L, (LuaBaseRef)obj);
			}
			else if (type.IsSubclassOf(typeof(Object)))
			{
				ToLua.Push(L, (Object)obj);
			}
			else if (type.IsSubclassOf(typeof(TrackedReference)))
			{
				ToLua.Push(L, (TrackedReference)obj);
			}
			else if (type == typeof(LuaByteBuffer))
			{
				LuaByteBuffer luaByteBuffer = (LuaByteBuffer)obj;
				LuaDLL.lua_pushlstring(L, luaByteBuffer.buffer, luaByteBuffer.buffer.Length);
			}
			else if (type.IsSubclassOf(typeof(Delegate)))
			{
				ToLua.Push(L, (Delegate)obj);
			}
			else if (obj is IEnumerator)
			{
				ToLua.Push(L, (IEnumerator)obj);
			}
			else if (type == typeof(EventObject))
			{
				ToLua.Push(L, (EventObject)obj);
			}
			else if (type == ToLua.monoType)
			{
				ToLua.Push(L, (Type)obj);
			}
			else
			{
				ToLua.PushObject(L, obj);
			}
		}

		public static void SetBack(IntPtr L, int stackPos, object o)
		{
			int num = LuaDLL.tolua_rawnetobj(L, stackPos);
			ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
			if (num != -1)
			{
				objectTranslator.SetBack(num, o);
			}
		}

		public static int Destroy(IntPtr L)
		{
			int udata = LuaDLL.tolua_rawnetobj(L, 1);
			ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
			objectTranslator.Destroy(udata);
			return 0;
		}

		public static void CheckArgsCount(IntPtr L, string method, int count)
		{
			int num = LuaDLL.lua_gettop(L);
			if (num != count)
			{
				throw new LuaException(string.Format("no overload for method '{0}' takes '{1}' arguments", method, num), null, 1);
			}
		}

		public static void CheckArgsCount(IntPtr L, int count)
		{
			int num = LuaDLL.lua_gettop(L);
			if (num != count)
			{
				throw new LuaException(string.Format("no overload for method takes '{0}' arguments", num), null, 1);
			}
		}
	}
}
