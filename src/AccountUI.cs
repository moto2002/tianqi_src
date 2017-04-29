using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AccountUI : UIBase
{
	private InputField account;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.55f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.account = base.FindTransform("User").GetComponent<InputField>();
		base.FindTransform("Sure").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOk);
	}

	private void Start()
	{
		this.SetAccount();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
	}

	private void OnClickOk(GameObject go)
	{
		if (string.IsNullOrEmpty(this.account.get_text().Trim()))
		{
			UIManagerControl.Instance.ShowToastText("输入为空,请输入账号");
			return;
		}
		LoginManager.Instance.SetAccount(this.account.get_text());
		UIManagerControl.Instance.UnLoadUIPrefab("AccountUI");
		if (LoginPanel.Instance != null)
		{
			LoginPanel.Instance.OpenFXMeshRender();
		}
	}

	public void SetAccount()
	{
		this.account.set_text(LoginManager.Instance.CurrentAccountName);
	}
}
