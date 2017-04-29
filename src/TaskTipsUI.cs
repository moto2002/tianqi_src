using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTipsUI : UIBase
{
	private Text mTxTitle;

	private Text mTxContent;

	private List<RewardItem> mRewards;

	private GameObject mRewardPanel;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.alpha = 0.7f;
		this.isMask = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ClearRewards();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mRewards = new List<RewardItem>();
		this.mRewardPanel = UIHelper.GetObject(base.get_transform(), "Rewards");
		UIHelper.GetText(base.get_transform(), "txReward").set_text("获得：");
		this.mTxTitle = UIHelper.GetText(base.get_transform(), "Title/Text");
		this.mTxContent = UIHelper.GetText(base.get_transform(), "txContent");
	}

	public void OnOpen(MeiRiMuBiao daily)
	{
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(daily.sysId);
		string text = (systemOpen.level <= 0) ? string.Empty : (systemOpen.level + GameDataUtils.GetChineseContent(301042, false));
		if (systemOpen.taskId > 0)
		{
			RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(systemOpen.taskId);
			if (renWuPeiZhi != null)
			{
				text += string.Format(GameDataUtils.GetChineseContent(301043, false), GameDataUtils.GetChineseContent(renWuPeiZhi.dramaIntroduce, false));
			}
		}
		this.mTxTitle.set_text(GameDataUtils.GetChineseContent(daily.introduction1, false));
		string text2 = string.Format(GameDataUtils.GetChineseContent(301044, false), GameDataUtils.GetChineseContent(daily.introduction4, false));
		text2 += string.Format(GameDataUtils.GetChineseContent(301045, false), GameDataUtils.GetChineseContent(daily.introduction1, false));
		text2 += string.Format(GameDataUtils.GetChineseContent(301046, false), text);
		text2 += string.Format(GameDataUtils.GetChineseContent(301047, false), GameDataUtils.GetChineseContent(daily.introduction3, false));
		this.mTxContent.set_text(text2);
		this.ClearRewards();
		for (int i = 0; i < daily.rewardIntroductionIcon.get_Count(); i++)
		{
			this.CreateReward(daily.rewardIntroductionIcon.get_Item(i));
		}
	}

	private void CreateReward(int id)
	{
		if (id > 0)
		{
			RewardItem rewardItem = this.mRewards.Find((RewardItem e) => e.get_gameObject().get_name() == "Unused");
			if (rewardItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("RewardItem");
				UGUITools.SetParent(this.mRewardPanel, instantiate2Prefab, false);
				rewardItem = instantiate2Prefab.GetComponent<RewardItem>();
				this.mRewards.Add(rewardItem);
			}
			rewardItem.get_gameObject().set_name(id.ToString());
			rewardItem.SetRewardItem(id, 1L, 0L);
			rewardItem.get_gameObject().SetActive(true);
		}
	}

	private void ClearRewards()
	{
		for (int i = 0; i < this.mRewards.get_Count(); i++)
		{
			this.mRewards.get_Item(i).get_gameObject().SetActive(false);
			this.mRewards.get_Item(i).set_name("Unused");
		}
	}
}
