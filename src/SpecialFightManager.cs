using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNetwork;

public class SpecialFightManager : BaseSubSystemManager
{
	private static SpecialFightManager instance;

	public SpecialFightMode SelectDetailMode;

	public bool isInDetailPanel;

	private SpecialFightCommonTableData commonData;

	private bool mIsResult = true;

	public static SpecialFightManager Instance
	{
		get
		{
			if (SpecialFightManager.instance == null)
			{
				SpecialFightManager.instance = new SpecialFightManager();
			}
			return SpecialFightManager.instance;
		}
	}

	public Dictionary<SpecialFightMode, SpecialFightInfo> SFightInfo
	{
		get;
		private set;
	}

	public int CurBuffCount
	{
		get;
		private set;
	}

	private SpecialFightManager()
	{
		this.SFightInfo = new Dictionary<SpecialFightMode, SpecialFightInfo>();
	}

	public override void Init()
	{
		base.Init();
		this.InitTable();
	}

	private void InitTable()
	{
		this.commonData = new SpecialFightCommonTableData();
		this.commonData.dungeonID = DataReader<FJingYanFuBenPeiZhi>.Get("dungeonId").num;
		this.commonData.priceList.AddRange(DataReader<FJingYanFuBenPeiZhi>.Get("price").date);
		this.commonData.itemIDs.AddRange(DataReader<FJingYanFuBenPeiZhi>.Get("ItemIds").date);
		this.commonData.itemNums.AddRange(DataReader<FJingYanFuBenPeiZhi>.Get("ItemNum").date);
		this.commonData.picture = DataReader<FJingYanFuBenPeiZhi>.Get("picture").num;
		this.commonData.descID = DataReader<FJingYanFuBenPeiZhi>.Get("descId").num;
	}

	public override void Release()
	{
		this.isInDetailPanel = false;
		this.CurBuffCount = 0;
		this.mIsResult = true;
		this.SFightInfo.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<ExperienceCopyInfoNty>(new NetCallBackMethod<ExperienceCopyInfoNty>(this.InitExperienceFightData));
		NetworkManager.AddListenEvent<ChallengeExperienceCopyRes>(new NetCallBackMethod<ChallengeExperienceCopyRes>(this.ChallengeExperienceRes));
		NetworkManager.AddListenEvent<ExtendExperienceCopyTimeRes>(new NetCallBackMethod<ExtendExperienceCopyTimeRes>(this.ExtendExperienceTimeRes));
		NetworkManager.AddListenEvent<BuyExperienceCopyBuffRes>(new NetCallBackMethod<BuyExperienceCopyBuffRes>(this.BuyExperienceBuffRes));
		NetworkManager.AddListenEvent<ExitExperienceCopyRes>(new NetCallBackMethod<ExitExperienceCopyRes>(this.ExitExperienceRes));
		NetworkManager.AddListenEvent<ResultExperienceCopyNty>(new NetCallBackMethod<ResultExperienceCopyNty>(this.ResultExperienceNty));
		NetworkManager.AddListenEvent<GetExperienceCopyRewardRes>(new NetCallBackMethod<GetExperienceCopyRewardRes>(this.OnGetExperienceCopyRewardRes));
	}

	private void InitExperienceFightData(short state, ExperienceCopyInfoNty down = null)
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
		SpecialFightMode specialFightMode = SpecialFightMode.Expericence;
		SpecialFightInfo specialFightInfo = this.GetFightInfo(specialFightMode);
		if (specialFightInfo != null)
		{
			specialFightInfo.m_Mode = specialFightMode;
			specialFightInfo.m_RestTimes = down.restChallengeTimes;
			specialFightInfo.m_ExtendTimes = down.extendTimes;
		}
		else
		{
			specialFightInfo = new SpecialFightInfo
			{
				m_Mode = specialFightMode,
				m_RestTimes = down.restChallengeTimes,
				m_ExtendTimes = down.extendTimes,
				m_InstanceID = DataReader<FJingYanFuBenPeiZhi>.Get("dungeonId").num
			};
			this.SFightInfo.Add(specialFightMode, specialFightInfo);
		}
		this.BroadcastRefreshEvent();
		EventDispatcher.Broadcast(EventNames.UpdateSpecialInstanceDetailUI);
		if (UIManagerControl.Instance.IsOpen("DailyTaskUI"))
		{
			EventDispatcher.Broadcast(EventNames.DailyTaskNty);
		}
	}

	public bool CanShowBuyExperienceTimesInDailyTask()
	{
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(SpecialFightMode.Expericence) as SpecialFightInfo;
		return specialFightInfo != null && specialFightInfo.m_RestTimes <= 0;
	}

	public bool IsFinishExperienceTimes()
	{
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(SpecialFightMode.Expericence) as SpecialFightInfo;
		if (specialFightInfo == null)
		{
			return false;
		}
		int maxVipTimesByType = VIPPrivilegeManager.Instance.GetMaxVipTimesByType(15);
		return specialFightInfo.m_ExtendTimes >= maxVipTimesByType;
	}

	public void OnBuyExperienceTimes()
	{
		SpecialFightInfo specialFightInfo = SpecialFightManager.GetSpecialFightInfo(SpecialFightMode.Expericence) as SpecialFightInfo;
		if (specialFightInfo == null)
		{
			return;
		}
		List<int> date = DataReader<FJingYanFuBenPeiZhi>.Get("price").date;
		List<int> date2 = DataReader<FJingYanFuBenPeiZhi>.Get("buyTimes").date;
		int extendTimes = specialFightInfo.m_ExtendTimes;
		int num = EntityWorld.Instance.EntSelf.VipLv;
		if (!VIPManager.Instance.IsVIPPrivilegeOn())
		{
			num = 0;
		}
		int num2 = 0;
		if (num <= 0 && VIPManager.Instance.IsVIPCardOn())
		{
			num2 = VIPPrivilegeManager.Instance.GetVipTimesByType(15);
		}
		int num3 = date2.get_Item(num);
		int num4 = Mathf.Max(num3 + num2 - extendTimes, 0);
		if (num4 <= 0 && extendTimes < date.get_Count())
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(505105, false), null, delegate
			{
				LinkNavigationManager.OpenVIPUI2Privilege();
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			return;
		}
		if (extendTimes >= date.get_Count() || num4 == 0)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(513531, false), GameDataUtils.GetChineseContent(513528, false), null, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
		}
		else
		{
			int num5 = date.get_Item(extendTimes);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(513531, false), string.Format(GameDataUtils.GetChineseContent(513530, false), num5, num4, num3), null, null, delegate
			{
				DialogBoxUIViewModel.Instance.BtnRclose = true;
				SpecialFightManager.Instance.ExtendExperienceTimeReq();
			}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null);
		}
	}

	public void ExtendExperienceTimeReq()
	{
		NetworkManager.Send(new ExtendExperienceCopyTimeReq(), ServerType.Data);
	}

	private void ExtendExperienceTimeRes(short state, ExtendExperienceCopyTimeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (UIManagerControl.Instance.IsOpen("DailyTaskUI"))
		{
			EventDispatcher.Broadcast(EventNames.DailyTaskNty);
		}
	}

	public void BuyExperienceCopyBuffReq(int id)
	{
		if (this.mIsResult)
		{
			Debug.Log("购买buff: " + id);
			this.mIsResult = false;
			NetworkManager.Send(new BuyExperienceCopyBuffReq
			{
				id = id
			}, ServerType.Data);
		}
	}

	private void BuyExperienceBuffRes(short state, BuyExperienceCopyBuffRes down = null)
	{
		this.mIsResult = true;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			EventDispatcher.Broadcast(EventNames.SpecialInstanceBuffFail);
			return;
		}
		this.CurBuffCount++;
		EventDispatcher.Broadcast(EventNames.BuySpecialBuffSuccess);
	}

	public void StartExperienceFight()
	{
		this.ChallengeExperienceReq();
	}

	private void ChallengeExperienceReq()
	{
		InstanceManager.SecurityCheck(delegate
		{
			WaitUI.OpenUI(0u);
			NetworkManager.Send(new ChallengeExperienceCopyReq(), ServerType.Data);
		}, null);
	}

	private void ChallengeExperienceRes(short state, ChallengeExperienceCopyRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public void ExitExperienceReq(bool isDouble = false)
	{
		NetworkManager.Send(new ExitExperienceCopyReq
		{
			doubleReward = isDouble
		}, ServerType.Data);
	}

	private void ExitExperienceRes(short state, ExitExperienceCopyRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.ExitExperienceSuccess);
		this.CurBuffCount = 0;
	}

	private void ResultExperienceNty(short state, ResultExperienceCopyNty down = null)
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
		ExperienceCopyInstance.Instance.GetInstanceResult(down);
	}

	public void SendGetExperienceCopyReward(bool isDouble = true)
	{
		NetworkManager.Send(new GetExperienceCopyRewardReq
		{
			doubleReward = isDouble
		}, ServerType.Data);
	}

	private void OnGetExperienceCopyRewardRes(short state, GetExperienceCopyRewardRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.GetExperienceCopyRewardRes);
	}

	public SpecialFightInfo GetFightInfo(SpecialFightMode mode)
	{
		if (!this.SFightInfo.ContainsKey(mode))
		{
			return null;
		}
		return this.SFightInfo.get_Item(mode);
	}

	private void BroadcastRefreshEvent()
	{
		SystemOpenManager.IsSystemOn(17);
		bool arg = false;
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsBattleTypeUISpecial, arg);
	}

	public static int GetSystemIDByMode(SpecialFightMode mode)
	{
		switch (mode)
		{
		case SpecialFightMode.Hold:
			return 54;
		case SpecialFightMode.Protect:
			return 55;
		case SpecialFightMode.Save:
			return 56;
		case SpecialFightMode.Expericence:
			return 63;
		default:
			return 0;
		}
	}

	public static SpecialFightModeGroup GetModeCroup(SpecialFightMode mode)
	{
		switch (mode)
		{
		case SpecialFightMode.Hold:
		case SpecialFightMode.Protect:
		case SpecialFightMode.Save:
			return SpecialFightModeGroup.Defend;
		case SpecialFightMode.Expericence:
			return SpecialFightModeGroup.Expericence;
		default:
			return (SpecialFightModeGroup)0;
		}
	}

	public static object GetSpecialFightInfo(SpecialFightMode mode)
	{
		SpecialFightModeGroup modeCroup = SpecialFightManager.GetModeCroup(mode);
		SpecialFightModeGroup specialFightModeGroup = modeCroup;
		if (specialFightModeGroup == SpecialFightModeGroup.Defend)
		{
			return DefendFightManager.Instance.GetModeInfo((DefendFightMode.DFMD)mode);
		}
		if (specialFightModeGroup != SpecialFightModeGroup.Expericence)
		{
			return null;
		}
		return SpecialFightManager.Instance.GetFightInfo(mode);
	}

	public static SpecialFightCommonTableData GetSpecialFightCommonTableData(SpecialFightMode mode)
	{
		SpecialFightModeGroup modeCroup = SpecialFightManager.GetModeCroup(mode);
		SpecialFightModeGroup specialFightModeGroup = modeCroup;
		if (specialFightModeGroup == SpecialFightModeGroup.Defend)
		{
			return DefendFightManager.Instance.GetSpecialFightCommonTableData((DefendFightMode.DFMD)mode);
		}
		if (specialFightModeGroup != SpecialFightModeGroup.Expericence)
		{
			return null;
		}
		return SpecialFightManager.Instance.commonData;
	}

	public long GetBatchExp(int batch)
	{
		if (batch > 0)
		{
			FJingYanFuBenBoCi data = DataReader<FJingYanFuBenBoCi>.Get(batch);
			if (data != null)
			{
				DiaoLuo diaoLuo = Enumerable.FirstOrDefault<DiaoLuo>(DataReader<DiaoLuo>.DataList, (DiaoLuo t) => t.ruleId == data.rewardId && t.minLv == EntityWorld.Instance.EntSelf.Lv);
				if (diaoLuo != null)
				{
					return diaoLuo.minNum;
				}
			}
		}
		return 0L;
	}
}
