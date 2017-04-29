using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDetailedUI : UIBase
{
	private ListPool equipPosListPool;

	private EquipPosItem currentSelectPosItem;

	private Dictionary<EquipLibType.ELT, EquipPosItem> LeftEquipPosDic;

	private Dictionary<EquipDetailedUIState, ButtonCustom> BtnEquipLeftDic;

	private Dictionary<EquipDetailedUIState, Transform> RightEquipTransformDic;

	private List<Transform> BrilliantAttrList;

	private Transform ExploredFXRoot;

	private Slider strengthProgressSlider;

	private Text nextSuccessRatioTxt;

	private ButtonCustom BtnStrengthen;

	private ButtonCustom BtnAutoStrengthen;

	private Text TextCostNumStrengthen1;

	private Image ImageStrengThenCost1;

	private Transform Attr1Strengthen;

	private Transform Attr2Strengthen;

	private Transform EquipCenter;

	private Transform EquipStrengthen;

	private Transform EquipStarUpCenter;

	private Transform BtnStrengthenFull;

	public EquipDetailedUIState currentEquipDetailedUIState;

	public EquipLibType.ELT currentSelectPos;

	private int CurrentSelectWashIndex;

	private bool IsLockWashPos;

	public bool IsFirstWash;

	private List<Transform> starTransformList;

	private List<int> starMaterialIDList;

	private int currentSelectStarMaterialState;

	private List<Transform> enchantmentItemList;

	private ButtonCustom BtnStarUp;

	private ButtonCustom BtnWash;

	private int canEnchantmentMinLv;

	private int canStarUpMinLv;

	private bool IsShowStrengthAnimation;

	private Transform strengthFXMaskTrans;

	private RectTransform equipPosListSRRect;

	private RectTransform ListScrollMaskRect;

	private Transform OperateTip;

	private Text operateTipText;

	private Transform equipForgingLeftRoot;

	private ListPool forgeCoinListPool;

	private ListPool checkAttrListPool;

	private Transform noForgingLeftRoot;

	private Text suitNameTitle;

	private Text noForgingTipText;

	private Text btnForgeNameText;

	private Text noForgingTipRightText;

	private ButtonCustom changeHighBtn;

	private ButtonCustom changeLowBtn;

	private ButtonCustom forgeBtn;

	private bool isSelectHigh;

	private TaoZhuangDuanZhu currentForgingCfg;

	private int fxPosID;

	private int fxBG;

	private int fxBtnCenter;

	private int progressBarFullFxID;

	private int strengthSuccessFxID;

	private int strengthBarExploredFxID;

	private readonly Color HIGH_LIGHT = new Color(1f, 0.98f, 0.902f);

	private readonly Color LOW_LIGHT = new Color(1f, 0.843f, 0.549f);

	private int equipIconFxID;

	private int currentRollStrengthLv;

	private int lastStrengthLv;

	private uint successProgressTimerID;

	private float lastSuccessRatioAmount;

	private float successRatioDelta = 0.005f;

	private int successBlessValueDelta;

	private int currentLvSuccessBlessValue;

	private int succesBlessValue;

	private List<Action> m_Actions = new List<Action>();

	private string finalProgressTextContent;

	private uint fx_mask_timer_id;

	private bool IsCanSendInIntensify = true;

	private int currentSelectEnchantmentPos = -1;

	private int currentExcellentID;

	private ExcellentAttr currentExcellentAttr;

	private int equipSuitFXID;

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		List<int> value = DataReader<ShengXingJiChuPeiZhi>.Get("boostStarMaterial").value;
		this.starMaterialIDList = new List<int>();
		for (int i = 0; i < value.get_Count(); i++)
		{
			this.starMaterialIDList.Add(value.get_Item(i));
		}
		this.canEnchantmentMinLv = (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("enchantLv").value);
		this.canStarUpMinLv = DataReader<ShengXingJiChuPeiZhi>.Get("equipLv").num;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.equipPosListPool = base.FindTransform("EquipPosList").GetComponent<ListPool>();
		this.equipPosListPool.SetItem("EquipPosItem");
		this.EquipCenter = base.FindTransform("EquipCenter");
		this.EquipStrengthen = base.FindTransform("EquipStrengthen");
		this.EquipStarUpCenter = base.FindTransform("EquipStarUpCenter");
		ButtonCustom expr_6A = this.EquipCenter.GetComponent<ButtonCustom>();
		expr_6A.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_6A.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickChangeEquip));
		this.equipPosListSRRect = base.FindTransform("EquipPosListSR").GetComponent<RectTransform>();
		this.ListScrollMaskRect = base.FindTransform("ListScrollMask").GetComponent<RectTransform>();
		this.OperateTip = base.FindTransform("OperateTip");
		this.operateTipText = base.FindTransform("OperateTipText").GetComponent<Text>();
		this.BtnStrengthen = base.FindTransform("BtnStrengthen").GetComponent<ButtonCustom>();
		this.BtnStrengthen.get_transform().FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505099, false));
		this.BtnAutoStrengthen = base.FindTransform("BtnAutoStrengthen").GetComponent<ButtonCustom>();
		this.BtnAutoStrengthen.get_transform().FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505100, false));
		this.BtnStrengthenFull = base.FindTransform("BtnStrengthenFull");
		this.Attr1Strengthen = base.FindTransform("Attr1Strengthen");
		this.Attr2Strengthen = base.FindTransform("Attr2Strengthen");
		this.TextCostNumStrengthen1 = base.FindTransform("TextCostNumStrengthen1").GetComponent<Text>();
		this.ImageStrengThenCost1 = base.FindTransform("ImageStrengThenCost1").GetComponent<Image>();
		this.ExploredFXRoot = base.FindTransform("ExploredFXRoot");
		this.strengthProgressSlider = base.FindTransform("nextLevelTip").GetComponent<Slider>();
		this.nextSuccessRatioTxt = base.FindTransform("NextSuccessRatio").GetComponent<Text>();
		this.BtnStrengthen.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnStrengthen);
		this.BtnAutoStrengthen.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnAutoStrengthen);
		this.strengthFXMaskTrans = base.FindTransform("StrengthFXMask");
		this.BrilliantAttrList = new List<Transform>();
		for (int i = 1; i < 4; i++)
		{
			Transform transform = base.FindTransform("BrilliantAttr" + i);
			if (transform != null)
			{
				this.BrilliantAttrList.Add(transform);
				transform.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectExcellentAttrPosBtn);
			}
		}
		ButtonCustom expr_2AC = base.FindTransform("BtnCheckBrilliantAttr").GetComponent<ButtonCustom>();
		expr_2AC.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_2AC.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickBtnCheckBrilliantAttr));
		this.BtnWash = base.FindTransform("BtnWash").GetComponent<ButtonCustom>();
		this.BtnWash.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickWashBtn);
		ButtonCustom expr_30A = base.FindTransform("BtnWashChange").GetComponent<ButtonCustom>();
		expr_30A.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_30A.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickWashChange));
		this.starTransformList = new List<Transform>();
		for (int j = 1; j < 16; j++)
		{
			Transform transform2 = base.FindTransform("star" + j);
			if (transform2 != null)
			{
				this.starTransformList.Add(transform2);
			}
		}
		this.BtnStarUp = base.FindTransform("BtnStarUp").GetComponent<ButtonCustom>();
		this.BtnStarUp.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStarUp);
		base.FindTransform("BtnResetStar").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickResetStar);
		base.FindTransform("CanNotStarUpText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(20005, false));
		this.enchantmentItemList = new List<Transform>();
		for (int k = 1; k < 4; k++)
		{
			Transform transform3 = base.FindTransform("EnchantmentItem" + k);
			if (transform3 != null)
			{
				this.enchantmentItemList.Add(transform3);
				transform3.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectEnchantmentItem);
			}
		}
		base.FindTransform("CanNotEnchantmentText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(20006, false));
		this.equipForgingLeftRoot = base.FindTransform("EquipForgingLeftRoot");
		this.forgeCoinListPool = base.FindTransform("ForgeCoinRegion").GetComponent<ListPool>();
		this.checkAttrListPool = base.FindTransform("CheckAttrListPool").GetComponent<ListPool>();
		this.noForgingLeftRoot = this.equipForgingLeftRoot.FindChild("NoForgingRoot");
		this.suitNameTitle = base.FindTransform("SuitNameTitle").GetComponent<Text>();
		this.noForgingTipText = base.FindTransform("NoForgingTipText").GetComponent<Text>();
		this.btnForgeNameText = base.FindTransform("BtnForgeName").GetComponent<Text>();
		this.noForgingTipRightText = base.FindTransform("NoForgingTipRightText").GetComponent<Text>();
		this.forgeBtn = base.FindTransform("BtnForge").GetComponent<ButtonCustom>();
		this.changeLowBtn = base.FindTransform("BtnChangeToLow").GetComponent<ButtonCustom>();
		this.changeHighBtn = base.FindTransform("BtnChangeToHigh").GetComponent<ButtonCustom>();
		this.checkAttrListPool.Clear();
		this.forgeCoinListPool.Clear();
		base.FindTransform("BtnHelp").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipSuitHelp);
		base.FindTransform("BtnForge").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnForge);
		this.changeLowBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangeBtn);
		this.changeHighBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangeBtn);
		this.noForgingTipText.set_text(GameDataUtils.GetChineseContent(510210, false));
		this.BtnEquipLeftDic = new Dictionary<EquipDetailedUIState, ButtonCustom>();
		ButtonCustom component = base.FindTransform("BtnEquipStrengthen").GetComponent<ButtonCustom>();
		ButtonCustom component2 = base.FindTransform("BtnEquipGem").GetComponent<ButtonCustom>();
		ButtonCustom component3 = base.FindTransform("BtnEquipWash").GetComponent<ButtonCustom>();
		ButtonCustom component4 = base.FindTransform("BtnEquipStarUp").GetComponent<ButtonCustom>();
		ButtonCustom component5 = base.FindTransform("BtnEquipEnchantment").GetComponent<ButtonCustom>();
		ButtonCustom component6 = base.FindTransform("BtnEquipForging").GetComponent<ButtonCustom>();
		component.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipStrengthen);
		component2.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipGem);
		component3.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipWash);
		component4.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipStarUp);
		component5.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipEnchantment);
		component6.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipSuitForge);
		this.BtnEquipLeftDic.Add(EquipDetailedUIState.EquipStrengthen, component);
		this.BtnEquipLeftDic.Add(EquipDetailedUIState.EquipGem, component2);
		this.BtnEquipLeftDic.Add(EquipDetailedUIState.EquipWash, component3);
		this.BtnEquipLeftDic.Add(EquipDetailedUIState.EquipStarUp, component4);
		this.BtnEquipLeftDic.Add(EquipDetailedUIState.EquipEnchantment, component5);
		this.BtnEquipLeftDic.Add(EquipDetailedUIState.EquipSuitForge, component6);
		this.LeftEquipPosDic = new Dictionary<EquipLibType.ELT, EquipPosItem>();
		this.RightEquipTransformDic = new Dictionary<EquipDetailedUIState, Transform>();
		Transform transform4 = base.FindTransform("RightStrengthen");
		Transform transform5 = base.FindTransform("RightGem");
		Transform transform6 = base.FindTransform("RightEquipWash");
		Transform transform7 = base.FindTransform("RightEquipStarUp");
		Transform transform8 = base.FindTransform("RightEquipEnchantment");
		Transform transform9 = base.FindTransform("RightEquipSuitForge");
		this.RightEquipTransformDic.Add(EquipDetailedUIState.EquipStrengthen, transform4);
		this.RightEquipTransformDic.Add(EquipDetailedUIState.EquipGem, transform5);
		this.RightEquipTransformDic.Add(EquipDetailedUIState.EquipWash, transform6);
		this.RightEquipTransformDic.Add(EquipDetailedUIState.EquipStarUp, transform7);
		this.RightEquipTransformDic.Add(EquipDetailedUIState.EquipEnchantment, transform8);
		this.RightEquipTransformDic.Add(EquipDetailedUIState.EquipSuitForge, transform9);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110021), string.Empty, delegate
		{
			if (!this.IsFirstWash)
			{
				string chineseContent = GameDataUtils.GetChineseContent(621264, false);
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, GameDataUtils.GetChineseContent(651056, false), null, delegate
				{
					this.IsFirstWash = true;
					EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
					if (equipLib != null)
					{
						EquipmentManager.Instance.SendCancelRefineDataReq((int)this.currentSelectPos, equipLib.wearingId);
					}
					UIStackManager.Instance.PopUIPrevious(base.uiType);
				}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
			}
			else
			{
				UIStackManager.Instance.PopUIPrevious(base.uiType);
			}
		}, false);
		this.IsFirstWash = true;
		this.RefreshStarUpStoneItem();
		this.ResetStrengthFXSetting(false);
		this.RefreshUI(EquipmentManager.Instance.LastSelectUIDetailState, EquipmentManager.Instance.LastSelectUIPos);
		this.changeHighBtn.get_gameObject().SetActive(true);
		this.changeLowBtn.get_gameObject().SetActive(false);
		this.isSelectHigh = false;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EquipmentManager.Instance.LastSelectUIDetailState = this.currentEquipDetailedUIState;
		EquipmentManager.Instance.LastSelectUIPos = this.currentSelectPos;
		this.RemoveStrengthSuccessRationAni();
		FXSpineManager.Instance.DeleteSpine(this.equipSuitFXID, true);
		TimerHeap.DelTimer(this.fx_mask_timer_id);
	}

	public void CheckBadge()
	{
		using (Dictionary<EquipLibType.ELT, EquipPosItem>.Enumerator enumerator = this.LeftEquipPosDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<EquipLibType.ELT, EquipPosItem> current = enumerator.get_Current();
				bool showTip = EquipmentManager.Instance.CheckBadageByPosAndEquipDetailedUIState(current.get_Key(), this.currentEquipDetailedUIState);
				current.get_Value().ShowTip = showTip;
			}
		}
		using (Dictionary<EquipDetailedUIState, ButtonCustom>.Enumerator enumerator2 = this.BtnEquipLeftDic.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<EquipDetailedUIState, ButtonCustom> current2 = enumerator2.get_Current();
				if (current2.get_Value().get_transform().FindChild("ImageBadge").get_gameObject() != null)
				{
					bool active = false;
					switch (current2.get_Key())
					{
					case EquipDetailedUIState.EquipStrengthen:
						active = EquipmentManager.Instance.CheckCanShowStrengthenTipAllPos();
						break;
					case EquipDetailedUIState.EquipGem:
						active = GemManager.Instance.IsCanWearGem();
						break;
					case EquipDetailedUIState.EquipStarUp:
						active = EquipmentManager.Instance.CheckCanShowStarUpTipAllPos();
						break;
					case EquipDetailedUIState.EquipEnchantment:
						active = EquipmentManager.Instance.CheckCanShowEnchantmentTipAllPos();
						break;
					}
					current2.get_Value().get_transform().FindChild("ImageBadge").get_gameObject().SetActive(active);
				}
			}
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
		EventDispatcher.AddListener(EventNames.OnGetAcquireNewEquipNty, new Callback(this.OnGetAcquireNewEquipNty));
		EventDispatcher.AddListener<bool>(EventNames.OnGetEquipAdvancedRes, new Callback<bool>(this.OnGetEquipAdvancedRes));
		EventDispatcher.AddListener(EventNames.OnResetEquipStar, new Callback(this.OnResetEquipStar));
		EventDispatcher.AddListener(EventNames.OnGetBuyGoldRes, new Callback(this.OnGetBuyGoldRes));
		EventDispatcher.AddListener(EventNames.EquipDetailedShouldCheckBadge, new Callback(this.EquipDetailedShouldCheckBadge));
		EventDispatcher.AddListener<bool>(EventNames.OnIntensifyPosSuccessOrFailed, new Callback<bool>(this.OnIntensifyPosSuccessOrFailed));
		EventDispatcher.AddListener<ExcellentAttr>(EventNames.OnRefineEquipRes, new Callback<ExcellentAttr>(this.OnRefineEquipRes));
		EventDispatcher.AddListener<int>(EventNames.OnRefineEquipResultAckRes, new Callback<int>(this.OnRefineEquipResultAckRes));
		EventDispatcher.AddListener(EventNames.OnCancelRefineDataRes, new Callback(this.OnCancelRefineDataRes));
		EventDispatcher.AddListener<int>(EventNames.OnEnchantEquipResultAckRes, new Callback<int>(this.OnEnchantEquipResultAckRes));
		EventDispatcher.AddListener(EventNames.EquipEquipmentSucess, new Callback(this.OnEquipmentChangeRes));
		EventDispatcher.AddListener<bool, int>(EventNames.OnStarUpRes, new Callback<bool, int>(this.OnStarUpFailOrSuccess));
		EventDispatcher.AddListener<int>(EventNames.OnForgingSuitRes, new Callback<int>(this.OnForgingSuitRes));
		EventDispatcher.AddListener<int>(EventNames.UpdateEquipPosGemData, new Callback<int>(this.UpdateEquipPosGemData));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetAcquireNewEquipNty, new Callback(this.OnGetAcquireNewEquipNty));
		EventDispatcher.RemoveListener<bool>(EventNames.OnGetEquipAdvancedRes, new Callback<bool>(this.OnGetEquipAdvancedRes));
		EventDispatcher.RemoveListener(EventNames.OnResetEquipStar, new Callback(this.OnResetEquipStar));
		EventDispatcher.RemoveListener(EventNames.OnGetBuyGoldRes, new Callback(this.OnGetBuyGoldRes));
		EventDispatcher.RemoveListener(EventNames.EquipDetailedShouldCheckBadge, new Callback(this.EquipDetailedShouldCheckBadge));
		EventDispatcher.RemoveListener<bool>(EventNames.OnIntensifyPosSuccessOrFailed, new Callback<bool>(this.OnIntensifyPosSuccessOrFailed));
		EventDispatcher.RemoveListener<ExcellentAttr>(EventNames.OnRefineEquipRes, new Callback<ExcellentAttr>(this.OnRefineEquipRes));
		EventDispatcher.RemoveListener<int>(EventNames.OnRefineEquipResultAckRes, new Callback<int>(this.OnRefineEquipResultAckRes));
		EventDispatcher.RemoveListener(EventNames.OnCancelRefineDataRes, new Callback(this.OnCancelRefineDataRes));
		EventDispatcher.RemoveListener<int>(EventNames.OnEnchantEquipResultAckRes, new Callback<int>(this.OnEnchantEquipResultAckRes));
		EventDispatcher.RemoveListener(EventNames.EquipEquipmentSucess, new Callback(this.OnEquipmentChangeRes));
		EventDispatcher.RemoveListener<bool, int>(EventNames.OnStarUpRes, new Callback<bool, int>(this.OnStarUpFailOrSuccess));
		EventDispatcher.RemoveListener<int>(EventNames.OnForgingSuitRes, new Callback<int>(this.OnForgingSuitRes));
		EventDispatcher.RemoveListener<int>(EventNames.UpdateEquipPosGemData, new Callback<int>(this.UpdateEquipPosGemData));
	}

	public void RefreshUI(EquipDetailedUIState state, EquipLibType.ELT pos = EquipLibType.ELT.Weapon)
	{
		this.UpdateEquipPosListPool(state, false);
		this.SetUIState(state, pos, false);
		this.RefreshPosBtnsSelectState(pos);
		this.RefreshCenterWhenSelectPos(state, pos, false);
		this.CheckBadge();
	}

	public void RefreshUIByTab(EquipDetailedUIState state)
	{
		this.UpdateEquipPosListPool(state, false);
		this.SetUIState(state, this.currentSelectPos, false);
		this.RefreshCenterWhenSelectPos(state, this.currentSelectPos, false);
		this.CheckBadge();
	}

	public void RefreshUIByPos(EquipLibType.ELT pos)
	{
		this.SetUIState(this.currentEquipDetailedUIState, pos, false);
		this.RefreshCenterWhenSelectPos(this.currentEquipDetailedUIState, pos, false);
		this.RefreshPosBtnsSelectState(pos);
		this.CheckBadge();
	}

	private void UpdateEquipPosListPool(EquipDetailedUIState equipDetailState = EquipDetailedUIState.EquipStrengthen, bool isForce = false)
	{
		this.equipPosListPool.Clear();
		this.LeftEquipPosDic.Clear();
		if (equipDetailState == EquipDetailedUIState.EquipSuitForge)
		{
			this.equipPosListSRRect.set_sizeDelta(new Vector2(this.equipPosListSRRect.get_sizeDelta().x, 520f));
			this.ListScrollMaskRect.set_sizeDelta(new Vector2(this.ListScrollMaskRect.get_sizeDelta().x, 520f));
			if (!this.equipForgingLeftRoot.get_gameObject().get_activeSelf())
			{
				this.equipForgingLeftRoot.get_gameObject().SetActive(true);
			}
			List<EquipLibType.ELT> canForgeEquipPosList = EquipGlobal.GetCanForgeEquipPosList(true);
			List<EquipLibType.ELT> canForgeEquipPosList2 = EquipGlobal.GetCanForgeEquipPosList(false);
			this.changeHighBtn.get_transform().FindChild("RedPoint").get_gameObject().SetActive(canForgeEquipPosList != null && canForgeEquipPosList.get_Count() > 0);
			this.changeLowBtn.get_transform().FindChild("RedPoint").get_gameObject().SetActive(canForgeEquipPosList2 != null && canForgeEquipPosList2.get_Count() > 0);
			EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(this.currentSelectPos);
			if (wearingEquipSimpleInfoByPos != null)
			{
				TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(wearingEquipSimpleInfoByPos.equipId);
				if (equipForgeCfgData != null && !isForce)
				{
					this.isSelectHigh = (equipForgeCfgData.suitId % 100 != 0);
				}
			}
			List<EquipLibType.ELT> list = (!this.isSelectHigh) ? canForgeEquipPosList2 : canForgeEquipPosList;
			if (list == null || list.get_Count() <= 0)
			{
				if (!isForce)
				{
					List<EquipLibType.ELT> list2 = this.isSelectHigh ? canForgeEquipPosList2 : canForgeEquipPosList;
					if (list2 != null && list2.get_Count() > 0)
					{
						this.isSelectHigh = !this.isSelectHigh;
						this.changeHighBtn.get_gameObject().SetActive(!this.isSelectHigh);
						this.changeLowBtn.get_gameObject().SetActive(this.isSelectHigh);
						if (this.noForgingLeftRoot.get_gameObject().get_activeSelf())
						{
							this.noForgingLeftRoot.get_gameObject().SetActive(false);
						}
						this.RefreshEquipLeftPosList(list2);
					}
					else if (!this.noForgingLeftRoot.get_gameObject().get_activeSelf())
					{
						this.noForgingLeftRoot.get_gameObject().SetActive(true);
					}
				}
				else if (!this.noForgingLeftRoot.get_gameObject().get_activeSelf())
				{
					this.noForgingLeftRoot.get_gameObject().SetActive(true);
				}
			}
			else
			{
				if (this.noForgingLeftRoot.get_gameObject().get_activeSelf())
				{
					this.noForgingLeftRoot.get_gameObject().SetActive(false);
				}
				this.RefreshEquipLeftPosList(list);
			}
		}
		else
		{
			this.equipPosListSRRect.set_sizeDelta(new Vector2(this.equipPosListSRRect.get_sizeDelta().x, 580f));
			this.ListScrollMaskRect.set_sizeDelta(new Vector2(this.ListScrollMaskRect.get_sizeDelta().x, 580f));
			if (this.equipForgingLeftRoot.get_gameObject().get_activeSelf())
			{
				this.equipForgingLeftRoot.get_gameObject().SetActive(false);
			}
			List<EquipLibType.ELT> list3 = new List<EquipLibType.ELT>();
			for (int i = 1; i <= 10; i++)
			{
				list3.Add((EquipLibType.ELT)i);
			}
			this.RefreshEquipLeftPosList(list3);
		}
	}

	private void RefreshEquipLeftPosList(List<EquipLibType.ELT> equipPosList)
	{
		this.equipPosListPool.Clear();
		this.LeftEquipPosDic.Clear();
		int selectIndex = 0;
		if (equipPosList != null)
		{
			this.equipPosListPool.Create(equipPosList.get_Count(), delegate(int index)
			{
				if (index < equipPosList.get_Count() && index < this.equipPosListPool.Items.get_Count())
				{
					EquipPosItem component = this.equipPosListPool.Items.get_Item(index).GetComponent<EquipPosItem>();
					ButtonCustom component2 = this.equipPosListPool.Items.get_Item(index).GetComponent<ButtonCustom>();
					if (!this.LeftEquipPosDic.ContainsKey(equipPosList.get_Item(index)))
					{
						this.LeftEquipPosDic.Add(equipPosList.get_Item(index), component);
					}
					else
					{
						this.LeftEquipPosDic.set_Item(equipPosList.get_Item(index), component);
					}
					if (component2 != null)
					{
						component2.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEquipPos);
					}
					if (component != null)
					{
						component.UpdateUIData(equipPosList.get_Item(index), this.currentEquipDetailedUIState);
						bool showTip = EquipmentManager.Instance.CheckBadageByPosAndEquipDetailedUIState(equipPosList.get_Item(index), this.currentEquipDetailedUIState);
						component.ShowTip = showTip;
						if (this.currentSelectPos == equipPosList.get_Item(index))
						{
							selectIndex = index;
							component.Selected = true;
							this.currentSelectPosItem = component;
						}
						else
						{
							component.Selected = false;
						}
					}
				}
			});
		}
	}

	private void SetUIState(EquipDetailedUIState state, EquipLibType.ELT pos, bool isShowAnim = false)
	{
		if (this.BtnEquipLeftDic.ContainsKey(this.currentEquipDetailedUIState) && this.RightEquipTransformDic.ContainsKey(this.currentEquipDetailedUIState))
		{
			this.RightEquipTransformDic.get_Item(this.currentEquipDetailedUIState).get_gameObject().SetActive(false);
			this.BtnEquipLeftDic.get_Item(this.currentEquipDetailedUIState).get_transform().FindChild("Image1").get_gameObject().SetActive(false);
			this.BtnEquipLeftDic.get_Item(this.currentEquipDetailedUIState).get_transform().FindChild("Image2").get_gameObject().SetActive(true);
			this.BtnEquipLeftDic.get_Item(this.currentEquipDetailedUIState).get_transform().FindChild("Text").GetComponent<Text>().set_color(this.LOW_LIGHT);
		}
		if (this.BtnEquipLeftDic.ContainsKey(state) && this.RightEquipTransformDic.ContainsKey(state))
		{
			this.RightEquipTransformDic.get_Item(state).get_gameObject().SetActive(true);
			this.BtnEquipLeftDic.get_Item(state).get_transform().FindChild("Image1").get_gameObject().SetActive(true);
			this.BtnEquipLeftDic.get_Item(state).get_transform().FindChild("Image2").get_gameObject().SetActive(false);
			this.BtnEquipLeftDic.get_Item(state).get_transform().FindChild("Text").GetComponent<Text>().set_color(this.HIGH_LIGHT);
		}
		switch (this.currentEquipDetailedUIState)
		{
		case EquipDetailedUIState.EquipStrengthen:
			this.EquipStrengthen.get_gameObject().SetActive(false);
			break;
		case EquipDetailedUIState.EquipStarUp:
			this.EquipStarUpCenter.get_gameObject().SetActive(false);
			break;
		}
		switch (state)
		{
		case EquipDetailedUIState.EquipStrengthen:
			this.EquipCenter.set_localPosition(new Vector3(485f, 0f, 0f));
			this.EquipStrengthen.get_gameObject().SetActive(true);
			this.RefreshRightStrengthen(pos, isShowAnim);
			if (this.OperateTip.get_gameObject().get_activeSelf())
			{
				this.OperateTip.get_gameObject().SetActive(false);
			}
			break;
		case EquipDetailedUIState.EquipGem:
			this.EquipCenter.set_localPosition(new Vector3(485f, 0f, 0f));
			if (state == EquipDetailedUIState.EquipGem && this.RightEquipTransformDic.ContainsKey(EquipDetailedUIState.EquipGem))
			{
				this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipGem).get_gameObject().SetActive(true);
				UIManagerControl.Instance.OpenUI("GemUI", this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipGem).Find("Content").Find("Image"), false, UIType.NonPush);
			}
			this.RefreshRightGem(pos);
			if (!this.OperateTip.get_gameObject().get_activeSelf())
			{
				this.OperateTip.get_gameObject().SetActive(true);
			}
			this.operateTipText.set_text(GameDataUtils.GetChineseContent(621003, false));
			break;
		case EquipDetailedUIState.EquipWash:
			this.EquipCenter.set_localPosition(new Vector3(315f, 0f, 0f));
			this.RefreshRightWash(pos);
			if (!this.OperateTip.get_gameObject().get_activeSelf())
			{
				this.OperateTip.get_gameObject().SetActive(true);
			}
			this.operateTipText.set_text(GameDataUtils.GetChineseContent(505046, false));
			break;
		case EquipDetailedUIState.EquipStarUp:
			this.EquipCenter.set_localPosition(new Vector3(485f, 0f, 0f));
			this.EquipStarUpCenter.get_gameObject().SetActive(true);
			this.RefreshRightStarUp(pos);
			if (!this.OperateTip.get_gameObject().get_activeSelf())
			{
				this.OperateTip.get_gameObject().SetActive(true);
			}
			this.operateTipText.set_text(GameDataUtils.GetChineseContent(20007, false));
			break;
		case EquipDetailedUIState.EquipEnchantment:
			this.EquipCenter.set_localPosition(new Vector3(485f, 0f, 0f));
			this.RefreshRightEnchantment(pos);
			if (!this.OperateTip.get_gameObject().get_activeSelf())
			{
				this.OperateTip.get_gameObject().SetActive(true);
			}
			this.operateTipText.set_text(GameDataUtils.GetChineseContent(20008, false));
			break;
		case EquipDetailedUIState.EquipSuitForge:
			this.EquipCenter.set_localPosition(new Vector3(315f, 0f, 0f));
			this.RefreshRightEquipSuitForge(pos);
			if (!this.OperateTip.get_gameObject().get_activeSelf())
			{
				this.OperateTip.get_gameObject().SetActive(true);
			}
			this.operateTipText.set_text(GameDataUtils.GetChineseContent(510202, false));
			break;
		}
		this.currentEquipDetailedUIState = state;
	}

	private void RefreshPosBtnsSelectState(EquipLibType.ELT pos)
	{
		if (this.LeftEquipPosDic.ContainsKey(this.currentSelectPos))
		{
			this.LeftEquipPosDic.get_Item(this.currentSelectPos).Selected = false;
		}
		if (this.LeftEquipPosDic.ContainsKey(pos))
		{
			this.LeftEquipPosDic.get_Item(pos).Selected = true;
		}
		this.currentSelectPos = pos;
	}

	public void UpdateEquipPosData(EquipDetailedUIState state, EquipLibType.ELT pos)
	{
		if (this.LeftEquipPosDic != null && this.LeftEquipPosDic.ContainsKey(this.currentSelectPos))
		{
			EquipPosItem equipPosItem = this.LeftEquipPosDic.get_Item(this.currentSelectPos);
			if (equipPosItem == null)
			{
				return;
			}
			if (state == EquipDetailedUIState.EquipEnchantment)
			{
				equipPosItem.UpdateEnchantmentData();
			}
			else if (state == EquipDetailedUIState.EquipWash)
			{
				equipPosItem.UpdateWashData();
			}
			else if (state == EquipDetailedUIState.EquipStarUp)
			{
				equipPosItem.UpdateStarUpData();
			}
			else if (state == EquipDetailedUIState.EquipStrengthen)
			{
				equipPosItem.UpdateEquipStrengthData();
			}
			else if (state == EquipDetailedUIState.EquipGem)
			{
				equipPosItem.UpdateGemData();
			}
			else if (state == EquipDetailedUIState.EquipSuitForge)
			{
				equipPosItem.UpdateEquipSuitForgeData();
			}
		}
	}

	private void RefreshCenterWhenSelectPos(EquipDetailedUIState state, EquipLibType.ELT pos, bool showStrengthAnim = false)
	{
		FXSpineManager.Instance.DeleteSpine(this.equipIconFxID, true);
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null)
		{
			return;
		}
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		if (equipSimpleInfo == null)
		{
			return;
		}
		Dictionary<string, string> iconNamesByEquipPos = EquipGlobal.GetIconNamesByEquipPos(pos, true);
		int cfgId = equipSimpleInfo.cfgId;
		int lv = equipLib.lv;
		this.EquipCenter.FindChild("TextLv").GetComponent<Text>().set_text(string.Empty);
		if (!showStrengthAnim && lv > 0 && lv > 0)
		{
			this.EquipCenter.FindChild("TextLv").GetComponent<Text>().set_text("+" + lv);
		}
		ResourceManager.SetSprite(this.EquipCenter.FindChild("ImageIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconName")));
		this.EquipCenter.FindChild("ItemStepText").GetComponent<Text>().set_text(iconNamesByEquipPos.get_Item("EquipStep"));
		Transform transform = this.EquipCenter.FindChild("ImageIcon");
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipSimpleInfo.equipId);
		if (equipSimpleInfo.suitId > 0 || (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipSuitForge && equipForgeCfgData != null))
		{
			ResourceManager.SetSprite(this.EquipCenter.FindChild("ImageFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(equipForgeCfgData.frame));
			if (transform != null)
			{
				this.equipIconFxID = FXSpineManager.Instance.PlaySpine(equipForgeCfgData.fxId, transform, "EquipDetailedUI", 2004, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
			if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipSuitForge)
			{
				this.EquipCenter.FindChild("ImageNameBG").get_gameObject().SetActive(true);
				this.EquipCenter.FindChild("TextName").get_gameObject().SetActive(true);
				this.EquipCenter.FindChild("TextName").GetComponent<Text>().set_text(EquipGlobal.GetEquipSuitMarkName(equipForgeCfgData.suitId) + GameDataUtils.GetItemName(cfgId, false, 0L));
			}
			else
			{
				if (this.EquipCenter.FindChild("TextName").get_gameObject().get_activeSelf())
				{
					this.EquipCenter.FindChild("TextName").get_gameObject().SetActive(false);
				}
				if (this.EquipCenter.FindChild("ImageNameBG").get_gameObject().get_activeSelf())
				{
					this.EquipCenter.FindChild("ImageNameBG").get_gameObject().SetActive(false);
				}
			}
		}
		else if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipSuitForge && equipForgeCfgData == null)
		{
			this.EquipCenter.FindChild("ImageNameBG").get_gameObject().SetActive(true);
			this.EquipCenter.FindChild("TextName").get_gameObject().SetActive(true);
			this.EquipCenter.FindChild("TextName").GetComponent<Text>().set_text(iconNamesByEquipPos.get_Item("ItemName"));
			ResourceManager.SetSprite(this.EquipCenter.FindChild("ImageFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconFrameName")));
		}
		else
		{
			ResourceManager.SetSprite(this.EquipCenter.FindChild("ImageFrame").GetComponent<Image>(), ResourceManager.GetIconSprite(iconNamesByEquipPos.get_Item("IconFrameName")));
			if (this.EquipCenter.FindChild("TextName").get_gameObject().get_activeSelf())
			{
				this.EquipCenter.FindChild("TextName").get_gameObject().SetActive(false);
			}
			if (this.EquipCenter.FindChild("ImageNameBG").get_gameObject().get_activeSelf())
			{
				this.EquipCenter.FindChild("ImageNameBG").get_gameObject().SetActive(false);
			}
			int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(equipLib.wearingId, 1f);
			this.equipIconFxID = EquipGlobal.GetEquipIconFX(cfgId, excellentAttrsCountByColor, transform, "EquipDetailedUI", 2004, false);
		}
		this.CheckCanShowChangeEquipTip(pos);
		this.UpdateEquipExcellentCount(equipLib.wearingId);
		this.UpdateEquipPosLevelText(lv, pos, !showStrengthAnim);
		Transform transform2 = this.EquipCenter.FindChild("ImageBinding");
		if (transform2 != null)
		{
			transform2.get_gameObject().SetActive(equipSimpleInfo.binding);
		}
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipStrengthen)
		{
			this.RefreshSuccessRatioTxt(pos, showStrengthAnim);
		}
		else if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipStarUp)
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
			int i;
			for (i = 0; i < zZhuangBeiPeiZhiBiao.starNum; i++)
			{
				this.starTransformList.get_Item(i).get_gameObject().SetActive(true);
				if (equipSimpleInfo.star > i)
				{
					this.starTransformList.get_Item(i).FindChild("OpenStar").get_gameObject().SetActive(true);
					string starLevelSpriteName = this.GetStarLevelSpriteName(equipSimpleInfo.starToMaterial.get_Item(i));
					ResourceManager.SetSprite(this.starTransformList.get_Item(i).FindChild("OpenStar").GetComponent<Image>(), ResourceManager.GetIconSprite(starLevelSpriteName));
				}
				else
				{
					this.starTransformList.get_Item(i).FindChild("OpenStar").get_gameObject().SetActive(false);
				}
			}
			for (int j = i; j < this.starTransformList.get_Count(); j++)
			{
				this.starTransformList.get_Item(j).get_gameObject().SetActive(false);
			}
			if (equipSimpleInfo.starAttrs != null && equipSimpleInfo.starAttrs.get_Count() > 0)
			{
				this.RefreshStarUpAttrText(equipSimpleInfo.starAttrs.get_Item(0));
			}
			else if (zZhuangBeiPeiZhiBiao.starNum > 0 && zZhuangBeiPeiZhiBiao.level >= this.canStarUpMinLv)
			{
				int key = zZhuangBeiPeiZhiBiao.boostStarAttr.get_Item(0);
				if (DataReader<Attrs>.Contains(key))
				{
					this.RefreshStarUpAttrText(new ExcellentAttr
					{
						attrId = DataReader<Attrs>.Get(key).attrs.get_Item(0),
						value = 0L
					});
				}
			}
			else
			{
				this.RefreshStarUpAttrText(null);
			}
			if (zZhuangBeiPeiZhiBiao.starNum <= 0 || zZhuangBeiPeiZhiBiao.level < this.canStarUpMinLv)
			{
				base.FindTransform("nextSuccessBgL").get_gameObject().SetActive(false);
			}
			else
			{
				base.FindTransform("nextSuccessBgL").get_gameObject().SetActive(true);
			}
		}
	}

	private void UpdateEquipExcellentCount(long equipID)
	{
		Transform transform = this.EquipCenter.FindChild("ExcellentAttrIconList");
		if (transform != null)
		{
			int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(equipID, 1f);
			if (excellentAttrsCountByColor > 0)
			{
				if (!transform.get_gameObject().get_activeSelf())
				{
					transform.get_gameObject().SetActive(true);
				}
				int i;
				for (i = 0; i < excellentAttrsCountByColor; i++)
				{
					if (i >= 3)
					{
						break;
					}
					transform.FindChild("Image" + (i + 1)).get_gameObject().SetActive(true);
				}
				for (int j = i; j < 3; j++)
				{
					transform.FindChild("Image" + (j + 1)).get_gameObject().SetActive(false);
				}
			}
			else if (transform.get_gameObject().get_activeSelf())
			{
				transform.get_gameObject().SetActive(false);
			}
		}
	}

	private void UpdateEquipPosLevelText(int lv, EquipLibType.ELT selectPos, bool ShowLv = true)
	{
	}

	private void CheckCanShowChangeEquipTip(EquipLibType.ELT pos)
	{
		if (EquipmentManager.Instance.CheckCanChangeEquip(pos))
		{
			if (!this.EquipCenter.FindChild("CanChangeTip").get_gameObject().get_activeSelf())
			{
				this.EquipCenter.FindChild("CanChangeTip").get_gameObject().SetActive(true);
			}
		}
		else if (this.EquipCenter.FindChild("CanChangeTip").get_gameObject().get_activeSelf())
		{
			this.EquipCenter.FindChild("CanChangeTip").get_gameObject().SetActive(false);
		}
	}

	private void RefreshRightStrengthen(EquipLibType.ELT pos, bool isShowAni = false)
	{
		int equipCfgIDByPos = EquipGlobal.GetEquipCfgIDByPos(pos);
		if (equipCfgIDByPos <= 0)
		{
			return;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		bool flag = false;
		zBuWeiQiangHua zBuWeiQiangHua = DataReader<zBuWeiQiangHua>.DataList.Find((zBuWeiQiangHua a) => a.partLv == equipLib.lv + 1);
		if (zBuWeiQiangHua == null)
		{
			flag = true;
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgIDByPos);
		if (zZhuangBeiPeiZhiBiao.step < zBuWeiQiangHua.openStep)
		{
			flag = true;
		}
		if (!flag)
		{
			this.TextCostNumStrengthen1.set_text(this.GetCostContent((long)zBuWeiQiangHua.cost.get_Item(0), EntityWorld.Instance.EntSelf.Gold));
			Items item = BackpackManager.Instance.GetItem(zBuWeiQiangHua.coinType.get_Item(0));
			int id;
			if (zBuWeiQiangHua.coinType.get_Item(0) < 5)
			{
				id = item.littleIcon;
			}
			else
			{
				id = item.icon;
			}
			ResourceManager.SetSprite(this.ImageStrengThenCost1, GameDataUtils.GetIcon(id));
		}
		Attrs attrs = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrBaseValue);
		List<int> equipmentProperties = EquipGlobal.GetEquipmentProperties(equipCfgIDByPos, equipLib.lv);
		List<int> equipmentProperties2 = EquipGlobal.GetEquipmentProperties(equipCfgIDByPos, equipLib.lv + 1);
		for (int i = 0; i < 2; i++)
		{
			Transform transform = null;
			if (i == 0)
			{
				transform = this.Attr1Strengthen;
			}
			else if (i == 1)
			{
				transform = this.Attr2Strengthen;
			}
			if (i >= attrs.attrs.get_Count())
			{
				transform.get_gameObject().SetActive(false);
			}
			else
			{
				transform.get_gameObject().SetActive(true);
				transform.FindChild("TextAttr1Upgrade").GetComponent<Text>().set_text(AttrUtility.GetAttrName(attrs.attrs.get_Item(i)));
				transform.FindChild("TextAttr1ValueNow").GetComponent<Text>().set_text(equipmentProperties.get_Item(i).ToString());
				transform.FindChild("TextAttr1ValueAfter").GetComponent<Text>().set_text(equipmentProperties2.get_Item(i).ToString());
			}
		}
		if (flag)
		{
			this.Attr1Strengthen.FindChild("TextAttr1ValueAfter").get_gameObject().SetActive(false);
			this.Attr1Strengthen.FindChild("ImageArrow1").get_gameObject().SetActive(false);
			this.Attr1Strengthen.FindChild("TextAttr1Full").get_gameObject().SetActive(true);
			this.Attr2Strengthen.FindChild("TextAttr1ValueAfter").get_gameObject().SetActive(false);
			this.Attr2Strengthen.FindChild("ImageArrow1").get_gameObject().SetActive(false);
			this.Attr2Strengthen.FindChild("TextAttr1Full").get_gameObject().SetActive(true);
			this.BtnStrengthenFull.get_gameObject().SetActive(!isShowAni);
			this.BtnStrengthen.get_gameObject().SetActive(false);
			this.BtnAutoStrengthen.get_gameObject().SetActive(false);
		}
		else
		{
			this.Attr1Strengthen.FindChild("TextAttr1ValueAfter").get_gameObject().SetActive(true);
			this.Attr1Strengthen.FindChild("ImageArrow1").get_gameObject().SetActive(true);
			this.Attr1Strengthen.FindChild("TextAttr1Full").get_gameObject().SetActive(false);
			this.Attr2Strengthen.FindChild("TextAttr1ValueAfter").get_gameObject().SetActive(true);
			this.Attr2Strengthen.FindChild("ImageArrow1").get_gameObject().SetActive(true);
			this.Attr2Strengthen.FindChild("TextAttr1Full").get_gameObject().SetActive(false);
			this.BtnStrengthenFull.get_gameObject().SetActive(false);
			this.BtnStrengthen.get_gameObject().SetActive(true);
			this.BtnAutoStrengthen.get_gameObject().SetActive(true);
		}
	}

	private void RefreshRightGem(EquipLibType.ELT pos)
	{
		GemUI gemUI = UIManagerControl.Instance.GetUIIfExist("GemUI") as GemUI;
		if (gemUI != null)
		{
			gemUI.Refresh(pos);
		}
	}

	private void RefreshRightWash(EquipLibType.ELT pos)
	{
		if (EquipmentManager.Instance.equipmentData == null || EquipmentManager.Instance.equipmentData.equipLibs == null)
		{
			return;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		List<ExcellentAttr> excellentAttrs = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).excellentAttrs;
		NoteData refineData = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).refineData;
		ExcellentAttr excellentAttr = null;
		int cfgId = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		if (excellentAttrs != null && excellentAttrs.get_Count() > 0)
		{
			base.FindTransform("NoExcellentAttr").get_gameObject().SetActive(false);
			base.FindTransform("HaveExcellentAttr").get_gameObject().SetActive(true);
			int i = 0;
			if (excellentAttrs.get_Count() <= this.CurrentSelectWashIndex)
			{
				this.CurrentSelectWashIndex = 0;
			}
			while (i < excellentAttrs.get_Count())
			{
				if (i > this.BrilliantAttrList.get_Count())
				{
					break;
				}
				string excellentTypeColor = EquipGlobal.GetExcellentTypeColor(excellentAttrs.get_Item(i).color);
				string text = string.Empty;
				text = AttrUtility.GetStandardAddDesc(excellentAttrs.get_Item(i).attrId, excellentAttrs.get_Item(i).value, excellentTypeColor, excellentTypeColor);
				string excellentRangeText = EquipGlobal.GetExcellentRangeText(zZhuangBeiPeiZhiBiao.id, excellentAttrs.get_Item(i).attrId);
				this.BrilliantAttrList.get_Item(i).get_gameObject().SetActive(true);
				this.BrilliantAttrList.get_Item(i).FindChild("AttrText").GetComponent<Text>().set_text(text);
				this.BrilliantAttrList.get_Item(i).FindChild("AttrRangeText").GetComponent<Text>().set_text(excellentRangeText);
				i++;
			}
			for (int j = i; j < this.BrilliantAttrList.get_Count(); j++)
			{
				this.BrilliantAttrList.get_Item(j).get_gameObject().SetActive(false);
			}
			string[] array = zZhuangBeiPeiZhiBiao.materialIdAndNum.Split(new char[]
			{
				';'
			});
			int k;
			for (k = 0; k < array.Length; k++)
			{
				string[] array2 = array[k].Split(new char[]
				{
					':'
				});
				int templateId = (int)float.Parse(array2[0]);
				long num = (long)float.Parse(array2[1]);
				Items item = BackpackManager.Instance.GetItem(templateId);
				base.FindTransform("WashCostRegion" + (k + 1)).get_gameObject().SetActive(true);
				Image component = base.FindTransform("WashCostRegion" + (k + 1)).FindChild("ImageWashCost").GetComponent<Image>();
				int id;
				if (item.id < 5)
				{
					id = item.littleIcon;
				}
				else
				{
					id = item.icon;
				}
				ResourceManager.SetSprite(component, GameDataUtils.GetIcon(id));
				component.SetNativeSize();
				long num2 = BackpackManager.Instance.OnGetGoodCount(templateId);
				if (num2 >= num)
				{
					base.FindTransform("WashCostRegion" + (k + 1)).FindChild("TextCostNumWash").GetComponent<Text>().set_text("x" + num);
				}
				else
				{
					base.FindTransform("WashCostRegion" + (k + 1)).FindChild("TextCostNumWash").GetComponent<Text>().set_text(TextColorMgr.GetColorByID("x" + num, 1000007));
				}
			}
			for (int l = k; l < 2; l++)
			{
				base.FindTransform("WashCostRegion" + (l + 1)).get_gameObject().SetActive(false);
			}
			int position = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).position;
			if (refineData != null && refineData.attrId != -1)
			{
				this.IsFirstWash = false;
				position = refineData.position;
				excellentAttr = new ExcellentAttr();
				excellentAttr.attrId = refineData.attrId;
				excellentAttr.value = refineData.value;
				excellentAttr.color = refineData.color;
				this.currentExcellentID = excellentAttr.attrId;
				this.currentExcellentAttr = excellentAttr;
			}
			if (this.IsFirstWash)
			{
				base.FindTransform("BtnWashChange").get_gameObject().SetActive(false);
			}
			else
			{
				base.FindTransform("BtnWashChange").get_gameObject().SetActive(true);
			}
			this.IsLockWashPos = false;
			if (position >= 0)
			{
				this.CurrentSelectWashIndex = position;
				this.IsLockWashPos = true;
			}
			if (!this.IsFirstWash)
			{
				this.IsLockWashPos = true;
			}
			this.SetRightExcellentAttrBtnListState();
			this.RefreshExtraAttrText(excellentAttr);
		}
		else
		{
			base.FindTransform("NoExcellentAttr").get_gameObject().SetActive(true);
			base.FindTransform("HaveExcellentAttr").get_gameObject().SetActive(false);
		}
		this.SetBtnWashState(false);
	}

	private void RefreshRightStarUp(EquipLibType.ELT pos)
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		if (equipSimpleInfo == null)
		{
			return;
		}
		int cfgId = equipSimpleInfo.cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return;
		}
		if (zZhuangBeiPeiZhiBiao.starNum <= 0 || zZhuangBeiPeiZhiBiao.level < this.canStarUpMinLv)
		{
			this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipStarUp).FindChild("Content").get_gameObject().SetActive(false);
			this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipStarUp).FindChild("CanNotOperation").get_gameObject().SetActive(true);
			return;
		}
		this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipStarUp).FindChild("Content").get_gameObject().SetActive(true);
		this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipStarUp).FindChild("CanNotOperation").get_gameObject().SetActive(false);
		this.RefreshStarMaterialNum();
		if (equipSimpleInfo.star < zZhuangBeiPeiZhiBiao.starNum)
		{
			base.FindTransform("NotStarUpFull").get_gameObject().SetActive(true);
			base.FindTransform("StarUpFull").get_gameObject().SetActive(false);
			int attrID = 0;
			if (this.currentSelectStarMaterialState >= 0 && this.currentSelectStarMaterialState < zZhuangBeiPeiZhiBiao.boostStarAttr.get_Count())
			{
				attrID = zZhuangBeiPeiZhiBiao.boostStarAttr.get_Item(this.currentSelectStarMaterialState);
			}
			this.RefreshStarSuccessRatioText(equipSimpleInfo.star);
			this.RefreshStarExtraAttrText(attrID);
		}
		else
		{
			base.FindTransform("StarUpFull").get_gameObject().SetActive(true);
			base.FindTransform("NotStarUpFull").get_gameObject().SetActive(false);
			base.FindTransform("starAttrTitleDown").GetComponent<Text>().set_text(string.Empty);
			base.FindTransform("NextStarUpExtraAttrText").GetComponent<Text>().set_text(string.Empty);
		}
		this.SetBtnStarUpState(false);
	}

	private void RefreshRightEnchantment(EquipLibType.ELT pos)
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		int cfgId = equipSimpleInfo.cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		if (zZhuangBeiPeiZhiBiao.enchantNum <= 0 || zZhuangBeiPeiZhiBiao.level < this.canEnchantmentMinLv)
		{
			this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipEnchantment).FindChild("Content").get_gameObject().SetActive(false);
			this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipEnchantment).FindChild("CanNotOperation").get_gameObject().SetActive(true);
			return;
		}
		this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipEnchantment).FindChild("Content").get_gameObject().SetActive(true);
		this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipEnchantment).FindChild("CanNotOperation").get_gameObject().SetActive(false);
		int i;
		for (i = 0; i < zZhuangBeiPeiZhiBiao.enchantNum; i++)
		{
			if (i >= this.enchantmentItemList.get_Count())
			{
				break;
			}
			this.enchantmentItemList.get_Item(i).get_gameObject().SetActive(true);
			bool active = EquipmentManager.Instance.CheckCanShowEnchantmentTipBySlotIndex(i, pos);
			this.enchantmentItemList.get_Item(i).FindChild("ImageBadge").get_gameObject().SetActive(active);
			Transform transform = this.enchantmentItemList.get_Item(i).FindChild("HaveEnchantment");
			Transform transform2 = this.enchantmentItemList.get_Item(i).FindChild("NotEnchantment");
			GameObject gameObject = this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipEnchantment).FindChild("Content").FindChild("NotEnchantment" + (i + 1)).get_gameObject();
			if (gameObject.get_activeSelf())
			{
				gameObject.SetActive(false);
			}
			if (i >= equipSimpleInfo.enchantAttrs.get_Count())
			{
				transform.get_gameObject().SetActive(false);
				transform2.get_gameObject().SetActive(true);
			}
			else if (equipSimpleInfo.enchantAttrs.get_Item(i).attrId > 0)
			{
				transform.get_gameObject().SetActive(true);
				transform2.get_gameObject().SetActive(false);
				int attrId = equipSimpleInfo.enchantAttrs.get_Item(i).attrId;
				Items items = DataReader<Items>.Get(attrId);
				if (items != null)
				{
					int num = items.color;
					if (num == 0)
					{
						num = 1;
					}
					ResourceManager.SetSprite(transform.FindChild("ImageFrame").GetComponent<Image>(), GameDataUtils.GetItemFrameByColor(num));
					ResourceManager.SetSprite(transform.FindChild("ImageIcon").GetComponent<Image>(), GameDataUtils.GetIcon(items.icon));
					transform.FindChild("ItemName").GetComponent<Text>().set_text(GameDataUtils.GetItemName(attrId, true, 0L));
					FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(attrId);
					if (fuMoDaoJuShuXing != null)
					{
						string text = "当前装备";
						if (fuMoDaoJuShuXing.valueType == 0)
						{
							string text2 = text;
							text = string.Concat(new object[]
							{
								text2,
								AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr),
								" +",
								(float)(equipSimpleInfo.enchantAttrs.get_Item(i).value * 100L) / 1000f,
								"%"
							});
						}
						else
						{
							string text2 = text;
							text = string.Concat(new object[]
							{
								text2,
								AttrUtility.GetAttrName((GameData.AttrType)fuMoDaoJuShuXing.runeAttr),
								" +",
								equipSimpleInfo.enchantAttrs.get_Item(i).value
							});
						}
						transform.FindChild("ItemAttr").GetComponent<Text>().set_text(text);
						string[] array = fuMoDaoJuShuXing.describe.Split(new char[]
						{
							';'
						});
						transform.FindChild("ItemAttrRange").GetComponent<Text>().set_text(string.Concat(new string[]
						{
							"（",
							array[0],
							"-",
							array[1],
							"）"
						}));
					}
				}
			}
			else
			{
				transform.get_gameObject().SetActive(false);
				transform2.get_gameObject().SetActive(true);
			}
		}
		for (int j = i; j < this.enchantmentItemList.get_Count(); j++)
		{
			this.enchantmentItemList.get_Item(j).get_gameObject().SetActive(false);
		}
		for (int k = i; k < 3; k++)
		{
			GameObject gameObject2 = this.RightEquipTransformDic.get_Item(EquipDetailedUIState.EquipEnchantment).FindChild("Content").FindChild("NotEnchantment" + (k + 1)).get_gameObject();
			if (!gameObject2.get_activeSelf())
			{
				gameObject2.SetActive(true);
			}
		}
	}

	private void RefreshRightEquipSuitForge(EquipLibType.ELT pos)
	{
		this.UpdateEquipPosForgeInfo(pos);
	}

	private void RefreshExtraAttrText(ExcellentAttr excellentAttr = null)
	{
		if (excellentAttr == null)
		{
			base.FindTransform("ExtraAttrText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505030, false));
		}
		else
		{
			EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
			int cfgId = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).cfgId;
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
			string excellentRangeText = EquipGlobal.GetExcellentRangeText(zZhuangBeiPeiZhiBiao.id, excellentAttr.attrId);
			string excellentTypeColor = EquipGlobal.GetExcellentTypeColor(excellentAttr.color);
			string text = string.Empty;
			text = AttrUtility.GetStandardAddDesc(excellentAttr.attrId, excellentAttr.value, excellentTypeColor, excellentTypeColor);
			base.FindTransform("ExtraAttrText").GetComponent<Text>().set_text(string.Concat(new string[]
			{
				string.Empty,
				text,
				"<size=18>",
				excellentRangeText,
				"</size>"
			}));
		}
	}

	private void SetRightExcellentAttrBtnListState()
	{
		for (int i = 0; i < this.BrilliantAttrList.get_Count(); i++)
		{
			this.BrilliantAttrList.get_Item(i).FindChild("BrilliantSelectImg").get_gameObject().SetActive(false);
			this.BrilliantAttrList.get_Item(i).FindChild("BrilliantLockImg").get_gameObject().SetActive(false);
			if (this.CurrentSelectWashIndex == i)
			{
				if (this.IsLockWashPos)
				{
					this.BrilliantAttrList.get_Item(i).FindChild("BrilliantLockImg").get_gameObject().SetActive(true);
				}
				else
				{
					this.BrilliantAttrList.get_Item(i).FindChild("BrilliantSelectImg").get_gameObject().SetActive(true);
				}
			}
			else if (this.IsLockWashPos)
			{
			}
		}
	}

	public void ShowIsSaveWashResult(EquipDetailedUIState equipState, EquipLibType.ELT partState, bool isClickTab = false)
	{
		if (!this.IsFirstWash)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(651056, false), null, delegate
			{
				this.IsFirstWash = true;
				EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
				if (equipLib != null)
				{
					EquipmentManager.Instance.SendCancelRefineDataReq((int)this.currentSelectPos, equipLib.wearingId);
					if (isClickTab)
					{
						this.RefreshUIByTab(equipState);
					}
					else
					{
						this.RefreshUIByPos(partState);
					}
				}
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
		else if (isClickTab)
		{
			this.RefreshUIByTab(equipState);
		}
		else
		{
			this.RefreshUIByPos(partState);
		}
	}

	private void RefreshSuccessRatioTxt(EquipLibType.ELT pos, bool showAnim = false)
	{
		if (EquipmentManager.Instance.equipmentData == null)
		{
			return;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null)
		{
			return;
		}
		zBuWeiQiangHua zBuWeiQiangHua = DataReader<zBuWeiQiangHua>.DataList.Find((zBuWeiQiangHua a) => a.partLv == equipLib.lv + 1);
		int cfgId = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		this.lastStrengthLv = EquipmentManager.Instance.LastLv;
		this.currentRollStrengthLv = this.lastStrengthLv;
		this.RemoveStrengthSuccessRationAni();
		this.nextSuccessRatioTxt.set_text(string.Empty);
		if (this.currentRollStrengthLv > 0 && showAnim)
		{
			this.EquipCenter.get_transform().FindChild("TextLv").GetComponent<Text>().set_text("+" + this.currentRollStrengthLv);
			this.UpdateEquipPosLevelText(this.currentRollStrengthLv, this.currentSelectPos, true);
		}
		if (zBuWeiQiangHua == null)
		{
			this.finalProgressTextContent = GameDataUtils.GetChineseContent(505031, false);
			this.SetSuccessRationAmount(1f, equipLib.lv - this.lastStrengthLv, showAnim);
			return;
		}
		if (zZhuangBeiPeiZhiBiao.step < zBuWeiQiangHua.openStep)
		{
			this.finalProgressTextContent = string.Format("MAX(超出{0})", equipLib.blessValue);
			this.SetSuccessRationAmount(1f, equipLib.lv - this.lastStrengthLv, showAnim);
			return;
		}
		float num = (float)equipLib.blessValue / ((float)zBuWeiQiangHua.proficiency * 1f);
		num = ((num <= 1f) ? num : 1f);
		if (num >= 1f)
		{
			this.finalProgressTextContent = string.Format("MAX(超出{0})", equipLib.blessValue);
		}
		else
		{
			this.finalProgressTextContent = equipLib.blessValue + "/" + zBuWeiQiangHua.proficiency;
		}
		this.SetSuccessRationAmount(num, equipLib.lv - this.lastStrengthLv, showAnim);
	}

	private string GetCostContent(long need, long have)
	{
		string result = string.Empty;
		if (need > have)
		{
			result = "<color=red>x" + need + "</color>";
		}
		else
		{
			result = "x" + need.ToString();
		}
		return result;
	}

	private void SetSuccessRationAmount(float successRation, int upLv, bool anim = false)
	{
		if (anim)
		{
			this.m_Actions.Add(delegate
			{
				this.lastSuccessRatioAmount = successRation;
				this.SetSuccessRatioDelta(upLv);
				this.SetSuccessBlessValueDelta(this.currentRollStrengthLv);
				this.PlayStrengthSuccessRationAnimation(upLv);
			});
			this.CheckSuccessRationAnimation();
		}
		else
		{
			this.strengthProgressSlider.set_value(successRation);
			this.nextSuccessRatioTxt.set_text(this.finalProgressTextContent);
		}
	}

	private void SetSuccessRatioDelta(int upLv)
	{
		int num = 25;
		int num2 = (upLv <= 0) ? 1 : upLv;
		float num3 = (float)(upLv * 1) + (this.lastSuccessRatioAmount - this.strengthProgressSlider.get_value());
		this.successRatioDelta = num3 / (float)(num * num2);
	}

	private void SetSuccessBlessValueDelta(int strengthLv)
	{
		this.succesBlessValue = 0;
		zBuWeiQiangHua zBuWeiQiangHua = DataReader<zBuWeiQiangHua>.DataList.Find((zBuWeiQiangHua a) => a.partLv == strengthLv + 1);
		if (zBuWeiQiangHua == null)
		{
			zBuWeiQiangHua = DataReader<zBuWeiQiangHua>.DataList.Find((zBuWeiQiangHua a) => a.partLv == strengthLv);
		}
		if (zBuWeiQiangHua != null)
		{
			this.successBlessValueDelta = zBuWeiQiangHua.proficiency / 50;
			this.currentLvSuccessBlessValue = zBuWeiQiangHua.proficiency;
		}
		this.successBlessValueDelta = ((this.successBlessValueDelta <= 0) ? 100 : this.successBlessValueDelta);
		this.currentLvSuccessBlessValue = ((this.currentLvSuccessBlessValue <= 0) ? 1300 : this.currentLvSuccessBlessValue);
	}

	private void PlayStrengthSuccessRationAnimation(int upLv)
	{
		TimerHeap.DelTimer(this.successProgressTimerID);
		this.ResetStrengthFXSetting(true);
		this.successProgressTimerID = TimerHeap.AddTimer(0u, 30, delegate
		{
			if (upLv > 0)
			{
				Slider expr_17 = this.strengthProgressSlider;
				expr_17.set_value(expr_17.get_value() + this.successRatioDelta);
				if (this.strengthProgressSlider.get_value() >= 0.2f && this.strengthProgressSlider.get_value() < 0.22f)
				{
					this.PlayStrengthBarExploredFX();
				}
				if (this.strengthProgressSlider.get_value() >= 0.4f && this.strengthProgressSlider.get_value() < 0.42f)
				{
					this.PlayStrengthBarExploredFX();
				}
				if (this.strengthProgressSlider.get_value() >= 0.6f && this.strengthProgressSlider.get_value() < 0.62f)
				{
					this.PlayStrengthBarExploredFX();
				}
				if (this.strengthProgressSlider.get_value() >= 0.8f && this.strengthProgressSlider.get_value() < 0.82f)
				{
					this.PlayStrengthBarExploredFX();
				}
				this.succesBlessValue += this.successBlessValueDelta;
				this.nextSuccessRatioTxt.set_text(this.succesBlessValue + "/" + this.currentLvSuccessBlessValue);
				if (this.strengthProgressSlider.get_value() >= 1f)
				{
					this.strengthProgressSlider.set_value(this.strengthProgressSlider.get_value() - 1f);
					this.PlayProgressBarFullFX();
					this.PlayStrengthSuccessRationAnimation(upLv - 1);
				}
			}
			else if (this.strengthProgressSlider.get_value() < this.lastSuccessRatioAmount && this.strengthProgressSlider.get_value() + this.successRatioDelta < this.lastSuccessRatioAmount)
			{
				Slider expr_23B = this.strengthProgressSlider;
				expr_23B.set_value(expr_23B.get_value() + this.successRatioDelta);
				if (this.strengthProgressSlider.get_value() >= 0.2f && this.strengthProgressSlider.get_value() < 0.22f)
				{
					this.PlayStrengthBarExploredFX();
				}
				if (this.strengthProgressSlider.get_value() >= 0.4f && this.strengthProgressSlider.get_value() < 0.42f)
				{
					this.PlayStrengthBarExploredFX();
				}
				if (this.strengthProgressSlider.get_value() >= 0.6f && this.strengthProgressSlider.get_value() < 0.62f)
				{
					this.PlayStrengthBarExploredFX();
				}
				if (this.strengthProgressSlider.get_value() >= 0.8f && this.strengthProgressSlider.get_value() < 0.82f)
				{
					this.PlayStrengthBarExploredFX();
				}
				this.succesBlessValue += this.successBlessValueDelta;
				this.nextSuccessRatioTxt.set_text(this.succesBlessValue + "/" + this.currentLvSuccessBlessValue);
			}
			else
			{
				this.strengthProgressSlider.set_value(this.lastSuccessRatioAmount);
				this.nextSuccessRatioTxt.set_text(this.finalProgressTextContent);
				EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
				if (equipLib != null)
				{
					if (equipLib.lv > 0)
					{
						this.EquipCenter.get_transform().FindChild("TextLv").GetComponent<Text>().set_text("+" + equipLib.lv);
					}
					this.UpdateEquipPosLevelText(equipLib.lv, this.currentSelectPos, true);
				}
				TimerHeap.DelTimer(this.successProgressTimerID);
				this.successProgressTimerID = 0u;
				this.CheckSuccessRationAnimation();
				this.SetUIState(this.currentEquipDetailedUIState, this.currentSelectPos, false);
			}
		});
	}

	private void PlayStrengthBarExploredFX()
	{
		FXSpineManager.Instance.ReplaySpine(this.strengthBarExploredFxID, 3801, this.ExploredFXRoot, "EquipDetailedUI", 2007, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void PlayProgressBarFullFX()
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
		if (equipLib != null)
		{
			this.currentRollStrengthLv = ((this.currentRollStrengthLv + 1 <= equipLib.lv) ? (this.currentRollStrengthLv + 1) : equipLib.lv);
			this.SetSuccessBlessValueDelta(this.currentRollStrengthLv);
			this.EquipCenter.get_transform().FindChild("TextLv").GetComponent<Text>().set_text("+" + this.currentRollStrengthLv);
			this.UpdateEquipPosLevelText(this.currentRollStrengthLv, this.currentSelectPos, true);
		}
		Transform host = base.FindTransform("StrengthSliderFX");
		FXSpineManager.Instance.ReplaySpine(this.progressBarFullFxID, 2206, host, "EquipDetailedUI", 2007, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.ReplaySpine(this.strengthSuccessFxID, 102, this.EquipCenter.FindChild("FX"), "EquipDetailedUI", 2006, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void CheckSuccessRationAnimation()
	{
		if (this.successProgressTimerID > 0u)
		{
			return;
		}
		if (this.m_Actions.get_Count() == 0)
		{
			this.RemoveStrengthSuccessRationAni();
			this.ResetStrengthFXSetting(false);
			return;
		}
		this.m_Actions.get_Item(0).Invoke();
		this.m_Actions.RemoveAt(0);
	}

	private void RemoveStrengthSuccessRationAni()
	{
		TimerHeap.DelTimer(this.successProgressTimerID);
		this.RemoveStrengthFXSpine();
		this.m_Actions.Clear();
		this.successProgressTimerID = 0u;
	}

	private void ResetStrengthFXSetting(bool isShow = false)
	{
		TimerHeap.DelTimer(this.fx_mask_timer_id);
		if (isShow)
		{
			this.fx_mask_timer_id = TimerHeap.AddTimer(10000u, 0, delegate
			{
				TimerHeap.DelTimer(this.fx_mask_timer_id);
				this.ResetStrengthFXSetting(false);
			});
		}
		this.strengthFXMaskTrans.get_gameObject().SetActive(isShow);
		this.BtnStrengthen.set_enabled(!isShow);
		this.BtnAutoStrengthen.set_enabled(!isShow);
		string spriteName = isShow ? "button_gray_1" : "button_yellow_1";
		ResourceManager.SetIconSprite(this.BtnAutoStrengthen.get_transform().FindChild("Image").GetComponent<Image>(), spriteName);
		ResourceManager.SetIconSprite(this.BtnStrengthen.get_transform().FindChild("Image").GetComponent<Image>(), spriteName);
	}

	private void RemoveStrengthFXSpine()
	{
		FXSpineManager.Instance.DeleteSpine(this.progressBarFullFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.strengthSuccessFxID, true);
		FXSpineManager.Instance.DeleteSpine(this.strengthBarExploredFxID, true);
	}

	private void RefreshStarUpAttrText(ExcellentAttr excellentAttr = null)
	{
		if (excellentAttr == null)
		{
			base.FindTransform("CurrentStarUpExtraAttrText").GetComponent<Text>().set_text(string.Empty);
		}
		else
		{
			base.FindTransform("CurrentStarUpExtraAttrText").GetComponent<Text>().set_text(string.Concat(new string[]
			{
				GameDataUtils.GetChineseContent(505032, false),
				AttrUtility.GetAttrName((GameData.AttrType)excellentAttr.attrId),
				GameDataUtils.GetChineseContent(505033, false),
				"：",
				AttrUtility.GetAttrValueDisplay((GameData.AttrType)excellentAttr.attrId, excellentAttr.value)
			}));
		}
	}

	private void RefreshStarExtraAttrText(int attrID = 0)
	{
		base.FindTransform("starAttrTitleDown").GetComponent<Text>().set_text("升星属性:");
		if (attrID > 0)
		{
			Attrs attrs = DataReader<Attrs>.Get(attrID);
			base.FindTransform("NextStarUpExtraAttrText").GetComponent<Text>().set_text(string.Empty);
			if (attrs == null)
			{
				return;
			}
			base.FindTransform("NextStarUpExtraAttrText").GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc(attrs.attrs.get_Item(0), attrs.values.get_Item(0), "ff7d4b", 3));
		}
		else
		{
			base.FindTransform("NextStarUpExtraAttrText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505034, false));
		}
	}

	private void RefreshStarSuccessRatioText(int currentStatNum)
	{
		if (currentStatNum >= 15)
		{
			return;
		}
		int num = 0;
		string text = string.Empty;
		switch (this.currentSelectStarMaterialState)
		{
		case 0:
			text = "copper";
			break;
		case 1:
			text = "silver";
			break;
		case 2:
			text = "gold";
			break;
		}
		if (text != string.Empty)
		{
			num = DataReader<ShengXingJiChuPeiZhi>.Get(text).value.get_Item(currentStatNum);
		}
		base.FindTransform("SuccessStarUpRatioTxt").GetComponent<Text>().set_text(string.Empty + num + "%");
	}

	private void RefreshStarUpStoneItem()
	{
		Transform transform = base.FindTransform("StarStoneItemRegion");
		if (transform == null)
		{
			return;
		}
		this.currentSelectStarMaterialState = -1;
		for (int i = 0; i < this.starMaterialIDList.get_Count(); i++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(WidgetName.PetChooseItem);
			instantiate2Prefab.set_name("Item_" + this.starMaterialIDList.get_Item(i));
			PetID component = instantiate2Prefab.GetComponent<PetID>();
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.get_transform().SetParent(transform);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
			instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectStarMaterialBtn);
			component.SetItemData(this.starMaterialIDList.get_Item(i));
			if (i == 0)
			{
				this.OnClickSelectStarMaterialBtn(instantiate2Prefab);
			}
		}
	}

	private void RefreshStarMaterialNum()
	{
		for (int i = 0; i < this.starMaterialIDList.get_Count(); i++)
		{
			long num = BackpackManager.Instance.OnGetGoodCount(this.starMaterialIDList.get_Item(i));
			Transform transform = base.FindTransform("StarStoneItemRegion");
			transform.GetChild(i).FindChild("Num").GetComponent<Text>().set_text(num.ToString());
		}
	}

	private void UpdateEquipPosForgeInfo(EquipLibType.ELT pos)
	{
		this.suitNameTitle.set_text(string.Empty);
		this.checkAttrListPool.Clear();
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(pos);
		if (wearingEquipSimpleInfoByPos == null)
		{
			return;
		}
		this.currentForgingCfg = EquipGlobal.GetEquipForgeCfgData(wearingEquipSimpleInfoByPos.equipId);
		bool flag = EquipGlobal.CheckEquipCanForging(pos) && this.currentForgingCfg != null;
		GameObject gameObject = base.FindTransform("NoForgingRightRoot").get_gameObject();
		if (gameObject.get_activeSelf() == flag)
		{
			gameObject.SetActive(!flag);
		}
		this.noForgingTipRightText.set_text(this.isSelectHigh ? GameDataUtils.GetChineseContent(510206, false) : GameDataUtils.GetChineseContent(510200, false));
		if (flag)
		{
			if (!this.forgeBtn.get_gameObject().get_activeSelf())
			{
				this.forgeBtn.get_gameObject().SetActive(true);
			}
			if (wearingEquipSimpleInfoByPos.suitId <= 0)
			{
				this.forgeBtn.set_enabled(true);
				ImageColorMgr.SetImageColor(this.forgeBtn.GetComponent<Image>(), false);
				this.btnForgeNameText.set_text(GameDataUtils.GetChineseContent(510201, false));
			}
			else if (wearingEquipSimpleInfoByPos.suitId > 0)
			{
				this.forgeBtn.set_enabled(false);
				ImageColorMgr.SetImageColor(this.forgeBtn.GetComponent<Image>(), true);
				this.btnForgeNameText.set_text(GameDataUtils.GetChineseContent(510203, false));
			}
			List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
			if (this.currentForgingCfg != null)
			{
				this.suitNameTitle.set_text(string.Concat(new object[]
				{
					GameDataUtils.GetChineseContent(this.currentForgingCfg.suitName, false),
					"\n（",
					EquipGlobal.GetEquipSuitNumByID(this.currentForgingCfg.suitId),
					"/",
					EquipGlobal.GetEquipSuitMaxNum(this.currentForgingCfg.suitId),
					"）"
				}));
				for (int i = 0; i < this.currentForgingCfg.suitcost.get_Count(); i++)
				{
					list.Add(new KeyValuePair<int, int>(this.currentForgingCfg.suitcost.get_Item(i).key, this.currentForgingCfg.suitcost.get_Item(i).value));
				}
			}
			this.RefreshSuitAttrRegion(pos);
			this.RefreshForgeCoinRegion(list);
		}
		else
		{
			if (this.forgeBtn.get_gameObject().get_activeSelf())
			{
				this.forgeBtn.get_gameObject().SetActive(false);
			}
			this.currentForgingCfg = null;
			this.RefreshForgeCoinRegion(null);
		}
	}

	private void RefreshForgeCoinRegion(List<KeyValuePair<int, int>> needMaterialList)
	{
		this.forgeCoinListPool.Clear();
		if (needMaterialList != null)
		{
			this.forgeCoinListPool.Create(needMaterialList.get_Count(), delegate(int index)
			{
				if (index < needMaterialList.get_Count() && index < this.forgeCoinListPool.Items.get_Count())
				{
					long num = BackpackManager.Instance.OnGetGoodCount(needMaterialList.get_Item(index).get_Key());
					RewardItem component = this.forgeCoinListPool.Items.get_Item(index).GetComponent<RewardItem>();
					component.SetRewardItem(needMaterialList.get_Item(index).get_Key(), (long)needMaterialList.get_Item(index).get_Value(), 0L);
					if (num < (long)needMaterialList.get_Item(index).get_Value())
					{
						this.forgeCoinListPool.Items.get_Item(index).get_transform().FindChild("RewardItemText").GetComponent<Text>().set_text(TextColorMgr.GetColorByID(needMaterialList.get_Item(index).get_Value() + "/" + num, 1000007));
					}
					else
					{
						this.forgeCoinListPool.Items.get_Item(index).get_transform().FindChild("RewardItemText").GetComponent<Text>().set_text(needMaterialList.get_Item(index).get_Value() + "/" + num);
					}
				}
			});
		}
	}

	private void RefreshSuitAttrRegion(EquipLibType.ELT pos)
	{
		this.checkAttrListPool.Clear();
		List<TaoZhuangDuanZhu> equipSuitAttrList = EquipGlobal.GetEquipSuitAttrList(pos);
		if (equipSuitAttrList == null && equipSuitAttrList.get_Count() <= 0)
		{
			return;
		}
		this.checkAttrListPool.Create(equipSuitAttrList.get_Count(), delegate(int index)
		{
			if (index < equipSuitAttrList.get_Count() && index < this.checkAttrListPool.Items.get_Count())
			{
				GameObject gameObject = this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitActiveRoot").get_gameObject();
				bool flag = EquipGlobal.CheckLowSuitIsActive(equipSuitAttrList.get_Item(index).id);
				if (flag != gameObject.get_activeSelf())
				{
					gameObject.SetActive(flag);
				}
				if (!flag)
				{
					bool flag2 = EquipGlobal.CheckSuitIsActive(equipSuitAttrList.get_Item(index).id);
					string text = "#c1c1c1";
					if (equipSuitAttrList.get_Item(index).suitQuality == 5)
					{
						text = ((!flag2) ? "#c1c1c1" : "#ff9759");
						this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitNameHighBg").get_gameObject().SetActive(false);
						this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitNameLowBg").get_gameObject().SetActive(flag2);
					}
					else if (equipSuitAttrList.get_Item(index).suitQuality == 6)
					{
						text = ((!flag2) ? "#c1c1c1" : "#feda52");
						this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitNameLowBg").get_gameObject().SetActive(false);
						this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitNameHighBg").get_gameObject().SetActive(flag2);
					}
					Text component = this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitName").GetComponent<Text>();
					component.set_text(string.Concat(new object[]
					{
						"<color=",
						text,
						">【",
						equipSuitAttrList.get_Item(index).suitNeedNum,
						"件】</color>"
					}));
					Transform transform = this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitAttrList");
					int i = 0;
					if (equipSuitAttrList.get_Item(index).effectType == 2)
					{
						this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitSkillItemText").GetComponent<Text>().set_text(string.Concat(new string[]
						{
							"<color=",
							text,
							">",
							GameDataUtils.GetChineseContent(equipSuitAttrList.get_Item(index).skillDesc, false),
							"</color>"
						}));
					}
					else
					{
						this.checkAttrListPool.Items.get_Item(index).get_transform().FindChild("SuitSkillItemText").GetComponent<Text>().set_text(string.Empty);
						if (DataReader<Attrs>.Contains(equipSuitAttrList.get_Item(index).suitattr))
						{
							Attrs attrs = DataReader<Attrs>.Get(equipSuitAttrList.get_Item(index).suitattr);
							while (i < attrs.attrs.get_Count())
							{
								if (i >= attrs.attrs.get_Count())
								{
									break;
								}
								Transform transform2 = transform.FindChild("AttrItem2Text" + i);
								if (transform2 != null)
								{
									transform2.FindChild("Item2Text").GetComponent<Text>().set_text(string.Concat(new string[]
									{
										"<color=",
										text,
										">",
										AttrUtility.GetStandardAddDesc(attrs.attrs.get_Item(i), attrs.values.get_Item(i)),
										"</color>"
									}));
								}
								i++;
							}
						}
					}
					for (int j = i; j < 3; j++)
					{
						transform.FindChild("AttrItem2Text" + j).FindChild("Item2Text").GetComponent<Text>().set_text(string.Empty);
					}
				}
				else
				{
					gameObject.get_transform().FindChild("SuitActiveTips").GetComponent<Text>().set_text("已激活传说套装属性");
				}
			}
		});
	}

	private void OnClickBtnEquipStrengthen(GameObject sender)
	{
		if (!SystemOpenManager.IsSystemClickOpen(21, 0, true))
		{
			return;
		}
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipStrengthen)
		{
			return;
		}
		this.ShowIsSaveWashResult(EquipDetailedUIState.EquipStrengthen, this.currentSelectPos, true);
	}

	private void OnClickBtnEquipGem(GameObject sender)
	{
		int level = DataReader<SystemOpen>.Get(22).level;
		if (EntityWorld.Instance.EntSelf.Lv < level)
		{
			string text = string.Format(GameDataUtils.GetChineseContent(606121, false), level);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
		}
		else if (this.currentEquipDetailedUIState != EquipDetailedUIState.EquipGem)
		{
			this.ShowIsSaveWashResult(EquipDetailedUIState.EquipGem, this.currentSelectPos, true);
		}
	}

	private void OnClickBtnEquipWash(GameObject sender)
	{
		if (!SystemOpenManager.IsSystemOn(38))
		{
			int level = DataReader<SystemOpen>.Get(38).level;
			string text = string.Format(GameDataUtils.GetChineseContent(606121, false), level);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
			return;
		}
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipWash)
		{
			return;
		}
		this.ShowIsSaveWashResult(EquipDetailedUIState.EquipWash, this.currentSelectPos, true);
	}

	private void OnClickBtnEquipStarUp(GameObject sender)
	{
		if (!SystemOpenManager.IsSystemOn(37))
		{
			int level = DataReader<SystemOpen>.Get(37).level;
			string text = string.Format(GameDataUtils.GetChineseContent(606121, false), level);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
			return;
		}
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipStarUp)
		{
			return;
		}
		if (EquipmentManager.Instance.CanShowStarUpTipPosDic.ContainsKey(this.currentSelectPos) && EquipmentManager.Instance.CanShowStarUpTipPosDic.get_Item(this.currentSelectPos))
		{
			EquipmentManager.Instance.CanShowStarUpTipPosDic.set_Item(this.currentSelectPos, false);
			this.CheckBadge();
		}
		this.ShowIsSaveWashResult(EquipDetailedUIState.EquipStarUp, this.currentSelectPos, true);
	}

	private void OnClickBtnEquipEnchantment(GameObject sender)
	{
		if (!SystemOpenManager.IsSystemOn(39))
		{
			int level = DataReader<SystemOpen>.Get(39).level;
			string text = string.Format(GameDataUtils.GetChineseContent(606121, false), level);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
			return;
		}
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipEnchantment)
		{
			return;
		}
		if (EquipmentManager.Instance.CanShowEnchantmentTipPosDic.ContainsKey(this.currentSelectPos) && EquipmentManager.Instance.CanShowEnchantmentTipPosDic.get_Item(this.currentSelectPos))
		{
			EquipmentManager.Instance.CanShowEnchantmentTipPosDic.set_Item(this.currentSelectPos, false);
			this.CheckBadge();
		}
		this.ShowIsSaveWashResult(EquipDetailedUIState.EquipEnchantment, this.currentSelectPos, true);
	}

	private void OnClickBtnEquipSuitForge(GameObject sender)
	{
		if (!SystemOpenManager.IsSystemOn(78))
		{
			int level = DataReader<SystemOpen>.Get(78).level;
			string text = string.Format(GameDataUtils.GetChineseContent(606121, false), level);
			UIManagerControl.Instance.ShowToastText(text, 2f, 2f);
			return;
		}
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipSuitForge)
		{
			return;
		}
		if (EquipmentManager.Instance.CanShowEnchantmentTipPosDic.ContainsKey(this.currentSelectPos) && EquipmentManager.Instance.CanShowEnchantmentTipPosDic.get_Item(this.currentSelectPos))
		{
			EquipmentManager.Instance.CanShowEnchantmentTipPosDic.set_Item(this.currentSelectPos, false);
			this.CheckBadge();
		}
		this.ShowIsSaveWashResult(EquipDetailedUIState.EquipSuitForge, this.currentSelectPos, true);
	}

	private void OnClickEquipPos(GameObject go)
	{
		EquipPosItem component = go.GetComponent<EquipPosItem>();
		if (component != null)
		{
			if (component.EquipPos == this.currentSelectPos)
			{
				return;
			}
			if (this.currentSelectPosItem != null)
			{
				this.currentSelectPosItem.Selected = false;
			}
			this.currentSelectPosItem = component;
			component.Selected = true;
			if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipEnchantment)
			{
				if (EquipmentManager.Instance.CanShowEnchantmentTipPosDic.ContainsKey(component.EquipPos) && EquipmentManager.Instance.CanShowEnchantmentTipPosDic.get_Item(component.EquipPos))
				{
					EquipmentManager.Instance.CanShowEnchantmentTipPosDic.set_Item(component.EquipPos, false);
					this.CheckBadge();
				}
			}
			else if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipStarUp && EquipmentManager.Instance.CanShowStarUpTipPosDic.ContainsKey(component.EquipPos) && EquipmentManager.Instance.CanShowStarUpTipPosDic.get_Item(component.EquipPos))
			{
				EquipmentManager.Instance.CanShowStarUpTipPosDic.set_Item(component.EquipPos, false);
				this.CheckBadge();
			}
			this.ShowIsSaveWashResult(this.currentEquipDetailedUIState, component.EquipPos, false);
		}
	}

	private void OnClickBtnAutoStrengthen(GameObject go)
	{
		EquipmentManager.Instance.SendAutoIntensifyPositionReq((int)this.currentSelectPos);
	}

	private void OnClickBtnStrengthen(GameObject sender)
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
		zBuWeiQiangHua zBuWeiQiangHua = DataReader<zBuWeiQiangHua>.DataList.Find((zBuWeiQiangHua a) => a.partLv == equipLib.lv + 1);
		if (zBuWeiQiangHua == null)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510102, false));
			return;
		}
		int cfgId = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return;
		}
		if (zZhuangBeiPeiZhiBiao.step < zBuWeiQiangHua.openStep)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510102, false));
			return;
		}
		if (this.IsCanSendInIntensify)
		{
			EquipmentManager.Instance.SendIntensifyPositionReq((int)this.currentSelectPos, 0);
		}
		if (this.IsCanSendInIntensify)
		{
			this.IsCanSendInIntensify = false;
			TimerHeap.AddTimer(500u, 0, delegate
			{
				this.IsCanSendInIntensify = true;
			});
		}
	}

	private void OnClickSelectExcellentAttrPosBtn(GameObject go)
	{
		int num = this.BrilliantAttrList.FindIndex((Transform a) => a == go.get_transform());
		if (this.IsLockWashPos)
		{
			if (num != this.CurrentSelectWashIndex)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505045, false));
			}
			return;
		}
		if (num == this.CurrentSelectWashIndex)
		{
			return;
		}
		if (num != -1)
		{
			this.CurrentSelectWashIndex = num;
			this.SetRightExcellentAttrBtnListState();
		}
	}

	private void OnClickSelectStarMaterialBtn(GameObject go)
	{
		PetID component = go.GetComponent<PetID>();
		int itemID = component.ItemID;
		int num = this.currentSelectStarMaterialState;
		for (int i = 0; i < this.starMaterialIDList.get_Count(); i++)
		{
			if (itemID == this.starMaterialIDList.get_Item(i))
			{
				this.currentSelectStarMaterialState = i;
			}
		}
		if (num == this.currentSelectStarMaterialState && component.Selected)
		{
			component.Selected = false;
			this.currentSelectStarMaterialState = -1;
		}
		else if (num != this.currentSelectStarMaterialState && !component.Selected)
		{
			component.Selected = true;
			if (num >= 0)
			{
				base.FindTransform("StarStoneItemRegion").GetChild(num).GetComponent<PetID>().Selected = false;
			}
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		int cfgId = equipSimpleInfo.cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		int attrID = 0;
		if (this.currentSelectStarMaterialState >= 0 && this.currentSelectStarMaterialState < zZhuangBeiPeiZhiBiao.boostStarAttr.get_Count())
		{
			attrID = zZhuangBeiPeiZhiBiao.boostStarAttr.get_Item(this.currentSelectStarMaterialState);
		}
		this.RefreshStarExtraAttrText(attrID);
		this.RefreshStarSuccessRatioText(equipSimpleInfo.star);
	}

	private void OnClickWashBtn(GameObject go)
	{
		this.SetBtnWashState(true);
		if (this.currentExcellentAttr != null && this.currentExcellentAttr.color >= 1f)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(505048, false), delegate
			{
				this.SetBtnWashState(false);
			}, delegate
			{
				this.SendRefineEquipReq();
				this.SetBtnWashState(false);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
			DialogBoxUIView.Instance.SetMask(0.7f, true, false);
			return;
		}
		this.SendRefineEquipReq();
	}

	private void SendRefineEquipReq()
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
		long equipId = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).equipId;
		if (this.CurrentSelectWashIndex >= 0)
		{
			EquipmentManager.Instance.SendRefineEquipReq((int)this.currentSelectPos, this.CurrentSelectWashIndex, equipId);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505035, false));
		}
	}

	private void OnClickWashChange(GameObject go)
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
		long equipId = EquipmentManager.Instance.dicEquips.get_Item(equipLib.wearingId).equipId;
		if (this.currentExcellentID <= 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505036, false));
			return;
		}
		EquipmentManager.Instance.SendRefineEquipResultAckReq((int)this.currentSelectPos, this.currentExcellentID, equipId, this.CurrentSelectWashIndex);
	}

	private void OnClickStarUp(GameObject go)
	{
		for (int i = 0; i < this.starMaterialIDList.get_Count(); i++)
		{
			if (this.currentSelectStarMaterialState == i && BackpackManager.Instance.OnGetGoodCount(this.starMaterialIDList.get_Item(i)) <= 0L)
			{
				LinkNavigationManager.ItemNotEnoughToLink(this.starMaterialIDList.get_Item(i), false, null, true);
				return;
			}
		}
		if (this.currentSelectStarMaterialState < 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505037, false));
			return;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		List<int> value = DataReader<ShengXingJiChuPeiZhi>.Get("boostStarLevel").value;
		if (equipSimpleInfo.star >= value.get_Count())
		{
			return;
		}
		if (equipLib.lv < value.get_Item(equipSimpleInfo.star))
		{
			string text = string.Format(GameDataUtils.GetChineseContent(20004, false), value.get_Item(equipSimpleInfo.star));
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		int materialID = this.starMaterialIDList.get_Item(this.currentSelectStarMaterialState);
		EquipmentManager.Instance.SendEnhanceEquipStarReq((int)this.currentSelectPos, materialID, equipSimpleInfo.equipId);
	}

	private void OnClickResetStar(GameObject go)
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
		EquipSimpleInfo equip = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		if (equip.star <= 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505038, false));
			return;
		}
		string chineseContent = GameDataUtils.GetChineseContent(20000, false);
		string chineseContent2 = GameDataUtils.GetChineseContent(20001, false);
		int num = DataReader<ShengXingJiChuPeiZhi>.Get("resetCoinType").num;
		Items items = DataReader<Items>.Get(num);
		string chineseContent3 = GameDataUtils.GetChineseContent(items.name, false);
		string text = DataReader<ShengXingJiChuPeiZhi>.Get("resetPrice").value.get_Item(equip.star - 1).ToString();
		string content = string.Format(chineseContent2, chineseContent3 + text);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, content, null, delegate
		{
			EquipmentManager.Instance.SendResetEquipStarReq((int)this.currentSelectPos, equip.equipId);
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickSelectEnchantmentItem(GameObject go)
	{
		int num = this.enchantmentItemList.FindIndex((Transform a) => a.get_gameObject() == go);
		if (num >= 0)
		{
			this.currentSelectEnchantmentPos = num;
			EquipEnchantmentUI equipEnchantmentUI = UIManagerControl.Instance.OpenUI("EquipEnchantMentUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as EquipEnchantmentUI;
			equipEnchantmentUI.RefreshUI((int)this.currentSelectPos, this.currentSelectEnchantmentPos);
		}
	}

	private void OnClickChangeEquip(GameObject go)
	{
		if (!this.IsFirstWash)
		{
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(651056, false), null, delegate
			{
				this.IsFirstWash = true;
				EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == this.currentSelectPos);
				if (equipLib != null)
				{
					EquipmentManager.Instance.SendCancelRefineDataReq((int)this.currentSelectPos, equipLib.wearingId);
					UIManagerControl.Instance.OpenUI("EquipDetailedPopUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
					EquipDetailedPopUI.Instance.SetSelectEquipTip(this.currentSelectPos, false);
				}
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
		else
		{
			UIManagerControl.Instance.OpenUI("EquipDetailedPopUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			EquipDetailedPopUI.Instance.SetSelectEquipTip(this.currentSelectPos, false);
		}
	}

	private void OnClickBtnCheckBrilliantAttr(GameObject go)
	{
		EquipWashCheckUI equipWashCheckUI = UIManagerControl.Instance.OpenUI("EquipWashCheckUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as EquipWashCheckUI;
		int equipCfgIDByPos = EquipGlobal.GetEquipCfgIDByPos(this.currentSelectPos);
		equipWashCheckUI.RefreshUI(equipCfgIDByPos);
	}

	private void OnClickBtnEquipSuitHelp(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 510205, 510204);
	}

	private void OnClickBtnForge(GameObject go)
	{
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(this.currentSelectPos);
		if (wearingEquipSimpleInfoByPos != null && wearingEquipSimpleInfoByPos.suitId <= 0 && this.currentForgingCfg != null)
		{
			EquipmentManager.Instance.SendForgingSuitReq(wearingEquipSimpleInfoByPos.equipId, this.currentSelectPos, this.currentForgingCfg.suitId);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510203, false));
		}
	}

	private void OnClickChangeBtn(GameObject go)
	{
		if (go == this.changeHighBtn.get_gameObject())
		{
			this.changeHighBtn.get_gameObject().SetActive(false);
			this.changeLowBtn.get_gameObject().SetActive(true);
			this.isSelectHigh = true;
		}
		else
		{
			this.changeHighBtn.get_gameObject().SetActive(true);
			this.changeLowBtn.get_gameObject().SetActive(false);
			this.isSelectHigh = false;
		}
		this.UpdateEquipPosListPool(EquipDetailedUIState.EquipSuitForge, true);
	}

	private void EquipDetailedShouldCheckBadge()
	{
		this.CheckBadge();
	}

	private void OnGetAcquireNewEquipNty()
	{
	}

	private void OnGetEquipAdvancedRes(bool haveChange)
	{
		this.RefreshUI(this.currentEquipDetailedUIState, this.currentSelectPos);
	}

	private void OnResetEquipStar()
	{
		this.UpdateEquipPosData(this.currentEquipDetailedUIState, this.currentSelectPos);
		this.RefreshCenterWhenSelectPos(this.currentEquipDetailedUIState, this.currentSelectPos, false);
		this.RefreshRightStarUp(this.currentSelectPos);
	}

	private void OnIntensifyPosSuccessOrFailed(bool isSuccess)
	{
		this.UpdateEquipPosData(this.currentEquipDetailedUIState, this.currentSelectPos);
		this.SetUIState(this.currentEquipDetailedUIState, this.currentSelectPos, isSuccess);
		this.RefreshPosBtnsSelectState(this.currentSelectPos);
		this.RefreshCenterWhenSelectPos(this.currentEquipDetailedUIState, this.currentSelectPos, true);
		this.CheckBadge();
	}

	private void OnGetBuyGoldRes()
	{
		this.SetUIState(this.currentEquipDetailedUIState, this.currentSelectPos, false);
		this.RefreshCenterWhenSelectPos(this.currentEquipDetailedUIState, this.currentSelectPos, false);
	}

	private void OnRefineEquipRes(ExcellentAttr excellentAttr)
	{
		this.IsFirstWash = false;
		this.RefreshRightWash(this.currentSelectPos);
		this.RefreshExtraAttrText(excellentAttr);
		if (excellentAttr != null)
		{
			this.currentExcellentID = excellentAttr.attrId;
			this.currentExcellentAttr = excellentAttr;
		}
		FXSpineManager.Instance.PlaySpine(1503, base.FindTransform("ExtraAttrFX"), "EquipDetailedUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void OnRefineEquipResultAckRes(int position)
	{
		this.UpdateEquipPosData(this.currentEquipDetailedUIState, this.currentSelectPos);
		this.RefreshCenterWhenSelectPos(EquipDetailedUIState.EquipWash, this.currentSelectPos, false);
		this.IsFirstWash = true;
		this.RefreshRightWash(this.currentSelectPos);
		this.currentExcellentID = 0;
		this.currentExcellentAttr = null;
		this.RefreshExtraAttrText(null);
		FXSpineManager.Instance.PlaySpine(109, this.EquipCenter.FindChild("FX"), "EquipDetailedUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void OnEnchantEquipResultAckRes(int position)
	{
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipEnchantment)
		{
			this.UpdateEquipPosData(this.currentEquipDetailedUIState, this.currentSelectPos);
			this.RefreshCenterWhenSelectPos(EquipDetailedUIState.EquipWash, this.currentSelectPos, false);
			this.RefreshRightEnchantment(this.currentSelectPos);
			FXSpineManager.Instance.PlaySpine(154, this.enchantmentItemList.get_Item(position).FindChild("EnchantmentFX"), "EquipDetailedUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void OnCancelRefineDataRes()
	{
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipWash)
		{
			this.RefreshUI(this.currentEquipDetailedUIState, this.currentSelectPos);
		}
	}

	private void OnEquipmentChangeRes()
	{
		if (!this.IsFirstWash)
		{
			this.IsFirstWash = true;
		}
		if (this.LeftEquipPosDic != null && this.LeftEquipPosDic.ContainsKey(this.currentSelectPos))
		{
			EquipPosItem equipPosItem = this.LeftEquipPosDic.get_Item(this.currentSelectPos);
			if (equipPosItem != null)
			{
				equipPosItem.UpdateUIData(this.currentSelectPos, this.currentEquipDetailedUIState);
			}
		}
		this.RefreshCenterWhenSelectPos(this.currentEquipDetailedUIState, this.currentSelectPos, false);
		this.SetUIState(this.currentEquipDetailedUIState, this.currentSelectPos, false);
	}

	private void OnStarUpFailOrSuccess(bool isSuccess, int currentStar)
	{
		this.SetBtnStarUpState(true);
		if (isSuccess)
		{
			FXSpineManager.Instance.PlaySpine(151, this.EquipCenter.FindChild("FX"), "EquipDetailedUI", 2007, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			Transform host = this.starTransformList.get_Item(currentStar - 1).FindChild("StarFX");
			FXSpineManager.Instance.PlaySpine(153, host, "EquipDetailedUI", 2002, delegate
			{
				this.UpdateEquipPosData(this.currentEquipDetailedUIState, this.currentSelectPos);
				this.RefreshCenterWhenSelectPos(this.currentEquipDetailedUIState, this.currentSelectPos, false);
				this.RefreshRightStarUp(this.currentSelectPos);
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.PlaySpine(152, this.EquipCenter.FindChild("FX"), "EquipDetailedUI", 2007, delegate
			{
				this.SetBtnStarUpState(false);
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.UpdateEquipPosData(this.currentEquipDetailedUIState, this.currentSelectPos);
			this.RefreshCenterWhenSelectPos(this.currentEquipDetailedUIState, this.currentSelectPos, false);
			this.RefreshRightStarUp(this.currentSelectPos);
		}
	}

	private void OnForgingSuitRes(int pos)
	{
		if (pos == (int)this.currentSelectPos && this.currentEquipDetailedUIState == EquipDetailedUIState.EquipSuitForge)
		{
			this.RefreshUIByPos((EquipLibType.ELT)pos);
			this.UpdateEquipPosData(this.currentEquipDetailedUIState, (EquipLibType.ELT)pos);
			this.equipSuitFXID = FXSpineManager.Instance.PlaySpine(107, this.EquipCenter.FindChild("FX"), "EquipDetailedUI", 2006, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void UpdateEquipPosGemData(int pos)
	{
		if (this.currentEquipDetailedUIState == EquipDetailedUIState.EquipGem)
		{
			this.UpdateEquipPosData(this.currentEquipDetailedUIState, (EquipLibType.ELT)pos);
		}
	}

	private string GetStarLevelSpriteName(int itemID)
	{
		switch (itemID)
		{
		case 8:
			return "pinji_tongxing1";
		case 9:
			return "pinji_yinxing1";
		case 10:
			return "pinji_jinxing1";
		default:
			return string.Empty;
		}
	}

	private void SetBtnStarUpState(bool isGray = false)
	{
		string spriteName = isGray ? "button_gray_1" : "button_yellow_1";
		this.BtnStarUp.set_enabled(!isGray);
		ResourceManager.SetIconSprite(this.BtnStarUp.get_transform().FindChild("Image").GetComponent<Image>(), spriteName);
	}

	public void SetBtnWashState(bool isGray = false)
	{
		string spriteName = isGray ? "button_gray_1" : "button_yellow_1";
		this.BtnWash.set_enabled(!isGray);
		ResourceManager.SetIconSprite(this.BtnWash.get_transform().FindChild("Image").GetComponent<Image>(), spriteName);
	}
}
