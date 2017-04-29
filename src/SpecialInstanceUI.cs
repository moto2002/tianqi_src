using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInstanceUI : UIBase
{
	protected static readonly string FirstEnterAnimaName = "FirstEnter";

	protected static readonly string GoSelectPanelAnimaName = "GoSelectPanel";

	protected static readonly string GoDetailPanelAnimaName = "GoDetailPanel";

	protected static readonly string RecoveryDetailAnimaName = "RecoveryDetail";

	protected List<SpecialInstanceItem> SelectModeList = new List<SpecialInstanceItem>();

	protected SpecialInstanceDetail DetailUI;

	protected Animator animator;

	protected ButtonCustom SpecBtn;

	protected GameObject Detail;

	protected GameObject ModeSelect;

	private void Awake()
	{
		base.hideMainCamera = true;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		string chineseContent = GameDataUtils.GetChineseContent(513517, false);
		string chineseContent2 = GameDataUtils.GetChineseContent(513516, false);
		string[] theWeekText = new string[]
		{
			string.Empty,
			GameDataUtils.GetChineseContent(513519, false),
			GameDataUtils.GetChineseContent(513520, false),
			GameDataUtils.GetChineseContent(513521, false),
			GameDataUtils.GetChineseContent(513522, false),
			GameDataUtils.GetChineseContent(513523, false),
			GameDataUtils.GetChineseContent(513524, false),
			GameDataUtils.GetChineseContent(513525, false)
		};
		SpecialInstanceItem component = base.FindTransform("Guard").GetComponent<SpecialInstanceItem>();
		component.Init(SpecialFightMode.Hold, new Action<SpecialFightMode>(this.OnClickEnterDetail), chineseContent, chineseContent2, theWeekText);
		this.SelectModeList.Add(component);
		SpecialInstanceItem component2 = base.FindTransform("Escort").GetComponent<SpecialInstanceItem>();
		component2.Init(SpecialFightMode.Protect, new Action<SpecialFightMode>(this.OnClickEnterDetail), chineseContent, chineseContent2, theWeekText);
		this.SelectModeList.Add(component2);
		SpecialInstanceItem component3 = base.FindTransform("Attack").GetComponent<SpecialInstanceItem>();
		component3.Init(SpecialFightMode.Save, new Action<SpecialFightMode>(this.OnClickEnterDetail), chineseContent, chineseContent2, theWeekText);
		this.SelectModeList.Add(component3);
		SpecialInstanceItem component4 = base.FindTransform("Exp").GetComponent<SpecialInstanceItem>();
		component4.Init(SpecialFightMode.Expericence, new Action<SpecialFightMode>(this.OnClickEnterDetail), chineseContent, chineseContent2, theWeekText);
		this.SelectModeList.Add(component4);
		this.Detail = base.FindTransform("Detail").get_gameObject();
		this.ModeSelect = base.FindTransform("ModeSelect").get_gameObject();
		this.DetailUI = this.Detail.GetComponent<SpecialInstanceDetail>();
		this.animator = base.GetComponent<Animator>();
		this.SpecBtn = base.FindTransform("SpecBtn").GetComponent<ButtonCustom>();
		this.SpecBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSpec);
		this.UpdateUI();
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110030), string.Empty, delegate
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
			SpecialFightManager.Instance.isInDetailPanel = false;
			UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		}, false);
		if (SpecialFightManager.Instance.isInDetailPanel)
		{
			this.animator.set_enabled(false);
			this.Detail.GetComponent<CanvasGroup>().set_alpha(0f);
			this.ModeSelect.GetComponent<CanvasGroup>().set_alpha(0f);
			TimerHeap.AddTimer(10u, 0, delegate
			{
				this.animator.set_enabled(true);
				this.OnEnterDetail(SpecialFightManager.Instance.SelectDetailMode, SpecialInstanceUI.RecoveryDetailAnimaName);
			});
		}
		else
		{
			this.EnabledModeList(true);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			ModelPool.Instance.Clear();
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateSpecialInstanceUI, new Callback(this.UpdateUI));
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.UpdateUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateSpecialInstanceUI, new Callback(this.UpdateUI));
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.UpdateUI));
	}

	protected void OnClickSpec(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 513513, 513514);
	}

	protected void OnClickEnterDetail(SpecialFightMode mode)
	{
		this.OnEnterDetail(mode, SpecialInstanceUI.GoDetailPanelAnimaName);
	}

	protected void OnEnterDetail(SpecialFightMode mode, string anim)
	{
		if (!SystemOpenManager.IsSystemClickOpen(SpecialFightManager.GetSystemIDByMode(mode), 0, true))
		{
			return;
		}
		if (!this.DetailUI)
		{
			return;
		}
		SpecialFightManager.Instance.SelectDetailMode = mode;
		if (SpecialFightManager.GetModeCroup(mode) == SpecialFightModeGroup.Defend)
		{
			DefendFightManager.Instance.SelectDetailMode = (DefendFightMode.DFMD)mode;
		}
		this.DetailUI.SetInit(mode);
		SpecialFightManager.Instance.isInDetailPanel = false;
		this.animator.Play(anim, 0, 0f);
		this.EnabledModeList(false);
	}

	protected void OnDetailAnimatorEnd()
	{
		SpecialFightManager.Instance.isInDetailPanel = true;
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		if (changePetChooseUI != null)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Defend, base.get_transform(), 0);
		}
	}

	protected void UpdateUI()
	{
		for (int i = 0; i < this.SelectModeList.get_Count(); i++)
		{
			this.SelectModeList.get_Item(i).UpdateItem();
		}
	}

	protected void EnabledModeList(bool enabled)
	{
		for (int i = 0; i < this.SelectModeList.get_Count(); i++)
		{
			this.SelectModeList.get_Item(i).GetComponent<ButtonCustom>().set_enabled(enabled);
		}
	}

	public void FakeClick(SpecialFightMode mode)
	{
		this.OnEnterDetail(mode, SpecialInstanceUI.RecoveryDetailAnimaName);
	}
}
