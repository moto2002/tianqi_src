using LuaInterface;
using System;
using UnityEngine;

namespace LuaFramework
{
	public class LuaManager : Manager
	{
		private LuaState lua;

		private LuaLoader loader;

		private LuaLooper loop;

		private void Awake()
		{
			this.loader = new LuaLoader();
			this.lua = new LuaState();
			this.OpenLibs();
			this.lua.LuaSetTop(0);
			LuaBinder.Bind(this.lua);
			LuaCoroutine.Register(this.lua, this);
		}

		public void InitStart()
		{
			this.InitLuaPath();
			this.InitLuaBundle();
			this.lua.Start();
			this.StartMain();
			this.StartLooper();
		}

		private void StartLooper()
		{
			this.loop = base.get_gameObject().AddComponent<LuaLooper>();
			this.loop.luaState = this.lua;
		}

		private void StartMain()
		{
			this.lua.DoFile("Main.lua");
			LuaFunction function = this.lua.GetFunction("Main", true);
			function.Call();
			function.Dispose();
		}

		private void OpenLibs()
		{
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_pb));
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_sproto_core));
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_protobuf_c));
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_lpeg));
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_cjson));
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_cjson_safe));
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_bit));
			this.lua.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_socket_core));
		}

		private void InitLuaPath()
		{
			if (Application.get_isMobilePlatform())
			{
				this.lua.AddSearchPath(Util.DataPath + "lua");
			}
			else
			{
				this.lua.AddSearchPath(Util.AppContentPath() + "lua");
			}
		}

		private void InitLuaBundle()
		{
			if (this.loader.beZip)
			{
				this.loader.AddBundle("Lua/Lua.unity3d");
				this.loader.AddBundle("Lua/Lua_math.unity3d");
				this.loader.AddBundle("Lua/Lua_system.unity3d");
				this.loader.AddBundle("Lua/Lua_u3d.unity3d");
				this.loader.AddBundle("Lua/Lua_Common.unity3d");
				this.loader.AddBundle("Lua/Lua_Logic.unity3d");
				this.loader.AddBundle("Lua/Lua_View.unity3d");
				this.loader.AddBundle("Lua/Lua_Controller.unity3d");
				this.loader.AddBundle("Lua/Lua_Misc.unity3d");
				this.loader.AddBundle("Lua/Lua_protobuf.unity3d");
				this.loader.AddBundle("Lua/Lua_3rd_cjson.unity3d");
				this.loader.AddBundle("Lua/Lua_3rd_luabitop.unity3d");
				this.loader.AddBundle("Lua/Lua_3rd_pbc.unity3d");
				this.loader.AddBundle("Lua/Lua_3rd_pblua.unity3d");
				this.loader.AddBundle("Lua/Lua_3rd_sproto.unity3d");
			}
		}

		public object[] DoFile(string filename)
		{
			return this.lua.DoFile(filename);
		}

		public object[] CallFunction(string funcName, params object[] args)
		{
			LuaFunction function = this.lua.GetFunction(funcName, true);
			if (function != null)
			{
				return function.Call(args);
			}
			return null;
		}

		public void LuaGC()
		{
			this.lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT, 0);
		}

		public void Close()
		{
			if (this.loop != null)
			{
				this.loop.Destroy();
				this.loop = null;
			}
			if (this.lua != null)
			{
				this.lua.Dispose();
				this.lua = null;
			}
			this.loader = null;
		}
	}
}
