using LuaFramework;
using LuaInterface;
using System;

public class LuaFramework_LuaHelperWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("LuaHelper");
		L.RegFunction("GetType", new LuaCSFunction(LuaFramework_LuaHelperWrap.GetType));
		L.RegFunction("GetPanelManager", new LuaCSFunction(LuaFramework_LuaHelperWrap.GetPanelManager));
		L.RegFunction("GetNetManager", new LuaCSFunction(LuaFramework_LuaHelperWrap.GetNetManager));
		L.RegFunction("GetSoundManager", new LuaCSFunction(LuaFramework_LuaHelperWrap.GetSoundManager));
		L.RegFunction("OnCallLuaFunc", new LuaCSFunction(LuaFramework_LuaHelperWrap.OnCallLuaFunc));
		L.RegFunction("OnJsonCallFunc", new LuaCSFunction(LuaFramework_LuaHelperWrap.OnJsonCallFunc));
		L.EndStaticLibs();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetType(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string classname = ToLua.CheckString(L, 1);
			Type type = LuaHelper.GetType(classname);
			ToLua.Push(L, type);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPanelManager(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			PanelManager panelManager = LuaHelper.GetPanelManager();
			ToLua.Push(L, panelManager);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetNetManager(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			NetworkManager netManager = LuaHelper.GetNetManager();
			ToLua.Push(L, netManager);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSoundManager(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			SoundManager soundManager = LuaHelper.GetSoundManager();
			ToLua.Push(L, soundManager);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnCallLuaFunc(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaByteBuffer data = new LuaByteBuffer(ToLua.CheckByteBuffer(L, 1));
			LuaFunction func = ToLua.CheckLuaFunction(L, 2);
			LuaHelper.OnCallLuaFunc(data, func);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnJsonCallFunc(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string data = ToLua.CheckString(L, 1);
			LuaFunction func = ToLua.CheckLuaFunction(L, 2);
			LuaHelper.OnJsonCallFunc(data, func);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
