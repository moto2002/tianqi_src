using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class ItemTipUIView : UIBase
{
	private const float Spacing = 30f;

	private const float HEIGHT_OTHER = 150f;

	private const float HEIGHT_HALF = 240f;

	public static ItemTipUIView Instance;

	private RectTransform m_rtBackground0;

	private Text m_lblDesc;

	private Text m_lblAttrDesc;

	private RectTransform m_rtAttrs;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		ItemTipUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	private void Update()
	{
		ItemTipUIView.Instance.Rearrange();
	}

	protected override void InitUI()
	{
		this.m_rtBackground0 = (base.FindTransform("Background0") as RectTransform);
		this.m_lblDesc = base.FindTransform("Desc").GetComponent<Text>();
		this.m_lblAttrDesc = base.FindTransform("AttrDesc").GetComponent<Text>();
		this.m_rtAttrs = (base.FindTransform("Attrs") as RectTransform);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("ItemName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemName";
		textBinder = base.FindTransform("ItemLv").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemLv";
		textBinder = base.FindTransform("ItemNum").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemNum";
		textBinder = base.FindTransform("ItemProfession").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemProfession";
		textBinder = base.FindTransform("Desc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "Desc";
		textBinder = base.FindTransform("AttrDesc").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "AttrDesc";
		textBinder = base.FindTransform("ItemStepText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemStepText";
		ImageBinder imageBinder = base.FindTransform("ItemFrame").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemFrame";
		imageBinder = base.FindTransform("ItemIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ItemIcon";
		ListBinder listBinder = base.FindTransform("Attrs").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "Item2Text";
		listBinder.SourceBinding.MemberName = "TextItems";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BtnUseVisibility";
		visibilityBinder.Target = base.FindTransform("BtnUse").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ItemStepVisibility";
		visibilityBinder.Target = base.FindTransform("ItemStep").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentAttrVisibility";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentImage1";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").FindChild("Image1").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentImage2";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").FindChild("Image2").get_gameObject();
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ExcellentImage3";
		visibilityBinder.Target = base.FindTransform("ItemExcellentAttrIconList").FindChild("Image3").get_gameObject();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnUse").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnUseUp";
	}

	public void SetLevelPosition()
	{
		Text component = base.FindTransform("ItemName").GetComponent<Text>();
		RectTransform rectTransform = base.FindTransform("ItemLv") as RectTransform;
		rectTransform.set_anchoredPosition(new Vector2(component.get_preferredWidth() + (component.get_transform() as RectTransform).get_anchoredPosition().x + 20f - 75f, rectTransform.get_anchoredPosition().y));
	}

	public void Rearrange()
	{
		float num = 0f;
		float preferredHeight = this.m_rtAttrs.GetComponent<GridLayoutGroup>().get_preferredHeight();
		if (preferredHeight > 0f)
		{
			this.m_rtAttrs.GetComponent<LayoutElement>().set_minHeight(preferredHeight);
			this.m_rtAttrs.GetComponent<LayoutElement>().set_preferredHeight(preferredHeight);
			num += preferredHeight;
		}
		else
		{
			this.m_rtAttrs.GetComponent<LayoutElement>().set_minHeight(0f);
			this.m_rtAttrs.GetComponent<LayoutElement>().set_preferredHeight(0f);
		}
		float num2 = this.m_lblDesc.get_preferredHeight();
		if (!string.IsNullOrEmpty(this.m_lblDesc.get_text()))
		{
			num2 += 30f;
			this.m_lblDesc.GetComponent<LayoutElement>().set_minHeight(num2);
			this.m_lblDesc.GetComponent<LayoutElement>().set_preferredHeight(num2);
			num += num2;
		}
		else
		{
			this.m_lblDesc.GetComponent<LayoutElement>().set_minHeight(0f);
			this.m_lblDesc.GetComponent<LayoutElement>().set_preferredHeight(0f);
		}
		float num3 = this.m_lblAttrDesc.get_preferredHeight();
		if (!string.IsNullOrEmpty(this.m_lblAttrDesc.get_text()))
		{
			num3 += 30f;
			this.m_lblAttrDesc.GetComponent<LayoutElement>().set_minHeight(num3);
			this.m_lblAttrDesc.GetComponent<LayoutElement>().set_preferredHeight(num3);
			num += num3;
		}
		else
		{
			this.m_lblAttrDesc.GetComponent<LayoutElement>().set_minHeight(0f);
			this.m_lblAttrDesc.GetComponent<LayoutElement>().set_preferredHeight(0f);
		}
		num += 10f;
		this.SetBackground(num + 150f);
	}

	private void SetBackground(float height)
	{
		this.m_rtBackground0.set_sizeDelta(new Vector2(this.m_rtBackground0.get_sizeDelta().x, height));
		(base.get_transform() as RectTransform).set_anchoredPosition(new Vector2(0f, -(480f - height) / 2f));
	}
}
