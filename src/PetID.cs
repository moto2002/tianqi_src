using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetID : BaseUIBehaviour
{
	public int ItemID;

	public long EquipID;

	public PetInfo petInfo;

	private Image m_spImageFrame;

	private Image m_spImageFramePet;

	private Image m_spImageIcon;

	private GameObject m_goImageSelect;

	private GameObject m_goImageSelect2;

	private GameObject m_goImageLimit;

	private GameObject m_goFrameMask;

	private GameObject m_goImageRecommend;

	private GameObject m_goImageBinding;

	private Text m_numTxt;

	private Text m_itemStepTxt;

	private bool selected;

	private SelectImgType currentSelectType;

	private Image m_excellentImage1;

	private Image m_excellentImage2;

	private Image m_excellentImage3;

	private int m_equipFXID;

	private bool isInit;

	private int _ExcellentCount;

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.ShowSelect(value);
		}
	}

	public int ExcellentCount
	{
		get
		{
			return this._ExcellentCount;
		}
		set
		{
			this._ExcellentCount = value;
			this.m_excellentImage1.set_enabled(this._ExcellentCount >= 1);
			this.m_excellentImage2.set_enabled(this._ExcellentCount >= 2);
			this.m_excellentImage3.set_enabled(this._ExcellentCount >= 3);
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spImageFrame = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.m_spImageFramePet = base.FindTransform("ImageFramePet").GetComponent<Image>();
		this.m_spImageIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.m_goImageSelect = base.FindTransform("ImageSelect").get_gameObject();
		this.m_goImageSelect2 = base.FindTransform("ImageSelect2").get_gameObject();
		this.m_goImageLimit = base.FindTransform("ImageLimit").get_gameObject();
		this.m_numTxt = base.FindTransform("Num").GetComponent<Text>();
		this.m_numTxt.get_gameObject().SetActive(false);
		this.m_goFrameMask = base.FindTransform("FrameMask").get_gameObject();
		this.m_goImageRecommend = base.FindTransform("ImageRecommend").get_gameObject();
		this.m_goImageBinding = base.FindTransform("ImageBinding").get_gameObject();
		this.m_itemStepTxt = base.FindTransform("ItemStepText").GetComponent<Text>();
		base.FindTransform("ItemStep").get_gameObject().SetActive(false);
		this.m_excellentImage1 = base.FindTransform("ExcellentAttrIconList").FindChild("Image1").GetComponent<Image>();
		this.m_excellentImage2 = base.FindTransform("ExcellentAttrIconList").FindChild("Image2").GetComponent<Image>();
		this.m_excellentImage3 = base.FindTransform("ExcellentAttrIconList").FindChild("Image3").GetComponent<Image>();
		this.ExcellentCount = 0;
		this.isInit = true;
	}

	public void SetItem(Pet dataPet, PetInfo petinfo)
	{
		if (dataPet == null)
		{
			return;
		}
		if (this.m_spImageFrame == null)
		{
			return;
		}
		if (this.m_spImageFramePet == null)
		{
			return;
		}
		ResourceManager.SetSprite(this.m_spImageFrame, PetManager.GetPetFrame01(petinfo.star));
		ResourceManager.SetSprite(this.m_spImageFramePet, PetManager.GetPetFrame02(petinfo.star));
		ResourceManager.SetSprite(this.m_spImageIcon, PetManager.Instance.GetSelfPetIcon2(dataPet));
	}

	public void ShowSelect(bool isShow)
	{
		this.selected = isShow;
		if (this.currentSelectType == SelectImgType.Check)
		{
			if (this.m_goImageSelect != null && this.m_goImageSelect.get_activeSelf() != isShow)
			{
				this.m_goImageSelect.SetActive(isShow);
			}
		}
		else if (this.currentSelectType == SelectImgType.HighLight && this.m_goImageSelect2 != null && this.m_goImageSelect2.get_activeSelf() != isShow)
		{
			this.m_goImageSelect2.SetActive(isShow);
		}
	}

	public void ShowLimit(bool isShow)
	{
		if (this.m_goImageLimit != null && this.m_goImageLimit.get_activeSelf() != isShow)
		{
			this.m_goImageLimit.SetActive(isShow);
		}
	}

	public void SetGemIcon(int itemID, int count)
	{
		this.ItemID = itemID;
		Items items = DataReader<Items>.Get(itemID);
		if (items == null)
		{
			return;
		}
		if (!this.isInit)
		{
			this.InitUI();
		}
		int num = items.color;
		if (num == 0)
		{
			num = 1;
		}
		if (this.m_spImageFrame == null)
		{
			return;
		}
		if (this.m_spImageFramePet == null)
		{
			return;
		}
		ResourceManager.SetSprite(this.m_spImageFrame, GameDataUtils.GetItemFrameByColor(num));
		ResourceManager.SetSprite(this.m_spImageFramePet, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.m_spImageIcon, GameDataUtils.GetIcon(items.icon));
		this.m_numTxt.get_gameObject().SetActive(true);
		this.m_numTxt.set_text(count.ToString());
		this.currentSelectType = SelectImgType.HighLight;
		if (items != null && items.step > 0)
		{
			base.FindTransform("ItemStep").get_gameObject().SetActive(true);
			this.m_itemStepTxt.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
	}

	public void SetItemData(int itemID)
	{
		this.ItemID = itemID;
		Items items = DataReader<Items>.Get(itemID);
		if (items == null)
		{
			return;
		}
		int num = items.color;
		if (num == 0)
		{
			num = 1;
		}
		if (this.m_spImageFrame == null)
		{
			return;
		}
		if (this.m_spImageFramePet == null)
		{
			return;
		}
		ResourceManager.SetSprite(this.m_spImageFrame, GameDataUtils.GetItemFrameByColor(num));
		ResourceManager.SetSprite(this.m_spImageFramePet, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.m_spImageIcon, GameDataUtils.GetIcon(items.icon));
		long num2 = BackpackManager.Instance.OnGetGoodCount(itemID);
		if (num2 < 0L)
		{
			this.m_numTxt.get_gameObject().SetActive(false);
		}
		else
		{
			this.m_numTxt.get_gameObject().SetActive(true);
			this.m_numTxt.set_text(num2.ToString());
		}
		if (items != null && items.step > 0)
		{
			base.FindTransform("ItemStep").get_gameObject().SetActive(true);
			this.m_itemStepTxt.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
	}

	public void SetEquipItemData(int itemID, long equipID, SelectImgType selectType = SelectImgType.HighLight)
	{
		FXSpineManager.Instance.DeleteSpine(this.m_equipFXID, true);
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.EquipID = equipID;
		this.ItemID = itemID;
		Dictionary<string, string> equipIconNamesByEquipDataID = EquipGlobal.GetEquipIconNamesByEquipDataID(itemID, true);
		if (equipIconNamesByEquipDataID == null)
		{
			return;
		}
		if (this.m_spImageFrame == null)
		{
			return;
		}
		if (this.m_spImageFramePet == null)
		{
			return;
		}
		ResourceManager.SetSprite(this.m_spImageFrame, ResourceManager.GetIconSprite(equipIconNamesByEquipDataID.get_Item("IconFrameName")));
		ResourceManager.SetSprite(this.m_spImageFramePet, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.m_spImageIcon, ResourceManager.GetIconSprite(equipIconNamesByEquipDataID.get_Item("IconName")));
		this.m_numTxt.set_text(string.Empty);
		this.currentSelectType = selectType;
		base.FindTransform("ItemStep").get_gameObject().SetActive(true);
		this.m_itemStepTxt.set_text(equipIconNamesByEquipDataID.get_Item("EquipStep"));
		this.ExcellentCount = EquipGlobal.GetExcellentAttrsCountByColor(equipID, 1f);
		EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(equipID);
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipID);
		if (equipSimpleInfoByEquipID != null && equipForgeCfgData != null && equipSimpleInfoByEquipID.suitId > 0)
		{
			ResourceManager.SetSprite(this.m_spImageFrame, ResourceManager.GetIconSprite(equipForgeCfgData.frame));
		}
		this.SetImageBinding(equipSimpleInfoByEquipID != null && equipSimpleInfoByEquipID.binding);
	}

	public void SetItemFrameMask()
	{
		if (this.m_goFrameMask != null)
		{
			this.m_goFrameMask.SetActive(true);
		}
	}

	public void SetRecommend(bool isShow)
	{
		if (this.m_goImageRecommend != null && this.m_goImageRecommend.get_activeSelf() != isShow)
		{
			this.m_goImageRecommend.SetActive(isShow);
		}
	}

	public void SetImageBinding(bool isShow)
	{
		if (this.m_goImageBinding != null && this.m_goImageBinding.get_activeSelf() != isShow)
		{
			this.m_goImageBinding.SetActive(isShow);
		}
	}

	private void ResetSelectImg(SelectImgType type)
	{
		if (type == SelectImgType.Check)
		{
			this.m_goImageSelect2.get_gameObject().SetActive(false);
			this.currentSelectType = SelectImgType.Check;
		}
		else if (type == SelectImgType.HighLight)
		{
			this.m_goImageSelect.get_gameObject().SetActive(false);
			this.currentSelectType = SelectImgType.HighLight;
		}
	}
}
