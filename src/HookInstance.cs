using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class HookInstance : BattleInstanceParent<SettleHookNty>
{
	protected const int PeaceCamp = 100;

	protected const int RevengerCamp = 101;

	private static HookInstance instance;

	protected BattleUI battleUI;

	protected int PeaceModeDefaultCD = 120;

	protected int PKModeDefaultCD;

	protected int peaceModeCDEnd;

	protected int pkModeCDEnd;

	protected int curWave = -1;

	protected int curBossProperty = -1;

	protected uint updatePlayerTimer;

	protected int updatePlayerInterval = 1000;

	protected int MaxPlayerNum;

	protected List<int> playerNumToExpRatio = new List<int>();

	protected BattleBackpackUI battleBackpackUI;

	protected XDict<int, int> backpackLockInfo = new XDict<int, int>();

	protected List<KeyValuePair<int, long>> fixRealTimeDrop = new List<KeyValuePair<int, long>>();

	protected long fixRealTimeExp;

	protected long fixRealTimeGold;

	protected List<KeyValuePair<int, long>> fixNewRealTimeDrop = new List<KeyValuePair<int, long>>();

	protected uint autoExitTimer;

	protected uint autoExitTime = 10000u;

	public static HookInstance Instance
	{
		get
		{
			if (HookInstance.instance == null)
			{
				HookInstance.instance = new HookInstance();
			}
			return HookInstance.instance;
		}
	}

	protected bool IsSelfPeaceMode
	{
		get
		{
			return EntityWorld.Instance.EntSelf != null && this.IsCampPeace(EntityWorld.Instance.EntSelf.Camp);
		}
	}

	protected bool IsSelfRevenger
	{
		get
		{
			return EntityWorld.Instance.EntSelf != null && this.IsCampRevenge(EntityWorld.Instance.EntSelf.Camp);
		}
	}

	protected bool IsSelfPKMode
	{
		get
		{
			return EntityWorld.Instance.EntSelf != null && this.IsCampPK(EntityWorld.Instance.EntSelf.Camp);
		}
	}

	public int CurWave
	{
		get
		{
			return this.curWave;
		}
		set
		{
			this.curWave = value;
		}
	}

	public int CurBossProperty
	{
		get
		{
			return this.curBossProperty;
		}
		set
		{
			this.curBossProperty = value;
		}
	}

	protected bool CurRoomInPeace
	{
		get
		{
			return DataReader<GuaJiQuYuPeiZhi>.Contains(HuntManager.Instance.AreaId) && DataReader<GuaJiQuYuPeiZhi>.Get(HuntManager.Instance.AreaId).areaType == 1;
		}
	}

	protected HookInstance()
	{
		base.Type = InstanceType.Hook;
		this.PeaceModeDefaultCD = (int)float.Parse(DataReader<GuaJiJiChuSheZhi>.Get("cdTime").value);
		this.PKModeDefaultCD = (int)float.Parse(DataReader<GuaJiJiChuSheZhi>.Get("cdTime1").value);
		this.backpackLockInfo.Add(0, (int)float.Parse(DataReader<GuaJiJiChuSheZhi>.Get("beginItems").value));
		List<int> list = new List<int>();
		string[] array = DataReader<GuaJiJiChuSheZhi>.Get("vips").value.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			list.Add(int.Parse(array[i]));
		}
		List<int> list2 = new List<int>();
		string[] array2 = DataReader<GuaJiJiChuSheZhi>.Get("unlockItems").value.Split(new char[]
		{
			','
		});
		for (int j = 0; j < array2.Length; j++)
		{
			list2.Add(int.Parse(array2[j]));
		}
		int num = (list.get_Count() >= list2.get_Count()) ? list2.get_Count() : list.get_Count();
		for (int k = 0; k < num; k++)
		{
			if (this.backpackLockInfo.ContainsKey(list.get_Item(k)))
			{
				XDict<int, int> xDict;
				XDict<int, int> expr_19F = xDict = this.backpackLockInfo;
				int num2;
				int expr_1AA = num2 = list.get_Item(k);
				num2 = xDict[num2];
				expr_19F[expr_1AA] = num2 + list2.get_Item(k);
			}
			else
			{
				this.backpackLockInfo.Add(list.get_Item(k), list2.get_Item(k));
			}
		}
		for (int l = 1; l < this.backpackLockInfo.Count; l++)
		{
			List<int> values;
			List<int> expr_20A = values = this.backpackLockInfo.Values;
			int num2;
			int expr_20F = num2 = l;
			num2 = values.get_Item(num2);
			expr_20A.set_Item(expr_20F, num2 + this.backpackLockInfo.Values.get_Item(l - 1));
		}
	}

	public override void AddInstanceListeners()
	{
		base.AddInstanceListeners();
		NetworkManager.AddListenEvent<ChangeModeRes>(new NetCallBackMethod<ChangeModeRes>(this.OnChangeModeRes));
		NetworkManager.AddListenEvent<HookRoleBatchInfoNty>(new NetCallBackMethod<HookRoleBatchInfoNty>(this.GetHookRoleBatchInfoNty));
		NetworkManager.AddListenEvent<SettleHookNty>(new NetCallBackMethod<SettleHookNty>(this.GetInstanceResult));
	}

	public override void RemoveInstanceListeners()
	{
		base.RemoveInstanceListeners();
		NetworkManager.RemoveListenEvent<ChangeModeRes>(new NetCallBackMethod<ChangeModeRes>(this.OnChangeModeRes));
		NetworkManager.RemoveListenEvent<HookRoleBatchInfoNty>(new NetCallBackMethod<HookRoleBatchInfoNty>(this.GetHookRoleBatchInfoNty));
		NetworkManager.RemoveListenEvent<SettleHookNty>(new NetCallBackMethod<SettleHookNty>(this.GetInstanceResult));
	}

	public override void ReleaseData()
	{
		this.peaceModeCDEnd = 0;
		this.pkModeCDEnd = 0;
		this.curWave = -1;
		this.curBossProperty = -1;
		this.fixRealTimeDrop.Clear();
		this.fixRealTimeExp = 0L;
		this.fixRealTimeGold = 0L;
		this.fixNewRealTimeDrop.Clear();
	}

	public override void EnterBattleField()
	{
		if (this.playerNumToExpRatio.get_Count() == 0 && DataReader<GuaJiJiChuSheZhi>.Contains("expRatio"))
		{
			string value = DataReader<GuaJiJiChuSheZhi>.Get("expRatio").value;
			string[] array = value.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				this.MaxPlayerNum = array.Length;
				this.playerNumToExpRatio.Add(0);
				for (int i = 0; i < array.Length; i++)
				{
					this.playerNumToExpRatio.Add(int.Parse(array[i]) / 10);
				}
			}
		}
		base.EnterBattleField();
	}

	public override void ShowBattleUI()
	{
		if (base.InstanceResult != null)
		{
			return;
		}
		this.battleUI = LinkNavigationManager.OpenBattleUI();
		this.battleUI.BtnQuitAction = delegate
		{
			UIManagerControl.Instance.OpenUI("GlobalBattleDialogUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(510109, false), GameDataUtils.GetChineseContent(511627, false), delegate
			{
			}, delegate
			{
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				this.InitiativeExitHookInstance();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
			GlobalBattleDialogUIView.Instance.isClick = false;
		};
		this.battleUI.ResetAllInstancePart();
		this.battleUI.ShowBattleTimeUI(true);
		this.battleUI.ShowArea(true);
		this.battleUI.SetAreaName(HuntManager.Instance.RoomName);
		this.battleUI.ShowPlayerNum(true);
		this.updatePlayerTimer = TimerHeap.AddTimer(0u, this.updatePlayerInterval, new Action(this.UpdatePlayerNum));
		if (this.CurWave >= 0 && this.battleUI)
		{
			this.battleUI.ShowBatchText(true);
			this.battleUI.SetBatchTextNum(this.CurWave);
		}
		if (!this.CurRoomInPeace && this.CurBossProperty >= 0 && this.battleUI)
		{
			this.battleUI.ShowBossProbability(true);
			this.battleUI.SetBossProbabilityText(string.Format(GameDataUtils.GetChineseContent(511611, false), (double)this.CurBossProperty * 0.1));
		}
		this.battleUI.ShowBattleMode(true);
		if (this.IsSelfPeaceMode)
		{
			if (TimeManager.Instance.PreciseServerSecond < this.pkModeCDEnd)
			{
				this.battleUI.ShowPKModeCD(true);
				this.battleUI.SetPKModeCD((float)this.PKModeDefaultCD, (float)(this.pkModeCDEnd - TimeManager.Instance.PreciseServerSecond) / (float)this.PKModeDefaultCD);
			}
			else
			{
				this.battleUI.ShowPKModeBtn(true);
			}
		}
		else if (this.IsSelfPKMode)
		{
			if (TimeManager.Instance.PreciseServerSecond < this.peaceModeCDEnd)
			{
				this.battleUI.ShowPeaceModeCD(true);
				this.battleUI.SetPeaceModeCD((float)this.PeaceModeDefaultCD, (float)(this.peaceModeCDEnd - TimeManager.Instance.PreciseServerSecond) / (float)this.PeaceModeDefaultCD);
			}
			else
			{
				this.battleUI.ShowPeaceModeBtn(true);
			}
		}
		if (EntityWorld.Instance.EntSelf != null)
		{
			long defaultBatchExp = 0L;
			if (DataReader<GuaJiQuYuPeiZhi>.Contains(HuntManager.Instance.AreaId))
			{
				defaultBatchExp = DataReader<GuaJiQuYuPeiZhi>.Get(HuntManager.Instance.AreaId).exp;
			}
			this.battleUI.ShowRewardPreviewUI(true, RewardPreviewUI.CopyType.HOOK, this.fixRealTimeExp, defaultBatchExp);
		}
		this.battleUI.ShowBackpackBtn(true);
		this.battleUI.IsPauseCheck = false;
		this.battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
		BattleTimeManager.Instance.ServerSetBattleTime(HuntManager.Instance.EnterTick);
		this.ShowTaskProgress();
	}

	protected void ShowTaskProgress()
	{
		List<BaseTask> list = new List<BaseTask>(MainTaskManager.Instance.TaskMap.get_Values());
		for (int i = 0; i < list.get_Count(); i++)
		{
			BaseTask baseTask = list.get_Item(i);
			if (baseTask is LinkTask && baseTask.Task.status == Package.Task.TaskStatus.TaskReceived && (baseTask.Data.type == 18 || ((baseTask.Data.type == 21 || baseTask.Data.type == 22) && baseTask.Targets.get_Item(1) == HuntManager.Instance.MapId)))
			{
				TaskProgressUI.OpenByTaskId = baseTask.Task.taskId;
				TaskProgressUI taskProgressUI = UIManagerControl.Instance.OpenUI("TaskProgressUI", null, false, UIType.NonPush) as TaskProgressUI;
				taskProgressUI.SetLayout(new Vector2(0f, 250f));
				break;
			}
		}
	}

	protected void GetInstanceResult(short state, SettleHookNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.GetInstanceResult(down);
		UIManagerControl.Instance.HideUI("TaskProgressUI");
	}

	public override void GetInstanceResult(SettleHookNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		this.instanceResult = result;
		InstanceManager.InstanceWin();
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		SettleHookNty.Reason.RS settleReason = base.InstanceResult.settleReason;
		switch (settleReason)
		{
		case SettleHookNty.Reason.RS.ActiveExit:
		case SettleHookNty.Reason.RS.SelfDead:
		case SettleHookNty.Reason.RS.TimeOut:
		case SettleHookNty.Reason.RS.Close:
		case SettleHookNty.Reason.RS.Logout:
			break;
		case SettleHookNty.Reason.RS.Revenge:
			if (this.IsSelfRevenger)
			{
				this.ShowRevengeSuccessUI();
			}
			return;
		default:
			if (settleReason != SettleHookNty.Reason.RS.UnKnow)
			{
				return;
			}
			break;
		}
		if (this.IsSelfRevenger)
		{
			this.ShowRevengeFailedUI();
		}
		else
		{
			this.ShowHookQuitUI();
		}
	}

	protected void ShowHookQuitUI()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		InstanceManager.StopAllClientAI(true);
		if (UIManagerControl.Instance.IsOpen("BattleBackpackUI"))
		{
			UIManagerControl.Instance.HideUI("BattleBackpackUI");
		}
		CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
		if (commonBattlePassUI)
		{
			commonBattlePassUI.UpdateHookReward(this.fixRealTimeExp, this.fixRealTimeGold, (this.CurWave - 1 >= 0) ? (this.CurWave - 1) : 0, delegate
			{
				BattleBackpackUI battleBackpackUI = UIManagerControl.Instance.OpenUI("BattleBackpackUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BattleBackpackUI;
				battleBackpackUI.SetTitleName(GameDataUtils.GetChineseContent(511612, false));
				battleBackpackUI.ShowTips(false, string.Empty);
				this.UpdateRealTimeDrop(InstanceManager.RealTimeDropCache);
			}, new Action(this.ExitHookInstance));
			commonBattlePassUI.PlayAnimation(InstanceResultType.Result);
			this.autoExitTimer = TimerHeap.AddTimer(this.autoExitTime, 0, new Action(this.ExitHookInstance));
		}
		else
		{
			this.ExitHookInstance();
		}
	}

	protected void ShowRevengeSuccessUI()
	{
		Debug.LogError("ShowRevengeSuccessUI");
	}

	protected void ShowRevengeFailedUI()
	{
		Debug.LogError("ShowRevengeFailedUI");
	}

	protected void InitiativeExitHookInstance()
	{
		NetworkManager.Send(new ActiveSettleNty(), ServerType.Data);
	}

	protected void ExitHookInstance()
	{
		TimerHeap.DelTimer(this.updatePlayerTimer);
		TimerHeap.DelTimer(this.autoExitTimer);
		HuntManager.Instance.SendExitRoomReq();
	}

	public override void ExitBattleField()
	{
		TimerHeap.DelTimer(this.updatePlayerTimer);
		TimerHeap.DelTimer(this.autoExitTimer);
		base.ExitBattleField();
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

	public void ChangePKMode()
	{
		NetworkManager.Send(new ChangeModeReq
		{
			isPk = true
		}, ServerType.Data);
	}

	public void ChangePeaceMode()
	{
		NetworkManager.Send(new ChangeModeReq
		{
			isPk = false
		}, ServerType.Data);
	}

	protected void OnChangeModeRes(short state, ChangeModeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
	}

	public override void SelfCampChanged(int oldCamp, int newCamp)
	{
		if (this.IsCampPeace(oldCamp) && this.IsCampPK(newCamp))
		{
			if (this.battleUI)
			{
				this.battleUI.ShowPeaceModeBtn(false);
				this.battleUI.ShowPeaceModeCD(true);
				this.battleUI.SetPeaceModeCD((float)this.PeaceModeDefaultCD, 1f);
				this.battleUI.ShowPKModeBtn(false);
				this.battleUI.ShowPKModeCD(false);
			}
			else
			{
				this.peaceModeCDEnd = TimeManager.Instance.PreciseServerSecond + this.PeaceModeDefaultCD;
			}
		}
		else if (this.IsCampPK(oldCamp) && this.IsCampPeace(newCamp))
		{
			if (this.battleUI)
			{
				this.battleUI.ShowPeaceModeBtn(false);
				this.battleUI.ShowPeaceModeCD(false);
				this.battleUI.ShowPKModeBtn(false);
				this.battleUI.ShowPKModeCD(true);
				this.battleUI.SetPKModeCD((float)this.PKModeDefaultCD, 1f);
			}
			else
			{
				this.pkModeCDEnd = TimeManager.Instance.PreciseServerSecond + this.PKModeDefaultCD;
			}
		}
	}

	protected bool IsCampPeace(int camp)
	{
		return camp == 100;
	}

	protected bool IsCampRevenge(int camp)
	{
		return camp == 101;
	}

	protected bool IsCampPK(int camp)
	{
		return camp != 100 && camp != 101;
	}

	protected void GetHookRoleBatchInfoNty(short state, HookRoleBatchInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.CurWave = down.batch;
		this.CurBossProperty = down.bossRefreshRate;
		this.UpdateWave();
		this.UpdateBossProperty();
	}

	protected void UpdateWave()
	{
		if (this.CurWave == -1)
		{
			return;
		}
		if (this.battleUI)
		{
			this.battleUI.ShowBatchText(true);
			this.battleUI.SetBatchTextNum(this.CurWave);
		}
		UIManagerControl.Instance.ShowBattleToastText(string.Format(GameDataUtils.GetChineseContent(InstanceManager.CurrentInstanceData.waveShow, false), this.CurWave), 5f);
	}

	protected void UpdateBossProperty()
	{
		if (this.CurRoomInPeace)
		{
			return;
		}
		if (this.CurBossProperty == -1)
		{
			return;
		}
		if (this.battleUI)
		{
			this.battleUI.ShowBossProbability(true);
			this.battleUI.SetBossProbabilityText(string.Format(GameDataUtils.GetChineseContent(511611, false), (double)this.CurBossProperty * 0.1));
		}
	}

	protected void UpdatePlayerNum()
	{
		if (!this.battleUI)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < EntityWorld.Instance.AllEntities.Count; i++)
		{
			EntityParent entityParent = EntityWorld.Instance.AllEntities.ElementValueAt(i);
			if (!entityParent.IsDead)
			{
				if (entityParent.IsEntitySelfType || entityParent.IsEntityPlayerType)
				{
					num++;
				}
			}
		}
		if (num > this.MaxPlayerNum)
		{
			num = this.MaxPlayerNum;
		}
		this.battleUI.SetPlayerNum(string.Format(GameDataUtils.GetChineseContent(511638, false), num, (num >= this.playerNumToExpRatio.get_Count()) ? 0 : this.playerNumToExpRatio.get_Item(num)));
	}

	public override void GetNewRealTimeDrop(XDict<int, long> newRealTimeDrop)
	{
		List<Items> list = new List<Items>();
		for (int i = 0; i < newRealTimeDrop.Keys.get_Count(); i++)
		{
			if (newRealTimeDrop.Keys.get_Item(i) != 1)
			{
				if (newRealTimeDrop.Keys.get_Item(i) != 2)
				{
					if (DataReader<Items>.Contains(newRealTimeDrop.Keys.get_Item(i)))
					{
						if (newRealTimeDrop.Values.get_Item(i) > 0L)
						{
							list.Add(DataReader<Items>.Get(newRealTimeDrop.Keys.get_Item(i)));
						}
					}
				}
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			long num = newRealTimeDrop[list.get_Item(j).id];
			if (list.get_Item(j).overlay == 0)
			{
				Debug.LogError("Logic Error overlay == 0: " + list.get_Item(j).id);
			}
			else
			{
				while (num > (long)list.get_Item(j).overlay)
				{
					num -= (long)list.get_Item(j).overlay;
					this.fixNewRealTimeDrop.Add(new KeyValuePair<int, long>(list.get_Item(j).id, (long)list.get_Item(j).overlay));
				}
				if (num > 0L)
				{
					this.fixNewRealTimeDrop.Add(new KeyValuePair<int, long>(list.get_Item(j).id, num));
				}
			}
		}
		this.CheckPlayBattleBackpackItem();
	}

	protected void CheckPlayBattleBackpackItem()
	{
		if (this.fixNewRealTimeDrop.get_Count() == 0)
		{
			return;
		}
		KeyValuePair<int, long> keyValuePair = this.fixNewRealTimeDrop.get_Item(0);
		this.fixNewRealTimeDrop.RemoveAt(0);
		if (this.battleUI)
		{
			this.battleUI.PlayBattleBackpackItem(keyValuePair.get_Key(), keyValuePair.get_Value(), new Action(this.CheckPlayBattleBackpackItem));
		}
		else
		{
			this.CheckPlayBattleBackpackItem();
		}
	}

	public override void UpdateRealTimeDrop(XDict<int, long> realTimeDrop)
	{
		this.fixRealTimeDrop.Clear();
		this.fixRealTimeExp = 0L;
		this.fixRealTimeGold = 0L;
		List<Items> list = new List<Items>();
		for (int i = 0; i < realTimeDrop.Keys.get_Count(); i++)
		{
			if (realTimeDrop.Keys.get_Item(i) == 1)
			{
				this.fixRealTimeExp = realTimeDrop.Values.get_Item(i);
			}
			else if (realTimeDrop.Keys.get_Item(i) == 2)
			{
				this.fixRealTimeGold = realTimeDrop.Values.get_Item(i);
			}
			else if (DataReader<Items>.Contains(realTimeDrop.Keys.get_Item(i)))
			{
				if (realTimeDrop.Values.get_Item(i) > 0L)
				{
					list.Add(DataReader<Items>.Get(realTimeDrop.Keys.get_Item(i)));
				}
			}
		}
		list.Sort(ComparisonUtility.ItemComparison);
		for (int j = 0; j < list.get_Count(); j++)
		{
			long num = realTimeDrop[list.get_Item(j).id];
			if (list.get_Item(j).overlay == 0)
			{
				Debug.LogError("Logic Error overlay == 0: " + list.get_Item(j).id);
			}
			else
			{
				while (num > (long)list.get_Item(j).overlay)
				{
					num -= (long)list.get_Item(j).overlay;
					this.fixRealTimeDrop.Add(new KeyValuePair<int, long>(list.get_Item(j).id, (long)list.get_Item(j).overlay));
				}
				if (num > 0L)
				{
					this.fixRealTimeDrop.Add(new KeyValuePair<int, long>(list.get_Item(j).id, num));
				}
			}
		}
		if (this.battleUI && EntityWorld.Instance.EntSelf != null)
		{
			long defaultBatchExp = 0L;
			if (DataReader<GuaJiQuYuPeiZhi>.Contains(HuntManager.Instance.AreaId))
			{
				defaultBatchExp = DataReader<GuaJiQuYuPeiZhi>.Get(HuntManager.Instance.AreaId).exp;
			}
			this.battleUI.ShowRewardPreviewUI(true, RewardPreviewUI.CopyType.HOOK, this.fixRealTimeExp, defaultBatchExp);
		}
		this.battleBackpackUI = (UIManagerControl.Instance.GetUIIfExist("BattleBackpackUI") as BattleBackpackUI);
		if (this.battleBackpackUI)
		{
			this.battleBackpackUI.SetItem(this.fixRealTimeDrop, this.backpackLockInfo, string.Empty);
		}
	}
}
