using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_MaterialWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Material), typeof(Object), null);
		L.RegFunction("SetColor", new LuaCSFunction(UnityEngine_MaterialWrap.SetColor));
		L.RegFunction("GetColor", new LuaCSFunction(UnityEngine_MaterialWrap.GetColor));
		L.RegFunction("SetVector", new LuaCSFunction(UnityEngine_MaterialWrap.SetVector));
		L.RegFunction("GetVector", new LuaCSFunction(UnityEngine_MaterialWrap.GetVector));
		L.RegFunction("SetTexture", new LuaCSFunction(UnityEngine_MaterialWrap.SetTexture));
		L.RegFunction("GetTexture", new LuaCSFunction(UnityEngine_MaterialWrap.GetTexture));
		L.RegFunction("SetTextureOffset", new LuaCSFunction(UnityEngine_MaterialWrap.SetTextureOffset));
		L.RegFunction("GetTextureOffset", new LuaCSFunction(UnityEngine_MaterialWrap.GetTextureOffset));
		L.RegFunction("SetTextureScale", new LuaCSFunction(UnityEngine_MaterialWrap.SetTextureScale));
		L.RegFunction("GetTextureScale", new LuaCSFunction(UnityEngine_MaterialWrap.GetTextureScale));
		L.RegFunction("SetMatrix", new LuaCSFunction(UnityEngine_MaterialWrap.SetMatrix));
		L.RegFunction("GetMatrix", new LuaCSFunction(UnityEngine_MaterialWrap.GetMatrix));
		L.RegFunction("SetFloat", new LuaCSFunction(UnityEngine_MaterialWrap.SetFloat));
		L.RegFunction("GetFloat", new LuaCSFunction(UnityEngine_MaterialWrap.GetFloat));
		L.RegFunction("SetInt", new LuaCSFunction(UnityEngine_MaterialWrap.SetInt));
		L.RegFunction("GetInt", new LuaCSFunction(UnityEngine_MaterialWrap.GetInt));
		L.RegFunction("SetBuffer", new LuaCSFunction(UnityEngine_MaterialWrap.SetBuffer));
		L.RegFunction("HasProperty", new LuaCSFunction(UnityEngine_MaterialWrap.HasProperty));
		L.RegFunction("GetTag", new LuaCSFunction(UnityEngine_MaterialWrap.GetTag));
		L.RegFunction("SetOverrideTag", new LuaCSFunction(UnityEngine_MaterialWrap.SetOverrideTag));
		L.RegFunction("Lerp", new LuaCSFunction(UnityEngine_MaterialWrap.Lerp));
		L.RegFunction("SetPass", new LuaCSFunction(UnityEngine_MaterialWrap.SetPass));
		L.RegFunction("CopyPropertiesFromMaterial", new LuaCSFunction(UnityEngine_MaterialWrap.CopyPropertiesFromMaterial));
		L.RegFunction("EnableKeyword", new LuaCSFunction(UnityEngine_MaterialWrap.EnableKeyword));
		L.RegFunction("DisableKeyword", new LuaCSFunction(UnityEngine_MaterialWrap.DisableKeyword));
		L.RegFunction("IsKeywordEnabled", new LuaCSFunction(UnityEngine_MaterialWrap.IsKeywordEnabled));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_MaterialWrap._CreateUnityEngine_Material));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_MaterialWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_MaterialWrap.Lua_ToString));
		L.RegVar("shader", new LuaCSFunction(UnityEngine_MaterialWrap.get_shader), new LuaCSFunction(UnityEngine_MaterialWrap.set_shader));
		L.RegVar("color", new LuaCSFunction(UnityEngine_MaterialWrap.get_color), new LuaCSFunction(UnityEngine_MaterialWrap.set_color));
		L.RegVar("mainTexture", new LuaCSFunction(UnityEngine_MaterialWrap.get_mainTexture), new LuaCSFunction(UnityEngine_MaterialWrap.set_mainTexture));
		L.RegVar("mainTextureOffset", new LuaCSFunction(UnityEngine_MaterialWrap.get_mainTextureOffset), new LuaCSFunction(UnityEngine_MaterialWrap.set_mainTextureOffset));
		L.RegVar("mainTextureScale", new LuaCSFunction(UnityEngine_MaterialWrap.get_mainTextureScale), new LuaCSFunction(UnityEngine_MaterialWrap.set_mainTextureScale));
		L.RegVar("passCount", new LuaCSFunction(UnityEngine_MaterialWrap.get_passCount), null);
		L.RegVar("renderQueue", new LuaCSFunction(UnityEngine_MaterialWrap.get_renderQueue), new LuaCSFunction(UnityEngine_MaterialWrap.set_renderQueue));
		L.RegVar("shaderKeywords", new LuaCSFunction(UnityEngine_MaterialWrap.get_shaderKeywords), new LuaCSFunction(UnityEngine_MaterialWrap.set_shaderKeywords));
		L.RegVar("globalIlluminationFlags", new LuaCSFunction(UnityEngine_MaterialWrap.get_globalIlluminationFlags), new LuaCSFunction(UnityEngine_MaterialWrap.set_globalIlluminationFlags));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Material(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Material)))
			{
				Material material = (Material)ToLua.CheckUnityObject(L, 1, typeof(Material));
				Material obj = new Material(material);
				ToLua.Push(L, obj);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(Shader)))
			{
				Shader shader = (Shader)ToLua.CheckUnityObject(L, 1, typeof(Shader));
				Material obj2 = new Material(shader);
				ToLua.Push(L, obj2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Material.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetColor(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(Color)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Color color = ToLua.ToColor(L, 3);
				material.SetColor(num2, color);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(Color)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Color color2 = ToLua.ToColor(L, 3);
				material2.SetColor(text, color2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.SetColor");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetColor(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Color color = material.GetColor(num2);
				ToLua.Push(L, color);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Color color2 = material2.GetColor(text);
				ToLua.Push(L, color2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.GetColor");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetVector(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(Vector4)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Vector4 vector = ToLua.ToVector4(L, 3);
				material.SetVector(num2, vector);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(Vector4)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Vector4 vector2 = ToLua.ToVector4(L, 3);
				material2.SetVector(text, vector2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.SetVector");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetVector(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Vector4 vector = material.GetVector(num2);
				ToLua.Push(L, vector);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Vector4 vector2 = material2.GetVector(text);
				ToLua.Push(L, vector2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.GetVector");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTexture(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(Texture)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Texture texture = (Texture)ToLua.ToObject(L, 3);
				material.SetTexture(num2, texture);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(Texture)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Texture texture2 = (Texture)ToLua.ToObject(L, 3);
				material2.SetTexture(text, texture2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.SetTexture");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTexture(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Texture texture = material.GetTexture(num2);
				ToLua.Push(L, texture);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Texture texture2 = material2.GetTexture(text);
				ToLua.Push(L, texture2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.GetTexture");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTextureOffset(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			Vector2 vector = ToLua.ToVector2(L, 3);
			material.SetTextureOffset(text, vector);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTextureOffset(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			Vector2 textureOffset = material.GetTextureOffset(text);
			ToLua.Push(L, textureOffset);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTextureScale(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			Vector2 vector = ToLua.ToVector2(L, 3);
			material.SetTextureScale(text, vector);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTextureScale(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			Vector2 textureScale = material.GetTextureScale(text);
			ToLua.Push(L, textureScale);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetMatrix(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(Matrix4x4)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Matrix4x4 matrix4x = (Matrix4x4)ToLua.ToObject(L, 3);
				material.SetMatrix(num2, matrix4x);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(Matrix4x4)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Matrix4x4 matrix4x2 = (Matrix4x4)ToLua.ToObject(L, 3);
				material2.SetMatrix(text, matrix4x2);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.SetMatrix");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMatrix(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				Matrix4x4 matrix = material.GetMatrix(num2);
				ToLua.PushValue(L, matrix);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				Matrix4x4 matrix2 = material2.GetMatrix(text);
				ToLua.PushValue(L, matrix2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.GetMatrix");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetFloat(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(float)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				float num3 = (float)LuaDLL.lua_tonumber(L, 3);
				material.SetFloat(num2, num3);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(float)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				float num4 = (float)LuaDLL.lua_tonumber(L, 3);
				material2.SetFloat(text, num4);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.SetFloat");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetFloat(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				float @float = material.GetFloat(num2);
				LuaDLL.lua_pushnumber(L, (double)@float);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				float float2 = material2.GetFloat(text);
				LuaDLL.lua_pushnumber(L, (double)float2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.GetFloat");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetInt(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				material.SetInt(num2, num3);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(int)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				int num4 = (int)LuaDLL.lua_tonumber(L, 3);
				material2.SetInt(text, num4);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.SetInt");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetInt(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				int @int = material.GetInt(num2);
				LuaDLL.lua_pushinteger(L, @int);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				int int2 = material2.GetInt(text);
				LuaDLL.lua_pushinteger(L, int2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.GetInt");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetBuffer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			ComputeBuffer computeBuffer = (ComputeBuffer)ToLua.CheckObject(L, 3, typeof(ComputeBuffer));
			material.SetBuffer(text, computeBuffer);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HasProperty(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(int)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				int num2 = (int)LuaDLL.lua_tonumber(L, 2);
				bool value = material.HasProperty(num2);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				bool value2 = material2.HasProperty(text);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.HasProperty");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTag(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(bool)))
			{
				Material material = (Material)ToLua.ToObject(L, 1);
				string text = ToLua.ToString(L, 2);
				bool flag = LuaDLL.lua_toboolean(L, 3);
				string tag = material.GetTag(text, flag);
				LuaDLL.lua_pushstring(L, tag);
				result = 1;
			}
			else if (num == 4 && TypeChecker.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(bool), typeof(string)))
			{
				Material material2 = (Material)ToLua.ToObject(L, 1);
				string text2 = ToLua.ToString(L, 2);
				bool flag2 = LuaDLL.lua_toboolean(L, 3);
				string text3 = ToLua.ToString(L, 4);
				string tag2 = material2.GetTag(text2, flag2, text3);
				LuaDLL.lua_pushstring(L, tag2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Material.GetTag");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetOverrideTag(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			string text2 = ToLua.CheckString(L, 3);
			material.SetOverrideTag(text, text2);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lerp(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 4);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			Material material2 = (Material)ToLua.CheckUnityObject(L, 2, typeof(Material));
			Material material3 = (Material)ToLua.CheckUnityObject(L, 3, typeof(Material));
			float num = (float)LuaDLL.luaL_checknumber(L, 4);
			material.Lerp(material2, material3, num);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPass(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			int pass = (int)LuaDLL.luaL_checknumber(L, 2);
			bool value = material.SetPass(pass);
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
	private static int CopyPropertiesFromMaterial(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			Material material2 = (Material)ToLua.CheckUnityObject(L, 2, typeof(Material));
			material.CopyPropertiesFromMaterial(material2);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EnableKeyword(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			material.EnableKeyword(text);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DisableKeyword(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			material.DisableKeyword(text);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsKeywordEnabled(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Material material = (Material)ToLua.CheckObject(L, 1, typeof(Material));
			string text = ToLua.CheckString(L, 2);
			bool value = material.IsKeywordEnabled(text);
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
	private static int get_shader(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Shader shader = material.get_shader();
			ToLua.Push(L, shader);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index shader on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_color(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Color color = material.get_color();
			ToLua.Push(L, color);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index color on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainTexture(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Texture mainTexture = material.get_mainTexture();
			ToLua.Push(L, mainTexture);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mainTexture on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainTextureOffset(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Vector2 mainTextureOffset = material.get_mainTextureOffset();
			ToLua.Push(L, mainTextureOffset);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mainTextureOffset on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainTextureScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Vector2 mainTextureScale = material.get_mainTextureScale();
			ToLua.Push(L, mainTextureScale);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mainTextureScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_passCount(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			int passCount = material.get_passCount();
			LuaDLL.lua_pushinteger(L, passCount);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index passCount on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_renderQueue(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			int renderQueue = material.get_renderQueue();
			LuaDLL.lua_pushinteger(L, renderQueue);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index renderQueue on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shaderKeywords(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			string[] shaderKeywords = material.get_shaderKeywords();
			ToLua.Push(L, shaderKeywords);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index shaderKeywords on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_globalIlluminationFlags(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			MaterialGlobalIlluminationFlags globalIlluminationFlags = material.get_globalIlluminationFlags();
			ToLua.Push(L, globalIlluminationFlags);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index globalIlluminationFlags on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shader(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Shader shader = (Shader)ToLua.CheckUnityObject(L, 2, typeof(Shader));
			material.set_shader(shader);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index shader on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_color(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Color color = ToLua.ToColor(L, 2);
			material.set_color(color);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index color on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mainTexture(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Texture mainTexture = (Texture)ToLua.CheckUnityObject(L, 2, typeof(Texture));
			material.set_mainTexture(mainTexture);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mainTexture on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mainTextureOffset(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Vector2 mainTextureOffset = ToLua.ToVector2(L, 2);
			material.set_mainTextureOffset(mainTextureOffset);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mainTextureOffset on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mainTextureScale(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			Vector2 mainTextureScale = ToLua.ToVector2(L, 2);
			material.set_mainTextureScale(mainTextureScale);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index mainTextureScale on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_renderQueue(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			int renderQueue = (int)LuaDLL.luaL_checknumber(L, 2);
			material.set_renderQueue(renderQueue);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index renderQueue on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shaderKeywords(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			string[] shaderKeywords = ToLua.CheckStringArray(L, 2);
			material.set_shaderKeywords(shaderKeywords);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index shaderKeywords on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_globalIlluminationFlags(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Material material = (Material)obj;
			MaterialGlobalIlluminationFlags globalIlluminationFlags = (int)ToLua.CheckObject(L, 2, typeof(MaterialGlobalIlluminationFlags));
			material.set_globalIlluminationFlags(globalIlluminationFlags);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index globalIlluminationFlags on a nil value");
		}
		return result;
	}
}
