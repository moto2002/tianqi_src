using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class RechargeManager : BaseSubSystemManager
{
	public class EventNames
	{
		public const string RechargeGoodsInfoUpdate = "RechargeManager.RechargeGoodsInfoUpdate";

		public const string OnPayTipChange = "OnRechargeTipChange";
	}

	public enum RechargeType
	{
		Diamond = 1,
		MonthCard,
		Box
	}

	private List<RechargeGoodsInfo> m_listRechargeGoodsInfo;

	private bool IsNeedRefresh = true;

	private Dictionary<int, RechargeInfo> RechargeDic = new Dictionary<int, RechargeInfo>();

	private List<int> RechargeIds = new List<int>();

	private List<MonthCardInfo> MonthCardInfos = new List<MonthCardInfo>();

	public bool IsShowTipsOfBox;

	private List<int> RechargeBoxs = new List<int>();

	private static RechargeManager instance;

	public List<RechargeGoodsInfo> listRechargeGoodsInfo
	{
		get
		{
			return this.m_listRechargeGoodsInfo;
		}
	}

	public static RechargeManager Instance
	{
		get
		{
			if (RechargeManager.instance == null)
			{
				RechargeManager.instance = new RechargeManager();
			}
			return RechargeManager.instance;
		}
	}

	private RechargeManager()
	{
	}

	private bool IsNeedRequestFromServer()
	{
		return this.m_listRechargeGoodsInfo == null || this.IsNeedRefresh;
	}

	public RechargeGoodsInfo GetRechargeGoodsInfo(int id)
	{
		if (this.m_listRechargeGoodsInfo == null)
		{
			Debug.LogError("m_listRechargeGoodsInfo is null");
			return null;
		}
		for (int i = 0; i < this.m_listRechargeGoodsInfo.get_Count(); i++)
		{
			if (this.m_listRechargeGoodsInfo.get_Item(i).ID == id)
			{
				return this.m_listRechargeGoodsInfo.get_Item(i);
			}
		}
		Debug.LogError("缺少充值方式数据:" + id);
		return null;
	}

	public RechargeGoodsInfo GetRechargeMonthGoodsInfo()
	{
		if (this.m_listRechargeGoodsInfo == null)
		{
			Debug.LogError("m_listRechargeGoodsInfo is null");
			return null;
		}
		for (int i = 0; i < this.m_listRechargeGoodsInfo.get_Count(); i++)
		{
			if (this.m_listRechargeGoodsInfo.get_Item(i).result == 1)
			{
				return this.m_listRechargeGoodsInfo.get_Item(i);
			}
		}
		Debug.LogError("缺少月卡投资充值方式数据");
		return null;
	}

	public MonthCardInfo GetMonthCardInfo(int id)
	{
		for (int i = 0; i < this.MonthCardInfos.get_Count(); i++)
		{
			if (this.MonthCardInfos.get_Item(i).id == id)
			{
				return this.MonthCardInfos.get_Item(i);
			}
		}
		return null;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.RechargeDic.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<RechargeGoodsNty>(new NetCallBackMethod<RechargeGoodsNty>(this.OnRechargeGoodsNty));
		NetworkManager.AddListenEvent<RechargeGoodsRes>(new NetCallBackMethod<RechargeGoodsRes>(this.OnRechargeGoodsRes));
		NetworkManager.AddListenEvent<RechargeInfosLoginPush>(new NetCallBackMethod<RechargeInfosLoginPush>(this.OnRechargeInfosLoginPush));
		NetworkManager.AddListenEvent<RechargeInfoChangeNty>(new NetCallBackMethod<RechargeInfoChangeNty>(this.OnRechargeInfoChangeNty));
		NetworkManager.AddListenEvent<RechargeDiamondRes>(new NetCallBackMethod<RechargeDiamondRes>(this.OnRechargeDiamondRes));
		NetworkManager.AddListenEvent<MonthCardInfoLoginPush>(new NetCallBackMethod<MonthCardInfoLoginPush>(this.OnMonthCardInfoLoginPush));
		NetworkManager.AddListenEvent<MonthCardChangeNty>(new NetCallBackMethod<MonthCardChangeNty>(this.OnMonthCardChangeNty));
		NetworkManager.AddListenEvent<BuyMonthCardRes>(new NetCallBackMethod<BuyMonthCardRes>(this.OnBuyMonthCardRes));
	}

	public void SendRechargeGoodsReq()
	{
		if (this.IsNeedRequestFromServer())
		{
			WaitUI.OpenUI(80000u);
			NetworkManager.Send(new RechargeGoodsReq(), ServerType.Data);
		}
	}

	private void SendRechargeDiamond(int rechargeId)
	{
		NetworkManager.Send(new RechargeDiamondReq
		{
			id = rechargeId
		}, ServerType.Data);
	}

	public void SendBuyMonthCard(int cardId)
	{
		NetworkManager.Send(new BuyMonthCardReq
		{
			id = cardId
		}, ServerType.Data);
	}

	private void OnRechargeGoodsNty(short state, RechargeGoodsNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsNeedRefresh = down.update;
			if (this.IsNeedRefresh)
			{
				this.m_listRechargeGoodsInfo = null;
			}
		}
	}

	private void OnRechargeGoodsRes(short state, RechargeGoodsRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.IsNeedRefresh = false;
			this.m_listRechargeGoodsInfo = new List<RechargeGoodsInfo>();
			int sDKType = SDKManager.Instance.GetSDKType();
			for (int i = 0; i < down.Info.get_Count(); i++)
			{
				if (down.Info.get_Item(i).channel == sDKType)
				{
					this.m_listRechargeGoodsInfo.Add(down.Info.get_Item(i));
				}
			}
			if (this.m_listRechargeGoodsInfo.get_Count() == 0)
			{
				for (int j = 0; j < down.Info.get_Count(); j++)
				{
					if (down.Info.get_Item(j).channel == 0)
					{
						this.m_listRechargeGoodsInfo.Add(down.Info.get_Item(j));
					}
				}
			}
			this.RechargeIds.Clear();
			this.RechargeBoxs.Clear();
			for (int k = 0; k < this.m_listRechargeGoodsInfo.get_Count(); k++)
			{
				RechargeGoodsInfo rechargeGoodsInfo = this.m_listRechargeGoodsInfo.get_Item(k);
				this.RechargeIds.Add(rechargeGoodsInfo.indexes);
				if (rechargeGoodsInfo.dropID.get_Count() > 0)
				{
					this.RechargeBoxs.Add(rechargeGoodsInfo.indexes);
				}
			}
			EventDispatcher.Broadcast("RechargeManager.RechargeGoodsInfoUpdate");
			this.CheckBoxTips();
		}
	}

	private void OnRechargeInfosLoginPush(short state, RechargeInfosLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.rechargeInfos != null)
		{
			FirstPayManager.Instance.FirstPayRechargeInfo = down.info;
			FirstPayManager.Instance.AllRechargeDiamond = down.rechargeDiamond;
			this.RechargeDic.Clear();
			for (int i = 0; i < down.rechargeInfos.get_Count(); i++)
			{
				RechargeInfo rechargeInfo = down.rechargeInfos.get_Item(i);
				this.RechargeDic.set_Item(rechargeInfo.id, rechargeInfo);
			}
			this.CheckBoxTips();
		}
	}

	private void OnRechargeInfoChangeNty(short state, RechargeInfoChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.rechargeInfos != null)
		{
			for (int i = 0; i < down.rechargeInfos.get_Count(); i++)
			{
				RechargeInfo rechargeInfo = down.rechargeInfos.get_Item(i);
				if (this.RechargeIds.Contains(rechargeInfo.id))
				{
					this.RechargeDic.set_Item(rechargeInfo.id, rechargeInfo);
				}
			}
		}
		if (PrivilegeUIViewModel.Instance != null)
		{
			PrivilegeUIViewModel.Instance.RefreshRechargePages();
		}
	}

	private void OnRechargeDiamondRes(short state, RechargeDiamondRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnMonthCardInfoLoginPush(short state, MonthCardInfoLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.monthCardInfos != null)
		{
			this.MonthCardInfos = down.monthCardInfos;
		}
	}

	private void OnMonthCardChangeNty(short state, MonthCardChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && down.monthCardInfos != null)
		{
			for (int i = 0; i < down.monthCardInfos.get_Count(); i++)
			{
				for (int j = 0; j < this.MonthCardInfos.get_Count(); j++)
				{
					if (this.MonthCardInfos.get_Item(j).id == down.monthCardInfos.get_Item(i).id)
					{
						this.MonthCardInfos.RemoveAt(j);
						break;
					}
				}
			}
			for (int k = 0; k < down.monthCardInfos.get_Count(); k++)
			{
				this.MonthCardInfos.Add(down.monthCardInfos.get_Item(k));
			}
		}
		if (PrivilegeUIViewModel.Instance != null)
		{
			PrivilegeUIViewModel.Instance.RefreshRechargePages();
		}
	}

	private void OnBuyMonthCardRes(short state, BuyMonthCardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (PrivilegeUIViewModel.Instance != null)
		{
			PrivilegeUIViewModel.Instance.RefreshRechargePages();
		}
	}

	public void CheckBoxTips()
	{
		if (this.listRechargeGoodsInfo == null)
		{
			return;
		}
		bool tipOfBox = false;
		for (int i = 0; i < this.RechargeBoxs.get_Count(); i++)
		{
			if (this.GetTodayRechargeCount(this.RechargeBoxs.get_Item(i)) == 0)
			{
				tipOfBox = true;
				break;
			}
		}
		this.SetTipOfBox(tipOfBox);
	}

	public void SetTipOfBox(bool value)
	{
		bool flag = this.IsShowTipsOfBox != value;
		if (flag)
		{
			this.IsShowTipsOfBox = value;
			EventDispatcher.Broadcast("OnRechargeTipChange");
		}
	}

	public bool IsFirstRecharge(int id)
	{
		if (this.RechargeDic != null)
		{
			RechargeInfo rechargeInfo = null;
			if (this.RechargeDic.TryGetValue(id, ref rechargeInfo))
			{
				return rechargeInfo.num <= 0;
			}
		}
		return true;
	}

	public int GetTodayRechargeCount(int id)
	{
		List<RechargeGoodsInfo> listRechargeGoodsInfo = RechargeManager.Instance.listRechargeGoodsInfo;
		int sDKType = SDKManager.Instance.GetSDKType();
		for (int i = 0; i < listRechargeGoodsInfo.get_Count(); i++)
		{
			if (id == listRechargeGoodsInfo.get_Item(i).ID && sDKType == listRechargeGoodsInfo.get_Item(i).channel)
			{
				id = listRechargeGoodsInfo.get_Item(i).indexes;
			}
		}
		if (this.RechargeDic != null)
		{
			RechargeInfo rechargeInfo = null;
			if (this.RechargeDic.TryGetValue(id, ref rechargeInfo))
			{
				return rechargeInfo.dayNum;
			}
		}
		return 0;
	}

	public void ExecutionToRechargeDiamond(int id)
	{
		RechargeGoodsInfo rechargeGoodsInfo = this.GetRechargeGoodsInfo(id);
		if (rechargeGoodsInfo == null)
		{
			return;
		}
		if (SDKManager.Instance.HasSDK())
		{
			if (SDKManager.Instance.IsAndroidYSDK())
			{
				WaitUI.OpenUI(8000u);
				SDKManager.Instance.CheckBalanceOnPay(rechargeGoodsInfo.ID.ToString(), "钻石", (double)rechargeGoodsInfo.rmb);
			}
			else
			{
				WaitUI.OpenUI(5000u);
				SDKManager.Instance.Pay(rechargeGoodsInfo.ID.ToString(), "钻石", (double)rechargeGoodsInfo.rmb);
			}
		}
		else
		{
			WaitUI.OpenUI(3000u);
			this.SendRechargeDiamond(id);
		}
	}
}
