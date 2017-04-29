using Package;
using System;

public class FieldInstance : BattleInstanceParent<ResultMirrorNty>
{
	private static FieldInstance instance;

	public static FieldInstance Instance
	{
		get
		{
			if (FieldInstance.instance == null)
			{
				FieldInstance.instance = new FieldInstance();
			}
			return FieldInstance.instance;
		}
	}

	protected FieldInstance()
	{
		base.Type = InstanceType.Field;
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
				MainTaskManager.Instance.SendExitMirrorReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
			GlobalBattleDialogUIView.Instance.isClick = false;
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
		UIManagerControl.Instance.OpenUI("TaskProgressUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public override void GetInstanceResult(ResultMirrorNty result)
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
		UIManagerControl.Instance.HideUI("TaskProgressUI");
	}

	public override void ShowWinPose()
	{
	}
}
