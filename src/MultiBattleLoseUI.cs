using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiBattleLoseUI : UIBase
{
	public static MultiBattleLoseUI Instance;

	public Action exitCallBack;

	public Action againCallBack;

	private Text countDownText;

	private Transform exitBtnTrans;

	private Transform againBtnTrans;

	private TimeCountDown timeCountDown;

	private void Awake()
	{
		MultiBattleLoseUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		this.countDownText = base.FindTransform("countDownText").GetComponent<Text>();
		this.exitBtnTrans = base.FindTransform("BtnExitNode");
		this.againBtnTrans = base.FindTransform("BtnNextNode");
		ButtonCustom expr_48 = base.FindTransform("BtnExit").GetComponent<ButtonCustom>();
		expr_48.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_48.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickExit));
		ButtonCustom expr_79 = base.FindTransform("BtnNext").GetComponent<ButtonCustom>();
		expr_79.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_79.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickAgain));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			MultiBattleLoseUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickExit(GameObject go)
	{
		if (this.exitCallBack != null)
		{
			this.exitCallBack.Invoke();
		}
		UIManagerControl.Instance.UnLoadUIPrefab("MultiBattleLoseUI");
	}

	private void OnClickAgain(GameObject go)
	{
		if (this.againCallBack != null)
		{
			this.againCallBack.Invoke();
		}
		UIManagerControl.Instance.UnLoadUIPrefab("MultiBattleLoseUI");
	}

	private void OnClickStaticsUp(GameObject go)
	{
	}

	public void OnCountDown(int countTime, Action countDownEndAction)
	{
		this.timeCountDown = new TimeCountDown(countTime, TimeFormat.SECOND, delegate
		{
			if (this.countDownText != null)
			{
				this.countDownText.set_text(string.Format("<color=green>" + this.timeCountDown.GetSeconds() + "秒</color>", new object[0]) + "后自动离开");
			}
		}, delegate
		{
			if (countDownEndAction != null)
			{
				countDownEndAction.Invoke();
			}
			UIManagerControl.Instance.UnLoadUIPrefab("MultiBattleLoseUI");
		}, true);
	}

	public void ShowButton(bool isShowExitBtn, bool isShowAgainBtn, bool isShowCountDown, bool isShowStatics = false)
	{
		if (this.exitBtnTrans.get_gameObject().get_activeSelf() != isShowExitBtn)
		{
			this.exitBtnTrans.get_gameObject().SetActive(isShowExitBtn);
		}
		if (this.againBtnTrans.get_gameObject().get_activeSelf() != isShowAgainBtn)
		{
			this.againBtnTrans.get_gameObject().SetActive(isShowAgainBtn);
		}
		if (this.countDownText.get_gameObject().get_activeSelf() != isShowCountDown)
		{
			this.countDownText.get_gameObject().SetActive(isShowCountDown);
		}
	}

	public void StopCountDown()
	{
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
	}
}
