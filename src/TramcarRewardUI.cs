using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TramcarRewardUI : BaseUIBehaviour
{
	private Text mTxLootTimes;

	private Text mTxLootFightTime;

	private Text mTxTramcarTips1;

	private Text mTxTramcarTips2;

	private Transform mRewardPanel;

	private KuangChePinZhi mData;

	private List<DiaoLuo> mFindReward = new List<DiaoLuo>();

	private TimeCountDown mLootTimeCountDown;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mTxLootTimes = base.FindTransform("txLootTimes").GetComponent<Text>();
		this.mTxLootFightTime = base.FindTransform("txLootFightTime").GetComponent<Text>();
		this.mTxTramcarTips1 = base.FindTransform("txTramcarTips1").GetComponent<Text>();
		this.mTxTramcarTips2 = base.FindTransform("txTramcarTips2").GetComponent<Text>();
		this.mRewardPanel = base.FindTransform("TramcarRewardGrid");
	}

	protected void OnEnable()
	{
		if (TramcarManager.Instance.CurLootQulity > 0)
		{
			this.mData = DataReader<KuangChePinZhi>.Get(TramcarManager.Instance.CurLootQulity);
			this.mTxLootFightTime.get_transform().get_parent().get_gameObject().SetActive(true);
			this.AddLootCountDown(TramcarManager.Instance.LootFightTime);
		}
		else
		{
			this.mData = DataReader<KuangChePinZhi>.Get(TramcarManager.Instance.CurQuality);
			this.mTxLootFightTime.get_transform().get_parent().get_gameObject().SetActive(false);
		}
		this.OnTramcarLootChangeNty(0);
	}

	protected override void OnDestroy()
	{
		this.RemoveLootCountDown();
		base.OnDestroy();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.TramcarLootChangeNty, new Callback<int>(this.OnTramcarLootChangeNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.TramcarLootChangeNty, new Callback<int>(this.OnTramcarLootChangeNty));
	}

	private void OnTramcarLootChangeNty(int times = 0)
	{
		if (TramcarManager.Instance.IsProtecting)
		{
			this.mTxLootTimes.set_text("护送成功奖励：");
			KuangCheDiTuPeiZhi kuangCheDiTuPeiZhi = DataReader<KuangCheDiTuPeiZhi>.Get(TramcarManager.Instance.CurMapId);
			if (kuangCheDiTuPeiZhi != null)
			{
				this.CreateOtherReward(kuangCheDiTuPeiZhi.frienddropID);
			}
			this.mTxTramcarTips1.get_gameObject().SetActive(false);
			this.mTxTramcarTips2.get_gameObject().SetActive(false);
		}
		else if (TramcarManager.Instance.CurLootQulity > 0)
		{
			this.mTxLootTimes.set_text("抢夺成功随机获得：");
			KuangCheDiTuPeiZhi kuangCheDiTuPeiZhi2 = DataReader<KuangCheDiTuPeiZhi>.Get(TramcarManager.Instance.CurMapId);
			if (kuangCheDiTuPeiZhi2 != null)
			{
				this.CreateOtherReward(kuangCheDiTuPeiZhi2.faightdropID);
			}
			this.mTxTramcarTips1.get_gameObject().SetActive(false);
			this.mTxTramcarTips2.get_gameObject().SetActive(false);
		}
		else
		{
			this.mTxLootTimes.set_text(string.Format("被抢夺次数：<color=#5ce228>{0}/{1}</color>", times, TramcarManager.Instance.BeLootLimit));
			if (TramcarManager.Instance.TramcarRewards != null)
			{
				this.CreateSelfReward(TramcarManager.Instance.TramcarRewards, (float)(100 - TramcarManager.Instance.LootDeduct * times) * 0.01f);
			}
			this.mTxTramcarTips1.get_gameObject().SetActive(true);
			this.mTxTramcarTips2.get_gameObject().SetActive(true);
		}
	}

	private void CreateSelfReward(List<DropItem> rewards, float pct)
	{
		for (int i = 0; i < this.mRewardPanel.get_childCount(); i++)
		{
			Object.Destroy(this.mRewardPanel.GetChild(i).get_gameObject());
		}
		long num = (long)((float)this.mData.parament * 0.01f * pct * 1000f);
		for (int j = 0; j < rewards.get_Count(); j++)
		{
			GameObject gameObject = ItemShow.ShowItem(this.mRewardPanel, rewards.get_Item(j).typeId, rewards.get_Item(j).count * num / 1000L, false, null, 2001);
			gameObject.get_transform().set_localScale(new Vector3(0.55f, 0.55f, 1f));
		}
	}

	private void CreateOtherReward(List<int> dropIds)
	{
		this.mFindReward.Clear();
		List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
		for (int i = 0; i < dropIds.get_Count(); i++)
		{
			this.FindDropReward(dataList, dropIds.get_Item(i));
		}
		for (int j = 0; j < this.mRewardPanel.get_childCount(); j++)
		{
			Object.Destroy(this.mRewardPanel.GetChild(j).get_gameObject());
		}
		for (int k = 0; k < this.mFindReward.get_Count(); k++)
		{
			GameObject gameObject = ItemShow.ShowItem(this.mRewardPanel, this.mFindReward.get_Item(k).goodsId, this.mFindReward.get_Item(k).minNum * (long)this.mData.parament / 100L, false, null, 2001);
			gameObject.get_transform().set_localScale(new Vector3(0.55f, 0.55f, 1f));
		}
	}

	private void FindDropReward(List<DiaoLuo> diaoluos, int dropId)
	{
		int lv = EntityWorld.Instance.EntSelf.Lv;
		for (int i = 0; i < diaoluos.get_Count(); i++)
		{
			DiaoLuo diaoLuo = diaoluos.get_Item(i);
			if (diaoLuo.ruleId == dropId)
			{
				if (diaoLuo.minLv == diaoLuo.maxLv && diaoLuo.minLv == 0)
				{
					this.mFindReward.Add(diaoLuo);
				}
				else if (diaoLuo.minLv == diaoLuo.maxLv && lv == diaoLuo.minLv)
				{
					this.mFindReward.Add(diaoLuo);
				}
				else if (diaoLuo.minLv < diaoLuo.maxLv && lv >= diaoLuo.minLv && lv < diaoLuo.maxLv)
				{
					this.mFindReward.Add(diaoLuo);
				}
			}
		}
	}

	private void AddLootCountDown(int remianTime)
	{
		this.RemoveLootCountDown();
		if (remianTime <= 0)
		{
			this.mTxLootFightTime.set_text("00:00");
			return;
		}
		this.mLootTimeCountDown = new TimeCountDown(remianTime, TimeFormat.SECOND, delegate
		{
			this.mTxLootFightTime.set_text(TimeConverter.GetTime(this.mLootTimeCountDown.GetSeconds(), TimeFormat.MMSS));
		}, null, true);
	}

	public void RemoveLootCountDown()
	{
		if (this.mLootTimeCountDown != null)
		{
			this.mLootTimeCountDown.Dispose();
			this.mLootTimeCountDown = null;
		}
	}
}
