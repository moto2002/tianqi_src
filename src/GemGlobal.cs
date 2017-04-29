using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class GemGlobal
{
	public static BaoShiShengJi GetBSSJ(int itemId)
	{
		return DataReader<BaoShiShengJi>.Get(itemId);
	}

	public static int GetGemId(int type, int lv)
	{
		List<BaoShiShengJi> dataList = DataReader<BaoShiShengJi>.DataList;
		using (List<BaoShiShengJi>.Enumerator enumerator = dataList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				BaoShiShengJi current = enumerator.get_Current();
				if (current.type == type && current.lv == lv)
				{
					return current.id;
				}
			}
		}
		return 0;
	}

	public static string GetGemName(int type, int lv)
	{
		int gemId = GemGlobal.GetGemId(type, lv);
		return GameDataUtils.GetItemName(gemId, true, 0L);
	}

	public static string GetGemColor(int itemId)
	{
		return GemGlobal.GetBSSJ(itemId).color;
	}

	public static int GetGemLv(int itemId)
	{
		return GemGlobal.GetBSSJ(itemId).lv;
	}

	public static int GetValue(int itemId)
	{
		return GemGlobal.GetBSSJ(itemId).value;
	}

	public static int GetAmount(int itemId)
	{
		return 0;
	}

	public static int GetComposeAmount(int itemId)
	{
		return GemGlobal.GetBSSJ(itemId).composeAmount;
	}

	public static int GetNeedId(int itemId)
	{
		return GemGlobal.GetBSSJ(itemId).needId;
	}

	public static int GetAfterId(int itemId)
	{
		return GemGlobal.GetBSSJ(itemId).afterId;
	}

	public static int GetNextGemItemId(int itemId)
	{
		if (itemId == 0)
		{
			return -888;
		}
		return GemGlobal.GetAfterId(itemId);
	}

	public static bool IsGemMaxLv(int itemId)
	{
		return itemId > 0 && GemGlobal.GetAfterId(itemId) == 0;
	}

	public static List<int> GetBetterItemIds(int nextItemId)
	{
		List<int> list = new List<int>();
		while (nextItemId != 0)
		{
			list.Add(nextItemId);
			nextItemId = GemGlobal.GetAfterId(nextItemId);
		}
		list.Reverse();
		return list;
	}

	public static List<int> GetOneLevelGemIds()
	{
		List<int> list = new List<int>();
		List<BaoShiShengJi> dataList = DataReader<BaoShiShengJi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).lv == 1)
			{
				list.Add(dataList.get_Item(i).id);
			}
		}
		return list;
	}

	public static List<int> GetOneLevelGemIds(EquipLibType.ELT equipNum, int slotNum)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> oneLevelGemIds = GemGlobal.GetOneLevelGemIds();
		using (List<int>.Enumerator enumerator = oneLevelGemIds.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				BaoShiShengJi BaoShiShengJiInfo = DataReader<BaoShiShengJi>.Get(current);
				BaoShiKongPeiZhi baoShiKongPeiZhi = DataReader<BaoShiKongPeiZhi>.DataList.Find((BaoShiKongPeiZhi a) => a.position == (int)equipNum && a.slotOpen == slotNum);
				if (baoShiKongPeiZhi != null && baoShiKongPeiZhi.gemType.Find((int e) => e == BaoShiShengJiInfo.type) > 0)
				{
					list.Add(BaoShiShengJiInfo.id);
				}
			}
		}
		return list;
	}

	public static List<MaterialGem> GetMaterialGems(EquipLibType.ELT equipNum, int slotNum)
	{
		List<MaterialGem> list = new List<MaterialGem>();
		List<int> oneLevelGemIds = GemGlobal.GetOneLevelGemIds(equipNum, slotNum);
		for (int i = 0; i < oneLevelGemIds.get_Count(); i++)
		{
			int num = oneLevelGemIds.get_Item(i);
			List<MaterialGem> list2 = new List<MaterialGem>();
			for (int num2 = num; num2 != 0; num2 = GemGlobal.GetAfterId(num2))
			{
				if (GemGlobal.IsGemEnoughLv(num2))
				{
					List<Goods> list3 = BackpackManager.Instance.OnGetGood(num2);
					for (int j = 0; j < list3.get_Count(); j++)
					{
						Goods goods = list3.get_Item(j);
						list2.Add(new MaterialGem
						{
							typeId = num2,
							gemId = goods.GetLongId(),
							count = goods.GetCount()
						});
					}
				}
			}
			list2.Reverse();
			list.AddRange(list2.ToArray());
		}
		return list;
	}

	public static Items GetItemInfo(int itemId)
	{
		return DataReader<Items>.Get(itemId);
	}

	public static int GetIconId(int itemId)
	{
		return GemGlobal.GetItemInfo(itemId).icon;
	}

	public static SpriteRenderer GetIconSprite(int itemId)
	{
		int iconId = GemGlobal.GetIconId(itemId);
		return GameDataUtils.GetIcon(iconId);
	}

	public static int GetDescribeId1(int itemId)
	{
		return GemGlobal.GetItemInfo(itemId).describeId1;
	}

	public static string GetName(int itemId)
	{
		int name = GemGlobal.GetItemInfo(itemId).name;
		return GameDataUtils.GetChineseContent(name, false);
	}

	public static int GetRoleLvRequire(int itemId)
	{
		return GemGlobal.GetItemInfo(itemId).minLv;
	}

	public static bool IsGemEnoughLv(int itemId)
	{
		return EntityWorld.Instance.EntSelf.Lv >= GemGlobal.GetRoleLvRequire(itemId);
	}

	public static SpriteRenderer GetGemItemFrameSprite(EquipLibType.ELT pos, int slotIndex)
	{
		BaoShiKongPeiZhi baoShiKongPeiZhi = DataReader<BaoShiKongPeiZhi>.DataList.Find((BaoShiKongPeiZhi a) => a.position == (int)pos && a.slotOpen == slotIndex);
		if (baoShiKongPeiZhi != null && baoShiKongPeiZhi.gemType.get_Count() > 0)
		{
			int color = baoShiKongPeiZhi.gemType.get_Item(0);
			return GemGlobal.GetGemItemFrameByColor(color);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetGemItemFrameByColor(int color)
	{
		return ResourceManager.GetIconSprite(GemGlobal.GetGemItemFrameName(color));
	}

	public static string GetGemItemFrameName(int quality)
	{
		string result = string.Empty;
		if (quality == 1)
		{
			result = "jewel_box_icon_white";
		}
		else if (quality == 2)
		{
			result = "jewel_box_icon_blue";
		}
		else if (quality == 3)
		{
			result = "jewel_box_icon_yellow";
		}
		else if (quality == 4)
		{
			result = "jewel_box_icon_green";
		}
		else if (quality == 5)
		{
			result = "jewel_box_icon_orange";
		}
		else if (quality == 6)
		{
			result = "jewel_box_icon_purple";
		}
		else
		{
			result = "frame_icon_small2";
		}
		return result;
	}

	public static int GetAllGemLv()
	{
		int num = 0;
		List<int> list = new List<int>();
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (GemManager.Instance.equipSlots.GetLength(0) >= i)
				{
					if (GemManager.Instance.equipSlots.GetLength(1) >= j)
					{
						GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[i, j];
						if (gemEmbedInfo != null && gemEmbedInfo.typeId > 0)
						{
							if (DataReader<BaoShiShengJi>.Contains(gemEmbedInfo.typeId))
							{
								BaoShiShengJi baoShiShengJi = DataReader<BaoShiShengJi>.Get(gemEmbedInfo.typeId);
								num += baoShiShengJi.lv;
							}
						}
					}
				}
			}
		}
		return num;
	}

	public static long getAllGemAttrValue()
	{
		long num = 0L;
		List<int> list = new List<int>();
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (GemManager.Instance.equipSlots.GetLength(0) >= i)
				{
					if (GemManager.Instance.equipSlots.GetLength(1) >= j)
					{
						GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[i, j];
						if (gemEmbedInfo != null && gemEmbedInfo.typeId > 0)
						{
							list.Add(gemEmbedInfo.typeId);
						}
					}
				}
			}
		}
		for (int k = 0; k < list.get_Count(); k++)
		{
			int itemId = list.get_Item(k);
			List<int> attrs = GemGlobal.GetAttrs(itemId);
			List<int> values = GemGlobal.GetValues(itemId);
			num += EquipGlobal.CalculateFightingByIDAndValue(attrs, values);
		}
		return num;
	}

	public static Attrs GetAttrInfo(int attrId)
	{
		return DataReader<Attrs>.Get(attrId);
	}

	public static int GetAttrId(int itemId)
	{
		BaoShiShengJi bSSJ = GemGlobal.GetBSSJ(itemId);
		if (bSSJ == null)
		{
			Debug.Log("<color=red>Error:</color>获取宝石属性失败，没找到对应配置:" + itemId);
			return -1;
		}
		return bSSJ.propertyType;
	}

	public static List<string> GetStrAttrs(int itemId)
	{
		List<int> attrs = GemGlobal.GetAttrs(itemId);
		List<int> values = GemGlobal.GetValues(itemId);
		List<string> list = new List<string>();
		for (int i = 0; i < attrs.get_Count(); i++)
		{
			list.Add(AttrUtility.GetStandardAddDesc(attrs.get_Item(i), values.get_Item(i), "ff7d4b"));
		}
		return list;
	}

	public static List<int> GetAttrs(int itemId)
	{
		int attrId = GemGlobal.GetAttrId(itemId);
		if (attrId > 0)
		{
			return GemGlobal.GetAttrInfo(attrId).attrs;
		}
		Debug.Log("<color=red>Error:</color>propertyType <= 0");
		return new List<int>();
	}

	public static List<int> GetValues(int itemId)
	{
		int attrId = GemGlobal.GetAttrId(itemId);
		if (attrId > 0)
		{
			return GemGlobal.GetAttrInfo(attrId).values;
		}
		Debug.Log("<color=red>Error:</color>propertyType <= 0");
		return new List<int>();
	}

	public static int GetSlotUnlockRequireBatchLv(EquipLibType.ELT pos, int slotNum)
	{
		BaoShiKongPeiZhi baoShiKongPeiZhi = DataReader<BaoShiKongPeiZhi>.DataList.Find((BaoShiKongPeiZhi a) => a.position == (int)pos && a.slotOpen == slotNum);
		if (baoShiKongPeiZhi != null)
		{
			return baoShiKongPeiZhi.openingCondition;
		}
		return -1;
	}

	public static bool IsCanCompose(int typeId)
	{
		if (GemGlobal.IsGemMaxLv(typeId))
		{
			return false;
		}
		int afterId = GemGlobal.GetAfterId(typeId);
		int composeAmount = GemGlobal.GetComposeAmount(afterId);
		int gemTotalCountInBag = GemGlobal.GetGemTotalCountInBag(typeId);
		return gemTotalCountInBag >= composeAmount;
	}

	public static void ComposeGemOne(int typeId)
	{
		int afterId = GemGlobal.GetAfterId(typeId);
		GemManager.Instance.SendGemSysCompositeReq(afterId, 1);
	}

	public static void ComposeGemAll(int typeId)
	{
		int afterId = GemGlobal.GetAfterId(typeId);
		GemManager.Instance.SendGemSysCompositeReq(afterId, -1);
	}

	public static string GetComposeGemTip(int typeId)
	{
		if (GemGlobal.IsGemMaxLv(typeId))
		{
			return "已升至顶级宝石";
		}
		if (!GemGlobal.IsCanCompose(typeId))
		{
			return "材料不足";
		}
		return string.Empty;
	}

	public static int GetOneKeyComposeCost(List<MaterialGem> gems, int typeId)
	{
		gems.Reverse();
		List<MaterialGem> list = new List<MaterialGem>();
		using (List<MaterialGem>.Enumerator enumerator = gems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MaterialGem current = enumerator.get_Current();
				for (int i = 0; i < current.count; i++)
				{
					MaterialGem materialGem = new MaterialGem
					{
						typeId = current.typeId,
						gemId = current.gemId,
						count = current.count
					};
					list.Add(materialGem);
				}
			}
		}
		int num = 0;
		int num2 = 0;
		while (num < list.get_Count() && list.get_Count() > 1)
		{
			int typeId2 = list.get_Item(num).typeId;
			int afterId = GemGlobal.GetAfterId(typeId2);
			int composeAmount = GemGlobal.GetComposeAmount(afterId);
			int num3 = 0;
			while (num < list.get_Count() && typeId2 == list.get_Item(num++).typeId)
			{
				num3++;
			}
			list.RemoveRange(0, num3);
			int num4 = num3 / composeAmount;
			for (int j = 0; j < num4; j++)
			{
				MaterialGem materialGem2 = new MaterialGem
				{
					typeId = afterId,
					gemId = -1L,
					count = 1
				};
				list.Insert(0, materialGem2);
				num2 += GemGlobal.GetAmount(afterId);
			}
			string text = string.Empty;
			using (List<MaterialGem>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					MaterialGem current2 = enumerator2.get_Current();
					text = text + current2.typeId + " ";
				}
			}
			if (list.get_Count() == 1)
			{
				break;
			}
			num = 0;
		}
		return num2;
	}

	public static List<MaterialGem> GetOneKeyComposeGems(EquipLibType.ELT equipNum, int slotNum, int nextTypeId)
	{
		List<MaterialGem> list = new List<MaterialGem>();
		int value = GemGlobal.GetValue(nextTypeId);
		int num = 0;
		GemEmbedInfo gemEmbedInfo = GemGlobal.GetGemInfo(equipNum, slotNum);
		for (int needId = GemGlobal.GetNeedId(nextTypeId); needId != 0; needId = GemGlobal.GetNeedId(needId))
		{
			if (gemEmbedInfo != null && gemEmbedInfo.typeId == needId)
			{
				list.Add(new MaterialGem
				{
					typeId = gemEmbedInfo.typeId,
					gemId = gemEmbedInfo.id,
					count = 1
				});
				num += GemGlobal.GetValue(needId);
				if (num == value)
				{
					return list;
				}
				gemEmbedInfo = null;
			}
			List<Goods> list2 = BackpackManager.Instance.OnGetGood(needId);
			for (int i = 0; i < list2.get_Count(); i++)
			{
				Goods goods = list2.get_Item(i);
				int num2 = 0;
				for (int j = 0; j < goods.GetCount(); j++)
				{
					num2++;
					num += GemGlobal.GetValue(needId);
					if (num == value)
					{
						list.Add(new MaterialGem
						{
							typeId = needId,
							gemId = goods.GetLongId(),
							count = num2
						});
						return list;
					}
				}
				list.Add(new MaterialGem
				{
					typeId = needId,
					gemId = goods.GetLongId(),
					count = goods.GetCount()
				});
			}
		}
		return null;
	}

	public static bool IsActiveOneKeyCompose(EquipLibType.ELT equipNum, int slotNum, int typeId)
	{
		Debug.Log("IsActiveOneKeyCompose typeId=" + typeId);
		int num = 1;
		BaoShiShengJi baoShiShengJi = DataReader<BaoShiShengJi>.Get(typeId);
		int num2 = GemGlobal.GetGemId(baoShiShengJi.type, 1);
		while (num2 != typeId && num2 != 0)
		{
			num2 = GemGlobal.GetAfterId(num2);
			num++;
		}
		return num >= 4;
	}

	public static bool IsCanOneKeyCompose(EquipLibType.ELT equipNum, int slotNum)
	{
		GemEmbedInfo gemInfo = GemGlobal.GetGemInfo(equipNum, slotNum);
		return gemInfo != null && GemGlobal.IsCanOneKeyCompose(equipNum, slotNum, gemInfo.typeId);
	}

	public static bool IsEnoughMoneyToOneKeyCompose(EquipLibType.ELT equipNum, int slotNum, int nextTypeId)
	{
		List<MaterialGem> oneKeyComposeGems = GemGlobal.GetOneKeyComposeGems(equipNum, slotNum, nextTypeId);
		if (oneKeyComposeGems != null)
		{
			int oneKeyComposeCost = GemGlobal.GetOneKeyComposeCost(oneKeyComposeGems, nextTypeId);
			return EntityWorld.Instance.EntSelf.Gold >= (long)oneKeyComposeCost;
		}
		return false;
	}

	public static bool IsCanOneKeyCompose(EquipLibType.ELT equipNum, int slotNum, int currTypeId)
	{
		if (GemGlobal.IsGemMaxLv(currTypeId))
		{
			return false;
		}
		if (currTypeId == 0)
		{
			return false;
		}
		int afterId = GemGlobal.GetAfterId(currTypeId);
		return GemGlobal.IsGemEnoughLv(afterId) && GemGlobal.IsEnoughMoneyToOneKeyCompose(equipNum, slotNum, afterId);
	}

	public static GemEmbedInfo GetGemInfo(EquipLibType.ELT equipNum, int slotNum)
	{
		return GemManager.Instance.equipSlots[equipNum - EquipLibType.ELT.Weapon, slotNum - 1];
	}

	public static List<int> GetAlreadyUsedGemTypes(EquipLibType.ELT equipNum, int slotNum)
	{
		int num = equipNum - EquipLibType.ELT.Weapon;
		int num2 = slotNum - 1;
		List<int> list = new List<int>();
		for (int i = 0; i < 4; i++)
		{
			if (i != num2)
			{
				GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[num, i];
				if (gemEmbedInfo != null && gemEmbedInfo.typeId != 0)
				{
					int type = GemGlobal.GetBSSJ(gemEmbedInfo.typeId).type;
					list.Add(type);
				}
			}
		}
		return list;
	}

	public static bool IsNewOpening(EquipLibType.ELT equipNum, int slotNum)
	{
		return GemManager.Instance.newOpeningSlots[equipNum - EquipLibType.ELT.Weapon, slotNum - 1];
	}

	public static int GetGemTotalCountInBag(int itemId)
	{
		int num = 0;
		List<Goods> list = BackpackManager.Instance.OnGetGood(itemId);
		for (int i = 0; i < list.get_Count(); i++)
		{
			num += list.get_Item(i).GetCount();
		}
		return num;
	}

	public static bool IsHasGemInBag()
	{
		List<BaoShiShengJi> dataList = DataReader<BaoShiShengJi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (BackpackManager.Instance.OnGetGoodCount(dataList.get_Item(i).id) > 0L)
			{
				return true;
			}
		}
		return false;
	}

	public static long GetGemIdInBag(int itemId)
	{
		if (itemId == 0)
		{
			return 0L;
		}
		return BackpackManager.Instance.OnGetGoodLongId(itemId);
	}

	public static bool IsCanGemLvUp(int slotNum, int typeId)
	{
		return true;
	}

	public static bool IsCanWearGem(EquipLibType.ELT equipNum, int slotNum)
	{
		GemEmbedInfo gemInfo = GemGlobal.GetGemInfo(equipNum, slotNum);
		return gemInfo != null && GemGlobal.IsCanWearGem((int)equipNum, slotNum, gemInfo.typeId);
	}

	public static bool IsCanWearGem(int equipNum, int slotNum, int currTypeId = 0)
	{
		List<Goods> runeGoods = BackpackManager.Instance.RuneGoods;
		if (runeGoods.get_Count() <= 0)
		{
			return false;
		}
		for (int i = 0; i < runeGoods.get_Count(); i++)
		{
			int itemId = runeGoods.get_Item(i).GetItemId();
			BaoShiShengJi gemItemCfg = DataReader<BaoShiShengJi>.Get(itemId);
			if (gemItemCfg != null)
			{
				if (GemManager.Instance.equipSlots.GetLength(0) >= equipNum)
				{
					if (GemManager.Instance.equipSlots.GetLength(1) > slotNum)
					{
						GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[equipNum - 1, slotNum];
						if (gemEmbedInfo != null)
						{
							BaoShiKongPeiZhi baoShiKongPeiZhi = DataReader<BaoShiKongPeiZhi>.DataList.Find((BaoShiKongPeiZhi a) => a.position == equipNum && a.slotOpen == slotNum + 1);
							if (baoShiKongPeiZhi != null)
							{
								if (baoShiKongPeiZhi.gemType.Find((int e) => e == gemItemCfg.type) > 0)
								{
									if (gemEmbedInfo.typeId <= 0)
									{
										return true;
									}
									BaoShiShengJi baoShiShengJi = DataReader<BaoShiShengJi>.Get(gemEmbedInfo.typeId);
									if (baoShiShengJi != null)
									{
										if (baoShiShengJi.type == gemItemCfg.type && baoShiShengJi.id < gemItemCfg.id)
										{
											return true;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return false;
	}

	public static bool IsCanCompose(EquipLibType.ELT equipNum, int slotNum)
	{
		GemEmbedInfo gemInfo = GemGlobal.GetGemInfo(equipNum, slotNum);
		return gemInfo != null && GemGlobal.IsCanCompose((int)equipNum, slotNum, gemInfo.typeId);
	}

	public static bool IsCanCompose(int equipNum, int slotNum, int currTypeId)
	{
		if (GemGlobal.IsGemMaxLv(currTypeId))
		{
			return false;
		}
		if (currTypeId == 0)
		{
			return false;
		}
		int afterId = GemGlobal.GetAfterId(currTypeId);
		if (!GemGlobal.IsGemEnoughLv(afterId))
		{
			return false;
		}
		if (!GemGlobal.IsEnoughMoneyToCompose(afterId))
		{
			return false;
		}
		int value = GemGlobal.GetValue(afterId);
		int needId = GemGlobal.GetNeedId(afterId);
		int num = 0;
		while (needId != 0)
		{
			List<Goods> list = BackpackManager.Instance.OnGetGood(needId);
			using (List<Goods>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Goods current = enumerator.get_Current();
					int count = current.GetCount();
					int itemId = current.GetItemId();
					if (GemGlobal.IsGemEnoughLv(itemId))
					{
						int value2 = GemGlobal.GetValue(itemId);
						num += value2 * count;
						if (num >= value)
						{
							return true;
						}
					}
				}
			}
			needId = GemGlobal.GetNeedId(needId);
		}
		return false;
	}

	public static bool IsEnoughMoneyToCompose(int typeId)
	{
		return EntityWorld.Instance.EntSelf.Gold >= (long)GemGlobal.GetAmount(typeId);
	}

	public static bool IsWearSameColorGem(EquipLibType.ELT equipNum, int slotNum, int color)
	{
		List<int> alreadyUsedGemTypes = GemGlobal.GetAlreadyUsedGemTypes(equipNum, slotNum);
		for (int i = 0; i < alreadyUsedGemTypes.get_Count(); i++)
		{
			if (alreadyUsedGemTypes.get_Item(i) == color)
			{
				return true;
			}
		}
		return false;
	}

	public static bool CheckCanShowTip(int pos)
	{
		List<Goods> runeGoods = BackpackManager.Instance.RuneGoods;
		if (runeGoods == null)
		{
			return false;
		}
		if (runeGoods.get_Count() <= 0)
		{
			return false;
		}
		for (int i = 0; i < runeGoods.get_Count(); i++)
		{
			int itemId = runeGoods.get_Item(i).GetItemId();
			BaoShiShengJi gemItemCfg = DataReader<BaoShiShengJi>.Get(itemId);
			if (gemItemCfg != null)
			{
				int j;
				for (j = 0; j < 4; j++)
				{
					GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[pos - 1, j];
					if (gemEmbedInfo != null)
					{
						BaoShiKongPeiZhi baoShiKongPeiZhi = DataReader<BaoShiKongPeiZhi>.DataList.Find((BaoShiKongPeiZhi a) => a.position == pos && a.slotOpen == j + 1);
						if (baoShiKongPeiZhi != null)
						{
							if (baoShiKongPeiZhi.gemType.Find((int e) => e == gemItemCfg.type) > 0)
							{
								if (gemEmbedInfo.typeId <= 0)
								{
									return true;
								}
							}
						}
					}
				}
			}
		}
		return false;
	}

	private static bool CheckGemSlotHasSameType(int pos, int type)
	{
		for (int i = 0; i < 4; i++)
		{
			GemEmbedInfo gemEmbedInfo = GemManager.Instance.equipSlots[pos - 1, i];
			if (gemEmbedInfo != null)
			{
				BaoShiShengJi baoShiShengJi = DataReader<BaoShiShengJi>.Get(gemEmbedInfo.typeId);
				if (baoShiShengJi != null)
				{
					if (baoShiShengJi.type == type)
					{
						return true;
					}
				}
			}
		}
		return false;
	}
}
