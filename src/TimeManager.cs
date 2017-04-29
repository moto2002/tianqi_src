using Package;
using System;
using System.Threading;
using UnityEngine;
using XNetwork;

public class TimeManager : BaseSubSystemManager
{
	protected static TimeManager instance;

	protected bool hasInit;

	protected int unscaleServerSecond;

	protected DateTime unscaleServerTime = DateTime.get_Now();

	protected int scaleServerSecond;

	protected DateTime scaleServerTime = DateTime.get_Now();

	protected int lastSyncServerSecond;

	protected DateTime lastSyncServerTime = DateTime.MinValue;

	protected DateTime ConstTimeOffset = new DateTime(1970, 1, 1, 0, 0, 0, 1);

	protected float syncTimeInterval = 30f;

	protected float lastSendSyncServerTime;

	protected Thread TimeThread;

	protected bool isOpenDebug;

	public static TimeManager Instance
	{
		get
		{
			if (TimeManager.instance == null)
			{
				TimeManager.instance = new TimeManager();
			}
			return TimeManager.instance;
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

	public int UnscaleServerSecond
	{
		get
		{
			return this.unscaleServerSecond;
		}
		protected set
		{
			this.unscaleServerSecond = value;
		}
	}

	public DateTime UnscaleServerTime
	{
		get
		{
			return this.unscaleServerTime;
		}
		protected set
		{
			this.unscaleServerTime = value;
		}
	}

	public int ScaleServerSecond
	{
		get
		{
			return this.scaleServerSecond;
		}
		protected set
		{
			this.scaleServerSecond = value;
		}
	}

	public DateTime ScaleServerTime
	{
		get
		{
			return this.scaleServerTime;
		}
		protected set
		{
			this.scaleServerTime = value;
		}
	}

	public int LastSyncServerSecond
	{
		get
		{
			return this.lastSyncServerSecond;
		}
		protected set
		{
			this.lastSyncServerSecond = value;
		}
	}

	public DateTime LastSyncServerTime
	{
		get
		{
			return this.lastSyncServerTime;
		}
		protected set
		{
			this.lastSyncServerTime = value;
		}
	}

	public int PreciseServerSecond
	{
		get
		{
			return this.UnscaleServerSecond;
		}
	}

	public DateTime PreciseServerTime
	{
		get
		{
			return this.UnscaleServerTime;
		}
	}

	public bool IsOpenDebug
	{
		protected get
		{
			return this.isOpenDebug;
		}
		set
		{
			this.isOpenDebug = value;
		}
	}

	protected TimeManager()
	{
	}

	public override void Init()
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		this.syncTimeInterval = 30f;
		this.SendSyncServerTimeReq();
		base.Init();
	}

	public override void Release()
	{
		if (this.TimeThread != null)
		{
			this.TimeThread.Abort();
			this.TimeThread = null;
		}
		this.hasInit = false;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ClientCheckTimeRes>(new NetCallBackMethod<ClientCheckTimeRes>(this.OnSyncServerTimeRes));
		EventDispatcher.AddListener("TimeManagerProtectedEvent.UnscaleSecondUpdate", new Callback(this.CalculateUnscaleServerTime));
		EventDispatcher.AddListener("TimeManagerProtectedEvent.ScaleSecondUpdate", new Callback(this.CalculateScaleServerTime));
	}

	protected void ServerSyncTime(int second)
	{
		this.UnscaleServerSecond = second;
		this.ScaleServerSecond = second;
		this.UnscaleServerTime = this.CalculateLocalServerTimeBySecond(second);
		this.ScaleServerTime = this.CalculateLocalServerTimeBySecond(second);
		if (this.IsOpenDebug)
		{
			Debug.Log("Sync ServerTime Before: " + this.LastSyncServerTime.ToString() + " After:" + this.UnscaleServerTime.ToString());
		}
		this.CheckCrossDay();
		this.LastSyncServerSecond = second;
		this.LastSyncServerTime = this.CalculateLocalServerTimeBySecond(second);
	}

	protected void CheckCrossDay()
	{
		if (this.LastSyncServerTime != DateTime.MinValue && this.LastSyncServerTime.get_Day() != this.UnscaleServerTime.get_Day())
		{
			EventDispatcher.Broadcast("TimeManagerEvent.ZeroPointTrigger");
		}
	}

	protected void CalculateUnscaleServerTime()
	{
		this.UnscaleServerSecond++;
		this.UnscaleServerTime = this.UnscaleServerTime.AddSeconds(1.0);
		EventDispatcher.Broadcast("TimeManagerEvent.UnscaleSecondPast");
	}

	protected void CalculateScaleServerTime()
	{
		this.ScaleServerSecond++;
		this.ScaleServerTime = this.ScaleServerTime.AddSeconds(1.0);
		EventDispatcher.Broadcast("TimeManagerEvent.ScaleSecondPast");
	}

	public void TimeUpdate(float timeSinceStartup)
	{
		if (!this.HasInit)
		{
			return;
		}
		this.TrySendSyncServerTime(timeSinceStartup, EntityWorld.Instance.EntSelf != null && EntityWorld.Instance.EntSelf.IsInBattle);
	}

	protected void TrySendSyncServerTime(float timeSinceStartup, bool isInBattle)
	{
		if (timeSinceStartup - this.lastSendSyncServerTime < this.syncTimeInterval)
		{
			return;
		}
		this.lastSendSyncServerTime = timeSinceStartup;
		this.SendSyncServerTimeReq();
	}

	protected void SendSyncServerTimeReq()
	{
		if (this.IsOpenDebug)
		{
			Debug.Log(string.Concat(new object[]
			{
				"SendSyncServerTimeReq: ",
				DateTime.get_Now().ToString(),
				" ",
				(int)(this.ScaleServerTime - TimeZone.get_CurrentTimeZone().ToLocalTime(this.ConstTimeOffset)).get_TotalSeconds(),
				" ",
				this.ScaleServerTime.ToString()
			}));
		}
		NetworkManager.Send(new ClientCheckTimeReq
		{
			clientTime = this.ScaleServerSecond,
			str = DateTime.get_Now().ToString()
		}, ServerType.Data);
	}

	protected void OnSyncServerTimeRes(short state, ClientCheckTimeRes down = null)
	{
		if (state != 0)
		{
			return;
		}
		if (down == null)
		{
			return;
		}
		this.ServerSyncTime(down.serverTime);
	}

	public void StartTimeRun()
	{
		this.TimeThread = new Thread(new ThreadStart(this.TimeRunning));
		this.TimeThread.Start();
	}

	public void StopTimeRun()
	{
		if (this.TimeThread != null)
		{
			this.TimeThread.Abort();
		}
	}

	protected void TimeRunning()
	{
	}

	public void ForceSyncTime(int second)
	{
		this.ServerSyncTime(second);
	}

	public void ForceSendSyncServerTime()
	{
		this.SendSyncServerTimeReq();
	}

	public DateTime CalculateServerTimeBySecond(int second)
	{
		return this.ConstTimeOffset.AddSeconds((double)second);
	}

	public DateTime CalculateLocalServerTimeBySecond(int second)
	{
		return TimeZone.get_CurrentTimeZone().ToLocalTime(this.ConstTimeOffset.AddSeconds((double)second));
	}

	public int GetTimeDiff(DateTime lastTime, DateTime preTime)
	{
		return (int)Math.Ceiling((lastTime - preTime).get_TotalMilliseconds());
	}

	public int GetRemainSecond(DateTime endTime)
	{
		int timeDiff = this.GetTimeDiff(endTime, this.UnscaleServerTime);
		return (timeDiff <= 0) ? 0 : (timeDiff / 1000);
	}

	public int GetRemainSecond(int endTime)
	{
		DateTime endTime2 = this.CalculateLocalServerTimeBySecond(endTime);
		return this.GetRemainSecond(endTime2);
	}
}
