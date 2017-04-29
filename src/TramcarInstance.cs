using Package;
using System;

public class TramcarInstance : BattleInstanceParent<ResultProtectFightNty>
{
	private static TramcarInstance instance;

	public static TramcarInstance Instance
	{
		get
		{
			if (TramcarInstance.instance == null)
			{
				TramcarInstance.instance = new TramcarInstance();
			}
			return TramcarInstance.instance;
		}
	}

	protected TramcarInstance()
	{
		base.Type = InstanceType.Tramcar;
	}

	public override void ShowBattleUI()
	{
		if (base.InstanceResult != null)
		{
			return;
		}
		BattleUI battleUI = LinkNavigationManager.OpenBattleUI();
		battleUI.BtnQuitAction = delegate
		{
			UIManagerControl.Instance.OpenUI("GlobalBattleDialogUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(510109, false), GameDataUtils.GetChineseContent(510110, false), delegate
			{
				InstanceManager.TryPause();
			}, delegate
			{
				InstanceManager.TryResume();
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				InstanceManager.TryResume();
				TramcarManager.Instance.SendExitProtectFightReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
			GlobalBattleDialogUIView.Instance.isClick = false;
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowTramcarRewardUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void GetInstanceResult(ResultProtectFightNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		battleUI.BtnQuitAction = null;
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			if (result.isWin)
			{
				InstanceManager.InstanceWin();
			}
			else
			{
				InstanceManager.InstanceLose();
			}
		}
	}

	public override void ShowWinPose()
	{
		InstanceManager.StopAllClientAI(true);
		base.ShowWinPose();
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		InstanceManager.StopAllClientAI(true);
		UIManagerControl.Instance.HideUI("BattleUI");
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			UIManagerControl.Instance.HideUI("BattleUI");
			CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
			if (commonBattlePassUI != null)
			{
				commonBattlePassUI.PlayAnimation(InstanceResultType.Win);
				commonBattlePassUI.UpdateEliteUI(base.InstanceResult.item);
				commonBattlePassUI.BtnStatictisVisibity = false;
				commonBattlePassUI.BtnAgainVisibility = false;
				commonBattlePassUI.BtnMultipleVisibility = false;
				commonBattlePassUI.BtnTipTextVisibility = false;
				commonBattlePassUI.ExitCallback = delegate
				{
					TramcarManager.Instance.SendExitProtectFightReq();
				};
			}
		});
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		InstanceManager.StopAllClientAI(true);
		BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
		battleLoseUI.BtnExitAction = delegate
		{
			TramcarManager.Instance.SendExitProtectFightReq();
		};
		battleLoseUI.ShowBtnAgainBtn(false);
		battleLoseUI.ShowBtnDamageCal(false);
	}
}
