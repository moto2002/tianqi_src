using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HuntBuyTimeUI : UIBase
{
	private Image mBuffIcon;

	private Image mCoinIcon;

	private Text mTxDesc;

	private Text mTxTimes;

	private Text mTxTips;

	private Text mTxPrice;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.RefreshUI();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mBuffIcon = base.FindTransform("Icon").GetComponent<Image>();
		this.mCoinIcon = base.FindTransform("CoinIcon").GetComponent<Image>();
		this.mTxDesc = base.FindTransform("txDesc").GetComponent<Text>();
		this.mTxTimes = base.FindTransform("txTimes").GetComponent<Text>();
		this.mTxTips = base.FindTransform("txTips").GetComponent<Text>();
		this.mTxPrice = base.FindTransform("txPrice").GetComponent<Text>();
		base.FindTransform("BtnConfirm").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickConfirm);
	}

	private void RefreshUI()
	{
		this.mTxDesc.set_text(string.Format(GameDataUtils.GetChineseContent(511624, false), TimeConverter.GetTime(HuntManager.Instance.BuyMinTime, TimeFormat.DHHMM_Chinese)));
		this.mTxTimes.set_text(string.Format(GameDataUtils.GetChineseContent(511625, false), Mathf.Max(0, HuntManager.Instance.CanBuyTimes - HuntManager.Instance.DayBuyTimes)));
		this.mTxPrice.set_text("x" + HuntManager.Instance.BuyTimePrice);
		this.mTxTips.set_text(GameDataUtils.GetChineseContent(511626, false));
	}

	private void OnClickConfirm(GameObject go)
	{
		HuntManager.Instance.SendBuyHookTimeReq();
		this.Show(false);
	}
}
