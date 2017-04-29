using GameData;
using System;
using UnityEngine;

public class SelfAIControlManager : BaseSubSystemManager
{
	private static SelfAIControlManager instance;

	protected bool isUIAuto;

	protected int manualCount;

	public int beginDragTimes;

	public int finishDragTimes;

	public int buttonDownTimes;

	public int buttonUpTimes;

	protected uint recoverAITimerID;

	protected uint recoverAITime = 500u;

	public static SelfAIControlManager Instance
	{
		get
		{
			if (SelfAIControlManager.instance == null)
			{
				SelfAIControlManager.instance = new SelfAIControlManager();
			}
			return SelfAIControlManager.instance;
		}
	}

	public bool IsUIAuto
	{
		get
		{
			return this.isUIAuto;
		}
		set
		{
			Debug.Log(string.Concat(new object[]
			{
				"=====IsUIAuto: ",
				value,
				" ",
				this.ManualCount
			}));
			this.isUIAuto = value;
			TimerHeap.DelTimer(this.recoverAITimerID);
			if (this.ManualCount == 0)
			{
				if (value)
				{
					EventDispatcher.Broadcast(AIManagerEvent.SelfAIActive);
				}
				else
				{
					EventDispatcher.Broadcast(AIManagerEvent.SelfAIDeactive);
					EventDispatcher.Broadcast(SelfAIControlManagerEvent.StartAutoSetUIAuto);
				}
			}
		}
	}

	public int ManualCount
	{
		get
		{
			return this.manualCount;
		}
		set
		{
			if (this.manualCount == 0 && value > 0)
			{
				this.StopBattleAI();
			}
			this.manualCount = value;
			if (this.manualCount == 0)
			{
				this.RecoverBattleAI();
				if (!this.IsUIAuto)
				{
					EventDispatcher.Broadcast(SelfAIControlManagerEvent.StartAutoSetUIAuto);
				}
			}
			else
			{
				EventDispatcher.Broadcast(SelfAIControlManagerEvent.StopAutoSetUIAuto);
			}
		}
	}

	protected SelfAIControlManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.recoverAITime = (uint)float.Parse(DataReader<GlobalParams>.Get("recoverAITime").value);
	}

	protected override void AddListener()
	{
	}

	public override void Release()
	{
		this.IsUIAuto = false;
		TimerHeap.DelTimer(this.recoverAITimerID);
	}

	public void OnBeginDrag()
	{
		this.beginDragTimes++;
		this.ManualCount++;
	}

	public void OnFinishDrag()
	{
		this.finishDragTimes++;
		this.ManualCount--;
	}

	public void OnButtonDown()
	{
		this.buttonDownTimes++;
		this.ManualCount++;
	}

	public void OnButtonUp()
	{
		this.buttonUpTimes++;
		this.ManualCount--;
	}

	protected void StopBattleAI()
	{
		TimerHeap.DelTimer(this.recoverAITimerID);
		EventDispatcher.Broadcast(SelfAIControlManagerEvent.StopAutoSetUIAuto);
		EventDispatcher.Broadcast(AIManagerEvent.SelfAIDeactive);
	}

	protected void RecoverBattleAI()
	{
		this.recoverAITimerID = TimerHeap.AddTimer(this.recoverAITime, 0, delegate
		{
			if (this.IsUIAuto)
			{
				EventDispatcher.Broadcast(AIManagerEvent.SelfAIActive);
			}
		});
	}
}
