using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GiftExchangeUI : UIBase
{
	public static GiftExchangeUI Instance;

	private InputField inputNumber;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		GiftExchangeUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.inputNumber = base.FindTransform("Input").GetComponent<InputField>();
		base.FindTransform("BtnConfirm").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOk);
		base.FindTransform("CloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnClose);
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			GiftExchangeUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	private void OnClickOk(GameObject go)
	{
		if (!string.IsNullOrEmpty(this.inputNumber.get_text().Trim()))
		{
			GiftExchangeManager.Instance.SendBuyPlanReq(this.inputNumber.get_text());
			this.OnClickMaskAction();
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("不能输入空格");
		}
	}

	private void OnClickBtnClose(GameObject go)
	{
		this.OnClickMaskAction();
	}
}
