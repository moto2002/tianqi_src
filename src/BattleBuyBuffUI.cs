using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleBuyBuffUI : UIBase
{
	private Image mBuffIcon;

	private Image mCoinIcon;

	private Text mBuffCount;

	private Text mBuffName;

	private Text mBuffEffect;

	private Text mTxDesc;

	private Text mTxPrice;

	private ButtonCustom mBtnConfirm;

	private GameObject mGoConfirmFg;

	private FZengYibuffPeiZhi mData;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		this.isInterruptStick = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mBuffIcon = base.FindTransform("SpecialBuffIcon").GetComponent<Image>();
		this.mCoinIcon = base.FindTransform("CoinIcon").GetComponent<Image>();
		this.mBuffCount = base.FindTransform("SpecialBuffCount").GetComponent<Text>();
		this.mBuffName = base.FindTransform("SpecialBuffName").GetComponent<Text>();
		this.mBuffEffect = base.FindTransform("SpecialBuffEffect").GetComponent<Text>();
		this.mTxDesc = base.FindTransform("txDesc").GetComponent<Text>();
		this.mTxPrice = base.FindTransform("txPrice").GetComponent<Text>();
		this.mGoConfirmFg = base.FindTransform("BtnConfirmFg").get_gameObject();
		this.mBtnConfirm = base.FindTransform("BtnConfirm").GetComponent<ButtonCustom>();
		this.mBtnConfirm.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickConfirm);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.BuySpecialBuffSuccess, new Callback(this.RefreshUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.BuySpecialBuffSuccess, new Callback(this.RefreshUI));
	}

	private void RefreshUI()
	{
		int num = SpecialFightManager.Instance.CurBuffCount + 1;
		if (num <= DataReader<FZengYibuffPeiZhi>.DataList.get_Count())
		{
			this.mData = DataReader<FZengYibuffPeiZhi>.Get(num);
			if (this.mData != null)
			{
				ResourceManager.SetSprite(this.mBuffIcon, GameDataUtils.GetIcon(this.mData.icon));
				ResourceManager.SetSprite(this.mCoinIcon, GameDataUtils.GetIcon(DataReader<Items>.Get(this.mData.coinType).littleIcon));
				this.mBuffCount.set_text(num.ToString());
				this.mBuffName.set_text(GameDataUtils.GetChineseContent(this.mData.buffName, false));
				this.mBuffEffect.set_text(GameDataUtils.GetChineseContent(this.mData.descId, false));
				this.mTxDesc.set_text(GameDataUtils.GetChineseContent(DataReader<FJingYanFuBenPeiZhi>.Get("buffDescId").num, false));
				this.mTxPrice.set_text("x" + this.mData.price);
			}
			this.mGoConfirmFg.SetActive(true);
		}
		else
		{
			this.mGoConfirmFg.SetActive(false);
		}
	}

	private void OnClickConfirm(GameObject go)
	{
		if (SpecialFightManager.Instance.CurBuffCount < 10)
		{
			SpecialFightManager.Instance.BuyExperienceCopyBuffReq(this.mData.id);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502323, false));
		}
	}
}
