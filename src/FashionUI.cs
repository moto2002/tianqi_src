using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FashionUI : UIBase
{
	protected Transform FashionUIPreviewCellSlot;

	protected FashionPreviewCell fashionPreviewCell;

	protected Text FashionUIBGTitleName;

	protected Button FashionUITabClotherBtn;

	protected Image FashionUITabClotherBtnImage;

	protected Text FashionUITabClotherBtnText;

	protected GameObject FashionUITabClotherBtnBadge;

	protected Button FashionUITabWeaponBtn;

	protected Image FashionUITabWeaponBtnImage;

	protected Text FashionUITabWeaponBtnText;

	protected GameObject FashionUITabWeaponBtnBadge;

	protected Button FashionUITabWingBtn;

	protected Image FashionUITabWingBtnImage;

	protected Text FashionUITabWingBtnText;

	protected GameObject FashionUITabWingBtnBadge;

	protected GameObject FashionUIAttrBtn;

	protected GameObject FashionUIAttrDisplay;

	protected Image FashionUIAttrDisplayBG;

	protected Text FashionUIAttrDisplayText;

	protected ListPool listPool;

	protected FashionDataSelete curSeleteType;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.FashionUIPreviewCellSlot = base.FindTransform("FashionUIPreviewCellSlot");
		this.FashionUIBGTitleName = base.FindTransform("FashionUIBGTitleName").GetComponent<Text>();
		this.FashionUITabClotherBtn = base.FindTransform("FashionUITabClotherBtn").GetComponent<Button>();
		this.FashionUITabClotherBtnImage = this.FashionUITabClotherBtn.GetComponent<Image>();
		this.FashionUITabClotherBtnText = base.FindTransform("FashionUITabClotherBtnText").GetComponent<Text>();
		this.FashionUITabClotherBtnBadge = base.FindTransform("FashionUITabClotherBtnBadge").get_gameObject();
		this.FashionUITabWeaponBtn = base.FindTransform("FashionUITabWeaponBtn").GetComponent<Button>();
		this.FashionUITabWeaponBtnImage = this.FashionUITabWeaponBtn.GetComponent<Image>();
		this.FashionUITabWeaponBtnText = base.FindTransform("FashionUITabWeaponBtnText").GetComponent<Text>();
		this.FashionUITabWeaponBtnBadge = base.FindTransform("FashionUITabWeaponBtnBadge").get_gameObject();
		this.FashionUITabWingBtn = base.FindTransform("FashionUITabWingBtn").GetComponent<Button>();
		this.FashionUITabWingBtnImage = this.FashionUITabWingBtn.GetComponent<Image>();
		this.FashionUITabWingBtnText = base.FindTransform("FashionUITabWingBtnText").GetComponent<Text>();
		this.FashionUITabWingBtnBadge = base.FindTransform("FashionUITabWingBtnBadge").get_gameObject();
		this.FashionUIAttrBtn = base.FindTransform("FashionUIAttrBtn").get_gameObject();
		this.FashionUIAttrDisplay = base.FindTransform("FashionUIAttrDisplay").get_gameObject();
		this.FashionUIAttrDisplayBG = base.FindTransform("FashionUIAttrDisplayBG").GetComponent<Image>();
		this.FashionUIAttrDisplayText = base.FindTransform("FashionUIAttrDisplayText").GetComponent<Text>();
		this.listPool = base.FindTransform("FashionUIItemList").GetComponent<ListPool>();
		this.listPool.SetItem("FashionUIItemUnit");
		this.FashionUITabClotherBtn.get_onClick().AddListener(new UnityAction(this.OnClotherBtnClick));
		this.FashionUITabWeaponBtn.get_onClick().AddListener(new UnityAction(this.OnWeaponBtnClick));
		this.FashionUITabWingBtn.get_onClick().AddListener(new UnityAction(this.OnWingBtnClick));
		ButtonCustom expr_20A = base.FindTransform("FashionUICloseBtn").GetComponent<ButtonCustom>();
		expr_20A.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_20A.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnCloseBtnClick));
		ButtonCustom expr_236 = this.FashionUIAttrBtn.GetComponent<ButtonCustom>();
		expr_236.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_236.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnAttrBtnClick));
		ButtonCustom expr_267 = base.FindTransform("FashionUIDescBtn").GetComponent<ButtonCustom>();
		expr_267.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_267.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnDescBtnClick));
		this.FashionUIBGTitleName.set_text(GameDataUtils.GetChineseContent(1005018, false));
		this.FashionUITabClotherBtnText.set_text(GameDataUtils.GetChineseContent(1005001, false));
		this.FashionUITabWeaponBtnText.set_text(GameDataUtils.GetChineseContent(1005002, false));
		this.FashionUITabWingBtnText.set_text(GameDataUtils.GetChineseContent(1005003, false));
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.curSeleteType = FashionDataSelete.None;
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetData(List<string> allFashionDataID, FashionPreviewCell.FashionPreviewCellType cellType, string attrText, bool isForceHideAttr, FashionDataSelete defaultSeleteType)
	{
		this.SetPreview(allFashionDataID, cellType);
		this.ShowClotherBtnBadge(false);
		this.ShowWeaponBtnBadge(false);
		this.ShowWingBtnBadge(false);
		this.ShowWingAttr(attrText, isForceHideAttr);
		if (defaultSeleteType != FashionDataSelete.Weapon)
		{
			if (defaultSeleteType != FashionDataSelete.Wing)
			{
				this.OnClotherBtnClick();
			}
			else
			{
				this.OnWingBtnClick();
			}
		}
		else
		{
			this.OnWeaponBtnClick();
		}
	}

	public void FlushData(List<string> allFashionDataID, FashionPreviewCell.FashionPreviewCellType cellType, string attrText, bool isForceHideAttr)
	{
		this.SetPreview(allFashionDataID, cellType);
		this.ShowClotherBtnBadge(false);
		this.ShowWeaponBtnBadge(false);
		this.ShowWingBtnBadge(false);
		this.ShowWingAttr(attrText, isForceHideAttr);
		switch (this.curSeleteType)
		{
		case FashionDataSelete.Clothes:
			FashionManager.Instance.FlushFashionUI(FashionDataSelete.Clothes);
			break;
		case FashionDataSelete.Weapon:
			FashionManager.Instance.FlushFashionUI(FashionDataSelete.Weapon);
			break;
		case FashionDataSelete.Wing:
			FashionManager.Instance.FlushFashionUI(FashionDataSelete.Wing);
			break;
		default:
			this.OnClotherBtnClick();
			break;
		}
	}

	public void SetPreview(List<string> allFashionDataID, FashionPreviewCell.FashionPreviewCellType cellType)
	{
		if (this.fashionPreviewCell != null && this.fashionPreviewCell.get_gameObject() != null)
		{
			Object.Destroy(this.fashionPreviewCell.get_gameObject());
		}
		this.fashionPreviewCell = FashionPreviewManager.Instance.GetPreview(this.FashionUIPreviewCellSlot);
		this.fashionPreviewCell.SetData(allFashionDataID, string.Empty, cellType, false);
	}

	public void SetFashion(FashionDataSelete seleteType, string theFashionDataID, List<FashionData> seletedList, List<string> newFashionTipdDataID)
	{
		this.fashionPreviewCell.SetDetailData(theFashionDataID, FashionPreviewCell.FashionPreviewCellType.Wardrobe);
		this.listPool.Create(seletedList.get_Count(), delegate(int index)
		{
			if (index < seletedList.get_Count() && index < this.listPool.Items.get_Count())
			{
				FashionUIItemUnit component = this.listPool.Items.get_Item(index).GetComponent<FashionUIItemUnit>();
				component.SetData(seletedList.get_Item(index), newFashionTipdDataID.Contains(seletedList.get_Item(index).dataID));
			}
		});
	}

	protected void ShowClotherBtnBadge(bool isShow)
	{
		this.FashionUITabClotherBtnBadge.SetActive(isShow);
	}

	protected void ShowWeaponBtnBadge(bool isShow)
	{
		this.FashionUITabWeaponBtnBadge.SetActive(isShow);
	}

	protected void ShowWingBtnBadge(bool isShow)
	{
		this.FashionUITabWingBtnBadge.SetActive(isShow);
	}

	protected void ShowWingAttr(string text, bool isForceHideAttr)
	{
		bool flag = !string.IsNullOrEmpty(text);
		this.FashionUIAttrBtn.SetActive(flag);
		if (isForceHideAttr || (this.FashionUIAttrDisplay.get_activeSelf() && !flag))
		{
			this.FashionUIAttrDisplay.SetActive(false);
		}
		if (flag)
		{
			this.FashionUIAttrDisplayText.set_text(text);
			this.FashionUIAttrDisplayText.get_rectTransform().set_sizeDelta(new Vector2(this.FashionUIAttrDisplayText.get_rectTransform().get_sizeDelta().x, this.FashionUIAttrDisplayText.get_preferredHeight()));
			this.FashionUIAttrDisplayBG.get_rectTransform().set_sizeDelta(new Vector2(this.FashionUIAttrDisplayBG.get_rectTransform().get_sizeDelta().x, 23f + this.FashionUIAttrDisplayText.get_preferredHeight()));
		}
	}

	protected void OnCloseBtnClick(GameObject go)
	{
		this.Show(false);
	}

	protected void OnClotherBtnClick()
	{
		if (this.curSeleteType == FashionDataSelete.Clothes)
		{
			return;
		}
		this.curSeleteType = FashionDataSelete.Clothes;
		ResourceManager.SetSprite(this.FashionUITabClotherBtnImage, ResourceManager.GetIconSprite("y_fenye1"));
		ResourceManager.SetSprite(this.FashionUITabWeaponBtnImage, ResourceManager.GetIconSprite("y_fenye2"));
		ResourceManager.SetSprite(this.FashionUITabWingBtnImage, ResourceManager.GetIconSprite("y_fenye2"));
		FashionManager.Instance.FlushFashionUI(FashionDataSelete.Clothes);
	}

	protected void OnWeaponBtnClick()
	{
		if (this.curSeleteType == FashionDataSelete.Weapon)
		{
			return;
		}
		this.curSeleteType = FashionDataSelete.Weapon;
		ResourceManager.SetSprite(this.FashionUITabClotherBtnImage, ResourceManager.GetIconSprite("y_fenye2"));
		ResourceManager.SetSprite(this.FashionUITabWeaponBtnImage, ResourceManager.GetIconSprite("y_fenye1"));
		ResourceManager.SetSprite(this.FashionUITabWingBtnImage, ResourceManager.GetIconSprite("y_fenye2"));
		FashionManager.Instance.FlushFashionUI(FashionDataSelete.Weapon);
	}

	protected void OnWingBtnClick()
	{
		if (this.curSeleteType == FashionDataSelete.Wing)
		{
			return;
		}
		this.curSeleteType = FashionDataSelete.Wing;
		ResourceManager.SetSprite(this.FashionUITabClotherBtnImage, ResourceManager.GetIconSprite("y_fenye2"));
		ResourceManager.SetSprite(this.FashionUITabWeaponBtnImage, ResourceManager.GetIconSprite("y_fenye2"));
		ResourceManager.SetSprite(this.FashionUITabWingBtnImage, ResourceManager.GetIconSprite("y_fenye1"));
		FashionManager.Instance.FlushFashionUI(FashionDataSelete.Wing);
	}

	protected void OnAttrBtnClick(GameObject go)
	{
		this.FashionUIAttrDisplay.SetActive(!this.FashionUIAttrDisplay.get_activeSelf());
	}

	protected void OnDescBtnClick(GameObject go)
	{
		FashionTipUI fashionTipUI = UIManagerControl.Instance.OpenUI("FashionTipUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as FashionTipUI;
		fashionTipUI.SetData(GameDataUtils.GetChineseContent(1005020, false), GameDataUtils.GetChineseContent(1005019, false));
	}

	private void OnApplicationPause(bool isPause)
	{
		if (this.fashionPreviewCell)
		{
			this.fashionPreviewCell.DoOnApplicationPause();
		}
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeInHierarchy() && this.fashionPreviewCell)
			{
				this.fashionPreviewCell.DoOnApplicationPause();
			}
		});
	}
}
