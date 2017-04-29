using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildBossItem : BaseUIBehaviour
{
	private Image bossStepNumImg;

	private Image bossIconImg;

	private Text bossNameText;

	private Image costIconImg;

	private Text costNumText;

	private JunTuanBOSSMoXing guildBossCfgData;

	private int callCostNum;

	private int callCostItemID;

	private string callBossName;

	private bool isInit;

	private List<KeyValuePair<int, long>> rewardList;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.bossStepNumImg = base.FindTransform("BossStepImgNum").GetComponent<Image>();
		this.bossIconImg = base.FindTransform("BossIcon").GetComponent<Image>();
		this.costIconImg = base.FindTransform("CostIconImg").GetComponent<Image>();
		this.bossNameText = base.FindTransform("BossName").GetComponent<Text>();
		this.costNumText = base.FindTransform("CostText").GetComponent<Text>();
		base.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCallBoss);
		base.FindTransform("BgGift").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSeeReward);
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	private void OnClickCallBoss(GameObject go)
	{
		if (!GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.OpenGuildBoss))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515087, false));
			return;
		}
		if (GuildManager.Instance.MyGuildnfo != null && GuildManager.Instance.MyGuildnfo.guildFund < this.callCostNum)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515086, false));
			return;
		}
		if (GuildBossManager.Instance.GuildBossActivityInfo == null || GuildBossManager.Instance.GuildBossActivityInfo.RemainCallBossTimes <= 0)
		{
			UIManagerControl.Instance.ShowToastText("本周今日召唤次数已用完------需提供中文字符ID");
			return;
		}
		string content = string.Format(GameDataUtils.GetChineseContent(515088, false), this.guildBossCfgData.rank, this.callBossName, this.callCostNum);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, null, delegate
		{
			GuildBossManager.Instance.SendCallGuildBossReq(this.guildBossCfgData.bossId);
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickSeeReward(GameObject go)
	{
		if (this.guildBossCfgData == null)
		{
			return;
		}
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		if (this.rewardList == null)
		{
			this.rewardList = new List<KeyValuePair<int, long>>();
			int rewardDropID = this.guildBossCfgData.rewardShow;
			List<DiaoLuo> list3 = DataReader<DiaoLuo>.DataList.FindAll((DiaoLuo a) => a.ruleId == rewardDropID);
			if (list3 == null)
			{
				return;
			}
			for (int i = 0; i < list3.get_Count(); i++)
			{
				DiaoLuo diaoLuo = list3.get_Item(i);
				if (diaoLuo.dropType == 1)
				{
					list.Add(diaoLuo.goodsId);
					list2.Add(diaoLuo.minNum);
					this.rewardList.Add(new KeyValuePair<int, long>(diaoLuo.goodsId, diaoLuo.minNum));
				}
			}
		}
		else
		{
			for (int j = 0; j < this.rewardList.get_Count(); j++)
			{
				list.Add(this.rewardList.get_Item(j).get_Key());
				list2.Add(this.rewardList.get_Item(j).get_Value());
			}
		}
		if (list.get_Count() > 0)
		{
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
			rewardUI.get_transform().SetAsLastSibling();
			rewardUI.SetRewardItem(GameDataUtils.GetChineseContent(513163, false), list, list2, true, true, null, null);
		}
	}

	public void UpdateBossItemData(JunTuanBOSSMoXing guildBossData)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.guildBossCfgData = guildBossData;
		if (guildBossData == null)
		{
			return;
		}
		this.bossNameText.set_text(string.Empty);
		Monster monster = DataReader<Monster>.Get(this.guildBossCfgData.bossId);
		if (monster != null)
		{
			this.callBossName = GameDataUtils.GetChineseContent(monster.name, false);
			this.bossNameText.set_text(this.callBossName);
		}
		ResourceManager.SetIconSprite(this.bossStepNumImg, "shuzi_jie_" + this.guildBossCfgData.rank);
		ResourceManager.SetSprite(this.bossIconImg, ResourceManager.GetIconSprite(this.guildBossCfgData.picture));
		this.GetCallCostItemAndNum();
		if (this.callCostItemID > 0)
		{
			ResourceManager.SetSprite(this.costIconImg, GameDataUtils.GetItemIcon(this.callCostItemID));
		}
		this.costNumText.set_text("x" + this.callCostNum);
	}

	private void GetCallCostItemAndNum()
	{
		this.callCostItemID = 0;
		this.callCostNum = 0;
		int num = GuildBossManager.Instance.GuildCallBossTotalTimesPerWeek - GuildBossManager.Instance.GuildBossActivityInfo.RemainCallBossTimes + 1;
		num = ((num >= 1) ? num : 1);
		string value = DataReader<GongHuiXinXi>.Get("Fund").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			string[] array2 = text.Split(new char[]
			{
				','
			});
			int num2 = int.Parse(array2[0]);
			if (num2 == num || (num2 < num && i == array2.Length - 1))
			{
				this.callCostItemID = int.Parse(array2[1]);
				this.callCostNum = int.Parse(array2[2]);
				break;
			}
		}
	}
}
