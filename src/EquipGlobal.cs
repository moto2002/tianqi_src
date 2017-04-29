using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipGlobal
{
	public const string IconFrameName = "IconFrameName";

	public const string IconQualityName = "IconQualityName";

	public const string IconName = "IconName";

	public const string QualityLv = "QualityLv";

	public const string ItemName = "ItemName";

	public const string EquipStep = "EquipStep";

	public static int GetCanEnchantmentMinLv()
	{
		return (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("enchantLv").value);
	}

	public static int GetCanStarUpMinLv()
	{
		return DataReader<ShengXingJiChuPeiZhi>.Get("equipLv").num;
	}

	public static void SendComposeOne(int itemID, EquipComposeType type = EquipComposeType.EnchantmentCompose)
	{
		if (type == EquipComposeType.EnchantmentCompose)
		{
			FuMoDaoJuHeCheng fuMoDaoJuHeCheng = DataReader<FuMoDaoJuHeCheng>.Get(itemID);
			if (fuMoDaoJuHeCheng == null || fuMoDaoJuHeCheng.afterId <= 0)
			{
				return;
			}
			int afterId = fuMoDaoJuHeCheng.afterId;
			EquipmentManager.Instance.SendDataCompositeReq(afterId, 1, DataCompositeReq.OpType.Enchanting);
		}
		else if (type == EquipComposeType.StarCompose)
		{
			ShengXingCaiLiaoHeCheng shengXingCaiLiaoHeCheng = DataReader<ShengXingCaiLiaoHeCheng>.Get(itemID);
			if (shengXingCaiLiaoHeCheng == null || shengXingCaiLiaoHeCheng.afterId <= 0)
			{
				return;
			}
			int afterId = shengXingCaiLiaoHeCheng.afterId;
			EquipmentManager.Instance.SendDataCompositeReq(afterId, 1, DataCompositeReq.OpType.RisingStar);
		}
	}

	public static void SendComposeAll(int itemID, EquipComposeType type = EquipComposeType.EnchantmentCompose)
	{
		if (type == EquipComposeType.EnchantmentCompose)
		{
			FuMoDaoJuHeCheng fuMoDaoJuHeCheng = DataReader<FuMoDaoJuHeCheng>.Get(itemID);
			if (fuMoDaoJuHeCheng == null || fuMoDaoJuHeCheng.afterId <= 0)
			{
				return;
			}
			int afterId = fuMoDaoJuHeCheng.afterId;
			EquipmentManager.Instance.SendDataCompositeReq(afterId, -1, DataCompositeReq.OpType.Enchanting);
		}
		else if (type == EquipComposeType.StarCompose)
		{
			ShengXingCaiLiaoHeCheng shengXingCaiLiaoHeCheng = DataReader<ShengXingCaiLiaoHeCheng>.Get(itemID);
			if (shengXingCaiLiaoHeCheng == null || shengXingCaiLiaoHeCheng.afterId <= 0)
			{
				return;
			}
			int afterId = shengXingCaiLiaoHeCheng.afterId;
			EquipmentManager.Instance.SendDataCompositeReq(afterId, -1, DataCompositeReq.OpType.RisingStar);
		}
	}

	public static bool IsCanCompose(int itemID, EquipComposeType type = EquipComposeType.EnchantmentCompose)
	{
		if (type == EquipComposeType.EnchantmentCompose)
		{
			if (EquipGlobal.IsEnchantmentItemMaxLv(itemID))
			{
				return false;
			}
			if (!EquipGlobal.IsEnchantmentComposeEnough(itemID))
			{
				return false;
			}
		}
		else if (type == EquipComposeType.StarCompose)
		{
			if (EquipGlobal.IsStarItemMaxLv(itemID))
			{
				return false;
			}
			if (!EquipGlobal.IsStarComposeEnough(itemID))
			{
				return false;
			}
		}
		return true;
	}

	public static string GetEnchantmentComposeTip(int itemID, EquipComposeType type = EquipComposeType.EnchantmentCompose)
	{
		if (type == EquipComposeType.EnchantmentCompose)
		{
			if (EquipGlobal.IsEnchantmentItemMaxLv(itemID))
			{
				return "无法合成";
			}
			if (!EquipGlobal.IsEnchantmentComposeEnough(itemID))
			{
				return "材料不足";
			}
		}
		else if (type == EquipComposeType.StarCompose)
		{
			if (EquipGlobal.IsStarItemMaxLv(itemID))
			{
				return "无法合成";
			}
			if (!EquipGlobal.IsStarComposeEnough(itemID))
			{
				return "材料不足";
			}
		}
		return string.Empty;
	}

	private static bool IsEnchantmentItemMaxLv(int itemID)
	{
		FuMoDaoJuHeCheng fuMoDaoJuHeCheng = DataReader<FuMoDaoJuHeCheng>.Get(itemID);
		return fuMoDaoJuHeCheng == null || fuMoDaoJuHeCheng.afterId <= 0;
	}

	private static bool IsEnchantmentComposeEnough(int itemID)
	{
		long num = BackpackManager.Instance.OnGetGoodCount(itemID);
		FuMoDaoJuHeCheng fuMoDaoJuHeCheng = DataReader<FuMoDaoJuHeCheng>.Get(itemID);
		if (fuMoDaoJuHeCheng == null || fuMoDaoJuHeCheng.afterId <= 0)
		{
			return false;
		}
		FuMoDaoJuHeCheng fuMoDaoJuHeCheng2 = DataReader<FuMoDaoJuHeCheng>.Get(fuMoDaoJuHeCheng.afterId);
		return fuMoDaoJuHeCheng2 != null && fuMoDaoJuHeCheng2.needId > 0 && fuMoDaoJuHeCheng2.needId == itemID && num >= (long)fuMoDaoJuHeCheng2.composeAmount;
	}

	private static bool IsStarItemMaxLv(int itemID)
	{
		ShengXingCaiLiaoHeCheng shengXingCaiLiaoHeCheng = DataReader<ShengXingCaiLiaoHeCheng>.Get(itemID);
		return shengXingCaiLiaoHeCheng == null || shengXingCaiLiaoHeCheng.afterId <= 0;
	}

	private static bool IsStarComposeEnough(int itemID)
	{
		long num = BackpackManager.Instance.OnGetGoodCount(itemID);
		ShengXingCaiLiaoHeCheng shengXingCaiLiaoHeCheng = DataReader<ShengXingCaiLiaoHeCheng>.Get(itemID);
		if (shengXingCaiLiaoHeCheng == null || shengXingCaiLiaoHeCheng.afterId <= 0)
		{
			return false;
		}
		ShengXingCaiLiaoHeCheng shengXingCaiLiaoHeCheng2 = DataReader<ShengXingCaiLiaoHeCheng>.Get(shengXingCaiLiaoHeCheng.afterId);
		return shengXingCaiLiaoHeCheng2 != null && shengXingCaiLiaoHeCheng2.needId > 0 && shengXingCaiLiaoHeCheng2.needId == itemID && num >= (long)shengXingCaiLiaoHeCheng2.composeAmount;
	}

	public static long GetAllEnchantmentAddValueByBaseAttrID(int attrID, int baseAttrValue, long equipID)
	{
		long num = 0L;
		EquipSimpleInfo equipSimpleInfo = null;
		if (EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			equipSimpleInfo = EquipmentManager.Instance.dicEquips.get_Item(equipID);
		}
		if (equipSimpleInfo == null)
		{
			return num;
		}
		for (int i = 0; i < equipSimpleInfo.enchantAttrs.get_Count(); i++)
		{
			if (equipSimpleInfo.enchantAttrs.get_Item(i).attrId > 0)
			{
				int attrId = equipSimpleInfo.enchantAttrs.get_Item(i).attrId;
				FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(attrId);
				if (fuMoDaoJuShuXing != null)
				{
					if (fuMoDaoJuShuXing.runeAttr == attrID)
					{
						if (fuMoDaoJuShuXing.Attrtype == 1)
						{
							if (fuMoDaoJuShuXing.valueType == 0)
							{
								num += (long)baseAttrValue * equipSimpleInfo.enchantAttrs.get_Item(i).value / 1000L;
							}
							else
							{
								num += equipSimpleInfo.enchantAttrs.get_Item(i).value;
							}
						}
					}
				}
			}
		}
		return num;
	}

	public static long GetEnchantmentAddValueByEnchantmentItemID(int itemID, long attrValue, long equipID)
	{
		long result = 0L;
		if (!EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			return result;
		}
		EquipSimpleInfo equipSimpleInfo = EquipmentManager.Instance.dicEquips.get_Item(equipID);
		int cfgId = equipSimpleInfo.cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return result;
		}
		Attrs attrs = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrBaseValue);
		if (attrs == null)
		{
			return result;
		}
		FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(itemID);
		if (fuMoDaoJuShuXing == null)
		{
			return result;
		}
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			if (fuMoDaoJuShuXing.runeAttr == attrs.attrs.get_Item(i))
			{
				if (fuMoDaoJuShuXing.valueType == 1)
				{
					return attrValue;
				}
				result = (long)attrs.values.get_Item(i) * attrValue / 1000L;
			}
		}
		return result;
	}

	public static Dictionary<string, string> GetEquipIconNamesByEquipDataID(int equipId, bool showItemColor = true)
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipId);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			Debug.LogError("data == null  equipID = " + equipId);
		}
		Items items = DataReader<Items>.Get(equipId);
		if (items == null)
		{
			Debug.LogError("item == null  itemID = " + equipId);
		}
		int color = items.color;
		string itemFrameName = GameDataUtils.GetItemFrameName(items.color);
		string qualityIconName = EquipGlobal.GetQualityIconName(zZhuangBeiPeiZhiBiao.quality);
		Icon icon = DataReader<Icon>.Get(items.icon);
		string text = string.Empty;
		if (icon != null)
		{
			text = icon.icon;
		}
		string itemName = GameDataUtils.GetItemName(items, showItemColor);
		string text2 = string.Format(GameDataUtils.GetChineseContent(505023, false), items.step);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("IconQualityName", qualityIconName);
		dictionary.Add("IconFrameName", itemFrameName);
		dictionary.Add("IconName", text);
		dictionary.Add("QualityLv", color.ToString());
		dictionary.Add("ItemName", itemName);
		dictionary.Add("EquipStep", text2);
		return dictionary;
	}

	public static int GetQualityLevelByEquipId(int equipId)
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipId);
		return EquipGlobal.GetQualityLevel(zZhuangBeiPeiZhiBiao.quality);
	}

	public static int GetQualityLevel(int quality)
	{
		int result;
		if (quality == 1)
		{
			result = 2;
		}
		else if (quality >= 2 && quality <= 4)
		{
			result = 3;
		}
		else if (quality >= 5 && quality <= 7)
		{
			result = 4;
		}
		else if (quality >= 8 && quality <= 10)
		{
			result = 5;
		}
		else if (quality >= 11 && quality <= 13)
		{
			result = 6;
		}
		else
		{
			result = 2;
		}
		return result;
	}

	public static string GetQualityIconName(int quality)
	{
		string result = string.Empty;
		switch (quality)
		{
		case 1:
			result = "point_green_D";
			break;
		case 2:
			result = "point_blue_C_minus";
			break;
		case 3:
			result = "point_blue_C";
			break;
		case 4:
			result = "point_blue_C_plus";
			break;
		case 5:
			result = "point_purple_B_minus";
			break;
		case 6:
			result = "point_purple_B";
			break;
		case 7:
			result = "point_purple_B_plus";
			break;
		case 8:
			result = "point_orange_A_minus";
			break;
		case 9:
			result = "point_orange_A";
			break;
		case 10:
			result = "point_orange_A_plus";
			break;
		case 11:
			result = "point_yellow_S_minus";
			break;
		case 12:
			result = "point_yellow_S";
			break;
		case 13:
			result = "point_yellow_S_plus";
			break;
		}
		return result;
	}

	public static Dictionary<string, string> GetIconNamesByEquipPos(EquipLibType.ELT part, bool showItemColor = true)
	{
		EquipLib equipLibInfo = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == part);
		if (equipLibInfo == null)
		{
			return null;
		}
		if (equipLibInfo.equips == null || equipLibInfo.equips.get_Count() <= 0 || equipLibInfo.wearingId <= 0L)
		{
			return null;
		}
		int cfgId = equipLibInfo.equips.Find((EquipSimpleInfo a) => a.equipId == equipLibInfo.wearingId).cfgId;
		if (cfgId <= 0)
		{
			return null;
		}
		return EquipGlobal.GetEquipIconNamesByEquipDataID(cfgId, showItemColor);
	}

	public static int GetSuitLv(int equipID)
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipID);
		int num = 1;
		int firstGroupId = zZhuangBeiPeiZhiBiao.firstGroupId;
		if (firstGroupId == 0)
		{
			return 0;
		}
		for (int i = 0; i < EquipmentManager.listWearingID.get_Count(); i++)
		{
			if (equipID != EquipmentManager.listWearingID.get_Item(i) && firstGroupId == DataReader<zZhuangBeiPeiZhiBiao>.Get(EquipmentManager.listWearingID.get_Item(i)).firstGroupId)
			{
				num++;
			}
		}
		int num2 = 0;
		zTaoZhuangPeiZhi zTaoZhuangPeiZhi = DataReader<zTaoZhuangPeiZhi>.Get(firstGroupId);
		if (zTaoZhuangPeiZhi == null)
		{
			Debug.LogError("zTaoZhuangPeiZhi 不存在 ID " + firstGroupId);
		}
		for (int j = 0; j < zTaoZhuangPeiZhi.startCondition.get_Count(); j++)
		{
			if (num >= zTaoZhuangPeiZhi.startCondition.get_Item(j))
			{
				num2++;
			}
		}
		return num2;
	}

	public static List<int> GetEquipmentProperties(int equipID, int strengThenLv)
	{
		List<int> list = new List<int>();
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipID);
		int suitLv = EquipGlobal.GetSuitLv(equipID);
		int firstGroupId = zZhuangBeiPeiZhiBiao.firstGroupId;
		zTaoZhuangPeiZhi zTaoZhuangPeiZhi = null;
		if (firstGroupId != 0)
		{
			zTaoZhuangPeiZhi = DataReader<zTaoZhuangPeiZhi>.Get(firstGroupId);
		}
		Attrs attrs = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrBaseValue);
		Attrs attrs2 = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrGrowValue);
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			int num = attrs.attrs.get_Item(i);
			float num2 = (float)attrs.values.get_Item(i);
			float num3 = 0f;
			if (attrs2 != null)
			{
				num3 = (float)(attrs2.values.get_Item(i) * strengThenLv);
			}
			if (firstGroupId != 0)
			{
				for (int j = 0; j < suitLv; j++)
				{
					if (zTaoZhuangPeiZhi.effectType.get_Item(j) != 1 || zTaoZhuangPeiZhi.effectId.get_Item(j) == num)
					{
					}
				}
			}
			float num4 = num2 + num3;
			list.Add((int)num4);
		}
		return list;
	}

	public static List<Goods> GetCanEnchantmentGoods(int partPos, int currentSlot)
	{
		List<Goods> list = new List<Goods>();
		List<Goods> enchantmentGoods = BackpackManager.Instance.EnchantmentGoods;
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)partPos);
		for (int i = 0; i < enchantmentGoods.get_Count(); i++)
		{
			Goods good = enchantmentGoods.get_Item(i);
			if (!list.Exists((Goods a) => a.GetItemId() == good.GetItemId()))
			{
				if (DataReader<FuMoDaoJuShuXing>.Contains(good.GetItemId()))
				{
					FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(good.GetItemId());
					if ((fuMoDaoJuShuXing.position != null && fuMoDaoJuShuXing.position.Contains(partPos)) || fuMoDaoJuShuXing.position.Contains(0))
					{
						list.Add(good);
					}
				}
			}
		}
		return list;
	}

	public static string GetCanEnchantmentPosDesc(int itemID)
	{
		string text = string.Empty;
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "无限定");
		dictionary.Add(1, GameDataUtils.GetChineseContent(14001, false));
		dictionary.Add(2, GameDataUtils.GetChineseContent(14002, false));
		dictionary.Add(3, GameDataUtils.GetChineseContent(14003, false));
		dictionary.Add(4, GameDataUtils.GetChineseContent(14004, false));
		dictionary.Add(5, GameDataUtils.GetChineseContent(14005, false));
		dictionary.Add(6, GameDataUtils.GetChineseContent(14006, false));
		dictionary.Add(7, GameDataUtils.GetChineseContent(14007, false));
		dictionary.Add(8, GameDataUtils.GetChineseContent(14008, false));
		dictionary.Add(9, GameDataUtils.GetChineseContent(14009, false));
		dictionary.Add(10, GameDataUtils.GetChineseContent(14010, false));
		if (DataReader<FuMoDaoJuShuXing>.Contains(itemID))
		{
			FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(itemID);
			for (int i = 0; i < fuMoDaoJuShuXing.position.get_Count(); i++)
			{
				int num = fuMoDaoJuShuXing.position.get_Item(i);
				if (dictionary.ContainsKey(num))
				{
					text += dictionary.get_Item(num);
					if (i < fuMoDaoJuShuXing.position.get_Count() - 1)
					{
						text += "、";
					}
				}
			}
		}
		return text;
	}

	public static string GetEquipOccupationName(int equipCfgId)
	{
		if (!DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgId))
		{
			return string.Empty;
		}
		string text = string.Empty;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgId);
		if (zZhuangBeiPeiZhiBiao.jobName != null)
		{
			for (int i = 0; i < zZhuangBeiPeiZhiBiao.jobName.get_Count(); i++)
			{
				int id = zZhuangBeiPeiZhiBiao.jobName.get_Item(i);
				text = GameDataUtils.GetChineseContent(id, false);
				if (i != zZhuangBeiPeiZhiBiao.jobName.get_Count() - 1)
				{
					text += "、";
				}
			}
		}
		if (EntityWorld.Instance.EntSelf.TypeID == zZhuangBeiPeiZhiBiao.occupation)
		{
			return text;
		}
		return TextColorMgr.GetColorByID(text, 1000007);
	}

	public static string GetExcellentTypeColor(float color)
	{
		string result = string.Empty;
		if (color == 0.5f)
		{
			result = "28c800";
		}
		else if (color == 0.6f || color == 0.7f)
		{
			result = "5ab9ff";
		}
		else if (color == 0.8f || color == 0.9f)
		{
			result = "ff73ff";
		}
		else
		{
			result = "ff7d4b";
		}
		return result;
	}

	public static string GetExcellentRangeText(int equipID, int attrID)
	{
		float num = 0f;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipID);
		if (zZhuangBeiPeiZhiBiao != null)
		{
			XPinZhiXiShu xPinZhiXiShu = DataReader<XPinZhiXiShu>.Get(zZhuangBeiPeiZhiBiao.quality);
			if (xPinZhiXiShu != null)
			{
				num = xPinZhiXiShu.qualityValue;
			}
		}
		float excellentCarrerValue = EquipGlobal.GetExcellentCarrerValue(attrID);
		float num2 = 0f;
		if (zZhuangBeiPeiZhiBiao != null)
		{
			XXiLianDengJiXiShu xXiLianDengJiXiShu = DataReader<XXiLianDengJiXiShu>.Get(zZhuangBeiPeiZhiBiao.step);
			if (xXiLianDengJiXiShu != null)
			{
				num2 = xXiLianDengJiXiShu.levelValue;
			}
		}
		XXiLianShuXingKu xXiLianShuXingKu = DataReader<XXiLianShuXingKu>.Get(attrID);
		if (xXiLianShuXingKu == null)
		{
			return string.Empty;
		}
		float num3 = xXiLianShuXingKu.normalRange.get_Item(0) * (float)xXiLianShuXingKu.maxValue * num * excellentCarrerValue * num2;
		float num4 = xXiLianShuXingKu.normalRange.get_Item(1) * (float)xXiLianShuXingKu.maxValue * num * excellentCarrerValue * num2;
		string empty = string.Empty;
		string attrValueDisplay = AttrUtility.GetAttrValueDisplay(attrID, (long)num3);
		string attrValueDisplay2 = AttrUtility.GetAttrValueDisplay(attrID, (long)num4);
		return string.Concat(new string[]
		{
			"（",
			attrValueDisplay,
			"-",
			attrValueDisplay2,
			"）"
		});
	}

	private static float GetExcellentCarrerValue(int attrID)
	{
		float result = 0f;
		XZhiYeXiShuYuShuXingDengJi xZhiYeXiShuYuShuXingDengJi = DataReader<XZhiYeXiShuYuShuXingDengJi>.Get(attrID);
		if (xZhiYeXiShuYuShuXingDengJi == null)
		{
			return result;
		}
		switch (EntityWorld.Instance.EntSelf.TypeID)
		{
		case 4:
			result = xZhiYeXiShuYuShuXingDengJi.kuangzhanshiJobValue;
			return result;
		case 7:
			result = xZhiYeXiShuYuShuXingDengJi.jixieshiJobValue;
			return result;
		case 8:
			result = xZhiYeXiShuYuShuXingDengJi.jianhaoJobValue;
			return result;
		}
		result = xZhiYeXiShuYuShuXingDengJi.kuangzhanshiJobValue;
		return result;
	}

	public static List<int> GetExcellentCheckList(int equipCfgID)
	{
		List<int> list = new List<int>();
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgID);
		if (zZhuangBeiPeiZhiBiao == null || EntityWorld.Instance.EntSelf == null)
		{
			return list;
		}
		int pos = 1;
		int job = 4;
		pos = zZhuangBeiPeiZhiBiao.position;
		int step = zZhuangBeiPeiZhiBiao.step;
		job = EntityWorld.Instance.EntSelf.TypeID;
		XBuWeiShuXingCanShu xBuWeiShuXingCanShu = DataReader<XBuWeiShuXingCanShu>.DataList.Find((XBuWeiShuXingCanShu a) => a.part == pos && a.job == job);
		if (xBuWeiShuXingCanShu == null)
		{
			return list;
		}
		List<int> attrid = xBuWeiShuXingCanShu.attrid;
		for (int i = 0; i < attrid.get_Count(); i++)
		{
			int num = attrid.get_Item(i);
			XZhiYeXiShuYuShuXingDengJi xZhiYeXiShuYuShuXingDengJi = DataReader<XZhiYeXiShuYuShuXingDengJi>.Get(num);
			XXiLianShuXingKu xXiLianShuXingKu = DataReader<XXiLianShuXingKu>.Get(num);
			if (num > 0 && xZhiYeXiShuYuShuXingDengJi != null && step >= xZhiYeXiShuYuShuXingDengJi.level && xXiLianShuXingKu != null && xXiLianShuXingKu.weight > 0)
			{
				list.Add(num);
			}
		}
		return list;
	}

	public static int GetExcellentAttrsCountByColor(long equipID, float minColorValue = 1f)
	{
		int num = 0;
		if (EquipmentManager.Instance.dicEquips != null && EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			EquipSimpleInfo equipSimpleInfo = EquipmentManager.Instance.dicEquips.get_Item(equipID);
			if (equipSimpleInfo != null)
			{
				for (int i = 0; i < equipSimpleInfo.excellentAttrs.get_Count(); i++)
				{
					if (equipSimpleInfo.excellentAttrs.get_Item(i).color >= minColorValue)
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public static void GetEquipRecommendItem(long equipID, int pos, Transform trans, bool showHaveCount = false)
	{
		if (!EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			return;
		}
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("EquipRecommendItem");
		instantiate2Prefab.get_transform().SetParent(trans);
		instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
		instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(new Vector3(0f, 0f, 0f));
		instantiate2Prefab.SetActive(true);
		instantiate2Prefab.set_name("Equip_" + pos);
		int itemID = EquipmentManager.Instance.dicEquips.get_Item(equipID).cfgId;
		EquipSimpleInfo equipData = EquipmentManager.Instance.dicEquips.get_Item(equipID);
		zZhuangBeiPeiZhiBiao data = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipData.cfgId);
		Items items = DataReader<Items>.Get(equipData.cfgId);
		int equipCfgIDByPos = EquipGlobal.GetEquipCfgIDByPos((EquipLibType.ELT)pos);
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgIDByPos);
		Items items2 = DataReader<Items>.Get(equipCfgIDByPos);
		if (data != null && items != null)
		{
			int num = (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("checkLv").value);
			bool flag = false;
			if (num <= EntityWorld.Instance.EntSelf.Lv)
			{
				if (data.step > zZhuangBeiPeiZhiBiao.step && items.color >= 4)
				{
					flag = true;
				}
				else if (data.step == zZhuangBeiPeiZhiBiao.step && items.color >= items2.color && items.color >= 4)
				{
					flag = true;
				}
			}
			if (flag)
			{
				int num2 = 0;
				if (DataReader<zZhuangBeiSheZhi>.Contains("checkNum" + items.color))
				{
					num2 = (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("checkNum" + items.color).value);
				}
				int wearingEquipNumByMinColor = EquipGlobal.GetWearingEquipNumByMinColor(items.color);
				if (num2 <= wearingEquipNumByMinColor)
				{
					flag = true;
				}
				if (!flag)
				{
					int num3 = (int)float.Parse(DataReader<zZhuangBeiSheZhi>.Get("rankNum").value);
					int wearingEquipNumByMinStep = EquipGlobal.GetWearingEquipNumByMinStep(data.step);
					if (num3 <= wearingEquipNumByMinStep)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				instantiate2Prefab.GetComponent<EquipRecommendItem>().UpdateUIData(itemID, "您获得新装备", "查 看", delegate
				{
					LinkNavigationManager.OpenActorUI(delegate
					{
						UIManagerControl.Instance.OpenUI("EquipDetailedPopUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
						EquipDetailedPopUI.Instance.SetSelectEquipTip((EquipLibType.ELT)pos, false);
					});
				}, false, null, 2000);
			}
			else
			{
				instantiate2Prefab.GetComponent<EquipRecommendItem>().UpdateUIData(itemID, "您获得新装备", "替 换", delegate
				{
					if (data.level > EntityWorld.Instance.EntSelf.Lv)
					{
						string text = GameDataUtils.GetChineseContent(510113, false);
						text = text.Replace("{s1}", data.level.ToString());
						UIManagerControl.Instance.ShowToastText(text);
						return;
					}
					EquipmentManager.Instance.SendPutOnEquipmentReq(data.position, equipData.equipId, itemID);
				}, false, null, 2000);
			}
			instantiate2Prefab.GetComponent<EquipRecommendItem>().FightingContent = EquipmentManager.Instance.GetEquipFightingByEquipID(equipID).ToString();
		}
	}

	public static List<KeyValuePair<EquipLibType.ELT, long>> GetEquipRecommendList(Dictionary<EquipLibType.ELT, long> equipDic)
	{
		List<KeyValuePair<EquipLibType.ELT, long>> list = new List<KeyValuePair<EquipLibType.ELT, long>>();
		if (equipDic == null)
		{
			return list;
		}
		using (Dictionary<EquipLibType.ELT, long>.Enumerator enumerator = EquipmentManager.Instance.RecommendDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<EquipLibType.ELT, long> current = enumerator.get_Current();
				list.Add(current);
			}
		}
		return list;
	}

	public static int GetWearingEquipNumByMinColor(int color)
	{
		int num = 0;
		for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
		{
			if (EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).type != EquipLibType.ELT.Experience)
			{
				long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).wearingId;
				int equipCfgIDByEquipID = EquipGlobal.GetEquipCfgIDByEquipID(wearingId);
				if (DataReader<Items>.Contains(equipCfgIDByEquipID))
				{
					Items items = DataReader<Items>.Get(equipCfgIDByEquipID);
					if (items.color >= color)
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public static int GetWearingEquipNumByMinStep(int step)
	{
		int num = 0;
		for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
		{
			if (EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).type != EquipLibType.ELT.Experience)
			{
				long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).wearingId;
				zZhuangBeiPeiZhiBiao equipCfgDataByEquipID = EquipGlobal.GetEquipCfgDataByEquipID(wearingId);
				if (equipCfgDataByEquipID != null && equipCfgDataByEquipID.step >= step)
				{
					num++;
				}
			}
		}
		return num;
	}

	public static bool IsContainHighLevel(List<long> list_equip_uuid)
	{
		for (int i = 0; i < list_equip_uuid.get_Count(); i++)
		{
			if (EquipGlobal.IsContainHighLevel(list_equip_uuid.get_Item(i)))
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsContainHighLevel(long equip_uuid)
	{
		int num = BackpackManager.Instance.OnGetGoodItemId(equip_uuid);
		if (num > 0)
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(num);
			if (zZhuangBeiPeiZhiBiao != null)
			{
				return zZhuangBeiPeiZhiBiao.level > EntityWorld.Instance.EntSelf.Lv;
			}
		}
		return false;
	}

	public static long GetStrengthEquipFightting(EquipLibType.ELT pos)
	{
		long result = 0L;
		zZhuangBeiPeiZhiBiao equipCfgDataByPos = EquipGlobal.GetEquipCfgDataByPos(pos);
		if (equipCfgDataByPos != null && DataReader<Attrs>.Contains(equipCfgDataByPos.attrGrowValue))
		{
			Attrs attrs = DataReader<Attrs>.Get(equipCfgDataByPos.attrGrowValue);
			return EquipGlobal.CalculateFightingByIDAndValue(attrs.attrs, attrs.values);
		}
		return result;
	}

	public static long GetAllEquipAttrValue()
	{
		long num = 0L;
		for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
		{
			EquipLibType.ELT type = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).type;
			if (type != EquipLibType.ELT.Experience)
			{
				long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).wearingId;
				if (EquipmentManager.Instance.dicEquips.ContainsKey(wearingId))
				{
					num += (long)EquipmentManager.Instance.GetEquipFightingByEquipID(wearingId);
				}
			}
		}
		return num;
	}

	public static long GetAllEquipDevelopAttrValue()
	{
		long num = 0L;
		for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
		{
			EquipLibType.ELT type = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).type;
			if (type != EquipLibType.ELT.Experience)
			{
				long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).wearingId;
				if (EquipmentManager.Instance.dicEquips.ContainsKey(wearingId))
				{
					int cfgId = EquipmentManager.Instance.dicEquips.get_Item(wearingId).cfgId;
					zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
					Attrs attrs = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrGrowValue);
					int lv = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).lv;
					if (attrs != null)
					{
						List<int> list = new List<int>();
						for (int j = 0; j < attrs.values.get_Count(); j++)
						{
							list.Add(attrs.values.get_Item(j) * lv);
						}
						num += EquipGlobal.CalculateFightingByIDAndValue(attrs.attrs, list);
					}
				}
			}
		}
		return num;
	}

	public static long GetAllEquipStarUpAttrValue()
	{
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
		{
			EquipLibType.ELT type = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).type;
			if (type != EquipLibType.ELT.Experience)
			{
				long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).wearingId;
				if (EquipmentManager.Instance.dicEquips.ContainsKey(wearingId))
				{
					EquipSimpleInfo equipSimpleInfo = EquipmentManager.Instance.dicEquips.get_Item(wearingId);
					if (equipSimpleInfo.starAttrs != null)
					{
						for (int j = 0; j < equipSimpleInfo.starAttrs.get_Count(); j++)
						{
							int attrId = equipSimpleInfo.starAttrs.get_Item(j).attrId;
							if (attrId > 0)
							{
								list.Add(attrId);
								list2.Add(equipSimpleInfo.starAttrs.get_Item(j).value);
							}
						}
					}
				}
			}
		}
		return EquipGlobal.CalculateFightingByIDAndValue(list, list2);
	}

	public static long GetAllEquipEnchantmentAttrValue()
	{
		long num = 0L;
		for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
		{
			EquipLibType.ELT type = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).type;
			if (type != EquipLibType.ELT.Experience)
			{
				long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i).wearingId;
				if (EquipmentManager.Instance.dicEquips.ContainsKey(wearingId))
				{
					EquipSimpleInfo equipSimpleInfo = EquipmentManager.Instance.dicEquips.get_Item(wearingId);
					if (equipSimpleInfo.enchantAttrs != null)
					{
						for (int j = 0; j < equipSimpleInfo.enchantAttrs.get_Count(); j++)
						{
							int attrId = equipSimpleInfo.enchantAttrs.get_Item(j).attrId;
							if (attrId > 0)
							{
								FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(attrId);
								if (fuMoDaoJuShuXing != null)
								{
									ZhanDouLiBiaoZhun zhanDouLiBiaoZhun = DataReader<ZhanDouLiBiaoZhun>.Get(fuMoDaoJuShuXing.runeAttr);
									if (zhanDouLiBiaoZhun != null)
									{
										if (fuMoDaoJuShuXing.Attrtype == 1)
										{
											long enchantmentAddValueByEnchantmentItemID = EquipGlobal.GetEnchantmentAddValueByEnchantmentItemID(attrId, equipSimpleInfo.enchantAttrs.get_Item(j).value, wearingId);
											num += (long)((float)enchantmentAddValueByEnchantmentItemID * zhanDouLiBiaoZhun.unit * zhanDouLiBiaoZhun.unitFightPower);
										}
										else
										{
											num += (long)((float)equipSimpleInfo.enchantAttrs.get_Item(j).value * zhanDouLiBiaoZhun.unit * zhanDouLiBiaoZhun.unitFightPower);
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return num;
	}

	public static long CalculateFightingByIDAndValue(List<int> attrIds, List<long> attrValues)
	{
		float num = 0f;
		if (attrIds == null || attrValues == null)
		{
			return 0L;
		}
		for (int i = 0; i < attrIds.get_Count(); i++)
		{
			int key = attrIds.get_Item(i);
			if (i >= attrValues.get_Count())
			{
				break;
			}
			long num2 = attrValues.get_Item(i);
			ZhanDouLiBiaoZhun zhanDouLiBiaoZhun = DataReader<ZhanDouLiBiaoZhun>.Get(key);
			if (zhanDouLiBiaoZhun != null)
			{
				num += (float)num2 * zhanDouLiBiaoZhun.unit * zhanDouLiBiaoZhun.unitFightPower;
			}
		}
		return (long)num;
	}

	public static long CalculateFightingByIDAndValue(List<int> attrIds, List<int> attrValues)
	{
		float num = 0f;
		if (attrIds == null || attrValues == null)
		{
			return 0L;
		}
		for (int i = 0; i < attrIds.get_Count(); i++)
		{
			int key = attrIds.get_Item(i);
			if (i >= attrValues.get_Count())
			{
				break;
			}
			long num2 = (long)attrValues.get_Item(i);
			ZhanDouLiBiaoZhun zhanDouLiBiaoZhun = DataReader<ZhanDouLiBiaoZhun>.Get(key);
			if (zhanDouLiBiaoZhun != null)
			{
				num += (float)num2 * zhanDouLiBiaoZhun.unit * zhanDouLiBiaoZhun.unitFightPower;
			}
		}
		return (long)num;
	}

	public static EquipSimpleInfo GetWearingEquipSimpleInfoByPos(EquipLibType.ELT pos)
	{
		EquipSimpleInfo result = null;
		int num = EquipmentManager.Instance.equipmentData.equipLibs.FindIndex((EquipLib a) => a.type == pos);
		if (num >= 0)
		{
			long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(num).wearingId;
			if (EquipmentManager.Instance.dicEquips.ContainsKey(wearingId))
			{
				return EquipmentManager.Instance.dicEquips.get_Item(wearingId);
			}
		}
		return result;
	}

	public static zZhuangBeiPeiZhiBiao GetEquipCfgDataByPos(EquipLibType.ELT pos)
	{
		zZhuangBeiPeiZhiBiao result = null;
		int num = EquipmentManager.Instance.equipmentData.equipLibs.FindIndex((EquipLib a) => a.type == pos);
		if (num >= 0)
		{
			long wearingId = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(num).wearingId;
			if (EquipmentManager.Instance.dicEquips.ContainsKey(wearingId))
			{
				int cfgId = EquipmentManager.Instance.dicEquips.get_Item(wearingId).cfgId;
				return DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
			}
		}
		return result;
	}

	public static EquipSimpleInfo GetEquipSimpleInfoByEquipID(long equipID)
	{
		EquipSimpleInfo result = null;
		if (EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			result = EquipmentManager.Instance.dicEquips.get_Item(equipID);
		}
		return result;
	}

	public static zZhuangBeiPeiZhiBiao GetEquipCfgDataByEquipID(long equipID)
	{
		zZhuangBeiPeiZhiBiao result = null;
		if (EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			int cfgId = EquipmentManager.Instance.dicEquips.get_Item(equipID).cfgId;
			return DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		}
		return result;
	}

	public static int GetEquipCfgIDByEquipID(long equipID)
	{
		int result = 0;
		if (EquipmentManager.Instance.dicEquips != null && EquipmentManager.Instance.dicEquips.ContainsKey(equipID))
		{
			result = EquipmentManager.Instance.dicEquips.get_Item(equipID).cfgId;
		}
		return result;
	}

	public static int GetEquipCfgIDByPos(EquipLibType.ELT pos)
	{
		if (EquipmentManager.Instance.equipmentData == null || EquipmentManager.Instance.equipmentData.equipLibs == null)
		{
			return 0;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null || !EquipmentManager.Instance.dicEquips.ContainsKey(equipLib.wearingId))
		{
			return 0;
		}
		return EquipGlobal.GetEquipCfgIDByEquipID(equipLib.wearingId);
	}

	public static bool IsWearing(EquipLibType.ELT type)
	{
		return EquipGlobal.GetWearingEquipSimpleInfoByPos(type) != null;
	}

	public static bool CheckCanShowEquipModel(int equipCfgID)
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgID);
		return zZhuangBeiPeiZhiBiao != null && zZhuangBeiPeiZhiBiao.model > 0 && DataReader<EquipBody>.Get(zZhuangBeiPeiZhiBiao.model) != null;
	}

	public static bool CheckCanShowEquipFX(int equipCfgID)
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgID);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return false;
		}
		if (zZhuangBeiPeiZhiBiao.model <= 0)
		{
			return false;
		}
		EquipBody equipBody = DataReader<EquipBody>.Get(zZhuangBeiPeiZhiBiao.model);
		if (equipBody != null)
		{
			string specialEfficiency = equipBody.specialEfficiency;
			if (equipBody.specialEfficiency == string.Empty)
			{
				return false;
			}
			string[] array = specialEfficiency.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string[] array2 = text.Split(new char[]
				{
					','
				});
				int num = (int)float.Parse(array2[0]);
				Items items = DataReader<Items>.Get(equipCfgID);
				if (items == null)
				{
					return false;
				}
				if (items.color == num)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static int GetEquipModelFXID(int equipCfgID, int gogokNum = 0)
	{
		if (!DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgID))
		{
			return -1;
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgID);
		if (zZhuangBeiPeiZhiBiao.model <= 0)
		{
			return -1;
		}
		if (!DataReader<EquipBody>.Contains(zZhuangBeiPeiZhiBiao.model))
		{
			return -1;
		}
		EquipBody equipBody = DataReader<EquipBody>.Get(zZhuangBeiPeiZhiBiao.model);
		string specialEfficiency = equipBody.specialEfficiency;
		if (equipBody.specialEfficiency == string.Empty)
		{
			return -1;
		}
		string[] array = specialEfficiency.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			string[] array2 = text.Split(new char[]
			{
				','
			});
			int num = (int)float.Parse(array2[0]);
			int result = (int)float.Parse(array2[1]);
			Items items = DataReader<Items>.Get(equipCfgID);
			if (items == null)
			{
				return 0;
			}
			if (items.color == num && EquipGlobal.GetShowEquipModelFXNeedGogok(items.color) <= gogokNum)
			{
				return result;
			}
		}
		return 0;
	}

	public static int GetEquipModelFXID2(int equipCfgID, int gogokNum = 0)
	{
		if (!DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgID))
		{
			return -1;
		}
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgID);
		if (zZhuangBeiPeiZhiBiao.model <= 0)
		{
			return -1;
		}
		if (!DataReader<EquipBody>.Contains(zZhuangBeiPeiZhiBiao.model))
		{
			return -1;
		}
		EquipBody equipBody = DataReader<EquipBody>.Get(zZhuangBeiPeiZhiBiao.model);
		string specialEfficiency = equipBody.specialEfficiency2;
		if (equipBody.specialEfficiency2 == string.Empty)
		{
			return -1;
		}
		string[] array = specialEfficiency.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			string[] array2 = text.Split(new char[]
			{
				','
			});
			int num = (int)float.Parse(array2[0]);
			int result = (int)float.Parse(array2[1]);
			Items items = DataReader<Items>.Get(equipCfgID);
			if (items == null)
			{
				return 0;
			}
			if (items.color == num && EquipGlobal.GetShowEquipModelFXNeedGogok(items.color) <= gogokNum)
			{
				return result;
			}
		}
		return 0;
	}

	public static int GetShowEquipModelFXNeedGogok(int quality)
	{
		int result = 0;
		if (quality == 4)
		{
			return 1;
		}
		if (quality == 5)
		{
			return 2;
		}
		if (quality == 6)
		{
			return 3;
		}
		return result;
	}

	public static int GetEquipIconFX(int itemCfgId, int gogokNum, Transform fxParentTrans, string uibase, int depthValue = 2000, bool stencilMask = false)
	{
		int result = 0;
		if (fxParentTrans != null)
		{
			for (int i = fxParentTrans.get_childCount() - 1; i >= 0; i--)
			{
				Transform child = fxParentTrans.GetChild(i);
				if (child != null)
				{
					child.get_gameObject().SetActive(false);
					Object.Destroy(child.get_gameObject(), 0.04f);
				}
			}
			fxParentTrans.DetachChildren();
			Items items = DataReader<Items>.Get(itemCfgId);
			if (items == null)
			{
				return result;
			}
			if (items.tab != 2)
			{
				return result;
			}
			if (gogokNum < 1)
			{
				return result;
			}
			if (items.color == 5)
			{
				int templateId = (159 + gogokNum - 1 >= 159) ? (159 + gogokNum - 1) : 159;
				result = FXSpineManager.Instance.PlaySpine(templateId, fxParentTrans, uibase, depthValue, null, "UI", 0f, 0f, 1f, 1f, stencilMask, FXMaskLayer.MaskState.None);
			}
			else if (items.color == 6)
			{
				int templateId = (161 + gogokNum - 1 >= 161) ? (161 + gogokNum - 1) : 161;
				result = FXSpineManager.Instance.PlaySpine(templateId, fxParentTrans, uibase, depthValue, null, "UI", 0f, 0f, 1f, 1f, stencilMask, FXMaskLayer.MaskState.None);
			}
			else if (items.color == 4)
			{
				int templateId = 158;
				result = FXSpineManager.Instance.PlaySpine(templateId, fxParentTrans, uibase, depthValue, null, "UI", 0f, 0f, 1f, 1f, stencilMask, FXMaskLayer.MaskState.None);
			}
		}
		return result;
	}

	public static List<EquipSimpleInfo> GetNPCShopEquipsData(int step, int quality, int pos, int gogok = 0)
	{
		List<EquipSimpleInfo> list = new List<EquipSimpleInfo>();
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)pos);
		if (equipLib != null)
		{
			for (int i = 0; i < equipLib.equips.get_Count(); i++)
			{
				EquipSimpleInfo equipSimpleInfo = equipLib.equips.get_Item(i);
				if (equipSimpleInfo != null)
				{
					if (DataReader<Items>.Contains(equipSimpleInfo.cfgId))
					{
						Items items = DataReader<Items>.Get(equipSimpleInfo.cfgId);
						if (items.step == step && items.color == quality)
						{
							int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(equipSimpleInfo.equipId, 1f);
							if (excellentAttrsCountByColor == EquipGlobal.GetGogokCfgData(quality, gogok))
							{
								list.Add(equipSimpleInfo);
							}
						}
					}
				}
			}
		}
		return list;
	}

	public static int GetGogokCfgData(int quality, int gogok)
	{
		int num = gogok;
		if (quality == 4)
		{
			num = 1;
		}
		else if (quality == 5)
		{
			num = 2;
		}
		else if (quality >= 6)
		{
			num = 3;
		}
		gogok = ((gogok <= num) ? gogok : 0);
		return gogok;
	}

	public static bool CheckEquipCanForging(EquipLibType.ELT type)
	{
		EquipSimpleInfo wearingEquipSimpleInfoByPos = EquipGlobal.GetWearingEquipSimpleInfoByPos(type);
		if (wearingEquipSimpleInfoByPos != null)
		{
			int cfgId = wearingEquipSimpleInfoByPos.cfgId;
			int num = 0;
			int num2 = 0;
			if (DataReader<Items>.Contains(cfgId))
			{
				Items items = DataReader<Items>.Get(cfgId);
				num2 = items.color;
			}
			if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(cfgId))
			{
				zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
				num = zZhuangBeiPeiZhiBiao.step;
			}
			int excellentAttrsCountByColor = EquipGlobal.GetExcellentAttrsCountByColor(wearingEquipSimpleInfoByPos.equipId, 1f);
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			if (DataReader<TaoZhuangXiangGuanXiShu>.Contains("stepLimit"))
			{
				num3 = DataReader<TaoZhuangXiangGuanXiShu>.Get("stepLimit").value;
			}
			if (DataReader<TaoZhuangXiangGuanXiShu>.Contains("QualityLimit"))
			{
				num4 = DataReader<TaoZhuangXiangGuanXiShu>.Get("QualityLimit").value;
			}
			if (DataReader<TaoZhuangXiangGuanXiShu>.Contains("magaNumLimit"))
			{
				num5 = DataReader<TaoZhuangXiangGuanXiShu>.Get("magaNumLimit").value;
			}
			if (num2 >= num4 && num >= num3 && excellentAttrsCountByColor >= num5)
			{
				return true;
			}
		}
		return false;
	}

	public static TaoZhuangDuanZhu GetEquipForgeCfgData(long equipID)
	{
		TaoZhuangDuanZhu result = null;
		EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(equipID);
		if (equipSimpleInfoByEquipID == null)
		{
			return result;
		}
		int cfgId = equipSimpleInfoByEquipID.cfgId;
		int step = 0;
		int quality = 0;
		if (DataReader<Items>.Contains(cfgId))
		{
			Items items = DataReader<Items>.Get(cfgId);
			quality = items.color;
		}
		if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(cfgId))
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
			step = zZhuangBeiPeiZhiBiao.step;
		}
		int gogokNum = EquipGlobal.GetExcellentAttrsCountByColor(equipSimpleInfoByEquipID.equipId, 1f);
		List<TaoZhuangDuanZhu> list = DataReader<TaoZhuangDuanZhu>.DataList.FindAll((TaoZhuangDuanZhu a) => a.suitStep == step && quality == a.suitQuality);
		if (list == null || list.get_Count() <= 0)
		{
			return result;
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			TaoZhuangDuanZhu taoZhuangDuanZhu = list.get_Item(i);
			int num = taoZhuangDuanZhu.suitmagaNum.FindIndex((int a) => a == gogokNum);
			if (num >= 0)
			{
				return taoZhuangDuanZhu;
			}
		}
		return result;
	}

	public static bool CheckForgeMaterialIsEnough(long equipID)
	{
		TaoZhuangDuanZhu equipForgeCfgData = EquipGlobal.GetEquipForgeCfgData(equipID);
		if (equipForgeCfgData != null)
		{
			for (int i = 0; i < equipForgeCfgData.suitcost.get_Count(); i++)
			{
				int key = equipForgeCfgData.suitcost.get_Item(i).key;
				int value = equipForgeCfgData.suitcost.get_Item(i).value;
				if (BackpackManager.Instance.OnGetGoodCount(key) < (long)value)
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public static List<TaoZhuangDuanZhu> GetEquipSuitAttrList(EquipLibType.ELT pos)
	{
		List<TaoZhuangDuanZhu> list = new List<TaoZhuangDuanZhu>();
		zZhuangBeiPeiZhiBiao equipCfgDataByPos = EquipGlobal.GetEquipCfgDataByPos(pos);
		if (equipCfgDataByPos != null)
		{
			int step = equipCfgDataByPos.step;
			int quality = 0;
			Items items = DataReader<Items>.Get(equipCfgDataByPos.id);
			if (items != null)
			{
				quality = items.color;
			}
			List<TaoZhuangDuanZhu> list2 = DataReader<TaoZhuangDuanZhu>.DataList.FindAll((TaoZhuangDuanZhu a) => a.suitStep == step && a.suitQuality == quality);
			if (list2 != null)
			{
				list.AddRange(list2);
			}
		}
		return list;
	}

	public static List<EquipLibType.ELT> GetCanForgeEquipPosList(bool isSelectHigh = false)
	{
		List<EquipLibType.ELT> list = new List<EquipLibType.ELT>();
		if (EquipmentManager.Instance.equipmentData != null && EquipmentManager.Instance.equipmentData.equipLibs != null)
		{
			for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
			{
				EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i);
				if (equipLib != null && equipLib.type != EquipLibType.ELT.Experience)
				{
					int equipCfgIDByEquipID = EquipGlobal.GetEquipCfgIDByEquipID(equipLib.wearingId);
					int num = 0;
					if (DataReader<Items>.Contains(equipCfgIDByEquipID))
					{
						Items items = DataReader<Items>.Get(equipCfgIDByEquipID);
						num = items.color;
					}
					if (((isSelectHigh && num >= 6) || (!isSelectHigh && num == 5)) && EquipGlobal.CheckEquipCanForging(equipLib.type))
					{
						list.Add(equipLib.type);
					}
				}
			}
		}
		return list;
	}

	public static string GetEquipSuitMarkName(int suitID)
	{
		TaoZhuangDuanZhu taoZhuangDuanZhu = DataReader<TaoZhuangDuanZhu>.DataList.Find((TaoZhuangDuanZhu a) => a.suitId == suitID);
		if (taoZhuangDuanZhu != null)
		{
			return "[" + GameDataUtils.GetChineseContent(taoZhuangDuanZhu.suitMark, false) + "]";
		}
		return string.Empty;
	}

	public static int GetEquipSuitNumByID(int suitID)
	{
		int num = 0;
		if (EquipmentManager.Instance.equipmentData != null && EquipmentManager.Instance.equipmentData.equipLibs != null)
		{
			for (int i = 0; i < EquipmentManager.Instance.equipmentData.equipLibs.get_Count(); i++)
			{
				EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.get_Item(i);
				if (equipLib.type != EquipLibType.ELT.Experience)
				{
					EquipSimpleInfo equipSimpleInfoByEquipID = EquipGlobal.GetEquipSimpleInfoByEquipID(equipLib.wearingId);
					if (equipSimpleInfoByEquipID.suitId == suitID)
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public static bool CheckSuitIsActive(int suitCfgID)
	{
		if (EquipmentManager.Instance.EquipSuitActiveIndexIds != null)
		{
			int num = EquipmentManager.Instance.EquipSuitActiveIndexIds.FindIndex((int a) => a == suitCfgID);
			if (num >= 0)
			{
				return true;
			}
		}
		return false;
	}

	public static bool CheckLowSuitIsActive(int suitCfgID)
	{
		if (!DataReader<TaoZhuangDuanZhu>.Contains(suitCfgID))
		{
			return false;
		}
		TaoZhuangDuanZhu taoZhuangDuanZhu = DataReader<TaoZhuangDuanZhu>.Get(suitCfgID);
		if (taoZhuangDuanZhu.suitQuality == 6)
		{
			return false;
		}
		int suitQuality = taoZhuangDuanZhu.suitQuality;
		int suitID = taoZhuangDuanZhu.suitId;
		int suitStep = taoZhuangDuanZhu.suitStep;
		int suitNeedNum = taoZhuangDuanZhu.suitNeedNum;
		if (DataReader<TaoZhuangDuanZhu>.DataList == null || DataReader<TaoZhuangDuanZhu>.DataList.get_Count() <= 0)
		{
			return false;
		}
		int num = DataReader<TaoZhuangDuanZhu>.DataList.FindIndex((TaoZhuangDuanZhu a) => a.suitNeedNum == suitNeedNum && a.suitStep == suitStep && a.suitId != suitID && a.suitQuality != suitQuality);
		if (num >= 0)
		{
			TaoZhuangDuanZhu taoZhuangDuanZhu2 = DataReader<TaoZhuangDuanZhu>.DataList.get_Item(num);
			return EquipGlobal.CheckSuitIsActive(taoZhuangDuanZhu2.id);
		}
		return false;
	}

	public static int GetEquipSuitMaxNum(int suitID)
	{
		int num = 0;
		List<TaoZhuangDuanZhu> list = DataReader<TaoZhuangDuanZhu>.DataList.FindAll((TaoZhuangDuanZhu a) => a.suitId == suitID);
		if (list != null && list.get_Count() > 0)
		{
			for (int i = 0; i < list.get_Count(); i++)
			{
				if (list.get_Item(i).suitNeedNum > num)
				{
					num = list.get_Item(i).suitNeedNum;
				}
			}
		}
		return num;
	}
}
