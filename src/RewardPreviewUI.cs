using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPreviewUI : BaseUIBehaviour
{
	public enum CopyType
	{
		NONE,
		EXP,
		GUARD,
		ESCORT,
		ATTACK,
		HOOK,
		GUILDWAR
	}

	private Text mTxTitle;

	private Text mTxExp;

	private Text mTxExpBatch;

	private Text mTxExpBatchNum;

	private Transform mSpecialRewards;

	private RewardPreviewUI.CopyType mCopyType;

	public Text TxExp
	{
		get
		{
			return this.mTxExp;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mTxTitle = base.FindTransform("txTitle").GetComponent<Text>();
		this.mTxExp = base.FindTransform("ExpText").GetComponent<Text>();
		this.mTxExpBatch = base.FindTransform("ExpBatchText").GetComponent<Text>();
		this.mTxExpBatchNum = base.FindTransform("ExpBatchNum").GetComponent<Text>();
		this.mSpecialRewards = base.FindTransform("SpecialRewards");
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(InstanceManagerEvent.BatchChanged, new Callback<int>(this.OnBatchChanged));
		EventDispatcher.AddListener<int, EntityMonster, string>("BattleDialogTrigger", new Callback<int, EntityMonster, string>(this.OnDialogTrigger));
		EventDispatcher.AddListener<int>(EventNames.DefendFightCarReachTipsNty, new Callback<int>(this.OnRefreshCarWave));
		EventDispatcher.AddListener<int>(EventNames.DefendFightMonsterTipsNty, new Callback<int>(this.OnRefreshMonsterWave));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(InstanceManagerEvent.BatchChanged, new Callback<int>(this.OnBatchChanged));
		EventDispatcher.RemoveListener<int, EntityMonster, string>("BattleDialogTrigger", new Callback<int, EntityMonster, string>(this.OnDialogTrigger));
		EventDispatcher.RemoveListener<int>(EventNames.DefendFightCarReachTipsNty, new Callback<int>(this.OnRefreshCarWave));
		EventDispatcher.RemoveListener<int>(EventNames.DefendFightMonsterTipsNty, new Callback<int>(this.OnRefreshMonsterWave));
	}

	private void OnBatchChanged(int curBatch)
	{
		if (this.mCopyType != RewardPreviewUI.CopyType.EXP)
		{
			return;
		}
		if (curBatch <= 1)
		{
			return;
		}
		int num = curBatch - 1;
		long batchExp = SpecialFightManager.Instance.GetBatchExp(num);
		this.mTxExp.set_text(AttrUtility.GetExpValueStr(batchExp * (long)num));
		this.mTxExpBatchNum.set_text(AttrUtility.GetExpValueStr(batchExp));
	}

	private void OnDialogTrigger(int type, EntityMonster entityMonster, string arg)
	{
		if (this.mCopyType != RewardPreviewUI.CopyType.GUARD)
		{
			return;
		}
		if (type == 10 && InstanceManager.CurrentInstanceData != null && InstanceManager.CurrentInstanceData.waveShow > 0)
		{
			int key = int.Parse(arg);
			FShouHuShuiJingFuBenBoCi fShouHuShuiJingFuBenBoCi = DataReader<FShouHuShuiJingFuBenBoCi>.Get(key);
			if (fShouHuShuiJingFuBenBoCi != null)
			{
				this.CreateReward(fShouHuShuiJingFuBenBoCi.rewardId);
			}
		}
	}

	private void OnRefreshCarWave(int wave)
	{
		if (this.mCopyType != RewardPreviewUI.CopyType.ESCORT)
		{
			return;
		}
		FHuSongKuangCheJiangLi fHuSongKuangCheJiangLi = DataReader<FHuSongKuangCheJiangLi>.Get(wave);
		if (fHuSongKuangCheJiangLi != null)
		{
			this.CreateReward(fHuSongKuangCheJiangLi.rewardId);
		}
	}

	private void OnRefreshMonsterWave(int wave)
	{
		if (this.mCopyType != RewardPreviewUI.CopyType.ATTACK)
		{
			return;
		}
		FXueCaiDianFengFuBenBoCi fXueCaiDianFengFuBenBoCi = DataReader<FXueCaiDianFengFuBenBoCi>.Get(wave);
		if (fXueCaiDianFengFuBenBoCi != null)
		{
			this.CreateReward(fXueCaiDianFengFuBenBoCi.rewardId);
		}
	}

	private void CreateReward(int rewardId)
	{
		for (int i = 0; i < this.mSpecialRewards.get_childCount(); i++)
		{
			Object.Destroy(this.mSpecialRewards.GetChild(i).get_gameObject());
		}
		List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
		for (int j = 0; j < dataList.get_Count(); j++)
		{
			DiaoLuo diaoLuo = dataList.get_Item(j);
			if (diaoLuo.ruleId == rewardId && diaoLuo.dropType == 1)
			{
				GameObject gameObject = ItemShow.ShowItem(this.mSpecialRewards, diaoLuo.goodsId, diaoLuo.minNum, false, null, 2001);
				gameObject.get_transform().set_localScale(new Vector3(0.5f, 0.5f, 1f));
			}
		}
	}

	private void RefreshReward(RewardPreviewUI.CopyType type)
	{
		SpecialFightMode mode = SpecialFightMode.None;
		switch (type)
		{
		case RewardPreviewUI.CopyType.GUARD:
			mode = SpecialFightMode.Hold;
			break;
		case RewardPreviewUI.CopyType.ESCORT:
			mode = SpecialFightMode.Protect;
			break;
		case RewardPreviewUI.CopyType.ATTACK:
			mode = SpecialFightMode.Save;
			break;
		}
		SpecialFightCommonTableData specialFightCommonTableData = SpecialFightManager.GetSpecialFightCommonTableData(mode);
		if (specialFightCommonTableData != null)
		{
			for (int i = 0; i < this.mSpecialRewards.get_childCount(); i++)
			{
				Object.Destroy(this.mSpecialRewards.GetChild(i).get_gameObject());
			}
			for (int j = 0; j < specialFightCommonTableData.itemIDs.get_Count(); j++)
			{
				GameObject gameObject = ItemShow.ShowItem(this.mSpecialRewards, specialFightCommonTableData.itemIDs.get_Item(j), 0L, false, null, 2001);
				gameObject.get_transform().set_localScale(new Vector3(0.5f, 0.5f, 1f));
			}
			this.mSpecialRewards.get_gameObject().SetActive(true);
		}
	}

	private void SetDafaultUI()
	{
		if (this.mTxExp != null)
		{
			this.mTxExp.set_fontSize(20);
			this.mTxExp.get_gameObject().SetActive(false);
		}
		this.mSpecialRewards.get_gameObject().SetActive(false);
		this.SetIconImage("1000011");
	}

	public void SetShowType(RewardPreviewUI.CopyType type)
	{
		this.mCopyType = type;
		this.SetDafaultUI();
		switch (type)
		{
		case RewardPreviewUI.CopyType.EXP:
		case RewardPreviewUI.CopyType.HOOK:
			this.mTxTitle.set_text(GameDataUtils.GetChineseContent(511608, false));
			this.mTxExpBatch.set_text(GameDataUtils.GetChineseContent(511609, false));
			this.mTxExp.get_gameObject().SetActive(true);
			break;
		case RewardPreviewUI.CopyType.GUARD:
		case RewardPreviewUI.CopyType.ESCORT:
		case RewardPreviewUI.CopyType.ATTACK:
			this.mTxTitle.set_text("已获得奖励");
			this.RefreshReward(type);
			break;
		case RewardPreviewUI.CopyType.GUILDWAR:
		{
			this.mTxExp.set_fontSize(32);
			this.mTxTitle.set_text("已采集资源");
			this.mTxExpBatch.set_text(string.Empty);
			this.mTxExp.get_gameObject().SetActive(true);
			this.mTxExpBatchNum.set_text(string.Empty);
			Icon icon = DataReader<Icon>.Get(5611);
			if (icon != null)
			{
				this.SetIconImage(icon.icon);
			}
			break;
		}
		}
	}

	public void SetIconImage(string iconName)
	{
		Image component = base.FindTransform("ExpIcon").GetComponent<Image>();
		if (component != null)
		{
			ResourceManager.SetIconSprite(component, iconName);
		}
	}

	public void SetDefaultExp(long exp, long batchExp)
	{
		this.SetDefaultExpText(exp);
		this.SetDefaultBatchExpText(batchExp);
	}

	public void SetDefaultExpText(long exp)
	{
		if (this.mTxExp != null)
		{
			this.mTxExp.set_text(AttrUtility.GetExpValueStr(exp));
		}
	}

	public void SetDefaultBatchExpText(long batchNum)
	{
		if (this.mTxExpBatchNum != null)
		{
			this.mTxExpBatchNum.set_text(AttrUtility.GetExpValueStr(batchNum));
		}
	}
}
