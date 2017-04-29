using GameData;
using Package;
using System;
using XNetwork;

public class TeamManager : BaseSubSystemManager
{
	private static TeamManager instance;

	private bool isQuick;

	public bool isMatching;

	public static TeamManager Instance
	{
		get
		{
			if (TeamManager.instance == null)
			{
				TeamManager.instance = new TeamManager();
			}
			return TeamManager.instance;
		}
	}

	private TeamManager()
	{
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
		NetworkManager.AddListenEvent<AnswerMatchRes>(new NetCallBackMethod<AnswerMatchRes>(this.OnAnswerMatchRes));
		NetworkManager.AddListenEvent<AutoMatchRes>(new NetCallBackMethod<AutoMatchRes>(this.OnAutoMatchRes));
		NetworkManager.AddListenEvent<TeamMatchStatusNty>(new NetCallBackMethod<TeamMatchStatusNty>(this.OnTeamMatchStatusNty));
		NetworkManager.AddListenEvent<AutoMatchNty>(new NetCallBackMethod<AutoMatchNty>(this.OnAutoMatchNty));
		NetworkManager.AddListenEvent<CancelAutoMatchRes>(new NetCallBackMethod<CancelAutoMatchRes>(this.OnCancelAutoMatchRes));
	}

	public void SendCancelAutoMatchReq()
	{
		NetworkManager.Send(new CancelAutoMatchReq(), ServerType.Data);
	}

	public void SendPveAutoMatchReq(AutoMatchType.ENUM type)
	{
		this.isQuick = (type == AutoMatchType.ENUM.Rule1);
		NetworkManager.Send(new AutoMatchReq
		{
			matchType = type
		}, ServerType.Data);
	}

	private void OnAutoMatchRes(short state, AutoMatchRes down = null)
	{
		if (state == 0)
		{
			EventDispatcher.Broadcast<bool>(EventNames.TeamAotoMatchSpecial, this.isQuick);
		}
		else
		{
			EventDispatcher.Broadcast<bool>(EventNames.TeamAotoMatchError, this.isQuick);
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnAutoMatchNty(short state, AutoMatchNty down = null)
	{
		if (down.retCode > 0)
		{
			StateManager.Instance.StateShow(state, 0);
			this.CloseMatchUI();
		}
	}

	private void OnCancelAutoMatchRes(short state, CancelAutoMatchRes down = null)
	{
		if (state == 0)
		{
			this.CloseMatchUI();
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnTeamMatchStatusNty(short state, TeamMatchStatusNty down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				switch (down.matchStatus)
				{
				case TeamMatchStatus.ENUM.AutoMatch:
					this.isMatching = true;
					EventDispatcher.Broadcast<bool>(EventNames.TeamAotoMatch, false);
					break;
				case TeamMatchStatus.ENUM.QuickEnter:
					this.isMatching = false;
					EventDispatcher.Broadcast<bool>(EventNames.TeamAotoMatch, true);
					break;
				case TeamMatchStatus.ENUM.LeaderInvite:
					EventDispatcher.Broadcast(EventNames.CloseMultiTimeUp);
					this.isMatching = false;
					break;
				}
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnAnswerMatchRes(short state, AnswerMatchRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void MatchEnd()
	{
		this.isMatching = false;
	}

	public void CloseMatchUI()
	{
		this.isMatching = false;
		UIManagerControl.Instance.UnLoadUIPrefab("MatchUI");
	}

	public void OpenQuickMatchUI()
	{
		if (!this.isQuick)
		{
			(UIManagerControl.Instance.OpenUI("MatchUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as MatchUI).SetDataOkAndCanle((int)(float.Parse(DataReader<MultiCopy>.Get("match_auto_time").value) * 0.001f), true, delegate
			{
				this.SendCancelAutoMatchReq();
			}, delegate
			{
				this.SendPveAutoMatchReq(AutoMatchType.ENUM.Rule1);
			}, new Action(this.MatchEnd), "快速进入");
		}
	}

	public MatchUI OpenMatchUI()
	{
		MatchUI matchUI = null;
		if (!this.isQuick)
		{
			this.isMatching = true;
			matchUI = (UIManagerControl.Instance.OpenUI("MatchUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as MatchUI);
			matchUI.SetDataCanle((int)(float.Parse(DataReader<MultiCopy>.Get("match_2_time").value + 5000) * 0.001f), true, delegate
			{
				this.SendCancelAutoMatchReq();
			}, new Action(this.MatchEnd));
		}
		return matchUI;
	}
}
