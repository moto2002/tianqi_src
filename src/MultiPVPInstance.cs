using Package;
using System;

public class MultiPVPInstance : BattleInstanceParent<MultiPvpSettleNty>
{
	private static MultiPVPInstance instance;

	public static MultiPVPInstance Instance
	{
		get
		{
			if (MultiPVPInstance.instance == null)
			{
				MultiPVPInstance.instance = new MultiPVPInstance();
			}
			return MultiPVPInstance.instance;
		}
	}

	protected MultiPVPInstance()
	{
		base.Type = InstanceType.MultiPVP;
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
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetNoticeText(100), GameDataUtils.GetChineseContent(510110, false), null, delegate
			{
				MultiPVPManager.Instance.SendLeaveMultiPvpReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_yellow_1", null, true);
			GlobalBattleDialogUIView.Instance.SetMask(0.7f, true, false);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowPeakBattleSlot(true);
		battleUI.ShowBattleTimeUI(true);
		battleUI.SetPeakBattleKillAndDeathNumText(0, 0);
		battleUI.SetPeakBattleVSText(0, 0);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void SetTime()
	{
		base.SetTime();
		BattleTimeManager.Instance.ClientSetBattleTime(base.InstanceData.time);
	}

	public override void GiveUpRelive()
	{
		MultiPVPManager.Instance.SendLeaveMultiPvpReq();
	}

	public override void GetInstanceResult(MultiPvpSettleNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		if (result == null)
		{
			return;
		}
		if (result.isWin)
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
		MultiPVPPassUI multiPVPPassUI = LinkNavigationManager.OpenMultiPVPPassUI();
		if (multiPVPPassUI != null)
		{
			multiPVPPassUI.PlayAnimation(InstanceResultType.Win);
			multiPVPPassUI.RefreshResultUI(base.InstanceResult);
			multiPVPPassUI.ExitCallBack = delegate
			{
				this.DoLeaveMultiPvp(true);
			};
			multiPVPPassUI.OnCountDownToExit(10, delegate
			{
				this.DoLeaveMultiPvp(true);
			});
		}
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		UIManagerControl.Instance.HideUI("DialogBoxUI");
		UIManagerControl.Instance.HideUI("BattleUI");
		MultiPVPPassUI multiPVPPassUI = LinkNavigationManager.OpenMultiPVPPassUI();
		if (multiPVPPassUI != null)
		{
			multiPVPPassUI.PlayAnimation(InstanceResultType.Lose);
			multiPVPPassUI.RefreshResultUI(base.InstanceResult);
			multiPVPPassUI.ExitCallBack = delegate
			{
				this.DoLeaveMultiPvp(false);
			};
			multiPVPPassUI.OnCountDownToExit(10, delegate
			{
				this.DoLeaveMultiPvp(false);
			});
		}
	}

	private void DoLeaveMultiPvp(bool isWin)
	{
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		if (isWin)
		{
			EventDispatcher.Broadcast("GuideManager.InstanceWin");
		}
		MultiPVPManager.Instance.SendLeaveMultiPvpReq();
	}
}
