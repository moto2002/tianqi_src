using System;
using System.Collections.Generic;

public interface ITeamMakable
{
	void OnNotifyMakeTeam(int id, string arg);

	void SetTeamUI(int instanceNameIcon, string backgroundIcon, string label1, int descChineseID, int challengeTimes, List<int> items, Action beginCallBack = null, Action returnCallBack = null);
}
