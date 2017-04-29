using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ActivityTossDiscountListAlert : UIBase
{
	public static ActivityTossDiscountListAlert Instance;

	private GridLayoutGroup m_awardlist;

	private ButtonCustom ButtonOK;

	private void Start()
	{
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		ActivityTossDiscountListAlert.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_awardlist = base.FindTransform("Content").FindChild("Grid").GetComponent<GridLayoutGroup>();
		this.ButtonOK = base.FindTransform("ButtonOK").GetComponent<ButtonCustom>();
		this.ButtonOK.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOK);
		base.FindTransform("ButtonAlert").FindChild("SelectProTitle").GetComponent<Text>().set_text(string.Empty);
		this.buttonShowLogic(ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId() || ActivityTossDiscountManager.Instance.TempSelectProductId != -1);
		this.RefreshUI();
	}

	public void buttonShowLogic(bool isThrowToss)
	{
		int num = ActivityTossDiscountManager.Instance.CurrentShangPinId;
		if (num == -1)
		{
			num = ActivityTossDiscountManager.Instance.TempSelectProductId;
		}
		this.updateItemLogoByItemId(isThrowToss, num);
	}

	public void updateItemLogoByItemId(bool isShowButton, int productId = -1)
	{
		int key = ActivityTossDiscountManager.Instance.CurrentShangPinId;
		if (productId != -1)
		{
			key = productId;
		}
		ShangPin shangPin = null;
		if (DataReader<ShangPin>.Contains(key))
		{
			shangPin = DataReader<ShangPin>.Get(key);
		}
		if (shangPin != null)
		{
			base.FindTransform("ButtonAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(isShowButton);
			base.FindTransform("ButtonSelectAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(!isShowButton);
			ResourceManager.SetSprite(base.FindTransform("ButtonAlert").FindChild("SelectProLogo2").GetComponent<Image>(), GameDataUtils.GetItemIcon(shangPin.goodsPool));
			Items items = DataReader<Items>.Get(shangPin.goodsPool);
			if (items != null)
			{
				base.FindTransform("ButtonAlert").FindChild("Name").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(items.name, false));
			}
			else
			{
				Debug.LogWarning("Items表没有此数据" + shangPin.goodsPool);
			}
		}
		else
		{
			base.FindTransform("ButtonAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(false);
			base.FindTransform("ButtonSelectAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(true);
		}
	}

	private void OnClickOK(GameObject go)
	{
		this.selectProductLogic();
		if (ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId())
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
			if (ActivityTossDiscountUI.Instance != null)
			{
				ActivityTossDiscountUI.Instance.confirmSelectPro(go);
			}
		}
		else
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(1005022, false), "您没有选中商品！", delegate
			{
				InstanceManager.TryPause();
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
		}
	}

	private void selectProductLogic()
	{
		ActivityTossDiscountManager.Instance.CurrentShangPinId = ActivityTossDiscountManager.Instance.TempSelectProductId;
		ActivityTossDiscountManager.Instance.setIsSelectItemById(ActivityTossDiscountManager.Instance.CurrentShangPinId);
		if (ActivityTossDiscountManager.Instance.IsAlreadyConfirm)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(1005022, false), "已经选中过了商品，请先更换!", delegate
			{
				InstanceManager.TryPause();
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
			return;
		}
		if (ActivityTossDiscountUI.Instance != null)
		{
			ActivityTossDiscountUI.Instance.updateItemLogoByItemId(true, -1);
			ActivityTossDiscountUI.Instance.RefreshUI();
		}
		this.updateItemLogoByItemId(true, -1);
		this.RefreshUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void ActionClose()
	{
		base.ActionClose();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
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

	private void OnGetSignChangedNty()
	{
	}

	private void OnClickExit()
	{
	}

	private void OnClickBtnBuy(GameObject go)
	{
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_awardlist.get_transform().get_childCount(); i++)
		{
			this.m_awardlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	public void UpdatePanel()
	{
		DiscountItemsLoginPush discountItemsData = ActivityTossDiscountManager.Instance.GetDiscountItemsData();
		if (discountItemsData == null)
		{
			return;
		}
		if (discountItemsData.itemsInfo == null)
		{
			return;
		}
		this.ClearScroll();
		for (int i = 0; i < discountItemsData.itemsInfo.get_Count(); i++)
		{
			this.AddScrollCell(i, discountItemsData.itemsInfo.get_Item(i));
		}
	}

	private void AddScrollCell(int index, DiscountItemsInfo info)
	{
		int id = info.id;
		Transform transform = this.m_awardlist.get_transform().FindChild("ActivityTossDiscountItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<ActivityTossDiscountItem>().UpdateItem(info, ActivityTossDiscountManager.selectProductSelectType);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ActivityTossDiscountItem");
			instantiate2Prefab.get_transform().SetParent(this.m_awardlist.get_transform(), false);
			instantiate2Prefab.set_name("ActivityTossDiscountItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<ActivityTossDiscountItem>().UpdateItem(info, ActivityTossDiscountManager.selectProductSelectType);
		}
	}

	private void ScrollToAvailableCell()
	{
		GrowthPlanListPush growUpPlanData = GrowUpPlanManager.Instance.GetGrowUpPlanData();
		RectTransform component = this.m_awardlist.GetComponent<RectTransform>();
		component.set_localPosition(new Vector3(0f, 0f, 0f)
		{
			y = (float)(-(float)growUpPlanData.item.get_Count()) * (this.m_awardlist.get_cellSize().y + this.m_awardlist.get_spacing().y)
		});
	}

	public void RefreshUI()
	{
		this.UpdatePanel();
	}
}
