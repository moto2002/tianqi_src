using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LimitTimeShoppingUnit : BaseUIBehaviour
{
	public long goodsNumber;

	private Text m_lblItemName;

	private Image m_spItemFrame;

	private Image m_spItemIcon;

	private Text m_lblItemNum;

	private GameObject m_goItemFlag;

	private Text m_lblTimeNum;

	private Text m_lblCountNum;

	private Text m_lblPriceRealNum;

	private Text m_lblPriceNowNum;

	private DateTime end_dateTime;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_lblItemName = base.FindTransform("ItemName").GetComponent<Text>();
		this.m_spItemFrame = base.FindTransform("ItemFrame").GetComponent<Image>();
		this.m_spItemIcon = base.FindTransform("ItemIcon").GetComponent<Image>();
		this.m_lblItemNum = base.FindTransform("ItemNum").GetComponent<Text>();
		this.m_goItemFlag = base.FindTransform("ItemFlag").get_gameObject();
		this.m_lblTimeNum = base.FindTransform("TimeNum").GetComponent<Text>();
		this.m_lblCountNum = base.FindTransform("CountNum").GetComponent<Text>();
		this.m_lblPriceRealNum = base.FindTransform("PriceRealNum").GetComponent<Text>();
		this.m_lblPriceNowNum = base.FindTransform("PriceNowNum").GetComponent<Text>();
		base.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickButton));
	}

	private void OnClickButton()
	{
		SingleGoods good = LimitTimeMarketManager.Instance.GetGood(this.goodsNumber);
		if (good == null)
		{
			return;
		}
		if (this.IsTimeOver())
		{
			UIManagerControl.Instance.ShowToastText("购买日期截止,无法购买");
			return;
		}
		UIManagerControl.Instance.OpenUI("BuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		BuyUIViewModel.Instance.BuyCallback = delegate(int count)
		{
			this.DoOK();
		};
		BuyUIViewModel.Instance.RefreshInfo(good.goodsInfo.cfgId, good.goodsInfo.count, good.priceInfo.price, 1);
	}

	private void DoOK()
	{
		LimitTimeMarketManager.Instance.SendTimeLimitedSalesBuy(this.goodsNumber);
	}

	public void SetItem(int itemId, int itemNum)
	{
		GameDataUtils.SetItem(itemId, this.m_spItemFrame, this.m_spItemIcon, this.m_lblItemName, true);
		this.m_lblItemNum.set_text(itemNum.ToString());
	}

	public void SetCount(int count)
	{
		this.m_lblCountNum.set_text(count.ToString());
		this.m_goItemFlag.SetActive(count <= 0);
	}

	public void SetPrice(int priceReal, int priceNow)
	{
		this.m_lblPriceRealNum.set_text("x" + priceReal);
		this.m_lblPriceNowNum.set_text("x" + priceNow);
	}

	public void SetEndTime(int seconds)
	{
		this.end_dateTime = TimeManager.Instance.CalculateLocalServerTimeBySecond(seconds);
		this.SetRemainTime();
	}

	private void Update()
	{
		this.SetRemainTime();
	}

	private void SetRemainTime()
	{
		int remainSecond = TimeManager.Instance.GetRemainSecond(this.end_dateTime);
		this.SetRemainTime(remainSecond);
	}

	private void SetRemainTime(int seconds)
	{
		string time = TimeConverter.GetTime(seconds, TimeFormat.HHMMSS);
		if (seconds > 0)
		{
			this.m_lblTimeNum.set_text(time);
		}
		else
		{
			this.m_lblTimeNum.set_text(TextColorMgr.GetColorByID(time, 1000007));
		}
	}

	private bool IsTimeOver()
	{
		return TimeManager.Instance.GetRemainSecond(this.end_dateTime) <= 0;
	}
}
