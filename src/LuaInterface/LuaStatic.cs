using System;
using System.IO;
using System.Text;

namespace LuaInterface
{
	public static class LuaStatic
	{
		public static int GetMetaReference(IntPtr L, Type t)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.GetMetaReference(t);
		}

		public static Type GetClassType(IntPtr L, int reference)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.GetClassType(reference);
		}

		public static LuaFunction GetFunction(IntPtr L, int reference)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.GetFunction(reference);
		}

		public static LuaTable GetTable(IntPtr L, int reference)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.GetTable(reference);
		}

		public static LuaThread GetLuaThread(IntPtr L, int reference)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.GetLuaThread(reference);
		}

		public static void GetUnpackRayRef(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			LuaDLL.lua_getref(L, luaState.UnpackRay);
		}

		public static void GetUnpackBounds(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			LuaDLL.lua_getref(L, luaState.UnpackBounds);
		}

		public static void GetPackRay(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			LuaDLL.lua_getref(L, luaState.PackRay);
		}

		public static void GetPackRaycastHit(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			LuaDLL.lua_getref(L, luaState.PackRaycastHit);
		}

		public static void GetPackTouch(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			LuaDLL.lua_getref(L, luaState.PackTouch);
		}

		public static void GetPackBounds(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			LuaDLL.lua_getref(L, luaState.PackBounds);
		}

		public static int GetOutMetatable(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.OutMetatable;
		}

		public static int GetArrayMetatable(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.ArrayMetatable;
		}

		public static int GetTypeMetatable(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.TypeMetatable;
		}

		public static int GetDelegateMetatable(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.DelegateMetatable;
		}

		public static int GetEventMetatable(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.EventMetatable;
		}

		public static int GetIterMetatable(IntPtr L)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.IterMetatable;
		}

		public static int GetEnumObject(IntPtr L, Enum e, out object obj)
		{
			LuaState luaState = LuaState.Get(L);
			obj = luaState.GetEnumObj(e);
			return luaState.EnumMetatable;
		}

		public static LuaCSFunction GetPreModule(IntPtr L, Type t)
		{
			LuaState luaState = LuaState.Get(L);
			return luaState.GetPreModule(t);
		}

		public static void OpenLibs(IntPtr L)
		{
			LuaDLL.tolua_atpanic(L, new LuaCSFunction(LuaStatic.Panic));
			LuaDLL.tolua_pushcfunction(L, new LuaCSFunction(LuaStatic.Print));
			LuaDLL.lua_setglobal(L, "print");
			LuaDLL.tolua_pushcfunction(L, new LuaCSFunction(LuaStatic.DoFile));
			LuaDLL.lua_setglobal(L, "dofile");
			LuaStatic.AddLuaLoader(L);
		}

		private static void AddLuaLoader(IntPtr L)
		{
			LuaDLL.lua_getglobal(L, "package");
			LuaDLL.lua_getfield(L, -1, "loaders");
			LuaDLL.tolua_pushcfunction(L, new LuaCSFunction(LuaStatic.Loader));
			for (int i = LuaDLL.lua_objlen(L, -2) + 1; i > 2; i--)
			{
				LuaDLL.lua_rawgeti(L, -2, i - 1);
				LuaDLL.lua_rawseti(L, -3, i);
			}
			LuaDLL.lua_rawseti(L, -2, 2);
			LuaDLL.lua_settop(L, 0);
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int Panic(IntPtr L)
		{
			string text = string.Format("PANIC: unprotected error in call to Lua API ({0})", LuaDLL.lua_tostring(L, -1));
			throw new Exception(text);
		}

		private static string LuaWhere(IntPtr L)
		{
			return string.Empty;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int Print(IntPtr L)
		{
			int result;
			try
			{
				int num = LuaDLL.lua_gettop(L);
				StringBuilder stringBuilder = StringBuilderCache.Acquire(256);
				stringBuilder.Append(LuaStatic.LuaWhere(L));
				for (int i = 1; i <= num; i++)
				{
					if (i > 1)
					{
						stringBuilder.Append("    ");
					}
					if (LuaDLL.lua_isstring(L, i) == 1)
					{
						stringBuilder.Append(LuaDLL.lua_tostring(L, i));
					}
					else if (LuaDLL.lua_isnil(L, i))
					{
						stringBuilder.Append("nil");
					}
					else if (LuaDLL.lua_isboolean(L, i))
					{
						stringBuilder.Append((!LuaDLL.lua_toboolean(L, i)) ? "false" : "true");
					}
					else
					{
						IntPtr intPtr = LuaDLL.lua_topointer(L, i);
						if (intPtr == IntPtr.Zero)
						{
							stringBuilder.Append("nil");
						}
						else
						{
							stringBuilder.AppendFormat("{0}:0x{1}", LuaDLL.luaL_typename(L, i), intPtr.ToString("X"));
						}
					}
				}
				Debugger.Log(stringBuilder.ToString());
				result = 0;
			}
			catch (Exception e)
			{
				result = LuaDLL.toluaL_exception(L, e, null);
			}
			return result;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		private static int Loader(IntPtr L)
		{
			int result;
			try
			{
				string text = LuaDLL.lua_tostring(L, 1);
				if (!Path.HasExtension(text))
				{
					text += ".lua";
				}
				byte[] array = LuaFileUtils.Instance.ReadFile(text);
				if (array == null)
				{
					result = 0;
				}
				else
				{
					LuaDLL.luaL_loadbuffer(L, array, array.Length, text);
					result = 1;
				}
			}
			catch (Exception e)
			{
				result = LuaDLL.toluaL_exception(L, e, null);
			}
			return result;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int DoFile(IntPtr L)
		{
			int result;
			try
			{
				string text = LuaDLL.lua_tostring(L, 1);
				if (!Path.HasExtension(text))
				{
					text += ".lua";
				}
				int num = LuaDLL.lua_gettop(L);
				byte[] array = LuaFileUtils.Instance.ReadFile(text);
				if (array == null)
				{
					result = 0;
				}
				else
				{
					if (LuaDLL.luaL_loadbuffer(L, array, array.Length, text) == 0 && LuaDLL.lua_pcall(L, 0, LuaDLL.LUA_MULTRET, 0) != 0)
					{
						string msg = LuaDLL.lua_tostring(L, -1);
						throw new LuaException(msg, null, 1);
					}
					result = LuaDLL.lua_gettop(L) - num;
				}
			}
			catch (Exception e)
			{
				result = LuaDLL.toluaL_exception(L, e, null);
			}
			return result;
		}
	}
}
