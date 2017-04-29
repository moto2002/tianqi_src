using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LuaInterface
{
	public class LuaDLL
	{
		private const string LUADLL = "tolua";

		public static string version = "1.0.4.126";

		public static int LUA_MULTRET = -1;

		public static string[] LuaTypeName = new string[]
		{
			"none",
			"nil",
			"boolean",
			"lightuserdata",
			"number",
			"string",
			"table",
			"function",
			"userdata",
			"thread"
		};

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_sproto_core(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_protobuf_c(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_pb(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_ffi(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_bit(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_struct(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_lpeg(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_socket_core(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_luasocket_scripts(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_cjson(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_cjson_safe(IntPtr L);

		public static int lua_upvalueindex(int i)
		{
			return LuaIndexes.LUA_GLOBALSINDEX - i;
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_close(IntPtr luaState);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr lua_newthread(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr lua_atpanic(IntPtr luaState, IntPtr panic);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_gettop(IntPtr luaState);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_settop(IntPtr luaState, int top);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_pushvalue(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_remove(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_insert(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_replace(IntPtr luaState, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_checkstack(IntPtr luaState, int extra);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_xmove(IntPtr from, IntPtr to, int n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_isnumber(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_isstring(IntPtr luaState, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_iscfunction(IntPtr luaState, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_isuserdata(IntPtr luaState, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern LuaTypes lua_type(IntPtr luaState, int index);

		public static string lua_typename(IntPtr luaState, LuaTypes type)
		{
			return LuaDLL.LuaTypeName[(int)(type + 1)];
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_equal(IntPtr luaState, int idx1, int idx2);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_rawequal(IntPtr luaState, int idx1, int idx2);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_lessthan(IntPtr luaState, int idx1, int idx2);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern double lua_tonumber(IntPtr luaState, int idx);

		public static int lua_tointeger(IntPtr luaState, int idx)
		{
			return LuaDLL.tolua_tointeger(luaState, idx);
		}

		public static bool lua_toboolean(IntPtr luaState, int idx)
		{
			return LuaDLL.tolua_toboolean(luaState, idx);
		}

		public static IntPtr lua_tolstring(IntPtr luaState, int index, out int strLen)
		{
			return LuaDLL.tolua_tolstring(luaState, index, out strLen);
		}

		public static int lua_objlen(IntPtr luaState, int idx)
		{
			return LuaDLL.tolua_objlen(luaState, idx);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr lua_tocfunction(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr lua_touserdata(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr lua_tothread(IntPtr L, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr lua_topointer(IntPtr L, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_pushnil(IntPtr luaState);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_pushnumber(IntPtr luaState, double number);

		public static void lua_pushinteger(IntPtr L, int n)
		{
			LuaDLL.lua_pushnumber(L, (double)n);
		}

		public static void lua_pushlstring(IntPtr luaState, byte[] str, int size)
		{
			LuaDLL.tolua_pushlstring(luaState, str, size);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_pushstring(IntPtr luaState, string str);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_pushcclosure(IntPtr luaState, IntPtr fn, int n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_pushboolean(IntPtr luaState, int value);

		public static void lua_pushboolean(IntPtr luaState, bool value)
		{
			LuaDLL.lua_pushboolean(luaState, (!value) ? 0 : 1);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_pushlightuserdata(IntPtr luaState, IntPtr udata);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_pushthread(IntPtr L);

		public static void lua_gettable(IntPtr L, int idx)
		{
			if (LuaDLL.tolua_gettable(L, idx) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		public static void lua_getfield(IntPtr L, int idx, string key)
		{
			if (LuaDLL.tolua_getfield(L, idx, key) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_rawget(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_rawgeti(IntPtr luaState, int idx, int n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_createtable(IntPtr luaState, int narr, int nrec);

		public static IntPtr lua_newuserdata(IntPtr luaState, int size)
		{
			return LuaDLL.tolua_newuserdata(luaState, size);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_getmetatable(IntPtr luaState, int objIndex);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_getfenv(IntPtr luaState, int idx);

		public static void lua_settable(IntPtr L, int idx)
		{
			if (LuaDLL.tolua_settable(L, idx) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		public static void lua_setfield(IntPtr L, int idx, string key)
		{
			if (LuaDLL.tolua_setfield(L, idx, key) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				throw new LuaException(msg, null, 1);
			}
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_rawset(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_rawseti(IntPtr luaState, int tableIndex, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_setmetatable(IntPtr luaState, int objIndex);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_setfenv(IntPtr luaState, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_call(IntPtr luaState, int nArgs, int nResults);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_pcall(IntPtr luaState, int nArgs, int nResults, int errfunc);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_cpcall(IntPtr L, IntPtr func, IntPtr ud);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_yield(IntPtr L, int nresults);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_resume(IntPtr L, int narg);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_status(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_gc(IntPtr luaState, LuaGCOptions what, int data);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_next(IntPtr luaState, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void lua_concat(IntPtr luaState, int n);

		public static void lua_pop(IntPtr luaState, int amount)
		{
			LuaDLL.lua_settop(luaState, -amount - 1);
		}

		public static void lua_newtable(IntPtr luaState)
		{
			LuaDLL.lua_createtable(luaState, 0, 0);
		}

		public static void lua_register(IntPtr luaState, string name, LuaCSFunction func)
		{
			LuaDLL.lua_pushcfunction(luaState, func);
			LuaDLL.lua_setglobal(luaState, name);
		}

		public static void lua_pushcfunction(IntPtr luaState, LuaCSFunction func)
		{
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(func);
			LuaDLL.lua_pushcclosure(luaState, functionPointerForDelegate, 0);
		}

		public static bool lua_isfunction(IntPtr luaState, int n)
		{
			return LuaDLL.lua_type(luaState, n) == LuaTypes.LUA_TFUNCTION;
		}

		public static bool lua_istable(IntPtr luaState, int n)
		{
			return LuaDLL.lua_type(luaState, n) == LuaTypes.LUA_TTABLE;
		}

		public static bool lua_islightuserdata(IntPtr luaState, int n)
		{
			return LuaDLL.lua_type(luaState, n) == LuaTypes.LUA_TLIGHTUSERDATA;
		}

		public static bool lua_isnil(IntPtr luaState, int n)
		{
			return LuaDLL.lua_type(luaState, n) == LuaTypes.LUA_TNIL;
		}

		public static bool lua_isboolean(IntPtr luaState, int n)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(luaState, n);
			return luaTypes == LuaTypes.LUA_TBOOLEAN || luaTypes == LuaTypes.LUA_TNIL;
		}

		public static bool lua_isthread(IntPtr luaState, int n)
		{
			return LuaDLL.lua_type(luaState, n) == LuaTypes.LUA_TTHREAD;
		}

		public static bool lua_isnone(IntPtr luaState, int n)
		{
			return LuaDLL.lua_type(luaState, n) == LuaTypes.LUA_TNONE;
		}

		public static bool lua_isnoneornil(IntPtr luaState, int n)
		{
			return LuaDLL.lua_type(luaState, n) <= LuaTypes.LUA_TNIL;
		}

		public static void lua_setglobal(IntPtr luaState, string name)
		{
			LuaDLL.lua_setfield(luaState, LuaIndexes.LUA_GLOBALSINDEX, name);
		}

		public static void lua_getglobal(IntPtr luaState, string name)
		{
			LuaDLL.lua_getfield(luaState, LuaIndexes.LUA_GLOBALSINDEX, name);
		}

		public static string lua_ptrtostring(IntPtr str, int len)
		{
			string text = Marshal.PtrToStringAnsi(str, len);
			if (text == null)
			{
				byte[] array = new byte[len];
				Marshal.Copy(str, array, 0, len);
				return Encoding.get_UTF8().GetString(array);
			}
			return text;
		}

		public static string lua_tostring(IntPtr luaState, int index)
		{
			int len = 0;
			IntPtr intPtr = LuaDLL.tolua_tolstring(luaState, index, out len);
			if (intPtr != IntPtr.Zero)
			{
				return LuaDLL.lua_ptrtostring(intPtr, len);
			}
			return null;
		}

		public static IntPtr lua_open()
		{
			return LuaDLL.luaL_newstate();
		}

		public static void lua_getregistry(IntPtr L)
		{
			LuaDLL.lua_pushvalue(L, LuaIndexes.LUA_REGISTRYINDEX);
		}

		public static int lua_getgccount(IntPtr L)
		{
			return LuaDLL.lua_gc(L, LuaGCOptions.LUA_GCCOUNT, 0);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_getstack(IntPtr L, int level, ref Lua_Debug ar);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_getinfo(IntPtr L, string what, ref Lua_Debug ar);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern string lua_getlocal(IntPtr L, ref Lua_Debug ar, int n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern string lua_setlocal(IntPtr L, ref Lua_Debug ar, int n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern string lua_getupvalue(IntPtr L, int funcindex, int n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern string lua_setupvalue(IntPtr L, int funcindex, int n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_sethook(IntPtr L, LuaHook func, int mask, int count);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern LuaHook lua_gethook(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_gethookmask(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int lua_gethookcount(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void luaL_openlibs(IntPtr luaState);

		public static int abs_index(IntPtr L, int i)
		{
			return (i <= 0 && i > LuaIndexes.LUA_REGISTRYINDEX) ? (LuaDLL.lua_gettop(L) + i + 1) : i;
		}

		public static int luaL_getn(IntPtr luaState, int i)
		{
			return LuaDLL.tolua_getn(luaState, i);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaL_getmetafield(IntPtr luaState, int stackPos, string field);

		public static int luaL_callmeta(IntPtr L, int stackPos, string field)
		{
			stackPos = LuaDLL.abs_index(L, stackPos);
			if (LuaDLL.luaL_getmetafield(L, stackPos, field) == 0)
			{
				return 0;
			}
			LuaDLL.lua_pushvalue(L, stackPos);
			if (LuaDLL.lua_pcall(L, 1, 1, 0) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				LuaDLL.lua_pop(L, 1);
				throw new LuaException(msg, null, 1);
			}
			return 1;
		}

		public static int luaL_argerror(IntPtr L, int narg, string extramsg)
		{
			if (LuaDLL.tolua_argerror(L, narg, extramsg) != 0)
			{
				string msg = LuaDLL.lua_tostring(L, -1);
				LuaDLL.lua_pop(L, 1);
				throw new LuaException(msg, null, 1);
			}
			return 0;
		}

		public static int luaL_typerror(IntPtr L, int stackPos, string tname, string t2 = null)
		{
			if (t2 == null)
			{
				t2 = LuaDLL.luaL_typename(L, stackPos);
			}
			string extramsg = string.Format("{1} expected, got {2}", stackPos - 1, tname, t2);
			return LuaDLL.luaL_argerror(L, stackPos, extramsg);
		}

		public static string luaL_checklstring(IntPtr L, int numArg, out int len)
		{
			IntPtr intPtr = LuaDLL.tolua_tolstring(L, numArg, out len);
			if (intPtr == IntPtr.Zero)
			{
				LuaDLL.luaL_typerror(L, numArg, "string", null);
				return null;
			}
			return LuaDLL.lua_ptrtostring(intPtr, len);
		}

		public static string luaL_optlstring(IntPtr L, int narg, string def, out int len)
		{
			if (LuaDLL.lua_isnoneornil(L, narg))
			{
				len = ((def == null) ? 0 : def.get_Length());
				return def;
			}
			return LuaDLL.luaL_checklstring(L, narg, out len);
		}

		public static double luaL_checknumber(IntPtr L, int stackPos)
		{
			double num = LuaDLL.lua_tonumber(L, stackPos);
			if (num == 0.0 && LuaDLL.lua_isnumber(L, stackPos) == 0)
			{
				LuaDLL.luaL_typerror(L, stackPos, "number", null);
				return 0.0;
			}
			return num;
		}

		public static double luaL_optnumber(IntPtr L, int idx, double def)
		{
			if (LuaDLL.lua_isnoneornil(L, idx))
			{
				return def;
			}
			return LuaDLL.luaL_checknumber(L, idx);
		}

		public static int luaL_checkinteger(IntPtr L, int stackPos)
		{
			int num = LuaDLL.lua_tointeger(L, stackPos);
			if (num == 0 && LuaDLL.lua_isnumber(L, stackPos) == 0)
			{
				LuaDLL.luaL_typerror(L, stackPos, "number", null);
				return 0;
			}
			return num;
		}

		public static int luaL_optinteger(IntPtr L, int idx, int def)
		{
			if (LuaDLL.lua_isnoneornil(L, idx))
			{
				return def;
			}
			return LuaDLL.luaL_checkinteger(L, idx);
		}

		public static bool luaL_checkboolean(IntPtr luaState, int index)
		{
			if (LuaDLL.lua_isboolean(luaState, index))
			{
				return LuaDLL.lua_toboolean(luaState, index);
			}
			LuaDLL.luaL_typerror(luaState, index, "boolean", null);
			return false;
		}

		public static void luaL_checkstack(IntPtr L, int space, string mes)
		{
			if (LuaDLL.lua_checkstack(L, space) == 0)
			{
				throw new LuaException(string.Format("stack overflow (%s)", mes), null, 1);
			}
		}

		public static void luaL_checktype(IntPtr L, int narg, LuaTypes t)
		{
			if (LuaDLL.lua_type(L, narg) != t)
			{
				LuaDLL.luaL_typerror(L, narg, LuaDLL.lua_typename(L, t), null);
			}
		}

		public static void luaL_checkany(IntPtr L, int narg)
		{
			if (LuaDLL.lua_type(L, narg) == LuaTypes.LUA_TNONE)
			{
				LuaDLL.luaL_argerror(L, narg, "value expected");
			}
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaL_newmetatable(IntPtr luaState, string meta);

		public static IntPtr luaL_checkudata(IntPtr L, int ud, string tname)
		{
			IntPtr intPtr = LuaDLL.lua_touserdata(L, ud);
			if (intPtr != IntPtr.Zero && LuaDLL.lua_getmetatable(L, ud) != 0)
			{
				LuaDLL.lua_getfield(L, LuaIndexes.LUA_REGISTRYINDEX, tname);
				if (LuaDLL.lua_rawequal(L, -1, -2) != 0)
				{
					LuaDLL.lua_pop(L, 2);
					return intPtr;
				}
			}
			LuaDLL.luaL_typerror(L, ud, tname, null);
			return IntPtr.Zero;
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void luaL_where(IntPtr luaState, int level);

		public static int luaL_throw(IntPtr luaState, string message)
		{
			throw new LuaException(message, null, 2);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaL_ref(IntPtr luaState, int t);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void luaL_unref(IntPtr luaState, int registryIndex, int reference);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaL_loadfile(IntPtr luaState, string filename);

		public static int luaL_loadbuffer(IntPtr luaState, byte[] buff, int size, string name)
		{
			return LuaDLL.tolua_loadbuffer(luaState, buff, size, name);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaL_loadstring(IntPtr luaState, string chunk);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr luaL_newstate();

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr luaL_gsub(IntPtr luaState, string str, string pattern, string replacement);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr luaL_findtable(IntPtr luaState, int idx, string fname, int szhint = 1);

		public static string luaL_typename(IntPtr luaState, int stackPos)
		{
			LuaTypes type = LuaDLL.lua_type(luaState, stackPos);
			return LuaDLL.lua_typename(luaState, type);
		}

		public static int luaL_dofile(IntPtr luaState, string fileName)
		{
			int num = LuaDLL.luaL_loadfile(luaState, fileName);
			if (num != 0)
			{
				return num;
			}
			return LuaDLL.lua_pcall(luaState, 0, LuaDLL.LUA_MULTRET, 0);
		}

		public static int luaL_dostring(IntPtr luaState, string chunk)
		{
			int num = LuaDLL.luaL_loadstring(luaState, chunk);
			if (num != 0)
			{
				return num;
			}
			return LuaDLL.lua_pcall(luaState, 0, LuaDLL.LUA_MULTRET, 0);
		}

		public static void luaL_getmetatable(IntPtr luaState, string meta)
		{
			LuaDLL.lua_getfield(luaState, LuaIndexes.LUA_REGISTRYINDEX, meta);
		}

		public static int lua_ref(IntPtr luaState)
		{
			return LuaDLL.luaL_ref(luaState, LuaIndexes.LUA_REGISTRYINDEX);
		}

		public static void lua_getref(IntPtr luaState, int reference)
		{
			LuaDLL.lua_rawgeti(luaState, LuaIndexes.LUA_REGISTRYINDEX, reference);
		}

		public static void lua_unref(IntPtr luaState, int reference)
		{
			LuaDLL.luaL_unref(luaState, LuaIndexes.LUA_REGISTRYINDEX, reference);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_openlibs(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_openint64(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_openlualibs(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tolua_tag();

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_newudata(IntPtr luaState, int val);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_rawnetobj(IntPtr luaState, int obj);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_pushudata(IntPtr L, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_pushnewudata(IntPtr L, int metaRef, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_beginpcall(IntPtr L, int reference);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushtraceback(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_getvec2(IntPtr luaState, int stackPos, out float x, out float y);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_getvec3(IntPtr luaState, int stackPos, out float x, out float y, out float z);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_getvec4(IntPtr luaState, int stackPos, out float x, out float y, out float z, out float w);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_getclr(IntPtr luaState, int stackPos, out float r, out float g, out float b, out float a);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_getquat(IntPtr luaState, int stackPos, out float x, out float y, out float z, out float w);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_getlayermask(IntPtr luaState, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushvec2(IntPtr luaState, float x, float y);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushvec3(IntPtr luaState, float x, float y, float z);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushvec4(IntPtr luaState, float x, float y, float z, float w);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushquat(IntPtr luaState, float x, float y, float z, float w);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushclr(IntPtr luaState, float r, float g, float b, float a);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushlayermask(IntPtr luaState, int mask);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_isint64(IntPtr luaState, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern long tolua_toint64(IntPtr luaState, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushint64(IntPtr luaState, long n);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_setindex(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_setnewindex(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int toluaL_ref(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void toluaL_unref(IntPtr L, int reference);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tolua_getmainstate(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern LuaValueType tolua_getvaluetype(IntPtr L, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_createtable(IntPtr L, string fullPath, int szhint = 0);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_pushluatable(IntPtr L, string fullPath);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_beginmodule(IntPtr L, string name);

		public static void tolua_endmodule(IntPtr L)
		{
			LuaDLL.lua_pop(L, 1);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_beginclass(IntPtr L, string name, int baseMetaRef, int reference = 0);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_endclass(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_function(IntPtr L, string name, IntPtr fn);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tolua_tocbuffer(string name, int sz);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_freebuffer(IntPtr buffer);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_variable(IntPtr L, string name, IntPtr get, IntPtr set);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_constant(IntPtr L, string name, double val);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_beginenum(IntPtr L, string name);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_endenum(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_beginstaticclass(IntPtr L, string name);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_endstaticclass(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_require(IntPtr L, string fileName);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_getmetatableref(IntPtr L, int pos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_setflag(int bit, bool flag);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_isvptrtable(IntPtr L, int index);

		public static int toluaL_exception(IntPtr L, Exception e, string msg = null)
		{
			msg = ((msg != null) ? msg : e.get_Message());
			LuaException.luaStack = new LuaException(msg, e, 2);
			return LuaDLL.tolua_error(L, msg);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_loadbuffer(IntPtr luaState, byte[] buff, int size, string name);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool tolua_toboolean(IntPtr luaState, int index);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_tointeger(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tolua_tolstring(IntPtr luaState, int index, out int strLen);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushlstring(IntPtr luaState, byte[] str, int size);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_objlen(IntPtr luaState, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tolua_newuserdata(IntPtr luaState, int size);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_argerror(IntPtr luaState, int narg, string extramsg);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_error(IntPtr L, string msg);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_getfield(IntPtr L, int idx, string key);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_setfield(IntPtr L, int idx, string key);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_gettable(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_settable(IntPtr luaState, int idx);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_getn(IntPtr luaState, int stackPos);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_strlen(IntPtr str);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushcclosure(IntPtr L, IntPtr fn);

		public static void tolua_pushcfunction(IntPtr luaState, LuaCSFunction func)
		{
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(func);
			LuaDLL.tolua_pushcclosure(luaState, functionPointerForDelegate);
		}

		public static string tolua_findtable(IntPtr L, int idx, string name, int size = 1)
		{
			int top = LuaDLL.lua_gettop(L);
			IntPtr intPtr = LuaDLL.luaL_findtable(L, idx, name, size);
			if (intPtr != IntPtr.Zero)
			{
				LuaDLL.lua_settop(L, top);
				int len = LuaDLL.tolua_strlen(intPtr);
				return LuaDLL.lua_ptrtostring(intPtr, len);
			}
			return null;
		}

		public static IntPtr tolua_atpanic(IntPtr L, LuaCSFunction func)
		{
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(func);
			return LuaDLL.lua_atpanic(L, functionPointerForDelegate);
		}

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr tolua_buffinit(IntPtr luaState);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_addlstring(IntPtr b, string str, int l);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_addstring(IntPtr b, string s);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_addchar(IntPtr b, byte s);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_pushresult(IntPtr b);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_update(IntPtr L, float deltaTime, float unscaledDelta);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_lateupdate(IntPtr L);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern int tolua_fixedupdate(IntPtr L, float fixedTime);

		[DllImport("tolua", CallingConvention = CallingConvention.Cdecl)]
		public static extern void tolua_regthis(IntPtr L, IntPtr get, IntPtr set);

		public static void tolua_bindthis(IntPtr L, LuaCSFunction get, LuaCSFunction set)
		{
			IntPtr get2 = IntPtr.Zero;
			IntPtr set2 = IntPtr.Zero;
			if (get != null)
			{
				get2 = Marshal.GetFunctionPointerForDelegate(get);
			}
			if (set != null)
			{
				set2 = Marshal.GetFunctionPointerForDelegate(set);
			}
			LuaDLL.tolua_regthis(L, get2, set2);
		}
	}
}
