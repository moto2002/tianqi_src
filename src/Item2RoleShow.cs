using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Item2RoleShow : BaseUIBehaviour
{
	private Image m_iconImg;

	private Image m_frameImg;

	private Text m_nameText;

	private Text m_levelText;

	private Text m_itemStepText;

	private bool isInit;

	private int itemID;

	private int equipType;

	private WearEquipInfo _EquipData;

	public int ItemID
	{
		get
		{
			return this.itemID;
		}
		set
		{
			this.itemID = value;
		}
	}

	public int EquipType
	{
		get
		{
			return this.equipType;
		}
		set
		{
			this.equipType = value;
		}
	}

	public WearEquipInfo EquipData
	{
		get
		{
			return this._EquipData;
		}
		set
		{
			this._EquipData = value;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_iconImg = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.m_frameImg = base.FindTransform("ImageFrame").GetComponent<Image>();
		this.m_levelText = base.FindTransform("Level").GetComponent<Text>();
		this.m_itemStepText = base.FindTransform("ItemStepText").GetComponent<Text>();
		this.m_nameText = base.FindTransform("NameText").GetComponent<Text>();
		base.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickItem);
		this.ResetEquipPartText();
		this.isInit = true;
	}

	private void OnClickItem(GameObject go)
	{
		Items item = BackpackManager.Instance.GetItem(this.ItemID);
		if (item == null)
		{
			return;
		}
		if (item.tab == 2 && this.EquipData != null)
		{
			ItemTipUIViewModel.ShowEquipItem(this.ItemID, this.EquipData, null);
			return;
		}
		ItemTipUIViewModel.ShowItem(this.ItemID, null);
	}

	private void ResetEquipPartText()
	{
		this.m_levelText.set_text(string.Empty);
		this.m_itemStepText.set_text(string.Empty);
		this.m_nameText.set_text(string.Empty);
	}

	public void ShowEquipItem(WearEquipInfo equipData)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (equipData == null)
		{
			return;
		}
		this.ItemID = equipData.id;
		this.EquipData = equipData;
		this.EquipType = equipData.type;
		Items items = DataReader<Items>.Get(this.ItemID);
		if (items == null)
		{
			return;
		}
		this.ResetEquipPartText();
		ResourceManager.SetSprite(this.m_frameImg, GameDataUtils.GetItemFrameByColor(items.color));
		ResourceManager.SetSprite(this.m_iconImg, GameDataUtils.GetIcon(items.icon));
		if (equipData.lv > 0)
		{
			this.m_levelText.set_text("Lv." + equipData.lv);
		}
		this.m_itemStepText.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		int num = 0;
		for (int i = 0; i < equipData.excellentAttrs.get_Count(); i++)
		{
			if (equipData.excellentAttrs.get_Item(i).color >= 1f)
			{
				num++;
			}
		}
		base.FindTransform("ExcellentAttrIconList").FindChild("Image1").GetComponent<Image>().set_enabled(num >= 1);
		base.FindTransform("ExcellentAttrIconList").FindChild("Image2").GetComponent<Image>().set_enabled(num >= 2);
		base.FindTransform("ExcellentAttrIconList").FindChild("Image3").GetComponent<Image>().set_enabled(num >= 3);
		Transform transform = this.m_iconImg.get_transform();
		EquipGlobal.GetEquipIconFX(equipData.id, num, transform, "RoleShowItem", 2000, false);
	}
}
