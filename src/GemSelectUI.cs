using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSelectUI : UIBase
{
	private const int attrMaxCount = 2;

	public static GemSelectUI instance;

	private Transform gridLayoutGroup;

	private List<Transform> gemList;

	private Text title;

	private Text noGem;

	private List<MaterialGem> materialGems;

	public int currGemIndex;

	public int currTypeId;

	public long currGemId;

	public int currCount;

	public int currEquip;

	public int currSlot;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		GemSelectUI.instance = this;
		this.gemList = new List<Transform>();
	}

	private void Init()
	{
		this.InitButtonEvent();
		this.noGem = base.FindTransform("noGem").GetComponent<Text>();
		this.materialGems = GemGlobal.GetMaterialGems((EquipLibType.ELT)this.currEquip, this.currSlot);
		if (this.materialGems.get_Count() <= 0)
		{
			base.FindTransform("centre").get_gameObject().SetActive(false);
			this.noGem.get_gameObject().SetActive(true);
			this.noGem.set_text(GameDataUtils.GetChineseContent(621016, false));
		}
		else
		{
			base.FindTransform("centre").get_gameObject().SetActive(true);
			this.noGem.get_gameObject().SetActive(false);
			this.SetScrollRect(this.materialGems);
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnToShop").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnToShop);
	}

	private void InitButtonEvent()
	{
		base.FindTransform("btnComposeOne").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnComposeOne);
		base.FindTransform("btnComposeAll").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnComposeAll);
		base.FindTransform("btnWear").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWear);
	}

	protected override void OnEnable()
	{
	}

	protected override void OnClickMaskAction()
	{
		GemSingleUI gemSingleUI = UIManagerControl.Instance.GetUIIfExist("GemSingleUI") as GemSingleUI;
		if (gemSingleUI != null)
		{
			gemSingleUI.Show(false);
		}
		this.Show(false);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void OnClickBtnToShop(GameObject go)
	{
		LinkNavigationManager.OpenXMarketUI();
		this.OnClickCloseBtn(null);
	}

	private void OnClickBtnComposeOne(GameObject sender)
	{
		Debug.Log("OnClickBtnComposeOne");
		int afterId = GemGlobal.GetAfterId(this.currTypeId);
		GemManager.Instance.SendGemSysCompositeReq(afterId, 1);
	}

	private void OnClickBtnComposeAll(GameObject sender)
	{
		Debug.Log("OnClickBtnComposeAll");
		int afterId = GemGlobal.GetAfterId(this.currTypeId);
		GemManager.Instance.SendGemSysCompositeReq(afterId, -1);
	}

	private void SetButtonSelected(GameObject aGameObject)
	{
		using (List<Transform>.Enumerator enumerator = this.gemList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Transform current = enumerator.get_Current();
				bool selected = current.get_name() == aGameObject.get_name();
				current.GetComponent<PetID>().Selected = selected;
			}
		}
	}

	private void OnClickOneGem(GameObject sender)
	{
		this.SetButtonSelected(sender);
		int num = int.Parse(sender.get_name().Split(new char[]
		{
			'_'
		})[1]);
		MaterialGem materialGem = this.materialGems.get_Item(num);
		this.currGemIndex = num;
		this.currTypeId = materialGem.typeId;
		this.currGemId = materialGem.gemId;
		this.currCount = materialGem.count;
		Debug.Log(string.Concat(new object[]
		{
			"gemIndex=",
			num,
			" currTypeId=",
			this.currTypeId,
			" currGemId=",
			this.currGemId
		}));
		this.SetOneGem(this.currTypeId);
	}

	private void SetOneGem(int typeId)
	{
		this.SetGemName(typeId);
		this.SetGemAttrs(typeId);
		this.SetGemDesc(typeId);
		this.SetGemButton(typeId);
	}

	private void SetGemButton(int typeId)
	{
		Transform transform = base.FindTransform("btnComposeOne");
		Transform transform2 = base.FindTransform("btnComposeAll");
		transform.Find("texBest").get_gameObject().SetActive(true);
		transform.Find("texBest").GetComponent<Text>().set_text(GemGlobal.GetComposeGemTip(typeId));
		if (GemGlobal.IsCanCompose(typeId))
		{
			SpriteRenderer iconSprite = ResourceManager.GetIconSprite("button_yellow_1");
			transform.GetComponent<ButtonCustom>().set_enabled(true);
			ResourceManager.SetSprite(transform.GetComponent<Image>(), iconSprite);
			transform2.GetComponent<ButtonCustom>().set_enabled(true);
			ResourceManager.SetSprite(transform2.GetComponent<Image>(), iconSprite);
		}
		else
		{
			SpriteRenderer iconSprite2 = ResourceManager.GetIconSprite("button_gray_1");
			transform.GetComponent<ButtonCustom>().set_enabled(false);
			ResourceManager.SetSprite(transform.GetComponent<Image>(), iconSprite2);
			transform2.GetComponent<ButtonCustom>().set_enabled(false);
			ResourceManager.SetSprite(transform2.GetComponent<Image>(), iconSprite2);
		}
		Transform transform3 = base.FindTransform("btnWear");
		GemEmbedInfo gemInfo = GemGlobal.GetGemInfo((EquipLibType.ELT)this.currEquip, this.currSlot);
		BaoShiShengJi baoShiShengJi = DataReader<BaoShiShengJi>.Get(this.currTypeId);
	}

	private void SetGemAttrs(int typeId)
	{
		List<string> strAttrs = GemGlobal.GetStrAttrs(typeId);
		string text = string.Empty;
		Text component = base.FindTransform("texAttr0").GetComponent<Text>();
		int num = (strAttrs.get_Count() <= 2) ? strAttrs.get_Count() : 2;
		for (int i = 0; i < num; i++)
		{
			text += strAttrs.get_Item(i);
			if (i < num - 1)
			{
				text += "\n";
			}
		}
		component.set_text(text);
	}

	private void SetGemName(int typeId)
	{
		Items dataItem = DataReader<Items>.Get(typeId);
		base.FindTransform("texName").GetComponent<Text>().set_text(GameDataUtils.GetItemName(dataItem, true));
	}

	private void SetGemDesc(int typeId)
	{
		Items items = DataReader<Items>.Get(typeId);
		base.FindTransform("texDesc").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(items.describeId1, false));
	}

	private void OnClickBtnWear(GameObject sender)
	{
		Debug.Log("OnClickBtnWear=" + sender);
		GemManager.Instance.SendGemSysEmbedReq((EquipLibType.ELT)this.currEquip, this.currSlot, this.currGemId);
	}

	private void ClearScrollRect()
	{
		for (int i = 0; i < this.gridLayoutGroup.get_childCount(); i++)
		{
			Transform child = this.gridLayoutGroup.GetChild(i);
			Object.Destroy(child.get_gameObject());
		}
	}

	private void SetScrollRect(List<MaterialGem> materialGems)
	{
		this.ClearScrollRect();
		this.gemList.Clear();
		int num = 6;
		int num2 = Mathf.CeilToInt((float)materialGems.get_Count() / (float)num);
		for (int i = 0; i < num2; i++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GemSelectCell");
			instantiate2Prefab.get_transform().SetParent(this.gridLayoutGroup, false);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.set_name("GemSelectCell_" + i);
			for (int j = 0; j < num; j++)
			{
				int num3 = num * i + j;
				if (num3 >= materialGems.get_Count())
				{
					break;
				}
				MaterialGem materialGem = materialGems.get_Item(num3);
				GameObject instantiate2Prefab2 = ResourceManager.GetInstantiate2Prefab(WidgetName.PetChooseItem);
				instantiate2Prefab2.set_name("GemItem_" + num3);
				PetID component = instantiate2Prefab2.GetComponent<PetID>();
				instantiate2Prefab2.SetActive(true);
				instantiate2Prefab2.get_transform().SetParent(instantiate2Prefab.get_transform());
				instantiate2Prefab2.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
				instantiate2Prefab2.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOneGem);
				component.SetGemIcon(materialGem.typeId, materialGem.count);
				this.gemList.Add(instantiate2Prefab2.get_transform());
			}
		}
		if (this.gemList.get_Count() > 0)
		{
			int num4 = this.gemList.FindIndex((Transform a) => a.GetComponent<PetID>().ItemID == this.currTypeId);
			if (num4 == -1)
			{
				num4 = Mathf.Clamp(0, this.currGemIndex - 1, this.gemList.get_Count() - 1);
				this.OnClickOneGem(this.gemList.get_Item(num4).get_gameObject());
			}
			else
			{
				this.OnClickOneGem(this.gemList.get_Item(num4).get_gameObject());
			}
		}
	}

	public void Init(int equip, int slot)
	{
		this.gridLayoutGroup = base.FindTransform("gridLayoutGroup");
		this.gridLayoutGroup.set_localPosition(new Vector3(this.gridLayoutGroup.get_localPosition().x, 0f));
		this.currGemIndex = 0;
		this.currTypeId = 0;
		this.Refresh(equip, slot);
	}

	public void Refresh(int equip, int slot)
	{
		this.currEquip = equip;
		this.currSlot = slot;
		this.Init();
	}
}
