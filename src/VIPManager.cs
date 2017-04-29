using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class VIPManager : BaseSubSystemManager
{
	public class VipType
	{
		public const int Limit = 1;

		public const int Detail = 2;

		public const int Recharge = 3;

		public const int Invest = 4;
	}

	public bool IsShowLimtCardTimeArrive = true;

	private List<VipEffectInfo> _VipEffectInfos = new List<VipEffectInfo>();

	private MonthCardInfoPush _LimitCardData = new MonthCardInfoPush();

	private float _vipExp;

	private List<VipBoxItemInfo> _VIPBoxs = new List<VipBoxItemInfo>();

	public int TotalRechargeNum;

	private static VIPManager instance;

	private bool isPushVipReq;

	public int MaxVipLevel
	{
		get;
		private set;
	}

	public List<VipEffectInfo> VipEffectInfos
	{
		get
		{
			return this._VipEffectInfos;
		}
		set
		{
			this._VipEffectInfos = value;
		}
	}

	public MonthCardInfoPush LimitCardData
	{
		get
		{
			return this._LimitCardData;
		}
		set
		{
			this._LimitCardData = value;
		}
	}

	public float vipExp
	{
		get
		{
			return this._vipExp;
		}
		set
		{
			this._vipExp = value;
		}
	}

	public List<VipBoxItemInfo> VIPBoxs
	{
		get
		{
			return this._VIPBoxs;
		}
		set
		{
			this._VIPBoxs = value;
		}
	}

	public static VIPManager Instance
	{
		get
		{
			if (VIPManager.instance == null)
			{
				VIPManager.instance = new VIPManager();
			}
			return VIPManager.instance;
		}
	}

	private VIPManager()
	{
		VIPPrivilegeManager.Instance.Init();
	}

	private void UpdateVipEffectInfo(VipEffectInfo newVEI)
	{
		for (int i = 0; i < this.VipEffectInfos.get_Count(); i++)
		{
			if (this.VipEffectInfos.get_Item(i).vipLv == newVEI.vipLv && this.VipEffectInfos.get_Item(i).effectId == newVEI.effectId)
			{
				this.VipEffectInfos.set_Item(i, newVEI);
			}
		}
	}

	public VipEffectInfo GetVipEffectInfo(int vipLv, int effectId)
	{
		for (int i = 0; i < this.VipEffectInfos.get_Count(); i++)
		{
			VipEffectInfo vipEffectInfo = this.VipEffectInfos.get_Item(i);
			if (vipEffectInfo.vipLv == vipLv && vipEffectInfo.effectId == effectId)
			{
				return vipEffectInfo;
			}
		}
		return null;
	}

	public List<VipBoxItemInfo> GetVIPBox(int effectId)
	{
		List<VipBoxItemInfo> list = new List<VipBoxItemInfo>();
		for (int i = 0; i < this.VIPBoxs.get_Count(); i++)
		{
			if (this.VIPBoxs.get_Item(i).effectId == effectId)
			{
				list.Add(this.VIPBoxs.get_Item(i));
			}
		}
		return list;
	}

	private bool IsFirstRecharge(int id)
	{
		return true;
	}

	public override void Init()
	{
		base.Init();
		this.MaxVipLevel = this.GetMaxVIP();
	}

	public override void Release()
	{
		this.VipEffectInfos.Clear();
		this.VIPBoxs.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<VipLoginPush>(new NetCallBackMethod<VipLoginPush>(this.OnVipLoginPush));
		NetworkManager.AddListenEvent<PushVipRes>(new NetCallBackMethod<PushVipRes>(this.OnPushVipRes));
		NetworkManager.AddListenEvent<VipChangedNty>(new NetCallBackMethod<VipChangedNty>(this.OnVipChangedNty));
		NetworkManager.AddListenEvent<OpenVipBoxRes>(new NetCallBackMethod<OpenVipBoxRes>(this.OnOpenVipBoxRes));
		NetworkManager.AddListenEvent<MonthCardInfoPush>(new NetCallBackMethod<MonthCardInfoPush>(this.OnMonthCardInfoPush));
		NetworkManager.AddListenEvent<VipExpChangedNty>(new NetCallBackMethod<VipExpChangedNty>(this.OnVipExpChangedNty));
		NetworkManager.AddListenEvent<GetSumRechargeRes>(new NetCallBackMethod<GetSumRechargeRes>(this.OnGetSumRechargeRes));
	}

	public void SendPushVip()
	{
		if (!this.isPushVipReq)
		{
			this.isPushVipReq = true;
			NetworkManager.Send(new PushVipReq(), ServerType.Data);
		}
	}

	public void SendOpenVipBox(int _effectId)
	{
		NetworkManager.Send(new OpenVipBoxReq
		{
			effectId = _effectId
		}, ServerType.Data);
	}

	public void SendLimitCardBuy(int cardId)
	{
		NetworkManager.Send(new BuyMonthCardReq
		{
			id = cardId
		}, ServerType.Data);
	}

	public void SendLimitCardTimeReq()
	{
		NetworkManager.Send(new MonthCardTimeReq(), ServerType.Data);
	}

	public void SendGetSumRechargeReq()
	{
		NetworkManager.Send(new GetSumRechargeReq(), ServerType.Data);
	}

	private void OnVipLoginPush(short state, VipLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.VipEffectInfos = down.effects;
			this.VIPBoxs = down.boxItems;
			if (PrivilegeUIViewModel.Instance != null && PrivilegeUIViewModel.Instance.get_gameObject().get_activeSelf())
			{
				PrivilegeUIViewModel.Instance.RefreshMode();
			}
		}
	}

	private void OnPushVipRes(short state, PushVipRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.VipEffectInfos = down.effects;
			this.VIPBoxs = down.boxItems;
			if (PrivilegeUIViewModel.Instance != null && PrivilegeUIViewModel.Instance.get_gameObject().get_activeSelf())
			{
				PrivilegeUIViewModel.Instance.RefreshMode();
			}
		}
	}

	private void OnVipChangedNty(short state, VipChangedNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.effects.get_Count(); i++)
			{
				this.UpdateVipEffectInfo(down.effects.get_Item(i));
			}
			EventDispatcher.Broadcast(EventNames.VipTimeLimitNty);
		}
	}

	private void OnOpenVipBoxRes(short state, OpenVipBoxRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		for (int i = 0; i < this.VipEffectInfos.get_Count(); i++)
		{
			if (this.VipEffectInfos.get_Item(i).effectId == down.effectId)
			{
				List<VipBoxItemInfo> vIPBox = this.GetVIPBox(down.effectId);
				if (vIPBox != null)
				{
					List<int> list = new List<int>();
					List<long> list2 = new List<long>();
					for (int j = 0; j < vIPBox.get_Count(); j++)
					{
						list.Add(vIPBox.get_Item(j).itemId);
						list2.Add((long)vIPBox.get_Item(j).itemCount);
					}
					RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
					rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, null);
				}
			}
		}
		if (PrivilegeUIViewModel.Instance != null && PrivilegeUIViewModel.Instance.get_gameObject().get_activeSelf())
		{
			PrivilegeUIViewModel.Instance.RefreshMode();
		}
	}

	private void OnMonthCardInfoPush(short state, MonthCardInfoPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.LimitCardData = down;
			if (this.IsVIPPrivilegeOn())
			{
				this.IsShowLimtCardTimeArrive = true;
			}
			else if (down.Times == 0)
			{
				this.IsShowLimtCardTimeArrive = false;
			}
			if (PrivilegeUIViewModel.Instance != null && PrivilegeUIViewModel.Instance.get_gameObject().get_activeSelf())
			{
				PrivilegeUIViewModel.Instance.RefreshVIPPanel();
			}
			EventDispatcher.Broadcast(EventNames.VipTimeLimitNty);
		}
	}

	private void OnVipExpChangedNty(short state, VipExpChangedNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.vipExp = down.vipExp;
			if (PrivilegeUIViewModel.Instance != null && PrivilegeUIViewModel.Instance.get_gameObject().get_activeSelf())
			{
				PrivilegeUIViewModel.Instance.RefreshVIPPanel();
			}
		}
	}

	private void OnGetSumRechargeRes(short state, GetSumRechargeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.TotalRechargeNum = down.rechargeNum;
			EventDispatcher.Broadcast(EventNames.OnGetSumRechargeRes);
		}
	}

	public int GetVIPNextLevel()
	{
		return EntityWorld.Instance.EntSelf.VipLv + 1;
	}

	public int GetVIPLevelDiamonds(int level)
	{
		int result = 0;
		VipDengJi vipDengJi = DataReader<VipDengJi>.Get(level);
		if (vipDengJi != null)
		{
			result = vipDengJi.diamonds;
		}
		return result;
	}

	public int GetNeedDiamondsToVIP(int level)
	{
		int vIPLevelDiamonds = this.GetVIPLevelDiamonds(level);
		if (vIPLevelDiamonds > EntityWorld.Instance.EntSelf.RechargeDiamond)
		{
			return vIPLevelDiamonds - EntityWorld.Instance.EntSelf.RechargeDiamond;
		}
		return 0;
	}

	public int GetNeedExpToVIP(int level)
	{
		int result = 0;
		VipDengJi vipDengJi = DataReader<VipDengJi>.Get(level);
		if (vipDengJi != null)
		{
			result = vipDengJi.vipExp;
		}
		return result;
	}

	public int ReachVIPLevel(int addDiamonds)
	{
		int diamonds = EntityWorld.Instance.EntSelf.RechargeDiamond + addDiamonds;
		return this.DiamondToVIPLevel(diamonds);
	}

	public int DiamondToVIPLevel(int diamonds)
	{
		List<VipDengJi> dataList = DataReader<VipDengJi>.DataList;
		int result = 0;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (diamonds < dataList.get_Item(i).diamonds)
			{
				break;
			}
			result = dataList.get_Item(i).level;
		}
		return result;
	}

	public int GetMaxVIP()
	{
		List<VipDengJi> dataList = DataReader<VipDengJi>.DataList;
		int num = 0;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).level > num)
			{
				num = dataList.get_Item(i).level;
			}
		}
		return num;
	}

	public bool GetCanShowTips()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return false;
		}
		bool result = false;
		for (int i = EntityWorld.Instance.EntSelf.VipLv; i > 0; i--)
		{
			VipXiaoGuo vipXiaoGuo = VIPPrivilegeManager.Instance.VIP2FirstObtainTreasure(i);
			if (vipXiaoGuo != null && !TreasureUIViewModel.IsTreasureObtain(i, vipXiaoGuo))
			{
				bool flag = TreasureUIViewModel.IsTreasureValid(i, vipXiaoGuo);
				if (flag)
				{
					return true;
				}
			}
		}
		return result;
	}

	public MonthCardInfoPush GetLimitCardData()
	{
		return this.LimitCardData;
	}

	public float GetVipExp()
	{
		return this.vipExp;
	}

	public bool IsVIPPrivilegeOn()
	{
		return this.LimitCardData != null && this.LimitCardData.Times > TimeManager.Instance.PreciseServerSecond;
	}

	public bool IsVIPCardOn()
	{
		return VipTasteCardManager.Instance.CardTime > TimeManager.Instance.PreciseServerSecond;
	}
}
