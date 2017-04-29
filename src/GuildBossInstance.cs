using Package;
using System;

public class GuildBossInstance : BattleInstanceParent<ChallengeGuildBossNty>
{
	public bool IsGetResult;

	private static GuildBossInstance instance;

	public static GuildBossInstance Instance
	{
		get
		{
			if (GuildBossInstance.instance == null)
			{
				GuildBossInstance.instance = new GuildBossInstance();
			}
			return GuildBossInstance.instance;
		}
	}

	protected GuildBossInstance()
	{
		base.Type = InstanceType.GuildBoss;
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
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetNoticeText(100), GameDataUtils.GetNoticeText(101), null, delegate
			{
				GuildBossManager.Instance.SendExitGuildBossBattleReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_yellow_1", null, true);
			GlobalBattleDialogUIView.Instance.isClick = true;
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
		this.IsGetResult = false;
		GuildBossManager.Instance.CheckShowGuildBossBattleCD();
	}

	public override void GetInstanceResult(ChallengeGuildBossNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		this.IsGetResult = true;
		GuildBossManager.Instance.CheckShowGuildBossBattleCD();
		if (base.InstanceResult.isEnd)
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
		CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
		if (commonBattlePassUI != null)
		{
			commonBattlePassUI.PlayAnimation(InstanceResultType.Result);
			commonBattlePassUI.UpdateGuildBossReward(base.InstanceResult);
			commonBattlePassUI.BtnStatictisVisibity = false;
			commonBattlePassUI.BtnAgainVisibility = false;
			commonBattlePassUI.BtnMultipleVisibility = false;
			commonBattlePassUI.BtnTipTextVisibility = false;
			commonBattlePassUI.ExitCallback = delegate
			{
				GuildBossManager.Instance.SendExitGuildBossBattleReq();
			};
		}
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		UIManagerControl.Instance.HideUI("BattleUI");
		CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
		if (commonBattlePassUI != null)
		{
			commonBattlePassUI.PlayAnimation(InstanceResultType.Result);
			commonBattlePassUI.UpdateGuildBossReward(base.InstanceResult);
			commonBattlePassUI.BtnStatictisVisibity = false;
			commonBattlePassUI.BtnAgainVisibility = false;
			commonBattlePassUI.BtnMultipleVisibility = false;
			commonBattlePassUI.BtnTipTextVisibility = false;
			commonBattlePassUI.ExitCallback = delegate
			{
				GuildBossManager.Instance.SendExitGuildBossBattleReq();
			};
		}
	}
}
