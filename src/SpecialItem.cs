using System;

public class SpecialItem
{
	public const int Exp = 1;

	public const int Gold = 2;

	public const int Diamond = 3;

	public const int Honor = 5;

	public const int SkillPoint = 11;

	public const int GuildFund = 13;

	public static bool IsSpecial(int itemId)
	{
		return itemId == 1 || itemId == 2 || itemId == 3;
	}
}
