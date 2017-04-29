using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntUI : UIBase
{
	public static int LastSelectMapId;

	private GameObject mMapContent;

	private RawImage mRadarMap;

	private Image mImgMapName;

	private List<HuntMapItem> mMapList;

	private Text mTxTimeTips;

	private Text mTxCurTime;

	private GameObject mAreaPanel;

	private List<HuntAreaItem> mAreaList;

	private GameObject mRewardPanel;

	private List<RewardItem> mRewardList;

	private HuntMapItem mLastSelectMap;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mAreaList = new List<HuntAreaItem>();
		this.mRewardList = new List<RewardItem>();
		this.mImgMapName = base.FindTransform("MapName").GetComponent<Image>();
		this.mMapContent = base.FindTransform("MapContent").get_gameObject();
		this.mRadarMap = base.FindTransform("RadarMap").GetComponent<RawImage>();
		this.mTxTimeTips = base.FindTransform("txTimeTips").GetComponent<Text>();
		this.mTxCurTime = base.FindTransform("txCurTime").GetComponent<Text>();
		this.mAreaPanel = base.FindTransform("AreaPanel").get_gameObject();
		this.mRewardPanel = base.FindTransform("RewardPanel").get_gameObject();
		base.FindTransform("btnInstruction").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickInstruction);
		base.FindTransform("btnAddTime").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAddTime);
		base.FindTransform("btnRank").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRank);
		base.FindTransform("btnAvenger").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAvenger);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110046), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		if (this.mLastSelectMap != null)
		{
			this.mLastSelectMap.IsSelect = false;
		}
		this.mLastSelectMap = null;
		this.RefreshRightPanel();
		this.RefreshLeftPanel((GuaJiDiTuPeiZhi)this.mLastSelectMap.Data);
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.GetHuntRoomList, new Callback(this.OnOpenRoomsUI));
		EventDispatcher.AddListener(EventNames.HuntInfosPush, new Callback(this.RefreshTimes));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.GetHuntRoomList, new Callback(this.OnOpenRoomsUI));
		EventDispatcher.RemoveListener(EventNames.HuntInfosPush, new Callback(this.RefreshTimes));
	}

	private bool OnClickCity(HuntMapItem item)
	{
		if (item.CurState == HuntMapItem.State.OPEN)
		{
			HuntUI.LastSelectMapId = item.Id;
			this.SelectMapItem(item);
			this.RefreshRightPanel();
			this.RefreshLeftPanel((GuaJiDiTuPeiZhi)item.Data);
			return true;
		}
		if (item.CurState == HuntMapItem.State.LOW)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511618, false));
		}
		else if (item.CurState == HuntMapItem.State.HIGH)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511617, false));
		}
		return false;
	}

	private void OnClickAddTime(GameObject go)
	{
		if (HuntManager.Instance.CanBuyTimes - HuntManager.Instance.DayBuyTimes > 0)
		{
			UIManagerControl.Instance.OpenUI("HuntBuyTimeUI", null, false, UIType.NonPush);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511628, false));
		}
	}

	private void OnClickArea(GuaJiQuYuPeiZhi data)
	{
		if (HuntManager.Instance.RemainTime <= 0)
		{
			if (HuntManager.Instance.CanBuyTimes - HuntManager.Instance.DayBuyTimes > 0)
			{
				UIManagerControl.Instance.OpenUI("HuntBuyTimeUI", null, false, UIType.NonPush);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511626, false));
			}
		}
		else if (HuntManager.Instance.RemainTime > 0 && data != null)
		{
			if (data.areaType == 1)
			{
				if (HuntManager.Instance.NormalRoomInfos == null || HuntManager.Instance.NormalRoomCD <= 0)
				{
					WaitUI.OpenUI((uint)(HuntManager.Instance.RefreshTime * 1000));
					HuntManager.Instance.SendEnterMapUiReq(data.setMap, data.id, data.areaType);
				}
				else if (HuntManager.Instance.NormalRoomInfos != null && HuntManager.Instance.NormalRoomCD > 0)
				{
					HuntManager.Instance.SetAreaData(data);
					UIManagerControl.Instance.OpenUI("HuntRoomUI", null, false, UIType.NonPush);
				}
			}
			else if (data.areaType == 2)
			{
				if (HuntManager.Instance.ChaosRoomInfos == null || HuntManager.Instance.ChaosRoomCD <= 0)
				{
					WaitUI.OpenUI((uint)(HuntManager.Instance.RefreshTime * 1000));
					HuntManager.Instance.SendEnterMapUiReq(data.setMap, data.id, data.areaType);
				}
				else if (HuntManager.Instance.ChaosRoomInfos != null && HuntManager.Instance.ChaosRoomCD > 0)
				{
					HuntManager.Instance.SetAreaData(data);
					UIManagerControl.Instance.OpenUI("HuntRoomUI", null, false, UIType.NonPush);
				}
			}
			else if (data.areaType == 3)
			{
				int vipLv = EntityWorld.Instance.EntSelf.VipLv;
				if (!VIPManager.Instance.IsVIPPrivilegeOn())
				{
					DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(511620, false), null, delegate
					{
						LinkNavigationManager.OpenVIPUI2Privilege();
					}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
					return;
				}
				if (vipLv < data.condition)
				{
					DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(511621, false), null, delegate
					{
						LinkNavigationManager.OpenVIPUI2Recharge();
					}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
					return;
				}
				if (HuntManager.Instance.VipRoomInfos == null || HuntManager.Instance.VipRoomCD <= 0)
				{
					WaitUI.OpenUI((uint)(HuntManager.Instance.RefreshTime * 1000));
					HuntManager.Instance.SendEnterMapUiReq(data.setMap, data.id, data.areaType);
				}
				else if (HuntManager.Instance.VipRoomInfos != null && HuntManager.Instance.VipRoomCD > 0)
				{
					HuntManager.Instance.SetAreaData(data);
					UIManagerControl.Instance.OpenUI("HuntRoomUI", null, false, UIType.NonPush);
				}
			}
		}
	}

	private void OnClickInstruction(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 511635, 511631);
	}

	private void OnClickAvenger(GameObject go)
	{
		UIManagerControl.Instance.ShowToastText("敬请期待!!!");
	}

	private void OnClickRank(GameObject go)
	{
		UIManagerControl.Instance.OpenUI("HuntRankUI", null, false, UIType.NonPush);
	}

	private void OnOpenRoomsUI()
	{
		WaitUI.CloseUI(0u);
		if (!UIManagerControl.Instance.IsOpen("HuntRoomUI"))
		{
			UIManagerControl.Instance.OpenUI("HuntRoomUI", null, false, UIType.NonPush);
		}
	}

	private void RefreshRightPanel()
	{
		if (this.mMapList == null)
		{
			this.mMapList = new List<HuntMapItem>();
			List<GuaJiDiTuPeiZhi> dataList = DataReader<GuaJiDiTuPeiZhi>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				HuntMapItem huntMapItem = this.CreateMapItem(dataList.get_Item(i));
				if (this.mLastSelectMap == null && huntMapItem.CurState == HuntMapItem.State.OPEN)
				{
					this.SelectMapItem(huntMapItem);
				}
			}
		}
		else
		{
			for (int j = 0; j < this.mMapList.get_Count(); j++)
			{
				HuntMapItem huntMapItem = this.mMapList.get_Item(j);
				huntMapItem.Refresh();
				if (this.mLastSelectMap == null && huntMapItem.CurState == HuntMapItem.State.OPEN)
				{
					this.SelectMapItem(huntMapItem);
				}
			}
		}
		if (HuntUI.LastSelectMapId > 0 && this.mLastSelectMap.Id != HuntUI.LastSelectMapId)
		{
			HuntMapItem huntMapItem2 = this.mMapList.Find((HuntMapItem e) => e.Id == HuntUI.LastSelectMapId);
			if (huntMapItem2 != null && huntMapItem2.MinLevel <= EntityWorld.Instance.EntSelf.Lv && huntMapItem2.MaxLevel >= EntityWorld.Instance.EntSelf.Lv)
			{
				this.SelectMapItem(huntMapItem2);
			}
		}
		HuntUI.LastSelectMapId = this.mLastSelectMap.Id;
		ResourceManager.SetSprite(this.mImgMapName, GameDataUtils.GetIcon((this.mLastSelectMap.Data as GuaJiDiTuPeiZhi).title));
		this.mImgMapName.SetNativeSize();
	}

	private HuntMapItem CreateMapItem(GuaJiDiTuPeiZhi data)
	{
		if (data != null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("HuntMapItem");
			UGUITools.SetParent(this.mMapContent, instantiate2Prefab, false);
			instantiate2Prefab.set_name("map" + data.id);
			HuntMapItem component = instantiate2Prefab.GetComponent<HuntMapItem>();
			component.SetData(data.id, data.minLv, data.maxLv, data.name, data);
			component.EventHandler = new Predicate<HuntMapItem>(this.OnClickCity);
			instantiate2Prefab.SetActive(true);
			this.mMapList.Add(component);
			return component;
		}
		return null;
	}

	private void SelectMapItem(HuntMapItem item)
	{
		if (this.mLastSelectMap != null)
		{
			this.mLastSelectMap.IsSelect = false;
		}
		this.mLastSelectMap = item;
		this.mLastSelectMap.IsSelect = true;
	}

	private void RefreshLeftPanel(GuaJiDiTuPeiZhi data)
	{
		this.RefreshArea(data);
		this.RefreshInfo(data);
		HuntManager.Instance.ClearCD();
	}

	private void RefreshArea(GuaJiDiTuPeiZhi data)
	{
		if (data != null)
		{
			ResourceManager.SetTexture(this.mRadarMap, data.miniMap);
			for (int i = 0; i < this.mAreaList.get_Count(); i++)
			{
				this.mAreaList.get_Item(i).SetUnused();
			}
			for (int j = 0; j < data.area.get_Count(); j++)
			{
				this.CreateArea(data.area.get_Item(j));
			}
		}
	}

	private void CreateArea(int areaId)
	{
		GuaJiQuYuPeiZhi guaJiQuYuPeiZhi = DataReader<GuaJiQuYuPeiZhi>.Get(areaId);
		if (guaJiQuYuPeiZhi != null)
		{
			HuntAreaItem huntAreaItem = this.mAreaList.Find((HuntAreaItem e) => e.get_gameObject().get_name() == "Unused");
			if (huntAreaItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("HuntAreaItem");
				UGUITools.SetParent(this.mAreaPanel, instantiate2Prefab, false);
				huntAreaItem = instantiate2Prefab.GetComponent<HuntAreaItem>();
				huntAreaItem.EventHandler = new Action<GuaJiQuYuPeiZhi>(this.OnClickArea);
				this.mAreaList.Add(huntAreaItem);
			}
			huntAreaItem.SetData(guaJiQuYuPeiZhi);
			huntAreaItem.get_transform().set_localPosition(new Vector3((float)guaJiQuYuPeiZhi.coordinates.get_Item(0), (float)guaJiQuYuPeiZhi.coordinates.get_Item(1)));
			huntAreaItem.get_gameObject().set_name("Area_" + areaId);
			huntAreaItem.get_gameObject().SetActive(true);
		}
	}

	private void RefreshInfo(GuaJiDiTuPeiZhi data)
	{
		this.RefreshTimes();
		this.RefreshReward(data);
	}

	private void RefreshTimes()
	{
		this.mTxTimeTips.set_text(string.Format(GameDataUtils.GetChineseContent(511632, false), HuntManager.Instance.CloseTime, HuntManager.Instance.OpenTime));
		if (HuntManager.Instance.RemainTime > 0)
		{
			this.mTxCurTime.set_text(GameDataUtils.GetChineseContent(511633, false) + "<color=#2ae70b>" + TimeConverter.GetTime(HuntManager.Instance.RemainTime, TimeFormat.DHHMM_Chinese) + "</color>");
		}
		else
		{
			this.mTxCurTime.set_text(GameDataUtils.GetChineseContent(511633, false) + "<color=red>00:00:00</color>");
		}
	}

	private void RefreshReward(GuaJiDiTuPeiZhi data)
	{
		if (data != null)
		{
			for (int i = 0; i < this.mRewardList.get_Count(); i++)
			{
				this.mRewardList.get_Item(i).get_gameObject().set_name("Unused");
				this.mRewardList.get_Item(i).get_gameObject().SetActive(false);
			}
			for (int j = 0; j < data.drop.get_Count(); j++)
			{
				this.CreateReward(data.drop.get_Item(j));
			}
		}
	}

	private void CreateReward(int id)
	{
		RewardItem rewardItem = this.mRewardList.Find((RewardItem e) => e.get_gameObject().get_name() == "Unused");
		if (rewardItem == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("RewardItem");
			UGUITools.SetParent(this.mRewardPanel, instantiate2Prefab, false);
			rewardItem = instantiate2Prefab.GetComponent<RewardItem>();
			this.mRewardList.Add(rewardItem);
		}
		rewardItem.SetRewardItem(id, -1L, 0L);
		rewardItem.get_gameObject().set_name("Reward_" + id);
		rewardItem.get_gameObject().SetActive(true);
	}

	public void OpenHuntAreaById(int areaId)
	{
		int cityId = areaId / 10;
		Debug.LogFormat("进入地图[{0}]区域[{1}]", new object[]
		{
			cityId,
			areaId
		});
		HuntMapItem huntMapItem = this.mMapList.Find((HuntMapItem e) => e.Id == cityId);
		if (huntMapItem != null && this.OnClickCity(huntMapItem))
		{
			GuaJiQuYuPeiZhi guaJiQuYuPeiZhi = DataReader<GuaJiQuYuPeiZhi>.Get(areaId);
			if (guaJiQuYuPeiZhi != null)
			{
				this.OnClickArea(guaJiQuYuPeiZhi);
			}
		}
	}

	public void OpenHuntCityById(int cityId)
	{
		Debug.LogFormat("进入地图[{0}]", new object[]
		{
			cityId
		});
		HuntMapItem huntMapItem = this.mMapList.Find((HuntMapItem e) => e.Id == cityId);
		if (huntMapItem != null)
		{
			this.OnClickCity(huntMapItem);
		}
	}
}
