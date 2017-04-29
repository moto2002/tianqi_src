using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogEnchantmentUI : UIBase
{
	private Action actionL;

	private Action actionR;

	private Text oldAttrText;

	private Text newAttrText;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = false;
		this.alpha = 0.7f;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.oldAttrText = base.FindTransform("OldAttrContent").GetComponent<Text>();
		this.newAttrText = base.FindTransform("NewAttrContent").GetComponent<Text>();
		ButtonCustom expr_42 = base.FindTransform("BtnLeft").GetComponent<ButtonCustom>();
		expr_42.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_42.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickLeftBtn));
		ButtonCustom expr_73 = base.FindTransform("BtnRight").GetComponent<ButtonCustom>();
		expr_73.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_73.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickRightBtn));
	}

	private void OnClickLeftBtn(GameObject go)
	{
		if (this.actionL != null)
		{
			this.actionL.Invoke();
		}
		this.Show(false);
	}

	private void OnClickRightBtn(GameObject go)
	{
		if (this.actionR != null)
		{
			this.actionR.Invoke();
		}
		this.Show(false);
	}

	public void ShowLeftAndRight(string oldAttrContent, string newAttrContent, Action actionL, Action actionR, string btnLeftText = "取消", string btnRightText = "确定")
	{
		this.oldAttrText.set_text(oldAttrContent);
		this.newAttrText.set_text(newAttrContent);
		this.actionL = actionL;
		this.actionR = actionR;
		base.FindTransform("BtnLeft").FindChild("BtnLeftText").GetComponent<Text>().set_text(btnLeftText);
		base.FindTransform("BtnRight").FindChild("BtnRightText").GetComponent<Text>().set_text(btnRightText);
	}
}
