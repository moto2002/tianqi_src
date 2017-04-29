using System;

public class StrongerInfoData
{
	public StrongerType Type;

	public long Fighting;

	public float Percent;

	public int SystemID;

	public StrongerInfoData(StrongerType type, int systemID)
	{
		this.Type = type;
		this.SystemID = systemID;
	}
}
