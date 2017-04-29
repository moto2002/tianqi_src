using Package;
using System;

public class TowerInstance : BattleInstanceParent<DefendFightBtlResultNty>
{
	private static TowerInstance instance;

	public static TowerInstance Instance
	{
		get
		{
			if (TowerInstance.instance == null)
			{
				TowerInstance.instance = new TowerInstance();
			}
			return TowerInstance.instance;
		}
	}

	protected TowerInstance()
	{
		base.Type = InstanceType.Defence;
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
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(501002, false), GameDataUtils.GetChineseContent(513529, false), delegate
			{
				InstanceManager.TryPause();
			}, delegate
			{
				InstanceManager.TryResume();
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				InstanceManager.TryResume();
				EventDispatcher.Broadcast("GuideManager.InstanceExit");
				DefendFightManager.Instance.ExitDefendFightReq(false);
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
			GlobalBattleDialogUIView.Instance.isClick = false;
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.ShowSpecialInstanceBattleUI(true);
		battleUI.InitSpecialInstanceBattleUI();
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
		RewardPreviewUI.CopyType type = RewardPreviewUI.CopyType.NONE;
		switch (DefendFightManager.Instance.SelectDetailMode)
		{
		case DefendFightMode.DFMD.Hold:
			type = RewardPreviewUI.CopyType.GUARD;
			break;
		case DefendFightMode.DFMD.Protect:
			type = RewardPreviewUI.CopyType.ESCORT;
			break;
		case DefendFightMode.DFMD.Save:
			type = RewardPreviewUI.CopyType.ATTACK;
			break;
		}
		battleUI.ShowRewardPreviewUI(true, type, 0L, 0L);
	}

	public override void GetInstanceResult(DefendFightBtlResultNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		if (base.InstanceResult.result.winnerId == EntityWorld.Instance.EntSelf.ID || DefendFightManager.Instance.SelectDetailMode == DefendFightMode.DFMD.Hold || DefendFightManager.Instance.SelectDetailMode == DefendFightMode.DFMD.Save || DefendFightManager.Instance.SelectDetailMode == DefendFightMode.DFMD.Protect)
		{
			InstanceManager.InstanceWin();
		}
		else
		{
			InstanceManager.InstanceLose();
		}
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		UIManagerControl.Instance.HideUI("BattleUI");
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
			commonBattlePassUI.PlayAnimation(InstanceResultType.Result);
			commonBattlePassUI.BtnStatictisVisibity = false;
			commonBattlePassUI.BtnAgainVisibility = false;
			commonBattlePassUI.BtnMultipleVisibility = false;
			commonBattlePassUI.BtnTipTextVisibility = true;
			commonBattlePassUI.PassTimeVisibility = true;
			commonBattlePassUI.UpdateTowerCopyUI(base.InstanceResult);
			commonBattlePassUI.ExitCallback = delegate
			{
				EventDispatcher.Broadcast("GuideManager.InstanceExit");
				EventDispatcher.Broadcast("GuideManager.InstanceWin");
				DefendFightManager.Instance.ExitDefendFightReq(false);
			};
		});
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
		battleLoseUI.BtnExitAction = delegate
		{
			DefendFightManager.Instance.ExitDefendFightReq(false);
		};
		battleLoseUI.ShowBtnAgainBtn(false);
		battleLoseUI.ShowBtnDamageCal(false);
	}
}
