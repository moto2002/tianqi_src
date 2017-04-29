using Foundation.Core.Databinding;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonInfo : BaseUIBehaviour
{
	private Text m_lblText;

	private Image m_spBg;

	private ButtonCustom m_ButtonCustom;

	public Action _CallBack;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_lblText = base.FindTransform("Text").GetComponent<Text>();
		this.m_spBg = base.FindTransform("Bg").GetComponent<Image>();
		this.m_ButtonCustom = base.GetComponent<ButtonCustom>();
		this.m_ButtonCustom.get_onClick().AddListener(new UnityAction(this.OnButtonClick));
	}

	private void OnButtonClick()
	{
		if (this._CallBack != null)
		{
			this._CallBack.Invoke();
		}
	}

	public void SetButtonText(string text)
	{
		this.m_lblText.set_text(text);
	}

	public void SetButtonBg(int color = 1)
	{
		ResourceManager.SetSprite(this.m_spBg, ButtonColorMgr.GetButton(color));
	}

	public void SetButtonCallback(Action callback)
	{
		this._CallBack = callback;
	}

	public void SetButtonEnable(bool bEnable)
	{
		ButtonColorMgr.SetButtonEnable(bEnable, this.m_ButtonCustom, this.m_spBg, null);
	}
}
