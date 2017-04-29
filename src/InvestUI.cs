using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestUI : UIBase
{
	public const int rechargeId = 11;

	public static InvestUI Instance;

	private GridLayoutGroup m_ScrollList;

	private RectTransform m_ScrollInvest;

	private Transform panelBtnSpine;

	private int m_GetSpineTag;

	private void Awake()
	{
		InvestUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_ScrollList = base.FindTransform("ScrollList").GetComponent<GridLayoutGroup>();
		this.m_ScrollInvest = base.FindTransform("ScrollInvest").GetComponent<RectTransform>();
		XiTongCanShu xiTongCanShu = DataReader<XiTongCanShu>.Get("cost");
		XiTongCanShu xiTongCanShu2 = DataReader<XiTongCanShu>.Get("restoreImmediately");
		base.FindTransform("TextDesc").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(508200, false), xiTongCanShu.value, xiTongCanShu2.value));
		base.FindTransform("BtnInvest").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnInvest);
		this.panelBtnSpine = base.FindTransform("PanelBtnSpine");
		this.panelBtnSpine.set_localScale(new Vector3(1f, 1f, 1f));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		RechargeManager.Instance.SendRechargeGoodsReq();
		this.RefreshScroll();
		this.ScrollToAvailableCell();
		this.PlayCanBuySpine(!InvestFundManager.Instance.hasBuy);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			InvestUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnInvestPushInfo, new Callback(this.OnPushInfoCallBack));
		EventDispatcher.AddListener("OnRechargeTipChange", new Callback(this.OnPayTipChangeCallBack));
		EventDispatcher.AddListener("RechargeManager.RechargeGoodsInfoUpdate", new Callback(this.OnRechargeGoodsInfoUpdate));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnInvestPushInfo, new Callback(this.OnPushInfoCallBack));
		EventDispatcher.RemoveListener("OnRechargeTipChange", new Callback(this.OnPayTipChangeCallBack));
		EventDispatcher.RemoveListener("RechargeManager.RechargeGoodsInfoUpdate", new Callback(this.OnRechargeGoodsInfoUpdate));
	}

	private void OnPushInfoCallBack()
	{
		this.RefreshScroll();
	}

	private void OnPayTipChangeCallBack()
	{
		this.PlayCanBuySpine(!InvestFundManager.Instance.hasBuy);
	}

	private void OnRechargeGoodsInfoUpdate()
	{
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_ScrollList.get_transform().get_childCount(); i++)
		{
			this.m_ScrollList.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void RefreshScroll()
	{
		this.ClearScroll();
		this.AddFirstScrollCell();
		List<GouMaiXiangGuan> dataList = DataReader<GouMaiXiangGuan>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			GouMaiXiangGuan config = dataList.get_Item(i);
			this.AddScrollCell(i, config);
		}
	}

	private void AddScrollCell(int index, GouMaiXiangGuan config)
	{
		RewardInfo investItemInfo = InvestFundManager.Instance.GetInvestItemInfo(config.type);
		bool canGet = false;
		bool hasGot = false;
		bool isOver = false;
		if (investItemInfo != null)
		{
			canGet = investItemInfo.canGet;
			hasGot = investItemInfo.hadGet;
			isOver = investItemInfo.overdue;
		}
		string title = string.Format(GameDataUtils.GetChineseContent(508201, false), config.type);
		string count = config.nameId.ToString();
		Transform transform = this.m_ScrollList.get_transform().FindChild("InvestItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<InvestItem>().UpdateItem(config.type, title, count, canGet, hasGot, isOver, new ButtonCustom.VoidDelegateObj(this.OnDayBtnClick));
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InvestItem");
			instantiate2Prefab.get_transform().SetParent(this.m_ScrollList.get_transform(), false);
			instantiate2Prefab.set_name("InvestItem" + index);
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.GetComponent<InvestItem>().UpdateItem(config.type, title, count, canGet, hasGot, isOver, new ButtonCustom.VoidDelegateObj(this.OnDayBtnClick));
		}
	}

	private void AddFirstScrollCell()
	{
		bool hasBuy = InvestFundManager.Instance.hasBuy;
		bool hasGet = InvestFundManager.Instance.hasGet;
		bool isOver = false;
		string chineseContent = GameDataUtils.GetChineseContent(508205, false);
		XiTongCanShu xiTongCanShu = DataReader<XiTongCanShu>.Get("restoreImmediately");
		string value = xiTongCanShu.value;
		Transform transform = this.m_ScrollList.get_transform().FindChild("InvestItemFirst");
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<InvestItem>().UpdateItem(0, chineseContent, value, hasBuy, hasGet, isOver, new ButtonCustom.VoidDelegateObj(this.OnFirstBtnClick));
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InvestItem");
			instantiate2Prefab.get_transform().SetParent(this.m_ScrollList.get_transform(), false);
			instantiate2Prefab.set_name("InvestItemFirst");
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.GetComponent<InvestItem>().UpdateItem(0, chineseContent, value, hasBuy, hasGet, isOver, new ButtonCustom.VoidDelegateObj(this.OnFirstBtnClick));
		}
	}

	private void OnDayBtnClick(GameObject sender)
	{
		InvestItem component = sender.get_transform().get_parent().GetComponent<InvestItem>();
		InvestFundManager.Instance.SendGetFundRewardReq(component.itemId);
	}

	private void OnFirstBtnClick(GameObject sender)
	{
		InvestFundManager.Instance.SendGetFundDiamondReq();
	}

	private void ScrollToAvailableCell()
	{
		Vector3 localPosition = new Vector3(0f, this.m_ScrollInvest.get_sizeDelta().y, 0f);
		if (InvestFundManager.Instance.hasBuy && InvestFundManager.Instance.hasGet)
		{
			List<RewardInfo> itemList = InvestFundManager.Instance.itemList;
			for (int i = 0; i < itemList.get_Count(); i++)
			{
				RewardInfo rewardInfo = itemList.get_Item(i);
				if (rewardInfo.canGet && !rewardInfo.hadGet && !rewardInfo.overdue)
				{
					localPosition.y += (float)(i + 1) * (this.m_ScrollList.get_cellSize().y + this.m_ScrollList.get_spacing().y);
					break;
				}
			}
		}
		this.m_ScrollList.GetComponent<RectTransform>().set_localPosition(localPosition);
	}

	private void OnClickBtnInvest(GameObject sender)
	{
		if (InvestFundManager.Instance.hasBuy)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(508202, false));
			return;
		}
		XiTongCanShu xiTongCanShu = DataReader<XiTongCanShu>.Get("openLv");
		if (int.Parse(xiTongCanShu.value) > EntityWorld.Instance.EntSelf.Lv)
		{
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(508204, false), xiTongCanShu.value));
			return;
		}
		RechargeGoodsInfo rechargeMonthGoodsInfo = RechargeManager.Instance.GetRechargeMonthGoodsInfo();
		int nowRechargeId = 11;
		string content = string.Empty;
		if (rechargeMonthGoodsInfo != null)
		{
			content = string.Format(GameDataUtils.GetChineseContent(508203, false), rechargeMonthGoodsInfo.rmb);
			nowRechargeId = rechargeMonthGoodsInfo.ID;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(string.Empty, content, null, delegate
		{
			RechargeManager.Instance.ExecutionToRechargeDiamond(nowRechargeId);
		}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void PlayCanBuySpine(bool isShow)
	{
		if (isShow && this.m_GetSpineTag == 0)
		{
			this.m_GetSpineTag = FXSpineManager.Instance.ReplaySpine(this.m_GetSpineTag, 4500, this.panelBtnSpine, "PrivilegeUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else if (!isShow && this.m_GetSpineTag != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_GetSpineTag, true);
			this.m_GetSpineTag = 0;
		}
	}
}
