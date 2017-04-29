using Package;
using System;
using System.Collections.Generic;

public class BossItemInfo
{
	public int bossId;

	public int survivalBossCount;

	public int nextRefreshSec;

	public bool trackFlag;

	public List<Pos> pos = new List<Pos>();

	public void UpdateInfo(BossLabelInfo info)
	{
		this.survivalBossCount = info.survivalBossNum;
		this.nextRefreshSec = info.nextRemainTimeSec;
		this.trackFlag = info.traceFlag;
		this.pos.Clear();
		this.pos.AddRange(info.pos);
	}

	public void ClearInfo()
	{
		this.survivalBossCount = 0;
		this.nextRefreshSec = 0;
		this.trackFlag = false;
		this.pos.Clear();
	}
}
