using LuaInterface;
using System;

public class DebuggerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Debugger");
		L.RegFunction("Log", new LuaCSFunction(DebuggerWrap.Log));
		L.RegFunction("LogWarning", new LuaCSFunction(DebuggerWrap.LogWarning));
		L.RegFunction("LogError", new LuaCSFunction(DebuggerWrap.LogError));
		L.RegFunction("LogException", new LuaCSFunction(DebuggerWrap.LogException));
		L.RegVar("useLog", new LuaCSFunction(DebuggerWrap.get_useLog), new LuaCSFunction(DebuggerWrap.set_useLog));
		L.RegVar("threadStack", new LuaCSFunction(DebuggerWrap.get_threadStack), new LuaCSFunction(DebuggerWrap.set_threadStack));
		L.RegVar("logger", new LuaCSFunction(DebuggerWrap.get_logger), new LuaCSFunction(DebuggerWrap.set_logger));
		L.EndStaticLibs();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Log(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string str = ToLua.ToString(L, 1);
				Debugger.Log(str);
				result = 0;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(object)))
			{
				object message = ToLua.ToVarObject(L, 1);
				Debugger.Log(message);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				string str2 = ToLua.ToString(L, 1);
				object arg = ToLua.ToVarObject(L, 2);
				Debugger.Log(str2, arg);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object)))
			{
				string str3 = ToLua.ToString(L, 1);
				object arg2 = ToLua.ToVarObject(L, 2);
				object arg3 = ToLua.ToVarObject(L, 3);
				Debugger.Log(str3, arg2, arg3);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object), typeof(object)))
			{
				string str4 = ToLua.ToString(L, 1);
				object arg4 = ToLua.ToVarObject(L, 2);
				object arg5 = ToLua.ToVarObject(L, 3);
				object arg6 = ToLua.ToVarObject(L, 4);
				Debugger.Log(str4, arg4, arg5, arg6);
				result = 0;
			}
			else if (TypeChecker.CheckTypes(L, 1, typeof(string)) && TypeChecker.CheckParamsType(L, typeof(object), 2, num - 1))
			{
				string str5 = ToLua.ToString(L, 1);
				object[] param = ToLua.ToParamsObject(L, 2, num - 1);
				Debugger.Log(str5, param);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: Debugger.Log");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LogWarning(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string str = ToLua.ToString(L, 1);
				Debugger.LogWarning(str);
				result = 0;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(object)))
			{
				object message = ToLua.ToVarObject(L, 1);
				Debugger.LogWarning(message);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				string str2 = ToLua.ToString(L, 1);
				object arg = ToLua.ToVarObject(L, 2);
				Debugger.LogWarning(str2, arg);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object)))
			{
				string str3 = ToLua.ToString(L, 1);
				object arg2 = ToLua.ToVarObject(L, 2);
				object arg3 = ToLua.ToVarObject(L, 3);
				Debugger.LogWarning(str3, arg2, arg3);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object), typeof(object)))
			{
				string str4 = ToLua.ToString(L, 1);
				object arg4 = ToLua.ToVarObject(L, 2);
				object arg5 = ToLua.ToVarObject(L, 3);
				object arg6 = ToLua.ToVarObject(L, 4);
				Debugger.LogWarning(str4, arg4, arg5, arg6);
				result = 0;
			}
			else if (TypeChecker.CheckTypes(L, 1, typeof(string)) && TypeChecker.CheckParamsType(L, typeof(object), 2, num - 1))
			{
				string str5 = ToLua.ToString(L, 1);
				object[] param = ToLua.ToParamsObject(L, 2, num - 1);
				Debugger.LogWarning(str5, param);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: Debugger.LogWarning");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LogError(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string str = ToLua.ToString(L, 1);
				Debugger.LogError(str);
				result = 0;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(object)))
			{
				object message = ToLua.ToVarObject(L, 1);
				Debugger.LogError(message);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				string str2 = ToLua.ToString(L, 1);
				object arg = ToLua.ToVarObject(L, 2);
				Debugger.LogError(str2, arg);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object)))
			{
				string str3 = ToLua.ToString(L, 1);
				object arg2 = ToLua.ToVarObject(L, 2);
				object arg3 = ToLua.ToVarObject(L, 3);
				Debugger.LogError(str3, arg2, arg3);
				result = 0;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object), typeof(object)))
			{
				string str4 = ToLua.ToString(L, 1);
				object arg4 = ToLua.ToVarObject(L, 2);
				object arg5 = ToLua.ToVarObject(L, 3);
				object arg6 = ToLua.ToVarObject(L, 4);
				Debugger.LogError(str4, arg4, arg5, arg6);
				result = 0;
			}
			else if (TypeChecker.CheckTypes(L, 1, typeof(string)) && TypeChecker.CheckParamsType(L, typeof(object), 2, num - 1))
			{
				string str5 = ToLua.ToString(L, 1);
				object[] param = ToLua.ToParamsObject(L, 2, num - 1);
				Debugger.LogError(str5, param);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: Debugger.LogError");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LogException(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Exception)))
			{
				Exception e = (Exception)ToLua.ToObject(L, 1);
				Debugger.LogException(e);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(Exception)))
			{
				string str = ToLua.ToString(L, 1);
				Exception e2 = (Exception)ToLua.ToObject(L, 2);
				Debugger.LogException(str, e2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: Debugger.LogException");
			}
		}
		catch (Exception e3)
		{
			result = LuaDLL.toluaL_exception(L, e3, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useLog(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Debugger.useLog);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_threadStack(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Debugger.threadStack);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_logger(IntPtr L)
	{
		ToLua.PushObject(L, Debugger.logger);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useLog(IntPtr L)
	{
		int result;
		try
		{
			bool useLog = LuaDLL.luaL_checkboolean(L, 2);
			Debugger.useLog = useLog;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_threadStack(IntPtr L)
	{
		int result;
		try
		{
			string threadStack = ToLua.CheckString(L, 2);
			Debugger.threadStack = threadStack;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_logger(IntPtr L)
	{
		int result;
		try
		{
			ILogger logger = (ILogger)ToLua.CheckObject(L, 2, typeof(ILogger));
			Debugger.logger = logger;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
