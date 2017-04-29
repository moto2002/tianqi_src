using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGiftUI : UIBase
{
	private GameObject mListGrid;

	private Dictionary<int, UpdateGiftItem> mItems;

	private List<int> reqIds;

	private bool mIsCheck;

	private GengXinYouLi mLastReward;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mItems = new Dictionary<int, UpdateGiftItem>();
		this.mListGrid = UIHelper.GetObject(base.get_transform(), "Content/Grid");
		this.CreateItemList(DataReader<GengXinYouLi>.DataList);
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		base.get_transform().SetAsFirstSibling();
		this.RefreshUI();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void RefreshUI()
	{
		this.RefreshList();
	}

	public UpdateGiftItem GetItemById(int acId)
	{
		using (Dictionary<int, UpdateGiftItem>.Enumerator enumerator = this.mItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, UpdateGiftItem> current = enumerator.get_Current();
				if (current.get_Key() == acId)
				{
					return current.get_Value();
				}
			}
		}
		return null;
	}

	public void RefreshProgress(int acId, float curSize, float allSize)
	{
		UpdateGiftItem itemById = this.GetItemById(acId);
		if (itemById != null)
		{
			itemById.SetProgressAndSpeed(curSize, allSize);
		}
	}

	private void RefreshList()
	{
		bool flag = true;
		int currentVersion = OperateActivityManager.Instance.CurrentVersion;
		using (List<UpdateAcInfo>.Enumerator enumerator = OperateActivityManager.Instance.LocalUpdateGiftInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				UpdateAcInfo current = enumerator.get_Current();
				if (this.mItems.ContainsKey(current.acId))
				{
					if ((current.status == UpdateAcInfo.AcStep.STEP.Finish || current.status == UpdateAcInfo.AcStep.STEP.Close) && this.mItems.get_Item(current.acId).Data.FinishPar > currentVersion && !this.mIsCheck)
					{
						current.status = UpdateAcInfo.AcStep.STEP.Ready;
					}
					this.mItems.get_Item(current.acId).Status = current.status;
					this.mItems.get_Item(current.acId).get_gameObject().SetActive(true);
					this.mItems.get_Item(current.acId).BtnStart.set_interactable(flag);
					if (flag)
					{
						flag = (current.status != UpdateAcInfo.AcStep.STEP.Start);
					}
				}
			}
		}
		this.mIsCheck = true;
	}

	private void CreateItemList(List<GengXinYouLi> list)
	{
		if (list == null)
		{
			Debug.Log("<color=red>Error:</color>[更新有礼]说好的配置表呢???");
			return;
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("UpdateGiftItem");
			UGUITools.SetParent(this.mListGrid, instantiate2Prefab, false);
			UpdateGiftItem component = instantiate2Prefab.GetComponent<UpdateGiftItem>();
			component.UpdateItem(list.get_Item(i));
			component.EventHandler = new Action<string, UpdateGiftItem>(this.OnClickButton);
			component.get_gameObject().SetActive(false);
			this.mItems.Add(list.get_Item(i).Id, component);
		}
		int currentVersion = OperateActivityManager.Instance.CurrentVersion;
		List<UpdateAcInfo> localUpdateGiftInfos = OperateActivityManager.Instance.LocalUpdateGiftInfos;
		using (List<UpdateAcInfo>.Enumerator enumerator = localUpdateGiftInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				UpdateAcInfo current = enumerator.get_Current();
				if (this.mItems.ContainsKey(current.acId) && this.mItems.get_Item(current.acId).Data.FinishPar <= currentVersion && current.status == UpdateAcInfo.AcStep.STEP.Ready)
				{
					if (this.reqIds == null)
					{
						this.reqIds = new List<int>();
					}
					OperateActivityManager.Instance.SendUpdateAwardReq(current.acId);
					this.reqIds.Add(current.acId);
				}
			}
		}
	}

	private void OnClickButton(string type, UpdateGiftItem item)
	{
		if (item == null)
		{
			return;
		}
		if (type == "ClickButton")
		{
			if (item.Status == UpdateAcInfo.AcStep.STEP.Ready && OperateActivityManager.Instance.DownloadPackID > 0)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513151, false));
				return;
			}
			switch (item.Status)
			{
			case UpdateAcInfo.AcStep.STEP.Ready:
				if (item.Data.FinishPar - 1 > OperateActivityManager.Instance.CurrentVersion)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513153, false));
				}
				else if (!OperateActivityManager.Instance.IsWifi)
				{
					UIManagerControl.Instance.OpenUI("DialogBoxUI", UINodesManager.T3RootOfSpecial, false, UIType.NonPush);
					DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), string.Format(GameDataUtils.GetChineseContent(513148, false), item.Pack.SizeMbit), null, delegate
					{
						OperateActivityManager.Instance.StartDownloadPackageById(item.Data.Id, true);
						item.Status = UpdateAcInfo.AcStep.STEP.Start;
					}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_yellow_1", UINodesManager.T4RootOfSpecial, true, true);
				}
				else
				{
					OperateActivityManager.Instance.StartDownloadPackageById(item.Data.Id, false);
					item.Status = UpdateAcInfo.AcStep.STEP.Start;
				}
				break;
			case UpdateAcInfo.AcStep.STEP.Finish:
				this.mLastReward = item.Data;
				OperateActivityManager.Instance.SendGetUpdateAwardReq(item.Data.Id);
				break;
			}
		}
		else if (type == "ClickReward")
		{
			this.mLastReward = item.Data;
			this.RewardPreView(true);
		}
	}

	private void OnStartDownloadResult()
	{
		if (this.reqIds != null)
		{
			for (int i = 0; i < this.reqIds.get_Count(); i++)
			{
				OperateActivityManager.Instance.SendDownLoadFinishReq(this.reqIds.get_Item(i));
			}
			this.reqIds = null;
		}
	}

	protected override void OnClickMaskAction()
	{
		if (OperateActivityManager.Instance.isForecUpdate)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513160, false));
		}
		else
		{
			base.OnClickMaskAction();
		}
	}

	private void RewardPreView(bool isPreview = true)
	{
		if (this.mLastReward == null)
		{
			return;
		}
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		List<DiaoLuoGuiZe> dataList = DataReader<DiaoLuoGuiZe>.DataList;
		List<DiaoLuoZu> dataList2 = DataReader<DiaoLuoZu>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).ruleId == this.mLastReward.DropId)
			{
				for (int j = 0; j < dataList2.get_Count(); j++)
				{
					if (dataList2.get_Item(j).groupId == dataList.get_Item(i).groupId)
					{
						list.Add(dataList2.get_Item(j).itemId);
						list2.Add((long)dataList2.get_Item(j).minNum);
					}
				}
			}
		}
		RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
		rewardUI.SetRewardItem(GameDataUtils.GetChineseContent((!isPreview) ? 513164 : 513163, false), list, list2, true, false, null, null);
	}

	private void GetRewardResult()
	{
		this.RewardPreView(false);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateAwardPush, new Callback(this.RefreshUI));
		EventDispatcher.AddListener<int, float, float>(EventNames.UpdateDownloadProgress, new Callback<int, float, float>(this.RefreshProgress));
		EventDispatcher.AddListener(EventNames.UpdateStartDownloadRes, new Callback(this.OnStartDownloadResult));
		EventDispatcher.AddListener(EventNames.GetUpdateRewardRes, new Callback(this.GetRewardResult));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateAwardPush, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener<int, float, float>(EventNames.UpdateDownloadProgress, new Callback<int, float, float>(this.RefreshProgress));
		EventDispatcher.RemoveListener(EventNames.UpdateStartDownloadRes, new Callback(this.OnStartDownloadResult));
		EventDispatcher.RemoveListener(EventNames.GetUpdateRewardRes, new Callback(this.GetRewardResult));
	}
}
