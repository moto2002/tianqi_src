using Package;
using System;
using System.Collections.Generic;

public class TeamData
{
	public long TeamID;

	public string TeamName;

	public long LeaderID;

	public int MinLV;

	public int MaxLV;

	public bool IsAutoAgree;

	public DungeonType.ENUM TargetDungeonType;

	public List<int> ChallengeIDParams;

	private List<MemberResume> teamRoleList;

	public List<MemberResume> TeamRoleList
	{
		get
		{
			if (this.teamRoleList == null)
			{
				this.teamRoleList = new List<MemberResume>();
			}
			return this.teamRoleList;
		}
		set
		{
			this.teamRoleList = value;
		}
	}

	public TeamData(long teamID, long leaderID)
	{
		this.TeamID = teamID;
		this.LeaderID = leaderID;
		this.TeamRoleList = new List<MemberResume>();
	}

	public void RemoveRoleMemberByID(long roleID)
	{
		if (this.teamRoleList == null)
		{
			return;
		}
		int num = this.teamRoleList.FindIndex((MemberResume a) => a.roleId == roleID);
		if (num >= 0)
		{
			this.teamRoleList.RemoveAt(num);
		}
		this.PutTeamLeaderToFirst();
	}

	public void Add2RoleMemberList(MemberResume memberResume)
	{
		if (this.teamRoleList == null)
		{
			this.teamRoleList = new List<MemberResume>();
		}
		int num = this.teamRoleList.FindIndex((MemberResume a) => a.roleId == memberResume.roleId);
		if (num < 0)
		{
			this.teamRoleList.Add(memberResume);
		}
		this.PutTeamLeaderToFirst();
	}

	public void Update2RoleMemberList(MemberResume memberResume)
	{
		if (this.teamRoleList == null)
		{
			return;
		}
		int num = this.teamRoleList.FindIndex((MemberResume a) => a.roleId == memberResume.roleId);
		if (num >= 0)
		{
			this.teamRoleList.set_Item(num, memberResume);
		}
		this.PutTeamLeaderToFirst();
	}

	public void PutTeamLeaderToFirst()
	{
		if (this.teamRoleList != null)
		{
			int num = this.teamRoleList.FindIndex((MemberResume a) => a.roleId == this.LeaderID);
			if (num > 0)
			{
				MemberResume memberResume = this.teamRoleList.get_Item(num);
				this.teamRoleList.set_Item(num, this.teamRoleList.get_Item(0));
				this.teamRoleList.set_Item(0, memberResume);
			}
		}
	}
}
