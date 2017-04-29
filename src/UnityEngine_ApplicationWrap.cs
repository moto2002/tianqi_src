using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_ApplicationWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Application");
		L.RegFunction("Quit", new LuaCSFunction(UnityEngine_ApplicationWrap.Quit));
		L.RegFunction("CancelQuit", new LuaCSFunction(UnityEngine_ApplicationWrap.CancelQuit));
		L.RegFunction("GetStreamProgressForLevel", new LuaCSFunction(UnityEngine_ApplicationWrap.GetStreamProgressForLevel));
		L.RegFunction("CanStreamedLevelBeLoaded", new LuaCSFunction(UnityEngine_ApplicationWrap.CanStreamedLevelBeLoaded));
		L.RegFunction("CaptureScreenshot", new LuaCSFunction(UnityEngine_ApplicationWrap.CaptureScreenshot));
		L.RegFunction("HasProLicense", new LuaCSFunction(UnityEngine_ApplicationWrap.HasProLicense));
		L.RegFunction("ExternalCall", new LuaCSFunction(UnityEngine_ApplicationWrap.ExternalCall));
		L.RegFunction("RequestAdvertisingIdentifierAsync", new LuaCSFunction(UnityEngine_ApplicationWrap.RequestAdvertisingIdentifierAsync));
		L.RegFunction("OpenURL", new LuaCSFunction(UnityEngine_ApplicationWrap.OpenURL));
		L.RegFunction("RequestUserAuthorization", new LuaCSFunction(UnityEngine_ApplicationWrap.RequestUserAuthorization));
		L.RegFunction("HasUserAuthorization", new LuaCSFunction(UnityEngine_ApplicationWrap.HasUserAuthorization));
		L.RegVar("streamedBytes", new LuaCSFunction(UnityEngine_ApplicationWrap.get_streamedBytes), null);
		L.RegVar("isPlaying", new LuaCSFunction(UnityEngine_ApplicationWrap.get_isPlaying), null);
		L.RegVar("isEditor", new LuaCSFunction(UnityEngine_ApplicationWrap.get_isEditor), null);
		L.RegVar("isWebPlayer", new LuaCSFunction(UnityEngine_ApplicationWrap.get_isWebPlayer), null);
		L.RegVar("platform", new LuaCSFunction(UnityEngine_ApplicationWrap.get_platform), null);
		L.RegVar("isMobilePlatform", new LuaCSFunction(UnityEngine_ApplicationWrap.get_isMobilePlatform), null);
		L.RegVar("isConsolePlatform", new LuaCSFunction(UnityEngine_ApplicationWrap.get_isConsolePlatform), null);
		L.RegVar("runInBackground", new LuaCSFunction(UnityEngine_ApplicationWrap.get_runInBackground), new LuaCSFunction(UnityEngine_ApplicationWrap.set_runInBackground));
		L.RegVar("dataPath", new LuaCSFunction(UnityEngine_ApplicationWrap.get_dataPath), null);
		L.RegVar("streamingAssetsPath", new LuaCSFunction(UnityEngine_ApplicationWrap.get_streamingAssetsPath), null);
		L.RegVar("persistentDataPath", new LuaCSFunction(UnityEngine_ApplicationWrap.get_persistentDataPath), null);
		L.RegVar("temporaryCachePath", new LuaCSFunction(UnityEngine_ApplicationWrap.get_temporaryCachePath), null);
		L.RegVar("srcValue", new LuaCSFunction(UnityEngine_ApplicationWrap.get_srcValue), null);
		L.RegVar("absoluteURL", new LuaCSFunction(UnityEngine_ApplicationWrap.get_absoluteURL), null);
		L.RegVar("unityVersion", new LuaCSFunction(UnityEngine_ApplicationWrap.get_unityVersion), null);
		L.RegVar("version", new LuaCSFunction(UnityEngine_ApplicationWrap.get_version), null);
		L.RegVar("bundleIdentifier", new LuaCSFunction(UnityEngine_ApplicationWrap.get_bundleIdentifier), null);
		L.RegVar("installMode", new LuaCSFunction(UnityEngine_ApplicationWrap.get_installMode), null);
		L.RegVar("sandboxType", new LuaCSFunction(UnityEngine_ApplicationWrap.get_sandboxType), null);
		L.RegVar("productName", new LuaCSFunction(UnityEngine_ApplicationWrap.get_productName), null);
		L.RegVar("companyName", new LuaCSFunction(UnityEngine_ApplicationWrap.get_companyName), null);
		L.RegVar("cloudProjectId", new LuaCSFunction(UnityEngine_ApplicationWrap.get_cloudProjectId), null);
		L.RegVar("webSecurityEnabled", new LuaCSFunction(UnityEngine_ApplicationWrap.get_webSecurityEnabled), null);
		L.RegVar("webSecurityHostUrl", new LuaCSFunction(UnityEngine_ApplicationWrap.get_webSecurityHostUrl), null);
		L.RegVar("targetFrameRate", new LuaCSFunction(UnityEngine_ApplicationWrap.get_targetFrameRate), new LuaCSFunction(UnityEngine_ApplicationWrap.set_targetFrameRate));
		L.RegVar("systemLanguage", new LuaCSFunction(UnityEngine_ApplicationWrap.get_systemLanguage), null);
		L.RegVar("stackTraceLogType", new LuaCSFunction(UnityEngine_ApplicationWrap.get_stackTraceLogType), new LuaCSFunction(UnityEngine_ApplicationWrap.set_stackTraceLogType));
		L.RegVar("backgroundLoadingPriority", new LuaCSFunction(UnityEngine_ApplicationWrap.get_backgroundLoadingPriority), new LuaCSFunction(UnityEngine_ApplicationWrap.set_backgroundLoadingPriority));
		L.RegVar("internetReachability", new LuaCSFunction(UnityEngine_ApplicationWrap.get_internetReachability), null);
		L.RegVar("genuine", new LuaCSFunction(UnityEngine_ApplicationWrap.get_genuine), null);
		L.RegVar("genuineCheckAvailable", new LuaCSFunction(UnityEngine_ApplicationWrap.get_genuineCheckAvailable), null);
		L.RegVar("isShowingSplashScreen", new LuaCSFunction(UnityEngine_ApplicationWrap.get_isShowingSplashScreen), null);
		L.RegVar("logMessageReceived", new LuaCSFunction(UnityEngine_ApplicationWrap.get_logMessageReceived), new LuaCSFunction(UnityEngine_ApplicationWrap.set_logMessageReceived));
		L.RegVar("logMessageReceivedThreaded", new LuaCSFunction(UnityEngine_ApplicationWrap.get_logMessageReceivedThreaded), new LuaCSFunction(UnityEngine_ApplicationWrap.set_logMessageReceivedThreaded));
		L.RegFunction("AdvertisingIdentifierCallback", new LuaCSFunction(UnityEngine_ApplicationWrap.UnityEngine_Application_AdvertisingIdentifierCallback));
		L.RegFunction("LogCallback", new LuaCSFunction(UnityEngine_ApplicationWrap.UnityEngine_Application_LogCallback));
		L.EndStaticLibs();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Quit(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			Application.Quit();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CancelQuit(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			Application.CancelQuit();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetStreamProgressForLevel(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				float streamProgressForLevel = Application.GetStreamProgressForLevel(text);
				LuaDLL.lua_pushnumber(L, (double)streamProgressForLevel);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(int)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				float streamProgressForLevel2 = Application.GetStreamProgressForLevel(num2);
				LuaDLL.lua_pushnumber(L, (double)streamProgressForLevel2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Application.GetStreamProgressForLevel");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CanStreamedLevelBeLoaded(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				bool value = Application.CanStreamedLevelBeLoaded(text);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(int)))
			{
				int num2 = (int)LuaDLL.lua_tonumber(L, 1);
				bool value2 = Application.CanStreamedLevelBeLoaded(num2);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Application.CanStreamedLevelBeLoaded");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CaptureScreenshot(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				Application.CaptureScreenshot(text);
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				string text2 = ToLua.ToString(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Application.CaptureScreenshot(text2, num2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Application.CaptureScreenshot");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HasProLicense(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			bool value = Application.HasProLicense();
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
	private static int ExternalCall(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			string text = ToLua.CheckString(L, 1);
			object[] array = ToLua.ToParamsObject(L, 2, num - 1);
			Application.ExternalCall(text, array);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RequestAdvertisingIdentifierAsync(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			Application.AdvertisingIdentifierCallback advertisingIdentifierCallback;
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				advertisingIdentifierCallback = (Application.AdvertisingIdentifierCallback)ToLua.CheckObject(L, 1, typeof(Application.AdvertisingIdentifierCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 1);
				advertisingIdentifierCallback = (DelegateFactory.CreateDelegate(typeof(Application.AdvertisingIdentifierCallback), func) as Application.AdvertisingIdentifierCallback);
			}
			bool value = Application.RequestAdvertisingIdentifierAsync(advertisingIdentifierCallback);
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
	private static int OpenURL(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			Application.OpenURL(text);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RequestUserAuthorization(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UserAuthorization userAuthorization = (int)ToLua.CheckObject(L, 1, typeof(UserAuthorization));
			AsyncOperation o = Application.RequestUserAuthorization(userAuthorization);
			ToLua.PushObject(L, o);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HasUserAuthorization(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UserAuthorization userAuthorization = (int)ToLua.CheckObject(L, 1, typeof(UserAuthorization));
			bool value = Application.HasUserAuthorization(userAuthorization);
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
	private static int get_streamedBytes(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Application.get_streamedBytes());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPlaying(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_isPlaying());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isEditor(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_isEditor());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isWebPlayer(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_isWebPlayer());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_platform(IntPtr L)
	{
		ToLua.Push(L, Application.get_platform());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isMobilePlatform(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_isMobilePlatform());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isConsolePlatform(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_isConsolePlatform());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_runInBackground(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_runInBackground());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_dataPath(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_dataPath());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_streamingAssetsPath(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_streamingAssetsPath());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_persistentDataPath(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_persistentDataPath());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_temporaryCachePath(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_temporaryCachePath());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_srcValue(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_srcValue());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_absoluteURL(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_absoluteURL());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_unityVersion(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_unityVersion());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_version(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_version());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bundleIdentifier(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_bundleIdentifier());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_installMode(IntPtr L)
	{
		ToLua.Push(L, Application.get_installMode());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sandboxType(IntPtr L)
	{
		ToLua.Push(L, Application.get_sandboxType());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_productName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_productName());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_companyName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_companyName());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cloudProjectId(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_cloudProjectId());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_webSecurityEnabled(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_webSecurityEnabled());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_webSecurityHostUrl(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Application.get_webSecurityHostUrl());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_targetFrameRate(IntPtr L)
	{
		Debug.LogError("===>get_targetFrameRate");
		LuaDLL.lua_pushinteger(L, Application.get_targetFrameRate());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_systemLanguage(IntPtr L)
	{
		ToLua.Push(L, Application.get_systemLanguage());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stackTraceLogType(IntPtr L)
	{
		ToLua.Push(L, Application.get_stackTraceLogType());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_backgroundLoadingPriority(IntPtr L)
	{
		ToLua.Push(L, Application.get_backgroundLoadingPriority());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_internetReachability(IntPtr L)
	{
		ToLua.Push(L, Application.get_internetReachability());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_genuine(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_genuine());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_genuineCheckAvailable(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_genuineCheckAvailable());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isShowingSplashScreen(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Application.get_isShowingSplashScreen());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_logMessageReceived(IntPtr L)
	{
		ToLua.Push(L, new EventObject("UnityEngine.Application.logMessageReceived"));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_logMessageReceivedThreaded(IntPtr L)
	{
		ToLua.Push(L, new EventObject("UnityEngine.Application.logMessageReceivedThreaded"));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_runInBackground(IntPtr L)
	{
		int result;
		try
		{
			bool runInBackground = LuaDLL.luaL_checkboolean(L, 2);
			Application.set_runInBackground(runInBackground);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_targetFrameRate(IntPtr L)
	{
		int result;
		try
		{
			Debug.LogError("===>set_targetFrameRate");
			int targetFrameRate = (int)LuaDLL.luaL_checknumber(L, 2);
			Application.set_targetFrameRate(targetFrameRate);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_stackTraceLogType(IntPtr L)
	{
		int result;
		try
		{
			StackTraceLogType stackTraceLogType = (int)ToLua.CheckObject(L, 2, typeof(StackTraceLogType));
			Application.set_stackTraceLogType(stackTraceLogType);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_backgroundLoadingPriority(IntPtr L)
	{
		int result;
		try
		{
			ThreadPriority backgroundLoadingPriority = (int)ToLua.CheckObject(L, 2, typeof(ThreadPriority));
			Application.set_backgroundLoadingPriority(backgroundLoadingPriority);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_logMessageReceived(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				EventObject eventObject = (EventObject)ToLua.ToObject(L, 2);
				if (eventObject.op == EventOp.Add)
				{
					Application.LogCallback logCallback = (Application.LogCallback)DelegateFactory.CreateDelegate(typeof(Application.LogCallback), eventObject.func);
					Application.add_logMessageReceived(logCallback);
				}
				else if (eventObject.op == EventOp.Sub)
				{
					Application.LogCallback logCallback2 = (Application.LogCallback)LuaMisc.GetEventHandler(null, typeof(Application), "logMessageReceived");
					Delegate[] invocationList = logCallback2.GetInvocationList();
					LuaState luaState = LuaState.Get(L);
					for (int i = 0; i < invocationList.Length; i++)
					{
						logCallback2 = (Application.LogCallback)invocationList[i];
						LuaDelegate luaDelegate = logCallback2.get_Target() as LuaDelegate;
						if (luaDelegate != null && luaDelegate.func == eventObject.func)
						{
							Application.remove_logMessageReceived(logCallback2);
							luaState.DelayDispose(luaDelegate.func);
							break;
						}
					}
					eventObject.func.Dispose();
				}
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "The event 'UnityEngine.Application.logMessageReceived' can only appear on the left hand side of += or -= when used outside of the type 'UnityEngine.Application'");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_logMessageReceivedThreaded(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				EventObject eventObject = (EventObject)ToLua.ToObject(L, 2);
				if (eventObject.op == EventOp.Add)
				{
					Application.LogCallback logCallback = (Application.LogCallback)DelegateFactory.CreateDelegate(typeof(Application.LogCallback), eventObject.func);
					Application.add_logMessageReceivedThreaded(logCallback);
				}
				else if (eventObject.op == EventOp.Sub)
				{
					Application.LogCallback logCallback2 = (Application.LogCallback)LuaMisc.GetEventHandler(null, typeof(Application), "logMessageReceivedThreaded");
					Delegate[] invocationList = logCallback2.GetInvocationList();
					LuaState luaState = LuaState.Get(L);
					for (int i = 0; i < invocationList.Length; i++)
					{
						logCallback2 = (Application.LogCallback)invocationList[i];
						LuaDelegate luaDelegate = logCallback2.get_Target() as LuaDelegate;
						if (luaDelegate != null && luaDelegate.func == eventObject.func)
						{
							Application.remove_logMessageReceivedThreaded(logCallback2);
							luaState.DelayDispose(luaDelegate.func);
							break;
						}
					}
					eventObject.func.Dispose();
				}
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "The event 'UnityEngine.Application.logMessageReceivedThreaded' can only appear on the left hand side of += or -= when used outside of the type 'UnityEngine.Application'");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_Application_AdvertisingIdentifierCallback(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(Application.AdvertisingIdentifierCallback), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_Application_LogCallback(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(Application.LogCallback), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
