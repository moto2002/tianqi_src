using GameData;
using Package;
using System;
using System.Collections.Generic;

public interface ILocalBattleData
{
	bool IsEnableCalculate
	{
		get;
		set;
	}

	void Update(float deltaTime);

	void AddGlobalBuff(EntityParent target);

	void AppClearBuff(long targetID);

	bool CheckBuffByTargetIDAndBuffID(long targetID, int buffID);

	bool CheckBuffTypeContainOther(Buff buffData, long targetID);

	List<ClientDrvBuffInfo> MakeClientDrvBuffInfo(long id);

	void AppClearFuse(long targetID, bool isDeadDefuse);
}
