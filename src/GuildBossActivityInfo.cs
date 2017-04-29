using System;
using System.Collections.Generic;

public class GuildBossActivityInfo
{
	public bool IsChallenging;

	public int GuildBossID;

	public long GuildBossCurrentBlood;

	public long GuildBossTotalBlood = 1L;

	public int GuildBossToEndCd;

	public int GuildBossLv;

	public int GuildBossOpenCD;

	public int CleanCDTimes;

	public int CanKillBossCD;

	public int WillChallengeBossTimes;

	public int RemainCallBossTimes;

	public List<GuildBossClientHurtRankingInfo> HurtRankingList;

	public GuildBossClientHurtRankingInfo FinalHurtInfo;
}
