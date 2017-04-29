using System;
using System.Collections.Generic;

public class LocalBattleEffectReliveHandler
{
	public static void AppRelive(List<long> effectTargetIDs, bool isCommunicateMix)
	{
		if (isCommunicateMix)
		{
			return;
		}
		for (int i = 0; i < effectTargetIDs.get_Count(); i++)
		{
			LocalBattleEffectReliveHandler.AppRelive(effectTargetIDs.get_Item(i), isCommunicateMix);
		}
	}

	protected static void AppRelive(long targetID, bool isCommunicateMix)
	{
		EntityParent entityByID = LocalAgent.GetEntityByID(targetID);
		if (entityByID == null)
		{
			return;
		}
		if (entityByID.BattleBaseAttrs == null)
		{
			return;
		}
		if (entityByID.BackUpBattleBaseAttrs == null)
		{
			return;
		}
		entityByID.BattleBaseAttrs.AssignAllAttrs(entityByID.BackUpBattleBaseAttrs);
		LocalAgent.SetSpiritCurHp(entityByID, entityByID.RealHpLmt, isCommunicateMix);
		entityByID.Hp = entityByID.RealHpLmt;
		entityByID.IsDead = false;
	}
}
