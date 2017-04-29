using System;

public class GuildMemberInfoInGuildWarScene
{
	public long RoleID;

	public int Rank;

	public string RoleName;

	public int RoleLv;

	public long RoleFighting;

	public int ResourceNum;

	public int Status;

	public GuildMemberInfoInGuildWarScene(long roleID)
	{
		this.RoleID = roleID;
	}
}
