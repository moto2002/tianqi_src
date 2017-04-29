using GameData;
using Package;
using System;

public class Goods
{
	private ItemInfo serverItem;

	private Items localItem;

	public ItemInfo ServerItem
	{
		get
		{
			return this.serverItem;
		}
	}

	public Items LocalItem
	{
		get
		{
			return this.localItem;
		}
	}

	public Goods(ItemInfo server)
	{
		this.serverItem = server;
		this.localItem = DataReader<Items>.Get(server.baseInfo.itemId);
	}

	public int GetCount()
	{
		return this.serverItem.baseInfo.count;
	}

	public int GetItemId()
	{
		return this.serverItem.baseInfo.itemId;
	}

	public long GetLongId()
	{
		return this.serverItem.baseInfo.id;
	}

	public Items GetItem()
	{
		return this.localItem;
	}

	public ItemInfo GetItemInfo()
	{
		return this.serverItem;
	}
}
