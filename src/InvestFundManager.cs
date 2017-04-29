using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class InvestFundManager : BaseSubSystemManager
{
	public bool hasBuy;

	public bool hasGet;

	public List<RewardInfo> itemList = new List<RewardInfo>();

	public bool IsShowTipsOfCanBuy;

	public bool IsShowTipsOfCanGet;

	private static InvestFundManager m_instance;

	public static InvestFundManager Instance
	{
		get
		{
			if (InvestFundManager.m_instance == null)
			{
				InvestFundManager.m_instance = new InvestFundManager();
			}
			return InvestFundManager.m_instance;
		}
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.hasBuy = false;
		this.itemList.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<PushFundInfo>(new NetCallBackMethod<PushFundInfo>(this.OnPushFundInfo));
		NetworkManager.AddListenEvent<GetFundRewardRes>(new NetCallBackMethod<GetFundRewardRes>(this.OnGetFundRewardRes));
		NetworkManager.AddListenEvent<GetFundDiamondRes>(new NetCallBackMethod<GetFundDiamondRes>(this.OnGetFundDiamondRes));
	}

	public void SendGetFundRewardReq(int day)
	{
		NetworkManager.Send(new GetFundRewardReq
		{
			Id = day
		}, ServerType.Data);
	}

	public void SendGetFundDiamondReq()
	{
		NetworkManager.Send(new GetFundDiamondReq(), ServerType.Data);
	}

	public void OnPushFundInfo(short state, PushFundInfo msg = null)
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
		this.hasBuy = msg.hasBuy;
		this.hasGet = msg.hasGet;
		this.itemList.Clear();
		this.itemList.AddRange(msg.items);
		this.CheckInvestTips();
		EventDispatcher.Broadcast(EventNames.OnInvestPushInfo);
	}

	public void OnGetFundRewardRes(short state, GetFundRewardRes msg = null)
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
	}

	public void OnGetFundDiamondRes(short state, GetFundDiamondRes msg = null)
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
		this.hasGet = true;
	}

	public RewardInfo GetInvestItemInfo(int day)
	{
		return this.itemList.Find((RewardInfo a) => a.Id == day);
	}

	public void CheckInvestTips()
	{
		this.CheckCanBuyFlag(false);
		this.CheckCanGetFlag(false);
		EventDispatcher.Broadcast("OnRechargeTipChange");
	}

	public void CheckCanGetFlag(bool isCast = false)
	{
		bool flag = false;
		if (this.hasBuy)
		{
			using (List<RewardInfo>.Enumerator enumerator = this.itemList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RewardInfo current = enumerator.get_Current();
					flag = (current.canGet && !current.hadGet && !current.overdue);
					if (flag)
					{
						break;
					}
				}
			}
		}
		if (isCast)
		{
			this.SetTipOfCanGet(flag);
		}
		else
		{
			this.IsShowTipsOfCanGet = flag;
		}
	}

	public void SetTipOfCanGet(bool value)
	{
		bool flag = this.IsShowTipsOfCanGet != value;
		if (flag)
		{
			this.IsShowTipsOfCanGet = value;
			EventDispatcher.Broadcast("OnRechargeTipChange");
		}
	}

	public void CheckCanBuyFlag(bool isCast = false)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		bool flag = !this.hasBuy;
		if (flag)
		{
			XiTongCanShu xiTongCanShu = DataReader<XiTongCanShu>.Get("openLv");
			if (int.Parse(xiTongCanShu.value) > EntityWorld.Instance.EntSelf.Lv)
			{
				flag = false;
			}
		}
		if (isCast)
		{
			this.SetTipOfCanBuy(flag);
		}
		else
		{
			this.IsShowTipsOfCanBuy = flag;
		}
	}

	public void SetTipOfCanBuy(bool value)
	{
		bool flag = this.IsShowTipsOfCanBuy != value;
		if (flag)
		{
			this.IsShowTipsOfCanBuy = value;
			EventDispatcher.Broadcast("OnRechargeTipChange");
		}
	}
}
