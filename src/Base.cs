using LuaFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
	private AppFacade m_Facade;

	private LuaManager m_LuaMgr;

	private NetworkManager m_NetMgr;

	private SoundManager m_SoundMgr;

	private TimerManager m_TimerMgr;

	protected AppFacade facade
	{
		get
		{
			if (this.m_Facade == null)
			{
				this.m_Facade = AppFacade.Instance;
			}
			return this.m_Facade;
		}
	}

	protected LuaManager LuaManager
	{
		get
		{
			if (this.m_LuaMgr == null)
			{
				this.m_LuaMgr = this.facade.GetManager<LuaManager>("LuaManager");
			}
			return this.m_LuaMgr;
		}
	}

	protected NetworkManager NetManager
	{
		get
		{
			if (this.m_NetMgr == null)
			{
				this.m_NetMgr = this.facade.GetManager<NetworkManager>("NetworkManager");
			}
			return this.m_NetMgr;
		}
	}

	protected SoundManager _SoundManager
	{
		get
		{
			if (this.m_SoundMgr == null)
			{
				this.m_SoundMgr = this.facade.GetManager<SoundManager>("SoundManager");
			}
			return this.m_SoundMgr;
		}
	}

	protected TimerManager TimerManager
	{
		get
		{
			if (this.m_TimerMgr == null)
			{
				this.m_TimerMgr = this.facade.GetManager<TimerManager>("TimeManager");
			}
			return this.m_TimerMgr;
		}
	}

	protected void RegisterMessage(IView view, List<string> messages)
	{
		if (messages == null || messages.get_Count() == 0)
		{
			return;
		}
		Controller.Instance.RegisterViewCommand(view, messages.ToArray());
	}

	protected void RemoveMessage(IView view, List<string> messages)
	{
		if (messages == null || messages.get_Count() == 0)
		{
			return;
		}
		Controller.Instance.RemoveViewCommand(view, messages.ToArray());
	}
}
