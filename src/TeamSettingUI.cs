using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSettingUI : UIBase
{
	private ListPool teamTargetListPool;

	private Text minLVText;

	private Text maxLVText;

	private Text teamLVTitle;

	private TeamTargetItem lastSelectTargetItem;

	private Text activityOpenTimeText;

	private int teamMinCfgLv;

	private int minCfgLv;

	private int maxCfgLv;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.minLVText = base.FindTransform("MinLVText").GetComponent<Text>();
		this.maxLVText = base.FindTransform("MaxLVText").GetComponent<Text>();
		this.teamLVTitle = base.FindTransform("TeamLVTitle").GetComponent<Text>();
		this.activityOpenTimeText = base.FindTransform("ActivityTimeName").GetComponent<Text>();
		this.teamTargetListPool = base.FindTransform("TeamTargetList").GetComponent<ListPool>();
		base.FindTransform("SureBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSureBtn);
		string value = DataReader<team>.Get("MinimumLv").value;
		this.teamMinCfgLv = (int)float.Parse(value);
		this.minCfgLv = this.teamMinCfgLv;
		string value2 = DataReader<team>.Get("MaximumLv").value;
		this.maxCfgLv = (int)float.Parse(value2);
		this.activityOpenTimeText.set_text(string.Empty);
		this.minLVText.set_text(this.minCfgLv + string.Empty);
		this.maxLVText.set_text(this.maxCfgLv + string.Empty);
		this.teamLVTitle.set_text(GameDataUtils.GetChineseContent(50736, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.teamTargetListPool.Clear();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void RefreshUI()
	{
		if (TeamBasicManager.Instance.MyTeamData != null)
		{
			this.minLVText.set_text(TeamBasicManager.Instance.MyTeamData.MinLV.ToString());
			this.maxLVText.set_text(this.maxCfgLv + string.Empty);
		}
		this.RefreshTargetList();
	}

	private void RefreshTargetList()
	{
		this.teamTargetListPool.Clear();
		List<DuiWuMuBiao> allTeamTargetList = TeamBasicManager.Instance.GetAllTeamTargetCfgList();
		if (allTeamTargetList != null && allTeamTargetList.get_Count() > 0)
		{
			this.teamTargetListPool.Create(allTeamTargetList.get_Count(), delegate(int index)
			{
				if (index < allTeamTargetList.get_Count() && index < this.teamTargetListPool.Items.get_Count())
				{
					TeamTargetItem component = this.teamTargetListPool.Items.get_Item(index).GetComponent<TeamTargetItem>();
					if (component != null)
					{
						component.UpdateUI(allTeamTargetList.get_Item(index));
						component.Selected = (index == 0);
						if (index == 0)
						{
							this.OnClickSelectTarget(component.get_gameObject());
						}
						else
						{
							component.Selected = false;
						}
						component.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectTarget);
					}
				}
			});
		}
	}

	private void SetTeamLevelLimit()
	{
		if (this.lastSelectTargetItem != null && this.lastSelectTargetItem.TeamTargetCfgData != null)
		{
			this.minCfgLv = this.lastSelectTargetItem.TeamTargetCfgData.Lv;
			this.minLVText.set_text(this.minCfgLv + string.Empty);
		}
	}

	private void SetActivityOpenTime()
	{
		this.activityOpenTimeText.set_text(string.Empty);
		if (this.lastSelectTargetItem != null && this.lastSelectTargetItem.DungeonType == 103)
		{
			string activityOpenTimeByActivityType = ActivityCenterManager.Instance.GetActivityOpenTimeByActivityType(ActivityType.MultiPeople);
			this.activityOpenTimeText.set_text(GameDataUtils.GetChineseContent(513168, false) + activityOpenTimeByActivityType);
		}
	}

	private void OnClickSureBtn(GameObject go)
	{
		int num = int.Parse(this.minLVText.get_text());
		int num2 = int.Parse(this.maxLVText.get_text());
		if (num > EntityWorld.Instance.EntSelf.Lv)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516132, false));
			return;
		}
		if (num < this.minCfgLv)
		{
			string text = "最小等级不能低于" + this.minCfgLv + "级";
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if (num2 > this.maxCfgLv)
		{
			string text2 = "最大等级不能高于" + this.maxCfgLv + "级";
			this.maxLVText.set_text(this.maxCfgLv.ToString());
			UIManagerControl.Instance.ShowToastText(text2);
			return;
		}
		if (num > num2)
		{
			string chineseContent = GameDataUtils.GetChineseContent(516113, false);
			UIManagerControl.Instance.ShowToastText(chineseContent);
			this.minLVText.set_text(num.ToString());
			this.maxLVText.set_text(num.ToString());
			return;
		}
		if (this.lastSelectTargetItem != null)
		{
			if (this.lastSelectTargetItem.DungeonType < 100)
			{
				this.lastSelectTargetItem.DungeonType = 100;
			}
			TeamBasicManager.Instance.SendTeamSettingReq(num, num2, (DungeonType.ENUM)this.lastSelectTargetItem.DungeonType, this.lastSelectTargetItem.DungeonParams);
		}
		else
		{
			TeamBasicManager.Instance.SendTeamSettingReq(num, num2, DungeonType.ENUM.Other, null);
		}
		this.Show(false);
	}

	private void OnClickSelectTarget(GameObject go)
	{
		TeamTargetItem component = go.GetComponent<TeamTargetItem>();
		if (component == this.lastSelectTargetItem)
		{
			return;
		}
		if (this.lastSelectTargetItem != null)
		{
			this.lastSelectTargetItem.Selected = false;
		}
		if (component != null)
		{
			component.Selected = true;
		}
		this.lastSelectTargetItem = component;
		this.SetTeamLevelLimit();
		this.SetActivityOpenTime();
	}
}
