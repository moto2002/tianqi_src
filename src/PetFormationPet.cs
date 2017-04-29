using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PetFormationPet : BaseUIBehaviour
{
	private GameObject m_goHavePet;

	private GameObject m_goNoPet;

	private GameObject m_goImageLimit;

	private Image m_spImageFrame;

	private Image m_spImageFramePet;

	private Image m_spImageIcon;

	private Image m_spImageQuality;

	private Text m_lblTextPower;

	private Text m_lblTextName;

	private Text m_lblTextLV;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_goHavePet = base.FindTransform("HavePet").get_gameObject();
		this.m_goNoPet = base.FindTransform("NoPet").get_gameObject();
		if (base.FindTransform("ImageLimit") != null)
		{
			this.m_goImageLimit = base.FindTransform("ImageLimit").get_gameObject();
		}
		this.m_spImageFrame = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.m_spImageFramePet = base.FindTransform("ImageFramePet").GetComponent<Image>();
		this.m_spImageIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		if (base.FindTransform("ImageQuality") != null)
		{
			this.m_spImageQuality = base.FindTransform("ImageQuality").GetComponent<Image>();
		}
		if (base.FindTransform("TextPower") != null)
		{
			this.m_lblTextPower = base.FindTransform("TextPower").GetComponent<Text>();
		}
		this.m_lblTextName = base.FindTransform("TextName").GetComponent<Text>();
		this.m_lblTextLV = base.FindTransform("TextLV").GetComponent<Text>();
	}

	public void SetItem(PetInfo petInfo = null, Pet dataPet = null, bool islimit = false)
	{
		if (petInfo != null)
		{
			ResourceManager.SetSprite(this.m_spImageFrame, PetManager.GetPetFrame01(petInfo.star));
			ResourceManager.SetSprite(this.m_spImageFramePet, PetManager.GetPetFrame02(petInfo.star));
			ResourceManager.SetSprite(this.m_spImageIcon, PetManager.Instance.GetSelfPetIcon2(dataPet));
			if (this.m_spImageQuality != null)
			{
				ResourceManager.SetSprite(this.m_spImageQuality, PetManager.GetPetQualityIcon(petInfo.star));
			}
			if (this.m_lblTextPower != null)
			{
				this.m_lblTextPower.set_text(petInfo.publicBaseInfo.simpleInfo.Fighting.ToString());
			}
			this.m_lblTextName.set_text(GameDataUtils.GetChineseContent(dataPet.name, false));
			this.m_lblTextLV.GetComponent<Text>().set_text("Lv." + petInfo.lv);
			this.ShowHave(true);
			this.ShowLimit(islimit);
		}
		else
		{
			this.ShowHave(false);
		}
	}

	private void ShowHave(bool isHave)
	{
		this.m_goHavePet.SetActive(isHave);
		this.m_goNoPet.SetActive(!isHave);
	}

	private void ShowLimit(bool isShow)
	{
		if (this.m_goImageLimit != null)
		{
			this.m_goImageLimit.SetActive(isShow);
		}
	}
}
