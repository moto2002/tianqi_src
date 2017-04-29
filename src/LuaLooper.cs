using LuaInterface;
using System;
using UnityEngine;

public class LuaLooper : MonoBehaviour
{
	public LuaState luaState;

	public LuaEvent UpdateEvent
	{
		get;
		private set;
	}

	public LuaEvent LateUpdateEvent
	{
		get;
		private set;
	}

	public LuaEvent FixedUpdateEvent
	{
		get;
		private set;
	}

	private void Start()
	{
		try
		{
			this.UpdateEvent = this.GetEvent("UpdateBeat");
			this.LateUpdateEvent = this.GetEvent("LateUpdateBeat");
			this.FixedUpdateEvent = this.GetEvent("FixedUpdateBeat");
		}
		catch (Exception ex)
		{
			Object.Destroy(this);
			throw ex;
		}
	}

	public void Destroy()
	{
		if (this.luaState != null)
		{
			if (this.UpdateEvent != null)
			{
				this.UpdateEvent.Dispose();
				this.UpdateEvent = null;
			}
			if (this.LateUpdateEvent != null)
			{
				this.LateUpdateEvent.Dispose();
				this.LateUpdateEvent = null;
			}
			if (this.FixedUpdateEvent != null)
			{
				this.FixedUpdateEvent.Dispose();
				this.FixedUpdateEvent = null;
			}
			this.luaState = null;
		}
	}

	private void OnDestroy()
	{
		if (this.luaState != null)
		{
			this.Destroy();
		}
	}

	private void Update()
	{
		if (this.luaState.LuaUpdate(Time.get_deltaTime(), Time.get_unscaledDeltaTime()) != 0)
		{
			this.ThrowException();
		}
		this.luaState.LuaPop(1);
		this.luaState.Collect();
	}

	private void LateUpdate()
	{
		if (this.luaState.LuaLateUpdate() != 0)
		{
			this.ThrowException();
		}
		this.luaState.LuaPop(1);
	}

	private void FixedUpdate()
	{
		if (this.luaState.LuaFixedUpdate(Time.get_fixedDeltaTime()) != 0)
		{
			this.ThrowException();
		}
		this.luaState.LuaPop(1);
	}

	private LuaEvent GetEvent(string name)
	{
		LuaTable table = this.luaState.GetTable(name, true);
		if (table == null)
		{
			throw new LuaException(string.Format("Lua table {0} not exists", name), null, 1);
		}
		LuaEvent result = new LuaEvent(table);
		table.Dispose();
		return result;
	}

	private void ThrowException()
	{
		string msg = this.luaState.LuaToString(-1);
		this.luaState.LuaPop(2);
		Exception luaStack = LuaException.luaStack;
		LuaException.luaStack = null;
		throw new LuaException(msg, luaStack, 1);
	}
}
