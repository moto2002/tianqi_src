using GameData;
using Package;
using System;
using UnityEngine;
using XNetwork;

public class GrowUpPlanManager : BaseSubSystemManager
{
	public GrowthPlanListPush dataList;

	private static GrowUpPlanManager instance;

	public static GrowUpPlanManager Instance
	{
		get
		{
			if (GrowUpPlanManager.instance == null)
			{
				GrowUpPlanManager.instance = new GrowUpPlanManager();
			}
			return GrowUpPlanManager.instance;
		}
	}

	private GrowUpPlanManager()
	{
	}

	private void UpdateData(GrowthPlanListPush data)
	{
		this.dataList = data;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GrowthPlanListPush>(new NetCallBackMethod<GrowthPlanListPush>(this.OnUpdatePanel));
		NetworkManager.AddListenEvent<BuyGrowthPlanRes>(new NetCallBackMethod<BuyGrowthPlanRes>(this.OnBuySuccessRes));
	}

	public void SendBuyPlanReq()
	{
		NetworkManager.Send(new BuyGrowthPlanReq
		{
			typeId = 8
		}, ServerType.Data);
	}

	public void SendGetRewardReq(int typeId, int roleLv)
	{
		NetworkManager.Send(new GetActivityItemPrizeReq
		{
			typeId = typeId,
			activityItemId = roleLv
		}, ServerType.Data);
	}

	public void OnUpdatePanel(short state, GrowthPlanListPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.UpdateData(down);
			this.RefreshUI();
		}
		else
		{
			Debug.LogError("OperateAcPushClient  is  empty！________________________________________________________________________");
		}
	}

	public void OnBuySuccessRes(short state, BuyGrowthPlanRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513183, false));
	}

	public void OnGetRewardRes(short state, GetActivityItemPrizeRes down = null)
	{
		if (GrowUpPlanUI.Instance != null && GrowUpPlanUI.Instance.get_gameObject().get_activeSelf())
		{
			ChengChangJiHua chengChangJiHua = DataReader<ChengChangJiHua>.Get(down.activityItemId);
			long itemNum = chengChangJiHua.ItemNum;
			string equipItemNameAndLV = GameDataUtils.GetEquipItemNameAndLV(chengChangJiHua.ItemId, false);
			string text = string.Format(GameDataUtils.GetChineseContent(513180, false), equipItemNameAndLV, itemNum);
			UIManagerControl.Instance.ShowToastText(text);
		}
	}

	public void RefreshUI()
	{
		EventDispatcher.Broadcast<int>(EventNames.UpdateGrowUpPlanReward, 8);
		if (GrowUpPlanUI.Instance != null && GrowUpPlanUI.Instance.get_gameObject().get_activeSelf())
		{
			GrowUpPlanUI.Instance.RefreshUI();
		}
	}

	public GrowthPlanListPush GetGrowUpPlanData()
	{
		return this.dataList;
	}

	public bool CheckGrwoUpPlanReward()
	{
		bool result = false;
		if (this.dataList != null && this.dataList.item != null)
		{
			for (int i = 0; i < this.dataList.item.get_Count(); i++)
			{
				if (this.dataList.item.get_Item(i).canGetFlag)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}
}
