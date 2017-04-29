using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPartUI : UIBase
{
	public static EquipPartUI Instance;

	private ButtonCustom BtnWeapon;

	private ButtonCustom BtnWaistPendant;

	private ButtonCustom BtnNecklace;

	private ButtonCustom BtnCloth;

	private ButtonCustom BtnTrousers;

	private ButtonCustom BtnShoe;

	private ButtonCustom BtnEquip;

	private ButtonCustom BtnProperty;

	private FXID fxID1 = new FXID();

	private FXID fxID2 = new FXID();

	private FXID fxID3 = new FXID();

	private FXID fxID4 = new FXID();

	private FXID fxID5 = new FXID();

	private FXID fxID6 = new FXID();

	private void Awake()
	{
		EquipPartUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BtnWeapon = base.FindTransform("BtnWeapon").GetComponent<ButtonCustom>();
		this.BtnWaistPendant = base.FindTransform("BtnWaistPendant").GetComponent<ButtonCustom>();
		this.BtnNecklace = base.FindTransform("BtnNecklace").GetComponent<ButtonCustom>();
		this.BtnCloth = base.FindTransform("BtnCloth").GetComponent<ButtonCustom>();
		this.BtnTrousers = base.FindTransform("BtnTrousers").GetComponent<ButtonCustom>();
		this.BtnShoe = base.FindTransform("BtnShoe").GetComponent<ButtonCustom>();
		this.BtnWeapon.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWeapon);
		this.BtnWaistPendant.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWaistPendant);
		this.BtnNecklace.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnNecklace);
		this.BtnCloth.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnCloth);
		this.BtnTrousers.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnTrousers);
		this.BtnShoe.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnShoe);
		this.BtnEquip = base.FindTransform("ButtonEquip").GetComponent<ButtonCustom>();
		this.BtnProperty = base.FindTransform("ButtonProperty").GetComponent<ButtonCustom>();
		this.BtnEquip.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangeEquipOrPropertyUI);
		this.BtnProperty.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangeEquipOrPropertyUI);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.EquipEquipmentSucess, new Callback(this.EquipEquipmentSucess));
	}

	public void CheckBadge()
	{
		this.BtnWeapon.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(EquipmentManager.Instance.CheckCanChangeEquip(EquipLibType.ELT.Weapon));
		this.BtnNecklace.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(EquipmentManager.Instance.CheckCanChangeEquip(EquipLibType.ELT.Necklace));
		this.BtnCloth.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(EquipmentManager.Instance.CheckCanChangeEquip(EquipLibType.ELT.Shirt));
		this.BtnWaistPendant.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(EquipmentManager.Instance.CheckCanChangeEquip(EquipLibType.ELT.Waist));
		this.BtnTrousers.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(EquipmentManager.Instance.CheckCanChangeEquip(EquipLibType.ELT.Pant));
		this.BtnShoe.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(EquipmentManager.Instance.CheckCanChangeEquip(EquipLibType.ELT.Shoe));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
		this.ChangeEquipOrProperty(1);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			EquipPartUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void SetBtn(EquipLibType.ELT type, ButtonCustom btn, FXID fxID)
	{
		Dictionary<string, string> iconNamesByEquipPos = EquipGlobal.GetIconNamesByEquipPos(type, true);
		if (iconNamesByEquipPos == null)
		{
			return;
		}
		EquipLib equipLibInfo = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == type);
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipLibInfo.equips.Find((EquipSimpleInfo a) => a.equipId == equipLibInfo.wearingId).cfgId);
		ResourceManager.SetSprite(btn.get_transform().FindChild("Icon").FindChild("ImageFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconFrameName")));
		ResourceManager.SetSprite(btn.get_transform().FindChild("Icon").FindChild("ImageIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconName")));
		Text component = btn.get_transform().FindChild("Name").FindChild("Text").GetComponent<Text>();
		component.set_text(iconNamesByEquipPos.get_Item("ItemName"));
		if (equipLibInfo.lv > 0)
		{
			btn.get_transform().FindChild("Icon").FindChild("EquipLV").GetComponent<Text>().set_text("+" + equipLibInfo.lv);
		}
		else
		{
			btn.get_transform().FindChild("Icon").FindChild("EquipLV").GetComponent<Text>().set_text(string.Empty);
		}
		btn.get_transform().FindChild("TextLV").GetComponent<Text>().set_text("Lv." + zZhuangBeiPeiZhiBiao.level);
		int quality = int.Parse(iconNamesByEquipPos.get_Item("QualityLv"));
		Dictionary<string, Color> textColorByQuality = GameDataUtils.GetTextColorByQuality(quality);
		component.set_color(textColorByQuality.get_Item("TextColor"));
		component.GetComponent<Outline>().set_effectColor(textColorByQuality.get_Item("TextOutlineColor"));
		if (zZhuangBeiPeiZhiBiao.firstGroupId != 0)
		{
			if (fxID.fxid == 0)
			{
				fxID.fxid = FXSpineManager.Instance.PlaySpine(104, btn.get_transform().FindChild("Icon").FindChild("FX"), "EquipPartUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		}
		else if (fxID.fxid != 0)
		{
			FXSpineManager.Instance.DeleteSpine(fxID.fxid, true);
			fxID.fxid = 0;
		}
	}

	public void RefreshUI()
	{
		this.SetBtn(EquipLibType.ELT.Weapon, this.BtnWeapon, this.fxID1);
		this.SetBtn(EquipLibType.ELT.Necklace, this.BtnNecklace, this.fxID2);
		this.SetBtn(EquipLibType.ELT.Pant, this.BtnTrousers, this.fxID3);
		this.SetBtn(EquipLibType.ELT.Shirt, this.BtnCloth, this.fxID4);
		this.SetBtn(EquipLibType.ELT.Shoe, this.BtnShoe, this.fxID5);
		this.SetBtn(EquipLibType.ELT.Waist, this.BtnWaistPendant, this.fxID6);
		this.CheckBadge();
	}

	private void OpenChangeEquipUI(EquipLibType.ELT selectPos)
	{
		UIManagerControl.Instance.OpenUI("EquipDetailedPopUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		EquipDetailedPopUI.Instance.SetSelectEquipTip(selectPos, false);
	}

	private void ChangeEquipOrProperty(int currentBtn = 1)
	{
		if (currentBtn == 1)
		{
			base.FindTransform("EquipPanel").get_gameObject().SetActive(true);
			base.FindTransform("ActorPropertyPanel").get_gameObject().SetActive(false);
			this.BtnEquip.get_transform().FindChild("Unselect").get_gameObject().SetActive(false);
			this.BtnProperty.get_transform().FindChild("Unselect").get_gameObject().SetActive(true);
		}
		else if (currentBtn == 2)
		{
			base.FindTransform("EquipPanel").get_gameObject().SetActive(false);
			base.FindTransform("ActorPropertyPanel").get_gameObject().SetActive(true);
			this.BtnEquip.get_transform().FindChild("Unselect").get_gameObject().SetActive(true);
			this.BtnProperty.get_transform().FindChild("Unselect").get_gameObject().SetActive(false);
			ActorPropertyUI component = base.FindTransform("ActorPropertyPanel").GetComponent<ActorPropertyUI>();
			component.RefreshUI(EntityWorld.Instance.EntSelf);
		}
	}

	private void EquipEquipmentSucess()
	{
		this.RefreshUI();
	}

	private void EquipViewShouldClose()
	{
	}

	private void ComposeEquipmentSucess(bool sucess)
	{
	}

	private void OnClickBtnWeapon(GameObject data)
	{
		this.OpenChangeEquipUI(EquipLibType.ELT.Weapon);
	}

	private void OnClickBtnWaistPendant(GameObject data)
	{
		this.OpenChangeEquipUI(EquipLibType.ELT.Waist);
	}

	private void OnClickBtnNecklace(GameObject data)
	{
		this.OpenChangeEquipUI(EquipLibType.ELT.Necklace);
	}

	private void OnClickBtnCloth(GameObject data)
	{
		this.OpenChangeEquipUI(EquipLibType.ELT.Shirt);
	}

	private void OnClickBtnTrousers(GameObject data)
	{
		this.OpenChangeEquipUI(EquipLibType.ELT.Pant);
	}

	private void OnClickBtnShoe(GameObject data)
	{
		this.OpenChangeEquipUI(EquipLibType.ELT.Shoe);
	}

	private void OnClickChangeEquipOrPropertyUI(GameObject sender)
	{
		if (sender == this.BtnEquip.get_gameObject())
		{
			this.ChangeEquipOrProperty(1);
		}
		else if (sender == this.BtnProperty.get_gameObject())
		{
			this.ChangeEquipOrProperty(2);
		}
	}
}
