using Package;
using System;

public class BountyInstance : BattleInstanceParent<BountyTaskResultNty>
{
	private static BountyInstance instance;

	public static BountyInstance Instance
	{
		get
		{
			if (BountyInstance.instance == null)
			{
				BountyInstance.instance = new BountyInstance();
			}
			return BountyInstance.instance;
		}
	}

	protected BountyInstance()
	{
		base.Type = InstanceType.Defence;
	}

	public override void OpeningCountDown(int millisecond)
	{
		base.OpeningCountDown(millisecond);
		UIManagerControl.Instance.UnLoadUIPrefab("GangFightMatchingFinishUI");
		TimeCountBackUI timeCountBackUI = UIManagerControl.Instance.OpenUI("TimeCountBackUI", UINodesManager.TopUIRoot, true, UIType.NonPush) as TimeCountBackUI;
		timeCountBackUI.StartTimeCountBack(millisecond / 1000, null);
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
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(501002, false), GameDataUtils.GetChineseContent((!BountyManager.Instance.isSelectDaily) ? 513658 : 513657, false), delegate
			{
			}, delegate
			{
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				NetworkManager.Send(new BountyTaskExitBtlReq(), ServerType.Data);
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.ShowBountyBattleUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void GetInstanceResult(BountyTaskResultNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		if (result.win == -1)
		{
			InstanceManager.InstanceLose();
		}
		else
		{
			InstanceManager.InstanceWin();
		}
	}

	public override void ShowEndingHint(int textID)
	{
		if (base.InstanceResult.win == 1)
		{
		}
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		TimerHeap.AddTimer(3000u, 0, delegate
		{
			BountyResultUI bountyResultUI = UIManagerControl.Instance.OpenUI("BountyWinUI", null, true, UIType.NonPush) as BountyResultUI;
			bountyResultUI.UpdateData(base.InstanceResult);
		});
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		BountyResultUI bountyResultUI = UIManagerControl.Instance.OpenUI("BountyWinUI", null, true, UIType.NonPush) as BountyResultUI;
		bountyResultUI.UpdateData(base.InstanceResult);
	}
}
