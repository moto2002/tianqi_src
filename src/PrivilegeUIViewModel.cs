using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrivilegeUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_PrivilegePages = "PrivilegePages";

		public const string Attr_PrivilegeShiftType = "PrivilegeShiftType";

		public const string Attr_PrivilegePageDetails = "PrivilegePageDetails";

		public const string Attr_PrivilegeDetailShiftType = "PrivilegeDetailShiftType";

		public const string Attr_VipBtns = "VipBtns";

		public const string Attr_VipBtnsType = "VipBtnsType";

		public const string Attr_LimitCardItems = "LimitCardItems";

		public const string Attr_CardListType = "CardListType";

		public const string Attr_RechargeUnitItems = "RechargeUnitItems";

		public const string Attr_RechargeShiftType = "RechargeShiftType";

		public const string Attr_PrivilegeVisibility = "PrivilegeVisibility";

		public const string Attr_PrivilegeDetailVisibility = "PrivilegeDetailVisibility";

		public const string Attr_RechargeVisibility = "RechargeVisibility";

		public const string Attr_LimitVisibility = "LimitVisibility";

		public const string Attr_InvestVisibility = "InvestVisibility";

		public const string Attr_VIPLevelUpTipVisisbility = "VIPLevelUpTipVisisbility";

		public const string Attr_VIPLevelUpTip = "VIPLevelUpTip";

		public const string Attr_VIPDayExp = "VIPDayExp";

		public const string Attr_VIPExpInfo = "VIPExpInfo";

		public const string Attr_VIPNextVisibility = "VIPNextVisibility";

		public const string Attr_VIPNextNumVisisbility = "VIPNextNumVisisbility";

		public const string Attr_VIPNextTitleVisisbility = "VIPNextTitleVisisbility";

		public const string Attr_VIPDayExpVisisbility = "VIPDayExpVisisbility";

		public const string Attr_ImageTitleInfo1Visisbility = "ImageTitleInfo1Visisbility";

		public const string Attr_ImageTitleInfo2Visisbility = "ImageTitleInfo2Visisbility";

		public const string Attr_ImageTitleInfo3Visisbility = "ImageTitleInfo3Visisbility";

		public const string Attr_CardBg1Visisbility = "CardBg1Visisbility";

		public const string Attr_CardBg2Visisbility = "CardBg2Visisbility";

		public const string Attr_CardBg3Visisbility = "CardBg3Visisbility";

		public const string Attr_VIPFillAmount = "VIPFillAmount";

		public const string Attr_VIPProgress = "VIPProgress";

		public const string Attr_VIPNextNum = "VIPNextNum";

		public const string Attr_VIPLVNow = "VIPLVNow";

		public const string Attr_VIPNextTitle = "VIPNextTitle";

		public const string Attr_VIPNextLevel = "VIPNextLevel";

		public const string Attr_VIPLevel10 = "VIPLevel10";

		public const string Attr_VIPLevel1 = "VIPLevel1";

		public const string Attr_CardEffectContent = "CardEffectContent";

		public const string Attr_TextTitleStr = "TextTitleStr";

		public const string Attr_TextTitleValue = "TextTitleValue";

		public const string Attr_ImageTitleIcon = "ImageTitleIcon";

		public const string Attr_VIPNowLevel10 = "VIPNowLevel10";

		public const string Attr_VIPNowLevel1 = "VIPNowLevel1";

		public const string Event_OnBtnRechargeUp = "OnBtnRechargeUp";

		public const string Event_OnBtnBackToPrivilege = "OnBtnBackToPrivilege";

		public const string Event_OnBtnVipLimit = "OnBtnVipLimit";

		public const string Event_OnBtnVipLv = "OnBtnVipLv";

		public const string Event_OnBtnVipRecharge = "OnBtnVipRecharge";

		public const string Event_OnBtnLimitCardBuy = "OnBtnLimitCardBuy";

		public const string Event_OnBtnClose = "OnBtnClose";

		public const string Event_OnBtnInvest = "OnBtnInvest";

		public const string Attr_TextPower = "TextPower";
	}

	public enum Mode
	{
		Privilege,
		PrivilegeDetail,
		Recharge,
		Limit,
		Invest
	}

	public class Position
	{
		public const int LowerCenter = 1;

		public const int MiddleCenter = 2;

		public static readonly Vector3 Ali_LowerCenter = new Vector3(0f, 20f, 7f);

		public static readonly Vector3 Ali_MiddleCenter = new Vector3(0f, 0f, 4f);

		public static Vector3 GetAli(int position)
		{
			if (position == 1)
			{
				return PrivilegeUIViewModel.Position.Ali_LowerCenter;
			}
			if (position != 2)
			{
				return PrivilegeUIViewModel.Position.Ali_MiddleCenter;
			}
			return PrivilegeUIViewModel.Position.Ali_MiddleCenter;
		}
	}

	public const string yueka = "icon_yueka";

	public static PrivilegeUIViewModel Instance;

	public int limitCardType = 1;

	private Vector3 _PrivilegeShiftType;

	private Vector3 _PrivilegeDetailShiftType;

	private bool _PrivilegeVisibility = true;

	private Vector3 _RechargeShiftType;

	private bool _PrivilegeDetailVisibility;

	private Vector3 _VipBtnsType;

	private bool _RechargeVisibility;

	private bool _LimitVisibility;

	private Vector3 _CardListType;

	public ObservableCollection<OOPrivilegePage> PrivilegePages = new ObservableCollection<OOPrivilegePage>();

	public ObservableCollection<OOPrivilegePageDetail> PrivilegePageDetails = new ObservableCollection<OOPrivilegePageDetail>();

	public ObservableCollection<OORechargeUnit> RechargeUnitItems = new ObservableCollection<OORechargeUnit>();

	public ObservableCollection<OOCardItem> LimitCardItems = new ObservableCollection<OOCardItem>();

	public ObservableCollection<OOVipBtn> VipBtns = new ObservableCollection<OOVipBtn>();

	private float _VIPFillAmount;

	private string _VIPProgress;

	private string _VIPNextNum;

	private string _VIPNextLevel;

	private string _VIPNextTitle;

	private string _VIPLVNow;

	private bool _VIPNextVisibility;

	private bool _VIPLevelUpTipVisisbility;

	private string _VIPLevelUpTip;

	private string _VIPDayExp;

	private string _VIPExpInfo;

	private string _CardEffectContent;

	private string _TextTitleStr;

	private string _TextTitleValue;

	private bool _VIPNextNumVisisbility;

	private bool _VIPNextTitleVisisbility;

	private bool _VIPDayExpVisisbility;

	private bool _ImageTitleInfo1Visisbility;

	private bool _ImageTitleInfo2Visisbility;

	private bool _ImageTitleInfo3Visisbility;

	private bool _CardBg1Visisbility;

	private bool _CardBg2Visisbility;

	private bool _CardBg3Visisbility;

	private SpriteRenderer _ImageTitleIcon;

	private SpriteRenderer _VIPNowLevel10;

	private SpriteRenderer _VIPNowLevel1;

	private string _TextPower;

	private PrivilegeUIViewModel.Mode m_Mode = PrivilegeUIViewModel.Mode.Limit;

	private int m_pageIndex;

	private TimeCountDown timeCoundDown;

	private int lastVipLevel;

	private uint m_expTimerID;

	private float lastVipExpAmount;

	private float vipExpDelta = 0.005f;

	private List<Action> m_Actions = new List<Action>();

	public Vector3 PrivilegeShiftType
	{
		get
		{
			return this._PrivilegeShiftType;
		}
		set
		{
			this._PrivilegeShiftType = value;
			base.NotifyProperty("PrivilegeShiftType", value);
		}
	}

	public Vector3 PrivilegeDetailShiftType
	{
		get
		{
			return this._PrivilegeDetailShiftType;
		}
		set
		{
			this._PrivilegeDetailShiftType = value;
			base.NotifyProperty("PrivilegeDetailShiftType", value);
		}
	}

	public bool PrivilegeVisibility
	{
		get
		{
			return this._PrivilegeVisibility;
		}
		set
		{
			this._PrivilegeVisibility = value;
			base.NotifyProperty("PrivilegeVisibility", value);
		}
	}

	public Vector3 RechargeShiftType
	{
		get
		{
			return this._RechargeShiftType;
		}
		set
		{
			this._RechargeShiftType = value;
			base.NotifyProperty("RechargeShiftType", value);
		}
	}

	public bool PrivilegeDetailVisibility
	{
		get
		{
			return this._PrivilegeDetailVisibility;
		}
		set
		{
			this._PrivilegeDetailVisibility = value;
			base.NotifyProperty("PrivilegeDetailVisibility", value);
		}
	}

	public Vector3 VipBtnsType
	{
		get
		{
			return this._VipBtnsType;
		}
		set
		{
			this._VipBtnsType = value;
			base.NotifyProperty("VipBtnsType", value);
		}
	}

	public bool RechargeVisibility
	{
		get
		{
			return this._RechargeVisibility;
		}
		set
		{
			this._RechargeVisibility = value;
			base.NotifyProperty("RechargeVisibility", value);
		}
	}

	public bool LimitVisibility
	{
		get
		{
			return this._LimitVisibility;
		}
		set
		{
			this._LimitVisibility = value;
			base.NotifyProperty("LimitVisibility", value);
		}
	}

	public Vector3 CardListType
	{
		get
		{
			return this._CardListType;
		}
		set
		{
			this._CardListType = value;
			base.NotifyProperty("CardListType", value);
		}
	}

	public float VIPFillAmount
	{
		get
		{
			return this._VIPFillAmount;
		}
		set
		{
			this._VIPFillAmount = value;
			base.NotifyProperty("VIPFillAmount", value);
		}
	}

	public string VIPProgress
	{
		get
		{
			return this._VIPProgress;
		}
		set
		{
			this._VIPProgress = value;
			base.NotifyProperty("VIPProgress", value);
		}
	}

	public string VIPNextNum
	{
		get
		{
			return this._VIPNextNum;
		}
		set
		{
			this._VIPNextNum = value;
			base.NotifyProperty("VIPNextNum", value);
		}
	}

	public string VIPNextLevel
	{
		get
		{
			return this._VIPNextLevel;
		}
		set
		{
			this._VIPNextLevel = value;
			base.NotifyProperty("VIPNextLevel", value);
		}
	}

	public string VIPNextTitle
	{
		get
		{
			return this._VIPNextTitle;
		}
		set
		{
			this._VIPNextTitle = value;
			base.NotifyProperty("VIPNextTitle", value);
		}
	}

	public string VIPLVNow
	{
		get
		{
			return this._VIPLVNow;
		}
		set
		{
			this._VIPLVNow = value;
			base.NotifyProperty("VIPLVNow", value);
		}
	}

	public bool VIPNextVisibility
	{
		get
		{
			return this._VIPNextVisibility;
		}
		set
		{
			this._VIPNextVisibility = value;
			base.NotifyProperty("VIPNextVisibility", value);
		}
	}

	public bool VIPLevelUpTipVisisbility
	{
		get
		{
			return this._VIPLevelUpTipVisisbility;
		}
		set
		{
			this._VIPLevelUpTipVisisbility = value;
			base.NotifyProperty("VIPLevelUpTipVisisbility", value);
		}
	}

	public string VIPLevelUpTip
	{
		get
		{
			return this._VIPLevelUpTip;
		}
		set
		{
			this._VIPLevelUpTip = value;
			base.NotifyProperty("VIPLevelUpTip", value);
		}
	}

	public string VIPDayExp
	{
		get
		{
			return this._VIPDayExp;
		}
		set
		{
			this._VIPDayExp = value;
			base.NotifyProperty("VIPDayExp", value);
		}
	}

	public string VIPExpInfo
	{
		get
		{
			return this._VIPExpInfo;
		}
		set
		{
			this._VIPExpInfo = value;
			base.NotifyProperty("VIPExpInfo", value);
		}
	}

	public string CardEffectContent
	{
		get
		{
			return this._CardEffectContent;
		}
		set
		{
			this._CardEffectContent = value;
			base.NotifyProperty("CardEffectContent", value);
		}
	}

	public string TextTitleStr
	{
		get
		{
			return this._TextTitleStr;
		}
		set
		{
			this._TextTitleStr = value;
			base.NotifyProperty("TextTitleStr", value);
		}
	}

	public string TextTitleValue
	{
		get
		{
			return this._TextTitleValue;
		}
		set
		{
			this._TextTitleValue = value;
			base.NotifyProperty("TextTitleValue", value);
		}
	}

	public bool VIPNextNumVisisbility
	{
		get
		{
			return this._VIPNextNumVisisbility;
		}
		set
		{
			this._VIPNextNumVisisbility = value;
			base.NotifyProperty("VIPNextNumVisisbility", value);
		}
	}

	public bool VIPNextTitleVisisbility
	{
		get
		{
			return this._VIPNextTitleVisisbility;
		}
		set
		{
			this._VIPNextTitleVisisbility = value;
			base.NotifyProperty("VIPNextTitleVisisbility", value);
		}
	}

	public bool VIPDayExpVisisbility
	{
		get
		{
			return this._VIPDayExpVisisbility;
		}
		set
		{
			this._VIPDayExpVisisbility = value;
			base.NotifyProperty("VIPDayExpVisisbility", value);
		}
	}

	public bool ImageTitleInfo1Visisbility
	{
		get
		{
			return this._ImageTitleInfo1Visisbility;
		}
		set
		{
			this._ImageTitleInfo1Visisbility = value;
			base.NotifyProperty("ImageTitleInfo1Visisbility", value);
		}
	}

	public bool ImageTitleInfo2Visisbility
	{
		get
		{
			return this._ImageTitleInfo2Visisbility;
		}
		set
		{
			this._ImageTitleInfo2Visisbility = value;
			base.NotifyProperty("ImageTitleInfo2Visisbility", value);
		}
	}

	public bool ImageTitleInfo3Visisbility
	{
		get
		{
			return this._ImageTitleInfo3Visisbility;
		}
		set
		{
			this._ImageTitleInfo3Visisbility = value;
			base.NotifyProperty("ImageTitleInfo3Visisbility", value);
		}
	}

	public bool CardBg1Visisbility
	{
		get
		{
			return this._CardBg1Visisbility;
		}
		set
		{
			this._CardBg1Visisbility = value;
			base.NotifyProperty("CardBg1Visisbility", value);
		}
	}

	public bool CardBg2Visisbility
	{
		get
		{
			return this._CardBg2Visisbility;
		}
		set
		{
			this._CardBg2Visisbility = value;
			base.NotifyProperty("CardBg2Visisbility", value);
		}
	}

	public bool CardBg3Visisbility
	{
		get
		{
			return this._CardBg3Visisbility;
		}
		set
		{
			this._CardBg3Visisbility = value;
			base.NotifyProperty("CardBg3Visisbility", value);
		}
	}

	public SpriteRenderer ImageTitleIcon
	{
		get
		{
			return this._ImageTitleIcon;
		}
		set
		{
			this._ImageTitleIcon = value;
			base.NotifyProperty("ImageTitleIcon", value);
		}
	}

	public SpriteRenderer VIPNowLevel10
	{
		get
		{
			return this._VIPNowLevel10;
		}
		set
		{
			this._VIPNowLevel10 = value;
			base.NotifyProperty("VIPNowLevel10", value);
		}
	}

	public SpriteRenderer VIPNowLevel1
	{
		get
		{
			return this._VIPNowLevel1;
		}
		set
		{
			this._VIPNowLevel1 = value;
			base.NotifyProperty("VIPNowLevel1", value);
		}
	}

	public string TextPower
	{
		get
		{
			return this._TextPower;
		}
		set
		{
			this._TextPower = value;
			base.NotifyProperty("TextPower", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		PrivilegeUIViewModel.Instance = this;
		string[] array = DataReader<Vip>.Get("rechargeExp").value.Split(new char[]
		{
			';'
		});
		string[] array2 = DataReader<Vip>.Get("rechargeExp").value.Split(new char[]
		{
			';'
		});
		string[] array3 = DataReader<Vip>.Get("landingExp").value.Split(new char[]
		{
			';'
		});
		this.VIPNextTitle = "VIP剩余时间:";
		this.VIPDayExp = string.Empty;
		this.VIPExpInfo = string.Format("VIP成员每天可获5点vip经验，充值<color=#7bff17>{0}</color>钻石获<color=#7bff17>{1}</color>点vip经验", array2[0], array2[1]);
		this.TextTitleStr = "激活后立即获赠:";
		this.TextTitleValue = string.Empty;
	}

	private void OnEnable()
	{
		if (EntityWorld.Instance.EntSelf != null)
		{
			this.SwitchMode(PrivilegeUIViewModel.Mode.Limit, EntityWorld.Instance.EntSelf.VipLv);
			this.lastVipLevel = EntityWorld.Instance.EntSelf.VipLv;
			this.RefreshVIP(false);
		}
	}

	private void OnDisable()
	{
		this.RemoveVIPExpAnimation();
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.VipLvChanged, new Callback(this.OnSelfAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.RechargeDiamondChanged, new Callback(this.OnSelfAttrChangedNty));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.VipLvChanged, new Callback(this.OnSelfAttrChangedNty));
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.RechargeDiamondChanged, new Callback(this.OnSelfAttrChangedNty));
	}

	private void OnSelfAttrChangedNty()
	{
		this.RefreshVIP(true);
	}

	public void OnBtnRechargeUp()
	{
		PrivilegeUIView.Instance.SetRechargeBtnVisible(3);
		this.SwitchMode(PrivilegeUIViewModel.Mode.Recharge, PrivilegeUIView.Instance.PrivilegeDetailPageIndex);
	}

	public void OnBtnBackToPrivilege()
	{
		PrivilegeUIView.Instance.SetRechargeBtnVisible(2);
		this.SwitchMode(PrivilegeUIViewModel.Mode.PrivilegeDetail, EntityWorld.Instance.EntSelf.VipLv);
	}

	public void OnBtnVipLimit()
	{
		PrivilegeUIView.Instance.SetRechargeBtnVisible(1);
		this.SwitchMode(PrivilegeUIViewModel.Mode.Limit, 0);
	}

	public void OnBtnVipLv()
	{
		PrivilegeUIView.Instance.SetRechargeBtnVisible(2);
		this.SwitchMode(PrivilegeUIViewModel.Mode.PrivilegeDetail, EntityWorld.Instance.EntSelf.VipLv);
	}

	public void OnBtnVipRecharge()
	{
		PrivilegeUIView.Instance.SetRechargeBtnVisible(3);
		this.SwitchMode(PrivilegeUIViewModel.Mode.Recharge, PrivilegeUIView.Instance.PrivilegeDetailPageIndex);
	}

	public void OnBtnInvest()
	{
		PrivilegeUIView.Instance.SetRechargeBtnVisible(4);
		this.SwitchMode(PrivilegeUIViewModel.Mode.Invest, 0);
	}

	private void CheckLimitCardBtnState()
	{
		VipXianShiQia vipXianShiQia = DataReader<VipXianShiQia>.Get(this.limitCardType);
		MonthCardInfoPush limitCardData = VIPManager.Instance.GetLimitCardData();
		bool limitCardBtnState = true;
		if (limitCardData != null)
		{
			int num = 0;
			switch (this.limitCardType)
			{
			case 1:
				num = limitCardData.silver;
				break;
			case 2:
				num = limitCardData.gold;
				break;
			case 3:
				num = limitCardData.diamond;
				break;
			}
			if (num > TimeManager.Instance.PreciseServerSecond)
			{
				int num2 = num - TimeManager.Instance.PreciseServerSecond;
				int buyingTime = vipXianShiQia.buyingTime;
				if (buyingTime * 24 * 3600 < num2)
				{
					limitCardBtnState = false;
				}
			}
		}
		PrivilegeUIView.Instance.SetLimitCardBtnState(limitCardBtnState);
	}

	public void OnBtnLimitCardBuy()
	{
		VipXianShiQia dataXianShi = DataReader<VipXianShiQia>.Get(this.limitCardType);
		int buyingTime = dataXianShi.buyingTime;
		MonthCardInfoPush limitCardData = VIPManager.Instance.GetLimitCardData();
		if (limitCardData != null)
		{
			int num = 0;
			switch (this.limitCardType)
			{
			case 1:
				num = limitCardData.silver;
				break;
			case 2:
				num = limitCardData.gold;
				break;
			case 3:
				num = limitCardData.diamond;
				break;
			}
			if (num > TimeManager.Instance.PreciseServerSecond)
			{
				int num2 = num - TimeManager.Instance.PreciseServerSecond;
				if (buyingTime * 24 * 3600 < num2)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(508069, false));
					return;
				}
			}
		}
		string content = string.Format("{0}需要花费￥{1} \n您确定激活吗？", GameDataUtils.GetChineseContent(dataXianShi.name, false), dataXianShi.Diamond);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(71012, false), content, null, delegate
		{
			List<RechargeGoodsInfo> listRechargeGoodsInfo = RechargeManager.Instance.listRechargeGoodsInfo;
			for (int i = 0; i < listRechargeGoodsInfo.get_Count(); i++)
			{
				RechargeGoodsInfo rechargeGoodsInfo = listRechargeGoodsInfo.get_Item(i);
				if (rechargeGoodsInfo.vip != 0 && rechargeGoodsInfo.vip == dataXianShi.ID)
				{
					RechargeManager.Instance.ExecutionToRechargeDiamond(rechargeGoodsInfo.ID);
					break;
				}
			}
		}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	public void OnBtnClose()
	{
		PrivilegeUIView.Instance.OnClose();
	}

	public void RefreshMode()
	{
		this.SwitchMode(this.m_Mode, this.m_pageIndex + 1);
	}

	public void RefreshSwitchMode()
	{
		this.SwitchMode(this.m_Mode, this.m_pageIndex);
	}

	public void SwitchMode(PrivilegeUIViewModel.Mode mode, int pageIndex)
	{
		pageIndex = ((pageIndex != 0) ? (pageIndex - 1) : 0);
		this.m_pageIndex = pageIndex;
		this.m_Mode = mode;
		ListShifts.ResetShift(ref this._PrivilegeDetailShiftType);
		ListShifts.ResetShift(ref this._RechargeShiftType);
		ListShifts.ResetShift(ref this._VipBtnsType);
		ListShifts.ResetShift(ref this._CardListType);
		switch (this.m_Mode)
		{
		case PrivilegeUIViewModel.Mode.PrivilegeDetail:
			this.LimitVisibility = false;
			this.RechargeVisibility = false;
			this.PrivilegeDetailVisibility = true;
			PrivilegeUIView.Instance.ShowInvestUI(false);
			PrivilegeUIView.Instance.SetVipExpVisible(true);
			this.RefreshPrivilegePageDetails(pageIndex);
			this.PrivilegeDetailShiftType = ListShifts.GetShift(5, this.m_pageIndex, true);
			this.VipBtnsType = ListShifts.GetShift(6, this.m_pageIndex, true);
			PrivilegeUIView.Instance.ResetSelectBtnState(2);
			break;
		case PrivilegeUIViewModel.Mode.Recharge:
			this.LimitVisibility = false;
			this.RechargeVisibility = true;
			this.PrivilegeDetailVisibility = false;
			PrivilegeUIView.Instance.ShowInvestUI(false);
			PrivilegeUIView.Instance.SetVipExpVisible(true);
			this.RefreshRechargePages();
			this.RechargeShiftType = ListShifts.GetShift(3, 0, true);
			PrivilegeUIView.Instance.ResetSelectBtnState(3);
			RechargeManager.Instance.SetTipOfBox(false);
			break;
		case PrivilegeUIViewModel.Mode.Limit:
			this.LimitVisibility = true;
			this.RechargeVisibility = false;
			this.PrivilegeDetailVisibility = false;
			PrivilegeUIView.Instance.ShowInvestUI(false);
			PrivilegeUIView.Instance.SetVipExpVisible(true);
			this.RefreshLimitPages();
			this.CardListType = ListShifts.GetShift(3, 0, true);
			PrivilegeUIView.Instance.ResetSelectBtnState(1);
			break;
		case PrivilegeUIViewModel.Mode.Invest:
			this.LimitVisibility = false;
			this.RechargeVisibility = false;
			this.PrivilegeDetailVisibility = false;
			PrivilegeUIView.Instance.ShowInvestUI(true);
			PrivilegeUIView.Instance.SetVipExpVisible(false);
			PrivilegeUIView.Instance.ResetSelectBtnState(4);
			InvestFundManager.Instance.SetTipOfCanBuy(false);
			break;
		}
	}

	public void SetCurrentPageVIPLv(int pageIndex, int vipLv)
	{
		if (EntityWorld.Instance.EntSelf != null)
		{
			this.ControlPrivilegeDetailPages(pageIndex);
		}
	}

	public void VipCountDown(int time)
	{
		this.timeCoundDown = new TimeCountDown(time, TimeFormat.SECOND, delegate
		{
			if (PrivilegeUIViewModel.Instance != null)
			{
				this.VIPNextNum = TimeConverter.GetTime(this.timeCoundDown.GetSeconds(), TimeFormat.DHHMM_Chinese);
			}
		}, delegate
		{
		}, true);
	}

	private void ClearTimeCoundDown()
	{
		if (this.timeCoundDown != null)
		{
			this.timeCoundDown.Dispose();
			this.timeCoundDown = null;
		}
	}

	private void RefreshVIP(bool isPlayAni = false)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		int vipLv = EntityWorld.Instance.EntSelf.VipLv;
		int level = vipLv + 1;
		this.VIPNowLevel1 = GameDataUtils.GetNumIcon10(vipLv, NumType.Yellow_big);
		this.VIPNowLevel10 = GameDataUtils.GetNumIcon1(vipLv, NumType.Yellow_big);
		int num = EntityWorld.Instance.EntSelf.RechargeDiamond - VIPManager.Instance.GetVIPLevelDiamonds(vipLv);
		int num2 = VIPManager.Instance.GetVIPLevelDiamonds(level) - VIPManager.Instance.GetVIPLevelDiamonds(vipLv);
		bool flag = true;
		MonthCardInfoPush limitCardData = VIPManager.Instance.GetLimitCardData();
		if (limitCardData != null)
		{
			if (limitCardData.Times > TimeManager.Instance.PreciseServerSecond)
			{
				int time = limitCardData.Times - TimeManager.Instance.PreciseServerSecond;
				this.VipCountDown(time);
			}
			else
			{
				flag = false;
			}
		}
		this.VIPNextVisibility = flag;
		this.VIPNextNumVisisbility = flag;
		this.VIPNextTitleVisisbility = flag;
		this.VIPDayExpVisisbility = flag;
		float vipExp = VIPManager.Instance.GetVipExp();
		float num3 = (float)VIPManager.Instance.GetNeedExpToVIP(level);
		int maxVIP = VIPManager.Instance.GetMaxVIP();
		if (vipLv >= maxVIP)
		{
			this.VIPProgress = "满级/上限";
			this.VIPLevelUpTipVisisbility = true;
			this.SetVIPEXPAmount(1f, vipLv - this.lastVipLevel, isPlayAni);
		}
		else
		{
			this.VIPProgress = vipExp + "/" + num3;
			this.VIPLevelUpTipVisisbility = false;
			float num4 = num3 - vipExp;
			this.RemoveVIPExpAnimation();
			this.SetVIPEXPAmount(vipExp / num3, vipLv - this.lastVipLevel, isPlayAni);
		}
		this.CheckLimitCardBtnState();
	}

	public void RefreshVIPPanel()
	{
		this.RefreshVIP(true);
	}

	private void SetVIPEXPAmount(float amount, int upLevel, bool anim = false)
	{
		if (anim)
		{
			this.m_Actions.Add(delegate
			{
				this.lastVipExpAmount = amount;
				this.SetExpDelta(upLevel);
				this.PlayVIPEXPAnimation(upLevel);
			});
			this.CheckVIPEXPAnimation();
		}
		else
		{
			this.VIPFillAmount = amount;
		}
	}

	private void SetExpDelta(int upLevel)
	{
		int num = 15;
		float num2 = (float)(upLevel * 1) + (this.lastVipExpAmount - this.VIPFillAmount);
		this.vipExpDelta = num2 / (float)num;
	}

	private void CheckVIPEXPAnimation()
	{
		if (this.m_expTimerID > 0u)
		{
			return;
		}
		if (this.m_Actions.get_Count() == 0)
		{
			if (this.lastVipLevel != EntityWorld.Instance.EntSelf.VipLv)
			{
				this.PlayVIPLvUpFX();
			}
			return;
		}
		this.m_Actions.get_Item(0).Invoke();
		this.m_Actions.RemoveAt(0);
	}

	private void PlayVIPEXPAnimation(int upLevel)
	{
		TimerHeap.DelTimer(this.m_expTimerID);
		this.m_expTimerID = TimerHeap.AddTimer(0u, 30, delegate
		{
			if (upLevel > 0)
			{
				this.VIPFillAmount += this.vipExpDelta;
				if (this.VIPFillAmount >= 1f)
				{
					this.VIPFillAmount = 0f;
					this.PlayVIPLvBarFX();
					this.PlayVIPEXPAnimation(upLevel - 1);
				}
			}
			else if (this.VIPFillAmount < this.lastVipExpAmount && this.VIPFillAmount + this.vipExpDelta < this.lastVipExpAmount)
			{
				this.VIPFillAmount += this.vipExpDelta;
			}
			else
			{
				this.VIPFillAmount = this.lastVipExpAmount;
				TimerHeap.DelTimer(this.m_expTimerID);
				this.m_expTimerID = 0u;
				this.CheckVIPEXPAnimation();
			}
		});
	}

	private void PlayVIPLvBarFX()
	{
		Transform transform = PrivilegeUIView.Instance.FindTransform("VIPProgressFX");
		if (transform != null)
		{
			FXSpineManager.Instance.PlaySpine(2206, transform, "PrivilegeUI", 2001, null, "UI", 0f, 0f, 1.27f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void PlayVIPLvUpFX()
	{
		UIManagerControl.Instance.OpenUI("VIPLevelUPUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush);
		VIPLevelUpUI.Instance.SetVIPlv(this.lastVipLevel);
		this.lastVipLevel = EntityWorld.Instance.EntSelf.VipLv;
	}

	private void RemoveVIPExpAnimation()
	{
		TimerHeap.DelTimer(this.m_expTimerID);
		this.m_Actions.Clear();
		this.m_expTimerID = 0u;
	}

	private void RefreshPrivilegePages()
	{
		this.PrivilegePages.Clear();
		List<VipDengJi> dataList = DataReader<VipDengJi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			VipDengJi vipDengJi = dataList.get_Item(i);
			if (vipDengJi.level > 0)
			{
				OOPrivilegePage privilegePage = this.GetPrivilegePage(i, vipDengJi);
				this.PrivilegePages.Add(privilegePage);
			}
		}
	}

	private void ReFreshVipBtnState()
	{
		int count = this.VipBtns.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				if (this.VipBtns.GetItem(i) != null)
				{
					this.VipBtns.GetItem(i).ImageGreyVisibility = true;
				}
			}
		}
	}

	private void RefreshPrivilegePageDetails(int pageIndex)
	{
		this.PrivilegePageDetails.Clear();
		List<VipDengJi> dataList = DataReader<VipDengJi>.DataList;
		this.VipBtns.Clear();
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			VipDengJi dataVIPLevel = dataList.get_Item(i);
			if (dataVIPLevel.level > 0)
			{
				OOPrivilegePageDetail privilegePageDetail = this.GetPrivilegePageDetail(dataVIPLevel);
				this.PrivilegePageDetails.Add(privilegePageDetail);
				OOVipBtn btn = new OOVipBtn();
				btn.BtnName = "VIP " + dataVIPLevel.level;
				btn.ImageGreyVisibility = true;
				btn.callback = delegate
				{
					this.ReFreshVipBtnState();
					btn.ImageGreyVisibility = false;
					this.PrivilegeDetailShiftType = ListShifts.GetShift(5, dataVIPLevel.level - 1, false);
				};
				this.VipBtns.Add(btn);
				if (i - 1 == pageIndex)
				{
					this.ReFreshVipBtnState();
					btn.ImageGreyVisibility = false;
				}
			}
		}
	}

	public void RefreshRechargePages()
	{
		this.RechargeUnitItems.Clear();
		this.RefreshItemsOfMonthCard();
		this.RefreshItemsOfCharge();
	}

	private void ControlPrivilegePages(int currentIndex)
	{
		if (this.m_Mode != PrivilegeUIViewModel.Mode.Privilege)
		{
			return;
		}
		for (int i = 0; i < this.PrivilegePages.Count; i++)
		{
			this.PrivilegePages[i].Node2HideVisibility = (Mathf.Abs(currentIndex - i) <= 1);
		}
	}

	private void ControlPrivilegeDetailPages(int currentIndex)
	{
		if (this.m_Mode != PrivilegeUIViewModel.Mode.PrivilegeDetail)
		{
			return;
		}
		for (int i = 0; i < this.PrivilegePageDetails.Count; i++)
		{
			this.PrivilegePageDetails[i].Node2HideVisibility = (Mathf.Abs(currentIndex - i) <= 1);
		}
	}

	private void ReFreshCardListState()
	{
		int count = this.LimitCardItems.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				if (this.LimitCardItems.GetItem(i) != null)
				{
					this.LimitCardItems.GetItem(i).ImageSelectVisibility = false;
					this.LimitCardItems.GetItem(i).ImageBgSelectVisibility = false;
				}
			}
		}
	}

	private void RefreshCardPanelInfo(int cardId)
	{
		VipXianShiQia vipXianShiQia = DataReader<VipXianShiQia>.Get(cardId);
		List<int> timeLimit = vipXianShiQia.timeLimit;
		string text = string.Empty;
		int num = -1;
		Transform transform = PrivilegeUIView.Instance.FindTransform("CardEffectContent");
		this.CardEffectContent = string.Empty;
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			GameObject gameObject = transform.GetChild(i).get_gameObject();
			Object.Destroy(gameObject);
		}
		for (int j = 0; j < transform.get_childCount(); j++)
		{
			GameObject gameObject2 = transform.GetChild(j).get_gameObject();
			Object.Destroy(gameObject2);
		}
		for (int k = 0; k < timeLimit.get_Count(); k++)
		{
			int key = timeLimit.get_Item(k);
			VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(key);
			if (vipXiaoGuo != null)
			{
				string chineseContent = GameDataUtils.GetChineseContent(vipXiaoGuo.name, true);
				if (!string.IsNullOrEmpty(chineseContent))
				{
					num++;
					if (num == 0)
					{
						text = chineseContent;
					}
					else
					{
						text = text + "\n" + chineseContent;
					}
					GameObject gameObject3 = new GameObject();
					gameObject3.set_name("diamond" + num);
					gameObject3.AddComponent<Image>();
					Image component = gameObject3.GetComponent<Image>();
					component.get_rectTransform().SetParent(transform);
					component.get_rectTransform().set_localPosition(new Vector3(-20f, 47f - 30f * (float)num, 0f));
					component.get_rectTransform().set_localScale(new Vector3(1f, 1f, 1f));
					component.get_rectTransform().set_sizeDelta(new Vector2(25f, 25f));
					ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("j_diamond001"));
				}
			}
		}
		this.CardEffectContent = text;
		string chineseContent2 = GameDataUtils.GetChineseContent(80261, true);
		this.TextPower = chineseContent2;
		ChengHao chengHao = DataReader<ChengHao>.Get(vipXianShiQia.titleId);
		if (chengHao != null)
		{
			this.ImageTitleIcon = GameDataUtils.GetIcon(chengHao.icon);
		}
		this.TextTitleValue = " " + vipXianShiQia.addDiamonds;
		this.RefreshTitleImg(cardId);
		this.limitCardType = cardId;
		this.CheckLimitCardBtnState();
	}

	public void RefreshTitleImg(int cardId)
	{
		this.ImageTitleInfo1Visisbility = false;
		this.ImageTitleInfo2Visisbility = false;
		this.ImageTitleInfo3Visisbility = false;
		this.CardBg1Visisbility = false;
		this.CardBg2Visisbility = false;
		this.CardBg3Visisbility = false;
		if (cardId == 1)
		{
			this.ImageTitleInfo1Visisbility = true;
			this.CardBg1Visisbility = true;
		}
		else if (cardId == 2)
		{
			this.ImageTitleInfo2Visisbility = true;
			this.CardBg2Visisbility = true;
		}
		else if (cardId == 3)
		{
			this.ImageTitleInfo3Visisbility = true;
			this.CardBg3Visisbility = true;
		}
	}

	public void RefreshLimitPages()
	{
		this.LimitCardItems.Clear();
		List<VipXianShiQia> dataList = DataReader<VipXianShiQia>.DataList;
		for (int i = dataList.get_Count() - 1; i >= 0; i--)
		{
			VipXianShiQia vipXianShiQia = dataList.get_Item(i);
			OOCardItem card = new OOCardItem();
			card.CardName = GameDataUtils.GetChineseContent(vipXianShiQia.name, false);
			card.Price = string.Format("￥{0}", vipXianShiQia.Diamond);
			card.ID = vipXianShiQia.ID;
			card.GoodsIcon = GameDataUtils.GetIcon(vipXianShiQia.icon);
			card.callback = delegate
			{
				this.ReFreshCardListState();
				card.ImageSelectVisibility = true;
				card.ImageBgSelectVisibility = true;
				this.RefreshCardPanelInfo(card.ID);
			};
			this.LimitCardItems.Add(card);
			if (i == dataList.get_Count() - 1)
			{
				this.ReFreshCardListState();
				card.ImageSelectVisibility = true;
				card.ImageBgSelectVisibility = true;
				this.RefreshCardPanelInfo(card.ID);
			}
		}
	}

	private OOPrivilegePage GetPrivilegePage(int index, VipDengJi dataVIPLevel)
	{
		OOPrivilegePage oOPrivilegePage = new OOPrivilegePage();
		List<int> effect = dataVIPLevel.effect;
		for (int i = 0; i < effect.get_Count(); i++)
		{
			int key = effect.get_Item(i);
			VipXiaoGuo dataVIPEffect = DataReader<VipXiaoGuo>.Get(key);
			if (dataVIPEffect != null)
			{
				oOPrivilegePage.VIPName = string.Format(GameDataUtils.GetChineseContent(508039, false), dataVIPLevel.level);
				SpriteRenderer icon = GameDataUtils.GetIcon(dataVIPEffect.picture);
				if (i == 0)
				{
					if (dataVIPEffect.position == 2)
					{
						oOPrivilegePage.PrivilegeItemBg = icon;
						oOPrivilegePage.TimesTipOn = true;
						oOPrivilegePage.TimesNum = ((dataVIPEffect.value1 != -1) ? dataVIPEffect.value1.ToString() : string.Empty);
						oOPrivilegePage.callback = null;
					}
					else if (dataVIPEffect.position == 1)
					{
						oOPrivilegePage.PrivilegeItemBg = icon;
						oOPrivilegePage.TimesTipOn = false;
						int num = VIPPrivilegeManager.Instance.Effect2TreasureID(dataVIPEffect.effect);
						if (num > 0)
						{
							oOPrivilegePage.TimesNum = ((dataVIPEffect.value1 != -1) ? dataVIPEffect.value1.ToString() : string.Empty);
							oOPrivilegePage.callback = delegate
							{
								TreasureUIViewModel.Instance.OpenTreasure(dataVIPLevel.level, dataVIPEffect);
							};
						}
					}
				}
				else if (i <= 3)
				{
					OOPrivilegeSmallItem oOPrivilegeSmallItem = new OOPrivilegeSmallItem();
					if (dataVIPEffect.position == 2)
					{
						oOPrivilegeSmallItem.Background = icon;
						oOPrivilegeSmallItem.TimesTipOn = true;
						oOPrivilegeSmallItem.TimesNum = ((dataVIPEffect.value1 != -1) ? dataVIPEffect.value1.ToString() : string.Empty);
						oOPrivilegeSmallItem.callback = null;
					}
					else if (dataVIPEffect.position == 1)
					{
						oOPrivilegeSmallItem.Background = icon;
						oOPrivilegeSmallItem.TimesTipOn = false;
						oOPrivilegeSmallItem.TimesNum = string.Empty;
						oOPrivilegeSmallItem.callback = null;
						int num2 = VIPPrivilegeManager.Instance.Effect2TreasureID(dataVIPEffect.effect);
						if (num2 > 0)
						{
							oOPrivilegeSmallItem.callback = delegate
							{
								if (TreasureUIViewModel.IsTreasureValid(dataVIPLevel.level, dataVIPEffect))
								{
									this.SwitchMode(PrivilegeUIViewModel.Mode.PrivilegeDetail, PrivilegeUIView.Instance.PrivilegePageIndex);
								}
							};
						}
					}
					oOPrivilegePage.SmallItems.Add(oOPrivilegeSmallItem);
				}
			}
		}
		OOPrivilegeSmallItem oOPrivilegeSmallItem2 = new OOPrivilegeSmallItem();
		oOPrivilegeSmallItem2.Background = ResourceManager.GetIconSprite("tequan");
		oOPrivilegeSmallItem2.TimesTipOn = false;
		oOPrivilegeSmallItem2.callback = delegate
		{
			this.SwitchMode(PrivilegeUIViewModel.Mode.PrivilegeDetail, PrivilegeUIView.Instance.PrivilegePageIndex);
		};
		oOPrivilegePage.SmallItems.Add(oOPrivilegeSmallItem2);
		return oOPrivilegePage;
	}

	private OOPrivilegePageDetail GetPrivilegePageDetail(VipDengJi dataVIPLevel)
	{
		OOPrivilegePageDetail oOPrivilegePageDetail = new OOPrivilegePageDetail();
		oOPrivilegePageDetail.callback = null;
		oOPrivilegePageDetail.ShowBtnOpen = false;
		List<int> effect = dataVIPLevel.effect;
		string text = string.Empty;
		int num = -1;
		List<string> list = new List<string>();
		for (int i = 0; i < effect.get_Count(); i++)
		{
			int key = effect.get_Item(i);
			VipXiaoGuo vipXiaoGuo = DataReader<VipXiaoGuo>.Get(key);
			if (vipXiaoGuo != null)
			{
				string chineseContent = GameDataUtils.GetChineseContent(vipXiaoGuo.name, true);
				if (!string.IsNullOrEmpty(chineseContent))
				{
					num++;
					if (num == 0)
					{
						text = chineseContent;
					}
					else
					{
						text = text + "\n" + chineseContent;
					}
					list.Add(chineseContent);
				}
			}
		}
		oOPrivilegePageDetail.VIPLevel1 = GameDataUtils.GetNumIcon1(dataVIPLevel.level, NumType.Yellow_big);
		oOPrivilegePageDetail.VIPLevel10 = GameDataUtils.GetNumIcon10(dataVIPLevel.level, NumType.Yellow_big);
		ChengHao chengHao = DataReader<ChengHao>.Get(dataVIPLevel.titleId);
		oOPrivilegePageDetail.ImageDetialTitleBgVisibility = false;
		if (chengHao != null)
		{
			oOPrivilegePageDetail.ImageDetialTitleBg = GameDataUtils.GetIcon(chengHao.icon);
			oOPrivilegePageDetail.ImageDetialTitleBgVisibility = true;
		}
		oOPrivilegePageDetail.EffectContent = string.Empty;
		oOPrivilegePageDetail.UpdateDiamondCount(num + 1, list);
		return oOPrivilegePageDetail;
	}

	private void RefreshItemsOfCharge()
	{
		List<RechargeGoodsInfo> listRechargeGoodsInfo = RechargeManager.Instance.listRechargeGoodsInfo;
		if (listRechargeGoodsInfo == null)
		{
			return;
		}
		listRechargeGoodsInfo.Sort((RechargeGoodsInfo a, RechargeGoodsInfo b) => a.order.CompareTo(b.order));
		for (int i = 0; i < listRechargeGoodsInfo.get_Count(); i++)
		{
			RechargeGoodsInfo rechargeGoodsInfo = listRechargeGoodsInfo.get_Item(i);
			if (rechargeGoodsInfo.order != 0)
			{
				OORechargeUnit oORechargeUnit = new OORechargeUnit();
				oORechargeUnit.ID = rechargeGoodsInfo.ID;
				oORechargeUnit.RefreshFXOfMonthCard = false;
				oORechargeUnit.PriceNum = "￥ " + rechargeGoodsInfo.rmb;
				oORechargeUnit.MCDayVisibility = false;
				oORechargeUnit.PriceVisibility = true;
				oORechargeUnit.ExtraEveryDayVisibility = false;
				string extraNum = string.Empty;
				if (RechargeManager.Instance.IsFirstRecharge(rechargeGoodsInfo.ID) && rechargeGoodsInfo.firstDiamonds > 0)
				{
					oORechargeUnit.VIPExtraVisibility = true;
					extraNum = string.Format(GameDataUtils.GetChineseContent(508001, false), rechargeGoodsInfo.addDiamonds + rechargeGoodsInfo.firstDiamonds);
				}
				else if (rechargeGoodsInfo.addDiamonds > 0)
				{
					oORechargeUnit.VIPExtraVisibility = true;
					extraNum = string.Format(GameDataUtils.GetChineseContent(508000, false), rechargeGoodsInfo.addDiamonds);
				}
				else
				{
					oORechargeUnit.VIPExtraVisibility = false;
				}
				oORechargeUnit.ExtraNum = extraNum;
				bool flag = rechargeGoodsInfo.result == 2;
				int addDiamonds;
				if (flag)
				{
					Dict dict = rechargeGoodsInfo.dropID.get_Item(0);
					oORechargeUnit.RType = RechargeManager.RechargeType.Box;
					oORechargeUnit.TreasureIcon = GameDataUtils.GetIcon(rechargeGoodsInfo.diamondsIcon);
					addDiamonds = (int)(rechargeGoodsInfo.rmb * 10f);
					oORechargeUnit.TodayBoughtVisibility = (RechargeManager.Instance.GetTodayRechargeCount(rechargeGoodsInfo.ID) > 0);
					oORechargeUnit.DiamondVisibility = false;
					oORechargeUnit.TreasureVisibility = true;
					oORechargeUnit.VIPExtraVisibility = true;
					oORechargeUnit.ExtraNum = GameDataUtils.GetItemName(dict.key, false, 0L);
					if (RechargeManager.Instance.GetTodayRechargeCount(rechargeGoodsInfo.ID) > 0)
					{
						oORechargeUnit.TreasureName = string.Empty;
					}
					else
					{
						oORechargeUnit.TreasureName = "每日限购一次宝箱";
					}
				}
				else
				{
					oORechargeUnit.RType = RechargeManager.RechargeType.Diamond;
					oORechargeUnit.DiamondNum = rechargeGoodsInfo.diamonds + "钻石";
					oORechargeUnit.BigIcon = GameDataUtils.GetIcon(rechargeGoodsInfo.diamondsIcon);
					addDiamonds = rechargeGoodsInfo.diamonds;
					oORechargeUnit.TodayBoughtVisibility = false;
					oORechargeUnit.DiamondVisibility = true;
					oORechargeUnit.TreasureVisibility = false;
				}
				oORechargeUnit.VIPTipVisibility = false;
				int num = VIPManager.Instance.ReachVIPLevel(addDiamonds);
				if (num > EntityWorld.Instance.EntSelf.VipLv)
				{
					oORechargeUnit.VIPTipVisibility = true;
					oORechargeUnit.VIPTipNum = string.Format(GameDataUtils.GetChineseContent(508002, false), num);
				}
				this.RechargeUnitItems.Add(oORechargeUnit);
			}
		}
	}

	private void RefreshItemsOfMonthCard()
	{
		List<YueQia> dataList = DataReader<YueQia>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			YueQia yueQia = dataList.get_Item(i);
			MonthCardInfo monthCardInfo = RechargeManager.Instance.GetMonthCardInfo(yueQia.ID);
			if (monthCardInfo != null)
			{
				OORechargeUnit oORechargeUnit = new OORechargeUnit();
				oORechargeUnit.ID = yueQia.ID;
				oORechargeUnit.RType = RechargeManager.RechargeType.MonthCard;
				oORechargeUnit.BigIcon = ResourceManager.GetIconSprite("icon_yueka");
				oORechargeUnit.PriceNum = "￥ " + yueQia.rmb;
				oORechargeUnit.DiamondNum = GameDataUtils.GetChineseContent(yueQia.name, false);
				if (monthCardInfo.hadBuyFlag)
				{
					oORechargeUnit.RefreshFXOfMonthCard = false;
					oORechargeUnit.VIPExtraVisibility = false;
					oORechargeUnit.PriceVisibility = false;
					oORechargeUnit.MCDayVisibility = true;
					oORechargeUnit.MCDayNum = string.Format(GameDataUtils.GetChineseContent(513004, false), yueQia.everydayDiamonds);
					oORechargeUnit.DaysNum = string.Format(GameDataUtils.GetChineseContent(513005, false), monthCardInfo.remainGetTimes);
					oORechargeUnit.VIPTipVisibility = false;
				}
				else
				{
					oORechargeUnit.RefreshFXOfMonthCard = true;
					oORechargeUnit.MCDayVisibility = false;
					oORechargeUnit.PriceVisibility = true;
					oORechargeUnit.VIPExtraVisibility = true;
					oORechargeUnit.ExtraNum = string.Format(GameDataUtils.GetChineseContent(508000, false), yueQia.diamonds);
					oORechargeUnit.ExtraEveryDayVisibility = true;
					oORechargeUnit.ExtraEveryDayNum = string.Format(GameDataUtils.GetChineseContent(513002, false), yueQia.everydayDiamonds);
					int num = VIPManager.Instance.ReachVIPLevel(yueQia.diamonds);
					if (num > EntityWorld.Instance.EntSelf.VipLv)
					{
						oORechargeUnit.VIPTipVisibility = true;
						oORechargeUnit.VIPTipNum = string.Format(GameDataUtils.GetChineseContent(508002, false), num);
					}
					else
					{
						oORechargeUnit.VIPTipVisibility = false;
					}
				}
				this.RechargeUnitItems.Add(oORechargeUnit);
			}
		}
	}
}
