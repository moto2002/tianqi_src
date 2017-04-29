using Package;
using System;

public class BeInviteData
{
	public long TeamID;

	public string TeamName;

	public MemberResume TeamMemberResume;

	public BeInviteData(long teamID, string teamName)
	{
		this.TeamID = teamID;
		this.TeamName = teamName;
	}
}
