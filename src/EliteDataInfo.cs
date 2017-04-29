using Package;
using System;
using System.Collections.Generic;

public class EliteDataInfo
{
	public int BossID;

	public int ArenaID;

	public int BossIconID;

	public int TaskID;

	public bool isOpen;

	public List<int> cfgIDList;

	public Dictionary<int, EliteCopyInfo> eliteCopyInfoDic;

	public EliteDataInfo(int bossID)
	{
		this.BossID = bossID;
		this.cfgIDList = new List<int>();
		this.eliteCopyInfoDic = new Dictionary<int, EliteCopyInfo>();
	}
}
