using GameData;
using Package;
using System;
using System.Collections.Generic;

public class LocalBattleEffectCalculatorTreatHandler : LocalBattleEffectCalculatorHandler
{
	public static void AppTheat(Effect effectData, EntityParent caster, List<long> effectTargetIDs, XPoint basePoint, int skillID, bool isAddEffect, bool isCommunicateMix)
	{
		if (!LocalAgent.GetEntityCalculatable(caster, isCommunicateMix))
		{
			return;
		}
		XDict<GameData.AttrType, long> effectCasterTempAttr = LocalBattleEffectCalculatorHandler.GetEffectCasterTempAttr(effectData, caster, skillID);
		bool flag = isAddEffect && isCommunicateMix;
		for (int i = 0; i < effectTargetIDs.get_Count(); i++)
		{
			EntityParent entityByID = LocalAgent.GetEntityByID(effectTargetIDs.get_Item(i));
			if (LocalAgent.GetEntityIsCurable(entityByID, isCommunicateMix))
			{
				if (!entityByID.IsUnconspicuous || effectData.forcePickup != 0)
				{
					long num = BattleCalculator.CalculateTreatment(caster.BattleBaseAttrs, entityByID.BattleBaseAttrs, entityByID.IsEntitySelfType || entityByID.IsEntityPlayerType, effectCasterTempAttr, null);
					if (num != 0L)
					{
						List<ClientDrvBuffInfo> casterBuffInfo = null;
						List<ClientDrvBuffInfo> targetBuffInfo = null;
						if (isCommunicateMix)
						{
							casterBuffInfo = LocalAgent.MakeClientDrvBuffInfo(caster.ID);
							targetBuffInfo = LocalAgent.MakeClientDrvBuffInfo(effectTargetIDs.get_Item(i));
						}
						long num2 = LocalAgent.GetSpiritCurHp(entityByID, isCommunicateMix) + num;
						if (num2 > entityByID.RealHpLmt)
						{
							num2 = entityByID.RealHpLmt;
						}
						Pos pos = null;
						if (caster.Actor)
						{
							pos = new Pos();
							pos.x = caster.Actor.FixTransform.get_position().x * 100f;
							pos.y = caster.Actor.FixTransform.get_position().z * 100f;
						}
						LocalAgent.SetSpiritCurHp(entityByID, num2, isCommunicateMix);
						if (isCommunicateMix)
						{
							GlobalBattleNetwork.Instance.SendClientDriveBattleEffectDamage(caster.ID, effectTargetIDs.get_Item(i), caster.Hp, num2, num, skillID, effectData.id, flag, true, casterBuffInfo, targetBuffInfo, basePoint, new List<long>(), string.Concat(new object[]
							{
								caster.TryAddValue(GameData.AttrType.SkillTreatScaleBOAtk, effectCasterTempAttr),
								"_",
								caster.TryAddValue(GameData.AttrType.SkillTreatScaleBOHpLmt, effectCasterTempAttr),
								"_",
								caster.TryAddValue(GameData.AttrType.SkillIgnoreDefenceHurt, effectCasterTempAttr)
							}), false);
						}
						LocalBattleProtocolSimulator.SendTreat(effectTargetIDs.get_Item(i), (GameObjectType.ENUM)entityByID.WrapType, caster.ID, (GameObjectType.ENUM)caster.WrapType, BattleAction_Treat.TreatSrcType.Treat, num, num2, pos);
						if (flag)
						{
							flag = false;
						}
					}
				}
			}
		}
	}
}
