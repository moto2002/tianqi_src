using GameData;
using Package;
using System;
using System.Collections.Generic;

public class MultiPlayerInstance : BattleInstanceParent<PveBattleResultNty>
{
	protected bool isBossAnimationEnd;

	protected bool hasGotResult;

	public int turnID;

	public int times;

	public bool isMulti;

	private static MultiPlayerInstance instance;

	public static MultiPlayerInstance Instance
	{
		get
		{
			if (MultiPlayerInstance.instance == null)
			{
				MultiPlayerInstance.instance = new MultiPlayerInstance();
			}
			return MultiPlayerInstance.instance;
		}
	}

	protected MultiPlayerInstance()
	{
		base.Type = InstanceType.DungeonMutiPeople;
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
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.ShowDamageRankingUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void GetInstanceResult(PveBattleResultNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		if (base.InstanceResult.result.winnerId == EntityWorld.Instance.EntSelf.ID)
		{
			InstanceManager.InstanceWin();
		}
		else
		{
			InstanceManager.InstanceLose();
		}
	}

	public override void ShowWinPose()
	{
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			base.ShowWinPose();
		});
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		UIManagerControl.Instance.HideUI("BattleUI");
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
			commonBattlePassUI.PlayAnimation(InstanceResultType.Win);
			commonBattlePassUI.BtnStatictisVisibity = true;
			commonBattlePassUI.BtnAgainVisibility = false;
			commonBattlePassUI.BtnMultipleVisibility = false;
			commonBattlePassUI.BtnTipTextVisibility = true;
			commonBattlePassUI.UpdateDungeonRewards(base.InstanceResult.normalPrize);
			commonBattlePassUI.ExitCallback = delegate
			{
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			commonBattlePassUI.OnCountDownToExit(5, delegate
			{
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			});
		});
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
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
			battleLoseUI.ShowBtnAgainBtn(false);
			battleLoseUI.ShowBtnDamageCal(true);
			battleLoseUI.ShowRecommendPower(false, 0);
			battleLoseUI.OnCountDown(5, delegate
			{
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			});
			battleLoseUI.BtnExitAction = delegate
			{
				battleLoseUI.StopCountDown();
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnAgainAction = null;
			battleLoseUI.BtnEquipQuaAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_EQUIPQUALITY";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnEquipLvAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_EQUIPLEVEL";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnGemLvAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_GEMLEVEL";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnSkillAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_SKILL";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnPetLvAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_PETLEVEL";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnPetStarAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_PETSTRA";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnPetSkillAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_PETSKILL";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnGodSoldierAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_GODSOLDIER";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
			battleLoseUI.BtnWingAction = delegate
			{
				SceneLoadedUISetting.CurrentType = "SHOW_WING";
				MultiPlayerManager.Instance.SendPveExitBattleReq();
			};
		});
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
}
