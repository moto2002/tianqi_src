using GameData;
using Package;
using System;
using System.Collections.Generic;

public class BattleInstanceParent<T> : InstanceParent where T : class
{
	public enum ReliveType
	{
		None,
		Countdown,
		Purchase,
		ConntdownOrPurchase,
		CountdownAndPurchase
	}

	protected List<int> playerTypeList = new List<int>();

	protected List<int> playerSkillList = new List<int>();

	protected List<int> petTypeList = new List<int>();

	protected List<int> petSkillList = new List<int>();

	protected List<int> monsterTypeList = new List<int>();

	protected T instanceResult;

	protected T InstanceResult
	{
		get
		{
			return this.instanceResult;
		}
	}

	public override void PreLoadData(int sceneID)
	{
		InstanceManager.PreloadEntityResource();
		InstanceManager.PreloadGroceries();
	}

	public override List<int> GetPreloadClientCreatePetIDs()
	{
		return LocalInstanceHandler.Instance.GetPreloadPetIDs();
	}

	public override List<int> GetPreloadClientCreateMonsterIDs()
	{
		return LocalInstanceHandler.Instance.GetPreloadMonsterIDs();
	}

	public override void SetPreloadTypeData(List<int> thePlayerTypeList, List<int> thePetTypeList, List<int> theMonsterTypreList, List<int> thePlayerSkillList, List<int> thePetSkillList)
	{
		this.playerTypeList.Clear();
		this.petTypeList.Clear();
		this.monsterTypeList.Clear();
		this.playerSkillList.Clear();
		this.petSkillList.Clear();
		if (thePlayerTypeList != null && thePlayerTypeList.get_Count() > 0)
		{
			this.playerTypeList.AddRange(thePlayerTypeList);
		}
		if (thePetTypeList != null && thePetTypeList.get_Count() > 0)
		{
			this.petTypeList.AddRange(thePetTypeList);
		}
		if (theMonsterTypreList != null && theMonsterTypreList.get_Count() > 0)
		{
			this.monsterTypeList.AddRange(theMonsterTypreList);
		}
		if (thePlayerSkillList != null && thePlayerSkillList.get_Count() > 0)
		{
			this.playerSkillList.AddRange(thePlayerSkillList);
		}
		if (thePetSkillList != null && thePetSkillList.get_Count() > 0)
		{
			this.petSkillList.AddRange(thePetSkillList);
		}
	}

	public override List<int> GetPreloadServerCreatePlayerTypeIDs()
	{
		return this.playerTypeList;
	}

	public override List<int> GetPreloadServerCreatePlayerSkillIDs()
	{
		return this.playerSkillList;
	}

	public override List<int> GetPreloadServerCreatePetTypeIDs()
	{
		return this.petTypeList;
	}

	public override List<int> GetPreloadServerCreatePetSkillIDs()
	{
		return this.petSkillList;
	}

	public override List<int> GetPreloadServerCreateMonsterTypeIDs()
	{
		return this.monsterTypeList;
	}

	public override List<int> GetPreloadCommonFxIDs()
	{
		List<int> list = new List<int>();
		list.Add(1103);
		list.Add(1104);
		list.Add(96);
		list.Add(93);
		list.Add(94);
		if (EntityWorld.Instance.EntSelf != null)
		{
			switch (EntityWorld.Instance.EntSelf.TypeID)
			{
			case 1:
				list.Add(102);
				list.Add(104);
				list.Add(106);
				list.Add(108);
				break;
			case 2:
				list.Add(406);
				list.Add(407);
				list.Add(408);
				list.Add(409);
				break;
			case 3:
				list.Add(506);
				list.Add(507);
				list.Add(508);
				list.Add(509);
				list.Add(510);
				break;
			}
		}
		list.Add((int)float.Parse(DataReader<GlobalParams>.Get("killReply").value));
		return list;
	}

	public override void SendClientLoadDoneReq(int sceneID)
	{
		NetworkManager.Send(new MapLoadDoneReport
		{
			mapId = sceneID
		}, ServerType.Data);
	}

	public override void PlayOpeningCG(int timeline, Action onPlayCGEnd)
	{
		if (onPlayCGEnd != null)
		{
			onPlayCGEnd.Invoke();
		}
	}

	public override void SetDebutCD()
	{
		for (int i = 0; i < EntityWorld.Instance.AllEntities.Values.get_Count(); i++)
		{
			if (EntityWorld.Instance.AllEntities.Values.get_Item(i).IsClientDominate)
			{
				EntityWorld.Instance.AllEntities.Values.get_Item(i).DebutBattle();
			}
		}
	}

	public override void ShowBattleUI()
	{
		if (this.InstanceResult != null)
		{
			return;
		}
		BattleUI battleUI = LinkNavigationManager.OpenBattleUI();
		battleUI.BtnQuitAction = null;
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void ShowOpeningHint(int textID)
	{
		UIManagerControl.Instance.ShowBattleToastText(DataReader<KaiShiXinXi>.Get(textID).info, 2f);
	}

	public override void SetAI(bool isOpen)
	{
		if (isOpen)
		{
			InstanceManager.IsAIThinking = true;
		}
	}

	public override void SetTime()
	{
		BattleTimeManager.Instance.IsUITimeCountDown = (InstanceManager.CurrentInstanceData.timingMode == 1);
	}

	public override void SelfInitEnd(EntitySelf self)
	{
		base.SelfInitEnd(self);
		BattleBlackboard.Instance.SetSelfProperty(self.IconID, self.Lv, self.VipLv, self.Fighting, self.Hp, self.RealHpLmt, self.ActPoint);
	}

	public override void SelfHpChange(EntitySelf self)
	{
		BattleBlackboard.Instance.SelfHp = self.Hp;
	}

	public override void SelfHpLmtChange(EntitySelf self)
	{
		BattleBlackboard.Instance.SelfHpLmt = self.RealHpLmt;
	}

	public override void SelfActPointChange(EntitySelf self)
	{
		BattleBlackboard.Instance.SelfActPoint = self.ActPoint;
	}

	public override void SelfActPointLmtChange(EntitySelf self)
	{
		BattleBlackboard.Instance.SelfActPointLmt = self.ActPointLmt;
	}

	public override void SelfDie()
	{
		if (this.InstanceResult != null)
		{
			return;
		}
		if (base.InstanceData == null)
		{
			InstanceManager.GlobalGiveUpRelive();
		}
		else
		{
			switch (base.InstanceData.revive)
			{
			case 1:
			{
				GlobalReliveUI globalReliveUI = UIManagerControl.Instance.OpenUI("GlobalReliveUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GlobalReliveUI;
				if (globalReliveUI)
				{
					globalReliveUI.ShowBuyBtn(false, null, 0, 0L);
					globalReliveUI.ShowExitBtn(false, null);
					globalReliveUI.SetCountDown(base.InstanceData.reviveTime / 1000, delegate
					{
					});
					globalReliveUI.ShowTip(false, string.Empty);
				}
				else
				{
					InstanceManager.GlobalGiveUpRelive();
				}
				break;
			}
			case 2:
			{
				GlobalReliveUI globalReliveUI = UIManagerControl.Instance.OpenUI("GlobalReliveUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GlobalReliveUI;
				if (globalReliveUI && base.InstanceData.reviveExpend.get_Count() > 1)
				{
					globalReliveUI.ShowBuyBtn(true, delegate
					{
						InstanceManager.GlobalRelive();
					}, base.InstanceData.reviveExpend.get_Item(0), (long)base.InstanceData.reviveExpend.get_Item(1));
					globalReliveUI.ShowExitBtn(true, delegate
					{
						UIManagerControl.Instance.HideUI("GlobalReliveUI");
						InstanceManager.GlobalGiveUpRelive();
					});
					globalReliveUI.SetCountDown(base.InstanceData.reviveTime / 1000, delegate
					{
						UIManagerControl.Instance.HideUI("GlobalReliveUI");
						InstanceManager.GlobalGiveUpRelive();
					});
					globalReliveUI.ShowTip(true, GameDataUtils.GetChineseContent(505189, false));
				}
				else
				{
					InstanceManager.GlobalGiveUpRelive();
				}
				break;
			}
			case 3:
			{
				GlobalReliveUI globalReliveUI = UIManagerControl.Instance.OpenUI("GlobalReliveUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GlobalReliveUI;
				if (globalReliveUI)
				{
					globalReliveUI.ShowBuyBtn(true, delegate
					{
						InstanceManager.GlobalRelive();
					}, base.InstanceData.reviveExpend.get_Item(0), (long)base.InstanceData.reviveExpend.get_Item(1));
					globalReliveUI.ShowExitBtn(true, delegate
					{
						UIManagerControl.Instance.HideUI("GlobalReliveUI");
						InstanceManager.GlobalGiveUpRelive();
					});
					globalReliveUI.SetCountDown(base.InstanceData.reviveTime / 1000, delegate
					{
					});
					globalReliveUI.ShowTip(false, string.Empty);
				}
				else
				{
					InstanceManager.GlobalGiveUpRelive();
				}
				break;
			}
			case 4:
			{
				GlobalReliveUI globalReliveUI = UIManagerControl.Instance.OpenUI("GlobalReliveUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GlobalReliveUI;
				if (globalReliveUI)
				{
					globalReliveUI.ShowBuyBtn(false, null, 0, 0L);
					globalReliveUI.ShowExitBtn(false, null);
					globalReliveUI.SetCountDown(base.InstanceData.reviveTime / 1000, delegate
					{
						globalReliveUI.ShowBuyBtn(true, delegate
						{
							InstanceManager.GlobalRelive();
						}, this.InstanceData.reviveExpend.get_Item(0), (long)this.InstanceData.reviveExpend.get_Item(1));
						globalReliveUI.ShowExitBtn(true, delegate
						{
							UIManagerControl.Instance.HideUI("GlobalReliveUI");
							InstanceManager.GlobalGiveUpRelive();
						});
					});
					globalReliveUI.ShowTip(false, string.Empty);
				}
				else
				{
					InstanceManager.GlobalGiveUpRelive();
				}
				break;
			}
			default:
				InstanceManager.GlobalGiveUpRelive();
				break;
			}
			if (base.InstanceData.revive != 0)
			{
				EventDispatcher.Broadcast<bool>(ShaderEffectEvent.PLAYER_DEAD, true);
			}
		}
	}

	public override void SelfRelive()
	{
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.PLAYER_DEAD, false);
		if (UIManagerControl.Instance.IsOpen("GlobalReliveUI"))
		{
			UIManagerControl.Instance.HideUI("GlobalReliveUI");
		}
	}

	public override void PlayerInitEnd(EntityPlayer player)
	{
		if (player.IsPlayerMate)
		{
			BattleBlackboard.Instance.SetTeamMateProperty(player.ID, player.IconID, player.Name, player.Lv, player.VipLv, player.Hp, player.RealHpLmt);
		}
	}

	public override void PlayerHpChange(EntityPlayer player)
	{
		if (player.IsPlayerMate)
		{
			BattleBlackboard.Instance.SetTeamMateProperty(player.ID, player.IconID, player.Name, player.Lv, player.VipLv, player.Hp, player.RealHpLmt);
		}
	}

	public override void PlayerHpLmtChange(EntityPlayer player)
	{
		if (player.IsPlayerMate)
		{
			BattleBlackboard.Instance.SetTeamMateProperty(player.ID, player.IconID, player.Name, player.Lv, player.VipLv, player.Hp, player.RealHpLmt);
		}
	}

	public override void BossInitEnd(EntityMonster boss)
	{
		BattleBlackboard.Instance.SetBossProperty(boss.IconID, boss.Name, boss.Lv, boss.Hp, boss.RealHpLmt, boss.Vp, boss.RealVpLmt);
	}

	public override void BossHpChange(EntityMonster boss)
	{
		BattleBlackboard.Instance.SetBossProperty(boss.IconID, boss.Name, boss.Lv, boss.Hp, boss.RealHpLmt, boss.Vp, boss.RealVpLmt);
	}

	public override void BossHpLmtChange(EntityMonster boss)
	{
		BattleBlackboard.Instance.SetBossProperty(boss.IconID, boss.Name, boss.Lv, boss.Hp, boss.RealHpLmt, boss.Vp, boss.RealVpLmt);
	}

	public override void BossVpChange(EntityMonster boss)
	{
		BattleBlackboard.Instance.SetBossProperty(boss.IconID, boss.Name, boss.Lv, boss.Hp, boss.RealHpLmt, boss.Vp, boss.RealVpLmt);
	}

	public override void BossVpLmtChange(EntityMonster boss)
	{
		BattleBlackboard.Instance.SetBossProperty(boss.IconID, boss.Name, boss.Lv, boss.Hp, boss.RealHpLmt, boss.Vp, boss.RealVpLmt);
	}

	public virtual void GetInstanceResult(T result)
	{
		this.OnGetInstanceResultHideUI();
		this.instanceResult = result;
	}

	protected virtual void OnGetInstanceResultHideUI()
	{
		if (UIManagerControl.Instance.IsOpen("GlobalReliveUI"))
		{
			UIManagerControl.Instance.HideUI("GlobalReliveUI");
		}
	}

	public override void EndingCountdown(Action onCountdownEnd)
	{
		if (onCountdownEnd != null)
		{
			onCountdownEnd.Invoke();
		}
	}

	public override void ShowEndingHint(int textID)
	{
	}

	public override void PlayEndingCG(int timeline, Action onPlayCGEnd)
	{
		if (onPlayCGEnd != null)
		{
			onPlayCGEnd.Invoke();
		}
	}

	public override void ShowWinPose()
	{
	}

	public override void HideBattleUIs()
	{
		UIManagerControl.Instance.HideUI("GlobalReliveUI");
		UIManagerControl.Instance.HideUI("GlobalBattleDialogUI");
	}

	public override void ExitBattleField()
	{
		this.instanceResult = (T)((object)null);
	}

	public override void ShowWinUI()
	{
		this.SetWinAndLose(true);
	}

	public override void ShowLoseUI()
	{
		this.SetWinAndLose(false);
	}

	private void SetWinAndLose(bool isWin)
	{
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		if (isWin)
		{
			EventDispatcher.Broadcast("GuideManager.InstanceWin");
		}
	}
}
