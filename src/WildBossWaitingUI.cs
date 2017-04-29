using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildBossWaitingUI : UIBase
{
	protected Image WildBossWaitingUICurrentZoneBossIcon;

	protected Text WildBossWaitingUICurrentZoneBossName;

	protected Text WildBossWaitingUICurrentZoneBossRank;

	protected Image WildBossWaitingUICurrentZoneBossBloodFG;

	protected ListPool WildBossWaitingUICurrentZoneList;

	protected ListPool WildBossWaitingUIWaitingZoneList;

	protected Text WildBossWaitingUIWaitingZoneUnitRank;

	protected Image WildBossWaitingUIWaitingZoneUnitHeadIcon;

	protected Text WildBossWaitingUIWaitingZoneUnitName;

	protected Text WildBossWaitingUIWaitingZoneUnitFighting;

	protected Image WildBossWaitingUIRefreshFG;

	protected bool isInRefreshCD;

	protected float defaultRefreshTime = 5f;

	protected float currentTime;

	protected bool hasShowRefreshTip;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.WildBossWaitingUICurrentZoneBossIcon = base.FindTransform("WildBossWaitingUICurrentZoneBossIcon").GetComponent<Image>();
		this.WildBossWaitingUICurrentZoneBossName = base.FindTransform("WildBossWaitingUICurrentZoneBossName").GetComponent<Text>();
		this.WildBossWaitingUICurrentZoneBossRank = base.FindTransform("WildBossWaitingUICurrentZoneBossRank").GetComponent<Text>();
		this.WildBossWaitingUICurrentZoneBossBloodFG = base.FindTransform("WildBossWaitingUICurrentZoneBossBloodFG").GetComponent<Image>();
		this.WildBossWaitingUICurrentZoneList = base.FindTransform("WildBossWaitingUICurrentZoneList").GetComponent<ListPool>();
		this.WildBossWaitingUICurrentZoneList.SetItem("WildBossWaitingUICurrentZoneUnit");
		this.WildBossWaitingUIWaitingZoneList = base.FindTransform("WildBossWaitingUIWaitingZoneList").GetComponent<ListPool>();
		this.WildBossWaitingUIWaitingZoneList.SetItem("WildBossWaitingUIWaitingZoneUnit");
		this.WildBossWaitingUIWaitingZoneUnitRank = base.FindTransform("WildBossWaitingUIWaitingZoneUnitRank").GetComponent<Text>();
		this.WildBossWaitingUIWaitingZoneUnitHeadIcon = base.FindTransform("WildBossWaitingUIWaitingZoneUnitHeadIcon").GetComponent<Image>();
		this.WildBossWaitingUIWaitingZoneUnitName = base.FindTransform("WildBossWaitingUIWaitingZoneUnitName").GetComponent<Text>();
		this.WildBossWaitingUIWaitingZoneUnitFighting = base.FindTransform("WildBossWaitingUIWaitingZoneUnitFighting").GetComponent<Text>();
		this.WildBossWaitingUIRefreshFG = base.FindTransform("WildBossWaitingUIRefreshFG").GetComponent<Image>();
		base.FindTransform("WildBossWaitingUITitleName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505158, false));
		base.FindTransform("WildBossWaitingUICurrentZoneTitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505184, false));
		base.FindTransform("WildBossWaitingUIWaitingZoneTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505181, false));
		base.FindTransform("WildBossWaitingUITip").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505182, false));
		base.FindTransform("WildBossWaitingUIQuitText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505177, false));
		base.FindTransform("WildBossWaitingUIRefreshText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505183, false));
		base.FindTransform("WildBossWaitingUICloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickClose);
		base.FindTransform("WildBossWaitingUIQuit").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuit);
		base.FindTransform("WildBossWaitingUIRefresh").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefresh);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.Broadcast(WildBossManagerEvent.UpdateQueue);
		this.StartRefreshButton();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void Update()
	{
		this.RefreshButton();
	}

	protected void OnClickClose(GameObject go)
	{
		this.Show(false);
	}

	protected void OnClickQuit(GameObject go)
	{
		EventDispatcher.Broadcast(WildBossManagerEvent.QuitQueue);
	}

	protected void OnClickRefresh(GameObject go)
	{
		this.RefreshButton();
		if (this.isInRefreshCD)
		{
			if (!this.hasShowRefreshTip)
			{
				this.hasShowRefreshTip = true;
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(511634, false));
			}
		}
		else
		{
			EventDispatcher.Broadcast(WildBossManagerEvent.UpdateQueue);
			this.StartRefreshButton();
		}
	}

	public void SetData(int bossIcon, string bossName, int bossRank, float bossPercetage, List<WildBossUICurrentData> currentDataList, List<WildBossUIWaitingData> waitingDataList, WildBossUIWaitingData selfWaitingData)
	{
		this.SetBossHeadIcon(bossIcon);
		this.SetBossName(bossName);
		this.SetBossRank(bossRank);
		this.SetBossHp(bossPercetage);
		this.SetCurrentData(currentDataList);
		this.SetWaitingData(waitingDataList);
		if (selfWaitingData != null)
		{
			this.SetSelfRank(selfWaitingData.rank);
			this.SetSelfHeadIcon(selfWaitingData.career);
			this.SetSelfName(selfWaitingData.name);
			this.SetSelfFighting((long)selfWaitingData.fighting);
		}
	}

	protected void SetBossHeadIcon(int icon)
	{
		ResourceManager.SetSprite(this.WildBossWaitingUICurrentZoneBossIcon, GameDataUtils.GetIcon(icon));
	}

	protected void SetBossName(string name)
	{
		this.WildBossWaitingUICurrentZoneBossName.set_text(name);
	}

	protected void SetBossRank(int rank)
	{
		this.WildBossWaitingUICurrentZoneBossRank.set_text(string.Format(GameDataUtils.GetChineseContent(505185, false), rank));
	}

	protected void SetBossHp(float percentage)
	{
		this.WildBossWaitingUICurrentZoneBossBloodFG.set_fillAmount(percentage);
	}

	protected void SetCurrentData(List<WildBossUICurrentData> currentDataList)
	{
		this.WildBossWaitingUICurrentZoneList.Create(currentDataList.get_Count(), delegate(int index)
		{
			if (index < currentDataList.get_Count() && index < this.WildBossWaitingUICurrentZoneList.Items.get_Count())
			{
				WildBossWaitingUICurrentZoneUnit component = this.WildBossWaitingUICurrentZoneList.Items.get_Item(index).GetComponent<WildBossWaitingUICurrentZoneUnit>();
				component.SetData(currentDataList.get_Item(index).career, currentDataList.get_Item(index).name, (float)((double)currentDataList.get_Item(index).curHp / (double)currentDataList.get_Item(index).hpLmt));
			}
		});
	}

	protected void SetWaitingData(List<WildBossUIWaitingData> waitingDataList)
	{
		this.WildBossWaitingUIWaitingZoneList.Create(waitingDataList.get_Count(), delegate(int index)
		{
			if (index < waitingDataList.get_Count() && index < this.WildBossWaitingUIWaitingZoneList.Items.get_Count())
			{
				WildBossWaitingUIWaitingZoneUnit component = this.WildBossWaitingUIWaitingZoneList.Items.get_Item(index).GetComponent<WildBossWaitingUIWaitingZoneUnit>();
				component.SetData(waitingDataList.get_Item(index).rank, waitingDataList.get_Item(index).career, waitingDataList.get_Item(index).name, (long)waitingDataList.get_Item(index).fighting);
			}
		});
	}

	protected void SetSelfRank(int rank)
	{
		this.WildBossWaitingUIWaitingZoneUnitRank.set_text(rank.ToString());
	}

	protected void SetSelfHeadIcon(int career)
	{
		ResourceManager.SetSprite(this.WildBossWaitingUIWaitingZoneUnitHeadIcon, UIUtils.GetRoleSmallIcon(career));
	}

	protected void SetSelfName(string name)
	{
		this.WildBossWaitingUIWaitingZoneUnitName.set_text(name);
	}

	protected void SetSelfFighting(long fighting)
	{
		this.WildBossWaitingUIWaitingZoneUnitFighting.set_text(string.Format(GameDataUtils.GetChineseContent(502070, false), fighting));
	}

	protected void StartRefreshButton()
	{
		this.isInRefreshCD = true;
		this.hasShowRefreshTip = false;
		this.currentTime = 0f;
	}

	protected void RefreshButton()
	{
		if (!this.isInRefreshCD)
		{
			return;
		}
		this.currentTime += Time.get_deltaTime();
		if (this.currentTime >= this.defaultRefreshTime)
		{
			this.isInRefreshCD = false;
			this.currentTime = 0f;
			this.WildBossWaitingUIRefreshFG.set_fillAmount(1f);
		}
		else
		{
			this.WildBossWaitingUIRefreshFG.set_fillAmount(this.currentTime / this.defaultRefreshTime);
		}
	}
}
