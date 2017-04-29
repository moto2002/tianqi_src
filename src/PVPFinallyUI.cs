using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PVPFinallyUI : UIBase
{
	private bool isWinResult;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		ButtonCustom expr_10 = base.FindTransform("SureButton").GetComponent<ButtonCustom>();
		expr_10.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_10.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickSure));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		Utils.WinSetting(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Utils.WinSetting(false);
	}

	private void OnDistroy()
	{
		ButtonCustom expr_10 = base.FindTransform("SureButton").GetComponent<ButtonCustom>();
		expr_10.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Remove(expr_10.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickSure));
	}

	private void OnClickSure(GameObject go)
	{
		PVPManager.Instance.SendArenaRoomDestoryReq();
		if (this.isWinResult)
		{
			PVPInstance.Instance.CloseWinUI();
		}
		else
		{
			PVPInstance.Instance.CloseLoseUI();
		}
	}

	protected override void OnClickMaskAction()
	{
		this.OnClickSure(null);
	}

	public void Init(ArenaChallengeBattleResult result)
	{
		switch (result.status)
		{
		case ArenaChallengeBattleResult.BattleResult.Win:
			this.SetWinAndLose(true);
			Debug.LogError(string.Concat(new object[]
			{
				"======>竞技币：",
				result.competitiveCurrency,
				" ",
				base.FindTransform("pvpCoinIntergral") == null
			}));
			base.FindTransform("pvpCoinIntergral").get_gameObject().SetActive(result.competitiveCurrency > 0);
			if (result.competitiveCurrency > 0)
			{
				base.FindTransform("pvpCoinValue").GetComponent<Text>().set_text("+" + result.competitiveCurrency.ToString());
			}
			goto IL_D4;
		}
		this.SetWinAndLose(false);
		IL_D4:
		ResourceManager.SetSprite(base.FindTransform("sign").GetComponent<Image>(), ResourceManager.GetIconSprite(PVPManager.Instance.GetIntegralByScore(result.oldScore + result.gainScore, true)));
		base.FindTransform("homeIntergralValue").GetComponent<Text>().set_text(result.gainScore.ToString());
		base.FindTransform("victoryTimeValue").GetComponent<Text>().set_text(result.combatWinCount.ToString());
	}

	private void SetWinAndLose(bool isWin)
	{
		this.isWinResult = isWin;
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		if (this.isWinResult)
		{
			EventDispatcher.Broadcast("GuideManager.InstanceWin");
		}
	}
}
