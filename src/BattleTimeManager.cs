using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class BattleTimeManager : BaseSubSystemManager
{
	protected static BattleTimeManager instance;

	protected bool isEnable;

	protected bool hasGotTime;

	protected bool isFirstGetTime;

	protected DateTime startTime;

	protected DateTime endTime;

	protected int instanceTime;

	protected int usedTime;

	protected int totalUsedTime;

	protected int leftTime;

	protected int uiTime;

	protected bool uiTimeDirty;

	protected bool isUITimeCountDown;

	protected List<Action<int>> currentTimeUIAction = new List<Action<int>>();

	public static BattleTimeManager Instance
	{
		get
		{
			if (BattleTimeManager.instance == null)
			{
				BattleTimeManager.instance = new BattleTimeManager();
			}
			return BattleTimeManager.instance;
		}
	}

	public bool IsEnable
	{
		get
		{
			return this.isEnable;
		}
		set
		{
			this.isEnable = value;
		}
	}

	public bool HasGotTime
	{
		get
		{
			return this.hasGotTime;
		}
		set
		{
			this.hasGotTime = value;
			if (value)
			{
				if (!this.IsFirstGetTime)
				{
					this.IsFirstGetTime = true;
				}
			}
			else
			{
				this.IsFirstGetTime = false;
			}
		}
	}

	public bool IsFirstGetTime
	{
		get
		{
			return this.isFirstGetTime;
		}
		set
		{
			this.isFirstGetTime = value;
		}
	}

	public DateTime StartTime
	{
		get
		{
			return this.startTime;
		}
		set
		{
			this.startTime = value;
		}
	}

	public DateTime EndTime
	{
		get
		{
			return this.endTime;
		}
		set
		{
			this.endTime = value;
		}
	}

	public int InstanceTime
	{
		get
		{
			return this.instanceTime;
		}
		set
		{
			this.instanceTime = value;
		}
	}

	public int UsedTime
	{
		get
		{
			return this.usedTime;
		}
		set
		{
			this.usedTime = value;
			EventDispatcher.Broadcast<float>("BattleDialogInstanceEscapeTime", (float)value);
			EventDispatcher.Broadcast<int>("GuideManager.InstanceOfTime", value);
		}
	}

	public int TotalUsedTime
	{
		get
		{
			return this.totalUsedTime;
		}
		set
		{
			this.totalUsedTime = value;
		}
	}

	public int LeftTime
	{
		get
		{
			return this.leftTime;
		}
		set
		{
			this.leftTime = value;
		}
	}

	public int LeftTimePercentage
	{
		get
		{
			return (this.InstanceTime != 0) ? (this.LeftTime / this.InstanceTime) : 0;
		}
	}

	public int UITime
	{
		get
		{
			return this.uiTime;
		}
		set
		{
			if (this.IsEnable && this.uiTime != value)
			{
				this.UITimeDirty = true;
			}
			this.uiTime = value;
		}
	}

	protected bool UITimeDirty
	{
		get
		{
			return this.uiTimeDirty;
		}
		set
		{
			this.uiTimeDirty = value;
		}
	}

	public bool IsUITimeCountDown
	{
		get
		{
			return this.isUITimeCountDown;
		}
		set
		{
			this.isUITimeCountDown = value;
		}
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<BattleTimeInfoRes>(new NetCallBackMethod<BattleTimeInfoRes>(this.OnBattleTimeInfoRes));
		NetworkManager.AddListenEvent<BattleTimeInfoNty>(new NetCallBackMethod<BattleTimeInfoNty>(this.OnBattleTimeInfoNty));
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	public void WaitForSetTime()
	{
		this.IsEnable = true;
		this.HasGotTime = false;
		this.InstanceTime = 0;
		this.UsedTime = 0;
		this.LeftTime = 0;
		this.TotalUsedTime = 0;
		this.UITime = 0;
	}

	public void ResetTimeToZero()
	{
		this.IsEnable = false;
		this.HasGotTime = false;
		this.IsFirstGetTime = false;
		this.InstanceTime = 0;
		this.UsedTime = 0;
		this.LeftTime = 0;
		this.TotalUsedTime = 0;
		this.UITime = 0;
	}

	protected void OnSecondsPast()
	{
		if (ClientGMManager.Instance.TimeSwitch00)
		{
			Debug.Log(string.Concat(new object[]
			{
				"OnSecondsPast: ",
				this.IsEnable,
				" ",
				this.HasGotTime,
				" ",
				InstanceManager.IsCurrentInBattle
			}));
		}
		if (!this.IsEnable || !this.HasGotTime || !InstanceManager.IsCurrentInBattle)
		{
			return;
		}
		this.UpdateTimeData();
		this.UpdateTimeUI();
	}

	protected void OnBattleTimeInfoRes(short state, BattleTimeInfoRes down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.ServerSetBattleTime(down.timeInfo);
			}
			else
			{
				Debug.LogError("=====BattleTimeInfoRes has an error===========");
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	protected void OnBattleTimeInfoNty(short state, BattleTimeInfoNty down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.ServerSetBattleTime(down.timeInfo);
			}
			else
			{
				Debug.LogError("=====BattleTimeInfoNty has an error===========");
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	public void ClientSetBattleTime(int instanceTime)
	{
		this.HasGotTime = true;
		this.StartTime = TimeManager.Instance.PreciseServerTime;
		this.EndTime = TimeManager.Instance.PreciseServerTime.AddSeconds((double)instanceTime);
		this.InitTimeData();
	}

	protected void ServerSetBattleTime(BattleTimeInfo info)
	{
		this.HasGotTime = true;
		TimeManager.Instance.ForceSyncTime(info.serverTick);
		this.StartTime = TimeManager.Instance.CalculateLocalServerTimeBySecond(info.battleBeginTick);
		this.EndTime = TimeManager.Instance.CalculateLocalServerTimeBySecond(info.battleEndTick);
		this.InitTimeData();
	}

	public void ServerSetBattleTime(int instanceAppointedEndTime)
	{
		this.HasGotTime = true;
		this.StartTime = TimeManager.Instance.CalculateLocalServerTimeBySecond(TimeManager.Instance.PreciseServerSecond);
		this.EndTime = TimeManager.Instance.CalculateLocalServerTimeBySecond(instanceAppointedEndTime);
		this.InitTimeData();
	}

	protected void InitTimeData()
	{
		this.InstanceTime = (int)(this.EndTime - this.StartTime).get_TotalSeconds();
		if (InstanceManager.IsClientCreate)
		{
			this.UsedTime = (int)(TimeManager.Instance.PreciseServerTime - this.StartTime).get_TotalSeconds();
			this.LeftTime = (int)(this.EndTime - TimeManager.Instance.PreciseServerTime).get_TotalSeconds();
		}
	}

	protected void UpdateTimeData()
	{
		if (InstanceManager.IsClientCreate)
		{
			if (!LocalInstanceHandler.Instance.IsPauseTimeEscape)
			{
				this.UsedTime = ((this.UsedTime + 1 <= this.InstanceTime) ? (this.UsedTime + 1) : this.InstanceTime);
				this.LeftTime = ((this.LeftTime - 1 >= 0) ? (this.LeftTime - 1) : 0);
				this.TotalUsedTime++;
			}
			this.UITime = ((!this.IsUITimeCountDown) ? this.UsedTime : this.LeftTime);
		}
		else if (InstanceManager.IsServerCreate)
		{
			int num = (int)(TimeManager.Instance.PreciseServerTime - this.StartTime).get_TotalSeconds();
			int num2 = (int)(this.EndTime - TimeManager.Instance.PreciseServerTime).get_TotalSeconds();
			this.UsedTime = ((num <= 0) ? 0 : ((num <= this.InstanceTime) ? num : this.InstanceTime));
			this.LeftTime = ((num <= 0) ? this.InstanceTime : ((num2 <= 0) ? 0 : num2));
			if (this.UsedTime > 0)
			{
				this.TotalUsedTime++;
			}
			this.UITime = ((!this.IsUITimeCountDown) ? this.UsedTime : this.LeftTime);
		}
	}

	public void AddCurrentTimeUIAction(Action<int> action)
	{
		if (action == null)
		{
			return;
		}
		if (!this.currentTimeUIAction.Contains(action))
		{
			this.currentTimeUIAction.Add(action);
		}
		action.Invoke(this.UITime);
	}

	public void RemoveCurrentTimeUIAction(Action<int> action)
	{
		if (action == null)
		{
			return;
		}
		if (this.currentTimeUIAction.Contains(action))
		{
			this.currentTimeUIAction.Remove(action);
		}
	}

	public void ClearCurrentTimeUIAction()
	{
		this.currentTimeUIAction.Clear();
	}

	public void UpdateTimeUI()
	{
		if (ClientGMManager.Instance.TimeSwitch00)
		{
			Debug.Log(string.Concat(new object[]
			{
				"UpdateTimeUI: ",
				this.currentTimeUIAction.get_Count(),
				" ",
				this.UITimeDirty,
				" ",
				this.UITime
			}));
		}
		if (this.UITimeDirty)
		{
			for (int i = 0; i < this.currentTimeUIAction.get_Count(); i++)
			{
				this.currentTimeUIAction.get_Item(i).Invoke(this.UITime);
			}
			this.UITimeDirty = false;
		}
	}
}
