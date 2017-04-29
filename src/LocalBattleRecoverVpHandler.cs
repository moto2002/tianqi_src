using GameData;
using System;
using System.Collections.Generic;

public class LocalBattleRecoverVpHandler
{
	public static void UpdateVp(float deltaTime)
	{
		List<EntityParent> values = EntityWorld.Instance.AllEntities.Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			LocalBattleRecoverVpHandler.RecoverVp(values.get_Item(i), deltaTime);
		}
	}

	protected static void RecoverVp(EntityParent target, float deltaTime)
	{
		if (target.Vp == target.RealVpLmt)
		{
			return;
		}
		int num = (int)target.BattleBaseAttrs.TryAddValue(AttrType.Vp, (long)((int)((float)((!target.IsWeak) ? target.IdleVpResume : (target.VpResume + target.IdleVpResume)) * deltaTime)));
		if (num > target.RealVpLmt)
		{
			num = target.RealVpLmt;
		}
		target.SetValue(AttrType.Vp, num, true);
	}
}
