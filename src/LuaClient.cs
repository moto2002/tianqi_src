using LuaInterface;
using System;
using UnityEngine;

public class LuaClient : MonoBehaviour
{
	protected LuaState luaState;

	protected LuaLooper loop;

	protected LuaFunction levelLoaded;

	protected bool openLuaSocket;

	protected bool beZbStart;

	public static LuaClient Instance
	{
		get;
		protected set;
	}

	protected virtual LuaFileUtils InitLoader()
	{
		if (LuaFileUtils.Instance != null)
		{
			return LuaFileUtils.Instance;
		}
		return new LuaFileUtils();
	}

	protected virtual void LoadLuaFiles()
	{
		this.OnLoadFinished();
	}

	protected virtual void OpenLibs()
	{
		this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_pb));
		this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_struct));
		this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_lpeg));
		if (LuaConst.openLuaSocket)
		{
			this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_socket_core));
			this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_luasocket_scripts));
		}
	}

	public void OpenZbsDebugger(string ip = null)
	{
		if (!LuaConst.openLuaSocket)
		{
			LuaConst.openLuaSocket = true;
			this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_socket_core));
			this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_luasocket_scripts));
		}
		this.luaState.AddSearchPath(LuaConst.zbsDir);
		if (ip != null)
		{
			this.luaState.DoString(string.Format("require('mobdebug').start('{0}')", ip), "LuaState.DoString");
		}
		else
		{
			this.luaState.DoString("require('mobdebug').start()", "LuaState.DoString");
		}
	}

	protected void OpenCJson()
	{
		this.luaState.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
		this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_cjson));
		this.luaState.LuaSetField(-2, "cjson");
		this.luaState.OpenLibs(new LuaCSFunction(LuaDLL.luaopen_cjson_safe));
		this.luaState.LuaSetField(-2, "cjson.safe");
	}

	protected virtual void CallMain()
	{
		LuaFunction function = this.luaState.GetFunction("Main", true);
		function.Call();
		function.Dispose();
	}

	protected virtual void StartMain()
	{
		this.luaState.DoFile("Main.lua");
		this.levelLoaded = this.luaState.GetFunction("OnLevelWasLoaded", true);
		this.CallMain();
	}

	protected void StartLooper()
	{
		this.loop = base.get_gameObject().AddComponent<LuaLooper>();
		this.loop.luaState = this.luaState;
	}

	protected virtual void Bind()
	{
		LuaCoroutine.Register(this.luaState, this);
		LuaBinder.Bind(this.luaState);
	}

	protected void Init()
	{
		this.InitLoader();
		this.luaState = new LuaState();
		this.OpenLibs();
		this.luaState.LuaSetTop(0);
		this.Bind();
		this.LoadLuaFiles();
	}

	protected void Awake()
	{
		LuaClient.Instance = this;
		this.Init();
	}

	protected virtual void OnLoadFinished()
	{
		this.luaState.Start();
		this.StartLooper();
		this.StartMain();
	}

	protected void OnLevelWasLoaded(int level)
	{
		if (this.levelLoaded != null)
		{
			this.levelLoaded.BeginPCall();
			this.levelLoaded.Push((double)level);
			this.levelLoaded.PCall();
			this.levelLoaded.EndPCall();
		}
	}

	protected void Destroy()
	{
		if (this.luaState != null)
		{
			if (this.levelLoaded != null)
			{
				this.levelLoaded.Dispose();
				this.levelLoaded = null;
			}
			if (this.loop != null)
			{
				this.loop.Destroy();
				this.loop = null;
			}
			if (this.luaState != null)
			{
				this.luaState.Dispose();
				this.luaState = null;
			}
			LuaClient.Instance = null;
		}
	}

	protected void OnDestroy()
	{
		this.Destroy();
	}

	protected void OnApplicationQuit()
	{
		this.Destroy();
	}

	public static LuaState GetMainState()
	{
		return LuaClient.Instance.luaState;
	}

	public LuaLooper GetLooper()
	{
		return this.loop;
	}
}
