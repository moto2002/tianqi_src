using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_AssetBundleWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AssetBundle), typeof(Object), null);
		L.RegFunction("LoadFromFileAsync", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadFromFileAsync));
		L.RegFunction("LoadFromFile", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadFromFile));
		L.RegFunction("LoadFromMemoryAsync", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadFromMemoryAsync));
		L.RegFunction("LoadFromMemory", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadFromMemory));
		L.RegFunction("Contains", new LuaCSFunction(UnityEngine_AssetBundleWrap.Contains));
		L.RegFunction("LoadAsset", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadAsset));
		L.RegFunction("LoadAssetAsync", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadAssetAsync));
		L.RegFunction("LoadAssetWithSubAssets", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadAssetWithSubAssets));
		L.RegFunction("LoadAssetWithSubAssetsAsync", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadAssetWithSubAssetsAsync));
		L.RegFunction("LoadAllAssets", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadAllAssets));
		L.RegFunction("LoadAllAssetsAsync", new LuaCSFunction(UnityEngine_AssetBundleWrap.LoadAllAssetsAsync));
		L.RegFunction("Unload", new LuaCSFunction(UnityEngine_AssetBundleWrap.Unload));
		L.RegFunction("GetAllAssetNames", new LuaCSFunction(UnityEngine_AssetBundleWrap.GetAllAssetNames));
		L.RegFunction("GetAllScenePaths", new LuaCSFunction(UnityEngine_AssetBundleWrap.GetAllScenePaths));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_AssetBundleWrap._CreateUnityEngine_AssetBundle));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_AssetBundleWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_AssetBundleWrap.Lua_ToString));
		L.RegVar("mainAsset", new LuaCSFunction(UnityEngine_AssetBundleWrap.get_mainAsset), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_AssetBundle(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				AssetBundle obj = new AssetBundle();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.AssetBundle.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromFileAsync(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				AssetBundleCreateRequest o = AssetBundle.LoadFromFileAsync(text);
				ToLua.PushObject(L, o);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(uint)))
			{
				string text2 = ToLua.ToString(L, 1);
				uint num2 = (uint)LuaDLL.lua_tonumber(L, 2);
				AssetBundleCreateRequest o2 = AssetBundle.LoadFromFileAsync(text2, num2);
				ToLua.PushObject(L, o2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromFileAsync");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromFile(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				AssetBundle obj = AssetBundle.LoadFromFile(text);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(uint)))
			{
				string text2 = ToLua.ToString(L, 1);
				uint num2 = (uint)LuaDLL.lua_tonumber(L, 2);
				AssetBundle obj2 = AssetBundle.LoadFromFile(text2, num2);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromFile");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromMemoryAsync(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(byte[])))
			{
				byte[] array = ToLua.CheckByteBuffer(L, 1);
				AssetBundleCreateRequest o = AssetBundle.LoadFromMemoryAsync(array);
				ToLua.PushObject(L, o);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(byte[]), typeof(uint)))
			{
				byte[] array2 = ToLua.CheckByteBuffer(L, 1);
				uint num2 = (uint)LuaDLL.lua_tonumber(L, 2);
				AssetBundleCreateRequest o2 = AssetBundle.LoadFromMemoryAsync(array2, num2);
				ToLua.PushObject(L, o2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromMemoryAsync");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromMemory(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(byte[])))
			{
				byte[] array = ToLua.CheckByteBuffer(L, 1);
				AssetBundle obj = AssetBundle.LoadFromMemory(array);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(byte[]), typeof(uint)))
			{
				byte[] array2 = ToLua.CheckByteBuffer(L, 1);
				uint num2 = (uint)LuaDLL.lua_tonumber(L, 2);
				AssetBundle obj2 = AssetBundle.LoadFromMemory(array2, num2);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromMemory");
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
			AssetBundle assetBundle = (AssetBundle)ToLua.CheckObject(L, 1, typeof(AssetBundle));
			string text = ToLua.CheckString(L, 2);
			bool value = assetBundle.Contains(text);
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
	private static int LoadAsset(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string)))
			{
				AssetBundle assetBundle = (AssetBundle)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Object obj = assetBundle.LoadAsset(text);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string), typeof(Type)))
			{
				AssetBundle assetBundle2 = (AssetBundle)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				Type type = (Type)ToLua.ToObject(L, 3);
				Object obj2 = assetBundle2.LoadAsset(text2, type);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAsset");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetAsync(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string)))
			{
				AssetBundle assetBundle = (AssetBundle)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				AssetBundleRequest o = assetBundle.LoadAssetAsync(text);
				ToLua.PushObject(L, o);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string), typeof(Type)))
			{
				AssetBundle assetBundle2 = (AssetBundle)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				Type type = (Type)ToLua.ToObject(L, 3);
				AssetBundleRequest o2 = assetBundle2.LoadAssetAsync(text2, type);
				ToLua.PushObject(L, o2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAssetAsync");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetWithSubAssets(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string)))
			{
				AssetBundle assetBundle = (AssetBundle)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Object[] array = assetBundle.LoadAssetWithSubAssets(text);
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string), typeof(Type)))
			{
				AssetBundle assetBundle2 = (AssetBundle)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				Type type = (Type)ToLua.ToObject(L, 3);
				Object[] array2 = assetBundle2.LoadAssetWithSubAssets(text2, type);
				ToLua.Push(L, array2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAssetWithSubAssets");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetWithSubAssetsAsync(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string)))
			{
				AssetBundle assetBundle = (AssetBundle)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				AssetBundleRequest o = assetBundle.LoadAssetWithSubAssetsAsync(text);
				ToLua.PushObject(L, o);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(string), typeof(Type)))
			{
				AssetBundle assetBundle2 = (AssetBundle)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				Type type = (Type)ToLua.ToObject(L, 3);
				AssetBundleRequest o2 = assetBundle2.LoadAssetWithSubAssetsAsync(text2, type);
				ToLua.PushObject(L, o2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAssetWithSubAssetsAsync");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAllAssets(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle)))
			{
				AssetBundle assetBundle = (AssetBundle)ToLua.ToObject(L, 1);
				Object[] array = assetBundle.LoadAllAssets();
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(Type)))
			{
				AssetBundle assetBundle2 = (AssetBundle)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				Object[] array2 = assetBundle2.LoadAllAssets(type);
				ToLua.Push(L, array2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAllAssets");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAllAssetsAsync(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle)))
			{
				AssetBundle assetBundle = (AssetBundle)ToLua.ToObject(L, 1);
				AssetBundleRequest o = assetBundle.LoadAllAssetsAsync();
				ToLua.PushObject(L, o);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(AssetBundle), typeof(Type)))
			{
				AssetBundle assetBundle2 = (AssetBundle)ToLua.ToObject(L, 1);
				Type type = (Type)ToLua.ToObject(L, 2);
				AssetBundleRequest o2 = assetBundle2.LoadAllAssetsAsync(type);
				ToLua.PushObject(L, o2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAllAssetsAsync");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Unload(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			AssetBundle assetBundle = (AssetBundle)ToLua.CheckObject(L, 1, typeof(AssetBundle));
			bool flag = LuaDLL.luaL_checkboolean(L, 2);
			assetBundle.Unload(flag);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAllAssetNames(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AssetBundle assetBundle = (AssetBundle)ToLua.CheckObject(L, 1, typeof(AssetBundle));
			string[] allAssetNames = assetBundle.GetAllAssetNames();
			ToLua.Push(L, allAssetNames);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAllScenePaths(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			AssetBundle assetBundle = (AssetBundle)ToLua.CheckObject(L, 1, typeof(AssetBundle));
			string[] allScenePaths = assetBundle.GetAllScenePaths();
			ToLua.Push(L, allScenePaths);
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
	private static int get_mainAsset(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			AssetBundle assetBundle = (AssetBundle)obj;
			Object mainAsset = assetBundle.get_mainAsset();
			ToLua.Push(L, mainAsset);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mainAsset on a nil value");
		}
		return result;
	}
}
