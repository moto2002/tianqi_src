using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class FashionTipUI : UIBase
{
	public Text FashionTipUITitleName;

	public Text FashionTipUIText;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.FashionTipUITitleName = base.FindTransform("FashionTipUITitleName").GetComponent<Text>();
		this.FashionTipUIText = base.FindTransform("FashionTipUIText").GetComponent<Text>();
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isMask = true;
		this.isClick = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	public void SetData(string title, string text)
	{
		this.FashionTipUITitleName.set_text(title);
		this.FashionTipUIText.set_text(text);
	}
}
