using GameData;
using Package;
using System;

public class PVPInstance : BattleInstanceParent<ArenaChallengeBattleResult>
{
	public static readonly int PVPInstanceDataID = 80001;

	private static PVPInstance instance;

	private bool forceExit;

	public static PVPInstance Instance
	{
		get
		{
			if (PVPInstance.instance == null)
			{
				PVPInstance.instance = new PVPInstance();
			}
			return PVPInstance.instance;
		}
	}

	public override int InstanceDataID
	{
		get
		{
			return PVPInstance.PVPInstanceDataID;
		}
		set
		{
		}
	}

	protected PVPInstance()
	{
		base.Type = InstanceType.Arena;
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
			}, delegate
			{
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				this.forceExit = true;
				NetworkManager.Send(new ExitAreaBattleReq(), ServerType.Data);
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void PlayerInitEnd(EntityPlayer player)
	{
		if (player.IsEntitySelfType)
		{
			return;
		}
	}

	public override void PlayerHpChange(EntityPlayer player)
	{
		if (player.IsEntitySelfType)
		{
			return;
		}
	}

	public override void PlayerHpLmtChange(EntityPlayer player)
	{
		if (player.IsEntitySelfType)
		{
			return;
		}
	}

	public override void PlayerDie(EntityPlayer player)
	{
		TimerHeap.AddTimer((uint)float.Parse(DataReader<GlobalParams>.Get("instanceEndCameraTime").value), 0, delegate
		{
			FXSpineManager.Instance.PVPSuccess();
		});
	}

	public override void BossInitEnd(EntityMonster boss)
	{
	}

	public override void BossHpChange(EntityMonster boss)
	{
	}

	public override void BossHpLmtChange(EntityMonster boss)
	{
	}

	public override void OpeningCountDown(int millisecond)
	{
		TimeCountBackUI timeCountBackUI = UIManagerControl.Instance.OpenUI("TimeCountBackUI", UINodesManager.TopUIRoot, true, UIType.NonPush) as TimeCountBackUI;
		timeCountBackUI.StartTimeCountBack(millisecond / 1000, null);
	}

	public override void OpeningCountDownTimesUp()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("TimeCountBackUI");
	}

	public override void SetCommonLogic()
	{
		PVPManager.Instance.isEnterPVP = false;
	}

	public override void GetInstanceResult(ArenaChallengeBattleResult result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		if (UIManagerControl.Instance.GetUIIfExist("DialogBoxUI"))
		{
			UIManagerControl.Instance.HideUI("DialogBoxUI");
		}
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			if (base.InstanceResult.status == ArenaChallengeBattleResult.BattleResult.Win)
			{
				InstanceManager.InstanceWin();
			}
			else if (this.forceExit)
			{
				this.forceExit = false;
				InstanceManager.IsAIThinking = false;
				this.ShowLoseUI();
			}
			else
			{
				InstanceManager.InstanceLose();
			}
		}
	}

	public override void ShowWinPose()
	{
		TimerHeap.AddTimer(3000u, 0, new Action(base.ShowWinPose));
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		UIManagerControl.Instance.HideUI("BattleUI");
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
			commonBattlePassUI.PlayAnimation(InstanceResultType.Win);
			commonBattlePassUI.UpdatePvpInstanceUI(base.InstanceResult);
			commonBattlePassUI.BtnExitContent = GameDataUtils.GetChineseContent(505114, false);
			commonBattlePassUI.ExitCallback = delegate
			{
				PVPManager.Instance.SendArenaRoomDestoryReq();
			};
		});
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		UIManagerControl.Instance.HideUI("BattleUI");
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
			commonBattlePassUI.PlayAnimation(InstanceResultType.Lose);
			commonBattlePassUI.UpdatePvpInstanceUI(base.InstanceResult);
			commonBattlePassUI.BtnExitContent = GameDataUtils.GetChineseContent(505114, false);
			commonBattlePassUI.ExitCallback = delegate
			{
				PVPManager.Instance.SendArenaRoomDestoryReq();
			};
		});
	}

	public void CloseLoseUI()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("PVPFinallyLoseUI");
	}

	public void CloseWinUI()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("PVPFinallyWinUI");
	}

	public override bool IsShowPlayerAimMark(bool isSameCamp)
	{
		return !isSameCamp;
	}

	public override bool IsShowPetAimMark(bool isSameCamp)
	{
		return !isSameCamp;
	}
}
