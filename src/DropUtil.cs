using GameData;
using System;
using System.Collections.Generic;

public static class DropUtil
{
	private static Dictionary<int, List<int>> ruleIdGroupIdMap;

	private static Dictionary<int, List<DiaoLuoZu>> groupIdItemIdMap;

	public static List<DiaoLuoZu> GetItemList(int dropId)
	{
		List<DiaoLuoZu> list = new List<DiaoLuoZu>();
		if (DropUtil.ruleIdGroupIdMap == null)
		{
			DropUtil.ruleIdGroupIdMap = new Dictionary<int, List<int>>();
			using (List<DiaoLuoGuiZe>.Enumerator enumerator = DataReader<DiaoLuoGuiZe>.DataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DiaoLuoGuiZe current = enumerator.get_Current();
					if (DropUtil.ruleIdGroupIdMap.ContainsKey(current.ruleId))
					{
						DropUtil.ruleIdGroupIdMap.get_Item(current.ruleId).Add(current.groupId);
					}
					else
					{
						List<int> list2 = new List<int>();
						list2.Add(current.groupId);
						DropUtil.ruleIdGroupIdMap.Add(current.ruleId, list2);
					}
				}
			}
			DropUtil.groupIdItemIdMap = new Dictionary<int, List<DiaoLuoZu>>();
			using (List<DiaoLuoZu>.Enumerator enumerator2 = DataReader<DiaoLuoZu>.DataList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					DiaoLuoZu current2 = enumerator2.get_Current();
					if (DropUtil.groupIdItemIdMap.ContainsKey(current2.groupId))
					{
						DropUtil.groupIdItemIdMap.get_Item(current2.groupId).Add(current2);
					}
					else
					{
						List<DiaoLuoZu> list3 = new List<DiaoLuoZu>();
						list3.Add(current2);
						DropUtil.groupIdItemIdMap.Add(current2.groupId, list3);
					}
				}
			}
		}
		if (DropUtil.ruleIdGroupIdMap.ContainsKey(dropId))
		{
			List<int> list4 = DropUtil.ruleIdGroupIdMap.get_Item(dropId);
			using (List<int>.Enumerator enumerator3 = list4.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					int current3 = enumerator3.get_Current();
					if (DropUtil.groupIdItemIdMap.ContainsKey(current3))
					{
						list.AddRange(DropUtil.groupIdItemIdMap.get_Item(current3));
					}
				}
			}
		}
		return list;
	}

	public static List<DiaoLuo> GetDropsByRuleId(int ruleId)
	{
		return DataReader<DiaoLuo>.DataList.FindAll((DiaoLuo a) => a.ruleId == ruleId);
	}

	public static long GetDropNum(int ruleId, int level, ref int itemId)
	{
		List<DiaoLuo> dropsByRuleId = DropUtil.GetDropsByRuleId(ruleId);
		if (dropsByRuleId.get_Count() == 0)
		{
			return 0L;
		}
		DiaoLuo diaoLuo = dropsByRuleId.get_Item(0);
		if (diaoLuo.dropType == 1)
		{
			return 0L;
		}
		if (diaoLuo.dropType == 2)
		{
			int groupId = diaoLuo.goodsId;
			List<Zu> list = DataReader<Zu>.DataList.FindAll((Zu a) => a.groupId == groupId);
			for (int i = 0; i < list.get_Count(); i++)
			{
				if (level >= list.get_Item(i).minLv && level <= list.get_Item(i).maxLv)
				{
					itemId = list.get_Item(i).itemId;
					return list.get_Item(i).Num;
				}
			}
		}
		return 0L;
	}
}
