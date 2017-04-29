using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityTossDiscountUI : UIBase
{
	public static ActivityTossDiscountUI Instance;

	private GridLayoutGroup m_awardlist;

	private GridLayoutGroup m_selectAwardlist;

	private ButtonCustom ButtonOK;

	private ButtonCustom ButtonHeadShow;

	private ButtonCustom ButtonHeadBack;

	public Transform DiscountAction;

	private Text timeCoundDownText;

	private Text DisCountResult;

	private float clickTimeInterval = 5f;

	private float alreadyTime;

	private bool isAlreadyClick;

	private int spineId = -1;

	private uint straightTimerSendID;

	private uint straightTimerResultID;

	private Transform _BtnTopsTransform;

	private int discountActionId = -1;

	private int successOrFailSpineId = -1;

	private int bottomSpineId = -1;

	public Transform BtnTopsTransform
	{
		get
		{
			return this._BtnTopsTransform;
		}
		set
		{
			this._BtnTopsTransform = value;
		}
	}

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

	private void Update()
	{
		if (!this.isAlreadyClick)
		{
			return;
		}
		this.alreadyTime += Time.get_deltaTime();
		if (this.alreadyTime > this.clickTimeInterval)
		{
			this.isAlreadyClick = false;
			this.alreadyTime = 0f;
		}
	}

	private void Awake()
	{
		ActivityTossDiscountUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("Condition").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(1006001, false));
		this.m_awardlist = base.FindTransform("Content").FindChild("Grid").GetComponent<GridLayoutGroup>();
		this.m_selectAwardlist = base.FindTransform("SelectContent").FindChild("SelectGrid").GetComponent<GridLayoutGroup>();
		base.FindTransform("ButtonGetPro").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetPro);
		base.FindTransform("BtnClose").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClosePanel);
		base.FindTransform("ButtonChangePro").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangePro);
		this.ButtonOK = base.FindTransform("ButtonOK").GetComponent<ButtonCustom>();
		this.ButtonOK.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOK);
		this.ButtonHeadShow = base.FindTransform("ButtonHeadShow").GetComponent<ButtonCustom>();
		this.ButtonHeadShow.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHeadShow);
		this.ButtonHeadBack = base.FindTransform("ButtonHeadBack").GetComponent<ButtonCustom>();
		this.ButtonHeadBack.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickHeadBack);
		this.DiscountAction = base.FindTransform("DiscountAction");
		this.timeCoundDownText = base.FindTransform("timeCoundDown").GetComponent<Text>();
		this.DisCountResult = base.FindTransform("DisCountResult").GetComponent<Text>();
		this.DisCountResult.set_text(string.Empty);
		base.FindTransform("ButtonAlert").FindChild("SelectProLogo2").GetComponent<Image>().set_sprite(null);
		base.FindTransform("ResultBg").FindChild("ResultBgLogo").GetComponent<DepthOfUINoCollider>().SortingOrder = 3060;
		base.FindTransform("ResultBg").FindChild("GetZheKou").GetComponent<DepthOfUINoCollider>().SortingOrder = 3061;
		base.FindTransform("ResultBg").FindChild("ResultSpine").GetComponent<DepthOfUINoCollider>().SortingOrder = 3200;
		this.initView();
		this.ButtonHeadShow.set_enabled(true);
		this.ButtonHeadBack.set_enabled(true);
	}

	public void initView()
	{
		this.timeCoundDownText.set_text(string.Empty);
		Text component = base.FindTransform("ProductLogoAndCount").FindChild("Count").GetComponent<Text>();
		long num = BackpackManager.Instance.OnGetGoodCount(ActivityTossDiscountManager.gameNeedItemId);
		component.set_text("x" + num);
		this.updateItemLogoByItemId(false, -1);
		this.buttonShowLogic(ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId());
		base.FindTransform("ButtonAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(false);
		base.FindTransform("ResultBg").get_gameObject().SetActive(false);
	}

	public void initYingBiSpine()
	{
		for (int i = 0; i < this.DiscountAction.get_childCount(); i++)
		{
			GameObject gameObject = this.DiscountAction.GetChild(i).get_gameObject();
			Object.Destroy(gameObject);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
		ActivityTossDiscountManager.Instance.TempSelectProductId = -1;
	}

	protected override void ActionClose()
	{
		base.ActionClose();
		if (this.discountActionId != -1)
		{
			FXSpineManager.Instance.DeleteSpine(this.discountActionId, true);
		}
		if (this.spineId != -1)
		{
			FXSpineManager.Instance.DeleteSpine(this.spineId, true);
		}
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

	private void OnClosePanel(GameObject go)
	{
		this.OnClickMaskAction();
	}

	protected override void OnClickMaskAction()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickChangePro(GameObject go)
	{
		this.changeProLogic();
	}

	public void changeProLogic()
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
				ActivityTossDiscountManager.Instance.SendReplaceItemReq(ActivityTossDiscountManager.Instance.CurrentShangPinId);
			}
		}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
	}

	private void OnClickOpenAlert(GameObject go)
	{
		UIManagerControl.Instance.OpenUI("ActivityTossDiscountListAlert", UINodesManager.MiddleUIRoot, false, UIType.Pop);
	}

	private void OnClickGetPro(GameObject go)
	{
		this.OnBtnUp();
	}

	public void OnBtnUp()
	{
		List<ButtonInfoData> list = new List<ButtonInfoData>();
		int num = 0;
		list.Add(PopButtonTabsManager.GetButtonDataTreasure((long)num, delegate
		{
			this.OnClickMaskAction();
		}));
		list.Add(PopButtonTabsManager.GetButtonDataFightBoss((long)num, delegate
		{
			this.OnClickMaskAction();
		}));
		list.Add(PopButtonTabsManager.GetButtonDataRewardTask((long)num, delegate
		{
			this.OnClickMaskAction();
		}));
		list.Add(PopButtonTabsManager.GetButtonDataDarkTrial((long)num, delegate
		{
			this.OnClickMaskAction();
		}));
		list.Add(PopButtonTabsManager.GetButtonDataZeroCityTask((long)num, delegate
		{
			this.OnClickMaskAction();
		}));
		if (list.get_Count() > 0)
		{
			PopButtonsAdjustUIView popButtonsAdjustUIView = UIManagerControl.Instance.OpenUI("PopButtonsAdjustUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as PopButtonsAdjustUIView;
			popButtonsAdjustUIView.get_transform().set_localPosition(new Vector3(255f, 100f, 0f));
			PopButtonsAdjustUIViewModel.Instance.SetButtonInfos(list);
		}
	}

	private void OnClickOK(GameObject go)
	{
		this.onClickOkBtnLogic(go);
	}

	public void onClickOkBtnLogic(GameObject go)
	{
		this.confirmSelectPro(go);
		this.RefreshUI();
	}

	public void confirmSelectPro(GameObject go)
	{
		ActivityTossDiscountManager.Instance.CurrentShangPinId = ActivityTossDiscountManager.Instance.TempSelectProductId;
		ActivityTossDiscountManager.Instance.TempSelectProductId = -1;
		ActivityTossDiscountManager.Instance.setIsSelectItemById(ActivityTossDiscountManager.Instance.CurrentShangPinId);
		this.setSelectProTitleStatus();
		if (ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId())
		{
			this.playYingBiActionState(4700, this.DiscountAction, null, 0f, 0f);
			this.buttonShowLogic(true);
			ActivityTossDiscountManager.Instance.SendSelectItemReq();
			ActivityTossDiscountManager.Instance.IsAlreadyConfirm = true;
			base.FindTransform("ButtonAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(false);
		}
		else
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(1005022, false), "您没有选中商品！", delegate
			{
				InstanceManager.TryPause();
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
		}
	}

	private void OnClickHeadShow(GameObject go)
	{
		this.OnClickHeadAndBackLogic(true);
	}

	private void OnClickHeadAndBackLogic(bool isShow)
	{
		base.FindTransform("ResultBg").get_gameObject().SetActive(false);
		long num = BackpackManager.Instance.OnGetGoodCount(ActivityTossDiscountManager.gameNeedItemId);
		if (num <= 0L)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(1005022, false), "您的游戏券不够！", delegate
			{
				InstanceManager.TryPause();
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
			return;
		}
		DiscountItemsInfo discountInfoById = ActivityTossDiscountManager.Instance.getDiscountInfoById(ActivityTossDiscountManager.Instance.CurrentShangPinId);
		if (discountInfoById.num <= 0)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(1005022, false), "该商品已经售完！", delegate
			{
				InstanceManager.TryPause();
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
			return;
		}
		ShangPin shangPin = DataReader<ShangPin>.Get(ActivityTossDiscountManager.Instance.CurrentShangPinId);
		if (shangPin == null)
		{
			Debug.LogWarning("配置表没有此商品，id=" + shangPin.Id);
		}
		int count = shangPin.discount.get_Count();
		float num2 = (float)shangPin.discount.get_Item(count - 1);
		float discountDataById = ActivityTossDiscountManager.Instance.getDiscountDataById(ActivityTossDiscountManager.Instance.CurrentShangPinId);
		if (discountDataById <= num2)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(1005022, false), GameDataUtils.GetChineseContent(1006005, false), delegate
			{
				InstanceManager.TryPause();
			}, GameDataUtils.GetNoticeText(102), "button_orange_1", null);
		}
		else
		{
			this.sendFlipCoinReqLogic(isShow);
		}
	}

	private void sendFlipCoinReqLogic(bool isShow)
	{
		this.ButtonHeadShow.set_enabled(false);
		this.ButtonHeadBack.set_enabled(false);
		this.isAlreadyClick = true;
		this.FlipCoinAction(isShow);
	}

	private void OnClickHeadBack(GameObject go)
	{
		this.OnClickHeadAndBackLogic(false);
	}

	private void FlipCoinAction(bool isShow)
	{
		ActivityTossDiscountManager.Instance.SendFlipCoinReq(isShow);
		if (this.spineId != -1)
		{
			FXSpineManager.Instance.DeleteSpine(this.spineId, true);
		}
		this.rePlayFirstSpineAction();
	}

	private void rePlayFirstSpineAction()
	{
		this.playYingBiActionState(4705, this.DiscountAction, null, 0f, 0f);
		this.discountActionId = FXSpineManager.Instance.PlaySpine(4706, this.DiscountAction, "ActivityTossDiscountUI", 3100, delegate
		{
			if (this.discountActionId != -1)
			{
				FXSpineManager.Instance.DeleteSpine(this.discountActionId, true);
			}
			if (this.spineId != -1)
			{
				FXSpineManager.Instance.DeleteSpine(this.spineId, true);
			}
			if (ActivityTossDiscountManager.Instance.FlipCoinResult != null)
			{
				this.updateFlipCoinRes(ActivityTossDiscountManager.Instance.FlipCoinResult);
				this.RefreshUI();
			}
			else
			{
				this.rePlayFirstSpineAction();
			}
		}, "UI", 0f, 80f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void playYingBiActionState(int fxId, Transform transform, Action callback = null, float xOffset = 0f, float yOffset = 0f)
	{
		if (this.spineId != -1)
		{
			FXSpineManager.Instance.DeleteSpine(this.spineId, true);
		}
		this.spineId = FXSpineManager.Instance.PlaySpine(fxId, transform, "ActivityTossDiscountUI", 3050, delegate
		{
			if (callback != null)
			{
				callback.Invoke();
			}
		}, "UI", xOffset, yOffset, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void OnReplaceItemRes(ReplaceItemRes selectItemRes)
	{
		this.replaceOrBuyReturnLogic(true);
	}

	public void replaceOrBuyReturnLogic(bool isConfirmSelectPro)
	{
		for (int i = 0; i < this.DiscountAction.get_childCount(); i++)
		{
			GameObject gameObject = this.DiscountAction.GetChild(i).get_gameObject();
			Object.Destroy(gameObject);
		}
		ActivityTossDiscountManager.Instance.IsAlreadyConfirm = false;
		this.buttonShowLogic(false);
		base.FindTransform("ButtonAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(false);
		base.FindTransform("SelectProTitle").GetComponent<Text>().get_gameObject().SetActive(false);
		base.FindTransform("ResultBg").get_gameObject().SetActive(false);
		if (isConfirmSelectPro)
		{
			this.confirmSelectPro(null);
		}
		this.RefreshUI();
	}

	public void buttonShowLogic(bool isThrowToss)
	{
		base.FindTransform("ButtonSelectAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(!isThrowToss);
		base.FindTransform("ButtonAlert").GetComponent<ButtonCustom>().get_gameObject().SetActive(!isThrowToss);
		base.FindTransform("ProductShow").GetComponent<Image>().get_gameObject().SetActive(isThrowToss);
		base.FindTransform("CountShow").GetComponent<Text>().get_gameObject().SetActive(isThrowToss);
		this.ButtonHeadShow.get_gameObject().SetActive(isThrowToss);
		base.FindTransform("ProductBack").GetComponent<Image>().get_gameObject().SetActive(isThrowToss);
		base.FindTransform("CountBack").GetComponent<Text>().get_gameObject().SetActive(isThrowToss);
		this.ButtonHeadBack.get_gameObject().SetActive(isThrowToss);
		if (isThrowToss)
		{
			this.playYingBiActionState(4700, this.DiscountAction, null, 0f, 0f);
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_awardlist.get_transform().get_childCount(); i++)
		{
			this.m_awardlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
			Object.Destroy(this.m_awardlist.get_transform().GetChild(i).get_gameObject());
		}
		for (int j = 0; j < this.m_selectAwardlist.get_transform().get_childCount(); j++)
		{
			this.m_selectAwardlist.get_transform().GetChild(j).get_gameObject().SetActive(false);
			Object.Destroy(this.m_selectAwardlist.get_transform().GetChild(j).get_gameObject());
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
		GridLayoutGroup gridLayoutGroup;
		if (info.isOpt)
		{
			gridLayoutGroup = this.m_selectAwardlist;
		}
		else
		{
			gridLayoutGroup = this.m_awardlist;
		}
		if (gridLayoutGroup == null)
		{
			return;
		}
		int id = info.id;
		Transform transform = gridLayoutGroup.get_transform().FindChild("ActivityTossDiscountItem" + index);
		transform = null;
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<ActivityTossDiscountItem>().UpdateItem(info, ActivityTossDiscountManager.payProductSelectType);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ActivityTossDiscountItem");
			instantiate2Prefab.get_transform().SetParent(gridLayoutGroup.get_transform(), false);
			instantiate2Prefab.set_name("ActivityTossDiscountItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<ActivityTossDiscountItem>().UpdateItem(info, ActivityTossDiscountManager.payProductSelectType);
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

	public void UpdateSelectPanel()
	{
	}

	public void RefreshUI()
	{
		if (ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId())
		{
			base.FindTransform("ListBg").FindChild("TitleName").GetComponent<Text>().set_text("选中商品");
			base.FindTransform("Content").GetComponent<RectTransform>().set_sizeDelta(new Vector2(415f, 280f));
			base.FindTransform("TitleNameTwo").GetComponent<Text>().get_gameObject().SetActive(true);
			base.FindTransform("LeftTitleBgTwo").GetComponent<Image>().get_gameObject().SetActive(true);
			base.FindTransform("LeftTitleBg2Two").GetComponent<Image>().get_gameObject().SetActive(true);
		}
		else
		{
			base.FindTransform("ListBg").FindChild("TitleName").GetComponent<Text>().set_text("商品区");
			base.FindTransform("Content").GetComponent<RectTransform>().set_sizeDelta(new Vector2(415f, 452f));
			base.FindTransform("TitleNameTwo").GetComponent<Text>().get_gameObject().SetActive(false);
			base.FindTransform("LeftTitleBgTwo").GetComponent<Image>().get_gameObject().SetActive(false);
			base.FindTransform("LeftTitleBg2Two").GetComponent<Image>().get_gameObject().SetActive(false);
		}
		this.UpdatePanel();
		this.UpdateSelectPanel();
	}

	protected void RefreshDiscountItemsList(List<DiscountItemsInfo> exchangeData, int id)
	{
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
		base.FindTransform("ButtonAlert").FindChild("SelectProTitle").GetComponent<Text>().get_gameObject().SetActive(shangPin == null);
	}

	public void setSelectProTitleStatus()
	{
		base.FindTransform("ButtonAlert").FindChild("SelectProTitle").GetComponent<Text>().get_gameObject().SetActive(!ActivityTossDiscountManager.Instance.isHaveCurrentShangPinId());
	}

	public void updateFlipCoinRes(FlipCoinRes flipCoinRes)
	{
		if (flipCoinRes != null)
		{
			if (this.spineId != -1)
			{
				FXSpineManager.Instance.DeleteSpine(this.spineId, true);
			}
			Text component = base.FindTransform("ProductLogoAndCount").FindChild("Count").GetComponent<Text>();
			long num = BackpackManager.Instance.OnGetGoodCount(ActivityTossDiscountManager.gameNeedItemId);
			component.set_text("x" + num);
			int id = flipCoinRes.id;
			int discount = flipCoinRes.discount;
			bool result = flipCoinRes.result;
			ActivityTossDiscountManager.Instance.FlipCoinResult = null;
			if (DataReader<ShangPin>.Contains(id))
			{
				ShangPin shangPin = DataReader<ShangPin>.Get(id);
			}
			int fxId;
			if (result)
			{
				this.setProductDiscountByPID(id, discount);
				if (ActivityTossDiscountManager.Instance.IsSelectHead)
				{
					fxId = 4702;
				}
				else
				{
					fxId = 4701;
				}
				ResourceManager.SetSprite(base.FindTransform("ResultBg").FindChild("ResultBgLogo").GetComponent<Image>(), ResourceManager.GetIconSprite("zksd_cg"));
				float num2 = (float)discount / 10f;
				if (num2 != 0f && num2 < 10f)
				{
					base.FindTransform("ResultBg").FindChild("GetZheKou").GetComponent<Text>().set_text("获得折扣：" + num2 + "折");
				}
			}
			else
			{
				if (ActivityTossDiscountManager.Instance.IsSelectHead)
				{
					fxId = 4701;
				}
				else
				{
					fxId = 4702;
				}
				ResourceManager.SetSprite(base.FindTransform("ResultBg").FindChild("ResultBgLogo").GetComponent<Image>(), ResourceManager.GetIconSprite("zksd_sb"));
				base.FindTransform("ResultBg").FindChild("GetZheKou").GetComponent<Text>().set_text("获得折扣：无");
			}
			this.playYingBiActionState(fxId, this.DiscountAction, null, 0f, 0f);
			this.discountActionId = FXSpineManager.Instance.PlaySpine(4704, this.DiscountAction, "ActivityTossDiscountUI", 3100, delegate
			{
				if (this.discountActionId != -1)
				{
					FXSpineManager.Instance.DeleteSpine(this.discountActionId, true);
				}
				TimerHeap.DelTimer(this.straightTimerResultID);
				this.straightTimerResultID = TimerHeap.AddTimer(200u, 0, new Action(this.playResultEffect));
			}, "UI", 0f, 80f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void playResultEffect()
	{
		base.FindTransform("ResultBg").get_gameObject().SetActive(true);
		this.ExpAnim();
		for (int i = 0; i < base.FindTransform("ResultBg").FindChild("ResultSpine").get_childCount(); i++)
		{
			GameObject gameObject = base.FindTransform("ResultBg").FindChild("ResultSpine").GetChild(i).get_gameObject();
			Object.Destroy(gameObject);
		}
		if (this.bottomSpineId != -1)
		{
			FXSpineManager.Instance.DeleteSpine(this.bottomSpineId, true);
		}
		this.bottomSpineId = FXSpineManager.Instance.PlaySpine(4504, base.FindTransform("ResultBg").FindChild("ResultSpine"), "ActivityTossDiscountUI", 3001, delegate
		{
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.successOrFailSpineId = FXSpineManager.Instance.PlaySpine(4703, base.FindTransform("ResultBg").FindChild("ResultSpine"), "ActivityTossDiscountUI", 3200, delegate
		{
			if (this.successOrFailSpineId != -1)
			{
				FXSpineManager.Instance.DeleteSpine(this.successOrFailSpineId, true);
			}
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void updateSelectItemRes(SelectItemRes selectItemRes)
	{
	}

	public void setProductDiscountByPID(int productID, int discount)
	{
	}

	public void updateTimeCoundDown(string formatTime)
	{
		this.timeCoundDownText.set_text(formatTime);
	}

	public void ExpAnim()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("from", 0.5);
		hashtable.Add("to", 1);
		hashtable.Add("time", 0.2f);
		hashtable.Add("onupdate", "ExpTween");
		hashtable.Add("oncomplete", "ExpAnimTransparent");
		iTween.ValueTo(base.get_gameObject(), hashtable);
	}

	private void ExpTween(float scale)
	{
		base.FindTransform("ResultBg").set_localScale(new Vector3(scale, scale, scale));
	}

	public void ExpAnimTransparent()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("from", 1);
		hashtable.Add("to", 0);
		hashtable.Add("time", 1.5f);
		hashtable.Add("onupdate", "ExpTweenTransparent");
		hashtable.Add("oncomplete", "resultPlayOverCallback");
		iTween.ValueTo(base.get_gameObject(), hashtable);
	}

	private void resultPlayOverCallback()
	{
		this.ButtonHeadShow.set_enabled(true);
		this.ButtonHeadBack.set_enabled(true);
	}

	private void ExpTweenTransparent(float transparent)
	{
		Color color = base.FindTransform("ResultBg").FindChild("ResultBgLogo").GetComponent<Image>().get_color();
		base.FindTransform("ResultBg").FindChild("ResultBgLogo").GetComponent<Image>().set_color(new Color(color.r, color.g, color.b, transparent));
		Color color2 = base.FindTransform("ResultBg").FindChild("GetZheKou").GetComponent<Text>().get_color();
		base.FindTransform("ResultBg").FindChild("GetZheKou").GetComponent<Text>().set_color(new Color(color2.r, color2.g, color2.b, transparent));
	}
}
