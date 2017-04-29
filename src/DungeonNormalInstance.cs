using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNormalInstance : BattleInstanceParent<ChallengeResult>
{
	private TaskProgressUI mProgressUI;

	private BattleUI mBattleUI;

	private static DungeonNormalInstance instance;

	public static DungeonNormalInstance Instance
	{
		get
		{
			if (DungeonNormalInstance.instance == null)
			{
				DungeonNormalInstance.instance = new DungeonNormalInstance();
			}
			return DungeonNormalInstance.instance;
		}
	}

	protected DungeonNormalInstance()
	{
		base.Type = InstanceType.DungeonNormal;
	}

	public override void LoadSceneStart(int lastSceneID, int nextSceneID)
	{
		LocalBattleHandler.Instance.SetData(new List<int>());
		if (DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.FIELD)
		{
			List<int> list = new List<int>();
			if (base.InstanceData.completeTarget == 0)
			{
				list.Add(5);
			}
			else
			{
				list.Add(base.InstanceData.completeTarget);
			}
			LocalInstanceHandler.Instance.SetData(base.InstanceData, list, DungeonManager.Instance.DungeonTarget, 0);
		}
		else
		{
			LocalInstanceHandler.Instance.SetData(base.InstanceData, 0);
		}
	}

	public override List<int> GetPreloadCGModelIDs()
	{
		if (base.InstanceData != null && MainTaskManager.Instance.IsGoingTask(this.InstanceDataID) && base.InstanceData.timeLine > 0)
		{
			return TimelineGlobal.GetNpcModelIds(base.InstanceData.timeLine);
		}
		return new List<int>();
	}

	public override List<int> GetPreloadCGComicIDs()
	{
		if (base.InstanceData != null && MainTaskManager.Instance.IsGoingTask(this.InstanceDataID) && base.InstanceData.timeLine > 0)
		{
			return TimelineGlobal.GetComicIds(base.InstanceData.timeLine);
		}
		return new List<int>();
	}

	public override void SendClientLoadDoneReq(int sceneID)
	{
		InstanceManager.SendClientLoadDoneResp(0, new BattleLoadInfo
		{
			unloadRoleCount = 0,
			loadTimeout = 0
		});
	}

	public override void PlayOpeningCG(int timeline, Action onPlayCGEnd)
	{
		if (InstanceManager.IsCurrentInstanceWithTask)
		{
			TimelineGlobal.Init(timeline, 0, onPlayCGEnd);
		}
		else if (onPlayCGEnd != null)
		{
			onPlayCGEnd.Invoke();
		}
	}

	public override void PlayEndingCG(int timeline, Action onPlayCGEnd)
	{
		if (InstanceManager.IsCurrentInstanceWithTask)
		{
			UIManagerControl.Instance.HideAll();
			FakeDrop.DeleteAllFakeDrop();
			TimelineGlobal.Init(timeline, 1, onPlayCGEnd);
		}
		else if (onPlayCGEnd != null)
		{
			onPlayCGEnd.Invoke();
		}
	}

	public override void SetCommonLogic()
	{
		if (DungeonManager.Instance.DungeonInstanceType != DungeonManager.InsType.WEAPON)
		{
			LocalInstanceHandler.Instance.Start();
		}
	}

	public override void SetTime()
	{
		base.SetTime();
		if (DungeonManager.Instance.DungeonInstanceType != DungeonManager.InsType.WEAPON)
		{
			BattleTimeManager.Instance.ClientSetBattleTime(base.InstanceData.time);
		}
		else
		{
			BattleTimeManager.Instance.ClientSetBattleTime(0);
		}
	}

	public override bool ShouldSetEndingCamera()
	{
		return !DungeonManager.Instance.shouldNotShowLoseUI;
	}

	public override void ShowBattleUI()
	{
		if (base.InstanceResult != null)
		{
			return;
		}
		this.mBattleUI = LinkNavigationManager.OpenBattleUI();
		this.mBattleUI.BtnQuitAction = delegate
		{
			DungeonManager.Instance.shouldNotShowLoseUI = true;
			UIManagerControl.Instance.OpenUI("GlobalBattleDialogUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetNoticeText(100), GameDataUtils.GetNoticeText(101), delegate
			{
				InstanceManager.TryPause();
			}, delegate
			{
				InstanceManager.TryResume();
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				InstanceManager.TryResume();
				EventDispatcher.Broadcast("GuideManager.InstanceExit");
				LocalInstanceHandler.Instance.Finish(false);
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_yellow_1", null);
			GlobalBattleDialogUIView.Instance.isClick = false;
		};
		this.mBattleUI.ResetAllInstancePart();
		this.mBattleUI.ShowBattleTimeUI(true);
		this.mBattleUI.IsPauseCheck = false;
		this.mBattleUI.IsInAuto = (base.InstanceData.autoFight == 0);
		if (!UIManagerControl.Instance.IsOpen("TaskProgressUI"))
		{
			this.mProgressUI = (UIManagerControl.Instance.OpenUI("TaskProgressUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TaskProgressUI);
		}
		if (DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.WEAPON)
		{
			this.mProgressUI.PlayWeaponEffect(DungeonManager.Instance.WeaponModelId);
			TimerHeap.AddTimer(4000u, 0, delegate
			{
				BattleTimeManager.Instance.ClientSetBattleTime(base.InstanceData.time);
				LocalInstanceHandler.Instance.Start();
			});
		}
	}

	public override void BossInitEnd(EntityMonster boss)
	{
		base.BossInitEnd(boss);
		if (!UIManagerControl.Instance.IsOpen("TaskProgressUI"))
		{
			this.mProgressUI = (UIManagerControl.Instance.OpenUI("TaskProgressUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TaskProgressUI);
		}
		this.mProgressUI.SetLayout(boss.MonsterRank == EntityParent.MonsterRankType.Boss);
	}

	public override bool IsShowMonsterBorn(int monsterID)
	{
		return !InstanceManager.IsCurrentInstanceWithTask || DataReader<Monster>.Get(monsterID).birthAction != 1;
	}

	public override void ResetBattleFieldResp()
	{
		if (base.InstanceData.serverBorn == 1)
		{
			return;
		}
		InstanceManager.SimulateExitField();
		InstanceManager.SimulateEnterField(base.InstanceData.type, null);
		InstanceManager.SimulateSwicthMap(base.InstanceData.scene, LocalInstanceHandler.Instance.CreateSelfInfo(InstanceManager.CurrentInstanceData.type, InstanceManager.CurrentInstanceDataID, InstanceManager.CurrentInstanceData.scene, 0, 0, null, null, null), null, 0);
	}

	public override void GetInstanceResult(ChallengeResult result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.StopAllClientAI(true);
		List<KeyValuePair<int, long>> list = new List<KeyValuePair<int, long>>();
		if (base.InstanceResult.items != null && base.InstanceResult.items.get_Count() > 0)
		{
			for (int i = 0; i < base.InstanceResult.items.get_Count(); i++)
			{
				ItemBriefInfo itemBriefInfo = base.InstanceResult.items.get_Item(i);
				KeyValuePair<int, long> keyValuePair = new KeyValuePair<int, long>(itemBriefInfo.cfgId, itemBriefInfo.count);
				list.Add(keyValuePair);
			}
		}
		InstanceManager.SetInstanceDrop(list);
		if (EntityWorld.Instance.EntSelf.IsInBattle)
		{
			if (base.InstanceResult.winnerId == EntityWorld.Instance.EntSelf.ID)
			{
				InstanceManager.InstanceWin();
				Debug.Log("任务完成！");
			}
			else
			{
				InstanceManager.InstanceLose();
				Debug.Log("任务失败！");
			}
		}
		if (this.mProgressUI != null && this.mProgressUI.ProgressPanel != null)
		{
			this.mProgressUI.ProgressPanel.get_gameObject().SetActive(false);
		}
		if (DialogBoxUIViewModel.Instance != null)
		{
			DialogBoxUIViewModel.Instance.Close();
		}
	}

	public override void ShowWinPose()
	{
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		if (this.mBattleUI != null)
		{
			this.mBattleUI.BtnQuitAction = null;
		}
		if (DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.FIELD)
		{
			return;
		}
		this.mProgressUI.PlayFinishSpine(DungeonManager.Instance.DungeonInstanceType, base.InstanceResult.winnerId == EntityWorld.Instance.EntSelf.ID);
		TimerHeap.AddTimer(4000u, 0, delegate
		{
			UIManagerControl.Instance.HideUI("BattleUI");
			CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
			if (commonBattlePassUI != null)
			{
				commonBattlePassUI.PlayAnimation(InstanceResultType.Win);
				commonBattlePassUI.UpdateDungeonRewards(base.InstanceResult.items);
				commonBattlePassUI.BtnStatictisVisibity = false;
				commonBattlePassUI.BtnAgainVisibility = false;
				commonBattlePassUI.BtnMultipleVisibility = false;
				commonBattlePassUI.BtnTipTextVisibility = false;
				if (DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.WEAPON)
				{
					commonBattlePassUI.NormalRewardVisibility = false;
					commonBattlePassUI.WeaponRewardId = DungeonManager.Instance.WeaponModelId;
				}
				commonBattlePassUI.ExitCallback = delegate
				{
					DungeonManager.Instance.SendExitDungeonReq();
				};
			}
		});
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		if (this.mBattleUI != null)
		{
			this.mBattleUI.BtnQuitAction = null;
		}
		if (DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.FIELD)
		{
			return;
		}
		if (DungeonManager.Instance.DungeonInstanceType == DungeonManager.InsType.WEAPON)
		{
			this.mProgressUI.PlayFinishSpine(DungeonManager.Instance.DungeonInstanceType, base.InstanceResult.winnerId == EntityWorld.Instance.EntSelf.ID);
			return;
		}
		if (DungeonManager.Instance.shouldNotShowLoseUI)
		{
			DungeonManager.Instance.shouldNotShowLoseUI = false;
			DungeonManager.Instance.SendExitDungeonReq();
			UIManagerControl.Instance.HideUI("TaskProgressUI");
		}
		else
		{
			TimerHeap.AddTimer(2000u, 0, delegate
			{
				BattleLoseUI battleLoseUI = LinkNavigationManager.OpenBattleLoseUI();
				UIManagerControl.Instance.HideUI("TaskProgressUI");
				battleLoseUI.BtnExitAction = delegate
				{
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnEquipQuaAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_EQUIPQUALITY";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnEquipLvAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_EQUIPLEVEL";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnGemLvAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_GEMLEVEL";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnSkillAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_SKILL";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnPetLvAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_PETLEVEL";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnPetStarAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_PETSTRA";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnPetSkillAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_PETSKILL";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnGodSoldierAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_GODSOLDIER";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.BtnWingAction = delegate
				{
					SceneLoadedUISetting.CurrentType = "SHOW_WING";
					DungeonManager.Instance.SendExitDungeonReq();
				};
				battleLoseUI.ShowBtnAgainBtn(false);
				battleLoseUI.ShowBtnDamageCal(false);
			});
		}
	}
}
