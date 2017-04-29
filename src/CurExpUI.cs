using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class CurExpUI : UIBase
{
	protected Text CurExpUIText;

	protected Text CurExpUINum;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.CurExpUIText = base.FindTransform("CurExpUIText").GetComponent<Text>();
		this.CurExpUINum = base.FindTransform("CurExpUINum").GetComponent<Text>();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		base.ReleaseSelf(destroy);
	}

	public void SetText(string text)
	{
		this.CurExpUIText.set_text(text);
	}

	public void SetNum(string num)
	{
		this.CurExpUINum.set_text(num);
	}
}
