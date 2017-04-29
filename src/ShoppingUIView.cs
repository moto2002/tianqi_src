using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class ShoppingUIView : UIBase
{
	public static ShoppingUIView Instance;

	public Transform Show1ItemSR;

	public Transform Show2ItemSR;

	[HideInInspector]
	public Transform Node2SwitchShops;

	[HideInInspector]
	public Transform Node2Page;

	private Text m_lblPageNum;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		ShoppingUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		UIManagerControl.Instance.OpenUI("PageUI", this.Node2Page, false, UIType.NonPush);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.Show1ItemSR.get_gameObject().SetActive(false);
		this.Show2ItemSR.get_gameObject().SetActive(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("RefreshTimeName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508010, false));
		base.FindTransform("RemainRefreshTimesName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(508033, false));
		this.Show1ItemSR = base.FindTransform("Show1ItemSR");
		this.Show2ItemSR = base.FindTransform("Show2ItemSR");
		this.Node2SwitchShops = base.FindTransform("Node2SwitchShops");
		this.Node2Page = base.FindTransform("Node2Page");
		this.m_lblPageNum = base.FindTransform("PageNum").GetComponent<Text>();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("Show1ItemList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "ShoppingPage";
		listBinder.SourceBinding.MemberName = "ItemList1";
		ScrollRectCustom itemSR1 = base.FindTransform("Show1ItemSR").GetComponent<ScrollRectCustom>();
		itemSR1.movePage = true;
		itemSR1.Arrow2First = base.FindTransform("ArrowL");
		itemSR1.Arrow2Last = base.FindTransform("ArrowR");
		itemSR1.OnPageChanged = delegate(int pageIndex)
		{
			this.m_lblPageNum.set_text((pageIndex + 1).ToString());
			PageUIView.Instance.SetPage(itemSR1.GetPageNum(), pageIndex);
			ShoppingUIViewModel.Instance.CurrentPageIndex = pageIndex;
		};
		ListShiftBinder listShiftBinder = base.FindTransform("Show1ItemList").get_gameObject().AddComponent<ListShiftBinder>();
		listShiftBinder.BindingProxy = base.get_gameObject();
		listShiftBinder.ShiftBinding.MemberName = "ShiftType";
		listBinder = base.FindTransform("Show2ItemList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "ShoppingPage2";
		listBinder.SourceBinding.MemberName = "ItemList2";
		ScrollRectCustom itemSR2 = base.FindTransform("Show2ItemSR").GetComponent<ScrollRectCustom>();
		itemSR2.movePage = true;
		itemSR2.Arrow2First = base.FindTransform("ArrowL");
		itemSR2.Arrow2Last = base.FindTransform("ArrowR");
		itemSR2.OnPageChanged = delegate(int pageIndex)
		{
			this.m_lblPageNum.set_text((pageIndex + 1).ToString());
			PageUIView.Instance.SetPage(itemSR2.GetPageNum(), pageIndex);
			ShoppingUIViewModel.Instance.CurrentPageIndex = pageIndex;
		};
		listShiftBinder = base.FindTransform("Show2ItemList").get_gameObject().AddComponent<ListShiftBinder>();
		listShiftBinder.BindingProxy = base.get_gameObject();
		listShiftBinder.ShiftBinding.MemberName = "ShiftType";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("BtnRefresh").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnRefreshVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("CurrentCoin").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "CurrentCoinVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("RemainRefreshTimesRegion").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "RemainRefreshTimesRegion";
		ImageBinder imageBinder = base.FindTransform("CurrentCoinIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "CurrentCoinIcon";
		imageBinder = base.FindTransform("RefreshCoinIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "RefreshCoinIcon";
		TextBinder textBinder = base.FindTransform("CurrentCoinNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "CurrentCoinNum";
		textBinder = base.FindTransform("MarketName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "MarketName";
		textBinder = base.FindTransform("RefreshTime").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "RefreshTime";
		textBinder = base.FindTransform("RemainRefreshTimes").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "RemainRefreshTimes";
		textBinder = base.FindTransform("BtnRefreshName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnRefreshName";
		textBinder = base.FindTransform("RefreshCoinNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "RefreshCoinNum";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("ArrowL").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnArrowLUp";
		buttonBinder = base.FindTransform("ArrowR").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnArrowRUp";
		buttonBinder = base.FindTransform("BtnRefresh").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnRefreshUp";
		buttonBinder = base.FindTransform("BtnClose").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnCloseUp";
		buttonBinder = base.FindTransform("BtnSwitch").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnSwtichUp";
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.DiamondChanged, new Callback(this.OnSelfAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.GoldChanged, new Callback(this.OnSelfAttrChangedNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.CompetitiveCurrencyChanged, new Callback(this.OnSelfAttrChangedNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.DiamondChanged, new Callback(this.OnSelfAttrChangedNty));
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.GoldChanged, new Callback(this.OnSelfAttrChangedNty));
		EventDispatcher.RemoveListener(ParticularCityAttrChangedEvent.CompetitiveCurrencyChanged, new Callback(this.OnSelfAttrChangedNty));
	}

	private void OnSelfAttrChangedNty()
	{
		BaseMarketManager.CurrentManagerInstance.RefreshShop();
	}
}
