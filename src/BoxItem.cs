using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{
	public enum BoxState
	{
		getBox,
		canBox,
		withoutBox
	}

	public Image BoxIcon;

	public Text num;

	private HuoYueDuJiangLi mData;

	private BoxItem.BoxState mBoxState;

	private List<int> itemIds;

	private List<long> itemNums;

	private GameObject mBoxFxTrans;

	private Transform mFxMask;

	private bool playfxing;

	private int FxID;

	private void Start()
	{
		this.mFxMask = base.get_transform().get_parent().get_parent().get_parent().GetComponent<DailyTaskUI>().FxMask;
		this.BoxIcon.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBox));
	}

	private void OnClickBox()
	{
		switch (this.mBoxState)
		{
		case BoxItem.BoxState.getBox:
			UIManagerControl.Instance.ShowToastText("已领取", 2f, 2f);
			break;
		case BoxItem.BoxState.canBox:
		{
			if (this.playfxing)
			{
				return;
			}
			if (this.mFxMask == null)
			{
				this.mFxMask = UINodesManager.MiddleUIRoot;
			}
			else
			{
				this.mFxMask.get_gameObject().SetActive(true);
			}
			this.playfxing = true;
			RewardUI rewardui;
			this.FxID = FXSpineManager.Instance.ReplaySpine(this.FxID, 801, this.mFxMask, string.Empty, 14000, delegate
			{
				FXSpineManager.Instance.PlaySpine(802, this.mFxMask, string.Empty, 14000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				rewardui = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
				rewardui.SetRewardItem("获得奖励", this.itemIds, this.itemNums, true, true, delegate
				{
					if (this.mFxMask != UINodesManager.MiddleUIRoot)
					{
						this.mFxMask.get_gameObject().SetActive(false);
					}
					if (this.FxID > 0)
					{
						FXSpineManager.Instance.DeleteSpine(this.FxID, true);
						this.FxID = 0;
					}
					if (this.mBoxFxTrans != null)
					{
						Object.Destroy(this.mBoxFxTrans);
						this.mBoxFxTrans = null;
					}
					DailyTaskManager.Instance.SendActivity(this.mData.id);
				}, null);
				this.playfxing = false;
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			FXSpineManager.Instance.PlaySpine(803, this.mFxMask, string.Empty, 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			break;
		}
		case BoxItem.BoxState.withoutBox:
		{
			RewardUI rewardui = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
			rewardui.SetRewardItem("奖励预览", this.itemIds, this.itemNums, false, false, delegate
			{
				UIManagerControl.Instance.ShowToastText("活跃度不足，快去做任务吧", 2f, 2f);
			}, null);
			break;
		}
		}
	}

	public BoxItem.BoxState SetData(HuoYueDuJiangLi data)
	{
		this.mData = data;
		if (this.itemIds == null || this.itemNums == null)
		{
			this.itemIds = new List<int>();
			this.itemNums = new List<long>();
			List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
			for (int i = 0; i < this.mData.reward.get_Count(); i++)
			{
				int num = this.mData.reward.get_Item(i);
				for (int j = 0; j < dataList.get_Count(); j++)
				{
					if (dataList.get_Item(j).ruleId == num)
					{
						this.itemIds.Add(dataList.get_Item(j).goodsId);
						this.itemNums.Add(dataList.get_Item(j).minNum);
					}
				}
			}
		}
		int num2 = DailyTaskManager.Instance.getActivityIds.Find((int e) => e == this.mData.id);
		if (num2 > 0)
		{
			this.mBoxState = BoxItem.BoxState.getBox;
		}
		else if (this.mData.numericalValue <= DailyTaskManager.Instance.totalActivity)
		{
			this.mBoxState = BoxItem.BoxState.canBox;
			if (this.mBoxFxTrans == null)
			{
				this.mBoxFxTrans = new GameObject("BoxFx");
				this.mBoxFxTrans.get_transform().set_parent(this.BoxIcon.get_transform());
				this.mBoxFxTrans.get_transform().set_localScale(Vector3.get_one());
				this.mBoxFxTrans.get_transform().set_localPosition(Vector3.get_zero());
				FXSpineManager.Instance.PlaySpine(805, this.mBoxFxTrans.get_transform(), string.Empty, 2001, null, "UI", -3f, 5f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}
		}
		else
		{
			this.mBoxState = BoxItem.BoxState.withoutBox;
		}
		this.num.set_text(this.mData.numericalValue.ToString());
		if (this.mBoxState == BoxItem.BoxState.getBox)
		{
			ResourceManager.SetSprite(this.BoxIcon, ResourceManager.GetIconSprite("dailytask_icon_bag" + (this.mData.id - 100) * 2));
		}
		else
		{
			ResourceManager.SetSprite(this.BoxIcon, ResourceManager.GetIconSprite("dailytask_icon_bag" + ((this.mData.id - 100) * 2 - 1)));
		}
		this.BoxIcon.GetComponent<Button>().set_interactable(this.mBoxState != BoxItem.BoxState.getBox);
		if (this.mBoxState != BoxItem.BoxState.canBox && this.mBoxFxTrans != null)
		{
			Object.Destroy(this.mBoxFxTrans);
			this.mBoxFxTrans = null;
		}
		return this.mBoxState;
	}
}
