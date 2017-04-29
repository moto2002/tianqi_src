using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailDaily : MonoBehaviour
{
	public Image ImageQuality;

	public GameObject Tips;

	public Text TextTips;

	public ButtonCustom ButtonRefresh;

	public Image StarProgress;

	public Text StarText;

	public GameObject StarBoxCanGet;

	public GameObject StarBoxHasGot;

	public Text StarBoxCountdown;

	public Transform FxTransform;

	private int quality;

	private int FxUid;

	private bool isTipsRewardBosx;

	private void Start()
	{
		this.ButtonRefresh.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefresh);
		this.StarBoxCanGet.get_transform().Find("ButtonChest").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGeStartReward);
		this.StarBoxHasGot.get_transform().Find("Image").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGeStartReward);
	}

	public void UpdateUI()
	{
		XuanShangRenWuPeiZhi xuanShangRenWuPeiZhi = DataReader<XuanShangRenWuPeiZhi>.Get(BountyManager.Instance.Info.taskId);
		ResourceManager.SetSprite(this.ImageQuality, ResourceManager.GetIconSprite("lcxs_quality_" + xuanShangRenWuPeiZhi.quality));
		this.quality = xuanShangRenWuPeiZhi.quality;
		int star = DataReader<ShengLiBaoXiang>.Get(BountyManager.Instance.rewardBoxId).star;
		this.StarText.set_text(BountyManager.Instance.Info.hasStar + "/" + star);
		this.StarProgress.set_fillAmount(1f * (float)BountyManager.Instance.Info.hasStar / (float)star);
		this.RefreshStarBoxDetail(BountyManager.Instance.Info.hasStar);
		this.OnSecondPass();
	}

	private void RefreshStarBoxDetail(int starNum)
	{
		FXSpineManager.Instance.DeleteSpine(this.FxUid, true);
		if (BountyManager.Instance.HasGotRewardDaily)
		{
			this.StarBoxHasGot.SetActive(true);
			this.StarBoxCanGet.SetActive(false);
			ImageColorMgr.SetImageColor(this.StarBoxHasGot.get_transform().Find("Image").GetComponent<Image>(), true);
			this.isTipsRewardBosx = false;
		}
		else
		{
			this.StarBoxHasGot.SetActive(false);
			this.StarBoxCanGet.SetActive(true);
			if (DataReader<ShengLiBaoXiang>.Get(BountyManager.Instance.rewardBoxId).star <= starNum)
			{
				ImageColorMgr.SetImageColor(this.StarBoxCanGet.get_transform().Find("ButtonChest").GetComponent<Image>(), false);
				this.FxUid = FXSpineManager.Instance.ReplaySpine(0, 1705, this.FxTransform, "BountyUI", 2011, null, "UI", -40f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.isTipsRewardBosx = false;
			}
			else
			{
				ImageColorMgr.SetImageColor(this.StarBoxCanGet.get_transform().Find("ButtonChest").GetComponent<Image>(), true);
				this.isTipsRewardBosx = true;
			}
		}
	}

	public void OnClickGeStartReward(GameObject go)
	{
		Debug.LogError(BountyManager.Instance.GettingReward);
		if (this.isTipsRewardBosx)
		{
			List<int> goods = new List<int>();
			List<long> goodNums = new List<long>();
			ShengLiBaoXiang shengLiBaoXiang = DataReader<ShengLiBaoXiang>.Get(BountyManager.Instance.rewardBoxId);
			int markIndex = BountyManager.Instance.GetMarkIndex();
			if (markIndex == 0)
			{
				goods = shengLiBaoXiang.item1;
				goodNums = shengLiBaoXiang.num1;
			}
			else if (markIndex == 1)
			{
				goods = shengLiBaoXiang.item2;
				goodNums = shengLiBaoXiang.num2;
			}
			else if (markIndex == 2)
			{
				goods = shengLiBaoXiang.item3;
				goodNums = shengLiBaoXiang.num3;
			}
			else if (markIndex == 3)
			{
				goods = shengLiBaoXiang.item4;
				goodNums = shengLiBaoXiang.num4;
			}
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
			rewardUI.SetRewardItem("奖励预览", goods, goodNums, true, false, null, null);
			if (shengLiBaoXiang.word > 0)
			{
				rewardUI.SetTipsText(GameDataUtils.GetChineseContent(shengLiBaoXiang.word, false));
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
					taskType = (!BountyManager.Instance.isSelectDaily) ? BountyTaskType.ENUM.Urgent : BountyTaskType.ENUM.Normal
				}, ServerType.Data);
			}
		}
	}

	public void OnSecondPass()
	{
		TimeSpan timeSpan = BountyManager.Instance.Countdown - TimeManager.Instance.PreciseServerTime;
		if (this.quality == 5)
		{
			this.TextTips.set_text(GameDataUtils.GetChineseContent(513652, false));
			this.ButtonRefresh.get_gameObject().SetActive(false);
			this.Tips.SetActive(false);
		}
		else if (timeSpan.get_TotalSeconds() > 0.0)
		{
			if (timeSpan.get_Days() > 0)
			{
				string text = timeSpan.get_Days() * 24 + timeSpan.get_Hours() + ":" + timeSpan.get_Minutes();
				this.TextTips.set_text(string.Format(GameDataUtils.GetChineseContent(513606, false), text));
			}
			else
			{
				this.TextTips.set_text(string.Format(GameDataUtils.GetChineseContent(513606, false), Convert.ToDateTime(timeSpan.ToString()).ToString("HH:mm")));
			}
			this.TextTips.get_gameObject().SetActive(true);
			this.Tips.SetActive(false);
			this.ButtonRefresh.get_gameObject().SetActive(true);
		}
		else
		{
			this.TextTips.set_text(GameDataUtils.GetChineseContent(513651, false));
			this.ButtonRefresh.get_gameObject().SetActive(true);
			this.Tips.SetActive(true);
		}
		if (this.StarBoxHasGot.get_activeSelf() && BountyManager.Instance.BoxStarCountdown > TimeManager.Instance.PreciseServerTime)
		{
			this.StarBoxCountdown.set_text(string.Format(GameDataUtils.GetChineseContent(513647, false), Convert.ToDateTime((BountyManager.Instance.BoxStarCountdown - TimeManager.Instance.PreciseServerTime).ToString()).ToString("HH:mm:ss")));
		}
	}

	public void OnClickRefresh(GameObject go)
	{
		if (BountyManager.Instance.Countdown < TimeManager.Instance.PreciseServerTime)
		{
			NetworkManager.Send(new BountyTaskRefreshReq
			{
				taskId = BountyManager.Instance.Info.taskId
			}, ServerType.Data);
		}
		else
		{
			UIManagerControl.Instance.OpenUI("DialogBoxUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			string text = "A";
			switch (this.quality + 1)
			{
			case 1:
				text = "D";
				break;
			case 2:
				text = "C";
				break;
			case 3:
				text = "B";
				break;
			case 4:
				text = "A";
				break;
			case 5:
				text = "S";
				break;
			}
			XuanShangRenWuPeiZhi xuanShangRenWuPeiZhi = DataReader<XuanShangRenWuPeiZhi>.Get(BountyManager.Instance.Info.taskId);
			string content = string.Format(GameDataUtils.GetChineseContent(513627, false), xuanShangRenWuPeiZhi.upgradeCost, text);
			string chineseContent = GameDataUtils.GetChineseContent(513628, false);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(chineseContent, content, delegate
			{
			}, delegate
			{
			}, delegate
			{
				NetworkManager.Send(new BountyTaskRefreshReq
				{
					taskId = BountyManager.Instance.Info.taskId
				}, ServerType.Data);
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		}
	}
}
