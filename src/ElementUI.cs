using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUI : UIBase
{
	private const int elementSubTypeNum = 6;

	private ButtonCustom Element1;

	private ButtonCustom Element2;

	private ButtonCustom Element3;

	private ButtonCustom Element4;

	private ButtonCustom Element5;

	private ButtonCustom Element6;

	private ButtonCustom BtnGround;

	private ButtonCustom BtnFire;

	private ButtonCustom BtnWater;

	private ButtonCustom BtnLighting;

	private Transform ImageArrows;

	private ButtonCustom BtnBack;

	private Image ImageLevelBG;

	private Text TextTitleName;

	private Text TextTitleLV;

	private Text TextNowEffectTitle;

	private Text TextNowEffect;

	private Text TextUpgradeEffectTitle;

	private Text TextUpgradeEffect;

	private Text TextCostTitle;

	private Text TextCostNum;

	private ButtonCustom BtnUpdate;

	private Text TextUpgradeCondition;

	private Text TextUpgradeConditionTitle;

	private Image ImageCost;

	private int lastType = 999;

	private int lastSelectSubType = 999;

	private Dictionary<int, Transform> dicElementBtns = new Dictionary<int, Transform>();

	private Dictionary<int, Transform> dicTypeBtns = new Dictionary<int, Transform>();

	private int fxidBG;

	private int fxBGElement1;

	private int fxBGElement2;

	private int fxBGElement3;

	private int fxBGElement4;

	private int fxUpgradeAble1;

	private int fxUpgradeAble2;

	private int fxUpgradeAble3;

	private int fxUpgradeAble4;

	private int fxUpgradeAble5;

	private int fxUpgradeAble6;

	private int fxSelected1;

	private int fxSelected2;

	private int fxSelected3;

	private int fxSelected4;

	private int fxSelected5;

	private int fxSelected6;

	private int fxArrow1;

	private int fxArrow2;

	private int fxArrow3;

	private int fxArrow4;

	private int fxArrow5;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.hideMainCamera = true;
		this.Element1 = base.FindTransform("Element1").GetComponent<ButtonCustom>();
		this.Element2 = base.FindTransform("Element2").GetComponent<ButtonCustom>();
		this.Element3 = base.FindTransform("Element3").GetComponent<ButtonCustom>();
		this.Element4 = base.FindTransform("Element4").GetComponent<ButtonCustom>();
		this.Element5 = base.FindTransform("Element5").GetComponent<ButtonCustom>();
		this.Element6 = base.FindTransform("Element6").GetComponent<ButtonCustom>();
		this.dicElementBtns.Add(1, this.Element1.get_transform());
		this.dicElementBtns.Add(2, this.Element2.get_transform());
		this.dicElementBtns.Add(3, this.Element3.get_transform());
		this.dicElementBtns.Add(4, this.Element4.get_transform());
		this.dicElementBtns.Add(5, this.Element5.get_transform());
		this.dicElementBtns.Add(6, this.Element6.get_transform());
		this.BtnGround = base.FindTransform("BtnGround").GetComponent<ButtonCustom>();
		this.BtnFire = base.FindTransform("BtnFire").GetComponent<ButtonCustom>();
		this.BtnWater = base.FindTransform("BtnWater").GetComponent<ButtonCustom>();
		this.BtnLighting = base.FindTransform("BtnLighting").GetComponent<ButtonCustom>();
		this.dicTypeBtns.Add(1, this.BtnGround.get_transform());
		this.dicTypeBtns.Add(2, this.BtnFire.get_transform());
		this.dicTypeBtns.Add(3, this.BtnWater.get_transform());
		this.dicTypeBtns.Add(4, this.BtnLighting.get_transform());
		this.ImageArrows = base.FindTransform("ImageArrows");
		this.BtnBack = base.FindTransform("BtnBack").GetComponent<ButtonCustom>();
		this.ImageLevelBG = base.FindTransform("ImageLevelBG").GetComponent<Image>();
		this.TextTitleName = base.FindTransform("TextTitleName").GetComponent<Text>();
		this.TextTitleLV = base.FindTransform("TextTitleLV").GetComponent<Text>();
		this.TextNowEffectTitle = base.FindTransform("TextNowEffectTitle").GetComponent<Text>();
		this.TextNowEffect = base.FindTransform("TextNowEffect").GetComponent<Text>();
		this.TextUpgradeEffectTitle = base.FindTransform("TextUpgradeEffectTitle").GetComponent<Text>();
		this.TextUpgradeEffect = base.FindTransform("TextUpgradeEffect").GetComponent<Text>();
		this.TextCostTitle = base.FindTransform("TextCostTitle").GetComponent<Text>();
		this.TextCostNum = base.FindTransform("TextCostNum").GetComponent<Text>();
		this.BtnUpdate = base.FindTransform("BtnUpdate").GetComponent<ButtonCustom>();
		this.TextUpgradeCondition = base.FindTransform("TextUpgradeCondition").GetComponent<Text>();
		this.TextUpgradeConditionTitle = base.FindTransform("TextUpgradeConditionTitle").GetComponent<Text>();
		this.ImageCost = base.FindTransform("ImageCost").GetComponent<Image>();
	}

	private void Start()
	{
		this.Element1.get_transform().FindChild("TextTile").GetComponent<Text>().set_text(this.GetSubElementTypeTextFromID(1));
		this.Element2.get_transform().FindChild("TextTile").GetComponent<Text>().set_text(this.GetSubElementTypeTextFromID(2));
		this.Element3.get_transform().FindChild("TextTile").GetComponent<Text>().set_text(this.GetSubElementTypeTextFromID(3));
		this.Element4.get_transform().FindChild("TextTile").GetComponent<Text>().set_text(this.GetSubElementTypeTextFromID(4));
		this.Element5.get_transform().FindChild("TextTile").GetComponent<Text>().set_text(this.GetSubElementTypeTextFromID(5));
		this.Element6.get_transform().FindChild("TextTile").GetComponent<Text>().set_text(this.GetSubElementTypeTextFromID(6));
		this.Element1.get_transform().FindChild("TextOpen").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505050, false));
		this.Element2.get_transform().FindChild("TextOpen").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505050, false));
		this.Element3.get_transform().FindChild("TextOpen").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505050, false));
		this.Element4.get_transform().FindChild("TextOpen").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505050, false));
		this.Element5.get_transform().FindChild("TextOpen").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505050, false));
		this.Element6.get_transform().FindChild("TextOpen").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505050, false));
		this.BtnGround.get_transform().FindChild("TextName1").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(1));
		this.BtnGround.get_transform().FindChild("TextName2").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(1));
		this.BtnFire.get_transform().FindChild("TextName1").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(2));
		this.BtnFire.get_transform().FindChild("TextName2").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(2));
		this.BtnWater.get_transform().FindChild("TextName1").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(3));
		this.BtnWater.get_transform().FindChild("TextName2").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(3));
		this.BtnLighting.get_transform().FindChild("TextName1").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(4));
		this.BtnLighting.get_transform().FindChild("TextName2").GetComponent<Text>().set_text(this.GetElementTypeTextFromID(4));
		this.ManagerFxidBG(this.lastType);
		this.TextUpgradeConditionTitle.set_text(GameDataUtils.GetChineseContent(505059, false));
		this.TextNowEffectTitle.set_text(GameDataUtils.GetChineseContent(505040, false));
		this.TextUpgradeEffectTitle.set_text(GameDataUtils.GetChineseContent(505041, false));
		this.TextCostTitle.set_text(GameDataUtils.GetChineseContent(505042, false));
		this.BtnUpdate.get_transform().FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505043, false));
		this.Element1.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElement1);
		this.Element2.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElement2);
		this.Element3.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElement3);
		this.Element4.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElement4);
		this.Element5.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElement5);
		this.Element6.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickElement6);
		this.BtnGround.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtn1);
		this.BtnFire.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtn2);
		this.BtnWater.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtn3);
		this.BtnLighting.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtn4);
		this.BtnBack.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBack);
		this.BtnUpdate.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnUpdate);
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110020), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.ManagerFxidBG(this.lastType);
		this.CheckBadge();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.RemoveFxs();
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetElementChangedNty, new Callback(this.OnGetElementUpRes));
		EventDispatcher.AddListener<int, int>(EventNames.ElementUpgrade, new Callback<int, int>(this.ElementUpgrade));
		EventDispatcher.AddListener<int, int>(EventNames.ElementUnLock, new Callback<int, int>(this.ElementUnLock));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetElementChangedNty, new Callback(this.OnGetElementUpRes));
		EventDispatcher.RemoveListener<int, int>(EventNames.ElementUpgrade, new Callback<int, int>(this.ElementUpgrade));
		EventDispatcher.RemoveListener<int, int>(EventNames.ElementUnLock, new Callback<int, int>(this.ElementUnLock));
	}

	private void OnClickElement1(GameObject sender)
	{
		if (this.lastSelectSubType == 1)
		{
			return;
		}
		this.ResetSelectSubTypeUI(1);
	}

	private void OnClickElement2(GameObject sender)
	{
		if (this.lastSelectSubType == 2)
		{
			return;
		}
		this.ResetSelectSubTypeUI(2);
	}

	private void OnClickElement3(GameObject sender)
	{
		if (this.lastSelectSubType == 3)
		{
			return;
		}
		this.ResetSelectSubTypeUI(3);
	}

	private void OnClickElement4(GameObject sender)
	{
		if (this.lastSelectSubType == 4)
		{
			return;
		}
		this.ResetSelectSubTypeUI(4);
	}

	private void OnClickElement5(GameObject sender)
	{
		if (this.lastSelectSubType == 5)
		{
			return;
		}
		this.ResetSelectSubTypeUI(5);
	}

	private void OnClickElement6(GameObject sender)
	{
		if (this.lastSelectSubType == 6)
		{
			return;
		}
		this.ResetSelectSubTypeUI(6);
	}

	private void OnClickBtn1(GameObject sender)
	{
		if (this.lastType == 1)
		{
			return;
		}
		this.ResetTypeUI(1, 1);
	}

	private void OnClickBtn2(GameObject sender)
	{
		if (this.lastType == 2)
		{
			return;
		}
		this.ResetTypeUI(2, 1);
	}

	private void OnClickBtn3(GameObject sender)
	{
		if (this.lastType == 3)
		{
			return;
		}
		this.ResetTypeUI(3, 1);
	}

	private void OnClickBtn4(GameObject sender)
	{
		if (this.lastType == 4)
		{
			return;
		}
		this.ResetTypeUI(4, 1);
	}

	private void OnClickBtnBack(GameObject sender)
	{
		this.Show(false);
	}

	private void OnClickBtnUpdate(GameObject sender)
	{
		if (this.lastType != 999 && this.lastSelectSubType != 999)
		{
			ElementManager.Instance.SendElementUpReq(this.lastType, this.lastSelectSubType);
		}
	}

	private string GetElementTypeTextFromID(int id)
	{
		switch (id)
		{
		case 1:
			return GameDataUtils.GetChineseContent(505054, false);
		case 2:
			return GameDataUtils.GetChineseContent(505055, false);
		case 3:
			return GameDataUtils.GetChineseContent(505056, false);
		case 4:
			return GameDataUtils.GetChineseContent(505057, false);
		default:
			return GameDataUtils.GetChineseContent(505054, false);
		}
	}

	private string GetSubElementTypeTextFromID(int id)
	{
		switch (id)
		{
		case 1:
			return GameDataUtils.GetChineseContent(505023, false);
		case 2:
			return GameDataUtils.GetChineseContent(505024, false);
		case 3:
			return GameDataUtils.GetChineseContent(505025, false);
		case 4:
			return GameDataUtils.GetChineseContent(505026, false);
		case 5:
			return GameDataUtils.GetChineseContent(505027, false);
		case 6:
			return GameDataUtils.GetChineseContent(505051, false);
		default:
			return GameDataUtils.GetChineseContent(505023, false);
		}
	}

	private string GetUpgradeCondition(YuanSuShuXing yssxNextLv)
	{
		string text = string.Empty;
		if (yssxNextLv.preElementLmt.get_Count() == 0)
		{
			string text2 = GameDataUtils.GetChineseContent(505058, false);
			text2 = text2.Replace("xxx", yssxNextLv.lvLmt.ToString());
			text += text2;
		}
		else
		{
			for (int i = 0; i < yssxNextLv.preElementLmt.get_Count(); i++)
			{
				YuanSuShuXing yuanSuShuXing = DataReader<YuanSuShuXing>.Get(yssxNextLv.preElementLmt.get_Item(i));
				string text3 = GameDataUtils.GetChineseContent(505060, false);
				text3 = text3.Replace("xxx1", this.GetSubElementTypeTextFromID(yuanSuShuXing.subType));
				text3 = text3.Replace("xxx2", yuanSuShuXing.lv.ToString());
				text3 += "\n";
				text += text3;
			}
			if (yssxNextLv.lvLmt != 0)
			{
				string text4 = GameDataUtils.GetChineseContent(505058, false);
				text4 = text4.Replace("xxx", yssxNextLv.lvLmt.ToString());
				text += text4;
			}
			else
			{
				text = text.Replace(",", "。");
			}
		}
		return text;
	}

	private string GetLvDes(int contentID, Attrs attrs)
	{
		string text = GameDataUtils.GetChineseContent(contentID, false);
		for (int i = 0; i < attrs.values.get_Count(); i++)
		{
			text = text.Replace("{num" + (i + 1).ToString() + "}", "<color=white>" + (attrs.values.get_Item(i) * 100).ToString("0.0") + "%</color>");
		}
		return text;
	}

	private string GetLvUpDes(int contentID, Attrs attrs, Attrs nextAttrs)
	{
		string text = GameDataUtils.GetChineseContent(contentID, false);
		for (int i = 0; i < nextAttrs.values.get_Count(); i++)
		{
			if (attrs == null)
			{
				text = text.Replace("{num" + (i + 1).ToString() + "}", "<color=white>0%</color>");
				text = text.Replace("{next" + (i + 1).ToString() + "}", "<color=white>" + (nextAttrs.values.get_Item(i) * 100).ToString("0.0") + "%</color>");
			}
			else
			{
				text = text.Replace("{num" + (i + 1).ToString() + "}", "<color=white>" + (attrs.values.get_Item(i) * 100).ToString("0.0") + "%</color>");
				text = text.Replace("{next" + (i + 1).ToString() + "}", "<color=white>" + (nextAttrs.values.get_Item(i) * 100).ToString("0.0") + "%</color>");
			}
		}
		return text;
	}

	private bool CanUpdate(YuanSuShuXing yssxNextLv)
	{
		bool result = true;
		for (int i = 0; i < yssxNextLv.preElementLmt.get_Count(); i++)
		{
			YuanSuShuXing yuanSuShuXing = DataReader<YuanSuShuXing>.Get(yssxNextLv.preElementLmt.get_Item(i));
			int eleID = ElementManager.Instance.MakeElementID(yuanSuShuXing.type, yuanSuShuXing.subType);
			ElementInfo elementInfo = ElementManager.Instance.elementInfos.Find((ElementInfo e) => e.elemId == eleID);
			if (elementInfo.elemLv < yuanSuShuXing.lv)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private void ResetSelectSubTypeUI(int subType)
	{
		switch (subType)
		{
		case 1:
			this.ManagerFxSelected(this.fxSelected1, this.Element1.get_transform().FindChild("FX"));
			break;
		case 2:
			this.ManagerFxSelected(this.fxSelected2, this.Element2.get_transform().FindChild("FX"));
			break;
		case 3:
			this.ManagerFxSelected(this.fxSelected3, this.Element3.get_transform().FindChild("FX"));
			break;
		case 4:
			this.ManagerFxSelected(this.fxSelected4, this.Element4.get_transform().FindChild("FX"));
			break;
		case 5:
			this.ManagerFxSelected(this.fxSelected5, this.Element5.get_transform().FindChild("FX"));
			break;
		case 6:
			this.ManagerFxSelected(this.fxSelected6, this.Element6.get_transform().FindChild("FX"));
			break;
		}
		this.lastSelectSubType = subType;
		int eleID = ElementManager.Instance.MakeElementID(this.lastType, subType);
		ElementInfo elementInfo = ElementManager.Instance.elementInfos.Find((ElementInfo e) => e.elemId == eleID);
		this.TextTitleName.set_text(this.GetSubElementTypeTextFromID(subType));
		this.TextTitleLV.set_text("Lv." + elementInfo.elemLv);
		if (elementInfo.elemLv == 0)
		{
			YuanSuShuXing yuanSuShuXing = DataReader<YuanSuShuXing>.DataList.Find((YuanSuShuXing e) => e.type == this.lastType && e.subType == this.lastSelectSubType && e.lv == elementInfo.elemLv + 1);
			ResourceManager.SetSprite(this.ImageCost, GameDataUtils.GetIcon(DataReader<Items>.Get(yuanSuShuXing.itemId.get_Item(0)).icon));
			this.TextNowEffect.set_text(GameDataUtils.GetChineseContent(yuanSuShuXing.desc3, false));
			Attrs nextAttrs = DataReader<Attrs>.Get(yuanSuShuXing.attributeTemplateID);
			this.TextUpgradeEffect.set_text(this.GetLvUpDes(yuanSuShuXing.desc2, null, nextAttrs));
			long num = BackpackManager.Instance.OnGetGoodCount(yuanSuShuXing.itemId.get_Item(0));
			int num2 = yuanSuShuXing.itemNum.get_Item(0);
			string text;
			if (num < (long)num2)
			{
				text = string.Concat(new object[]
				{
					"<color=red>",
					num,
					"</color>/",
					yuanSuShuXing.itemNum.get_Item(0)
				});
			}
			else
			{
				text = string.Concat(new object[]
				{
					" ",
					num,
					"/",
					yuanSuShuXing.itemNum.get_Item(0)
				});
			}
			this.TextCostNum.set_text(text);
			if (!elementInfo.upgradable)
			{
				this.BtnUpdate.get_gameObject().SetActive(false);
				this.TextUpgradeConditionTitle.get_gameObject().SetActive(true);
				this.TextUpgradeCondition.get_gameObject().SetActive(true);
				this.TextUpgradeCondition.set_text(this.GetUpgradeCondition(yuanSuShuXing));
			}
			else if (elementInfo.upgradable)
			{
				this.TextUpgradeConditionTitle.get_gameObject().SetActive(false);
				this.TextUpgradeCondition.get_gameObject().SetActive(false);
				this.BtnUpdate.get_gameObject().SetActive(true);
			}
			else
			{
				this.TextUpgradeConditionTitle.get_gameObject().SetActive(true);
				this.TextUpgradeCondition.get_gameObject().SetActive(true);
				this.BtnUpdate.get_gameObject().SetActive(false);
				this.TextUpgradeCondition.set_text(this.GetUpgradeCondition(yuanSuShuXing));
			}
		}
		else if (elementInfo.elemLv < elementInfo.elemMaxLv)
		{
			YuanSuShuXing yuanSuShuXing2 = DataReader<YuanSuShuXing>.DataList.Find((YuanSuShuXing e) => e.type == this.lastType && e.subType == this.lastSelectSubType && e.lv == elementInfo.elemLv);
			Attrs attrs = DataReader<Attrs>.Get(yuanSuShuXing2.attributeTemplateID);
			ResourceManager.SetSprite(this.ImageCost, GameDataUtils.GetIcon(DataReader<Items>.Get(yuanSuShuXing2.itemId.get_Item(0)).icon));
			this.TextNowEffect.set_text(this.GetLvDes(yuanSuShuXing2.desc, attrs));
			YuanSuShuXing yuanSuShuXing3 = DataReader<YuanSuShuXing>.DataList.Find((YuanSuShuXing e) => e.type == this.lastType && e.subType == this.lastSelectSubType && e.lv == elementInfo.elemLv + 1);
			Attrs nextAttrs2 = DataReader<Attrs>.Get(yuanSuShuXing3.attributeTemplateID);
			long num3 = BackpackManager.Instance.OnGetGoodCount(yuanSuShuXing3.itemId.get_Item(0));
			int num4 = yuanSuShuXing3.itemNum.get_Item(0);
			string text2;
			if (num3 < (long)num4)
			{
				text2 = string.Concat(new object[]
				{
					"<color=red>",
					num3,
					"</color>/",
					yuanSuShuXing3.itemNum.get_Item(0)
				});
			}
			else
			{
				text2 = string.Concat(new object[]
				{
					" ",
					num3,
					"/",
					yuanSuShuXing3.itemNum.get_Item(0)
				});
			}
			this.TextCostNum.set_text(text2);
			this.TextUpgradeEffect.set_text(this.GetLvUpDes(yuanSuShuXing3.desc2, attrs, nextAttrs2));
			if (elementInfo.upgradable)
			{
				this.TextUpgradeConditionTitle.get_gameObject().SetActive(false);
				this.TextUpgradeCondition.get_gameObject().SetActive(false);
				this.BtnUpdate.get_gameObject().SetActive(true);
			}
			else
			{
				this.TextUpgradeConditionTitle.get_gameObject().SetActive(true);
				this.TextUpgradeCondition.get_gameObject().SetActive(true);
				this.BtnUpdate.get_gameObject().SetActive(false);
				this.TextUpgradeCondition.set_text(this.GetUpgradeCondition(yuanSuShuXing3));
			}
		}
		else
		{
			YuanSuShuXing yuanSuShuXing4 = DataReader<YuanSuShuXing>.DataList.Find((YuanSuShuXing e) => e.type == this.lastType && e.subType == this.lastSelectSubType && e.lv == elementInfo.elemLv);
			Attrs attrs2 = DataReader<Attrs>.Get(yuanSuShuXing4.attributeTemplateID);
			ResourceManager.SetSprite(this.ImageCost, GameDataUtils.GetIcon(DataReader<Items>.Get(yuanSuShuXing4.itemId.get_Item(0)).icon));
			this.TextNowEffect.set_text(this.GetLvDes(yuanSuShuXing4.desc, attrs2));
			this.TextUpgradeEffect.set_text(GameDataUtils.GetChineseContent(505062, false));
			this.TextCostNum.set_text("0/0");
			this.BtnUpdate.get_gameObject().SetActive(false);
			this.TextUpgradeConditionTitle.get_gameObject().SetActive(true);
			this.TextUpgradeCondition.get_gameObject().SetActive(true);
			string text3 = GameDataUtils.GetChineseContent(505062, false);
			text3 = text3.Replace("xxx", this.GetSubElementTypeTextFromID(subType));
			this.TextUpgradeCondition.set_text(text3);
		}
	}

	public void ResetTypeUI(int type, int subType)
	{
		int[] array = new int[4];
		for (int i = 0; i < ElementManager.Instance.elementInfos.get_Count(); i++)
		{
			ElementInfo elementInfo = ElementManager.Instance.elementInfos.get_Item(i);
			array[ElementManager.Instance.ParseElementID(elementInfo.elemId).get_Key() - 1] += elementInfo.elemLv;
		}
		if (type != this.lastType)
		{
			switch (type)
			{
			case 1:
				this.ManagerFxBGElement(140);
				ResourceManager.SetSprite(this.ImageLevelBG.GetComponent<Image>(), ResourceManager.GetIconSprite("dadi_fazhen_quan"));
				break;
			case 2:
				this.ManagerFxBGElement(118);
				ResourceManager.SetSprite(this.ImageLevelBG.GetComponent<Image>(), ResourceManager.GetIconSprite("huo_fazhen_quan"));
				break;
			case 3:
				this.ManagerFxBGElement(107);
				ResourceManager.SetSprite(this.ImageLevelBG.GetComponent<Image>(), ResourceManager.GetIconSprite("bin_fazhen_quan"));
				break;
			case 4:
				this.ManagerFxBGElement(129);
				ResourceManager.SetSprite(this.ImageLevelBG.GetComponent<Image>(), ResourceManager.GetIconSprite("dian_fazhen_quan"));
				break;
			}
		}
		if (this.lastType != type)
		{
			Transform transform = this.dicTypeBtns.get_Item(type);
			transform.FindChild("ImageArrow").get_gameObject().SetActive(true);
			transform.GetComponent<CanvasGroup>().set_alpha(1f);
			transform.FindChild("TextName2").get_gameObject().SetActive(true);
			transform.FindChild("TextName1").get_gameObject().SetActive(false);
			transform.FindChild("ImageIcon").GetComponent<Image>().set_color(Color.get_white());
			if (this.lastType != 999)
			{
				transform = this.dicTypeBtns.get_Item(this.lastType);
				transform.FindChild("ImageArrow").get_gameObject().SetActive(false);
				transform.GetComponent<CanvasGroup>().set_alpha(0.7f);
				transform.FindChild("TextName2").get_gameObject().SetActive(false);
				transform.FindChild("TextName1").get_gameObject().SetActive(true);
				transform.FindChild("ImageIcon").GetComponent<Image>().set_color(new Color(0.6666667f, 0.6666667f, 0.6666667f, 1f));
			}
			this.ManagerFxidBG(type);
		}
		int num = this.lastType;
		this.lastType = type;
		int num2 = 0;
		for (int j = 1; j <= 6; j++)
		{
			int eleID = ElementManager.Instance.MakeElementID(type, j);
			ElementInfo elementInfo2 = ElementManager.Instance.elementInfos.Find((ElementInfo e) => e.elemId == eleID);
			Transform transform2 = this.dicElementBtns.get_Item(j);
			num2 += elementInfo2.elemLv;
			if (!elementInfo2.upgradable)
			{
				transform2.FindChild("TextOpen").get_gameObject().SetActive(false);
				if (elementInfo2.elemLv == 0)
				{
					transform2.FindChild("TextLevel").get_gameObject().SetActive(false);
					ResourceManager.SetSprite(transform2.FindChild("ImageIcon1").GetComponent<Image>(), ResourceManager.GetIconSprite("yuansu_" + j + "_1"));
				}
				else
				{
					transform2.FindChild("TextLevel").get_gameObject().SetActive(true);
					ResourceManager.SetSprite(transform2.FindChild("ImageIcon1").GetComponent<Image>(), ResourceManager.GetIconSprite("yuansu_" + j));
				}
				switch (j)
				{
				case 2:
					if (elementInfo2.elemLv == 0)
					{
						this.ImageArrows.FindChild("Arrow1").get_gameObject().SetActive(false);
					}
					else
					{
						this.ImageArrows.FindChild("Arrow1").get_gameObject().SetActive(true);
						this.PlayArrowFX(1);
					}
					break;
				case 3:
					if (elementInfo2.elemLv == 0)
					{
						this.ImageArrows.FindChild("Arrow2").get_gameObject().SetActive(false);
					}
					else
					{
						this.ImageArrows.FindChild("Arrow2").get_gameObject().SetActive(true);
						this.PlayArrowFX(2);
					}
					break;
				case 4:
					if (elementInfo2.elemLv == 0)
					{
						this.ImageArrows.FindChild("Arrow3").get_gameObject().SetActive(false);
					}
					else
					{
						this.ImageArrows.FindChild("Arrow3").get_gameObject().SetActive(true);
						this.PlayArrowFX(3);
					}
					break;
				case 5:
					if (elementInfo2.elemLv == 0)
					{
						this.ImageArrows.FindChild("Arrow4").get_gameObject().SetActive(false);
					}
					else
					{
						this.ImageArrows.FindChild("Arrow4").get_gameObject().SetActive(true);
						this.PlayArrowFX(4);
					}
					break;
				case 6:
					if (elementInfo2.elemLv == 0)
					{
						this.ImageArrows.FindChild("Arrow5").get_gameObject().SetActive(false);
					}
					else
					{
						this.ImageArrows.FindChild("Arrow5").get_gameObject().SetActive(true);
						this.PlayArrowFX(5);
					}
					break;
				}
			}
			else
			{
				transform2.FindChild("TextOpen").get_gameObject().SetActive(false);
				transform2.FindChild("TextLevel").get_gameObject().SetActive(true);
				ResourceManager.SetSprite(transform2.FindChild("ImageIcon1").GetComponent<Image>(), ResourceManager.GetIconSprite("yuansu_" + j));
				switch (j)
				{
				case 1:
					this.ManagerFxUpgradeAble(this.fxUpgradeAble1, this.Element1.get_transform().FindChild("FX"));
					break;
				case 2:
					this.ManagerFxUpgradeAble(this.fxUpgradeAble2, this.Element2.get_transform().FindChild("FX"));
					this.ImageArrows.FindChild("Arrow1").get_gameObject().SetActive(true);
					if (this.lastType != num)
					{
						this.PlayArrowFX(1);
					}
					break;
				case 3:
					this.ManagerFxUpgradeAble(this.fxUpgradeAble3, this.Element3.get_transform().FindChild("FX"));
					this.ImageArrows.FindChild("Arrow2").get_gameObject().SetActive(true);
					if (this.lastType != num)
					{
						this.PlayArrowFX(2);
					}
					break;
				case 4:
					this.ManagerFxUpgradeAble(this.fxUpgradeAble4, this.Element4.get_transform().FindChild("FX"));
					this.ImageArrows.FindChild("Arrow3").get_gameObject().SetActive(true);
					if (this.lastType != num)
					{
						this.PlayArrowFX(3);
					}
					break;
				case 5:
					this.ManagerFxUpgradeAble(this.fxUpgradeAble5, this.Element5.get_transform().FindChild("FX"));
					this.ImageArrows.FindChild("Arrow4").get_gameObject().SetActive(true);
					if (this.lastType != num)
					{
						this.PlayArrowFX(4);
					}
					break;
				case 6:
					this.ManagerFxUpgradeAble(this.fxUpgradeAble6, this.Element6.get_transform().FindChild("FX"));
					this.ImageArrows.FindChild("Arrow5").get_gameObject().SetActive(true);
					if (this.lastType != num)
					{
						this.PlayArrowFX(5);
					}
					break;
				}
			}
			transform2.FindChild("TextLevel").GetComponent<Text>().set_text("Lv." + elementInfo2.elemLv);
		}
		this.ResetSelectSubTypeUI(subType);
	}

	public void CheckBadge()
	{
		Dictionary<int, bool> dictionary = ElementManager.Instance.CheckElementTypesCanUpdate();
		if (dictionary.get_Item(1))
		{
			this.BtnGround.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
		}
		else
		{
			this.BtnGround.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		}
		if (dictionary.get_Item(2))
		{
			this.BtnFire.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
		}
		else
		{
			this.BtnFire.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		}
		if (dictionary.get_Item(3))
		{
			this.BtnWater.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
		}
		else
		{
			this.BtnWater.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		}
		if (dictionary.get_Item(4))
		{
			this.BtnLighting.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(true);
		}
		else
		{
			this.BtnLighting.get_transform().FindChild("ImageBadge").get_gameObject().SetActive(false);
		}
	}

	public void ResetUI()
	{
		if (this.lastType == 999)
		{
			this.ResetTypeUI(1, 1);
		}
		else
		{
			this.ResetTypeUI(this.lastType, this.lastSelectSubType);
		}
	}

	private void ManagerFxSelected(int fxSelected, Transform tr)
	{
		int num = 0;
		if (this.lastType != 1)
		{
			if (this.lastType != 2)
			{
				if (this.lastType != 3)
				{
					if (this.lastType == 4)
					{
					}
				}
			}
		}
		if (fxSelected == this.fxSelected1)
		{
			this.fxSelected1 = num;
		}
		else if (fxSelected == this.fxSelected2)
		{
			this.fxSelected2 = num;
		}
		else if (fxSelected == this.fxSelected3)
		{
			this.fxSelected3 = num;
		}
		else if (fxSelected == this.fxSelected4)
		{
			this.fxSelected4 = num;
		}
		else if (fxSelected == this.fxSelected5)
		{
			this.fxSelected5 = num;
		}
		else if (fxSelected == this.fxSelected6)
		{
			this.fxSelected6 = num;
		}
	}

	private void ManagerFxUpgradeAble(int fxUpgradeAble, Transform tr)
	{
		int num = 0;
		if (this.lastType == 1)
		{
		}
		if (this.lastType == 2)
		{
		}
		if (this.lastType == 3)
		{
		}
		if (this.lastType == 4)
		{
		}
		if (fxUpgradeAble == this.fxUpgradeAble1)
		{
			this.fxUpgradeAble1 = num;
		}
		else if (fxUpgradeAble == this.fxUpgradeAble2)
		{
			this.fxUpgradeAble2 = num;
		}
		else if (fxUpgradeAble == this.fxUpgradeAble3)
		{
			this.fxUpgradeAble3 = num;
		}
		else if (fxUpgradeAble == this.fxUpgradeAble4)
		{
			this.fxUpgradeAble4 = num;
		}
		else if (fxUpgradeAble == this.fxUpgradeAble5)
		{
			this.fxUpgradeAble5 = num;
		}
		else if (fxUpgradeAble == this.fxUpgradeAble6)
		{
			this.fxUpgradeAble6 = num;
		}
	}

	private void ManagerFxBGElement(int fxID)
	{
	}

	private void ManagerFxidBG(int type)
	{
	}

	private void ManagerElementUpgrade(Transform tr)
	{
	}

	private void ManagerElementUnLock(Transform tr, int element)
	{
	}

	private void OnGetElementUpRes()
	{
		this.ResetUI();
		this.CheckBadge();
	}

	private void ElementUpgrade(int type, int subtype)
	{
		if (this.lastType == type)
		{
			switch (subtype)
			{
			case 1:
				this.ManagerElementUpgrade(this.Element1.get_transform().FindChild("FX"));
				break;
			case 2:
				this.ManagerElementUpgrade(this.Element2.get_transform().FindChild("FX"));
				break;
			case 3:
				this.ManagerElementUpgrade(this.Element3.get_transform().FindChild("FX"));
				break;
			case 4:
				this.ManagerElementUpgrade(this.Element4.get_transform().FindChild("FX"));
				break;
			case 5:
				this.ManagerElementUpgrade(this.Element5.get_transform().FindChild("FX"));
				break;
			case 6:
				this.ManagerElementUpgrade(this.Element6.get_transform().FindChild("FX"));
				break;
			}
		}
	}

	private void RemoveArrowFx(int i)
	{
	}

	private void PlayArrowFX(int i)
	{
		if (this.lastType != 1)
		{
			if (this.lastType != 2)
			{
				if (this.lastType != 3)
				{
					if (this.lastType == 4)
					{
					}
				}
			}
		}
		if (i != 1)
		{
			if (i != 2)
			{
				if (i != 3)
				{
					if (i != 4)
					{
						if (i == 5)
						{
						}
					}
				}
			}
		}
	}

	private void ElementUnLock(int type, int subtype)
	{
		if (this.lastType == type)
		{
			switch (subtype)
			{
			case 1:
				this.ManagerElementUnLock(this.Element1.get_transform().FindChild("FX"), 0);
				break;
			case 2:
				this.ManagerElementUnLock(this.Element2.get_transform().FindChild("FX"), 1);
				break;
			case 3:
				this.ManagerElementUnLock(this.Element3.get_transform().FindChild("FX"), 2);
				break;
			case 4:
				this.ManagerElementUnLock(this.Element4.get_transform().FindChild("FX"), 3);
				break;
			case 5:
				this.ManagerElementUnLock(this.Element4.get_transform().FindChild("FX"), 4);
				break;
			case 6:
				this.ManagerElementUnLock(this.Element4.get_transform().FindChild("FX"), 5);
				break;
			}
		}
	}

	private void RemoveFxs()
	{
	}
}
