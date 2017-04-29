using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class GiftExchangeManager : BaseSubSystemManager
{
	private static GiftExchangeManager instance;

	public static GiftExchangeManager Instance
	{
		get
		{
			if (GiftExchangeManager.instance == null)
			{
				GiftExchangeManager.instance = new GiftExchangeManager();
			}
			return GiftExchangeManager.instance;
		}
	}

	private GiftExchangeManager()
	{
	}

	public static bool IsNotNull()
	{
		return GiftExchangeManager.instance != null;
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
		NetworkManager.AddListenEvent<GetGiftResultNty>(new NetCallBackMethod<GetGiftResultNty>(this.OnGitRes));
	}

	public void SendBuyPlanReq(string info)
	{
		NetworkManager.Send(new GetGiftReq
		{
			key = info,
			channel = SDKManager.Instance.GetSDKType()
		}, ServerType.Data);
	}

	public void OnGitRes(short state, GetGiftResultNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		if (down.code > 0)
		{
			switch (down.code)
			{
			case 100:
				Debug.Log("礼包ok");
				break;
			case 104:
				UIManagerControl.Instance.ShowToastText("礼包不存在");
				break;
			case 105:
				UIManagerControl.Instance.ShowToastText("礼包已被使用");
				break;
			case 106:
				UIManagerControl.Instance.ShowToastText("礼包码无效");
				break;
			case 107:
				UIManagerControl.Instance.ShowToastText("已经使用过该类型礼包");
				break;
			}
			if (down.code != 100)
			{
				return;
			}
		}
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		List<long> list3 = new List<long>();
		for (int i = 0; i < down.items.get_Count(); i++)
		{
			list.Add(down.items.get_Item(i).cfgId);
			list2.Add(down.items.get_Item(i).count);
			list3.Add(down.items.get_Item(i).uId);
		}
		RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
		rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
	}
}
