using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_Texture2DWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Texture2D), typeof(Texture), null);
		L.RegFunction("CreateExternalTexture", new LuaCSFunction(UnityEngine_Texture2DWrap.CreateExternalTexture));
		L.RegFunction("UpdateExternalTexture", new LuaCSFunction(UnityEngine_Texture2DWrap.UpdateExternalTexture));
		L.RegFunction("SetPixel", new LuaCSFunction(UnityEngine_Texture2DWrap.SetPixel));
		L.RegFunction("GetPixel", new LuaCSFunction(UnityEngine_Texture2DWrap.GetPixel));
		L.RegFunction("GetPixelBilinear", new LuaCSFunction(UnityEngine_Texture2DWrap.GetPixelBilinear));
		L.RegFunction("SetPixels", new LuaCSFunction(UnityEngine_Texture2DWrap.SetPixels));
		L.RegFunction("SetPixels32", new LuaCSFunction(UnityEngine_Texture2DWrap.SetPixels32));
		L.RegFunction("LoadImage", new LuaCSFunction(UnityEngine_Texture2DWrap.LoadImage));
		L.RegFunction("LoadRawTextureData", new LuaCSFunction(UnityEngine_Texture2DWrap.LoadRawTextureData));
		L.RegFunction("GetRawTextureData", new LuaCSFunction(UnityEngine_Texture2DWrap.GetRawTextureData));
		L.RegFunction("GetPixels", new LuaCSFunction(UnityEngine_Texture2DWrap.GetPixels));
		L.RegFunction("GetPixels32", new LuaCSFunction(UnityEngine_Texture2DWrap.GetPixels32));
		L.RegFunction("Apply", new LuaCSFunction(UnityEngine_Texture2DWrap.Apply));
		L.RegFunction("Resize", new LuaCSFunction(UnityEngine_Texture2DWrap.Resize));
		L.RegFunction("Compress", new LuaCSFunction(UnityEngine_Texture2DWrap.Compress));
		L.RegFunction("PackTextures", new LuaCSFunction(UnityEngine_Texture2DWrap.PackTextures));
		L.RegFunction("ReadPixels", new LuaCSFunction(UnityEngine_Texture2DWrap.ReadPixels));
		L.RegFunction("EncodeToPNG", new LuaCSFunction(UnityEngine_Texture2DWrap.EncodeToPNG));
		L.RegFunction("EncodeToJPG", new LuaCSFunction(UnityEngine_Texture2DWrap.EncodeToJPG));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_Texture2DWrap._CreateUnityEngine_Texture2D));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_Texture2DWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_Texture2DWrap.Lua_ToString));
		L.RegVar("mipmapCount", new LuaCSFunction(UnityEngine_Texture2DWrap.get_mipmapCount), null);
		L.RegVar("format", new LuaCSFunction(UnityEngine_Texture2DWrap.get_format), null);
		L.RegVar("whiteTexture", new LuaCSFunction(UnityEngine_Texture2DWrap.get_whiteTexture), null);
		L.RegVar("blackTexture", new LuaCSFunction(UnityEngine_Texture2DWrap.get_blackTexture), null);
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Texture2D(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2)
			{
				int num2 = (int)LuaDLL.luaL_checknumber(L, 1);
				int num3 = (int)LuaDLL.luaL_checknumber(L, 2);
				Texture2D obj = new Texture2D(num2, num3);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(TextureFormat), typeof(bool)))
			{
				int num4 = (int)LuaDLL.luaL_checknumber(L, 1);
				int num5 = (int)LuaDLL.luaL_checknumber(L, 2);
				TextureFormat textureFormat = (int)ToLua.CheckObject(L, 3, typeof(TextureFormat));
				bool flag = LuaDLL.luaL_checkboolean(L, 4);
				Texture2D obj2 = new Texture2D(num4, num5, textureFormat, flag);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(int), typeof(int), typeof(TextureFormat), typeof(bool), typeof(bool)))
			{
				int num6 = (int)LuaDLL.luaL_checknumber(L, 1);
				int num7 = (int)LuaDLL.luaL_checknumber(L, 2);
				TextureFormat textureFormat2 = (int)ToLua.CheckObject(L, 3, typeof(TextureFormat));
				bool flag2 = LuaDLL.luaL_checkboolean(L, 4);
				bool flag3 = LuaDLL.luaL_checkboolean(L, 5);
				Texture2D obj3 = new Texture2D(num6, num7, textureFormat2, flag2, flag3);
				ToLua.Push(L, obj3);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Texture2D.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreateExternalTexture(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 6);
			int num = (int)LuaDLL.luaL_checknumber(L, 1);
			int num2 = (int)LuaDLL.luaL_checknumber(L, 2);
			TextureFormat textureFormat = (int)ToLua.CheckObject(L, 3, typeof(TextureFormat));
			bool flag = LuaDLL.luaL_checkboolean(L, 4);
			bool flag2 = LuaDLL.luaL_checkboolean(L, 5);
			IntPtr intPtr = LuaDLL.lua_touserdata(L, 6);
			Texture2D obj = Texture2D.CreateExternalTexture(num, num2, textureFormat, flag, flag2, intPtr);
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
	private static int UpdateExternalTexture(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Texture2D texture2D = (Texture2D)ToLua.CheckObject(L, 1, typeof(Texture2D));
			IntPtr intPtr = LuaDLL.lua_touserdata(L, 2);
			texture2D.UpdateExternalTexture(intPtr);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPixel(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 4);
			Texture2D texture2D = (Texture2D)ToLua.CheckObject(L, 1, typeof(Texture2D));
			int num = (int)LuaDLL.luaL_checknumber(L, 2);
			int num2 = (int)LuaDLL.luaL_checknumber(L, 3);
			Color color = ToLua.ToColor(L, 4);
			texture2D.SetPixel(num, num2, color);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPixel(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Texture2D texture2D = (Texture2D)ToLua.CheckObject(L, 1, typeof(Texture2D));
			int num = (int)LuaDLL.luaL_checknumber(L, 2);
			int num2 = (int)LuaDLL.luaL_checknumber(L, 3);
			Color pixel = texture2D.GetPixel(num, num2);
			ToLua.Push(L, pixel);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPixelBilinear(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Texture2D texture2D = (Texture2D)ToLua.CheckObject(L, 1, typeof(Texture2D));
			float num = (float)LuaDLL.luaL_checknumber(L, 2);
			float num2 = (float)LuaDLL.luaL_checknumber(L, 3);
			Color pixelBilinear = texture2D.GetPixelBilinear(num, num2);
			ToLua.Push(L, pixelBilinear);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPixels(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Color[])))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				Color[] pixels = ToLua.CheckObjectArray<Color>(L, 2);
				texture2D.SetPixels(pixels);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Color[]), typeof(int)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				Color[] array = ToLua.CheckObjectArray<Color>(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				texture2D2.SetPixels(array, num2);
				result = 0;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int), typeof(int), typeof(int), typeof(Color[])))
			{
				Texture2D texture2D3 = (Texture2D)ToLua.ToObject(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				int num6 = (int)LuaDLL.lua_tonumber(L, 5);
				Color[] array2 = ToLua.CheckObjectArray<Color>(L, 6);
				texture2D3.SetPixels(num3, num4, num5, num6, array2);
				result = 0;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int), typeof(int), typeof(int), typeof(Color[]), typeof(int)))
			{
				Texture2D texture2D4 = (Texture2D)ToLua.ToObject(L, 1);
				int num7 = (int)LuaDLL.lua_tonumber(L, 2);
				int num8 = (int)LuaDLL.lua_tonumber(L, 3);
				int num9 = (int)LuaDLL.lua_tonumber(L, 4);
				int num10 = (int)LuaDLL.lua_tonumber(L, 5);
				Color[] array3 = ToLua.CheckObjectArray<Color>(L, 6);
				int num11 = (int)LuaDLL.lua_tonumber(L, 7);
				texture2D4.SetPixels(num7, num8, num9, num10, array3, num11);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.SetPixels");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPixels32(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Color32[])))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				Color32[] pixels = ToLua.CheckObjectArray<Color32>(L, 2);
				texture2D.SetPixels32(pixels);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Color32[]), typeof(int)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				Color32[] array = ToLua.CheckObjectArray<Color32>(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				texture2D2.SetPixels32(array, num2);
				result = 0;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int), typeof(int), typeof(int), typeof(Color32[])))
			{
				Texture2D texture2D3 = (Texture2D)ToLua.ToObject(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				int num6 = (int)LuaDLL.lua_tonumber(L, 5);
				Color32[] array2 = ToLua.CheckObjectArray<Color32>(L, 6);
				texture2D3.SetPixels32(num3, num4, num5, num6, array2);
				result = 0;
			}
			else if (num == 7 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int), typeof(int), typeof(int), typeof(Color32[]), typeof(int)))
			{
				Texture2D texture2D4 = (Texture2D)ToLua.ToObject(L, 1);
				int num7 = (int)LuaDLL.lua_tonumber(L, 2);
				int num8 = (int)LuaDLL.lua_tonumber(L, 3);
				int num9 = (int)LuaDLL.lua_tonumber(L, 4);
				int num10 = (int)LuaDLL.lua_tonumber(L, 5);
				Color32[] array3 = ToLua.CheckObjectArray<Color32>(L, 6);
				int num11 = (int)LuaDLL.lua_tonumber(L, 7);
				texture2D4.SetPixels32(num7, num8, num9, num10, array3, num11);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.SetPixels32");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadImage(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(byte[])))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				byte[] array = ToLua.CheckByteBuffer(L, 2);
				bool value = texture2D.LoadImage(array);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(byte[]), typeof(bool)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				byte[] array2 = ToLua.CheckByteBuffer(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				bool value2 = texture2D2.LoadImage(array2, flag);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.LoadImage");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadRawTextureData(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(byte[])))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				byte[] array = ToLua.CheckByteBuffer(L, 2);
				texture2D.LoadRawTextureData(array);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(IntPtr), typeof(int)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				IntPtr intPtr = LuaDLL.lua_touserdata(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				texture2D2.LoadRawTextureData(intPtr, num2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.LoadRawTextureData");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetRawTextureData(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Texture2D texture2D = (Texture2D)ToLua.CheckObject(L, 1, typeof(Texture2D));
			byte[] rawTextureData = texture2D.GetRawTextureData();
			ToLua.Push(L, rawTextureData);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPixels(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D)))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				Color[] pixels = texture2D.GetPixels();
				ToLua.Push(L, pixels);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Color[] pixels2 = texture2D2.GetPixels(num2);
				ToLua.Push(L, pixels2);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int), typeof(int), typeof(int)))
			{
				Texture2D texture2D3 = (Texture2D)ToLua.ToObject(L, 1);
				int num3 = (int)LuaDLL.lua_tonumber(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				int num6 = (int)LuaDLL.lua_tonumber(L, 5);
				Color[] pixels3 = texture2D3.GetPixels(num3, num4, num5, num6);
				ToLua.Push(L, pixels3);
				result = 1;
			}
			else if (num == 6 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)))
			{
				Texture2D texture2D4 = (Texture2D)ToLua.ToObject(L, 1);
				int num7 = (int)LuaDLL.lua_tonumber(L, 2);
				int num8 = (int)LuaDLL.lua_tonumber(L, 3);
				int num9 = (int)LuaDLL.lua_tonumber(L, 4);
				int num10 = (int)LuaDLL.lua_tonumber(L, 5);
				int num11 = (int)LuaDLL.lua_tonumber(L, 6);
				Color[] pixels4 = texture2D4.GetPixels(num7, num8, num9, num10, num11);
				ToLua.Push(L, pixels4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.GetPixels");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPixels32(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D)))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				Color32[] pixels = texture2D.GetPixels32();
				ToLua.Push(L, pixels);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Color32[] pixels2 = texture2D2.GetPixels32(num2);
				ToLua.Push(L, pixels2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.GetPixels32");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Apply(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D)))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				texture2D.Apply();
				result = 0;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(bool)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				bool flag = LuaDLL.lua_toboolean(L, 2);
				texture2D2.Apply(flag);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(bool), typeof(bool)))
			{
				Texture2D texture2D3 = (Texture2D)ToLua.ToObject(L, 1);
				bool flag2 = LuaDLL.lua_toboolean(L, 2);
				bool flag3 = LuaDLL.lua_toboolean(L, 3);
				texture2D3.Apply(flag2, flag3);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.Apply");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Resize(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int)))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				bool value = texture2D.Resize(num2, num3);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int), typeof(int), typeof(TextureFormat), typeof(bool)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				int num4 = (int)LuaDLL.lua_tonumber(L, 2);
				int num5 = (int)LuaDLL.lua_tonumber(L, 3);
				TextureFormat textureFormat = (int)ToLua.ToObject(L, 4);
				bool flag = LuaDLL.lua_toboolean(L, 5);
				bool value2 = texture2D2.Resize(num4, num5, textureFormat, flag);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.Resize");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Compress(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Texture2D texture2D = (Texture2D)ToLua.CheckObject(L, 1, typeof(Texture2D));
			bool flag = LuaDLL.luaL_checkboolean(L, 2);
			texture2D.Compress(flag);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PackTextures(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Texture2D[]), typeof(int)))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				Texture2D[] array = ToLua.CheckObjectArray<Texture2D>(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				Rect[] array2 = texture2D.PackTextures(array, num2);
				ToLua.Push(L, array2);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Texture2D[]), typeof(int), typeof(int)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				Texture2D[] array3 = ToLua.CheckObjectArray<Texture2D>(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				int num4 = (int)LuaDLL.lua_tonumber(L, 4);
				Rect[] array4 = texture2D2.PackTextures(array3, num3, num4);
				ToLua.Push(L, array4);
				result = 1;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Texture2D[]), typeof(int), typeof(int), typeof(bool)))
			{
				Texture2D texture2D3 = (Texture2D)ToLua.ToObject(L, 1);
				Texture2D[] array5 = ToLua.CheckObjectArray<Texture2D>(L, 2);
				int num5 = (int)LuaDLL.lua_tonumber(L, 3);
				int num6 = (int)LuaDLL.lua_tonumber(L, 4);
				bool flag = LuaDLL.lua_toboolean(L, 5);
				Rect[] array6 = texture2D3.PackTextures(array5, num5, num6, flag);
				ToLua.Push(L, array6);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.PackTextures");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadPixels(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Rect), typeof(int), typeof(int)))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				Rect rect = (Rect)ToLua.ToObject(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				int num3 = (int)LuaDLL.lua_tonumber(L, 4);
				texture2D.ReadPixels(rect, num2, num3);
				result = 0;
			}
			else if (num == 5 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(Rect), typeof(int), typeof(int), typeof(bool)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				Rect rect2 = (Rect)ToLua.ToObject(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				int num5 = (int)LuaDLL.lua_tonumber(L, 4);
				bool flag = LuaDLL.lua_toboolean(L, 5);
				texture2D2.ReadPixels(rect2, num4, num5, flag);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.ReadPixels");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EncodeToPNG(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Texture2D texture2D = (Texture2D)ToLua.CheckObject(L, 1, typeof(Texture2D));
			byte[] array = texture2D.EncodeToPNG();
			ToLua.Push(L, array);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EncodeToJPG(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D)))
			{
				Texture2D texture2D = (Texture2D)ToLua.ToObject(L, 1);
				byte[] array = texture2D.EncodeToJPG();
				ToLua.Push(L, array);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Texture2D), typeof(int)))
			{
				Texture2D texture2D2 = (Texture2D)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				byte[] array2 = texture2D2.EncodeToJPG(num2);
				ToLua.Push(L, array2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.EncodeToJPG");
			}
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
	private static int get_mipmapCount(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Texture2D texture2D = (Texture2D)obj;
			int mipmapCount = texture2D.get_mipmapCount();
			LuaDLL.lua_pushinteger(L, mipmapCount);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mipmapCount on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_format(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Texture2D texture2D = (Texture2D)obj;
			TextureFormat format = texture2D.get_format();
			ToLua.Push(L, format);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index format on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_whiteTexture(IntPtr L)
	{
		ToLua.Push(L, Texture2D.get_whiteTexture());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_blackTexture(IntPtr L)
	{
		ToLua.Push(L, Texture2D.get_blackTexture());
		return 1;
	}
}
