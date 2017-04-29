using GameData;
using Package;
using System;
using UnityEngine;
using XNetwork;

public class DefendFightManager : BaseSubSystemManager
{
	protected XDict<DefendFightMode.DFMD, SpecialFightCommonTableData> commonTableInfo = new XDict<DefendFightMode.DFMD, SpecialFightCommonTableData>();

	protected XDict<DefendFightMode.DFMD, DefendFightModeInfo> modeInfo = new XDict<DefendFightMode.DFMD, DefendFightModeInfo>();

	protected int bossRandomRate;

	protected DefendFightMode.DFMD selectDetailMode = DefendFightMode.DFMD.Hold;

	protected CurrentDefindFightData currentData;

	public int wave;

	protected static DefendFightManager instance;

	public bool IsShowTip
	{
		get
		{
			return SystemOpenManager.IsSystemOn(17) && (this.IsModeOpen(DefendFightMode.DFMD.Hold) || this.IsModeOpen(DefendFightMode.DFMD.Protect) || this.IsModeOpen(DefendFightMode.DFMD.Save));
		}
	}

	public XDict<DefendFightMode.DFMD, DefendFightModeInfo> ModeInfo
	{
		get
		{
			return this.modeInfo;
		}
	}

	public int BossRandomRate
	{
		get
		{
			return this.bossRandomRate;
		}
	}

	public DefendFightMode.DFMD SelectDetailMode
	{
		get
		{
			return this.selectDetailMode;
		}
		set
		{
			this.selectDetailMode = value;
		}
	}

	public static DefendFightManager Instance
	{
		get
		{
			if (DefendFightManager.instance == null)
			{
				DefendFightManager.instance = new DefendFightManager();
			}
			return DefendFightManager.instance;
		}
	}

	protected DefendFightManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.InitTable();
	}

	protected void InitTable()
	{
		this.commonTableInfo.Clear();
		SpecialFightCommonTableData specialFightCommonTableData = new SpecialFightCommonTableData();
		specialFightCommonTableData.dungeonID = DataReader<FShouHuShuiJingFuBenPeiZhi>.Get("dungeonId").num;
		specialFightCommonTableData.priceList.AddRange(DataReader<FShouHuShuiJingFuBenPeiZhi>.Get("price").date);
		specialFightCommonTableData.itemIDs.AddRange(DataReader<FShouHuShuiJingFuBenPeiZhi>.Get("ItemIds").date);
		specialFightCommonTableData.itemNums.AddRange(DataReader<FShouHuShuiJingFuBenPeiZhi>.Get("ItemNum").date);
		specialFightCommonTableData.picture = DataReader<FShouHuShuiJingFuBenPeiZhi>.Get("picture").num;
		specialFightCommonTableData.descID = DataReader<FShouHuShuiJingFuBenPeiZhi>.Get("descId").num;
		this.commonTableInfo.Add(DefendFightMode.DFMD.Hold, specialFightCommonTableData);
		SpecialFightCommonTableData specialFightCommonTableData2 = new SpecialFightCommonTableData();
		specialFightCommonTableData2.dungeonID = DataReader<FHuSongKuangCheFuBenPeiZhi>.Get("dungeonId").num;
		specialFightCommonTableData2.priceList.AddRange(DataReader<FHuSongKuangCheFuBenPeiZhi>.Get("price").date);
		specialFightCommonTableData2.itemIDs.AddRange(DataReader<FHuSongKuangCheFuBenPeiZhi>.Get("ItemIds").date);
		specialFightCommonTableData2.itemNums.AddRange(DataReader<FHuSongKuangCheFuBenPeiZhi>.Get("ItemNum").date);
		specialFightCommonTableData2.picture = DataReader<FHuSongKuangCheFuBenPeiZhi>.Get("picture").num;
		specialFightCommonTableData2.descID = DataReader<FHuSongKuangCheFuBenPeiZhi>.Get("descId").num;
		this.commonTableInfo.Add(DefendFightMode.DFMD.Protect, specialFightCommonTableData2);
		SpecialFightCommonTableData specialFightCommonTableData3 = new SpecialFightCommonTableData();
		specialFightCommonTableData3.dungeonID = DataReader<FXueCaiDianFengFuBenPeiZhi>.Get("dungeonId").num;
		specialFightCommonTableData3.priceList.AddRange(DataReader<FXueCaiDianFengFuBenPeiZhi>.Get("price").date);
		specialFightCommonTableData3.itemIDs.AddRange(DataReader<FXueCaiDianFengFuBenPeiZhi>.Get("ItemIds").date);
		specialFightCommonTableData3.itemNums.AddRange(DataReader<FXueCaiDianFengFuBenPeiZhi>.Get("ItemNum").date);
		specialFightCommonTableData3.picture = DataReader<FXueCaiDianFengFuBenPeiZhi>.Get("picture").num;
		specialFightCommonTableData3.descID = DataReader<FXueCaiDianFengFuBenPeiZhi>.Get("descId").num;
		this.commonTableInfo.Add(DefendFightMode.DFMD.Save, specialFightCommonTableData3);
	}

	public override void Release()
	{
		this.ModeInfo.Clear();
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<DefendFightInitNty>(new NetCallBackMethod<DefendFightInitNty>(this.InitDefendFightData));
		NetworkManager.AddListenEvent<DefendFightModeInfoNty>(new NetCallBackMethod<DefendFightModeInfoNty>(this.UpdateDefendFightData));
		NetworkManager.AddListenEvent<DefendFightBuyChallengeRes>(new NetCallBackMethod<DefendFightBuyChallengeRes>(this.OnDefendFightBuyChallengeRes));
		NetworkManager.AddListenEvent<DefendFightRes>(new NetCallBackMethod<DefendFightRes>(this.OnChallengeDefendFightRes));
		NetworkManager.AddListenEvent<DefendFightMatchStatusNty>(new NetCallBackMethod<DefendFightMatchStatusNty>(this.OnDefendFightMatchStatusNty));
		NetworkManager.AddListenEvent<DefendFightStepNty>(new NetCallBackMethod<DefendFightStepNty>(this.OnDefendFightStepNty));
		NetworkManager.AddListenEvent<DefendFightMonsterTipsNty>(new NetCallBackMethod<DefendFightMonsterTipsNty>(this.OnDefendFightMonsterTipsNty));
		NetworkManager.AddListenEvent<DefendFightCarReachTipsNty>(new NetCallBackMethod<DefendFightCarReachTipsNty>(this.OnDefendFightCarReachTipsNty));
		NetworkManager.AddListenEvent<DefendFightBtlResultNty>(new NetCallBackMethod<DefendFightBtlResultNty>(this.OnDefendFightBtlResultNty));
		NetworkManager.AddListenEvent<DefendFightExitBtlRes>(new NetCallBackMethod<DefendFightExitBtlRes>(this.OnExitDefendFightRes));
	}

	protected void InitDefendFightData(short state, DefendFightInitNty down = null)
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
		for (int i = 0; i < down.modeInfo.get_Count(); i++)
		{
			this.modeInfo.Add(down.modeInfo.get_Item(i).mode, this.FixDefendFightModeInfo(down.modeInfo.get_Item(i)));
		}
		this.bossRandomRate = down.bossRandomRate;
		this.BroadcastRefreshEvent();
	}

	protected void UpdateDefendFightData(short state, DefendFightModeInfoNty down = null)
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
		this.modeInfo.Remove(down.modeInfo.mode);
		this.modeInfo.Add(down.modeInfo.mode, this.FixDefendFightModeInfo(down.modeInfo));
		this.BroadcastRefreshEvent();
	}

	protected DefendFightModeInfo FixDefendFightModeInfo(DefendFightModeInfo info)
	{
		DefendFightModeInfo defendFightModeInfo = new DefendFightModeInfo();
		defendFightModeInfo.mode = info.mode;
		defendFightModeInfo.dungeonIds.AddRange(info.dungeonIds);
		defendFightModeInfo.todayChallengeTimes = info.todayChallengeTimes;
		defendFightModeInfo.todayBuyTimes = info.todayBuyTimes;
		defendFightModeInfo.todayCanChallengeTimes = info.todayCanChallengeTimes;
		defendFightModeInfo.todayCanBuyTimes = info.todayCanBuyTimes;
		return defendFightModeInfo;
	}

	protected void BroadcastRefreshEvent()
	{
		EventDispatcher.Broadcast<string, bool>(EventNames.OnTipsStateChange, TipsEvents.ButtonTipsBattleTypeUISpecial, this.IsShowTip);
	}

	public void SendDefendFightBuyChallengeReq(DefendFightMode.DFMD mode)
	{
		NetworkManager.Send(new DefendFightBuyChallengeReq
		{
			mode = mode
		}, ServerType.Data);
	}

	protected void OnDefendFightBuyChallengeRes(short state, DefendFightBuyChallengeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.UpdateSpecialInstanceDetailUI);
		if (UIManagerControl.Instance.IsOpen("DailyTaskUI"))
		{
			EventDispatcher.Broadcast(EventNames.DailyTaskNty);
		}
	}

	public void StartFight()
	{
		if (!this.ModeInfo.ContainsKey(this.SelectDetailMode))
		{
			return;
		}
		if (this.ModeInfo[this.SelectDetailMode].dungeonIds.get_Count() == 0)
		{
			return;
		}
		this.currentData.mode = this.SelectDetailMode;
		this.currentData.instanceID = this.ModeInfo[this.SelectDetailMode].dungeonIds.get_Item(0);
		this.ChallengeDefendFightReq(this.currentData.instanceID);
	}

	protected void ChallengeDefendFightReq(int instanceID)
	{
		InstanceManager.SecurityCheck(delegate
		{
			WaitUI.OpenUI(0u);
			NetworkManager.Send(new DefendFightReq
			{
				dungeonId = instanceID
			}, ServerType.Data);
		}, null);
	}

	protected void OnChallengeDefendFightRes(short state, DefendFightRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	protected void OnDefendFightMatchStatusNty(short state, DefendFightMatchStatusNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	protected void OnDefendFightStepNty(short state, DefendFightStepNty down = null)
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
		DefendFightTips step = (DefendFightTips)down.step;
		if (step != DefendFightTips.AllRoleLoaded)
		{
			if (step == DefendFightTips.BossEnter)
			{
				SpecialInstanceTips specialInstanceTips = UIManagerControl.Instance.OpenUI("SpecialInstanceTipsUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as SpecialInstanceTips;
				if (specialInstanceTips != null)
				{
					specialInstanceTips.SetInit(DefendFightTips.BossEnter, null);
				}
			}
		}
	}

	protected void OnDefendFightMonsterTipsNty(short state, DefendFightMonsterTipsNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.wave = down.wave;
		UIManagerControl.Instance.ShowBattleToastText(string.Format(GameDataUtils.GetChineseContent(InstanceManager.CurrentInstanceData.waveShow, false), down.wave), 2f);
		EventDispatcher.Broadcast<int>(EventNames.DefendFightMonsterTipsNty, down.wave);
	}

	protected void OnDefendFightCarReachTipsNty(short state, DefendFightCarReachTipsNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowBattleToastText(string.Format(GameDataUtils.GetChineseContent(502271, false), down.wave), 2f);
		EventDispatcher.Broadcast<int>(EventNames.DefendFightCarReachTipsNty, down.wave);
	}

	protected void OnDefendFightBtlResultNty(short state, DefendFightBtlResultNty down = null)
	{
		Debug.Log("----------结算面板 OnDefendFightBtlResultNty----------");
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.UpdateServerDataByBattleResult(down);
		TowerInstance.Instance.GetInstanceResult(down);
	}

	protected void UpdateServerDataByBattleResult(DefendFightBtlResultNty result)
	{
		if (!this.modeInfo.ContainsKey(this.currentData.mode))
		{
			return;
		}
		this.bossRandomRate = result.bossRandomRate;
		this.BroadcastRefreshEvent();
	}

	public void ExitDefendFightReq(bool isChallengeAgain)
	{
		NetworkManager.Send(new DefendFightExitBtlReq
		{
			again = isChallengeAgain
		}, ServerType.Data);
	}

	protected void OnExitDefendFightRes(short state, DefendFightExitBtlRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public DefendFightModeInfo GetModeInfo(DefendFightMode.DFMD mode)
	{
		if (!this.modeInfo.ContainsKey(mode))
		{
			return null;
		}
		return this.modeInfo[mode];
	}

	public int GetSystemIDByMode(DefendFightMode.DFMD mode)
	{
		switch (mode)
		{
		case DefendFightMode.DFMD.Hold:
			return 54;
		case DefendFightMode.DFMD.Protect:
			return 55;
		case DefendFightMode.DFMD.Save:
			return 56;
		default:
			return 0;
		}
	}

	protected bool IsModeOpen(DefendFightMode.DFMD mode)
	{
		return true;
	}

	public SpecialFightCommonTableData GetSpecialFightCommonTableData(DefendFightMode.DFMD mode)
	{
		if (this.commonTableInfo.ContainsKey(mode))
		{
			return this.commonTableInfo[mode];
		}
		return null;
	}

	public void OnBuyDefendTimes(SpecialFightMode mode)
	{
		DefendFightModeInfo defendFightModeInfo = SpecialFightManager.GetSpecialFightInfo(mode) as DefendFightModeInfo;
		if (defendFightModeInfo == null)
		{
			return;
		}
		SpecialFightCommonTableData specialFightCommonTableData = SpecialFightManager.GetSpecialFightCommonTableData(mode);
		if (specialFightCommonTableData == null)
		{
			return;
		}
		int todayBuyTimes = defendFightModeInfo.todayBuyTimes;
		int todayCanBuyTimes = defendFightModeInfo.todayCanBuyTimes;
		int canBuyTimes = Math.Max(todayCanBuyTimes - todayBuyTimes, 0);
		if (canBuyTimes <= 0 && todayBuyTimes < specialFightCommonTableData.priceList.get_Count())
		{
			string chineseContent = GameDataUtils.GetChineseContent(505105, false);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), chineseContent, null, delegate
			{
				LinkNavigationManager.OpenVIPUI2Privilege();
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			return;
		}
		if (todayBuyTimes >= specialFightCommonTableData.priceList.get_Count())
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(513531, false), GameDataUtils.GetChineseContent(513528, false), delegate
			{
			}, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
		}
		else
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(513531, false), string.Format(GameDataUtils.GetChineseContent(513530, false), specialFightCommonTableData.priceList.get_Item(todayBuyTimes), canBuyTimes, todayCanBuyTimes), null, null, delegate
			{
				if (canBuyTimes == 0)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513528, false), 1f, 1f);
					DialogBoxUIViewModel.Instance.BtnRclose = false;
				}
				else
				{
					DialogBoxUIViewModel.Instance.BtnRclose = true;
					DefendFightManager.Instance.SendDefendFightBuyChallengeReq((DefendFightMode.DFMD)mode);
				}
			}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null);
		}
	}

	public bool CanShowBuyBtnInDailyTask(SpecialFightMode mode)
	{
		DefendFightModeInfo defendFightModeInfo = SpecialFightManager.GetSpecialFightInfo(mode) as DefendFightModeInfo;
		return defendFightModeInfo != null && defendFightModeInfo.todayCanChallengeTimes <= 0;
	}

	public bool IsFinishInDailyTask(SpecialFightMode mode)
	{
		DefendFightModeInfo defendFightModeInfo = SpecialFightManager.GetSpecialFightInfo(mode) as DefendFightModeInfo;
		if (defendFightModeInfo == null)
		{
			return false;
		}
		int type = 0;
		switch (mode)
		{
		case SpecialFightMode.Hold:
			type = 8;
			break;
		case SpecialFightMode.Protect:
			type = 7;
			break;
		case SpecialFightMode.Save:
			type = 6;
			break;
		}
		int maxVipTimesByType = VIPPrivilegeManager.Instance.GetMaxVipTimesByType(type);
		return defendFightModeInfo.todayBuyTimes >= maxVipTimesByType;
	}
}
