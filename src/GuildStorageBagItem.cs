using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GuildStorageBagItem : BaseUIBehaviour
{
	public bool isSpecialItem;

	public ItemBriefInfo m_SpecialItemInfo;

	public EquipSimpleInfo m_EquipSimpleInfo;

	private GameObject itemRootNullObj;

	private GameObject itemRootObj;

	private GameObject itemStepObj;

	private GameObject selectedImgObj;

	private Image itemIconImg;

	private Image itemFrameImg;

	private Text itemNumText;

	private Text itemStepNumText;

	private bool isInit;

	private bool selected;

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.selected = value;
			if (this.selectedImgObj != null && this.selectedImgObj.get_activeSelf() != this.selected)
			{
				this.selectedImgObj.SetActive(this.selected);
			}
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.itemRootNullObj = base.FindTransform("ItemRootNull").get_gameObject();
		this.itemRootObj = base.FindTransform("ItemRoot").get_gameObject();
		this.itemStepObj = base.FindTransform("ItemStep").get_gameObject();
		this.selectedImgObj = base.FindTransform("Selected01").get_gameObject();
		this.itemIconImg = base.FindTransform("ItemIcon").GetComponent<Image>();
		this.itemFrameImg = base.FindTransform("ItemFrame").GetComponent<Image>();
		this.itemNumText = base.FindTransform("ItemNum").GetComponent<Text>();
		this.itemStepNumText = base.FindTransform("ItemStepNum").GetComponent<Text>();
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdateItemNull()
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.m_EquipSimpleInfo = null;
		this.m_SpecialItemInfo = null;
		if (!this.itemRootNullObj.get_activeSelf())
		{
			this.itemRootNullObj.SetActive(true);
		}
		if (this.itemRootObj.get_activeSelf())
		{
			this.itemRootObj.SetActive(false);
		}
		this.Selected = false;
	}

	public void UpdateItemData(ItemBriefInfo itemInfo)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (itemInfo != null && DataReader<Items>.Contains(itemInfo.cfgId))
		{
			this.m_SpecialItemInfo = itemInfo;
			this.isSpecialItem = true;
			if (this.itemRootNullObj.get_activeSelf())
			{
				this.itemRootNullObj.SetActive(false);
			}
			if (!this.itemRootObj.get_activeSelf())
			{
				this.itemRootObj.SetActive(true);
			}
			if (this.itemStepObj.get_activeSelf())
			{
				this.itemRootObj.SetActive(false);
			}
			ResourceManager.SetSprite(this.itemIconImg, GameDataUtils.GetItemIcon(itemInfo.cfgId));
			ResourceManager.SetSprite(this.itemFrameImg, GameDataUtils.GetItemFrame(itemInfo.cfgId));
			this.itemNumText.set_text(itemInfo.count + string.Empty);
		}
		this.Selected = false;
	}

	public void UpdateEquipItemData(EquipSimpleInfo equipSimpleInfo)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (equipSimpleInfo != null && DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipSimpleInfo.cfgId))
		{
			this.m_EquipSimpleInfo = equipSimpleInfo;
			this.isSpecialItem = false;
			if (this.itemRootNullObj.get_activeSelf())
			{
				this.itemRootNullObj.SetActive(false);
			}
			if (!this.itemRootObj.get_activeSelf())
			{
				this.itemRootObj.SetActive(true);
			}
			if (!this.itemStepObj.get_activeSelf())
			{
				this.itemStepObj.SetActive(true);
			}
			ResourceManager.SetSprite(this.itemIconImg, GameDataUtils.GetItemIcon(equipSimpleInfo.cfgId));
			this.itemNumText.set_text(string.Empty);
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
			this.itemStepNumText.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), zZhuangBeiPeiZhiBiao.step));
			TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipSimpleInfo.equipId);
			if (equipForgeCfgData != null && equipSimpleInfo.suitId > 0)
			{
				ResourceManager.SetSprite(base.FindTransform("ItemFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(equipForgeCfgData.frame));
			}
			else
			{
				ResourceManager.SetSprite(this.itemFrameImg, GameDataUtils.GetItemFrame(equipSimpleInfo.cfgId));
			}
			int num = 0;
			if (equipSimpleInfo.excellentAttrs != null)
			{
				for (int i = 0; i < equipSimpleInfo.excellentAttrs.get_Count(); i++)
				{
					if (equipSimpleInfo.excellentAttrs.get_Item(i).attrId > 0 && equipSimpleInfo.excellentAttrs.get_Item(i).color >= 1f)
					{
						num++;
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (j >= 3)
				{
					break;
				}
				base.FindTransform("ExcellentAttrIconList").FindChild("Image" + (j + 1)).get_gameObject().SetActive(true);
			}
			for (int k = num; k < 3; k++)
			{
				base.FindTransform("ExcellentAttrIconList").FindChild("Image" + (k + 1)).get_gameObject().SetActive(false);
			}
		}
		this.Selected = false;
	}
}
