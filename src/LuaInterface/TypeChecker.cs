using System;
using UnityEngine;

namespace LuaInterface
{
	public static class TypeChecker
	{
		public static bool IsValueType(Type t)
		{
			return !t.get_IsEnum() && t.get_IsValueType();
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0)
		{
			return TypeChecker.CheckType(L, type0, begin);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2) && TypeChecker.CheckType(L, type3, begin + 3);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2) && TypeChecker.CheckType(L, type3, begin + 3) && TypeChecker.CheckType(L, type4, begin + 4);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2) && TypeChecker.CheckType(L, type3, begin + 3) && TypeChecker.CheckType(L, type4, begin + 4) && TypeChecker.CheckType(L, type5, begin + 5);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2) && TypeChecker.CheckType(L, type3, begin + 3) && TypeChecker.CheckType(L, type4, begin + 4) && TypeChecker.CheckType(L, type5, begin + 5) && TypeChecker.CheckType(L, type6, begin + 6);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6, Type type7)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2) && TypeChecker.CheckType(L, type3, begin + 3) && TypeChecker.CheckType(L, type4, begin + 4) && TypeChecker.CheckType(L, type5, begin + 5) && TypeChecker.CheckType(L, type6, begin + 6) && TypeChecker.CheckType(L, type7, begin + 7);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6, Type type7, Type type8)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2) && TypeChecker.CheckType(L, type3, begin + 3) && TypeChecker.CheckType(L, type4, begin + 4) && TypeChecker.CheckType(L, type5, begin + 5) && TypeChecker.CheckType(L, type6, begin + 6) && TypeChecker.CheckType(L, type7, begin + 7) && TypeChecker.CheckType(L, type8, begin + 8);
		}

		public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6, Type type7, Type type8, Type type9)
		{
			return TypeChecker.CheckType(L, type0, begin) && TypeChecker.CheckType(L, type1, begin + 1) && TypeChecker.CheckType(L, type2, begin + 2) && TypeChecker.CheckType(L, type3, begin + 3) && TypeChecker.CheckType(L, type4, begin + 4) && TypeChecker.CheckType(L, type5, begin + 5) && TypeChecker.CheckType(L, type6, begin + 6) && TypeChecker.CheckType(L, type7, begin + 7) && TypeChecker.CheckType(L, type8, begin + 8) && TypeChecker.CheckType(L, type9, begin + 9);
		}

		public static bool CheckTypes(IntPtr L, params Type[] types)
		{
			for (int i = 0; i < types.Length; i++)
			{
				if (!TypeChecker.CheckType(L, types[i], i + 1))
				{
					return false;
				}
			}
			return true;
		}

		public static bool CheckParamsType(IntPtr L, Type t, int begin, int count)
		{
			if (t == typeof(object))
			{
				return true;
			}
			for (int i = 0; i < count; i++)
			{
				if (!TypeChecker.CheckType(L, t, i + begin))
				{
					return false;
				}
			}
			return true;
		}

		public static bool CheckType(IntPtr L, Type t, int pos)
		{
			if (t == typeof(object))
			{
				return true;
			}
			switch (LuaDLL.lua_type(L, pos))
			{
			case LuaTypes.LUA_TNIL:
				return t == null || t.get_IsEnum() || !t.get_IsValueType();
			case LuaTypes.LUA_TBOOLEAN:
				return t == typeof(bool);
			case LuaTypes.LUA_TLIGHTUSERDATA:
				return t == typeof(IntPtr);
			case LuaTypes.LUA_TNUMBER:
				return t.get_IsPrimitive();
			case LuaTypes.LUA_TSTRING:
				return t == typeof(string) || t == typeof(byte[]) || t == typeof(char[]);
			case LuaTypes.LUA_TTABLE:
				return TypeChecker.lua_isusertable(L, t, pos);
			case LuaTypes.LUA_TFUNCTION:
				return t == typeof(LuaFunction);
			case LuaTypes.LUA_TUSERDATA:
				return TypeChecker.IsMatchUserData(L, t, pos);
			default:
				throw new LuaException("undefined type to check" + LuaDLL.luaL_typename(L, pos), null, 1);
			}
		}

		private static bool IsMatchUserData(IntPtr L, Type t, int pos)
		{
			if (t == typeof(LuaInteger64))
			{
				return LuaDLL.tolua_isint64(L, pos);
			}
			int num = LuaDLL.tolua_rawnetobj(L, pos);
			if (num != -1)
			{
				ObjectTranslator objectTranslator = ObjectTranslator.Get(L);
				object @object = objectTranslator.GetObject(num);
				if (@object == null)
				{
					return !t.get_IsValueType();
				}
				Type type = @object.GetType();
				if (t == type || t.IsAssignableFrom(type))
				{
					return true;
				}
			}
			return false;
		}

		private static bool lua_isusertable(IntPtr L, Type t, int pos)
		{
			if (t.get_IsArray())
			{
				return true;
			}
			if (t == typeof(LuaTable))
			{
				return true;
			}
			if (t.get_IsValueType())
			{
				switch (LuaDLL.tolua_getvaluetype(L, pos))
				{
				case LuaValueType.Vector3:
					return typeof(Vector3) == t;
				case LuaValueType.Quaternion:
					return typeof(Quaternion) == t;
				case LuaValueType.Vector2:
					return typeof(Vector2) == t;
				case LuaValueType.Color:
					return typeof(Color) == t;
				case LuaValueType.Vector4:
					return typeof(Vector4) == t;
				case LuaValueType.Ray:
					return typeof(Ray) == t;
				case LuaValueType.Bounds:
					return typeof(Bounds) == t;
				case LuaValueType.Touch:
					return typeof(Touch) == t;
				case LuaValueType.LayerMask:
					return typeof(LayerMask) == t;
				case LuaValueType.RaycastHit:
					return typeof(RaycastHit) == t;
				}
			}
			else if (LuaDLL.tolua_isvptrtable(L, pos))
			{
				return TypeChecker.IsMatchUserData(L, t, pos);
			}
			return false;
		}
	}
}
