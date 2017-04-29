using Foundation.Core.Databinding;
using System;
using UnityEngine;

public class BattleTypeUI : UIBase
{
	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		ButtonCustom expr_10 = base.FindTransform("ItemPVP").GetComponent<ButtonCustom>();
		expr_10.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_10.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickGangPVPFight));
		ButtonCustom expr_41 = base.FindTransform("ItemSC").GetComponent<ButtonCustom>();
		expr_41.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_41.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickSurvivalChallenge));
		ButtonCustom expr_72 = base.FindTransform("ItemSpecialInstance").GetComponent<ButtonCustom>();
		expr_72.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_72.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickItemSpecialInstance));
		ButtonCustom expr_A3 = base.FindTransform("ItemElement").GetComponent<ButtonCustom>();
		expr_A3.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_A3.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickItemElement));
		ButtonCustom expr_D4 = base.FindTransform("ItemInstance").GetComponent<ButtonCustom>();
		expr_D4.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_D4.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickItemInstance));
		CurrenciesUIViewModel.Show(true);
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110022), string.Empty, delegate
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		EventDispatcher.Broadcast<UIBase>(EventNames.RefreshTipsButtonStateInUIBase, this);
		this.CheckBadge();
		DungeonManager.Instance.OnGetDungeonDataReq();
	}

	protected override void OnDisable()
	{
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void CheckBadge()
	{
		if (base.FindTransform("ItemInstance").FindChild("ButtonTips").get_gameObject().get_activeSelf())
		{
			base.FindTransform("ItemInstance").FindChild("ButtonTips").get_gameObject().SetActive(false);
		}
		DungeonManager.Instance.CheckCanShowTip();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.ShowCanGetRewardNty, new Callback(this.OnShowInstanceTip));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.ShowCanGetRewardNty, new Callback(this.OnShowInstanceTip));
	}

	private void OnClickGangExpedition(GameObject go)
	{
		UIManagerControl.Instance.ShowToastText("当前功能未开放", 2f, 2f);
	}

	private void OnClickGangFight(GameObject go)
	{
		LinkNavigationManager.OpenGangFightUI();
	}

	private void OnClickGangPVPFight(GameObject go)
	{
		LinkNavigationManager.OpenPVPUI();
	}

	private void OnClickSurvivalChallenge(GameObject go)
	{
		LinkNavigationManager.OpenSurvivalChallengeUI();
	}

	private void OnClickItemSpecialInstance(GameObject sender)
	{
		LinkNavigationManager.OpenSpecialInstanceUI();
	}

	private void OnClickItemElement(GameObject sender)
	{
		UIManagerControl.Instance.OpenUI("MemCollabUI", UINodesManager.NormalUIRoot, false, UIType.FullScreen);
	}

	private void OnClickItemInstance(GameObject sender)
	{
		InstanceManagerUI.OpenEliteDungeonUI();
	}

	private void OnShowInstanceTip()
	{
		base.FindTransform("ItemInstance").FindChild("ButtonTips").get_gameObject().SetActive(true);
	}
}
