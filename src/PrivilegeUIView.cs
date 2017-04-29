using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class PrivilegeUIView : UIBase
{
	public static PrivilegeUIView Instance;

	private Text m_lblPrivilegeDetailPageNum;

	public int PrivilegeDetailPageIndex;

	public int PrivilegePageIndex;

	public ScrollRectCustom PrivilegeDetailSR;

	public GameObject btnRechargeObj;

	public GameObject btnDetailObj;

	public GameObject CommonBtnUnSelect1;

	public GameObject CommonBtnUnSelect2;

	public GameObject CommonBtnUnSelect3;

	public GameObject CommonBtnUnSelect4;

	public Text CommonBtnText1;

	public Text CommonBtnText2;

	public Text CommonBtnText3;

	public Text CommonBtnText4;

	public GameObject BtnTips3;

	public GameObject BtnTips4;

	public GameObject Bgs4;

	public GameObject VIPExp;

	public GameObject PrivilegeInvest;

	private int fxBackground;

	private int fxGold;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		VIPManager.Instance.SendPushVip();
		PrivilegeUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.AddFX();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		RechargeManager.Instance.SendRechargeGoodsReq();
		FirstPayManager.Instance.StateTOVIP = FirstPayManager.Instance.State;
		this.OnPayTipChange();
		VIPManager.Instance.SendLimitCardTimeReq();
		EventDispatcher.Broadcast<bool>(EventNames.ShowPayUI, true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.DeleteFX();
			PrivilegeUIView.Instance = null;
			PrivilegeUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		base.OnClickMaskAction();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
		EventDispatcher.Broadcast<bool>(EventNames.ShowPayUI, false);
	}

	public void OnClose()
	{
		this.OnClickMaskAction();
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnRechargeText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508005, false));
		base.FindTransform("BtnDetailText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508026, false));
		this.btnRechargeObj = base.FindTransform("BtnRecharge").get_gameObject();
		this.btnDetailObj = base.FindTransform("BtnDetail").get_gameObject();
		this.m_lblPrivilegeDetailPageNum = base.FindTransform("PrivilegeDetailPageNum").GetComponent<Text>();
		this.PrivilegeDetailSR = base.FindTransform("PrivilegeDetailSR").GetComponent<ScrollRectCustom>();
		this.CommonBtnUnSelect1 = base.FindTransform("BtnUnSelect1").get_gameObject();
		this.CommonBtnUnSelect2 = base.FindTransform("BtnUnSelect2").get_gameObject();
		this.CommonBtnUnSelect3 = base.FindTransform("BtnUnSelect3").get_gameObject();
		this.CommonBtnUnSelect4 = base.FindTransform("BtnUnSelect4").get_gameObject();
		this.CommonBtnText1 = base.FindTransform("BtnNameText1").GetComponent<Text>();
		this.CommonBtnText2 = base.FindTransform("BtnNameText2").GetComponent<Text>();
		this.CommonBtnText3 = base.FindTransform("BtnNameText3").GetComponent<Text>();
		this.CommonBtnText4 = base.FindTransform("BtnNameText4").GetComponent<Text>();
		this.BtnTips3 = base.FindTransform("BtnTips3").get_gameObject();
		this.BtnTips4 = base.FindTransform("BtnTips4").get_gameObject();
		this.Bgs4 = base.FindTransform("Bgs4").get_gameObject();
		this.VIPExp = base.FindTransform("VIP").get_gameObject();
		this.PrivilegeInvest = base.FindTransform("PrivilegeInvest").get_gameObject();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("PrivilegeDetailList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "PrivilegePageDetail";
		listBinder.SourceBinding.MemberName = "PrivilegePageDetails";
		listBinder.ShiftBinding.MemberName = "PrivilegeDetailShiftType";
		ScrollRectCustom component = base.FindTransform("PrivilegeDetailSR").GetComponent<ScrollRectCustom>();
		component.movePage = true;
		component.OnPageChanged = delegate(int pageIndex)
		{
			this.m_lblPrivilegeDetailPageNum.set_text(pageIndex.ToString());
			this.PrivilegeDetailPageIndex = pageIndex;
			this.SetPage(pageIndex);
		};
		listBinder = base.FindTransform("VipBtnList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "VipBtn";
		listBinder.SourceBinding.MemberName = "VipBtns";
		listBinder.ShiftBinding.MemberName = "VipBtnsType";
		listBinder = base.FindTransform("RechargeList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "RechargeUnit";
		listBinder.SourceBinding.MemberName = "RechargeUnitItems";
		ScrollRectCustom component2 = base.FindTransform("RechargeSR").GetComponent<ScrollRectCustom>();
		component2.movePage = false;
		listBinder = base.FindTransform("CardList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "CardItem";
		listBinder.SourceBinding.MemberName = "LimitCardItems";
		ListShiftBinder listShiftBinder = base.FindTransform("RechargeList").get_gameObject().AddComponent<ListShiftBinder>();
		listShiftBinder.BindingProxy = base.get_gameObject();
		listShiftBinder.ShiftBinding.MemberName = "RechargeShiftType";
		listShiftBinder = base.FindTransform("CardList").get_gameObject().AddComponent<ListShiftBinder>();
		listShiftBinder.BindingProxy = base.get_gameObject();
		listShiftBinder.ShiftBinding.MemberName = "CardListType";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("PrivilegeDetailRegion").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "PrivilegeDetailVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("RechargeRegion").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "RechargeVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("PrivilegeLimitRegion").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "LimitVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("VIPNextRegion").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "VIPNextVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("viplevelUpTip").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "VIPLevelUpTipVisisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("VIPNextNum").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "VIPNextNumVisisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("VIPNextTitle").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "VIPNextTitleVisisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("VIPDayExp").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "VIPDayExpVisisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageTitleInfo1").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageTitleInfo1Visisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageTitleInfo2").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageTitleInfo2Visisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageTitleInfo3").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageTitleInfo3Visisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("CardBg1").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "CardBg1Visisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("CardBg2").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "CardBg2Visisbility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("CardBg3").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "CardBg3Visisbility";
		FillAmmountBinder fillAmmountBinder = base.FindTransform("VIPBarFg").get_gameObject().AddComponent<FillAmmountBinder>();
		fillAmmountBinder.BindingProxy = base.get_gameObject();
		fillAmmountBinder.FillValueBinding.MemberName = "VIPFillAmount";
		TextBinder textBinder = base.FindTransform("VIPProgress").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPProgress";
		textBinder = base.FindTransform("VIPNextNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPNextNum";
		textBinder = base.FindTransform("VIPNextLevel").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPNextLevel";
		textBinder = base.FindTransform("VIPNextTitle").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPNextTitle";
		textBinder = base.FindTransform("vipNowLv").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPLVNow";
		textBinder = base.FindTransform("viplevelUpTip").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPLevelUpTip";
		textBinder = base.FindTransform("VIPDayExp").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPDayExp";
		textBinder = base.FindTransform("VIPExpInfo").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "VIPExpInfo";
		textBinder = base.FindTransform("CardEffectContent").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "CardEffectContent";
		textBinder.SetHeight = true;
		textBinder = base.FindTransform("TextTitleStr").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TextTitleStr";
		textBinder = base.FindTransform("TextTitleValue").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TextTitleValue";
		ImageBinder imageBinder = base.FindTransform("ImageTitle").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageTitleIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("VIPNowLevel10").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPNowLevel10";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("VIPNowLevel1").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPNowLevel1";
		imageBinder.SetNativeSize = true;
		textBinder = base.FindTransform("TextPower").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TextPower";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnRecharge").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnRechargeUp";
		buttonBinder = base.FindTransform("BtnDetail").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnBackToPrivilege";
		buttonBinder = base.FindTransform("CommonBtn1").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnVipLimit";
		buttonBinder = base.FindTransform("CommonBtn2").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnVipLv";
		buttonBinder = base.FindTransform("CommonBtn3").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnVipRecharge";
		buttonBinder = base.FindTransform("CommonBtn4").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnInvest";
		buttonBinder = base.FindTransform("BtnLimitCardBuy").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnLimitCardBuy";
		buttonBinder = base.FindTransform("CloseBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnClose";
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("OnRechargeTipChange", new Callback(this.OnPayTipChange));
		EventDispatcher.AddListener("RechargeManager.RechargeGoodsInfoUpdate", new Callback(this.OnRechargeGoodsInfoUpdate));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener("OnRechargeTipChange", new Callback(this.OnPayTipChange));
		EventDispatcher.AddListener("RechargeManager.RechargeGoodsInfoUpdate", new Callback(this.OnRechargeGoodsInfoUpdate));
	}

	private void OnPayTipChange()
	{
		this.BtnTips3.SetActive(RechargeManager.Instance.IsShowTipsOfBox);
		this.BtnTips4.SetActive(InvestFundManager.Instance.IsShowTipsOfCanBuy || InvestFundManager.Instance.IsShowTipsOfCanGet);
	}

	private void OnRechargeGoodsInfoUpdate()
	{
		if (PrivilegeUIViewModel.Instance != null)
		{
			PrivilegeUIViewModel.Instance.RefreshSwitchMode();
		}
	}

	public void SetRechargeBtnVisible(int showType)
	{
		if (this.btnRechargeObj != null)
		{
			bool active = showType != 3;
			this.btnRechargeObj.SetActive(active);
		}
	}

	public void SetLimitCardBtnState(bool state)
	{
		Image component = base.FindTransform("ImageLight").GetComponent<Image>();
		ImageColorMgr.SetImageColor(component, !state);
	}

	private void SetPage(int pageIndex)
	{
		int vipLv = pageIndex + 1;
		if (PrivilegeUIViewModel.Instance != null)
		{
			PrivilegeUIViewModel.Instance.SetCurrentPageVIPLv(pageIndex, vipLv);
		}
	}

	public void ResetSelectBtnState(int showType)
	{
		if (showType == 1)
		{
			this.CommonBtnUnSelect1.SetActive(false);
			this.CommonBtnText1.set_color(new Color32(255, 255, 234, 255));
		}
		else
		{
			this.CommonBtnUnSelect1.SetActive(true);
			this.CommonBtnText1.set_color(new Color32(255, 219, 146, 255));
		}
		if (showType == 2)
		{
			this.CommonBtnUnSelect2.SetActive(false);
			this.CommonBtnText2.set_color(new Color32(255, 255, 234, 255));
		}
		else
		{
			this.CommonBtnUnSelect2.SetActive(true);
			this.CommonBtnText2.set_color(new Color32(255, 219, 146, 255));
		}
		if (showType == 3)
		{
			this.CommonBtnUnSelect3.SetActive(false);
			this.CommonBtnText3.set_color(new Color32(255, 255, 234, 255));
		}
		else
		{
			this.CommonBtnUnSelect3.SetActive(true);
			this.CommonBtnText3.set_color(new Color32(255, 219, 146, 255));
		}
		if (showType == 4)
		{
			this.CommonBtnUnSelect4.SetActive(false);
			this.CommonBtnText4.set_color(new Color32(255, 255, 234, 255));
		}
		else
		{
			this.CommonBtnUnSelect4.SetActive(true);
			this.CommonBtnText4.set_color(new Color32(255, 219, 146, 255));
		}
	}

	public void SetVipExpVisible(bool isShow)
	{
		this.Bgs4.SetActive(isShow);
		this.VIPExp.SetActive(isShow);
	}

	public void ShowInvestUI(bool isShow)
	{
		this.PrivilegeInvest.SetActive(isShow);
		if (isShow)
		{
			if (this.PrivilegeInvest.get_transform().get_childCount() > 0)
			{
				Transform child = this.PrivilegeInvest.get_transform().GetChild(0);
				child.get_gameObject().SetActive(true);
			}
			else
			{
				UIManagerControl.Instance.OpenUI("InvestUI", this.PrivilegeInvest.get_transform(), false, UIType.NonPush);
			}
		}
	}

	private void AddFX()
	{
		this.fxBackground = FXSpineManager.Instance.ReplaySpine(this.fxBackground, 701, base.get_transform(), "PrivilegeUI", 2010, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fxGold = FXSpineManager.Instance.ReplaySpine(this.fxGold, 709, base.FindTransform("CardBg3"), "PrivilegeUI", 3010, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fxBackground, true);
		FXSpineManager.Instance.DeleteSpine(this.fxGold, true);
	}
}
