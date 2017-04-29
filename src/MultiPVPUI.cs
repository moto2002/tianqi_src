using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPVPUI : UIBase
{
	private Text activityRewardTip;

	private Text lvLimitText;

	private Text openTimeText;

	private Text openTime2Text;

	private Text joinTimesText;

	private Text activityNameText;

	private Text activityRemainTimeText;

	private ListPool rewardItemsListPool;

	private ListPool multiPvpRewardListPool;

	private TimeCountDown remainTimeCD;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.SetMask(0.7f, true, true);
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.lvLimitText = base.FindTransform("TextLvLimitName").GetComponent<Text>();
		this.openTimeText = base.FindTransform("TextOpenTime").GetComponent<Text>();
		this.joinTimesText = base.FindTransform("TextJoinTimeName").GetComponent<Text>();
		this.activityNameText = base.FindTransform("TextActivityNameTitle").GetComponent<Text>();
		this.activityRewardTip = base.FindTransform("ActivityRewardTip").GetComponent<Text>();
		this.activityRemainTimeText = base.FindTransform("TextRemainTime").GetComponent<Text>();
		this.openTime2Text = base.FindTransform("TextOpenTime2").GetComponent<Text>();
		this.multiPvpRewardListPool = base.FindTransform("MultiPvpRewardListPool").GetComponent<ListPool>();
		this.rewardItemsListPool = base.FindTransform("RewardItemsList").GetComponent<ListPool>();
		base.FindTransform("PVPShop").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickShop);
		base.FindTransform("BtnMatch").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnMatch);
		this.ResetUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Arena, base.get_transform(), 0);
		changePetChooseUI.Show(true);
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110047), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.RemoveRemainTimeCD();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnMultiPvpDailyRewardInfoNty, new Callback(this.OnMultiPvpDailyRewardInfoNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnMultiPvpDailyRewardInfoNty, new Callback(this.OnMultiPvpDailyRewardInfoNty));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnMatch(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.LeaderID != EntityWorld.Instance.EntSelf.ID)
		{
			UIManagerControl.Instance.ShowToastText("您不是队长，请等待队长开启！");
			return;
		}
		MultiPVPManager.Instance.SendMultiPvpBeginMatchReq();
	}

	private void OnClickShop(GameObject go)
	{
		LinkNavigationManager.OpenPVPShopUI();
	}

	private void ResetUI()
	{
		base.FindTransform("LeftUpTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(503104, false));
		base.FindTransform("LeftDownTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(503106, false));
		base.FindTransform("TextTitle").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(503100, false));
		this.lvLimitText.set_text(string.Empty);
		this.openTimeText.set_text(string.Empty);
		this.openTime2Text.set_text(string.Empty);
		this.joinTimesText.set_text(string.Empty);
		this.activityRemainTimeText.set_text(string.Empty);
		this.activityRewardTip.set_text(GameDataUtils.GetChineseContent(503105, false));
		this.activityNameText.set_text(GameDataUtils.GetChineseContent(503101, false));
		this.rewardItemsListPool.Clear();
		this.multiPvpRewardListPool.Clear();
	}

	private void RefreshUI()
	{
		this.UpdateMultiPVPActInfo();
		this.UpdateMultiPvpRewardList();
	}

	private void UpdateMultiPVPActInfo()
	{
		HuoDongZhongXin activityCfgData = ActivityCenterManager.Instance.GetActivityCfgData(ActivityType.MultiPVP);
		if (activityCfgData != null)
		{
			this.lvLimitText.set_text(string.Format(GameDataUtils.GetChineseContent(503102, false), activityCfgData.minLv));
			string text = (activityCfgData.num > 0) ? (activityCfgData.num + string.Empty) : "无限制";
			this.joinTimesText.set_text(string.Format(GameDataUtils.GetChineseContent(503103, false), text));
			this.openTimeText.set_text(MultiPVPManager.Instance.GetMultiPvpOpenTimeText(0));
			this.openTime2Text.set_text(MultiPVPManager.Instance.GetMultiPvpOpenTimeText(1));
			this.UpdateActivityRewardList(activityCfgData.award);
			int activityRemainTime = MultiPVPManager.Instance.GetActivityRemainTime();
			this.SetRemainTimeCD(activityRemainTime);
		}
	}

	private void UpdateMultiPvpRewardList()
	{
		this.multiPvpRewardListPool.Clear();
		if (MultiPVPManager.Instance.MultiPvpRewardInfoList != null && MultiPVPManager.Instance.MultiPvpRewardInfoList.get_Count() > 0)
		{
			this.multiPvpRewardListPool.Create(MultiPVPManager.Instance.MultiPvpRewardInfoList.get_Count(), delegate(int index)
			{
				if (index < MultiPVPManager.Instance.MultiPvpRewardInfoList.get_Count() && index < this.multiPvpRewardListPool.Items.get_Count())
				{
					MultiPVPRewardItem component = this.multiPvpRewardListPool.Items.get_Item(index).GetComponent<MultiPVPRewardItem>();
					if (component != null)
					{
						component.UpdateUIData(MultiPVPManager.Instance.MultiPvpRewardInfoList.get_Item(index));
					}
				}
			});
		}
	}

	private void UpdateActivityRewardList(List<int> awardIconIDList)
	{
		this.rewardItemsListPool.Clear();
		if (awardIconIDList != null && awardIconIDList.get_Count() > 0)
		{
			this.rewardItemsListPool.Create(awardIconIDList.get_Count(), delegate(int index)
			{
				if (index < awardIconIDList.get_Count() && index < this.rewardItemsListPool.Items.get_Count())
				{
					RewardItem component = this.rewardItemsListPool.Items.get_Item(index).GetComponent<RewardItem>();
					if (component != null)
					{
						component.SetRewardItem(awardIconIDList.get_Item(index), -1L, 0L);
					}
				}
			});
		}
	}

	private void OnMultiPvpDailyRewardInfoNty()
	{
		this.UpdateMultiPvpRewardList();
	}

	private void SetRemainTimeCD(int remainTime)
	{
		this.RemoveRemainTimeCD();
		if (remainTime > 0)
		{
			this.remainTimeCD = new TimeCountDown(remainTime, TimeFormat.HHMMSS, delegate
			{
				string time = TimeConverter.GetTime(remainTime, TimeFormat.HHMMSS);
				if (this.remainTimeCD != null)
				{
					time = this.remainTimeCD.GetTime();
				}
				string text = GameDataUtils.GetChineseContent(510116, false);
				text = text.Replace("{s1}", time);
				this.activityRemainTimeText.set_text(text);
			}, delegate
			{
				this.RemoveRemainTimeCD();
				this.activityRemainTimeText.set_text(string.Empty);
			}, true);
		}
		else
		{
			this.activityRemainTimeText.set_text(string.Empty);
		}
	}

	private void RemoveRemainTimeCD()
	{
		if (this.remainTimeCD != null)
		{
			this.remainTimeCD.Dispose();
			this.remainTimeCD = null;
		}
	}
}
