using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class RedBagManager : BaseSubSystemManager
{
	protected static RedBagManager instance;

	private OwnerRedPacketsNty InfoData;

	private GetRedPacketRes PanelData;

	private bool isCantnotGetInfo;

	private uint delayId;

	public static RedBagManager Instance
	{
		get
		{
			if (RedBagManager.instance == null)
			{
				RedBagManager.instance = new RedBagManager();
			}
			return RedBagManager.instance;
		}
	}

	protected RedBagManager()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<RedPacketNty>(new NetCallBackMethod<RedPacketNty>(this.OnRedPacketNty));
		NetworkManager.AddListenEvent<GetRedPacketRes>(new NetCallBackMethod<GetRedPacketRes>(this.OnGetRedPacketRes));
		NetworkManager.AddListenEvent<OwnerRedPacketsNty>(new NetCallBackMethod<OwnerRedPacketsNty>(this.OnOwnerRedPacketsNty));
	}

	public override void Release()
	{
	}

	public void SendGetRedPacketReq()
	{
		if (this.InfoData != null)
		{
			int id = 0;
			int num = 0;
			if (this.InfoData.redPackets != null && this.InfoData.redPackets.get_Count() > 0)
			{
				for (int i = 0; i < this.InfoData.redPackets.get_Count(); i++)
				{
					if (i == 0)
					{
						num = this.InfoData.redPackets.get_Item(i).times;
						id = this.InfoData.redPackets.get_Item(i).id;
					}
					if (this.InfoData.redPackets.get_Item(i).times <= num)
					{
						id = this.InfoData.redPackets.get_Item(i).id;
						num = this.InfoData.redPackets.get_Item(i).times;
					}
				}
			}
			NetworkManager.Send(new GetRedPacketReq
			{
				id = id
			}, ServerType.Data);
		}
	}

	public void OnRedPacketNty(short state, RedPacketNty down = null)
	{
		if (this.delayId != 0u)
		{
			TimerHeap.DelTimer(this.delayId);
		}
		this.isCantnotGetInfo = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.InfoData != null)
			{
				this.InfoData.redPackets.Add(down.redPackets);
			}
			else
			{
				this.InfoData = new OwnerRedPacketsNty();
				this.InfoData.redPackets.Add(down.redPackets);
			}
		}
		EventDispatcher.Broadcast(EventNames.RedBagFresh);
	}

	public void OnGetRedPacketRes(short state, GetRedPacketRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.Status)
			{
				this.PanelData = down;
				UIManagerControl.Instance.OpenUI("RedBagUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513247, false));
			}
			if (this.delayId != 0u)
			{
				TimerHeap.DelTimer(this.delayId);
			}
			this.isCantnotGetInfo = false;
			this.delayId = TimerHeap.AddTimer(6000u, 0, delegate
			{
				this.isCantnotGetInfo = true;
			});
		}
	}

	public void OnOwnerRedPacketsNty(short state, OwnerRedPacketsNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.InfoData = down;
		}
		EventDispatcher.Broadcast(EventNames.RedBagFresh);
	}

	public bool CheckRedBagActive()
	{
		return this.InfoData != null && this.InfoData.redPackets != null && this.InfoData.redPackets.get_Count() > 0;
	}

	public int GetRedBagNum()
	{
		if (this.InfoData == null || this.InfoData.redPackets == null)
		{
			return 0;
		}
		return this.InfoData.redPackets.get_Count();
	}

	public int GetRedBagLeftTime()
	{
		if (this.InfoData == null)
		{
			return 0;
		}
		int num = 0;
		if (this.InfoData != null && this.InfoData.redPackets != null && this.InfoData.redPackets.get_Count() > 0)
		{
			for (int i = 0; i < this.InfoData.redPackets.get_Count(); i++)
			{
				if (i == 0)
				{
					num = this.InfoData.redPackets.get_Item(i).times;
				}
				if (this.InfoData.redPackets.get_Item(i).times < num)
				{
					num = this.InfoData.redPackets.get_Item(i).times;
				}
			}
		}
		if (num > TimeManager.Instance.PreciseServerSecond)
		{
			return num - TimeManager.Instance.PreciseServerSecond;
		}
		return 0;
	}

	public GetRedPacketRes GetPanelData()
	{
		return this.PanelData;
	}

	public void ShowGift()
	{
		if (this.PanelData != null && this.PanelData.rewards != null && this.PanelData.rewards.get_Count() > 0)
		{
			List<int> list = new List<int>();
			List<long> list2 = new List<long>();
			List<long> list3 = new List<long>();
			for (int i = 0; i < this.PanelData.rewards.get_Count(); i++)
			{
				list.Add(this.PanelData.rewards.get_Item(i).cfgId);
				list2.Add(this.PanelData.rewards.get_Item(i).count);
				list3.Add(this.PanelData.rewards.get_Item(i).uId);
			}
			RewardUI rewardUI = LinkNavigationManager.OpenRewardUI(UINodesManager.TopUIRoot);
			rewardUI.SetRewardItem("获得物品", list, list2, true, false, null, list3);
		}
	}
}
