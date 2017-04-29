using Package;
using System;
using UnityEngine;

public class ExperienceCopyInstance : BattleInstanceParent<ResultExperienceCopyNty>
{
	private bool mIsShowPass;

	private static ExperienceCopyInstance instance;

	public int BeforeLevel
	{
		get;
		private set;
	}

	public long BeforeExp
	{
		get;
		private set;
	}

	public long BeforeExpLmt
	{
		get;
		private set;
	}

	public static ExperienceCopyInstance Instance
	{
		get
		{
			if (ExperienceCopyInstance.instance == null)
			{
				ExperienceCopyInstance.instance = new ExperienceCopyInstance();
			}
			return ExperienceCopyInstance.instance;
		}
	}

	protected ExperienceCopyInstance()
	{
		base.Type = InstanceType.ExperienceCopy;
	}

	public override void AddInstanceListeners()
	{
		this.mIsShowPass = false;
		EventDispatcher.AddListener<bool>(EventNames.ShowPayUI, new Callback<bool>(this.OnShowPayUI));
	}

	public override void RemoveInstanceListeners()
	{
		this.mIsShowPass = true;
		EventDispatcher.RemoveListener<bool>(EventNames.ShowPayUI, new Callback<bool>(this.OnShowPayUI));
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
				SpecialFightManager.Instance.ExitExperienceReq(false);
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.ShowSpecialBuffUI(true);
		battleUI.ShowSpecialInstanceBattleUI(true);
		battleUI.InitSpecialInstanceBattleUI();
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
		int num = InstanceManager.CurrentInstanceBatch - 1;
		long batchExp = SpecialFightManager.Instance.GetBatchExp(num);
		battleUI.ShowRewardPreviewUI(true, RewardPreviewUI.CopyType.EXP, batchExp * (long)num, SpecialFightManager.Instance.GetBatchExp(InstanceManager.CurrentInstanceBatch));
		this.BeforeLevel = EntityWorld.Instance.EntSelf.Lv;
		this.BeforeExp = EntityWorld.Instance.EntSelf.Exp;
		this.BeforeExpLmt = EntityWorld.Instance.EntSelf.ExpLmt;
		Debug.Log(string.Concat(new object[]
		{
			"进副本前 等级:Lv.",
			this.BeforeLevel,
			", 已有经验:",
			this.BeforeExp,
			", 升级经验:",
			this.BeforeExpLmt
		}));
	}

	public override void GetInstanceResult(ResultExperienceCopyNty result)
	{
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		InstanceManager.InstanceWin();
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
		this.mIsShowPass = true;
		TimerHeap.AddTimer(1000u, 0, new Action(this.ShowResultUI));
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		InstanceManager.StopAllClientAI(true);
		BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
		battleLoseUI.BtnExitAction = delegate
		{
			SpecialFightManager.Instance.ExitExperienceReq(false);
		};
		battleLoseUI.ShowBtnAgainBtn(false);
		battleLoseUI.ShowBtnDamageCal(false);
	}

	private void OnShowPayUI(bool isShow)
	{
		if (!isShow)
		{
			if (this.mIsShowPass)
			{
				this.ShowResultUI();
			}
			else
			{
				this.ShowBattleUI();
			}
		}
	}

	private void ShowResultUI()
	{
		UIManagerControl.Instance.HideUI("BattleUI");
		UIManagerControl.Instance.HideUI("BattleBuyBuffUI");
		if (DialogBoxUIViewModel.Instance != null)
		{
			DialogBoxUIViewModel.Instance.Close();
		}
		CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
		commonBattlePassUI.PlayAnimation(InstanceResultType.Result);
		commonBattlePassUI.BtnStatictisVisibity = false;
		commonBattlePassUI.BtnAgainVisibility = false;
		commonBattlePassUI.BtnMultipleVisibility = true;
		commonBattlePassUI.BtnTipTextVisibility = true;
		commonBattlePassUI.PassTimeVisibility = false;
		commonBattlePassUI.DropRewardVisibility = false;
		commonBattlePassUI.UpdateExperienceCopyUI(base.InstanceResult, base.InstanceResult.rewardBatch);
		commonBattlePassUI.ExitCallback = delegate
		{
			SpecialFightManager.Instance.ExitExperienceReq(false);
		};
		commonBattlePassUI.MultipleCallback = delegate
		{
			SpecialFightManager.Instance.SendGetExperienceCopyReward(true);
		};
	}
}
