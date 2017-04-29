using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyFashionUI : UIBase
{
	protected Transform BuyFashionUIPreviewSlot;

	protected FashionPreviewCell fashionPreviewCell;

	protected Text BuyFashionUIDetailTitleName;

	protected Text BuyFashionUIDetailItemName;

	protected Image BuyFashionUIDetailItemFrame;

	protected Image BuyFashionUIDetailItemIcon;

	protected Text BuyFashionUIDetailCareer;

	protected Text BuyFashionUIDetailCareerName;

	protected Text BuyFashionUIDetailDescLineTitle;

	protected Text BuyFashionUIDetailDescText;

	protected Toggle BuyFashionUIDetailBuyGroupItem0;

	protected GameObject BuyFashionUIDetailBuyGroupItem0Cm;

	protected Toggle BuyFashionUIDetailBuyGroupItem1;

	protected GameObject BuyFashionUIDetailBuyGroupItem1Cm;

	protected Toggle BuyFashionUIDetailBuyGroupItem2;

	protected GameObject BuyFashionUIDetailBuyGroupItem2Cm;

	protected Text BuyFashionUIDetailCostNum;

	protected ButtonCustom BuyFashionUIDetailBuyBtn;

	protected Image BuyFashionUIDetailBuyBtnImage;

	protected Text BuyFashionUIDetailBuyBtnImageName;

	protected int commodityDataID;

	protected string fashionDataID;

	protected int curBuyRank;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BuyFashionUIPreviewSlot = base.FindTransform("BuyFashionUIPreviewSlot");
		this.BuyFashionUIDetailTitleName = base.FindTransform("BuyFashionUIDetailTitleName").GetComponent<Text>();
		this.BuyFashionUIDetailItemName = base.FindTransform("BuyFashionUIDetailItemName").GetComponent<Text>();
		this.BuyFashionUIDetailItemFrame = base.FindTransform("BuyFashionUIDetailItemFrame").GetComponent<Image>();
		this.BuyFashionUIDetailItemIcon = base.FindTransform("BuyFashionUIDetailItemIcon").GetComponent<Image>();
		this.BuyFashionUIDetailCareer = base.FindTransform("BuyFashionUIDetailCareer").GetComponent<Text>();
		this.BuyFashionUIDetailCareerName = base.FindTransform("BuyFashionUIDetailCareerName").GetComponent<Text>();
		this.BuyFashionUIDetailDescLineTitle = base.FindTransform("BuyFashionUIDetailDescLineTitle").GetComponent<Text>();
		this.BuyFashionUIDetailDescText = base.FindTransform("BuyFashionUIDetailDescText").GetComponent<Text>();
		this.BuyFashionUIDetailBuyGroupItem0 = base.FindTransform("BuyFashionUIDetailBuyGroupItem0").GetComponent<Toggle>();
		this.BuyFashionUIDetailBuyGroupItem0Cm = base.FindTransform("BuyFashionUIDetailBuyGroupItem0Cm").get_gameObject();
		this.BuyFashionUIDetailBuyGroupItem0.onValueChanged.AddListener(new UnityAction<bool>(this.OnBuyFashionUIDetailBuyGroupItem0Click));
		this.BuyFashionUIDetailBuyGroupItem1 = base.FindTransform("BuyFashionUIDetailBuyGroupItem1").GetComponent<Toggle>();
		this.BuyFashionUIDetailBuyGroupItem1Cm = base.FindTransform("BuyFashionUIDetailBuyGroupItem1Cm").get_gameObject();
		this.BuyFashionUIDetailBuyGroupItem1.onValueChanged.AddListener(new UnityAction<bool>(this.OnBuyFashionUIDetailBuyGroupItem1Click));
		this.BuyFashionUIDetailBuyGroupItem2 = base.FindTransform("BuyFashionUIDetailBuyGroupItem2").GetComponent<Toggle>();
		this.BuyFashionUIDetailBuyGroupItem2Cm = base.FindTransform("BuyFashionUIDetailBuyGroupItem2Cm").get_gameObject();
		this.BuyFashionUIDetailBuyGroupItem2.onValueChanged.AddListener(new UnityAction<bool>(this.OnBuyFashionUIDetailBuyGroupItem2Click));
		this.BuyFashionUIDetailCostNum = base.FindTransform("BuyFashionUIDetailCostNum").GetComponent<Text>();
		this.BuyFashionUIDetailBuyBtn = base.FindTransform("BuyFashionUIDetailBuyBtn").GetComponent<ButtonCustom>();
		ButtonCustom expr_1D3 = this.BuyFashionUIDetailBuyBtn;
		expr_1D3.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_1D3.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnBuyBtnClick));
		this.BuyFashionUIDetailBuyBtnImage = base.FindTransform("BuyFashionUIDetailBuyBtnImage").GetComponent<Image>();
		this.BuyFashionUIDetailBuyBtnImageName = base.FindTransform("BuyFashionUIDetailBuyBtnImageName").GetComponent<Text>();
		ButtonCustom expr_230 = base.FindTransform("BuyFashionUIDetailCloseBtn").GetComponent<ButtonCustom>();
		expr_230.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_230.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnCloseBtnClick));
		this.BuyFashionUIDetailTitleName.set_text(GameDataUtils.GetChineseContent(1005012, false));
		this.BuyFashionUIDetailCareer.set_text(GameDataUtils.GetChineseContent(1005010, false));
		this.BuyFashionUIDetailDescLineTitle.set_text(GameDataUtils.GetChineseContent(1005013, false));
		this.BuyFashionUIDetailBuyBtnImageName.set_text(GameDataUtils.GetChineseContent(1005015, false));
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

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetData(List<string> allFashionDataID, string theFashionDataID, int theCommodityDataID, bool isEnableBuy)
	{
		this.commodityDataID = theCommodityDataID;
		this.fashionDataID = theFashionDataID;
		this.SetPreview(allFashionDataID, theFashionDataID, FashionPreviewCell.FashionPreviewCellType.None);
		if (DataReader<ShiZhuangXiTong>.Contains(theFashionDataID))
		{
			ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(theFashionDataID);
			if (DataReader<Items>.Contains(shiZhuangXiTong.itemsID))
			{
				Items items = DataReader<Items>.Get(shiZhuangXiTong.itemsID);
				this.SetItemName(GameDataUtils.GetChineseContent(items.name, false));
				this.SetItemFrame(shiZhuangXiTong.itemsID);
				this.SetItemIcon(items.icon);
				this.SetCareerName(UIUtils.GetRoleName(items.career));
				this.SetDesc(GameDataUtils.GetChineseContent(items.describeId1, false));
			}
			else
			{
				this.SetItemName(string.Empty);
				this.SetItemFrame(0);
				this.SetItemIcon(0);
				this.SetCareerName(string.Empty);
				this.SetDesc(string.Empty);
			}
		}
		else
		{
			this.SetItemName(string.Empty);
			this.SetItemFrame(0);
			this.SetItemIcon(0);
			this.SetCareerName(string.Empty);
			this.SetDesc(string.Empty);
		}
		this.SetDefaultClick();
		this.SetButtonEnable(isEnableBuy);
	}

	protected void SetDefaultClick()
	{
		this.BuyFashionUIDetailBuyGroupItem1.set_isOn(true);
		this.BuyFashionUIDetailBuyGroupItem1Cm.SetActive(true);
		this.OnBuyFashionUIDetailBuyGroupItem1Click(true);
	}

	protected void SetPreview(List<string> allFashionDataID, string theFashionDataID, FashionPreviewCell.FashionPreviewCellType type)
	{
		if (this.fashionPreviewCell != null && this.fashionPreviewCell.get_gameObject() != null)
		{
			Object.Destroy(this.fashionPreviewCell.get_gameObject());
		}
		this.fashionPreviewCell = FashionPreviewManager.Instance.GetPreview(this.BuyFashionUIPreviewSlot);
		this.fashionPreviewCell.SetData(allFashionDataID, theFashionDataID, type, true);
	}

	protected void SetTitleName(string name)
	{
		this.BuyFashionUIDetailTitleName.set_text(name);
	}

	protected void SetItemName(string name)
	{
		this.BuyFashionUIDetailItemName.set_text(name);
	}

	protected void SetItemFrame(int id)
	{
		ResourceManager.SetSprite(this.BuyFashionUIDetailItemFrame, GameDataUtils.GetItemFrame(id));
	}

	protected void SetItemIcon(int icon)
	{
		ResourceManager.SetSprite(this.BuyFashionUIDetailItemIcon, GameDataUtils.GetIcon(icon));
	}

	protected void SetCareerPrefix(string text)
	{
		this.BuyFashionUIDetailCareer.set_text(text);
	}

	protected void SetCareerName(string name)
	{
		this.BuyFashionUIDetailCareerName.set_text(name);
	}

	protected void SetDescLineTitle(string text)
	{
		this.BuyFashionUIDetailDescLineTitle.set_text(text);
	}

	protected void SetDesc(string text)
	{
		this.BuyFashionUIDetailDescText.set_text(text);
	}

	protected void SetCost(string text)
	{
		this.BuyFashionUIDetailCostNum.set_text(text);
	}

	protected void SetButtonEnable(bool isEnable)
	{
		this.BuyFashionUIDetailBuyBtn.set_enabled(isEnable);
		ImageColorMgr.SetImageColor(this.BuyFashionUIDetailBuyBtnImage, !isEnable);
	}

	protected void SetBuyBtnText(string text)
	{
		this.BuyFashionUIDetailBuyBtnImageName.set_text(text);
	}

	protected void OnBuyFashionUIDetailBuyGroupItem0Click(bool isUp)
	{
		if (!isUp)
		{
			return;
		}
		this.curBuyRank = 0;
		this.SetCost(this.curBuyRank);
	}

	protected void OnBuyFashionUIDetailBuyGroupItem1Click(bool isUp)
	{
		if (!isUp)
		{
			return;
		}
		this.curBuyRank = 0;
		this.SetCost(this.curBuyRank);
	}

	protected void OnBuyFashionUIDetailBuyGroupItem2Click(bool isUp)
	{
		if (!isUp)
		{
			return;
		}
		this.curBuyRank = 1;
		this.SetCost(this.curBuyRank);
	}

	protected void SetCost(int curBuyRank)
	{
		StoreGoodsInfo commodityInfo = XMarketManager.Instance.GetCommodityInfo(this.commodityDataID);
		this.SetCost(string.Format(GameDataUtils.GetChineseContent(1005014, false), (commodityInfo.unitPrice.get_Count() <= curBuyRank) ? 2147483647 : commodityInfo.unitPrice.get_Item(curBuyRank)));
	}

	protected void OnBuyBtnClick(GameObject go)
	{
		this.Show(false);
		FashionManager.Instance.Buy(this.commodityDataID, this.curBuyRank);
	}

	protected void OnCloseBtnClick(GameObject go)
	{
		this.Show(false);
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
