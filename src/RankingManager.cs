using Package;
using System;
using XNetwork;

public class RankingManager : BaseSubSystemManager
{
	public DateTime overTime;

	private GetRanInfoRes personal;

	private TimeCountDown remainingTime;

	private LvRankInfoRes lvRank;

	private FightingRankInfoRes fightingRank;

	private PetFRankInfoRes petFRank;

	private static RankingManager instance;

	public RankingType.ENUM current;

	public GetRanInfoRes Personal
	{
		get
		{
			return this.personal;
		}
	}

	public TimeCountDown RemainingTime
	{
		get
		{
			return this.remainingTime;
		}
	}

	public LvRankInfoRes LvRank
	{
		get
		{
			return this.lvRank;
		}
	}

	public FightingRankInfoRes FightingRank
	{
		get
		{
			return this.fightingRank;
		}
	}

	public PetFRankInfoRes PetFRank
	{
		get
		{
			return this.petFRank;
		}
	}

	public static RankingManager Instance
	{
		get
		{
			if (RankingManager.instance == null)
			{
				RankingManager.instance = new RankingManager();
			}
			return RankingManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.personal = null;
		this.lvRank = null;
		this.fightingRank = null;
		this.petFRank = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<LvRankInfoRes>(new NetCallBackMethod<LvRankInfoRes>(this.OnLvRankInfoRes));
		NetworkManager.AddListenEvent<FightingRankInfoRes>(new NetCallBackMethod<FightingRankInfoRes>(this.OnFightingRankInfoRes));
		NetworkManager.AddListenEvent<PetFRankInfoRes>(new NetCallBackMethod<PetFRankInfoRes>(this.OnPetFRankInfoRes));
		NetworkManager.AddListenEvent<GetRanInfoRes>(new NetCallBackMethod<GetRanInfoRes>(this.OnGetRanInfoRes));
	}

	public void SendGetRankInfoReq()
	{
		Action act = delegate
		{
			NetworkManager.Send(new GetRankInfoReq(), ServerType.Data);
		};
		ClientApp.Instance.DelayAction(0.2f, act);
	}

	public void SendRankInfoReq(RankingType.ENUM type)
	{
		NetworkManager.Send(new RankInfoReq
		{
			rankingType = (int)type
		}, ServerType.Data);
	}

	private void OnGetRanInfoRes(short state, GetRanInfoRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.personal = down;
			EventDispatcher.Broadcast(EventNames.OnRankingPersonalPush);
		}
	}

	private void OnLvRankInfoRes(short state, LvRankInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.lvRank = down;
			this.SetTime(down.remainTime);
			EventDispatcher.Broadcast(EventNames.OnRankingPush);
		}
	}

	private void OnFightingRankInfoRes(short state, FightingRankInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.fightingRank = down;
			this.SetTime(down.remainTime);
			EventDispatcher.Broadcast(EventNames.OnRankingPush);
		}
	}

	private void OnPetFRankInfoRes(short state, PetFRankInfoRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.petFRank = down;
			this.SetTime(down.remainTime);
			EventDispatcher.Broadcast(EventNames.OnRankingPush);
		}
	}

	private void SetTime(int remainTime)
	{
		if (this.remainingTime == null)
		{
			this.remainingTime = new TimeCountDown(remainTime, TimeFormat.HHMMSS, null, delegate
			{
				this.SendRankInfoReq(this.current);
				this.SendGetRankInfoReq();
				this.remainingTime.Dispose();
				this.remainingTime = null;
			}, true);
		}
		else
		{
			this.remainingTime.ResetSeconds(remainTime);
		}
	}

	public void OpenRankingUI()
	{
		UIManagerControl.Instance.OpenUI("RankingUI", null, false, UIType.FullScreen);
	}
}
