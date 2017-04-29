using LuaInterface;
using System;

public class LuaFramework_AppConstWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AppConst), typeof(object), null);
		L.RegFunction("New", new LuaCSFunction(LuaFramework_AppConstWrap._CreateLuaFramework_AppConst));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_AppConstWrap.Lua_ToString));
		L.RegConstant("UpdateMode", 1.0);
		L.RegConstant("AsyncLoad", 1.0);
		L.RegConstant("LuaByteMode", 0.0);
		L.RegConstant("LuaBundleMode", 0.0);
		L.RegConstant("TimerInterval", 1.0);
		L.RegConstant("GameFrameRate", 30.0);
		L.RegVar("AppName", new LuaCSFunction(LuaFramework_AppConstWrap.get_AppName), null);
		L.RegVar("LuaTempDir", new LuaCSFunction(LuaFramework_AppConstWrap.get_LuaTempDir), null);
		L.RegVar("AppPrefix", new LuaCSFunction(LuaFramework_AppConstWrap.get_AppPrefix), null);
		L.RegVar("ExtName", new LuaCSFunction(LuaFramework_AppConstWrap.get_ExtName), null);
		L.RegVar("AssetDir", new LuaCSFunction(LuaFramework_AppConstWrap.get_AssetDir), null);
		L.RegVar("WebUrl", new LuaCSFunction(LuaFramework_AppConstWrap.get_WebUrl), null);
		L.RegVar("CodeName", new LuaCSFunction(LuaFramework_AppConstWrap.get_CodeName), null);
		L.RegVar("UserId", new LuaCSFunction(LuaFramework_AppConstWrap.get_UserId), new LuaCSFunction(LuaFramework_AppConstWrap.set_UserId));
		L.RegVar("SocketPort", new LuaCSFunction(LuaFramework_AppConstWrap.get_SocketPort), new LuaCSFunction(LuaFramework_AppConstWrap.set_SocketPort));
		L.RegVar("SocketAddress", new LuaCSFunction(LuaFramework_AppConstWrap.get_SocketAddress), new LuaCSFunction(LuaFramework_AppConstWrap.set_SocketAddress));
		L.RegVar("FrameworkRoot", new LuaCSFunction(LuaFramework_AppConstWrap.get_FrameworkRoot), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateLuaFramework_AppConst(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				AppConst o = new AppConst();
				ToLua.PushObject(L, o);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: AppConst.New");
			}
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
	private static int get_AppName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, "LuaFramework");
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LuaTempDir(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, "Lua/");
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_AppPrefix(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, "LuaFramework_");
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ExtName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, ".unity3d");
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_AssetDir(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, "StreamingAssets");
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_WebUrl(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, AppConst.WebUrl);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_CodeName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, "ProX.dll");
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_UserId(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, AppConst.UserId);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_SocketPort(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, AppConst.SocketPort);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_SocketAddress(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, AppConst.SocketAddress);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_FrameworkRoot(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, AppConst.FrameworkRoot);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_UserId(IntPtr L)
	{
		int result;
		try
		{
			string userId = ToLua.CheckString(L, 2);
			AppConst.UserId = userId;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_SocketPort(IntPtr L)
	{
		int result;
		try
		{
			int socketPort = (int)LuaDLL.luaL_checknumber(L, 2);
			AppConst.SocketPort = socketPort;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_SocketAddress(IntPtr L)
	{
		int result;
		try
		{
			string socketAddress = ToLua.CheckString(L, 2);
			AppConst.SocketAddress = socketAddress;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
