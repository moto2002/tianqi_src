using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPosItem : BaseUIBehaviour
{
	private Image m_spImageBinding;

	private Image m_spItemStepImg;

	private Image equipFrameImg;

	private Image equipIconImg;

	private Image equipShowTipImg;

	private Image equipPosSelectImg;

	private Image equipBindingImg;

	private Text equipPosTipText;

	private Transform equipIconRegionTrans;

	private ListPool equipGemListPool;

	private ListPool equipEnchantmentListPool;

	private ListPool equipStarUpListPool;

	private int equip_fxID;

	private EquipLibType.ELT equipPos;

	private bool selected;

	private bool showTip;

	public string EquipPosTip
	{
		get
		{
			if (this.equipPosTipText != null)
			{
				return this.equipPosTipText.get_text();
			}
			return string.Empty;
		}
		set
		{
			if (this.equipPosTipText != null)
			{
				this.equipPosTipText.set_text(value);
			}
		}
	}

	public EquipLibType.ELT EquipPos
	{
		get
		{
			return this.equipPos;
		}
		set
		{
			this.equipPos = value;
		}
	}

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.selected = value;
			if (this.equipPosSelectImg != null)
			{
				this.equipPosSelectImg.set_enabled(this.selected);
			}
		}
	}

	public bool ShowTip
	{
		get
		{
			return this.showTip;
		}
		set
		{
			this.showTip = value;
			if (this.equipShowTipImg != null)
			{
				this.equipShowTipImg.set_enabled(this.showTip);
			}
		}
	}

	private void Awake()
	{
		this.m_spImageBinding = base.FindTransform("ImageBinding").GetComponent<Image>();
		this.m_spItemStepImg = base.FindTransform("EquipIconRegion").FindChild("ItemStepImg").GetComponent<Image>();
		Text component = base.FindTransform("EquipIconRegion").FindChild("ItemStepText").GetComponent<Text>();
		ResourceManager.SetImageToStencil(ref this.m_spItemStepImg, 0);
		ResourceManager.SetTextToStencil(ref component);
		ResourceManager.SetImageToStencil(ref this.m_spImageBinding, 1);
		for (int i = 1; i <= 3; i++)
		{
			Image component2 = base.FindTransform("ExcellentAttrIconList").FindChild("Image" + i).GetComponent<Image>();
			if (component2 != null)
			{
				ResourceManager.SetImageToStencil(ref component2, 2);
			}
		}
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.equipBindingImg = base.FindTransform("ImageBinding").GetComponent<Image>();
		this.equipFrameImg = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.equipIconImg = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.equipPosTipText = base.FindTransform("EquipPosTipText").GetComponent<Text>();
		this.equipPosTipText.set_text(string.Empty);
		this.equipIconRegionTrans = base.FindTransform("EquipIconRegion");
		this.equipPosSelectImg = base.FindTransform("EquipPosSelectBg").GetComponent<Image>();
		this.equipShowTipImg = base.FindTransform("BadgeTipImg").GetComponent<Image>();
		this.equipGemListPool = base.FindTransform("EquipGemListPool").GetComponent<ListPool>();
		this.equipEnchantmentListPool = base.FindTransform("EquipEnchantmentListPool").GetComponent<ListPool>();
		this.equipStarUpListPool = base.FindTransform("EquipStarUpListPool").GetComponent<ListPool>();
		this.equipEnchantmentListPool.Clear();
		this.equipGemListPool.Clear();
		this.equipStarUpListPool.Clear();
	}

	protected override void OnDestroy()
	{
		FXSpineManager.Instance.DeleteSpine(this.equip_fxID, true);
		base.OnDestroy();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void ResetUI()
	{
		this.equipGemListPool.Clear();
		this.equipStarUpListPool.Clear();
		this.equipEnchantmentListPool.Clear();
		this.equipPosTipText.set_text(string.Empty);
	}

	private void UpdateEmbedGemList()
	{
		this.equipGemListPool.Clear();
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(this.EquipPos);
		if (wearingEquipSimpleInfoByPos != null)
		{
			List<int> gemTypeIdList = new List<int>();
			for (int i = 0; i < 4; i++)
			{
				GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[this.EquipPos - EquipLibType.ELT.Weapon, i];
				if (gemEmbedInfo != null && gemEmbedInfo.typeId > 0)
				{
					gemTypeIdList.Add(gemEmbedInfo.typeId);
				}
			}
			this.equipGemListPool.Create(gemTypeIdList.get_Count(), delegate(int index)
			{
				if (index < gemTypeIdList.get_Count() && index < this.equipGemListPool.Items.get_Count())
				{
					RewardItem component = this.equipGemListPool.Items.get_Item(index).GetComponent<RewardItem>();
					component.SetRewardItem(gemTypeIdList.get_Item(index), -1L, 0L);
				}
			});
		}
	}

	private void UpdateEmbedEnchantmentList()
	{
		this.equipEnchantmentListPool.Clear();
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(this.EquipPos);
		if (wearingEquipSimpleInfoByPos != null && wearingEquipSimpleInfoByPos.enchantAttrs != null && wearingEquipSimpleInfoByPos.enchantAttrs.get_Count() > 0)
		{
			List<int> typeIdList = new List<int>();
			for (int i = 0; i < wearingEquipSimpleInfoByPos.enchantAttrs.get_Count(); i++)
			{
				int attrId = wearingEquipSimpleInfoByPos.enchantAttrs.get_Item(i).attrId;
				if (attrId > 0)
				{
					typeIdList.Add(attrId);
				}
			}
			this.equipEnchantmentListPool.Create(typeIdList.get_Count(), delegate(int index)
			{
				if (index < typeIdList.get_Count() && index < this.equipEnchantmentListPool.Items.get_Count())
				{
					RewardItem component = this.equipEnchantmentListPool.Items.get_Item(index).GetComponent<RewardItem>();
					component.SetRewardItem(typeIdList.get_Item(index), -1L, 0L);
				}
			});
		}
	}

	private void UpdateEmbedStarList()
	{
		this.equipStarUpListPool.Clear();
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(this.EquipPos);
		if (wearingEquipSimpleInfoByPos != null && wearingEquipSimpleInfoByPos.starToMaterial != null && wearingEquipSimpleInfoByPos.starToMaterial.get_Count() > 0)
		{
			List<int> typeIdList = wearingEquipSimpleInfoByPos.starToMaterial;
			this.equipStarUpListPool.Create(typeIdList.get_Count(), delegate(int index)
			{
				if (index < typeIdList.get_Count() && index < this.equipStarUpListPool.Items.get_Count())
				{
					Image component = this.equipStarUpListPool.Items.get_Item(index).get_transform().FindChild("OpenStar").GetComponent<Image>();
					string starLevelSpriteName = this.GetStarLevelSpriteName(typeIdList.get_Item(index));
					ResourceManager.SetSprite(component, ResourceManager.GetIconSprite(starLevelSpriteName));
				}
			});
		}
	}

	private void UpdateSuitForgeContent()
	{
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(this.EquipPos);
		if (wearingEquipSimpleInfoByPos != null)
		{
			if (wearingEquipSimpleInfoByPos.suitId <= 0)
			{
				if (EquipGlobal.CheckForgeMaterialIsEnough(wearingEquipSimpleInfoByPos.equipId))
				{
					this.equipPosTipText.set_text(GameDataUtils.GetChineseContent(510208, false));
				}
				else
				{
					this.equipPosTipText.set_text(TextColorMgr.GetColor(GameDataUtils.GetChineseContent(510209, false), "ff0000", string.Empty));
				}
			}
			else
			{
				this.equipPosTipText.set_text(TextColorMgr.GetColor(GameDataUtils.GetChineseContent(510207, false), "33beff", string.Empty));
			}
		}
	}

	private string GetStarLevelSpriteName(int itemID)
	{
		switch (itemID)
		{
		case 8:
			return "pinji_tongxing1";
		case 9:
			return "pinji_yinxing1";
		case 10:
			return "pinji_jinxing1";
		default:
			return string.Empty;
		}
	}

	private void UpdateEquipItemData()
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.EquipPos);
		if (equipLib == null)
		{
			return;
		}
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		if (equipSimpleInfo == null)
		{
			return;
		}
		Dictionary<string, string> iconNamesByEquipPos = EquipGlobal.GetIconNamesByEquipPos(this.EquipPos, false);
		this.equipIconRegionTrans.FindChild("TextName").GetComponent<Text>().set_text(EquipGlobal.GetEquipSuitMarkName(equipSimpleInfo.suitId) + iconNamesByEquipPos.get_Item("ItemName"));
		ResourceManager.SetSprite(this.equipIconImg, ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconName")));
		int lv = equipLib.lv;
		this.equipIconRegionTrans.FindChild("TextLv").GetComponent<Text>().set_text(string.Empty);
		if (lv > 0)
		{
			this.equipIconRegionTrans.FindChild("TextLv").GetComponent<Text>().set_text("+" + lv);
		}
		this.equipIconRegionTrans.FindChild("ItemStepText").GetComponent<Text>().set_text(iconNamesByEquipPos.get_Item("EquipStep"));
		int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(equipSimpleInfo.equipId, 1f);
		this.equipIconRegionTrans.FindChild("ExcellentAttrIconList").FindChild("Image1").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 1);
		this.equipIconRegionTrans.FindChild("ExcellentAttrIconList").FindChild("Image2").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 2);
		this.equipIconRegionTrans.FindChild("ExcellentAttrIconList").FindChild("Image3").GetComponent<Image>().set_enabled(excellentAttrsCountByColor >= 3);
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipSimpleInfo.equipId);
		if (equipForgeCfgData != null && equipSimpleInfo.suitId > 0)
		{
			ResourceManager.SetSprite(this.equipFrameImg, ResourceManager.GetIconSprite(equipForgeCfgData.frame));
			FXSpineManager.Instance.DeleteSpine(this.equip_fxID, true);
			if (this.equipIconImg != null)
			{
				this.equip_fxID = FXSpineManager.Instance.PlaySpine(equipForgeCfgData.fxId, this.equipIconImg.get_transform(), "EquipPosItem", 2001, null, "UI", 0f, 0f, 1f, 1f, true, FXMaskLayer.MaskState.None);
			}
		}
		else
		{
			ResourceManager.SetSprite(this.equipFrameImg, ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconFrameName")));
			this.equip_fxID = EquipGlobal.GetEquipIconFX(equipSimpleInfo.cfgId, excellentAttrsCountByColor, this.equipIconImg.get_transform(), "EquipPosItem", 2001, true);
		}
		if (this.equipBindingImg != null)
		{
			this.equipBindingImg.get_gameObject().SetActive(equipSimpleInfo.binding);
		}
	}

	public void UpdateUIData(EquipLibType.ELT pos = EquipLibType.ELT.Weapon, EquipDetailedUIState state = EquipDetailedUIState.EquipStrengthen)
	{
		this.EquipPos = pos;
		this.UpdateEquipItemData();
		this.ResetUI();
		if (state == EquipDetailedUIState.EquipStarUp)
		{
			this.UpdateEmbedStarList();
		}
		else if (state == EquipDetailedUIState.EquipGem)
		{
			this.UpdateEmbedGemList();
		}
		else if (state == EquipDetailedUIState.EquipEnchantment)
		{
			this.UpdateEmbedEnchantmentList();
		}
		else if (state == EquipDetailedUIState.EquipSuitForge)
		{
			this.UpdateSuitForgeContent();
		}
	}

	public void UpdateEnchantmentData()
	{
		this.UpdateEmbedEnchantmentList();
	}

	public void UpdateWashData()
	{
		this.UpdateEquipItemData();
	}

	public void UpdateStarUpData()
	{
		this.UpdateEmbedStarList();
	}

	public void UpdateGemData()
	{
		this.UpdateEmbedGemList();
	}

	public void UpdateEquipStrengthData()
	{
		this.UpdateEquipItemData();
	}

	public void UpdateEquipSuitForgeData()
	{
		this.UpdateEquipItemData();
		this.UpdateSuitForgeContent();
	}
}
