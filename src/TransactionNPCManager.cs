using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class TransactionNPCManager : BaseSubSystemManager
{
	protected static TransactionNPCManager instance;

	public int SystemId = 77;

	public int NpcTypeRandom = 1001;

	public int NpcTypeStationary = 2001;

	protected int functionId;

	protected int systemId;

	public List<NpcShopSt> TransactionNpcData;

	public int CurrentShopId = 2001;

	protected int mineEnteredID;

	public static TransactionNPCManager Instance
	{
		get
		{
			if (TransactionNPCManager.instance == null)
			{
				TransactionNPCManager.instance = new TransactionNPCManager();
			}
			return TransactionNPCManager.instance;
		}
	}

	protected int MineEnteredID
	{
		get
		{
			return this.mineEnteredID;
		}
		set
		{
			this.mineEnteredID = value;
		}
	}

	public bool IsWaitForUI
	{
		get
		{
			return this.mineEnteredID > 0;
		}
	}

	protected TransactionNPCManager()
	{
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener<NPC>(EventNames.OpenNPCMenu, new Callback<NPC>(this.OnEnterMineNPC));
		EventDispatcher.AddListener<NPC>(EventNames.OpenNPCSystem, new Callback<NPC>(this.OnEnterMineNPC));
		EventDispatcher.AddListener(EventNames.CloseNPCMenu, new Callback(this.OnExitNPC));
		EventDispatcher.AddListener<bool, int>(EventNames.UpdateTalkBubble, new Callback<bool, int>(this.UpdateTalkBubble));
		NetworkManager.AddListenEvent<NpcShopStPush>(new NetCallBackMethod<NpcShopStPush>(this.OnNpcShopStPush));
		NetworkManager.AddListenEvent<OpenNpcShopRes>(new NetCallBackMethod<OpenNpcShopRes>(this.OnNpcShopRes));
		NetworkManager.AddListenEvent<ShoppingRes>(new NetCallBackMethod<ShoppingRes>(this.OnShoppingRes));
	}

	public override void Release()
	{
	}

	public void SendNpcShopReq(int shopId)
	{
		NetworkManager.Send(new OpenNpcShopReq
		{
			shopId = shopId
		}, ServerType.Data);
	}

	public void SendShoppingReq(ShopType.ST shopType, int shopId, int goodsId, int count)
	{
		NetworkManager.Send(new ShoppingReq
		{
			shopType = shopType,
			shopId = shopId,
			goodsId = goodsId,
			count = count
		}, ServerType.Data);
	}

	public void OnNpcShopStPush(short state, NpcShopStPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.TransactionNpcData == null || this.TransactionNpcData.get_Count() < 1)
			{
				this.TransactionNpcData = down.npcShopSt;
			}
			else
			{
				for (int i = 0; i < this.TransactionNpcData.get_Count(); i++)
				{
					for (int j = 0; j < down.npcShopSt.get_Count(); j++)
					{
						if (this.TransactionNpcData.get_Item(i).shopId == down.npcShopSt.get_Item(j).shopId)
						{
							this.TransactionNpcData.set_Item(i, down.npcShopSt.get_Item(j));
						}
					}
				}
			}
		}
		if (NpcShopUI.Instance != null && NpcShopUI.Instance.get_gameObject().get_activeSelf())
		{
			NpcShopUI.Instance.InitTime();
			NpcShopUI.Instance.SendMsg();
		}
		TaskNPCManager.Instance.UpdateAllShopNPC();
	}

	public void OnNpcShopRes(short state, OpenNpcShopRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && NpcShopUI.Instance != null && NpcShopUI.Instance.get_gameObject().get_activeSelf())
		{
			NpcShopUI.Instance.RefreshUI(down);
		}
	}

	public void OnShoppingRes(short state, ShoppingRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513183, false));
			if (NpcShopUI.Instance != null && NpcShopUI.Instance.get_gameObject().get_activeSelf())
			{
				NpcShopUI.Instance.UpdateShoppingInfo(down.goodsId, down.stock);
			}
		}
	}

	public void ReFreshItem()
	{
		if (NpcShopUI.Instance != null && NpcShopUI.Instance.get_gameObject().get_activeSelf())
		{
			NpcShopUI.Instance.ReFreshItemSelect();
		}
	}

	protected void OnEnterMineNPC(NPC npcData)
	{
		if (npcData == null || npcData.function.get_Count() <= 0 || npcData.function.get_Item(0) != this.SystemId)
		{
			return;
		}
		this.mineEnteredID = 1;
		this.functionId = npcData.function.get_Item(0);
		this.systemId = 0;
		if (npcData.function.get_Count() > 1)
		{
			this.systemId = npcData.function.get_Item(1);
		}
	}

	private void OnExitNPC()
	{
		this.Clear();
	}

	private void UpdateTalkBubble(bool isShow, int npcId)
	{
		if (npcId == 0)
		{
			return;
		}
		NPC nPC = DataReader<NPC>.Get(npcId);
		if (nPC != null && nPC.function != null && nPC.function.get_Count() > 0 && nPC.function.get_Item(0) == TransactionNPCManager.Instance.SystemId && !isShow)
		{
			this.mineEnteredID = 0;
			this.functionId = 0;
			this.systemId = 0;
		}
	}

	public void OnClickChallengeUI()
	{
		if (this.functionId == TransactionNPCManager.Instance.SystemId)
		{
			if (this.CheckIsNpcRandomShop(this.systemId))
			{
				this.OpenRandomNpcShop();
			}
			else
			{
				this.OpenStationaryNpcShop();
			}
		}
	}

	public void OpenRandomNpcShop()
	{
		this.CurrentShopId = this.systemId;
		LinkNavigationManager.OpenNpcShopUIUI();
	}

	public void OpenStationaryNpcShop()
	{
		this.CurrentShopId = this.systemId;
		LinkNavigationManager.OpenNpcShopUIUI();
	}

	public void Clear()
	{
		this.mineEnteredID = 0;
		this.functionId = 0;
		this.systemId = 0;
	}

	public bool CheckIsNpcRandomShop(int shopId)
	{
		NPCShangChengBiao nPCShangChengBiao = DataReader<NPCShangChengBiao>.Get(shopId);
		return nPCShangChengBiao.type == 1;
	}
}
