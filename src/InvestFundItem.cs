using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InvestFundItem : BaseUIBehaviour
{
	private Text m_textDays;

	private Text m_textCount;

	private GameObject m_imageGot;

	public ButtonCustom m_BtnGet;

	private GameObject m_imageBtnDisable;

	public int itemId
	{
		get;
		private set;
	}

	public void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_textDays = base.FindTransform("Text_days").GetComponent<Text>();
		this.m_textCount = base.FindTransform("Text_count").GetComponent<Text>();
		this.m_imageGot = base.FindTransform("Image_got").get_gameObject();
		Transform transform = base.FindTransform("Btn_get");
		this.m_BtnGet = transform.GetComponent<ButtonCustom>();
		transform.FindChild("Text_btnGet").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(513152, false));
		this.m_imageBtnDisable = transform.FindChild("Image_btn_disable").get_gameObject();
	}

	public void UpdateItem(int index, FundListPush.Items info, ButtonCustom.VoidDelegateObj callback)
	{
		this.itemId = index;
		this.m_textDays.set_text(string.Format(GameDataUtils.GetChineseContent(513169, false), info.days));
		this.m_textCount.set_text("x" + info.itemNum);
		this.m_imageGot.SetActive(info.hasGetPrize);
		this.m_BtnGet.get_gameObject().SetActive(!info.hasGetPrize);
		this.m_BtnGet.set_enabled(info.canGetFlag);
		this.m_imageBtnDisable.SetActive(!info.canGetFlag);
		this.m_BtnGet.onClickCustom = callback;
	}
}
