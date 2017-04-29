using Package;
using System;

public class SurvivalInstance : BattleInstanceParent<SecretAreaChallengeResultNty>
{
	private static SurvivalInstance instance;

	public static SurvivalInstance Instance
	{
		get
		{
			if (SurvivalInstance.instance == null)
			{
				SurvivalInstance.instance = new SurvivalInstance();
			}
			return SurvivalInstance.instance;
		}
	}

	protected SurvivalInstance()
	{
		base.Type = InstanceType.SurvivalChallenge;
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
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(512048, false), GameDataUtils.GetChineseContent(512049, false), delegate
			{
				InstanceManager.TryPause();
			}, delegate
			{
				InstanceManager.TryResume();
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				InstanceManager.TryResume();
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
			GlobalBattleDialogUIView.Instance.isClick = false;
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.ShowCallBoss(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void SetTime()
	{
		base.SetTime();
	}

	public override void BossInitEnd(EntityMonster boss)
	{
		base.BossInitEnd(boss);
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.ShowCallBoss(true);
	}

	public override void GetInstanceResult(SecretAreaChallengeResultNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		if (base.InstanceResult == null)
		{
			return;
		}
		int num = (base.InstanceResult.startStage - 1 <= 0) ? 1 : (base.InstanceResult.startStage - 1);
		if (num == base.InstanceResult.endStage && base.InstanceResult.copyRewards != null && base.InstanceResult.copyRewards.get_Count() <= 0)
		{
			InstanceManager.InstanceLose();
		}
		else
		{
			InstanceManager.InstanceWin();
		}
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		UIManagerControl.Instance.HideUI("BattleUI");
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			int num = (base.InstanceResult.startStage - 1 <= 0) ? 1 : (base.InstanceResult.startStage - 1);
			if (num == base.InstanceResult.endStage && base.InstanceResult.copyRewards.get_Count() <= 0)
			{
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
				return;
			}
			CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
			commonBattlePassUI.PlayAnimation(InstanceResultType.Result);
			commonBattlePassUI.BtnStatictisVisibity = false;
			commonBattlePassUI.BtnAgainVisibility = false;
			commonBattlePassUI.BtnMultipleVisibility = false;
			commonBattlePassUI.BtnTipTextVisibility = false;
			commonBattlePassUI.PassTimeVisibility = false;
			commonBattlePassUI.UpdateScResultUI(base.InstanceResult);
			commonBattlePassUI.ExitCallback = delegate
			{
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
		});
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			UIManagerControl.Instance.HideUI("BattleUI");
			BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
			battleLoseUI.BtnExitAction = delegate
			{
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnEquipQuaAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_EQUIPQUALITY";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnEquipLvAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_EQUIPLEVEL";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnGemLvAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_GEMLEVEL";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnSkillAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_SKILL";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnPetLvAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_PETLEVEL";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnPetStarAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_PETSTRA";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnPetSkillAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_PETSKILL";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnGodSoldierAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_GODSOLDIER";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.BtnWingAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_WING";
				SurvivalManager.Instance.SendExitSecretAreaChallengeReq();
			};
			battleLoseUI.ShowBtnAgainBtn(false);
			battleLoseUI.ShowBtnDamageCal(false);
		});
	}

	public override void ExitBattleField()
	{
		base.ExitBattleField();
		BattleDmgCollectManager.Instance.ClearData();
	}
}
