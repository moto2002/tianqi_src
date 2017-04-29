using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : UIBase
{
	public static BackpackUI Instance;

	[HideInInspector]
	public List<Text> btnTexts = new List<Text>();

	private GameObject m_goBtnComposeOne;

	private GameObject m_goBtnComposeAll;

	private GameObject m_goBtnUse;

	private GameObject m_goBtnUseBatch;

	private GameObject m_goBtnSell;

	private GameObject m_goBtnSellBatch;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		BackpackUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.btnTexts.Add(base.FindTransform("gText").GetComponent<Text>());
		this.btnTexts.Add(base.FindTransform("eqText").GetComponent<Text>());
		this.btnTexts.Add(base.FindTransform("otherText").GetComponent<Text>());
		this.btnTexts.Add(base.FindTransform("fragText").GetComponent<Text>());
		this.btnTexts.Add(base.FindTransform("EnchantmentText").GetComponent<Text>());
		this.m_goBtnComposeOne = base.FindTransform("BtnComposeOne").get_gameObject();
		this.m_goBtnComposeAll = base.FindTransform("BtnComposeAll").get_gameObject();
		this.m_goBtnUse = base.FindTransform("BtnUse").get_gameObject();
		this.m_goBtnUseBatch = base.FindTransform("BtnUseBatch").get_gameObject();
		this.m_goBtnSell = base.FindTransform("BtnSell").get_gameObject();
		this.m_goBtnSellBatch = base.FindTransform("BtnSellBatch").get_gameObject();
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110004), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			BackpackUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("Count").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Count";
		textBinder = base.FindTransform("ItemProfession").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemProfession";
		textBinder = base.FindTransform("DecsText1").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Decs";
		textBinder = base.FindTransform("DecsText2").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Decs2";
		textBinder = base.FindTransform("ItemName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Name";
		textBinder = base.FindTransform("Tips").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "TipsName";
		textBinder = base.FindTransform("ComposeGemTip").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ComposeGemTip";
		textBinder = base.FindTransform("ItemStepText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemStep";
		textBinder = base.FindTransform("BtnSellName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnSellName";
		textBinder = base.FindTransform("BtnSellBatchName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnSellBatchName";
		ImageBinder imageBinder = base.FindTransform("ItemFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemFrame";
		imageBinder = base.FindTransform("Icon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "BgIcon";
		imageBinder = base.FindTransform("Prop").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "PropIcon";
		imageBinder = base.FindTransform("Equipment").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "EquipmentIcon";
		imageBinder = base.FindTransform("Fragment").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "FragmentIcon";
		imageBinder = base.FindTransform("Rune").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "RuneIcon";
		imageBinder = base.FindTransform("Enchantment").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "EnchantmentIcon";
		ListBinder component = base.FindTransform("Contair").get_gameObject().GetComponent<ListBinder>();
		component.PrefabName = "BackpackItem";
		component.BindingProxy = base.get_gameObject();
		component.LoadNumberFrame = 5;
		component.SourceBinding.MemberName = "BackpackItems";
		component.ITEM_NAME = "_Item2Backpack";
		component = base.FindTransform("attrs").get_gameObject().GetComponent<ListBinder>();
		component.BindingProxy = base.get_gameObject();
		component.SourceBinding.MemberName = "TextItems";
		component = base.FindTransform("suitAttrs").get_gameObject().GetComponent<ListBinder>();
		component.BindingProxy = base.get_gameObject();
		component.SourceBinding.MemberName = "SuitTextItems";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "AttrVisibility";
		visibilityBinder.Target = base.FindTransform("attrs").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SuitNameVisibility";
		visibilityBinder.Target = base.FindTransform("suitName").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "SuitVisibility";
		visibilityBinder.Target = base.FindTransform("suitAttrs").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "PetFlagVisibility";
		visibilityBinder.Target = base.FindTransform("petFlag").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "TipsVisibility";
		visibilityBinder.Target = base.FindTransform("DetailTips").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemStepVisibility";
		visibilityBinder.Target = base.FindTransform("ItemStep").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnUseRedPointVisibility";
		visibilityBinder.Target = base.FindTransform("BtnUse").FindChild("RedPoint").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemExcellentAttrVisibility";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemExcellentImage1";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").FindChild("Image1").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemExcellentImage2";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").FindChild("Image2").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemExcellentImage3";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").FindChild("Image3").get_gameObject();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnUse").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnUse";
		buttonBinder = base.FindTransform("BtnUseBatch").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnUseBatch";
		buttonBinder = base.FindTransform("BtnComposeOne").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnComposeOne";
		buttonBinder.EnabledBinding.MemberName = "IsBtnComposeOneOn";
		buttonBinder = base.FindTransform("BtnComposeAll").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnComposeAll";
		buttonBinder.EnabledBinding.MemberName = "IsBtnComposeAllOn";
		buttonBinder = base.FindTransform("DecomposeBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnDecomposeEquipment";
		buttonBinder = base.FindTransform("AutoDecomposeBtn").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnAutoDecomposeEquipment";
		buttonBinder = base.FindTransform("Equipment").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnEquipment";
		buttonBinder = base.FindTransform("Prop").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnProp";
		buttonBinder = base.FindTransform("Fragment").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnFragment";
		buttonBinder = base.FindTransform("Rune").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnRune";
		buttonBinder = base.FindTransform("Enchantment").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnEnchantment";
		buttonBinder = base.FindTransform("BtnSell").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnSell";
		buttonBinder = base.FindTransform("BtnSellBatch").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnSellBatch";
	}

	public void ShowButtonUse(bool isShow, bool isBatchOn)
	{
		this.m_goBtnUse.SetActive(isShow);
		this.m_goBtnUseBatch.SetActive(isBatchOn);
		if (isBatchOn)
		{
			(this.m_goBtnUse.get_transform() as RectTransform).set_anchoredPosition(new Vector2(110f, -252f));
		}
		else
		{
			(this.m_goBtnUse.get_transform() as RectTransform).set_anchoredPosition(new Vector2(0f, -252f));
		}
	}

	public void ShowButtonCompose(bool isShow)
	{
		this.m_goBtnComposeOne.SetActive(isShow);
		this.m_goBtnComposeAll.SetActive(isShow);
	}

	public void ShowButtonSell(bool isShowSell, bool isShowUse = false, bool isBatchOn = false)
	{
		this.m_goBtnUse.SetActive(isShowUse);
		this.m_goBtnSell.SetActive(isShowSell);
		this.m_goBtnSellBatch.SetActive(isBatchOn && !isShowUse && isShowSell);
		if (isShowUse && isShowSell && !isBatchOn)
		{
			(this.m_goBtnUse.get_transform() as RectTransform).set_anchoredPosition(new Vector2(-110f, -252f));
			(this.m_goBtnSell.get_transform() as RectTransform).set_anchoredPosition(new Vector2(110f, -252f));
		}
		else if (isShowUse && !isBatchOn && !isShowSell)
		{
			(this.m_goBtnUse.get_transform() as RectTransform).set_anchoredPosition(new Vector2(0f, -252f));
		}
		else if (isShowSell && !isShowUse && !isBatchOn)
		{
			(this.m_goBtnSell.get_transform() as RectTransform).set_anchoredPosition(new Vector2(0f, -252f));
		}
		else if (isShowSell && isBatchOn)
		{
			(this.m_goBtnSell.get_transform() as RectTransform).set_anchoredPosition(new Vector2(110f, -252f));
			(this.m_goBtnSellBatch.get_transform() as RectTransform).set_anchoredPosition(new Vector2(-110f, -252f));
		}
	}
}
