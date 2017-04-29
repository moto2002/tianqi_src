using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemTipUI : BaseUIBehaviour
{
	private Transform BaseAttr;

	private Transform ExcellentAttr;

	private Transform StarUpAttr;

	private Transform EnchantmentAttr;

	private int ItemID;

	private long EquipID;

	private bool IsWearing;

	private List<Transform> starTransformList;

	private int DepthValue;

	private bool _BtnReplaceVisibility;

	private bool _BtnStrengthVisibility;

	private bool _CurrentEquipIconVisibility;

	private bool _HaveEquipObjVisibility;

	private bool _NoEquipObjVisibility;

	private int equipFxID;

	public bool BtnReplaceVisibility
	{
		get
		{
			return this._BtnReplaceVisibility;
		}
		set
		{
			this._BtnReplaceVisibility = value;
			GameObject gameObject = base.FindTransform("BtnReplace").get_gameObject();
			if (gameObject != null)
			{
				gameObject.SetActive(this._BtnReplaceVisibility);
			}
		}
	}

	public bool BtnStrengthVisibility
	{
		get
		{
			return this._BtnStrengthVisibility;
		}
		set
		{
			this._BtnStrengthVisibility = value;
			GameObject gameObject = base.FindTransform("BtnStrength").get_gameObject();
			if (gameObject != null)
			{
				gameObject.SetActive(this._BtnStrengthVisibility);
			}
		}
	}

	public bool CurrentEquipIconVisibility
	{
		get
		{
			return this._CurrentEquipIconVisibility;
		}
		set
		{
			this._CurrentEquipIconVisibility = value;
			GameObject gameObject = base.FindTransform("CurrentEquipTip").get_gameObject();
			if (gameObject != null)
			{
				gameObject.SetActive(this._CurrentEquipIconVisibility);
			}
		}
	}

	public bool HaveEquipObjVisibility
	{
		get
		{
			return this._HaveEquipObjVisibility;
		}
		set
		{
			this._HaveEquipObjVisibility = value;
			GameObject gameObject = base.FindTransform("HaveEquip").get_gameObject();
			if (gameObject != null)
			{
				gameObject.SetActive(this._HaveEquipObjVisibility);
			}
		}
	}

	public bool NoEquipObjVisibility
	{
		get
		{
			return this._NoEquipObjVisibility;
		}
		set
		{
			this._NoEquipObjVisibility = value;
			GameObject gameObject = base.FindTransform("NoEquipTip").get_gameObject();
			if (gameObject != null)
			{
				gameObject.SetActive(this._NoEquipObjVisibility);
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
		this.starTransformList = new List<Transform>();
		for (int i = 1; i < 16; i++)
		{
			Transform transform = base.FindTransform("star" + i);
			if (transform != null)
			{
				this.starTransformList.Add(transform);
			}
		}
		base.FindTransform("BtnStrength").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLinkToStrength);
		base.FindTransform("BtnReplace").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickReplace);
		base.FindTransform("BtnBuy").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBuy);
		this.BaseAttr = base.FindTransform("BaseAttr");
		this.ExcellentAttr = base.FindTransform("ExcellentAttr");
		this.StarUpAttr = base.FindTransform("StarUpAttr");
		this.EnchantmentAttr = base.FindTransform("EnchantmentAttr");
	}

	private void OnClickReplace(GameObject go)
	{
		EquipSimpleInfo equipSimpleInfo = null;
		List<EquipLib> equipLibs = EquipmentManager.Instance.equipmentData.equipLibs;
		for (int i = 0; i < equipLibs.get_Count(); i++)
		{
			int num = equipLibs.get_Item(i).equips.FindIndex((EquipSimpleInfo a) => a.equipId == this.EquipID);
			if (num >= 0)
			{
				equipSimpleInfo = equipLibs.get_Item(i).equips.get_Item(num);
				break;
			}
		}
		if (equipSimpleInfo != null)
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
			if (zZhuangBeiPeiZhiBiao.level > EntityWorld.Instance.EntSelf.Lv)
			{
				string text = GameDataUtils.GetChineseContent(510113, false);
				text = text.Replace("{s1}", zZhuangBeiPeiZhiBiao.level.ToString());
				UIManagerControl.Instance.ShowToastText(text);
				return;
			}
			EquipmentManager.Instance.SendPutOnEquipmentReq(zZhuangBeiPeiZhiBiao.position, equipSimpleInfo.equipId, equipSimpleInfo.cfgId);
		}
	}

	private void OnClickBuy(GameObject go)
	{
	}

	private void OnClickLinkToStrength(GameObject go)
	{
		if (EquipmentManager.Instance.dicEquips.ContainsKey(this.EquipID))
		{
			int cfgId = EquipmentManager.Instance.dicEquips.get_Item(this.EquipID).cfgId;
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
			if (zZhuangBeiPeiZhiBiao == null)
			{
				return;
			}
			UIManagerControl.Instance.HideUI("EquipDetailedPopUI");
			LinkNavigationManager.OpenEquipStrengthenUI((EquipLibType.ELT)zZhuangBeiPeiZhiBiao.position, null);
		}
	}

	private void SetEquipItemData(int equipItemID, int equipLV = 0)
	{
		FXSpineManager.Instance.DeleteSpine(this.equipFxID, true);
		this.equipFxID = 0;
		if (this.DepthValue < 3000)
		{
			this.DepthValue = 3000;
		}
		zZhuangBeiPeiZhiBiao data = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipItemID);
		if (data == null)
		{
			return;
		}
		Dictionary<string, string> equipIconNamesByEquipDataID = EquipGlobal.GetEquipIconNamesByEquipDataID(equipItemID, true);
		ResourceManager.SetSprite(base.FindTransform("ItemFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(equipIconNamesByEquipDataID.get_Item("IconFrameName")));
		ResourceManager.SetSprite(base.FindTransform("ItemIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(equipIconNamesByEquipDataID.get_Item("IconName")));
		DepthOfUI depthOfUI = base.FindTransform("EquipStepImg").GetComponent<DepthOfUI>();
		if (depthOfUI == null)
		{
			depthOfUI = base.FindTransform("EquipStepImg").get_gameObject().AddComponent<DepthOfUI>();
		}
		depthOfUI.SortingOrder = this.DepthValue + 2;
		DepthOfUI depthOfUI2 = base.FindTransform("EquipStepText").GetComponent<DepthOfUI>();
		if (depthOfUI2 == null)
		{
			depthOfUI2 = base.FindTransform("EquipStepText").get_gameObject().AddComponent<DepthOfUI>();
		}
		depthOfUI2.SortingOrder = this.DepthValue + 2;
		DepthOfUI depthOfUI3 = base.FindTransform("ExcellentAttrIconList").GetComponent<DepthOfUI>();
		if (depthOfUI3 == null)
		{
			depthOfUI3 = base.FindTransform("ExcellentAttrIconList").get_gameObject().AddComponent<DepthOfUI>();
		}
		depthOfUI3.SortingOrder = this.DepthValue + 2;
		DepthOfUI depthOfUI4 = base.FindTransform("ImageBinding").GetComponent<DepthOfUI>();
		if (depthOfUI4 == null)
		{
			depthOfUI4 = base.FindTransform("ImageBinding").get_gameObject().AddComponent<DepthOfUI>();
		}
		depthOfUI4.SortingOrder = this.DepthValue + 2;
		base.FindTransform("ImageBinding").get_gameObject().SetActive(false);
		base.FindTransform("ExcellentAttrIconList").get_gameObject().SetActive(false);
		base.FindTransform("EquipStepText").GetComponent<Text>().set_text(equipIconNamesByEquipDataID.get_Item("EquipStep"));
		base.FindTransform("ItemName").GetComponent<Text>().set_text(equipIconNamesByEquipDataID.get_Item("ItemName"));
		int color = DataReader<Items>.Get(equipItemID).color;
		Dictionary<string, Color> textColorByQuality = GameDataUtils.GetTextColorByQuality(color);
		base.FindTransform("ItemNum").GetComponent<Text>().set_text(string.Empty);
		if (this.IsWearing)
		{
			if (equipLV > 0)
			{
				base.FindTransform("ItemNum").GetComponent<Text>().set_text("+" + equipLV.ToString());
				base.FindTransform("ItemNum").GetComponent<Text>().set_color(textColorByQuality.get_Item("TextColor"));
				base.FindTransform("ItemNum").GetComponent<Outline>().set_effectColor(textColorByQuality.get_Item("TextOutlineColor"));
			}
			else
			{
				base.FindTransform("ItemNum").GetComponent<Text>().set_text(string.Empty);
			}
		}
		base.FindTransform("ItemLv").GetComponent<Text>().set_text(data.level.ToString());
		string equipOccupationName = EquipGlobal.GetEquipOccupationName(data.id);
		base.FindTransform("ItemCareerLimit").GetComponent<Text>().set_text(equipOccupationName);
		base.FindTransform("AdvancedTipText").GetComponent<Text>().set_text(string.Empty);
		int i;
		for (i = 0; i < data.starNum; i++)
		{
			this.starTransformList.get_Item(i).get_gameObject().SetActive(true);
			this.starTransformList.get_Item(i).FindChild("OpenStar").get_gameObject().SetActive(false);
		}
		for (int j = i; j < this.starTransformList.get_Count(); j++)
		{
			this.starTransformList.get_Item(j).get_gameObject().SetActive(false);
		}
		Attrs attrs = DataReader<Attrs>.Get(data.attrBaseValue);
		Attrs attrs2 = DataReader<Attrs>.Get(data.attrGrowValue);
		int key = 0;
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)data.position);
		equipLV = equipLib.lv;
		if (EquipmentManager.Instance.dicEquips.ContainsKey(equipLib.wearingId))
		{
			key = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).cfgId;
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(key);
		if (attrs != null)
		{
			for (int k = 0; k < attrs.attrs.get_Count(); k++)
			{
				if (k > 2)
				{
					break;
				}
				long value = (long)attrs.values.get_Item(k);
				this.BaseAttr.FindChild("EquipItem2Text" + k).get_gameObject().SetActive(true);
				this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Item2Text").GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc((GameData.AttrType)attrs.attrs.get_Item(k), value, "ff7d4b"));
				if (attrs2 != null)
				{
					int num = attrs2.values.get_Item(k) * equipLV;
					long value2 = (long)num;
					this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Item2TextRange").GetComponent<Text>().set_text("（强化 +" + AttrUtility.GetAttrValueDisplay((GameData.AttrType)attrs2.attrs.get_Item(k), value2) + "）");
					if (!this.IsWearing)
					{
						if (zZhuangBeiPeiZhiBiao != null)
						{
							Attrs attrs3 = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrGrowValue);
							float num2 = (float)(attrs3.values.get_Item(k) * equipLV);
							if (num2 > (float)num)
							{
								this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrup").get_gameObject().SetActive(false);
								this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrdown").get_gameObject().SetActive(true);
							}
							else if (num2 == (float)num)
							{
								this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrup").get_gameObject().SetActive(false);
								this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrdown").get_gameObject().SetActive(false);
							}
							else
							{
								this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrup").get_gameObject().SetActive(true);
								this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrdown").get_gameObject().SetActive(false);
							}
						}
					}
					else
					{
						this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrup").get_gameObject().SetActive(false);
						this.BaseAttr.FindChild("EquipItem2Text" + k).FindChild("Attrdown").get_gameObject().SetActive(false);
					}
				}
			}
			for (int l = attrs.attrs.get_Count(); l < 3; l++)
			{
				this.BaseAttr.FindChild("EquipItem2Text" + l).get_gameObject().SetActive(false);
			}
		}
	}

	private void SetEquipSimpleInfoData(long equipId)
	{
		EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(equipId);
		if (equipSimpleInfoByEquipID == null)
		{
			return;
		}
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipId);
		if (equipForgeCfgData != null && equipSimpleInfoByEquipID.suitId > 0)
		{
			ResourceManager.SetSprite(base.FindTransform("ItemFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(equipForgeCfgData.frame));
			base.FindTransform("ItemName").GetComponent<Text>().set_text(TextColorMgr.GetColor(EquipGlobal.GetEquipSuitMarkName(equipSimpleInfoByEquipID.suitId) + GameDataUtils.GetItemName(equipSimpleInfoByEquipID.cfgId, false, 0L), "FF1919", string.Empty));
		}
		Transform transform = base.FindTransform("ImageBinding");
		if (transform != null)
		{
			transform.get_gameObject().SetActive(equipSimpleInfoByEquipID.binding);
		}
	}

	public void SetEquipIconFX(long equipID)
	{
		EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(equipID);
		if (equipSimpleInfoByEquipID == null)
		{
			return;
		}
		Transform transform = base.FindTransform("ItemIcon");
		if (transform == null)
		{
			return;
		}
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipID);
		if (equipForgeCfgData != null && equipSimpleInfoByEquipID.suitId > 0)
		{
			this.equipFxID = FXSpineManager.Instance.PlaySpine(equipForgeCfgData.fxId, transform, "EquipPosItem", this.DepthValue, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(equipID, 1f);
			this.equipFxID = EquipGlobal.GetEquipIconFX(this.ItemID, excellentAttrsCountByColor, transform, "EquipItemTipUI", this.DepthValue, false);
		}
	}

	public void SetEquipIconFXByGogokNum(int gogokNum, int itemID)
	{
		if (gogokNum <= 0)
		{
			return;
		}
		Transform fxParentTrans = base.FindTransform("ItemIcon");
		this.equipFxID = EquipGlobal.GetEquipIconFX(itemID, gogokNum, fxParentTrans, "EquipItemTipUI", this.DepthValue, false);
	}

	private void SetFightingData(long fighting)
	{
		base.FindTransform("ItemFightingScore").GetComponent<Text>().set_text(fighting.ToString());
	}

	private void SetEnchantmentData(List<ExcellentAttr> enchantAttrs)
	{
		if (enchantAttrs == null || enchantAttrs.get_Count() <= 0)
		{
			this.EnchantmentAttr.get_gameObject().SetActive(false);
			return;
		}
		this.EnchantmentAttr.get_gameObject().SetActive(true);
		int i = 0;
		bool flag = false;
		int num = 0;
		while (i < enchantAttrs.get_Count())
		{
			if (i >= 3)
			{
				break;
			}
			if (enchantAttrs.get_Item(i).attrId > 0)
			{
				flag = true;
				int attrId = enchantAttrs.get_Item(i).attrId;
				Items items = DataReader<Items>.Get(attrId);
				string text = string.Empty;
				if (items != null)
				{
					text = GameDataUtils.GetChineseContent(items.name, false);
				}
				FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(attrId);
				this.EnchantmentAttr.FindChild("EquipItem2Text" + i).get_gameObject().SetActive(true);
				string text2 = string.Empty;
				if (fuMoDaoJuShuXing != null)
				{
					if (fuMoDaoJuShuXing.valueType == 0)
					{
						text2 = string.Concat(new object[]
						{
							AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr),
							" +",
							(float)(enchantAttrs.get_Item(i).value * 100L) / 1000f,
							"%"
						});
					}
					else
					{
						text2 = AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr) + " +" + enchantAttrs.get_Item(i).value;
					}
					this.EnchantmentAttr.FindChild("EquipItem2Text" + i).FindChild("Item2Text").GetComponent<Text>().set_text(string.Format(text + "： <color=#ff7d4b>{0}</color>", text2));
					num++;
				}
			}
			i++;
		}
		if (!flag)
		{
			this.EnchantmentAttr.get_gameObject().SetActive(false);
		}
		else
		{
			for (int j = num; j < 3; j++)
			{
				this.EnchantmentAttr.FindChild("EquipItem2Text" + j).get_gameObject().SetActive(false);
			}
		}
	}

	private void SetStarMaterialData(List<int> starToMaterial)
	{
		for (int i = 0; i < starToMaterial.get_Count(); i++)
		{
			this.starTransformList.get_Item(i).FindChild("OpenStar").get_gameObject().SetActive(true);
			string starLevelSpriteName = this.GetStarLevelSpriteName(starToMaterial.get_Item(i));
			ResourceManager.SetSprite(this.starTransformList.get_Item(i).FindChild("OpenStar").GetComponent<Image>(), ResourceManager.GetIconSprite(starLevelSpriteName));
		}
	}

	private void SetStarAttrData(List<ExcellentAttr> starAttrs)
	{
		if (starAttrs == null || starAttrs.get_Count() <= 0)
		{
			this.StarUpAttr.get_gameObject().SetActive(false);
			return;
		}
		this.StarUpAttr.get_gameObject().SetActive(true);
		this.StarUpAttr.FindChild("EquipItem2Text0").FindChild("Item2Text").GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc((GameData.AttrType)starAttrs.get_Item(0).attrId, starAttrs.get_Item(0).value, "ff7d4b"));
	}

	private void SetExcellentAttrsUnKnow()
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(this.ItemID);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			this.ExcellentAttr.get_gameObject().SetActive(false);
		}
		XPinZhiXiShu xPinZhiXiShu = DataReader<XPinZhiXiShu>.Get(zZhuangBeiPeiZhiBiao.quality);
		if (xPinZhiXiShu != null && xPinZhiXiShu.attrNum > 0)
		{
			this.ExcellentAttr.get_gameObject().SetActive(true);
			int i;
			for (i = 0; i < xPinZhiXiShu.attrNum; i++)
			{
				string text = "？？？？？？";
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).get_gameObject().SetActive(true);
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).FindChild("Item2Text").GetComponent<Text>().set_text(text);
				string text2 = "（获得属性后鉴定）";
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).FindChild("Item2TextRange").GetComponent<Text>().set_text(text2);
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).FindChild("ItemImg").GetComponent<Image>().set_enabled(false);
			}
			for (int j = i; j < 5; j++)
			{
				this.ExcellentAttr.FindChild("EquipItem2Text" + j).get_gameObject().SetActive(false);
			}
		}
		else
		{
			this.ExcellentAttr.get_gameObject().SetActive(false);
		}
	}

	private void SetExcellentAttrsData(List<ExcellentAttr> excellentAttrs)
	{
		if (excellentAttrs == null || excellentAttrs.get_Count() <= 0)
		{
			this.ExcellentAttr.get_gameObject().SetActive(false);
			return;
		}
		this.ExcellentAttr.get_gameObject().SetActive(true);
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(this.ItemID);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return;
		}
		int i = 0;
		int num = 0;
		while (i < excellentAttrs.get_Count())
		{
			if (i >= 5)
			{
				break;
			}
			if (excellentAttrs.get_Item(i).attrId < 0)
			{
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).get_gameObject().SetActive(false);
			}
			else if (excellentAttrs.get_Item(i).attrId > 0)
			{
				string excellentTypeColor = EquipGlobal.GetExcellentTypeColor(excellentAttrs.get_Item(i).color);
				string text = string.Empty;
				text = AttrUtility.GetStandardAddDesc(excellentAttrs.get_Item(i).attrId, excellentAttrs.get_Item(i).value, excellentTypeColor, excellentTypeColor);
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).get_gameObject().SetActive(true);
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).FindChild("Item2Text").GetComponent<Text>().set_text(text);
				string excellentRangeText = EquipGlobal.GetExcellentRangeText(zZhuangBeiPeiZhiBiao.id, excellentAttrs.get_Item(i).attrId);
				this.ExcellentAttr.FindChild("EquipItem2Text" + i).FindChild("Item2TextRange").GetComponent<Text>().set_text(excellentRangeText);
				if (excellentAttrs.get_Item(i).color >= 1f)
				{
					this.ExcellentAttr.FindChild("EquipItem2Text" + i).FindChild("ItemImg").GetComponent<Image>().set_enabled(true);
					num++;
				}
				else
				{
					this.ExcellentAttr.FindChild("EquipItem2Text" + i).FindChild("ItemImg").GetComponent<Image>().set_enabled(false);
				}
			}
			i++;
		}
		for (int j = i; j < 5; j++)
		{
			this.ExcellentAttr.FindChild("EquipItem2Text" + j).get_gameObject().SetActive(false);
		}
		if (num > 0)
		{
			Transform transform = base.FindTransform("ExcellentAttrIconList");
			if (!transform.get_gameObject().get_activeSelf())
			{
				transform.get_gameObject().SetActive(true);
			}
			for (int k = 0; k < num; k++)
			{
				if (num >= 3)
				{
					break;
				}
				transform.FindChild("Image" + (k + 1)).get_gameObject().SetActive(true);
			}
			for (int l = num; l < 3; l++)
			{
				transform.FindChild("Image" + (l + 1)).get_gameObject().SetActive(false);
			}
		}
	}

	public void RefreshUI(long equipID, bool isWearing = false, bool noEquip = false, bool isShowStrengthen = false, int depthValue = 3000)
	{
		this.DepthValue = depthValue;
		this.EquipID = equipID;
		this.IsWearing = isWearing;
		this.NoEquipObjVisibility = noEquip;
		this.HaveEquipObjVisibility = !noEquip;
		if (noEquip)
		{
			return;
		}
		this.BtnStrengthVisibility = false;
		if (isShowStrengthen && SystemOpenManager.IsSystemOn(40))
		{
			this.BtnStrengthVisibility = true;
		}
		this.CurrentEquipIconVisibility = isWearing;
		this.BtnReplaceVisibility = !isWearing;
		if (EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			EquipSimpleInfo equipSimpleInfo = EquipmentManager.Instance.dicEquips.get_Item(equipID);
			this.ItemID = equipSimpleInfo.cfgId;
			int equipLV = 0;
			zZhuangBeiPeiZhiBiao equipCfgData = DataReader<zZhuangBeiPeiZhiBiao>.Get(this.ItemID);
			if (equipCfgData == null)
			{
				return;
			}
			EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)equipCfgData.position);
			if (equipLib != null)
			{
				equipLV = equipLib.lv;
			}
			this.SetEquipItemData(this.ItemID, equipLV);
			this.SetFightingData((long)EquipmentManager.Instance.GetEquipFightingByEquipID(equipID));
			this.SetEquipSimpleInfoData(equipID);
			this.SetEquipIconFX(equipID);
			this.SetExcellentAttrsData(equipSimpleInfo.excellentAttrs);
			this.SetStarAttrData(equipSimpleInfo.starAttrs);
			this.SetStarMaterialData(equipSimpleInfo.starToMaterial);
			this.SetEnchantmentData(equipSimpleInfo.enchantAttrs);
		}
	}

	public void RefreshUIByEquipID(long equipID, int depthValue = 3000)
	{
		this.DepthValue = depthValue;
		this.EquipID = equipID;
		this.IsWearing = false;
		int equipLV = 0;
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.wearingId == equipID);
		if (equipLib != null)
		{
			this.IsWearing = true;
			equipLV = equipLib.lv;
		}
		this.HaveEquipObjVisibility = true;
		this.NoEquipObjVisibility = false;
		this.BtnReplaceVisibility = false;
		this.CurrentEquipIconVisibility = this.IsWearing;
		if (EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			EquipSimpleInfo equipSimpleInfo = EquipmentManager.Instance.dicEquips.get_Item(equipID);
			this.ItemID = equipSimpleInfo.cfgId;
			this.SetEquipItemData(this.ItemID, equipLV);
			this.SetFightingData((long)EquipmentManager.Instance.GetEquipFightingByEquipID(equipID));
			this.SetEquipSimpleInfoData(equipID);
			this.SetEquipIconFX(equipID);
			this.SetStarMaterialData(equipSimpleInfo.starToMaterial);
			this.SetExcellentAttrsData(equipSimpleInfo.excellentAttrs);
		}
	}

	public void RefreshUIByItemID(int itemID, int depthValue = 3000)
	{
		this.DepthValue = depthValue;
		this.ItemID = itemID;
		this.IsWearing = false;
		this.HaveEquipObjVisibility = true;
		this.NoEquipObjVisibility = false;
		this.BtnReplaceVisibility = false;
		this.CurrentEquipIconVisibility = this.IsWearing;
		this.SetEquipItemData(this.ItemID, 0);
		this.SetFightingData((long)EquipmentManager.Instance.GetEquipFightingByItemID(this.ItemID));
		this.SetExcellentAttrsUnKnow();
	}

	public void RefreshUIByWearingInfo(WearEquipInfo wearingInfo, int depthValue = 3000)
	{
		this.DepthValue = depthValue;
		this.ItemID = wearingInfo.id;
		this.IsWearing = false;
		this.HaveEquipObjVisibility = true;
		this.NoEquipObjVisibility = false;
		this.BtnReplaceVisibility = false;
		this.BtnStrengthVisibility = false;
		this.CurrentEquipIconVisibility = false;
		this.SetEquipItemData(this.ItemID, wearingInfo.lv);
		this.SetFightingData((long)EquipmentManager.Instance.GetEquipFightingByExcellentAttrs(this.ItemID, wearingInfo.excellentAttrs));
		this.SetExcellentAttrsData(wearingInfo.excellentAttrs);
		if (wearingInfo != null && wearingInfo.excellentAttrs != null)
		{
			int num = 0;
			for (int i = 0; i < wearingInfo.excellentAttrs.get_Count(); i++)
			{
				if (wearingInfo.excellentAttrs.get_Item(i).color >= 1f)
				{
					num++;
				}
			}
			this.SetEquipIconFXByGogokNum(num, this.ItemID);
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
}
