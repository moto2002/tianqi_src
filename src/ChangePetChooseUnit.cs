using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangePetChooseUnit : BaseUIBehaviour
{
	private GameObject NoPet;

	private GameObject ImageTip;

	private GameObject HavePet;

	private Image m_spImageFrame;

	private Image m_spImageFramePet;

	private Image m_spImageIcon;

	private GameObject ImageLimit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.NoPet = base.FindTransform("NoPet").get_gameObject();
		this.ImageTip = base.FindTransform("ImageTip").get_gameObject();
		this.HavePet = base.FindTransform("HavePet").get_gameObject();
		this.m_spImageFrame = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.m_spImageFramePet = base.FindTransform("ImageFramePet").GetComponent<Image>();
		this.m_spImageIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.ImageLimit = base.FindTransform("ImageLimit").get_gameObject();
	}

	public void SetHavePet(bool isHave)
	{
		this.HavePet.SetActive(isHave);
		this.NoPet.SetActive(!isHave);
	}

	public void SetImageTip(bool isShow)
	{
		this.ImageTip.SetActive(isShow);
	}

	public void SetItem(Pet dataPet, PetInfo petinfo)
	{
		ResourceManager.SetSprite(this.m_spImageFrame, PetManager.GetPetFrame01(petinfo.star));
		ResourceManager.SetSprite(this.m_spImageFramePet, PetManager.GetPetFrame02(petinfo.star));
		ResourceManager.SetSprite(this.m_spImageIcon, PetManager.Instance.GetSelfPetIcon2(dataPet));
	}

	public void SetImageLimit(bool isShow)
	{
		this.ImageLimit.SetActive(isShow);
	}
}
