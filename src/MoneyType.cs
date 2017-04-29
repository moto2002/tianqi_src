using GameData;
using System;
using UnityEngine;

public class MoneyType
{
	public const int Diamond = 1;

	public const int Gold = 2;

	public const int CompetitiveCurrency = 3;

	public const int ExpeditionCurrency = 4;

	public const int GuildContribution = 5;

	public const int GuildFund = 6;

	public const int EqiupEssence = 7;

	public const int GuildStoragePoint = 19;

	public static long GetNum(int moneyType)
	{
		long result = 0L;
		switch (moneyType)
		{
		case 1:
			result = (long)EntityWorld.Instance.EntSelf.Diamond;
			break;
		case 2:
			result = EntityWorld.Instance.EntSelf.Gold;
			break;
		case 3:
			result = (long)EntityWorld.Instance.EntSelf.CompetitiveCurrency;
			break;
		case 4:
			break;
		case 5:
			if (GuildManager.Instance.MyMemberInfo != null)
			{
				result = (long)GuildManager.Instance.MyMemberInfo.contribution;
			}
			break;
		case 6:
			if (GuildManager.Instance.MyGuildnfo != null)
			{
				result = (long)GuildManager.Instance.MyGuildnfo.guildFund;
			}
			break;
		case 7:
			if (GuildManager.Instance.MyGuildnfo != null)
			{
				result = GuildManager.Instance.MyGuildnfo.equipEssence;
			}
			break;
		default:
			if (moneyType == 19)
			{
				if (GuildStorageManager.Instance.GuildStoragePersonalInfo != null)
				{
					result = (long)GuildStorageManager.Instance.GuildStoragePersonalInfo.points;
				}
			}
			break;
		}
		return result;
	}

	public static SpriteRenderer GetIcon(int moneyType)
	{
		if (DataReader<HuoBi>.Contains(moneyType))
		{
			HuoBi huoBi = DataReader<HuoBi>.Get(moneyType);
			return GameDataUtils.GetItemIcon(huoBi.items);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static string GetName(int moneyType)
	{
		string result = "***";
		HuoBi huoBi = DataReader<HuoBi>.Get(moneyType);
		if (huoBi != null)
		{
			result = GameDataUtils.GetItemName(huoBi.items, true, 0L);
		}
		return result;
	}

	public static int GetItemId(int moneyType)
	{
		HuoBi huoBi = DataReader<HuoBi>.Get(moneyType);
		if (huoBi != null)
		{
			return huoBi.items;
		}
		return 0;
	}
}
