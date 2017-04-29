using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class MushroomHitManager : BaseSubSystemManager
{
	public int gameTimes;

	public DaMoGu mushroomHitConfig;

	private static MushroomHitManager instance;

	public static MushroomHitManager Instance
	{
		get
		{
			if (MushroomHitManager.instance == null)
			{
				MushroomHitManager.instance = new MushroomHitManager();
			}
			return MushroomHitManager.instance;
		}
	}

	private MushroomHitManager()
	{
	}

	public override void Init()
	{
		base.Init();
		List<DaMoGu> dataList = DataReader<DaMoGu>.DataList;
		this.mushroomHitConfig = dataList.get_Item(0);
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GetHitMouseInfoRes>(new NetCallBackMethod<GetHitMouseInfoRes>(this.OnGetHitMouseInfoRes));
		NetworkManager.AddListenEvent<HitMouseStartRes>(new NetCallBackMethod<HitMouseStartRes>(this.OnHitMouseStartRes));
		NetworkManager.AddListenEvent<HitMouseSettleRes>(new NetCallBackMethod<HitMouseSettleRes>(this.OnHitMouseSettleRes));
		NetworkManager.AddListenEvent<GetHitMouseRankRes>(new NetCallBackMethod<GetHitMouseRankRes>(this.OnHitMouseRankRes));
	}

	public void SendGetHitMouseInfoReq()
	{
		NetworkManager.Send(new GetHitMouseInfoReq(), ServerType.Data);
	}

	public void SendGetHitMouseRankReq()
	{
		NetworkManager.Send(new GetHitMouseRankReq(), ServerType.Data);
	}

	public void OnGetHitMouseInfoRes(short state, GetHitMouseInfoRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		this.gameTimes = msg.hadTimes;
		EventDispatcher.Broadcast(EventNames.MushroomHitInfo);
	}

	public void SendHitMouseStartReq()
	{
		InstanceManager.SecurityCheck(delegate
		{
			NetworkManager.Send(new HitMouseStartReq(), ServerType.Data);
		}, null);
	}

	public void OnHitMouseStartRes(short state, HitMouseStartRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		EventDispatcher.Broadcast(EventNames.MushroomHitStart);
	}

	public void SendHitMouseSettleReq(int _score, int second)
	{
		NetworkManager.Send(new HitMouseSettleReq
		{
			score = _score,
			useSec = second
		}, ServerType.Data);
	}

	public void OnHitMouseSettleRes(short state, HitMouseSettleRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast(EventNames.MushroomHitError);
			return;
		}
		if (msg == null)
		{
			EventDispatcher.Broadcast(EventNames.MushroomHitError);
			return;
		}
		EventDispatcher.Broadcast<HitMouseSettleRes>(EventNames.MushroomHitResult, msg);
	}

	public void OnHitMouseRankRes(short state, GetHitMouseRankRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast(EventNames.MushroomHitError);
			return;
		}
		if (msg == null)
		{
			EventDispatcher.Broadcast(EventNames.MushroomHitError);
			return;
		}
		EventDispatcher.Broadcast<GetHitMouseRankRes>(EventNames.MushroomHitRankInfo, msg);
	}

	public static FenShuChengHao GetEvaluate(int score)
	{
		List<FenShuChengHao> dataList = DataReader<FenShuChengHao>.DataList;
		for (int i = dataList.get_Count() - 1; i >= 0; i--)
		{
			FenShuChengHao fenShuChengHao = dataList.get_Item(i);
			if (score >= fenShuChengHao.fraction)
			{
				return fenShuChengHao;
			}
		}
		return dataList.get_Item(0);
	}
}
