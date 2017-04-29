using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVPUI : UIBase
{
	public static PVPUI Instance;

	public ListPool pool;

	private void Awake()
	{
		PVPUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.hideMainCamera = true;
	}

	protected override void InitUI()
	{
		ButtonCustom expr_10 = base.FindTransform("PVPButton").GetComponent<ButtonCustom>();
		expr_10.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_10.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickPVP));
		ButtonCustom expr_41 = base.FindTransform("PVPShop").GetComponent<ButtonCustom>();
		expr_41.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_41.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickShop));
		ButtonCustom expr_72 = base.FindTransform("PVPDiary").GetComponent<ButtonCustom>();
		expr_72.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_72.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickDairy));
		ButtonCustom expr_A3 = base.FindTransform("PVPIntergralInstruction").GetComponent<ButtonCustom>();
		expr_A3.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_A3.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickIntergralInstruction));
		base.FindTransform("LeftTips").GetComponent<Text>().set_text(string.Format("积分大于{0}才可进入排行", (int)DataReader<JingJiChangXiShu>.Get("showRanking").num));
	}

	protected override void OnEnable()
	{
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Arena, base.get_transform(), 0);
		changePetChooseUI.Show(true);
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110017), string.Empty, new Action(this.OnClickExit), false);
		TimerHeap.AddTimer(100u, 0, new Action(this.OpenContent));
	}

	protected override void OnDisable()
	{
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			PVPUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.PVPUpdateList, new Callback(this.UpdateList));
		EventDispatcher.AddListener(EventNames.PVPUpdateUI, new Callback(this.UpdateUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.PVPUpdateList, new Callback(this.UpdateList));
		EventDispatcher.RemoveListener(EventNames.PVPUpdateUI, new Callback(this.UpdateUI));
	}

	private void OnClickIntergralInstruction(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 503011, 503012);
	}

	private void OnClickExit()
	{
		PVPManager.Instance.ClosePVPUI();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnClickDairy(GameObject go)
	{
		throw new NotImplementedException();
	}

	private void OnClickPVP(GameObject go)
	{
		if (InstanceManagerUI.IsPetLimit())
		{
			return;
		}
		PVPManager.Instance.SendStartChallengeReq();
	}

	private void OnClickShop(GameObject go)
	{
		LinkNavigationManager.OpenPVPShopUI();
	}

	public void OpenContent()
	{
		PVPManager.Instance.SendRankingReq();
		this.UpdateUI();
	}

	public void UpdateUI()
	{
		base.FindTransform("leveValue").GetComponent<Text>().set_text(string.Format("{0}  Lv.{1}", EntityWorld.Instance.EntSelf.Name, EntityWorld.Instance.EntSelf.Lv.ToString()));
		base.FindTransform("fightValue").GetComponent<Text>().set_text("战斗力: " + EntityWorld.Instance.EntSelf.CityBaseAttrs.Fighting.ToString());
		base.FindTransform("pvpCoinValue").GetComponent<Text>().set_text("竞技币可获取次数:<color=#ffeb4b>" + PVPManager.Instance.PVPData.rewardNum + "</color>");
		int score = PVPManager.Instance.PVPData.score;
		base.FindTransform("RankingValue").GetComponent<Text>().set_text((PVPManager.Instance.PVPData.rank != 0 && score >= 300) ? ("排名:" + PVPManager.Instance.PVPData.rank.ToString()) : "暂无排名");
		base.FindTransform("integralValue").GetComponent<Text>().set_text(score.ToString());
		ResourceManager.SetSprite(base.FindTransform("achieveIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(PVPManager.Instance.GetIntegralByScore(PVPManager.Instance.PVPData.score, true)));
		JingJiChangFenDuan integralLevel = PVPManager.Instance.GetIntegralLevel(PVPManager.Instance.PVPData.score);
		base.FindTransform("achieveRange").GetComponent<Text>().set_text(string.Format("( {0}~{1} )", integralLevel.min, integralLevel.max));
	}

	private void UpdateList()
	{
		List<RankingsItem> rank = PVPManager.Instance.RankingData;
		base.FindTransform("TipsBg").get_gameObject().SetActive(rank.get_Count() == 0);
		this.pool.Create(rank.get_Count(), delegate(int index)
		{
			if (index < rank.get_Count() && index < this.pool.Items.get_Count())
			{
				this.pool.Items.get_Item(index).GetComponent<PVPRankingItem>().UpdateItem(rank.get_Item(index));
			}
		});
	}
}
