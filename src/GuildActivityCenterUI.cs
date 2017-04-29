using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildActivityCenterUI : UIBase
{
	private Transform activityRoot1Trans;

	private Transform activityRoot2Trans;

	private Transform activityRewardListTrans1;

	private Transform activityRewardListTrans2;

	private ButtonCustom joinBtn;

	private List<int> bonfireRewardList;

	private List<int> competitionRewardList;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("JoinBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickJoinBtn);
		this.activityRoot1Trans = base.FindTransform("ActivityRoot1");
		this.activityRoot2Trans = base.FindTransform("ActivityRoot2");
		this.activityRewardListTrans1 = this.activityRoot1Trans.FindChild("ActivityRewardList");
		this.activityRewardListTrans2 = this.activityRoot2Trans.FindChild("ActivityRewardList");
		this.bonfireRewardList = new List<int>();
		this.competitionRewardList = new List<int>();
		string value = DataReader<GongHuiXinXi>.Get("BonfireReward").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			this.bonfireRewardList.Add((int)float.Parse(array[i]));
		}
		string value2 = DataReader<GongHuiXinXi>.Get("CompetitionReward").value;
		string[] array2 = value2.Split(new char[]
		{
			';'
		});
		for (int j = 0; j < array2.Length; j++)
		{
			this.competitionRewardList.Add((int)float.Parse(array2[j]));
		}
		this.RefreshUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		base.ReleaseSelf(destroy);
	}

	private void OnClickJoinBtn(GameObject go)
	{
		EventDispatcher.Broadcast(CityManagerEvent.EnterGuildField);
	}

	private void RefreshUI()
	{
		Text component = this.activityRoot1Trans.FindChild("ActivityName").GetComponent<Text>();
		component.set_text(GameDataUtils.GetChineseContent(515057, false));
		Text component2 = this.activityRoot1Trans.FindChild("ActivityNameDescText").GetComponent<Text>();
		component2.set_text(GameDataUtils.GetChineseContent(515054, false).Replace("/n", "\n"));
		Text component3 = this.activityRoot2Trans.FindChild("ActivityName").GetComponent<Text>();
		component3.set_text(GameDataUtils.GetChineseContent(515056, false));
		Text component4 = this.activityRoot2Trans.FindChild("ActivityNameDescText").GetComponent<Text>();
		component4.set_text(GameDataUtils.GetChineseContent(515055, false).Replace("/n", "\n"));
		this.RefreshBonfireRewardList();
		this.RefreshCompetionRewardList();
	}

	private void RefreshBonfireRewardList()
	{
		for (int i = 0; i < this.activityRewardListTrans2.get_childCount(); i++)
		{
			Transform child = this.activityRewardListTrans2.GetChild(i);
			Object.Destroy(child.get_gameObject());
		}
		for (int j = 0; j < this.bonfireRewardList.get_Count(); j++)
		{
			ItemShow.ShowItem(this.activityRewardListTrans2, this.bonfireRewardList.get_Item(j), -1L, false, null, 2001);
		}
	}

	private void RefreshCompetionRewardList()
	{
		for (int i = 0; i < this.activityRewardListTrans1.get_childCount(); i++)
		{
			Transform child = this.activityRewardListTrans1.GetChild(i);
			Object.Destroy(child.get_gameObject());
		}
		for (int j = 0; j < this.competitionRewardList.get_Count(); j++)
		{
			ItemShow.ShowItem(this.activityRewardListTrans1, this.competitionRewardList.get_Item(j), -1L, false, null, 2001);
		}
	}
}
