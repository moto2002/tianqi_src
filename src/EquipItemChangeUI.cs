using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemChangeUI : UIBase
{
	private List<Transform> starTransformList;

	private Text titleNameText;

	private Text btnNameText;

	private Text costNumText;

	private Image costIconImg;

	private Text fightingText;

	private ButtonCustom btnChange;

	private Action actionBtnCB;

	private int iconFxID;

	public Action ActionClickBtnCB
	{
		set
		{
			this.actionBtnCB = value;
		}
	}

	public string TitleName
	{
		get
		{
			string result = string.Empty;
			if (this.titleNameText != null)
			{
				result = this.titleNameText.get_text();
			}
			return result;
		}
		set
		{
			if (this.titleNameText != null)
			{
				this.titleNameText.set_text(value);
			}
		}
	}

	public string BtnName
	{
		get
		{
			string result = string.Empty;
			if (this.btnNameText != null)
			{
				result = this.btnNameText.get_text();
			}
			return result;
		}
		set
		{
			if (this.btnNameText != null)
			{
				this.btnNameText.set_text(value);
			}
		}
	}

	public int CostNum
	{
		set
		{
			if (this.costNumText != null)
			{
				this.costNumText.set_text(value + string.Empty);
			}
		}
	}

	public string CostNumText
	{
		get
		{
			if (this.costNumText != null)
			{
				return this.costNumText.get_text();
			}
			return string.Empty;
		}
		set
		{
			if (this.costNumText != null)
			{
				this.costNumText.set_text(value + string.Empty);
			}
		}
	}

	public SpriteRenderer CostIconSR
	{
		set
		{
			if (this.costIconImg != null)
			{
				ResourceManager.SetSprite(this.costIconImg, value);
			}
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.titleNameText = base.FindTransform("Name").GetComponent<Text>();
		this.btnNameText = base.FindTransform("BtnChange").FindChild("Text").GetComponent<Text>();
		this.costNumText = base.FindTransform("CostNum").GetComponent<Text>();
		this.fightingText = base.FindTransform("ItemFightingScore").GetComponent<Text>();
		this.costIconImg = base.FindTransform("CostIcon").GetComponent<Image>();
		this.btnChange = base.FindTransform("BtnChange").GetComponent<ButtonCustom>();
		this.btnChange.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnChange);
		this.starTransformList = new List<Transform>();
		for (int i = 1; i < 16; i++)
		{
			Transform transform = base.FindTransform("starList").FindChild("star" + i);
			if (transform != null)
			{
				this.starTransformList.Add(transform);
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnChange(GameObject go)
	{
		if (this.actionBtnCB != null)
		{
			this.actionBtnCB.Invoke();
		}
		this.Show(false);
	}

	public void SetEquipItemData(EquipSimpleInfo equipSimpleInfo, int hasMaxMoney = 0)
	{
		if (equipSimpleInfo == null)
		{
			return;
		}
		if (!DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipSimpleInfo.cfgId))
		{
			return;
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
		Image component = base.FindTransform("ItemFrame").GetComponent<Image>();
		if (component != null)
		{
			ResourceManager.SetSprite(component, GameDataUtils.GetItemFrame(equipSimpleInfo.cfgId));
		}
		Image component2 = base.FindTransform("ItemIcon").GetComponent<Image>();
		if (component2 != null)
		{
			ResourceManager.SetSprite(component2, GameDataUtils.GetItemIcon(equipSimpleInfo.cfgId));
		}
		Text component3 = base.FindTransform("ItemName").GetComponent<Text>();
		if (component3 != null)
		{
			component3.set_text(GameDataUtils.GetItemName(equipSimpleInfo.cfgId, true, 0L));
		}
		Text component4 = base.FindTransform("ItemLv").GetComponent<Text>();
		component4.set_text(zZhuangBeiPeiZhiBiao.level.ToString());
		Text component5 = base.FindTransform("ItemCareerLimit").GetComponent<Text>();
		string equipOccupationName = EquipGlobal.GetEquipOccupationName(zZhuangBeiPeiZhiBiao.id);
		component5.set_text(equipOccupationName);
		Text component6 = base.FindTransform("ItemFightingScore").GetComponent<Text>();
		Text component7 = base.FindTransform("EquipStepText").GetComponent<Text>();
		component7.set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), zZhuangBeiPeiZhiBiao.step));
		long num = (long)EquipmentManager.Instance.GetEquipFightingByExcellentAttrs(equipSimpleInfo.cfgId, equipSimpleInfo.excellentAttrs);
		if (this.fightingText != null)
		{
			this.fightingText.set_text(num + string.Empty);
		}
		Transform transform = base.FindTransform("BaseAttr");
		Attrs attrs = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrBaseValue);
		if (attrs != null)
		{
			for (int i = 0; i < attrs.attrs.get_Count(); i++)
			{
				if (i > 2)
				{
					break;
				}
				long value = (long)attrs.values.get_Item(i);
				transform.FindChild("EquipItem2Text" + i).get_gameObject().SetActive(true);
				transform.FindChild("EquipItem2Text" + i).FindChild("Item2Text").GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc((GameData.AttrType)attrs.attrs.get_Item(i), value, "ff7d4b"));
			}
			for (int j = attrs.attrs.get_Count(); j < 3; j++)
			{
				transform.FindChild("EquipItem2Text" + j).get_gameObject().SetActive(false);
			}
		}
		Transform transform2 = base.FindTransform("ExcellentAttr");
		Transform transform3 = base.FindTransform("ExcellentAttrIconList");
		transform3.get_gameObject().SetActive(false);
		if (equipSimpleInfo.excellentAttrs.get_Count() > 0)
		{
			transform2.get_gameObject().SetActive(true);
			int k = 0;
			int num2 = 0;
			while (k < equipSimpleInfo.excellentAttrs.get_Count())
			{
				if (k >= 5)
				{
					break;
				}
				if (equipSimpleInfo.excellentAttrs.get_Item(k).attrId < 0)
				{
					transform2.FindChild("EquipItem2Text" + k).get_gameObject().SetActive(false);
				}
				else
				{
					string excellentTypeColor = EquipGlobal.GetExcellentTypeColor(equipSimpleInfo.excellentAttrs.get_Item(k).color);
					string text = string.Empty;
					text = AttrUtility.GetStandardAddDesc(equipSimpleInfo.excellentAttrs.get_Item(k).attrId, equipSimpleInfo.excellentAttrs.get_Item(k).value, excellentTypeColor, excellentTypeColor);
					transform2.FindChild("EquipItem2Text" + k).get_gameObject().SetActive(true);
					transform2.FindChild("EquipItem2Text" + k).FindChild("Item2Text").GetComponent<Text>().set_text(text);
					string excellentRangeText = EquipGlobal.GetExcellentRangeText(equipSimpleInfo.cfgId, equipSimpleInfo.excellentAttrs.get_Item(k).attrId);
					transform2.FindChild("EquipItem2Text" + k).FindChild("Item2TextRange").GetComponent<Text>().set_text(excellentRangeText);
					if (equipSimpleInfo.excellentAttrs.get_Item(k).color >= 1f)
					{
						transform2.FindChild("EquipItem2Text" + k).FindChild("ItemImg").GetComponent<Image>().set_enabled(true);
						num2++;
					}
					else
					{
						transform2.FindChild("EquipItem2Text" + k).FindChild("ItemImg").GetComponent<Image>().set_enabled(false);
					}
				}
				k++;
			}
			for (int l = k; l < 5; l++)
			{
				transform2.FindChild("EquipItem2Text" + l).get_gameObject().SetActive(false);
			}
			if (num2 > 0)
			{
				if (!transform3.get_gameObject().get_activeSelf())
				{
					transform3.get_gameObject().SetActive(true);
				}
				for (int m = 0; m < num2; m++)
				{
					if (num2 >= 3)
					{
						break;
					}
					transform3.FindChild("Image" + (m + 1)).get_gameObject().SetActive(true);
				}
				for (int n = num2; n < 3; n++)
				{
					transform3.FindChild("Image" + (n + 1)).get_gameObject().SetActive(false);
				}
			}
		}
		int num3;
		for (num3 = 0; num3 < zZhuangBeiPeiZhiBiao.starNum; num3++)
		{
			this.starTransformList.get_Item(num3).get_gameObject().SetActive(true);
			if (equipSimpleInfo.star > num3)
			{
				this.starTransformList.get_Item(num3).FindChild("OpenStar").get_gameObject().SetActive(true);
				string starLevelSpriteName = this.GetStarLevelSpriteName(equipSimpleInfo.starToMaterial.get_Item(num3));
				ResourceManager.SetSprite(this.starTransformList.get_Item(num3).FindChild("OpenStar").GetComponent<Image>(), ResourceManager.GetIconSprite(starLevelSpriteName));
			}
			else
			{
				this.starTransformList.get_Item(num3).FindChild("OpenStar").get_gameObject().SetActive(false);
			}
		}
		for (int num4 = num3; num4 < this.starTransformList.get_Count(); num4++)
		{
			this.starTransformList.get_Item(num4).get_gameObject().SetActive(false);
		}
		List<int> list = new List<int>();
		list.Add(equipSimpleInfo.cfgId);
		int equipsTotalPoint = GuildStorageManager.Instance.GetEquipsTotalPoint(list, false);
		string text2 = "x" + equipsTotalPoint;
		if (hasMaxMoney >= equipsTotalPoint)
		{
			this.CostNumText = " " + text2;
		}
		else
		{
			this.CostNumText = " " + TextColorMgr.GetColorByID(text2, 1000007);
		}
		this.CostIconSR = GameDataUtils.GetItemIcon(19);
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
