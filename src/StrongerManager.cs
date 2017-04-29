using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class StrongerManager : BaseSubSystemManager
{
	private List<BianQiangJieMianPeiZhi> allStrongerInfoList;

	public List<StrongerInfo> ListStrongerInfo = new List<StrongerInfo>();

	private List<StrongerInfoData> strongerDataList;

	private List<StrongerInfoData> canShowStrongerDataList;

	private List<HongDianTuiSong> promoteWayTipsList;

	private Dictionary<PromoteWayType, HongDianTuiSong> showPromoteWayDic;

	private Dictionary<PromoteWayType, HongDianTuiSong> allShowPromoteWays;

	private static StrongerManager instance;

	public List<BianQiangJieMianPeiZhi> AllStrongerInfoList
	{
		get
		{
			return this.allStrongerInfoList;
		}
	}

	public Dictionary<PromoteWayType, HongDianTuiSong> ShowPromoteWayDic
	{
		get
		{
			return this.showPromoteWayDic;
		}
	}

	public static StrongerManager Instance
	{
		get
		{
			if (StrongerManager.instance == null)
			{
				StrongerManager.instance = new StrongerManager();
			}
			return StrongerManager.instance;
		}
	}

	public override void Init()
	{
		base.Init();
		this.GetPromoteWayCfgDataList();
		this.GetStrongerDataList();
	}

	public override void Release()
	{
		this.promoteWayTipsList = null;
		this.showPromoteWayDic = null;
		this.allShowPromoteWays = null;
		this.strongerDataList = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<StrongerInfoRes>(new NetCallBackMethod<StrongerInfoRes>(this.OnStrongerInfoRes));
		NetworkManager.AddListenEvent<StrongLoginPush>(new NetCallBackMethod<StrongLoginPush>(this.OnStrongLoginPush));
		NetworkManager.AddListenEvent<StrongerInfoAnyTimeRes>(new NetCallBackMethod<StrongerInfoAnyTimeRes>(this.OnStrongerInfoAnyTimeRes));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.LvChanged, new Callback(this.OnRoleLevelUp));
	}

	public void StrongerInfoReqest()
	{
		NetworkManager.Send(new StrongerInfoReq(), ServerType.Data);
	}

	public void SendStrongerInfoAnyTimeReq()
	{
		NetworkManager.Send(new StrongerInfoAnyTimeReq(), ServerType.Data);
	}

	private void OnStrongLoginPush(short state, StrongLoginPush down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnStrongerInfoAnyTimeRes(short state, StrongerInfoAnyTimeRes down)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	private void OnStrongerInfoRes(short state, StrongerInfoRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
	}

	public List<StrongerInfo> GetLowest3()
	{
		List<StrongerInfo> list = new List<StrongerInfo>();
		if (this.ListStrongerInfo.get_Count() <= 3)
		{
			list.AddRange(this.ListStrongerInfo);
		}
		else
		{
			list.Add(this.ListStrongerInfo.get_Item(0));
			list.Add(this.ListStrongerInfo.get_Item(1));
			list.Add(this.ListStrongerInfo.get_Item(2));
		}
		return list;
	}

	public List<StrongerInfoData> GetCanShowStrongerDataList()
	{
		this.canShowStrongerDataList = null;
		this.canShowStrongerDataList = new List<StrongerInfoData>();
		for (int i = 0; i < this.strongerDataList.get_Count(); i++)
		{
			StrongerInfoData strongerInfoData = this.strongerDataList.get_Item(i);
			if (SystemOpenManager.IsSystemOn(strongerInfoData.SystemID))
			{
				strongerInfoData.Fighting = this.GetFightingByStrongerType(strongerInfoData.Type);
				strongerInfoData.Percent = this.GetProgressByStrongerType(strongerInfoData.Type);
				this.canShowStrongerDataList.Add(strongerInfoData);
			}
		}
		this.canShowStrongerDataList.Sort(new Comparison<StrongerInfoData>(StrongerManager.StrongerProgressSortCompare));
		return this.canShowStrongerDataList;
	}

	public List<StrongerInfoData> GetLowest3StrongerData()
	{
		List<StrongerInfoData> list = new List<StrongerInfoData>();
		if (this.canShowStrongerDataList == null || this.canShowStrongerDataList.get_Count() < 0)
		{
			this.GetCanShowStrongerDataList();
		}
		for (int i = 0; i < 3; i++)
		{
			if (i >= this.canShowStrongerDataList.get_Count())
			{
				break;
			}
			list.Add(this.canShowStrongerDataList.get_Item(i));
		}
		return list;
	}

	private void GetStrongerDataList()
	{
		this.strongerDataList = new List<StrongerInfoData>();
		List<BianQiangJieMianPeiZhi> dataList = DataReader<BianQiangJieMianPeiZhi>.DataList;
		if (dataList == null || dataList.get_Count() <= 0)
		{
			return;
		}
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			int type = dataList.get_Item(i).type;
			int pathId = dataList.get_Item(i).pathId;
			int num = this.strongerDataList.FindIndex((StrongerInfoData a) => a.Type == (StrongerType)type);
			if (num < 0)
			{
				StrongerInfoData strongerInfoData = new StrongerInfoData((StrongerType)type, pathId);
				this.strongerDataList.Add(strongerInfoData);
			}
		}
	}

	public long GetFightingByStrongerType(StrongerType type)
	{
		long result = 0L;
		switch (type)
		{
		case StrongerType.Equip:
			result = EquipGlobal.GetAllEquipAttrValue();
			break;
		case StrongerType.EquipStrength:
			result = EquipGlobal.GetAllEquipDevelopAttrValue();
			break;
		case StrongerType.EquipStarUp:
			result = EquipGlobal.GetAllEquipStarUpAttrValue();
			break;
		case StrongerType.EquipEnchantment:
			result = EquipGlobal.GetAllEquipEnchantmentAttrValue();
			break;
		case StrongerType.Gem:
			result = GemGlobal.getAllGemAttrValue();
			break;
		case StrongerType.Wing:
			result = WingGlobal.GetCurrentWingFightingValue();
			break;
		case StrongerType.PetLevel:
			result = (long)PetManager.Instance.GetFormationPetLevel();
			break;
		case StrongerType.PetUpgrade:
			result = PetManager.Instance.GetFormationAddFighting();
			break;
		case StrongerType.GodSoldier:
			result = GodSoldierManager.Instance.GetAllGodSoliderAttrValue();
			break;
		}
		return result;
	}

	public float GetProgressByStrongerType(StrongerType type)
	{
		float result = 0f;
		long fightingByStrongerType = this.GetFightingByStrongerType(type);
		int standardFightingByStrongerType = this.GetStandardFightingByStrongerType(type);
		if (standardFightingByStrongerType > 0)
		{
			result = (float)fightingByStrongerType / ((float)standardFightingByStrongerType * 1f);
		}
		return result;
	}

	public string GetProgressDescByStrongerType(StrongerType type)
	{
		string empty = string.Empty;
		int lv = EntityWorld.Instance.EntSelf.Lv;
		DangQianDengJiLiLunZhanLi dangQianDengJiLiLunZhanLi = DataReader<DangQianDengJiLiLunZhanLi>.Get(lv);
		if (dangQianDengJiLiLunZhanLi == null)
		{
			return empty;
		}
		int id = 0;
		int id2 = 0;
		switch (type)
		{
		case StrongerType.Equip:
			id2 = dangQianDengJiLiLunZhanLi.equipExplain;
			break;
		case StrongerType.EquipStrength:
			id2 = dangQianDengJiLiLunZhanLi.strengthenExplain;
			break;
		case StrongerType.EquipStarUp:
			id2 = dangQianDengJiLiLunZhanLi.equipStarExplain;
			break;
		case StrongerType.EquipEnchantment:
			id2 = dangQianDengJiLiLunZhanLi.enchantExplain;
			break;
		case StrongerType.Gem:
			id2 = dangQianDengJiLiLunZhanLi.diamondExplain;
			break;
		case StrongerType.Wing:
			id2 = dangQianDengJiLiLunZhanLi.wingExplain;
			break;
		case StrongerType.PetLevel:
			id2 = dangQianDengJiLiLunZhanLi.petLvExplain;
			break;
		case StrongerType.PetUpgrade:
			id2 = dangQianDengJiLiLunZhanLi.petStarExplain;
			break;
		case StrongerType.GodSoldier:
			id2 = dangQianDengJiLiLunZhanLi.shenBingExplain;
			break;
		}
		BianQiangJieMianPeiZhi bianQiangJieMianPeiZhi = DataReader<BianQiangJieMianPeiZhi>.Get((int)type);
		if (bianQiangJieMianPeiZhi != null)
		{
			id = bianQiangJieMianPeiZhi.name3;
		}
		return GameDataUtils.GetChineseContent(id, false) + ": <color=#ff7d4b>" + GameDataUtils.GetChineseContent(id2, false) + "</color>";
	}

	private static int StrongerProgressSortCompare(StrongerInfoData AF1, StrongerInfoData AF2)
	{
		if (AF1.Percent < AF2.Percent)
		{
			return -1;
		}
		return 1;
	}

	public int GetStandardFightingByStrongerType(StrongerType type)
	{
		int result = 0;
		int lv = EntityWorld.Instance.EntSelf.Lv;
		DangQianDengJiLiLunZhanLi dangQianDengJiLiLunZhanLi = DataReader<DangQianDengJiLiLunZhanLi>.Get(lv);
		if (dangQianDengJiLiLunZhanLi == null)
		{
			return result;
		}
		switch (type)
		{
		case StrongerType.Equip:
			result = dangQianDengJiLiLunZhanLi.equip;
			break;
		case StrongerType.EquipStrength:
			result = dangQianDengJiLiLunZhanLi.strengthen;
			break;
		case StrongerType.EquipStarUp:
			result = dangQianDengJiLiLunZhanLi.equipStar;
			break;
		case StrongerType.EquipEnchantment:
			result = dangQianDengJiLiLunZhanLi.enchant;
			break;
		case StrongerType.Gem:
			result = dangQianDengJiLiLunZhanLi.diamond;
			break;
		case StrongerType.Wing:
			result = dangQianDengJiLiLunZhanLi.wing;
			break;
		case StrongerType.PetLevel:
			result = dangQianDengJiLiLunZhanLi.petLv;
			break;
		case StrongerType.PetUpgrade:
			result = dangQianDengJiLiLunZhanLi.petStar;
			break;
		case StrongerType.GodSoldier:
			result = dangQianDengJiLiLunZhanLi.shenBing;
			break;
		}
		return result;
	}

	private void GetPromoteWayCfgDataList()
	{
		this.showPromoteWayDic = new Dictionary<PromoteWayType, HongDianTuiSong>();
		this.promoteWayTipsList = new List<HongDianTuiSong>();
		this.allShowPromoteWays = new Dictionary<PromoteWayType, HongDianTuiSong>();
		List<HongDianTuiSong> list = DataReader<HongDianTuiSong>.DataList.FindAll((HongDianTuiSong a) => a.push == 1);
		if (list != null)
		{
			this.promoteWayTipsList = list;
		}
	}

	public void UpdatePromoteWayDic(PromoteWayType type, bool isShow = false)
	{
		if (this.promoteWayTipsList == null)
		{
			return;
		}
		int num = this.promoteWayTipsList.FindIndex((HongDianTuiSong a) => a.type == (int)type);
		if (num >= 0)
		{
			if (isShow && !this.allShowPromoteWays.ContainsKey(type))
			{
				this.allShowPromoteWays.Add(type, this.promoteWayTipsList.get_Item(num));
			}
			if (EntityWorld.Instance.EntSelf == null)
			{
				return;
			}
			if (EntityWorld.Instance.EntSelf.Lv < this.promoteWayTipsList.get_Item(num).lv)
			{
				isShow = false;
			}
			if (isShow)
			{
				if (!this.showPromoteWayDic.ContainsKey(type))
				{
					this.showPromoteWayDic.Add(type, this.promoteWayTipsList.get_Item(num));
				}
			}
			else if (this.showPromoteWayDic.ContainsKey(type))
			{
				this.showPromoteWayDic.Remove(type);
			}
			EventDispatcher.Broadcast(EventNames.UpdatePromoteWayTip);
		}
	}

	public void GetPromoteWayButtons()
	{
		if (this.showPromoteWayDic == null && this.promoteWayTipsList.get_Count() <= 0)
		{
			return;
		}
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		using (Dictionary<PromoteWayType, HongDianTuiSong>.Enumerator enumerator = this.showPromoteWayDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<PromoteWayType, HongDianTuiSong> current = enumerator.get_Current();
				int jump = current.get_Value().jump;
				list.Add(this.GetButton2Promote(jump, current.get_Value().name));
			}
		}
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIView popButtonsAdjustUIView = UIManagerControl.Instance.OpenUI("PopButtonsAdjustUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as PopButtonsAdjustUIView;
			popButtonsAdjustUIView.get_transform().set_localPosition(new Vector3(350f, 0f, 0f));
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	public bool CheckCanShowPromoteWay()
	{
		return this.showPromoteWayDic != null && this.showPromoteWayDic.get_Count() > 0;
	}

	private ButtonInfoData GetButton2Promote(int systemID, int chineseID)
	{
		return new ButtonInfoData
		{
			buttonName = GameDataUtils.GetChineseContent(chineseID, false),
			color = "button_yellow_1",
			onCall = delegate
			{
				LinkNavigationManager.SystemLink(systemID, true, null);
			}
		};
	}

	private void OnRoleLevelUp()
	{
		if (this.promoteWayTipsList == null || this.showPromoteWayDic == null || this.allShowPromoteWays == null)
		{
			return;
		}
		bool flag = false;
		using (Dictionary<PromoteWayType, HongDianTuiSong>.Enumerator enumerator = this.allShowPromoteWays.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<PromoteWayType, HongDianTuiSong> current = enumerator.get_Current();
				int type = (int)current.get_Key();
				int num = this.promoteWayTipsList.FindIndex((HongDianTuiSong a) => a.type == type);
				bool flag2 = false;
				if (num >= 0)
				{
					if (EntityWorld.Instance.EntSelf.Lv >= this.promoteWayTipsList.get_Item(num).lv)
					{
						if (flag2 && !this.showPromoteWayDic.ContainsKey(current.get_Key()))
						{
							this.showPromoteWayDic.Add(current.get_Key(), this.promoteWayTipsList.get_Item(num));
							flag = true;
						}
					}
				}
			}
		}
		if (flag)
		{
			EventDispatcher.Broadcast(EventNames.UpdatePromoteWayTip);
		}
	}
}
