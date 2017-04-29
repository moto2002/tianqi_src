using GameData;
using Package;
using System;
using System.Collections.Generic;

public class DungeonEliteInstance : BattleInstanceParent<EliteBattleResultNty>
{
	protected TimeCountDown timeCountDown;

	private static DungeonEliteInstance instance;

	public static DungeonEliteInstance Instance
	{
		get
		{
			if (DungeonEliteInstance.instance == null)
			{
				DungeonEliteInstance.instance = new DungeonEliteInstance();
			}
			return DungeonEliteInstance.instance;
		}
	}

	protected DungeonEliteInstance()
	{
		base.Type = InstanceType.DungeonElite;
	}

	public override void SetTime()
	{
		base.SetTime();
		BattleTimeManager.Instance.ClientSetBattleTime(base.InstanceData.time);
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
			DungeonManager.Instance.shouldNotShowLoseUI = true;
			UIManagerControl.Instance.OpenUI("GlobalBattleDialogUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetNoticeText(100), GameDataUtils.GetNoticeText(101), delegate
			{
			}, delegate
			{
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				EliteDungeonManager.Instance.SendEliteExitReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_yellow_1", null);
			GlobalBattleDialogUIView.Instance.SetMask(0.7f, true, false);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.ShowDamageRankingUI(true);
		battleUI.ShowGlobalRank(true, BattleUI.RankRewardType.Text);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void PlayerInitEnd(EntityPlayer player)
	{
	}

	public override void PlayerHpChange(EntityPlayer player)
	{
	}

	public override void PlayerHpLmtChange(EntityPlayer player)
	{
	}

	public override void GiveUpRelive()
	{
		EliteDungeonManager.Instance.SendEliteExitReq();
	}

	public override void GetInstanceResult(EliteBattleResultNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		UIManagerControl.Instance.HideUI("GlobalReliveUI");
		if (base.InstanceResult.result.winnerId == EntityWorld.Instance.EntSelf.ID)
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
			commonBattlePassUI.PlayAnimation(InstanceResultType.Win);
			commonBattlePassUI.UpdateDungeonRewards(base.InstanceResult.normalPrize);
			commonBattlePassUI.BtnStatictisVisibity = false;
			commonBattlePassUI.BtnAgainVisibility = false;
			commonBattlePassUI.BtnMultipleVisibility = false;
			commonBattlePassUI.BtnTipTextVisibility = false;
			if (base.InstanceResult != null && base.InstanceResult.remainTimes <= 0)
			{
				commonBattlePassUI.NoRewardTipText = GameDataUtils.GetChineseContent(505047, false);
			}
			commonBattlePassUI.ExitCallback = delegate
			{
				EliteDungeonManager.Instance.SendEliteExitReq();
			};
			commonBattlePassUI.OnCountDownToExit(5, delegate
			{
				EliteDungeonManager.Instance.SendEliteExitReq();
			});
		}
	}

	protected PassUICommonDrop FixInstanceDrop()
	{
		PassUICommonDrop passUICommonDrop = new PassUICommonDrop();
		List<ItemBriefInfo> normalPrize = base.InstanceResult.normalPrize;
		if (normalPrize == null)
		{
			return passUICommonDrop;
		}
		for (int i = 0; i < normalPrize.get_Count(); i++)
		{
			int cfgId = normalPrize.get_Item(i).cfgId;
			long count = normalPrize.get_Item(i).count;
			Items item = BackpackManager.Instance.GetItem(cfgId);
			if (item != null)
			{
				int secondType = item.secondType;
				if (secondType != 15)
				{
					if (secondType != 16)
					{
						passUICommonDrop.item.Add(new KeyValuePair<int, long>(cfgId, count));
					}
					else
					{
						passUICommonDrop.exp += count;
					}
				}
				else
				{
					passUICommonDrop.gold += count;
				}
			}
		}
		return passUICommonDrop;
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		if (DungeonManager.Instance.shouldNotShowLoseUI)
		{
			DungeonManager.Instance.shouldNotShowLoseUI = false;
			EliteDungeonManager.Instance.SendEliteExitReq();
		}
		else
		{
			TimerHeap.AddTimer(1000u, 0, delegate
			{
				UIManagerControl.Instance.HideUI("BattleUI");
				BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
				battleLoseUI.BtnExitAction = delegate
				{
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnEquipQuaAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_EQUIPQUALITY";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnEquipLvAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_EQUIPLEVEL";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnGemLvAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_GEMLEVEL";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnSkillAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_SKILL";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnPetLvAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_PETLEVEL";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnPetStarAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_PETSTRA";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnPetSkillAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_PETSKILL";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnGodSoldierAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_GODSOLDIER";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.BtnWingAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_WING";
					EliteDungeonManager.Instance.SendEliteExitReq();
				};
				battleLoseUI.ShowBtnAgainBtn(false);
				battleLoseUI.ShowBtnDamageCal(false);
			});
		}
	}

	public override int GetRankByPassTime(int passTime)
	{
		return EliteDungeonManager.Instance.GetEliteScoreRankIndex(passTime);
	}

	public override string GetRankInfoText(int rank, int remainTime)
	{
		return EliteDungeonManager.Instance.GetScoreText(rank, (remainTime < 0) ? 0 : remainTime);
	}

	public override int GetStandardTimeByRank(int rank)
	{
		return EliteDungeonManager.Instance.GetScoreTime(rank);
	}

	public override string GetRankRewardText(int rank)
	{
		return EliteDungeonManager.Instance.GetScoreRewardText(rank);
	}
}
