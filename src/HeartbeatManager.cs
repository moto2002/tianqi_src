using GameData;
using System;

public class HeartbeatManager : BaseSubSystemManager
{
	protected static HeartbeatManager instance;

	protected bool hasInit;

	protected uint heartbeatTimer = 4294967295u;

	protected uint heartbeatCityInterval = 30000u;

	protected uint heartbeatBattleInterval = 3000u;

	public static HeartbeatManager Instance
	{
		get
		{
			if (HeartbeatManager.instance == null)
			{
				HeartbeatManager.instance = new HeartbeatManager();
			}
			return HeartbeatManager.instance;
		}
	}

	public bool HasInit
	{
		get
		{
			return this.hasInit;
		}
		protected set
		{
			this.hasInit = value;
		}
	}

	protected HeartbeatManager()
	{
	}

	public override void Init()
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		this.InitHeartbeat();
		base.Init();
	}

	public override void Release()
	{
		this.hasInit = false;
		TimerHeap.DelTimer(this.heartbeatTimer);
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener(HeartbeatManagerEvent.ForceSendHeartbeat, new Callback(this.ForceSendHeartbeat));
	}

	public void InitHeartbeat()
	{
		this.heartbeatCityInterval = (uint)(DataReader<WangLuoLianJie>.Get("HeartbeatCity").num * 1000);
		this.heartbeatBattleInterval = (uint)(DataReader<WangLuoLianJie>.Get("HeartbeatBattle").num * 1000);
		this.ForceSendHeartbeat();
	}

	protected void ForceSendHeartbeat()
	{
		this.SendHeartbeat();
		this.ResetDataServerHeartbeatTimer();
	}

	protected void SendHeartbeat()
	{
		NetworkManager.Instance.SendHeartbeat(ServerType.Data);
	}

	protected void ResetDataServerHeartbeatTimer()
	{
		TimerHeap.DelTimer(this.heartbeatTimer);
		this.heartbeatTimer = TimerHeap.AddTimer((!EntityWorld.Instance.IsEntitySelfInBattle) ? this.heartbeatCityInterval : this.heartbeatBattleInterval, 0, delegate
		{
			this.SendHeartbeat();
			this.ResetDataServerHeartbeatTimer();
		});
	}
}
