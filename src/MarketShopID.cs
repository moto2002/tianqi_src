using GameData;
using System;
using System.Collections.Generic;

public class MarketShopID
{
	public const int MainCityShop = 1;

	public const int ArenaShop = 2;

	public const int FleaShop = 3;

	public static List<int> GetShopIds()
	{
		List<ShangChengBiao> dataList = DataReader<ShangChengBiao>.DataList;
		List<int> list = new List<int>();
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			list.Add(dataList.get_Item(i).shopId);
		}
		return list;
	}
}
