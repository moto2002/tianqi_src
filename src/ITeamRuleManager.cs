using Package;
using System;
using System.Collections.Generic;

public interface ITeamRuleManager
{
	void OnMatchRes(int countDown, bool isOrder = false, Action callBack = null);

	void OnChallengeRes();

	void OnMakeTeam(DungeonType.ENUM dungeonType, List<int> dungeonParams = null, int systemID = 0);

	void OnMatchFailedCallBack();
}
