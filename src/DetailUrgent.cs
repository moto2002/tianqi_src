using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailUrgent : MonoBehaviour
{
	public Image ImageQuality;

	public Text TextTips;

	public Transform[] ButtonBoxes;

	public Image StarProgress;

	public Text[] StarNums;

	public Transform[] FxTransform;

	private List<int> FxUidList = new List<int>();

	private int quality;

	private void Start()
	{
		this.ButtonBoxes[0].GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetStartReward);
		this.ButtonBoxes[1].GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetStartReward);
		this.ButtonBoxes[2].GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetStartReward);
	}

	public void UpdateUI()
	{
		XuanShangRenWuPeiZhi xuanShangRenWuPeiZhi = DataReader<XuanShangRenWuPeiZhi>.Get(BountyManager.Instance.Info.urgentTaskId);
		ResourceManager.SetSprite(this.ImageQuality, ResourceManager.GetIconSprite("lcxs_quality_" + xuanShangRenWuPeiZhi.quality));
		this.quality = xuanShangRenWuPeiZhi.quality;
		int star = DataReader<JinJiShengLiBaoXiang>.Get(3).star;
		this.StarProgress.set_fillAmount(1f * (float)BountyManager.Instance.Info.hasStarUrgent / (float)star);
		using (List<int>.Enumerator enumerator = this.FxUidList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				FXSpineManager.Instance.DeleteSpine(current, true);
			}
		}
		this.FxUidList.Clear();
		for (int i = 0; i < BountyManager.Instance.HasGotRewardUrgent.Length; i++)
		{
			ImageColorMgr.SetImageColor(this.ButtonBoxes[i].GetComponent<Image>(), false);
			JinJiShengLiBaoXiang jinJiShengLiBaoXiang = DataReader<JinJiShengLiBaoXiang>.Get(i + 1);
			if (BountyManager.Instance.Info.hasStarUrgent < jinJiShengLiBaoXiang.star)
			{
				ResourceManager.SetSprite(this.ButtonBoxes[i].GetComponent<Image>(), ResourceManager.GetIconSprite("dailytask_icon_bag7"));
				ImageColorMgr.SetImageColor(this.ButtonBoxes[i].GetComponent<Image>(), true);
			}
			else if (BountyManager.Instance.HasGotRewardUrgent[i])
			{
				ResourceManager.SetSprite(this.ButtonBoxes[i].GetComponent<Image>(), ResourceManager.GetIconSprite("dailytask_icon_bag8"));
			}
			else
			{
				this.FxUidList.Add(FXSpineManager.Instance.ReplaySpine(0, 1705, this.FxTransform[i], "BountyUI", 2011, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
				ResourceManager.SetSprite(this.ButtonBoxes[i].GetComponent<Image>(), ResourceManager.GetIconSprite("dailytask_icon_bag7"));
			}
			this.StarNums[i].set_text("x" + jinJiShengLiBaoXiang.star);
			this.StarNums[i].get_transform().Find("Text").GetComponent<Text>().set_text("x" + jinJiShengLiBaoXiang.star);
			this.StarNums[i].get_transform().Find("Text").get_gameObject().SetActive(BountyManager.Instance.Info.hasStarUrgent >= jinJiShengLiBaoXiang.star);
			this.StarNums[i].set_enabled(BountyManager.Instance.Info.hasStarUrgent < jinJiShengLiBaoXiang.star);
		}
		this.StarNums[3].set_text("x" + BountyManager.Instance.Info.hasStarUrgent);
		this.OnSecondPass();
	}

	public void OnSecondPass()
	{
		bool flag = false;
		for (int i = 1; i < DataReader<JinJiKaiFangShiJian>.DataList.get_Count() + 1; i++)
		{
			JinJiKaiFangShiJian jinJiKaiFangShiJian = DataReader<JinJiKaiFangShiJian>.Get(i);
			DateTime dateTime = DateTime.Parse(TimeManager.Instance.PreciseServerTime.ToString("yyyy-MM-dd ") + jinJiKaiFangShiJian.openTime);
			DateTime dateTime2 = dateTime.AddSeconds((double)jinJiKaiFangShiJian.time);
			if (TimeManager.Instance.PreciseServerTime < dateTime)
			{
				this.TextTips.set_text(string.Format(GameDataUtils.GetChineseContent(513611, false), dateTime.ToString("HH:mm"), string.Empty));
				flag = true;
				break;
			}
			if (TimeManager.Instance.PreciseServerTime < dateTime2)
			{
				this.TextTips.set_text(string.Format(GameDataUtils.GetChineseContent(513612, false), dateTime2.ToString("HH:mm"), string.Empty));
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			DateTime dateTime3 = DateTime.Parse(TimeManager.Instance.PreciseServerTime.AddDays(1.0).ToString("yyyy-MM-dd ") + DataReader<JinJiKaiFangShiJian>.Get(1).openTime);
			this.TextTips.set_text(string.Format(GameDataUtils.GetChineseContent(513611, false), dateTime3.get_Hour(), (dateTime3.get_Minute() != 0) ? dateTime3.get_Minute().ToString() : "00"));
		}
	}

	public void OnClickGetStartReward(GameObject go)
	{
		Debug.LogError(BountyManager.Instance.GettingReward);
		int num = 0;
		if (go.get_name().Equals("ButtonBox1"))
		{
			num = 1;
		}
		else if (go.get_name().Equals("ButtonBox2"))
		{
			num = 2;
		}
		else if (go.get_name().Equals("ButtonBox3"))
		{
			num = 3;
		}
		if (BountyManager.Instance.Info.hasStarUrgent < DataReader<JinJiShengLiBaoXiang>.Get(num).star || BountyManager.Instance.HasGotRewardUrgent[num - 1])
		{
			List<int> goods = new List<int>();
			List<long> goodNums = new List<long>();
			int markIndex = BountyManager.Instance.GetMarkIndex();
			JinJiShengLiBaoXiang jinJiShengLiBaoXiang = DataReader<JinJiShengLiBaoXiang>.Get(num);
			if (markIndex == 0)
			{
				goods = jinJiShengLiBaoXiang.item1;
				goodNums = jinJiShengLiBaoXiang.num1;
			}
			else if (markIndex == 1)
			{
				goods = jinJiShengLiBaoXiang.item2;
				goodNums = jinJiShengLiBaoXiang.num2;
			}
			else if (markIndex == 2)
			{
				goods = jinJiShengLiBaoXiang.item3;
				goodNums = jinJiShengLiBaoXiang.num3;
			}
			else if (markIndex == 3)
			{
				goods = jinJiShengLiBaoXiang.item4;
				goodNums = jinJiShengLiBaoXiang.num4;
			}
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
			rewardUI.SetRewardItem("奖励预览", goods, goodNums, true, false, null, null);
			if (jinJiShengLiBaoXiang.word > 0)
			{
				rewardUI.SetTipsText(GameDataUtils.GetChineseContent(jinJiShengLiBaoXiang.word, false));
			}
		}
		else if (!BackpackManager.Instance.ShowBackpackFull() && !BountyManager.Instance.GettingReward)
		{
			if (BountyManager.Instance.HasGotRewardDaily)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513630, false), 1f, 2f);
			}
			else
			{
				BountyManager.Instance.GettingReward = true;
				NetworkManager.Send(new BountyGetStarBoxReq
				{
					taskType = (!BountyManager.Instance.isSelectDaily) ? BountyTaskType.ENUM.Urgent : BountyTaskType.ENUM.Normal,
					boxTypeId = num
				}, ServerType.Data);
			}
		}
	}
}
