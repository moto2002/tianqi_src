using LuaInterface;
using System;
using System.Globalization;
using System.Text;

public class System_StringWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(string), typeof(object), null);
		L.RegFunction("Equals", new LuaCSFunction(System_StringWrap.Equals));
		L.RegFunction("Clone", new LuaCSFunction(System_StringWrap.Clone));
		L.RegFunction("GetTypeCode", new LuaCSFunction(System_StringWrap.GetTypeCode));
		L.RegFunction("CopyTo", new LuaCSFunction(System_StringWrap.CopyTo));
		L.RegFunction("ToCharArray", new LuaCSFunction(System_StringWrap.ToCharArray));
		L.RegFunction("Split", new LuaCSFunction(System_StringWrap.Split));
		L.RegFunction("Substring", new LuaCSFunction(System_StringWrap.Substring));
		L.RegFunction("Trim", new LuaCSFunction(System_StringWrap.Trim));
		L.RegFunction("TrimStart", new LuaCSFunction(System_StringWrap.TrimStart));
		L.RegFunction("TrimEnd", new LuaCSFunction(System_StringWrap.TrimEnd));
		L.RegFunction("Compare", new LuaCSFunction(System_StringWrap.Compare));
		L.RegFunction("CompareTo", new LuaCSFunction(System_StringWrap.CompareTo));
		L.RegFunction("CompareOrdinal", new LuaCSFunction(System_StringWrap.CompareOrdinal));
		L.RegFunction("EndsWith", new LuaCSFunction(System_StringWrap.EndsWith));
		L.RegFunction("IndexOfAny", new LuaCSFunction(System_StringWrap.IndexOfAny));
		L.RegFunction("IndexOf", new LuaCSFunction(System_StringWrap.IndexOf));
		L.RegFunction("LastIndexOf", new LuaCSFunction(System_StringWrap.LastIndexOf));
		L.RegFunction("LastIndexOfAny", new LuaCSFunction(System_StringWrap.LastIndexOfAny));
		L.RegFunction("Contains", new LuaCSFunction(System_StringWrap.Contains));
		L.RegFunction("IsNullOrEmpty", new LuaCSFunction(System_StringWrap.IsNullOrEmpty));
		L.RegFunction("Normalize", new LuaCSFunction(System_StringWrap.Normalize));
		L.RegFunction("IsNormalized", new LuaCSFunction(System_StringWrap.IsNormalized));
		L.RegFunction("Remove", new LuaCSFunction(System_StringWrap.Remove));
		L.RegFunction("PadLeft", new LuaCSFunction(System_StringWrap.PadLeft));
		L.RegFunction("PadRight", new LuaCSFunction(System_StringWrap.PadRight));
		L.RegFunction("StartsWith", new LuaCSFunction(System_StringWrap.StartsWith));
		L.RegFunction("Replace", new LuaCSFunction(System_StringWrap.Replace));
		L.RegFunction("ToLower", new LuaCSFunction(System_StringWrap.ToLower));
		L.RegFunction("ToLowerInvariant", new LuaCSFunction(System_StringWrap.ToLowerInvariant));
		L.RegFunction("ToUpper", new LuaCSFunction(System_StringWrap.ToUpper));
		L.RegFunction("ToUpperInvariant", new LuaCSFunction(System_StringWrap.ToUpperInvariant));
		L.RegFunction("ToString", new LuaCSFunction(System_StringWrap.ToString));
		L.RegFunction("Format", new LuaCSFunction(System_StringWrap.Format));
		L.RegFunction("Copy", new LuaCSFunction(System_StringWrap.Copy));
		L.RegFunction("Concat", new LuaCSFunction(System_StringWrap.Concat));
		L.RegFunction("Insert", new LuaCSFunction(System_StringWrap.Insert));
		L.RegFunction("Intern", new LuaCSFunction(System_StringWrap.Intern));
		L.RegFunction("IsInterned", new LuaCSFunction(System_StringWrap.IsInterned));
		L.RegFunction("Join", new LuaCSFunction(System_StringWrap.Join));
		L.RegFunction("GetEnumerator", new LuaCSFunction(System_StringWrap.GetEnumerator));
		L.RegFunction("GetHashCode", new LuaCSFunction(System_StringWrap.GetHashCode));
		L.RegFunction("New", new LuaCSFunction(System_StringWrap._CreateSystem_String));
		L.RegFunction("__eq", new LuaCSFunction(System_StringWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(System_StringWrap.Lua_ToString));
		L.RegVar("Empty", new LuaCSFunction(System_StringWrap.get_Empty), null);
		L.RegVar("Length", new LuaCSFunction(System_StringWrap.get_Length), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSystem_String(IntPtr L)
	{
		int result;
		try
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TSTRING)
			{
				string o = LuaDLL.lua_tostring(L, 1);
				ToLua.PushObject(L, o);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to string's ctor method");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Equals(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				bool value = (text == null) ? (text2 == null) : text.Equals(text2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				string text3 = (string)ToLua.ToObject(L, 1);
				object obj = ToLua.ToVarObject(L, 2);
				bool value2 = (text3 == null) ? (obj == null) : text3.Equals(obj);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
			{
				string text4 = (string)ToLua.ToObject(L, 1);
				string text5 = ToLua.ToString(L, 2);
				StringComparison stringComparison = (int)ToLua.ToObject(L, 3);
				bool value3 = (text4 == null) ? (text5 == null) : text4.Equals(text5, stringComparison);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Equals");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Clone(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			object obj = text.Clone();
			ToLua.Push(L, obj);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTypeCode(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			TypeCode typeCode = text.GetTypeCode();
			ToLua.Push(L, typeCode);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CopyTo(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 5);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			int num = (int)LuaDLL.luaL_checknumber(L, 2);
			char[] array = ToLua.CheckCharBuffer(L, 3);
			int num2 = (int)LuaDLL.luaL_checknumber(L, 4);
			int num3 = (int)LuaDLL.luaL_checknumber(L, 5);
			text.CopyTo(num, array, num2, num3);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToCharArray(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				char[] array = text.ToCharArray();
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				char[] array2 = text2.ToCharArray(num2, num3);
				ToLua.Push(L, array2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.ToCharArray");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Split(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(StringSplitOptions)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				char[] array = ToLua.CheckCharBuffer(L, 2);
				StringSplitOptions stringSplitOptions = (int)ToLua.ToObject(L, 3);
				string[] array2 = text.Split(array, stringSplitOptions);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				char[] array3 = ToLua.CheckCharBuffer(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				string[] array4 = text2.Split(array3, num2);
				ToLua.Push(L, array4);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(StringSplitOptions)))
			{
				string text3 = (string)ToLua.ToObject(L, 1);
				string[] array5 = ToLua.CheckStringArray(L, 2);
				StringSplitOptions stringSplitOptions2 = (int)ToLua.ToObject(L, 3);
				string[] array6 = text3.Split(array5, stringSplitOptions2);
				ToLua.Push(L, array6);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(int), typeof(StringSplitOptions)))
			{
				string text4 = (string)ToLua.ToObject(L, 1);
				string[] array7 = ToLua.CheckStringArray(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				StringSplitOptions stringSplitOptions3 = (int)ToLua.ToObject(L, 4);
				string[] array8 = text4.Split(array7, num3, stringSplitOptions3);
				ToLua.Push(L, array8);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int), typeof(StringSplitOptions)))
			{
				string text5 = (string)ToLua.ToObject(L, 1);
				char[] array9 = ToLua.CheckCharBuffer(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				StringSplitOptions stringSplitOptions4 = (int)ToLua.ToObject(L, 4);
				string[] array10 = text5.Split(array9, num4, stringSplitOptions4);
				ToLua.Push(L, array10);
				result = 1;
			}
			else if (TypeChecker.CheckParamsType(L, typeof(char), 2, num - 1))
			{
				string text6 = (string)ToLua.ToObject(L, 1);
				char[] array11 = ToLua.ToParamsChar(L, 2, num - 1);
				string[] array12 = text6.Split(array11);
				ToLua.Push(L, array12);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Split");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Substring(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				string str = text.Substring(num2);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				string str2 = text2.Substring(num3, num4);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Substring");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Trim(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string str = text.Trim();
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (TypeChecker.CheckParamsType(L, typeof(char), 2, num - 1))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				char[] array = ToLua.ToParamsChar(L, 2, num - 1);
				string str2 = text2.Trim(array);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Trim");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TrimStart(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			char[] array = ToLua.CheckParamsChar(L, 2, num - 1);
			string str = text.TrimStart(array);
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TrimEnd(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			char[] array = ToLua.CheckParamsChar(L, 2, num - 1);
			string str = text.TrimEnd(array);
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Compare(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				string text2 = ToLua.ToString(L, 2);
				int n = string.Compare(text, text2);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
			{
				string text3 = ToLua.ToString(L, 1);
				string text4 = ToLua.ToString(L, 2);
				StringComparison stringComparison = (int)ToLua.ToObject(L, 3);
				int n2 = string.Compare(text3, text4, stringComparison);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool)))
			{
				string text5 = ToLua.ToString(L, 1);
				string text6 = ToLua.ToString(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				int n3 = string.Compare(text5, text6, flag);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(CultureInfo), typeof(CompareOptions)))
			{
				string text7 = ToLua.ToString(L, 1);
				string text8 = ToLua.ToString(L, 2);
				CultureInfo cultureInfo = (CultureInfo)ToLua.ToObject(L, 3);
				CompareOptions compareOptions = (int)ToLua.ToObject(L, 4);
				int n4 = string.Compare(text7, text8, cultureInfo, compareOptions);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool), typeof(CultureInfo)))
			{
				string text9 = ToLua.ToString(L, 1);
				string text10 = ToLua.ToString(L, 2);
				bool flag2 = LuaDLL.lua_toboolean(L, 3);
				CultureInfo cultureInfo2 = (CultureInfo)ToLua.ToObject(L, 4);
				int n5 = string.Compare(text9, text10, flag2, cultureInfo2);
				LuaDLL.lua_pushinteger(L, n5);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int)))
			{
				string text11 = ToLua.ToString(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				string text12 = ToLua.ToString(L, 3);
				int num3 = (int)LuaDLL.lua_tonumber(L, 4);
				int num4 = (int)LuaDLL.lua_tonumber(L, 5);
				int n6 = string.Compare(text11, num2, text12, num3, num4);
				LuaDLL.lua_pushinteger(L, n6);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(StringComparison)))
			{
				string text13 = ToLua.ToString(L, 1);
				int num5 = (int)LuaDLL.lua_tonumber(L, 2);
				string text14 = ToLua.ToString(L, 3);
				int num6 = (int)LuaDLL.lua_tonumber(L, 4);
				int num7 = (int)LuaDLL.lua_tonumber(L, 5);
				StringComparison stringComparison2 = (int)ToLua.ToObject(L, 6);
				int n7 = string.Compare(text13, num5, text14, num6, num7, stringComparison2);
				LuaDLL.lua_pushinteger(L, n7);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool)))
			{
				string text15 = ToLua.ToString(L, 1);
				int num8 = (int)LuaDLL.lua_tonumber(L, 2);
				string text16 = ToLua.ToString(L, 3);
				int num9 = (int)LuaDLL.lua_tonumber(L, 4);
				int num10 = (int)LuaDLL.lua_tonumber(L, 5);
				bool flag3 = LuaDLL.lua_toboolean(L, 6);
				int n8 = string.Compare(text15, num8, text16, num9, num10, flag3);
				LuaDLL.lua_pushinteger(L, n8);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(CultureInfo), typeof(CompareOptions)))
			{
				string text17 = ToLua.ToString(L, 1);
				int num11 = (int)LuaDLL.lua_tonumber(L, 2);
				string text18 = ToLua.ToString(L, 3);
				int num12 = (int)LuaDLL.lua_tonumber(L, 4);
				int num13 = (int)LuaDLL.lua_tonumber(L, 5);
				CultureInfo cultureInfo3 = (CultureInfo)ToLua.ToObject(L, 6);
				CompareOptions compareOptions2 = (int)ToLua.ToObject(L, 7);
				int n9 = string.Compare(text17, num11, text18, num12, num13, cultureInfo3, compareOptions2);
				LuaDLL.lua_pushinteger(L, n9);
				result = 1;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool), typeof(CultureInfo)))
			{
				string text19 = ToLua.ToString(L, 1);
				int num14 = (int)LuaDLL.lua_tonumber(L, 2);
				string text20 = ToLua.ToString(L, 3);
				int num15 = (int)LuaDLL.lua_tonumber(L, 4);
				int num16 = (int)LuaDLL.lua_tonumber(L, 5);
				bool flag4 = LuaDLL.lua_toboolean(L, 6);
				CultureInfo cultureInfo4 = (CultureInfo)ToLua.ToObject(L, 7);
				int n10 = string.Compare(text19, num14, text20, num15, num16, flag4, cultureInfo4);
				LuaDLL.lua_pushinteger(L, n10);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Compare");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareTo(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				int n = text.CompareTo(text2);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				string text3 = (string)ToLua.ToObject(L, 1);
				object obj = ToLua.ToVarObject(L, 2);
				int n2 = text3.CompareTo(obj);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.CompareTo");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareOrdinal(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				string text2 = ToLua.ToString(L, 2);
				int n = string.CompareOrdinal(text, text2);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int)))
			{
				string text3 = ToLua.ToString(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				string text4 = ToLua.ToString(L, 3);
				int num3 = (int)LuaDLL.lua_tonumber(L, 4);
				int num4 = (int)LuaDLL.lua_tonumber(L, 5);
				int n2 = string.CompareOrdinal(text3, num2, text4, num3, num4);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.CompareOrdinal");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EndsWith(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				bool value = text.EndsWith(text2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
			{
				string text3 = (string)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				StringComparison stringComparison = (int)ToLua.ToObject(L, 3);
				bool value2 = text3.EndsWith(text4, stringComparison);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool), typeof(CultureInfo)))
			{
				string text5 = (string)ToLua.ToObject(L, 1);
				string text6 = ToLua.ToString(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				CultureInfo cultureInfo = (CultureInfo)ToLua.ToObject(L, 4);
				bool value3 = text5.EndsWith(text6, flag, cultureInfo);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.EndsWith");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IndexOfAny(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[])))
			{
				string text = (string)ToLua.ToObject(L, 1);
				char[] array = ToLua.CheckCharBuffer(L, 2);
				int n = text.IndexOfAny(array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				char[] array2 = ToLua.CheckCharBuffer(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				int n2 = text2.IndexOfAny(array2, num2);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int), typeof(int)))
			{
				string text3 = (string)ToLua.ToObject(L, 1);
				char[] array3 = ToLua.CheckCharBuffer(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				int num4 = (int)LuaDLL.lua_tonumber(L, 4);
				int n3 = text3.IndexOfAny(array3, num3, num4);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.IndexOfAny");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IndexOf(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				char c = (char)LuaDLL.lua_tonumber(L, 2);
				int n = text.IndexOf(c);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				int n2 = text2.IndexOf(text3);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int)))
			{
				string text4 = (string)ToLua.ToObject(L, 1);
				string text5 = ToLua.ToString(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				int n3 = text4.IndexOf(text5, num2);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int)))
			{
				string text6 = (string)ToLua.ToObject(L, 1);
				char c2 = (char)LuaDLL.lua_tonumber(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				int n4 = text6.IndexOf(c2, num3);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
			{
				string text7 = (string)ToLua.ToObject(L, 1);
				string text8 = ToLua.ToString(L, 2);
				StringComparison stringComparison = (int)ToLua.ToObject(L, 3);
				int n5 = text7.IndexOf(text8, stringComparison);
				LuaDLL.lua_pushinteger(L, n5);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int)))
			{
				string text9 = (string)ToLua.ToObject(L, 1);
				string text10 = ToLua.ToString(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				int n6 = text9.IndexOf(text10, num4, num5);
				LuaDLL.lua_pushinteger(L, n6);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(StringComparison)))
			{
				string text11 = (string)ToLua.ToObject(L, 1);
				string text12 = ToLua.ToString(L, 2);
				int num6 = (int)LuaDLL.lua_tonumber(L, 3);
				StringComparison stringComparison2 = (int)ToLua.ToObject(L, 4);
				int n7 = text11.IndexOf(text12, num6, stringComparison2);
				LuaDLL.lua_pushinteger(L, n7);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int), typeof(int)))
			{
				string text13 = (string)ToLua.ToObject(L, 1);
				char c3 = (char)LuaDLL.lua_tonumber(L, 2);
				int num7 = (int)LuaDLL.lua_tonumber(L, 3);
				int num8 = (int)LuaDLL.lua_tonumber(L, 4);
				int n8 = text13.IndexOf(c3, num7, num8);
				LuaDLL.lua_pushinteger(L, n8);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int), typeof(StringComparison)))
			{
				string text14 = (string)ToLua.ToObject(L, 1);
				string text15 = ToLua.ToString(L, 2);
				int num9 = (int)LuaDLL.lua_tonumber(L, 3);
				int num10 = (int)LuaDLL.lua_tonumber(L, 4);
				StringComparison stringComparison3 = (int)ToLua.ToObject(L, 5);
				int n9 = text14.IndexOf(text15, num9, num10, stringComparison3);
				LuaDLL.lua_pushinteger(L, n9);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.IndexOf");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LastIndexOf(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				char c = (char)LuaDLL.lua_tonumber(L, 2);
				int n = text.LastIndexOf(c);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				string text3 = ToLua.ToString(L, 2);
				int n2 = text2.LastIndexOf(text3);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int)))
			{
				string text4 = (string)ToLua.ToObject(L, 1);
				string text5 = ToLua.ToString(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				int n3 = text4.LastIndexOf(text5, num2);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int)))
			{
				string text6 = (string)ToLua.ToObject(L, 1);
				char c2 = (char)LuaDLL.lua_tonumber(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				int n4 = text6.LastIndexOf(c2, num3);
				LuaDLL.lua_pushinteger(L, n4);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
			{
				string text7 = (string)ToLua.ToObject(L, 1);
				string text8 = ToLua.ToString(L, 2);
				StringComparison stringComparison = (int)ToLua.ToObject(L, 3);
				int n5 = text7.LastIndexOf(text8, stringComparison);
				LuaDLL.lua_pushinteger(L, n5);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int)))
			{
				string text9 = (string)ToLua.ToObject(L, 1);
				string text10 = ToLua.ToString(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				int n6 = text9.LastIndexOf(text10, num4, num5);
				LuaDLL.lua_pushinteger(L, n6);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(StringComparison)))
			{
				string text11 = (string)ToLua.ToObject(L, 1);
				string text12 = ToLua.ToString(L, 2);
				int num6 = (int)LuaDLL.lua_tonumber(L, 3);
				StringComparison stringComparison2 = (int)ToLua.ToObject(L, 4);
				int n7 = text11.LastIndexOf(text12, num6, stringComparison2);
				LuaDLL.lua_pushinteger(L, n7);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int), typeof(int)))
			{
				string text13 = (string)ToLua.ToObject(L, 1);
				char c3 = (char)LuaDLL.lua_tonumber(L, 2);
				int num7 = (int)LuaDLL.lua_tonumber(L, 3);
				int num8 = (int)LuaDLL.lua_tonumber(L, 4);
				int n8 = text13.LastIndexOf(c3, num7, num8);
				LuaDLL.lua_pushinteger(L, n8);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int), typeof(StringComparison)))
			{
				string text14 = (string)ToLua.ToObject(L, 1);
				string text15 = ToLua.ToString(L, 2);
				int num9 = (int)LuaDLL.lua_tonumber(L, 3);
				int num10 = (int)LuaDLL.lua_tonumber(L, 4);
				StringComparison stringComparison3 = (int)ToLua.ToObject(L, 5);
				int n9 = text14.LastIndexOf(text15, num9, num10, stringComparison3);
				LuaDLL.lua_pushinteger(L, n9);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.LastIndexOf");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LastIndexOfAny(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[])))
			{
				string text = (string)ToLua.ToObject(L, 1);
				char[] array = ToLua.CheckCharBuffer(L, 2);
				int n = text.LastIndexOfAny(array);
				LuaDLL.lua_pushinteger(L, n);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				char[] array2 = ToLua.CheckCharBuffer(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				int n2 = text2.LastIndexOfAny(array2, num2);
				LuaDLL.lua_pushinteger(L, n2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int), typeof(int)))
			{
				string text3 = (string)ToLua.ToObject(L, 1);
				char[] array3 = ToLua.CheckCharBuffer(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				int num4 = (int)LuaDLL.lua_tonumber(L, 4);
				int n3 = text3.LastIndexOfAny(array3, num3, num4);
				LuaDLL.lua_pushinteger(L, n3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.LastIndexOfAny");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Contains(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			string text2 = ToLua.CheckString(L, 2);
			bool value = text.Contains(text2);
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
	private static int IsNullOrEmpty(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			bool value = string.IsNullOrEmpty(text);
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
	private static int Normalize(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string str = text.Normalize();
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(NormalizationForm)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				NormalizationForm normalizationForm = (int)ToLua.ToObject(L, 2);
				string str2 = text2.Normalize(normalizationForm);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Normalize");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsNormalized(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				bool value = text.IsNormalized();
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(NormalizationForm)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				NormalizationForm normalizationForm = (int)ToLua.ToObject(L, 2);
				bool value2 = text2.IsNormalized(normalizationForm);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.IsNormalized");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Remove(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				string str = text.Remove(num2);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				string str2 = text2.Remove(num3, num4);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Remove");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PadLeft(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				string str = text.PadLeft(num2);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(char)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				char c = (char)LuaDLL.lua_tonumber(L, 3);
				string str2 = text2.PadLeft(num3, c);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.PadLeft");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PadRight(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				string str = text.PadRight(num2);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int), typeof(char)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				char c = (char)LuaDLL.lua_tonumber(L, 3);
				string str2 = text2.PadRight(num3, c);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.PadRight");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartsWith(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				bool value = text.StartsWith(text2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
			{
				string text3 = (string)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				StringComparison stringComparison = (int)ToLua.ToObject(L, 3);
				bool value2 = text3.StartsWith(text4, stringComparison);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool), typeof(CultureInfo)))
			{
				string text5 = (string)ToLua.ToObject(L, 1);
				string text6 = ToLua.ToString(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				CultureInfo cultureInfo = (CultureInfo)ToLua.ToObject(L, 4);
				bool value3 = text5.StartsWith(text6, flag, cultureInfo);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.StartsWith");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Replace(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				string text3 = ToLua.ToString(L, 3);
				string str = text.Replace(text2, text3);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(char), typeof(char)))
			{
				string text4 = (string)ToLua.ToObject(L, 1);
				char c = (char)LuaDLL.lua_tonumber(L, 2);
				char c2 = (char)LuaDLL.lua_tonumber(L, 3);
				string str2 = text4.Replace(c, c2);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Replace");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToLower(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string str = text.ToLower();
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(CultureInfo)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				CultureInfo cultureInfo = (CultureInfo)ToLua.ToObject(L, 2);
				string str2 = text2.ToLower(cultureInfo);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.ToLower");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToLowerInvariant(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			string str = text.ToLowerInvariant();
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToUpper(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string str = text.ToUpper();
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(CultureInfo)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				CultureInfo cultureInfo = (CultureInfo)ToLua.ToObject(L, 2);
				string str2 = text2.ToUpper(cultureInfo);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.ToUpper");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToUpperInvariant(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			string str = text.ToUpperInvariant();
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToString(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = (string)ToLua.ToObject(L, 1);
				string str = text.ToString();
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(IFormatProvider)))
			{
				string text2 = (string)ToLua.ToObject(L, 1);
				IFormatProvider formatProvider = (IFormatProvider)ToLua.ToObject(L, 2);
				string str2 = text2.ToString(formatProvider);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.ToString");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Format(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				string text = ToLua.ToString(L, 1);
				object obj = ToLua.ToVarObject(L, 2);
				string str = string.Format(text, obj);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object)))
			{
				string text2 = ToLua.ToString(L, 1);
				object obj2 = ToLua.ToVarObject(L, 2);
				object obj3 = ToLua.ToVarObject(L, 3);
				string str2 = string.Format(text2, obj2, obj3);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object), typeof(object)))
			{
				string text3 = ToLua.ToString(L, 1);
				object obj4 = ToLua.ToVarObject(L, 2);
				object obj5 = ToLua.ToVarObject(L, 3);
				object obj6 = ToLua.ToVarObject(L, 4);
				string str3 = string.Format(text3, obj4, obj5, obj6);
				LuaDLL.lua_pushstring(L, str3);
				result = 1;
			}
			else if (TypeChecker.CheckTypes(L, 1, typeof(IFormatProvider), typeof(string)) && TypeChecker.CheckParamsType(L, typeof(object), 3, num - 2))
			{
				IFormatProvider formatProvider = (IFormatProvider)ToLua.ToObject(L, 1);
				string text4 = ToLua.ToString(L, 2);
				object[] array = ToLua.ToParamsObject(L, 3, num - 2);
				string str4 = string.Format(formatProvider, text4, array);
				LuaDLL.lua_pushstring(L, str4);
				result = 1;
			}
			else if (TypeChecker.CheckTypes(L, 1, typeof(string)) && TypeChecker.CheckParamsType(L, typeof(object), 2, num - 1))
			{
				string text5 = ToLua.ToString(L, 1);
				object[] array2 = ToLua.ToParamsObject(L, 2, num - 1);
				string str5 = string.Format(text5, array2);
				LuaDLL.lua_pushstring(L, str5);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Format");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Copy(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			string str = string.Copy(text);
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Concat(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(object)))
			{
				object obj = ToLua.ToVarObject(L, 1);
				string str = string.Concat(obj);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				string text2 = ToLua.ToString(L, 2);
				string str2 = text + text2;
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(object), typeof(object)))
			{
				object obj2 = ToLua.ToVarObject(L, 1);
				object obj3 = ToLua.ToVarObject(L, 2);
				string str3 = obj2 + obj3;
				LuaDLL.lua_pushstring(L, str3);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string)))
			{
				string text3 = ToLua.ToString(L, 1);
				string text4 = ToLua.ToString(L, 2);
				string text5 = ToLua.ToString(L, 3);
				string str4 = text3 + text4 + text5;
				LuaDLL.lua_pushstring(L, str4);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(object), typeof(object), typeof(object)))
			{
				object obj4 = ToLua.ToVarObject(L, 1);
				object obj5 = ToLua.ToVarObject(L, 2);
				object obj6 = ToLua.ToVarObject(L, 3);
				string str5 = obj4 + obj5 + obj6;
				LuaDLL.lua_pushstring(L, str5);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string), typeof(string)))
			{
				string text6 = ToLua.ToString(L, 1);
				string text7 = ToLua.ToString(L, 2);
				string text8 = ToLua.ToString(L, 3);
				string text9 = ToLua.ToString(L, 4);
				string str6 = text6 + text7 + text8 + text9;
				LuaDLL.lua_pushstring(L, str6);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(object), typeof(object), typeof(object), typeof(object)))
			{
				object obj7 = ToLua.ToVarObject(L, 1);
				object obj8 = ToLua.ToVarObject(L, 2);
				object obj9 = ToLua.ToVarObject(L, 3);
				object obj10 = ToLua.ToVarObject(L, 4);
				string str7 = string.Concat(new object[]
				{
					obj7,
					obj8,
					obj9,
					obj10
				});
				LuaDLL.lua_pushstring(L, str7);
				result = 1;
			}
			else if (TypeChecker.CheckParamsType(L, typeof(string), 1, num))
			{
				string[] array = ToLua.ToParamsString(L, 1, num);
				string str8 = string.Concat(array);
				LuaDLL.lua_pushstring(L, str8);
				result = 1;
			}
			else if (TypeChecker.CheckParamsType(L, typeof(object), 1, num))
			{
				object[] array2 = ToLua.ToParamsObject(L, 1, num);
				string str9 = string.Concat(array2);
				LuaDLL.lua_pushstring(L, str9);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Concat");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Insert(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			int num = (int)LuaDLL.luaL_checknumber(L, 2);
			string text2 = ToLua.CheckString(L, 3);
			string str = text.Insert(num, text2);
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Intern(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			string str = string.Intern(text);
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsInterned(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			string str = string.IsInterned(text);
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Join(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string[])))
			{
				string text = ToLua.ToString(L, 1);
				string[] array = ToLua.CheckStringArray(L, 2);
				string str = string.Join(text, array);
				LuaDLL.lua_pushstring(L, str);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(int), typeof(int)))
			{
				string text2 = ToLua.ToString(L, 1);
				string[] array2 = ToLua.CheckStringArray(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				int num3 = (int)LuaDLL.lua_tonumber(L, 4);
				string str2 = string.Join(text2, array2, num2, num3);
				LuaDLL.lua_pushstring(L, str2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: System.String.Join");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetEnumerator(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			CharEnumerator enumerator = text.GetEnumerator();
			ToLua.Push(L, enumerator);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetHashCode(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = (string)ToLua.CheckObject(L, 1, typeof(string));
			int hashCode = text.GetHashCode();
			LuaDLL.lua_pushinteger(L, hashCode);
			result = 1;
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
			string text = ToLua.ToString(L, 1);
			string text2 = ToLua.ToString(L, 2);
			bool value = text == text2;
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
	private static int get_Empty(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, string.Empty);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Length(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			string text = (string)obj;
			int length = text.get_Length();
			LuaDLL.lua_pushinteger(L, length);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index Length on a nil value");
		}
		return result;
	}
}
