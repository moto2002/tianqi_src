using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class BuyUIView : UIBase
{
	private const int HEIGHT_BUY_NUMBER_ADJUST = 80;

	private const int HEIGHT_DESC_MIN = 30;

	private const int HEIGHT_ATTR_MORE = 20;

	public static BuyUIView Instance;

	private LayoutElement m_leAttrTxt;

	private LayoutElement m_leItemDesc;

	private Text m_lblItemDesc;

	private RectTransform m_RootOffset;

	private RectTransform m_background0;

	private RectTransform m_background1;

	private RectTransform m_costRegion;

	private RectTransform m_buttons;

	private RectTransform m_InputFieldRegion;

	private int m_attrs_count;

	private bool m_buy_number_adjust_on;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		BuyUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Update()
	{
		this.SetAutoLayOut(this.m_attrs_count, this.m_buy_number_adjust_on);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_leAttrTxt = base.FindTransform("AttrTxt").GetComponent<LayoutElement>();
		this.m_leItemDesc = base.FindTransform("ItemDesc").GetComponent<LayoutElement>();
		this.m_lblItemDesc = base.FindTransform("ItemDesc").GetComponent<Text>();
		this.m_RootOffset = (base.FindTransform("RootOffset") as RectTransform);
		this.m_background0 = (base.FindTransform("Background0") as RectTransform);
		this.m_background1 = (base.FindTransform("Background1") as RectTransform);
		this.m_costRegion = (base.FindTransform("CostRegion") as RectTransform);
		this.m_buttons = (base.FindTransform("Buttons") as RectTransform);
		this.m_InputFieldRegion = (base.FindTransform("InputFieldRegion") as RectTransform);
		base.FindTransform("Placeholder").GetComponent<Text>().set_text(string.Empty);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("attackTxtNum").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "AttackVisible";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("defendTxtNum").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "DefenceVisible";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("sanbiTxtNum").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "DodgeVisible";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("AttrTxt").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "AttrTxtVisible";
		TextBinder textBinder = base.FindTransform("attackTxtNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "AttackTxtNum";
		textBinder = base.FindTransform("defendTxtNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "DefenceTxtNum";
		textBinder = base.FindTransform("sanbiTxtNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "DodgeTxtNum";
		textBinder = base.FindTransform("ItemName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemName";
		textBinder = base.FindTransform("ItemDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemDesc";
		textBinder = base.FindTransform("ItemOwn").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemOwn";
		textBinder = base.FindTransform("ItemNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemNumName";
		textBinder = base.FindTransform("ItemProfession").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemProfession";
		textBinder = base.FindTransform("ItemProfessionName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemProfessionName";
		textBinder = base.FindTransform("BuyCount").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BuyCount";
		textBinder = base.FindTransform("CostNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "CostNum";
		textBinder = base.FindTransform("ItemFighting").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemFighting";
		textBinder = base.FindTransform("ItemFightingName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemFightingName";
		ImageBinder imageBinder = base.FindTransform("ItemFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemFrame";
		imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemIcon";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("CostIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "CostIcon";
		imageBinder.SetNativeSize = true;
		imageBinder.RefreshLayout = true;
		InputFieldBinder inputFieldBinder = base.FindTransform("InputField").get_gameObject().AddComponent<InputFieldBinder>();
		inputFieldBinder.BindingProxy = base.get_gameObject();
		inputFieldBinder.TextBinding.MemberName = "Input";
		inputFieldBinder.SetCharacterLimit(4);
		inputFieldBinder.SetContentType(2);
		textBinder = base.FindTransform("BtnOKName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnOKName";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnAdd").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnAddClick";
		buttonBinder.EnabledBinding.MemberName = "BtnAddEnable";
		buttonBinder = base.FindTransform("BtnMinuse").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnMinuseClick";
		buttonBinder.EnabledBinding.MemberName = "BtnMinuseEnable";
		buttonBinder = base.FindTransform("BtnOK").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnOKClick";
		buttonBinder.EnabledBinding.MemberName = "BtnOKEnable";
	}

	public void SetAutoLayOut(int attrs_count, bool buy_number_adjust_on = false)
	{
		this.m_attrs_count = attrs_count;
		this.m_buy_number_adjust_on = buy_number_adjust_on;
		int num = attrs_count * 30;
		int num2 = num;
		int num3 = num;
		if (attrs_count > 0)
		{
			num3 += 20;
			num2 += 20;
		}
		if (buy_number_adjust_on)
		{
			num2 += 80;
		}
		float num4 = Mathf.Clamp(this.m_lblItemDesc.get_preferredHeight(), 30f, 250f);
		float num5 = (float)(420 + num2) + num4 - 60f;
		this.m_RootOffset.set_anchoredPosition(new Vector2(0f, (num5 - 500f) * 0.5f));
		this.m_leAttrTxt.set_minHeight((float)num);
		this.m_leAttrTxt.set_preferredHeight((float)num);
		this.m_leItemDesc.set_minHeight(num4);
		this.m_leItemDesc.set_preferredHeight(num4);
		if (buy_number_adjust_on)
		{
			this.m_InputFieldRegion.get_gameObject().SetActive(true);
			this.m_InputFieldRegion.set_anchoredPosition(new Vector2(this.m_InputFieldRegion.get_sizeDelta().x, -23f - num4 - (float)num3));
		}
		else
		{
			this.m_InputFieldRegion.get_gameObject().SetActive(false);
		}
		this.m_background0.set_sizeDelta(new Vector2(this.m_background0.get_sizeDelta().x, 360f + num4 + (float)num2));
		this.m_background1.set_sizeDelta(new Vector2(this.m_background1.get_sizeDelta().x, 140f + num4 + (float)num3));
		this.m_costRegion.set_localPosition(new Vector3(this.m_costRegion.get_localPosition().x, 40f - num4 - (float)num2, this.m_costRegion.get_localPosition().z));
		this.m_buttons.set_localPosition(new Vector3(this.m_buttons.get_localPosition().x, -15f - num4 - (float)num2, this.m_buttons.get_localPosition().z));
	}
}
