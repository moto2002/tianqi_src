using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityTossDiscountItem : MonoBehaviour
{
	private Image imgIcon;

	public Text textCount;

	private RankDataUnite data;

	private Transform icon;

	private Text productName;

	private Image payIcon;

	private int currentShangPinId;

	private Image zhekouBg;

	private Text zhekouValue;

	private Text productSum;

	private int selectType;

	private DiscountItemsInfo currentDiscountItemsInfo;

	private void Awake()
	{
		this.icon = base.get_transform().FindChild("ItemGoods");
		this.imgIcon = this.icon.FindChild("GoodsIcon").GetComponent<Image>();
		this.textCount = base.get_transform().FindChild("Count").GetComponent<Text>();
		this.productName = base.get_transform().FindChild("ProductName").GetComponent<Text>();
		this.payIcon = base.get_transform().FindChild("Icon").GetComponent<Image>();
		this.zhekouBg = base.get_transform().FindChild("zhekouBg").GetComponent<Image>();
		this.zhekouValue = base.get_transform().FindChild("zhekouBg").FindChild("zhekouValue").GetComponent<Text>();
		this.productSum = base.get_transform().FindChild("ItemGoods").FindChild("STextNum").GetComponent<Text>();
		base.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPanel);
		base.get_transform().FindChild("ButtonChangePro").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangePro);
	}

	private void OnEnable()
	{
	}

	private void OnClickGet(GameObject go)
	{
	}

	public void UpdateItem(DiscountItemsInfo info, int type)
	{
		this.currentDiscountItemsInfo = info;
		this.currentShangPinId = info.id;
		this.selectType = type;
		base.get_transform().FindChild("ButtonChangePro").GetComponent<ButtonCustom>().get_gameObject().SetActive(false);
		ShangPin shangPin = DataReader<ShangPin>.Get(this.currentShangPinId);
		if (shangPin != null)
		{
			List<int> discount = shangPin.discount;
			if (discount.get_Count() > 0)
			{
				base.get_transform().FindChild("MinZheKouValue").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(1006008, false), (float)discount.get_Item(discount.get_Count() - 1) / 10f));
			}
			else
			{
				base.get_transform().FindChild("MinZheKouValue").GetComponent<Text>().set_text(string.Empty);
			}
			base.get_transform().FindChild("ItemFlag").get_gameObject().SetActive(info.num <= 0);
			this.productSum.set_text(Utils.SwitchChineseNumber((long)info.num, 1));
			ResourceManager.SetSprite(this.imgIcon, GameDataUtils.GetItemIcon(shangPin.goodsPool));
			this.textCount.set_text("x" + shangPin.diamond);
			Items items = DataReader<Items>.Get(shangPin.goodsPool);
			if (items != null)
			{
				this.productName.set_text(GameDataUtils.GetChineseContent(items.name, false));
			}
			else
			{
				Debug.LogWarning("Items表没有此数据" + shangPin.goodsPool);
			}
			int count = shangPin.discount.get_Count();
			float num = (float)shangPin.discount.get_Item(count - 1);
			float num2 = info.discount / 10f;
			if (num2 < 10f)
			{
				int num3 = (int)Math.Ceiling((double)((float)shangPin.diamond * (num2 / 10f)));
				this.textCount.set_text("x" + num3);
				this.zhekouValue.set_text(num2 + "折");
				this.zhekouBg.get_gameObject().SetActive(true);
			}
			else
			{
				this.zhekouBg.get_gameObject().SetActive(false);
			}
		}
		else
		{
			Debug.LogWarning("ShangPin表没有此数据" + this.currentShangPinId);
		}
	}

	private void SetButtonState(int showState)
	{
	}

	private void DoOK(int count)
	{
		if (this.currentShangPinId == ActivityTossDiscountManager.Instance.CurrentShangPinId)
		{
			ActivityTossDiscountManager.Instance.sendBuyDiscountItemReq(this.currentShangPinId);
		}
		else
		{
			this.changeProLogic();
		}
	}

	public void changeProLogic()
	{
		if (ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId())
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(1005022, false), GameDataUtils.GetChineseContent(1006006, false), delegate
			{
				InstanceManager.TryPause();
			}, delegate
			{
				InstanceManager.TryResume();
			}, delegate
			{
				InstanceManager.TryResume();
				if (ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId())
				{
					ActivityTossDiscountManager.Instance.TempSelectProductId = this.currentShangPinId;
					ActivityTossDiscountManager.Instance.SendReplaceItemReq(ActivityTossDiscountManager.Instance.CurrentShangPinId);
				}
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		}
		else
		{
			this.selectProductLogic();
		}
	}

	private void OnClickPanel(GameObject go)
	{
		if (this.currentDiscountItemsInfo.num <= 0)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(1005022, false), "该商品已经售完！", delegate
			{
				InstanceManager.TryPause();
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
			return;
		}
		if (this.selectType == ActivityTossDiscountManager.selectProductSelectType)
		{
			int listDataIndexByID = ActivityTossDiscountManager.Instance.getListDataIndexByID(ActivityTossDiscountManager.Instance.CurrentShangPinId);
			if (listDataIndexByID != -1)
			{
			}
			this.selectProductLogic();
		}
		else if (this.selectType == ActivityTossDiscountManager.payProductSelectType)
		{
			int key = this.currentShangPinId;
			ShangPin shangPin = null;
			if (DataReader<ShangPin>.Contains(key))
			{
				shangPin = DataReader<ShangPin>.Get(key);
			}
			if (shangPin != null)
			{
				UIBase uIBase = UIManagerControl.Instance.OpenUI("BuyUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
				uIBase.get_transform().SetAsLastSibling();
				BuyUIViewModel.Instance.BuyCallback = delegate(int count)
				{
					this.DoOK(count);
				};
				float discountDataById = ActivityTossDiscountManager.Instance.getDiscountDataById(shangPin.Id);
				float num = discountDataById / 10f;
				int price;
				if (num < 10f)
				{
					price = (int)Math.Ceiling((double)(num / 10f * (float)shangPin.diamond));
				}
				else
				{
					price = shangPin.diamond;
				}
				BuyUIViewModel.Instance.RefreshInfo(shangPin.goodsPool, shangPin.num, price, 1);
				if (this.currentShangPinId == ActivityTossDiscountManager.Instance.CurrentShangPinId)
				{
					BuyUIViewModel.Instance.BtnOKName = GameDataUtils.GetChineseContent(508013, false);
				}
				else
				{
					BuyUIViewModel.Instance.BtnOKName = "获取折扣";
				}
			}
			if (ActivityTossDiscountListAlert.Instance != null)
			{
				ActivityTossDiscountListAlert.Instance.RefreshUI();
			}
		}
	}

	private void selectProductLogic()
	{
		ActivityTossDiscountManager.Instance.TempSelectProductId = this.currentShangPinId;
		if (ActivityTossDiscountUI.Instance != null)
		{
			ActivityTossDiscountUI.Instance.updateItemLogoByItemId(true, this.currentShangPinId);
			ActivityTossDiscountUI.Instance.onClickOkBtnLogic(null);
		}
		if (ActivityTossDiscountListAlert.Instance != null)
		{
			ActivityTossDiscountListAlert.Instance.updateItemLogoByItemId(true, this.currentShangPinId);
		}
	}

	private void OnClickChangePro(GameObject go)
	{
		if (ActivityTossDiscountUI.Instance != null)
		{
			ActivityTossDiscountUI.Instance.changeProLogic();
		}
	}
}
