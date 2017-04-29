using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PTF_InfoItem : BaseUIBehaviour
{
	private Image m_spImageFrame;

	private Image m_spImageFramePet;

	private Image m_spImageIcon;

	private GameObject m_goImageChoose;

	public int m_petId;

	private int m_spineId;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_spImageFrame = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.m_spImageFramePet = base.FindTransform("ImageFramePet").GetComponent<Image>();
		this.m_spImageIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.m_goImageChoose = base.FindTransform("ImageChoose").get_gameObject();
	}

	private void OnDisable()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_spineId, true);
	}

	public void SetItemToPet(int petId, int star = 1)
	{
		this.m_petId = petId;
		Pet dataPet = DataReader<Pet>.Get(petId);
		this.m_spImageFramePet.set_enabled(true);
		ResourceManager.SetSprite(this.m_spImageFrame, GameDataUtils.GetItemFrameByColor(1));
		ResourceManager.SetSprite(this.m_spImageFramePet, PetManager.GetPetFrame02(star));
		ResourceManager.SetSprite(this.m_spImageIcon, PetManager.Instance.GetSelfPetIcon2(dataPet));
	}

	public void SetItemToMonster(int iconId)
	{
		this.m_spImageFramePet.set_enabled(false);
		ResourceManager.SetSprite(this.m_spImageFrame, GameDataUtils.GetItemFrameByColor(1));
		ResourceManager.SetSprite(this.m_spImageIcon, GameDataUtils.GetIcon(iconId));
	}

	public void SetMatch(bool isMatch)
	{
		this.m_goImageChoose.SetActive(isMatch);
		if (isMatch)
		{
			this.PlaySpine();
		}
	}

	private void PlaySpine()
	{
		this.m_spineId = FXSpineManager.Instance.ReplaySpine(this.m_spineId, 117, base.get_transform(), "PetTaskFormationUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
