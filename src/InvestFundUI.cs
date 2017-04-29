using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InvestFundUI : UIBase
{
	public static InvestFundUI Instance;

	private Text m_textTime;

	private Text m_textBuyCost;

	private GameObject m_goBtnBuy;

	private GridLayoutGroup m_awardlist;

	private void Awake()
	{
		InvestFundUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_awardlist = base.FindTransform("Scrollview_award").FindChild("List_award").GetComponent<GridLayoutGroup>();
		this.m_textTime = base.FindTransform("Text_activityTime").GetComponent<Text>();
		Transform transform = base.FindTransform("Btn_buy");
		this.m_goBtnBuy = transform.get_gameObject();
		this.m_goBtnBuy.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBuy);
		this.m_textBuyCost = transform.FindChild("Text_buy_cost").GetComponent<Text>();
		transform.FindChild("Text_btnBuy").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(513170, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
		this.InitAwardList();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			InvestFundUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateInvestFundPush, new Callback(this.InvestFundPushCallBack));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.UpdateInvestFundPush, new Callback(this.InvestFundPushCallBack));
		base.RemoveListeners();
	}

	private void InitAwardList()
	{
		this.RefreshScroll();
		this.ScrollToAvailableCell();
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_awardlist.get_transform().get_childCount(); i++)
		{
			this.m_awardlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void RefreshScroll()
	{
	}

	private void AddScrollCell(int index, FundListPush.Items info)
	{
		Transform transform = this.m_awardlist.get_transform().FindChild("InvestFundItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<InvestFundItem>().UpdateItem(index + 1, info, new ButtonCustom.VoidDelegateObj(this.OnCellBtnClick));
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InvestFundItem");
			instantiate2Prefab.get_transform().SetParent(this.m_awardlist.get_transform(), false);
			instantiate2Prefab.set_name("InvestFundItem" + index);
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.GetComponent<InvestFundItem>().UpdateItem(index + 1, info, new ButtonCustom.VoidDelegateObj(this.OnCellBtnClick));
		}
	}

	private void OnCellBtnClick(GameObject sender)
	{
	}

	private void ScrollToAvailableCell()
	{
	}

	private void RefreshUI()
	{
	}

	private void InvestFundPushCallBack()
	{
		this.RefreshUI();
		this.RefreshScroll();
	}

	private void InvestFundBuyCallBack()
	{
	}

	private void OnClickBtnBuy(GameObject sender)
	{
	}
}
