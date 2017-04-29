using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TramcarUI : UIBase
{
	public static int LastSelectMapId;

	public static int ITEM_COUNT = 5;

	private GameObject mMapContent;

	private Image mImgMapName;

	private List<HuntMapItem> mMapList;

	private Text mTxProtectTimes;

	private Text mTxPackNumber;

	private Text mTxMultTips;

	private Dropdown mSelectQuality;

	private Toggle mUntilRefresh;

	private GameObject mGoMask;

	private GameObject mTramcarPanel;

	private List<TramcarItem> mTramcarList;

	private Image mSelfIcon;

	private Image mFriendIcon;

	private List<BuddyInfo> mFriendInfos;

	private long mLastProtectFriendId;

	private TramcarItem mLastSelectItem;

	private HuntMapItem mLastSelectMap;

	private TramcarCountDownUI mCountDownUI;

	private int mFxId;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isInterruptStick = true;
		this.isEndNav = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mImgMapName = base.FindTransform("MapName").GetComponent<Image>();
		this.mMapContent = base.FindTransform("MapContent").get_gameObject();
		this.mTxProtectTimes = base.FindTransform("txTodayTimes").GetComponent<Text>();
		this.mTxPackNumber = base.FindTransform("txPackNumber").GetComponent<Text>();
		this.mTxMultTips = base.FindTransform("txMultTips").GetComponent<Text>();
		this.mTramcarPanel = base.FindTransform("Grid").get_gameObject();
		this.mGoMask = base.FindTransform("Mask").get_gameObject();
		this.mUntilRefresh = base.FindTransform("UntilRefresh").GetComponent<Toggle>();
		this.mSelfIcon = base.FindTransform("SelfIcon").GetComponent<Image>();
		this.mFriendIcon = base.FindTransform("FriendIcon").GetComponent<Image>();
		this.mSelectQuality = base.FindTransform("QualityDropdown").GetComponent<Dropdown>();
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		for (int i = 2; i < TramcarManager.Instance.TRAMCAR_NAME.Length; i++)
		{
			list.Add(new Dropdown.OptionData(TramcarManager.Instance.TRAMCAR_NAME[i]));
		}
		this.mSelectQuality.set_options(list);
		base.FindTransform("btnNotice").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickNotice);
		base.FindTransform("btnLoot").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLootRoom);
		base.FindTransform("BtnRefresh").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefresh);
		base.FindTransform("BtnStart").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStart);
		base.FindTransform("btnAddFriend").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAddFreind);
		base.FindTransform("btnKickFriend").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickKickFreind);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110048), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		if (this.mLastSelectMap != null)
		{
			this.mLastSelectMap.IsSelect = false;
		}
		this.mLastSelectMap = null;
		this.mTxMultTips.set_text(GameDataUtils.GetChineseContent(513665, false));
		this.RefreshRightPanel();
		this.RefreshLeftPanel(false);
		this.RefreshInfoPanel();
		if (TramcarManager.Instance.CurMapId != TramcarUI.LastSelectMapId)
		{
			TramcarManager.Instance.SendOpenTramcarPanelReq(TramcarUI.LastSelectMapId);
		}
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.OpenTramcarUI, new Callback(this.RefreshUI));
		EventDispatcher.AddListener<bool>(EventNames.RefreshTramcarUI, new Callback<bool>(this.RefreshTramcar));
		EventDispatcher.AddListener(EventNames.TramcarProtectFightNty, new Callback(this.RefreshInfoPanel));
		EventDispatcher.AddListener(EventNames.OpenTramcarFriendList, new Callback(this.OnOpenTramcarFriendUI));
		EventDispatcher.AddListener(EventNames.InviteFriendSuccess, new Callback(this.OnInviteFriendSuccess));
		EventDispatcher.AddListener(EventNames.TramcarInviteFriendNty, new Callback(this.CheckTramcarInviteMessage));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.OpenTramcarUI, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener<bool>(EventNames.RefreshTramcarUI, new Callback<bool>(this.RefreshTramcar));
		EventDispatcher.RemoveListener(EventNames.TramcarProtectFightNty, new Callback(this.RefreshInfoPanel));
		EventDispatcher.RemoveListener(EventNames.OpenTramcarFriendList, new Callback(this.OnOpenTramcarFriendUI));
		EventDispatcher.RemoveListener(EventNames.InviteFriendSuccess, new Callback(this.OnInviteFriendSuccess));
		EventDispatcher.RemoveListener(EventNames.TramcarInviteFriendNty, new Callback(this.CheckTramcarInviteMessage));
	}

	private bool OnClickCity(HuntMapItem item)
	{
		if (item.CurState == HuntMapItem.State.OPEN)
		{
			TramcarUI.LastSelectMapId = item.Id;
			this.SelectMapItem(item);
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

	private void OnClickNotice(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 513666, 513663);
	}

	public void OnClickLootRoom(GameObject go)
	{
		if (TramcarManager.Instance.FightInfo.todayGrabTimes > 0)
		{
			UIManagerControl.Instance.OpenUI("TramcarLootUI", null, false, UIType.NonPush);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("今日抢夺次数已用完，请明日再来！");
		}
	}

	private void OnClickAddFreind(GameObject go)
	{
		TramcarManager.Instance.SendGetFriendProtectListReq();
	}

	private void OnClickKickFreind(GameObject go)
	{
		if (this.mCountDownUI != null)
		{
			this.mCountDownUI.RemoveTramcarCountDown();
			this.mCountDownUI.Show(false);
		}
		TramcarManager.Instance.SendDelFriendHelpReq();
	}

	private void OnOpenTramcarFriendUI()
	{
		UIManagerControl.Instance.OpenUI("TramcarFriendUI", null, false, UIType.NonPush);
	}

	private void OnClickRefresh(GameObject go)
	{
		if (this.mLastSelectItem == null || this.mLastSelectItem.Data.quality < TramcarUI.ITEM_COUNT)
		{
			if (this.mUntilRefresh.get_isOn())
			{
				this.mUntilRefresh.set_isOn(false);
				int num = this.mSelectQuality.get_value() + 2;
				if (this.mLastSelectItem.Data.quality < num)
				{
					TramcarManager.Instance.SendRefreshTramcarReq(num, false);
				}
				else
				{
					UIManagerControl.Instance.ShowToastText("已到达该品质！");
				}
			}
			else
			{
				TramcarManager.Instance.SendRefreshTramcarReq(0, false);
			}
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("已到达最高品质！");
		}
	}

	private void OnClickStart(GameObject go)
	{
		if (this.mCountDownUI != null)
		{
			this.mCountDownUI.RemoveTramcarCountDown();
			this.mCountDownUI.Show(false);
		}
		TramcarManager.Instance.SendProtectFightReq(TramcarUI.LastSelectMapId);
	}

	private void OnInviteFriendSuccess()
	{
		this.mCountDownUI = (UIManagerControl.Instance.OpenUI("TramcarCountDownUI", base.get_transform(), false, UIType.NonPush) as TramcarCountDownUI);
		this.mCountDownUI.Open(10, delegate
		{
			TramcarManager.Instance.SendDelFriendHelpReq();
		});
	}

	private void CheckTramcarInviteMessage()
	{
		if (!TramcarManager.Instance.IsDontShowAgainBeInvite && base.get_gameObject().get_activeInHierarchy() && TramcarManager.Instance.InviteMessage.get_Count() > 0)
		{
			UIManagerControl.Instance.OpenUI("TramcarBeInviteUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
	}

	private void RefreshUI()
	{
		this.RefreshInfoPanel();
		this.RefreshLeftPanel(false);
	}

	private void RefreshTramcar(bool isChange)
	{
		this.RefreshInfoPanel();
		this.RefreshLeftPanel(true);
	}

	private void RefreshInfoPanel()
	{
		this.mTxPackNumber.set_text(string.Format("拥有刷新券\n<color=#fdb22f>{0}</color>", BackpackManager.Instance.OnGetGoodCount(71034)));
		ResourceManager.SetSprite(this.mSelfIcon, UIUtils.GetRoleSmallIcon(EntityWorld.Instance.EntSelf.TypeID));
		this.mFriendIcon.get_gameObject().SetActive(false);
		if (TramcarManager.Instance.FightInfo != null)
		{
			this.mTxProtectTimes.set_text(string.Format("今天剩余次数\n<color=#fdb22f>{0}</color>", TramcarManager.Instance.FightInfo.todayProtectTimes));
			long helpId = TramcarManager.Instance.FightInfo.helpRoleId;
			if (helpId > 0L)
			{
				if (helpId == this.mLastProtectFriendId)
				{
					this.mFriendIcon.get_gameObject().SetActive(true);
				}
				else
				{
					if (this.mFriendInfos == null)
					{
						this.mFriendInfos = FriendManager.Instance.GetFriends();
					}
					BuddyInfo buddyInfo = this.mFriendInfos.Find((BuddyInfo e) => e.id == helpId);
					if (buddyInfo != null)
					{
						this.mLastProtectFriendId = helpId;
						this.mFriendIcon.get_gameObject().SetActive(true);
						ResourceManager.SetSprite(this.mFriendIcon, UIUtils.GetRoleSmallIcon(buddyInfo.career));
					}
					else
					{
						Debug.Log("<color=red>Error:</color>矿车邀请列表里没好友:" + helpId);
					}
				}
			}
		}
	}

	private void RefreshLeftPanel(bool isShowEffect = false)
	{
		this.mGoMask.SetActive(false);
		this.RefreshTramcarList(isShowEffect);
	}

	private void RefreshTramcarList(bool isShowEffect)
	{
		if (this.mTramcarList == null)
		{
			this.mTramcarList = new List<TramcarItem>();
			List<KuangChePinZhi> dataList = DataReader<KuangChePinZhi>.DataList;
			TramcarUI.ITEM_COUNT = dataList.get_Count();
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				this.CreateTramcar(dataList.get_Item(i));
			}
		}
		else
		{
			for (int j = 0; j < this.mTramcarList.get_Count(); j++)
			{
				this.mTramcarList.get_Item(j).Refresh();
			}
		}
		this.SelectItem(TramcarManager.Instance.CurQuality, isShowEffect);
	}

	private void CreateTramcar(KuangChePinZhi data)
	{
		if (data != null)
		{
			TramcarItem tramcarItem = this.mTramcarList.Find((TramcarItem e) => e.get_gameObject().get_name() == "Unused");
			if (tramcarItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("TramcarItem");
				UGUITools.SetParent(this.mTramcarPanel, instantiate2Prefab, false);
				tramcarItem = instantiate2Prefab.GetComponent<TramcarItem>();
				this.mTramcarList.Add(tramcarItem);
			}
			tramcarItem.SetData(data);
			tramcarItem.get_gameObject().set_name("Tramcar" + data.quality);
			tramcarItem.get_gameObject().SetActive(true);
			if (this.mLastSelectItem != null)
			{
				tramcarItem.IsSelect = (this.mLastSelectItem.Data.quality == data.quality);
			}
		}
	}

	private void SelectTramcar(int quality)
	{
		Debug.Log("刷出矿车品质:" + quality);
		this.mGoMask.SetActive(true);
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			this.mGoMask.SetActive(false);
		});
		int num = (!(this.mLastSelectItem != null)) ? 1 : this.mLastSelectItem.Data.quality;
		int num2 = TramcarUI.ITEM_COUNT + quality;
		int num3 = -1;
		for (int i = num; i < num2; i++)
		{
			int num4 = i % TramcarUI.ITEM_COUNT + 1;
			if (num4 >= num)
			{
				num3++;
				TimerHeap.AddTimer<int, bool>((uint)(num3 * 100), 0, new Action<int, bool>(this.SelectItem), num4, true);
			}
		}
	}

	private void SelectItem(int quality, bool isShowEffect)
	{
		if (this == null || base.get_gameObject() == null || quality <= 0)
		{
			return;
		}
		if (this.mLastSelectItem != null)
		{
			this.mLastSelectItem.IsSelect = false;
		}
		this.mLastSelectItem = this.mTramcarList.Find((TramcarItem e) => e.Data.quality == quality);
		if (this.mLastSelectItem != null)
		{
			this.mLastSelectItem.IsSelect = true;
		}
		if (isShowEffect)
		{
			this.mFxId = FXSpineManager.Instance.ReplaySpine(this.mFxId, 4801, this.mLastSelectItem.get_transform(), "TramcarUI", 2001, null, "UI", 35f, -225f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void RefreshRightPanel()
	{
		if (this.mMapList == null)
		{
			this.mMapList = new List<HuntMapItem>();
			List<KuangCheDiTuPeiZhi> dataList = DataReader<KuangCheDiTuPeiZhi>.DataList;
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
		if (TramcarUI.LastSelectMapId > 0 && this.mLastSelectMap.Id != TramcarUI.LastSelectMapId)
		{
			HuntMapItem huntMapItem2 = this.mMapList.Find((HuntMapItem e) => e.Id == TramcarUI.LastSelectMapId);
			if (huntMapItem2 != null && huntMapItem2.MinLevel <= EntityWorld.Instance.EntSelf.Lv && huntMapItem2.MaxLevel >= EntityWorld.Instance.EntSelf.Lv)
			{
				this.SelectMapItem(huntMapItem2);
			}
		}
		TramcarUI.LastSelectMapId = this.mLastSelectMap.Id;
		ResourceManager.SetSprite(this.mImgMapName, GameDataUtils.GetIcon((this.mLastSelectMap.Data as KuangCheDiTuPeiZhi).title));
		this.mImgMapName.SetNativeSize();
	}

	private HuntMapItem CreateMapItem(KuangCheDiTuPeiZhi data)
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
}
