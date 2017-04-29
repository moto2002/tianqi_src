using Package;
using System;
using UnityEngine;

public class ElementInstance : BattleInstanceParent<ElementCopyBattleResultNty>
{
	private static ElementInstance instance;

	public static ElementInstance Instance
	{
		get
		{
			if (ElementInstance.instance == null)
			{
				ElementInstance.instance = new ElementInstance();
			}
			return ElementInstance.instance;
		}
	}

	protected ElementInstance()
	{
		base.Type = InstanceType.Element;
	}

	public override void GetInstanceResult(ElementCopyBattleResultNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		if (base.InstanceResult.result)
		{
			InstanceManager.InstanceWin();
		}
		else
		{
			InstanceManager.InstanceLose();
		}
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
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetNoticeText(100), GameDataUtils.GetChineseContent(502269, false), delegate
			{
			}, delegate
			{
				BallElement.Instance.shouldChangePosImmediately = false;
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				EventDispatcher.Broadcast("GuideManager.InstanceExit");
				ElementInstanceManager.Instance.SendExitElementCopyReq(delegate
				{
				});
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		UIManagerControl.Instance.HideUI("BattleUI");
		TimerHeap.AddTimer(5500u, 0, delegate
		{
		});
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		InstanceManager.StopAllClientAI(true);
		BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
		battleLoseUI.BtnExitAction = delegate
		{
			ElementInstanceManager.Instance.SendExitElementCopyReq(delegate
			{
			});
			BallElement.Instance.shouldChangePosImmediately = false;
		};
		battleLoseUI.BtnAgainAction = null;
		battleLoseUI.BtnPetLvAction = delegate
		{
			ElementInstanceManager.Instance.SendExitElementCopyReq(delegate
			{
				EventDispatcher.Broadcast("SHOW_PETLEVEL");
			});
		};
		battleLoseUI.ShowBtnAgainBtn(false);
		battleLoseUI.ShowBtnDamageCal(false);
	}

	public override void ShowWinPose()
	{
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			base.ShowWinPose();
		});
	}

	public override void LoadSceneEnd(int sceneID)
	{
		Debug.Log("-----------------------------------  InstanceData.id" + base.InstanceData.id);
	}
}
