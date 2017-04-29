using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DarkTrialInstance : BattleInstanceParent<MultiPveSettleNty>
{
	private static DarkTrialInstance instance;

	protected BattleUI battleUI;

	protected BattleBackpackUI battleBackpackUI;

	protected XDict<int, DarkLevel> rankDataList = new XDict<int, DarkLevel>();

	protected List<KeyValuePair<int, long>> fixRealTimeDrop = new List<KeyValuePair<int, long>>();

	protected long fixRealTimeExp;

	protected long fixRealTimeGold;

	protected List<KeyValuePair<int, long>> fixNewRealTimeDrop = new List<KeyValuePair<int, long>>();

	public static DarkTrialInstance Instance
	{
		get
		{
			if (DarkTrialInstance.instance == null)
			{
				DarkTrialInstance.instance = new DarkTrialInstance();
			}
			return DarkTrialInstance.instance;
		}
	}

	protected DarkTrialInstance()
	{
		base.Type = InstanceType.DarkTrial;
	}

	public override void ReleaseData()
	{
		this.rankDataList.Clear();
		this.fixRealTimeDrop.Clear();
		this.fixRealTimeExp = 0L;
		this.fixRealTimeGold = 0L;
		this.fixNewRealTimeDrop.Clear();
	}

	public override void SetCommonLogic()
	{
		List<DarkLevel> dataList = DataReader<DarkLevel>.DataList;
		List<DarkLevel> list = new List<DarkLevel>();
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).Id == this.InstanceDataID)
			{
				list.Add(dataList.get_Item(i));
			}
		}
		list.Sort(ComparisonUtility.DarkLevelComparison);
		for (int j = 0; j < list.get_Count(); j++)
		{
			this.rankDataList.Add(list.get_Item(j).Lv, list.get_Item(j));
		}
		list.Clear();
	}

	public override void ShowBattleUI()
	{
		this.battleUI = LinkNavigationManager.OpenBattleUI();
		this.battleUI.BtnQuitAction = delegate
		{
			UIManagerControl.Instance.OpenUI("GlobalBattleDialogUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(510109, false), GameDataUtils.GetChineseContent(50739, false), delegate
			{
			}, delegate
			{
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				this.ExitDarkTrialInstance();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
			GlobalBattleDialogUIView.Instance.isClick = false;
		};
		this.battleUI.ResetAllInstancePart();
		this.battleUI.ShowBattleTimeUI(true);
		this.battleUI.ShowGlobalRank(true, BattleUI.RankRewardType.Icon);
		this.battleUI.ShowTopLeftTabs(true, new BattleUI.TopLeftTabData[]
		{
			new BattleUI.TopLeftTabData
			{
				name = GameDataUtils.GetChineseContent(515128, false),
				showAction = new Action<bool>(this.battleUI.TabShowGlobalRank),
				stretchGameObject = this.battleUI.GlobalRank
			},
			new BattleUI.TopLeftTabData
			{
				name = GameDataUtils.GetChineseContent(515127, false),
				showAction = new Action<bool>(this.battleUI.TabShowTeamMember),
				stretchGameObject = this.battleUI.TeamMember
			}
		});
		this.battleUI.ShowBackpackPreviewBtn(true);
		this.battleUI.IsPauseCheck = false;
		this.battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	public override void GiveUpRelive()
	{
		this.ExitDarkTrialInstance();
	}

	public override void GetInstanceResult(MultiPveSettleNty result)
	{
		if (InstanceManager.CurrentInstanceType != base.Type)
		{
			return;
		}
		base.GetInstanceResult(result);
		if (base.InstanceResult.isWin)
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
		CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
		if (commonBattlePassUI)
		{
			commonBattlePassUI.UpdateDarkTrialReward(true, InstanceManager.CurUsedTime, InstanceManager.CurrentInstanceBatch, this.fixRealTimeExp, this.fixRealTimeGold, this.fixRealTimeDrop, new Action(this.ExitDarkTrialInstance));
			commonBattlePassUI.UpdateDungeonRewards(this.instanceResult.items);
			commonBattlePassUI.PlayAnimation(InstanceResultType.Win);
			commonBattlePassUI.OnCountDownToExit(5, new Action(this.ExitDarkTrialInstance));
		}
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		CommonBattlePassUI commonBattlePassUI = LinkNavigationManager.OpenCommonBattlePassUI();
		if (commonBattlePassUI)
		{
			commonBattlePassUI.UpdateDarkTrialReward(false, InstanceManager.CurUsedTime, (InstanceManager.CurrentInstanceBatch - 1 >= 0) ? (InstanceManager.CurrentInstanceBatch - 1) : 0, this.fixRealTimeExp, this.fixRealTimeGold, this.fixRealTimeDrop, new Action(this.ExitDarkTrialInstance));
			commonBattlePassUI.UpdateDungeonRewards(this.instanceResult.items);
			commonBattlePassUI.PlayAnimation(InstanceResultType.TimesUp);
			commonBattlePassUI.OnCountDownToExit(5, new Action(this.ExitDarkTrialInstance));
		}
	}

	protected void ExitDarkTrialInstance()
	{
		DarkTrialManager.Instance.ExitDarkTrial();
	}

	public override int GetRankByPassTime(int passTime)
	{
		if (this.rankDataList == null)
		{
			return -1;
		}
		if (this.rankDataList.Count == 0)
		{
			return -1;
		}
		List<DarkLevel> values = this.rankDataList.Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (passTime < values.get_Item(i).Time)
			{
				return values.get_Item(i).Lv;
			}
		}
		return values.get_Item(values.get_Count() - 1).Lv;
	}

	public override int GetStandardTimeByRank(int rank)
	{
		if (!this.rankDataList.ContainsKey(rank))
		{
			return 0;
		}
		return this.rankDataList[rank].Time;
	}

	public override string GetRankInfoText(int rank, int remainTime)
	{
		return string.Format(GameDataUtils.GetChineseContent(50706, false), remainTime, this.GetRankString(rank));
	}

	protected string GetRankString(int rank)
	{
		switch (rank)
		{
		case 1:
			return "S";
		case 2:
			return "A";
		case 3:
			return "B";
		case 4:
			return "C";
		default:
			return "D";
		}
	}

	public override XDict<int, long> GetRankReward(int rank)
	{
		if (!this.rankDataList.ContainsKey(rank))
		{
			return null;
		}
		XDict<int, long> xDict = new XDict<int, long>();
		List<int> reward = this.rankDataList[rank].Reward;
		for (int i = 0; i < reward.get_Count(); i++)
		{
			xDict.Add(reward.get_Item(i), -1L);
		}
		return xDict;
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
		this.battleBackpackUI = (UIManagerControl.Instance.GetUIIfExist("BattleBackpackUI") as BattleBackpackUI);
		if (this.battleBackpackUI)
		{
			this.battleBackpackUI.SetItem(this.fixRealTimeDrop, GameDataUtils.GetChineseContent(50727, false));
		}
	}
}
