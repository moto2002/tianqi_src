using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipEnchantmentUI : UIBase
{
	private const int attrMaxCount = 4;

	public static EquipEnchantmentUI Instance;

	private Transform NoEnchantMentTrans;

	private Transform HaveEnchantMentTrans;

	private GridLayoutGroup scrollLayout;

	private List<Transform> cellTransList;

	public int CurrentPos;

	private int CurrentSlot;

	private int CurrentSelectID;

	private void Awake()
	{
		EquipEnchantmentUI.Instance = this;
		this.cellTransList = new List<Transform>();
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.NoEnchantMentTrans = base.FindTransform("NoEnchantmentTip");
		this.HaveEnchantMentTrans = base.FindTransform("HaveChantment");
		ButtonCustom expr_38 = base.FindTransform("btnReplace").GetComponent<ButtonCustom>();
		expr_38.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_38.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickEnchantmentBtn));
		ButtonCustom expr_69 = base.FindTransform("BtnToShop").GetComponent<ButtonCustom>();
		expr_69.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_69.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickBtnToShop));
		Transform transform = this.HaveEnchantMentTrans.FindChild("scroll");
		this.scrollLayout = transform.FindChild("scrollLayout").GetComponent<GridLayoutGroup>();
		transform.GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
		this.NoEnchantMentTrans.FindChild("bg").FindChild("EnChantmentTitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(20002, false));
		this.NoEnchantMentTrans.FindChild("attention").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(20003, false));
		this.HaveEnchantMentTrans.FindChild("bg").FindChild("EnChantmentTitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(20002, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.CurrentSelectID = -1;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			EquipEnchantmentUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.OnEnchantEquipResultAckRes, new Callback<int>(this.OnEnchantEquipResultAckRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.OnEnchantEquipResultAckRes, new Callback<int>(this.OnEnchantEquipResultAckRes));
	}

	private void SetEnchantmentIcon(int typeId)
	{
		Items items = DataReader<Items>.Get(typeId);
		if (items == null)
		{
			return;
		}
		Transform transform = base.FindTransform("imgGrid0");
		Image component = transform.GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetItemFrameByColor(items.color));
		Image component2 = transform.FindChild("imgItem").GetComponent<Image>();
		ResourceManager.SetSprite(component2, GameDataUtils.GetIcon(items.icon));
		Text component3 = transform.FindChild("texName").GetComponent<Text>();
		component3.set_text(GameDataUtils.GetItemName(typeId, true, 0L));
		Dictionary<string, Color> textColorByQuality = GameDataUtils.GetTextColorByQuality(items.color);
		component3.set_color(textColorByQuality.get_Item("TextColor"));
		transform.get_transform().FindChild("texName").GetComponent<Outline>().set_effectColor(textColorByQuality.get_Item("TextOutlineColor"));
		Text component4 = transform.FindChild("texLv").GetComponent<Text>();
		component4.set_text(string.Empty);
		Text component5 = base.FindTransform("texDesc").GetComponent<Text>();
		int describeId = items.describeId1;
		component5.set_text(GameDataUtils.GetChineseContent(describeId, false));
		string canEnchantmentPosDesc = EquipGlobal.GetCanEnchantmentPosDesc(items.id);
		Text expr_112 = component5;
		expr_112.set_text(expr_112.get_text() + "\n");
		Text expr_129 = component5;
		expr_129.set_text(expr_129.get_text() + "\n");
		Text expr_140 = component5;
		expr_140.set_text(expr_140.get_text() + "可用部位：" + canEnchantmentPosDesc);
	}

	private void SetEnchantmentAttrs(int typeId)
	{
		List<string> list = new List<string>();
		FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(typeId);
		string[] array = fuMoDaoJuShuXing.describe.Split(new char[]
		{
			';'
		});
		string text = AttrUtility.GetAttrName(fuMoDaoJuShuXing.runeAttr) + "   " + string.Format(string.Concat(new string[]
		{
			"<color=#ff7d4b>",
			array[0],
			" - ",
			array[1],
			"</color>"
		}), new object[0]) + string.Empty;
		list.Add(text);
		for (int i = 0; i < 4; i++)
		{
			Text component = base.FindTransform("texAttr" + i).GetComponent<Text>();
			component.get_gameObject().SetActive(false);
		}
		int num = 0;
		while (num < list.get_Count() && num < 4)
		{
			Text component2 = base.FindTransform("texAttr" + num).GetComponent<Text>();
			component2.get_gameObject().SetActive(true);
			component2.set_text(list.get_Item(num));
			num++;
		}
	}

	private void SetEnchantmentGoodInfo()
	{
		this.SetEnchantmentIcon(this.CurrentSelectID);
		this.SetEnchantmentAttrs(this.CurrentSelectID);
	}

	private void OnClickEnchantmentBtn(GameObject go)
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)this.CurrentPos);
		if (equipLib != null)
		{
			long equipId = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).equipId;
			EquipmentManager.Instance.SendEnchantEquipReq(this.CurrentPos, this.CurrentSlot, this.CurrentSelectID, equipId);
		}
		else
		{
			Debug.Log("未找到该装备==========");
		}
	}

	private void OnClickBtnToShop(GameObject go)
	{
		LinkNavigationManager.OpenXMarketUI2();
		this.OnClickCloseBtn(null);
	}

	private void OnClickSelectEnchantmentItem(GameObject go)
	{
		int num = this.cellTransList.FindIndex((Transform a) => a.GetComponent<PetID>().ItemID == this.CurrentSelectID);
		if (num >= 0)
		{
			this.cellTransList.get_Item(num).GetComponent<PetID>().Selected = false;
		}
		PetID component = go.GetComponent<PetID>();
		component.Selected = true;
		this.CurrentSelectID = component.ItemID;
		this.SetEnchantmentGoodInfo();
	}

	private void OnEnchantEquipResultAckRes(int position)
	{
		this.Show(false);
	}

	public void OnEnchantmentEquipRes(ExcellentAttr oldExcellentAttr, ExcellentAttr newExcellentAttr)
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)this.CurrentPos);
		string text = "原属性：";
		if (oldExcellentAttr == null)
		{
			text = "未附魔";
		}
		else if (oldExcellentAttr.attrId > 0)
		{
			FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(oldExcellentAttr.attrId);
			if (fuMoDaoJuShuXing.valueType == 0)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr),
					" +",
					(float)(oldExcellentAttr.value * 100L) / 1000f,
					"%"
				});
			}
			else
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr),
					" +",
					oldExcellentAttr.value
				});
			}
		}
		else
		{
			text = "未附魔";
		}
		string text3 = string.Empty;
		if (newExcellentAttr != null)
		{
			FuMoDaoJuShuXing fuMoDaoJuShuXing2 = DataReader<FuMoDaoJuShuXing>.Get(newExcellentAttr.attrId);
			if (fuMoDaoJuShuXing2 != null)
			{
				text3 += "新属性：";
				if (fuMoDaoJuShuXing2.valueType == 0)
				{
					string text2 = text3;
					text3 = string.Concat(new object[]
					{
						text2,
						AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing2.runeAttr),
						" +",
						(float)(newExcellentAttr.value * 100L) / 1000f,
						"%"
					});
				}
				else
				{
					string text2 = text3;
					text3 = string.Concat(new object[]
					{
						text2,
						AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing2.runeAttr),
						" +",
						newExcellentAttr.value
					});
				}
			}
		}
		DialogEnchantmentUI dialogEnchantmentUI = UIManagerControl.Instance.OpenUI("DialogoEnchantmentUI", UINodesManager.TopUIRoot, false, UIType.NonPush) as DialogEnchantmentUI;
		dialogEnchantmentUI.ShowLeftAndRight(text, text3, null, delegate
		{
			if (equipLib != null)
			{
				EquipmentManager.Instance.SendEnchantEquipResultAckReq(this.CurrentPos, equipLib.wearingId, newExcellentAttr);
			}
		}, "放 弃", "更 换");
		this.RefreshUI(this.CurrentPos, this.CurrentSlot);
	}

	public void RefreshUI(int pos, int currentSlot)
	{
		this.CurrentPos = pos;
		this.CurrentSlot = currentSlot;
		this.cellTransList.Clear();
		List<Goods> canEnchantmentGoods = EquipGlobal.GetCanEnchantmentGoods(this.CurrentPos, this.CurrentSlot);
		if (canEnchantmentGoods.get_Count() <= 0)
		{
			this.NoEnchantMentTrans.get_gameObject().SetActive(true);
			this.HaveEnchantMentTrans.get_gameObject().SetActive(false);
		}
		else
		{
			this.NoEnchantMentTrans.get_gameObject().SetActive(false);
			this.HaveEnchantMentTrans.get_gameObject().SetActive(true);
			for (int i = 0; i < this.scrollLayout.get_transform().get_childCount(); i++)
			{
				Transform child = this.scrollLayout.get_transform().GetChild(i);
				Object.Destroy(child.get_gameObject());
			}
			for (int j = 0; j < canEnchantmentGoods.get_Count(); j++)
			{
				Goods goods = canEnchantmentGoods.get_Item(j);
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(WidgetName.PetChooseItem);
				instantiate2Prefab.set_name("Enchantment_" + goods.GetItemId());
				PetID component = instantiate2Prefab.GetComponent<PetID>();
				instantiate2Prefab.SetActive(true);
				instantiate2Prefab.get_transform().SetParent(this.scrollLayout.get_transform());
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectEnchantmentItem);
				component.SetItemData(goods.GetItemId());
				this.cellTransList.Add(instantiate2Prefab.get_transform());
			}
			if (this.CurrentSelectID < 0 && this.cellTransList.get_Count() > 0)
			{
				this.CurrentSelectID = this.cellTransList.get_Item(0).GetComponent<PetID>().ItemID;
				this.cellTransList.get_Item(0).GetComponent<PetID>().Selected = true;
				this.SetEnchantmentGoodInfo();
			}
			else if (this.CurrentSelectID > 0)
			{
				int num = this.cellTransList.FindIndex((Transform a) => a.GetComponent<PetID>().ItemID == this.CurrentSelectID);
				if (num >= 0)
				{
					this.cellTransList.get_Item(num).GetComponent<PetID>().Selected = true;
				}
			}
		}
	}
}
