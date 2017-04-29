using GameData;
using Package;
using System;

public class GangFightInstance : BattleInstanceParent<GangFightBattleResult>
{
	public static readonly int GangFightInstanceDataID = 90001;

	private static GangFightInstance instance;

	public static GangFightInstance Instance
	{
		get
		{
			if (GangFightInstance.instance == null)
			{
				GangFightInstance.instance = new GangFightInstance();
			}
			return GangFightInstance.instance;
		}
	}

	public override int InstanceDataID
	{
		get
		{
			return GangFightInstance.GangFightInstanceDataID;
		}
		set
		{
		}
	}

	protected GangFightInstance()
	{
		base.Type = InstanceType.GangFight;
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

	public override void LoadSceneEnd(int sceneID)
	{
		UIManagerControl.Instance.HideUI("GangFightUI");
		base.LoadSceneEnd(sceneID);
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
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(510109, false), GameDataUtils.GetChineseContent(510110, false), delegate
			{
			}, delegate
			{
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				EventDispatcher.Broadcast("GuideManager.InstanceExit");
				GangFightManager.Instance.SendExitFromGangFightFieldReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void GetInstanceResult(GangFightBattleResult result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		if (base.InstanceResult.winnerId == EntityWorld.Instance.EntSelf.ID)
		{
			InstanceManager.StopAllClientAI(false);
			InstanceManager.InstanceWin();
			GangFightManager.Instance.IsWinLastFight = true;
		}
		else
		{
			InstanceManager.StopAllClientAI(true);
			InstanceManager.InstanceLose();
			GangFightManager.Instance.IsWinLastFight = false;
		}
	}

	public override void ShowWinPose()
	{
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		GangFightSettle gangFightSettle = UIManagerControl.Instance.OpenUI("GangFightSettleUI", null, false, UIType.NonPush) as GangFightSettle;
		gangFightSettle.ShowAnimate(true);
		gangFightSettle.RefreshUI(base.InstanceResult);
		this.instanceResult = null;
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		GangFightSettle gangFightSettle = UIManagerControl.Instance.OpenUI("GangFightSettleUI", null, false, UIType.NonPush) as GangFightSettle;
		gangFightSettle.ShowAnimate(false);
		gangFightSettle.RefreshUI(base.InstanceResult);
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
