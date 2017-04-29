using Package;
using System;
using UnityEngine;
using XNetwork;

public class RankUpManager : BaseSubSystemManager
{
	protected static RankUpManager instance;

	protected bool hasInit;

	protected int rank;

	protected int curState;

	protected bool isWaitingRankUpResult;

	public static RankUpManager Instance
	{
		get
		{
			if (RankUpManager.instance == null)
			{
				RankUpManager.instance = new RankUpManager();
			}
			return RankUpManager.instance;
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

	public int Rank
	{
		get
		{
			return this.rank;
		}
		set
		{
			this.rank = value;
		}
	}

	public int CurState
	{
		get
		{
			return this.curState;
		}
		set
		{
			this.curState = value;
		}
	}

	public bool IsWaitingRankUpResult
	{
		get
		{
			return this.isWaitingRankUpResult;
		}
		protected set
		{
			this.isWaitingRankUpResult = value;
		}
	}

	protected RankUpManager()
	{
	}

	public override void Init()
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		base.Init();
	}

	public override void Release()
	{
		this.hasInit = false;
		this.rank = 0;
		this.isWaitingRankUpResult = false;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<AdvancedOccupationLoginPush>(new NetCallBackMethod<AdvancedOccupationLoginPush>(this.OnRankLoginPush));
		NetworkManager.AddListenEvent<AdvancedOccupationNty>(new NetCallBackMethod<AdvancedOccupationNty>(this.OnRankChangedNty));
		NetworkManager.AddListenEvent<AdvancedOccupationRes>(new NetCallBackMethod<AdvancedOccupationRes>(this.OnRankUpRes));
		EventDispatcher.AddListener(RankUpManagerEvent.AcceptTask, new Callback(this.AcceptTask));
		EventDispatcher.AddListener(RankUpManagerEvent.CommitTask, new Callback(this.CommitTask));
	}

	protected void OnRankLoginPush(short state, AdvancedOccupationLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.Rank = down.advancedTimes;
	}

	protected void RankUpReq()
	{
		this.BeginWait();
		NetworkManager.Send(new AdvancedOccupationReq
		{
			advancedStep = this.Rank + 1
		}, ServerType.Data);
	}

	protected void OnRankUpRes(short state, AdvancedOccupationRes down = null)
	{
		Debug.LogError("OnRankUpRes: " + state);
		this.TryEndWait();
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	protected void OnRankChangedNty(short state, AdvancedOccupationNty down = null)
	{
		Debug.LogError(string.Concat(new object[]
		{
			"OnRankChangedNty: ",
			state,
			" ",
			down.advancedTimes
		}));
		if (state != 0)
		{
			this.TryEndWait();
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.Rank = down.advancedTimes;
		this.TryUpdateRankUpUI();
		this.TryEndWait();
	}

	protected void BeginWait()
	{
		this.IsWaitingRankUpResult = true;
		WaitUI.OpenUI(15000u);
	}

	protected void TryEndWait()
	{
		if (this.IsWaitingRankUpResult)
		{
			WaitUI.CloseUI(0u);
			this.IsWaitingRankUpResult = false;
		}
	}

	public void OpenRankUpUI(int state)
	{
		this.CurState = state;
		RankUpUI rankUpUI = UIManagerControl.Instance.OpenUI("RankUpUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as RankUpUI;
		if (rankUpUI)
		{
			rankUpUI.SetData(this.Rank, this.GetRankUpUIState(this.CurState));
		}
	}

	protected void TryUpdateRankUpUI()
	{
		RankUpUI rankUpUI = UIManagerControl.Instance.GetUIIfExist("RankUpUI") as RankUpUI;
		if (!rankUpUI)
		{
			return;
		}
		if (this.IsWaitingRankUpResult)
		{
			rankUpUI.RankUp();
		}
		else
		{
			rankUpUI.SetData(this.Rank, this.GetRankUpUIState(this.CurState));
		}
	}

	protected RankUpUIState GetRankUpUIState(int state)
	{
		switch (state)
		{
		case 2:
			return RankUpUIState.AcceptTask;
		case 3:
			return RankUpUIState.TaskState;
		case 4:
			return RankUpUIState.CommitTask;
		default:
			return RankUpUIState.None;
		}
	}

	protected void AcceptTask()
	{
	}

	protected void CommitTask()
	{
		this.RankUpReq();
	}
}
