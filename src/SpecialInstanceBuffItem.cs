using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceBuffItem : BaseUIBehaviour
{
	private Image m_imageBuffIcon;

	private Text m_textBuffUsing;

	private Text m_textCost;

	private Image m_imageIconCost;

	private ButtonCustom m_ClickBg;

	private GameObject m_imageSelect;

	public int buffId;

	public int tipsId = 502328;

	public void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_imageBuffIcon = base.FindTransform("ImageBuffIcon").GetComponent<Image>();
		this.m_textBuffUsing = base.FindTransform("TextBuffUsing").GetComponent<Text>();
		this.m_textCost = base.FindTransform("TextCost").GetComponent<Text>();
		this.m_imageIconCost = base.FindTransform("ImageIconCost").GetComponent<Image>();
		this.m_ClickBg = base.FindTransform("ImageBg1").GetComponent<ButtonCustom>();
		this.m_imageSelect = base.FindTransform("ImageSelect").get_gameObject();
		this.m_textBuffUsing.set_text(GameDataUtils.GetChineseContent(502326, false));
		this.m_ClickBg.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnCellBtnClick);
	}

	public void UpdateItem(FZengYibuffPeiZhi config)
	{
		this.buffId = config.id;
		this.m_textCost.set_text("x" + config.price);
		ResourceManager.SetSprite(this.m_imageBuffIcon, GameDataUtils.GetIcon(config.icon));
		ResourceManager.SetSprite(this.m_imageIconCost, GameDataUtils.GetIcon(DataReader<Items>.Get(config.coinType).littleIcon));
	}

	public void setSelect(bool isSelect)
	{
		this.m_imageSelect.SetActive(isSelect);
		this.m_textBuffUsing.get_gameObject().SetActive(isSelect);
		this.m_ClickBg.set_enabled(!isSelect);
		this.m_textCost.get_gameObject().SetActive(!isSelect);
		this.m_imageIconCost.get_gameObject().SetActive(!isSelect);
	}

	public void OnCellBtnClick(GameObject go)
	{
		FZengYibuffPeiZhi fZengYibuffPeiZhi = DataReader<FZengYibuffPeiZhi>.Get(this.buffId);
		if (fZengYibuffPeiZhi == null)
		{
			return;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(502327, false), string.Format(GameDataUtils.GetChineseContent(this.tipsId, false), fZengYibuffPeiZhi.price + GameDataUtils.GetChineseContent(DataReader<Items>.Get(fZengYibuffPeiZhi.coinType).name, false), string.Empty), null, null, delegate
		{
			SpecialFightManager.Instance.BuyExperienceCopyBuffReq(this.buffId);
		}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", null);
	}
}
