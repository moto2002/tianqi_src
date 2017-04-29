using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildConstructUI : UIBase
{
	private Text guildContributionText;

	private Text guildAcceptBtnText;

	private Text guildDonateTimesText;

	private Text guildTaskTimesText;

	private int costDiamondNum;

	private List<int> rewardNumList;

	private List<int> taskRewardNumList;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isClick = true;
		this.isMask = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		this.costDiamondNum = (int)float.Parse(DataReader<GongHuiXinXi>.Get("Donate").value);
		this.rewardNumList = new List<int>();
		string value = DataReader<GongHuiXinXi>.Get("DonateReward").value;
		string[] array = value.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			int num = (int)float.Parse(text);
			this.rewardNumList.Add(num);
		}
		this.taskRewardNumList = new List<int>();
		string value2 = DataReader<GongHuiXinXi>.Get("TaskReward").value;
		string[] array2 = value2.Split(new char[]
		{
			','
		});
		for (int j = 0; j < array2.Length; j++)
		{
			string text2 = array2[j];
			int num2 = (int)float.Parse(text2);
			this.taskRewardNumList.Add(num2);
		}
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.guildContributionText = base.FindTransform("guildAttribution").GetComponent<Text>();
		this.guildDonateTimesText = base.FindTransform("remainDonateTimesText").GetComponent<Text>();
		this.guildTaskTimesText = base.FindTransform("remainTaskTimesText").GetComponent<Text>();
		base.FindTransform("BtnContribution").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickContribution);
		base.FindTransform("BtnAccept").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAccept);
		base.FindTransform("CostNum").GetComponent<Text>().set_text("x" + this.costDiamondNum);
		this.guildAcceptBtnText = base.FindTransform("BtnAcceptName").GetComponent<Text>();
		base.FindTransform("GuildTaskDesc").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515023, false));
		this.SetRewardNumList();
		this.SetTextName();
	}

	private void SetTextName()
	{
		base.FindTransform("GuildContribution").FindChild("Title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515041, false));
		base.FindTransform("GuildTask").FindChild("Title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515042, false));
		base.FindTransform("remainDonateTimesName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515107, false));
		base.FindTransform("remainTaskTimesName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515107, false));
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGuildBuildRes, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.OnDissolveGuildRes, new Callback(this.OnDissolveGuildRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGuildBuildRes, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.OnDissolveGuildRes, new Callback(this.OnDissolveGuildRes));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void OnEnable()
	{
		this.RefreshUI();
	}

	private void SetRewardNumList()
	{
		for (int i = 0; i < 2; i++)
		{
			string text = string.Empty;
			if (i == 0)
			{
				text = GameDataUtils.GetChineseContent(515053, false);
			}
			else if (i == 1)
			{
				text = GameDataUtils.GetChineseContent(515029, false);
			}
			base.FindTransform("GuildContribution").FindChild("RewardRegion").FindChild("rewardItem" + (i + 1)).FindChild("rewardName").GetComponent<Text>().set_text(text + "X" + this.rewardNumList.get_Item(i));
			base.FindTransform("GuildTask").FindChild("RewardRegion").FindChild("rewardItem" + (i + 1)).FindChild("rewardName").GetComponent<Text>().set_text(text + "X" + this.taskRewardNumList.get_Item(i));
		}
	}

	private void RefreshUI()
	{
		if (GuildManager.Instance.MyGuildnfo == null)
		{
			return;
		}
		int num = GuildManager.Instance.GuildTaskTotalTimes - GuildManager.Instance.MyGuildnfo.taskedCount;
		if (num <= 0)
		{
			num = 0;
		}
		int num2 = GuildManager.Instance.GuildDonateTotalTimes - GuildManager.Instance.MyGuildnfo.builtedCount;
		if (num2 <= 0)
		{
			num2 = 0;
		}
		this.guildTaskTimesText.set_text(num + string.Empty);
		this.guildDonateTimesText.set_text(num2 + string.Empty);
		if (GuildManager.Instance.MyMemberInfo != null)
		{
			this.guildContributionText.set_text(GuildManager.Instance.MyMemberInfo.contribution.ToString());
		}
		if (MainTaskManager.Instance.GuildTaskTimes > 0 && MainTaskManager.Instance.GuildTaskId > 0)
		{
			this.guildAcceptBtnText.set_text("执行中");
		}
		else if (MainTaskManager.Instance.GuildTaskTimes <= 0 && MainTaskManager.Instance.GuildTaskId == 0)
		{
			this.guildAcceptBtnText.set_text("接 受");
		}
	}

	private void OnClickContribution(GameObject go)
	{
		if (GuildManager.Instance.MyGuildnfo == null)
		{
			return;
		}
		int num = GuildManager.Instance.GuildDonateTotalTimes - GuildManager.Instance.MyGuildnfo.builtedCount;
		if (num <= 0)
		{
			string tipContentByKey = GuildManager.Instance.GetTipContentByKey("BuildWord");
			UIManagerControl.Instance.ShowToastText(tipContentByKey);
			return;
		}
		string content = string.Format(GuildManager.Instance.GetTipContentByKey("UiWord"), this.costDiamondNum);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(515041, false), content, null, delegate
		{
			GuildManager.Instance.SendGuildBuildReq(GuildBuildType.GBT.GUILD_DONATE);
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickAccept(GameObject go)
	{
		if (MainTaskManager.Instance.GuildTaskTimes > 0 || MainTaskManager.Instance.GuildTaskId > 0)
		{
			string tipContentByKey = GuildManager.Instance.GetTipContentByKey("ExecuteWord");
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), tipContentByKey, null, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
			return;
		}
		if (GuildManager.Instance.MyGuildnfo == null)
		{
			return;
		}
		int num = GuildManager.Instance.GuildTaskTotalTimes - GuildManager.Instance.MyGuildnfo.taskedCount;
		if (num <= 0)
		{
			string tipContentByKey2 = GuildManager.Instance.GetTipContentByKey("BuildWord");
			UIManagerControl.Instance.ShowToastText(tipContentByKey2);
			return;
		}
		GuildManager.Instance.SendGuildBuildReq(GuildBuildType.GBT.GUILD_TASK);
	}

	private void OnDissolveGuildRes()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}
}
