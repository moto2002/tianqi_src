using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LimitTimeShoppingUIView : UIBase
{
	public static LimitTimeShoppingUIView Instance;

	private ListPool m_listPool;

	private Text m_lblTime;

	protected override void Preprocessing()
	{
		base.Preprocessing();
	}

	private void Awake()
	{
		LimitTimeShoppingUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_listPool = base.FindTransform("ListItems").GetComponent<ListPool>();
		this.m_listPool.SetItem("LimitTimeShoppingUnit");
		this.m_listPool.isAnimation = false;
		this.m_lblTime = base.FindTransform("Time").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		LimitTimeMarketManager.Instance.SendTimeLimitedSales(1);
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			LimitTimeShoppingUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnActivation()
	{
	}

	private void OnClickBtnAdd()
	{
	}

	public void RefreshUI()
	{
		this.RefreshGoods();
		this.RefreshTimeDesc();
	}

	private void RefreshGoods()
	{
		List<SingleGoods> goods = LimitTimeMarketManager.Instance.GetCurrentShowGoods();
		if (goods == null)
		{
			this.m_listPool.Create(0, null);
			return;
		}
		this.m_listPool.Create(goods.get_Count(), delegate(int index)
		{
			if (index < goods.get_Count() && index < this.m_listPool.Items.get_Count())
			{
				LimitTimeShoppingUnit component = this.m_listPool.Items.get_Item(index).GetComponent<LimitTimeShoppingUnit>();
				SingleGoods singleGoods = goods.get_Item(index);
				component.goodsNumber = singleGoods.goodsNumber;
				component.SetItem(singleGoods.goodsInfo.cfgId, singleGoods.goodsInfo.count);
				component.SetPrice(singleGoods.priceInfo.originalCost, singleGoods.priceInfo.price);
				component.SetCount(LimitTimeMarketManager.Instance.GetRemainBuyCount(singleGoods.goodsNumber));
				component.SetEndTime(singleGoods.limitSecond);
			}
		});
	}

	private void RefreshTimeDesc()
	{
		this.m_lblTime.set_text(string.Format("活动时间：{0}~{1}", LimitTimeMarketManager.Instance.GetActivityTimeBegin(), LimitTimeMarketManager.Instance.GetActivityTimeEnd()));
	}
}
