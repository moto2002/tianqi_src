using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BossBookPageBtn : BaseUIBehaviour
{
	private ButtonCustom m_BtnPage;

	private Image m_BtnPageImage;

	private Text m_BtnName;

	private Outline m_BtnNameOutline;

	public int pageId;

	public void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_BtnPage = base.FindTransform("BtnPage").GetComponent<ButtonCustom>();
		this.m_BtnPageImage = this.m_BtnPage.get_transform().GetComponent<Image>();
		this.m_BtnName = base.FindTransform("BtnName").GetComponent<Text>();
		this.m_BtnNameOutline = this.m_BtnName.GetComponent<Outline>();
	}

	public void InitBtn(int pageId, ButtonCustom.VoidDelegateObj clickCallback)
	{
		BMuLuFenYe bMuLuFenYe = DataReader<BMuLuFenYe>.Get(pageId);
		if (bMuLuFenYe != null)
		{
			this.m_BtnName.set_text(GameDataUtils.GetChineseContent(bMuLuFenYe.name, false));
		}
		this.m_BtnPage.onClickCustom = clickCallback;
		this.pageId = pageId;
	}

	public void SetSelect(bool isSelect)
	{
		if (isSelect)
		{
			this.m_BtnPage.set_enabled(false);
			ResourceManager.SetSprite(this.m_BtnPageImage, ResourceManager.GetIconSprite("bt_fenleianniu_1"));
			this.m_BtnName.set_color(new Color32(255, 250, 193, 255));
			this.m_BtnNameOutline.set_effectColor(new Color32(150, 87, 0, 255));
		}
		else
		{
			this.m_BtnPage.set_enabled(true);
			ResourceManager.SetSprite(this.m_BtnPageImage, ResourceManager.GetIconSprite("bt_fenleianniu_2"));
			this.m_BtnName.set_color(new Color32(107, 189, 233, 255));
			this.m_BtnNameOutline.set_effectColor(new Color32(20, 40, 56, 255));
		}
	}
}
