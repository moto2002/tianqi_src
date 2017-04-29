using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSingleUI : UIBase
{
	private const int attrMaxCount = 2;

	private ButtonCustom imgIcon;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsLastSibling();
		this.Init();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		GemComposeUI gemComposeUI = UIManagerControl.Instance.GetUIIfExist("GemComposeUI") as GemComposeUI;
		if (gemComposeUI != null)
		{
			gemComposeUI.Show(false);
		}
	}

	private void OnclickBtnUndress(GameObject sender)
	{
		Debug.Log("OnclickBtnUndress=" + sender);
		GemManager.Instance.SendGemSysTakeoffReq(GemUI.instance.equipCurr, GemUI.instance.slotCurr);
	}

	private void SetButtonEvent(int typeId)
	{
		base.FindTransform("btnUndress").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnclickBtnUndress);
	}

	private void Init()
	{
		this.imgIcon = base.FindTransform("imgGrid").GetComponent<ButtonCustom>();
		this.SetOneGem(GemUI.instance.typeIdCurr);
		this.SetButtonEvent(GemUI.instance.typeIdCurr);
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

	private void SetGemIcon(int typeId)
	{
		Items items = DataReader<Items>.Get(typeId);
		if (items == null)
		{
			return;
		}
		Image component = this.imgIcon.GetComponent<Image>();
		ResourceManager.SetSprite(component, GameDataUtils.GetItemFrameByColor(items.color));
		Image component2 = this.imgIcon.get_transform().FindChild("imgItem").GetComponent<Image>();
		ResourceManager.SetSprite(component2, GameDataUtils.GetIcon(items.icon));
		Text component3 = this.imgIcon.get_transform().FindChild("texName").GetComponent<Text>();
		component3.set_text(GameDataUtils.GetItemName(items, true));
		Dictionary<string, Color> textColorByQuality = GameDataUtils.GetTextColorByQuality(items.color);
		component3.set_color(textColorByQuality.get_Item("TextColor"));
		this.imgIcon.get_transform().FindChild("texName").GetComponent<Outline>().set_effectColor(textColorByQuality.get_Item("TextOutlineColor"));
		Text component4 = this.imgIcon.get_transform().FindChild("texLv").GetComponent<Text>();
		component4.set_text(string.Empty);
		if (!GemGlobal.IsGemEnoughLv(typeId))
		{
			int roleLvRequire = GemGlobal.GetRoleLvRequire(typeId);
			component4.set_text(string.Format(GameDataUtils.GetChineseContent(509011, false), roleLvRequire));
		}
		base.FindTransform("texDesc").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(items.describeId1, false));
	}

	public void SetOneGem(int typeId)
	{
		this.SetGemIcon(typeId);
		this.SetGemAttrs(typeId);
	}
}
