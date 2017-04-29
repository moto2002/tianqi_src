using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDataUtils
{
	public static string DEFAULT_CHINESE = "缺少数据配置,敬请期待";

	private static Color textColor1 = new Color(0.156862751f, 0.784313738f, 0f, 1f);

	private static Color textOutlineColor1 = new Color(0.294117659f, 0.09803922f, 0f, 1f);

	private static Color textColor2 = new Color(0.3529412f, 0.7254902f, 1f, 1f);

	private static Color textOutlineColor2 = new Color(0.294117659f, 0.09803922f, 0f, 1f);

	private static Color textColor3 = new Color(1f, 0.4509804f, 1f, 1f);

	private static Color textOutlineColor3 = new Color(0.294117659f, 0.09803922f, 0f, 1f);

	private static Color textColor4 = new Color(1f, 0.490196079f, 0.294117659f, 1f);

	private static Color textOutlineColor4 = new Color(0.294117659f, 0.09803922f, 0f, 1f);

	private static Color textColor5 = new Color(1f, 0.921568632f, 0.294117659f, 1f);

	private static Color textOutlineColor5 = new Color(0.294117659f, 0.09803922f, 0f, 1f);

	private static string Suffix_Dot0 = ".0";

	public static string GetChineseContent(int _id, bool canNull = false)
	{
		ChineseData chineseData = DataReader<ChineseData>.Get(_id);
		if (chineseData != null)
		{
			return chineseData.content.Replace("\\n", "\n").Replace("\\t", "\t");
		}
		Debug.LogError("GameData.ChineseData no exist, id:" + _id);
		return (!canNull) ? GameDataUtils.DEFAULT_CHINESE : string.Empty;
	}

	public static string GetNoticeText(int _id)
	{
		Noticetext noticetext = DataReader<Noticetext>.Get(_id);
		return (noticetext == null) ? GameDataUtils.DEFAULT_CHINESE : noticetext.content;
	}

	public static bool IsDefaultChinese(string text)
	{
		return text.Equals(GameDataUtils.DEFAULT_CHINESE);
	}

	public static SpriteRenderer GetIcon(int _id)
	{
		Icon icon = DataReader<Icon>.Get(_id);
		if (icon != null && !string.IsNullOrEmpty(icon.icon))
		{
			return ResourceManager.GetIconSprite(GameDataUtils.SplitString4Dot0(icon.icon));
		}
		return ResourceManager.GetIconSprite("99999");
	}

	public static string GetIconName(int _id)
	{
		Icon icon = DataReader<Icon>.Get(_id);
		if (icon != null)
		{
			return icon.icon;
		}
		if (_id > 0)
		{
			Debug.LogError("============>GameData.Icon no key = " + _id);
		}
		return "99999";
	}

	public static string GetTexture(int _id)
	{
		Icon icon = DataReader<Icon>.Get(_id);
		if (icon != null)
		{
			return icon.icon;
		}
		return string.Empty;
	}

	private static SpriteRenderer GetNumIcon(int num, NumType type)
	{
		if (type == NumType.Yellow_light)
		{
			return ResourceManager.GetIconSprite("new_z_vip_" + num);
		}
		if (type == NumType.Yellow)
		{
			return ResourceManager.GetIconSprite("font_vip_" + num);
		}
		if (type == NumType.Yellow_big)
		{
			return ResourceManager.GetIconSprite("ts_" + num);
		}
		if (type == NumType.jn)
		{
			return ResourceManager.GetIconSprite("jn_" + num);
		}
		if (type == NumType.EquipStarUp)
		{
			return ResourceManager.GetIconSprite("sx" + num);
		}
		if (type == NumType.Fight_time)
		{
			return ResourceManager.GetIconSprite("fight_time_" + num);
		}
		return ResourceManager.GetIconSprite("font_vip_" + num);
	}

	public static SpriteRenderer GetNumIcon100(int num, NumType type)
	{
		SpriteRenderer result;
		if (num < 100)
		{
			result = ResourceManagerBase.GetNullSprite();
		}
		else
		{
			result = GameDataUtils.GetNumIcon(num / 100, type);
		}
		return result;
	}

	public static SpriteRenderer GetNumIcon10(int level, NumType type)
	{
		SpriteRenderer result;
		if (level >= 100)
		{
			int num = level % 100 / 10;
			result = GameDataUtils.GetNumIcon(num, type);
		}
		else if (level >= 10 && level < 100)
		{
			result = GameDataUtils.GetNumIcon(level / 10, type);
		}
		else
		{
			result = ResourceManagerBase.GetNullSprite();
		}
		return result;
	}

	public static SpriteRenderer GetNumIcon1(int level, NumType type)
	{
		SpriteRenderer numIcon;
		if (level >= 100)
		{
			int num = level % 100 % 10;
			numIcon = GameDataUtils.GetNumIcon(num, type);
		}
		else
		{
			numIcon = GameDataUtils.GetNumIcon(level % 10, type);
		}
		return numIcon;
	}

	public static bool IsCodeIconExist(string name)
	{
		return true;
	}

	public static void SetItem(int cfgId, Image itemFrame, Image itemIcon, Text itemName = null, bool showNameColor = true)
	{
		ResourceManager.SetSprite(itemFrame, GameDataUtils.GetItemFrame(cfgId));
		ResourceManager.SetSprite(itemIcon, GameDataUtils.GetItemIcon(cfgId));
		if (itemName != null)
		{
			itemName.set_text(GameDataUtils.GetItemName(cfgId, showNameColor, 0L));
		}
	}

	public static void SetItem(Items dataItem, Image itemFrame, Image itemIcon, Text itemName = null, bool showNameColor = true)
	{
		ResourceManager.SetSprite(itemFrame, GameDataUtils.GetItemFrame(dataItem));
		ResourceManager.SetSprite(itemIcon, GameDataUtils.GetItemIcon(dataItem));
		if (itemName != null)
		{
			itemName.set_text(GameDataUtils.GetItemName(dataItem, showNameColor));
		}
	}

	public static Dictionary<string, Color> GetTextColorByQuality(int quality)
	{
		Dictionary<string, Color> dictionary = new Dictionary<string, Color>();
		switch (quality)
		{
		case 2:
			dictionary.Add("TextColor", GameDataUtils.textColor1);
			dictionary.Add("TextOutlineColor", GameDataUtils.textOutlineColor1);
			return dictionary;
		case 3:
			dictionary.Add("TextColor", GameDataUtils.textColor2);
			dictionary.Add("TextOutlineColor", GameDataUtils.textOutlineColor2);
			return dictionary;
		case 4:
			dictionary.Add("TextColor", GameDataUtils.textColor3);
			dictionary.Add("TextOutlineColor", GameDataUtils.textOutlineColor3);
			return dictionary;
		case 5:
			dictionary.Add("TextColor", GameDataUtils.textColor4);
			dictionary.Add("TextOutlineColor", GameDataUtils.textOutlineColor4);
			return dictionary;
		}
		dictionary.Add("TextColor", GameDataUtils.textColor5);
		dictionary.Add("TextOutlineColor", GameDataUtils.textOutlineColor5);
		return dictionary;
	}

	public static SpriteRenderer GetItemFrame(int cfgId)
	{
		Items dataItem = DataReader<Items>.Get(cfgId);
		return GameDataUtils.GetItemFrame(dataItem);
	}

	public static SpriteRenderer GetItemFrame(Items dataItem)
	{
		if (dataItem != null)
		{
			return GameDataUtils.GetItemFrameByColor(dataItem.color);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetItemFrameByColor(int color)
	{
		return ResourceManager.GetIconSprite(GameDataUtils.GetItemFrameName(color));
	}

	public static SpriteRenderer GetFrameOfFragment(int quality)
	{
		quality = Mathf.Clamp(quality, 1, 6);
		return ResourceManager.GetIconSprite("fragment_" + quality);
	}

	public static string GetItemFrameName(int quality)
	{
		string result = string.Empty;
		if (quality == 1)
		{
			result = "frame_icon_white";
		}
		else if (quality == 2)
		{
			result = "frame_icon_green";
		}
		else if (quality == 3)
		{
			result = "frame_icon_blue";
		}
		else if (quality == 4)
		{
			result = "frame_icon_purple";
		}
		else if (quality == 5)
		{
			result = "frame_icon_orange";
		}
		else if (quality == 6)
		{
			result = "frame_icon_yellow";
		}
		else
		{
			result = "frame_icon_white";
		}
		return result;
	}

	public static SpriteRenderer GetItemIcon(int cfgId)
	{
		Items dataItem = DataReader<Items>.Get(cfgId);
		return GameDataUtils.GetItemIcon(dataItem);
	}

	public static SpriteRenderer GetItemIcon(Items dataItem)
	{
		if (dataItem != null)
		{
			return GameDataUtils.GetIcon(dataItem.icon);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetItemLitterIcon(int cfgId)
	{
		Items dataItem = DataReader<Items>.Get(cfgId);
		return GameDataUtils.GetItemLitterIcon(dataItem);
	}

	public static SpriteRenderer GetItemLitterIcon(Items dataItem)
	{
		if (dataItem != null)
		{
			return GameDataUtils.GetIcon(dataItem.littleIcon);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static string GetItemDesc1(int cfgId)
	{
		Items dataItem = DataReader<Items>.Get(cfgId);
		return GameDataUtils.GetItemDesc1(dataItem);
	}

	public static string GetItemDesc1(Items dataItem)
	{
		string result = string.Empty;
		if (dataItem != null)
		{
			result = GameDataUtils.GetChineseContent(dataItem.describeId1, false);
		}
		return result;
	}

	public static string GetItemDescWithTab(Items dataItem, string color = "")
	{
		string text = string.Empty;
		text = ((dataItem.describeId1 <= 0) ? GameDataUtils.DEFAULT_CHINESE : GameDataUtils.GetChineseContent(dataItem.describeId1, false));
		if (dataItem.tab == 5)
		{
			string canEnchantmentPosDesc = EquipGlobal.GetCanEnchantmentPosDesc(dataItem.id);
			text += "\n";
			text += "\n";
			if (canEnchantmentPosDesc != string.Empty)
			{
				text = text + "可用部位：" + canEnchantmentPosDesc;
			}
		}
		if (!string.IsNullOrEmpty(color))
		{
			text = TextColorMgr.GetColor(text, color, string.Empty);
		}
		return text;
	}

	public static string GetEquipItemNameAndLV(int cfgID, bool colorShow = false)
	{
		Items items = DataReader<Items>.Get(cfgID);
		if (items == null)
		{
			return string.Empty;
		}
		string text = string.Empty;
		if (items.firstType != 1)
		{
			text = GameDataUtils.GetItemName(cfgID, colorShow, 0L);
		}
		else
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgID);
			text = GameDataUtils.GetItemName(cfgID, colorShow, 0L);
			if (zZhuangBeiPeiZhiBiao != null)
			{
				if (EntityWorld.Instance.EntSelf.Lv >= zZhuangBeiPeiZhiBiao.level)
				{
					text = text + "  LV." + zZhuangBeiPeiZhiBiao.level;
				}
				else
				{
					text += string.Format("<color=#ff0000>  LV.{0}</color>", zZhuangBeiPeiZhiBiao.level);
				}
			}
		}
		return text;
	}

	public static string GetItemName(int cfgId, bool colorShow = true, long num = 0L)
	{
		Items dataItem = DataReader<Items>.Get(cfgId);
		if (num <= 0L)
		{
			return GameDataUtils.GetItemName(dataItem, colorShow);
		}
		return GameDataUtils.GetItemNameAndNum(dataItem, colorShow, num);
	}

	public static string GetItemName(Items dataItem, bool colorShow = true)
	{
		if (dataItem == null)
		{
			return string.Empty;
		}
		if (colorShow)
		{
			return TextColorMgr.GetColorByQuality(GameDataUtils.GetChineseContent(dataItem.name, false), dataItem.color);
		}
		return GameDataUtils.GetChineseContent(dataItem.name, false);
	}

	public static string GetItemNameAndNum(Items dataItem, bool colorShow = true, long num = 0L)
	{
		if (dataItem == null)
		{
			return string.Empty;
		}
		if (colorShow)
		{
			if (num <= 0L)
			{
				return TextColorMgr.GetColorByQuality(GameDataUtils.GetChineseContent(dataItem.name, false), dataItem.color);
			}
			return TextColorMgr.GetColorByQuality(GameDataUtils.GetChineseContent(dataItem.name, false) + "x" + num, dataItem.color);
		}
		else
		{
			if (num <= 0L)
			{
				return GameDataUtils.GetChineseContent(dataItem.name, false);
			}
			return GameDataUtils.GetChineseContent(dataItem.name, false) + "x" + num;
		}
	}

	public static string GetItemNameCustom(Items dataItem, string itemName)
	{
		if (dataItem != null)
		{
			return TextColorMgr.GetColorByQuality(itemName, dataItem.color);
		}
		return itemName;
	}

	public static string GetItemProfession(int cfgId)
	{
		Items dataItem = DataReader<Items>.Get(cfgId);
		return GameDataUtils.GetItemProfession(dataItem);
	}

	public static string GetItemProfession(Items dataItem)
	{
		if (dataItem.career == 0 || dataItem.career == 999)
		{
			return GameDataUtils.GetChineseContent(514099, false);
		}
		if (dataItem.career == EntityWorld.Instance.EntSelf.TypeID)
		{
			return TextColorMgr.GetColor(UIUtils.GetRoleName(dataItem.career), "A55A41", string.Empty);
		}
		return TextColorMgr.GetColorByID(UIUtils.GetRoleName(dataItem.career), 1000007);
	}

	public static List<int> GetAllMonsterIDByMonsterLibraryID(int libraryID)
	{
		List<int> list = new List<int>();
		List<MonsterLibrary> dataList = DataReader<MonsterLibrary>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).librariesId == libraryID && dataList.get_Item(i).unlockLv <= EntityWorld.Instance.EntSelf.Lv)
			{
				list.Add(dataList.get_Item(i).monsterID);
			}
		}
		return list;
	}

	public static string GetFashionIDByCommodityID(int commodityID)
	{
		List<ShiZhuangXiTong> dataList = DataReader<ShiZhuangXiTong>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).mallID == commodityID)
			{
				return dataList.get_Item(i).ID;
			}
		}
		return string.Empty;
	}

	public static string SplitString4Dot0(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return string.Empty;
		}
		if (name.get_Length() >= GameDataUtils.Suffix_Dot0.get_Length() && name.Substring(name.get_Length() - GameDataUtils.Suffix_Dot0.get_Length()).Equals(GameDataUtils.Suffix_Dot0))
		{
			name = name.Substring(0, name.get_Length() - GameDataUtils.Suffix_Dot0.get_Length());
		}
		return name;
	}
}
