using System;

public class GuildResourceInfo
{
	public long GuildID;

	public string GuildName;

	public int GuildMemberNum;

	public int TotalResourceNum;

	public int MaxResourceNum;

	public bool IsLeft;

	public GuildResourceInfo(long guildID, string guildName, int guildMemberNum, int totalResourceNum, int maxResourceNum, bool isLeft)
	{
		this.GuildID = guildID;
		this.GuildName = guildName;
		this.GuildMemberNum = guildMemberNum;
		this.TotalResourceNum = totalResourceNum;
		this.MaxResourceNum = maxResourceNum;
		this.IsLeft = isLeft;
	}
}
