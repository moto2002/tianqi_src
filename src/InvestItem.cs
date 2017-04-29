using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InvestItem : BaseUIBehaviour
{
	private Text m_TextDay;

	private Text m_TextCount;

	private GameObject m_ImageGot;

	private GameObject m_ImageOut;

	private GameObject m_BtnAward;

	private GameObject m_ImageBtnDisable;

	public int itemId;

	public void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_TextDay = base.FindTransform("TextDay").GetComponent<Text>();
		this.m_TextCount = base.FindTransform("TextCount").GetComponent<Text>();
		this.m_ImageGot = base.FindTransform("ImageGot").get_gameObject();
		this.m_ImageOut = base.FindTransform("ImageOut").get_gameObject();
		this.m_BtnAward = base.FindTransform("BtnAward").get_gameObject();
		this.m_ImageBtnDisable = base.FindTransform("ImageBtnDisable").get_gameObject();
	}

	public void UpdateItem(int id, string title, string count, bool canGet, bool hasGot, bool isOver, ButtonCustom.VoidDelegateObj callback)
	{
		this.itemId = id;
		this.m_TextDay.set_text(title);
		this.m_TextCount.set_text(count);
		this.m_BtnAward.GetComponent<ButtonCustom>().onClickCustom = callback;
		this.m_ImageGot.SetActive(hasGot);
		this.m_ImageOut.SetActive(!hasGot && isOver);
		this.m_BtnAward.SetActive(!hasGot && !isOver);
		this.m_BtnAward.GetComponent<ButtonCustom>().set_enabled(canGet);
		this.m_ImageBtnDisable.SetActive(!canGet);
	}
}
