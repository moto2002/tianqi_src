using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPVPRewardItem : BaseUIBehaviour
{
	private Text rewardTypeDesc;

	private Text rewardTargetDesc;

	private Image rewardIcon;

	private MultiPvpDailyRewardInfoNty.MultiPvpRewardInfo m_rewardInfo;

	private bool isCanGet;

	private bool isInit;

	private int rewardIconFXID;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.rewardTargetDesc = base.FindTransform("RewardTargetDesc").GetComponent<Text>();
		this.rewardTypeDesc = base.FindTransform("RewardTypeDesc").GetComponent<Text>();
		this.rewardIcon = base.FindTransform("RewardIcon").GetComponent<Image>();
		this.rewardIcon.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetReward);
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		FXSpineManager.Instance.DeleteSpine(this.rewardIconFXID, true);
		base.OnDestroy();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void OnClickGetReward(GameObject go)
	{
		if (this.m_rewardInfo == null)
		{
			return;
		}
		if (this.m_rewardInfo.getFlag)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502214, false));
			return;
		}
		if (DataReader<PVPMeiRiJiangLi>.Contains(this.m_rewardInfo.rewardId))
		{
			if (!this.isCanGet)
			{
				List<int> reward = DataReader<PVPMeiRiJiangLi>.Get(this.m_rewardInfo.rewardId).reward;
				List<int> list = new List<int>();
				List<long> list2 = new List<long>();
				for (int i = 0; i < reward.get_Count(); i++)
				{
					int dropID = reward.get_Item(i);
					List<KeyValuePair<int, long>> dropRewardLit = MultiPVPManager.Instance.GetDropRewardLit(dropID);
					if (dropRewardLit != null && dropRewardLit.get_Count() > 0)
					{
						for (int j = 0; j < dropRewardLit.get_Count(); j++)
						{
							list.Add(dropRewardLit.get_Item(j).get_Key());
							list2.Add(dropRewardLit.get_Item(j).get_Value());
						}
					}
				}
				if (list != null && list.get_Count() > 0)
				{
					RewardUI rewardUI = UIManagerControl.Instance.OpenUI("RewardUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as RewardUI;
					rewardUI.SetRewardItem(GameDataUtils.GetChineseContent(513163, false), list, list2, true, false, null, null);
				}
				return;
			}
			int type = DataReader<PVPMeiRiJiangLi>.Get(this.m_rewardInfo.rewardId).type;
			MultiPVPManager.Instance.SendMultiPvpGetDailyRewardReq(type);
		}
	}

	public void UpdateUIData(MultiPvpDailyRewardInfoNty.MultiPvpRewardInfo rewardInfo)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.m_rewardInfo = rewardInfo;
		int rewardId = rewardInfo.rewardId;
		bool getFlag = rewardInfo.getFlag;
		FXSpineManager.Instance.DeleteSpine(this.rewardIconFXID, true);
		if (DataReader<PVPMeiRiJiangLi>.Contains(rewardId))
		{
			PVPMeiRiJiangLi pVPMeiRiJiangLi = DataReader<PVPMeiRiJiangLi>.Get(rewardId);
			int num = (pVPMeiRiJiangLi.completeTarget.get_Count() < 2) ? 0 : pVPMeiRiJiangLi.completeTarget.get_Item(1);
			int num2 = (rewardInfo.process < num) ? rewardInfo.process : num;
			this.rewardTypeDesc.set_text(GameDataUtils.GetChineseContent(pVPMeiRiJiangLi.name, false));
			this.rewardTargetDesc.set_text(string.Format(GameDataUtils.GetChineseContent(pVPMeiRiJiangLi.state, false), num2, num));
			if (num2 >= num)
			{
				this.isCanGet = true;
			}
			if (getFlag)
			{
				ResourceManager.SetSprite(this.rewardIcon, GameDataUtils.GetIcon(1603));
			}
			else
			{
				ResourceManager.SetSprite(this.rewardIcon, GameDataUtils.GetIcon(pVPMeiRiJiangLi.icon));
			}
			if (this.isCanGet && !getFlag)
			{
				this.rewardIconFXID = FXSpineManager.Instance.ReplaySpine(this.rewardIconFXID, 610, this.rewardIcon.get_transform(), "MultiPVPUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		}
	}
}
