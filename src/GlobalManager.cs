using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class GlobalManager : BaseSubSystemManager
{
	private List<ItemBriefInfo> dropGoods;

	private bool isCollectMopupDropitems;

	private int collectMopupDropitemsTimes;

	public List<List<DropItem>> ListDropGoods = new List<List<DropItem>>();

	private static GlobalManager instance;

	public int PromptWay;

	public List<ItemBriefInfo> DropGoods
	{
		get
		{
			return this.dropGoods;
		}
		set
		{
			this.dropGoods = value;
		}
	}

	public static GlobalManager Instance
	{
		get
		{
			if (GlobalManager.instance == null)
			{
				GlobalManager.instance = new GlobalManager();
			}
			return GlobalManager.instance;
		}
	}

	private GlobalManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		if (this.DropGoods != null)
		{
			this.DropGoods.Clear();
		}
		if (this.ListDropGoods != null)
		{
			this.ListDropGoods.Clear();
		}
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<DropItemsNotice>(new NetCallBackMethod<DropItemsNotice>(this.OnGetDropItems));
	}

	private void OnGetDropItems(short state, DropItemsNotice down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down.type == DropType.DT.GuildField)
		{
			this.ShowExpFloatTips(down.items);
			return;
		}
		this.dropGoods = down.items;
		this.ShowBackpackUseGoodRewards(down.items);
		EventDispatcher.Broadcast(DungeonManagerEvent.OnGetDropItems);
	}

	private void ShowBackpackUseGoodRewards(List<ItemBriefInfo> rewardList)
	{
		if (this.PromptWay == 1 && rewardList != null)
		{
			List<int> list = new List<int>();
			List<long> list2 = new List<long>();
			List<long> list3 = new List<long>();
			for (int i = 0; i < rewardList.get_Count(); i++)
			{
				list.Add(rewardList.get_Item(i).cfgId);
				list2.Add(rewardList.get_Item(i).count);
				list3.Add(rewardList.get_Item(i).uId);
			}
			if (list.get_Count() > 0)
			{
				RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.MiddleUIRoot);
				rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
			}
		}
		this.PromptWay = -1;
	}

	private void ShowExpFloatTips(List<ItemBriefInfo> rewardItems)
	{
		if (rewardItems != null && rewardItems.get_Count() > 0)
		{
			for (int i = 0; i < rewardItems.get_Count(); i++)
			{
				if (rewardItems.get_Item(i).cfgId == 1)
				{
					string content = GameDataUtils.GetItemName(1, false, 0L) + "+" + Utils.SwitchChineseNumber(rewardItems.get_Item(i).count, 0);
					FloatTipManager.Instance.AddFloatTip(EntityWorld.Instance.EntSelf.ID, EntityWorld.Instance.EntSelf.Actor.FixTransform, content, "50ff14", false, 1f, 1f, 2.5f, 58f);
					break;
				}
			}
		}
	}

	public void CollectMopupDropitems(int time)
	{
		this.ListDropGoods.Clear();
		this.collectMopupDropitemsTimes = time;
		this.isCollectMopupDropitems = true;
	}
}
