using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcOpenServerItem : BaseUIBehaviour
{
	private KaiFuPaiMing activityTaskCfg;

	private Text roleNameText;

	private Text titleText;

	private ButtonCustom btnGet;

	private ListPool itemsGrid;

	private Image notAttainImg;

	private Image hadGetRewardImg;

	private Image notGetRewardImg;

	private Image hadEndImg;

	private ButtonCustom roleNameBtn;

	private bool isInit;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.titleText = base.FindTransform("TitleText").GetComponent<Text>();
		this.btnGet = base.FindTransform("BtnGet").GetComponent<ButtonCustom>();
		this.itemsGrid = base.FindTransform("ItemsGrid").GetComponent<ListPool>();
		this.notAttainImg = base.FindTransform("NotAttainImg").GetComponent<Image>();
		this.roleNameText = base.FindTransform("RoleNameText").GetComponent<Text>();
		this.roleNameBtn = base.FindTransform("RoleNameBtn").GetComponent<ButtonCustom>();
		this.roleNameBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCheckRoleNameBtn);
		this.hadGetRewardImg = base.FindTransform("HadGetImg").GetComponent<Image>();
		this.notGetRewardImg = base.FindTransform("NotGetImg").GetComponent<Image>();
		this.hadEndImg = base.FindTransform("HadEndImg").GetComponent<Image>();
		this.btnGet.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetRewardBtn);
		this.itemsGrid.Clear();
		this.isInit = true;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.OnGetOpenServerActRewardRes, new Callback<int>(this.OnGetOpenServerActRewardRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdateUI(KaiFuPaiMing taskCfg)
	{
		this.activityTaskCfg = taskCfg;
		if (taskCfg == null)
		{
			return;
		}
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (this.activityTaskCfg.objective == 1)
		{
			this.titleText.set_text(string.Format(GameDataUtils.GetChineseContent(taskCfg.chinese, false), taskCfg.parameter));
		}
		else
		{
			string text = (taskCfg.ranking1 != taskCfg.ranking2) ? (taskCfg.ranking1 + "~" + taskCfg.ranking2) : (taskCfg.ranking1 + string.Empty);
			this.titleText.set_text(string.Format(GameDataUtils.GetChineseContent(taskCfg.chinese, false), text, taskCfg.parameter));
		}
		this.roleNameText.set_text(string.Empty);
		this.UpdateRewardItems();
		this.roleNameBtn.set_enabled(false);
		this.hadGetRewardImg.set_enabled(false);
		this.notGetRewardImg.set_enabled(false);
		this.hadEndImg.set_enabled(false);
		this.UpdateData(AcOpenServerManager.Instance.GetTargetTaskInfo(this.activityTaskCfg.Type, this.activityTaskCfg.taskId));
	}

	public void UpdateData(TargetTaskInfo taskInfo)
	{
		if (taskInfo == null)
		{
			return;
		}
		if (this.activityTaskCfg == null)
		{
			this.activityTaskCfg = DataReader<KaiFuPaiMing>.Get(taskInfo.targetID);
		}
		this.roleNameText.set_text(string.Empty);
		List<RankingRoleInfo> roleRankingInfoListByTargetID = AcOpenServerManager.Instance.GetRoleRankingInfoListByTargetID(taskInfo.targetID);
		if (roleRankingInfoListByTargetID != null && roleRankingInfoListByTargetID.get_Count() > 0 && this.activityTaskCfg.objective != 1)
		{
			this.roleNameBtn.set_enabled(true);
			if (roleRankingInfoListByTargetID.get_Count() > 1)
			{
				this.roleNameText.set_text("点击查看名单\n____________");
			}
			else if (roleRankingInfoListByTargetID.get_Count() == 1)
			{
				this.roleNameText.set_text(roleRankingInfoListByTargetID.get_Item(0).name);
				int num = Mathf.FloorToInt(this.roleNameText.get_preferredWidth()) / 10;
				num = ((num > 0) ? num : 1);
				string text = new string('_', num);
				this.roleNameText.set_text(roleRankingInfoListByTargetID.get_Item(0).name + "\n" + text);
			}
		}
		else if (this.activityTaskCfg.objective != 1 && roleRankingInfoListByTargetID != null && roleRankingInfoListByTargetID.get_Count() <= 0)
		{
			this.roleNameText.set_text("暂无");
		}
		if (taskInfo.status == TargetTaskInfo.GetRewardStatus.HadGet)
		{
			this.hadGetRewardImg.set_enabled(true);
			this.notAttainImg.set_enabled(false);
			this.notGetRewardImg.set_enabled(false);
			this.hadEndImg.set_enabled(false);
			this.btnGet.get_gameObject().SetActive(false);
		}
		else if (taskInfo.status == TargetTaskInfo.GetRewardStatus.Unavailable)
		{
			int num2 = 0;
			if (DataReader<KaiFuHuoDong>.Contains(this.activityTaskCfg.Type))
			{
				List<int> openDay = DataReader<KaiFuHuoDong>.Get(this.activityTaskCfg.Type).openDay;
				if (openDay != null && openDay.get_Count() > 0)
				{
					num2 = openDay.get_Item(openDay.get_Count() - 1);
				}
			}
			if (num2 < AcOpenServerManager.Instance.OpenServerDay)
			{
				this.btnGet.get_gameObject().SetActive(false);
				this.hadGetRewardImg.set_enabled(false);
				this.notGetRewardImg.set_enabled(false);
				this.notAttainImg.set_enabled(false);
				this.hadEndImg.set_enabled(true);
			}
			else
			{
				this.btnGet.get_gameObject().SetActive(false);
				this.hadGetRewardImg.set_enabled(false);
				this.notGetRewardImg.set_enabled(false);
				this.hadEndImg.set_enabled(false);
				this.notAttainImg.set_enabled(true);
			}
		}
		else if (taskInfo.status == TargetTaskInfo.GetRewardStatus.Available)
		{
			this.notAttainImg.set_enabled(false);
			this.hadGetRewardImg.set_enabled(false);
			this.notGetRewardImg.set_enabled(false);
			this.hadEndImg.set_enabled(false);
			this.btnGet.get_gameObject().SetActive(true);
		}
		else
		{
			this.btnGet.get_gameObject().SetActive(false);
			this.hadGetRewardImg.set_enabled(false);
			this.notAttainImg.set_enabled(false);
			this.hadEndImg.set_enabled(false);
			this.notGetRewardImg.set_enabled(true);
		}
	}

	private void UpdateRewardItems()
	{
		this.itemsGrid.Clear();
		if (this.activityTaskCfg != null)
		{
			List<int> itemIDs = new List<int>();
			List<int> itemCounts = new List<int>();
			string[] array = this.activityTaskCfg.rewardItem.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					','
				});
				itemIDs.Add((int)float.Parse(array2[0]));
				itemCounts.Add((int)float.Parse(array2[1]));
			}
			if (this.activityTaskCfg.labelItem != 0)
			{
				itemIDs.Add(this.activityTaskCfg.labelItem);
				itemCounts.Add(0);
			}
			this.itemsGrid.Create(itemIDs.get_Count(), delegate(int index)
			{
				if (index < itemIDs.get_Count() && index < itemCounts.get_Count() && index < this.itemsGrid.Items.get_Count())
				{
					int itemId = itemIDs.get_Item(index);
					int num = itemCounts.get_Item(index);
					this.itemsGrid.Items.get_Item(index).GetComponent<RewardItem>().SetRewardItem(itemId, (long)num, 0L);
				}
			});
		}
	}

	private void OnClickGetRewardBtn(GameObject go)
	{
		if (this.activityTaskCfg != null)
		{
			AcOpenServerManager.Instance.SendGetOpenServerActRewardReq(this.activityTaskCfg.taskId, this.activityTaskCfg.Type);
		}
	}

	private void OnGetOpenServerActRewardRes(int taskID)
	{
		if (this.activityTaskCfg != null && this.activityTaskCfg.taskId == taskID)
		{
			if (this.hadGetRewardImg != null)
			{
				this.hadGetRewardImg.set_enabled(true);
			}
			if (this.notAttainImg != null)
			{
				this.notAttainImg.set_enabled(false);
			}
			if (this.btnGet != null)
			{
				this.btnGet.get_gameObject().SetActive(false);
			}
		}
	}

	public void OnClickCheckRoleNameBtn(GameObject go)
	{
		List<RankingRoleInfo> roleRankingInfoListByTargetID = AcOpenServerManager.Instance.GetRoleRankingInfoListByTargetID(this.activityTaskCfg.taskId);
		if (roleRankingInfoListByTargetID == null || roleRankingInfoListByTargetID.get_Count() < 1)
		{
			return;
		}
		FindTipsUI findTipsUI = UIManagerControl.Instance.OpenUI("FindTipsUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as FindTipsUI;
		findTipsUI.get_transform().SetAsLastSibling();
		findTipsUI.OnSetRolesAndValue("上榜名单", AcOpenServerManager.Instance.GetRoleRankingNameContent(this.activityTaskCfg.taskId).get_Item(0), AcOpenServerManager.Instance.GetRoleRankingNameContent(this.activityTaskCfg.taskId).get_Item(1), AcOpenServerManager.Instance.GetRoleRankingNameContent(this.activityTaskCfg.taskId).get_Item(2));
	}
}
